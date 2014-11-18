using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Data;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class WorkTransactionTaskItem : ICounterField
    {
        public WorkTransactionTaskItem(){}

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "WorkTransactionTaskItem"; }
        }

        #endregion        

        #region FindBy WorkTransactionTaskId

        private const string SqlFindByWorkTransactionTask =
            @"SELECT *
            FROM WorkTransactionTaskItem
                WHERE 
                    WorkTransactionTaskId = @WorkTransactionTaskId";

        public static List<WorkTransactionTaskItem> FindBy(int workTransactionTaskId)
        {
            List<WorkTransactionTaskItem> transactionTaskItems = new List<WorkTransactionTaskItem>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkTransactionTask))
            {
                Database.PutParameter(dbCommand, "@WorkTransactionTaskId", workTransactionTaskId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        transactionTaskItems.Add(Load(dataReader));
                    }
                }
            }
            return transactionTaskItems;
        }

        #endregion        
    }
}
      