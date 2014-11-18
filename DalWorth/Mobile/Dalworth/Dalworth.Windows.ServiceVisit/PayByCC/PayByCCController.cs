using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Dalworth.Data;
using Dalworth.Domain;
using Dalworth.Domain.SyncService;
using Dalworth.Windows.ServiceVisit.PayByCCDone;
using Dalworth.Windows.ServiceVisit.ServiceVisit;
using CreditCardTypeEnum=Dalworth.Domain.CreditCardTypeEnum;
using WorkTransaction=Dalworth.Domain.WorkTransaction;
using WorkTransactionPaymentTypeEnum=Dalworth.Domain.WorkTransactionPaymentTypeEnum;

namespace Dalworth.Windows.ServiceVisit.PayByCC
{
    public class PayByCCController : SingleFormController<PayByCCModel, PayByCCView>
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
            Model.Amount = (decimal) data[1];
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
            View.m_rbnVisa.CheckedChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_rbnDinner.CheckedChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_rbnMaster.CheckedChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_rbnDiscover.CheckedChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_txtCCNumber.TextChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_cmbExpMonth.SelectedIndexChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_cmbExpYear.SelectedIndexChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_cmbCVV2Type.SelectedIndexChanged += new EventHandler(OnRequiredFieldChanged);
            View.m_txtCVV2.TextChanged += new EventHandler(OnRequiredFieldChanged);            
            
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsLeftActionExist = true;
            IsRightActionExist = true;

            LeftActionName = "Cancel";
            RightActionName = "Done";

            View.m_lblAmountDue.Text = Model.Amount.ToString("C");
            View.m_lblVisitNumber.Text = Model.ServiceVisitModel.Visit.Tasks[0].Task.Number;
            View.m_lblCustomerName.Text = Model.ServiceVisitModel.Visit.Customer.FirstName + ", " +
                                          Model.ServiceVisitModel.Visit.Customer.LastName;
            View.m_lblTaskType.Text =
                TaskType.GetText((TaskTypeEnum)Model.ServiceVisitModel.Visit.Tasks[0].Task.TaskTypeId);

            View.m_txtFirstName.Text = Model.ServiceVisitModel.Visit.Customer.FirstName;
            View.m_txtLastName.Text = Model.ServiceVisitModel.Visit.Customer.LastName;
            View.m_txtAddress.Text = Model.ServiceVisitModel.Visit.ServiceAddress.Address1;
            if (Model.ServiceVisitModel.Visit.ServiceAddress.Address2 != null
                && Model.ServiceVisitModel.Visit.ServiceAddress.Address2 != string.Empty)
            {
                View.m_txtAddress.Text += " " + Model.ServiceVisitModel.Visit.ServiceAddress.Address2;
            }
            View.m_txtCity.Text = Model.ServiceVisitModel.Visit.ServiceAddress.City;
            View.m_cmbState.SelectedItem = Model.ServiceVisitModel.Visit.ServiceAddress.State;
            View.m_cmbCVV2Type.SelectedIndex = 1; //CVV2 Used
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
                payment.WorkTransactionPaymentTypeId = (int)WorkTransactionPaymentTypeEnum.CreditCard;
                payment.PaymentAmount = Model.Amount;
                payment.FirstName = View.m_txtFirstName.Text;
                payment.LastName = View.m_txtLastName.Text;
                payment.Address = View.m_txtAddress.Text;
                payment.City = View.m_txtCity.Text;
                payment.State = View.m_cmbState.SelectedItem.ToString();
                payment.Zip = View.m_txtZip.Text;

                if (View.m_rbnDinner.Checked)
                    payment.CreditCardTypeId = (int)CreditCardTypeEnum.Dinner;
                else if (View.m_rbnDiscover.Checked)
                    payment.CreditCardTypeId = (int)CreditCardTypeEnum.Discover;
                else if (View.m_rbnMaster.Checked)
                    payment.CreditCardTypeId = (int)CreditCardTypeEnum.MasterCard;
                else if (View.m_rbnVisa.Checked)
                    payment.CreditCardTypeId = (int)CreditCardTypeEnum.Visa;

