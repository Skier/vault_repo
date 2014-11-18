using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.AddressEdit;
using Dalworth.Server.MainForm.CustomerEdit;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using BaseControl=DevExpress.XtraEditors.BaseControl;

namespace Dalworth.Server.MainForm.CustomerLookup
{
    public class CustomerLookupController : Controller<CustomerLookupModel, CustomerLookupView>
    {
        private bool m_isAutoRefreshEnabled = true;

        #region IsCustomerSelected

        private bool m_isCustomerSelected;
        public bool IsCustomerSelected
        {
            get { return m_isCustomerSelected; }
        }

        #endregion

        #region Masked Properties

        private string HomePhoneUI
        {
            get
            {
                if (View.m_txtHomePhone.EditValue != null)
                    return Utils.ExtractDigits(View.m_txtHomePhone.EditValue.ToString());
                return string.Empty;
            }
        }

        private string BusinessPhoneUI
        {
            get
            {
                if (View.m_txtBusinessPhone.EditValue != null)
                    return Utils.ExtractDigits(View.m_txtBusinessPhone.EditValue.ToString());
                return string.Empty;
            }
        }

        private string ZipUI
        {
            get
            {
                if (View.m_ctrlAddress.m_txtZip.EditValue != null)
                    return Utils.ExtractDigits(View.m_ctrlAddress.m_txtZip.EditValue.ToString());
                return string.Empty;
            }
        }

        #endregion


        #region Customer

        private CustomerAndAddress m_customer;
        public CustomerAndAddress Customer
        {
            get { return m_customer; }
        }

        #endregion

        #region SelectedCustomer

        private CustomerAndAddress SelectedCustomer
        {
            get
            {
                if (View.m_gridViewCustomers.FocusedRowHandle >= 0)
                    return (CustomerAndAddress) View.m_gridViewCustomers.GetRow(
                        View.m_gridViewCustomers.FocusedRowHandle);
                return null;
            }
        }

        #endregion        

        #region BaseLead

        private LeadWrapper m_baseLead;
        public LeadWrapper BaseLead
        {
            set
            {
                m_baseLead = value;
                LoadLeadDataToUI();
            }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data != null && data.Length > 0 && data[0] != null)
                m_customer = (CustomerAndAddress)data[0];
                
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_txtFirstName.TextChanged += OnFilterChanged;
            View.m_txtLastName.TextChanged += OnFilterChanged;
            View.m_txtHomePhone.TextChanged += OnFilterChanged;
            View.m_txtBusinessPhone.TextChanged += OnFilterChanged;
            View.m_ctrlAddress.m_txtZip.TextChanged += OnFilterChanged;
            View.m_ctrlAddress.m_txtBlock.TextChanged += OnFilterChanged;
            View.m_ctrlAddress.m_txtStreet.TextChanged += OnFilterChanged;
            View.m_ctrlAddress.m_txtCity.TextChanged += OnFilterChanged;

            View.m_txtFirstName.KeyPress += OnFirstNameKeyPress;
            View.m_txtLastName.KeyPress += OnFirstNameKeyPress;
            View.m_txtHomePhone.KeyPress += OnPhoneKeyPress;
            View.m_txtBusinessPhone.KeyPress += OnPhoneKeyPress;

            View.m_ctrlAddress.m_txtBlock.KeyPress += OnAddressKeyPress;
            View.m_ctrlAddress.m_txtStreet.KeyPress += OnAddressKeyPress;
            View.m_ctrlAddress.m_txtZip.KeyPress += OnAddressKeyPress;


            View.m_txtFirstName.LostFocus += OnFirstNameLostFocus;
            View.m_txtLastName.LostFocus += OnLastNameLostFocus;
            View.m_txtHomePhone.LostFocus += OnHomePhoneLostFocus;
            View.m_txtBusinessPhone.LostFocus += OnBusinessPhoneLostFocus;

            View.m_ctrlAddress.m_txtBlock.LostFocus += OnAddressLostFocus;
            View.m_ctrlAddress.m_txtStreet.LostFocus += OnAddressLostFocus;
            View.m_ctrlAddress.m_txtZip.LostFocus += OnAddressLostFocus;

            View.m_btnCreate.Click += OnCreateClick;
            View.m_btnDelete.Click += OnDeleteClick;
            View.m_btnCancel.Click += OnCancelClick;
            View.m_gridViewCustomers.RowCellStyle += OnCustomersRowPaint;

