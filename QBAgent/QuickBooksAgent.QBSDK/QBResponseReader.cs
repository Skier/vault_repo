using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Diagnostics;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.QBSDK
{




    public abstract class QBResponseReader
    {
        public const string NODE_END_ADD = "AddRs";
        public const string NODE_END_MOD = "ModRs";
        public const string NODE_END_QUERY = "QueryRs";

        protected List<QBResponseStatus> m_responseStatus = new List<QBResponseStatus>();

        public List<QBResponseStatus> ResponseStatus
        {
            get { return m_responseStatus; }
        }

        public abstract bool IsRootNode(string nodeName);

        public abstract bool ProcessNode(XmlTextReader xmlTextReader);

        public QBResponseStatus FindStatus(int requestId)
        {
            foreach (QBResponseStatus status in m_responseStatus)
                if (status.RequestId == requestId)
                    return status;

            throw new QBException("Status not found");
        }
    }

    public abstract class QBResponseReader<TDomainClass> : QBResponseReader
        where TDomainClass : class
    {

        QBAffectedObjectsCollection<TDomainClass> m_items
            = new QBAffectedObjectsCollection<TDomainClass>();

        public QBAffectedObjectsCollection<TDomainClass> Items
        {
            get { return m_items; }
        }

        QBCommandTypeEnum m_currentCommand;

        int m_requestId;
        
        public override bool ProcessNode(XmlTextReader xmlTextReader)
        {
            Debug.WriteLine("QBResponseReader::ProcessNode " + xmlTextReader.Name);

            if (xmlTextReader.Name.EndsWith(NODE_END_ADD))
                m_currentCommand = QBCommandTypeEnum.Add;
            else if (xmlTextReader.Name.EndsWith(NODE_END_MOD))
                m_currentCommand = QBCommandTypeEnum.Update;
            else if (xmlTextReader.Name.EndsWith(NODE_END_QUERY))
                m_currentCommand = QBCommandTypeEnum.Query;
            else
                m_currentCommand = QBCommandTypeEnum.Unknown;

            if (m_currentCommand != QBCommandTypeEnum.Unknown)
            {
                m_requestId = int.Parse(xmlTextReader.GetAttribute("requestID"));
                QBResponseStatus status = new QBResponseStatus(xmlTextReader, m_currentCommand);
                m_responseStatus.Add(status);
                if (status.IsError)
                {
                    string objectName = string.Empty;                    
                    
                    try
                    {
                        string className = xmlTextReader.Name;
                        className = className.Replace(NODE_END_ADD, string.Empty);
                        className = className.Replace(NODE_END_MOD, string.Empty);
                        className = className.Replace(NODE_END_QUERY, string.Empty);

                        Type type = Type.GetType("QuickBooksAgent.Domain." + className + ",qagent.domain");
                        MethodInfo method = type.GetMethod("FindByPrimaryKey");
                        Object domainObject = method.Invoke(null, new Object[] {m_requestId});
                        objectName = domainObject.ToString();
                    }
                    catch (Exception) {}
                    m_responseStatus[m_responseStatus.Count - 1].DomainObjects.Add(objectName);                    
                }
                
            }

            string rootNode = xmlTextReader.Name;

            while (!(xmlTextReader.Name == rootNode && xmlTextReader.NodeType == XmlNodeType.EndElement))
            {                                
                if (TargetNodeName.Equals(xmlTextReader.Name) && xmlTextReader.NodeType == XmlNodeType.Element)
                {                                        
                    XmlSerializer xmlSerializer = new XmlSerializer(TargetClassType);
                    Object deserializedObject = xmlSerializer.Deserialize(xmlTextReader);

                    if (m_currentCommand != QBCommandTypeEnum.Unknown)
                    {
                        QBAffectedObject<TDomainClass> item = new QBAffectedObject<TDomainClass>(
                                Convert(deserializedObject),
                                m_requestId);

                        item.CommandType = m_currentCommand;
                        ProcessResponse(item);

                        m_responseStatus[m_responseStatus.Count - 1].DomainObjects.Add(item.DomainObject.ToString());
                    }
                    
                    continue;
                }

                xmlTextReader.Read();

                if ((xmlTextReader.Name.EndsWith(NODE_END_ADD) || xmlTextReader.Name.EndsWith(NODE_END_MOD)
                    || xmlTextReader.Name.EndsWith(NODE_END_QUERY) || xmlTextReader.Name.EndsWith("QBXMLMsgsRs")) 
                    && !xmlTextReader.Name.Equals(rootNode))
                {
                    break;
                }                
            }
                            
            return true;
        }

        protected abstract String TargetNodeName { get;}

        protected abstract Type TargetClassType { get;}

        protected abstract TDomainClass Convert(Object item);

        protected abstract void ProcessResponse(QBAffectedObject<TDomainClass> item);
        
        
    }
}
