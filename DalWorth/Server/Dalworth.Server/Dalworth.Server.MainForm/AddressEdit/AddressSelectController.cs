using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;

namespace Dalworth.Server.MainForm.AddressEdit
{
    public class AddressSelectController : Controller<AddressSelectModel, AddressSelectView>
    {
        #region AddressWrapper

        public class AddressWrapper
        {
            private Address m_address;
            private bool m_selected;

            public AddressWrapper(Address address)
            {
                m_address = address;
            }
            
            public bool IsSelected
            {
                get { return m_selected; }
                set { m_selected = value; }
            }
            
            public string AddressSingleLine
            {
                get { return m_address.AddressSingleLine; }
            }
            
            public string Map
            {
                get { return m_address.Map; }
            }

            public Address Address
            {
                get { return m_address; }
            }
        }

        #endregion
        
        #region BaseAddress

        private Address m_baseAddress;
        public Address BaseAddress
        {
            get { return m_baseAddress; }
            set
            {
                m_baseAddress = value;
            }
        }

        #endregion

        #region CurrentAddress

        private Address m_currentAddress;
        public Address CurrentAddress
        {
            get { return m_currentAddress; }
            set
            {
                m_currentAddress = value;
            }
        }

        #endregion

        #region IsBaseAddressActive

        private bool m_isBaseAddressActive;
        public bool IsBaseAddressActive
        {
            get { return m_isBaseAddressActive; }
            set
            {
                m_isBaseAddressActive = value;
            }
        }

        #endregion

        #region BaseAddressName

        private string m_baseAddressName;
        public string BaseAddressName
        {
            get { return m_baseAddressName; }
            set { m_baseAddressName = value; }
        }

        #endregion

        #region Customer

        private Customer m_customer;
        public Customer Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        #endregion

        #region Addresses

        private BindingList<AddressWrapper> m_addresses;
        public BindingList<AddressWrapper> Addresses
        {
            get { return m_addresses; }
        }

        #endregion

        private string m_caption;

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region Selected / Focused Address
        
        private AddressWrapper SelectedAddress
        {
            get
            {
                if (m_addresses != null)
                    foreach (AddressWrapper wrapper in m_addresses)
                        if (wrapper.IsSelected)
                            return wrapper;
                
                return null;
            }
        }

        private AddressWrapper FocusedAddress
        {
            get
            {
                int[] selectedRows = View.m_gridViewAddresses.GetSelectedRows();
                
                if (selectedRows == null || selectedRows.Length == 0)
                    return null;
                
                return (AddressWrapper) View.m_gridViewAddresses.GetRow(selectedRows[0]);
            }
            
            set
            {
                if (value == null) 
                    return;
                
                BindingList<AddressWrapper> addresses = (BindingList<AddressWrapper>)View.m_gridAddresses.DataSource;
                
                if (addresses != null) 
                {
                    for (int i = 0; i < addresses.Count; i++)
                    {
                        if (addresses[i].Address.ID == value.Address.ID)
                        {
                            int rowHandel = View.m_gridViewAddresses.GetRowHandle(i);
                            View.m_gridViewAddresses.FocusedRowHandle = rowHandel;
                            break;
                        }
                    }

                    View.m_gridAddresses.RefreshDataSource();
                }
            }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            BaseAddress = (Address) data[0];
            CurrentAddress = (Address)data[1];
            BaseAddressName = (string) data[2];
            IsBaseAddressActive = (bool) data[3];
            m_caption = (string)data[4];
            m_customer = (Customer) data[5];
            base.OnModelInitialize(data);

            UpdateAddresses(CurrentAddress);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnAdd.Click += OnAddClick;
            View.m_btnEdit.Click += OnEditClick;
            View.m_btnDelete.Click += OnDeleteClick;
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;
            View.m_chkUseBaseAddress.CheckedChanged += OnUseBaseAddressCheckedChanged;
            View.m_gridViewAddresses.FocusedRowChanged += OnAddressFocusedRowChanged;
            View.m_chkAddress.CheckedChanged += OnAddressCheckedChanged;
            View.m_gridAddresses.DataSource = Addresses;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.Text = m_caption + " Edit";
            
            View.m_chkUseBaseAddress.Checked = IsBaseAddressActive;
            View.m_chkUseBaseAddress.Text += " " + BaseAddressName;
            View.m_chkUseBaseAddress.Focus();
            
            RefreshUI(SelectedAddress);
        }

        #endregion

        #region RefreshUI

