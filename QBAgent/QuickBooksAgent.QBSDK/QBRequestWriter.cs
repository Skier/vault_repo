using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QuickBooksAgent.QBSDK
{
    public abstract class QBRequestWriter<TDomainClass>
        where TDomainClass:class
    {
        public QBExpectedResponse<TDomainClass> Process(XmlWriter xmlWriter)
        {
            QBExpectedResponse<TDomainClass> expectedResponse 
                = new QBExpectedResponse<TDomainClass>();

            OnProcess(xmlWriter, QBCommandTypeEnum.Add, expectedResponse.AddedObjects);
            OnProcess(xmlWriter, QBCommandTypeEnum.Delete, expectedResponse.DeletedObjects);
            OnProcess(xmlWriter, QBCommandTypeEnum.Update, expectedResponse.UpdatedObjects);

            return expectedResponse;
        }

        protected abstract void OnProcess(XmlWriter xmlWriter,
            QBCommandTypeEnum commandType, 
            List<QBAffectedObject<TDomainClass>> returnList);

        #region WriteAttribute

        protected void WriteAttribute(XmlWriter xmlWriter, string localName, string value)
        {
            if (value != null && value != string.Empty)
                xmlWriter.WriteAttributeString(localName, value);
        }

        #endregion

        #region WriteElement

        protected void WriteElement(XmlWriter xmlWriter, string localName, string value)
        {
            if (value != null && value != string.Empty)
                xmlWriter.WriteElementString(localName, value);
        }

        #endregion

        #region IsAllEmptyOrNull

        protected bool IsAllEmptyOrNull(params string[] values)
        {
            foreach (string value in values)
                if (value != null && value != string.Empty)
                    return false;
            return true;
        }

        #endregion
    }
}
