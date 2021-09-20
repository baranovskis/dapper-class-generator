namespace DapperClassGenerator.Models
{
    public sealed class ColumnInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsNullable { get; set; }
        public int MaximumLength { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsComputed { get; set; }
        public bool IsPrimary { get; set; }
        public string CleanName { get; set; }
        public RelationshipRoot Relationship { get; set; }
    }
}
