using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class DashboardUserSettings
    {
        public DashboardUserSettings(){ }

        #region Technician

        public Employee m_technician;
        public Employee Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
        }

        #endregion

        #region FindSettings

        private const string SqlFindSettings =
            @"SELECT
                IFNULL(dus.DispatchId, ?DispatchId),
                IFNULL(dus.TechnicianId, e.ID),
                IFNULL(dus.IsVisible, 1),
                IFNULL(dus.Sequence, -1) as ResultSequence,
                e.*
                FROM Employee e
                left join DashboardUserSettings dus on e.ID = dus.TechnicianId and dus.DispatchId = ?DispatchId
                where e.EmployeeTypeId = 1 and e.IsActive and e.IsRestoration
                order by dus.Sequence, e.ID";


        public static List<DashboardUserSettings> FindSettings(int dispatchId)
        {
            List<DashboardUserSettings> result = new List<DashboardUserSettings>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindSettings))
            {
                Database.PutParameter(dbCommand, "?DispatchId", dispatchId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        DashboardUserSettings settings = Load(dataReader);
                        settings.Technician = Employee.Load(dataReader, FieldsCount);
                        result.Add(settings);
                    }
                }
            }

            return result;
        }

        #endregion

        #region IsTechnicianVisitble

        private const string SqlIsTechnicianVisible =
            @"SELECT * FROM DashboardUserSettings d
                where DispatchId = ?DispatchId and TechnicianId = ?TechnicianId";


        public static bool IsTechnicianVisitble(int dispatchId, int technicianId)
        {
            bool result;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlIsTechnicianVisible))
            {
                Database.PutParameter(dbCommand, "?DispatchId", dispatchId);
                Database.PutParameter(dbCommand, "?TechnicianId", technicianId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                    {
                        DashboardUserSettings settings = Load(dataReader);
                        result = settings.IsVisible;
                    } else
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        #endregion

        #region DeleteSettings

        private const string SqlDeleteSettings =
            @"Delete From DashboardUserSettings 
                where DispatchId = ?DispatchId";

        public static void DeleteSettings(int dispatchId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteSettings))
            {
                Database.PutParameter(dbCommand, "?DispatchId", dispatchId);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
      