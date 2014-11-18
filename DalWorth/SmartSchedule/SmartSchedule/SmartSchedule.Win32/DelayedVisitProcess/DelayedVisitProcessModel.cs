using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.DelayedVisitProcess
{
    public class DelayedVisitProcessModel : IModel
    {
        #region DelayedVisit

        private Visit m_delayedVisit;
        public Visit DelayedVisit
        {
            get { return m_delayedVisit; }
            set { m_delayedVisit = value; }
        }

        #endregion


        #region TimeFrameChangeOptions

        private BindingList<VisitAddResult> m_timeFrameChangeOptions;
        public BindingList<VisitAddResult> TimeFrameChangeOptions
        {
            get { return m_timeFrameChangeOptions; }
        }

        #endregion

        #region WorkingHoursExtensionsMap

        private Dictionary<int, List<WorkingHoursExtensionResult>> m_workingHoursExtensionsMap;
        public Dictionary<int, List<WorkingHoursExtensionResult>> WorkingHoursExtensionsMap
        {
            get { return m_workingHoursExtensionsMap; }
        }

        #endregion

        private List<int> m_doNotCallVisitIds;
        public BucketProcessingOptions ProcessingOptions { get; set; }

        #region Init

        public void Init()
        {
            ProcessingOptions = WcfClient.WcfClient.Instance
                .GetBucketProcessingOptions(m_delayedVisit.ID);

            m_timeFrameChangeOptions = new BindingList<VisitAddResult>(
                ProcessingOptions.TimeFrameChangeOptions);

            m_workingHoursExtensionsMap = new Dictionary<int, List<WorkingHoursExtensionResult>>();
            foreach (WorkingHoursExtensionResult extension in ProcessingOptions.WorkingHoursExtensions)
            {
                Technician technician = extension.Technician;

                if (!m_workingHoursExtensionsMap.ContainsKey(technician.ID))
                    m_workingHoursExtensionsMap.Add(technician.ID, new List<WorkingHoursExtensionResult>());

                m_workingHoursExtensionsMap[technician.ID].Add(extension);
            }

            m_doNotCallVisitIds = new List<int>();
        }

        #endregion

        #region ProcessDelayedVisitTempExclusivity

        public void ProcessDelayedVisitTempExclusivity(int tempExclusiveTechnicianId)
        {
            WcfClient.WcfClient.Instance.ProcessDelayedVisitTempExclusivity(
                GetDelayedVisitSaveInfo(), tempExclusiveTechnicianId);
        }

        #endregion

        #region ProcessDelayedVisitIgnoreExclusivity

        public void ProcessDelayedVisitIgnoreExclusivity()
        {
            WcfClient.WcfClient.Instance.ProcessDelayedVisitIgnoreExclusivity(
                GetDelayedVisitSaveInfo());            
        }

        #endregion

        #region ProcessDelayedVisitChangeFrame

        public void ProcessDelayedVisitChangeFrame(VisitAddResult frameChangeOption)
        {
            WcfClient.WcfClient.Instance.ProcessDelayedVisitChangeFrame(
                GetDelayedVisitSaveInfo(), frameChangeOption);
        }

        #endregion

        #region ProcessDelayedVisitExtendWorkingHours

        public void ProcessDelayedVisitExtendWorkingHours(WorkingHoursExtensionResult workHourExtensionOption)
        {
            WcfClient.WcfClient.Instance.ProcessDelayedVisitExtendWorkingHours(
                GetDelayedVisitSaveInfo(), workHourExtensionOption);            
        }

        #endregion

        #region SaveDelayedVisit

        public void SaveDelayedVisit()
        {
            WcfClient.WcfClient.Instance.SaveDelayedVisit(GetDelayedVisitSaveInfo());
        }

        #endregion


        #region GetDelayedVisitSaveInfo

        private DelayedVisitSaveInfo GetDelayedVisitSaveInfo()
        {
            return new DelayedVisitSaveInfo(m_delayedVisit, m_doNotCallVisitIds);
        }

        #endregion
    }
}
