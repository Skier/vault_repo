using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

namespace SmartSchedule.Domain
{
    public class BookingEngine
    {
        public delegate void VisitsCountChangedHandler(int visitsCount);
        public event VisitsCountChangedHandler VisitsCountChanged;

        public delegate void TechniciansLoadChangedHandler();
        public event TechniciansLoadChangedHandler TechniciansLoadChanged;

        #region Constructor

        public BookingEngine()
        {
            m_visits = new BindingListEx<Visit>();
            m_visits.ListChanged += OnVisitsListChanged;
            m_visits.BeforeRemove += OnVisitsBeforeRemove;

            TechniciansCount = 3;
            FutureDaysCount = 4;
        }

        #endregion

        #region Technicians

        private BindingList<Technician> m_technicians;
        public BindingList<Technician> Technicians
        {
            get { return m_technicians; }
        }

        #endregion

        #region Visits

        private BindingListEx<Visit> m_visits;
        public BindingListEx<Visit> Visits
        {
            get { return m_visits; }
        }

        #endregion

        #region TimeFrames

        private BindingList<TimeFrame> m_timeFrames;
        public BindingList<TimeFrame> TimeFrames
        {
            get { return m_timeFrames; }
        }

        #endregion

        #region FutureDaysCount

        private int m_futureDaysCount;
        public int FutureDaysCount
        {
            get { return m_futureDaysCount; }
            set
            {
                if (m_futureDaysCount != value)
                {
                    m_futureDaysCount = value;

                    List<TimeFrame> timeFrames = TimeFrame.GetTimeFrames();
                    m_timeFrames = new BindingList<TimeFrame>();

                    foreach (TimeFrame frame in timeFrames)
                    {
                        DateTime currentDate = DateTime.Now.Date;
                        while (currentDate <= DateTime.Now.Date.AddDays(m_futureDaysCount - 1))
                        {
                            TimeFrame newFrame = new TimeFrame(
                                currentDate.Add(frame.TimeStart.TimeOfDay),
                                currentDate.Add(frame.TimeEnd.TimeOfDay));
                            m_timeFrames.Add(newFrame);
                            currentDate = currentDate.AddDays(1);
                        }
                    }            
                }                
            }
        }

        #endregion

        #region TechniciansCount

        private int m_techniciansCount;
        public int TechniciansCount
        {
            get { return m_techniciansCount; }
            set
            {
                if (m_techniciansCount != value)
                {
                    if (m_technicians == null)
                    {
                        m_technicians = new BindingList<Technician>();
                        foreach (Technician technician in Technician.Technicians.Values)
                            technician.LoadChanged += OnTechnicianLoadChanged;
                    }
                        
                    if (value > m_techniciansCount)
                    {
                        for (int i = m_techniciansCount + 1; i <= value; i++)
                            m_technicians.Add(Technician.Technicians[i]);                            
                    } else
                    {
                        for (int i = value; i < m_techniciansCount; i++)
                            m_technicians.RemoveAt(m_technicians.Count - 1);
                    }
                    
                    m_techniciansCount = value;

                    if (TechniciansLoadChanged != null)
                        TechniciansLoadChanged.Invoke();
                }
            }
        }

        #endregion

        #region CanInsertVisit

        public bool CanInsertVisit(Visit visitToInsert)
        {
            VisitAddResult result = CanInsertVisit(visitToInsert, false);
            return result.IsAddAllowed;
        }

        public VisitAddResult CanInsertVisitEx(Visit visitToInsert)
        {
            return CanInsertVisit(visitToInsert, true);            
        }

        #endregion

        #region InsertVisit

        public bool InsertVisit(Visit visitToInsert)
        {
            VisitAddResult result = CanInsertVisit(visitToInsert, true);
            if (!result.IsAddAllowed)
                return false;

            foreach (VisitChangeInstruction instruction in result.ChangeInstructions)
            {
                if (instruction.NewTechnician != null)
                    m_visits[instruction.VisitIndex].Technician = instruction.NewTechnician;

                m_visits[instruction.VisitIndex].SetTimeStartFixDuration(instruction.TimeStart);
                m_visits.ResetItem(instruction.VisitIndex);
            }

            visitToInsert.SetTimeStartFixDuration(result.VisitToAddTimeStart);
            visitToInsert.Technician = result.Technician;
            m_visits.Add(visitToInsert);

            return true;
        }

