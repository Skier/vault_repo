using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace SmartSchedule.Domain
{
    public class BindingListVisit : BindingList<Visit>
    {
        private Dictionary<int, Visit> m_visitMap;
        private Dictionary<string, Visit> m_visitTicketMap;
        private Dictionary<int, SortedList<VisitKey, Visit>> m_sortedVisits;

        public IList<Visit> GetTechnicianVisits(int technicianId)
        {
            if (!m_sortedVisits.ContainsKey(technicianId))
                return new List<Visit>();
            return new List<Visit>(m_sortedVisits[technicianId].Values);
        }
        
        private void RemoveFromSortedVisits(Visit visit)
        {
            if (!visit.TechnicianId.HasValue)
                return;

            m_sortedVisits[visit.TechnicianId.Value].Remove(
                new VisitKey(visit.ID, visit.TimeStart));
        }

        private void AddToSortedVisits(Visit visit)
        {
            if (!visit.TechnicianId.HasValue)
                return;

            if (!m_sortedVisits.ContainsKey(visit.TechnicianId.Value))
                m_sortedVisits.Add(visit.TechnicianId.Value, new SortedList<VisitKey, Visit>());

            m_sortedVisits[visit.TechnicianId.Value].Add(
                new VisitKey(visit.ID, visit.TimeStart), visit);
        }

        public Visit GetVisitByTicketNumber(string ticketNumber)
        {
            if (!m_visitTicketMap.ContainsKey(ticketNumber))
                return null;
            return m_visitTicketMap[ticketNumber];
        }

        public Visit GetVisitById(int visitId)
        {
            if (!m_visitMap.ContainsKey(visitId))
                return null;
            return m_visitMap[visitId];
        }

        public List<Visit> GetVisits(DateTime date)
        {
            return this.Where(visit => visit.ScheduleDate == date.Date).ToList();
        }

        public bool ContainsVisit(int visitId)
        {
            return m_visitMap.ContainsKey(visitId);
        }

        public BindingListVisit(IList<Visit> list) : base(list)
        {
            m_sortedVisits = new Dictionary<int, SortedList<VisitKey, Visit>>();            
            m_visitTicketMap = new Dictionary<string, Visit>();
            m_visitMap = new Dictionary<int, Visit>();

            foreach (var visit in list)
            {
                m_visitTicketMap.Add(visit.TicketNumber, visit);
                m_visitMap.Add(visit.ID, visit);

                if (!visit.TechnicianId.HasValue)
                    continue;

                AddToSortedVisits(visit);
            }
        }

        public BindingListVisit()
        {
            m_sortedVisits = new Dictionary<int, SortedList<VisitKey, Visit>>();
            m_visitTicketMap = new Dictionary<string, Visit>();
            m_visitMap = new Dictionary<int, Visit>();
        }
       
        protected override void RemoveItem(int index)
        {
            Visit visit = this[index];

            m_visitTicketMap.Remove(visit.TicketNumber);
            m_visitMap.Remove(visit.ID);
            if (visit.TechnicianId.HasValue)
                RemoveFromSortedVisits(visit);

            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            m_visitTicketMap.Clear();
            m_visitMap.Clear();
            m_sortedVisits.Clear();
            base.ClearItems();
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                Visit visit = this[e.NewIndex];

                m_visitTicketMap.Add(visit.TicketNumber, visit);
                m_visitMap.Add(visit.ID, visit);

                if (visit.TechnicianId.HasValue)
                    AddToSortedVisits(visit);
            }
            else if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                Visit visit = this[e.NewIndex];

                if (visit.TechnicianId.HasValue && e.PropertyDescriptor != null
                    && e.PropertyDescriptor.Name == "TimeStart")
                {
                    m_sortedVisits[visit.TechnicianId.Value].RemoveAt(
                        m_sortedVisits[visit.TechnicianId.Value].IndexOfValue(visit));

                    AddToSortedVisits(visit);
                }
                else if (e.PropertyDescriptor != null && e.PropertyDescriptor.Name == "TechnicianId")
                {
                    bool isRemoved = false;

                    VisitKey key = new VisitKey(visit.ID, visit.TimeStart);

                    if (visit.TechnicianId.HasValue 
                        && m_sortedVisits.ContainsKey(visit.TechnicianId.Value)
                        && m_sortedVisits[visit.TechnicianId.Value].ContainsKey(key))
                    {
                        m_sortedVisits[visit.TechnicianId.Value].Remove(key);
                        isRemoved = true;
                    }

                    if (!isRemoved)
                        AddToSortedVisits(visit);
                }
            }            
        }
    }
}
