using System;
using System.Xml;
using System.Xml.Serialization;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.QBSDK.Domain
{
    public class QBResponseAccountReader : QBResponseReader<Account>
    {
        #region Convert

        protected override Account Convert(object item)
        {
            AccountRet accountRet = (AccountRet)item;

            Account account =
                new Account(
                    AccountType.FindBy(accountRet.AccountType),                                
                    null,
                    null,
                    0,                    
                    accountRet.ListId,
                    accountRet.EditSequence,
                    accountRet.Name,
                    accountRet.FullName,
                    accountRet.AccountNumber,
                    accountRet.LastCheckNumber,
                    accountRet.Descriptive,
                    accountRet.Balance,
                    accountRet.TotalBalance
                );

            if (accountRet.DetailAccountType != null 
                && accountRet.DetailAccountType != string.Empty)
            {
                account.DetailAccountType = DetailAccountType.FindBy(accountRet.DetailAccountType);
            }

            return account;
        }

        #endregion

        #region ProcessResponse

        protected override void ProcessResponse(QBAffectedObject<Account> item)
        {
            #region Process queried Account
            
            if (item.CommandType == QBCommandTypeEnum.Query)
            {
                Account account = item.DomainObject;
                try
                {
                    Account existingAccount = Account.FindBy((int)account.QuickBooksListId);
                    account.AccountId = existingAccount.AccountId;
                    account.EntityState = EntityState.Synchronized;
                    Account.Update(account);
                }
                catch (DataNotFoundException)
                {
                    Counter.Assign(account);

                    account.EntityState = EntityState.Synchronized;
                    Account.Insert(account);
                }
            }

            #endregion            
        }

        #endregion

        #region TargetNodeName
        protected override string TargetNodeName
        {
            get { return "AccountRet"; }
        }
        #endregion

        #region TargetClassType
        protected override Type TargetClassType
        {
            get { return typeof(AccountRet); }
        }
        #endregion        
        
        #region IsRootNode


        public override bool IsRootNode(string nodeName)
        {
            return "AccountQueryRs".Equals(nodeName)
                   || "AccountAddRs".Equals(nodeName);
        }

        #endregion        
        
        #region AccountRet

        [XmlRoot("AccountRet")]
        public class AccountRet : QBResponseItem
        {
            #region Name

            string m_name;
            [XmlElement("Name")]
            public string Name
            {
                get { return m_name; }
                set { m_name = value; }
            }

            #endregion

            #region FullName

            string m_fullName;
            [XmlElement("FullName")]
            public string FullName
            {
                get { return m_fullName; }
                set { m_fullName = value; }
            }

            #endregion

            #region AccountType

            string m_accountType;
            [XmlElement("AccountType")]
            public string AccountType
            {
                get { return m_accountType; }
                set { m_accountType = value; }
            }

            #endregion

            #region DetailAccountType

            string m_detailAccountType;
            [XmlElement("DetailAccountType")]
            public string DetailAccountType
            {
                get { return m_detailAccountType; }
                set { m_detailAccountType = value; }
            }

            #endregion

            #region AccountNumber

            string m_accountNumber;
            [XmlElement("AccountNumber")]
            public string AccountNumber
            {
                get { return m_accountNumber; }
                set { m_accountNumber = value; }
            }

            #endregion

            #region LastCheckNumber

            string m_lastCheckNumber;
            [XmlElement("LastCheckNumber")]
            public string LastCheckNumber
            {
                get { return m_lastCheckNumber; }
                set { m_lastCheckNumber = value; }
            }

            #endregion

            #region Descriptive

            string m_descriptive;
            [XmlElement("Desc")]
            public string Descriptive
            {
                get { return m_descriptive; }
                set { m_descriptive = value; }
            }

            #endregion

            #region Balance

            decimal m_balance;
            [XmlElement("Balance")]
            public decimal Balance
            {
                get { return m_balance; }
                set { m_balance = value; }
            }

            #endregion

            #region TotalBalance

            decimal m_totalBalance;
            [XmlElement("TotalBalance")]
            public decimal TotalBalance
            {
                get { return m_totalBalance; }
                set { m_totalBalance = value; }
            }

            #endregion
        }

        #endregion
    }
}
