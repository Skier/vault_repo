using System;
using System.Collections.Generic;
using System.Text;

namespace Weborb.Management.CodeGen
{
    public class CodeFile:CodeItem
    {
        private string m_content;

        public string Content
        {
            get { return m_content; }
            set { m_content = value; }
        }
    }
}
