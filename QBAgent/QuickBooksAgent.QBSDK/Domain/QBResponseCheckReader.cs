using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using QuickBooksAgent.QBSDK.Domain;

namespace QuickBooksAgent.QBSDK
{
    public class QBResponseCheckReader : QBResponseReader<Check>
    {
        #region Convert

        protected override Check Convert(object item)
        {            
            CheckRet checkRet = (CheckRet) item;
            
            Check check = new Check(
                null,
                null,
                null,
                0,
                checkRet.QuickBooksTxnId,
                checkRet.EditSequence,
                checkRet.TimeCreated,
                checkRet.TimeModified,
                checkRet.TxnNumber,
                checkRet.TxnDate,
                null,
                checkRet.RefNumber,
                checkRet.Amount,
                checkRet.Memo,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                checkRet.IsToBePrinted ?? false);
            
            check.Account = new Account();
            check.Account.QuickBooksListId = checkRet.AccountRef.ListId;
            
            if (checkRet.PayeeEntityRef != null)
            {
                check.PayeeQBEntityId = checkRet.PayeeEntityRef.ListId;
            }
            
            if (checkRet.Address != null)
            {
                check.Addr1 = checkRet.Address.Addr1;
                check.Addr2 = checkRet.Address.Addr2;
                check.Addr3 = checkRet.Address.Addr3;
                check.Addr4 = checkRet.Address.Addr4;
                check.City = checkRet.Address.City;
                check.State = checkRet.Address.State;
                check.PostalCode = checkRet.Address.PostalCode;
                check.Country = checkRet.Address.Country;
            }
            
            
            if (checkRet.ExpenceLines != null)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ExpenceLineRet));
                check.ExpenceLines = new List<CheckExpenceLine>();
                
