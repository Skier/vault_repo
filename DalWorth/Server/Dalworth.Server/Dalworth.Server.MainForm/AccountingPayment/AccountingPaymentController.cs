using System;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.AccountingPayment
{
    class AccountingPaymentController : Controller<AccountingPaymentModel, AccountingPaymentView>
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
            View.m_lblCheckNumber.Text = Model.QbTransaction.QbPayment.RefNumber;
            View.m_lblCustomer.Text = Model.Customer.DisplayName;
            View.m_lblDate.Text = Model.QbTransaction.CreatedDate.HasValue ?
                Model.QbTransaction.CreatedDate.Value.ToShortDateString() : string.Empty;
            View.m_lblQbAccount.Text = Model.QbAccount.FullName;
            View.m_lblQbPaymentMethod.Text = Model.QbPaymentMethod != null 
                ? Model.QbPaymentMethod.Name : string.Empty;

            if (Model.Project != null)
            {
                View.m_lblProjectId.Text = Model.Project.ID.ToString();
                View.m_lblProjectType.Text = Model.Project.ProjectTypeText;
                View.m_lblClaimNumber.Text = Model.Project.ClaimNumber;
                View.m_lblClosedAmount.Text = Model.Project.ClosedAmount.ToString("C");
            }

            View.m_txtMemo.Text = Model.QbTransaction.QbPayment.Memo;
            View.m_lblTotal.Text = Model.QbTransaction.QbPayment.TotalAmount.ToString("C");            
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
