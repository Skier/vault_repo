using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSchedule.SDK;

namespace SmartSchedule.Domain
{
    public class Optimizer
    {
        private BookingEngine m_bookingEngine;
        private Schedule m_schedule;

        public delegate void IterationDoneHandler();
        public event IterationDoneHandler IterationDone;

        #region Optimizer

        public Optimizer(Schedule schedule)
        {
            m_schedule = schedule;
            m_bookingEngine = new BookingEngine(m_schedule.ScheduleDate.Date,
                m_schedule.Visits, m_schedule.TimeFrames, m_schedule.Technicians);
            Technician.LoadTechnicians(schedule.Technicians, schedule.DefaultTechnicians);
        }

        #endregion

        #region Optimize

        public Schedule Optimize()
        {
            m_schedule.InitialCost = GetCurrentScheduleCost();
            Host.Trace("OPTIMIZER", string.Format("Initial schedule cost = {0}, Bucket cnt = {1}, Schedule Date = {2}",
                m_schedule.InitialCost.ToString("0.00"), m_schedule.DelayedVisits.Count, m_schedule.ScheduleDate.ToShortDateString()));                       

            DateTime optimizationStartTime = DateTime.Now;
            Schedule bestResult = Schedule.CreateEmpty();
            while (DateTime.Now.Subtract(optimizationStartTime).TotalMinutes < Configuration.OptimizerWorkTimeLimitMinutes)
            {
                m_bookingEngine = new BookingEngine(m_schedule.ScheduleDate.Date,
                    m_schedule.Visits, m_schedule.TimeFrames, m_schedule.Technicians);                
                List<Visit> newDelayedVisits = new List<Visit>();
                List<Visit> visits = GetVisitsToReinsert();
                visits = visits.OrderByDescending(visit => visit.ExclusiveTechnicianDefaultId.HasValue)
                    .ThenByDescending(visit => visit.Cost).ToList();

                foreach (Visit visit in visits)
                {
                    Visit newVisit = m_bookingEngine.GetNewVisit(visit, 
                        visit.IsTempIgnoreExclusivity, visit.TempExclusiveTechnician);
                    VisitInsertResult visitInsertResult = m_bookingEngine.InsertVisitOptimizer(newVisit);

                    if (visitInsertResult == null || !visitInsertResult.IsInsertSucceed)
                        newDelayedVisits.Add(newVisit);
                }

                m_bookingEngine.SetDelayedVisits(m_schedule.ScheduleDate, newDelayedVisits);

                double newCost = GetCurrentScheduleCost();
                if ((bestResult.IsEmpty && newCost < m_schedule.InitialCost)
                    || (!bestResult.IsEmpty && newCost < bestResult.Cost))
                {
                    bestResult = Schedule.CreateFromBookingEngine(m_bookingEngine, m_schedule.ScheduleDate, true);
                    bestResult.InitialCost = m_schedule.InitialCost;
                    bestResult.Cost = newCost;
                    Host.Trace("OPTIMIZER", string.Format("BETTER schedule found. Schedule cost = {0}, Bucket cnt = {1}",
                        newCost.ToString("0.00"), m_schedule.DelayedVisits.Count));
                }
                else
                {
                    Host.Trace("OPTIMIZER", string.Format("New schedule: cost = {0}, Bucket cnt = {1}. WORSE than existings",
                        newCost.ToString("0.00"), m_schedule.DelayedVisits.Count));
                }
                
                if (IterationDone != null)
                    IterationDone();

                break;
            }

            return bestResult;
        }

        #endregion


        #region GetCurrentScheduleCost

        private double GetCurrentScheduleCost()
        {
            double result = 0;

            foreach (var technician in m_bookingEngine.GetTechnicians(m_schedule.ScheduleDate))
            {
                result += BookingEngine.GetCost(
                    m_bookingEngine.Visits.GetTechnicianVisits(technician.ID).ToList(),
                    technician);
            }

            decimal bucketTotalCost = decimal.Zero;
            foreach (Visit delayedVisit in m_schedule.DelayedVisits)
            {
                if (!delayedVisit.CanBook)
                    bucketTotalCost += delayedVisit.DurationCost;
            }                

            return result + (double)bucketTotalCost/10;
        }

        #endregion

        #region GetVisitsToReinsert

        private List<Visit> GetVisitsToReinsert()
        {
            List<Visit> visitsToReinsert = new List<Visit>();

            int maxId = 0;
            foreach (var visit in m_bookingEngine.Visits)
            {
                if (visit.ID > maxId)
                    maxId = visit.ID;

                if (!visit.IsBlockout)
                    visitsToReinsert.Add(visit);
            }

            foreach (var visit in visitsToReinsert)
                m_bookingEngine.Visits.Remove(visit);

            foreach (var visit in m_schedule.DelayedVisits)
            {
                if (visit.ID > maxId)
                    maxId = visit.ID;
                visitsToReinsert.Add(visit);
            }

            Visit.SetTemporaryIdCounter(maxId + 1);
            return visitsToReinsert;
        }

        #endregion
    }
}
