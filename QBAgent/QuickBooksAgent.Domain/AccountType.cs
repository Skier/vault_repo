using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class AccountType
    {
        public static AccountType AccountsPayable       = new AccountType(0 , "AccountsPayable"); 
        public static AccountType AccountsReceivable    = new AccountType(1 , "AccountsReceivable");
        public static AccountType Bank                  = new AccountType(2 , "Bank");
        public static AccountType CostOfGoodsSold       = new AccountType(3 , "CostOfGoodsSold");
        public static AccountType CreditCard            = new AccountType(4 , "CreditCard");
        public static AccountType Equity                = new AccountType(5 , "Equity");
        public static AccountType Expense               = new AccountType(6 , "Expense");
        public static AccountType FixedAsset            = new AccountType(7 , "FixedAsset");
        public static AccountType Income                = new AccountType(8 , "Income");
        public static AccountType LongTermLiability     = new AccountType(9 , "LongTermLiability");
        public static AccountType NonPosting            = new AccountType(10, "NonPosting");
        public static AccountType OtherAsset            = new AccountType(11, "OtherAsset");
        public static AccountType OtherCurrentAsset     = new AccountType(12, "OtherCurrentAsset");
        public static AccountType OtherCurrentLiability = new AccountType(13, "OtherCurrentLiability");
        public static AccountType OtherExpense          = new AccountType(14, "OtherExpense");
        public static AccountType OtherIncome           = new AccountType(15, "OtherIncome");        
        
        public AccountType() {}

        public static bool operator ==(AccountType arg1, AccountType arg2)
        {
            return arg1.Equals(arg2);
        }
        
        public static bool operator !=(AccountType arg1, AccountType arg2)
        {
            return !arg1.Equals(arg2);
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj is AccountType)
                return (obj as AccountType).AccountTypeId == AccountTypeId;

            return false;
        }

        public override int GetHashCode()
        {
            return AccountTypeId.GetHashCode();
        }

        #region FindBy Description

        private const string SqlFindDescription =
            @"SELECT AccountTypeId, AccountTypeDescription 
            FROM AccountType
                WHERE AccountTypeDescription = @AccountTypeDescription";

        public static AccountType FindBy(string description)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindDescription))
            {
                Database.PutParameter(dbCommand, "@AccountTypeDescription", description);
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);

                    throw new DataNotFoundException("AccountType " + description + " not found");
                }
            }
        }

        #endregion                
    }
}
      