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

namespace Dalworth.Server.MainForm.AccountingCreditMemo
{
    class CreditMemoController : Controller<CreditMemoModel, CreditMemoView>
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
            View.m_btnClose.Click += OnCloseClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_lblCustomer.Text = Model.Customer.DisplayName;
            View.m_lblProjectId.Text = Model.Project.ID.ToString();
            View.m_lblProjectType.Text = Model.Project.ProjectTypeText;
            View.m_lblClosedAmount.Text = Model.Project.ClosedAmount.ToString("C");
            View.m_lblQbClass.Text = Model.QbClass != null ? Model.QbClass.Name : string.Empty;
            View.m_lblQbAccount.Text = Model.QbAccount.FullName;
            View.m_lblQbTemplate.Text = Model.QbTemplate != null ? Model.QbTemplate.Name : string.Empty;

            View.m_lblCreditNumber.Text = Model.QbTransaction.QbCreditMemo.RefNumber;
            View.m_lblDate.Text = Model.QbTransaction.CreatedDate.HasValue ?
                Model.QbTransaction.CreatedDate.Value.ToShortDateString() : string.Empty;
            View.m_lblTerms.Text = Model.QbTerm != null ? Model.QbTerm.Name : string.Empty;
            View.m_lblClaimNumber.Text = Model.Project.ClaimNumber;
            View.m_lblRep.Text = Model.QbSalesRep != null ? Model.QbSalesRep.FullName : string.Empty;

            View.m_ctlShipAddress.BaseAddress = Model.ShipAddress;
            View.m_ctlShipAddress.CurrentAddress = Model.ShipAddress;
            View.m_ctlShipAddress.Caption = "Ship To Address";
            View.m_ctlShipAddress.Enabled = false;

            View.m_lblSalesTaxName.Text = Model.QbItemSalesTax != null ? Model.QbItemSalesTax.Name : string.Empty;
            View.m_gridMemoLines.DataSource = Model.CreditMemoLines;

            View.m_lblSubTotal.Text = Model.QbTransaction.QbCreditMemo.SubTotalAmount.ToString("C");
            View.m_lblSalesTaxTotal.Text = Model.QbTransaction.QbCreditMemo.TaxAmount.ToString("C");
            View.m_lblTotalAmount.Text = Model.QbTransaction.QbCreditMemo.TotalAmount.ToString("C");
            View.m_txtMemo.Text = Model.QbTransaction.QbCreditMemo.Memo;
            View.m_lblRemainingCredit.Text = Model.QbTransaction.QbCreditMemo.CreditRemaining.ToString("C");
        }

        #endregion


        #region OnCloseClick

        private void OnCloseClick(object sender, EventArgs e)
        {
            View.Destroy();
        }

        #endregion
    }
}
