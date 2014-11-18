using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;

namespace QuickBooksAgent.Windows.UI.CustomerOperations.Invoice
{
    public class InvoiceLineController : SingleFormController<InvoiceLineModel, InvoiceLineView>
    {
        private List<Item> m_items;
        private decimal? m_currentQty; //Null whe undefined
        private decimal? m_currentRate; //Null whe undefined
        
        #region Fields

        #region IsCanceled

        private bool m_isCanceled;
        public bool IsCanceled
        {
            get { return m_isCanceled; }
            set { m_isCanceled = value; }
        }

        #endregion

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.InvoiceModel = (InvoiceModel) data[0];

            if (data.Length >= 2)
                Model.InvoiceLine = (InvoiceLine) data[1];
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsDefaultActionExist = true;
            DefaultActionName = "Cancel";

            m_items = Item.Find();
            //m_items.Insert(0, new Item(0, 0, 0, String.Empty, 0)); //Empty Item
            foreach (Item item in m_items)
            {
                View.m_cmbProductService.Items.Add(item);
            }            
            
            if (Model.InvoiceLine != null)
            {
                if (Model.InvoiceLine.ServiceDate != null)
                    View.m_dtpServiceDate.Value = Model.InvoiceLine.ServiceDate;
                else
                    View.m_dtpServiceDate.Value = null;
                
                if (Model.InvoiceLine.Item != null)
                    View.m_cmbProductService.SelectedItem = Model.InvoiceLine.Item;

                View.m_txtDescription.Text = Model.InvoiceLine.LineDescription;

                if (Model.InvoiceLine.Quantity != null)
                {
                    View.m_txtQty.Text = QBDataType.RoundTripFormat(Model.InvoiceLine.Quantity);
                    m_currentQty = Model.InvoiceLine.Quantity;
                }                    

                if (Model.InvoiceLine.Rate != null)
                {
                    View.m_txtRate.Text = QBDataType.RoundTripFormat(Model.InvoiceLine.Rate);
                    m_currentRate = Model.InvoiceLine.Rate;
                }                    
                else if (Model.InvoiceLine.RatePercent != null)
                {
                    View.m_txtRate.Text = QBDataType.RoundTripFormat(Model.InvoiceLine.RatePercent);
                    m_currentRate = Model.InvoiceLine.RatePercent;
                    View.m_chkRateAsPercent.Checked = true;
                }

                View.m_chkTax.Checked = Model.InvoiceLine.IsTaxable;
                View.m_curAmount.Value = Model.InvoiceLine.Amount;                    
                View.Text = "Edit Charge - Q-Agent";
                
                if (Model.InvoiceModel.IsReadOnly)
                {
                    View.Enabled = false;
                    View.Text = "View Charge - Q-Agent";
                } 
                    
            } else
            {
                View.Text = "Add Charge - Q-Agent";
                View.m_dtpServiceDate.Value = null;
            }

            View.m_cmbProductService.Focus();            
            
            //Handlers             
            
            View.m_cmbProductService.SelectedIndexChanged += new EventHandler(OnProductServiceChanged);
            View.m_txtQty.TextChanged += new EventHandler(OnQtyChanged);
            View.m_txtRate.TextChanged += new EventHandler(OnRateChanged);
            View.m_chkRateAsPercent.CheckStateChanged += new EventHandler(OnRateAsPercentChanged);
            View.m_curAmount.TextChanged += new EventHandler(OnAmountChanged);            
        }

        #endregion
                

        #region OnProductServiceChanged

        private void OnProductServiceChanged(object sender, EventArgs e)
        {
            Item item = (Item) View.m_cmbProductService.SelectedItem;
            
            if (View.m_txtQty.Text == String.Empty)
            {
                View.m_txtQty.Text = "1";
                m_currentQty = 1;
            }


            View.m_txtRate.Text = QBDataType.RoundTripFormat(item.SalesPrice);
        }

        #endregion

        #region Qty and rate validation

        private bool IsQtyValid()
        {
            if (View.m_txtQty.Text == "-")
            {
                m_currentQty = null;
                return true;
            }                
            
            try
            {
                if (View.m_txtQty.Text != string.Empty)
                    m_currentQty = decimal.Parse(View.m_txtQty.Text);
                else
                    m_currentQty = null;
                
            }
            catch (OverflowException)
            {
                MessageDialog.Show(MessageDialogType.Information, "Quantity is too long.");
                View.m_txtQty.SelectAll();
                View.m_txtQty.Focus();
                m_currentQty = null;
                return false;                
            }
            catch (Exception)
            {
                MessageDialog.Show(MessageDialogType.Information, "Incorrect Quantity.");
                View.m_txtQty.SelectAll();
                View.m_txtQty.Focus();
                m_currentQty = null;
                return false;
            }
            
            return true;
        }

