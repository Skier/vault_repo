using System;
using System.Collections.Generic;

namespace TractInc.Server.Codegen
{
    public class TableMeta
    {
        public String Name = String.Empty;

        public Dictionary<String, TableMeta> RelatedTables = new Dictionary<string,TableMeta>();

        public Dictionary<String, TableMeta> DependsTables = new Dictionary<string,TableMeta>();

        public bool IsRelated(String name)
        {


            foreach (TableMeta tableMeta in RelatedTables.Values)
            {
                if (tableMeta.Name.Equals(name)
                    || tableMeta.IsRelated(name))
                    return true;

            }

            return false;
        }

        public bool IsDepends(String name)
        {

            foreach (TableMeta tableMeta in DependsTables.Values)
            {
                if (tableMeta.Name.Equals(name)
                    || tableMeta.IsDepends(name))
                    return true;

            }

            return false;
        }

        public override string ToString()
        {
            return Name;
        }
    }

}