        private VisitAddResult CanInsertVisit(Visit visitToInsert, bool provideInstructions)
        {            
            TimeInterval allowedInterval = visitToInsert.AllowedInterval;
            if (visitToInsert.DurationMin > allowedInterval.DurationMin)
                return new VisitAddResult(false, DateTime.MinValue);

            List<KeyValuePair<Visit, Visit>> jointPossibilities = FindPossibleJointVisits(visitToInsert);

            //Getting Rank 2
            foreach (KeyValuePair<Visit, Visit> jointPossibility in jointPossibilities)
            {
                if (jointPossibility.Value != null 
                    && jointPossibility.Key.Technician == jointPossibility.Value.Technician)
                {
                    VisitAddResult result = GetAffordableResult(jointPossibility.Key.Technician, 
                        provideInstructions, false, jointPossibility.Key, visitToInsert, jointPossibility.Value);
                    if (result != null)
                        return result;
                }
            }

            foreach (KeyValuePair<Visit, Visit> jointPossibility in jointPossibilities)
            {
                if (jointPossibility.Value != null)
                {
                    foreach (Technician technician in m_technicians)
                    {
                        if (jointPossibility.Key.Technician == jointPossibility.Value.Technician
                            && jointPossibility.Key.Technician == technician)
                        {
                            continue;
                        }

                        VisitAddResult result = GetAffordableResult(technician, 
                            provideInstructions, false, jointPossibility.Key, visitToInsert, jointPossibility.Value);
                        if (result != null)
                            return result;                        
                    }
                }
            }

            //Getting Rank 1
            List<Visit> possibleJoints = new List<Visit>();
            foreach (KeyValuePair<Visit, Visit> jointPossibility in jointPossibilities)
            {
                if (!possibleJoints.Contains(jointPossibility.Key))
                    possibleJoints.Add(jointPossibility.Key);

                if (jointPossibility.Value != null && !possibleJoints.Contains(jointPossibility.Value))
                    possibleJoints.Add(jointPossibility.Value);
            }

            foreach (Visit possibleJoint in possibleJoints)
            {
                VisitAddResult result = GetAffordableResult(possibleJoint.Technician,
                    provideInstructions, false, possibleJoint, visitToInsert);
                if (result != null)
                    return result;                
            }

            foreach (Visit possibleJoint in possibleJoints)
            {
                foreach (Technician technician in m_technicians)
                {
                    if (technician == possibleJoint.Technician)
                        continue;

                    VisitAddResult result = GetAffordableResult(technician, 
                        provideInstructions, false, possibleJoint, visitToInsert);
                    if (result != null)
                        return result;                    
                }
            }

            //Trying to insert and don't loose rank
            foreach (Technician technician in m_technicians)
            {
                VisitAddResult result = GetAffordableResult(technician, 
                    provideInstructions, false, visitToInsert);
                if (result != null)
                    return result;
            }

            //Trying to insert and minimize lost rank
            VisitAddResult bestResult = new VisitAddResult(false, DateTime.MinValue);
            bestResult.NewJointsCount = int.MinValue;

            foreach (Technician technician in m_technicians)
            {
                VisitAddResult result = GetAffordableResult(technician, 
                    provideInstructions, true, visitToInsert);
                if (result != null && result.NewJointsCount > bestResult.NewJointsCount)
                {
                    bestResult = result;
                }                    
            }

            return bestResult;         
        }

        //ignoredTechnician - if visit is removed from this technician, we ignore rank decrease
        private int GetRankChangeAfterRemoval(Technician ignoredTechnician, Visit[] removedVisits, 
            out Dictionary<Technician, List<PathItem>> affectedPaths)
        {
            Dictionary<Technician, List<Visit>> removalsByTechnician = new Dictionary<Technician, List<Visit>>();
            foreach (Visit visit in removedVisits)
            {
                if (visit.Technician != ignoredTechnician && m_visits.Contains(visit))
                {
                    if (!removalsByTechnician.ContainsKey(visit.Technician))
                        removalsByTechnician.Add(visit.Technician, new List<Visit>());

                    removalsByTechnician[visit.Technician].Add(visit);
                }
            }

            int result = 0;

            Dictionary<Technician, List<PathItem>> paths = new Dictionary<Technician, List<PathItem>>();

            foreach (Technician technician in removalsByTechnician.Keys)
            {
                List<Visit> sortedVisits = GetSortedVisits(technician, removedVisits[0].TimeStart.Date);
                int oldRank = GetTechnicianJointRank(sortedVisits);

                foreach (Visit visit in removalsByTechnician[technician])
                    sortedVisits.Remove(visit);

                Dictionary<Visit, List<Visit>> precedenceGraph = GetPrecedenceGraph(sortedVisits);

                int newRank;
                List<PathItem> path;

                if (sortedVisits.Count == 0)
                {
                    newRank = 0;
                    path = new List<PathItem>();
                } 
                else
                {
                    Visit[] dummy = new Visit[0];
                    Dictionary<List<Visit>, int> dummy2;
                    path = FindPath(precedenceGraph, sortedVisits, dummy,
                        true, out dummy2, out newRank);                    
                }

                result += newRank - oldRank;
                paths.Add(technician, path);
            }

            affectedPaths = paths;
            return result;
        }

        private void IncludePathInResult(VisitAddResult result, List<PathItem> path, 
            Technician pahtsTechnician)
        {
            foreach (PathItem pathItem in path)
            {
                if (!m_visits.Contains(pathItem.Visit))
                {
                    result.VisitToAddTimeStart = pathItem.TimeStart;
                    continue;
                }

                if (pathItem.Visit.TimeStart != pathItem.TimeStart
                    || pathItem.Visit.Technician != pahtsTechnician)
                {
                    VisitChangeInstruction instruction = new VisitChangeInstruction(
                        m_visits.IndexOf(pathItem.Visit), pathItem.TimeStart);
                    instruction.NewTechnician = pahtsTechnician;
                    result.ChangeInstructions.Add(instruction);
                }
            }            
        }

        //If affordable result not found returns null        
        //visitsToInsert will be placed to specified technician
        //reinserted visits could be placed at any existing technician excluding specified one
        //This method calculates what rank is affordable to gain
        
