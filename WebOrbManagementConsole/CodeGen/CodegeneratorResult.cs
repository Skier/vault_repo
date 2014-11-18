using System;
using System.Collections.Generic;
using System.Text;

namespace Weborb.Management.CodeGen
{
    public class CodegeneratorResult
    {
        private CodeItem m_codeItem;

        public CodeItem Result
        {
            get { return m_codeItem; }
            set { m_codeItem = value; }
        }

        private bool m_savedOnServer;

        public bool SavedOnServer
        {
            get { return m_savedOnServer; }
            set { m_savedOnServer = value; }
        }

        private String m_downloadUri;

        public String DownloadUri
        {
            get { return m_downloadUri; }
            set { m_downloadUri = value; }
        }



        private String m_info;

        public String Info
        {
            get { return m_info; }
            set { m_info = value; }
        }

        public CodegeneratorResult() { }
    }
}
