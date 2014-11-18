using System;
using System.Collections.Generic;
using System.Text;
using QuickBooksAgent.Domain;
using QuickBooksAgent.Windows.UI.Controls;

namespace QuickBooksAgent.Windows.UI.CustomerOperations.Invoice
{
    public class InvoiceLineModel : IModel
    {
        #region Fields

        #region InvoiceModel

        private InvoiceModel m_invoiceModel;
        public InvoiceModel InvoiceModel
        {
            get { return m_invoiceModel; }
            set { m_invoiceModel = value; }
        }

        #endregion

        #region InvoiceLine

        private InvoiceLine m_invoiceLine;
        public InvoiceLine InvoiceLine
        {
            get { return m_invoiceLine; }
            set { m_invoiceLine = value; }
        }

        #endregion        

        #endregion

        #region Init

        public void Init()
        {

        }

        #endregion
    }
}
