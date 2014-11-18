using System;
using System.Collections.Generic;
using System.Data;
using SmartSchedule.Data;

namespace SmartSchedule.Domain
{
    public partial class TechnicianZip
    {
        public TechnicianZip(){ }

        #region FindByTechnician

        private const string SqlFindByTechnician =
            @"SELECT *
            FROM TechnicianZip
                WHERE TechnicianId = ?TechnicianId";

        public static List<TechnicianZip> FindByTechnician(int technicianId)
        {
            List<TechnicianZip> result = new List<TechnicianZip>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTechnician))
            {
                Database.PutParameter(dbCommand, "?TechnicianId", technicianId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;

        }

        #endregion

        #region FindZipsByTechnicians

        public static List<string> FindZipsByTechnicians(IList<Technician> technicians)
        {
            string SqlFindZipsByTechnicians =
                @"SELECT distinct zip FROM technicianzip
                    where TechnicianId in ({0})";

            List<string> result = new List<string>();
            if (technicians.Count == 0)
                return result;

            string technicianIds = string.Empty;
            foreach (Technician technician in technicians)
                technicianIds += technician.ID + ", ";
            technicianIds = technicianIds.Substring(0, technicianIds.Length - 2);
            SqlFindZipsByTechnicians = string.Format(SqlFindZipsByTechnicians, technicianIds);

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindZipsByTechnicians))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(dataReader.GetString(0));
                }
            }

            return result;
        }

        #endregion

        #region FindAllZipCodes

        private const string SqlFindAllZipCodes =
            @"SELECT distinct zip FROM technicianzip";

        public static List<string> FindAllZipCodes()
        {
            List<string> result = new List<string>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindAllZipCodes))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(dataReader.GetString(0));
                }
            }

            return result;

        }

        #endregion

        #region DeleteByTechnician

        private const string SqlDeleteByTechnician =
            @"DELETE
            FROM TechnicianZip
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
    }
}
      