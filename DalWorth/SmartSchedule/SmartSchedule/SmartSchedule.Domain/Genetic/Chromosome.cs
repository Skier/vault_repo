using System;
using System.Collections.Generic;
using System.Text;

namespace SmartSchedule.Domain.Genetic
{
    public class Chromosome : ICloneable
    {
        private BookingEngine m_bookingEngine;
        private Dictionary<Technician, List<Visit>> m_data;
        private Dictionary<Visit, ListEx<Technician>> m_visitsAvailableTechniciansMap;
        private Dictionary<Visit, Technician> m_oldVisitToTechnicianMap;
        private ListEx<Visit> m_nonFixedVisits;

        public Chromosome(BookingEngine bookingEngine)
        {
            m_bookingEngine = bookingEngine;
        }

        private Chromosome(BookingEngine bookingEngine, Dictionary<Technician, List<Visit>> data, 
            Dictionary<Visit, ListEx<Technician>> visitsAvailableTechniciansMap, ListEx<Visit> nonFixedVisits)
        {
            m_bookingEngine = bookingEngine;
            m_data = data;
            m_visitsAvailableTechniciansMap = visitsAvailableTechniciansMap;
            m_nonFixedVisits = nonFixedVisits;
        }

        #region FitnessValue

        private Fitness m_fitnessValue;
        public Fitness FitnessValue
        {
            get
            {
                if (m_fitnessValue == null)
                    m_fitnessValue = CalculateFitness();

                return m_fitnessValue;
            }
        }

        #endregion

        
        #region FillRandom

        //Returns percentage of difference
        public void Fill(bool randomRearrange)
        {
            m_data = new Dictionary<Technician, List<Visit>>();
            foreach (Technician technician in m_bookingEngine.GetTechnicians(DateTime.Now.Date))
                m_data.Add(technician, new List<Visit>());
            m_oldVisitToTechnicianMap = new Dictionary<Visit, Technician>();

            m_visitsAvailableTechniciansMap = new Dictionary<Visit, ListEx<Technician>>();
            m_nonFixedVisits = new ListEx<Visit>();

            foreach (Visit visit in m_bookingEngine.Visits)
            {
                if (!m_bookingEngine.GetTechnicians(DateTime.Now.Date).Contains(visit.Technician))
                    continue;

                ListEx<Technician> availableTechnicians = new ListEx<Technician>();

                foreach (Technician technician in visit.PrimaryTechnicians)
                {
                    if (m_bookingEngine.GetTechnicians(DateTime.Now.Date).Contains(technician))
                        availableTechnicians.Add(technician);
                }

                foreach (Technician technician in visit.SecondaryTechnicians)
                {
                    if (m_bookingEngine.GetTechnicians(DateTime.Now.Date).Contains(technician))
                        availableTechnicians.Add(technician);
                }

                Visit visitClone = (Visit)visit.Clone();
                m_visitsAvailableTechniciansMap.Add(visitClone, availableTechnicians);
                m_data[visitClone.Technician].Add(visitClone);
                m_oldVisitToTechnicianMap.Add(visitClone, visitClone.Technician);

                if (availableTechnicians.Count > 1)
                {
                    availableTechnicians.Shuffle();
                    m_nonFixedVisits.Add(visitClone);                                        
                } 
            }

            m_nonFixedVisits.Shuffle();

            if (randomRearrange)
                RearrangeVisits(m_nonFixedVisits);

            int visitsOnOtherTechniciansCount = 0;
            foreach (Visit visit in m_visitsAvailableTechniciansMap.Keys)
            {
                if (m_visitsAvailableTechniciansMap[visit].Count <= 1)
                    continue;

                if (visit.TechnicianId != m_oldVisitToTechnicianMap[visit].ID)
                    visitsOnOtherTechniciansCount++;
            }
            
            //return (int)(((decimal)visitsOnOtherTechniciansCount / m_oldVisitToTechnicianMap.Count) * 100);
        }

        #endregion

        #region InsertVisit

