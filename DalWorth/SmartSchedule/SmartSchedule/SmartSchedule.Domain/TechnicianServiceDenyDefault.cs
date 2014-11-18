using System;
using System.Collections.Generic;
using System.Data;
using SmartSchedule.Data;

namespace SmartSchedule.Domain
{
    public partial class TechnicianServiceDenyDefault
    {
        public TechnicianServiceDenyDefault(){}

        #region Convert

        public TechnicianServiceDeny Convert()
        {
            return new TechnicianServiceDeny(m_technicianId, m_serviceId, m_isForNonExclusive);
        }

        public static TechnicianServiceDenyDefault Convert(TechnicianServiceDeny serviceDeny)
        {
            return new TechnicianServiceDenyDefault(serviceDeny.TechnicianId, serviceDeny.ServiceId,
                serviceDeny.IsForNonExclusive);
        }

        #endregion

        #region FindByTechnician

        private const string SqlFindByTechnician =
            @"SELECT * FROM TechnicianServiceDenyDefault
                where TechnicianId = ?TechnicianId";

        public static List<TechnicianServiceDenyDefault> FindByTechnician(int technicianDefaultId)
        {
            List<TechnicianServiceDenyDefault> result = new List<TechnicianServiceDenyDefault>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTechnician))
            {
                Database.PutParameter(dbCommand, "?TechnicianId", technicianDefaultId);

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
            FROM TechnicianServiceDenyDefault
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
      