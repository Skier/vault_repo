using System;
using Dalworth.Server.Domain;
using Dalworth.Server.Windows;
using Dalworth.Server.SDK;

namespace Dalworth.Server.MainForm.FeedbackEdit
{
    public class FeedbackEditController : Controller<FeedbackEditModel, FeedbackEditView>
    {
        private bool m_isCancelled;
        public bool IsCancelled
        {
            get { return m_isCancelled; }
        }

        #region Controller Overrides 

        protected override void OnInitialize()
        {
            View.m_btnCancel.Click += OnCancelClick;
            View.m_btnOk.Click += OnOkClick;
        }

        protected override void OnModelInitialize(object[] data)
        {
            if (data != null && data.Length > 0 && data[0] != null)
                Model.CurrentProjectFeedbackWrapper = (ProjectFeedbackWrapper)data[0];

            base.OnModelInitialize(data);
        }

        protected override void OnViewLoad()
        {
            ProjectFeedbackWrapper feedback = Model.CurrentProjectFeedbackWrapper;

            View.m_lblFirstName.Text = feedback.Customer.FirstName;
            View.m_lblLastName.Text = feedback.Customer.LastName;
            View.m_lblAddress1.Text = feedback.Address.AddressFirstLine;
            View.m_lblAddress2.Text = feedback.Address.AddressSecondLine;
            View.m_lblEmail.Text = feedback.Customer.Email;
            View.m_lblHomePhone.Text = feedback.Customer.Phone1Formatted;
            View.m_lblOtherPhone.Text = feedback.Customer.Phone2Formatted;
            View.m_memoCustomerNotes.Text = feedback.CustomerNote;
            View.m_memoCustomerNotesEdited.Text = feedback.CustomerNoteEdited!= null && feedback.CustomerNoteEdited.Trim().Length > 0?
                feedback.CustomerNoteEdited:feedback.CustomerNote;
            View.m_lblRating.Text = feedback.ProjectFeedbackRate.Rate;
            View.m_lblRemiderSentDate.Text = feedback.ProjectFeedback.ReminderEmailSentDate.HasValue?
                string.Format("{0:MM/dd/yyyy}", feedback.ProjectFeedback.ReminderEmailSentDate.Value):string.Empty;
            View.m_lblReviwedBy.Text = feedback.ReviewedByEmployeeName;
            View.m_lblDateCreated.Text = string.Format("{0:MM/dd/yyyy}", feedback.ProjectFeedback.DateCreated);
            View.m_lblDateReviewed.Text = feedback.DateReviewed;
            View.m_lblCallbackDate.Text = feedback.CallbackDate;
            View.m_chkPublishToSite.Checked = feedback.IsPublished;
            View.m_memoDispatcherNotes.Text = feedback.ProjectFeedback.DispatcherNote;
        }

        #endregion 

        #region Event Handlers

        private void OnCancelClick(object sender, EventArgs e)
        {
            m_isCancelled = true;
            View.Destroy();
        }

        public void OnOkClick(object sender, EventArgs e)
        {
            ProjectFeedbackWrapper feedback = Model.CurrentProjectFeedbackWrapper;

            feedback.ProjectFeedback.DateReviewed = DateTime.Now;
            feedback.ProjectFeedback.ReviewedByEmployeeId = Configuration.CurrentDispatchId;
            feedback.ReviewedByEmployee = Employee.FindByPrimaryKey(Configuration.CurrentDispatchId);
            feedback.ProjectFeedback.EditedCustomerNote = View.m_memoCustomerNotesEdited.Text;
            feedback.ProjectFeedback.DispatcherNote = View.m_memoDispatcherNotes.Text;
            feedback.ProjectFeedback.CanBePostedOnWebSite = View.m_chkPublishToSite.Checked;
            
            if (feedback.ProjectFeedback.EditedCustomerNote.Trim().Length == 0)
                feedback.ProjectFeedback.CanBePostedOnWebSite = false;

            ProjectFeedback.Update(feedback.ProjectFeedback);

            BackgroundJobPending backgroundJob = new BackgroundJobPending();
            backgroundJob.BackgroundJobType = BackgroundJobTypeEnum.ProjectFeedbackProcessed;
            backgroundJob.ProjectFeedbackId = feedback.ProjectFeedback.ID;
            BackgroundJobPending.Insert(backgroundJob);

            View.Destroy();
        }

        #endregion 
    }
}
