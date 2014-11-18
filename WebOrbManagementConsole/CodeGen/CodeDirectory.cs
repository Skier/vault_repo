using System;
using System.Collections.Generic;
using System.Text;

namespace Weborb.Management.CodeGen
{
    public class CodeDirectory:CodeItem
    {
        private List<CodeItem> m_items = new List<CodeItem>();

        public List<CodeItem> Items
        {
            get { return m_items; }
            set { m_items = value; }
        }
    }
}