        private VisitAddResult GetAffordableResult(Technician technician, bool provideInstructions, 
            bool returnBestPossibleResult, params Visit[] visitsToInsert)
        {
            Dictionary<Technician, List<PathItem>> affectedPaths;
            int rankChangeOnBlockForming = GetRankChangeAfterRemoval(technician, visitsToInsert, out affectedPaths);

            int rankToGain = visitsToInsert.Length - 1 - rankChangeOnBlockForming;

            List<Visit> sortedVisits = GetSortedVisits(technician, visitsToInsert[0].TimeStart.Date);
            int mainPathOldRank = GetTechnicianJointRank(sortedVisits);

            foreach (Visit visitToInsert in visitsToInsert)
            {
                if (!sortedVisits.Contains(visitToInsert))
                    sortedVisits.Add(visitToInsert);
            }            

            Dictionary<Visit, List<Visit>> precedenceGraph = GetPrecedenceGraph(sortedVisits);
            Dictionary<List<Visit>, int> visitsToRemove;

            int mainPathNewRank;
            List<PathItem> path = FindPath(precedenceGraph, sortedVisits, visitsToInsert,
                false, out visitsToRemove, out mainPathNewRank);

            VisitAddResult bestPossibleResult = null;

            //we've got good result w/o reinsertion
            if (path.Count > 0
                && (mainPathNewRank - mainPathOldRank >= rankToGain
                    || returnBestPossibleResult))
            {
                VisitAddResult result = new VisitAddResult(true, DateTime.MinValue);
                result.Technician = technician;
                result.NewJointsCount = mainPathNewRank - mainPathOldRank;

                if (provideInstructions)
                {
                    IncludePathInResult(result, path, technician);
                    foreach (KeyValuePair<Technician, List<PathItem>> affectedPath in affectedPaths)
                        IncludePathInResult(result, affectedPath.Value, affectedPath.Key);
                }
                    


                if (mainPathNewRank - mainPathOldRank >= rankToGain)
                    return result;
                bestPossibleResult = result;
            }

            //Try to remove visits and reinsert to other technicians

            foreach (KeyValuePair<List<Visit>, int> removeSet in visitsToRemove)
            {
                int rankToGainOnSetReinsertion = rankToGain - (removeSet.Value - mainPathOldRank);
                int gainedRankOnSetReinsertion = 0;

                List<List<PathItem>> bestReinsertionPaths = new List<List<PathItem>>();
                List<Technician> bestReinsertionPathTechnicians = new List<Technician>();
                Dictionary<Technician, List<Visit>> technicianVisitsAdjusted = new Dictionary<Technician, List<Visit>>();
                Dictionary<Technician, int> technicianRankAdjusted = new Dictionary<Technician, int>();
                bool isSetFullyReinserted = true;

                foreach (Visit visitToReinsert in removeSet.Key)
                {
                    List<PathItem> bestReinsertionPath = null;
                    Technician bestReinsertionPathTechnician = null;
                    int bestReinsertionPathGainedRank = int.MinValue;                    
                    int bestReinsertionPathRank = int.MinValue;                    

                    foreach (Technician technicianToReinsert in m_technicians)
                    {
                        if (technicianToReinsert == technician)
                            continue;

                        List<Visit> sortedVisitsReinsert;
                        int oldReinsertionPathRank;

                        if (technicianVisitsAdjusted.ContainsKey(technicianToReinsert))
                        {
                            sortedVisitsReinsert = technicianVisitsAdjusted[technicianToReinsert];
                            oldReinsertionPathRank = technicianRankAdjusted[technicianToReinsert];
                        } 
                        else
                        {
                            sortedVisitsReinsert = GetSortedVisits(technicianToReinsert,
                                visitsToInsert[0].TimeStart.Date);
                            foreach (Visit visit in visitsToInsert)
                            {
                                if (sortedVisitsReinsert.Contains(visit))
                                    sortedVisitsReinsert.Remove(visit);
                            }

                            oldReinsertionPathRank = GetTechnicianJointRank(sortedVisitsReinsert);
                        }
                        
                        sortedVisitsReinsert.Add(visitToReinsert);

                        Dictionary<Visit, List<Visit>> precedenceGraphReinsertion
                            = GetPrecedenceGraph(sortedVisitsReinsert);                        

                        List<Visit> visitsToReinsert = new List<Visit>();
                        visitsToReinsert.Add(visitToReinsert);

                        int newReinsertionPathRank;

                        Dictionary<List<Visit>, int> dummy;
                        List<PathItem> pathReinsertion = FindPath(precedenceGraphReinsertion, sortedVisitsReinsert,
                            visitsToReinsert.ToArray(), true, out dummy, out newReinsertionPathRank);

                        if (pathReinsertion.Count > 0 && newReinsertionPathRank > bestReinsertionPathGainedRank)
                        {
                            bestReinsertionPath = pathReinsertion;
                            bestReinsertionPathTechnician = technicianToReinsert;
                            bestReinsertionPathGainedRank = newReinsertionPathRank - oldReinsertionPathRank;
                            bestReinsertionPathRank = newReinsertionPathRank;
                        }
                    }

                    if (bestReinsertionPath == null)
                    {
                        isSetFullyReinserted = false;
                        break;
                    }
                        

                    if (bestReinsertionPathTechnicians.Contains(bestReinsertionPathTechnician))
                    {
                        int pathIndex = bestReinsertionPathTechnicians.IndexOf(bestReinsertionPathTechnician);
                        bestReinsertionPaths[pathIndex] = bestReinsertionPath;
                    } 
                    else
                    {
                        bestReinsertionPaths.Add(bestReinsertionPath);
                        bestReinsertionPathTechnicians.Add(bestReinsertionPathTechnician);                        
                    }

                    gainedRankOnSetReinsertion += bestReinsertionPathGainedRank;

                    if (!technicianVisitsAdjusted.ContainsKey(bestReinsertionPathTechnician))
                    {
                        technicianVisitsAdjusted.Add(bestReinsertionPathTechnician, new List<Visit>());
                        technicianRankAdjusted.Add(bestReinsertionPathTechnician, 0);
                    }
                    
                    technicianVisitsAdjusted[bestReinsertionPathTechnician].Clear();
                    foreach (PathItem pathItem in bestReinsertionPath)
                        technicianVisitsAdjusted[bestReinsertionPathTechnician].Add(pathItem.Visit);
                    technicianRankAdjusted[bestReinsertionPathTechnician] = bestReinsertionPathRank;
                }


                if (isSetFullyReinserted
                    && (gainedRankOnSetReinsertion >= rankToGainOnSetReinsertion || returnBestPossibleResult))
                {
                    VisitAddResult result = new VisitAddResult(true, DateTime.MinValue);                    
                    result.Technician = technician;
                    result.NewJointsCount = removeSet.Value - mainPathOldRank + gainedRankOnSetReinsertion;

                    if (bestPossibleResult != null
                        && result.NewJointsCount > bestPossibleResult.NewJointsCount)
                    {
                        bestPossibleResult = result;
                    }

                    if (!provideInstructions)
                    {
                        if (returnBestPossibleResult)
                            continue;
                        return result;
                    }                        

                    //Construct main path
                    List<Visit> sortedVisitsForResult = GetSortedVisits(technician, visitsToInsert[0].TimeStart.Date);
                    foreach (Visit visit in visitsToInsert)
                    {
                        if (!sortedVisitsForResult.Contains(visit))
                            sortedVisitsForResult.Add(visit);
                    }

                    foreach (Visit visit in removeSet.Key)
                    {
                        if (sortedVisitsForResult.Contains(visit))
                            sortedVisitsForResult.Remove(visit);                        
                    }

                    Dictionary<Visit, List<Visit>> precedenceGraphForResult = GetPrecedenceGraph(sortedVisitsForResult);
                    Dictionary<List<Visit>, int> dummy;
                    Visit[] dummy2 = new Visit[0];
                    int dummy3;

                    List<PathItem> mainPathResult = FindPath(precedenceGraphForResult, sortedVisitsForResult,
                        dummy2, true, out dummy, out dummy3);

                    IncludePathInResult(result, mainPathResult, technician);

                    int currentBestPathIndex = 0;
                    foreach (List<PathItem> bestReinsertionPath in bestReinsertionPaths)
                    {
                        Technician pathTechnician = bestReinsertionPathTechnicians[currentBestPathIndex];
                        IncludePathInResult(result, bestReinsertionPath, pathTechnician);

                        if (affectedPaths.ContainsKey(pathTechnician))
                            affectedPaths.Remove(pathTechnician);

                        currentBestPathIndex++;
                    }

                    foreach (KeyValuePair<Technician, List<PathItem>> affectedPath in affectedPaths)
                        IncludePathInResult(result, affectedPath.Value, affectedPath.Key);

                    if (!returnBestPossibleResult)
                        return result;
                }
            }

            if (returnBestPossibleResult && bestPossibleResult != null)
                return bestPossibleResult;

            return null;
        }

