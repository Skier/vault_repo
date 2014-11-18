using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using Dalworth.Data;
using Dalworth.Domain;
using Dalworth.Domain.SyncService;
using Dalworth.Windows.ServiceVisit.PayByCheckDone;
using Dalworth.Windows.ServiceVisit.ServiceVisit;
using WorkTransaction=Dalworth.Domain.WorkTransaction;
using WorkTransactionPaymentTypeEnum=Dalworth.Domain.WorkTransactionPaymentTypeEnum;

namespace Dalworth.Windows.ServiceVisit.PayByCheck
{
    public class PayByCheckController : SingleFormController<PayByCheckModel, PayByCheckView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
            set { m_isCancelled = value; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            Model.ServiceVisitModel = (ServiceVisitModel)data[0];
            Model.Amount = (decimal)data[1];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_txtFirstName.TextChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_txtLastName.TextChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_txtAddress.TextChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_txtCity.TextChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_cmbState.SelectedIndexChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_txtZip.TextChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_cmbAccountType.SelectedIndexChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_txtCompany.TextChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_txtBankName.TextChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_txtAccountNumber.TextChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_txtCheckNumber.TextChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_txtBankRouteNumber.TextChanged += new EventHandler(OnRequiredFieldChanged);              
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsLeftActionExist = true;
            IsRightActionExist = true;

            LeftActionName = "Cancel";
            RightActionName = "Done";

            View.m_cmbAccountType.SelectedIndex = 0;

            View.m_lblAmountDue.Text = Model.Amount.ToString("C");
            View.m_lblVisitNumber.Text = Model.ServiceVisitModel.Visit.Tasks[0].Task.Number;
            View.m_lblCustomerName.Text = Model.ServiceVisitModel.Visit.Customer.FirstName + ", " +
                                          Model.ServiceVisitModel.Visit.Customer.LastName;
            View.m_lblTaskType.Text =
                TaskType.GetText((TaskTypeEnum)Model.ServiceVisitModel.Visit.Tasks[0].Task.TaskTypeId);

            View.m_txtFirstName.Text = Model.ServiceVisitModel.Visit.Customer.FirstName;
            View.m_txtLastName.Text = Model.ServiceVisitModel.Visit.Customer.LastName;
            if (Model.ServiceVisitModel.Visit.ServiceAddress.Address2 != null
                && Model.ServiceVisitModel.Visit.ServiceAddress.Address2 != string.Empty)
            {
                View.m_txtAddress.Text += " " + Model.ServiceVisitModel.Visit.ServiceAddress.Address2;
            }
            View.m_txtCity.Text = Model.ServiceVisitModel.Visit.ServiceAddress.City;
            View.m_cmbState.SelectedItem = Model.ServiceVisitModel.Visit.ServiceAddress.State;
            View.m_txtZip.Text = Model.ServiceVisitModel.Visit.ServiceAddress.Zip;

            UpdateMenuStatus();
            View.m_tabs.Focus();
        }

        #endregion

        #region OnLeftAction

