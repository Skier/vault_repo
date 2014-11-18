using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;
using Dalworth.Server.Windows;
using DevExpress.Data;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;

namespace Dalworth.Server.MainForm.MakePayment
{
    public class MakePaymentController : Controller<MakePaymentModel, MakePaymentView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.CompletePackage = (VisitCompletePackage) data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnOk.Click += OnOkClick;
            View.m_btnCancel.Click += OnCancelClick;            
            View.m_gridPaymentsView.CellValueChanging += OnGridPaymentsCellValueChanging;
            View.m_gridPaymentsView.RowCountChanged += OnGridPaymentsRowCountChanged;
            View.m_gridProjectsView.CellValueChanging += OnGridProjectsCellValueChanging;
            View.m_gridProjectsView.RowCountChanged += OnGridProjectsViewRowCountChanged;
            View.m_gridPaymentsView.KeyDown += OnPaymentsKeyDown;
            View.m_gridPayments.EmbeddedNavigator.ButtonClick += OnPaymentsButtonClick;
            View.m_gridPaymentsView.FocusedRowChanged += OnPaymentsFocusedRowChanged;            

            View.m_chkShowOtherProjects.CheckedChanged += OnShowOtherProjectsCheckedChanged;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_chkShowOtherProjects.Enabled = Model.OtherProjects.Count > 0;
            View.m_chkShowOtherProjects.Checked = Model.IsOtherProjectsPaidLastTime;

            View.m_gridPayments.DataSource = Model.Payments;            
            View.m_gridProjects.DataSource = Model.Projects;

            View.m_colProjectDate.Visible = View.m_chkShowOtherProjects.Checked;            
        }

        #endregion

        #region OnPaymentsKeyDown

        private void OnPaymentsKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                if (e.KeyCode == Keys.Insert && View.m_btnAddPayment.Enabled)
                {
                    View.m_gridPaymentsView.AddNewRow();

                    e.Handled = true;
                    e.SuppressKeyPress = true;

                }
                else if (e.KeyCode == Keys.Delete && View.m_btnDeletePayment.Enabled)
                {
                    NavigatorButtonClickEventArgs arg = new NavigatorButtonClickEventArgs(
                        View.m_btnDeletePayment);

                    OnPaymentsButtonClick(null, arg);

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            }            
        }

        #endregion

        #region OnPaymentsButtonClick

        private void OnPaymentsButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button == View.m_btnAddPayment)
                View.m_gridPaymentsView.CloseEditor();
            if (e.Button == View.m_btnDeletePayment)
                View.m_gridPaymentsView.DeleteSelectedRows();
        }

        #endregion

        #region OnPaymentsFocusedRowChanged

        private void OnPaymentsFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            View.m_btnDeletePayment.Enabled = e.FocusedRowHandle >= 0;
        }

        #endregion


        #region IsValid

        private bool IsValid()
        {
            foreach (PaymentInfo payment in Model.Payments)
            {
                payment.ShowPaymentMethodError = true;                                    
                payment.ShowAmountError = true;                                    
            }                
            Model.Payments.ResetBindings();            
            View.m_gridPayments.Update();

            foreach (PaymentInfo payment in Model.Payments)
            {
                if (!payment.IsValid)
                    return false;
            }

            if (Model.CurrentPaymentAmount - Model.CurrentDistibutedAmount != decimal.Zero)
            {
                XtraMessageBox.Show("Please distribute unapplied paid amount to available projects",
                                    "Unapplied payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Model.IsRecomplete && Model.Payments.Count == 0)
            {                
                XtraMessageBox.Show("Please add payment",
                    "No payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;                
            }

            return true;
        }

        #endregion

        #region OnGridPaymentsCellValueChanging

        private void OnGridPaymentsCellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == View.m_colAmount)
            {
                object row = View.m_gridPaymentsView.GetRow(e.RowHandle);
                if (row != null)
                {
                    PaymentInfo payment = (PaymentInfo)row;
                    payment.PaymentAmount = decimal.Parse(e.Value.ToString());
                    OnPaymentAmountChanged();
                    if (!payment.ShowAmountError)
                        payment.ShowAmountError = true;
                }
            }
        }

        #endregion

        #region OnPaymentAmountChanged

        private void OnPaymentAmountChanged()
        {
            if (Model.Projects.Count == 1 && !Model.IsRecomplete)
            {
                Model.Projects[0].PaidAmount = Model.CurrentPaymentAmount;
                Model.Projects.ResetItem(0);
            }

            if (Model.IsRecomplete && Model.Payments.Count == 0)
            {
                foreach (ProjectWrapper project in Model.Projects)
                    project.PaidAmount = decimal.Zero;
                Model.Projects.ResetBindings();
            }

            OnUnappliedAmountChanged();
        }

        private void OnGridPaymentsRowCountChanged(object sender, EventArgs e)
        {
            OnPaymentAmountChanged();            
        }

        private void OnGridProjectsViewRowCountChanged(object sender, EventArgs e)
        {
            OnPaymentAmountChanged();
        }

        #endregion

        #region OnUnappliedAmountChanged

        private void OnUnappliedAmountChanged()
        {
            decimal unappliedAmount = Model.CurrentPaymentAmount - Model.CurrentDistibutedAmount;
            View.m_lblUnappliedAmount.Text = unappliedAmount.ToString("C");            
            View.m_lblUnappliedAmount.ForeColor 
                = unappliedAmount == decimal.Zero ? Color.Black : Color.Red;
        }

        private void OnGridProjectsCellValueChanging(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column == View.m_colProjectPaidAmount)
            {
                object row = View.m_gridProjectsView.GetRow(e.RowHandle);
                if (row != null)
                {
                    Project project = (Project)row;
                    project.PaidAmount = decimal.Parse(e.Value.ToString());
                    OnUnappliedAmountChanged();
                }
            }
        }

        #endregion

        #region OnShowOtherProjectsCheckedChanged

        private void OnShowOtherProjectsCheckedChanged(object sender, EventArgs e)
        {
            Model.RefreshProjects(View.m_chkShowOtherProjects.Checked);
            View.m_colProjectDate.Visible = View.m_chkShowOtherProjects.Checked;
        }

        #endregion


        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            if (!IsValid())
                return;
            
            Model.PutPaymentInfoToCompletePackage();

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
