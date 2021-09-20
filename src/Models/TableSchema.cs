namespace DapperClassGenerator.Models
{
    public sealed class TableSchema
    {
        public string SchemaName { get; set; }
        public string TableName { get; set; }
        public string TableType { get; set; }
        public string ColumnName { get; set; }
        public int ColumnPosition { get; set; }
        public bool IsNullable { get; set; }
        public string DataType { get; set; }
        public int MaximumLength { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsComputed { get; set; }
        
        public string RelationshipXML { get; set; }
    }
}