        //If key has value - 2 joints possible
        //Otherwise just one joint
        private List<KeyValuePair<Visit, Visit>> FindPossibleJointVisits(Visit visitToInsert)
        {
            List<Visit> oneJointVisits = new List<Visit>();
            List<VisitPossibleJoint> oneJointVisitsEx = new List<VisitPossibleJoint>();

            foreach (Visit visit in m_visits)
            {
                if (visit.IsCloseDrive(visitToInsert))
                {
                    VisitPossibleJoint possibleJoint = new VisitPossibleJoint(visit);

                    if (visit.MinTimeEnd <= visitToInsert.MaxTimeStart
                        && visit.MaxTimeEnd >= visitToInsert.MinTimeStart)
                    {
                        possibleJoint.CanJoinAtTop = true;
                    }

                    if (visitToInsert.MinTimeEnd <= visit.MaxTimeStart
                        && visitToInsert.MaxTimeEnd >= visit.MinTimeStart)
                    {
                        possibleJoint.CanJoinAtBottom = true;                        
                    }

                    if (possibleJoint.CanJoinAtTop || possibleJoint.CanJoinAtBottom)
                    {
                        oneJointVisitsEx.Add(possibleJoint);
                        oneJointVisits.Add(visit);
                    }                        
                }
            }

            List<KeyValuePair<Visit, Visit>> result = new List<KeyValuePair<Visit, Visit>>();

            for (int i = 0; i < oneJointVisitsEx.Count; i++)
            {
                bool isSearchForTopJoint = oneJointVisitsEx[i].CanJoinAtBottom;
                bool isSearchForBottomJoint = oneJointVisitsEx[i].CanJoinAtTop;

                for (int j = i + 1; j < oneJointVisitsEx.Count; j++)
                {
                    if (isSearchForTopJoint && oneJointVisitsEx[j].CanJoinAtTop
                        && CanHaveTwoJoints(oneJointVisitsEx[j].Visit, visitToInsert, oneJointVisitsEx[i].Visit))
                    {
                        result.Add(new KeyValuePair<Visit, Visit>(oneJointVisitsEx[j].Visit, oneJointVisitsEx[i].Visit));
                        oneJointVisits.Remove(oneJointVisitsEx[i].Visit);
                        oneJointVisits.Remove(oneJointVisitsEx[j].Visit);
                        continue;
                    }

                    if (isSearchForBottomJoint && oneJointVisitsEx[j].CanJoinAtBottom
                        && CanHaveTwoJoints(oneJointVisitsEx[i].Visit, visitToInsert, oneJointVisitsEx[j].Visit))
                    {
                        result.Add(new KeyValuePair<Visit, Visit>(oneJointVisitsEx[i].Visit, oneJointVisitsEx[j].Visit));
                        oneJointVisits.Remove(oneJointVisitsEx[i].Visit);
                        oneJointVisits.Remove(oneJointVisitsEx[j].Visit);
                        continue;
                    }
                }
            }

            foreach (Visit visit in oneJointVisits)
                result.Add(new KeyValuePair<Visit, Visit>(visit, null));

            return result;
        }