        private void RefreshUI(AddressWrapper focusedAddress)
        {
            bool useBaseAddress = View.m_chkUseBaseAddress.Checked;

            if (useBaseAddress) 
            {
                View.m_gridAddresses.Enabled = false;
                View.m_btnAdd.Enabled = false;
                View.m_btnEdit.Enabled = false;
                View.m_btnDelete.Enabled = false;
                
                if (SelectedAddress != null)
                    SelectedAddress.IsSelected = false;
                
                View.m_btnOk.Enabled = true;
            } 
            else 
            {
                View.m_gridAddresses.Enabled = true;
                View.m_btnAdd.Enabled = true;
                View.m_btnOk.Enabled = SelectedAddress != null;
            }

            View.m_gridAddresses.RefreshDataSource();

            if (!useBaseAddress)
            {
                FocusedAddress = focusedAddress;

                View.m_btnEdit.Enabled = focusedAddress != null;
                View.m_btnDelete.Enabled = focusedAddress != null && focusedAddress.Address.ID != CurrentAddress.ID;
            }
        }

        #endregion

        #region OnUseCustomerAddressCheckedChanged

        private void OnUseBaseAddressCheckedChanged(object sender, EventArgs e)
        {
            RefreshUI(FocusedAddress);
        }

        #endregion

        #region OnAddressFocusedRowChanged

        private void OnAddressFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (!View.m_chkUseBaseAddress.Checked)
                RefreshUI(FocusedAddress);
        }

        #endregion

        #region OnAddressCheckedChanged

        void OnAddressCheckedChanged(object sender, EventArgs e)
        {
            CheckEdit editor = View.m_gridViewAddresses.ActiveEditor as CheckEdit;

            if (SelectedAddress != null)
                SelectedAddress.IsSelected = false;
            
            if (editor.Checked)
                FocusedAddress.IsSelected = true;

            RefreshUI(FocusedAddress);
        }

        #endregion

        #region OnAddClick

        private void OnAddClick(object sender, EventArgs e)
        {
            AddressEditController controller = Prepare<AddressEditController>();
            controller.Execute(false);

            if (!controller.IsCancelled)
            {
                try
                {
                    Database.Begin();

                    controller.Address.Modified = DateTime.Now;
                    Address.Insert(controller.Address);

                    CustomerAddressAdditional addressAdditional = new CustomerAddressAdditional(
                        m_customer.ID, controller.Address.ID);

                    CustomerAddressAdditional.Insert(addressAdditional);

                    Database.Commit();
                }
                catch
                {
                    Database.Rollback();
                    throw;
                }

                UpdateAddresses(controller.Address);
                RefreshUI(SelectedAddress);
            }            
        }

        #endregion

        #region OnEditClick

        private void OnEditClick(object sender, EventArgs e)
        {
            AddressEditController controller = Prepare<AddressEditController>(FocusedAddress.Address);
            controller.Execute(false);

            if (!controller.IsCancelled)
            {
                try
                {
                    Database.Begin();

                    controller.Address.Modified = DateTime.Now;
                    Address.Update(controller.Address);

                    Database.Commit();
                }
                catch
                {
                    Database.Rollback();
                    throw;
                }

                Address selectedAddress;
                
                if (SelectedAddress == null)
                    selectedAddress = FocusedAddress.Address;
                else
                    selectedAddress = SelectedAddress.Address;

                UpdateAddresses(selectedAddress);
                RefreshUI(new AddressWrapper(controller.Address));
            }            
        }

        #endregion

        #region OnDeleteClick

        private void OnDeleteClick(object sender, EventArgs e)
        {
            DialogResult result = XtraMessageBox.Show(
                "Are you sure?", m_caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            try {
                if (result == DialogResult.Yes) 
                {
                    try
                    {
                        Database.Begin();

                        CustomerAddressAdditional addressAdditional;
                        addressAdditional = CustomerAddressAdditional.FindByPrimaryKey(
                            m_customer.ID, FocusedAddress.Address.ID);

                        CustomerAddressAdditional.Delete(addressAdditional);

                        Address.Delete(FocusedAddress.Address);

                        Database.Commit();
                    }
                    catch
                    {
                        Database.Rollback();
                        throw;
                    }

                    if (SelectedAddress == null)
                        UpdateAddresses(null);
                    else
                        UpdateAddresses(SelectedAddress.Address);
                    
                    RefreshUI(FocusedAddress);
                }
            } catch {
                MessageBox.Show(
                    "The selected address can not be deleted.", 
                    m_caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            IsBaseAddressActive = View.m_chkUseBaseAddress.Checked;
            if (!IsBaseAddressActive)
                CurrentAddress = FocusedAddress.Address;
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

        #region UpdateAddresses
        
        private void UpdateAddresses(Address selectedAddress)
        {
            List<Address> addresses = Address.FindAdditionalBy(m_customer);
            
            List<AddressWrapper> addressWrappers = new List<AddressWrapper>(addresses.Count);
            foreach (Address address in addresses) {
                addressWrappers.Add(new AddressWrapper(address));
            }

            if (m_addresses == null)
                m_addresses = new BindingList<AddressWrapper>();
            else
                m_addresses.Clear();

            foreach (AddressWrapper address in addressWrappers)
            {
                address.IsSelected = selectedAddress != null && selectedAddress.ID == address.Address.ID;
                m_addresses.Add(address);
            }
        }

        #endregion
    }
}
