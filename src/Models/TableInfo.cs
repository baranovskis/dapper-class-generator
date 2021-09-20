using System.Collections.Generic;

namespace DapperClassGenerator.Models
{
    public sealed class TableInfo
    {
        public string Name { get; set; }
        public string Schema { get; set; }
        public bool IsView { get; set; }
        public string CleanName { get; set; }
        public List<ColumnInfo> Columns { get; set; }

        public TableInfo()
        {
            Columns = new List<ColumnInfo>();
        }
    }
}
