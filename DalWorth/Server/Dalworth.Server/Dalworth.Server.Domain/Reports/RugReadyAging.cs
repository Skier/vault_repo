using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.Reports
{
    public class RugReadyAging
    {
        #region Constructor

        public RugReadyAging(int rugsCount, decimal estimatedClosedAmount)
        {
            m_rugsCount = rugsCount;
            m_estimatedClosedAmount = estimatedClosedAmount;
        }

        #endregion

        #region RugsCount

        private int m_rugsCount;
        public int RugsCount
        {
            get { return m_rugsCount; }
            set { m_rugsCount = value; }
        }

        #endregion

        #region EstimatedClosedAmount

        private decimal m_estimatedClosedAmount;
        public decimal EstimatedClosedAmount
        {
            get { return m_estimatedClosedAmount; }
            set { m_estimatedClosedAmount = value; }
        }

        #endregion


        #region Find

        private const string SqlFind =
            @"SELECT count(t.ID), COALESCE(sum(t.EstimatedClosedAmount), 0) FROM Task t
                inner join Project p on p.ID = t.ProjectId
                where t.TaskTypeId = 2 and t.TaskStatusId = 1
                  and t.IsReady = 1 and p.ProjectTypeId = 1 and DumpedTaskId is null
                  and DATEDIFF(Now(), t.ReadyDate) >= ?DaysFrom and DATEDIFF(Now(), t.ReadyDate) <= ?DaysTo";

        public static RugReadyAging Find(int daysFrom, int daysTo)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind))
            {
                Database.PutParameter(dbCommand, "?DaysFrom", daysFrom);
                Database.PutParameter(dbCommand, "?DaysTo", daysTo);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        return new RugReadyAging(
                            dataReader.GetInt32(0),
                            dataReader.GetDecimal(1));
                    }
                }
            }

            return new RugReadyAging(0, decimal.Zero);            
        }

        #endregion    
    }
}
