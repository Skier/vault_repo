using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartSchedule.SDK;

namespace SmartSchedule.Domain
{
    public class GraspOptimizer
    {
        private BookingEngine m_bookingEngine;
        private Schedule m_schedule;

        public delegate void IterationDoneHandler();
        public event IterationDoneHandler IterationDone;

        #region GraspOptimizer

        public GraspOptimizer(Schedule schedule)
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
            Host.Trace("GRASP", string.Format("Initial schedule cost = {0}, Schedule Date = {1}",
                m_schedule.InitialCost.ToString("0.00"), m_schedule.ScheduleDate.ToShortDateString()));
            Random random = new Random();
           
            DateTime optimizationStartTime = DateTime.Now;
            Schedule bestResult = Schedule.CreateEmpty();
            while (DateTime.Now.Subtract(optimizationStartTime).TotalMinutes < Configuration.GraspOptimizingTimeMinutes)
            {
                m_bookingEngine = new BookingEngine(m_schedule.ScheduleDate.Date,
                    m_schedule.Visits, m_schedule.TimeFrames, m_schedule.Technicians);
                List<Visit> visits = GetVisitsToReinsert();
                visits = MarkSeeds(visits);
                List<Visit> newDelayedVisits = new List<Visit>();

                while (true)
                {
                    Visit visit = GetNextVisitByGrasp(visits, random);
                    if (visit == null)
                        break;

                    VisitInsertResult visitInsertResult;
                    if (visit.ForceTechnician != null)
                    {
                        visitInsertResult = m_bookingEngine.InsertVisitGrasp(visit,
                            Technician.GetTechnicianDefault(visit.ForceTechnician.TechnicianDefaultId), true);
                    }
                    else
                        visitInsertResult = m_bookingEngine.InsertVisitGrasp(visit, null, true);
                        

                    if (visitInsertResult == null || !visitInsertResult.IsInsertSucceed)
                    {
                        visit.ID = 0;
                        visitInsertResult = m_bookingEngine.InsertVisitGrasp(visit, null, false);
                        if (visitInsertResult == null || !visitInsertResult.IsInsertSucceed)
                            newDelayedVisits.Add(visit);
                    }                        
                }

                m_bookingEngine.SetDelayedVisits(m_schedule.ScheduleDate, newDelayedVisits);

                double newCost = GetCurrentScheduleCost();
                if ((bestResult.IsEmpty && newCost < m_schedule.InitialCost)
                    || (!bestResult.IsEmpty && newCost < bestResult.Cost))
                {
                    bestResult = Schedule.CreateFromBookingEngine(m_bookingEngine, m_schedule.ScheduleDate, true);
                    bestResult.InitialCost = m_schedule.InitialCost;
                    bestResult.Cost = newCost;
                    Host.Trace("GRASP", string.Format("Better schedule found. Schedule cost = {0}",
                        newCost.ToString("0.00")));
                }
                else
                {
                    Host.Trace("GRASP", string.Format("New schedule cost = {0}, which is worse than existings",
                        newCost.ToString("0.00")));                        
                }

                if (IterationDone != null)
                    IterationDone();
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

                if (visit.PrimaryTechnicians.Count + visit.SecondaryTechnicians.Count <= 1)
                    continue;

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

        #region MarkSeeds

        private List<Visit> MarkSeeds(List<Visit> visits)
        {
            List<Visit> result = new List<Visit>();
            foreach (var visit in visits)
                result.Add(m_bookingEngine.GetNewVisit(visit, visit.IsTempIgnoreExclusivity, null));

            foreach (var visit in result)
            {
                List<Technician> allowedTechnicians = visit.PrimaryTechnicians;
                if (allowedTechnicians.Count == 0)
                    allowedTechnicians.AddRange(visit.SecondaryTechnicians);

                double minDrive = double.MaxValue;
                Technician minDriveTechnician = null;

                foreach (Technician technician in allowedTechnicians)
                {
                    if (technician.Distance(visit) < minDrive)
                    {
                        minDrive = technician.Distance(visit);
                        minDriveTechnician = technician;
                    }
                }

                Host.Trace("GRASP: Min Drive", string.Format("Ticket {0}, Min Drive {1}, {2}",
                    visit.TicketNumber, minDrive.ToString("0.00"),
                    minDriveTechnician == null ? string.Empty : minDriveTechnician.Name));

                visit.MinDrive = minDrive;
            }

            result = result.OrderByDescending(visit => visit.MinDrive).ToList();

            List<Visit> seeds = new List<Visit>();
            int maxSeedsCount = result.Count / 10;
            foreach (Visit visit in result)
            {
                if (visit.MinDrive == double.MaxValue)
                    continue;

                bool isVisitSeed = true;
                foreach (Visit seed in seeds)
                {
                    if (seed.Distance(visit) < 10)
                    {
                        isVisitSeed = false;
                        break;
                    }
                }

                if (isVisitSeed)
                {
                    seeds.Add(visit);
                    visit.IsSeed = true;

                    if (seeds.Count >= maxSeedsCount)
                        break;
                }
            }

            return result.OrderByDescending(visit => visit.IsSeed)
                .ThenByDescending(visit => visit.MinDrive).ToList();
        }

        #endregion

        #region GetNextVisitByGrasp

        private Visit GetNextVisitByGrasp(List<Visit> pendingVisits, Random random)
        {
            if (pendingVisits.Count == 0)
                return null;

            Visit firstVisit = pendingVisits[0];

            if (firstVisit.IsSeed)
            {
                Host.Trace("Sync", string.Format("Importing seed Ticket {0}, MinDrive = {1}",
                    firstVisit.TicketNumber, firstVisit.MinDrive.ToString("0.00")));
                
                Dictionary<Technician, VisitAddResult> bestResultMap = new Dictionary<Technician, VisitAddResult>();
                foreach (Technician primaryTechnician in firstVisit.PrimaryTechnicians)
                {
                    VisitAddResult result = m_bookingEngine.CanInsertVisit(firstVisit, false);
                    if (result.IsAddAllowed && !result.SecondaryArea)
                        bestResultMap.Add(primaryTechnician, result);
                }

                if (bestResultMap.Count == 0)
                {
                    List<Technician> allowedTechnicians = firstVisit.PrimaryTechnicians;
                    allowedTechnicians.AddRange(firstVisit.SecondaryTechnicians);

                    foreach (Technician technician in allowedTechnicians)
                    {
                        VisitAddResult result = m_bookingEngine.CanInsertVisit(firstVisit, false);
                        if (result.IsAddAllowed)
                            bestResultMap.Add(technician, result);                        
                    }
                }

                VisitAddResult bestResult = m_bookingEngine.CanInsertVisit(firstVisit, false);
                if (!bestResult.IsAddAllowed)
                {
                    pendingVisits.Remove(firstVisit);
                    return firstVisit;
                }

                List<Technician> closeAllowedTechnicians = new List<Technician>();
                closeAllowedTechnicians.Add(bestResult.Technician);

                foreach (var bestTechResult in bestResultMap)
                {
                    if (bestTechResult.Value.CostChange - bestResult.CostChange < 10)
                    {
                        Technician technician = bestTechResult.Key;
                        closeAllowedTechnicians.Add(technician);
                        Host.Trace("Sync", string.Format("Importing seed Ticket {0}, Possible tech with affordable drive change {1}, CostChange = {2}",
                            firstVisit.TicketNumber, technician.Name, bestTechResult.Value.CostChange.ToString("0.00")));
                    }
                }

                Technician selectedTechnician = closeAllowedTechnicians[random.Next(Math.Min(3, closeAllowedTechnicians.Count))];

                Host.Trace("Sync", string.Format("Importing seed Ticket {0}, Selected tech {1}",
                    firstVisit.TicketNumber, selectedTechnician.Name));
                firstVisit.ForceTechnician = selectedTechnician;

                pendingVisits.Remove(firstVisit);
                return firstVisit;
            }


            Dictionary<VisitAddResult, Visit> bestResultVisits = new Dictionary<VisitAddResult, Visit>();
            bool isPrimaryAreaInsertion = false;

            foreach (Visit visit in pendingVisits)
            {
                VisitAddResult addResult = m_bookingEngine.CanInsertVisitGrasp(visit, null);

                if (addResult != null && addResult.IsAddAllowed)
                {
                    if (!isPrimaryAreaInsertion && !addResult.SecondaryArea)
                    {
                        bestResultVisits.Clear();
                        isPrimaryAreaInsertion = true;
                    }

                    if ((isPrimaryAreaInsertion && !addResult.SecondaryArea)
                        || !isPrimaryAreaInsertion)
                    {
                        bestResultVisits.Add(addResult, visit);
                    }
                }
            }

            List<VisitAddResult> bestResults = new List<VisitAddResult>(bestResultVisits.Keys);
            if (bestResults.Count == 0)
            {
                Host.Trace("Sync", string.Format("Ticket {0} placed to bucket", firstVisit.TicketNumber));
                pendingVisits.Remove(firstVisit);
                return firstVisit;
            }

            bestResults = bestResults.OrderByDescending(addResult => addResult.Urgency).ToList();
            int bestUrgencyTicketsCount = Math.Min(3, bestResults.Count);

            for (int i = 0; i < bestUrgencyTicketsCount; i++)
            {
                Host.Trace("Sync", string.Format("One of the max urgent ticket is {0}, Urgency = {1}, PrimaryArea = {2}",
                    bestResultVisits[bestResults[i]].TicketNumber, bestResults[i].Urgency.ToString("0.00"),
                    isPrimaryAreaInsertion));
            }

            for (int i = 1; i <= 3; i++)
            {
                if (bestUrgencyTicketsCount >= 2
                    && bestResults[0].Urgency == double.MaxValue
                    && bestResults[bestUrgencyTicketsCount - 1].Urgency != double.MaxValue)
                {
                    bestUrgencyTicketsCount--;
                }
            }

            VisitAddResult selectedResult = bestResults[random.Next(bestUrgencyTicketsCount)];
            Visit selectedVisit = bestResultVisits[selectedResult];
            Host.Trace("Sync", string.Format("Randomly selected ticket is {0}",
                selectedVisit.TicketNumber));

            pendingVisits.Remove(selectedVisit);
            return selectedVisit;
        }

        #endregion
    }
}