        private bool InsertVisit(Visit visit, List<Technician> availableTechnicians, 
            bool findBestPath, out Technician newTechnician)
        {
            List<PathItem> path = null;
            Technician pathTechnician = null;
            double? pathFitnessValue = null;

            foreach (Technician technician in availableTechnicians)
            {
                List<Visit> sortedVisits = new List<Visit>(m_data[technician]);
                sortedVisits.Add(visit);
                List<Visit> visitToInsert = new List<Visit>();
                visitToInsert.Add(visit);
                Dictionary<List<Visit>, int> dummy;
                int dummy2;

                List<PathItem> currentPath = BookingEngine.FindPath(technician, sortedVisits,
                    visitToInsert.ToArray(), true, out dummy, out dummy2, null, 0);

                if (currentPath != null && currentPath.Count > 0)
                {
                    if (pathFitnessValue == null)
                    {
                        path = currentPath;
                        pathTechnician = technician;

                        if (findBestPath)
                            pathFitnessValue = CalculateFitnessValue(currentPath);
                        else
                            break;
                    }
                    else
                    {
                        double newFitnessValue = CalculateFitnessValue(currentPath);

                        if (newFitnessValue < pathFitnessValue)
                        {
                            path = currentPath;
                            pathTechnician = technician;
                            pathFitnessValue = newFitnessValue;
                        }
                    }
                }
            }


            if (path != null)
            {
                m_data[pathTechnician].Add(visit);

                foreach (PathItem pathItem in path)
                {
                    int visitIndex = m_data[pathTechnician].IndexOf(pathItem.Visit);

                    if (visitIndex >= 0)
                    {
                        m_data[pathTechnician][visitIndex].TechnicianId = pathTechnician.ID;
                        m_data[pathTechnician][visitIndex].SetTimeStartFixDuration(pathItem.TimeStart);
                    }
                }

                newTechnician = pathTechnician;
                return true;
            }

            newTechnician = null;
            return false;
        }

        #endregion

        #region RearrangeVisits

        private void RearrangeVisits(List<Visit> visits)
        {
            foreach (Visit mainVisit in visits)
            {
                if (m_oldVisitToTechnicianMap != null
                    && m_oldVisitToTechnicianMap[mainVisit].ID != mainVisit.TechnicianId)
                {
                    continue;
                }
                    
                List<Technician> availableTechniciansMain = new List<Technician>(
                    m_visitsAvailableTechniciansMap[mainVisit]);

                Technician mainVisitOldTechnician = mainVisit.Technician;
                availableTechniciansMain.Remove(mainVisitOldTechnician);

                Technician dummy;
                if (InsertVisit(mainVisit, availableTechniciansMain, false, out dummy))
                {
                    m_data[mainVisitOldTechnician].Remove(mainVisit);
                    continue;
                }
                
                m_data[mainVisitOldTechnician].Remove(mainVisit);
                bool isReinsertionSucceeded = false;                               

                foreach (Technician technician in availableTechniciansMain)                
                {
                    List<Visit> sortedVisits = new List<Visit>(m_data[technician]);
                    sortedVisits.Add(mainVisit);
                    List<Visit> visitToInsert = new List<Visit>();
                    visitToInsert.Add(mainVisit);
                    Dictionary<List<Visit>, int> visitsToRemove;
                    int dummy2;

                    BookingEngine.FindPath(technician, sortedVisits, visitToInsert.ToArray(), false, 
                        out visitsToRemove, out dummy2, null, 0);

                    if (visitsToRemove == null)
                        continue;                    

                    foreach (List<Visit> removeSet in visitsToRemove.Keys)
                    {
                        if (removeSet.Count == 0)
                            continue;

                        //contains visits which were successfully reinserted, path item contains old time start
                        Dictionary<PathItem, Technician> reinsertionMap = new Dictionary<PathItem, Technician>();                        

                        foreach (Visit visitToReinsert in removeSet)
                        {
                            List<Technician> availableTechnicians = new List<Technician>(
                                m_visitsAvailableTechniciansMap[visitToReinsert]);

                            if (m_oldVisitToTechnicianMap != null)
                                availableTechnicians.Remove(m_oldVisitToTechnicianMap[visitToReinsert]);
                            availableTechnicians.Remove(visitToReinsert.Technician);
                            if (availableTechnicians.Count == 0)
                                break;

                            Technician newTechnician;
                            PathItem pathItem = new PathItem(visitToReinsert, visitToReinsert.TimeStart, null);

                            if (InsertVisit(visitToReinsert, availableTechnicians, false, out newTechnician))
                            {
                                reinsertionMap.Add(pathItem, newTechnician);
                            }
                            else
                                break;
                        }

                        if (reinsertionMap.Count != removeSet.Count)//set wasn't fully reinserted
                        {                            
                            //revert all the reinserted visits back
                            foreach (KeyValuePair<PathItem, Technician> mapItem in reinsertionMap)
                            {
                                m_data[mapItem.Value].Remove(mapItem.Key.Visit);
                                mapItem.Key.Visit.Technician = technician;
                                mapItem.Key.Visit.SetTimeStartFixDuration(mapItem.Key.TimeStart);
                            }
                        }
                        else
                        {
                            //apply changes                            
                            foreach (KeyValuePair<PathItem, Technician> mapItem in reinsertionMap)
                                m_data[technician].Remove(mapItem.Key.Visit);

                            List<Technician> techniciansForMainVisit = new List<Technician>();
                            techniciansForMainVisit.Add(technician);
                            Technician dummy3;
                            InsertVisit(mainVisit, techniciansForMainVisit, false, out dummy3);
                            isReinsertionSucceeded = true;
                            break;
                        }
                    }

                    if (isReinsertionSucceeded)
                        break;
                }                

                if (!isReinsertionSucceeded)
                {
                    List<Technician> availableTechnicians = new List<Technician>();
                    availableTechnicians.Add(mainVisitOldTechnician);
                    Technician dummy2;
                    InsertVisit(mainVisit, availableTechnicians, false, out dummy2);
                }
            }                                        
        }

