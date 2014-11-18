using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.RouteAnalyze
{
    public class RouteAnalyzeModel : IModel
    {
        #region TechnicianId

        private int m_technicianId;
        public int TechnicianId
        {
            get { return m_technicianId; }
            set { m_technicianId = value; }
        }

        #endregion

        #region CurrentDrive

        private double m_currentDrive;
        public double CurrentDrive
        {
            get { return m_currentDrive; }
            set { m_currentDrive = value; }
        }

        #endregion

        #region OptimizeOptions

        private BindingList<DriveOptimizeOption> m_optimizeOptions;
        public BindingList<DriveOptimizeOption> OptimizeOptions
        {
            get { return m_optimizeOptions; }
        }

        #endregion

        #region Init

        public void Init()
        {
//            m_optimizeOptions = new BindingList<DriveOptimizeOption>(
//                WcfClient.WcfClient.Instance.GetVisitsToRemoveToOptimizeDrive(m_technicianId));
        }

        #endregion    
    
        
    }
}