        private bool CanHaveTwoJoints(Visit topVisit, Visit middleVisit, Visit bottomVisit)
        {            
            DateTime topEnd = topVisit.MinTimeEnd;

            DateTime middleStart;
            if (middleVisit.MinTimeStart > topEnd)
                middleStart = middleVisit.MinTimeStart;
            else
                middleStart = topEnd;
            DateTime middleEnd = middleStart.AddMinutes(middleVisit.DurationMin);

            if (bottomVisit.MaxTimeStart < middleEnd)
                return false;

            DateTime bottomStart;
            if (bottomVisit.MinTimeStart > middleEnd)
                bottomStart = bottomVisit.MinTimeStart;
            else
                bottomStart = middleEnd;

            if (bottomStart != middleEnd)
            {
                if (middleVisit.MaxTimeEnd >= bottomStart)
                {
                    middleEnd = bottomStart;
                    middleStart = middleEnd.AddMinutes(-middleVisit.DurationMin);
                }
                else
                    return false;
            }

            if (middleStart != topEnd)
            {
                if (topVisit.MaxTimeEnd >= middleStart)
                    return true;
                else
                    return false;
            }
            else
                return true;
        }

        //Ordered dictionary: Key - start time in path, Value - Visit
        //Inner list contains visit and it's minimal start time in this path
        private static List<PathItem> FindPath(Dictionary<Visit, List<Visit>> graph,
            List<Visit> sortedVisits, Visit[] visitsToInsert, bool forceFullPath, out Dictionary<List<Visit>, int> removeSets,
            out int pathTotalRank)
        {
            List<List<PathItem>> paths = new List<List<PathItem>>();
            List<PathItem> path = new List<PathItem>();
            Dictionary<Visit, bool> pathCopy = new Dictionary<Visit, bool>();

            //value - path rank after removal
            Dictionary<List<Visit>, int> visitsToRemove = new Dictionary<List<Visit>, int>();

            path.Add(new PathItem(sortedVisits[0], sortedVisits[0].AllowedInterval.Start));
            pathCopy.Add(sortedVisits[0], false);
            
            if (forceFullPath)
            {
                foreach (Visit visit in sortedVisits)
                {
                    List<Visit> removeSet = new List<Visit>();
                    removeSet.Add(visit);
                    visitsToRemove.Add(removeSet, 0);
                }
            } 
            else
            {
                foreach (Visit visit in visitsToInsert)
                {
                    List<Visit> removeSet = new List<Visit>();
                    removeSet.Add(visit);
                    visitsToRemove.Add(removeSet, 0);                    
                }
            }

            PathItem lastRemovedItem = null;
            while(true)
            {
                if (path.Count == graph.Count)
                {
                    List<PathItem> copiedPath = new List<PathItem>();
                    foreach (PathItem item in path)
                        copiedPath.Add(new PathItem(item.Visit, item.TimeStart));
                    paths.Add(copiedPath);                    
                }
                    
                if (path.Count == 0)
                {
                    int lastRemovedIndex = sortedVisits.IndexOf(lastRemovedItem.Visit);

                    if (lastRemovedIndex == sortedVisits.Count - 1)
                        break;

                    for (int i = lastRemovedIndex + 1; i < sortedVisits.Count; i++)
                    {
                        if (forceFullPath && graph[sortedVisits[i]].Count + 1 < sortedVisits.Count)
                            break;                     

                        if (IsRightCandidateToBeNextInPathByContiguity(
                            sortedVisits[i], graph, pathCopy, visitsToRemove))
                        {
                            Visit newFirstVisit = sortedVisits[i];
                            path.Add(new PathItem(newFirstVisit, newFirstVisit.AllowedInterval.Start));
                            pathCopy.Add(newFirstVisit, false);
                            lastRemovedItem = null;
                            break;
                        }
                    }

                    if (path.Count == 0) //No more visits to be first in path
                        break;
                }

                PathItem lastInPath = path[path.Count - 1];
                PathItem nextInPath = null;                                

                List<Visit> adjacentVisits = graph[lastInPath.Visit];
                int startIndex = 0;
                if (lastRemovedItem != null)
                    startIndex = adjacentVisits.IndexOf(lastRemovedItem.Visit) + 1;
                DateTime minEndTime = lastInPath.TimeEnd;

                for (int i = startIndex; i < adjacentVisits.Count; i++)
                {
                    if (forceFullPath && graph[adjacentVisits[i]].Count + 1 < sortedVisits.Count - path.Count)
                        break;

                     if (!pathCopy.ContainsKey(adjacentVisits[i])
                        && IsRightCandidateToBeNextInPathByContiguity(adjacentVisits[i], graph, pathCopy, visitsToRemove)
                        && adjacentVisits[i].MaxTimeStart >= minEndTime)
                    {
                        if (adjacentVisits[i].MinTimeStart <= minEndTime)
                            nextInPath = new PathItem(adjacentVisits[i], minEndTime);
                        else
                            nextInPath = new PathItem(adjacentVisits[i], adjacentVisits[i].MinTimeStart);

                        break;                        
                    }
                }

                if (nextInPath != null)
                {
                    path.Add(nextInPath);
                    pathCopy.Add(nextInPath.Visit, false);
                    lastRemovedItem = null;
                }                    
                else
                {
                    if (!forceFullPath && lastRemovedItem == null && path.Count < sortedVisits.Count)
                    {
                        List<Visit> removeList = new List<Visit>();
                        foreach (Visit visit in sortedVisits)
                        {
                            if (!pathCopy.ContainsKey(visit))
                                removeList.Add(visit);
                        }

                        ModifyVisitsToRemove(visitsToRemove, removeList, path);
                    }

                    path.Remove(lastInPath);
                    pathCopy.Remove(lastInPath.Visit);
                    lastRemovedItem = lastInPath;
                }
            }

            if (forceFullPath)
                removeSets = null;
            else
            {
                foreach (Visit visit in visitsToInsert)
                {
                    foreach (List<Visit> set in visitsToRemove.Keys)
                    {
                        if (set.Count == 1 && visit == set[0])
                        {
                            visitsToRemove.Remove(set);
                            break;
                        }
                    }
                }

                removeSets = visitsToRemove;
            }

            if (paths.Count > 0)
            {
                List<PathItem> bestPath = paths[0];
                int bestPathTotalRank = int.MinValue;

                foreach (List<PathItem> currentPath in paths)
                {
                    int pathRank = GetPathJointRank(currentPath);
                    if (pathRank > bestPathTotalRank)
                    {
                        bestPathTotalRank = pathRank;
                        bestPath = currentPath;
                    }
                }

                pathTotalRank = bestPathTotalRank;
                return bestPath;
            }

            pathTotalRank = 0;
            return new List<PathItem>();
        }
                
