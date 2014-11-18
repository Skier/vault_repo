using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using hobson.controls;

namespace dalworth.preview
{
    public partial class EndDayDone : BaseForm
    {
        public EndDayDone()
        {
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBox.Show("Dispatch notified");
            base.OnClosing(e);
        }

        private void OnDoneClick(object sender, EventArgs e)
        {
            Close();
        }
    }
}