        private bool IsRateValid()
        {
            if (View.m_txtRate.Text == "-")
            {
                m_currentRate = null;
                return true;
            }                
            
            try
            {
                if (View.m_txtRate.Text != string.Empty)
                {
                    m_currentRate = decimal.Parse(View.m_txtRate.Text);
                    if (View.m_chkRateAsPercent.Checked)
                        m_currentRate *= (decimal)0.01;
                }                                    
                else
                    m_currentRate = null;
            }
            catch(OverflowException)
            {
                MessageDialog.Show(MessageDialogType.Information, "Rate is too long.");
                View.m_txtRate.SelectAll();
                View.m_txtRate.Focus();
                m_currentRate = null;
                return false;                
            }
            catch (Exception)
            {
                MessageDialog.Show(MessageDialogType.Information, "Incorrect Rate.");
                View.m_txtRate.SelectAll();
                View.m_txtRate.Focus();
                m_currentRate = null;
                return false;
            }
                        
            return true;
        }

        #endregion

        #region IsAmountValid

        private bool IsAmountValid()
        {
            if (!QBDataType.IsInFormat(View.m_curAmount.Value, 8, 2))
            {
                MessageDialog.Show(MessageDialogType.Information, "Amount maximum allowed length is 10 digits (in 8.2 format).");
                View.m_curAmount.SelectAll();
                View.m_curAmount.Focus();
                return false;
            }

            return true;
        }

        #endregion
        
        
        #region OnQtyChanged
                       
        private void OnQtyChanged(object sender, EventArgs e)
        {
            IsQtyValid();

            View.m_curAmount.TextChanged -= new EventHandler(OnAmountChanged);
            
            if (m_currentRate == null)
                View.m_curAmount.Value = 0;
            else if (m_currentQty == null)
                View.m_curAmount.Value = m_currentRate;
            else
            {
                try
                {
                    View.m_curAmount.Value = m_currentRate * m_currentQty;
                }
                catch (OverflowException)
                {
                    MessageDialog.Show(MessageDialogType.Information, "Quantity is too long.");
                    View.m_txtQty.SelectAll();
                    View.m_txtQty.Focus();
                }
            }
                                                    
            View.m_curAmount.TextChanged += new EventHandler(OnAmountChanged);
        }

        #endregion

        #region OnRateChanged

        private void OnRateChanged(object sender, EventArgs e)
        {
            IsRateValid();

            View.m_curAmount.TextChanged -= new EventHandler(OnAmountChanged);

            if (m_currentRate == null)
                View.m_curAmount.Value = 0;
            else if (m_currentQty == null)
                View.m_curAmount.Value = m_currentRate;
            else
            {
                try
                {
                    View.m_curAmount.Value = m_currentRate * m_currentQty;
                }
                catch (OverflowException)
                {
                    MessageDialog.Show(MessageDialogType.Information, "Rate is too long.");
                    View.m_txtRate.SelectAll();
                    View.m_txtRate.Focus();
                }
            }
            View.m_curAmount.TextChanged += new EventHandler(OnAmountChanged);
        }

        #endregion

        #region OnRateAsPercentChanged

        private void OnRateAsPercentChanged(object sender, EventArgs e)
        {
            OnRateChanged(sender, e);
        }

        #endregion

        #region OnAmountChanged

        private void OnAmountChanged(object sender, EventArgs e)
        {
            if (m_currentQty != null && m_currentQty != decimal.Zero)
            {
                View.m_txtRate.TextChanged -= new EventHandler(OnRateChanged);
                
                try
                {
                    m_currentRate = View.m_curAmount.Value / m_currentQty;
                    View.m_txtRate.Text = QBDataType.RoundTripFormat(decimal.Round(m_currentRate.Value, 5));
                }
                catch (OverflowException)
                {
                    MessageDialog.Show(MessageDialogType.Information, "Amount is too long.");
                    View.m_curAmount.Focus();
                }
                
                View.m_txtRate.TextChanged += new EventHandler(OnRateChanged);
            }                                            
        }

        #endregion
               

        #region OnDefaultAction

