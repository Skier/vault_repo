using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using SmartSchedule.Data;
using SmartSchedule.Domain.Combinatorics;
using SmartSchedule.Domain.Sync;
using SmartSchedule.Domain.WCF;
using SmartSchedule.SDK;
using System.Linq;

namespace SmartSchedule.Domain
{
    public class BookingEngine
    {
        private Dictionary<int, TimeFrame> m_timeFrameMap;
        private static bool m_ignoreWorkingHoursMode;

        #region Constructor

        public BookingEngine(DateTime date, List<Visit> visits, List<TimeFrame> timeFrames, 
            List<Technician> technicians)
        {
            try
            {                
                m_visits = new BindingListVisit();
                foreach (var visit in visits)
                    Visits.Add(visit);

                m_timeFrameMap = new Dictionary<int, TimeFrame>();
                foreach (TimeFrame frame in timeFrames)
                    m_timeFrameMap.Add(frame.ID, frame);

                m_technicians = new Dictionary<DateTime, BindingList<Technician>>();
                m_technicians.Add(date, new BindingList<Technician>());

                foreach (Technician technician in technicians)
                    m_technicians[date].Add(technician);

                m_delayedVisits = new Dictionary<DateTime, List<Visit>>();
                m_temporaryAssignedVisits = new Dictionary<DateTime, List<Visit>>();
                m_visitsUnscheduledManually = new List<string>();
            }
            catch (Exception e)
            {
                Host.Trace("Error", e.Message + e.StackTrace);
                throw;
            }
        }

        public BookingEngine()
        {
            Init();
        }        

        public void Init()
        {
            try
            {
                m_visits = new BindingListVisit();
                m_technicians = new Dictionary<DateTime, BindingList<Technician>>();
                m_visitsUnscheduledManually = new List<string>();

                RefreshTechnicians();

                m_timeFrameMap = new Dictionary<int, TimeFrame>();
                foreach (TimeFrame frame in TimeFrame.TimeFrames.Values)
                {
                    if (!m_timeFrameMap.ContainsKey(frame.ID))
                        m_timeFrameMap.Add(frame.ID, frame);
                }

                foreach (Visit visit in Visit.FindWithDetails())
                    Visits.Add(visit);
                RefreshBuckets();
            }
            catch (Exception e)
            {
                Host.Trace("Error", e.Message + e.StackTrace);
                throw;
            }        
        }        
        
        public void Init(DateTime date)
        {
            try
            {
                RefreshTechnicians(date);


                foreach (Visit visit in Visit.FindWithDetails())
                    Visits.Add(visit);
                RefreshBuckets();
            }
            catch (Exception e)
            {
                Host.Trace("Error", e.Message + e.StackTrace);
                throw;
            }        
        }

        #endregion        

        #region Technicians

        private Dictionary<DateTime, BindingList<Technician>> m_technicians;
        public BindingList<Technician> GetTechnicians(DateTime date)
        {
            if (!m_technicians.ContainsKey(date.Date))
                m_technicians.Add(date.Date, new BindingList<Technician>());
            return m_technicians[date.Date];
        }

        #endregion

        #region Visits

        private BindingListVisit m_visits;
        public BindingListVisit Visits
        {
            get { return m_visits; }
        }

        #endregion

        #region VisitsUnscheduledManually

        private List<string> m_visitsUnscheduledManually;
        public List<string> VisitsUnscheduledManually
        {
            get { return m_visitsUnscheduledManually; }
        }

        #endregion

        #region DelayedVisits

        private Dictionary<DateTime, List<Visit>> m_delayedVisits;

        public List<Visit> GetDelayedVisits()
        {
            List<Visit> result = new List<Visit>();
            foreach (List<Visit> visits in m_delayedVisits.Values)
                result.AddRange(visits);
            return result.OrderByDescending(visit => visit.CanBook).ThenBy(visit => visit.TimeStart.Date).ToList();           
        }

        public List<Visit> GetDelayedVisits(DateTime date)
        {
            if (m_delayedVisits == null)
                m_delayedVisits = new Dictionary<DateTime, List<Visit>>();
            if (!m_delayedVisits.ContainsKey(date.Date))
                m_delayedVisits.Add(date.Date, new List<Visit>());

            return m_delayedVisits[date.Date];
        }

        public void SetDelayedVisits(DateTime date, List<Visit> visits)
        {
            GetDelayedVisits(date);
            m_delayedVisits[date.Date] = new List<Visit>(visits);
        }

        #endregion

        #region TemporaryAssignedVisits

        private Dictionary<DateTime, List<Visit>> m_temporaryAssignedVisits;
        public List<Visit> GetTemporaryAssignedVisits(DateTime date)
        {
            if (m_temporaryAssignedVisits == null)
                m_temporaryAssignedVisits = new Dictionary<DateTime, List<Visit>>();
            if (!m_temporaryAssignedVisits.ContainsKey(date.Date))
                m_temporaryAssignedVisits.Add(date.Date, new List<Visit>());

            return m_temporaryAssignedVisits[date.Date];
        }

        public void SetTemporaryAssignedVisits(DateTime date, List<Visit> visits)
        {
            GetTemporaryAssignedVisits(date);
            m_temporaryAssignedVisits[date.Date] = visits;
        }

        #endregion

        #region RefreshTechnicians

        public void RefreshTechnicians()
        {
            foreach (var date in Technician.FindScheduleDates())
                RefreshTechnicians(date);
        }

        public void RefreshTechnicians(DateTime date)
        {
            List<Technician> technicians = Technician.GetTechnicians(date);
            technicians.Sort((x, y) => x.DisplaySequence.CompareTo(y.DisplaySequence));

            if (!m_technicians.ContainsKey(date))
                m_technicians.Add(date, new BindingList<Technician>());

            m_technicians[date].Clear();

            foreach (Technician technician in technicians)
                m_technicians[date].Add(technician);
        }

        #endregion

        #region IgnoreWorkingHours

        public static bool IgnoreWorkingHoursMode
        {
            get { return m_ignoreWorkingHoursMode; }
        }

        private static void SetIgnoreWorkingHours()
        {
            m_ignoreWorkingHoursMode = true;
        }

        private static void DiscardIgnoreWorkingHoursMode()
        {
            m_ignoreWorkingHoursMode = false;
        }

        #endregion

        #region CanInsertVisit

        public bool CanInsertVisit(Visit visitToInsert)
        {
            InputParameters input = new InputParameters(visitToInsert, false);
            input.FindFirstPossibleResult = true;
            return CanInsertVisit(input).IsAddAllowed;
        }

        public VisitAddResult CanInsertVisit(Visit visitToInsert, bool provideInstructions)
        {
            InputParameters input = new InputParameters(visitToInsert, provideInstructions);
            return CanInsertVisit(input);
        }

        #endregion

        #region FindTimeFrameChanges

