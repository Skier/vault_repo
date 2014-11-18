using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Serialization;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class WorkTransactionTask
    {
        public WorkTransactionTask(){ }

        #region WorkTransactionTaskAction

        [XmlIgnore]
        public WorkTransactionTaskActionEnum WorkTransactionTaskAction
        {
            get { return (WorkTransactionTaskActionEnum)m_workTransactionTaskActionId; }
            set { m_workTransactionTaskActionId = (int)value; }
        }

        #endregion

        #region FindBy WorkTransaction And Task

        private const string SqlFindByWorkTransactionAndTask =
            @"SELECT * FROM WorkTransactionTask
                WHERE WorkTransactionId = ?WorkTransactionId
                    and TaskId = ?TaskId";

        public static WorkTransactionTask FindBy(WorkTransaction transaction, Task task)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkTransactionAndTask))
            {
                Database.PutParameter(dbCommand, "?WorkTransactionId", transaction.ID);
                Database.PutParameter(dbCommand, "?TaskId", task.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("WorkTransactionTask not found");
        }

        #endregion        

        #region FindBy WorkTransaction

        private const string SqlFindByWorkTransaction =
            @"SELECT * FROM WorkTransactionTask
                WHERE WorkTransactionId = ?WorkTransactionId";

        public static List<WorkTransactionTask> FindBy(WorkTransaction transaction)
        {
            List<WorkTransactionTask> result = new List<WorkTransactionTask>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkTransaction))
            {
                Database.PutParameter(dbCommand, "?WorkTransactionId", transaction.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region FindBy Task and Action

        private const string SqlFindByTaskAndAction =
            @"SELECT * FROM WorkTransactionTask
                WHERE TaskId = ?TaskId
                    and WorkTransactionTaskActionId = ?WorkTransactionTaskActionId
              order by WorkTransactionId";

        public static List<WorkTransactionTask> FindBy(Task task, WorkTransactionTaskActionEnum taskAction)
        {
            List<WorkTransactionTask> result = new List<WorkTransactionTask>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTaskAndAction))
            {
                Database.PutParameter(dbCommand, "?TaskId", task.ID);
                Database.PutParameter(dbCommand, "?WorkTransactionTaskActionId", (int)taskAction);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region FindBy WorkTransaction and Action

        private const string SqlFindByWorkTransactionAndAction =
            @"SELECT * FROM WorkTransactionTask
                WHERE WorkTransactionId = ?WorkTransactionId
                    and WorkTransactionTaskActionId = ?WorkTransactionTaskActionId";

        public static List<WorkTransactionTask> FindBy(WorkTransaction workTransaction, 
            WorkTransactionTaskActionEnum taskAction)
        {
            List<WorkTransactionTask> result = new List<WorkTransactionTask>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByWorkTransactionAndAction))
            {
                Database.PutParameter(dbCommand, "?WorkTransactionId", workTransaction.ID);
                Database.PutParameter(dbCommand, "?WorkTransactionTaskActionId", (int)taskAction);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion
    }
}
      