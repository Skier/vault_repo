using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.CustomerLookup;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Dalworth.Server.MainForm.TransferEquipment
{
    public class TransferEquipmentController : Controller<TransferEquipmentModel, TransferEquipmentView>
    {        
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region ModifiedEquipment

        private List<EquipmentWrapper> m_modifiedEquipment;
        public List<EquipmentWrapper> ModifiedEquipment
        {
            get { return m_modifiedEquipment; }
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
            View.m_cmbLocationType.SelectedIndexChanged += OnLocationTypeChanged;

            View.m_cmbInventoryRoom.Validating += OnValidating;            
            View.m_cmbVan.Validating += OnValidating;
            View.m_cmbCustomerAddress.Validating += OnValidating;            
            View.m_txtCustomerId.Validating += OnCustomerIdValidating;

            View.m_gridEquipmentView.FocusedColumnChanged += OnGridEquipmentFocusedColumnChanged;
            View.m_gridEquipmentView.ValidatingEditor += OnGridEquipmentValidatingEditor;
            View.m_colEquipmentType.View.CustomColumnSort += OnColEquipmentTypeSort;

            View.KeyDown += OnKeyDown;
            View.m_btnCustomerLookup.Click += OnCustomerLookupClick;
        }

        #endregion

        #region OnGridEquipmentFocusedColumnChanged

        private void OnGridEquipmentFocusedColumnChanged(object sender, FocusedColumnChangedEventArgs e)
        {
            if (IsAllCellsInGroupAreFilled(View.m_gridEquipmentView))
            {
                View.m_gridEquipmentView.BeginUpdate();
                Model.EquipmentNumbers.Add(
                    new EquipmentNumber(GetCurrentEquipmentType(View.m_gridEquipmentView)));
                View.m_gridEquipmentView.EndUpdate();
            }
        }

        #endregion

        #region Add equipment rows

        private bool IsAllCellsInGroupAreFilled(GridView gridView)
        {
            EquipmentType currentEquipmentType = GetCurrentEquipmentType(gridView);

            if (currentEquipmentType == null)
                return false;

            IList<EquipmentNumber> dataSource
                = (IList<EquipmentNumber>)gridView.DataSource;

            int emptyCellsCount = 0;

            for (int i = 0; i < dataSource.Count; i++)
            {
                if (dataSource[i].EquipmentType.Equals(currentEquipmentType))
                {
                    if (dataSource[i].SerialNumber1 == string.Empty)
                        emptyCellsCount++;
                    if (dataSource[i].SerialNumber2 == string.Empty)
                        emptyCellsCount++;
                    if (dataSource[i].SerialNumber3 == string.Empty)
                        emptyCellsCount++;
                    if (dataSource[i].SerialNumber4 == string.Empty)
                        emptyCellsCount++;

                    if (emptyCellsCount > 1)
                        return false;

                }
            }

            return true;
        }

        private EquipmentType GetCurrentEquipmentType(GridView gridView)
        {
            if (gridView.FocusedRowHandle < 0)
                return null;
            return ((EquipmentNumber)gridView.GetRow(gridView.FocusedRowHandle)).EquipmentType;
        }

        #endregion

        #region Equipment Validation

        private void OnGridEquipmentValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            if ((string)e.Value == string.Empty)
                return;

            string currentNumber
                = (string)View.m_gridEquipmentView.GetRowCellValue(View.m_gridEquipmentView.FocusedRowHandle,
                                                                   View.m_gridEquipmentView.FocusedColumn);

            EquipmentType currentEquipmentType = GetCurrentEquipmentType(View.m_gridEquipmentView);

            if (currentNumber == (string)e.Value)
                return;

            if (!Model.IsEquipmentNumberExist((string)e.Value))
            {
                e.Valid = false;
                e.ErrorText = "Invalid equipment serial number";
            }
            else if (!Model.IsEquipmentNumberExist((string)e.Value, currentEquipmentType))
            {
                e.Valid = false;
                e.ErrorText = "Equipment is not " + currentEquipmentType.Type;
            }
            else if (Model.IsLoadAndKeepNumberDuplicated((string)e.Value))
            {
                e.Valid = false;
                e.ErrorText = "Duplicate equipment serial number";
            }
        }

        #endregion

        #region OnColEquipmentTypeSort

        private void OnColEquipmentTypeSort(object sender, CustomColumnSortEventArgs e)
        {
            e.Result = Model.EquipmentNumbers[e.ListSourceRowIndex1].EquipmentType.ID.CompareTo(
                Model.EquipmentNumbers[e.ListSourceRowIndex2].EquipmentType.ID);
            e.Handled = true;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_gridEquipment.DataSource = Model.EquipmentNumbers;            
            OnLocationTypeChanged(null, null);

            View.m_gridEquipmentView.FocusedRowHandle = View.m_gridEquipmentView.GetRowHandle(0);
            View.m_gridEquipmentView.FocusedColumn = View.m_colSerialNumber1;
        }

        #endregion

        #region OnKeyDown

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                OnOkClick(null, null);
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        #endregion

        #region SetCustomerToUI

        private void SetCustomerToUI(Customer customer)
        {
            Model.SelectedCustomer = customer;
            View.m_lblCustomerName.Text = Model.SelectedCustomer.DisplayName;

            View.m_cmbCustomerAddress.Properties.DataSource
                = Model.GetCustomerAddresses(Model.SelectedCustomer);
            View.m_cmbCustomerAddress.ItemIndex = 0;
            View.m_errorProvider.SetError(View.m_cmbCustomerAddress, string.Empty);
            
        }

        #endregion

        #region OnCustomerLookupClick

        private void OnCustomerLookupClick(object sender, EventArgs e)
        {
            using (CustomerLookupController controller = Prepare<CustomerLookupController>())
            {
                controller.Execute(false);
                if (controller.IsCustomerSelected)
                {
                    View.m_txtCustomerId.Text = controller.Customer.Customer.ID.ToString();
                    SetCustomerToUI(controller.Customer.Customer);
                }
                    
            }            
        }

        #endregion

        #region OnLocationTypeChanged

        private void OnLocationTypeChanged(object sender, EventArgs e)
        {
            EquipmentLocationTypeEnum locationType = (EquipmentLocationTypeEnum)View.m_cmbLocationType.EditValue;
            UpdateLocationComboData(locationType);
            ShowLocationTypeControls(locationType);
        }

        private void UpdateLocationComboData(EquipmentLocationTypeEnum locationType)
        {
            Model.UpdateData(locationType);

            switch (locationType)
            {
                case EquipmentLocationTypeEnum.InventoryRoom:
                    View.m_cmbInventoryRoom.Properties.DataSource = Model.InventoryRooms;
                    View.m_cmbInventoryRoom.EditValue = null;
                    break;
                case EquipmentLocationTypeEnum.Van:
                    View.m_cmbVan.Properties.DataSource = Model.Vans;
                    View.m_cmbVan.EditValue = null;
                    break;
            }            
        }

        private void ShowLocationTypeControls(EquipmentLocationTypeEnum locationType)
        {
            View.m_pnlCustomer.Visible = false;
            View.m_pnlInventoryRoom.Visible = false;
            View.m_pnlVan.Visible = false;

            switch (locationType)
            {
                case EquipmentLocationTypeEnum.InventoryRoom:
                    View.m_pnlInventoryRoom.Visible = true;                    
                    break;
                case EquipmentLocationTypeEnum.Van:
                    View.m_pnlVan.Visible = true;
                    break;
                case EquipmentLocationTypeEnum.Customer:
                    View.m_pnlCustomer.Visible = true;
                    break;
            }

            View.m_errorProvider.ClearErrors();
        }

        #endregion

        #region Validation

        private void OnValidating(object sender, EventArgs e)
        {
            Validate();
        }

        private bool IsValid()
        {
            int locationType = (int)View.m_cmbLocationType.EditValue;        
    
            if (locationType == (int)EquipmentLocationTypeEnum.Customer
                && Model.SelectedCustomer == null)
            {
                XtraMessageBox.Show("Please select Customer", "No Customer", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void OnCustomerIdValidating(object sender, CancelEventArgs e)
        {
            if (View.m_txtCustomerId.Text == string.Empty)
                View.m_errorProvider.SetError(View.m_txtCustomerId, string.Empty);
            else
            {
                int customerId = int.Parse(View.m_txtCustomerId.Text);

                try
                {
                    Customer customer = Customer.FindByPrimaryKey(customerId);
                    if (Model.SelectedCustomer == null || Model.SelectedCustomer.ID != customer.ID)
                        SetCustomerToUI(customer);
                    View.m_errorProvider.SetError(View.m_txtCustomerId, string.Empty);
                }
                catch (DataNotFoundException)
                {
                    View.m_errorProvider.SetError(View.m_txtCustomerId, "Customer not found");
                }
            }
        }


        private void Validate()
        {
            int locationType = (int) View.m_cmbLocationType.EditValue;            

            if (locationType == (int)EquipmentLocationTypeEnum.InventoryRoom)
            {
                if (View.m_cmbInventoryRoom.EditValue == null)
                    View.m_errorProvider.SetError(View.m_cmbInventoryRoom, "Please select destination inventory room");
                else
                    View.m_errorProvider.SetError(View.m_cmbInventoryRoom, string.Empty);
            }
            else if (locationType == (int)EquipmentLocationTypeEnum.Van)
            {
                if (View.m_cmbVan.EditValue == null)
                    View.m_errorProvider.SetError(View.m_cmbVan, "Please select destination van");                
                else
                    View.m_errorProvider.SetError(View.m_cmbVan, string.Empty);                
            }
            else if (locationType == (int)EquipmentLocationTypeEnum.Customer)
            {
                if (View.m_cmbCustomerAddress.EditValue == null)
                    View.m_errorProvider.SetError(View.m_cmbCustomerAddress, "Please select customer address");                                
                else
                    View.m_errorProvider.SetError(View.m_cmbCustomerAddress, string.Empty);                                
            }

        }

        #endregion

        #region IsWarningOperation
        
        private bool IsWarningOperation()
        {
            int locationType =
                (int)((ImageComboBoxItem)View.m_cmbLocationType.SelectedItem).Value;

            if ((locationType == (int)EquipmentLocationTypeEnum.InventoryRoom
                && Model.IsAllEquipmentInInventoryRoom())
                || locationType == (int)EquipmentLocationTypeEnum.Lost)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            View.m_gridEquipmentView.CloseEditor();
            View.m_gridEquipmentView.ValidateEditor();

            if (!IsValid())
                return;
                
            Validate();
                
            if (View.m_errorProvider.HasErrors || View.m_gridEquipmentView.HasColumnErrors)
                return;

            m_modifiedEquipment = Model.GetEquipmentWrappers();
            if (m_modifiedEquipment.Count == 0)
            {
                XtraMessageBox.Show("Please enter Equipment to be transferred",
                    "No Equipment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;                
            }

            if (IsWarningOperation())
            {
                if (XtraMessageBox.Show(
                    "You are trying to transfer equipment between some location and customer site or van.\nThis is not common use of this operation and should be considered as an emergency way.\nPlease use it if you have a credible reason, otherwise use Start Day, End Day and Service Visit to transfer equipment.\nDo you want to continue?",
                    "Suspicious operation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;
            }

            int locationType =
                (int)((ImageComboBoxItem)View.m_cmbLocationType.SelectedItem).Value;


            
            foreach (EquipmentWrapper equipment in m_modifiedEquipment)
            {
                equipment.Equipment.AddressId = null;
                equipment.Equipment.VanId = null;
                equipment.Equipment.InventoryRoomId = null;

                if (locationType == (int)EquipmentLocationTypeEnum.InventoryRoom)
                    equipment.Equipment.InventoryRoomId = (int)View.m_cmbInventoryRoom.EditValue;
                else if (locationType == (int)EquipmentLocationTypeEnum.Van)
                    equipment.Equipment.VanId = (int)View.m_cmbVan.EditValue;
                else if (locationType == (int)EquipmentLocationTypeEnum.Customer)
                    equipment.Equipment.AddressId = (int)View.m_cmbCustomerAddress.EditValue;
            }

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
