using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SmartSchedule.Data;
using SmartSchedule.SDK;

namespace SmartSchedule.Domain.Sync
{
    [DataContract]
    public class RecommendationResponseItem : IComparable<RecommendationResponseItem>
    {
        #region Constructor

        public RecommendationResponseItem(string guid, DateTime dateSchedule, string timeFrame, 
            int rank, decimal frameCost, bool isSecondaryArea, bool isBucket)
        {
            m_guid = guid;
            m_dateSchedule = dateSchedule;
            m_timeFrame = timeFrame;
            m_rank = rank;
            m_frameCost = frameCost;
            m_isSecondaryArea = isSecondaryArea;
            m_isBucket = isBucket;
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

        #region DateSchedule

        private DateTime m_dateSchedule;
        [DataMember]
        public DateTime DateSchedule
        {
            get { return m_dateSchedule; }
            set { m_dateSchedule = value; }
        }

        #endregion

        #region TimeFrame

        private string m_timeFrame;
        [DataMember]
        public string TimeFrame
        {
            get { return m_timeFrame; }
            set { m_timeFrame = value; }
        }

        #endregion

        #region Rank

        private int m_rank;
        [DataMember]
        public int Rank
        {
            get { return m_rank; }
            set { m_rank = value; }
        }

        #endregion        

        #region FrameCost

        private decimal m_frameCost;
        [DataMember]
        public decimal FrameCost
        {
            get { return m_frameCost; }
            set { m_frameCost = value; }
        }

        public string FrameCostText
        {
            get
            {
                if (IsBucket)
                    return string.Empty;
                return m_frameCost.ToString();
            }
        }

        #endregion

        #region IsSecondaryArea

        private bool m_isSecondaryArea;
        [DataMember]
        public bool IsSecondaryArea
        {
            get { return m_isSecondaryArea; }
            set { m_isSecondaryArea = value; }
        }

        public string IsSecondaryAreaText
        {
            get { return IsSecondaryArea ? "Yes" : string.Empty; }
        }

        #endregion

        #region IsBucket

        private bool m_isBucket;
        [DataMember]
        public bool IsBucket
        {
            get { return m_isBucket; }
            set { m_isBucket = value; }
        }

        public string IsBucketText
        {
            get { return IsBucket ? "Yes" : string.Empty; }
        }

        #endregion

        #region Comparision

        //Less is better
        public int CompareTo(RecommendationResponseItem other)
        {
            if (DateSchedule == other.DateSchedule
                && FrameCost == other.FrameCost
                && IsSecondaryArea == other.IsSecondaryArea
                && IsBucket == other.IsBucket)
            {
                return Utils.GetTimeFrameId(TimeFrame).CompareTo(
                    Utils.GetTimeFrameId(other.TimeFrame));
            }

            if (IsBucket != other.IsBucket)
                return IsBucket ? 1 : -1;

            if (IsSecondaryArea != other.IsSecondaryArea)
                return IsSecondaryArea ? 1 : -1;

            if (DateSchedule == other.DateSchedule)
                return FrameCost.CompareTo(other.FrameCost);

            if (IsCostGod && other.IsCostGod)
            {
                if (IsDeltaHigh(other))
                    return FrameCost.CompareTo(other.FrameCost);
                return DateSchedule.CompareTo(other.DateSchedule);
            }

            if (IsCostGod != other.IsCostGod)
                return IsCostGod ? -1 : 1;

            //Both costs bad
            if (IsDeltaHigh(other))
                return FrameCost.CompareTo(other.FrameCost);
            return -DateSchedule.CompareTo(other.DateSchedule);
        }

        private bool IsCostGod
        {
            get
            {
                DateTime today = DateTime.Now.Date;
                if (!Configuration.IsRealtimeMode)
                    today = new DateTime(2009, 7, 13);

                if ((int)DateSchedule.Subtract(today).TotalDays < 2)
                    return FrameCost <= 2;
                if ((int)DateSchedule.Subtract(today).TotalDays == 2)
                    return FrameCost <= 4;
                if ((int)DateSchedule.Subtract(today).TotalDays == 3)
                    return FrameCost <= 6;
                if ((int)DateSchedule.Subtract(today).TotalDays == 4)
                    return FrameCost <= 8;
                return FrameCost <= 10;
            }
        }

        private bool IsDeltaHigh(RecommendationResponseItem other)
        {
            decimal delta = Math.Abs(FrameCost - other.FrameCost);
            if (delta > GetDeltaThreshold(other))
                return true;
            return false;
        }

        private decimal GetDeltaThreshold(RecommendationResponseItem other)
        {
            decimal minCost = Math.Min(FrameCost, other.FrameCost);
            if (minCost < 10)
                return 3;
            return minCost*(decimal)0.25;
        }

        #endregion


        #region Insert

        private const string SqlInsert =
            @"insert into smrt_rs (rq_guid, d_schedule, time_frame, rank, frame_cost, sec_area, is_bucket)
              Values (?, ?, ?, ?, ?, ?, ?)";

        public static void Insert(RecommendationResponseItem responseItem, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
            {
                Database.PutParameter(dbCommand, "@rq_guid", responseItem.Guid);
                Database.PutParameter(dbCommand, "@d_schedule", responseItem.DateSchedule);
                Database.PutParameter(dbCommand, "@time_frame", responseItem.TimeFrame);
                Database.PutParameter(dbCommand, "@rank", responseItem.Rank);
                Database.PutParameter(dbCommand, "@frame_cost", (float)Math.Round(responseItem.FrameCost, 2));
                Database.PutParameter(dbCommand, "@sec_area", responseItem.IsSecondaryArea);
                Database.PutParameter(dbCommand, "@is_bucket", responseItem.IsBucket);
                dbCommand.ExecuteNonQuery();
            }            
        }

        #endregion

        #region GetResponseItems

        public static List<RecommendationResponseItem> GetResponseItems(RecommendationRequest request, 
            BookingEngine bookingEngine)
        {            
            List<RecommendationResponseItem> result = new List<RecommendationResponseItem>();
            Coordinate coordinate = GoogleGeocoder.Geocode(request.Address);

            int? exclusiveTechId = null;
            int? forbiddenTechId = null;
            if (request.ExclusiveTechnicianServmanId != string.Empty)
            {
                if (Technician.GetTechnician(request.ExclusiveTechnicianServmanId) != null)
                    exclusiveTechId = Technician.GetTechnician(request.ExclusiveTechnicianServmanId).ID;
                else
                    Host.Trace("GetResponseItems", string.Format("!!!ERROR, Specified technician {0} doesn't exists. Exclusivity ignored",
                        request.ExclusiveTechnicianServmanId));
            }
                
            if (request.ForbiddenTechnicianServmanId != string.Empty)
            {
                if (Technician.GetTechnician(request.ForbiddenTechnicianServmanId) != null)
                    forbiddenTechId = Technician.GetTechnician(request.ForbiddenTechnicianServmanId).ID;
                else
                    Host.Trace("GetResponseItems", string.Format("!!!ERROR, Specified technician {0} doesn't exists. Forbidden Technician ignored",
                        request.ForbiddenTechnicianServmanId));                
            }
                
            decimal durationCost = request.Cost;
            if (request.IsEstimateAndDo)
                durationCost = 200;            

            List<KeyValuePair<DateTime, TimeFrame>> dateFrameList = new List<KeyValuePair<DateTime, TimeFrame>>();
            foreach (DateTime scheduleDate in request.ScheduleDates)
            {
                if (PredictionIgnore.IsDateIgnored(scheduleDate))
                    continue;

                foreach (var timeFrame in Domain.TimeFrame.TimeFrames.Values)
                    dateFrameList.Add(new KeyValuePair<DateTime, TimeFrame>(scheduleDate, timeFrame));                
            }

            Parallel.ForEach(dateFrameList, item => ProcessTimeFrame(item.Key, item.Value, request,
                bookingEngine, coordinate, exclusiveTechId, forbiddenTechId, durationCost, result));
            result.Sort();

            if (result.Count == 0)
            {
                result.Add(new RecommendationResponseItem(request.Guid, 
                    request.ScheduleDates[0], "ANY", 0, 0, false, true));                
            }

            for (int i = 0; i < result.Count; i++)
                result[i].Rank = i + 1;
            return result;
        }

        private static void ProcessTimeFrame(DateTime scheduleDate, TimeFrame timeFrame, RecommendationRequest request,
            BookingEngine bookingEngine, Coordinate coordinate, int? exclusiveTechId, 
            int? forbiddenTechId, decimal durationCost, List<RecommendationResponseItem> resultCollection)
        {
            Visit visit = bookingEngine.GetNewVisit(scheduleDate.Date, timeFrame, request.Cost,
                coordinate.Latitude, coordinate.Longitude, request.Zip, null, exclusiveTechId, null,
                false, forbiddenTechId, request.TicketNumber, null, string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, DateTime.Now, request.IsEstimate,
                request.IsEstimateAndDo, request.IsRework, string.Empty, string.Empty, string.Empty,
                false, false, string.Empty, 1, string.Empty, 0, null, null, string.Empty, null, string.Empty,
                false, string.Empty, null, 0, 0, durationCost, false);
            visit.Details = new List<VisitDetail>();
            foreach (var serviceType in request.ServiceTypes)
                visit.Details.Add(new VisitDetail(0, 0, int.Parse(serviceType), 0, string.Empty, 0));

            VisitAddResult addResult = bookingEngine.CanInsertVisit(visit, false);

            RecommendationResponseItem responseItem = new RecommendationResponseItem(request.Guid,
                scheduleDate.Date, timeFrame.Text, 0, (decimal)addResult.CostChange,
                addResult.SecondaryArea, !addResult.IsAddAllowed);
            lock (resultCollection)
                resultCollection.Add(responseItem);            
        }

        #endregion

        #region FindResponse

        private const string SqlFindResponse =
            @"select * from smrt_rs where rq_guid = ?";

        public static List<RecommendationResponseItem> FindResponse(RecommendationRequest request)
        {
            List<RecommendationResponseItem> result = new List<RecommendationResponseItem>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindResponse, ConnectionKeyEnum.Servman))
            {
                Database.PutParameter(dbCommand, "@rq_guid", request.Guid);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(new RecommendationResponseItem(
                            dataReader.GetString(0).Trim(),
                            dataReader.GetDateTime(1),
                            dataReader.GetString(2).Trim(),
                            (int)dataReader.GetDecimal(3),                            
                            dataReader.GetDecimal(4),
                            dataReader.GetBoolean(5),
                            dataReader.GetBoolean(6)));
                    }
                }
            }

            return result;
        }

        #endregion

        #region DeleteResponse

        private const string SqlDeleteResponse =
            @"delete from smrt_rs where rq_guid = ?";

        public static void DeleteResponse(RecommendationRequest request)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteResponse, ConnectionKeyEnum.Servman))
            {
                Database.PutParameter(dbCommand, "@rq_guid", request.Guid);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
