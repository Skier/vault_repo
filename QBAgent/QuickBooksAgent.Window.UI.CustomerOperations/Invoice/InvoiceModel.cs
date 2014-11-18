using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using QuickBooksAgent.Data;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI;
using QuickBooksAgent.Windows.UI.Controls;
using QuickBooksAgent.Windows.UI.CustomerOperations.InvoiceSelection;

namespace QuickBooksAgent.Windows.UI.CustomerOperations.Invoice
{
    public class InvoiceModel : ITableModel, IModel
    {
        #region Events
        
        public event InvoiceAffectHandler InvoiceAffected;
        
        internal delegate void InvoiceLinesAmountChangedHandler (decimal amount);
        internal event InvoiceLinesAmountChangedHandler InvoiceLinesAmountChanged;

        internal delegate void InvoiceLinesChangedHandler();
        internal event InvoiceLinesChangedHandler InvoiceLinesChanged;
        
        
        #endregion

        #region Fields

        #region InvoiceLines

        List<InvoiceLine> m_invoiceLines;
        List<InvoiceLine> m_serviceLines; //Synchronized taxes, discounts etc

        #endregion

        #region Customer

        Customer m_customer;        
        public Customer Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        #endregion        

        #region Invoice

        Domain.Invoice m_invoice;
        public Domain.Invoice Invoice
        {
            get { return m_invoice; }
            set { m_invoice = value; }
        }

        #endregion

        #region IsReadOnly

        public bool IsReadOnly
        {
            get
            {
                if (Invoice == null)
                    return false;                                
                else if (Invoice.EntityState == EntityState.Synchronized)
                    return true;
                else
                    return false;
            }

        }

        #endregion

        #region AccountsList

        private List<Account> m_accountsList;
        public List<Account> AccountsList
        {
            get { return m_accountsList; }
        }

        #endregion

        #endregion

        #region IModel Members

        public void Init()
        {            
            m_invoiceLines = new List<InvoiceLine>();
            m_serviceLines = new List<InvoiceLine>();

            Account defaultAccount = new Account();
            defaultAccount.Name = "(Default)";
            m_accountsList = Account.Find();
            m_accountsList.Insert(0, defaultAccount); //Default Account

            if (Invoice != null)            
            {
                List<InvoiceLine> invoiceLineList = 
                    InvoiceLine.FindBy(Invoice.InvoiceId);

                m_serviceLines = InvoiceLine.FindServiceLinesBy(Invoice.InvoiceId);
                
                m_invoiceLines = invoiceLineList;                
                Invoice.InvoiceLines = invoiceLineList;
            }
        }

        #endregion

        #region Invoice Lines TableModel

        public int GetRowCount()
        {
            return m_invoiceLines.Count;
        }

        public int GetColumnCount()
        {
            return 2;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex == 0)
                return "Product/Service";

            return "Amount";
        }

        public Type GetColumnClass(int columnIndex)
        {
            if (columnIndex == 1)
                return typeof(decimal);

            return String.Empty.GetType();
        }

        public bool IsCellEditable(int rowIndex, int columnIndex)
        {
            return false;
        }

        public object GetValueAt(int rowIndex, int columnIndex)
        {
            if (columnIndex == 0)
            {
                if (m_invoiceLines[rowIndex].Item == null)
                    return string.Empty;
                else
                    return m_invoiceLines[rowIndex].Item.Name ?? string.Empty;
            }
            else
            {
                if (m_invoiceLines[rowIndex].Amount == null)
                    return decimal.Zero.ToString("C");
                else
                    return m_invoiceLines[rowIndex].Amount.Value.ToString("C");
            }
        }

        public void SetValueAt(object aValue, int rowIndex, int columnIndex)
        {            
        }

        public object GetObjectAt(int rowIndex, int columnIndex)
        {
            return m_invoiceLines[rowIndex];
        }

        public event TableModelChangeHandler Change;

        #endregion

        #region IsInvoiceLinesExist

        public bool IsInvoiceLinesExist()
        {
            return m_invoiceLines.Count > 0;
        }

        #endregion

        #region Save

