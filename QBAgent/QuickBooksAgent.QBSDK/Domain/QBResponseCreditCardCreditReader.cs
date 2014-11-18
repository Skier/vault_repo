using System;
using System.Xml;
using System.Xml.Serialization;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBResponseCreditCardCreditReader : QBResponseCreditCardReader
    {
        #region Convert

        protected override CreditCard Convert(object item)
        {
            ((CreditCardRet)item).CreditCardType = CreditCardType.Credit;

            return base.Convert(item);
        }

        #endregion

        #region ProcessResponse

        protected override void ProcessResponse(QBAffectedObject<CreditCard> item)
        {
            CreditSynchronizeHelper.ProcessCreditCardResponse(item, Items);
        }

        #endregion

        #region TargetNodeName
        protected override string TargetNodeName
        {
            get { return "CreditCardCreditRet"; }
        }
        #endregion

        #region TargetClassType
        protected override Type TargetClassType
        {
            get { return typeof(CreditCardCreditRet); }
        }
        #endregion

        #region IsRootNode

        public override bool IsRootNode(string nodeName)
        {
            return "CreditCardCreditQueryRs".Equals(nodeName)
                   || "CreditCardCreditAddRs".Equals(nodeName);
        }

        #endregion

        #region CreditCardCreditRet

        [XmlRoot("CreditCardCreditRet")]
        public class CreditCardCreditRet : CreditCardRet { }

        #endregion
        
    }
}
