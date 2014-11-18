using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchedule.Domain.WCF
{
    [DataContract]
    public class BucketProcessingOptions
    {
        private BookingEngine m_bookingEngine;
        private Visit m_delayedVisit;

        [DataMember]
        public bool IsIgnoreExclusivityAllowed { get; set; }
        [DataMember]
        public List<Technician> TempExclusivityTechnicians { get; set; }
        [DataMember]
        public List<VisitAddResult> TimeFrameChangeOptions { get; set; }
        [DataMember]
        public List<WorkingHoursExtensionResult> WorkingHoursExtensions { get; set; }
        [DataMember]
        public string BucketReason { get; set; }


        #region Constructor

        public BucketProcessingOptions(BookingEngine bookingEngine, int delayedVisitId)
        {
            DateTime scheduleDate = Visit.FindByPrimaryKey(delayedVisitId).ScheduleDate;

            m_bookingEngine = bookingEngine;
            foreach (var visit in m_bookingEngine.GetDelayedVisits(scheduleDate))
            {
                if (visit.ID == delayedVisitId)
                {
                    m_delayedVisit = visit;
                    break;
                }
            }

            if (m_delayedVisit.IsFixed)
            {
                m_delayedVisit.IsFixed = false;
                Visit.Update(m_delayedVisit);
            }

            InitTempExclusivityTechnicians(scheduleDate);
            IsIgnoreExclusivityAllowed = CanInsertVisitIngoreExclusivity();
            BucketReason = GetBucketReason();
            InitWorkingHoursExtensions();
            InitTimeFrameChangeOptions();
        }

        #endregion


        #region InitTempExclusivityTechnicians

        private void InitTempExclusivityTechnicians(DateTime date)
        {
            TempExclusivityTechnicians = new List<Technician>();

            Dictionary<Technician, KeyValuePair<double, VisitAddResult>> technicianRankMap
                = new Dictionary<Technician, KeyValuePair<double, VisitAddResult>>();

            foreach (Technician technician in m_bookingEngine.GetTechnicians(date))
            {
                if (m_delayedVisit.ForbiddenTechnician != null
                    && m_delayedVisit.Technician.ID == m_delayedVisit.ForbiddenTechnician.ID)
                {
                    continue;
                }

                Visit newVisit = m_bookingEngine.GetNewVisit(m_delayedVisit, true, technician);
                VisitAddResult addResult = m_bookingEngine.CanInsertVisit(newVisit, false);
                if (addResult != null && addResult.IsAddAllowed)
                {
                    double costChange = addResult.CostChange;
                    technicianRankMap.Add(technician, new KeyValuePair<double, VisitAddResult>(
                                            costChange, addResult));
                    TempExclusivityTechnicians.Add(technician);
                }                
            }

            TempExclusivityTechnicians.Sort(delegate(Technician x, Technician y)
                {
                    KeyValuePair<double, VisitAddResult> rankX = technicianRankMap[x];
                    KeyValuePair<double, VisitAddResult> rankY = technicianRankMap[y];

                    if (rankX.Value.CostChange != rankY.Value.CostChange)
                        return rankX.Value.CostChange.CompareTo(rankY.Value.CostChange);
                    return rankX.Key.CompareTo(rankY.Key);
                });
        }

        #endregion

        #region CanInsertVisitIngoreExclusivity

        private bool CanInsertVisitIngoreExclusivity()
        {
            if (m_delayedVisit.ExclusiveTechnicianDefaultId == null)
                return false;

            Visit newVisit = m_bookingEngine.GetNewVisit(m_delayedVisit, true, null);
            return m_bookingEngine.CanInsertVisit(newVisit);
        }

        #endregion        

        #region GetBucketReason

        public string GetBucketReason()
        {
            if (m_delayedVisit.ExclusiveTechnicianDefaultId.HasValue
                && m_delayedVisit.ExclusiveTechnician == null)
            {
                return "Exclusive technician is off";
            }

            List<Technician> allowedTechnicians = new List<Technician>(m_delayedVisit.PrimaryTechnicians);
            allowedTechnicians.AddRange(new List<Technician>(m_delayedVisit.SecondaryTechnicians));

            if (!m_delayedVisit.ExclusiveTechnicianDefaultId.HasValue && allowedTechnicians.Count == 0)
                return "No one can service visit's zip " + m_delayedVisit.Zip;

            bool isAnyoneHasNeededServices = false;
            foreach (Technician technician in allowedTechnicians)
            {
                if (m_delayedVisit.CanTechnicianServiceVisit(technician))
                {
                    isAnyoneHasNeededServices = true;
                    break;
                }
            }

            if (!isAnyoneHasNeededServices)
            {
                if (m_delayedVisit.ExclusiveTechnicianDefaultId.HasValue)
                    return m_delayedVisit.ExclusiveTechnicianDefault.Name + " cannot peform all the required services";
                return "Neither of allowed technicians can peform all the required services";
            }

            bool isCanFitIntoOnesWorkingHours = false;
            DateTime minEndTime = DateTime.MaxValue;
            foreach (Technician technician in allowedTechnicians)
            {
                if (m_delayedVisit.GetMinTimeEnd(technician, true).Value < minEndTime)
                    minEndTime = m_delayedVisit.GetMinTimeEnd(technician, true).Value;

                if (m_delayedVisit.GetAllowedInterval(technician) != null)
                {
                    isCanFitIntoOnesWorkingHours = true;
                    break;
                }
            }

            if (!isCanFitIntoOnesWorkingHours)
            {
                if (minEndTime.Hour > 21 || minEndTime.Date > m_delayedVisit.ScheduleDate) //After 9 PM
                    return "Estimated duration is too long";

                if (m_delayedVisit.ExclusiveTechnicianDefaultId.HasValue)
                {
                    return string.Format("Visit cannot be booked because of {0} working hours",
                        m_delayedVisit.ExclusiveTechnicianDefault.Name);
                }

                return "Neither of allowed technicians has corresponding working hours";
            }


            return "Overbooking";

        }

        #endregion

        #region InitWorkingHoursExtensions

        private void InitWorkingHoursExtensions()
        {
            Visit newVisit = m_bookingEngine.GetNewVisit(m_delayedVisit, false, null);
            InputParameters input = new InputParameters(newVisit, false);
            input.IgnoreWorkingHours = true;
            m_bookingEngine.CanInsertVisit(input);

            Dictionary<string, WorkingHoursExtensionResult> bestResults = new Dictionary<string, WorkingHoursExtensionResult>();
            foreach (WorkingHoursExtensionResult extension in input.Extensions)
            {
                string stringEqualityRepresentation = extension.TechnicianName
                    + extension.NewWorkingHoursText + extension.VisitShortInfoText;

                if (extension.NewWorkInterval.TimeStart.TimeOfDay < new TimeSpan(6, 0, 0)
                    || extension.NewWorkInterval.TimeStart.TimeOfDay > new TimeSpan(20, 0, 0))
                {
                    continue;
                }

                if (bestResults.ContainsKey(stringEqualityRepresentation))
                {
                    if (extension.IsBetterThan(bestResults[stringEqualityRepresentation]))
                        bestResults[stringEqualityRepresentation] = extension;
                }
                else
                    bestResults.Add(stringEqualityRepresentation, extension);
            }

            WorkingHoursExtensions = new List<WorkingHoursExtensionResult>(bestResults.Values);
            WorkingHoursExtensions.Sort();
            WorkingHoursExtensions.Reverse();
        }

        #endregion

        #region InitTimeFrameChangeOptions

        private void InitTimeFrameChangeOptions()
        {
            TimeFrameChangeOptions = m_bookingEngine.FindTimeFrameChanges(m_delayedVisit);
        }

        #endregion
    }
}
