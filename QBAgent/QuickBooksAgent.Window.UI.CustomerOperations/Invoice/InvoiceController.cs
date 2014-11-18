using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using QuickBooksAgent.Data;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.CustomerOperations.Invoice
{
    public class InvoiceController : SingleFormController<InvoiceModel, InvoiceView>
    {
        #region OnInitialize

        protected override void OnInitialize()
        {
            foreach (TabPage tabPage in View.m_tabs.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                    if (control is TextBox)
                    {
                        control.TextChanged += new EventHandler(OnTextChanged);
                        control.GotFocus += new EventHandler(OnGotFocus);
                    }
                    else if (control is ComboBox)
                        control.TextChanged += new EventHandler(OnTextChanged);
            }

            View.m_chkDelivery.Click += new EventHandler(OnTextChanged);
            View.m_tabs.SelectedIndexChanged += new EventHandler(OnSelectedIndexChanged);
            View.m_btnClearShipDate.Click += new EventHandler(OnClearShipDateClick);
            
            View.m_menuAddNewCharge.Click += new EventHandler(OnAddChargeClick);
            View.m_menuDeleteNewCharge.Click += new EventHandler(OnDeleteChargeClick);
            View.m_menuEditNewCharge.Click += new EventHandler(OnEditChargeClick);
            
            View.m_table.RowChanged += new RowHandler(OnInvoiceLineRowChanged);
            View.m_table.GotFocus += new EventHandler(OnTableInvoiceLineFocusChanged);
            View.m_table.Enter += new CellValueHandler(OnTableInvoiceLineEnter);
            View.m_tabs.SelectedIndexChanged += new EventHandler(OnTabChanged);
            Model.InvoiceLinesAmountChanged += new InvoiceModel.InvoiceLinesAmountChangedHandler(OnTotalAmountInvoiceLineChanged);
            View.m_lblSubtotalNewChargesValue.TextChanged += new EventHandler(OnSubtotalNewChargesValueChanged);
            View.m_chkIsCustomerTaxable.CheckStateChanged += new EventHandler(OnCustomerTaxableChanged);
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Debug.Assert(data != null, "Customer must be passed");

            Model.Customer = (Customer)data[0];

            if (data.Length > 1)
                Model.Invoice = (Domain.Invoice)data[1];            

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {           
            View.m_table.BindModel(Model);            
            List<Terms> termsList = Terms.Find();

            View.m_cmbTerms.Items.Add(new Terms(0, string.Empty));

            foreach (Terms terms in termsList)
                View.m_cmbTerms.Items.Add(terms);

            foreach (Account account in Model.AccountsList)
            {
                View.m_cmbDiscountAccount.Items.Add(account);
                View.m_cmbTaxAccount.Items.Add(account);
                View.m_cmbShippingAccount.Items.Add(account);
            }            

            if (Model.Invoice == null)
            {
                /////////////Add New invoice///////////////                                
                View.Text = "Add Invoice - Q-Agent";

                View.m_cmbTerms.SelectedIndexChanged += new EventHandler(OnTermsChanged);
                View.m_dtpInvoiceDate.ValueChanged += new EventHandler(OnInvoiceDateChanged);                
                
                //Addresses
                View.m_txtBillToStreet.Text = Address.GetStreetText(Model.Customer.BillAddr1,
                    Model.Customer.BillAddr2, Model.Customer.BillAddr3, Model.Customer.BillAddr4);
                View.m_txtBillToCity.Text = Model.Customer.BillCity;
                View.m_txtBillToState.Text = Model.Customer.BillState;
                View.m_txtBillToZip.Text = Model.Customer.BillPostalCode;
                View.m_txtBillToCountry.Text = Model.Customer.BillCountry;

                if (Model.Customer.ShippingAddressSameAsBilling)
                {
                    View.m_txtShipToStreet.Text = View.m_txtBillToStreet.Text;
                    View.m_txtShipToCity.Text = View.m_txtBillToCity.Text;
                    View.m_txtShipToState.Text = View.m_txtBillToState.Text;
                    View.m_txtShipToZip.Text = View.m_txtBillToZip.Text;
                    View.m_txtShipToCountry.Text = View.m_txtBillToCountry.Text;                                                     
                } else
                {
                    View.m_txtShipToStreet.Text = Address.GetStreetText(Model.Customer.ShipAddr1,
                        Model.Customer.ShipAddr2, Model.Customer.ShipAddr3, Model.Customer.ShipAddr4);
                    View.m_txtShipToCity.Text = Model.Customer.ShipCity;
                    View.m_txtShipToState.Text = Model.Customer.ShipState;
                    View.m_txtShipToZip.Text = Model.Customer.ShipPostalCode;
                    View.m_txtShipToCountry.Text = Model.Customer.ShipCountry;                    
                }

                if (Model.Customer.Terms != null && Model.Customer.Terms.TermsId != 0)
                    View.m_cmbTerms.SelectedItem = Model.Customer.Terms;

                View.m_dtpShipDate.Value = null;
                View.m_cmbCalcTaxSubtotal.SelectedIndex = 0;

                View.m_cmbDiscountAccount.SelectedIndex = 0;
                View.m_cmbTaxAccount.SelectedIndex = 0;
                View.m_cmbShippingAccount.SelectedIndex = 0;
                View.m_cmbTerms.Focus();
            }
            else
            {
                if (Model.Invoice.EntityState == EntityState.Synchronized)
                {
                    ///////////View Synchronized invoice///////////////
                    View.Text = "View Invoice - Q-Agent";
                    View.m_menuEditNewCharge.Text = "View Charge";

                    Model.UpdateTable();
                    if (Model.Invoice.Subtotal != null)
                    {
                        View.m_lblSubtotalValue.Text = View.m_lblSubtotalNewChargesValue.Text;
                    }
                    
                    if (Model.Invoice.BalanceRemaining != null)
                        View.m_lblBalanceDueAmount.Text = Model.Invoice.BalanceRemaining.Value.ToString("C");
                    
                    InvoiceLine discount = Model.GetSynchronizedDiscount();
                    if (discount != null)
                    {
                        if (discount.Rate != null)
                        {
                            decimal rate;
                            if (discount.Rate < 0)
                                rate = -100*discount.Rate.Value;
                            else
                                rate = 100*discount.Rate.Value;

                            if (decimal.Truncate(rate) == rate)
                                View.m_txtDiscountPercent.Text = decimal.ToInt32(rate).ToString();
                            else
                                View.m_txtDiscountPercent.Text = QBDataType.RoundTripFormat(rate);
                        }
                        View.m_curDiscountAmount.Value = discount.Amount;

                        if (discount.IsTaxable)
                            View.m_cmbCalcTaxSubtotal.SelectedIndex = 0;
                        else
                            View.m_cmbCalcTaxSubtotal.SelectedIndex = 1;
                    }

                    InvoiceLine tax = Model.GetSynchronizedTax();
                    if (tax != null)
                    {
                        View.m_chkIsCustomerTaxable.Checked = true;
                        View.m_txtTaxPercent.Text = QBDataType.RoundTripFormat(tax.RatePercent);
                        View.m_curTaxAmount.Value = tax.Amount;
                    }

                    InvoiceLine shipping = Model.GetSynchronizedShipping();
                    if (shipping != null)
                    {
                        View.m_curShipping.Value = shipping.Amount;
                    }
                    
                    RecalculateTaxSubtotal();                                                                
                    
                    DisableControls();
                    View.m_tabs.Focus();            
                } else
                {
                    ////////////Edit created invoice/////////////////////
                    View.Text = "Edit Invoice - Q-Agent";

                    View.m_txtDiscountPercent.Text = QBDataType.RoundTripFormat(Model.Invoice.DiscountLineRatePercent);
                    View.m_curDiscountAmount.Value = Model.Invoice.DiscountLineAmount;
                    View.m_chkIsCustomerTaxable.Checked = Model.Invoice.IsCustomerTaxable;
                    if (Model.Invoice.TaxCalculationType == false)
                        View.m_cmbCalcTaxSubtotal.SelectedIndex = 0;
                    else
                        View.m_cmbCalcTaxSubtotal.SelectedIndex = 1;
                    View.m_txtTaxPercent.Text = QBDataType.RoundTripFormat(Model.Invoice.SalesTaxLineRatePercent);
                    View.m_curTaxAmount.Value = Model.Invoice.SalesTaxLineAmount;
                    View.m_curShipping.Value = Model.Invoice.ShippingLineAmount;
                    Model.UpdateTable();
                    View.m_lblSubtotalValue.Text = View.m_lblSubtotalNewChargesValue.Text;
                        
                    RecalculateTaxSubtotal();
                    UpdateBalanceDue();
                    View.m_cmbTerms.Focus();            
                }
                
                ////////////View and Edit invoice/////////////////////
                

                //First page
                if (Model.Invoice.TxnDate.HasValue)
                    View.m_dtpInvoiceDate.Value = Model.Invoice.TxnDate.Value.Date;
                if (Model.Invoice.DueDate.HasValue)
                    View.m_dtpDueDate.Value = Model.Invoice.DueDate.Value.Date;
                if (Model.Invoice.ShipDate.HasValue)
                    View.m_dtpShipDate.Value = Model.Invoice.ShipDate.Value.Date;
                else
                    View.m_dtpShipDate.Value = null;

                //Addresses
                View.m_txtBillToStreet.Text = Address.GetStreetText(Model.Invoice.BillAddr1,
                    Model.Invoice.BillAddr2, Model.Invoice.BillAddr3, Model.Invoice.BillAddr4);
                View.m_txtBillToCity.Text = Model.Invoice.BillCity;
                View.m_txtBillToState.Text = Model.Invoice.BillState;
                View.m_txtBillToZip.Text = Model.Invoice.BillPostalCode;
                View.m_txtBillToCountry.Text = Model.Invoice.BillCountry;

                View.m_txtShipToStreet.Text = Address.GetStreetText(Model.Invoice.ShipAddr1,
                    Model.Invoice.ShipAddr2, Model.Invoice.ShipAddr3, Model.Invoice.ShipAddr4);
                View.m_txtShipToCity.Text = Model.Invoice.ShipCity;
                View.m_txtShipToState.Text = Model.Invoice.ShipState;
                View.m_txtShipToZip.Text = Model.Invoice.ShipPostalCode;
                View.m_txtShipToCountry.Text = Model.Invoice.ShipCountry;


                //Last page
                View.m_txtMemo.Text = Model.Invoice.Memo;

                View.m_chkDelivery.Checked = Model.Invoice.IsToBePrinted;

                if (Model.Invoice.Terms != null)
                    View.m_cmbTerms.SelectedItem = Model.Invoice.Terms;

                if (Model.Invoice.DiscountLineAccountId != null)
                    View.m_cmbDiscountAccount.SelectedItem = new Account(Model.Invoice.DiscountLineAccountId.Value);
                else
                    View.m_cmbDiscountAccount.SelectedIndex = 0;

                if (Model.Invoice.SalesTaxLineAccountId != null)
                    View.m_cmbTaxAccount.SelectedItem = new Account(Model.Invoice.SalesTaxLineAccountId.Value);
                else
                    View.m_cmbTaxAccount.SelectedIndex = 0;

                if (Model.Invoice.ShippingLineAccountId != null)
                    View.m_cmbShippingAccount.SelectedItem = new Account(Model.Invoice.ShippingLineAccountId.Value);
                else
                    View.m_cmbShippingAccount.SelectedIndex = 0;

                View.m_cmbTerms.SelectedIndexChanged += new EventHandler(OnTermsChanged);
                View.m_dtpInvoiceDate.ValueChanged += new EventHandler(OnInvoiceDateChanged);
            }
            
            m_dirty = false;

            if (!Model.IsReadOnly)
            {
                //Other events which should take place after controls init
                View.m_txtDiscountPercent.TextChanged += new EventHandler(OnDiscountPercentChanged);
                View.m_curDiscountAmount.TextChanged += new EventHandler(OnDiscountAmountChanged);
                View.m_lblSubtotalValue.TextChanged += new EventHandler(OnSubtotalChanged);
                View.m_cmbCalcTaxSubtotal.SelectedIndexChanged += new EventHandler(OnCalcTaxSubtotalChanged);
                Model.InvoiceLinesChanged += new InvoiceModel.InvoiceLinesChangedHandler(OnInvoiceLinesChanged);

                View.m_txtTaxPercent.TextChanged += new EventHandler(OnTaxPercentChanged);
                View.m_curTaxAmount.TextChanged += new EventHandler(OnTaxAmountChanged);
                View.m_lblTaxSubtotalAmount.TextChanged += new EventHandler(OnTaxSubtotalChanged);
                View.m_curShipping.TextChanged += new EventHandler(OnShippingChanged);                
            }
            
        }

        #endregion  

        #region DisableControls

        private void DisableControls()
        {
            View.m_chkDelivery.Enabled = false;
            View.m_chkIsCustomerTaxable.Enabled = false;
            View.m_btnClearShipDate.Enabled = false;

            View.m_menuAddNewCharge.Enabled = false;
            View.m_menuDeleteNewCharge.Enabled = false;

            foreach (TabPage tabPage in View.m_tabs.TabPages)
            {
                foreach (Control control in tabPage.Controls)
                {
                    if (control is TextBox || control is ComboBox || 
                        control is Button || control is DateTimePicker)
                        control.Enabled = false;
                }
            } 
        }

        #endregion

        #region OnTextChanged

        void OnTextChanged(object sender, EventArgs e)
        {
            m_dirty = true;
        }

        #endregion

        #region OnGotFocus

        void OnGotFocus(object sender, EventArgs e)
        {
            (sender as TextBox).Select((sender as TextBox).Text.Length, 0);
            (sender as TextBox).Focus();
        }

        #endregion        

        #region OnSelectedIndexChanged

        void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (View.m_tabs.SelectedIndex == 1 && Model.GetRowCount() > 0)
                View.m_table.Select(0);
        }

        #endregion                

        #region OnTermsChanged

        private void OnTermsChanged(object sender, EventArgs e)
        {
            if ((Model.Invoice == null || Model.Invoice.EntityState == EntityState.Created) 
                && View.m_cmbTerms.SelectedItem != null)
            View.m_dtpDueDate.Value
                = View.m_dtpInvoiceDate.Value.AddDays(
                    ((Terms) View.m_cmbTerms.SelectedItem).StdDueDays ?? 0);
        }

        #endregion

        #region OnInvoiceDateChanged

        private void OnInvoiceDateChanged(object sender, EventArgs e)
        {
            OnTermsChanged(null, EventArgs.Empty);
        }

        #endregion

        #region OnClearShipDateClick

        private void OnClearShipDateClick(object sender, EventArgs e)
        {
            View.m_dtpShipDate.Value = null;
        }

        #endregion

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

        #region OnSave
        
        private bool IsFormValid()
        {
            if (!Model.IsInvoiceLinesExist())
            {
                MessageDialog.Show(MessageDialogType.Information,
                    "There are no items to save.");
                View.m_tabs.SelectedIndex = 1;
                return false;
            }

            try
            {
                Address.GetStreetText(View.m_txtBillToStreet.Text);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageDialog.Show(MessageDialogType.Information,
                                   string.Format("Error in Bill To Street field. Each line cannot have more than {0} characters.", Address.MAX_CHARS_PER_LINE));
                View.m_tabs.SelectedIndex = 2;
                View.m_txtBillToStreet.SelectAll();
                View.m_txtBillToStreet.Focus();
                return false;
            }

            try
            {
                Address.GetStreetText(View.m_txtShipToStreet.Text);
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageDialog.Show(MessageDialogType.Information,
                                   string.Format("Error in Ship To Street field. Each line cannot have more than {0} characters.", Address.MAX_CHARS_PER_LINE));
                View.m_tabs.SelectedIndex = 3;
                View.m_txtShipToStreet.SelectAll();
                View.m_txtShipToStreet.Focus();
                return false;
            }

            if (!IsDiscountPercentValid(true))
                return false;

            if (!QBDataType.IsInFormat(GetDiscountPercent(), 3, 5))
            {
                MessageDialog.Show(MessageDialogType.Information, "Discount percentage maximum allowed length is 8 digits (in 3.5 format).");
                View.m_tabs.SelectedIndex = 4;
                View.m_txtDiscountPercent.SelectAll();
                View.m_txtDiscountPercent.Focus();
                return false;
            }

            if (!QBDataType.IsInFormat(View.m_curDiscountAmount.Value, 8, 2))
            {
                MessageDialog.Show(MessageDialogType.Information, "Discount amount maximum allowed length is 10 digits (in 8.2 format).");
                View.m_tabs.SelectedIndex = 4;
                View.m_curDiscountAmount.SelectAll();
                View.m_curDiscountAmount.Focus();
                return false;
            }
            
            if (!IsTaxPercentValid(true))
                return false;
            
            if (!QBDataType.IsInFormat(GetTaxPercent(), 3, 5))
            {
                MessageDialog.Show(MessageDialogType.Information, "Tax percentage maximum allowed length is 8 digits (in 3.5 format).");
                View.m_tabs.SelectedIndex = 4;
                View.m_txtTaxPercent.SelectAll();
                View.m_txtTaxPercent.Focus();
                return false;
            }

                        
            if (!QBDataType.IsInFormat(View.m_curTaxAmount.Value, 8, 2))
            {
                MessageDialog.Show(MessageDialogType.Information, "Tax amount maximum allowed length is 10 digits (in 8.2 format).");
                View.m_tabs.SelectedIndex = 4;
                View.m_curTaxAmount.SelectAll();
                View.m_curTaxAmount.Focus();
                return false;                
            }
            
            if (!QBDataType.IsInFormat(View.m_curShipping.Value, 8, 2))
            {
                MessageDialog.Show(MessageDialogType.Information, "Shipping amount maximum allowed length is 10 digits (in 8.2 format).");
                View.m_tabs.SelectedIndex = 4;
                View.m_curShipping.SelectAll();
                View.m_curShipping.Focus();
                return false;                
            }
            
            if (View.m_curDiscountAmount.Value != null)
            {
                
                if (Math.Abs(View.m_curDiscountAmount.Value.Value) > 
                    Math.Abs(decimal.Parse(View.m_lblSubtotalValue.Text, NumberStyles.Currency))
                    )
                {
                    MessageDialog.Show(MessageDialogType.Information, "Discount amount cannot be greater than the Subtotal.");
                    View.m_tabs.SelectedIndex = 4;
                    View.m_curDiscountAmount.SelectAll();
                    View.m_curDiscountAmount.Focus();
                    return false;                    
                }                                
            }
            
            
            if (decimal.Parse(View.m_lblBalanceDueAmount.Text, NumberStyles.Currency) < 0)
            {
                MessageDialog.Show(MessageDialogType.Information, "Please specify a transaction amount that is 0 or greater.");
                View.m_tabs.SelectedIndex = 4;
                View.m_tabs.Focus();
                return false;                                    
            }
                        
            return true;
        }

        protected override bool OnSave()
        {
            if (Model.IsReadOnly)
                return true;

            View.m_table.ApplyEdit();

            if (!IsFormValid())
                return false;

            Domain.Invoice invoice;
            if (Model.Invoice == null)
                invoice = new Domain.Invoice();
            else
                invoice = Model.Invoice;

            //Info page
            invoice.TxnDate = View.m_dtpInvoiceDate.Value;
            if (View.m_cmbTerms.SelectedIndex > 0)
                invoice.Terms = (Terms)View.m_cmbTerms.SelectedItem;
            else
                invoice.Terms = null;
                        
            invoice.DueDate = View.m_dtpDueDate.Value;

            if (View.m_dtpShipDate.Value != null)
                invoice.ShipDate = (DateTime)View.m_dtpShipDate.Value;
            else
                invoice.ShipDate = null;
            invoice.Memo = View.m_txtMemo.Text.Trim();
            invoice.IsToBePrinted = View.m_chkDelivery.Checked;
            
            //BillTo and ShipTo
            
            string[] separatedBillAddress = Address.GetStreetText(View.m_txtBillToStreet.Text);
            string[] separatedShipAddress = Address.GetStreetText(View.m_txtShipToStreet.Text);

            invoice.BillAddr1 = separatedBillAddress[0];
            invoice.BillAddr2 = separatedBillAddress[1];
            invoice.BillAddr3 = separatedBillAddress[2];
            invoice.BillAddr4 = separatedBillAddress[3];
            invoice.BillCity = View.m_txtBillToCity.Text;
            invoice.BillCountry = View.m_txtBillToCountry.Text;
            invoice.BillPostalCode = View.m_txtBillToZip.Text;
            invoice.BillState = View.m_txtBillToState.Text;

            invoice.ShipAddr1 = separatedShipAddress[0];
            invoice.ShipAddr2 = separatedShipAddress[1];
            invoice.ShipAddr3 = separatedShipAddress[2];
            invoice.ShipAddr4 = separatedShipAddress[3];
            invoice.ShipCity = View.m_txtShipToCity.Text;
            invoice.ShipCountry = View.m_txtShipToCountry.Text;
            invoice.ShipPostalCode = View.m_txtShipToZip.Text;
            invoice.ShipState = View.m_txtShipToState.Text;
                        
            //Totals
            invoice.DiscountLineRatePercent = GetDiscountPercent();
            invoice.DiscountLineAmount = View.m_curDiscountAmount.Value;
            invoice.IsCustomerTaxable = View.m_chkIsCustomerTaxable.Checked;
            if (View.m_cmbCalcTaxSubtotal.SelectedIndex == 0)
                invoice.TaxCalculationType = false;
            else
                invoice.TaxCalculationType = true;

            invoice.SalesTaxLineRatePercent = GetTaxPercent();
            invoice.SalesTaxLineAmount = View.m_curTaxAmount.Value;
            invoice.ShippingLineAmount = View.m_curShipping.Value;


            if (View.m_cmbDiscountAccount.SelectedIndex == 0)
                invoice.DiscountLineAccountId = null;
            else
                invoice.DiscountLineAccountId = ((Account)View.m_cmbDiscountAccount.SelectedItem).AccountId;

            if (View.m_cmbTaxAccount.SelectedIndex == 0)
                invoice.SalesTaxLineAccountId = null;
            else
                invoice.SalesTaxLineAccountId = ((Account)View.m_cmbTaxAccount.SelectedItem).AccountId;

            if (View.m_cmbShippingAccount.SelectedIndex == 0)
                invoice.ShippingLineAccountId = null;
            else
                invoice.ShippingLineAccountId = ((Account)View.m_cmbShippingAccount.SelectedItem).AccountId;

            
            try
            {
                using (WaitCursor waitCursor = new WaitCursor())
                {
                    Model.Save(invoice);
                }
            }
            catch (Exception e)
            {
                EventService.AddEvent(
                    new QuickBooksAgentException("Unable to save invoice", e));

                return false;
            }

            return true;
        }

        #endregion  

        #region IsDefaultActionExist

        public override bool IsDefaultActionExist
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region DefaultActionName

        public override string DefaultActionName
        {
            get
            {
                return "Cancel";
            }
        }

        #endregion

        #region OnDefaultAction

        public override void OnDefaultAction()
        {
            bool cancel = false;

            if (IsDirty && (Model.Invoice == null || 
                            Model.Invoice.EntityState != EntityState.Synchronized))
                cancel = base.OnCancel("Do you want discard changes?");

            m_isCancelled = !cancel;

            if (!cancel)
                View.Destroy();
        }

        #endregion

        #region OnAddChargeClick

        private void OnAddChargeClick(object sender, EventArgs e)
        {
            InvoiceLineController invoiceLineController
                = Prepare<InvoiceLineController>(Model);
            invoiceLineController.Closed += new SingleFormClosedHandler(OnInvoiceLineAddClosed);
            invoiceLineController.Execute();
        }

        private void OnInvoiceLineAddClosed(SingleFormController controller)
        {
            if (!((InvoiceLineController)controller).IsCanceled)
            {
                View.m_tabs.SelectedIndex = 1;
                View.m_table.Focus();
                View.m_table.Select(0);            
            }                
        }

        #endregion

        #region OnDeleteChargeClick

        private void OnDeleteChargeClick(object sender, EventArgs e)
        {
            Model.DeleteInvoiceLine(
                (InvoiceLine) Model.GetObjectAt(View.m_table.CurrentRowIndex, 0));

            if (Model.GetRowCount() <= 0)
            {
                View.m_menuEditNewCharge.Enabled = false;
                View.m_menuDeleteNewCharge.Enabled = false;
            }
        }

        #endregion

        #region OnEditChargeClick

        private void OnEditChargeClick(object sender, EventArgs e)
        {
            InvoiceLineController invoiceLineController
                = Prepare<InvoiceLineController>(Model, Model.GetObjectAt(View.m_table.CurrentRowIndex, 0));
            invoiceLineController.Closed += new SingleFormClosedHandler(OnInvoiceLineEditClosed);
            invoiceLineController.Execute();
        }

        private void OnInvoiceLineEditClosed(SingleFormController controller)
        {
            View.m_table.Focus();
        }

        #endregion



        #region OnInvoiceLineRowChanged

        private void OnInvoiceLineRowChanged(int rowIndex)
        {
            if (Model.GetRowCount() > 0 && rowIndex >= 0)
            {
                View.m_menuEditNewCharge.Enabled = true;
                View.m_menuDeleteNewCharge.Enabled = true;
            }
            else
            {
                View.m_menuEditNewCharge.Enabled = false;
                View.m_menuDeleteNewCharge.Enabled = false;
            }
            
            if (Model.IsReadOnly)
                View.m_menuDeleteNewCharge.Enabled = false;

        }

        #endregion

        #region OnTableInvoiceLineFocusChanged

        private void OnTableInvoiceLineFocusChanged(object sender, EventArgs e)
        {
            if (!View.m_table.Focused)
            {
                View.m_menuEditNewCharge.Enabled = false;
                View.m_menuDeleteNewCharge.Enabled = false;
            }
            else if (Model.GetRowCount() > 0 && View.m_table.CurrentRowIndex >= 0)
            {
                View.m_menuEditNewCharge.Enabled = true;
                View.m_menuDeleteNewCharge.Enabled = true;
            }

            if (Model.IsReadOnly)
                View.m_menuDeleteNewCharge.Enabled = false;
            
        }

        #endregion

        #region OnTableInvoiceLineEnter

        private void OnTableInvoiceLineEnter(TableCell cell)
        {
            if (Model.GetRowCount() > 0 && View.m_table.CurrentRowIndex >= 0)
                OnEditChargeClick(this, EventArgs.Empty);
        }

        #endregion

        #region OnTabChanged

        private void OnTabChanged(object sender, EventArgs e)
        {
            if (View.m_tabs.SelectedIndex != 1)
            {
                View.m_menuDeleteNewCharge.Enabled = false;
                View.m_menuEditNewCharge.Enabled = false;
            }

            if (Model.IsReadOnly)
                View.m_menuDeleteNewCharge.Enabled = false;            
        }

        #endregion

        #region OnTotalAmountInvoiceLineChanged

        private void OnTotalAmountInvoiceLineChanged(decimal amount)
        {
            View.m_lblSubtotalNewChargesValue.Text = amount.ToString("C");
        }

        #endregion


        ///////////Totals///////////////////

        #region OnSubtotalNewChargesValueChanged

        private void OnSubtotalNewChargesValueChanged(object sender, EventArgs e)
        {
            View.m_lblSubtotalValue.Text = View.m_lblSubtotalNewChargesValue.Text;
        }

        #endregion

        #region Discounts

        private void OnDiscountPercentChanged(object sender, EventArgs e)
        {
            if (!IsDiscountPercentValid(true))
                return;

            View.m_curDiscountAmount.TextChanged -= new EventHandler(OnDiscountAmountChanged);
            if (GetDiscountPercent() != null)
            {
                
                View.m_curDiscountAmount.Value =
                    (decimal)-0.01 * GetDiscountPercent() * decimal.Parse(View.m_lblSubtotalValue.Text, NumberStyles.Currency);                
            } else
            {
                View.m_curDiscountAmount.Value = null;
            }
            View.m_curDiscountAmount.TextChanged += new EventHandler(OnDiscountAmountChanged);
            RecalculateTaxSubtotal();
            UpdateBalanceDue();
        }
        
        private bool IsDiscountPercentValid(bool showUserMessage)
        {                        
            try
            {                
                if (View.m_txtDiscountPercent.Text != string.Empty)
                {                    
                    decimal discountPercent = decimal.Parse(View.m_txtDiscountPercent.Text);
                    if (discountPercent < 0 || discountPercent > 100)
                    {
                        if (showUserMessage)
                        {
                            MessageDialog.Show(MessageDialogType.Information, "Discount percentage must be between 0% and 100%.");
                            View.m_tabs.SelectedIndex = 4;
                            View.m_txtDiscountPercent.SelectAll();
                            View.m_txtDiscountPercent.Focus();                            
                        }
                        return false;                        
                    }
                }                                    
            }
            catch (Exception)
            {
                if (showUserMessage)
                {
                    MessageDialog.Show(MessageDialogType.Information, "Invalid Discount percentage value.");
                    View.m_tabs.SelectedIndex = 4;
                    View.m_txtDiscountPercent.SelectAll();
                    View.m_txtDiscountPercent.Focus();
                }
                return false;
            }
            return true;            
        }

        private decimal? GetDiscountPercent()
        {
            if (!IsDiscountPercentValid(false))
                return null;

            if (View.m_txtDiscountPercent.Text == string.Empty)
                return null;

            return decimal.Parse(View.m_txtDiscountPercent.Text);
        }

        private void OnDiscountAmountChanged(object sender, EventArgs e)
        {
            View.m_txtDiscountPercent.TextChanged -= new EventHandler(OnDiscountPercentChanged);
            View.m_txtDiscountPercent.Text = string.Empty;
            View.m_txtDiscountPercent.TextChanged += new EventHandler(OnDiscountPercentChanged);
            if (View.m_curDiscountAmount.Value > 0)
                View.m_curDiscountAmount.Value *= -1;
            
            RecalculateTaxSubtotal();
            UpdateBalanceDue();
        }
        
        
        #endregion

        #region OnSubtotalChanged

        private void OnSubtotalChanged(object sender, EventArgs e)
        {
            if (View.m_txtDiscountPercent.Text != string.Empty)
                OnDiscountPercentChanged(sender, e);
            RecalculateTaxSubtotal();
            UpdateBalanceDue();
        }

        #endregion

        #region OnCustomerTaxableChanged

        private void OnCustomerTaxableChanged(object sender, EventArgs e)
        {
            View.m_lblCalcTaxSubtotal.Enabled = View.m_chkIsCustomerTaxable.Checked;
            View.m_cmbCalcTaxSubtotal.Enabled = View.m_chkIsCustomerTaxable.Checked;
            View.m_lblTaxSubtotal.Enabled = View.m_chkIsCustomerTaxable.Checked;
            View.m_lblTaxSubtotalAmount.Enabled = View.m_chkIsCustomerTaxable.Checked;
            View.m_lblTax.Enabled = View.m_chkIsCustomerTaxable.Checked;
            View.m_txtTaxPercent.Enabled = View.m_chkIsCustomerTaxable.Checked;
            View.m_lblPercentSignTax.Enabled = View.m_chkIsCustomerTaxable.Checked;
            View.m_curTaxAmount.Enabled = View.m_chkIsCustomerTaxable.Checked;
            RecalculateTaxSubtotal();
        }

        #endregion

        #region Taxes

        #region OnInvoiceLinesChanged

        private void OnInvoiceLinesChanged()
        {
            RecalculateTaxSubtotal();
        }

        #endregion

        #region OnCalcTaxSubtotalChanged

        private void OnCalcTaxSubtotalChanged(object sender, EventArgs e)
        {
            RecalculateTaxSubtotal();
        }

        #endregion

        #region RecalculateTaxSubtotal

        private void RecalculateTaxSubtotal()
        {
            if (View.m_chkIsCustomerTaxable.Checked)
            {
                if (View.m_cmbCalcTaxSubtotal.SelectedIndex == 1) //Before discount
                {
                    View.m_lblTaxSubtotalAmount.Text = Model.GetTaxableInvoiceLinesTotal().ToString("C");
                }
                else //After discount
                {
                    if (View.m_curDiscountAmount.Value != null)
                    {
                        View.m_lblTaxSubtotalAmount.Text
                            = (Model.GetTaxableInvoiceLinesTotal() + View.m_curDiscountAmount.Value.Value).ToString("C");
                        
                    } else
                    {
                        View.m_lblTaxSubtotalAmount.Text = Model.GetTaxableInvoiceLinesTotal().ToString("C");
                    }                                                         
                }
                                                                
            } else
            {
                View.m_lblTaxSubtotalAmount.Text = decimal.Zero.ToString("C");
            }
        }

        #endregion

        #region Tax percent and Tax amount

        private bool IsTaxPercentValid(bool showUserMessage)
        {
            try
            {
                if (View.m_txtTaxPercent.Text != string.Empty)
                {
                    decimal taxPercent = decimal.Parse(View.m_txtTaxPercent.Text);
                    if (taxPercent < 0 || taxPercent > 100)
                    {
                        if (showUserMessage)
                        {
                            MessageDialog.Show(MessageDialogType.Information, "Tax percentage must be between 0% and 100%.");
                            View.m_tabs.SelectedIndex = 4;
                            View.m_txtTaxPercent.SelectAll();
                            View.m_txtTaxPercent.Focus();
                        }
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                if (showUserMessage)
                {
                    MessageDialog.Show(MessageDialogType.Information, "Invalid Tax percentage value.");
                    View.m_tabs.SelectedIndex = 4;
                    View.m_txtTaxPercent.SelectAll();
                    View.m_txtTaxPercent.Focus();
                }
                return false;
            }
            return true;
        }

        private decimal? GetTaxPercent()
        {
            if (!IsTaxPercentValid(false))
                return null;

            if (View.m_txtTaxPercent.Text == string.Empty)
                return null;

            return decimal.Parse(View.m_txtTaxPercent.Text);
        }
        

        private void OnTaxPercentChanged(object sender, EventArgs e)
        {
            if (!IsTaxPercentValid(true))
                return;

            View.m_curTaxAmount.TextChanged -= new EventHandler(OnTaxAmountChanged);
            if (GetTaxPercent() != null)
            {
                View.m_curTaxAmount.Value =
                    (decimal)0.01 * GetTaxPercent() * decimal.Parse(View.m_lblTaxSubtotalAmount.Text, NumberStyles.Currency);
            }
            else
            {
                View.m_curTaxAmount.Value = null;
            }
            View.m_curTaxAmount.TextChanged += new EventHandler(OnTaxAmountChanged);
            UpdateBalanceDue();
        }
        

        private void OnTaxAmountChanged(object sender, EventArgs e)
        {
            View.m_txtTaxPercent.TextChanged -= new EventHandler(OnTaxPercentChanged);
            View.m_txtTaxPercent.Text = string.Empty;
            View.m_txtTaxPercent.TextChanged += new EventHandler(OnTaxPercentChanged);
            UpdateBalanceDue();
        }

        private void OnTaxSubtotalChanged(object sender, EventArgs e)
        {
            if (IsTaxPercentValid(false) && GetTaxPercent() != null)            
                OnTaxPercentChanged(sender, e);
        }
        
        #endregion

        #region UpdateBalanceDue

        private void UpdateBalanceDue()
        {
            decimal balanceDue = decimal.Zero;

            balanceDue += decimal.Parse(View.m_lblSubtotalNewChargesValue.Text, NumberStyles.Currency);
            if (View.m_curDiscountAmount.Value != null)
                balanceDue += View.m_curDiscountAmount.Value.Value;
            if (View.m_curTaxAmount.Value != null)
                balanceDue += View.m_curTaxAmount.Value.Value;
            if (View.m_curShipping.Value != null)
                balanceDue += View.m_curShipping.Value.Value;

            View.m_lblBalanceDueAmount.Text = balanceDue.ToString("C");            
        }

        #endregion

        #region OnShippingChanged

        private void OnShippingChanged(object sender, EventArgs e)
        {
            UpdateBalanceDue();
        }

        #endregion


        #endregion
    }
}