                foreach (XmlElement expenceLineElement in checkRet.ExpenceLines)
                {                    
                    ExpenceLineRet expenceLineRet =
                        (ExpenceLineRet) serializer.Deserialize(new XmlNodeReader(expenceLineElement));

                    CheckExpenceLine checkExpenceLine = new CheckExpenceLine(
                        null,
                        null,
                        null,
                        0,
                        expenceLineRet.TxnLineID,
                        expenceLineRet.Amount,
                        expenceLineRet.Memo);
                    
                    if (expenceLineRet.AccountRef != null)
                    {
                        checkExpenceLine.Account = new Account();
                        checkExpenceLine.Account.QuickBooksListId = expenceLineRet.AccountRef.ListId;
                    }

                    if (expenceLineRet.CustomerRef != null)
                    {
                        checkExpenceLine.Customer = new Customer();
                        checkExpenceLine.Customer.QuickBooksListId = expenceLineRet.CustomerRef.ListId;
                    }

                    
                    check.ExpenceLines.Add(checkExpenceLine);
                }
            }
            
            return check;
        }

        #endregion

        #region ProcessResponse

        protected override void ProcessResponse(QBAffectedObject<Check> item)
        {
            #region Process added Checks
                                
            if (item.CommandType == QBCommandTypeEnum.Add)
            {                
                Check check = item.DomainObject;
                
                QBAffectedObject<Check> expectedCheck;
                try
                {
                    expectedCheck = new QBAffectedObject<Check>(Check.FindByPrimaryKey(item.RequestId), item.RequestId);
                }
                catch (DataNotFoundException)
                {
                    throw new QuickBooksAgentException("Expected DB object not found");
                }
                                    
                Check checkForUpdation = check;
                checkForUpdation.CheckId = expectedCheck.DomainObject.CheckId;
                checkForUpdation.EntityState = EntityState.Synchronized;
                checkForUpdation.Account = expectedCheck.DomainObject.Account;
                checkForUpdation.PayeeQBEntityId = expectedCheck.DomainObject.PayeeQBEntityId;

                Check.Update(checkForUpdation);

                int responseLineCounter = 0;
                if (expectedCheck.DomainObject.ExpenceLines != null)
                {
                    foreach (CheckExpenceLine expenceLine in expectedCheck.DomainObject.ExpenceLines)
                    {
                        expenceLine.TxnLineID = check.ExpenceLines[responseLineCounter].TxnLineID;
                        CheckExpenceLine.Update(expenceLine);
                        responseLineCounter++;
                    }
                }

            }
            #endregion

            #region Process queried Checks

            if (item.CommandType == QBCommandTypeEnum.Query)
            {
                Check check = item.DomainObject;
                
                try
                {                    
                    Check existingCheck = Check.FindBy((int)check.QuickBooksTxnId);

                    check.CheckId = existingCheck.CheckId;
                    check.EntityState = EntityState.Synchronized;

                    CheckExpenceLine.Delete(check.CheckId);

                    try
                    {
                        InitAndValidateCheck(check);
                        Check.Update(check);
                        if (check.ExpenceLines != null)
                            foreach (CheckExpenceLine expenceLine in check.ExpenceLines)
                            {
                                Counter.Assign(expenceLine);
                                expenceLine.Check = check;
                                CheckExpenceLine.Insert(expenceLine);
                            }

                    }
                    catch (DataNotFoundException)
                    {
                        Check.Delete(check);
                    }
                }
                catch (DataNotFoundException)
                {
                    Counter.Assign(check);
                    check.EntityState = EntityState.Synchronized;

                    try
                    {
                        InitAndValidateCheck(check);
                        Check.Insert(check);
                        if (check.ExpenceLines != null)
                            foreach (CheckExpenceLine expenceLine in check.ExpenceLines)
                            {
                                Counter.Assign(expenceLine);
                                expenceLine.Check = check;
                                CheckExpenceLine.Insert(expenceLine);
                            }
                    }
                    catch (DataNotFoundException)
                    {
			return;
                    }
                }
            }

            #endregion
            
        }

        #region InitAndValidateCheck

        //Inits incoming (from QBOE) check with acualy ID's in our DB in contrast to 
        //their ListID's.
        //Throws DataNotFoundException if at least one dependent record not found
        private void InitAndValidateCheck(Check check)
        {
            check.Account = Account.FindBy(check.Account.QuickBooksListId.Value);
            if (check.PayeeQBEntityId != null)
                QBEntity.FindByPrimaryKey(check.PayeeQBEntityId.Value);

            if (check.ExpenceLines != null && check.ExpenceLines.Count != 0)
            {
                foreach (CheckExpenceLine expenceLine in check.ExpenceLines)
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

        #endregion

        #region TargetNodeName
        protected override string TargetNodeName
        {
            get { return "CheckRet"; }
        }
        #endregion

        #region TargetClassType
        protected override Type TargetClassType
        {
            get { return typeof(CheckRet); }
        }
        #endregion

        #region IsRootNode

        public override bool IsRootNode(string nodeName)
        {
            return "CheckQueryRs".Equals(nodeName)
                   || "CheckAddRs".Equals(nodeName);
        }

        #endregion                        

        #region CheckRet

        [XmlRoot("CheckRet")]
        public class CheckRet : QBResponseItem
        {
            #region QuickBooksTxnId

            int m_quickBooksTxnId;
            [XmlElement("TxnID")]
            public int QuickBooksTxnId
            {
                get { return m_quickBooksTxnId; }
                set { m_quickBooksTxnId = value; }
            }

            #endregion            

            #region TxnNumber

            private int? m_txnNumber;
            [XmlElement("TxnNumber")]
            public int? TxnNumber
            {
                get { return m_txnNumber; }
                set { m_txnNumber = value; }
            }

            #endregion

            #region AccountRef

            private QBResponseItem m_accountRef;
            [XmlElement("AccountRef")]
            public QBResponseItem AccountRef
            {
                get { return m_accountRef; }
                set { m_accountRef = value; }
            }

            #endregion

            #region PayeeEntityRef

            private QBResponseItem m_payeeEntityRef;
            [XmlElement("PayeeEntityRef")]
            public QBResponseItem PayeeEntityRef
            {
                get { return m_payeeEntityRef; }
                set { m_payeeEntityRef = value; }
            }

            #endregion

            #region RefNumber

            private string m_refNumber;
            [XmlElement("RefNumber")]
            public string RefNumber
            {
                get { return m_refNumber; }
                set { m_refNumber = value; }
            }

            #endregion

            #region TxnDate

            private DateTime m_txnDate;
            [XmlElement("TxnDate")]
            public DateTime TxnDate
            {
                get { return m_txnDate; }
                set { m_txnDate = value; }
            }

            #endregion

            #region Amount

            private decimal m_amount;
            [XmlElement("Amount")]
            public decimal Amount
            {
                get { return m_amount; }
                set { m_amount = value; }
            }

            #endregion

            #region Memo

            private string m_memo;
            [XmlElement("Memo")]
            public string Memo
            {
                get { return m_memo; }
                set { m_memo = value; }
            }

            #endregion

            #region Address

            private CheckAddress m_address;
            [XmlElement("Address")]
            public CheckAddress Address
            {
                get { return m_address; }
                set { m_address = value; }
            }

            #endregion

            #region IsToBePrinted

            private bool? m_isToBePrinted;
            [XmlElement("IsToBePrinted")]            
            public bool? IsToBePrinted
            {
                get { return m_isToBePrinted; }
                set { m_isToBePrinted = value; }
            }

            #endregion

            #region ExpenceLines

            private XmlElement[] m_expenceLines;
            [XmlAnyElement("ExpenseLineRet")]
            public XmlElement[] ExpenceLines
            {
                get { return m_expenceLines; }
                set { m_expenceLines = value; }
            }

            #endregion        
        }

        #endregion

        #region CheckAddress

        public class CheckAddress
        {
            #region Addr1
            string m_addr1;
            [XmlElement("Addr1")]
            public string Addr1
            {
                get { return m_addr1; }
                set { m_addr1 = value; }
            }
            #endregion

            #region Addr2
            string m_addr2;
            [XmlElement("Addr2")]
            public string Addr2
            {
                get { return m_addr2; }
                set { m_addr2 = value; }
            }
            #endregion

            #region Addr3
            string m_addr3;
            [XmlElement("Addr3")]
            public string Addr3
            {
                get { return m_addr3; }
                set { m_addr3 = value; }
            }
            #endregion

            #region Addr4
            string m_addr4;
            [XmlElement("Addr4")]
            public string Addr4
            {
                get { return m_addr4; }
                set { m_addr4 = value; }
            }
            #endregion

            #region City
            string m_city;
            [XmlElement("City")]
            public string City
            {
                get { return m_city; }
                set { m_city = value; }
            }
            #endregion

            #region State
            string m_state;
            [XmlElement("State")]
            public string State
            {
                get { return m_state; }
                set { m_state = value; }
            }
            #endregion

            #region PostalCode
            string m_postalCode;
            [XmlElement("PostalCode")]
            public string PostalCode
            {
                get { return m_postalCode; }
                set { m_postalCode = value; }
            }
            #endregion

            #region Country
            string m_country;
            [XmlElement("Country")]
            public string Country
            {
                get { return m_country; }
                set { m_country = value; }
            }
            #endregion
        }

        #endregion

        #region ExpenceLine
        [XmlRoot("ExpenseLineRet")]
        public class ExpenceLineRet : QBResponseItem
        {            
            #region TxnLineID

            private int m_txnLineID;
            [XmlElement("TxnLineID")]
            public int TxnLineID
            {
                get { return m_txnLineID; }
                set { m_txnLineID = value; }
            }

            #endregion

            #region AccountRef

            private QBResponseItem m_accountRef;
            [XmlElement("AccountRef")]
            public QBResponseItem AccountRef
            {
                get { return m_accountRef; }
                set { m_accountRef = value; }
            }

            #endregion

            #region Amount

            private decimal m_amount;
            [XmlElement("Amount")]
            public decimal Amount
            {
                get { return m_amount; }
                set { m_amount = value; }
            }

            #endregion

            #region Memo

            private string m_memo;
            [XmlElement("Memo")]
            public string Memo
            {
                get { return m_memo; }
                set { m_memo = value; }
            }

            #endregion

            #region CustomerRef

            private QBResponseItem m_customerRef;
            [XmlElement("CustomerRef")]
            public QBResponseItem CustomerRef
            {
                get { return m_customerRef; }
                set { m_customerRef = value; }
            }

            #endregion
        }

        #endregion
    }
}
