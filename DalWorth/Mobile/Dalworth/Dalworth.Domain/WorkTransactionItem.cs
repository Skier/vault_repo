using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Data;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class WorkTransactionItem : ICounterField
    {
        public WorkTransactionItem(){}

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "WorkTransactionItem"; }
        }

        #endregion        

        #region FindBy WorkTransaction

        private const string SqlFindByWorkTransaction =
            @"SELECT *
            FROM WorkTransactionItem
                WHERE 
                    WorkTransactionId = @WorkTransactionId";

        public static List<WorkTransactionItem> FindBy(int workTransactionId)
        {
            List<WorkTransactionItem> transactionItems = new List<WorkTransactionItem>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkTransaction))
            {
                Database.PutParameter(dbCommand, "@WorkTransactionId", workTransactionId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        transactionItems.Add(Load(dataReader));
                    }
                }
            }
            return transactionItems;
        }

        #endregion        
    }
}
      