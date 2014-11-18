using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace QuickBooksAgent.Windows.UI.ManageTime.Menu
{
    public partial class ManageTimeMenuView : BaseControl
    {
        internal MenuItem m_menuSingleTimeSheet = new MenuItem();
        internal MenuItem m_menuWeeklyTimeSheet = new MenuItem();
        
        
        public ManageTimeMenuView()
        {
            InitializeComponent();

            MenuItems.Add(m_menuSingleTimeSheet);
            MenuItems.Add(m_menuWeeklyTimeSheet);
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);
            Text = "Manage Time - Q-Agent";
            
            m_menuSingleTimeSheet.Text = "Single Time Entry";
            m_menuWeeklyTimeSheet.Text = "Weekly Time Sheet";
        }

        protected override void OnInit()
        {
            base.OnInit();

            Joystick.Add(m_mbSingleTimeSheet, m_mbWeeklyTimeSheet, m_mbWeeklyTimeSheet, m_mbSingleTimeSheet, m_mbSingleTimeSheet);
            Joystick.Add(m_mbWeeklyTimeSheet, m_mbSingleTimeSheet, m_mbSingleTimeSheet, m_mbWeeklyTimeSheet, m_mbWeeklyTimeSheet);
        }
    }
}
