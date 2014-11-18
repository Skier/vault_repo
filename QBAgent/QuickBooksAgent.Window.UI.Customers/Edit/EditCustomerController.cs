using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Customers.Manage;

namespace QuickBooksAgent.Windows.UI.Customers.Edit
{
    public class EditCustomerController : SingleFormController<EditCustomerModel, EditCustomerView>
    {        
        #region Fields

        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion       

        #region IsDirty
        bool m_dirty;
        private bool IsDirty
        {
            get { return m_dirty; }
        }

        #endregion

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            foreach (TabPage tabPage in View.m_tabs.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                    if (control is TextBox)
                    {
                        control.TextChanged += new EventHandler(OnTextChanged);                        
                    }
                    else if (control is ComboBox)
                        control.TextChanged += new EventHandler(OnTextChanged);
            }

            View.m_chkSameAsBill.Click +=new EventHandler(OnSameAsBillClick);            
            View.m_txtDisplayAs.GotFocus += new EventHandler(OnDisplayAsOrPrintAsGotFocus);
            View.m_txtPrintAs.GotFocus += new EventHandler(OnDisplayAsOrPrintAsGotFocus);
            
            View.m_txtPhone.LostFocus += new EventHandler(OnPhoneLostFocus);
            View.m_txtMobile.LostFocus += new EventHandler(OnPhoneLostFocus);
            View.m_txtFax.LostFocus += new EventHandler(OnPhoneLostFocus);
            View.m_txtPager.LostFocus += new EventHandler(OnPhoneLostFocus);
            View.m_txtOther.LostFocus += new EventHandler(OnPhoneLostFocus);
        }

        #endregion        

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Debug.Assert(data != null && data.Length > 0, "Least one parameter required");

            Debug.Assert(data[0] is List<Customer>, "First parameter must be list of customers");

            Model.CustomerList = data[0] as List<Customer>;

            if (data.Length > 1)
            {
                if (data[1] is Customer)
                    Model.CurrentCustomer = data[1] as Customer;
                else
                    throw new QuickBooksAgentException("Invalid second parameter");
            }
            
            if (data.Length > 2)
            {
                Model.IsReadOnly = (bool) data[2];
            }

            Model.Init();          
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            // fill the salutation list
            List<Salutation> salutationList = Salutation.Find();

            foreach (Salutation salutation in salutationList)
                View.m_cmbSalutation.Items.Add(salutation.Name);

            // fill the suffix list
            List<Suffix> suffixList = Suffix.Find();

            foreach (Suffix suffix in suffixList)
                View.m_cmbSuffix.Items.Add(suffix.Name);

            // fill the terms list
            List<Terms> termsList = Terms.Find();

            // if terms once has been set, than dont allow to remove it (allow changing only)
            if ((Model.CurrentCustomer != null &&  (Model.CurrentCustomer.Terms == null
                || Model.CurrentCustomer.EntityState == EntityState.Created)) ||
                    Model.CurrentCustomer == null)          
                        View.m_cmbTerms.Items.Add(new Terms(0, string.Empty));           
            
            foreach (Terms terms in termsList)
                View.m_cmbTerms.Items.Add(terms);

            View.m_cmbDelivery.Items.Add("Print");
            View.m_cmbDelivery.Items.Add("Email");
            View.m_cmbDelivery.Items.Add("None");

            View.m_cmbSalutation.SelectedIndex = 0;
            View.m_cmbSuffix.SelectedIndex = 0;
            View.m_cmbTerms.SelectedIndex = 0;
            View.m_cmbDelivery.SelectedIndex = 0;

            RefreshUI();

            m_dirty = false;

            View.m_txtFirstName.Select(View.m_txtFirstName.Text.Length, 0);
            View.m_txtFirstName.Focus();
            
