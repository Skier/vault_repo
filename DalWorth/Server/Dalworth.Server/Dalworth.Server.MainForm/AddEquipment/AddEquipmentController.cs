using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors.Controls;

namespace Dalworth.Server.MainForm.AddEquipment
{
    public class AddEquipmentController : Controller<AddEquipmentModel, AddEquipmentView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region AddedEquipment

        public Domain.Equipment AddedEquipment
        {
            get { return Model.AddedEquipment; }
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;            

            View.m_txtSerialNumber.Validating += OnSerialNumberValidating;
            View.m_cmbInventoryRoom.Validating += OnInventoryRoomValidating;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            foreach (EquipmentType type in Model.EquipmentTypes)
            {
                View.m_cmbType.Properties.Items.Add(
                    new ImageComboBoxItem(type.Type, (object) type.ID));
            }

            foreach (EquipmentStatus status in Model.EquipmentStatuses)
            {
                View.m_cmbStatus.Properties.Items.Add(
                    new ImageComboBoxItem(status.Status, (object)status.ID));                
            }

            View.m_cmbType.SelectedIndex = 0;
            View.m_cmbStatus.SelectedIndex = 0;
            
            PopulateInvantoryRooms();
        }

        #endregion

        #region PopulateInvantoryRooms

        private void PopulateInvantoryRooms()
        {
            View.m_cmbInventoryRoom.Properties.Items.Clear();

            foreach (InventoryRoom room in Model.InventoryRooms)
            {
                View.m_cmbInventoryRoom.Properties.Items.Add(
                    new ImageComboBoxItem(room.Name, (object)room.ID));
            }

            View.m_cmbInventoryRoom.SelectedIndex = 0;
        }

        #endregion

        #region OnSerialNumberValidating

        private void OnSerialNumberValidating(object sender, CancelEventArgs e)
        {
            ValidateSerialNumber(false);
        }

        private void ValidateSerialNumber(bool serverValidation)
        {            
            if (View.m_txtSerialNumber.Text == string.Empty)
            {
                View.m_errorProvider.SetError(View.m_txtSerialNumber, "Please enter serial number");            
                return;
            }                
            else if (serverValidation)
            {
                try
                {
                    Domain.Equipment.FindBy(View.m_txtSerialNumber.Text);
                    View.m_errorProvider.SetError(View.m_txtSerialNumber, "This serial number is already exist, please enter another number");
                    return;
                }
                catch (DataNotFoundException){}
            }

            View.m_errorProvider.SetError(View.m_txtSerialNumber, string.Empty);
        }

        #endregion

        #region OnInventoryRoomValidating

        private void OnInventoryRoomValidating(object sender, CancelEventArgs e)
        {            
            if (View.m_cmbInventoryRoom.EditValue == null)
                View.m_errorProvider.SetError(View.m_cmbInventoryRoom, "Please select Inventory Room");            
            else
                View.m_errorProvider.SetError(View.m_cmbInventoryRoom, string.Empty);            
        }

        #endregion
               
        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            ValidateSerialNumber(true);
            OnInventoryRoomValidating(null, null);
            if (View.m_errorProvider.HasErrors)
                return;

            Model.AddedEquipment = new Domain.Equipment(0,
                (int)((ImageComboBoxItem)View.m_cmbType.SelectedItem).Value,
                (int)((ImageComboBoxItem)View.m_cmbStatus.SelectedItem).Value,
                (int)((ImageComboBoxItem)View.m_cmbInventoryRoom.SelectedItem).Value,
                null, null,
                View.m_txtSerialNumber.EditValue.ToString());
            
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
