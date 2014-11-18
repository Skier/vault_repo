using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;
using SmartSchedule.Data;
using System.Linq;
using SmartSchedule.SDK;

namespace SmartSchedule.Domain
{
    public partial class Technician
    {
        public Technician(){ }

        private static Dictionary<DateTime, List<Technician>> m_contractors;

        #region DistributionCostTarget

        private decimal m_distributionCostTarget;
        public decimal DistributionCostTarget
        {
            get { return m_distributionCostTarget; }
            set { m_distributionCostTarget = value; }
        }

        #endregion        

        #region Caption

        public string Caption
        {
            get
            {
                return string.Format("{0} {1}/h", Name, HourlyRate.ToString("C"));
            }
        }

        #endregion

        #region Technicians

        //Keys - Date, DefaultId
        private static Dictionary<DateTime, Dictionary<int, Technician>> m_techniciansMap;
        private static Dictionary<DateTime, Dictionary<int, Technician>> TechniciansMap
        {
            get
            {
                if (m_techniciansMap == null)
                    LoadTechnicians();

                return m_techniciansMap;
            }
        }        


        private static Dictionary<int, Technician> m_technicians;        
        private static Dictionary<int, Technician> Technicians
        {
            get
            {
                if (m_technicians == null)
                    LoadTechnicians();                        

                return m_technicians;                
            }
            set { m_technicians = value; }
        }        
        
        private static Dictionary<int, TechnicianDefault> m_defaultTechnicians;
        private static Dictionary<int, TechnicianDefault> DefaultTechnicians
        {
            get
            {
                if (m_defaultTechnicians == null)
                    LoadTechnicians();

                return m_defaultTechnicians;                
            }
            set { m_defaultTechnicians = value; }
        }

        public static Technician GetTechnician(int id)
        {            
            if (Configuration.IsClientApplication)
                return GetClientCachedTechnician(id);

            return Technicians[id];
        }

        public static Technician GetTechnician(DateTime date, int defaultId)
        {
            if (Configuration.IsClientApplication)
            {
                foreach (Technician technician in m_clientTechnicianCache.Values)
                {
                    if (technician.ScheduleDate == date.Date 
                        && technician.TechnicianDefaultId == defaultId)
                    {
                        return technician;
                    }
                }

                return null;
            }

            if (!TechniciansMap.ContainsKey(date.Date))
                return null;

            if (!TechniciansMap[date.Date].ContainsKey(defaultId))
                return null;

            return TechniciansMap[date.Date][defaultId];
        }

        public static List<Technician> GetTechnicians(DateTime date)
        {
            return new List<Technician>(Technicians.Values.Where(
                technician => technician.ScheduleDate.Date == date.Date));
        }

        public static TechnicianDefault GetTechnicianDefault(int defaultId)
        {            
            if (Configuration.IsClientApplication)
                Debug.Fail("Client code shouldn't reach here");

            return DefaultTechnicians[defaultId];
        }

        public static List<TechnicianDefault> GetTechnicianDefaults()
        {
            return DefaultTechnicians.Values.ToList();
        }

        #endregion        

        #region ServmanIdTechnicianMap

        private static Dictionary<string, TechnicianDefault> m_servmanIdTechnicianMap;
        private static Dictionary<string, TechnicianDefault> ServmanIdTechnicianMap
        {
            get
            {                
                if (m_servmanIdTechnicianMap == null)
                    LoadTechnicians();

                return m_servmanIdTechnicianMap;
            }
        }

        public static TechnicianDefault GetTechnician(string servmanId)
        {
            if (!ServmanIdTechnicianMap.ContainsKey(servmanId))
                return null;
            return ServmanIdTechnicianMap[servmanId];
        }

        #endregion