            if (Model.IsReadOnly)
            {
                foreach (TabPage page in View.m_tabs.TabPages)
                    page.Enabled = false;
                View.m_tabs.Focus();
            }
                
        }

        #endregion

        #region RefreshUI

        private void RefreshUI()
        {
            if (Model.CurrentCustomer == null)
                View.Text = "Add Customer - Q-Agent";
            else
            {
                View.Text = "Edit Customer - Q-Agent";                
                View.m_curBalance.Enabled = View.m_lblBalance.Enabled = Model.CurrentCustomer.EntityState == EntityState.Created;
                View.m_lblBalanceDate.Visible = View.m_dtpBalanceDate.Visible = Model.CurrentCustomer.EntityState == EntityState.Created;

                View.m_txtFirstName.Text = Model.CurrentCustomer.FirstName;
                View.m_txtLastName.Text = Model.CurrentCustomer.LastName;
                View.m_txtMiddleName.Text = Model.CurrentCustomer.MiddleName;
                View.m_txtDisplayAs.Text = Model.CurrentCustomer.Name;                                
                View.m_txtPrintAs.Text = Model.CurrentCustomer.PrintAs;                                
                View.m_txtCompanyName.Text = Model.CurrentCustomer.CompanyName;
                
                View.m_curBalance.Value = Model.CurrentCustomer.Balance;

                if (Model.CurrentCustomer.BalanceDate.HasValue)
                    View.m_dtpBalanceDate.Value = Model.CurrentCustomer.BalanceDate.Value.Date;

                View.m_txtAddress.Text = Model.CurrentCustomer.BillAddr1;
                View.m_txtCity.Text = Model.CurrentCustomer.BillCity;
                View.m_txtCountry.Text = Model.CurrentCustomer.BillCountry;
                View.m_txtPostalCode.Text = Model.CurrentCustomer.BillPostalCode;
                View.m_txtState.Text = Model.CurrentCustomer.BillState;

                View.m_txtPhone.Text = Model.CurrentCustomer.Phone;
                View.m_txtMobile.Text = Model.CurrentCustomer.Mobile;
                View.m_txtEmail.Text = Model.CurrentCustomer.Email;
                View.m_txtFax.Text = Model.CurrentCustomer.Fax;
                View.m_txtOther.Text = Model.CurrentCustomer.AltPhone;
                View.m_txtPager.Text = Model.CurrentCustomer.Pager;

                if (Model.CurrentCustomer.ShippingAddressSameAsBilling)
                {
                    View.m_chkSameAsBill.Checked = true;
                }
                else
                {
                    View.m_txtShipAddress.Text = Model.CurrentCustomer.ShipAddr1;
                    View.m_txtShipCity.Text = Model.CurrentCustomer.ShipCity;
                    View.m_txtShipCountry.Text = Model.CurrentCustomer.ShipCountry;
                    View.m_txtShipPostalCode.Text = Model.CurrentCustomer.ShipPostalCode;
                    View.m_txtShipState.Text = Model.CurrentCustomer.ShipState;
                }

                View.m_cmbSalutation.SelectedItem = Model.CurrentCustomer.Salutation;
                View.m_cmbSuffix.SelectedItem = Model.CurrentCustomer.Suffix;

                View.m_txtResale.Text = Model.CurrentCustomer.ResaleNumber;

                if (Model.CurrentCustomer.Terms != null)
                {
                    foreach (Terms terms in View.m_cmbTerms.Items)
                        if (terms.QuickBooksListId ==
                                Model.CurrentCustomer.Terms.QuickBooksListId)
                            View.m_cmbTerms.SelectedItem = terms;
                }

                View.m_cmbDelivery.SelectedItem = Model.CurrentCustomer.DeliveryMethod;                
            }

            UpdateControls();
        }

        #endregion

        #region IsEmail

        public bool IsEmail(string inputEmail)
        {
            const string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";

            Regex re = new Regex(strRegex);

            if (re.IsMatch(inputEmail))
                return true;
            
            return false;
        }
        #endregion

        #region OnSameAsBillClick

        void OnSameAsBillClick(object sender, EventArgs e)
        {
            UpdateControls();

            m_dirty = true;
        }

        #endregion

        #region UpdateControls

        private void UpdateControls()
        {
            bool check = View.m_chkSameAsBill.Checked;

            View.m_txtShipAddress.Enabled = View.m_lblShipStreet.Enabled = !check;
            View.m_txtShipCity.Enabled = View.m_lblShipCity.Enabled = !check;
            View.m_txtShipCountry.Enabled = View.m_lblShipCountry.Enabled = !check;
            View.m_txtShipPostalCode.Enabled = View.m_lblShipZip.Enabled = !check;
            View.m_txtShipState.Enabled = View.m_lblShipState.Enabled = !check;

            if (check)
            {
                View.m_txtShipAddress.Text = View.m_txtAddress.Text;
                View.m_txtShipCity.Text = View.m_txtCity.Text;
                View.m_txtShipCountry.Text = View.m_txtCountry.Text;
                View.m_txtShipPostalCode.Text = View.m_txtPostalCode.Text;
                View.m_txtShipState.Text = View.m_txtState.Text;
            } 
        }

        #endregion

        #region OnTextChanged

        void OnTextChanged(object sender, EventArgs e)
        {
            m_dirty = true;
        }

        #endregion        

        #region Default Action

        public override bool IsDefaultActionExist
        {
            get
            {
                return true;
            }
        }

        public override string DefaultActionName
        {
            get
            {
                return "Cancel";
            }
        }

        public override void OnDefaultAction()
        {
            bool cancel = false;

            if (IsDirty)
                cancel = base.OnCancel("Do you want discard changes?");

            m_isCancelled = !cancel;

            if (!cancel)
                View.Destroy();
        }

        #endregion 

        #region GetNameFromUI
        
        private string GetNameFromUI()
        {
            string result = string.Empty;
            
            result += View.m_cmbSalutation.SelectedItem;

            if (string.IsNullOrEmpty(result))
                result += View.m_txtFirstName.Text;
            else if (!string.IsNullOrEmpty(View.m_txtFirstName.Text))
                result += " " + View.m_txtFirstName.Text;
            
            if (string.IsNullOrEmpty(result))
                result += View.m_txtMiddleName.Text;
            else if (!string.IsNullOrEmpty(View.m_txtMiddleName.Text))
                result += " " + View.m_txtMiddleName.Text;
            
            if (string.IsNullOrEmpty(result))
                result += View.m_txtLastName.Text;
            else if (!string.IsNullOrEmpty(View.m_txtLastName.Text))
                result += " " + View.m_txtLastName.Text;
            
            if (string.IsNullOrEmpty(result))
                result += View.m_cmbSuffix.SelectedItem;
            else if (!string.IsNullOrEmpty(View.m_cmbSuffix.SelectedItem.ToString()))
                result += " " + View.m_cmbSuffix.SelectedItem;

            return result;
        }

        #endregion

        #region OnDisplayAsOrPrintAsGotFocus

        private void OnDisplayAsOrPrintAsGotFocus(object sender, EventArgs e)
        {
            if (Model.CurrentCustomer == null)//Create
            {
                TextBox textBox = (TextBox) sender;
                
                if (textBox.Text == String.Empty)
                {
                    if (textBox.Name == "m_txtDisplayAs")
                    {
                        if (GetNameFromUI() != string.Empty)
                            textBox.Text = GetNameFromUI();
                        else
                            textBox.Text = View.m_txtCompanyName.Text;                        
                    } else
                    {
                        if (View.m_txtCompanyName.Text != string.Empty)
                            textBox.Text = View.m_txtCompanyName.Text;
                        else
                            textBox.Text = GetNameFromUI();                        
                    }
                    
                }
            }
        }

        #endregion                       

        #region OnPhoneLostFocus
        /// <summary>
        /// Format Phone number (123) 456-7890
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnPhoneLostFocus(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox) sender;

            string digitsString = GetDigitsOnly(textBox.Text);
            string result = string.Empty;

            if (digitsString.Length == 10 || digitsString.Length == 11)
            {
                if (digitsString.Length == 11)
                {
                    result += digitsString[0] + " ";
                    digitsString = digitsString.Remove(0, 1);
                }

                result += "(";
                for (int i = 0; i < digitsString.Length; i++)
                {
                    result += digitsString[i];

                    if (i == 2)
                        result += ") ";
                    
                    if (i == 5)
                        result += "-";
                }

                textBox.Text = result;
            }
            
        }
                
        private string GetDigitsOnly(string s)
        {
            string result = string.Empty;

            foreach (char c in s)
            {
                if (char.IsDigit(c))
                    result += c;
            }
            
            return result;            
        }

        #endregion               

        #region OnSave

        protected override bool OnSave()
        {
            if (Model.IsReadOnly)
                return true;
            
            if (!m_dirty)
                return true;
            
            OnDisplayAsOrPrintAsGotFocus(View.m_txtDisplayAs, EventArgs.Empty);
            OnDisplayAsOrPrintAsGotFocus(View.m_txtPrintAs, EventArgs.Empty);
            OnPhoneLostFocus(View.m_txtPhone, EventArgs.Empty);
            OnPhoneLostFocus(View.m_txtMobile, EventArgs.Empty);
            OnPhoneLostFocus(View.m_txtFax, EventArgs.Empty);
            OnPhoneLostFocus(View.m_txtPager, EventArgs.Empty);
            OnPhoneLostFocus(View.m_txtOther, EventArgs.Empty);
            
            if (View.m_txtDisplayAs.Text == string.Empty)
            {
                MessageDialog.Show(MessageDialogType.Information, "Display As shouldn't be empty");                
                View.m_tabs.SelectedIndex = 0;
                View.m_txtDisplayAs.Focus();
                View.m_txtDisplayAs.SelectAll();
                return false;                
            }

            if((View.m_cmbDelivery.SelectedIndex == 1) && !IsEmail(View.m_txtEmail.Text))
            {                
                View.m_tabs.SelectedIndex = 1;
                MessageDialog.Show(MessageDialogType.Information, "The email adress should be in the format username@somewhere.com");
                return false;
            }
            
            Customer customer = new Customer();
            
            customer.Balance = View.m_curBalance.Value;

            //check for unique
            string excludeFullName = string.Empty;
            string checkedFullName = View.m_txtDisplayAs.Text;
            
            if (Model.CurrentCustomer != null) {// create
                excludeFullName = Model.CurrentCustomer.FullName;
                if (Model.CurrentCustomer.FullName.IndexOf(":") != -1)
                {
                    int lastHierarchySeparatorIndex
                        = Model.CurrentCustomer.FullName.LastIndexOf(":");
                    checkedFullName = Model.CurrentCustomer.FullName.Substring(0, lastHierarchySeparatorIndex + 1)
                                      + View.m_txtDisplayAs.Text;
                }                
            }

            if (Customer.IsFullNameExist(checkedFullName, excludeFullName))
            {
                MessageDialog.Show(MessageDialogType.Information, "Entered Display As name already used. Please specify another name");
                View.m_tabs.SelectedIndex = 0;
                View.m_txtDisplayAs.Focus();
                View.m_txtDisplayAs.SelectAll();
                return false;                                
            }            
            
            if (Model.CurrentCustomer == null || (Model.CurrentCustomer != null && 
                Model.CurrentCustomer.EntityState == EntityState.Created))
                    customer.BalanceDate = View.m_dtpBalanceDate.Value.Date;

            customer.Salutation = View.m_cmbSalutation.SelectedItem.ToString();
            customer.Suffix = View.m_cmbSuffix.SelectedItem.ToString();
            customer.CompanyName = View.m_txtCompanyName.Text;
            customer.FirstName = View.m_txtFirstName.Text;
            customer.LastName = View.m_txtLastName.Text;
            customer.MiddleName = View.m_txtMiddleName.Text;
            customer.Name = View.m_txtDisplayAs.Text;
            customer.PrintAs = View.m_txtPrintAs.Text;
            customer.FullName = checkedFullName;

            customer.Phone = View.m_txtPhone.Text;
            customer.Mobile = View.m_txtMobile.Text;
            customer.Email = View.m_txtEmail.Text;
            customer.Fax = View.m_txtFax.Text;
            customer.AltPhone = View.m_txtOther.Text;
            customer.Pager = View.m_txtPager.Text;

            customer.BillAddr1 = View.m_txtAddress.Text;
            customer.BillCity = View.m_txtCity.Text;
            customer.BillCountry = View.m_txtCountry.Text; ;
            customer.BillPostalCode = View.m_txtPostalCode.Text;
            customer.BillState = View.m_txtState.Text;

            if (View.m_chkSameAsBill.Checked)
            {
                customer.ShipAddr1 = customer.BillAddr1;
                customer.ShipCity = customer.BillCity;
                customer.ShipCountry = customer.BillCountry;
                customer.ShipPostalCode = customer.BillPostalCode;
                customer.ShipState = customer.BillState;
            }
            else
            {
                customer.ShipAddr1 = View.m_txtShipAddress.Text;
                customer.ShipCity = View.m_txtShipCity.Text;
                customer.ShipCountry = View.m_txtShipCountry.Text;
                customer.ShipPostalCode = View.m_txtShipPostalCode.Text;
                customer.ShipState = View.m_txtShipState.Text;
            }

            customer.ShippingAddressSameAsBilling = View.m_chkSameAsBill.Checked;

            customer.ResaleNumber = View.m_txtResale.Text;

            if (View.m_cmbTerms.SelectedIndex > 0)
                customer.Terms = (Terms)View.m_cmbTerms.SelectedItem;
            else
                customer.Terms = null;

            customer.DeliveryMethod = View.m_cmbDelivery.Text;

            try
            {
                Model.Save(customer);
            }
            catch (Exception e)
            {
                EventService.AddEvent(
                    new QuickBooksAgentException("Unable to save customer",e));

                return false;
            }

            return true;
        }

        #endregion 
    }
}
