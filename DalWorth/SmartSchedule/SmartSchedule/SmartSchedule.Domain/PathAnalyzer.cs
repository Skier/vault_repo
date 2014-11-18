using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace SmartSchedule.Domain
{
    //Analyzes path 
    //Currently for working hours

    public class PathAnalyzer
    {
        private PathAnalyzer() { }

        #region GetWorkingHoursExtensionResult

        public static WorkingHoursExtensionResult GetWorkingHoursExtensionResult(List<Visit> validPathSequence, 
            Technician technician)
        {
            return GetWorkingHoursExtensionResult(BuildPath(validPathSequence, technician), technician);
        }

        public static WorkingHoursExtensionResult GetWorkingHoursExtensionResult(List<PathItem> path, Technician technician)
        {
            WorkingHoursExtensionResult result = new WorkingHoursExtensionResult(technician);
            result.IsPossibleWithWorkingHours = true;

            if (path.Count == 0)
                return result;

            TimeInterval pathInterval = new TimeInterval(path[0].TimeStart, path[path.Count - 1].TimeEnd);
            TimeInterval workingInterval = technician.WorkingIntervals[0].GetInterval();

            if (pathInterval.Start < workingInterval.Start)
            {
                double goalMoveDown = workingInterval.Start.Subtract(pathInterval.Start).TotalMinutes;
                double actualMoveDownPossibility = GetMaxShiftDown(path[0], path, technician,
                    true, goalMoveDown, workingInterval.End);

                if (goalMoveDown == actualMoveDownPossibility)
                    return result;

                result.IsPossibleWithWorkingHours = false;
                result.OldWorkInterval = technician.WorkingIntervals[0];
                result.NewWorkInterval = new TechnicianWorkTime(technician.ID,
                    pathInterval.Start.AddMinutes(actualMoveDownPossibility), workingInterval.End);
                result.VisitInExtendedHours = path[0].Visit;
                return result;
            }

            if (pathInterval.End > workingInterval.End)
            {
                //Don't need to try move up here 
                result.IsPossibleWithWorkingHours = false;
                result.OldWorkInterval = technician.WorkingIntervals[0];
                result.NewWorkInterval = new TechnicianWorkTime(technician.ID, workingInterval.Start,
                    pathInterval.End);
                result.VisitInExtendedHours = path[path.Count - 1].Visit;
            }

            return result;
        }

        public static bool IsPathPossibleWithWorkingHours(List<Visit> validPathSequence, Technician technician)
        {
            return GetWorkingHoursExtensionResult(validPathSequence, technician).IsPossibleWithWorkingHours;
        }

        public static bool IsPathPossibleWithWorkingHours(List<PathItem> path, Technician technician)
        {
            return GetWorkingHoursExtensionResult(path, technician).IsPossibleWithWorkingHours;
        }

        #endregion

        #region BuildPath

        private static List<PathItem> BuildPath(List<Visit> validSequence, Technician technician)
        {
            List<PathItem> result = new List<PathItem>();
            DateTime? currentTimeEnd = null;
            foreach (Visit visit in validSequence)
            {
                DateTime minTimeStart = visit.GetMinTimeStart(technician).Value;
                if (currentTimeEnd == null || minTimeStart >= currentTimeEnd.Value)
                {
                    result.Add(new PathItem(visit, minTimeStart, technician));
                    currentTimeEnd = minTimeStart.AddMinutes(visit.GetDurationOnTechnician(technician));
                }
                else
                {
                    result.Add(new PathItem(visit, currentTimeEnd.Value, technician));
                    currentTimeEnd = currentTimeEnd.Value.AddMinutes(visit.GetDurationOnTechnician(technician));                    
                }
            }

            return result;            
        }

        #endregion


        #region CopyPath

        private static List<PathItem> CopyPath(IList<PathItem> originalPath)
        {
            List<PathItem> result = new List<PathItem>();
            foreach (PathItem item in originalPath)
                result.Add((PathItem) item.Clone());
            return result;
        }

        #endregion


        #region GetMaxShiftUp

        private static double GetMaxShiftUp(PathItem pathItem, IList<PathItem> path, Technician technician,
            bool ignoreWorkingHours, double? shiftLimit, DateTime? upperTimeLimit)
        {
            if (shiftLimit == 0)
                return 0;

            int currentItemIndex = path.IndexOf(pathItem);

            double maxShift = pathItem.TimeStart.Subtract(
                pathItem.Visit.GetAllowedInterval(technician, ignoreWorkingHours).Start).TotalMinutes;

            if (shiftLimit.HasValue)
                maxShift = Math.Min(maxShift, shiftLimit.Value);

            if (currentItemIndex == 0 && upperTimeLimit.HasValue
                && upperTimeLimit.Value > pathItem.TimeStart.AddMinutes(-maxShift))
            {
                return maxShift - upperTimeLimit.Value.Subtract(pathItem.TimeStart.AddMinutes(-maxShift)).TotalMinutes;
            }

            if (currentItemIndex == 0 || maxShift == 0)
                return maxShift;

            DateTime newTimeStart = pathItem.TimeStart.AddMinutes(-maxShift);
            double nextShift = newTimeStart.Subtract(path[currentItemIndex - 1].TimeEnd).TotalMinutes;
            if (nextShift >= 0)
                return maxShift;

            nextShift = Math.Abs(nextShift);
            return maxShift - (nextShift
                - GetMaxShiftUp(path[currentItemIndex - 1], path, technician, ignoreWorkingHours, nextShift, upperTimeLimit));
        }

        #endregion

        #region GetMaxShiftDown

        private static double GetMaxShiftDown(PathItem pathItem, IList<PathItem> path, Technician technician,
            bool ignoreWorkingHours, double? shiftLimit, DateTime? lowerTimeLimit)
        {
            if (shiftLimit == 0)
                return 0;

            int currentItemIndex = path.IndexOf(pathItem);

            double maxShift = pathItem.Visit.GetAllowedInterval(technician, ignoreWorkingHours).End
                .Subtract(pathItem.TimeEnd).TotalMinutes;

            if (shiftLimit.HasValue)
                maxShift = Math.Min(maxShift, shiftLimit.Value);

            if (currentItemIndex == path.Count - 1 && lowerTimeLimit.HasValue
                && pathItem.TimeEnd.AddMinutes(maxShift) > lowerTimeLimit.Value)
            {
                return maxShift - pathItem.TimeEnd.AddMinutes(maxShift).Subtract(lowerTimeLimit.Value).TotalMinutes;
            }

            if (currentItemIndex == path.Count - 1 || maxShift == 0)
                return maxShift;

            DateTime newTimeEnd = pathItem.TimeEnd.AddMinutes(maxShift);
            double nextShift = newTimeEnd.Subtract(path[currentItemIndex + 1].TimeStart).TotalMinutes;
            if (nextShift <= 0)
                return maxShift;
            return maxShift - (nextShift
                - GetMaxShiftDown(path[currentItemIndex + 1], path, technician, ignoreWorkingHours, nextShift, lowerTimeLimit));            
        }

        #endregion

        #region ShiftDown

        //Ignores working hours restriction
        private List<PathItem> ShiftDown(PathItem pathItem, IList<PathItem> path, Technician technician, 
                                         double shiftValue)
        {
            List<PathItem> result = new List<PathItem>();

            if (shiftValue == 0)
                return CopyPath(path);

            int pathItemIndex = path.IndexOf(pathItem);
            for (int i = 0; i <= pathItemIndex - 1; i++)
                result.Add((PathItem) path[i].Clone());

            result.Add(new PathItem(pathItem.Visit, pathItem.TimeStart.AddMinutes(shiftValue), technician));

            for (int i = pathItemIndex + 1; i < path.Count; i++)
            {
                DateTime prevTimeEnd = result[result.Count - 1].TimeEnd;
                double additionalMove = path[i].TimeStart.Subtract(prevTimeEnd).TotalMinutes;

                if (additionalMove >= 0)
                    result.Add((PathItem)path[i].Clone());
                else
                    result.Add(new PathItem(path[i].Visit, 
                        pathItem.TimeStart.AddMinutes(-additionalMove), technician));
            }

            return result;
        }

        #endregion

        #region ShiftUp

        //Ignores working hours restriction
        private List<PathItem> ShiftUp(PathItem pathItem, IList<PathItem> path, Technician technician,
                                       double shiftValue)
        {
            List<PathItem> result = new List<PathItem>();

            if (shiftValue == 0)
                return CopyPath(path);

            int pathItemIndex = path.IndexOf(pathItem);
            for (int i = path.Count - 1; i > pathItemIndex; i--)
                result.Add((PathItem)path[i].Clone());

            result.Add(new PathItem(pathItem.Visit, pathItem.TimeStart.AddMinutes(-shiftValue), technician));


            for (int i = pathItemIndex - 1; i >= 0; i--)
            {
                DateTime prevTimeStart = result[result.Count - 1].TimeStart;

                double additionalMove = path[i].TimeEnd.Subtract(prevTimeStart).TotalMinutes;

                if (additionalMove > 0)
                {
                    result.Add(new PathItem(path[i].Visit,
                        pathItem.TimeStart.AddMinutes(-additionalMove), technician));
                }
                else
                    result.Add((PathItem)path[i].Clone());
            }

            result.Reverse();
            return result;
        }

        #endregion

        #region IsTechnicianLimitsExceeded

        public static bool IsTechnicianLimitsExceeded(List<PathItem> path, Technician technician)
        {
            if (path == null)
                return false;

            if (path.Count > technician.MaxVisitsCount)
                return true;

            int nonExclusiveVisitsCount = 0;
            foreach (var pathItem in path)
            {
                if (!pathItem.Visit.ExclusiveTechnicianDefaultId.HasValue)
                    nonExclusiveVisitsCount++;
            }

            if (nonExclusiveVisitsCount > technician.MaxNonExclusiveVisitsCount)
                return true;
            return false;
        }

        #endregion
    }

    [DataContract]
    public class WorkingHoursExtensionResult : IComparable
    {
        #region Constructor

        public WorkingHoursExtensionResult(Technician technician)
        {
            m_technician = technician;
        }

        #endregion


        #region IsPossibleWithWorkingHours

        private bool m_isPossibleWithWorkingHours;
        [DataMember]
        public bool IsPossibleWithWorkingHours
        {
            get { return m_isPossibleWithWorkingHours; }
            set { m_isPossibleWithWorkingHours = value; }
        }

        #endregion

        #region Technician

        private Technician m_technician;
        [DataMember]
        public Technician Technician
        {
            get { return m_technician; }
            set { m_technician = value; }
        }

        #endregion

        #region OldWorkInterval

        private TechnicianWorkTime m_oldWorkInterval;
        [DataMember]
        public TechnicianWorkTime OldWorkInterval
        {
            get { return m_oldWorkInterval; }
            set { m_oldWorkInterval = value; }
        }

        #endregion

        #region NewWorkInterval

        private TechnicianWorkTime m_newWorkInterval;
        [DataMember]
        public TechnicianWorkTime NewWorkInterval
        {
            get { return m_newWorkInterval; }
            set { m_newWorkInterval = value; }
        }

        #endregion

        #region CostChange

        private double m_costChange;
        [DataMember]
        public double CostChange
        {
            get { return m_costChange; }
            set { m_costChange = value; }
        }

        #endregion

        #region SecondaryArea

        private bool m_secondaryArea;
        [DataMember]
        public bool SecondaryArea
        {
            get { return m_secondaryArea; }
            set { m_secondaryArea = value; }
        }

        #endregion

        #region VisitInExtendedHours

        private Visit m_visitInExtendedHours;
        [DataMember]
        public Visit VisitInExtendedHours
        {
            get { return m_visitInExtendedHours; }
            set { m_visitInExtendedHours = value; }
        }

        #endregion

        #region ExtensionInterval

        public TimeInterval ExtensionInterval
        {
            get
            {
                if (m_newWorkInterval.TimeStart != m_oldWorkInterval.TimeStart)
                    return new TimeInterval(m_newWorkInterval.TimeStart, m_oldWorkInterval.TimeStart);

                if (m_newWorkInterval.TimeEnd != m_oldWorkInterval.TimeEnd)
                    return new TimeInterval(m_oldWorkInterval.TimeEnd, m_newWorkInterval.TimeEnd);

                throw new Exception("Working hours were not extended");
            }
        }

        #endregion



        #region SecondaryAreaText

        public string SecondaryAreaText
        {
            get
            {
                if (m_secondaryArea)
                    return "Yes";
                return string.Empty;
            }
        }

        #endregion  

        #region TechnicianName

        public string TechnicianName
        {
            get { return m_technician.Name; }
        }

        #endregion

        #region OldWorkingHoursText

        public string OldWorkingHoursText
        {
            get { return OldWorkInterval.GetInterval().ToString(); }
        }

        #endregion

        #region NewWorkingHoursText

        public string NewWorkingHoursText
        {
            get { return NewWorkInterval.GetInterval().ToString(); }
        }

        #endregion

        #region VisitShortInfoText

        public string VisitShortInfoText
        {
            get
            {
                return string.Format("{0}, {1}",
                     VisitInExtendedHours.TicketNumber,
                     VisitInExtendedHours.Cost.ToString("C"));
            }
        }

        #endregion

        #region DurationDelta

        public double DurationDelta
        {
            get
            {
                return NewWorkInterval.GetInterval().DurationMin
                    - OldWorkInterval.GetInterval().DurationMin;
            }
        }

        public string DurationDeltaText
        {
            get
            {
                TimeSpan span = new TimeSpan(0, (int)DurationDelta, 0);

                string result = string.Empty;
                if (span.Hours > 0)
                    result = span.Hours + " h";
                if (span.Minutes > 0)
                {
                    if (result == string.Empty)
                        result = span.Minutes + " min";
                    else
                        result += " " + span.Minutes + " min";
                }

                return result;
            }
        }

        #endregion

        #region IsBetterThan

        public int CompareTo(object obj)
        {
            WorkingHoursExtensionResult otherResult = (WorkingHoursExtensionResult)obj;

            if (DurationDelta != otherResult.DurationDelta)
                return -DurationDelta.CompareTo(otherResult.DurationDelta);

            if (SecondaryArea != otherResult.SecondaryArea)
                return -SecondaryArea.CompareTo(otherResult.SecondaryArea);

            if (CostChange != otherResult.CostChange)
                return -CostChange.CompareTo(otherResult.CostChange);

            return 0;
        }

        public bool IsBetterThan(WorkingHoursExtensionResult result)
        {
            if (CompareTo(result) > 0)
                return true;
            return false;
        }

        #endregion
    }
}