        public void Save(Domain.Invoice invoice)
        {
            Debug.Assert(!Database.UnderTransaction, "System transaction has already started");

            Database.Begin();

            try
            {
                if (Invoice == null)
                {
                    Counter.Assign(invoice);

                    invoice.EntityState = EntityState.Created;
                    invoice.Customer = Customer;
                    invoice.EditSequence = 0;
                    invoice.TimeCreated = DateTime.Now;
                    invoice.TimeModified = DateTime.Now;

                    QuickBooksAgent.Domain.Invoice.Insert(invoice);
                    
                    foreach (InvoiceLine invoiceLine in m_invoiceLines)
                    {
                        if (invoiceLine.Quantity == 0)
                            continue;

                        Counter.Assign(invoiceLine);

                        invoiceLine.Invoice = invoice;
                        InvoiceLine.Insert(invoiceLine);
                    }                    
                    
                }
                else
                {
                    invoice.InvoiceId = Invoice.InvoiceId;
                    invoice.EditSequence = Invoice.EditSequence;
                    invoice.TimeModified = DateTime.Now;
                    invoice.TimeCreated = Invoice.TimeCreated;
                    invoice.Customer = Customer;

                    InvoiceLine.DeleteByInvoice(invoice.InvoiceId);
                    QuickBooksAgent.Domain.Invoice.Update(invoice);                    

                    foreach (InvoiceLine invoiceLine in m_invoiceLines)
                    {
                        if (invoiceLine.Quantity <= 0)
                            continue;

                        Counter.Assign(invoiceLine);

                        invoiceLine.Invoice = invoice;
                        InvoiceLine.Insert(invoiceLine);
                    }                                        
                }

                if (InvoiceAffected != null)
                    InvoiceAffected.Invoke(invoice);

                Database.Commit();
            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                if (Database.UnderTransaction)
                    Database.Rollback();
            }
        }

        #endregion

        #region InvokeChange

        public void InvokeChange()
        {
            if (Change != null)
            {
                Change.Invoke();
            }
        }

        #endregion        

        #region AddInvoiceLine

        internal void AddInvoiceLine(InvoiceLine invoiceLine)
        {            
            m_invoiceLines.Insert(0, invoiceLine);
            UpdateTable();
        }

        #endregion                              
        
        #region DeleteInvoiceLine

        internal void DeleteInvoiceLine(InvoiceLine invoiceLine)
        {
            m_invoiceLines.Remove(invoiceLine);
            UpdateTable();            
        }

        #endregion

        #region UpdateTable

        public void UpdateTable()
        {
            UpdateLinesTotalAmount();
            if (Change != null)
                Change.Invoke();
            
            if (InvoiceLinesChanged != null)
                InvoiceLinesChanged.Invoke();
        }

        #endregion

        #region UpdateLinesTotalAmount

        private void UpdateLinesTotalAmount()
        {
            decimal totalAmount = 0;
            foreach (InvoiceLine invoiceLine in m_invoiceLines)
                totalAmount += invoiceLine.Amount ?? decimal.Zero;

            if (InvoiceLinesAmountChanged != null)
                InvoiceLinesAmountChanged.Invoke(totalAmount);
        }

        #endregion

        #region GetTaxableInvoiceLinesTotal

        public decimal GetTaxableInvoiceLinesTotal()
        {
            decimal result = decimal.Zero;
            foreach (InvoiceLine line in m_invoiceLines)
            {
                if (line.IsTaxable && line.Amount != null)
                    result += line.Amount.Value;
            }

            return result;
        }

        #endregion

        #region Synchronized Discount, Tax and Shipping as Invoice Line

        public InvoiceLine GetSynchronizedDiscount()
        {
            foreach (InvoiceLine line in m_serviceLines)
            {
                if (line.LineDescription == "Discount")
                    return line;
            }
            return null;
        }
        
        public InvoiceLine GetSynchronizedTax()
        {
            foreach (InvoiceLine line in m_serviceLines)
            {
                if (line.LineDescription == "Tax")
                    return line;
            }
            return null;            
        }
        
        public InvoiceLine GetSynchronizedShipping()
        {
            foreach (InvoiceLine line in m_serviceLines)
            {
                if (line.LineDescription == "Shipping")
                    return line;
            }
            return null;                        
        }

        #endregion
    }
}
