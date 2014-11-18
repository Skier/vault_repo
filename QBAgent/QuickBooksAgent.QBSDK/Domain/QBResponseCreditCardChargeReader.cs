using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBResponseCreditCardChargeReader : QBResponseCreditCardReader
    {
        #region Convert

        protected override CreditCard Convert(object item)
        {
            ((CreditCardRet)item).CreditCardType = CreditCardType.Charge;
            
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
            get { return "CreditCardChargeRet"; }
        }
        #endregion
        
        #region TargetClassType
        protected override Type TargetClassType
        {
            get { return typeof(CreditCardChargeRet); }
        }
        #endregion
        
        #region IsRootNode

        public override bool IsRootNode(string nodeName)
        {
            return "CreditCardChargeQueryRs".Equals(nodeName)
                   || "CreditCardChargeAddRs".Equals(nodeName);
        }

        #endregion                        

        #region CreditCardChargeRet

        [XmlRoot("CreditCardChargeRet")]
        public class CreditCardChargeRet : CreditCardRet{}

        #endregion
    }

    #region CreditSynchronizeHelper

    public class CreditSynchronizeHelper
    {
        #region ProcessCreditCardResponse

        public static void ProcessCreditCardResponse(QBAffectedObject<CreditCard> item, QBAffectedObjectsCollection<CreditCard> items)
        {
            #region Process added Credit Cards

            if (item.CommandType == QBCommandTypeEnum.Add)
            {                
                CreditCard creditCard = item.DomainObject;
                
                QBAffectedObject<CreditCard> expectedCard;
                try
                {
                    expectedCard = new QBAffectedObject<CreditCard>(CreditCard.FindByPrimaryKey(item.RequestId), item.RequestId);
                }
                catch (DataNotFoundException)
                {
                    throw new QuickBooksAgentException("Expected DB object not found");
                }

                                

                CreditCard cardForUpdation = creditCard;
                cardForUpdation.CreditCardId = expectedCard.DomainObject.CreditCardId;
                cardForUpdation.EntityState = EntityState.Synchronized;
                cardForUpdation.Account = expectedCard.DomainObject.Account;
                cardForUpdation.PayeeQBEntityId = expectedCard.DomainObject.PayeeQBEntityId;

                CreditCard.Update(cardForUpdation);
                UpdateExpenceLines(expectedCard, cardForUpdation);
            }

            #endregion

            #region Process queried Credit Cards

            if (item.CommandType == QBCommandTypeEnum.Query)
            {
                CreditCard card = item.DomainObject;
                
                try
                {
                    CreditCard existingCard = CreditCard.FindBy((int)card.QuickBooksTxnId);

                    card.CreditCardId = existingCard.CreditCardId;
                    card.EntityState = EntityState.Synchronized;

                    CreditCardExpenceLine.Delete(card.CreditCardId);

                    try
                    {
                        InitIdsByQBListIds(card);
                        CreditCard.Update(card);
                        InsertExpenceLines(card);
                    }
                    catch (DataNotFoundException)
                    {
                        CreditCard.Delete(card);
                    }
                }

                catch (DataNotFoundException)
                {
                    Counter.Assign(card);
                    card.EntityState = EntityState.Synchronized;

                    try
                    {
                        InitIdsByQBListIds(card);
                        CreditCard.Insert(card);
                        InsertExpenceLines(card);
                    }
                    catch (DataNotFoundException)
                    {
			return;
                    }
                }
            }

            #endregion

        }

        #endregion

        #region InitIdsByQBListIds

        private static void InitIdsByQBListIds(CreditCard creditCard)
        {
            creditCard.Account = Account.FindBy(creditCard.Account.QuickBooksListId.Value);

            if (creditCard.PayeeQBEntityId != null)// Just make sure if this QBEntity exist in our DB
                QBEntity.FindByPrimaryKey(creditCard.PayeeQBEntityId.Value);

            if (creditCard.ExpenceLines != null && creditCard.ExpenceLines.Count != 0)
            {
                foreach (CreditCardExpenceLine expenceLine in creditCard.ExpenceLines)
                {
                    if (expenceLine.Account != null)
                        expenceLine.Account =
                            Account.FindBy(expenceLine.Account.QuickBooksListId.Value);

                    if (expenceLine.Customer != null)
                        expenceLine.Customer =
                            Customer.FindByQuickBooksId(expenceLine.Customer.QuickBooksListId.Value);
                }
            }
        }

        #endregion

        #region InsertExpenceLines

        private static void InsertExpenceLines(CreditCard creditCard)
        {
            if (creditCard.ExpenceLines != null)
            {
                foreach (CreditCardExpenceLine expenceLine in creditCard.ExpenceLines)
                {
                    Counter.Assign(expenceLine);
                    expenceLine.CreditCard = creditCard;
                    CreditCardExpenceLine.Insert(expenceLine);
                }
            }
        }

        #endregion

        #region UpdateExpenceLines

        private static void UpdateExpenceLines(CreditCard expectedCard, CreditCard creditCard)
        {
            if (expectedCard.ExpenceLines != null)
            {
                int responseLineCounter = 0;
                foreach (CreditCardExpenceLine expenceLine in expectedCard.ExpenceLines)
                {
                    expenceLine.TxnLineID = creditCard.ExpenceLines[responseLineCounter].TxnLineID;
                    CreditCardExpenceLine.Update(expenceLine);
                    responseLineCounter++;
                }
            }
        }

        #endregion
    }

    #endregion
}
