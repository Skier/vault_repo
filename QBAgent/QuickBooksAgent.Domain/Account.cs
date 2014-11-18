using System.Collections.Generic;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class Account : ICounterField
    {
        #region Account

        public Account()
        {
            EntityState = new EntityState();
            AccountType = new AccountType();
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_accountId; }
            set { m_accountId = value; }
        }

        public string CounterName
        {
            get { return "Account"; }
        }

        #endregion        
        
        #region FindBy Entity State

        private const string SqlFindByEntityState =
            @"SELECT AccountId, QuickBooksListId, EntityStateId, EditSequence, Name, 
                FullName, AccountTypeId, DetailAccountTypeId, AccountNumber, LastCheckNumber, 
                Descriptive, Balance, TotalBalance 
            FROM Account
                WHERE EntityStateId = @EntityStateId";        
        

        public static List<Account> FindBy(EntityState entityState)
        {
            List<Account> accountList = new List<Account>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEntityState))
            {
                Database.PutParameter(dbCommand, "@EntityStateId", entityState.EntityStateId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        accountList.Add(Load(dataReader));
                    }
                }
            }
            return accountList;
        }

        #endregion        
        
        #region FindBy QuickBooksId

        private const string SqlFindByQuickBooksId =
            @"SELECT AccountId, QuickBooksListId, EntityStateId, EditSequence, Name, 
                FullName, AccountTypeId, DetailAccountTypeId, AccountNumber, LastCheckNumber, 
                Descriptive, Balance, TotalBalance 
            FROM Account
                WHERE QuickBooksListId = @QuickBooksListId";

        public static Account FindBy(int quickBooksListId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByQuickBooksId))
            {
                Database.PutParameter(dbCommand, "@QuickBooksListId", quickBooksListId.ToString());

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);

                    throw new DataNotFoundException("Account with List ID "
                        + quickBooksListId.ToString() + " not found");
                }
            }
        }

        #endregion                
        
        #region FindBy Account Type

        private const string SqlFindByAccountType =
            @"SELECT AccountId, QuickBooksListId, EntityStateId, EditSequence, Name, 
                FullName, AccountTypeId, DetailAccountTypeId, AccountNumber, LastCheckNumber, 
                Descriptive, Balance, TotalBalance 
            FROM Account
                WHERE AccountTypeId = @AccountTypeId
                    AND EntityStateId = @EntityStateId";


        public static List<Account> FindBy(AccountType accountType, EntityState entityState)
        {
            List<Account> accountList = new List<Account>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByAccountType))
            {
                Database.PutParameter(dbCommand, "@AccountTypeId", accountType.AccountTypeId);
                Database.PutParameter(dbCommand, "@EntityStateId", entityState.EntityStateId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        accountList.Add(Load(dataReader));
                    }
                }
            }
            return accountList;
        }

        #endregion        
        
        #region GetBalance

        private const string SqlTotalAmountForCreatedChecks =
            @"SELECT AccountId, sum(Amount) TotalAmount
            FROM [Check]
            WHERE AccountId = @AccountId
                AND EntityStateId = 0
            GROUP BY AccountId";
        
        private const string SqlTotalAmountForCreatedCCCharges =
            @"SELECT AccountId, sum(Amount) TotalAmount
            FROM CreditCard
            WHERE AccountId = @AccountId
                AND EntityStateId = 0
            GROUP BY AccountId";

        public static decimal GetBalance(Account account)
        {
            decimal balance = account.Balance ?? decimal.Zero;   
            
            if (account.AccountType == AccountType.Bank)
                using (IDbCommand dbCommand = Database.PrepareCommand(SqlTotalAmountForCreatedChecks))
                {
                    Database.PutParameter(dbCommand, "@AccountId", account.AccountId);
                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                            balance -= dataReader.GetDecimal(1);
                    }
                }

            if (account.AccountType == AccountType.CreditCard)
                using (IDbCommand dbCommand = Database.PrepareCommand(SqlTotalAmountForCreatedCCCharges))
                {
                    Database.PutParameter(dbCommand, "@AccountId", account.AccountId);
                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                            balance -= dataReader.GetDecimal(1);
                    }
                }
            
            return balance;            
        }

        #endregion            

        #region Equals and HashCode

        public override int GetHashCode()
        {
            return m_accountId;
        }

        public override bool Equals(object obj)
        {
            if (this == obj) return true;
            Account account = obj as Account;
            if (account == null) return false;
            if (m_accountId != account.m_accountId) return false;
            return true;
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return Name;
        }

        #endregion

    }
}
      