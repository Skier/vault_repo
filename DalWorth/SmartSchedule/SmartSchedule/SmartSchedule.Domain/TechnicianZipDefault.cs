using System;
using System.Collections.Generic;
using System.Data;
using SmartSchedule.Data;

namespace SmartSchedule.Domain
{
    public partial class TechnicianZipDefault
    {
        public TechnicianZipDefault(){}

        #region Convert

        public TechnicianZip Convert()
        {
            return new TechnicianZip(m_technicianId, m_zip, m_isPrimaryZip);
        }

        public static TechnicianZipDefault Convert(TechnicianZip technicianZip)
        {
            return new TechnicianZipDefault(technicianZip.TechnicianId, technicianZip.Zip,
                technicianZip.IsPrimaryZip);
        }

        #endregion

        #region FindByTechnician

        private const string SqlFindByTechnician =
            @"SELECT *
            FROM TechnicianZipDefault
                WHERE TechnicianId = ?TechnicianDefaultId";

        public static List<TechnicianZipDefault> FindByTechnician(Technician technician)
        {
            List<TechnicianZipDefault> result = new List<TechnicianZipDefault>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTechnician))
            {
                Database.PutParameter(dbCommand, "?TechnicianDefaultId", technician.TechnicianDefaultId);

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
            FROM TechnicianZipDefault
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
      