using System;
using System.Collections.Generic;
using System.Data;
using SmartSchedule.Data;

namespace SmartSchedule.Domain
{
    public partial class TechnicianWorkTimeDefaultPreset
    {
        public TechnicianWorkTimeDefaultPreset(){}

        #region FindByTechnician

        private const string SqlFindByTechnician =
            @"SELECT *
            FROM TechnicianWorkTimeDefaultPreset
                WHERE TechnicianId = ?TechnicianId 
            order by PresetNumber";

        public static List<TechnicianWorkTimeDefaultPreset> FindByTechnician(int technicianDefaultId)
        {
            List<TechnicianWorkTimeDefaultPreset> result = new List<TechnicianWorkTimeDefaultPreset>();

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
            FROM TechnicianWorkTimeDefaultPreset
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
      