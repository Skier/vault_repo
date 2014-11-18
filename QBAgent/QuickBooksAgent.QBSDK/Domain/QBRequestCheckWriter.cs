using System.Collections.Generic;
using System.Xml;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBRequestCheckWriter : QBRequestWriter<Check>
    {
        #region OnProcess

        protected override void OnProcess(XmlWriter xmlWriter, QBCommandTypeEnum commandType,
                                          List<QBAffectedObject<Check>> returnList)
        {
            List<Check> checkList;
            
            if (commandType == QBCommandTypeEnum.Add)
                checkList = Check.FindBy(EntityState.Created);
            else return;
            
            foreach (Check check in checkList)
            {
                int requestId = check.CheckId;
                
                xmlWriter.WriteStartElement("CheckAddRq");
                xmlWriter.WriteAttributeString("requestID", requestId.ToString());
                xmlWriter.WriteStartElement("CheckAdd");

                xmlWriter.WriteStartElement("AccountRef");
                check.Account = Account.FindByPrimaryKey(check.Account.AccountId);
                WriteElement(xmlWriter, "ListID", check.Account.QuickBooksListId.ToString());
                xmlWriter.WriteEndElement();

                if (check.PayeeQBEntityId != null)
                {
                    xmlWriter.WriteStartElement("PayeeEntityRef");
                    WriteElement(xmlWriter, "ListID", check.PayeeQBEntityId.ToString());
                    xmlWriter.WriteEndElement();
                }

                WriteElement(xmlWriter, "RefNumber", check.RefNumber);
                if (check.TxnDate != null)
                    WriteElement(xmlWriter, "TxnDate", check.TxnDate.Value.ToString("yyyy-MM-dd"));
                WriteElement(xmlWriter, "Memo", check.Memo);
                WriteElement(xmlWriter, "IsToBePrinted", check.IsToBePrinted.ToString().ToLower());

                List<CheckExpenceLine> expenceLines = CheckExpenceLine.FindBy(check);
                
                foreach (CheckExpenceLine expenceLine in expenceLines)
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
}