            View.KeyDown += OnFormKeyDown;
            View.m_gridViewCustomers.KeyPress += OnGridCustomersKeyPress;
            View.m_gridViewCustomers.DoubleClick += OnGridCustomersDoubleClick;

            View.m_txtLastName.Validating += OnLastNameValidating;
            View.m_txtHomePhone.Validating += OnPhoneValidating;
            View.m_txtBusinessPhone.Validating += OnPhoneValidating;
            View.m_ctrlAddress.m_txtZip.Validating += OnZipValidating;
            View.m_ctrlAddress.m_txtBlock.Validating += OnBlockValidating;
            View.m_ctrlAddress.m_txtStreet.Validating += OnStreetValidating;
            View.m_ctrlAddress.m_txtCity.Validating += OnCityValidating;

        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_gridCustomers.DataSource = Model.Customers;
            EnableDisableCreateCustomer();

            Address defaultAddress = new Address();
            if (m_baseLead != null && m_baseLead.Lead.ContainsAddress)
            {
                defaultAddress.State = m_baseLead.Lead.State;
                defaultAddress.Zip = m_baseLead.Lead.Zip;
            }
            else
            {
                defaultAddress.State = "TX";
            }
            View.m_ctrlAddress.Address = defaultAddress;
            View.m_ctrlAddress.IsValidationDisabled = true;
        }

        #endregion

        #region LoadLeadDataToUI

        private void LoadLeadDataToUI()
        {
            if (m_baseLead == null)
                return;

            View.m_txtFirstName.Text = m_baseLead.Lead.FirstName;
            View.m_txtLastName.Text = m_baseLead.Lead.LastName;
            View.m_txtHomePhone.Text = Utils.FormatPhone(m_baseLead.Lead.Phone1);
            View.m_txtBusinessPhone.Text = Utils.FormatPhone(m_baseLead.Lead.Phone2);
            View.m_txtBusinessPhone.Invalidate();

            View.m_ctrlAddress.m_txtZip.Text = m_baseLead.Lead.Zip.ToString();
            View.m_ctrlAddress.m_cmbState.EditValue = m_baseLead.Lead.State;
            View.m_ctrlAddress.m_lblAddressTip.Text = m_baseLead.Lead.Address1 + " " + m_baseLead.Lead.Address2;

            ApplyFilter(SearchPreferredCriteria.Name);
        }

        #endregion

        #region ShortCuts

        private void OnGridCustomersDoubleClick(object sender, EventArgs e)
        {
            if (SelectedCustomer == null)
                return;

            m_customer = SelectedCustomer;
            m_isCustomerSelected = true;
            View.Destroy();
        }

        private void OnGridCustomersKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (SelectedCustomer == null)
                    return;