        #endregion

        #region CalculateFitness

        private Fitness CalculateFitness()
        {
            double drive = 0;
            int secondaryAreaVisits = 0;
            int tempAssignedVisits = 0;
            double sumOfSquares = 0;

            foreach (Technician technician in m_data.Keys)
            {
                decimal technicianCost = decimal.Zero;                

                List<Visit> technicianVisits = m_data[technician];

                if (technicianVisits.Count > 0)
                {
                    technicianCost += technicianVisits[0].Cost;
                    if (technicianVisits[0].IsInSecondaryArea)
                        secondaryAreaVisits++;
                    if (technicianVisits[0].IsTemporaryAssigned)
                        tempAssignedVisits++;

                    drive += technician.Distance(technicianVisits[0]);
                    for (int i = 1; i < technicianVisits.Count; i++)
                    {
                        drive += technicianVisits[i - 1].Distance(technicianVisits[i]);
                        technicianCost += technicianVisits[i].Cost;
                        if (technicianVisits[i].IsInSecondaryArea)
                            secondaryAreaVisits++;
                        if (technicianVisits[i].IsTemporaryAssigned)
                            tempAssignedVisits++;

                    }
                    drive += technicianVisits[technicianVisits.Count - 1].Distance(technician);
                }

                sumOfSquares += (double) ((technicianCost - technician.DistributionCostTarget) *
                    (technicianCost - technician.DistributionCostTarget));
            }

            return new Fitness(drive, Math.Sqrt(sumOfSquares / m_data.Keys.Count), 
                secondaryAreaVisits, tempAssignedVisits);
        }


        public double CalculateFitnessValue(Technician technician)
        {
            double drive = 0;
            int secondaryAreaVisits = 0;
            int tempAssignedVisits = 0;
            decimal totalCost = decimal.Zero;

            List<Visit> technicianVisits = m_data[technician];
            technicianVisits.Sort(delegate(Visit x, Visit y) { return x.TimeStart.CompareTo(y.TimeStart); });

            if (technicianVisits.Count > 0)
            {
                totalCost += technicianVisits[0].Cost;
                if (technicianVisits[0].IsInSecondaryArea)
                    secondaryAreaVisits++;
                if (technicianVisits[0].IsTemporaryAssigned)
                    tempAssignedVisits++;

                drive += technician.Distance(technicianVisits[0]);
                for (int i = 1; i < technicianVisits.Count; i++)
                {
                    drive += technicianVisits[i - 1].Distance(technicianVisits[i]);
                    totalCost += technicianVisits[i].Cost;
                    if (technicianVisits[i].IsInSecondaryArea)
                        secondaryAreaVisits++;
                    if (technicianVisits[i].IsTemporaryAssigned)
                        tempAssignedVisits++;
                }
                drive += technicianVisits[technicianVisits.Count - 1].Distance(technician);
            }          

            Fitness fitness = new Fitness(drive, 
                (double)Math.Abs(totalCost - technician.DistributionCostTarget), secondaryAreaVisits, tempAssignedVisits);
            return fitness.TotalValue;
        }

        public double CalculateFitnessValue(List<PathItem> path)
        {            
            double drive = 0;
            int secondaryAreaVisits = 0;
            int tempAssignedVisits = 0;
            decimal totalCost = decimal.Zero;

            if (path.Count != 0)
            {
                Technician technician = path[0].Technician;

                totalCost += path[0].Visit.Cost;
                if (path[0].Visit.IsInSecondaryArea)
                    secondaryAreaVisits++;
                if (path[0].Visit.IsTemporaryAssigned)
                    tempAssignedVisits++;
                    
                drive += technician.Distance(path[0].Visit);
                for (int i = 1; i < path.Count; i++)
                {
                    drive += path[i - 1].Visit.Distance(path[i].Visit);

                    totalCost += path[i].Visit.Cost;
                    if (path[i].Visit.IsInSecondaryArea)
                        secondaryAreaVisits++;
                    if (path[i].Visit.IsTemporaryAssigned)
                        tempAssignedVisits++;
                }
                    
                drive += path[path.Count - 1].Visit.Distance(technician);                


                Fitness fitness = new Fitness(drive,
                    (double)Math.Abs(totalCost - technician.DistributionCostTarget), secondaryAreaVisits, tempAssignedVisits);
                return fitness.TotalValue;
            }

            return 0;
        }

