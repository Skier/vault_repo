using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Xml.Schema;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.QBSDK
{
    public class StreamSkipDtd : StreamReader
    {
        public StreamSkipDtd(string path) : base(path) { }

        private bool m_isCheckDtd = true;

        public override int Read(char[] buffer, int index, int count)
        {
            int result = base.Read(buffer, index, count);
            
            if (m_isCheckDtd)
            {                
                for (int i = 0; i < buffer.Length; i++)
                {
                    if (buffer[i] == '<' && buffer[i + 1] == '!')
                    {
                        int j = i;
                        while (buffer[j] != '>')
                        {
                            buffer[j] = ' ';
                            j++;
                        }
                        buffer[j] = ' ';                            
                        break;
                    }
                }
                
                m_isCheckDtd = false;
            }

            return result;
        }
    }
    
    public class QuickBooksHeaderException : Exception
    {
        private int m_code;
        public int Code
        {
            get { return m_code; }
            set { m_code = value; }
        }

        private string m_severity;
        public string Severity
        {
            get { return m_severity; }
            set { m_severity = value; }
        }

        public QuickBooksHeaderException(int code, string severity, string message)
            : base(message)
        {
            m_code = code;
            m_severity = severity;
        }
    }
    
    public class QBResponse
    {
        const string ROOT_ELEMENT = "SignonDesktopRs";

        List<QBResponseReader> m_readers;

        int m_statusCode;

        public int StatusCode
        {
            get { return m_statusCode; }
            set { m_statusCode = value; }
        }

        String m_statusMessage = String.Empty;

        public String StatusMessage
        {
            get { return m_statusMessage; }
            set { m_statusMessage = value; }
        }


        String m_statusSeverity = string.Empty;

        public String StatusSeverity
        {
            get { return m_statusSeverity; }
            set { m_statusSeverity = value; }
        }

        DateTime m_syncDate;
        public DateTime SyncDate
        {
            get { return m_syncDate; }
            set { m_syncDate = value; }
        }                        

        #region Process
        
        public void Process(List<QBResponseReader> readers)
        {
            m_readers = readers;

            Host.Trace("QBResponse::Process", "Analyzing response, in detail look in response.xml file");

            using (StreamSkipDtd stream = new StreamSkipDtd(Host.GetPath("response.xml")))
            {
                XmlTextReader xmlTextReader = new XmlTextReader(stream);
                xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
                xmlTextReader.MoveToContent();
                using (xmlTextReader)
                {
                    ProcessDocument(xmlTextReader);
                }                            
            }                        
        }

        private void ProcessHeader(XmlTextReader xmlTextReader)
        {
            while (!xmlTextReader.EOF)
            {
                xmlTextReader.Read();
                
                if (xmlTextReader.Name == ROOT_ELEMENT && xmlTextReader.NodeType == XmlNodeType.Element)
                {
                    m_statusCode = int.Parse(xmlTextReader.GetAttribute("statusCode"));
                    m_statusMessage = xmlTextReader.GetAttribute("statusMessage");
                    m_statusSeverity = xmlTextReader.GetAttribute("statusSeverity");
                    if (!IsSuccessResultCode)
                    {
                        xmlTextReader.Close();
                        throw new QuickBooksHeaderException(m_statusCode, m_statusSeverity, m_statusMessage);
                    }
                }

                if (xmlTextReader.Name == "ServerDateTime" && xmlTextReader.NodeType == XmlNodeType.Element)
                {
                    m_syncDate =
                        DateTime.Parse(xmlTextReader.ReadElementContentAsString(), QBDataType.USCulture);
                    return;
                }
            }                        
        }

        public bool IsSuccessResultCode
        {
            get { return !"ERROR".Equals(m_statusSeverity); }
        }
        
        public delegate void ReaderChangedHandler(QBResponseReader reader);
        public event ReaderChangedHandler ReaderChanged;


        private void ProcessDocument(XmlTextReader xmlTextReader)
        {
            ProcessHeader(xmlTextReader);

            while (!xmlTextReader.EOF)
            {
                xmlTextReader.Read();
                
                if ((xmlTextReader.Name.EndsWith("AddRs") || xmlTextReader.Name.EndsWith("ModRs") 
                    || xmlTextReader.Name.EndsWith("QueryRs")) && xmlTextReader.NodeType == XmlNodeType.Element)
                {
                    foreach (QBResponseReader reader in m_readers)
                    {
                        if (reader.IsRootNode(xmlTextReader.Name))
                        {
                            if (ReaderChanged != null)
                                ReaderChanged.Invoke(reader);
                            
                            reader.ProcessNode(xmlTextReader);
                        }
                            
                    }
                }
            }                                    
        }

        public void Process(QBResponseReader reader)
        {
            List<QBResponseReader> readers = new List<QBResponseReader>();

            readers.Add(reader);

            Process(readers);
        }
        #endregion
    }
}
