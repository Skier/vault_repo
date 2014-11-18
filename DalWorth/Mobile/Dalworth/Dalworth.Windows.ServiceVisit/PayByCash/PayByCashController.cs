using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Dalworth.Data;
using Dalworth.Domain;
using Dalworth.Domain.SyncService;
using Dalworth.Windows.ServiceVisit.ServiceVisit;
using WorkTransaction=Dalworth.Domain.WorkTransaction;
using WorkTransactionPaymentTypeEnum=Dalworth.Domain.WorkTransactionPaymentTypeEnum;

namespace Dalworth.Windows.ServiceVisit.PayByCash
{
    public class PayByCashController : SingleFormController<PayByCashModel, PayByCashView>
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
            Model.ServiceVisitModel = (ServiceVisitModel) data[0];
            Model.Amount = (decimal) data[1];

            base.OnModelInitialize(data);
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
            if (MessageDialog.Show(MessageDialogType.Question, string.Format("Did you collect {0}?", Model.Amount.ToString("C")))
                 == DialogResult.No)
            {
                return;
            }

            try
            {
                WorkTransactionPayment payment = new WorkTransactionPayment();
                payment.WorkTransactionPaymentTypeId = (int)WorkTransactionPaymentTypeEnum.Cash;
                payment.PaymentAmount = Model.Amount;
                payment.FirstName = Model.ServiceVisitModel.Visit.Customer.FirstName;
                payment.LastName = Model.ServiceVisitModel.Visit.Customer.LastName;
                payment.City = Model.ServiceVisitModel.Visit.ServiceAddress.City;
                payment.State = Model.ServiceVisitModel.Visit.ServiceAddress.State;
                payment.Zip = Model.ServiceVisitModel.Visit.ServiceAddress.Zip;

                using (new WaitCursor())
                {
                    Database.Begin();
                    Model.ServiceVisitModel.CompleteVisit(payment);
                    Database.Commit();
                    WorkTransaction.Send();
                    View.Destroy();
                }
            }
            catch (Exception ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Unknown application error. Please contact dispatch");
                Host.Trace("PayByCashController::OnRightAction", ex.Message + ex.StackTrace);
                return;
            }
        }

        #endregion
    }
}
