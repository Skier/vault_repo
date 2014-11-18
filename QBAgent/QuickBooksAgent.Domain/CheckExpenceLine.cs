using System;
using System.Collections.Generic;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    public partial class CheckExpenceLine : ICounterField
    {
        #region Default constructor

        public CheckExpenceLine(){}

        #endregion

        #region Delete by CheckId

        private const String SqlDeleteByCheckId =
            @"Delete From 
                CheckExpenceLine  
            Where 
                CheckId = @CheckId ";
        
        public static void Delete(int checkId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByCheckId))
            {
                Database.PutParameter(dbCommand, "@CheckId", checkId);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_checkExpenceLineId; }
            set { m_checkExpenceLineId = value; }
        }

        public string CounterName
        {
            get { return "ExpenceLine"; }
        }

        #endregion        
        
        #region FindBy Check

        private const string SqlFindByCheck =
            @"SELECT CheckExpenceLineId, CheckId, TxnLineID, AccountId, Amount, Memo, CustomerId 
            FROM CheckExpenceLine
                WHERE CheckId = @CheckId";

        public static List<CheckExpenceLine> FindBy(Check check)
        {
            List<CheckExpenceLine> expenceLines = new List<CheckExpenceLine>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByCheck))
            {
                Database.PutParameter(dbCommand, "@CheckId", check.CheckId);
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
      