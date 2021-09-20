using System.Collections.Generic;
using DapperClassGenerator.Models;

namespace DapperClassGenerator.Templates
{
    public partial class ModelValidatorTemplate
    {
        /// <summary>
        /// Entity's Namespace.
        /// </summary>
        public string NameSpace { get; }

        /// <summary>
        /// Table Data.
        /// </summary>
        public List<TableInfo> Tables { get; }

        /// <summary>
        /// Creates an Instance of TableEntityTemplate.
        /// </summary>
        /// <param name="nameSpace">Entity's Namespace.</param>
        /// <param name="tables">Tables List.</param>
        public ModelValidatorTemplate(string nameSpace, List<TableInfo> tables)
            => (NameSpace, Tables) = (nameSpace, tables);
    }
}