        private static void ModifyVisitsToRemove(Dictionary<List<Visit>, int> currentRemoveSets, List<Visit> newRemoveSet,
            List<PathItem> currentPath)
        {
            foreach (List<Visit> existingRemoveList in currentRemoveSets.Keys)
            {
                if (existingRemoveList.Count <= newRemoveSet.Count
                    && IsContainsAll(existingRemoveList, newRemoveSet))
                {
                    return;
                }
            }
            
            List<List<Visit>> keysToRemove = new List<List<Visit>>();
            foreach (List<Visit> set in currentRemoveSets.Keys)
            {
                if (set.Count > newRemoveSet.Count
                    && IsContainsAll(newRemoveSet, set))
                {
                    keysToRemove.Add(set);                    
                }                
            }

            foreach (List<Visit> key in keysToRemove)
                currentRemoveSets.Remove(key);

            currentRemoveSets.Add(newRemoveSet, GetPathJointRank(currentPath));            
        }

        private static bool IsRightCandidateToBeNextInPathByContiguity(Visit candidate,
            Dictionary<Visit, List<Visit>> graph, 
            Dictionary<Visit, bool> currentPath, Dictionary<List<Visit>, int> visitsToRemove)
        {
            List<Visit> candidateConnections = graph[candidate];

            foreach (List<Visit> removeSet in visitsToRemove.Keys)
            {
                if (removeSet.Count == 1)
                {
                    if (candidate != removeSet[0]
                        && !currentPath.ContainsKey(removeSet[0])
                        && !candidateConnections.Contains(removeSet[0]))
                    {
                        return false;
                    }

                } else
                {
                    bool isSatisfiesOneFromSet = false;

                    foreach (Visit visitInSet in removeSet)
                    {
                        if (candidate == visitInSet
                            || currentPath.ContainsKey(visitInSet)
                            || candidateConnections.Contains(visitInSet))
                        {
                            isSatisfiesOneFromSet = true;
                            break;
                        }
                    }

                    if (!isSatisfiesOneFromSet)
                        return false;
                }
            }

            return true;
        }

        private static bool IsContainsAll(List<Visit> listToCheck, List<Visit> sourceList)
        {
            foreach (Visit visit in listToCheck)
            {
                if (!sourceList.Contains(visit))
                    return false;
            }

            return true;
        }

        private static Dictionary<Visit, List<Visit>> GetPrecedenceGraph(List<Visit> technicianSortedVisits)
        {
            technicianSortedVisits.Sort(delegate(Visit x, Visit y)
            {
                int rv = x.MinTimeEnd.CompareTo(y.MinTimeEnd);
                if (rv == 0)
                    return x.TimeStart.CompareTo(y.TimeStart);
                return rv;
            });
         
            Dictionary<Visit, List<Visit>> result = new Dictionary<Visit, List<Visit>>();
            Dictionary<Visit, int> outgoingVisitsCountMap = new Dictionary<Visit, int>();

            int prevConnectionsCount = int.MaxValue;
            bool isResortRequired = false;

            foreach (Visit keyVisit in technicianSortedVisits)
            {
                List<Visit> keyConnections = new List<Visit>();
                DateTime keyVisitMinEndTime = keyVisit.MinTimeEnd;

                foreach (Visit valueVisit in technicianSortedVisits)
                {
                    if (keyVisit == valueVisit)
                        continue;
                    
                    if (valueVisit.MaxTimeStart >= keyVisitMinEndTime)
                        keyConnections.Add(valueVisit);
                }

                result.Add(keyVisit, keyConnections);
                outgoingVisitsCountMap.Add(keyVisit, keyConnections.Count);

                if (!isResortRequired && keyConnections.Count > prevConnectionsCount)
                    isResortRequired = true;
                prevConnectionsCount = keyConnections.Count;
            }

            if (isResortRequired)
            {
                technicianSortedVisits.Sort(delegate(Visit x, Visit y)
                {
                    return outgoingVisitsCountMap[y].CompareTo(outgoingVisitsCountMap[x]);
                });

                foreach (List<Visit> connections in result.Values)
                {
                    connections.Sort(delegate(Visit x, Visit y)
                    {
                        return outgoingVisitsCountMap[y].CompareTo(outgoingVisitsCountMap[x]);
                    });
                }                
            }

            return result;
        }