        public List<VisitAddResult> FindTimeFrameChanges(Visit bucketVisit)
        {
            List<VisitAddResult> results = new List<VisitAddResult>();

            //Gettting frames for current visit
            Visit newVisit = GetNewVisit(bucketVisit, false, null);
            foreach (TimeFrame timeFrame in TimeFrame.TimeFrames.Values)
            {                
                newVisit.ConfirmedTimeFrame = timeFrame;
                InputParameters input = new InputParameters(newVisit, false);
                VisitAddResult tempResult = CanInsertVisit(input);
                tempResult.OtherFrame = timeFrame;

                if (tempResult.IsAddAllowed)
                    results.Add(tempResult);
            }

            results.Sort(delegate(VisitAddResult result1, VisitAddResult result2)
                             {
                                 if (result1 == result2)
                                     return 0;
                                 return result1.IsBetterThan(result2) ? -1 : 1;
                             });
            return results;
        }

        #endregion


        #region Manual schedule mark

        public void MarkVisitUnscheduledManually(Visit visit)
        {
            if (!m_visitsUnscheduledManually.Contains(visit.TicketNumber))
                m_visitsUnscheduledManually.Add(visit.TicketNumber);
        }

        public void UnMarkVisitUnscheduledManually(Visit visit)
        {
            m_visitsUnscheduledManually.Remove(visit.TicketNumber);            
        }

        public bool IsVisitUnscheduledManually(Visit visit)
        {
            return m_visitsUnscheduledManually.Contains(visit.TicketNumber);
        }

        #endregion


        #region Remove

        public void RemoveTechinician(Technician technician)
        {
            m_technicians[technician.ScheduleDate].Remove(technician);
        }

        public List<Visit> RemoveVisit(Visit visitToRemove, bool isToBucket, out UserAction userAction)
        {
            return RemoveVisit(visitToRemove, isToBucket, null, out userAction);
        }

        public List<Visit> RemoveVisit(Visit visitToRemove, bool isToBucket, User user)
        {
            UserAction dummy;
            return RemoveVisit(visitToRemove, isToBucket, user, out dummy);
        }

        //Visit should be removed from main collection before running this methos
        //Returns modified visits
        private List<Visit> RemoveVisit(Visit visitToRemove, bool isToBucket, User user, out UserAction userAction)
        {
            List<Visit> result = new List<Visit>();

            string actionText;
            if (isToBucket)
            {
                actionText = string.Format("{0} unscheduled from {1}.", visitToRemove.UserFrindlyIdLong,
                    visitToRemove.Technician.Name);
            } 
            else
            {
                actionText = string.Format("{0} removed from {1}.", visitToRemove.UserFrindlyIdLong,
                    visitToRemove.Technician.Name);
            }

            Technician removedTechnician = visitToRemove.Technician;
            double removedTechcianOldCost = GetCost(Visits.GetTechnicianVisits(
                removedTechnician.ID).ToList(), removedTechnician);

            if (isToBucket)
            {
                Visit visit = (Visit) visitToRemove.Clone();
                visit.TechnicianId = null;
                visit.TempExclusiveTechnicianId = null;
                visit.IsTempIgnoreExclusivity = false;
                visit.IsFixed = false;
                Visit.Update(visit);
            }
            else
            {
                Visit.DeleteWithDetails(visitToRemove);
            }                
            
            m_visits.Remove(visitToRemove);
            double removedTechnicianNewCost = GetCost(Visits.GetTechnicianVisits(
                removedTechnician.ID).ToList(), removedTechnician);
            int costChange = (int)(removedTechnicianNewCost - removedTechcianOldCost);

            if (!visitToRemove.IsBlockout)
            {
                actionText += string.Format(" Cost change {0} -> {1} ({2})", removedTechcianOldCost.ToString("0"),
                    removedTechnicianNewCost.ToString("0"), costChange >= 0 ? "+" + costChange : "-" + (-costChange));                
            }

            userAction = new UserAction(0, user != null ? user.ID : 0,
                visitToRemove.IsBlockout ? (int)UserActionTypeEnum.Blockout : (int)UserActionTypeEnum.Visit,
                visitToRemove.IsBlockout ? removedTechnician.TechnicianDefaultId : (int?)null, 
                visitToRemove.IsBlockout ? string.Empty : visitToRemove.TicketNumber, 
                visitToRemove.ScheduleDate, DateTime.Now, actionText);

            if (user != null)
                UserAction.Insert(userAction);
           
            return result;
        }

        #endregion


        #region InsertVisit

        public VisitInsertResult InsertVisit(Visit visitToInsert)
        {
            InputParameters input = new InputParameters(visitToInsert, true);
            return InsertVisit(input);
        }

        public VisitInsertResult InsertVisitOptimizer(Visit visitToInsert)
        {
            InputParameters input = new InputParameters(visitToInsert, true);
            input.DontWriteToDb = true;
            return InsertVisit(input);
        }