        #endregion

        #region GetVisits

        public List<Visit> GetVisits(Technician technician)
        {
            return m_data[technician];
        }

        #endregion

        #region RemoveVisitsById

        public void RemoveVisitsById(List<Visit> visits)
        {
            List<Visit> visitsToRemove = new List<Visit>(visits);

            foreach (List<Visit> visitList in m_data.Values)
            {
                for (int i = visitList.Count - 1; i >= 0; i--)
                {
                    foreach (Visit visit in visitsToRemove)
                    {
                        if (visit.ID == visitList[i].ID)
                        {
                            visitsToRemove.Remove(visit);
                            visitList.RemoveAt(i);
                            if (visitsToRemove.Count == 0)
                                return;
                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region InsertVisitById

        public void InsertVisitById(Visit visit, Technician technician)
        {
            foreach (Visit chromosomeVisit in m_visitsAvailableTechniciansMap.Keys)
            {
                if (chromosomeVisit.ID == visit.ID)
                {
                    List<Technician> availableTechnicians = new List<Technician>();
                    availableTechnicians.Add(technician);
                    Technician dummy2;
                    InsertVisit(chromosomeVisit, availableTechnicians, false, out dummy2);
                    return;
                }
            }
        }

        #endregion

        #region InsertVisitsMinizingCost

        public bool InsertVisitsMinizingCost(List<Visit> visits, Technician technicianToIngore)
        {
            foreach (Visit visit in visits)
            {
                List<Technician> technicians = new List<Technician>(m_visitsAvailableTechniciansMap[visit]);
                technicians.Remove(technicianToIngore);

                Technician dummy;
                if (!InsertVisit(visit, technicians, true, out dummy))
                    return false;
            }

            return true;
        }

        #endregion

        #region Mutate

        public void Mutate(int mutationPercentage)
        {
            int visitsToMutateCount = m_nonFixedVisits.Count * mutationPercentage/100;

            Random random = new Random();
            List<Visit> visitsToMutate = new List<Visit>();

            while(visitsToMutate.Count < visitsToMutateCount)
            {
                Visit candidate = m_nonFixedVisits[random.Next(0, m_nonFixedVisits.Count)];

                if (!visitsToMutate.Contains(candidate))
                    visitsToMutate.Add(candidate);
            }

            foreach (Visit visit in visitsToMutate)
                m_visitsAvailableTechniciansMap[visit].Shuffle();

            RearrangeVisits(visitsToMutate);
            m_fitnessValue = null;
        }

        #endregion


        #region Clone

        public object Clone()
        {
            Dictionary<Technician, List<Visit>> dataCopy = new Dictionary<Technician, List<Visit>>();
            Dictionary<Visit, ListEx<Technician>> availabilityMap = new Dictionary<Visit, ListEx<Technician>>();
            ListEx<Visit> nonFixedVisits = new ListEx<Visit>();

            foreach (KeyValuePair<Technician, List<Visit>> pair in m_data)
            {
                dataCopy.Add(pair.Key, new List<Visit>());
                foreach (Visit visit in pair.Value)
                {
                    Visit visitClone = (Visit) visit.Clone();
                    dataCopy[pair.Key].Add(visitClone);
                    availabilityMap.Add(visitClone, new ListEx<Technician>(m_visitsAvailableTechniciansMap[visit]));

                    if (m_visitsAvailableTechniciansMap[visit].Count > 1)
                        nonFixedVisits.Add(visitClone);
                }                                    
            }

            return new Chromosome(m_bookingEngine, dataCopy, availabilityMap, nonFixedVisits);
        }

        #endregion

        #region Display

        public void Display()
        {
            m_bookingEngine.Visits.Clear();
            foreach (Technician technician in m_bookingEngine.GetTechnicians(DateTime.Now.Date))
            {
                foreach (Visit visit in m_data[technician])
                {
                    Visit.Update(visit);
                    m_bookingEngine.Visits.Add(visit);
                }                    
            }            
        }

        #endregion
    }
}
