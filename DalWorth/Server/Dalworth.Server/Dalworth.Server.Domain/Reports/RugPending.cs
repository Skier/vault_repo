using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain.Reports
{
    public class RugPending
    {
        #region Constructor

        public RugPending(int inWorkOrderQty, int inWorkRugQty, decimal inWorkAmt, int readyOrderQty, int readyRugQty, decimal readyAmt)
        {
            m_inWorkOrderQty = inWorkOrderQty;
            m_inWorkRugQty = inWorkRugQty;
            m_inWorkAmt = inWorkAmt;
            m_readyOrderQty = readyOrderQty;
            m_readyRugQty = readyRugQty;
            m_readyAmt = readyAmt;
        }

        #endregion

        #region InWorkOrderQty

        private int m_inWorkOrderQty;
        public int InWorkOrderQty
        {
            get { return m_inWorkOrderQty; }
            set { m_inWorkOrderQty = value; }
        }

        #endregion

        #region InWorkRugQty

        private int m_inWorkRugQty;
        public int InWorkRugQty
        {
            get { return m_inWorkRugQty; }
            set { m_inWorkRugQty = value; }
        }

        #endregion

        #region InWorkAmt

        private decimal m_inWorkAmt;
        public decimal InWorkAmt
        {
            get { return m_inWorkAmt; }
            set { m_inWorkAmt = value; }
        }

        #endregion

        #region ReadyOrderQty

        private int m_readyOrderQty;
        public int ReadyOrderQty
        {
            get { return m_readyOrderQty; }
            set { m_readyOrderQty = value; }
        }

        #endregion

        #region ReadyRugQty

        private int m_readyRugQty;
        public int ReadyRugQty
        {
            get { return m_readyRugQty; }
            set { m_readyRugQty = value; }
        }

        #endregion

        #region ReadyAmt

        private decimal m_readyAmt;
        public decimal ReadyAmt
        {
            get { return m_readyAmt; }
            set { m_readyAmt = value; }
        }

        #endregion

        #region TotalAmt

        public decimal TotalAmt
        {
            get { return InWorkAmt + ReadyAmt; }            
        }

        #endregion


        #region Find

        private const string SqlFind =
            @"select
                (SELECT count(t.ID) FROM Task t
                 inner join Project p on p.ID = t.ProjectId
                 where t.TaskTypeId = 2 and t.TaskStatusId = 1 and p.ProjectTypeId = 1
                  and t.IsReady = 0 and DumpedTaskId is null
                ) as InWorkOrderQty,

                (SELECT count(i.ID) FROM Task t
                inner join Project p on p.ID = t.ProjectId
                inner join Item i on t.ID = i.TaskId
                where t.TaskTypeId = 2 and t.TaskStatusId = 1 and p.ProjectTypeId = 1
                  and t.IsReady = 0 and DumpedTaskId is null
                ) as InWorkRugQty,

                (select COALESCE(sum(EstimatedClosedAmount), 0) from Task t
                where t.TaskTypeId = 1 and t.TaskStatusId = 2 and DumpedTaskId is null
                and t.ProjectId in (
                SELECT p.ID FROM Task t2
                inner join Project p on p.ID = t2.ProjectId
                where t2.TaskTypeId = 2 and t2.TaskStatusId = 1
                  and t2.IsReady = 0 and p.ProjectTypeId = 1 and t2.DumpedTaskId is null)
                ) as InWorkTotalAmt,

                (SELECT count(t.ID) FROM Task t
                inner join Project p on p.ID = t.ProjectId
                where t.TaskTypeId = 2 and t.TaskStatusId = 1
                  and t.IsReady = 1 and p.ProjectTypeId = 1 and DumpedTaskId is null
                ) as ReadyOrderQty,

                (SELECT count(i.ID) FROM Task t
                inner join Item i on t.ID = i.TaskId
                inner join Project p on p.ID = t.ProjectId
                where p.ProjectTypeId = 1 and t.TaskTypeId = 2 and t.TaskStatusId = 1
                  and t.IsReady = 1 and DumpedTaskId is null
                ) as ReadyRugsQty,

                (select COALESCE(sum(EstimatedClosedAmount), 0) from Task t
                where t.TaskTypeId = 1 and t.TaskStatusId = 2 and DumpedTaskId is null
                and t.ProjectId in (
                SELECT p.ID FROM Task t2
                inner join Project p on p.ID = t2.ProjectId
                where t2.TaskTypeId = 2 and t2.TaskStatusId = 1 and p.ProjectTypeId = 1
                  and t2.IsReady = 1 and t2.DumpedTaskId is null)
                ) as ReadyAmt";

        public static RugPending Find()
        {            
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFind))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        return new RugPending(
                            dataReader.GetInt32(0),
                            dataReader.GetInt32(1),
                            dataReader.GetDecimal(2),
                            dataReader.GetInt32(3),
                            dataReader.GetInt32(4),
                            dataReader.GetDecimal(5));
                    }
                }
            }

            throw new DataNotFoundException("Pending Rug Report data not found");
        }

        #endregion    
    }
}
