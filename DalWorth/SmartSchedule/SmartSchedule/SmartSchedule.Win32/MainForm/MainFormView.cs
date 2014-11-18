using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using SmartSchedule.Win32.Controls;
using SmartSchedule.Windows;

namespace SmartSchedule.Win32.MainForm
{
    public partial class MainFormView : BaseForm
    {
        internal DXMenuItem m_menuSetAsWorkingTime;
        internal DXMenuItem m_menuSetAsFreeTime;
        internal DXMenuItem m_menuBlockOutTime;

        internal DXMenuItem m_menuEditTechnicianDaySettings;
        internal DXMenuItem m_menuEditTechnicianDefaultSettings;
        internal DXMenuItem m_menuVisitPrint;
        internal DXMenuItem m_menuTechnicianPrint;
        internal DXMenuItem m_menuTechnicianShowRoute;
        //internal DXMenuItem m_menuTechnicianAnalyzeRoute;

        public MainFormView()
        {
            InitializeComponent();

            m_menuSetAsWorkingTime = new DXMenuItem("Mark as working time");
            m_menuSetAsFreeTime = new DXMenuItem("Mark as free time");
            m_menuBlockOutTime = new DXMenuItem("Block out time");
            m_menuEditTechnicianDaySettings = new DXMenuItem("Edit Day Settings");
            m_menuEditTechnicianDefaultSettings = new DXMenuItem("Edit Default Settings");
            m_menuVisitPrint = new DXMenuItem("Print");
            m_menuTechnicianPrint = new DXMenuItem("Print");
            m_menuTechnicianShowRoute = new DXMenuItem("Show Route");
            //m_menuTechnicianAnalyzeRoute = new DXMenuItem("Analyze Route");
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Smart Schedule";
        }
    }
}