        private static int GetTechnicianJointRank(IList<Visit> technicianSortedVisits)
        {
            if (technicianSortedVisits.Count < 2)
                return 0;

            int result = 0;

            for (int i = 1; i < technicianSortedVisits.Count; i++)
            {
                if (technicianSortedVisits[i].TimeStart == technicianSortedVisits[i - 1].TimeEnd
                    && technicianSortedVisits[i].IsCloseDrive(technicianSortedVisits[i - 1]))
                {
                    result++;
                }                    
            }

            return result;
        }

        private static int GetPathJointRank(List<PathItem> path)
        {
            int result = 0;

            if (path.Count <= 1)
                return result;

            List<PathItem> currentGoodJointBlock = new List<PathItem>();
            currentGoodJointBlock.Add(path[0]);

            for (int i = 1; i < path.Count; i++)
            {
                if (path[i].Visit.IsCloseDrive(path[i - 1].Visit))
                {
                    if (path[i].TimeStart == path[i - 1].TimeEnd)
                    {
                        result++;
                        currentGoodJointBlock.Add(path[i]);
                    }                        
                    else
                    {
                        //Trying to move block down to create good joint
                        DateTime currentTimeStart = path[i].TimeStart;
                        bool isNewGoodBlockFailed = false;
                        for (int j = currentGoodJointBlock.Count - 1; j >= 0; j--)
                        {
                            if (currentGoodJointBlock[j].Visit.MaxTimeEnd < currentTimeStart)
                            {
                                isNewGoodBlockFailed = true;
                                break;
                            }                                
                            else
                            {
                                currentTimeStart = currentTimeStart.AddMinutes(
                                    -currentGoodJointBlock[j].Visit.DurationMin);
                            }                                
                        }

                        if (!isNewGoodBlockFailed)
                        {
                            result++;
                            double shiftDownValue = path[i].TimeStart.Subtract(
                                currentGoodJointBlock[currentGoodJointBlock.Count - 1].TimeEnd).TotalMinutes;

                            foreach (PathItem item in currentGoodJointBlock)
                                item.TimeStart = item.TimeStart.AddMinutes(shiftDownValue);

                            currentGoodJointBlock.Add(path[i]);
                        } else
                        {
                            currentGoodJointBlock.Clear();
                            currentGoodJointBlock.Add(path[i]);                            
                        }                        
                    }

                } else
                {
                    currentGoodJointBlock.Clear();
                    currentGoodJointBlock.Add(path[i]);
                }
            }

            return result;
        }

        private List<Visit> GetSortedVisits(Technician technician, DateTime date)
        {
            List<Visit> visits = new List<Visit>();
            foreach (Visit visit in m_visits)
            {
                if (visit.Technician == technician && visit.TimeStart.Date == date)
                    visits.Add(visit);
            }

            visits.Sort(delegate(Visit x, Visit y)
                        {
                            return x.TimeStart.CompareTo(y.TimeStart);
                        });
            return visits;
        }

        #endregion

        #region OnVisitsListChanged

        private void OnVisitsBeforeRemove(int index)
        {
            m_visits[index].Technician.TotalWorkDuration -= m_visits[index].WorkDuration;
            if (TechniciansLoadChanged != null)
                TechniciansLoadChanged.Invoke();
        }

        private void OnVisitsListChanged(object sender, ListChangedEventArgs e)
        {
            if (VisitsCountChanged != null
                && (e.ListChangedType == ListChangedType.ItemAdded
                    || e.ListChangedType == ListChangedType.ItemDeleted))
            {
                VisitsCountChanged.Invoke(m_visits.Count);
            }

            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                m_visits[e.NewIndex].Technician.TotalWorkDuration += m_visits[e.NewIndex].WorkDuration;

                if (TechniciansLoadChanged != null)
                    TechniciansLoadChanged.Invoke();
            }                
        }

        #endregion

        #region CloneVisits

        public List<Visit> CloneVisits()
        {
            List<Visit> result = new List<Visit>();

            foreach (Visit visit in m_visits)
                result.Add((Visit)visit.Clone());
            return result;
        }

        #endregion


        #region NextVisitId

        public int NextVisitId
        {
            get { return m_visits.Count + 1; }
        }

        #endregion

        #region GetNewVisit

        public Visit GetNewVisit(TimeFrame timeFrame, double duration, Point location)
        {
            return new Visit(
                NextVisitId,
                m_technicians[0],
                timeFrame.TimeStart,
                timeFrame.TimeStart.AddMinutes(duration),
                timeFrame, location);
        }

        #endregion

        #region OnTechnicianLoadChanged

        private void OnTechnicianLoadChanged()
        {
            if (TechniciansLoadChanged != null)
                TechniciansLoadChanged.Invoke();
        }

        #endregion


        #region GetTechnicianRankMap

//        private List<Technician> GetTechniciansSortedByLoad()
//        {
//            List<Technician> technicians = new List<Technician>(m_technicians);
//            technicians.Sort(delegate(Technician x, Technician y)
//                             {
//                                 return x.TotalWorkDuration.CompareTo(y.TotalWorkDuration);
//                             });
//            return technicians;
//        }

//        private Dictionary<Technician, TimeFrameRank> GetTechnicianRankMap()
//        {
//            List<Technician> sortedTechnicians = GetTechniciansSortedByLoad();
//            Dictionary<Technician, TimeFrameRank> result = new Dictionary<Technician, TimeFrameRank>();
//
//            TimeFrameRank currentRank = TimeFrameRank.High;
//            result.Add(sortedTechnicians[0], currentRank);
//
//            for (int i = 1; i < sortedTechnicians.Count; i++)
//            {
//                if (sortedTechnicians[i].TotalWorkDuration
//                    != sortedTechnicians[i - 1].TotalWorkDuration)
//                {
//                    if (currentRank == TimeFrameRank.High)
//                        currentRank = TimeFrameRank.Medium;
//                    else if (currentRank == TimeFrameRank.Medium)
//                        currentRank = TimeFrameRank.Low;
//                }
//
//                result.Add(sortedTechnicians[i], currentRank);
//            }
//
//            return result;
//        }

