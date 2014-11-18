using System;
using System.Collections.Generic;
using System.Text;

namespace Weborb.Samples.Ftp.Entities
{
    public class FtpDirNode
    {
        FtpDirNode m_parent;

        public FtpDirNode Parent
        {
            get { return m_parent; }
            set { m_parent = value; }
        }

        private List<FtpDirNode> m_items = new List<FtpDirNode>();

        public List<FtpDirNode> Items
        {
            get { return m_items; }
            set { m_items = value; }
        }

        private String m_name = String.Empty;

        public String Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

    }

}
