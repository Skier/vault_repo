using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace Dalworth.Windows.StartDay.WorkSummary
{
    public partial class WorkSummaryView : BaseControl
    {
        internal MenuItem m_menuCancel;
        internal MenuItem m_menuBack;

        public WorkSummaryView()
        {
            InitializeComponent();
            m_menuCancel = new MenuItem();
            m_menuBack = new MenuItem();
            MenuItemsLeft.Add(m_menuCancel);
            MenuItemsLeft.Add(m_menuBack);
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Work Summary";
            m_menuCancel.Text = "Cancel";
            m_menuBack.Text = "Back";
        }

    }
}
