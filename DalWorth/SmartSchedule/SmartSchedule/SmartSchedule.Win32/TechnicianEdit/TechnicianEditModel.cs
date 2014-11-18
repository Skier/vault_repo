using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.TechnicianEdit
{
    public class TechnicianEditModel : IModel
    {
        private Dictionary<DateTime, TechnicianDetail> m_initialTechnicianSettings;

        #region IsDefaultSettingsMode

        private bool m_isDefaultSettingsMode;
        public bool IsDefaultSettingsMode
        {
            get { return m_isDefaultSettingsMode; }
            set { m_isDefaultSettingsMode = value; }
        }

        #endregion

        #region Technician

        private Technician m_technician;
        public Technician Technician
        {
            set { m_technician = value; }
        }

        #endregion

        #region BaseTechnicianDate

        public DateTime BaseTechnicianDate
        {
            get
            {
                if (m_isDefaultSettingsMode)
                    return DateTime.Now.Date;
                return m_technician.ScheduleDate.Date;
            }
        }

        #endregion


        #region TechnicianDefault

        private TechnicianDetail m_technicianDefault;
        public TechnicianDetail TechnicianDefault
        {
            get { return m_technicianDefault; }
        }

        #endregion        

        #region WorkTimePresets

        private List<TechnicianWorkTimeDefaultPreset> m_workTimePresets;
        public List<TechnicianWorkTimeDefaultPreset> WorkTimePresets
        {
            get { return m_workTimePresets; }
        }

        #endregion

        #region Technicians

        private Dictionary<DateTime, TechnicianDetail> m_technicians;
        public Dictionary<DateTime, TechnicianDetail> Technicians
        {
            get { return m_technicians; }
        }

        #endregion

        #region IsTechnicianCreate

        public bool IsTechnicianCreate
        {
            get { return m_technician.TechnicianDefaultId == 0; }
        }

        #endregion

        #region Init

        public void Init()
        {
            m_technicians = new Dictionary<DateTime, TechnicianDetail>();
            m_initialTechnicianSettings = new Dictionary<DateTime, TechnicianDetail>();

            if (m_technician == null)
            {
                m_technician = new Technician();
                m_technician.WorkingIntervals = new List<TechnicianWorkTime>();
                m_technician.WorkingIntervals.Add(new TechnicianWorkTime(0, DateTime.MinValue, DateTime.MinValue));
                m_technician.ScheduleDate = DateTime.Now.Date;
                m_technicianDefault = new TechnicianDetail(m_technician, null, null);
                m_technicianDefault.Technician.Name = string.Empty;
                m_technicianDefault.Technician.ServmanId = string.Empty;
                m_technicianDefault.Email = string.Empty;
                m_technicianDefault.Technician.DepotAddress = string.Empty;
                m_technicianDefault.PrimaryZipCodesText = string.Empty;
                m_technicianDefault.SecondaryZipCodesText = string.Empty;

                List<Service> services = WcfClient.WcfClient.Instance.GetServices();
                List<TechnicianService> technicianServices = new List<TechnicianService>();
                foreach (var service in services)
                    technicianServices.Add(new TechnicianService(service, TechnicianService.ServiceAllowanceEnum.Allowed));
                m_technicianDefault.Services = technicianServices;
                m_workTimePresets = new List<TechnicianWorkTimeDefaultPreset>();
                m_workTimePresets.Add(new TechnicianWorkTimeDefaultPreset(0, 1));
                m_workTimePresets.Add(new TechnicianWorkTimeDefaultPreset(0, 2));
                m_workTimePresets.Add(new TechnicianWorkTimeDefaultPreset(0, 3));

                m_technicians.Add(DateTime.Now.Date, m_technicianDefault);
                m_initialTechnicianSettings.Add(DateTime.Now.Date, m_technicianDefault);
                return;
            }

            m_technicianDefault = WcfClient.WcfClient.Instance.GetTechnicianDetails(
                m_technician.TechnicianDefaultId, true)[0];
            m_technicianDefault.Services = new BindingList<TechnicianService>(m_technicianDefault.Services.ToList());
            m_workTimePresets = m_technicianDefault.WorkingHoursPresets.ToList();

            List<TechnicianDetail> technicianDetails = WcfClient.WcfClient.Instance.GetTechnicianDetails(
                m_technician.TechnicianDefaultId, m_isDefaultSettingsMode);            
            
            foreach (var technicianDetail in technicianDetails)
            {
                technicianDetail.Services = new BindingList<TechnicianService>(technicianDetail.Services.ToList());
                m_technicians.Add(technicianDetail.Technician.ScheduleDate, technicianDetail);
                m_initialTechnicianSettings.Add(technicianDetail.Technician.ScheduleDate, technicianDetail);
            }                
        }

        #endregion

        #region GetAffectedTechnicians

        public List<TechnicianDetail> GetAffectedTechnicians()
        {
            List<TechnicianDetail> affectedTechnicians = new List<TechnicianDetail>();

            if (m_isDefaultSettingsMode)
            {
                affectedTechnicians.Add(m_technicians.Values.First());
                return affectedTechnicians;
            }

            foreach (TechnicianDetail technician in m_technicians.Values)
            {
                if (technician.IsDirty)
                    affectedTechnicians.Add(technician);
            }

            return affectedTechnicians;
        }

        #endregion

        #region Save

        public List<TechnicianDetailValidationError> Save()
        {
            List<DateTime> removedDates = new List<DateTime>();
            List<DateTime> currentDates = m_technicians.Keys.ToList();
            
            foreach (DateTime date in m_initialTechnicianSettings.Keys)
            {
                if (!currentDates.Contains(date))
                    removedDates.Add(date);                    
            }

            return WcfClient.WcfClient.Instance.SaveTechnicianDetail(GetAffectedTechnicians(),
                removedDates, m_technician.TechnicianDefaultId, m_isDefaultSettingsMode);
        }

        #endregion
    }
}
