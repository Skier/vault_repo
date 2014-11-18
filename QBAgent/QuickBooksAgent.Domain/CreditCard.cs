using System;
using System.Collections.Generic;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class CreditCard : ICounterField
    {
        #region CreditCard

        public CreditCard()
        {
            EntityState = new EntityState();
            Account = new Account();           
            m_expenceLines = new List<CreditCardExpenceLine>();
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_creditCardId; }
            set { m_creditCardId = value; }
        }

        public string CounterName
        {
            get { return "CreditCard"; }
        }

        #endregion        
        
        #region ExpenceLines

        List<CreditCardExpenceLine> m_expenceLines;
        public List<CreditCardExpenceLine> ExpenceLines
        {
            get { return m_expenceLines; }
            set { m_expenceLines = value; }
        }

        #endregion

        #region FindBy Entity State
        
        private const string SqlFindByEntityState =
            @"SELECT CreditCardId, CreditCardTypeId, QuickBooksTxnId, EntityStateId, 
                EditSequence, TimeCreated, TimeModified, TxnNumber, TxnDate, AccountId, 
                PayeeQBEntityId, RefNumber, Amount, Memo 
            FROM CreditCard
                WHERE EntityStateId = @EntityStateId
                AND CreditCardTypeId = @CreditCardTypeId";

        public static List<CreditCard> FindBy(EntityState entityState, CreditCardType cardType)
        {
            List<CreditCard> creditCardList = new List<CreditCard>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEntityState))
            {
                Database.PutParameter(dbCommand, "@EntityStateId", entityState.EntityStateId);
                Database.PutParameter(dbCommand, "@CreditCardTypeId", cardType.CreditCardTypeId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        creditCardList.Add(Load(dataReader));
                    }
                }
            }
            return creditCardList;
        }

        #endregion

        #region FindBy QuickBooksTxnId

        private const string SqlFindByQuickBooksTxnId =
            @"SELECT CreditCardId, CreditCardTypeId, QuickBooksTxnId, EntityStateId, 
                EditSequence, TimeCreated, TimeModified, TxnNumber, TxnDate, AccountId, 
                PayeeQBEntityId, RefNumber, Amount, Memo 
            FROM CreditCard
                WHERE QuickBooksTxnId = @QuickBooksTxnId";

        public static CreditCard FindBy(int quickBooksTxnId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByQuickBooksTxnId))
            {
                Database.PutParameter(dbCommand, "@QuickBooksTxnId", quickBooksTxnId.ToString());

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);

                    throw new DataNotFoundException("Credit Card with TxnID "
                        + quickBooksTxnId.ToString() + " not found");
                }
            }
        }

        #endregion                

        #region FindBy Account

        private const string SqlFindByAccount =
            @"SELECT CreditCardId, CreditCardTypeId, QuickBooksTxnId, EntityStateId, 
                EditSequence, TimeCreated, TimeModified, TxnNumber, TxnDate, AccountId, 
                PayeeQBEntityId, RefNumber, Amount, Memo 
            FROM CreditCard
                WHERE AccountId = @AccountId";

        public static List<CreditCard> FindBy(Account account)
        {
            List<CreditCard> creditCardList = new List<CreditCard>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByAccount))
            {
                Database.PutParameter(dbCommand, "@AccountId", account.AccountId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        creditCardList.Add(Load(dataReader));
                    }
                }
            }
            return creditCardList;
        }

        #endregion        
        
        #region DeleteOlderThan

        private const string SqlDeleteOlderThanExpenceLine =
            @"delete from CreditCardExpenceLine
                where CreditCardId IN 
	                (select CreditCardId from CreditCard	
		                where EntityStateId = 1 and TimeModified < @Date
	                )";

        private const string SqlDeleteOlderThan =
            @"delete from CreditCard
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
                rv = "Credit Card";

            return rv;            
        }

        #endregion
    }
}
      