        #region GetTechnicians

//        private static Dictionary<DateTime, Dictionary<int, List<Technician>>> m_companyTechnicians;
//        public static List<Technician> GetTechnicians(int companyId, DateTime date)
//        {
//            if (m_companyTechnicians == null)
//            {
//                m_companyTechnicians = new Dictionary<DateTime, Dictionary<int, List<Technician>>>();
//
//                foreach (Technician technician in Technicians.Values)
//                {                    
//                    if (!m_companyTechnicians.ContainsKey(technician.ScheduleDate.Date))
//                        m_companyTechnicians.Add(technician.ScheduleDate.Date, new Dictionary<int, List<Technician>>());
//
//                    if (!m_companyTechnicians[technician.ScheduleDate.Date].ContainsKey(technician.CompanyId))
//                        m_companyTechnicians[technician.ScheduleDate.Date].Add(technician.CompanyId, new List<Technician>());
//
//                    m_companyTechnicians[technician.ScheduleDate.Date][technician.CompanyId].Add(technician);
//                }                    
//            }
//
//            return new List<Technician>(m_companyTechnicians[date.Date][companyId]);
//        }

        #endregion

        #region LoadTechnicians

        public static void LoadTechnicians(List<Technician> technicians, 
            List<TechnicianDefault> defaultTechnicians)
        {
            LoadTechnicians(technicians, defaultTechnicians, false);
        }

        private static void LoadTechnicians()
        {
            LoadTechnicians(FindTechnicians(), TechnicianDefault.Find(), true);
        }
        
        private static void LoadTechnicians(List<Technician> technicians, 
            List<TechnicianDefault> defaultTechnicians, bool loadDetailsFromDb)
        {
            m_technicians = new Dictionary<int, Technician>();
            m_techniciansMap = new Dictionary<DateTime, Dictionary<int, Technician>>();
            m_defaultTechnicians = new Dictionary<int, TechnicianDefault>();
            m_servmanIdTechnicianMap = new Dictionary<string, TechnicianDefault>();
            m_zipPrimaryTechnicians = new Dictionary<DateTime, Dictionary<string, List<Technician>>>();
            m_zipSecondaryTechnicians = new Dictionary<DateTime, Dictionary<string, List<Technician>>>();
            m_contractors = new Dictionary<DateTime, List<Technician>>();
            //m_companyTechnicians = null;            

            foreach (Technician technician in technicians)
                AddTechnicianToServerCache(technician, loadDetailsFromDb);

            foreach (var defaultTechnician in defaultTechnicians)
            {
                m_defaultTechnicians.Add(defaultTechnician.ID, defaultTechnician);
                m_servmanIdTechnicianMap.Add(defaultTechnician.ServmanId, defaultTechnician);
            }
        }

        #endregion

        #region AddTechnicianToServerCache

        public static void AddTechnicianToServerCache(Technician technician, bool loadDetailsFromDb)
        {
            technician.LoadTechnicianDetails(loadDetailsFromDb);
            m_technicians.Add(technician.ID, technician);

            if (!m_techniciansMap.ContainsKey(technician.ScheduleDate))
                m_techniciansMap.Add(technician.ScheduleDate, new Dictionary<int, Technician>());

            m_techniciansMap[technician.ScheduleDate].Add(technician.TechnicianDefaultId, technician);

            if (technician.IsContractor)
            {
                if (!m_contractors.ContainsKey(technician.ScheduleDate))
                    m_contractors.Add(technician.ScheduleDate, new List<Technician>());

                m_contractors[technician.ScheduleDate].Add(technician);
            }            
        }

        #endregion

        #region RemoveTechnicianFromServerCache

