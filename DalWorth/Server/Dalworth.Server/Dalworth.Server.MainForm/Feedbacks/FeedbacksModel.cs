using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Dalworth.Server.Windows;
using Dalworth.Server.Domain;

namespace Dalworth.Server.MainForm.Feedbacks
{
    public class FeedbacksModel : IModel
    {
        private BindingList<ProjectFeedbackWrapper> m_ProjectFeedbackWrappers;
        public BindingList<ProjectFeedbackWrapper> ProjectFeedbackWrappers
        {
            get { return m_ProjectFeedbackWrappers; }
            set { m_ProjectFeedbackWrappers = value; }
        }

        public void Init()
        {
            m_ProjectFeedbackWrappers = ProjectFeedbackWrapper.FindProjectFeedbacks(null, null, 0, null, null,null);
        }

        public void UpdateProjectFeedbackWrappers(string firstName, string lastName, int? status, DateRange dateCreatedRange, DateRange dateCallbackRange)
        {
            if (firstName.Trim() == string.Empty
                && lastName.Trim() == string.Empty
                && !status.HasValue
                && dateCreatedRange == null
                && dateCallbackRange == null)
            {
                m_ProjectFeedbackWrappers = new BindingList<ProjectFeedbackWrapper>();
                return;
            }

            m_ProjectFeedbackWrappers = ProjectFeedbackWrapper.FindProjectFeedbacks(firstName, lastName, status, dateCreatedRange, dateCallbackRange, null);
        }
    }
}
