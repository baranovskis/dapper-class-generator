using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Dapper;
using DapperClassGenerator.Models;
using Humanizer;

namespace DapperClassGenerator
{
    public static class DbReader 
    {
        private static string GetTablesInfoSql => @"SELECT
			SchemaName = SCHEMA_NAME(t.schema_id),
			TableName = t.[name],
			ColumnName = c.[name],
			ColumnPosition = c.column_id,
			IsNullable = c.is_nullable, 
			DataType = ty.[name], 
			MaximumLength = COLUMNPROPERTY(c.object_id, c.[name], 'CharMaxLen'),
			IsIdentity = c.is_identity,
			IsComputed = c.is_computed,
			IsPrimary = ISNULL(pk.is_primary_key, 0),
			RelationshipXML = fk.Relationship
		FROM sys.tables t
		INNER JOIN sys.columns c ON c.object_id = t.object_id
		LEFT JOIN sys.types ty ON ty.user_type_id = c.user_type_id
		OUTER APPLY (
			SELECT i.is_primary_key
			FROM sys.indexes i
			INNER JOIN sys.index_columns ic ON ic.object_id = i.object_id 
				AND ic.index_id = i.index_id
				AND ic.column_id = c.column_id
			WHERE i.object_id = t.object_id
				AND i.is_primary_key = 1
		) pk
		OUTER APPLY (
			SELECT 
				TableName = t2.[name],
				Relationships = (
					SELECT
						ParentColumnName = pc.[name],
						ReferenceColumnName = rc.[name]
					FROM sys.foreign_key_columns fkca
					INNER JOIN sys.columns pc ON fkca.parent_column_id = pc.column_id 
						AND fkca.parent_object_id = pc.object_id
					INNER JOIN sys.columns rc ON fkca.referenced_column_id = rc.column_id 
						AND fkca.referenced_object_id = rc.object_id
					WHERE fkca.constraint_object_id = fkc.constraint_object_id
						AND fkca.parent_object_id = fkc.parent_object_id
					FOR XML PATH('Relationship'), TYPE
				)
			FROM sys.foreign_key_columns fkc
			INNER JOIN sys.tables t2 ON t2.object_id = fkc.referenced_object_id
			WHERE fkc.parent_column_id = c.column_id
				AND fkc.parent_object_id = c.object_id
				AND fkc.constraint_column_id = 1
			FOR XML PATH('RelationshipRoot')
		) fk (Relationship)
		ORDER BY SchemaName, TableName, ColumnPosition";
        
        public static List<TableInfo> ReadSchema(string connectionString)
        {
            var result = new List<TableInfo>();

            using var connection = new SqlConnection(connectionString);
            var queryResult = connection.Query<TableSchema>(GetTablesInfoSql)
	            .ToList();

            var groupedTables = queryResult.GroupBy(x => new
                {
                    x.SchemaName,
                    x.TableName,
                    x.TableType
                })
	            .ToList();

            foreach (var table in groupedTables)
            {
                var tbl = new TableInfo
                {
                    Name = table.Key.TableName,
                    Schema = table.Key.SchemaName,
                    IsView = string.Compare(table.Key.TableType, "View", StringComparison.OrdinalIgnoreCase) == 0,
                };

                tbl.CleanName = Helpers.CleanUp(tbl.Name)
	                ?.Pascalize()
	                ?.Singularize();

                foreach (var res in queryResult
                    .Where(x => x.TableName == tbl.Name && x.SchemaName == tbl.Schema)
                    .OrderBy(x => x.ColumnPosition))
                {
                    var col = new ColumnInfo
                    {
                        Name = res.ColumnName,
                        Type = Helpers.ConvertPropertyType(res.DataType),
                        IsIdentity = res.IsIdentity,
                        IsPrimary = res.IsPrimary,
                        IsComputed = res.IsComputed,
                        IsNullable = res.IsNullable,
                        MaximumLength = res.MaximumLength
                    };

                    col.CleanName = Helpers.CleanUp(col.Name)
	                    ?.Pascalize();

                    if (!string.IsNullOrEmpty(res.RelationshipXML))
                    {
	                    var serializer = new XmlSerializer(typeof(RelationshipRoot));
	                    using var sr = new StringReader(res.RelationshipXML);
	                    col.Relationship = serializer.Deserialize(sr) as RelationshipRoot;
                    }

                    if (col.Relationship != null)
                    {
	                    col.Relationship.CleanName = Helpers.CleanUp(col.Relationship.TableName)
		                    ?.Pascalize()
		                    ?.Singularize();
                    }

                    tbl.Columns.Add(col);
                }
                    
                result.Add(tbl);
            }

            return result;
        }
    }
}
