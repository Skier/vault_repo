using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text;
using Dalworth.Controls;
using Dalworth.Domain;
using Dalworth.Windows.ServiceVisit.PayByCash;
using Dalworth.Windows.ServiceVisit.PayByCC;
using Dalworth.Windows.ServiceVisit.PayByCheck;
using Dalworth.Windows.ServiceVisit.ServiceVisit;
using Item=Dalworth.Domain.SyncService.Item;

namespace Dalworth.Windows.ServiceVisit.VisitReceipt
{
    public class VisitReceiptController : SingleFormController<VisitReceiptModel, VisitReceiptView>
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
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnPayByCC.Click += OnPayByCC;
            View.m_btnPayByCheck.Click += OnPayByCheck;
            View.m_btnPayByCash.Click += OnPayByCash;

            View.m_menuPayByCC.Click += OnPayByCC;
            View.m_menuPayByCheck.Click += OnPayByCheck;
            View.m_menuPayByCash.Click += OnPayByCash;

            View.m_curService.TextChanged += OnServiceAmountChanged;
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsLeftActionExist = true;
            IsRightActionExist = true;

            LeftActionName = "Cancel";
            RightActionName = "Menu";

            View.m_btnPayByCash.Picture = ImageKeys.Cash;
            View.m_btnPayByCC.Picture = ImageKeys.CreditCard;
            View.m_btnPayByCheck.Picture = ImageKeys.Check;

            View.m_lblTaskNumber.Text = Model.ServiceVisitModel.Visit.Tasks[0].Task.Number;
            View.m_lblCustomerName.Text = Model.ServiceVisitModel.Visit.Customer.FirstName + ", " + Model.ServiceVisitModel.Visit.Customer.LastName;
            View.m_lblTaskType.Text =
                TaskType.GetText((TaskTypeEnum)Model.ServiceVisitModel.Visit.Tasks[0].Task.TaskTypeId);


            View.m_curService.Value = Model.ServiceVisitModel.GetTasksViewTotal() / (1 + Application.TAX_PERCENT);            
            UpdateTotal();
            UpdateMenuStatus();            
        }

        #endregion

        #region OnViewActivated

        public override void OnViewActivated()
        {
            View.m_btnPayByCC.Focus();            
        }

        #endregion

        #region UpdateTotal

        private void UpdateTotal()
        {
            View.m_lblTaxAmount.Text = (View.m_curService.Value.Value*Application.TAX_PERCENT).ToString("C");
            View.m_lblTotal.Text = (View.m_curService.Value.Value * (Application.TAX_PERCENT + 1)).ToString("C");
        }

        #endregion

        #region UpdateMenuStatus

        private void UpdateMenuStatus()
        {
            if (!View.m_curService.Value.HasValue
                || View.m_curService.Value.Value == decimal.Zero)
            {
                View.m_menuPayByCash.Enabled = View.m_menuPayByCC.Enabled = View.m_menuPayByCheck.Enabled
                    = View.m_btnPayByCash.Enabled = View.m_btnPayByCC.Enabled = View.m_btnPayByCheck.Enabled = false;
                View.m_lblService.ForeColor = Color.Red;
            }
            else
            {
                View.m_menuPayByCash.Enabled = View.m_menuPayByCC.Enabled = View.m_menuPayByCheck.Enabled
                    = View.m_btnPayByCash.Enabled = View.m_btnPayByCC.Enabled = View.m_btnPayByCheck.Enabled = true;
                View.m_lblService.ForeColor = Color.Black;
            }
        }

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            IsCancelled = true;
            return true;
        }

        #endregion

        #region OnLeftAction

        public override void OnLeftAction()
        {
            IsCancelled = true;
            View.Destroy();
        }

        #endregion

        #region OnPayByCC

        private void OnPayByCC(object sender, EventArgs e)
        {
            if (!IsFromValid())
                return;

            PayByCCController controller = Prepare<PayByCCController>(Model.ServiceVisitModel, decimal.Parse(View.m_lblTotal.Text, NumberStyles.Currency));
            controller.Closed += OnPayByCCClosed;
            controller.Execute();
        }

        private void OnPayByCCClosed(SingleFormController controller)
        {
            PayByCCController ccController = (PayByCCController) controller;
            if (!ccController.IsCancelled)
                View.Destroy();
        }

        #endregion

        #region OnPayByCheck

        private void OnPayByCheck(object sender, EventArgs e)
        {
            if (!IsFromValid())
                return;

            PayByCheckController controller = Prepare<PayByCheckController>(Model.ServiceVisitModel, decimal.Parse(View.m_lblTotal.Text, NumberStyles.Currency));
            controller.Closed += OnPayByCheckClosed;
            controller.Execute();
        }

        private void OnPayByCheckClosed(SingleFormController controller)
        {
            PayByCheckController checkController = (PayByCheckController) controller;
            if (!checkController.IsCancelled)
                View.Destroy();
        }

        #endregion

        #region OnPayByCash

        private void OnPayByCash(object sender, EventArgs e)
        {
            if (!IsFromValid())
                return;

            PayByCashController controller = Prepare<PayByCashController>(Model.ServiceVisitModel, decimal.Parse(View.m_lblTotal.Text, NumberStyles.Currency));
            controller.Closed += OnPayByCashClosed;
            controller.Execute();
        }

        private void OnPayByCashClosed(SingleFormController controller)
        {
            PayByCashController cashController = (PayByCashController) controller;
            if (!cashController.IsCancelled)
                View.Destroy();
        }

        #endregion        

        #region OnServiceAmountChanged

        private void OnServiceAmountChanged(object sender, EventArgs e)
        {
            UpdateTotal();
            UpdateMenuStatus();            
        }

        #endregion

        #region IsFromValid

        private bool IsFromValid()
        {
            if (!View.m_curService.Value.HasValue || View.m_curService.Value.Value == decimal.Zero)
            {
                MessageDialog.Show(MessageDialogType.Warning, "Please enter Service amount");
                View.m_curService.Focus();
                return false;
            }

            return true;
        }

        #endregion
    }
}
