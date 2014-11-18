using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using SmartSchedule.Data;
using SmartSchedule.SDK;

namespace SmartSchedule.Domain.Sync
{
    [DataContract]
    public class RecommendationRequest
    {
        #region Constructor

        public RecommendationRequest(string guid, string address, string zip, decimal cost, string exclusiveTechnicianServmanId, string forbiddenTechnicianServmanId, bool isEstimate, bool isEstimateAndDo, bool isRework)
        {
            m_guid = guid;
            m_address = address;
            m_zip = zip;
            m_cost = cost;
            m_exclusiveTechnicianServmanId = exclusiveTechnicianServmanId;
            m_forbiddenTechnicianServmanId = forbiddenTechnicianServmanId;
            m_isEstimate = isEstimate;
            m_isEstimateAndDo = isEstimateAndDo;
            m_isRework = isRework;
            m_serviceTypes = new List<string>();
            m_scheduleDates = new List<DateTime>();
        }

        #endregion        

        #region Guid

        private string m_guid;
        [DataMember]
        public string Guid
        {
            get { return m_guid; }
            set { m_guid = value; }
        }

        #endregion

        #region Address

        private string m_address;
        [DataMember]
        public string Address
        {
            get { return m_address; }
            set { m_address = value; }
        }

        #endregion

        #region Zip

        private string m_zip;
        [DataMember]
        public string Zip
        {
            get { return m_zip; }
            set { m_zip = value; }
        }

        #endregion

        #region Cost

        private decimal m_cost;
        [DataMember]
        public decimal Cost
        {
            get { return m_cost; }
            set { m_cost = value; }
        }

        #endregion

        #region ExclusiveTechnicianServmanId

        private string m_exclusiveTechnicianServmanId;
        [DataMember]
        public string ExclusiveTechnicianServmanId
        {
            get { return m_exclusiveTechnicianServmanId; }
            set { m_exclusiveTechnicianServmanId = value; }
        }

        #endregion

        #region ForbiddenTechnicianServmanId

        private string m_forbiddenTechnicianServmanId;
        [DataMember]
        public string ForbiddenTechnicianServmanId
        {
            get { return m_forbiddenTechnicianServmanId; }
            set { m_forbiddenTechnicianServmanId = value; }
        }

        #endregion

        #region IsEstimate

        private bool m_isEstimate;
        [DataMember]
        public bool IsEstimate
        {
            get { return m_isEstimate; }
            set { m_isEstimate = value; }
        }

        #endregion

        #region IsEstimateAndDo

        private bool m_isEstimateAndDo;
        [DataMember]
        public bool IsEstimateAndDo
        {
            get { return m_isEstimateAndDo; }
            set { m_isEstimateAndDo = value; }
        }

        #endregion

        #region IsRework

        private bool m_isRework;
        [DataMember]
        public bool IsRework
        {
            get { return m_isRework; }
            set { m_isRework = value; }
        }

        #endregion

        #region ServiceTypes

        private List<string> m_serviceTypes;
        [DataMember]
        public List<string> ServiceTypes
        {
            get { return m_serviceTypes; }
            set { m_serviceTypes = value; }
        }

        #endregion

        #region ScheduleDates

        private List<DateTime> m_scheduleDates;
        [DataMember]
        public List<DateTime> ScheduleDates
        {
            get { return m_scheduleDates; }
            set { m_scheduleDates = value; }
        }

        #endregion


        #region TicketNumber

        private string m_ticketNumber;
        public string TicketNumber
        {
            get { return m_ticketNumber; }
            set { m_ticketNumber = value; }
        }

        #endregion


        #region GetUnprocessedRequest

        private const string SqlFindUnprocessedRequest =
            @"select * from smrt_rq";
        private const string SqlFindUnprocessedRequestDetails =
            @"select * from dsmrt_rq where rq_guid = ?";
        private const string SqlFindUnprocessedRequestDates =
            @"select * from smrt_rqd where rq_guid = ?";

