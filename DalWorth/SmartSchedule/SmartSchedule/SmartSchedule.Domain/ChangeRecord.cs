using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SmartSchedule.Data;
using System.Linq;
using SmartSchedule.Domain.WCF;
using SmartSchedule.SDK;

namespace SmartSchedule.Domain
{
    public partial class ChangeRecord
    {
        private static Dictionary<DateTime, ChangeRecordInfo> m_cache;

        public ChangeRecord() {}        

        #region ReadLogText

        public static string ReadLogText(DateTime date)
        {
            StringBuilder result = new StringBuilder();
            List<ChangeRecord> changeRecords = FindLimited(date);

            foreach (ChangeRecord record in changeRecords)
                result = result.Append(record.ChangeText);

            return result.ToString();
        }

        #endregion

        #region Write

        public static ChangeRecord Write(string logRecord, DateTime dashboardDate)
        {
            return Write(logRecord, dashboardDate, false);
        }

        public static ChangeRecord Write(string logRecord, DateTime dashboardDate, bool isOptimizationChange)
        {
            ChangeRecord record = new ChangeRecord(GetNextId(dashboardDate), dashboardDate.Date, 
                DateTime.Now, logRecord, isOptimizationChange);
            Insert(record);

            if (isOptimizationChange)
            {
                m_cache[dashboardDate.Date].LastOptimizedId = record.ID;
                return record;
            }

            if (m_cache[dashboardDate.Date].LastOptimizedId == null
                || record.ID - m_cache[dashboardDate.Date].LastOptimizedId > Configuration.ChangesCountToRequestOptimization)
            {
                m_cache[dashboardDate.Date].LastOptimizedId = record.ID;
                WcfService.EnqueueOptimizationStatic(dashboardDate.Date);
            }

            return record;
        }

        private static int GetNextId(DateTime dashboardDate)
        {
            if (m_cache == null)
                InitCache();

            if (!m_cache.ContainsKey(dashboardDate))
            {
                m_cache.Add(dashboardDate, new ChangeRecordInfo(2));
                return 1;
            }

            return m_cache[dashboardDate].NextId++;
        }

        private static void InitCache()
        {
            m_cache = new Dictionary<DateTime, ChangeRecordInfo>();
            Dictionary<DateTime, int> maxIdMap = FindMaxIdMap();
            foreach (var mapItem in maxIdMap)
                m_cache.Add(mapItem.Key, new ChangeRecordInfo(mapItem.Value + 1));

            Dictionary<DateTime, int> lastOptimizedIdMap = FindLastOptimizedIdMap();
            foreach (var mapItem in lastOptimizedIdMap)
                m_cache[mapItem.Key].LastOptimizedId = mapItem.Value;
        }

        private const string SqlFindMaxIdMap =
            @"select DashboardDate, Max(ID) from ChangeRecord
                where DashboardDate >= ?DashboardDate
                group by DashboardDate";

        private static Dictionary<DateTime, int> FindMaxIdMap()
        {
            Dictionary<DateTime, int> result = new Dictionary<DateTime, int>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindMaxIdMap))
            {
                if (Configuration.IsRealtimeMode)
                    Database.PutParameter(dbCommand, "?DashboardDate", DateTime.Now.Date);
                else
                    Database.PutParameter(dbCommand, "?DashboardDate", DateTime.MinValue);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(dataReader.GetDateTime(0), dataReader.GetInt32(1));
                }
            }

            return result;
        }

        private const string SqlFindLastOptimizedIdMap =
            @"SELECT DashboardDate, Max(ID) FROM changerecord 
                where IsAllPreviousChangesOptimized = 1 and DashboardDate >= ?DashboardDate
                group by DashboardDate";

        private static Dictionary<DateTime, int> FindLastOptimizedIdMap()
        {
            Dictionary<DateTime, int> result = new Dictionary<DateTime, int>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindLastOptimizedIdMap))
            {
                if (Configuration.IsRealtimeMode)
                    Database.PutParameter(dbCommand, "?DashboardDate", DateTime.Now.Date);
                else
                    Database.PutParameter(dbCommand, "?DashboardDate", DateTime.MinValue);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(dataReader.GetDateTime(0), dataReader.GetInt32(1));
                }
            }

            return result;
        }

        #endregion

        #region FindLimited

        private const string SqlFindByDate =
            @"select * from (SELECT * FROM ChangeRecord where DashboardDate = ?Date order by ID desc
               {0}) temp order by ID";

        private static List<ChangeRecord> FindByDate(DateTime date, bool limitResults)
        {
            string query = SqlFindByDate;
            if (limitResults)
                query = string.Format(query, "limit 100");

            List<ChangeRecord> result = new List<ChangeRecord>();

            using (IDbCommand dbCommand = Database.PrepareCommand(query))
            {
                Database.PutParameter(dbCommand, "?Date", date.Date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        private static List<ChangeRecord> FindLimited(DateTime date)
        {
            return FindByDate(date, true);
        }

        public static List<ChangeRecord> Find(DateTime date)
        {
            return FindByDate(date, false);
        }

        #endregion        
    }

    public class ChangeRecordInfo
    {
        #region ChangeRecordInfo

        public ChangeRecordInfo(int nextId)
        {
            m_nextId = nextId;
        }

        #endregion

        #region NextId

        private int m_nextId;
        public int NextId
        {
            get { return m_nextId; }
            set { m_nextId = value; }
        }

        #endregion

        #region LastOptimizedId

        private int? m_lastOptimizedId;
        public int? LastOptimizedId
        {
            get { return m_lastOptimizedId; }
            set { m_lastOptimizedId = value; }
        }

        #endregion
    }
}
      