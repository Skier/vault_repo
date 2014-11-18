using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using SmartSchedule.Data;
using SmartSchedule.Domain;
using SmartSchedule.Domain.WCF;
using SmartSchedule.Win32.Authenticate;
using SmartSchedule.Win32.Controls;
using SmartSchedule.Win32.HistoricalOrders;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;
using Point=SmartSchedule.Domain.Point;

namespace SmartSchedule.Win32.BlockoutCreate
{
    public class BlockoutCreateController : Controller<BlockoutCreateModel, BlockoutCreateView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion        

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data[0] != null)
                Model.Visit = (Visit)data[0] ;
            if (data.Length > 1)
            {
                Model.Technician = (Technician)data[1];
                Model.InitialTimeStart = (DateTime)data[2];
                Model.InitialTimeEnd = (DateTime)data[3];                 
            }
            base.OnModelInitialize(data);            
        }

        #endregion


        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_timeStart.Validating += OnTimeValidating;
            View.m_timeEnd.Validating += OnTimeValidating;

            View.m_btnOk.Click += OnOkClick;
            View.m_btnCancel.Click += OnCancelClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_timeStart.Time = Model.InitialTimeStart;
            View.m_timeEnd.Time = Model.InitialTimeEnd;
            if (Model.Visit != null)
            {
                View.Text = "Edit Blockout";
                View.m_txtNotes.Text = Model.Visit.Note;           
            }

            View.AlwaysAllowedControls.Add(View.m_btnCancel);
            View.MinRequiredUserRole = UserRoleEnum.Supervisor;            
        }

        #endregion

        #region OnTimeValidating

        private void OnTimeValidating(object sender, CancelEventArgs cancelEventArgs)
        {
            TimeEditEx control = (TimeEditEx)sender;
            TimeEditEx associatedControl = control == View.m_timeStart ? View.m_timeEnd : View.m_timeStart;

            if (control.Time.TimeOfDay.TotalHours == 0)
                View.m_errorProvider.SetError(control, "Time should be greater than 12AM");
            else if (control.Time.Minute % 15 != 0)
                View.m_errorProvider.SetError(control, "Time minutes should be multiple of 15");
            else if (control == View.m_timeStart && control.Time.TimeOfDay >= associatedControl.Time.TimeOfDay)
                View.m_errorProvider.SetError(control, "Time Start should be less than Time End");
            else if (control != View.m_timeStart && control.Time.TimeOfDay <= associatedControl.Time.TimeOfDay)
                View.m_errorProvider.SetError(control, "Time End should be greater than Time Start");
            else
            {
                View.m_errorProvider.SetError(control, string.Empty);

                if (View.m_errorProvider.GetError(associatedControl) != string.Empty)
                    OnTimeValidating(associatedControl, null);
            }
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors)
                return;
            User.ResetLogOutTimer();

            string error = Model.CreateEditBlockout(View.m_timeStart.Time, View.m_timeEnd.Time, View.m_txtNotes.Text);
            if (error != string.Empty)
            {
                XtraMessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            View.Destroy();            
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs eventArgs)
        {
            m_isCancelled = true;
            View.Destroy();            
        }

        #endregion

    }
}