        #endregion

        #region RecalculateTimeFramesRanks
        /// <summary>
        /// Sets Rank and IsAllowed property for each known Time Frame entity
        /// </summary>
        /// <param name="duration">Visit duration</param>        
        /// <param name="location">Visit location</param>
        public void RecalculateTimeFramesRanks(double duration, Point location)
        {
            List<TimeFrame> allowedTimeFrames = new List<TimeFrame>();

            foreach (TimeFrame timeFrame in TimeFrames)
            {
                Visit newVisit = GetNewVisit(timeFrame, duration, location);
                VisitAddResult addResult = CanInsertVisit(newVisit, false);

                timeFrame.Rank = addResult.NewJointsCount;
                timeFrame.IsAllowed = addResult.IsAddAllowed;

                if (timeFrame.IsAllowed)
                    allowedTimeFrames.Add(timeFrame);
            }

            allowedTimeFrames.Sort(delegate(TimeFrame x, TimeFrame y)
                                   {
                                       if (x.Rank != y.Rank)
                                           return -1*x.Rank.CompareTo(y.Rank);
                                       return x.TimeStart.CompareTo(y.TimeStart);
                                   });

            int currentRank = 0;
            foreach (TimeFrame timeFrame in allowedTimeFrames)
            {
                timeFrame.Rank = currentRank;
                currentRank++;
            }           

            SetPrimarySecondaryRanksBasedOnRank();            
        }

        #endregion

        #region SetPrimarySecondaryRanks

        public List<TimeFrame> GetAllowedFramesSortedByRank()
        {
            return GetAllowedFramesSortedByRank(new List<TimeFrame>(m_timeFrames));
        }

        public List<TimeFrame> GetAllowedFramesSortedByRank(List<TimeFrame> framesToRank)
        {
            List<TimeFrame> allowedFrames = new List<TimeFrame>();
            foreach (TimeFrame frame in framesToRank)
            {
                if (frame.IsAllowed)
                    allowedFrames.Add(frame);
            }

            allowedFrames.Sort(delegate(TimeFrame x, TimeFrame y) { return x.Rank.CompareTo(y.Rank); });
            return allowedFrames;
        }

        private const int PRIMARY_RANKS_COUNT = 3;
        public void SetPrimarySecondaryRanksBasedOnRank(List<TimeFrame> framesToRank)
        {
            List<TimeFrame> allowedFrames = GetAllowedFramesSortedByRank(framesToRank);
            int framesCountInPrimaryRank = allowedFrames.Count / PRIMARY_RANKS_COUNT;
            int lastGroupMinRank = (PRIMARY_RANKS_COUNT - 1) * framesCountInPrimaryRank;

            for (int i = 0; i < allowedFrames.Count; i++)
            {
                if (framesCountInPrimaryRank == 0)
                {
                    allowedFrames[i].PrimaryRank = i;
                    allowedFrames[i].SecondaryRank = 0;
                }
                else
                {
                    allowedFrames[i].PrimaryRank = i / framesCountInPrimaryRank;
                    allowedFrames[i].SecondaryRank = i % framesCountInPrimaryRank;
                }

                if (allowedFrames[i].PrimaryRank > PRIMARY_RANKS_COUNT - 1)
                    allowedFrames[i].PrimaryRank = PRIMARY_RANKS_COUNT - 1;

                if (allowedFrames[i].PrimaryRank == PRIMARY_RANKS_COUNT - 1)
                    allowedFrames[i].SecondaryRank = i - lastGroupMinRank;                
            }

            foreach (TimeFrame frame in m_timeFrames)
            {
                if (frame.IsAllowed && !framesToRank.Contains(frame))
                {
                    frame.PrimaryRank = -1;
                    frame.SecondaryRank = -1;
                }
            }

            TimeFrames.ResetBindings();
        }

        public void SetPrimarySecondaryRanksBasedOnRank()
        {
            SetPrimarySecondaryRanksBasedOnRank(new List<TimeFrame>(m_timeFrames));
        }

        #endregion
    }

    public class PathItem
    {
        #region Constructor

        public PathItem(Visit visit, DateTime timeStart)
        {
            m_visit = visit;
            m_timeStart = timeStart;
        }

        #endregion

        #region Visit

        private Visit m_visit;        
        public Visit Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region TimeStart

        private DateTime m_timeStart;
        public DateTime TimeStart
        {
            get { return m_timeStart; }
            set { m_timeStart = value; }
        }

        #endregion

        #region TimeEnd

        public DateTime TimeEnd
        {
            get
            {
                return TimeStart.AddMinutes(Visit.DurationMin);
            }
        }

        #endregion
    }

    public class VisitPossibleJoint
    {
        #region Constuctor

        public VisitPossibleJoint(Visit visit)
        {
            m_visit = visit;
        }

        #endregion

        #region Visit

        private Visit m_visit;               
        public Visit Visit
        {
            get { return m_visit; }
            set { m_visit = value; }
        }

        #endregion

        #region CanJoinAtTop

        private bool m_canJoinAtTop;
        public bool CanJoinAtTop
        {
            get { return m_canJoinAtTop; }
            set { m_canJoinAtTop = value; }
        }

        #endregion

        #region CanJoinAtBottom

        private bool m_canJoinAtBottom;
        public bool CanJoinAtBottom
        {
            get { return m_canJoinAtBottom; }
            set { m_canJoinAtBottom = value; }
        }

        #endregion
    }
}
