using System;
using System.Collections.Generic;
using System.Data;
using QuickBooksAgent.Data;

namespace QuickBooksAgent.Domain
{
    #region Week

    public class Week
    {
        #region Week

        public Week(DateTime startDay, DateTime endDay)
        {
            m_startDay = startDay;
            m_endDay = endDay;
        }

        public Week(DateTime date)
        {
            Week currentWeek = GetWeekFromDate(date);
            m_startDay = currentWeek.StartDay;
            m_endDay = currentWeek.EndDay;
        }

        #endregion

        #region StartDay

        private DateTime m_startDay;
        public DateTime StartDay
        {
            get { return m_startDay; }
            set { m_startDay = value; }
        }

        #endregion

        #region EndDay

        private DateTime m_endDay;
        public DateTime EndDay
        {
            get { return m_endDay; }
            set { m_endDay = value; }
        }

        #endregion

        #region AddWeeks

        public Week AddWeeks(int weeksCount)
        {
            return new Week(m_startDay.AddDays(weeksCount*7), m_endDay.AddDays(weeksCount*7));
        }

        #endregion

        #region SubstractWeeks

        public Week SubstractWeeks(int weeksCount)
        {                        
            return new Week(
                m_startDay.Subtract(new TimeSpan(weeksCount*7, 0, 0, 0)),
                m_endDay.Subtract(new TimeSpan(weeksCount * 7, 0, 0, 0)));
        }

        #endregion

        #region GetMonthName

        private string GetMonthName(int monthIndex)
        {
            switch (monthIndex)
            {
                case 1: return "Jan";
                case 2: return "Feb";
                case 3: return "Mar";
                case 4: return "Apr";
                case 5: return "May";
                case 6: return "Jun";
                case 7: return "Jul";
                case 8: return "Aug";
                case 9: return "Sep";
                case 10: return "Oct";
                case 11: return "Nov";
                case 12: return "Dec";
                default:
                    throw new Exception("Incorrect month");
            }
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            return GetMonthName(m_startDay.Month) + " " + m_startDay.Day.ToString("00")
                   + "-" + m_endDay.Day.ToString("00") + ", " + m_startDay.Year;
        }

        #endregion

        #region GetHashCode

        public override int GetHashCode()
        {
            return m_startDay.GetHashCode() + 29 * m_endDay.GetHashCode();
        }

        #endregion

        #region Equals

        public override bool Equals(object obj)
        {
            Week week = obj as Week;
            if (week == null) return false;
            if (m_startDay != week.m_startDay) return false;
            if (m_endDay != week.m_endDay) return false;
            return true;
        }

        #endregion

        #region GetWeekFromDate

        private static Week GetWeekFromDate(DateTime date)
        {
            DateTime sundayDate
                = date.Subtract(new TimeSpan((int)date.DayOfWeek, 0, 0, 0));

            return new Week(sundayDate, sundayDate.AddDays(6));            
        }

        #endregion

        #region Current

        public static Week Current
        {
            get { return GetWeekFromDate(DateTime.Now); }
        }

        #endregion
    }

    #endregion
    
    public partial class TimeTracking : ICounterField
    {
        #region Constructor

        public TimeTracking()
        {
            this.EntityState = new EntityState();
            this.QBEntity = new QBEntity();
        }

        #endregion

        #region ICounterField Members

        public int CounterValue
        {
            get { return m_timeTrackingId; }
            set { m_timeTrackingId = value; }
        }

        public string CounterName
        {
            get { return "TimeTracking"; }
        }

        #endregion        
        
        #region FindBy Entity State

        private const string SqlFindByEntityState =
            @"SELECT TimeTrackingId, QuickBooksTxnId, EntityStateId, EditSequence, 
                TimeCreated, TimeModified, TxnNumber, TxnDate, QBEntityId, CustomerId, 
                ItemId, Rate, Duration, Notes, IsBillable, IsBilled        
            FROM TimeTracking
                WHERE EntityStateId = @EntityStateId";
        