        private VisitInsertResult InsertVisit(InputParameters input)
        {
            VisitInsertResult returnResult = new VisitInsertResult(false, input.VisitToInsert);
            returnResult.UserAction = new UserAction(0, 0, (int)UserActionTypeEnum.Visit, 
                null, string.Empty, input.VisitToInsert.ScheduleDate, DateTime.Now, string.Empty);            

            if (input.VisitToInsert.ConfirmedTimeFrame.IsDelayedBooking)
            {
                input.VisitToInsert.TechnicianId = null;

                if (input.DontWriteToDb)
                    input.VisitToInsert.AssignTemporaryId();                    
                else
                    Visit.InsertWithDetails(input.VisitToInsert);

                returnResult.UserAction.TicketNumber = input.VisitToInsert.TicketNumber;
                returnResult.UserAction.Text = string.Format("{0} added to bucket. ", input.VisitToInsert.UserFrindlyIdLong);
                returnResult.IsInsertSucceed = true;
                return returnResult;
            }

            VisitAddResult result = CanInsertVisit(input);
            if (result == null || !result.IsAddAllowed)
            {
                if (input.VisitToInsert.IsBlockout)
                {
                    input.VisitToInsert.Technician = input.VisitToInsert.ExclusiveTechnician;

                    if (input.DontWriteToDb)
                        input.VisitToInsert.AssignTemporaryId();
                    else
                        Visit.InsertWithDetails(input.VisitToInsert);

                    m_visits.Add(input.VisitToInsert);
                    returnResult.IsInsertSucceed = true;
                    return returnResult;
                }

                //check if delayed booking is possible
                if (BucketConstraint.IsBucketFull(input.VisitToInsert.Zip, input.VisitToInsert.ConfirmedTimeFrame))
                    return returnResult;

                input.VisitToInsert.TechnicianId = null;

                if (input.DontWriteToDb)
                    input.VisitToInsert.AssignTemporaryId();
                else
                    Visit.InsertWithDetails(input.VisitToInsert);

                returnResult.UserAction.TicketNumber = input.VisitToInsert.TicketNumber;
                returnResult.UserAction.Text = string.Format("{0} added to bucket. ", input.VisitToInsert.UserFrindlyIdLong)
                    + returnResult.UserAction.Text;

                returnResult.IsInsertSucceed = false;                                

                return returnResult;                                    
            }                

            Dictionary<Technician, double> oldCostMap = GetOldCostOnAffectedTechnicians(result);
            foreach (VisitChangeInstruction instruction in result.ChangeInstructions)
            {
                Visit visit = m_visits[instruction.VisitIndex];

                if (instruction.NewTechnician != null
                    && instruction.NewTechnician != visit.Technician)
                {
                    returnResult.UserAction.TicketNumber += visit.TicketNumber + ", ";
                    returnResult.UserAction.Text += string.Format("{0} moved from {1} to {2}. ",
                        visit.UserFrindlyIdLong, visit.Technician.Name, instruction.NewTechnician.Name);
                    visit.Technician = instruction.NewTechnician;
                }                        

                visit.SetTimeStartFixDuration(instruction.TimeStart);
                m_visits.ResetItem(instruction.VisitIndex);
                if (!input.DontWriteToDb)
                    Visit.Update(visit);
                returnResult.AddModifiedVisit(visit);
            }

            input.VisitToInsert.Technician = result.Technician;
            input.VisitToInsert.SetTimeStartFixDuration(result.VisitToAddTimeStart);
            UnMarkVisitUnscheduledManually(input.VisitToInsert);

            if (input.DontWriteToDb)
                input.VisitToInsert.AssignTemporaryId();                
            else
                Visit.InsertWithDetails(input.VisitToInsert);
                
            m_visits.Add(input.VisitToInsert);

            returnResult.UserAction.TicketNumber = input.VisitToInsert.TicketNumber + ", " + returnResult.UserAction.TicketNumber;
            returnResult.UserAction.TicketNumber = returnResult.UserAction.TicketNumber.Substring(
                0, returnResult.UserAction.TicketNumber.Length - 2);
            returnResult.UserAction.Text = string.Format("{0} added to {1}. ", input.VisitToInsert.UserFrindlyIdLong,
                input.VisitToInsert.Technician.Name) + returnResult.UserAction.Text;            

            foreach (KeyValuePair<Technician, double> pair in oldCostMap)
            {
                double previousCost = pair.Value;
                double currentCost = GetCost(
                    Visits.GetTechnicianVisits(pair.Key.ID).ToList(), pair.Key);
                int costChange = (int)(currentCost - previousCost);

                returnResult.UserAction.Text += string.Format("Cost change for {0}: {1} -> {2} ({3}). ", pair.Key.Name,
                    previousCost.ToString("0"), currentCost.ToString("0"), costChange >= 0 ? "+" + costChange : "-" + (-costChange));
            }

            returnResult.IsInsertSucceed = true;
            return returnResult;
        }

        public VisitAddResult CanInsertVisit(InputParameters input)
        {
            VisitAddResult result = null;

            while (true)
            {
                try
                {
                    if (input.IgnoreWorkingHours)
                        SetIgnoreWorkingHours();
                    result = CanInsertVisitInner(input);
                    break;
                }
                catch (VisitOutOfAllowedIntervalException e)
                {
                    if (input.IgnoreWorkingHours)
                        DiscardIgnoreWorkingHoursMode();                     
                    RearrangeVisit(e.Visit, User.Sync);
                }
                finally
                {
                    if (input.IgnoreWorkingHours)
                        DiscardIgnoreWorkingHoursMode();                     
                }                
            }

            return result;
        }

        private Dictionary<Technician, VisitAddResult> ProcessResult(
            Dictionary<Technician, VisitAddResult> bestResultMap, VisitAddResult newResult)
        {
            if (!newResult.IsAddAllowed || newResult.Technician == null)
                return bestResultMap;

            if (!bestResultMap.ContainsKey(newResult.Technician))
            {
                bestResultMap.Add(newResult.Technician, newResult);
                return bestResultMap;
            }

            if (newResult.IsBetterThan(bestResultMap[newResult.Technician]))
                bestResultMap[newResult.Technician] = newResult;
            return bestResultMap;
        }

        private static VisitAddResult GetBestResult(Dictionary<Technician, VisitAddResult> bestResultMap)
        {
            if (bestResultMap.Count == 0)
                return new VisitAddResult(false, DateTime.MinValue);

            List<VisitAddResult> results = bestResultMap.Values.ToList();
            VisitAddResult bestResult = results[0];
            foreach (VisitAddResult result in results)
            {
                if (result.IsBetterThan(bestResult))
                    bestResult = result;
            }

            results.Remove(bestResult);
            if (results.Count == 0)
            {
                bestResult.Urgency = double.MaxValue;
                return bestResult;
            }
            
            VisitAddResult bestResult2 = results[0];
            foreach (VisitAddResult result in results)
            {
                if (result.IsBetterThan(bestResult2))
                    bestResult2 = result;
            }

            bestResult.Urgency = bestResult2.CostChange - bestResult.CostChange;
            return bestResult;
        }

        private VisitAddResult CanInsertVisitInner(InputParameters input)
        {   
            Dictionary<Technician, VisitAddResult> bestResultMap 
                = new Dictionary<Technician, VisitAddResult>();

            if (!input.VisitToInsert.IsBlockout && (input.VisitToInsert.Zip == null || input.VisitToInsert.Zip.Length != 5))
                return new VisitAddResult(false, DateTime.MinValue);
           
            //Find in primary area
            List<Technician> primaryTechnicians;
            if (input.TechnicianDefaultToPlaceOnOnly != null)
            {
                primaryTechnicians = new List<Technician>();
                if (input.TechnicianToPlaceOnOnly != null)
                    primaryTechnicians.Add(input.TechnicianToPlaceOnOnly);
            }
            else
                primaryTechnicians = input.VisitToInsert.PrimaryTechnicians;

            bool isRequiredTechnicianSecondaryArea = input.TechnicianToPlaceOnOnly != null
                && input.VisitToInsert.SecondaryTechnicians.Contains(input.TechnicianToPlaceOnOnly);

            foreach (Technician technician in primaryTechnicians)
            {
                VisitAddResult result = GetBestResult(input, technician, false);
                if (input.FindFirstPossibleResult && result.IsAddAllowed)
                    return result;
                if (isRequiredTechnicianSecondaryArea)
                    result.SecondaryArea = true;
                bestResultMap = ProcessResult(bestResultMap, result);
            }

            if (isRequiredTechnicianSecondaryArea)
            {
                foreach (Technician technician in primaryTechnicians)
                {
                    VisitAddResult result = GetBestResult(input, technician, true);
                    if (input.FindFirstPossibleResult && result.IsAddAllowed)
                        return result;
                    bestResultMap = ProcessResult(bestResultMap, result);
                }
            }

            if (input.TechnicianDefaultToPlaceOnOnly != null || bestResultMap.Count > 0)
                return GetBestResult(bestResultMap);
                

            //Find in secondary area
            foreach (Technician technician in primaryTechnicians)
            {
                VisitAddResult result = GetBestResult(input, technician, true);
                if (input.FindFirstPossibleResult && result.IsAddAllowed)
                    return result;
                bestResultMap = ProcessResult(bestResultMap, result);
            }

            foreach (Technician technician in input.VisitToInsert.SecondaryTechnicians)
            {
                VisitAddResult result = GetBestResult(input, technician, false);
                result.SecondaryArea = true;
                if (input.FindFirstPossibleResult && result.IsAddAllowed)
                    return result;
                bestResultMap = ProcessResult(bestResultMap, result);
            }

            foreach (Technician technician in input.VisitToInsert.SecondaryTechnicians)
            {
                VisitAddResult result = GetBestResult(input, technician, true);
                result.SecondaryArea = true;
                if (input.FindFirstPossibleResult && result.IsAddAllowed)
                    return result;
                bestResultMap = ProcessResult(bestResultMap, result);
            }

            return GetBestResult(bestResultMap);
        }

