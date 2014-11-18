using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using SmartSchedule.Data;
using SmartSchedule.Domain;
using SmartSchedule.Domain.Sync;
using SmartSchedule.Domain.WCF;
using SmartSchedule.SDK;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.MainForm
{
    public class MainFormModel : IModel
    {
        #region Init

        public void Init()
        {

        }

        #endregion

        #region Technicians

        private BindingList<Technician> m_technicians;
        public BindingList<Technician> Technicians
        {
            get { return m_technicians; }
            set { m_technicians = value; }
        }

        #endregion

        #region Visits

        private BindingListVisit m_visits;
        public BindingListVisit Visits
        {
            get { return m_visits; }
            set { m_visits = value; }
        }

        #endregion

        #region EstimatedDashboardInfo

        private DashboardStatisticInfo m_estimatedDashboardInfo;
        public DashboardStatisticInfo EstimatedDashboardInfo
        {
            get { return m_estimatedDashboardInfo; }
            set { m_estimatedDashboardInfo = value; }
        }

        #endregion


        #region Init Form

        public List<Technician> GetTechnicians(bool getDefaultTechnicians)
        {
            var result = WcfClient.WcfClient.Instance.GetTechnicians(
                WcfClient.WcfClient.ClientDate, getDefaultTechnicians, false);
            Technician.CacheClient(result);
            return result;
        }

        public VisitsFullChangeDetail GetFullViewInfo()
        {
            return WcfClient.WcfClient.Instance.GetFullViewInfo(WcfClient.WcfClient.ClientDate);
        }

        #endregion


        #region Import

        public void ImportTickets()
        {
            WcfClient.WcfClient.Instance.RunSync(SyncTypeEnum.Visits);
        }

        public void ImportSettings()
        {
            WcfClient.WcfClient.Instance.RunSync(SyncTypeEnum.TechnicianSettings);            
        }

        #endregion

        #region MarkTimeAs

        public void MarkTimeAs(TimeInterval interval, Technician technician, bool markAsWorking)
        {
            WcfClient.WcfClient.Instance.MarkTimeAs(interval, technician.ID, markAsWorking);
        }

        #endregion

        #region BookDelayedVisit

        public bool BookDelayedVisit(int visitId, RecommendationResponseItem item)
        {
            return WcfClient.WcfClient.Instance.BookDelayedVisit(visitId, item);
        }

        #endregion

        #region GetExistingSnapshotDate

        public DateTime? GetExistingSnapshotDate()
        {
            return WcfClient.WcfClient.Instance.GetExistingSnapshotDate();
        }

        #endregion

        #region CreateSnapshot

        public void CreateSnapshot()
        {
            WcfClient.WcfClient.Instance.CreateSnapshot();
        }

        #endregion

        #region UnscheduleVisit

        public void UnscheduleVisit(Visit visit)
        {
            WcfClient.WcfClient.Instance.UnscheduleVisit(visit.TicketNumber);
        }

        #endregion

        #region UpdateVisit

        public void UpdateVisit(Visit visit)
        {
            WcfClient.WcfClient.Instance.UpdateVisit(visit, false, true);
        }

        #endregion

        #region KeepAliveDummy

        public void KeepAliveDummy()
        {
            WcfClient.WcfClient.Instance.KeepAliveDummy();
        }

        #endregion

        #region GetGoogleMapRouteUrl

        public string GetGoogleMapRouteUrl(Technician technician, IList<Visit> visits)
        {
            Visit firstNonBlockout = null;
            foreach (var visit in visits)
            {
                if (!visit.IsBlockout)
                {
                    firstNonBlockout = visit;
                    break;
                }
            }

            if (firstNonBlockout == null)
                return string.Empty;

            string result = string.Format("http://maps.google.com/maps?f=d&source=s_d&saddr={0},{1}&daddr={2},{3}",
                technician.DepotLatitude, technician.DepotLongitude, firstNonBlockout.Latitude, firstNonBlockout.Longitude);

            for (int i = visits.IndexOf(firstNonBlockout) + 1; i < visits.Count; i++)
            {
                if (visits[i].IsBlockout)
                    continue;
                result += string.Format("+to:{0},{1}", visits[i].Latitude, visits[i].Longitude);
            }

            result += string.Format("+to:{0},{1}", technician.DepotLatitude, technician.DepotLongitude);
            return result;
        }

        #endregion

        #region GetTechnicianDriveDistance

        public static double GetTechnicianDriveDistance(Technician technician, IList<Visit> visits, out double penalty)
        {
            double result = 0;
            penalty = 0;

            if (visits.Count == 0)
                return result;

            double tempDistance = technician.Distance(visits[0]);
            result += tempDistance;
            if (tempDistance > Utils.PENALTY_THRESHOLD)
                penalty += tempDistance - Utils.PENALTY_THRESHOLD;

            tempDistance = technician.Distance(visits[visits.Count - 1]);
            result += tempDistance;
            if (tempDistance > Utils.PENALTY_THRESHOLD)
                penalty += tempDistance - Utils.PENALTY_THRESHOLD;

            for (int i = 1; i < visits.Count; i++)
            {
                tempDistance = visits[i].Distance(visits[i - 1]);
                result += tempDistance;
                if (tempDistance > Utils.PENALTY_THRESHOLD)
                    penalty += tempDistance - Utils.PENALTY_THRESHOLD;
            }
                
            return result;
        }

        #endregion

        #region OptimizeRpm

        public void OptimizeRpm(int technicianId)
        {
            //WcfClient.WcfClient.Instance.OptimizeRpm(technicianId);
        }

        #endregion

        #region SendRecommendationRequest

        public RecommendationRequest SendRecommendationRequest(Visit visit)
        {   
            RecommendationRequest request = new RecommendationRequest(Guid.NewGuid().ToString(), 
                visit.Address + " " + visit.Zip, visit.Zip, visit.Cost,
                visit.ExclusiveTechnicianDefaultId.HasValue ? visit.ExclusiveTechnician.ServmanId : string.Empty,
                visit.ForbiddenTechnicianDefaultId.HasValue ? visit.ForbiddenTechnician.ServmanId : string.Empty,
                visit.IsEstimate, visit.IsEstimateAndDo, visit.IsRework);
            request.TicketNumber = visit.TicketNumber;

            foreach (var visitDetail in visit.Details)
                request.ServiceTypes.Add(visitDetail.ServiceId.ToString());
            RecommendationRequest.Insert(request);
            Connection.DeleteInstance(ConnectionKeyEnum.Servman);
            return request;
        }

        #endregion

        #region ModifyPredictionIgnoreDate

        public void ModifyPredictionIgnoreDate(bool isIgnore)
        {
            WcfClient.WcfClient.Instance.ModifyPredictionIgnoreDate(isIgnore);
        }

        #endregion        
    }

    public class DashboardStatisticInfo
    {
        private int m_secondaryAreaVisits;
        private double m_drive;
        private double m_penalty;

        private string m_statisticsText;
        private string m_errorsText;
        private List<Visit> m_bucketVisits;

        #region SecondaryAreaVisits

        public int SecondaryAreaVisits
        {
            get { return m_secondaryAreaVisits; }
        }

        #endregion

        #region Drive

        public double Drive
        {
            get { return m_drive; }
        }

        #endregion

        #region Penalty

        public double Penalty
        {
            get { return m_penalty; }
        }

        #endregion

        #region StatisticsText

        public string StatisticsText
        {
            get { return m_statisticsText; }
        }

        #endregion

        #region ErrorsText

        public string ErrorsText
        {
            get { return m_errorsText; }
        }

        #endregion

        #region Cost

        public double Cost
        {
            get
            {
                double bucketCost = 0;
                foreach (Visit visit in m_bucketVisits)
                {
                    if (!visit.CanBook)
                        bucketCost += (double)visit.DurationCost;
                }

                return m_drive + m_penalty + bucketCost / 10;
            }
        }

        #endregion


        #region Constructor

        public DashboardStatisticInfo(MainFormModel model, List<Visit> bucketVisits)
        {
            m_bucketVisits = bucketVisits;

            List<Visit> visitsOutOfFrame = new List<Visit>();
            List<Visit> visitsOnWrongTech = new List<Visit>();
            List<Visit> visitsOverlapped = new List<Visit>();
            List<Technician> techExceededVisits = new List<Technician>();

            foreach (Technician technician in model.Technicians)
            {
                IList<Visit> techVisits = model.Visits.GetTechnicianVisits(technician.ID);
                double techPenalty;
                m_drive += MainFormModel.GetTechnicianDriveDistance(technician, techVisits, out techPenalty);
                m_penalty += techPenalty;
                int nonExclusiveVisitsCount = 0;

                for (int i = 0; i < techVisits.Count; i++)
                {
                    Visit visit = techVisits[i];

                    if (!visit.ExclusiveTechnicianDefaultId.HasValue)
                        nonExclusiveVisitsCount++;

                    if (i + 1 < techVisits.Count)
                    {
                        Visit nextVisit = techVisits[i + 1];
                        TimeInterval visitInterval = new TimeInterval(visit.TimeStart, visit.TimeEnd);
                        TimeInterval nextVisitInterval = new TimeInterval(nextVisit.TimeStart, nextVisit.TimeEnd);
                        if (visitInterval.IsIntersects(nextVisitInterval))
                            visitsOverlapped.Add(visit);
                    }                        

                    if (visit.IsInSecondaryArea)
                        m_secondaryAreaVisits++;

                    if (!visit.IsWithinAllowedInterval)
                        visitsOutOfFrame.Add(visit);

                    if (!visit.IsInPrimaryArea && !visit.IsInSecondaryArea)
                        visitsOnWrongTech.Add(visit);                    
                }
             
                if (techVisits.Count > technician.MaxVisitsCount 
                    || nonExclusiveVisitsCount > technician.MaxNonExclusiveVisitsCount)
                {
                    techExceededVisits.Add(technician);
                }
            }

            m_statisticsText = string.Format("{0} Secondary area visits\r\n{1} Total drive distance\r\n{2} Penalty\r\n{3} Total Cost",
                m_secondaryAreaVisits, (int)m_drive, (int)m_penalty, (int)Cost);

            m_errorsText = string.Empty;
            if (visitsOutOfFrame.Count > 0)
            {
                m_errorsText += "\r\n\r\nERROR - the following visits are out of timeframe\r\n     ";
                foreach (Visit visit in visitsOutOfFrame)
                    m_errorsText += visit.TicketNumber + ", ";
                m_errorsText = m_errorsText.Remove(m_errorsText.Length - 2);
            }

            if (visitsOnWrongTech.Count > 0)
            {
                m_errorsText += "\r\n\r\nERROR - the following visits are on wrong technicians\r\n     ";
                foreach (Visit visit in visitsOnWrongTech)
                    m_errorsText += visit.TicketNumber + ", ";
                m_errorsText = m_errorsText.Remove(m_errorsText.Length - 2);
            }

            if (visitsOverlapped.Count > 0)
            {
                m_errorsText += "\r\n\r\nERROR - the following visits are overlapped\r\n     ";
                foreach (Visit visit in visitsOverlapped)
                    m_errorsText += visit.TicketNumber + ", ";
                m_errorsText = m_errorsText.Remove(m_errorsText.Length - 2);
            }

            if (techExceededVisits.Count > 0)
            {
                m_errorsText += "\r\n\r\nERROR - the following technicians exceeded their max visit limits\r\n     ";
                foreach (Technician technician in techExceededVisits)
                    m_errorsText += technician.Name + "; ";
                m_errorsText = m_errorsText.Remove(m_errorsText.Length - 2);                
            }
        }

        #endregion     
    }
}
