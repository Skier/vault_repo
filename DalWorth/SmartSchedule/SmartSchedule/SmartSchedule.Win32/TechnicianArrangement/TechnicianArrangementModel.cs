using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.TechnicianArrangement
{
    public class TechnicianArrangementModel : IModel
    {
        #region IsDefaultSettingsMode

        private bool m_isDefaultSettingsMode;
        public bool IsDefaultSettingsMode
        {
            get { return m_isDefaultSettingsMode; }
            set { m_isDefaultSettingsMode = value; }
        }

        #endregion       

        #region OrderedTechnicians

        private BindingList<TechnicianArrange> m_orderedTechnicians;
        public BindingList<TechnicianArrange> OrderedTechnicians
        {
            get { return m_orderedTechnicians; }
        }

        #endregion

        #region UnorderedTechnicians

        private BindingList<TechnicianArrange> m_unorderedTechnicians;
        public BindingList<TechnicianArrange> UnorderedTechnicians
        {
            get { return m_unorderedTechnicians; }
        }

        #endregion

        #region Init

        public void Init()
        {   
            List<Technician> technicians = WcfClient.WcfClient.Instance.GetTechnicians(
                WcfClient.WcfClient.ClientDate, m_isDefaultSettingsMode, true);

            m_orderedTechnicians = new BindingList<TechnicianArrange>(new List<TechnicianArrange>(
                from technician in technicians
                select new TechnicianArrange(technician)));

            m_unorderedTechnicians = new BindingList<TechnicianArrange>();
        }

        #endregion

        #region Save

        public void Save()
        {
            WcfClient.WcfClient.Instance.SaveTechnicianArrangement(
                m_orderedTechnicians.Select(technicianArrange => technicianArrange.Technician).ToList(),                
                m_isDefaultSettingsMode);                
        }

        #endregion
    }

    public class TechnicianArrange
    {
        #region TechnicianArrange

        public TechnicianArrange(Technician technician)
        {
            m_technician = technician;
        }

        #endregion

        #region Technician

        private Technician m_technician;
        public Technician Technician
        {
            get { return m_technician; }
        }

        #endregion        

        #region Name

        public string Name
        {
            get { return m_technician.Name; }            
        }

        #endregion

        #region ImageIndex

        public int ImageIndex
        {
            get { return -1; }
        }

        #endregion

    }
}
