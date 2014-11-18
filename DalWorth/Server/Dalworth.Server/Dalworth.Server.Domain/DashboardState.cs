using System;
using System.Data;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;

namespace Dalworth.Server.Domain
{
    public partial class DashboardState
    {
        private static DashboardState m_latestDashboardState = new DashboardState();

        public DashboardState(){ }

        #region IsDashboardDirty

        private const string SqlSelectIsDirty = SqlSelectAll + @" where id >= ?MaxId and EmployeeId != ?EmployeeId order by ID desc limit 1";

        public static bool IsDashboardDirty(int dispatchId, IDbConnection connection)
        {
            DashboardState latestDashboardState = null;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectIsDirty, connection))
            {
                Database.PutParameter(dbCommand, "?MaxId", m_latestDashboardState.ID);
                Database.PutParameter(dbCommand, "?EmployeeId", dispatchId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        latestDashboardState = Load(dataReader);
                }
            }

            if (latestDashboardState == null)
                return false;

            if (m_latestDashboardState.ID < latestDashboardState.ID)
            {
                m_latestDashboardState = latestDashboardState;
                return true;
            }

            return false;
        }

        #endregion               

        #region MakeDashboardDirty

        public static void MakeDashboardDirty(int dispatchId)
        {
            MakeDashboardDirty(dispatchId, null);
        }

        public static void MakeDashboardDirty(int dispatchId, IDbConnection connection)
        {
            DashboardState dashboardState = new DashboardState();
            dashboardState.EmployeeId = dispatchId;
            dashboardState.DateCreated = DateTime.Now;
            DashboardState.Insert(dashboardState, connection);
        }       

        public static void MakeDashboardDirty(IDbConnection connection)
        {
            MakeDashboardDirty(0, connection);
        }

        public static void MakeDashboardDirty()
        {
            MakeDashboardDirty(null);
        }

        #endregion
    }
}
      