using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.QBSDK
{
    public class QBAffectedObject<TDomainClass>
        where TDomainClass:class
    {
        TDomainClass m_domainObject;

        public TDomainClass DomainObject
        {
            get { return m_domainObject; }
        }

        int m_requestId;

        public int RequestId
        {
            get { return m_requestId; }

        }

        private QBCommandTypeEnum m_commandType;
        public QBCommandTypeEnum CommandType
        {
            get { return m_commandType; }
            set { m_commandType = value; }
        }

        public QBAffectedObject(TDomainClass domainObject, int requestId)
        {
            m_domainObject = domainObject;
            m_requestId = requestId;
        }

        public static implicit operator TDomainClass(QBAffectedObject<TDomainClass> affectedObject)
        {
            return affectedObject != null ? affectedObject.m_domainObject : null;
        }
    }
}

