using System;
using System.Windows.Forms;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;
using Dalworth.Server.MainForm.CreateVisit;
using Dalworth.Server.Windows;
using Dalworth.Server.SDK;
using DevExpress.XtraEditors;


namespace Dalworth.Server.MainForm.LeadEdit
{
    class LeadEditController : Controller<LeadEditModel, LeadEditView>
    {
        #region IsCancelled

        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #endregion

        #region AffectedLead

        public Lead AffectedLead
        {
            get { return Model.CurrentLead.Lead; }
        }

        #endregion

        #region OnModelInitialize

        protected override void OnModelInitialize(object[] data)
        {
            if (data != null && data.Length > 0 && data[0] != null)
                Model.CurrentLead = (LeadWrapper)data[0];

            base.OnModelInitialize(data);
        }

        #endregion

        #region OnInitialize

        protected override void OnInitialize()
        {
            View.m_btnCreateProject.Click += OnCreateProjectClick;
            View.m_btnCancelLead.Click += OnCancelLeadClick;
            View.m_btnPendingLead.Click += OnPendingLeadClick;
            View.m_btnOk.Click += OnOkClick;
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnPrint.Click += OnPrintClick;
        }

        #endregion

        #region OnViewLoad

        protected override void OnViewLoad()
        {
            View.m_lblFirstName.Text = Model.CurrentLead.Lead.FirstName;
            View.m_lblLastName.Text = Model.CurrentLead.Lead.LastName;
            View.m_lblCompany.Text = Model.CurrentLead.Lead.Company;
            View.m_lblHomePhone.Text = Utils.FormatPhone(Model.CurrentLead.Lead.Phone1);
            View.m_lblOtherPhone.Text = Utils.FormatPhone(Model.CurrentLead.Lead.Phone2);
            View.m_lblEmail.Text = Model.CurrentLead.Lead.Email;
            View.m_lblAddress1.Text = Model.CurrentLead.Lead.Address1;
            View.m_lblAddress2.Text = Model.CurrentLead.Lead.Address2;
            View.m_lblCity.Text = Model.CurrentLead.Lead.City;
            View.m_lblState.Text = Model.CurrentLead.Lead.State;
            View.m_lblZip.Text = Model.CurrentLead.Zip;
            View.m_lblPreferredDate.Text = Model.CurrentLead.Lead.PreferredServiceDate.HasValue ? Model.CurrentLead.Lead.PreferredServiceDate.Value.ToShortDateString() : string.Empty;
            View.m_lblPreferredTime.Text = Model.CurrentLead.Lead.PreferredTime;
            View.m_lblDateCreated.Text = Model.CurrentLead.Lead.DateCreated.ToShortDateString();
            View.m_memoCustomerNotes.Text = Model.CurrentLead.Lead.CustomerNotes;
            View.m_lblBusinessPartner.Text = Model.CurrentLead.BusinessPartnerName;
            View.m_lblAdvertisingSource.Text = Model.CurrentLead.AdvertisingSourceAcronym;

            if (Model.CurrentLead.Lead.Status == LeadStatusEnum.Pending)
            {
                View.m_btnPendingLead.Visible = false;
            }
            else if (Model.CurrentLead.Lead.Status == LeadStatusEnum.Cancelled)
            {
                View.m_btnPendingLead.Visible = false;
                View.m_btnCancelLead.Enabled = false;
                View.m_lblDateCancelledCaption.Visible = true;
                View.m_lblDateCancelled.Visible = true;
                if (Model.CurrentLead.Lead.DateCancelled != null)
                    View.m_lblDateCancelled.Text = Model.CurrentLead.Lead.DateCancelled.Value.ToShortDateString();
            }
            else if (Model.CurrentLead.Lead.Status == LeadStatusEnum.Converted)
            {
                View.m_btnCreateProject.Visible = false;
                View.m_btnCancelLead.Visible = false;
                View.m_btnPendingLead.Visible = false;
            }

            View.m_memoDispatcherNotes.Text = Model.CurrentLead.Lead.DispatchNotes;

            View.m_memoDispatcherNotes.Select();
        }

        #endregion

        #region CreateProject

        private void CreateProject()
        {
            using (CreateVisitController controller = Prepare<CreateVisitController>())
            {
                controller.BaseLead = Model.CurrentLead;

                controller.Execute(false);
                
                if (!controller.IsCancelled)
                    View.Destroy();
            }
        }

        #endregion

        #region IsValid

        private static bool IsValid()
        {
            return true;
        }

        #endregion

        #region OnCreateProjectClick

        private void OnCreateProjectClick(object sender, EventArgs e)
        {
            if (Model.CurrentLead.Lead.Status != LeadStatusEnum.Converted)
                CreateProject();
        }

        #endregion

        #region OnCancelLeadClick

        private void OnCancelLeadClick(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("Are you sure?", "Cancel Lead", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Model.CurrentLead.Lead.Status = LeadStatusEnum.Cancelled;
                Model.CurrentLead.Lead.DateCancelled = DateTime.Now;
                
                OnOkClick(sender, e);
            }
        }

        #endregion

        #region OnPendingLeadClick

        private void OnPendingLeadClick(object sender, EventArgs e)
        {
            Model.CurrentLead.Lead.Status = LeadStatusEnum.Pending;
            OnOkClick(sender, e);
        }

        #endregion

        #region OnOkClick

        private void OnOkClick(object sender, EventArgs e)
        {
            if (!IsValid())
                return;

            Lead lead = Model.CurrentLead.Lead;
            lead.DispatchNotes = View.m_memoDispatcherNotes.Text;
            lead.LastUpdateEmployeeId = Configuration.CurrentDispatchId;
            lead.DateLastSetPending = DateTime.Now;

            if (lead.FirstUpdateEmployeeId == null)
            {
                lead.DateFirstSetPending = DateTime.Now;
                lead.FirstUpdateEmployeeId = Configuration.CurrentDispatchId;
            }

            try
            {
                Database.Begin();
                Lead.Save(lead);
                Database.Commit();
            }
            catch (Exception)
            {
                Database.Rollback();
                throw;
            }

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

        #region OnPrintClick

        public void OnPrintClick(object sender, EventArgs e)
        {
            OnOkClick(sender, e);
            Model.PrintLead();
            View.Destroy();
        }

        #endregion
    }
}
