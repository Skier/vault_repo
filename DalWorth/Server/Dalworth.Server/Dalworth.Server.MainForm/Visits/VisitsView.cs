using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraScheduler.Native;
using DevExpress.XtraScheduler.UI;

namespace Dalworth.Server.MainForm.Visits
{
    public partial class VisitsView : BaseControl
    {
        internal RepositoryItemDuration m_gridVisitsCmbDuration;        

        public VisitsView()
        {
            InitializeComponent();
            m_gridVisitsCmbDuration = new RepositoryItemDuration();                        
        }

        protected override void ApplyUIResources(CultureInfo cultureInfo)
        {
            base.ApplyUIResources(cultureInfo);

            Text = "Dalworth - Visits";
        }
    }
}
