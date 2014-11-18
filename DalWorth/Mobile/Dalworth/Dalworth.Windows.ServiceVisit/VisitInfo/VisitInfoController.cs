using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Dalworth.Data;
using Dalworth.Domain;
using Dalworth.Domain.SyncService;
using Dalworth.Windows.ServiceVisit.CustomerMessage;
using Microsoft.WindowsMobile.Telephony;
using WorkTransaction=Dalworth.Domain.WorkTransaction;

namespace Dalworth.Windows.ServiceVisit.VisitInfo
{
    public class VisitInfoController : SingleFormController<VisitInfoModel, VisitInfoView>
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
            Model.Visit = (VisitPackage)data[0];
            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_linkPhone1.Click += OnPhoneLinkClick;
            View.m_linkPhone2.Click += OnPhoneLinkClick;            
        }

        #endregion

        #region OnViewLoad

        public override void OnViewLoad()
        {
            IsRightActionExist = true;
            RightActionName = "Arrived";

            View.m_lblDate.Text = DateTime.Now.ToString("ddd, MMM dd yyyy");
            View.m_lblCustomerName.Text = Model.Visit.Customer.FirstName + ", " + Model.Visit.Customer.LastName;

            if (Model.Visit.Customer.Phone1 != string.Empty)
                View.m_linkPhone1.Text = Model.Visit.Customer.Phone1;
            else
                View.m_linkPhone1.Visible = false;

            if (Model.Visit.Customer.Phone2 != string.Empty)
                View.m_linkPhone2.Text = Model.Visit.Customer.Phone2;
            else
                View.m_linkPhone2.Visible = false;

            if (Model.Visit.ServiceAddress != null && Model.Visit.ServiceAddress.Map != string.Empty)
                View.m_lblMap.Text = "MAP: " + Model.Visit.ServiceAddress.Map;
            else
                View.m_lblMap.Visible = false;

            if (Model.Visit.ServiceAddress != null)
            {
                View.m_txtAddress.Text = Model.Visit.ServiceAddress.Address1 + "\r\n"
                     + Model.Visit.ServiceAddress.City + ", "
                     + Model.Visit.ServiceAddress.State + ", "
                     + Model.Visit.ServiceAddress.Zip;
            }
            else
            {
                View.m_txtAddress.Text = string.Empty;
            }

            View.m_lblTaskType.Text = TaskType.GetText((TaskTypeEnum)Model.Visit.Tasks[0].Task.TaskTypeId);
            View.m_lblTaskNumber.Text = "TSK: " + Model.Visit.Tasks[0].Task.Number;
            View.m_txtNotes.Text = Model.Visit.Visit.Notes;

            View.m_txtAddress.Focus();
        }

        #endregion

        #region OnSave

        protected override bool OnSave()
        {
            return false;
        }

        #endregion

        #region OnRightAction

        public override void OnRightAction() 
        {
            if (MessageDialog.Show(MessageDialogType.Question,
                "Did you arrive to destination?") == DialogResult.No)
            {
                return;
            }


            try
            {
                using (WaitCursor cursor = new WaitCursor())
                {
                    Database.Begin();
                    Model.ArriveToVisit();
                    Database.Commit();
                    WorkTransaction.Send();
                }

                CustomerMessageController controller = Prepare<CustomerMessageController>(Model.Visit);
                controller.Closed += OnCustomerMessageClosed;
                controller.Execute();
            }
            catch (Exception ex)
            {
                Database.Rollback();
                MessageDialog.Show(MessageDialogType.Warning,
                        "Unknown application error. Please contact dispatch");
                Host.Trace("VisitInfoController::OnRightAction", ex.Message + ex.StackTrace);
                return;
            }
        }

        private void OnCustomerMessageClosed(SingleFormController controller)
        {
            View.Destroy();
        }

        #endregion

        #region OnPhoneLinkClick

        private void OnPhoneLinkClick(object sender, EventArgs e)
        {
            LinkLabel linkLabel = (LinkLabel)sender;
            if (linkLabel.Visible == false || linkLabel.Text == string.Empty)
                return;

            string customerName = Model.Visit.Customer.FirstName + ", " + Model.Visit.Customer.LastName;

            if (MessageDialog.Show(MessageDialogType.Question,
                string.Format("Do you want to call {0} at {1}?",
                customerName, linkLabel.Text)) == DialogResult.No)
            {
                return;
            }

            try
            {
                Phone phone = new Phone();
                phone.Talk(linkLabel.Text + "\0", false);
            }
            catch (Exception)
            {
                MessageDialog.Show(MessageDialogType.Warning, "Cannot make a call, please check your phone settings");
            }

        }

        #endregion
    }
}