        public override void OnLeftAction()
        {
            IsCancelled = true;
            View.Destroy();
        }

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            IsCancelled = true;
            return true;
        }

        #endregion

        #region OnRightAction

        public override void OnRightAction()
        {
            try
            {
                WorkTransactionPayment payment = new WorkTransactionPayment();
                payment.WorkTransactionPaymentTypeId = (int)WorkTransactionPaymentTypeEnum.BankCheck;
                payment.PaymentAmount = Model.Amount;
                payment.FirstName = View.m_txtFirstName.Text;
                payment.LastName = View.m_txtLastName.Text;
                payment.Address = View.m_txtAddress.Text;
                payment.City = View.m_txtCity.Text;
                payment.State = View.m_cmbState.SelectedItem.ToString();
                payment.Zip = View.m_txtZip.Text;
                payment.BankCheckAccountTypeId = (View.m_cmbAccountType.SelectedIndex + 1);
                payment.BankCheckCompany = (payment.BankCheckAccountTypeId == (int)BankCheckAccountTypeEnum.Personal) ? string.Empty : View.m_txtCompany.Text;
                payment.BankCheckBankName = View.m_txtBankName.Text;
                payment.BankCheckAccountNumber = View.m_txtAccountNumber.Text;
                payment.BankCheckNumber = View.m_txtCheckNumber.Text;
                payment.BankRouteNumber = View.m_txtBankRouteNumber.Text;

                using (new WaitCursor())
                {
                    Database.Begin();
                    Model.PaymentResult = Model.ServiceVisitModel.CompleteVisit(payment);
                    Database.Commit();
                    WorkTransaction.Send();
                }
            }
            catch (WebException ex)
            {                
                MessageDialog.Show(MessageDialogType.Warning,
                        "Couldn't establish connection to the server. Please check your connection availability and try again.");
                Host.Trace("PayByCheckController::OnRightAction", ex.Message + ex.StackTrace);
                return;
            }
            catch (Exception ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Unknown application error. Please contact dispatch");
                Host.Trace("PayByCheckController::OnRightAction", ex.Message + ex.StackTrace);
                return;
            }

            PayByCheckDoneController controller = Prepare<PayByCheckDoneController>(Model);
            controller.Closed += OnPayByCheckDoneClosed;
            controller.Execute();
        }

        private void OnPayByCheckDoneClosed(SingleFormController controller)
        {
            if (Model.PaymentResult.IsAccepted)
                View.Destroy();
        }

        #endregion

        #region OnRequiredFieldChanged

        private void OnRequiredFieldChanged(object sender, EventArgs e)
        {
            UpdateMenuStatus();
        }

        #endregion

        #region UpdateMenuStatus

        private void UpdateMenuStatus()
        {
            if (View.m_txtFirstName.Text == String.Empty)
                View.m_lblFirstName.ForeColor = Color.Red;
            else
                View.m_lblFirstName.ForeColor = Color.Black;

            if (View.m_txtLastName.Text == String.Empty)
                View.m_lblLastName.ForeColor = Color.Red;
            else
                View.m_lblLastName.ForeColor = Color.Black;

            if (View.m_txtAddress.Text == String.Empty)
                View.m_lblAddress.ForeColor = Color.Red;
            else
                View.m_lblAddress.ForeColor = Color.Black;

            if (View.m_txtCity.Text == String.Empty)
                View.m_lblCity.ForeColor = Color.Red;
            else
                View.m_lblCity.ForeColor = Color.Black;

            if (View.m_txtZip.Text == String.Empty || View.m_cmbState.SelectedIndex < 0)
                View.m_lblStateZip.ForeColor = Color.Red;
            else
                View.m_lblStateZip.ForeColor = Color.Black;

            if (View.m_cmbAccountType.SelectedIndex == 0)
            {
                View.m_txtCompany.Enabled = false;
                View.m_lblCompanyName.Enabled = false;
            } else
            {
                View.m_txtCompany.Enabled = true;
                View.m_lblCompanyName.Enabled = true;                
            }

            if (View.m_txtCompany.Text == String.Empty)
                View.m_lblCompanyName.ForeColor = Color.Red;
            else
                View.m_lblCompanyName.ForeColor = Color.Black;

            if (View.m_txtBankName.Text == String.Empty)
                View.m_lblBankName.ForeColor = Color.Red;
            else
                View.m_lblBankName.ForeColor = Color.Black;

            if (View.m_txtAccountNumber.Text == String.Empty)
                View.m_lblAccountNumber.ForeColor = Color.Red;
            else
                View.m_lblAccountNumber.ForeColor = Color.Black;

            if (View.m_txtCheckNumber.Text == String.Empty)
                View.m_lblCheckNumber.ForeColor = Color.Red;
            else
                View.m_lblCheckNumber.ForeColor = Color.Black;

            if (View.m_txtBankRouteNumber.Text == String.Empty)
                View.m_lblBankRouteNumber.ForeColor = Color.Red;
            else
                View.m_lblBankRouteNumber.ForeColor = Color.Black;

            if (View.m_txtFirstName.Text == string.Empty
                || View.m_txtLastName.Text == string.Empty
                || View.m_txtAddress.Text == string.Empty
                || View.m_txtCity.Text == string.Empty
                || View.m_cmbState.SelectedIndex < 0
                || View.m_txtZip.Text == string.Empty
                || (View.m_txtCompany.Enabled && View.m_txtCompany.Text == string.Empty)
                || View.m_txtBankName.Text == string.Empty
                || View.m_txtAccountNumber.Text == string.Empty                
                || View.m_txtCheckNumber.Text == string.Empty
                || View.m_txtBankRouteNumber.Text == string.Empty)
            {
                IsRightActionExist = false;
            }
            else
                IsRightActionExist = true;

        }

        #endregion
    }
}