        private void IncludePathInResult(VisitAddResult result, List<PathItem> path, Technician pahtsTechnician)
        {
            foreach (PathItem pathItem in path)
            {
                if (pathItem.Visit.ID == 0)
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

        //Rerutns object in main path technician should be extended to achieve path with removal
        private WorkingHoursExtensionResult GetMainPathExtension(Technician technician, Visit visitToInsert,
            List<Visit> removeSet)
        {
            List<Visit> sortedVisits = GetSortedVisits(technician);
            foreach (Visit removedVisit in removeSet)
                sortedVisits.Remove(removedVisit);
            List<Visit> mainPathWithRemoval = FindPath(technician, sortedVisits, visitToInsert);
            WorkingHoursExtensionResult extensionResult = PathAnalyzer.GetWorkingHoursExtensionResult(
                mainPathWithRemoval, technician);

            if (extensionResult.IsPossibleWithWorkingHours)
                return null;
            return extensionResult;
        }

        private VisitAddResult GetBestResult(InputParameters input, Technician technician,
            bool increaseVisitsInSecondaryAreaOnReinsertion)
        {
            VisitAddResult result = new VisitAddResult(false, DateTime.MinValue);
            result.Technician = technician;
            result.SecondaryArea = increaseVisitsInSecondaryAreaOnReinsertion;
            result.CostChange = double.MaxValue;

            List<Visit> sortedVisits = GetSortedVisits(technician);
            double mainPathOldCost = GetCost(sortedVisits, technician);
                       
            List<List<Visit>> visitsToRemove;
            double mainPathNewCost;
            
            List<PathItem> mainPath = FindPath(technician, sortedVisits, input.VisitToInsert,
                input.DisableSecondLevelReinsert, out visitsToRemove, out mainPathNewCost);

            Dictionary<Visit, Technician> bestPlacement = null;
            double bestPlacementCostChange = double.MaxValue;

            if (mainPath.Count > 0)
            {
                result.IsAddAllowed = true;
                result.CostChange = mainPathNewCost - mainPathOldCost;
                bestPlacementCostChange = result.CostChange;
                bestPlacement = new Dictionary<Visit, Technician>();

                if (IgnoreWorkingHoursMode && 
                    !PathAnalyzer.IsPathPossibleWithWorkingHours(mainPath, technician))
                {
                    WorkingHoursExtensionResult extension = PathAnalyzer.GetWorkingHoursExtensionResult(
                        mainPath, technician);
                    extension.CostChange = result.CostChange;
                    extension.SecondaryArea = result.SecondaryArea;
                    input.Extensions.Add(extension);
                    return result;
                }

                if (input.FindFirstPossibleResult)
                    return result;
            }

            //Try to remove visits and reinsert to other technicians
            foreach (List<Visit> removeSet in visitsToRemove)
            {
                List<WorkingHoursExtensionResult> extensions = new List<WorkingHoursExtensionResult>();
                if (IgnoreWorkingHoursMode)
                {
                    WorkingHoursExtensionResult mainPathExtension = GetMainPathExtension(
                        technician, input.VisitToInsert, removeSet);
                    if (mainPathExtension != null)
                        extensions.Add(mainPathExtension);
                }

                ReinsertionHelper helper = new ReinsertionHelper(this, removeSet, technician,
                    increaseVisitsInSecondaryAreaOnReinsertion, extensions.Count == 0);

                double costChange;
                Dictionary<Visit, Technician> currentPlacement = helper.GetBestPlacement(out costChange, out extensions);

                if (extensions.Count > 0)
                {
                    double mainPathCostChange = GetMainPathCostChange(input.VisitToInsert, technician, 
                        removeSet, mainPathOldCost);
                    foreach (WorkingHoursExtensionResult extension in extensions)
                    {
                        extension.SecondaryArea = result.SecondaryArea;
                        extension.CostChange += mainPathCostChange;
                    }

                    input.Extensions.AddRange(extensions);
                }

                if (currentPlacement == null)
                    continue;
                costChange += GetMainPathCostChange(input.VisitToInsert, technician, removeSet, mainPathOldCost);

                if (input.FindFirstPossibleResult)
                {
                    bestPlacement = currentPlacement;
                    break;
                }                    

                if (costChange < bestPlacementCostChange)
                {
                    bestPlacementCostChange = costChange;
                    bestPlacement = currentPlacement;
                }
            }

            if (bestPlacement != null)
            {
                result.IsAddAllowed = true;
                result.CostChange = bestPlacementCostChange;
                if (!input.ProvideInstructions)
                    return result;

                result.CostChange = 0;
                sortedVisits = GetSortedVisits(technician);
                foreach (Visit reinsertedVisit in bestPlacement.Keys)
                    sortedVisits.Remove(reinsertedVisit);

                mainPath = FindPath(technician, sortedVisits, input.VisitToInsert,
                    true, out visitsToRemove, out mainPathNewCost);
                result.CostChange += mainPathNewCost - mainPathOldCost;
                Debug.Assert(mainPath.Count > 0, "Unable to construct main path");
                IncludePathInResult(result, mainPath, technician);

                Dictionary<Technician, List<Visit>> bestPlacementTechMap 
                    = new Dictionary<Technician, List<Visit>>();
                foreach (KeyValuePair<Visit, Technician> placementItem in bestPlacement)
                {
                    if (!bestPlacementTechMap.ContainsKey(placementItem.Value))
                        bestPlacementTechMap.Add(placementItem.Value, new List<Visit>());
                    bestPlacementTechMap[placementItem.Value].Add(placementItem.Key);
                }

                foreach (KeyValuePair<Technician, List<Visit>> placementTechItem in bestPlacementTechMap)
                {
                    sortedVisits = GetSortedVisits(placementTechItem.Key);
                    double oldCost = GetCost(sortedVisits, placementTechItem.Key);

                    for (int i = 1; i < placementTechItem.Value.Count; i++)
                        sortedVisits.Add(placementTechItem.Value[i]);

                    List<PathItem> path = FindPath(placementTechItem.Key, sortedVisits, 
                        placementTechItem.Value[0], true, out visitsToRemove, out mainPathNewCost);
                    Debug.Assert(path.Count > 0, "Unable to construct one of the reinsertion paths");
                    IncludePathInResult(result, path, placementTechItem.Key);

                    result.CostChange += GetCost(path, placementTechItem.Key) - oldCost;
                }

                Debug.Assert(Math.Round(result.CostChange, 2) == Math.Round(bestPlacementCostChange, 2), 
                    "Error in reinsertion cost calculation");
            }

            return result;
        }

        private double GetMainPathCostChange(Visit visitToInsert, Technician mainTechnician, 
            List<Visit> removeSet, double mainPathOldCost)
        {
            List<Visit> sortedVisits = GetSortedVisits(mainTechnician);
            foreach (Visit removedVisit in removeSet)
                sortedVisits.Remove(removedVisit);
            double adjustedMainPathCost;
            List<Visit> adjustedMainPath = FindPath(mainTechnician, sortedVisits, visitToInsert, out adjustedMainPathCost);
            Debug.Assert(adjustedMainPath.Count > 0, "Adjusted main path cannot be found");
            return adjustedMainPathCost - mainPathOldCost;            
        }

        #endregion

        #region Find path in graph

        private static List<PathItem> CopyPath(List<PathItem> path)
        {
            List<PathItem> result = new List<PathItem>();
            foreach (PathItem item in path)
                result.Add(new PathItem(item.Visit, item.TimeStart, item.Technician));
            return result;
        }

        public static List<Visit> FindPath(Technician technician, List<Visit> sortedVisits,
            Visit visitToInsert)
        {
            double dummy2;
            return FindPath(technician, sortedVisits, visitToInsert, out dummy2);
        }

        public static List<Visit> FindPath(Technician technician, List<Visit> sortedVisits,
            Visit visitToInsert, out double pathTotalCost)
        {
            List<List<Visit>> dummy1;

            List<Visit> result = new List<Visit>();
            List<PathItem> path = FindPath(technician, sortedVisits, visitToInsert, true, out dummy1, out pathTotalCost);
            if (path == null || path.Count == 0)
                return result;
            foreach (PathItem item in path)
                result.Add(item.Visit);
            return result;
        }

        //Ordered dictionary: Key - start time in path, Value - Visit
        //Inner list contains visit and it's minimal start time in this path
        public static List<PathItem> FindPath(Technician technician, List<Visit> sortedVisits, 
            Visit visitToInsert, bool forceFullPath, out List<List<Visit>> removeSets,
            out double pathTotalCost)
        {
            List<List<PathItem>> paths = new List<List<PathItem>>();
            List<PathItem> path = new List<PathItem>();
            Dictionary<Visit, bool> pathCopy = new Dictionary<Visit, bool>();

            sortedVisits = new List<Visit>(sortedVisits);
            if (!sortedVisits.Contains(visitToInsert))
                sortedVisits.Add(visitToInsert);

            if (visitToInsert.GetAllowedInterval(technician) == null)
            {
                removeSets = new List<List<Visit>>();
                pathTotalCost = 0;
                return path;
            }

            Dictionary<Visit, List<Visit>> graph = GetPrecedenceGraph(sortedVisits, technician);
            List<List<Visit>> visitsToRemove = new List<List<Visit>>();
            
            path.Add(new PathItem(sortedVisits[0],
                sortedVisits[0].GetAllowedInterval(technician).Start, technician));
            pathCopy.Add(sortedVisits[0], false);
            
            if (forceFullPath)
            {
                foreach (Visit visit in sortedVisits)
                {
                    List<Visit> removeSet = new List<Visit>();
                    removeSet.Add(visit);
                    visitsToRemove.Add(removeSet);
                }
            } 
            else
            {
                List<Visit> removeSet = new List<Visit>();
                removeSet.Add(visitToInsert);
                visitsToRemove.Add(removeSet);
            }

            PathItem lastRemovedItem = null;
            while(true)
            {
                if (path.Count == graph.Count && !PathAnalyzer.IsTechnicianLimitsExceeded(path, technician))
                    paths.Add(CopyPath(path));                                        
                    
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
                            path.Add(new PathItem(newFirstVisit, 
                                newFirstVisit.GetAllowedInterval(technician).Start, technician));
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
                        && adjacentVisits[i].GetMaxTimeStart(technician) >= minEndTime)
                    {
                        if (adjacentVisits[i].GetMinTimeStart(technician) <= minEndTime)
                            nextInPath = new PathItem(adjacentVisits[i], minEndTime, technician);
                        else
                        {
                            nextInPath = new PathItem(adjacentVisits[i], 
                                adjacentVisits[i].GetMinTimeStart(technician).Value, technician);
                        }
                            

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

                        if (!PathAnalyzer.IsTechnicianLimitsExceeded(path, technician))
                            ModifyVisitsToRemove(visitsToRemove, removeList);
                    }

                    path.Remove(lastInPath);
                    pathCopy.Remove(lastInPath.Visit);
                    lastRemovedItem = lastInPath;
                }
            }

            //Find bestpath
            List<PathItem> bestPath = new List<PathItem>();
            double bestPathCost = double.MaxValue;
            if (paths.Count > 0)
            {
                bestPath = paths[0];
                visitsToRemove.Clear(); //No visits to remove if path exists
                foreach (List<PathItem> currentPath in paths)
                {                    
                    double pathCost = GetCost(currentPath, technician);
                    if (pathCost < bestPathCost)
                    {
                        bestPathCost = pathCost;
                        bestPath = currentPath;
                    }
                }
            }


            if (forceFullPath)
                removeSets = new List<List<Visit>>();
            else
            {
                foreach (List<Visit> set in visitsToRemove)
                {
                    if (set.Count == 1 && visitToInsert == set[0])
                    {
                        visitsToRemove.Remove(set);
                        break;
                    }
                }

                removeSets = new List<List<Visit>>();
                foreach (List<Visit> removeSet in visitsToRemove)
                    removeSets.Add(removeSet);
            }

            pathTotalCost = bestPathCost;
            return bestPath;
        }
                