        public static void RemoveTechnicianFromServerCache(Technician technician)
        {
            m_technicians.Remove(technician.ID);
            m_techniciansMap[technician.ScheduleDate].Remove(technician.TechnicianDefaultId);
            if (m_techniciansMap[technician.ScheduleDate].Count == 0)
                m_techniciansMap.Remove(technician.ScheduleDate);

            if (technician.IsContractor)
            {
                m_contractors[technician.ScheduleDate].Remove(technician);
                if (m_contractors[technician.ScheduleDate].Count == 0)
                    m_contractors.Remove(technician.ScheduleDate);
            }

            foreach (string zipCode in technician.PrimaryZipCodes)
            {
                m_zipPrimaryTechnicians[technician.ScheduleDate][zipCode].Remove(technician);
                if (m_zipPrimaryTechnicians[technician.ScheduleDate][zipCode].Count == 0)
                {
                    m_zipPrimaryTechnicians[technician.ScheduleDate].Remove(zipCode);
                    if (m_zipPrimaryTechnicians[technician.ScheduleDate].Count == 0)
                        m_zipPrimaryTechnicians.Remove(technician.ScheduleDate);
                }
            }
            
            foreach (string zipCode in technician.SecondaryZipCodes)
            {
                m_zipSecondaryTechnicians[technician.ScheduleDate][zipCode].Remove(technician);
                if (m_zipSecondaryTechnicians[technician.ScheduleDate][zipCode].Count == 0)
                {
                    m_zipSecondaryTechnicians[technician.ScheduleDate].Remove(zipCode);
                    if (m_zipSecondaryTechnicians[technician.ScheduleDate].Count == 0)
                        m_zipSecondaryTechnicians.Remove(technician.ScheduleDate);
                }
            }
        }

        #endregion

        #region RefreshTechnicianInServerCache

        public static void RefreshTechnicianInServerCache(TechnicianDefault technician)
        {
            m_defaultTechnicians[technician.ID] = technician;
            m_servmanIdTechnicianMap[technician.ServmanId] = technician;
        }

        #endregion

        #region LoadTechnicianDetailsFromDB

        public void LoadTechnicianDetails(bool fromDb)
        {
            if (m_zipPrimaryTechnicians.ContainsKey(ScheduleDate))
            {
                foreach (List<Technician> technicianList in m_zipPrimaryTechnicians[ScheduleDate].Values)
                    technicianList.Remove(this);                
            }

            if (m_zipSecondaryTechnicians.ContainsKey(ScheduleDate))
            {
                foreach (List<Technician> technicianList in m_zipSecondaryTechnicians[ScheduleDate].Values)
                    technicianList.Remove(this);                
            }

            if (!fromDb)
            {
                foreach (var zipCode in PrimaryZipCodes)
                    AddTechnicianToServerZipCache(zipCode, true);
                foreach (var zipCode in SecondaryZipCodes)
                    AddTechnicianToServerZipCache(zipCode, false);
                return;
            }

            List<TechnicianServiceDeny> deniedServices = TechnicianServiceDeny.FindByTechnician(this);
            m_deniedServicesMap = new Dictionary<int, TechnicianServiceDeny>();
            foreach (TechnicianServiceDeny serviceDeny in deniedServices)
                m_deniedServicesMap.Add(serviceDeny.ServiceId, serviceDeny);

            WorkingIntervals = TechnicianWorkTime.FindByTechnician(this);
            List<TechnicianZip> zips = TechnicianZip.FindByTechnician(ID);            

            PrimaryZipCodes = new List<string>();
            foreach (TechnicianZip zip in zips)
            {
                if (zip.IsPrimaryZip)
                {
                    PrimaryZipCodes.Add(zip.Zip);
                    AddTechnicianToServerZipCache(zip.Zip, true);
                }
            }

            SecondaryZipCodes = new List<string>();
            foreach (TechnicianZip zip in zips)
            {
                if (!zip.IsPrimaryZip)
                {
                    SecondaryZipCodes.Add(zip.Zip);
                    AddTechnicianToServerZipCache(zip.Zip, false);
                }
            }
        }

        private void AddTechnicianToServerZipCache(string zip, bool isPrimaryZip)
        {
            if (isPrimaryZip)
            {
                if (!m_zipPrimaryTechnicians.ContainsKey(ScheduleDate))
                    m_zipPrimaryTechnicians.Add(ScheduleDate, new Dictionary<string, List<Technician>>());

                if (!m_zipPrimaryTechnicians[ScheduleDate].ContainsKey(zip))
                    m_zipPrimaryTechnicians[ScheduleDate].Add(zip, new List<Technician>());

                m_zipPrimaryTechnicians[ScheduleDate][zip].Add(this);                
            }
            else
            {
                if (!m_zipSecondaryTechnicians.ContainsKey(ScheduleDate))
                    m_zipSecondaryTechnicians.Add(ScheduleDate, new Dictionary<string, List<Technician>>());

                if (!m_zipSecondaryTechnicians[ScheduleDate].ContainsKey(zip))
                    m_zipSecondaryTechnicians[ScheduleDate].Add(zip, new List<Technician>());

                m_zipSecondaryTechnicians[ScheduleDate][zip].Add(this);                
            }
        }

