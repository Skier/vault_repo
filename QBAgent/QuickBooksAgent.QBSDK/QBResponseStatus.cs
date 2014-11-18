using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QuickBooksAgent.QBSDK
{
    public class QBResponseStatus
    {
        int m_affectedCount;

        public int AffectedCount
        {
            get { return m_affectedCount; }
            set { m_affectedCount = value; }
        }

        int m_code;

        public int Code
        {
            get { return m_code; }
        }

        String m_message;

        public String Message
        {
            get { return m_message; }
        }

        String m_severity;

        public String Severity
        {
            get { return m_severity; }
        }

        int m_requestId;

        public int RequestId
        {
            get { return m_requestId; }
        }


        public QBResponseStatus(XmlTextReader xmlTextReader, QBCommandTypeEnum commandType)
        {
            m_code = int.Parse(xmlTextReader.GetAttribute("statusCode"));

            m_message = xmlTextReader.GetAttribute("statusMessage");

            m_severity = xmlTextReader.GetAttribute("statusSeverity");

            m_requestId = int.Parse(xmlTextReader.GetAttribute("requestID"));

            m_commandType = commandType;
        }

        public bool IsError
        {
            get { return "ERROR".Equals(m_severity.ToUpper()); }
        }

        public bool IsSuccessfulResult
        {
            get { return !IsError; }
        }

        QBCommandTypeEnum m_commandType;

        public QBCommandTypeEnum CommandType
        {
          get { return m_commandType; }

        }

        List<Object> m_domainObjects = new List<Object>();

        public List<Object> DomainObjects
        {
            get { return m_domainObjects; }
        }

        public bool IsVersionConflictError
        {
            get
            {
                return m_code == 3200;
            }
        }

    }
}
