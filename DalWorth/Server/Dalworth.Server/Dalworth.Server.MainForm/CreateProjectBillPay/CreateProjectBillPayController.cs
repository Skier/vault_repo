using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace Dalworth.Server.MainForm.CreateProjectBillPay
{
    public class CreateProjectBillPayController : Controller<CreateProjectBillPayModel, CreateProjectScopeView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region CreatedProjectBillPay

        public ProjectConstructionBillPay CreatedProjectBillPay
        {
            get { return Model.ProjectBillPay; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data != null && data.Length >= 1 && data[0] != null)
                Model.ProjectBillPay = (ProjectConstructionBillPay) data[0];
            else 
                Model.ProjectBillPay = new ProjectConstructionBillPay();

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnOk.Click += OnOkClick;
            View.m_btnCancel.Click += OnCancelClick;

            View.m_cmbTransactionType.SelectedValueChanged += OnTransactionTypeChanged;
            View.m_txtCollectedAmount.TextChanged += OnCollectedAmountChanged;

            View.m_cmbTransactionType.Validated += OnTransactionTypeValidate;
            View.m_txtNumber.Validated += OnNumberValidate;
            View.m_dtpPaymentDate.Validated += OnIssueDateValidate;
            View.m_txtCollectedAmount.Validated += OnAmountValidate;
            View.m_txtNotes.Validated += OnNotesValidate;

            View.m_txtNumber.Focus();
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_txtNumber.EditValue = Model.ProjectBillPay.Number;
            View.m_dtpPaymentDate.Properties.MaxValue = DateTime.Now.Date;
            View.m_txtCollectedAmount.EditValue = Model.ProjectBillPay.Amount;
            View.m_txtNotes.EditValue = Model.ProjectBillPay.Notes;

            foreach (ProjectConstructionBillPayType type in Model.BillPayTypes)
            {
                View.m_cmbTransactionType.Properties.Items.Add(new ImageComboBoxItem(type.BillPayType, (object)type.ID));
            }

            if (!Model.IsNewProjectBillPay)
            {
                View.Text = "Dalworth - Edit Bill/Pay";
                View.m_dtpPaymentDate.DateTime = Model.ProjectBillPay.IssueDate;

                View.m_cmbTransactionType.EditValue = (int)Model.ProjectBillPay.BillPayType;
                View.m_cmbTransactionType.Enabled = false;
                View.m_dtpPaymentDate.Enabled = false;
                View.m_txtNumber.Enabled = false;
                View.m_txtCollectedAmount.Enabled = false;
            }
            else
            {
                View.Text = "Dalworth - Create Bill/Pay";
                View.m_dtpPaymentDate.DateTime = DateTime.Now.Date;
            }
        }

        #endregion

        #region OnCollectedAmountChanged

        private void OnCollectedAmountChanged(object sender, EventArgs e)
        {
            if (View.m_dtpPaymentDate.EditValue == null)
                View.m_dtpPaymentDate.DateTime = DateTime.Now.Date;
        }

        #endregion

        #region OnTransactionTypeChanged

        private void OnTransactionTypeChanged(object sender, EventArgs e)
        {
            // to do - replace labels;
        }

        #endregion

        #region Validate

        private void OnTransactionTypeValidate(object sender, EventArgs e)
        {
            if (View.m_cmbTransactionType.SelectedItem == null)
            {
                View.m_errorProvider.SetError(View.m_cmbTransactionType, "Transaction Type can't be empty");
                return;
            }

            View.m_errorProvider.SetError(View.m_cmbTransactionType, string.Empty);
        }

        private void OnNumberValidate(object sender, EventArgs e)
        {
            int maxLenght = 50;

            if (View.m_txtNumber.EditValue != null 
                && View.m_txtNumber.EditValue.ToString().Length > maxLenght)
            {
                View.m_errorProvider.SetError(View.m_txtNumber, "Number can't be longer than " + maxLenght);
                return;
            }

            View.m_errorProvider.SetError(View.m_txtNumber, string.Empty);
        }

        private void OnIssueDateValidate(object sender, EventArgs e)
        {
            if (View.m_dtpPaymentDate.EditValue == null)
            {
                View.m_errorProvider.SetError(View.m_dtpPaymentDate, "Issue Date can't be empty");
                return;
            }

            View.m_errorProvider.SetError(View.m_dtpPaymentDate, string.Empty);
        }

        private void OnAmountValidate(object sender, EventArgs e)
        {
            if ((decimal)View.m_txtCollectedAmount.EditValue == 0)
            {
                View.m_errorProvider.SetError(View.m_txtCollectedAmount, "Amount can't be equal to zero");
                return;
            }
            else if ((decimal)View.m_txtCollectedAmount.EditValue < decimal.Zero)
            {
                View.m_errorProvider.SetError(View.m_txtCollectedAmount, "Amount can't be negative");
                return;
            }

            View.m_errorProvider.SetError(View.m_txtCollectedAmount, string.Empty);
        }

        private void OnNotesValidate(object sender, EventArgs e)
        {
            int maxLenght = 200;

            if (View.m_txtNotes.EditValue != null
                && View.m_txtNotes.EditValue.ToString().Length > maxLenght)
            {
                View.m_errorProvider.SetError(View.m_txtNotes, "Notes max lenght is " + maxLenght);
                return;
            }

            View.m_errorProvider.SetError(View.m_txtNotes, string.Empty);
        }

        #endregion


        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            View.ValidateChildren();
            if (View.m_errorProvider.HasErrors)
                return;

            ProjectConstructionBillPay billPay = Model.ProjectBillPay;

            if (View.m_cmbTransactionType.EditValue != null && (int)View.m_cmbTransactionType.EditValue != 0)
                billPay.ProjectConstructionBillPayTypeId = (int)View.m_cmbTransactionType.EditValue;

            if (View.m_txtNumber.EditValue != null)
                billPay.Number = (string)View.m_txtNumber.EditValue;
            else
                billPay.Number = string.Empty;

            if (View.m_dtpPaymentDate.EditValue != null)
                billPay.IssueDate = View.m_dtpPaymentDate.DateTime;
            else
                billPay.IssueDate = DateTime.Now;

            if (View.m_txtCollectedAmount.EditValue != null)
                billPay.Amount = (decimal)View.m_txtCollectedAmount.EditValue;
            else
                billPay.Amount = Decimal.Zero;

            if (View.m_txtNotes.EditValue != null)
                billPay.Notes = (string)View.m_txtNotes.EditValue;
            else
                billPay.Notes = string.Empty;

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
    }
}