        public static List<TimeTracking> FindBy(EntityState entityState)
        {
            List<TimeTracking> timeTrackingList = new List<TimeTracking>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEntityState))
            {
                Database.PutParameter(dbCommand, "@EntityStateId", entityState.EntityStateId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        timeTrackingList.Add(Load(dataReader));
                    }
                }
            }
            return timeTrackingList;
        }

        #endregion   
     
        #region FindBy Entity State and Customer

        private const string SqlFindByEntityStateAndCustomer =
            @"Select TimeTrackingId, QuickBooksTxnId, EntityStateId, EditSequence, 
                TimeCreated, TimeModified, TxnNumber, TxnDate, QBEntityId, CustomerId, 
                ItemId, Rate, Duration, Notes, IsBillable, IsBilled        
            From TimeTracking            
                Where EntityStateId != @EntityStateId And CustomerId = @CustomerId";
        [Obsolete("Need to search by invoice too")]
        public static List<TimeTracking> FindByCustomer(int customerId)
        {
            List<TimeTracking> timeTrackingList = new List<TimeTracking>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByEntityStateAndCustomer))
            {
                Database.PutParameter(dbCommand, "@EntityStateId", EntityState.Created.EntityStateId);
                Database.PutParameter(dbCommand, "@CustomerId", customerId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        timeTrackingList.Add(Load(dataReader));
                    }
                }
            }
            return timeTrackingList;
        }

        #endregion 
        
        #region FindBy QuickBooksTxnId

        private const string SqlFindByQuickBooksTxnId =
            @"SELECT TimeTrackingId, QuickBooksTxnId, EntityStateId, EditSequence, 
                TimeCreated, TimeModified, TxnNumber, TxnDate, QBEntityId, CustomerId, 
                ItemId, Rate, Duration, Notes, IsBillable, IsBilled 
            FROM TimeTracking
                WHERE QuickBooksTxnId = @QuickBooksTxnId";

        public static TimeTracking FindBy(int quickBooksTxnId)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByQuickBooksTxnId))
            {
                Database.PutParameter(dbCommand, "@QuickBooksTxnId", quickBooksTxnId.ToString());

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);

                    throw new DataNotFoundException("TimeTracking with TxnID "
                        + quickBooksTxnId.ToString() + " not found");
                }
            }
        }

        #endregion    
        
        #region Find By Date Range

        private const string SqlFindByDateRange =
            @"SELECT TimeTrackingId, QuickBooksTxnId, TimeTracking.EntityStateId, TimeTracking.EditSequence, 
                TimeCreated, TimeModified, TxnNumber, TxnDate, QBEntityId, TimeTracking.CustomerId, 
                ItemId, Rate, Duration, Notes, IsBillable, IsBilled,
				Customer.FullName
			FROM TimeTracking
			LEFT OUTER JOIN Customer on TimeTracking.CustomerId = Customer.CustomerId
                WHERE TxnDate >= @DateStart and TxnDate <= @DateEnd and QBEntityId = @QBEntityId
                ORDER BY TxnDate";

        public static List<TimeTracking> FindBy(DateTime startDate, DateTime endDate, int qbEntityId)
        {
            List<TimeTracking> timeTrackingList = new List<TimeTracking>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByDateRange))
            {
                Database.PutParameter(dbCommand, "@DateStart", startDate);
                Database.PutParameter(dbCommand, "@DateEnd", endDate);
                Database.PutParameter(dbCommand, "@QBEntityId", qbEntityId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        TimeTracking timeTracking = Load(dataReader);

                        if (!dataReader.IsDBNull(16))
                        {
                            if (timeTracking.Customer == null)                                
                                timeTracking.Customer = new Customer();
                            
                            timeTracking.Customer.FullName = dataReader.GetString(16);
                        }                            
                        
                        timeTrackingList.Add(timeTracking);                                                
                    }                        
                }
            }
            return timeTrackingList;
        }
        #endregion

        #region Find Used Weeks

        private const string SqlFindUsedWeeks =
            @"SELECT 
                distinct
                DATEADD(day,
                   8 - DATEPART(weekday, 'Jan 1, ' + convert(nvarchar, DATEPART(year, TxnDate))) 
                        + (DATEPART(week, TxnDate) - 2) * 7,
                   'Jan 1, ' + convert(nvarchar, DATEPART(year, TxnDate)))
            FROM TimeTracking";

        public static List<Week> FindUsedWeeks()
        {
            List<Week> timeTrackingWeeks = new List<Week>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindUsedWeeks))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        DateTime currentSunday = dataReader.GetDateTime(0);
                        timeTrackingWeeks.Add(new Week(currentSunday, currentSunday.AddDays(6)));
                    }
                }
            }
            return timeTrackingWeeks;
        }
        #endregion

        #region DeleteOlderThan

        private const string SqlDeleteOlderThan =
            @"delete from TimeTracking	
		        where EntityStateId = 1 and TimeModified < @Date";

        public static void DeleteOlderThan(DateTime date)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteOlderThan))
            {
                Database.PutParameter(dbCommand, "@Date", date);
                dbCommand.ExecuteNonQuery();
            }
        }

        #endregion            

        #region ConvertQBDuration

        public static TimeSpan ConvertQBDuration(string qbDuration)
        {
            int hIndex = qbDuration.IndexOf("H");
            int mIndex = qbDuration.IndexOf("M");

            string hours = qbDuration.Substring(2, hIndex - 2);
            string minutes = qbDuration.Substring(hIndex + 1, mIndex - (hIndex + 1));

            return new TimeSpan(int.Parse(hours), int.Parse(minutes), 0);
        }

        #endregion

        #region ToString

        public override string ToString()
        {
            string rv = string.Empty;

            if (TxnDate.HasValue)
                rv = TxnDate.Value.ToString("yyyy-MM-dd");

            TimeSpan duration = ConvertQBDuration(Duration);
            if (rv == String.Empty)
                rv += (int)duration.TotalHours + " hour(s)" + " " + duration.Minutes + " min";
            else                
                rv += ": " + (int)duration.TotalHours + " hour(s)" + " " + duration.Minutes + " min";
            
            return rv;
        }

        #endregion
    }
}
      