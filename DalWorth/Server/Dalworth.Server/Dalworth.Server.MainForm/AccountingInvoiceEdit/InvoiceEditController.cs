using System;
using System.Collections.Generic;
using System.Text;

using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Columns;

using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using Dalworth.Server.SDK;

namespace Dalworth.Server.MainForm.AccountingInvoiceEdit
{    
    class InvoiceEditController : Controller<InvoiceEditModel, InvoiceEditView>
    {
        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data.Length == 1 && data[0] != null)
            {
                Model.QbTransaction = (QbTransaction)data[0];
            }

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            base.OnInitialize();
            View.m_btnClose.Click += OnCloseInvoiceClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_lblPending.Visible = Model.QbTransaction.QbInvoice.IsPending;
            View.m_lblCustomer.Text = Model.Customer.DisplayName;
            View.m_lblProjectId.Text = Model.Project.ID.ToString();
            View.m_lblProjectType.Text = Model.Project.ProjectTypeText;
            View.m_lblClosedAmount.Text = Model.Project.ClosedAmount.ToString("C");
            View.m_lblQbClass.Text = Model.QbClass != null ? Model.QbClass.Name : string.Empty;
            View.m_lblQbAccount.Text = Model.QbAccount.FullName;
            View.m_lblQbTemplate.Text = Model.QbTemplate != null ? Model.QbTemplate.Name : string.Empty;

            View.m_lblInvoiceNumber.Text = Model.QbTransaction.QbInvoice.RefNumber;

            View.m_lblTxnDate.Text = Model.QbTransaction.QbInvoice.TxnDate.HasValue ? Model.QbTransaction.QbInvoice.TxnDate.Value.ToShortDateString() : "";
            View.m_lblTerms.Text = Model.QbInvoiceTerm != null ? Model.QbInvoiceTerm.Name : string.Empty;
            View.m_lblClaimNumber.Text = Model.Project.ClaimNumber;
            View.m_lblRep.Text = Model.QbSalesRep != null ? Model.QbSalesRep.FullName : string.Empty;

            View.m_ctlBillAddress.BaseAddress = Model.BillAddress;
            View.m_ctlBillAddress.CurrentAddress = Model.BillAddress;            
            View.m_ctlBillAddress.Caption = "Bill To Address";
            View.m_ctlBillAddress.Enabled = false;

            View.m_ctlShipAddress.BaseAddress = Model.ShipAddress;
            View.m_ctlShipAddress.CurrentAddress = Model.ShipAddress;
            View.m_ctlShipAddress.Caption = "Ship To Address";
            View.m_ctlShipAddress.Enabled = false;

            View.m_lblInvoiceTax.Text = Model.QbItemSalesTax.Name;
            View.m_gridInvoiceItems.DataSource = Model.InvoiceLines;

            View.m_lblInvoiceSubTotal.Text = Model.QbTransaction.QbInvoice.SubTotalAmount.ToString("C");
            View.m_lblInvoiceTaxAmount.Text = Model.QbTransaction.QbInvoice.TaxAmount.ToString("C");
            View.m_lblInvoiceTotal.Text = Model.QbTransaction.QbInvoice.TotalAmount.ToString("C");
            View.m_txtInvoiceMemo.Text = Model.QbTransaction.QbInvoice.Memo;
        }

        #endregion


        #region OnCloseInvoiceClick

        private void OnCloseInvoiceClick(object sender, EventArgs e)
        {
            View.Destroy();
        }

        #endregion
    }
}
