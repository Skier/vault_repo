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
using SmartSchedule.Win32.Authenticate;
using SmartSchedule.Win32.Controls;
using SmartSchedule.Win32.HistoricalOrders;
using SmartSchedule.Win32.MainForm;
using SmartSchedule.Windows;
using Point=SmartSchedule.Domain.Point;

namespace SmartSchedule.Win32.DelayedVisitProcess
{
    public class DelayedVisitProcessController : Controller<DelayedVisitProcessModel, DelayedVisitProcessView>
    {
        private bool m_isShowingAllCustomers;
        private bool m_isShowingAllTechnicians;

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
            Model.DelayedVisit = (Visit) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnProcessIgnoreExclusivity.Click += OnProcessIgnoreExclusivityClick;
            View.m_btnProcessTempAssignment.Click += OnProcessTempAssignmentClick;
            View.m_btnProcessTimeFrameChange.Click += OnProcessTimeFrameChangeClick;
            View.m_btnProcessWorkingHoursExtension.Click += OnProcessWorkingHoursExtensionClick;

            View.m_btnShowCurrentTechnician.Click += OnShowCurrentTechnicianClick;
            View.m_ctlVisitInfo.DurationAmountChanged += OnVisitInfoDurationAmountChanged;

            View.m_btnSave.Click += OnSaveClick;
            View.m_btnCancel.Click += OnCancelClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {            
            View.m_btnProcessIgnoreExclusivity.Enabled
                = Model.ProcessingOptions.IsIgnoreExclusivityAllowed;

            View.m_cmbExclusiveTechnician.Properties.Items.Add(
                new ImageComboBoxItem(string.Empty, null));
            foreach (Technician technician in Model.ProcessingOptions.TempExclusivityTechnicians)
            {
                View.m_cmbExclusiveTechnician.Properties.Items.Add(
                    new ImageComboBoxItem(technician.Name, (object)technician.ID));
            }

            View.m_gridTimeFrame.DataSource = Model.TimeFrameChangeOptions;
            View.m_btnProcessTimeFrameChange.Enabled = Model.TimeFrameChangeOptions.Count > 0;
            m_isShowingAllCustomers = true;

            View.m_btnProcessWorkingHoursExtension.Enabled 
                = Model.ProcessingOptions.WorkingHoursExtensions.Count > 0;
            View.m_gridWorkHoursExtension.DataSource
                = new BindingList<WorkingHoursExtensionResult>(Model.ProcessingOptions.WorkingHoursExtensions);
            if (Model.WorkingHoursExtensionsMap.Count <= 1)
                View.m_btnShowCurrentTechnician.Enabled = false;
            m_isShowingAllTechnicians = true;

            View.m_ctlVisitInfo.Visit = Model.DelayedVisit;
            View.m_btnSave.Enabled = false;
            View.m_lblBucketReason.Text = Model.ProcessingOptions.BucketReason;

            View.AlwaysAllowedControls.Add(View.m_btnCancel);
            View.MinRequiredUserRole = UserRoleEnum.Dispatrcher;
        }

        #endregion

        #region OnShowCurrentTechnicianClick

        private void OnShowCurrentTechnicianClick(object sender, EventArgs args)
        {
            if (m_isShowingAllTechnicians)
            {
                //Show current technician only
                WorkingHoursExtensionResult extension = (WorkingHoursExtensionResult)View.m_gridWorkHoursExtensionView.GetRow(
                    View.m_gridWorkHoursExtensionView.FocusedRowHandle);

                if (extension == null)
                {
                    XtraMessageBox.Show("Please select working hours extension option", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                View.m_gridWorkHoursExtension.DataSource = Model.WorkingHoursExtensionsMap[extension.Technician.ID];
                View.m_btnShowCurrentTechnician.Text = "Show All Technicians";
            }
            else
            {
                //Show all technicians
                View.m_gridWorkHoursExtension.DataSource 
                    = new BindingList<WorkingHoursExtensionResult>(Model.ProcessingOptions.WorkingHoursExtensions);
                View.m_btnShowCurrentTechnician.Text = "Show Single Technician Only";
            }

            m_isShowingAllTechnicians = !m_isShowingAllTechnicians;            
        }

        #endregion

        #region OnVisitInfoDurationAmountChanged

        private void OnVisitInfoDurationAmountChanged()
        {
            View.m_btnSave.Enabled = true;
        }

        #endregion

        #region OnSaveClick

        private void OnSaveClick(object sender, EventArgs e)
        {
            User.ResetLogOutTimer();
            Model.SaveDelayedVisit();
        }

        #endregion

        #region OnProcess

        private void OnProcessIgnoreExclusivityClick(object sender, EventArgs e)
        {
            User.ResetLogOutTimer();
            Model.ProcessDelayedVisitIgnoreExclusivity();            
            View.Destroy();
        }
        
        private void OnProcessTempAssignmentClick(object sender, EventArgs e)
        {
            if (View.m_cmbExclusiveTechnician.SelectedIndex <= 0)
            {
                XtraMessageBox.Show("Please select Technicaian", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            User.ResetLogOutTimer();
            Model.ProcessDelayedVisitTempExclusivity(
                (int)((ImageComboBoxItem)View.m_cmbExclusiveTechnician.SelectedItem).Value);            
            View.Destroy();
        }

        private void OnProcessTimeFrameChangeClick(object sender, EventArgs e)
        {
            VisitAddResult option = (VisitAddResult)View.m_gridTimeFrameView.GetRow(
                View.m_gridTimeFrameView.FocusedRowHandle);

            if (option == null)
            {
                XtraMessageBox.Show("Please select TimeFrame change option", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;                
            }
            User.ResetLogOutTimer();

            Model.ProcessDelayedVisitChangeFrame(option);            
            View.Destroy();
        }

        private void OnProcessWorkingHoursExtensionClick(object sender, EventArgs args)
        {
            WorkingHoursExtensionResult addResult = (WorkingHoursExtensionResult)View.m_gridWorkHoursExtensionView.GetRow(
                View.m_gridWorkHoursExtensionView.FocusedRowHandle);

            if (addResult == null)
            {
                XtraMessageBox.Show("Please select working hours extension option", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            User.ResetLogOutTimer();

            Model.ProcessDelayedVisitExtendWorkingHours(addResult);            
            View.Destroy();
        }

        #endregion        

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        #endregion   
    }
}
