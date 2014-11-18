using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;

namespace SmartSchedule.Win32.Controls
{
    public class Scheduler : SchedulerControl
    {
        #region OnKeyDown

        protected override void OnKeyDown(KeyEventArgs e)
        {
            //Prevent switching to prev next date using Shift
            if (e.Shift && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right))
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            //Prevent switching to prev date
            if (e.KeyCode == Keys.Left && Storage.Resources.Count > 0
                && Storage.Resources[0] == SelectedResource)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            //Prevent switching to next date
            if (e.KeyCode == Keys.Right && Storage.Resources.Count > 0
                && Storage.Resources[Storage.Resources.Count - 1] == SelectedResource)
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
                return;
            }

            base.OnKeyDown(e);
        }

        #endregion
    }
}
