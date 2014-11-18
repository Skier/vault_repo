using System;
using System.Collections.Generic;
using System.Data;
using SmartSchedule.Data;

namespace SmartSchedule.Domain
{
    public partial class TechnicianWorkTimeDefault
    {
        public TechnicianWorkTimeDefault(){}

        #region Convert

        public TechnicianWorkTime Convert()
        {
            return new TechnicianWorkTime(m_technicianId, m_timeStart, m_timeEnd);
        }

        public static TechnicianWorkTimeDefault Convert(TechnicianWorkTime workTime)
        {
            return new TechnicianWorkTimeDefault(workTime.TechnicianId, workTime.TimeStart, 
                workTime.TimeEnd);
        }


        #endregion

        #region FindByTechnician

        private const string SqlFindByTechnician =
            @"SELECT *
            FROM TechnicianWorkTimeDefault
                WHERE TechnicianId = ?DefaultTechnicianId";

        public static List<TechnicianWorkTimeDefault> FindByTechnician(Technician technician)
        {
            List<TechnicianWorkTimeDefault> result = new List<TechnicianWorkTimeDefault>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTechnician))
            {
                Database.PutParameter(dbCommand, "?DefaultTechnicianId", technician.TechnicianDefaultId);

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
            FROM TechnicianWorkTimeDefault
                WHERE TechnicianId = ?TechnicianId";

        public static void DeleteByTechnician(TechnicianDefault technician)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByTechnician))
            {
                Database.PutParameter(dbCommand, "?TechnicianId", technician.ID);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
      