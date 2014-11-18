using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Data;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class WorkTransactionTask : ICounterField
    {
        public WorkTransactionTask(){}

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "WorkTransactionTask"; }
        }

        #endregion        

        #region TaskStatus

        public TaskStatusEnum TaskStatus
        {
            get { return (TaskStatusEnum)m_taskStatusId; }
            set { m_taskStatusId = (int)value; }
        }

        #endregion

        #region FindBy WorkTransaction

        private const string SqlFindByWorkTransaction =
            @"SELECT *
            FROM WorkTransactionTask
                WHERE 
                    WorkTransactionId = @WorkTransactionId";

        public static List<WorkTransactionTask> FindBy(int workTransactionId)
        {
            List<WorkTransactionTask> transactionTasks = new List<WorkTransactionTask>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkTransaction))
            {
                Database.PutParameter(dbCommand, "@WorkTransactionId", workTransactionId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        transactionTasks.Add(Load(dataReader));
                    }
                }
            }
            return transactionTasks;
        }

        #endregion        
    }
}
      