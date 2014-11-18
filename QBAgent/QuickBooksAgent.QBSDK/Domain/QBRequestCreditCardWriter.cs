using System.Collections.Generic;
using System.Xml;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.QBSDK.Domain
{
    public abstract class QBRequestCreditCardWriter : QBRequestWriter<CreditCard>
    {
        #region OnProcess

        protected void OnProcess(XmlWriter xmlWriter, QBCommandTypeEnum commandType,
                                          List<QBAffectedObject<CreditCard>> returnList, 
                                          CreditCardType cardType)
        {
            List<CreditCard> cardList;

            if (commandType == QBCommandTypeEnum.Add)
                cardList = CreditCard.FindBy(EntityState.Created, cardType);
            else return;            

            foreach (CreditCard card in cardList)
            {
                int requestId = card.CreditCardId;
                
                if (cardType == CreditCardType.Charge)
                {
                    xmlWriter.WriteStartElement("CreditCardChargeAddRq");
                    xmlWriter.WriteAttributeString("requestID", requestId.ToString());
                    xmlWriter.WriteStartElement("CreditCardChargeAdd");                    
                } else
                {
                    xmlWriter.WriteStartElement("CreditCardCreditAddRq");
                    xmlWriter.WriteAttributeString("requestID", requestId.ToString());
                    xmlWriter.WriteStartElement("CreditCardCreditAdd");                                        
                }
                                
                xmlWriter.WriteStartElement("AccountRef");
                card.Account = Account.FindByPrimaryKey(card.Account.AccountId);
                WriteElement(xmlWriter, "ListID", card.Account.QuickBooksListId.ToString());
                xmlWriter.WriteEndElement();

                if (card.PayeeQBEntityId != null)
                {
                    xmlWriter.WriteStartElement("PayeeEntityRef");
                    WriteElement(xmlWriter, "ListID", card.PayeeQBEntityId.ToString());
                    xmlWriter.WriteEndElement();
                }

                if (card.TxnDate != null)
                    WriteElement(xmlWriter, "TxnDate", card.TxnDate.Value.ToString("yyyy-MM-dd"));                
                WriteElement(xmlWriter, "RefNumber", card.RefNumber);
                WriteElement(xmlWriter, "Memo", card.Memo);

                List<CreditCardExpenceLine> expenceLines = CreditCardExpenceLine.FindBy(card);

                foreach (CreditCardExpenceLine expenceLine in expenceLines)
                {
                    xmlWriter.WriteStartElement("ExpenseLineAdd");

                    if (expenceLine.Account != null)
                    {
                        xmlWriter.WriteStartElement("AccountRef");
                        expenceLine.Account
                            = Account.FindByPrimaryKey(expenceLine.Account.AccountId);
                        WriteElement(xmlWriter, "ListID", expenceLine.Account.QuickBooksListId.ToString());
                        xmlWriter.WriteEndElement();
                    }

                    if (expenceLine.Amount != null)
                        WriteElement(xmlWriter, "Amount", expenceLine.Amount.Value.ToString("0.00", QBDataType.USCulture));

                    WriteElement(xmlWriter, "Memo", expenceLine.Memo);

                    if (expenceLine.Customer != null)
                    {
                        xmlWriter.WriteStartElement("CustomerRef");
                        expenceLine.Customer
                            = Customer.FindByPrimaryKey(expenceLine.Customer.CustomerId);
                        WriteElement(xmlWriter, "ListID", expenceLine.Customer.QuickBooksListId.ToString());
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();
                }


                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
            }

        }

        #endregion
    }

    #region QBRequestCreditCardChargeWriter

    public class QBRequestCreditCardChargeWriter : QBRequestCreditCardWriter
    {
        protected override void OnProcess(XmlWriter xmlWriter, QBCommandTypeEnum commandType,
                                          List<QBAffectedObject<CreditCard>> returnList)
        {
            OnProcess(xmlWriter, commandType, returnList, CreditCardType.Charge);
        }
    }

    #endregion

    #region QBRequestCreditCardCreditWriter

    public class QBRequestCreditCardCreditWriter : QBRequestCreditCardWriter
    {
        protected override void OnProcess(XmlWriter xmlWriter, QBCommandTypeEnum commandType,
                                          List<QBAffectedObject<CreditCard>> returnList)
        {
            OnProcess(xmlWriter, commandType, returnList, CreditCardType.Credit);
        }
    }

    #endregion
}