        public static List<RecommendationRequest> FindUnprocessedRequests(IDbConnection connection)
        {
            List<RecommendationRequest> result = new List<RecommendationRequest>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindUnprocessedRequest, connection))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(new RecommendationRequest(
                            dataReader.GetString(0).Trim(),
                            dataReader.GetString(1).Trim(),
                            dataReader.GetString(2).Trim(),
                            dataReader.GetDecimal(3),
                            dataReader.GetString(4).Trim(),
                            dataReader.GetString(5).Trim(),
                            dataReader.GetBoolean(6),
                            dataReader.GetBoolean(7),
                            dataReader.GetBoolean(8)));                        
                    }
                }
            }

            foreach (RecommendationRequest request in result)
            {
                using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindUnprocessedRequestDetails, connection))
                {                    
                    Database.PutParameter(dbCommand, "@rq_guid", request.Guid);
                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while (dataReader.Read())
                            request.ServiceTypes.Add(dataReader.GetString(1).Trim());
                    }
                }

                using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindUnprocessedRequestDates, connection))
                {
                    Database.PutParameter(dbCommand, "@rq_guid", request.Guid);
                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {                        
                        while (dataReader.Read())
                            request.ScheduleDates.Add(dataReader.GetDateTime(1));
                    }
                }                
            }

            return result;
        }

        #endregion

        #region DeleteRequest

        private const string SqlDeleteRequest =
            @"delete from smrt_rq where rq_guid = ?";
        private const string SqlDeleteRequestDetails =
            @"delete from dsmrt_rq where rq_guid = ?";
        private const string SqlDeleteRequestDates =
            @"delete from smrt_rqd where rq_guid = ?";

        public static void DeleteRequest(RecommendationRequest request, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteRequest, connection))
            {
                Database.PutParameter(dbCommand, "@rq_guid", request.Guid);
                dbCommand.ExecuteNonQuery();
            }

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteRequestDetails, connection))
            {
                Database.PutParameter(dbCommand, "@rq_guid", request.Guid);
                dbCommand.ExecuteNonQuery();
            }

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteRequestDates, connection))
            {
                Database.PutParameter(dbCommand, "@rq_guid", request.Guid);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion

        #region Insert

        private const string SqlInsert =
            @"insert into smrt_rq (rq_guid, address, zip, cost, ex_tech_id, fb_tech_id, is_est, is_est_do, is_rework)
              Values (?, ?, ?, ?, ?, ?, ?, ?, ?)";

        private const string SqlInsertDetails =
            @"insert into dsmrt_rq (rq_guid, serv_type)
              Values (?, ?)";

        private const string SqlInsertDates =
            @"insert into smrt_rqd (rq_guid, d_schedule)
              Values (?, ?)";

        public static void Insert(RecommendationRequest request)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlInsertDetails,
                ConnectionKeyEnum.Servman))
            {
                foreach (string serviceType in request.ServiceTypes)
                {
                    if (request.ServiceTypes.IndexOf(serviceType) == 0)
                    {
                        Database.PutParameter(dbCommand, "@rq_guid", request.Guid);
                        Database.PutParameter(dbCommand, "@serv_type", serviceType);                        
                    }
                    else
                    {
                        Database.UpdateParameter(dbCommand, "@rq_guid", request.Guid);
                        Database.UpdateParameter(dbCommand, "@serv_type", serviceType);                                                
                    }
                }

                dbCommand.ExecuteNonQuery();
            }

            DateTime startDate;
            if (Configuration.IsRealtimeMode)
                startDate = DateTime.Now.Date;
            else
                startDate = new DateTime(2009, 7, 13);

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlInsertDates,
                ConnectionKeyEnum.Servman))
            {
                for (int i = 0; i < 5; i++)
                {
                    DateTime scheduleDate = startDate.AddDays(i);
                    if (i == 0)
                    {
                        Database.PutParameter(dbCommand, "@rq_guid", request.Guid);
                        Database.PutParameter(dbCommand, "@d_schedule", scheduleDate.Date);
                    }
                    else
                    {
                        Database.UpdateParameter(dbCommand, "@rq_guid", request.Guid);
                        Database.UpdateParameter(dbCommand, "@d_schedule", scheduleDate.Date);
                    }
                    dbCommand.ExecuteNonQuery();
                }
            }
          
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
            {
                Database.PutParameter(dbCommand, "@rq_guid", request.Guid);
                Database.PutParameter(dbCommand, "@address", request.Address);
                Database.PutParameter(dbCommand, "@zip", request.Zip);
                Database.PutParameter(dbCommand, "@cost", (double)request.Cost);
                Database.PutParameter(dbCommand, "@ex_tech_id", request.ExclusiveTechnicianServmanId);
                Database.PutParameter(dbCommand, "@fb_tech_id", request.ForbiddenTechnicianServmanId);
                Database.PutParameter(dbCommand, "@is_est", request.IsEstimate);
                Database.PutParameter(dbCommand, "@is_est_do", request.IsEstimateAndDo);
                Database.PutParameter(dbCommand, "@is_rework", request.IsRework);
                dbCommand.ExecuteNonQuery();
            }            
        }

        #endregion
    }
}
