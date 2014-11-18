using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Dalworth.Server.Domain;
using Dalworth.Server.SDK;
using Dalworth.Server.Windows;

namespace Dalworth.Server.MainForm.ProjectCustomerHistory
{
    public class ProjectCustomerHistoryModel : IModel
    {
        #region Projects

        private BindingList<ProjectWrapper> m_projects;
        public BindingList<ProjectWrapper> Projects
        {
            get { return m_projects; }
            set { m_projects = value; }
        }

        #endregion

        #region CustomerAndAddress

        private CustomerAndAddress m_customer;
        public CustomerAndAddress Customer
        {
            get { return m_customer; }
            set { m_customer = value; }
        }

        #endregion

        #region Init

        public void Init()
        {
        }

        #endregion
    }
}
