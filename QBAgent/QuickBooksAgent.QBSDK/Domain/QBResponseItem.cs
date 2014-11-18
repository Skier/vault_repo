using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBResponseItem
    {
        #region ListId
        int m_listId;

        [XmlElement("ListID")]
        public int ListId
        {
            get { return m_listId; }
            set { m_listId = value; }
        }
        #endregion

        /*int m_requestId;
        [XmlElement("requestID")]
        public int RequestId
        {
            get { return m_requestId; }
            set { m_requestId = value; }
        }
        */

        #region EditSequence
        int m_editSequence;

        [XmlElement("EditSequence")]
        public int EditSequence
        {
            get { return m_editSequence; }
            set { m_editSequence = value; }
        }
        #endregion

        #region TimeCreated
        DateTime m_timeCreated;
        [XmlElement("TimeCreated")]
        public DateTime TimeCreated
        {
            get { return m_timeCreated; }
            set { m_timeCreated = value; }
        }
        #endregion

        #region TimeModified
        DateTime m_timeModified;

        [XmlElement("TimeModified")]
        public DateTime TimeModified
        {
            get { return m_timeModified; }
            set { m_timeModified = value; }
        }
        #endregion
    }
}
