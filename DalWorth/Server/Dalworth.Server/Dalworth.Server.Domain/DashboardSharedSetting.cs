using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Domain
{
    public partial class DashboardSharedSetting
    {
        public DashboardSharedSetting() {}

        #region Technician

        private Employee m_technician;
        public Employee Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
        }

        #endregion

        #region IsContainsSettings

        private const string SqlIsContainsSettings =
            @"select * from DashboardSharedSetting
                where Date(DashboardDate) = ?DashboardDate
             limit 1";

        public static bool IsContainsSettings(DateTime date)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlIsContainsSettings))
            {
                Database.PutParameter(dbCommand, "?DashboardDate", date.Date);

                using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
                {
                    return reader.Read();
                }
            }
        }

        #endregion

        #region FindSettings

        private const string SqlFindSettings =
            @"select * from DashboardSharedSetting dss
                inner join Employee e on dss.TechnicianId = e.ID
                where Date(DashboardDate) = ?DashboardDate
                order by dss.Sequence";

        public static List<DashboardSharedSetting> FindSettings(DateTime date)
        {
            List<DashboardSharedSetting> result = new List<DashboardSharedSetting>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindSettings))
            {
                Database.PutParameter(dbCommand, "?DashboardDate", date.Date);

                using (IDataReader reader = dbCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DashboardSharedSetting setting = Load(reader);
                        setting.Technician = Employee.Load(reader, FieldsCount);
                        result.Add(setting);
                    }                        
                }
            }

            return result;
        }

        #endregion

        #region DeleteSettings

        private const string SqlDeleteSettings =
            @"Delete From DashboardSharedSetting
                where Date(DashboardDate) = ?DashboardDate";

        public static void DeleteSettings(DateTime dashboardDate)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteSettings))
            {
                Database.PutParameter(dbCommand, "?DashboardDate", dashboardDate.Date);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
      