using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using SmartSchedule.Data;
using SmartSchedule.SDK;

namespace SmartSchedule.Domain
{
    public partial class TechnicianServiceDeny
    {
        public TechnicianServiceDeny(){ }        

        #region OnSerialize

        [DataMember]
        public string ServiceName { get; set; }

        [OnSerializing]
        internal void OnSerialize(StreamingContext context)
        {
            if (Configuration.IsClientApplication || Configuration.IsOptimizer)
                return;

            ServiceName = Service.GetService(m_serviceId).Name;
        }

        #endregion


        #region FindByTechnician

        private const string SqlFindByTechnician =
            @"SELECT * FROM technicianservicedeny
                where TechnicianId = ?TechnicianId";

        public static List<TechnicianServiceDeny> FindByTechnician(Technician technician)
        {
            List<TechnicianServiceDeny> result = new List<TechnicianServiceDeny>();

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
            FROM TechnicianServiceDeny
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

        #region ToString

        public override string ToString()
        {
            if (IsForNonExclusive)
                return ServiceName + " (non-excl)";
            return ServiceName;
        }

        #endregion
    }
}
      