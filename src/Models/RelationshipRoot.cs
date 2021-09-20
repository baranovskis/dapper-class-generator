using System.Collections.Generic;

namespace DapperClassGenerator.Models
{
    public class RelationshipRoot
    {
        public string TableName { get; set; }
        public string CleanName { get; set; }
        public List<Relationship> Relationships { get; set; }
    }
}