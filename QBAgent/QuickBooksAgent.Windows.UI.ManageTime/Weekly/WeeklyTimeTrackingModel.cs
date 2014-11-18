using System;
using System.Collections.Generic;
using System.Diagnostics;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.ManageTime.Weekly
{
    public enum PersonTypeEnum { Vendor, Employee }    

    #region TimeTrackingTableElement

    public class TimeTrackingTableElement
    {
        #region Constructor

        public TimeTrackingTableElement(TimeTracking timeTracking)
        {
            m_timeTracking = timeTracking;
            
            if (timeTracking.Customer != null)
                m_name = timeTracking.Customer.FullName ?? string.Empty;
            else
                m_name = string.Empty;            

            m_duration = ParseQBDuration(timeTracking.Duration);
            
            try
            {
                m_cost = (timeTracking.Rate ?? 0) * (decimal) m_duration.TotalHours;
            }
            catch (OverflowException)
            {
                m_cost = decimal.Zero;
            }
        }

        public TimeTrackingTableElement(DateTime dayOfWeekDate)
        {
            m_dayOfWeekDate = dayOfWeekDate;
            m_name = m_dayOfWeekDate.ToString("ddd, dd MMM");
            m_duration = TimeSpan.Zero;            
        }

        #endregion

        #region ParseQBDuration

        public static TimeSpan ParseQBDuration(string qbDuration)
        {
            int hIndex = qbDuration.IndexOf("H");
            int mIndex = qbDuration.IndexOf("M");

            string hours = qbDuration.Substring(2, hIndex - 2);
            string minutes = qbDuration.Substring(hIndex + 1, mIndex - (hIndex + 1));

            return new TimeSpan(int.Parse(hours), int.Parse(minutes), 0);
        }

        #endregion

        #region TimeTracking

        private TimeTracking m_timeTracking;
        public TimeTracking TimeTracking
        {
            get { return m_timeTracking; }
        }

        #endregion

        #region Name

        private string m_name;
        public string Name
        {
            get { return m_name; }
        }

        #endregion

        #region DayOfWeekDate

        private DateTime m_dayOfWeekDate;
        public DateTime DayOfWeekDate
        {
            get { return m_dayOfWeekDate; }
        }

        #endregion

        #region Status

        public string Status
        {
            get
            {
                if (m_timeTracking != null)
                {
                    if (m_timeTracking.EntityState == EntityState.Created)
                        return "C";
                    else if (m_timeTracking.EntityState == EntityState.Synchronized)
                        return "S";
                    else if (m_timeTracking.EntityState == EntityState.Deleted)
                        return "D";
                    Debug.Assert(false, "TimeTracking should never have modified status");
                }

                return string.Empty;
            }
        }

        #endregion

        #region Duration

        private TimeSpan m_duration;
        public TimeSpan Duration
        {
            get { return m_duration; }
            set { m_duration = value; }
        }

        #endregion                

        #region Cost

        private decimal m_cost;
        public decimal Cost
        {
            get { return m_cost; }
            set { m_cost = value; }
        }

        #endregion

        #region IsDayOfWeek

        public bool IsDayOfWeek
        {
            get { return m_timeTracking == null; }
        }

        #endregion

        #region IsDeleteAllowed

        public bool IsDeleteAllowed
        {
            get
            {
                if (m_timeTracking != null && m_timeTracking.EntityState == EntityState.Created)
                    return true;

                return false;
            }
        }

        #endregion

        #region IsUndoAllowed

        public bool IsUndoAllowed
        {
            get
            {
                if (m_timeTracking != null && m_timeTracking.EntityState == EntityState.Deleted)
                    return true;

                return false;
            }
        }

        #endregion

        #region IsEditOrViewAllowed

        public bool IsEditOrViewAllowed
        {
            get
            {
                if (m_timeTracking != null)
                    return true;

                return false;
            }
        }

        #endregion

        #region IsEditAllowed

        public bool IsEditAllowed
        {
            get
            {
                if (IsEditOrViewAllowed && m_timeTracking.EntityState == EntityState.Created)
                    return true;

                return false;
            }
        }

        #endregion
    }

    #endregion
    
    public class WeeklyTimeTrackingModel : ITableModel, IModel
    {
        #region Fields

        #region TableElements

        private List<TimeTrackingTableElement> m_tableElements = new List<TimeTrackingTableElement>();

        #endregion

        private TimeSpan m_totalDuration = TimeSpan.Zero;
        private decimal m_totalCost;

        #region Employees

        private List<Employee> m_employees;
        public List<Employee> Employees
        {
            get { return m_employees; }
        }

        #endregion

        #region Vendors

        private List<Vendor> m_vendors;
        public List<Vendor> Vendors
        {
            get { return m_vendors; }
        }

        #endregion

        #endregion

        #region UpdateWeeklyTimeSheet

        public void UpdateWeeklyTimeSheet(Week week, int qbEntityId)
        {
            Dictionary<DayOfWeek, List<TimeTracking>> m_weeklyTimeTrackings 
                = new Dictionary<DayOfWeek, List<TimeTracking>>();

            m_weeklyTimeTrackings[DayOfWeek.Sunday] = new List<TimeTracking>();
            m_weeklyTimeTrackings[DayOfWeek.Monday] = new List<TimeTracking>();
            m_weeklyTimeTrackings[DayOfWeek.Tuesday] = new List<TimeTracking>();
            m_weeklyTimeTrackings[DayOfWeek.Wednesday] = new List<TimeTracking>();
            m_weeklyTimeTrackings[DayOfWeek.Thursday] = new List<TimeTracking>();
            m_weeklyTimeTrackings[DayOfWeek.Friday] = new List<TimeTracking>();
            m_weeklyTimeTrackings[DayOfWeek.Saturday] = new List<TimeTracking>();

            List<TimeTracking> timeTrackings 
                = TimeTracking.FindBy(
                    new DateTime(week.StartDay.Year, week.StartDay.Month, week.StartDay.Day, 0, 0, 0, 0),
                    new DateTime(week.EndDay.Year, week.EndDay.Month, week.EndDay.Day, 23, 59, 59, 999),
                    qbEntityId);

            foreach (TimeTracking tracking in timeTrackings)
                m_weeklyTimeTrackings[tracking.TxnDate.Value.DayOfWeek].Add(tracking);
            
            
            //Init flat structure            
            m_tableElements.Clear();
            m_totalDuration = TimeSpan.Zero;
            m_totalCost = 0;
            
            foreach (KeyValuePair<DayOfWeek, List<TimeTracking>> dayTrackingPair in m_weeklyTimeTrackings)
            {
                int currentDayOfWeekIndex;
                TimeSpan currentDayOfWeekDuration = TimeSpan.Zero;
                decimal currentDayOfWeekCost = 0;
                
                DateTime currentDayOfWeekDate = week.StartDay.AddDays((int)dayTrackingPair.Key);                                        
                m_tableElements.Add(
                    new TimeTrackingTableElement(currentDayOfWeekDate));
                currentDayOfWeekIndex = m_tableElements.Count - 1;                                
                                    
                foreach (TimeTracking timeTracking in dayTrackingPair.Value)
                {                                        
                    m_tableElements.Add(
                        new TimeTrackingTableElement(timeTracking));

                    TimeSpan currentTimeTrackingDuration =
                        TimeTrackingTableElement.ParseQBDuration(timeTracking.Duration);
                    
                    currentDayOfWeekDuration 
                        = currentDayOfWeekDuration.Add(currentTimeTrackingDuration);
                    currentDayOfWeekCost += (timeTracking.Rate ?? 0)
                           * (decimal)currentTimeTrackingDuration.TotalHours;
                }

                m_tableElements[currentDayOfWeekIndex].Duration = currentDayOfWeekDuration;
                m_totalDuration = m_totalDuration.Add(currentDayOfWeekDuration);
                m_tableElements[currentDayOfWeekIndex].Cost = currentDayOfWeekCost;
                m_totalCost += currentDayOfWeekCost;
            }            

            if (m_tableElements.Count <= 1)
                m_tableElements.Clear();
            
            if (Change != null)
                Change.Invoke();
        }

        #endregion        

        #region IModel Members

        public void Init()
        {            
            m_employees = Employee.Find();
            m_vendors = Vendor.Find();            
        }

        #endregion

        #region ITableModel Members

        public int GetRowCount()
        {
            return m_tableElements.Count;
        }

        public int GetColumnCount()
        {
            return 4;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 1)
                return "Total";
            else if (columnIndex == 2)
                return ConvertTimeSpan(m_totalDuration, true);
            else if (columnIndex == 3)
                return ConvertCost(m_totalCost, true);

            return string.Empty;
        }

        public Type GetColumnClass(int columnIndex)
        {
            return String.Empty.GetType();
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {                        
            if (columnIndex == 0)
                return m_tableElements[rowIndex].Status;
            else if (columnIndex == 1)
                return m_tableElements[rowIndex].Name;
            else if (columnIndex == 3)
                return ConvertCost(m_tableElements[rowIndex].Cost, true);
            else
                return ConvertTimeSpan(m_tableElements[rowIndex].Duration, 
                    m_tableElements[rowIndex].IsDayOfWeek);                
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {
            throw new Exception("The method or operation is not supported.");
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_tableElements[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion

        #region ConvertTimeSpan

        private string ConvertTimeSpan(TimeSpan timeSpan, bool allowEmptyResult)
        {
            if (allowEmptyResult && timeSpan.Days == 0 
                && timeSpan.Hours == 0 && timeSpan.Minutes == 0)
                return string.Empty;            
            
            return timeSpan.Days * 24 + timeSpan.Hours + ":" + timeSpan.Minutes.ToString("00");
        }

        #endregion        

        #region ConvertCost

        private string ConvertCost(decimal cost, bool allowEmptyResult)
        {
            if (allowEmptyResult && cost == decimal.Zero)
                return string.Empty;

            return cost.ToString("$0.00");
        }

        #endregion
    }
}
