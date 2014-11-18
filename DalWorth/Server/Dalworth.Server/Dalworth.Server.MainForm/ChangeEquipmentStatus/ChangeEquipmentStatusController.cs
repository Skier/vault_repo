using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;

namespace Dalworth.Server.MainForm.ChangeEquipmentStatus
{
    public class ChangeEquipmentStatusController : Controller<ChangeEquipmentStatusModel, ChangeEquipmentStatusView>
    {
        private bool m_isStatusUpdating;

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region ModifiedEquipment

        public List<EquipmentWrapper> ModifiedEquipment
        {
            get { return new List<EquipmentWrapper>(Model.ModifiedEquipment); }
        }

        #endregion

        #region Notes

        public string Notes
        {
            get { return View.m_txtNotes.Text; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.InitialEquipment = (List<EquipmentWrapper>) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;
            View.m_btnReset.Click += OnResetClick;            
            View.m_cmbStatus.SelectedIndexChanged += OnStatusChanged;
            View.m_gridEquipmentView.RowUpdated += OnGridRowUpdated;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            foreach (EquipmentStatus status in Model.EquipmentStatuses)
            {
                View.m_cmbStatus.Properties.Items.Add(
                    new ImageComboBoxItem(status.Status, (object)status.ID));                
            }

            View.m_gridEquipment.DataSource = Model.ModifiedEquipment;
            InitStatusCombo();
        }

        #endregion        

        #region OnResetClick

        private void OnResetClick(object sender, EventArgs e)
        {
            Model.ResetEquipment();
            View.m_gridEquipment.DataSource = Model.ModifiedEquipment;
            InitStatusCombo();
        }

        #endregion

        #region InitStatusCombo

        private void InitStatusCombo()
        {
            m_isStatusUpdating = true;

            int? status = Model.GetEquipmentsStatus();

            if (status != null)
            {
                View.m_cmbStatus.SelectedItem
                    = View.m_cmbStatus.Properties.Items.GetItem(status.Value);
            }
            else
                View.m_cmbStatus.EditValue = null;

            m_isStatusUpdating = false;
        }

        #endregion

        #region OnStatusChanged

        private void OnStatusChanged(object sender, EventArgs e)
        {
            if (m_isStatusUpdating)
                return;

            Model.SetAllStatus(
                (int)((ImageComboBoxItem)View.m_cmbStatus.SelectedItem).Value);
            Model.ModifiedEquipment.ResetBindings();
        }

        #endregion

        #region OnGridRowUpdated

        private void OnGridRowUpdated(object sender, RowObjectEventArgs e)
        {
            InitStatusCombo();
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {            
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