                payment.CreditCardNumber = View.m_txtCCNumber.Text;
                payment.CreditCardExpirationDate = new DateTime(
                    int.Parse(View.m_cmbExpYear.SelectedItem.ToString()),
                    int.Parse(View.m_cmbExpMonth.SelectedItem.ToString()), 1);

                if (View.m_cmbCVV2Type.SelectedIndex == 0)
                    payment.CreditCardCVV2TypeId = (int)CreditCardCVV2TypeEnum.NotUsed;
                else if (View.m_cmbCVV2Type.SelectedIndex == 1)
                    payment.CreditCardCVV2TypeId = (int)CreditCardCVV2TypeEnum.Used;
                else if (View.m_cmbCVV2Type.SelectedIndex == 2)
                    payment.CreditCardCVV2TypeId = (int)CreditCardCVV2TypeEnum.Illegible;
                else if (View.m_cmbCVV2Type.SelectedIndex == 3)
                    payment.CreditCardCVV2TypeId = (int)CreditCardCVV2TypeEnum.NoCVV2Imprinted;

                payment.CreditCardCVV2 = View.m_txtCVV2.Enabled ? View.m_txtCVV2.Text : string.Empty;

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
                Host.Trace("PayByCCController::OnRightAction", ex.Message + ex.StackTrace);
                return;
            }
            catch (Exception ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Unknown application error. Please contact dispatch");
                Host.Trace("PayByCCController::OnRightAction", ex.Message + ex.StackTrace);
                return;
            }


            PayByCCDoneController controller = Prepare<PayByCCDoneController>(Model);
            controller.Closed += OnPayByCCDoneControllerClosed;
            controller.Execute();
        }

        private void OnPayByCCDoneControllerClosed(SingleFormController controller)
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

            if (View.m_txtCCNumber.Text == String.Empty)
                View.m_lblNumber.ForeColor = Color.Red;
            else
                View.m_lblNumber.ForeColor = Color.Black;

            if (View.m_cmbExpYear.SelectedIndex < 0 || View.m_cmbExpMonth.SelectedIndex < 0)
                View.m_lblExpirationDate.ForeColor = Color.Red;
            else
                View.m_lblExpirationDate.ForeColor = Color.Black;

            if (View.m_cmbCVV2Type.SelectedIndex == 1 || View.m_cmbCVV2Type.SelectedIndex == 2)
            {
                View.m_txtCVV2.Enabled = true;
                View.m_lblCVV2.Enabled = true;
            }                
            else
            {
                View.m_txtCVV2.Enabled = false;
                View.m_lblCVV2.Enabled = false;
            }                

            if (View.m_txtCVV2.Text == String.Empty && View.m_txtCVV2.Enabled)
                View.m_lblCVV2.ForeColor = Color.Red;
            else
                View.m_lblCVV2.ForeColor = Color.Black;


            if (View.m_txtFirstName.Text == string.Empty
                || View.m_txtLastName.Text == string.Empty
                || View.m_txtAddress.Text == string.Empty
                || View.m_txtCity.Text == string.Empty
                || View.m_cmbState.SelectedIndex < 0
                || View.m_txtZip.Text == string.Empty
                || (!View.m_rbnVisa.Checked && !View.m_rbnMaster.Checked && !View.m_rbnDinner.Checked && !View.m_rbnDiscover.Checked)
                || View.m_txtCCNumber.Text == string.Empty
                || View.m_cmbExpMonth.SelectedIndex < 0
                || View.m_cmbExpYear.SelectedIndex < 0
                || (View.m_txtCVV2.Text == string.Empty && View.m_txtCVV2.Enabled))
            {
                IsRightActionExist = false;
            }
            else
                IsRightActionExist = true;

        }

        #endregion

    }
}
