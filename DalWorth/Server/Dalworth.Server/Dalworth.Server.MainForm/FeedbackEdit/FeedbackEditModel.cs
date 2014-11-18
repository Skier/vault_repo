using System;
using System.Collections.Generic;
using System.Text;

using Dalworth.Server.Windows;
using Dalworth.Server.Domain;

namespace Dalworth.Server.MainForm.FeedbackEdit
{
    public class FeedbackEditModel : IModel
    {
        ProjectFeedbackWrapper m_currentProjectFeedbackWrapper;
        public ProjectFeedbackWrapper CurrentProjectFeedbackWrapper
        {
            get { return m_currentProjectFeedbackWrapper; }
            set { m_currentProjectFeedbackWrapper = value; }
        }

        public void Init()
        {
            if (m_currentProjectFeedbackWrapper.Address == null)
            {
                m_currentProjectFeedbackWrapper.Address = Address.FindByPrimaryKey(m_currentProjectFeedbackWrapper.Customer.AddressId.Value);
            }
        }
    }
}
