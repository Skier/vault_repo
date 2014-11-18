using System;
using System.Collections.Generic;
using System.Text;

namespace Weborb.Data.Inspector2
{
    public class ColumnInfo
    {
        public string name;
        public bool isNullable;
        public ColumnDataTypeInfo dataTypeInfo;
        public ColumnKeyType keyType;
        public bool isAutoIncrement;
        public ForeignKeyData foreignKey;
    }
}
