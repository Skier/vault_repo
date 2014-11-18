using System;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class PendingTaskGridState
    {
        private static PendingTaskGridState m_latestPendingTaskGridState = new PendingTaskGridState();

        public PendingTaskGridState(){ }

        #region IsPendingTaskGridDirty

        private const string SqlSelectIsDirty = SqlSelectAll + @" where id >= ?MaxId order by ID desc limit 1";

        public static bool IsPendingTaskGridDirty(int dispatchId, IDbConnection connection)
        {
            PendingTaskGridState latestPendingTaskGridState = null;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectIsDirty, connection))
            {
                Database.PutParameter(dbCommand, "?MaxId", m_latestPendingTaskGridState.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        latestPendingTaskGridState = Load(dataReader);
                }
            }

            if (latestPendingTaskGridState == null)
                return false;

            if (m_latestPendingTaskGridState.ID < latestPendingTaskGridState.ID)
            {
                m_latestPendingTaskGridState = latestPendingTaskGridState;
                return true;
            }

            return false;
        }

        #endregion

        #region MakePendingTaskGridDirty

        public static void MakePendingTaskGridDirty(int dispatchId)
        {
            MakePendingTaskGridDirty(dispatchId, null);
        }

        public static void MakePendingTaskGridDirty(int dispatchId, IDbConnection connection)
        {
            PendingTaskGridState state = new PendingTaskGridState();
            state.EmployeeId = dispatchId;
            state.DateCreated = DateTime.Now;
            PendingTaskGridState.Insert(state, connection);
            m_latestPendingTaskGridState = state;
        }

        public static void MakePendingTaskGridDirty(IDbConnection connection)
        {
            MakePendingTaskGridDirty(0, connection);
        }

        public static void MakePendingTaskGridDirty()
        {
            MakePendingTaskGridDirty(null);
        }

        #endregion
    }
}
      