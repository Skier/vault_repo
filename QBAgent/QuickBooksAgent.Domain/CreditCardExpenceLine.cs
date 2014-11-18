using System;
using System.Collections.Generic;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class CreditCardExpenceLine : ICounterField
    {
        #region CreditCardExpenceLine

        public CreditCardExpenceLine() {}

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_creditCardExpenceLineId; }
            set { m_creditCardExpenceLineId = value; }
        }

        public string CounterName
        {
            get { return "CreditCardExpenceLine"; }
        }

        #endregion        
        
        #region Delete by CreditCardId

        private const String SqlDeleteByCreditCardId =
            @"Delete From 
                CreditCardExpenceLine  
            Where 
                CreditCardId = @CreditCardId ";

        public static void Delete(int creditCardId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByCreditCardId))
            {
                Database.PutParameter(dbCommand, "@CreditCardId", creditCardId);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region FindBy CreditCard

        private const string SqlFindByCreditCard =
            @"SELECT CreditCardExpenceLineId, CreditCardId, TxnLineID, AccountId, Amount, Memo, CustomerId 
            FROM CreditCardExpenceLine
                WHERE CreditCardId = @CreditCardId";

        public static List<CreditCardExpenceLine> FindBy(CreditCard creditCard)
        {
            List<CreditCardExpenceLine> expenceLines = new List<CreditCardExpenceLine>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByCreditCard))
            {
                Database.PutParameter(dbCommand, "@CreditCardId", creditCard.CreditCardId);
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        expenceLines.Add(Load(dataReader));
                    }
                }
            }
            return expenceLines;
        }

        #endregion        
    }
}
      