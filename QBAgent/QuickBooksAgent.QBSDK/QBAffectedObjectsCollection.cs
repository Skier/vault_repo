using System;
using System.Collections.Generic;
using System.Text;

namespace QuickBooksAgent.QBSDK
{
    public class QBAffectedObjectsCollection<TDomainClass> 
        where TDomainClass:class
    {
        Dictionary<QBCommandTypeEnum, List<QBAffectedObject<TDomainClass>>> m_items = new
            Dictionary<QBCommandTypeEnum, List<QBAffectedObject<TDomainClass>>>();

        public QBAffectedObjectsCollection()
        {
            m_items.Add(QBCommandTypeEnum.Query, new List<QBAffectedObject<TDomainClass>>());
            m_items.Add(QBCommandTypeEnum.Add, new List<QBAffectedObject<TDomainClass>>());
            m_items.Add(QBCommandTypeEnum.Delete, new List<QBAffectedObject<TDomainClass>>());
            m_items.Add(QBCommandTypeEnum.Update, new List<QBAffectedObject<TDomainClass>>());
        }

        public List<QBAffectedObject<TDomainClass>> DeletedObjects
        {
            get { return m_items[QBCommandTypeEnum.Delete]; }
        }

        public List<QBAffectedObject<TDomainClass>> AddedObjects
        {
            get { return m_items[QBCommandTypeEnum.Add]; }
        }

        public List<QBAffectedObject<TDomainClass>> UpdatedObjects
        {
            get { return m_items[QBCommandTypeEnum.Update]; }
        }

        public List<QBAffectedObject<TDomainClass>> QueriedObjects
        {
            get { return m_items[QBCommandTypeEnum.Query]; }
        }


        public List<QBAffectedObject<TDomainClass>> this[QBCommandTypeEnum commandType]
        {
            get
            {
                return m_items[commandType];
            }
        }


        public TDomainClass FindDomainObject(QBCommandTypeEnum qBCommandTypeEnum, int requestId)
        {
            QBAffectedObject<TDomainClass> affectedObject =
                    FindAffectedObject(qBCommandTypeEnum, requestId);

            return affectedObject != null ? affectedObject.DomainObject : affectedObject;
        }


        public QBAffectedObject<TDomainClass> FindAffectedObject(QBCommandTypeEnum qBCommandTypeEnum, int requestId)
        {
            foreach (QBAffectedObject<TDomainClass> affectedObject in this[qBCommandTypeEnum])
            {
                if (affectedObject.RequestId == requestId)
                    return affectedObject;
            }

            return null;
        }
    }
}
