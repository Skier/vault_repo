using System;
using System.Collections.Generic;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class Check : ICounterField
    {        
        #region Default constructor

        public Check()
        {
            EntityState = new EntityState();
            Account = new Account();
            m_expenceLines = new List<CheckExpenceLine>();
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_checkId; }
            set { m_checkId = value; }
        }

        public string CounterName
        {
            get { return "Check"; }
        }

        #endregion        

        #region ExpenceLines

        List<CheckExpenceLine> m_expenceLines;
        public List<CheckExpenceLine> ExpenceLines
        {
            get { return m_expenceLines; }
            set { m_expenceLines = value; }
        }

        #endregion

        #region FindBy Entity State

        private const string SqlFindByEntityState =
            @"SELECT CheckId, QuickBooksTxnId, EntityStateId, EditSequence, 
                TimeCreated, TimeModified, TxnNumber, TxnDate, AccountId, 
                PayeeQBEntityId, RefNumber, Amount, Memo, Addr1, Addr2, Addr3, 
                Addr4, City, State, PostalCode, Country, IsToBePrinted 
            FROM [Check]
                WHERE EntityStateId = @EntityStateId";

        public static List<Check> FindBy(EntityState entityState)
        {
            List<Check> checkList = new List<Check>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEntityState))
            {
                Database.PutParameter(dbCommand, "@EntityStateId", entityState.EntityStateId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        checkList.Add(Load(dataReader));
                    }
                }
            }
            return checkList;
        }

        #endregion        
        
        #region FindBy QuickBooksTxnId

        private const string SqlFindByQuickBooksTxnId =
            @"SELECT CheckId, QuickBooksTxnId, EntityStateId, EditSequence, 
                TimeCreated, TimeModified, TxnNumber, TxnDate, AccountId, 
                PayeeQBEntityId, RefNumber, Amount, Memo, Addr1, Addr2, Addr3, 
                Addr4, City, State, PostalCode, Country, IsToBePrinted 
            FROM [Check]
                WHERE QuickBooksTxnId = @QuickBooksTxnId";

        public static Check FindBy(int quickBooksTxnId)
        {                        
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByQuickBooksTxnId))
            {
                Database.PutParameter(dbCommand, "@QuickBooksTxnId", quickBooksTxnId.ToString());

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);

                    throw new DataNotFoundException("Check with TxnID "
                        + quickBooksTxnId.ToString() + " not found");
                }
            }
        }

        #endregion            
        
        #region FindNextCheckNumber

        private const string SqlFindRefNumberInLastTransactionStatusCreated =
            @"SELECT RefNumber
            FROM [Check]
            WHERE AccountId = @AccountId
	            AND QuickBooksTxnId is null
	            AND (RefNumber LIKE '%0%'
		            or RefNumber LIKE '%1%'
		            or RefNumber LIKE '%2%'
		            or RefNumber LIKE '%3%'
		            or RefNumber LIKE '%4%'
		            or RefNumber LIKE '%5%'
		            or RefNumber LIKE '%6%'
		            or RefNumber LIKE '%7%'
		            or RefNumber LIKE '%8%'
		            or RefNumber LIKE '%9%')
            ORDER BY CheckId DESC";
        
        private const string SqlFindRefNumberInLastTransactionStatusSynchronized =
            @"SELECT RefNumber
            FROM [Check]
            WHERE AccountId = @AccountId
	            AND QuickBooksTxnId is not null
	            AND (RefNumber LIKE '%0%'
		            or RefNumber LIKE '%1%'
		            or RefNumber LIKE '%2%'
		            or RefNumber LIKE '%3%'
		            or RefNumber LIKE '%4%'
		            or RefNumber LIKE '%5%'
		            or RefNumber LIKE '%6%'
		            or RefNumber LIKE '%7%'
		            or RefNumber LIKE '%8%'
		            or RefNumber LIKE '%9%')
            ORDER BY QuickBooksTxnId DESC";

        public static string FindNextCheckNumber(Account account)
        {
            string refNumber = null;
            
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindRefNumberInLastTransactionStatusCreated))
            {
                Database.PutParameter(dbCommand, "@AccountId", account.AccountId);
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        refNumber = dataReader.GetString(0);
                }
            }
            
            if (refNumber == null)
            {
                using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindRefNumberInLastTransactionStatusSynchronized))
                {
                    Database.PutParameter(dbCommand, "@AccountId", account.AccountId);
                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        if (dataReader.Read())
                            refNumber = dataReader.GetString(0);
                    }
                }                
            }

            if (refNumber == null)
                return "1";
            else
            {
                int digitStartIndex = 0;
                int digitEndIndex = 0;
                
                for (int i = refNumber.Length - 1; i >= 0; i--)
                {
                    if (char.IsDigit(refNumber[i]))
                    {
                        digitEndIndex = i;                                                
                        for(int j = i; j >= 0; j--)
                        {
                            if (!char.IsDigit(refNumber[j]))
                            {
                                digitStartIndex = j + 1;
                                break;
                            }
                                
                        }
                        break;
                    }
                }

                string head = refNumber.Substring(0, digitStartIndex);
                string foundedNumber
                    = refNumber.Substring(digitStartIndex, digitEndIndex - digitStartIndex + 1);
                
                string tail = string.Empty;
                if (digitEndIndex != refNumber.Length - 1)
                    tail = refNumber.Substring(digitEndIndex + 1);                        

                long nextNumber = long.Parse(foundedNumber) + 1;

                return head + nextNumber.ToString() + tail;                
            }
        }

        #endregion            
        
        #region IsCheckNumberExist

        private const string SqlCheckNumberExist =
            @"SELECT 1 
            FROM [Check]
                WHERE RefNumber = @RefNumber
                    AND AccountId = @AccountId";

        public static bool IsCheckNumberExist(string checkNumber, Account account)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlCheckNumberExist))
            {
                Database.PutParameter(dbCommand, "@RefNumber", checkNumber);
                Database.PutParameter(dbCommand, "@AccountId", account.AccountId);
                using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
                {
                    return reader.Read();
                }
            }

        }

        #endregion            
        
        #region FindBy Account

        private const string SqlFindByAccount =
            @"SELECT CheckId, QuickBooksTxnId, EntityStateId, EditSequence, 
                TimeCreated, TimeModified, TxnNumber, TxnDate, AccountId, 
                PayeeQBEntityId, RefNumber, Amount, Memo, Addr1, Addr2, Addr3, 
                Addr4, City, State, PostalCode, Country, IsToBePrinted 
            FROM [Check]
                WHERE AccountId = @AccountId";

        public static List<Check> FindBy(Account account)
        {
            List<Check> checkList = new List<Check>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByAccount))
            {
                Database.PutParameter(dbCommand, "@AccountId", account.AccountId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        checkList.Add(Load(dataReader));
                    }
                }
            }
            return checkList;
        }

        #endregion        
        
        #region DeleteOlderThan

        private const string SqlDeleteOlderThanExpenceLine =
            @"delete from CheckExpenceLine
                where CheckId IN 
	                (select CheckId from [Check]	
		                where EntityStateId = 1 and TimeModified < @Date
	                )";
        
        private const string SqlDeleteOlderThan =
            @"delete from [Check]	
		        where EntityStateId = 1 and TimeModified < @Date";

        public static void DeleteOlderThan(DateTime date)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteOlderThanExpenceLine))
            {
                Database.PutParameter(dbCommand, "@Date", date);
                dbCommand.ExecuteNonQuery();
            }

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteOlderThan))
            {
                Database.PutParameter(dbCommand, "@Date", date);
                dbCommand.ExecuteNonQuery();
            }

        }

        #endregion            

        #region ToString

        public override string ToString()
        {
            string rv = string.Empty;

            if (RefNumber != null)
                rv += "#" + RefNumber;

            if (TxnDate.HasValue)
            {
                if (rv == String.Empty)
                    rv += TxnDate.Value.ToString("yyyy-MM-dd");
                else
                    rv += ", " + TxnDate.Value.ToString("yyyy-MM-dd");
            }
            
            if (Amount.HasValue)
            {
                if (rv == String.Empty)
                    rv += Amount.Value.ToString("C");
                else
                    rv += ", " + Amount.Value.ToString("C");
            }

            if (rv == String.Empty)
                rv = "Check";

            return rv;
        }

        #endregion
    }
}
      