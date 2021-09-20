using System.Collections.Generic;
using System.Linq;
using System.Text;
using DapperClassGenerator.Models;

namespace DapperClassGenerator.Templates
{
    public partial class ModelTemplate
    {
        /// <summary>
        /// Entity's Namespace.
        /// </summary>
        public string NameSpace { get; }
        
        /// <summary>
        /// Use dapper contrib attributes.
        /// </summary>
        public bool UseDataAnnotations { get; }
        
        /// <summary>
        /// Table Data.
        /// </summary>
        public List<TableInfo> Tables { get; }

        /// <summary>
        /// Creates an Instance of TableEntityTemplate.
        /// </summary>
        /// <param name="nameSpace">Entity's Namespace.</param>
        /// <param name="useDataAnnotations">Use dapper contrib - data annotation keys</param>
        /// <param name="tables">Tables list.</param>
        public ModelTemplate(string nameSpace, bool useDataAnnotations, List<TableInfo> tables)
            => (NameSpace, UseDataAnnotations, Tables) = (nameSpace, useDataAnnotations, tables);

        /// <summary>
        /// Get .NET Type Name from typeDictionary.
        /// </summary>
        /// <param name="column">Column's data.</param>
        /// <returns>
        /// Returns typeDictionary's value if found.
        /// Returns <see cref="ColumnInfo.Type"/> if not found.
        /// If the column is nullable, add "?" To the return value.
        /// </returns>
        public static string GetColumnType(ColumnInfo column)
        {
            if (column.IsNullable &&
                column.Type != "byte[]" &&
                column.Type != "string" &&
                column.Type != "Microsoft.SqlServer.Types.SqlGeography" &&
                column.Type != "Microsoft.SqlServer.Types.SqlGeometry")
            {
                return column.Type + "?";
            }

            return column.Type;
        }

        /// <summary>
        /// Get SQL query table join example.
        /// </summary>
        /// <param name="column">Column's data.</param>
        /// <returns>
        /// Returns generated query.
        /// </returns>
        public static string GetTableJoin(ColumnInfo column)
        {
            var result = new StringBuilder();
            result.Append($"{(column.IsNullable ? "LEFT" : "INNER")} JOIN {column.Relationship.TableName} B");

            foreach (var item in column.Relationship.Relationships.Select((value, i) => new { i, value }))
            {
                result.Append($" {(item.i == 0 ? "ON" : "AND")} B.{item.value.ReferenceColumnName} = A.{item.value.ParentColumnName}");
            }

            return result.ToString();
        }
    }
}