using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Data;
using Dalworth.SDK;

namespace Dalworth.Domain
{
    public partial class Task : ICounterField
    {
        public Task(){}

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_iD; }
            set { m_iD = value; }
        }

        public string CounterName
        {
            get { return "Task"; }
        }

        #endregion        

        #region TaskStatus

        public TaskStatusEnum TaskStatus
        {
            get { return (TaskStatusEnum)m_taskStatusId; }
            set { m_taskStatusId = (int)value; }
        }

        #endregion

        #region FindByVisit

        private const string SqlFindByVisit =
            @"SELECT *
            FROM Task
                WHERE VisitId = @VisitId";

        public static List<Task> FindByVisit(Visit visit)
        {
            List<Task> tasks = new List<Task>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByVisit))
            {
                Database.PutParameter(dbCommand, "@VisitId", visit.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        tasks.Add(Load(dataReader));
                }
            }
            return tasks;
        }

        #endregion

        #region FindByServerId

        private const string SqlFindByServerId =
            @"SELECT *
            FROM Task
                WHERE ServerId = @ServerId";

        public static Task FindByServerId(int serverId, IDbTransaction transaction)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByServerId, transaction))
            {
                Database.PutParameter(dbCommand, "@ServerId", serverId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("Task not found by ServerId");
        }

        #endregion
    }
}
      