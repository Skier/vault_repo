using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using SmartSchedule.Data;

namespace SmartSchedule.Domain
{    
    public partial class TechnicianWorkTime
    {
        public TechnicianWorkTime(){}

        #region FindByTechnician

        private const string SqlFindByTechnician =
            @"SELECT *
            FROM TechnicianWorkTime
                WHERE TechnicianId = ?TechnicianId";

        public static List<TechnicianWorkTime> FindByTechnician(Technician technician)
        {
            List<TechnicianWorkTime> result = new List<TechnicianWorkTime>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTechnician))
            {
                Database.PutParameter(dbCommand, "?TechnicianId", technician.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;

        }

        #endregion

        #region DeleteByTechnician

        private const string SqlDeleteByTechnician =
            @"DELETE
            FROM TechnicianWorkTime
                WHERE TechnicianId = ?TechnicianId";

        public static void DeleteByTechnician(Technician technician)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByTechnician))
            {
                Database.PutParameter(dbCommand, "?TechnicianId", technician.ID);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region GetInterval

        public TimeInterval GetInterval()
        {
            return new TimeInterval(m_timeStart, m_timeEnd);
        }

        #endregion
    }
}
      