        #endregion

        #region Client Cache

        private static Dictionary<int, Technician> m_clientTechnicianCache 
            = new Dictionary<int, Technician>();

        public static void CacheClient(Technician technician)
        {
            if (!m_clientTechnicianCache.ContainsKey(technician.ID))
                m_clientTechnicianCache.Add(technician.ID, technician);
            else
                m_clientTechnicianCache[technician.ID] = technician;            
        }

        public static void CacheClient(IEnumerable<Technician> technicians)
        {
            foreach (var technician in technicians)
                CacheClient(technician);
        }

        private static Technician GetClientCachedTechnician(int id)
        {
            return m_clientTechnicianCache[id];
        }

        #endregion        


        #region FindScheduleDates

        private const string SqlFindScheduleDates =
            @"select distinct Date(TimeStart) from Visit where Date(TimeStart) >= ?MinDateVisit
                union
              SELECT distinct(ScheduleDate) FROM technician where ScheduleDate >= ?MinDateTech";

        public static List<DateTime> FindScheduleDates()
        {
            List<DateTime> result = new List<DateTime>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindScheduleDates))
            {
                if (Configuration.IsRealtimeMode)
                {
                    Database.PutParameter(dbCommand, "?MinDateVisit", DateTime.Now.Date);
                    Database.PutParameter(dbCommand, "?MinDateTech", DateTime.Now.Date);
                }
                else
                {
                    Database.PutParameter(dbCommand, "?MinDateVisit", DateTime.MinValue);
                    Database.PutParameter(dbCommand, "?MinDateTech", DateTime.MinValue);                    
                }

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(dataReader.GetDateTime(0));
                }
            }

            return result;
        }

        #endregion

        #region FindTechnicians

        private const string SqlFindTechnicians =
            @"select * from technician where ScheduleDate >= ?ScheduleDate";

        public static List<Technician> FindTechnicians()
        {
            List<Technician> result = new List<Technician>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindTechnicians))
            {
                if (Configuration.IsRealtimeMode)
                    Database.PutParameter(dbCommand, "?ScheduleDate", DateTime.Now.Date);
                else
                    Database.PutParameter(dbCommand, "?ScheduleDate", DateTime.MinValue);                

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        #endregion


        #region DeniedServices

        private Dictionary<int, TechnicianServiceDeny> m_deniedServicesMap;
        [DataMember]
        public Dictionary<int, TechnicianServiceDeny> DeniedServicesMap
        {
            get { return m_deniedServicesMap; }
            set { m_deniedServicesMap = value; }
        }

        #endregion

        #region CanService

        public bool CanService(int serviceId, bool isExclusiveVisit)
        {
            if (m_deniedServicesMap.ContainsKey(serviceId))
            {
                if (m_deniedServicesMap[serviceId].IsForNonExclusive && isExclusiveVisit)
                    return true;
                return false;
            }

            return true;
        }

        #endregion

        #region GetPrimaryTechniciansByZip

        private static Dictionary<DateTime, Dictionary<string, List<Technician>>> m_zipPrimaryTechnicians;
        public static List<Technician> GetPrimaryTechniciansByZip(string zip, DateTime date)
        {
            if (!m_zipPrimaryTechnicians.ContainsKey(date.Date))
                return new List<Technician>();

            if (!m_zipPrimaryTechnicians[date.Date].ContainsKey(zip))
                return new List<Technician>();

            return new List<Technician>(m_zipPrimaryTechnicians[date.Date][zip]);
        }

        #endregion

        #region GetSecondaryTechniciansByZip

        private static Dictionary<DateTime, Dictionary<string, List<Technician>>> m_zipSecondaryTechnicians;
        public static List<Technician> GetSecondaryTechniciansByZip(string zip, DateTime date)
        {
            if (!m_zipSecondaryTechnicians.ContainsKey(date.Date))
                return new List<Technician>();

            List<Technician> result;
            if (!m_zipSecondaryTechnicians[date.Date].ContainsKey(zip))
                result = new List<Technician>();
            else
                result = new List<Technician>(m_zipSecondaryTechnicians[date.Date][zip]);

            if (m_contractors.ContainsKey(date.Date))
                result.AddRange(m_contractors[date.Date]);
            return result;
        }

        #endregion

        #region PrimaryZipCodes

        private List<string> m_primaryZipCodes;
        [DataMember]
        public List<string> PrimaryZipCodes
        {
            get { return m_primaryZipCodes; }
            set { m_primaryZipCodes = value; }
        }

        #endregion

        #region SecondaryZipCodes

        private List<string> m_secondaryZipCodes;
        [DataMember]
        public List<string> SecondaryZipCodes
        {
            get { return m_secondaryZipCodes; }
            set { m_secondaryZipCodes = value; }
        }

        #endregion

        #region WorkingIntervals

        private List<TechnicianWorkTime> m_workingIntervals;
        [DataMember]
        public List<TechnicianWorkTime> WorkingIntervals
        {
            get { return m_workingIntervals; }
            set { m_workingIntervals = value; }
        }

        #endregion

        #region Distance

        public double Distance(Visit visit)
        {
            if (visit.IsBlockout)
                return 0;
            return Utils.Distance(DepotLatitude, DepotLongitude, visit.Latitude, visit.Longitude);
        }

        #endregion

        #region GetWorkDayDurationHours

        public double GetWorkDayDurationHours()
        {
            double result = 0;
            foreach (TechnicianWorkTime workingInterval in WorkingIntervals)
                result += workingInterval.TimeEnd.Subtract(workingInterval.TimeStart).TotalHours;

            return result;
        }

        #endregion

        #region GetToolTipText

        public string GetToolTipText(double cost)
        {
            string result = string.Empty;

            result += string.Format("Max Jobs/NCO: {0}/{1}", MaxVisitsCount, MaxNonExclusiveVisitsCount);
            result += "\nTotal Cost: " + cost.ToString("0.00");
            result += "\n\n";

            if (m_deniedServicesMap.Count > 0)
            {
                result += "Cannot do:\n";

                foreach (TechnicianServiceDeny service in m_deniedServicesMap.Values)
                {
                    if (service.IsForNonExclusive)
                        result += service.ServiceName + "(nEx)\n";
                    else
                        result += service.ServiceName + "\n";
                }

                result = result.Substring(0, result.Length - 1);
            }
            else
                result += "Can do all the services";

            return result;
        }

        #endregion

        #region IsChangedReschedulePossible

        public static bool IsChangedReschedulePossible(Technician oldTechnician, Technician newTechnician)
        {
            if (oldTechnician.DeniedServicesMap.Count != newTechnician.DeniedServicesMap.Count)
                return true;            

            if (oldTechnician.DriveTimeMinutes != newTechnician.DriveTimeMinutes
                || oldTechnician.HourlyRate != newTechnician.HourlyRate
                || oldTechnician.HourlyRate150to300 != newTechnician.HourlyRate150to300
                || oldTechnician.HourlyRateMore300 != newTechnician.HourlyRateMore300
                || newTechnician.WorkingIntervals[0].TimeStart != oldTechnician.WorkingIntervals[0].TimeStart
                || newTechnician.WorkingIntervals[0].TimeEnd != oldTechnician.WorkingIntervals[0].TimeEnd
                || newTechnician.MaxNonExclusiveVisitsCount != oldTechnician.MaxNonExclusiveVisitsCount
                || newTechnician.MaxVisitsCount != oldTechnician.MaxVisitsCount)
            {
                return true;
            }

            return false;
        }

        #endregion

    }
}