                m_customer = SelectedCustomer;
                m_isCustomerSelected = true;
                View.Destroy();
            }
        }

        private void OnFormKeyDown(object sender, KeyEventArgs e)
        {   
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                if (IsCreateFieldsValid())
                    OnCreateClick(null, null);                
            }                            
        }

        #endregion

        #region Clear fild errors

        private void OnCityValidating(object sender, CancelEventArgs e)
        {
            if (View.m_ctrlAddress.m_txtCity.Text != string.Empty)
                View.m_errorProvider.SetError(View.m_ctrlAddress.m_txtCity, string.Empty);
        }

        private void OnStreetValidating(object sender, CancelEventArgs e)
        {
            if (View.m_ctrlAddress.m_txtStreet.Text != string.Empty)
                View.m_errorProvider.SetError(View.m_ctrlAddress.m_txtStreet, string.Empty);
        }

        private void OnBlockValidating(object sender, CancelEventArgs e)
        {
            if (View.m_ctrlAddress.m_txtBlock.Text != string.Empty)
                View.m_errorProvider.SetError(View.m_ctrlAddress.m_txtBlock, string.Empty);
        }

        private void OnZipValidating(object sender, CancelEventArgs e)
        {
            if (View.m_ctrlAddress.m_txtZip.Text != string.Empty)
                View.m_errorProvider.SetError(View.m_ctrlAddress.m_txtZip, string.Empty);
        }

        private void OnPhoneValidating(object sender, CancelEventArgs e)
        {
            if (HomePhoneUI != string.Empty || BusinessPhoneUI != string.Empty)
            {
                View.m_errorProvider.SetError(View.m_txtHomePhone, string.Empty);
                View.m_errorProvider.SetError(View.m_txtBusinessPhone, string.Empty);
            }
        }

        private void OnLastNameValidating(object sender, CancelEventArgs e)
        {
            if (View.m_txtLastName.Text != string.Empty)
                View.m_errorProvider.SetError(View.m_txtLastName, string.Empty);
        }

        #endregion

        #region IsCreateFieldsValid

        private bool IsCreateFieldsValid()
        {            
            if (View.m_ctrlAddress.m_txtCity.Text != string.Empty)
                View.m_errorProvider.SetError(View.m_ctrlAddress.m_txtCity, string.Empty);
            else
                View.m_errorProvider.SetError(View.m_ctrlAddress.m_txtCity, "Please enter City");

            if (View.m_ctrlAddress.m_txtStreet.Text != string.Empty)
                View.m_errorProvider.SetError(View.m_ctrlAddress.m_txtStreet, string.Empty);
            else
                View.m_errorProvider.SetError(View.m_ctrlAddress.m_txtStreet, "Please enter Street");

            if (View.m_ctrlAddress.m_txtBlock.Text != string.Empty)
                View.m_errorProvider.SetError(View.m_ctrlAddress.m_txtBlock, string.Empty);
            else
                View.m_errorProvider.SetError(View.m_ctrlAddress.m_txtBlock, "Please enter Block number");

            if (View.m_ctrlAddress.m_txtZip.Text != string.Empty)
                View.m_errorProvider.SetError(View.m_ctrlAddress.m_txtZip, string.Empty);
            else
                View.m_errorProvider.SetError(View.m_ctrlAddress.m_txtZip, "Please enter Zip code");

            if (HomePhoneUI == string.Empty && BusinessPhoneUI == string.Empty)
            {
                View.m_errorProvider.SetError(View.m_txtHomePhone, "Please enter at least one contact phone number");
                
            } else 
            {
                View.m_errorProvider.SetError(View.m_txtHomePhone, string.Empty);
                View.m_errorProvider.SetError(View.m_txtBusinessPhone, string.Empty);
            }

            if (View.m_txtLastName.Text != string.Empty)
                View.m_errorProvider.SetError(View.m_txtLastName, string.Empty);
            else
                View.m_errorProvider.SetError(View.m_txtLastName, "Please enter Last Name");

            return !View.m_errorProvider.HasErrors;
        }

        #endregion


        #region FilterOnEnter

        private void OnFirstNameKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                //View.m_timerFilter.Stop();
                ApplyFilter(SearchPreferredCriteria.Name);
            }
        }

        private void OnPhoneKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                //View.m_timerFilter.Stop();
                ApplyFilter(SearchPreferredCriteria.Phone);
            }
        }

        private void OnAddressKeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                if (ZipUI != string.Empty && View.m_ctrlAddress.m_txtBlock.Text != string.Empty)
                    ApplyFilter(SearchPreferredCriteria.Address);
            }
        }        

        #endregion

        #region FilterOnLeave

        private void OnLastNameLostFocus(object sender, EventArgs e)
        {
            if (View.m_txtFirstName.Text != string.Empty && View.m_txtLastName.Text != string.Empty)
                ApplyFilter(SearchPreferredCriteria.Name);
        }

        private void OnFirstNameLostFocus(object sender, EventArgs e)
        {
            if (View.m_txtLastName.Text != string.Empty)
                ApplyFilter(SearchPreferredCriteria.Name);
        }

        private void OnHomePhoneLostFocus(object sender, EventArgs e)
        {
            if (HomePhoneUI != string.Empty)
                ApplyFilter(SearchPreferredCriteria.Phone);
        }

        private void OnBusinessPhoneLostFocus(object sender, EventArgs e)
        {
            if (BusinessPhoneUI != string.Empty)
                ApplyFilter(SearchPreferredCriteria.Phone);
        }

        private void OnAddressLostFocus(object sender, EventArgs e)
        {
            if (ZipUI != string.Empty && View.m_ctrlAddress.m_txtBlock.Text != string.Empty)
                ApplyFilter(SearchPreferredCriteria.Address);
        }

        #endregion

        #region OnFilterChanged

        private void OnFilterChanged(object sender, EventArgs e)
        {
            EnableDisableCreateCustomer();
        }

        private SearchPreferredCriteria m_lastCriteria;
        private string m_lastFirstName;
        private string m_lastLastName;
        private string m_lastHomePhone;
        private string m_lastBusinessPhone;
        private string m_lastBlock;
        private string m_lastStreet;
        private string m_lastZip;

        private void ApplyFilter(SearchPreferredCriteria criteria)
        {
            if (!m_isAutoRefreshEnabled)
                return;

            if (criteria == SearchPreferredCriteria.Name && View.m_txtLastName.Text.Trim() == string.Empty)
                return;

            if (criteria == SearchPreferredCriteria.Phone)
            {
                if (HomePhoneUI == string.Empty && BusinessPhoneUI == string.Empty)
                    return;

                if (HomePhoneUI != string.Empty && HomePhoneUI.Length < 10)
                    return;

                if (BusinessPhoneUI != string.Empty && BusinessPhoneUI.Length < 10)
                    return;
            }

            if (criteria == SearchPreferredCriteria.Address)
            {
                if (ZipUI.Length < 5)
                    return;

                if (View.m_ctrlAddress.m_txtBlock.Text.Trim() == string.Empty
                    && View.m_ctrlAddress.m_txtStreet.Text.Trim() != string.Empty)
                {
                    return;
                }
            }

            if (m_lastCriteria == criteria
                && m_lastFirstName == View.m_txtFirstName.Text
                && m_lastLastName == View.m_txtLastName.Text
                && m_lastHomePhone == HomePhoneUI
                && m_lastBusinessPhone == BusinessPhoneUI
                && m_lastBlock == View.m_ctrlAddress.m_txtBlock.Text
                && m_lastStreet == View.m_ctrlAddress.m_txtStreet.Text
                && m_lastZip == ZipUI)
            {
                return;
            }


            using (new WaitCursor())
            {
                View.m_gridCustomers.BeginUpdate();
                Model.Refresh(criteria, View.m_txtFirstName.Text, View.m_txtLastName.Text,
                    HomePhoneUI, BusinessPhoneUI,
                    View.m_ctrlAddress.m_txtBlock.Text, View.m_ctrlAddress.m_txtStreet.Text, ZipUI);
                View.m_gridCustomers.DataSource = Model.Customers;
                View.m_gridCustomers.EndUpdate();

                m_lastCriteria = criteria;
                m_lastFirstName = View.m_txtFirstName.Text;
                m_lastLastName = View.m_txtLastName.Text;
                m_lastHomePhone = HomePhoneUI;
                m_lastBusinessPhone = BusinessPhoneUI;
                m_lastBlock = View.m_ctrlAddress.m_txtBlock.Text;
                m_lastStreet = View.m_ctrlAddress.m_txtStreet.Text;
                m_lastZip = ZipUI;

                View.m_btnDelete.Enabled = Model.Customers.Count > 0;                
            }
        }

        #endregion

        #region EnableDisableCreateCustomer

        private void EnableDisableCreateCustomer()
        {
            if (View.m_txtLastName.Text != string.Empty
                && (HomePhoneUI != string.Empty || BusinessPhoneUI != string.Empty)
                && ZipUI != string.Empty
                && View.m_ctrlAddress.m_txtBlock.Text != string.Empty
                && View.m_ctrlAddress.m_txtStreet.Text != string.Empty
                && View.m_ctrlAddress.m_txtCity.Text != string.Empty)
            {
                View.m_btnCreate.Enabled = true;
            } else
            {
                View.m_btnCreate.Enabled = false;
            }
        }

        #endregion

        #region OnCustomersRowPaint

        private void OnCustomersRowPaint(object sender, RowCellStyleEventArgs e)
        {
            CustomerAndAddress customer
                = (CustomerAndAddress)View.m_gridViewCustomers.GetRow(e.RowHandle);


            if (Model.CurrentSearchCriteria != SearchPreferredCriteria.Name)
            {
                if (e.Column.Name == View.m_colFirstName.Name && View.m_txtFirstName.Text != string.Empty
                    && customer.Customer.FirstName.StartsWith(View.m_txtFirstName.Text, true, CultureInfo.CurrentCulture))
                {
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size,
                                                 FontStyle.Bold);      
                    
                    e.Appearance.ForeColor = Color.Red;
                    return;
                }

                if (e.Column.Name == View.m_colLastName.Name && View.m_txtLastName.Text != string.Empty
                    && customer.Customer.LastName.StartsWith(View.m_txtLastName.Text, true, CultureInfo.CurrentCulture))
                {
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size,
                                                 FontStyle.Bold);
                    e.Appearance.ForeColor = Color.Red;
                    return;
                }
            }

            if (Model.CurrentSearchCriteria != SearchPreferredCriteria.Phone)
            {
                if (e.Column.Name == View.m_colPhones.Name 
                    && (HomePhoneUI != string.Empty || BusinessPhoneUI != string.Empty)
                    && (customer.Customer.Phone1.StartsWith(HomePhoneUI, true, CultureInfo.CurrentCulture)
                        || customer.Customer.Phone1.StartsWith(BusinessPhoneUI, true, CultureInfo.CurrentCulture)
                        || customer.Customer.Phone2.StartsWith(HomePhoneUI, true, CultureInfo.CurrentCulture)
                        || customer.Customer.Phone2.StartsWith(BusinessPhoneUI, true, CultureInfo.CurrentCulture)))
                {
                    if ((HomePhoneUI != string.Empty
                        && (customer.Customer.Phone1.StartsWith(HomePhoneUI, true, CultureInfo.CurrentCulture)
                            || customer.Customer.Phone2.StartsWith(HomePhoneUI, true, CultureInfo.CurrentCulture)))
                        || (BusinessPhoneUI != string.Empty
                        && (customer.Customer.Phone1.StartsWith(BusinessPhoneUI, true, CultureInfo.CurrentCulture)
                            || customer.Customer.Phone2.StartsWith(BusinessPhoneUI, true, CultureInfo.CurrentCulture))))
                    {
                        e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size,
                                                     FontStyle.Bold);
                        e.Appearance.ForeColor = Color.Red;
                    } 
                    return;
                }
            }


            if (Model.CurrentSearchCriteria != SearchPreferredCriteria.Address)
            {
                if (e.Column.Name == View.m_colZip.Name && ZipUI != string.Empty
                    && customer.Address.Zip.ToString().StartsWith(ZipUI, true, CultureInfo.CurrentCulture))
                {
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size,
                                                 FontStyle.Bold);
                    e.Appearance.ForeColor = Color.Red;
                    return;
                }

                if (e.Column.Name == View.m_colBlock.Name && View.m_ctrlAddress.m_txtBlock.Text != string.Empty
                    && customer.Address.Block.StartsWith(View.m_ctrlAddress.m_txtBlock.Text, true, CultureInfo.CurrentCulture))
                {
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size,
                                                 FontStyle.Bold);
                    e.Appearance.ForeColor = Color.Red;
                    return;
                }

                if (e.Column.Name == View.m_colStreet.Name && View.m_ctrlAddress.m_txtStreet.Text != string.Empty
                    && customer.Address.Street.StartsWith(View.m_ctrlAddress.m_txtStreet.Text, true, CultureInfo.CurrentCulture))
                {
                    e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size,
                                                 FontStyle.Bold);
                    e.Appearance.ForeColor = Color.Red;
                    return;
                }
            }           
        }

        #endregion


        #region OnCreateClick

        private void OnCreateClick(object sender, EventArgs e)
        {
            Customer customer = new Customer();
            customer.FirstName = View.m_txtFirstName.Text;
            customer.LastName = View.m_txtLastName.Text;
            customer.Phone1 = HomePhoneUI;
            customer.Phone2 = BusinessPhoneUI;
            customer.CustomerTypeId = (byte)(View.m_cmbCustomerStatus.SelectedIndex + 1);

            CustomerAndAddress customerAndAddress 
                = new CustomerAndAddress(customer, View.m_ctrlAddress.Address);

            try
            {
                Database.Begin();
                Model.CreateNewCustomer(customerAndAddress);
                Database.Commit();

                m_customer = customerAndAddress;
                m_isCustomerSelected = true;
                View.Destroy();

            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }
        }

        #endregion

        #region OnDeleteClick

        private void OnDeleteClick(object sender, EventArgs e)
        {
            CustomerAndAddress selectedCustomer = SelectedCustomer;
            if (selectedCustomer == null)
            {
                XtraMessageBox.Show("Please select Customer to delete", "Customer Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);                
                return;
            }


            if (XtraMessageBox.Show("Do you really want to delete this customer", "Customer Delete", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            if (m_customer != null && selectedCustomer.Customer.ID == m_customer.Customer.ID)
            {
                XtraMessageBox.Show("This Customer cannot be deleted", "Customer Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);                
                return;
            }

            try
            {
                Database.Begin();
                Domain.Customer.Delete(selectedCustomer.Customer);
                Address.Delete(selectedCustomer.Address);
                Database.Commit();
                ApplyFilter(Model.CurrentSearchCriteria);
                //OnFilterChanged(null, null);
            }
            catch (Exception)
            {
                Database.Rollback();
                XtraMessageBox.Show("This Customer cannot be deleted", "Customer Delete", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        #endregion

        #region OnCancelClick

        private void OnCancelClick(object sender, EventArgs e)
        {
            View.Destroy();
        }

        #endregion
    }
}
