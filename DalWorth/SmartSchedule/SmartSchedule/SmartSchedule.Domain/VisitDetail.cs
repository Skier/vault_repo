using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;
using SmartSchedule.Data;
using SmartSchedule.SDK;
using System.Linq;

namespace SmartSchedule.Domain
{
    public partial class VisitDetail
    {
        public VisitDetail(){ }

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


        #region FindByVisit

        private const string SqlFindByVisit =
            @"select * from VisitDetail
                where VisitId = ?VisitId";

        public static List<VisitDetail> FindByVisit(Visit visit)
        {
            object o = Service.Services; //make sure services initialized before query run

            List<VisitDetail> result = new List<VisitDetail>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByVisit))
            {
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion

        #region DeleteByVisit

        private const string SqlDeleteByVisit =
            @"delete from VisitDetail                
                where VisitId = ?VisitId";

        public static void DeleteByVisit(Visit visit)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByVisit))
            {
                Database.PutParameter(dbCommand, "?VisitId", visit.ID);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region Clear

        private const string SqlClearByDate =
            @"delete FROM VisitDetail where VisitId in 
                (SELECT ID FROM visit v where Date(TimeStart) = ?Date)";

        public static void Clear(DateTime date)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlClearByDate))
            {
                Database.PutParameter(dbCommand, "?Date", date.Date);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region IsEqual

        public static bool IsEqual(List<VisitDetail> details1, List<VisitDetail> details2)
        {
            if (details1.Count != details2.Count)
                return false;

            List<VisitDetail> sortedDetails1 = details1.OrderBy(visitDetail => visitDetail.ServiceId).ToList();
            List<VisitDetail> sortedDetails2 = details2.OrderBy(visitDetail => visitDetail.ServiceId).ToList();



            for (int i = 0; i < sortedDetails1.Count; i++)
            {
                if (sortedDetails1[i].ServiceId != sortedDetails2[i].ServiceId)
                    return false;
            }

            return true;            
        }

        #endregion
    }
}
      