        private static void ModifyVisitsToRemove(List<List<Visit>> currentRemoveSets, 
            List<Visit> newRemoveSet)
        {
            foreach (List<Visit> existingRemoveList in currentRemoveSets)
            {
                if (existingRemoveList.Count <= newRemoveSet.Count
                    && IsContainsAll(existingRemoveList, newRemoveSet))
                {
                    return;
                }
            }
            
            List<List<Visit>> keysToRemove = new List<List<Visit>>();
            foreach (List<Visit> set in currentRemoveSets)
            {
                if (set.Count > newRemoveSet.Count
                    && IsContainsAll(newRemoveSet, set))
                {
                    keysToRemove.Add(set);                    
                }                
            }

            foreach (List<Visit> key in keysToRemove)
                currentRemoveSets.Remove(key);

            currentRemoveSets.Add(newRemoveSet);            
        }

        private static bool IsRightCandidateToBeNextInPathByContiguity(Visit candidate,
            Dictionary<Visit, List<Visit>> graph, 
            Dictionary<Visit, bool> currentPath, List<List<Visit>> visitsToRemove)
        {
            List<Visit> candidateConnections = graph[candidate];

            foreach (List<Visit> removeSet in visitsToRemove)
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

        private static Dictionary<Visit, List<Visit>> GetPrecedenceGraph(List<Visit> sortedVisits, Technician technician)
        {
            try
            {
                sortedVisits.Sort(delegate(Visit x, Visit y)
                {
                    //when it fails here that means path is not possible at all, so ticket we trying to
                    //insert shouldn't be in sorted list (we have to return empty path before)                
                    int rv = x.GetMinTimeEnd(technician).Value.CompareTo(y.GetMinTimeEnd(technician).Value);
                    if (rv == 0)
                        return x.DurationCost.CompareTo(y.DurationCost);
                    return rv;
                });
            }
            catch (Exception e)
            {
                Host.Trace("GetPrecedenceGraph", "Unhandled exception: " + e.Message + e.StackTrace);
                foreach (Visit visit in sortedVisits)
                {
                    if (visit.GetMinTimeEnd(technician) == null)
                    {
                        Host.Trace("GetPrecedenceGraph", string.Format("GetMinTimeEnd is null for ticket {0} on Technician {1}. Original technician is {2}",
                            visit.TicketNumber, technician.Name, visit.TechnicianName));
                        if (!visit.IsWithinAllowedInterval)
                        {
                            Host.Trace("GetPrecedenceGraph", string.Format("Visit {0} is out of allwed interval. Adjusting...",
                                visit.TicketNumber));
                            throw new VisitOutOfAllowedIntervalException(visit);
                        }
                    }
                }
                throw;
            }
         
            Dictionary<Visit, List<Visit>> result = new Dictionary<Visit, List<Visit>>();
            Dictionary<Visit, int> outgoingVisitsCountMap = new Dictionary<Visit, int>();

            int prevConnectionsCount = int.MaxValue;
            bool isResortRequired = false;


            foreach (Visit keyVisit in sortedVisits)
            {
                List<Visit> keyConnections = new List<Visit>();
                DateTime keyVisitMinEndTime = keyVisit.GetMinTimeEnd(technician).Value;

                foreach (Visit valueVisit in sortedVisits)
                {
                    if (keyVisit == valueVisit)
                        continue;
                    
                    if (valueVisit.GetMaxTimeStart(technician) >= keyVisitMinEndTime)
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
                sortedVisits.Sort(delegate(Visit x, Visit y)
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

        public List<Visit> GetSortedVisits(Technician technician)
        {
            return m_visits.GetTechnicianVisits(technician.ID).ToList();
        }        

        #endregion

        #region GetNewVisit

        public Visit GetNewVisit(DateTime scheduleDate, TimeFrame timeFrame, decimal cost, float latitude, float longitude,
            string zip, int? exclusiveCompanyId, int? exclusiveTechnicianDefaultId, int? tempExclusiveTechnicianId,
            bool isTempIgnoreExclusivity, int? forbiddenTechnicianDefaultId, string ticketNumber, 
            List<VisitDetail> visitDetails, string customerName, string street, string address2, string city, string state, 
            DateTime? callDateTime, bool isEstimate, bool isEstimateAndDo, bool isRework, string mapsco,
            string homePhone, string businessPhone, bool isCalledCustomer, bool isFixed, string area,
            int servType, string adsourceAcronym, int customerRank, int? originatedTechnicianDefaultId,
            DateTime? originatedCompleteDate, string originatedTicketNumber, int? customerExclusiveTechnicianDefaultId,
            string note, bool expCred, string specName, int? servmanBaseTimeFrameId, decimal sdPercent,
            decimal taxPercent, decimal durationCost, bool isBlockout)
        {
            Technician technician;

            if (m_technicians.ContainsKey(scheduleDate.Date) && m_technicians[scheduleDate.Date].Count > 0)
                technician = m_technicians[scheduleDate.Date][0];
            else
                technician = Technician.GetTechnicianDefaults()[0].ConvertTo(scheduleDate.Date);

            return new Visit(technician, scheduleDate.Date.Add(timeFrame.TimeStart.TimeOfDay), 
                timeFrame, latitude, longitude, zip, cost, exclusiveCompanyId, exclusiveTechnicianDefaultId, 
                tempExclusiveTechnicianId, isTempIgnoreExclusivity, forbiddenTechnicianDefaultId, ticketNumber, 
                visitDetails, customerName, street, address2, city, state, callDateTime,
                isEstimate, isEstimateAndDo, isRework, mapsco, homePhone, businessPhone, isCalledCustomer,
                isFixed, area, servType, adsourceAcronym, customerRank, originatedTechnicianDefaultId,
                originatedCompleteDate, originatedTicketNumber, customerExclusiveTechnicianDefaultId,
                note, expCred, specName, servmanBaseTimeFrameId, sdPercent, taxPercent, durationCost, isBlockout);
        }

        public Visit GetNewVisit(Order order, Visit correspondingExistingVisit)
        {
            TimeFrame timeFrame;
            int? servmanBaseTimeFrameId = null;

            if (correspondingExistingVisit != null
                && correspondingExistingVisit.ServmanBaseTimeFrameId.HasValue)
            {
                if (order.TimeFrameId != correspondingExistingVisit.ConfirmedTimeFrame.ID
                    && order.TimeFrameId != correspondingExistingVisit.ServmanBaseTimeFrameId)
                {
                    timeFrame = m_timeFrameMap[order.TimeFrameId];
                } else
                {
                    timeFrame = correspondingExistingVisit.ConfirmedTimeFrame;
                    servmanBaseTimeFrameId = correspondingExistingVisit.ServmanBaseTimeFrameId;
                }                    
            } else
                timeFrame = m_timeFrameMap[order.TimeFrameId];

            if (correspondingExistingVisit == null)
            {
                correspondingExistingVisit = new Visit();
                correspondingExistingVisit.ExclusiveCompanyId = null;
                correspondingExistingVisit.TempExclusiveTechnicianId = null;
                correspondingExistingVisit.IsTempIgnoreExclusivity = false;
                correspondingExistingVisit.ForbiddenTechnicianDefaultId = null;
                correspondingExistingVisit.IsCalledCustomer = false;
                correspondingExistingVisit.IsFixed = false;
            }

            return GetNewVisit(order.DateSchedule.Date, timeFrame, order.Cost, order.Latitude, order.Longitude,
                order.Zip, correspondingExistingVisit.ExclusiveCompanyId, order.ExclusiveTechnicianDefaultId, 
                correspondingExistingVisit.TempExclusiveTechnicianId, correspondingExistingVisit.IsTempIgnoreExclusivity, 
                correspondingExistingVisit.ForbiddenTechnicianDefaultId, order.TicketNumber,
                OrderDetail.Convert(order.OrderDetails), order.Cutomer, order.Street, order.Address2, order.City, 
                order.State, order.CallDateTime,
                order.IsEstimate, order.IsEstimateAndDo, order.IsRework, order.Mapsco, order.HomePhone,
                order.BusinessPhone, correspondingExistingVisit.IsCalledCustomer, correspondingExistingVisit.IsFixed, 
                order.AreaId, order.ServType, order.AdsourceAcronym, 
                order.CustomerRank, order.OriginatedTechnicianDefaultId, order.OriginatedCompleteDate, 
                order.OriginatedTicketNumber, order.CustomerExclusiveTechnicianDefaultId, order.Note,
                order.ExpCred, order.SpecName, servmanBaseTimeFrameId, order.SdPercent,
                order.TaxPercent, order.DurationCost, false);
        }

        public Visit GetNewVisit(OrderHistory order)
        {
            return GetNewVisit(order.DateSchedule.Date, m_timeFrameMap[order.TimeFrameId.Value], order.Cost, order.Latitude,
                order.Longitude, order.Zip, order.ExclusiveCompanyId, null, null, false, null, order.TicketNumber, new List<VisitDetail>(), 
                order.CustomerName, order.Street, string.Empty, order.City, order.State, order.DateTimeCall, false, false,
                false, string.Empty, string.Empty, string.Empty, false, false, string.Empty, 0, string.Empty, 0,
                null, null, string.Empty, null, string.Empty, false, string.Empty, null, 0, 0, order.Cost, false);
        }

        public Visit GetNewVisit(Visit visit, bool isTempIgonereExclusivity, Technician tempExclusiveTechnician)
        {
            Visit newVisit = GetNewVisit(visit.ScheduleDate, visit.ConfirmedTimeFrame, visit.Cost, visit.Latitude, visit.Longitude, 
                visit.Zip, visit.ExclusiveCompanyId, visit.ExclusiveTechnicianDefaultId,
                tempExclusiveTechnician != null ? tempExclusiveTechnician.ID : (int?)null, 
                isTempIgonereExclusivity, visit.ForbiddenTechnicianDefaultId, visit.TicketNumber, visit.Details, 
                visit.CustomerName, visit.Street, visit.Address2, visit.City, visit.State, visit.CallDateTime,
                visit.IsEstimate, visit.IsEstimateAndDo, visit.IsRework, visit.Mapsco, visit.HomePhone, 
                visit.BusinessPhone, visit.IsCalledCustomer, visit.IsFixed, visit.Area, visit.ServType, 
                visit.AdsourceAcronym, visit.CustomerRank, visit.OriginatedTechnicianDefaultId, visit.OriginatedCompleteDate, 
                visit.OriginatedTicketNumber, visit.CustomerExclusiveTechnicianDefaultId, visit.Note, 
                visit.ExpCred, visit.SpecName, visit.ServmanBaseTimeFrameId, visit.SdPercent, visit.TaxPercent, 
                visit.DurationCost, visit.IsBlockout);

            if (visit.IsBlockout)
            {
                newVisit.TimeStart = visit.TimeStart;
                newVisit.TimeEnd = visit.TimeEnd;
            }

            return newVisit;
        }


        #endregion               

        #region RearrangeVisits

        public VisitsChangeDetail RearrangeVisit(Visit visit, User user)
        {
            List<Visit> visits = new List<Visit>();
            visits.Add(visit);
            return RearrangeVisits(visits, visit.Technician, user);
        }

        public VisitsChangeDetail RearrangeVisits(Technician technician, User user)
        {
            return RearrangeVisits(null, technician, user);
        }

        private VisitsChangeDetail RearrangeVisits(List<Visit> visits, Technician technician, User user)
        {
            IList<Visit> visitsToRearrange;
            if (visits == null || visits.Count == 0)
                visitsToRearrange = m_visits.GetTechnicianVisits(technician.ID);
            else
                visitsToRearrange = new List<Visit>(visits);

            visitsToRearrange = visitsToRearrange.OrderByDescending(visit => visit.IsBlockout)
                .ThenByDescending(visit => visit.ExclusiveTechnicianDefaultId.HasValue).ToList();

            Dictionary<string, UserAction> visitChangeLogMap = new Dictionary<string, UserAction>();

            foreach (Visit visit in visitsToRearrange)
            {
                UserAction userAction;
                RemoveVisit(visit, false, out userAction);
                visitChangeLogMap.Add(visit.TicketNumber, userAction);
            }

            VisitsChangeDetail detail = new VisitsChangeDetail();

            foreach (Visit visit in visitsToRearrange)
            {
                Visit newVisit = GetNewVisit(visit, visit.IsTempIgnoreExclusivity, 
                    visit.TempExclusiveTechnician);
                VisitInsertResult insertResult = InsertVisit(newVisit);

                if (newVisit.TechnicianId != technician.ID)
                {
                    if (newVisit.TechnicianId == null)
                        detail.AddRemovedVisit(newVisit);
                    else
                    {
                        detail.AddModifiedVisit(newVisit);
                        foreach (var modifiedVisit in insertResult.ModifiedVisits)
                            detail.AddModifiedVisit(modifiedVisit);
                    }

                    visitChangeLogMap[visit.TicketNumber].User = user;
                    visitChangeLogMap[visit.TicketNumber].Text = "Auto rearrange. " + visitChangeLogMap[visit.TicketNumber].Text;
                    insertResult.UserAction.User = user;
                    insertResult.UserAction.Text = "Auto rearrange. " + insertResult.UserAction.Text;
                    UserAction.Insert(visitChangeLogMap[visit.TicketNumber]);
                    UserAction.Insert(insertResult.UserAction);
                }
                else if (newVisit.TimeStart != visit.TimeStart
                    || newVisit.TimeEnd != visit.TimeEnd)
                {
                    detail.AddModifiedVisit(newVisit);
                    foreach (var modifiedVisit in insertResult.ModifiedVisits)
                        detail.AddModifiedVisit(modifiedVisit);                    
                }                    
            }

            if (detail.DashboardModifiedVisits.Count > 0 || detail.DashboardRemovedVisits.Count > 0)
                RefreshBucket(technician.ScheduleDate);
            detail.BucketVisits = GetDelayedVisits(technician.ScheduleDate);
            detail.TemporaryAssignedVisits = GetTemporaryAssignedVisits(technician.ScheduleDate);
            return detail;
        }

        #endregion

        #region RefreshBucket

        public bool RefreshBuckets()
        {
            bool result = false;

            foreach (var scheduleDate in Technician.FindScheduleDates())
            {
                if (Configuration.IsRealtimeMode && scheduleDate.Date < DateTime.Now.Date)
                    continue;

                if (RefreshBucket(scheduleDate))
                    result = true;                
            }
            return result;
        }

        //Returns true is bucket was changed
        public bool RefreshBucket(DateTime date)
        {
            Dictionary<int, bool> oldDelayedVisits = new Dictionary<int, bool>();
            foreach (Visit visit in GetDelayedVisits(date))
                oldDelayedVisits.Add(visit.ID, visit.CanBook);

            SetDelayedVisits(date, Visit.FindDelayedVisits(date));
            SetTemporaryAssignedVisits(date, Visit.FindTemporaryAssignedVisits(date));

            List<Visit> bothVisitTypes = new List<Visit>(GetDelayedVisits(date));
            bothVisitTypes.AddRange(GetTemporaryAssignedVisits(date));

            foreach (Visit visit in bothVisitTypes)
            {
                Visit newVisit = GetNewVisit(visit, false, null);
                if (CanInsertVisit(newVisit))
                    visit.CanBook = true;
                else
                    UnMarkVisitUnscheduledManually(visit);
            }

            List<Visit> newDelayedVisits = GetDelayedVisits(date);
            newDelayedVisits.Sort(delegate(Visit x, Visit y) { return y.CanBook.CompareTo(x.CanBook); });
            GetTemporaryAssignedVisits(date).Sort(delegate(Visit x, Visit y) { return y.CanBook.CompareTo(x.CanBook); });

            if (oldDelayedVisits.Count != newDelayedVisits.Count)
                return true;

            foreach (Visit newDelayedVisit in newDelayedVisits)
            {
                if (!oldDelayedVisits.ContainsKey(newDelayedVisit.ID))
                    return true;
                if (oldDelayedVisits[newDelayedVisit.ID] != newDelayedVisit.CanBook)
                    return true;
            }

            return false;
        }

        #endregion

        #region BookDelayedVisit

        public List<VisitsChangeDetail> BookDelayedVisit(Visit visit, RecommendationResponseItem recommendationItem, User user)
        {
            List<VisitsChangeDetail> results = new List<VisitsChangeDetail>();
            DateTime originalDate = visit.ScheduleDate;
            if (recommendationItem != null)
            {
                visit.ServmanBaseTimeFrameId = visit.ConfirmedTimeFrame.ID;
                int newTimeFrameId = Utils.GetTimeFrameId(recommendationItem.TimeFrame);
                visit.ConfirmedTimeFrame = TimeFrame.TimeFrames[newTimeFrameId];
                visit.TimeStart = recommendationItem.DateSchedule.Add(visit.TimeStart.TimeOfDay);
            }
            Visit newVisit = GetNewVisit(visit, false, null);

            if (!CanInsertVisit(newVisit))
            {
                VisitsChangeDetail failResult = new VisitsChangeDetail();
                failResult.IsOperationFailed = true;
                RefreshBucket(visit.ScheduleDate);
                failResult.BucketVisits = GetDelayedVisits(visit.ScheduleDate);
                failResult.TemporaryAssignedVisits = GetTemporaryAssignedVisits(visit.ScheduleDate);
                failResult.AffectedDates = new List<DateTime>() { visit.ScheduleDate };
                results.Add(failResult);
                return results;
            }

            Visit.DeleteWithDetails(visit);
            if (visit.TempExclusiveTechnician != null)
                Visits.Remove(Visits.GetVisitByTicketNumber(visit.TicketNumber));

            VisitInsertResult insertResult = InsertVisit(newVisit);
            VisitsChangeDetail result = new VisitsChangeDetail();
            result.AddAddedVisit(insertResult.InsertionVisit);
            result.DashboardModifiedVisits.AddRange(insertResult.ModifiedVisits);

            insertResult.UserAction.User = user;
            insertResult.UserAction.Text = "Green bucket ticket resolved. " + insertResult.UserAction.Text;
            UserAction.Insert(insertResult.UserAction);

            RefreshBucket(visit.ScheduleDate);
            result.BucketVisits = GetDelayedVisits(visit.ScheduleDate);            
            result.TemporaryAssignedVisits = GetTemporaryAssignedVisits(visit.ScheduleDate);
            result.AffectedDates = new List<DateTime>() { visit.ScheduleDate };
            results.Add(result);

            if (visit.ScheduleDate != originalDate)
            {
                result = new VisitsChangeDetail();
                RefreshBucket(originalDate);
                result.BucketVisits = GetDelayedVisits(originalDate);
                result.TemporaryAssignedVisits = GetTemporaryAssignedVisits(originalDate);
                result.AffectedDates = new List<DateTime>() { originalDate };
                results.Add(result);
            }
            return results;
        }

        #endregion

        #region FindServerVisit

        public Visit FindServerVisit(int visitId, DateTime date)
        {
            if (Visits.ContainsVisit(visitId))
                return Visits.GetVisitById(visitId);

            foreach (var delayedVisit in GetDelayedVisits(date))
            {                
                if (delayedVisit.ID == visitId)
                    return delayedVisit;
            }

            foreach (var tempAssignedVisit in GetTemporaryAssignedVisits(date))
            {
                if (tempAssignedVisit.ID == visitId)
                    return tempAssignedVisit;
            }

            return null;
        }

        #endregion

        #region GetCost

        private Dictionary<Technician, double> GetOldCostOnAffectedTechnicians(VisitAddResult addResult)
        {
            Dictionary<Technician, double> map = new Dictionary<Technician, double>();

            if (addResult.Technician != null)
                map.Add(addResult.Technician, GetCost(Visits.GetTechnicianVisits(
                    addResult.Technician.ID).ToList(), addResult.Technician));

            foreach (VisitChangeInstruction instruction in addResult.ChangeInstructions)
            {
                Technician affectedTechnician1 = m_visits[instruction.VisitIndex].Technician;
                Technician affectedTechnician2 = instruction.NewTechnician;

                if (!map.ContainsKey(affectedTechnician1))
                {
                    map.Add(affectedTechnician1, GetCost(Visits.GetTechnicianVisits(
                        affectedTechnician1.ID).ToList(), affectedTechnician1));
                }

                if (affectedTechnician2 != null && !map.ContainsKey(affectedTechnician2))
                {
                    map.Add(affectedTechnician2, GetCost(Visits.GetTechnicianVisits(
                        affectedTechnician2.ID).ToList(), affectedTechnician2));
                }
            }

            return map;
        }

        public static double GetCost(List<PathItem> path, Technician technician)
        {
            List<Visit> visits = new List<Visit>();
            foreach (var pathItem in path)
                visits.Add(pathItem.Visit);
            return GetCost(visits, technician);
        }

        public static double GetCost(List<Visit> visits, Technician technician)
        {
            double result = 0;
            double penalty = 0;

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

            return result + penalty * Utils.PENALTY_MULTIPLIER;
        }

        #endregion
    }

    public class PathItem : ICloneable
    {
        #region Constructor

        public PathItem(Visit visit, DateTime timeStart, Technician technician)
        {
            m_visit = visit;
            m_timeStart = timeStart;
            m_technician = technician;
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

        #region Technician

        private Technician m_technician;
        public Technician Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
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
                return TimeStart.AddMinutes(Visit.GetDurationOnTechnician(m_technician));
            }
        }

        #endregion

        #region Clone

        public object Clone()
        {
            return new PathItem(m_visit, m_timeStart, m_technician);
        }

        #endregion

    }      
}