        public override void OnDefaultAction()
        {
            m_isCanceled = true;
            View.Destroy();
        }

        #endregion        
        
        #region OnSave

        protected override bool OnSave()
        {
            if (Model.InvoiceModel.IsReadOnly)
                return true;
            
            if (!IsFormValid())
                return false;

            m_isCanceled = false;

            if (Model.InvoiceLine != null)
            {
                SetEntityFromUI(Model.InvoiceLine);
                Model.InvoiceModel.UpdateTable();
                return true;
            }

            InvoiceLine invoiceLine = new InvoiceLine();
            SetEntityFromUI(invoiceLine);            
            Model.InvoiceModel.AddInvoiceLine(invoiceLine);

            return true;
        }
        
        private void SetEntityFromUI(InvoiceLine invoiceLine)
        {
            if (View.m_dtpServiceDate.Value == null)
                invoiceLine.ServiceDate = null;
            else
                invoiceLine.ServiceDate = (DateTime?) View.m_dtpServiceDate.Value;

            invoiceLine.Item = (Item) View.m_cmbProductService.SelectedItem;

            if (View.m_txtDescription.Text == string.Empty)
                invoiceLine.LineDescription = null;
            else
                invoiceLine.LineDescription = View.m_txtDescription.Text;

            if (View.m_txtQty.Text == string.Empty)
                invoiceLine.Quantity = null;
            else
                invoiceLine.Quantity = decimal.Parse(View.m_txtQty.Text);

            if (View.m_chkRateAsPercent.Checked)
            {
                invoiceLine.Rate = null;
                if (View.m_txtRate.Text == string.Empty)
                    invoiceLine.RatePercent = null;
                else
                    invoiceLine.RatePercent = decimal.Parse(View.m_txtRate.Text);
                
            } else
            {
                invoiceLine.RatePercent = null;
                if (View.m_txtRate.Text == string.Empty)
                    invoiceLine.Rate = null;
                else
                    invoiceLine.Rate = decimal.Parse(View.m_txtRate.Text);                
            }

            invoiceLine.IsTaxable = View.m_chkTax.Checked;
            invoiceLine.Amount = View.m_curAmount.Value;            
        }

        #endregion

        #region IsFormValid

        private bool IsFormValid()
        {
            if (!IsQtyValid())
                return false;
            
            //Qty extra validation
            if (!QBDataType.IsInFormat(m_currentQty, 7, 5))
            {
                MessageDialog.Show(MessageDialogType.Information, "Quantity maximum allowed length is 12 digits (in 7.5 format).");
                View.m_txtQty.SelectAll();
                View.m_txtQty.Focus();
                m_currentQty = null;
                return false;
            }            
            
            if (View.m_txtQty.Text == "-")
            {
                MessageDialog.Show(MessageDialogType.Information, "Incorrect Quantity.");
                View.m_txtQty.SelectAll();
                View.m_txtQty.Focus();
                m_currentQty = null;
                return false;                
            }
                
            
            if (!IsRateValid())
                return false;

            //Rate extra validation
            if (View.m_chkRateAsPercent.Checked)
            {
                if (!QBDataType.IsInFormat(m_currentRate, 7, 5))
                {
                    MessageDialog.Show(MessageDialogType.Information, "Rate percentage maximum allowed length is 12 digits (in 7.5 format).");
                    View.m_txtRate.SelectAll();
                    View.m_txtRate.Focus();
                    m_currentRate = null;
                    return false;
                }
            }
            else
            {
                if (!QBDataType.IsInFormat(m_currentRate, 8, 5))
                {
                    MessageDialog.Show(MessageDialogType.Information, "Rate maximum allowed length is 13 digits (in 8.5 format).");
                    View.m_txtRate.SelectAll();
                    View.m_txtRate.Focus();
                    m_currentRate = null;
                    return false;
                }
            }

            if (View.m_txtRate.Text == "-")
            {
                MessageDialog.Show(MessageDialogType.Information, "Incorrect Rate.");
                View.m_txtRate.SelectAll();
                View.m_txtRate.Focus();
                m_currentRate = null;
                return false;
            }
            
            if (!IsAmountValid())
                return false;
                        
            if (View.m_cmbProductService.SelectedIndex < 0)
            {
                MessageDialog.Show(MessageDialogType.Information, "Please specify Product/Service.");
                View.m_cmbProductService.Focus();
                return false;
            }
                            
            return true;
        }

        #endregion        
    }
}
