using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchedule.Domain
{
    [DataContract]
    public class Schedule
    {
        private Schedule(){ }

        #region ScheduleDate

        private DateTime m_scheduleDate;
        [DataMember]
        public DateTime ScheduleDate
        {
            get { return m_scheduleDate; }
            set { m_scheduleDate = value; }
        }

        #endregion

        #region Visits

        private List<Visit> m_visits;
        [DataMember]
        public List<Visit> Visits
        {
            get { return m_visits; }
            set { m_visits = value; }
        }

        #endregion

        #region DelayedVisits

        private List<Visit> m_delayedVisits;
        [DataMember]
        public List<Visit> DelayedVisits
        {
            get { return m_delayedVisits; }
            set { m_delayedVisits = value; }
        }

        #endregion        

        #region Technicians

        private List<Technician> m_technicians;
        [DataMember]
        public List<Technician> Technicians
        {
            get { return m_technicians; }
            set { m_technicians = value; }
        }

        #endregion

        #region TimeFrames

        private List<TimeFrame> m_timeFrames;
        [DataMember]
        public List<TimeFrame> TimeFrames
        {
            get { return m_timeFrames; }
            set { m_timeFrames = value; }
        }

        #endregion

        #region DefaultTechnicians

        private List<TechnicianDefault> m_defaultTechnicians;
        [DataMember]
        public List<TechnicianDefault> DefaultTechnicians
        {
            get { return m_defaultTechnicians; }
            set { m_defaultTechnicians = value; }
        }

        #endregion

        #region Cost

        private double m_initialCost;
        [DataMember]
        public double InitialCost
        {
            get { return m_initialCost; }
            set { m_initialCost = value; }
        }

        #endregion

        #region Cost

        private double m_cost;
        [DataMember]
        public double Cost
        {
            get { return m_cost; }
            set { m_cost = value; }
        }

        #endregion

        #region IsEmpty

        private bool m_isEmpty;
        [DataMember]
        public bool IsEmpty
        {
            get { return m_isEmpty; }
            set { m_isEmpty = value; }
        }

        #endregion

        #region Create

        public static Schedule Create(BookingEngine bookingEngine, DateTime date)
        {
            Schedule schedule = CreateFromBookingEngine(bookingEngine, date, false); 
            schedule.m_timeFrames = TimeFrame.TimeFrames.Values.ToList();
            schedule.m_defaultTechnicians = Technician.GetTechnicianDefaults();
            return schedule;
        }        

        public static Schedule CreateEmpty()
        {
            Schedule schedule = new Schedule();
            schedule.IsEmpty = true;
            return schedule;
        }        
        
        public static Schedule CreateFromBookingEngine(BookingEngine bookingEngine, DateTime date, 
            bool isOptimizationResult)
        {
            Schedule schedule = new Schedule();
            schedule.m_scheduleDate = date;

            if (isOptimizationResult)
            {
                schedule.m_visits = new List<Visit>();
                foreach (var visit in bookingEngine.Visits.GetVisits(date))
                    schedule.m_visits.Add((Visit)visit.Clone());

                schedule.m_delayedVisits = new List<Visit>();
                foreach (var visit in bookingEngine.GetDelayedVisits(date))
                    schedule.m_delayedVisits.Add((Visit)visit.Clone());                
            }
            else
            {
                schedule.m_visits = new List<Visit>(bookingEngine.Visits.GetVisits(date));
                schedule.m_delayedVisits = new List<Visit>(bookingEngine.GetDelayedVisits(date));                
            }

            schedule.m_technicians = new List<Technician>(bookingEngine.GetTechnicians(date));
            return schedule;
        }

        #endregion


        #region IsScheduleOutdated

        public bool IsScheduleOutdated(BookingEngine bookingEngine)
        {
            Dictionary<int, Technician> currentTechnicians = new Dictionary<int, Technician>();
            foreach (var technician in bookingEngine.GetTechnicians(ScheduleDate))
                currentTechnicians.Add(technician.ID, technician);

            if (currentTechnicians.Count != Technicians.Count)
                return true;

            foreach (var technician in Technicians)
            {
                if (!currentTechnicians.ContainsKey(technician.ID))
                    return true;

                if (Technician.IsChangedReschedulePossible(technician, currentTechnicians[technician.ID]))
                    return true;
            }

            List<Visit> currentVisits = bookingEngine.Visits.GetVisits(ScheduleDate);
            currentVisits.AddRange(bookingEngine.GetDelayedVisits(ScheduleDate));

            Dictionary<string, Visit> optimizedVisits = new Dictionary<string, Visit>();
            foreach (var visit in Visits)
                optimizedVisits.Add(visit.TicketNumber, visit);
            foreach (var visit in DelayedVisits)
                optimizedVisits.Add(visit.TicketNumber, visit);            

            if (currentVisits.Count != optimizedVisits.Count)
                return true;

            foreach (var currentVisit in currentVisits)
            {
                if (!optimizedVisits.ContainsKey(currentVisit.TicketNumber))
                    return true;

                if (currentVisit.IsChangedReschedulePossible(optimizedVisits[currentVisit.TicketNumber]))
                    return true;
            }

            return false;
        }

        #endregion

        #region Apply

        public void Apply(BookingEngine bookingEngine)
        {
            Dictionary<string, Visit> currentBucketVisits = new Dictionary<string, Visit>();
            foreach (var visit in bookingEngine.GetDelayedVisits(ScheduleDate))
                currentBucketVisits.Add(visit.TicketNumber, visit);                

            foreach (var visit in Visits)
            {
                Visit existingVisit = bookingEngine.Visits.GetVisitByTicketNumber(visit.TicketNumber);

                if (existingVisit == null)
                {
                    string changeText = "---------OPTIMIZE[" + visit.TicketNumber + "]---------\r\n";
                    changeText += string.Format("[{0}] BKT -> {1} \r\n", visit.TicketNumber, visit.TechnicianName);
                    ChangeRecord.Write(changeText, ScheduleDate, true);

                    existingVisit = currentBucketVisits[visit.TicketNumber];
                    existingVisit.TechnicianId = visit.TechnicianId;
                    existingVisit.TimeStart = visit.TimeStart;
                    existingVisit.TimeEnd = visit.TimeEnd;
                    Visit.Update(existingVisit);
                    bookingEngine.Visits.Add(existingVisit);
//                    Host.Trace("OPTIMIZE APPLY", string.Format("BKT -> {0}, Ticket {1}, {2} - {3}",
//                        visit.TechnicianName, visit.TicketNumber, visit.TimeStart.ToShortTimeString(), visit.TimeEnd.ToShortTimeString()));
                    continue;
                }

                if (existingVisit.TechnicianId != visit.TechnicianId
                    || existingVisit.TimeStart != visit.TimeStart
                    || existingVisit.TimeEnd != visit.TimeEnd)
                {
                    if (existingVisit.TechnicianId != visit.TechnicianId)
                    {
                        string changeText = "---------OPTIMIZE[" + existingVisit.TicketNumber + "]---------\r\n";
                        changeText += string.Format("[{0}] {1} -> {2} \r\n", existingVisit.TicketNumber,
                            existingVisit.TechnicianName, visit.TechnicianName);
                        ChangeRecord.Write(changeText, ScheduleDate, true);
                    }

                    existingVisit.TechnicianId = visit.TechnicianId;
                    existingVisit.TimeStart = visit.TimeStart;
                    existingVisit.TimeEnd = visit.TimeEnd;
                    Visit.Update(existingVisit);
//                    Host.Trace("OPTIMIZE APPLY", string.Format("TECH = {0}, Ticket {1}, {2} - {3}",
//                        visit.TechnicianName, visit.TicketNumber, visit.TimeStart.ToShortTimeString(), visit.TimeEnd.ToShortTimeString()));
                }
                else
                {
//                    Host.Trace("OPTIMIZE APPLY", string.Format("Ticket {1} no changes, {0}, {2} - {3}",
//                        visit.TechnicianName, visit.TicketNumber, visit.TimeStart.ToShortTimeString(), visit.TimeEnd.ToShortTimeString()));                    
                }
            }

            foreach (var visit in DelayedVisits)
            {
                if (!currentBucketVisits.ContainsKey(visit.TicketNumber))
                {
                    Visit existingVisit = bookingEngine.Visits.GetVisitByTicketNumber(visit.TicketNumber);

                    string changeText = "---------OPTIMIZE[" + existingVisit.TicketNumber + "]---------\r\n";
                    changeText += string.Format("[{0}] {1} -> BKT \r\n",
                        existingVisit.TicketNumber, existingVisit.TechnicianName);
                    ChangeRecord.Write(changeText, ScheduleDate, true);

                    bookingEngine.Visits.Remove(existingVisit);
                    existingVisit.TechnicianId = null;
                    Visit.Update(existingVisit);
//                    Host.Trace("OPTIMIZE APPLY", string.Format("TO BKT, Ticket {0}, {1} - {2}",
//                        visit.TicketNumber, visit.TimeStart.ToShortTimeString(), visit.TimeEnd.ToShortTimeString()));
                }                    
                else
                {
//                    Host.Trace("OPTIMIZE APPLY", string.Format("IN BKT, Ticket {0} no changes, {1} - {2}",
//                        visit.TicketNumber, visit.TimeStart.ToShortTimeString(), visit.TimeEnd.ToShortTimeString()));                    
                }
            }            

            bookingEngine.RefreshBucket(ScheduleDate);

            string changeTextSum = "---------OPTIMIZE SUMMARY---------\r\n";
            changeTextSum += string.Format("{0} -> {1} \r\n", InitialCost.ToString("0.00"), Cost.ToString("0.00"));
            ChangeRecord.Write(changeTextSum, ScheduleDate, true);
        }

        #endregion
    }
}
