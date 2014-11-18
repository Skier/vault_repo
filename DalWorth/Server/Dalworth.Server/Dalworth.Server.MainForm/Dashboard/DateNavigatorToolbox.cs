using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;

namespace Dalworth.Server.MainForm.Dashboard
{
    public partial class DateNavigatorToolbox : Form
    {
        public DateNavigatorToolbox()
        {
            InitializeComponent();
        }

        public DateNavigator DateNavigator
        {
            get
            {
                return m_dateNavigator;
            }
        }
    }
}