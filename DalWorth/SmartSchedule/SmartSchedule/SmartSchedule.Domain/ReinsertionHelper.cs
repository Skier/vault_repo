using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SmartSchedule.Domain
{
    public class ReinsertionHelper
    {
        public ReinsertionHelper(BookingEngine bookingEngine, List<Visit> visitsToReinsert, 
            Technician mainVisitTechnicician, bool increaseVisitsInSecondaryAreaOnReinsertion,
            bool extendWorkingHoursOnReinsertion)
        {
            m_bookingEngine = bookingEngine;
            m_visitsToReinsert = visitsToReinsert;
            InitPossibleTechnicians(mainVisitTechnicician, increaseVisitsInSecondaryAreaOnReinsertion);
            m_technicianStateMap = new Dictionary<Technician, TechnicianState>();
            m_maxTechniciansCountWithExtendedHours = extendWorkingHoursOnReinsertion ? 1 : 0;
        }

        private readonly BookingEngine m_bookingEngine;
        private List<List<ReinsertionItem>> m_reinsertionItems;
        private int m_outerReinsertionListIndex;
        private readonly Dictionary<Technician, TechnicianState> m_technicianStateMap 
            = new Dictionary<Technician, TechnicianState>();
        private readonly List<Visit> m_visitsToReinsert;
        private int m_maxTechniciansCountWithExtendedHours;

        #region InitPossibleTechnicians

        private void InitPossibleTechnicians(Technician mainVisitTechnicician,
            bool increaseVisitsInSecondaryAreaOnReinsertion)
        {   
            m_reinsertionItems = new List<List<ReinsertionItem>>();
            m_reinsertionItems.Add(new List<ReinsertionItem>());

            foreach (Visit visit in m_visitsToReinsert)
            {
                List<Technician> possibleTechnicians = visit.PrimaryTechnicians;

                if (increaseVisitsInSecondaryAreaOnReinsertion || visit.IsInSecondaryArea)
                    possibleTechnicians.AddRange(visit.SecondaryTechnicians);                    

                possibleTechnicians.Remove(mainVisitTechnicician);
                if (possibleTechnicians.Count == 0)
                {
                    m_reinsertionItems[m_reinsertionItems.Count - 1].Clear();
                    break;
                }

                m_reinsertionItems[m_reinsertionItems.Count - 1].Add(new ReinsertionItem(visit, possibleTechnicians));
            }
            
            if (m_reinsertionItems[0].Count > 0)
                m_reinsertionItems[0][m_visitsToReinsert.Count - 1].CurrentTechnicianIndex = -1;
        }

        #endregion

        #region GetBestPlacement

        public Dictionary<Visit, Technician> GetBestPlacement(out double costChange, 
            out List<WorkingHoursExtensionResult> extensions)
        {
            extensions = new List<WorkingHoursExtensionResult>();
            Dictionary<Visit, Technician> bestResult = null;

            if (m_reinsertionItems[0].Count == 0)
            {
                costChange = 0;
                return bestResult;
            }
                

            double bestResultCostChange = double.MaxValue;
            Dictionary<Visit, Technician> currentLayout = new Dictionary<Visit, Technician>();

            Visit failedCombinationVisit = null;
            Technician failedCombinationTechnician = null;
            while (true)
            {
                Dictionary<Visit, Technician> currentIteration 
                    = NextIteration(failedCombinationVisit, failedCombinationTechnician);
                if (currentIteration == null)
                {
                    costChange = bestResultCostChange;
                    return bestResult;
                }
                
                foreach (KeyValuePair<Visit, Technician> iterationItem in currentIteration)
                {
                    if (!currentLayout.ContainsKey(iterationItem.Key)
                        || currentLayout[iterationItem.Key] != iterationItem.Value)
                    {
                        if (!m_technicianStateMap.ContainsKey(iterationItem.Value))
                            m_technicianStateMap.Add(iterationItem.Value, 
                                new TechnicianState(iterationItem.Value, m_bookingEngine));

                        if (currentLayout.ContainsKey(iterationItem.Key))
                        {
                            m_technicianStateMap[currentLayout[iterationItem.Key]].RemoveVisit(iterationItem.Key);
                            currentLayout.Remove(iterationItem.Key);
                        }
                                                    
                        if (m_technicianStateMap[iterationItem.Value].AddVisit(iterationItem.Key))
                            currentLayout.Add(iterationItem.Key, iterationItem.Value);
                        else
                        {
                            failedCombinationVisit = iterationItem.Key;
                            failedCombinationTechnician = iterationItem.Value;                            
                            break;
                        }
                    }
                }

                if (currentLayout.Count == m_visitsToReinsert.Count)
                {
                    failedCombinationVisit = null;
                    failedCombinationTechnician = null;
                    double currentCost = 0;                    

                    foreach (Technician affectedTechnician in currentLayout.Values.Distinct())
                        currentCost += m_technicianStateMap[affectedTechnician].GetCostChange();

                    if (currentCost < bestResultCostChange)
                    {
                        bestResult = new Dictionary<Visit, Technician>(currentLayout);
                        bestResultCostChange = currentCost;
                    }

                    if (BookingEngine.IgnoreWorkingHoursMode 
                        && m_maxTechniciansCountWithExtendedHours > 0)
                    {
                        int extendedTechniciansCount = 0;

                        WorkingHoursExtensionResult extensionItem = null;
                        foreach (Technician affectedTechnician in currentLayout.Values.Distinct())
                        {
                            if (!PathAnalyzer.IsPathPossibleWithWorkingHours(
                                m_technicianStateMap[affectedTechnician].ModifedPath, affectedTechnician))
                            {
                                extendedTechniciansCount++;
                                if (extendedTechniciansCount > m_maxTechniciansCountWithExtendedHours)
                                    break;
                                extensionItem = PathAnalyzer.GetWorkingHoursExtensionResult(
                                    m_technicianStateMap[affectedTechnician].ModifedPath, affectedTechnician);
                            }
                        }

                        if (extendedTechniciansCount <= m_maxTechniciansCountWithExtendedHours
                            && extensionItem != null)
                        {
                            extensionItem.CostChange = currentCost;
                            extensions.Add(extensionItem);
                        }
                    }
                }
            }
        }

        #endregion


        #region NextIteration

        private Dictionary<Visit, Technician> NextIteration(Visit failedCombinationVisit, Technician failedCombinationTechnician)
        {
            if (!IncrementCurrentIndexMap(failedCombinationVisit, failedCombinationTechnician))
                return null;

            Dictionary<Visit, Technician> result = new Dictionary<Visit, Technician>();
            foreach (ReinsertionItem item in m_reinsertionItems[m_outerReinsertionListIndex])
                result.Add(item.Visit, item.CurrentTechnician);
            
            return result;
        }

        #endregion

        #region IncrementCurrentIndexMap

        private bool IncrementCurrentIndexMap(int positionIndex)
        {
            if (m_reinsertionItems[m_outerReinsertionListIndex][positionIndex].CurrentTechnicianIndex
                == m_reinsertionItems[m_outerReinsertionListIndex][positionIndex].MaxIndex)
            {
                if (positionIndex == 0)
                {
                    m_outerReinsertionListIndex++;
                    if (m_outerReinsertionListIndex == m_reinsertionItems.Count)
                        return false;
                    if (m_reinsertionItems[m_outerReinsertionListIndex].Count == 0)
                        return false;

                    foreach (ReinsertionItem item in m_reinsertionItems[m_outerReinsertionListIndex])
                        item.CurrentTechnicianIndex = 0;

                    return true;
                }

                m_reinsertionItems[m_outerReinsertionListIndex][positionIndex].CurrentTechnicianIndex = 0;
                return IncrementCurrentIndexMap(positionIndex - 1);
            }
                
            m_reinsertionItems[m_outerReinsertionListIndex][positionIndex].CurrentTechnicianIndex++;
            return true;
        }        

        private bool IncrementCurrentIndexMap(Visit failedCombinationVisit, Technician failedCombinationTechnician)
        {
            if (failedCombinationVisit != null)
            {
                foreach (ReinsertionItem item in m_reinsertionItems[m_outerReinsertionListIndex])
                {
                    if (item.Visit == failedCombinationVisit)
                    {
                        Debug.Assert(item.CurrentTechnician == failedCombinationTechnician, 
                            "Failed combination should come from the previous state");

                        int index = m_reinsertionItems[m_outerReinsertionListIndex].IndexOf(item);
                        for (int i = index + 1; i < m_reinsertionItems[m_outerReinsertionListIndex].Count; i++)
                            m_reinsertionItems[m_outerReinsertionListIndex][i].CurrentTechnicianIndex = 0;
                        return IncrementCurrentIndexMap(index);
                    }
                }

                Debug.Assert(false, "Failed combination visit is not found");
            }

            return IncrementCurrentIndexMap(m_visitsToReinsert.Count - 1);
        }

        #endregion
    }

    internal class ReinsertionItem
    {
        #region ReinsertionItem

        public ReinsertionItem(Visit visit, List<Technician> allowedTechnicians)
        {
            m_visit = visit;
            m_allowedTechnicians = allowedTechnicians;
        }

        #endregion


        #region Visit

        private Visit m_visit;
        public Visit Visit
        {
            get { return m_visit; }
        }

        #endregion

        #region AllowedTechnicians

        private List<Technician> m_allowedTechnicians;
        public List<Technician> AllowedTechnicians
        {
            get { return m_allowedTechnicians; }
        }

        #endregion

        #region MaxIndex

        public int MaxIndex
        {
            get { return m_allowedTechnicians.Count - 1; }
        }

        #endregion

        #region CurrentTechnicianIndex

        private int m_currentTechnicianIndex;
        public int CurrentTechnicianIndex
        {
            get { return m_currentTechnicianIndex; }
            set { m_currentTechnicianIndex = value; }
        }

        #endregion

        #region CurrentTechnician

        public Technician CurrentTechnician
        {
            get
            {
                return m_allowedTechnicians[m_currentTechnicianIndex];
            }
        }

        #endregion
    }

    internal class TechnicianState
    {
        #region TechnicianState

        public TechnicianState(Technician technician, BookingEngine bookingEngine)
        {
            m_technician = technician;
            m_originalPath = bookingEngine.GetSortedVisits(technician);
            m_originalCost = BookingEngine.GetCost(m_originalPath, technician);

            m_modifiedPath = new List<Visit>();
            foreach (Visit visit in m_originalPath)
                m_modifiedPath.Add(visit);
        }

        #endregion

        private Technician m_technician;
        private List<Visit> m_originalPath;
        private double m_originalCost;
        private List<Visit> m_modifiedPath;

        #region AddVisit

        public bool AddVisit(Visit visit)
        {
            List<Visit> newPath = BookingEngine.FindPath(m_technician, m_modifiedPath, visit);
            if (newPath.Count == 0)
                return false;
            m_modifiedPath = newPath;
            return true;
        }

        #endregion

        #region RemoveVisit

        public void RemoveVisit(Visit visit)
        {
            if (!m_modifiedPath.Remove(visit))
                Debug.Fail("Visit removal from modified list failed");
        }

        #endregion

        #region GetCostChange

        public double GetCostChange()
        {
            return BookingEngine.GetCost(m_modifiedPath, m_technician) - m_originalCost;
        }

        #endregion

        #region ModifedPath

        public List<Visit> ModifedPath
        {
            get { return m_modifiedPath; }
        }

        #endregion

    }
}

