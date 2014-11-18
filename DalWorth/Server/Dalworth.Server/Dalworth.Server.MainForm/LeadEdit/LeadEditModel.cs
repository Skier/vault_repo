using System.ComponentModel;
using Dalworth.Server.Domain;
using Dalworth.Server.Domain.package;

namespace Dalworth.Server.MainForm.LeadEdit
{
    public class LeadEditModel
    {
        #region CurrentLead

        private LeadWrapper m_currentLead;
        public LeadWrapper CurrentLead
        {
            get { return m_currentLead; }
            set { m_currentLead = value; }
        }

        #endregion

        #region RelatedProjects

        private BindingList<ProjectWrapper> m_relatedProjects;
        public BindingList<ProjectWrapper> RelatedProjects
        {
            get { return m_relatedProjects; }
        }

        #endregion

        #region RelatedCustomers

        private BindingList<Customer> m_relatedCustomers;
        public BindingList<Customer> RelatedCustomers
        {
            get { return m_relatedCustomers; }
        }

        #endregion

        #region Init

        public void Init()
        {
            BindingList<LeadWrapper> leads = Lead.FindLeadWrappers(m_currentLead.Lead.ID);
            if (leads.Count > 0 && leads[0] != null)
                m_currentLead = leads[0];
        }

        #endregion

        #region RefreshCollections

        public void RefreshProjects()
        {
            if (RelatedProjects == null)
                m_relatedProjects = new BindingList<ProjectWrapper>();

            m_relatedProjects.Clear();

        }

        public void RefreshCustomers()
        {
            if (RelatedCustomers == null)
                m_relatedCustomers = new BindingList<Customer>();

            m_relatedCustomers.Clear();

        }

        #endregion

        #region SaveLead

        public void PrintLead()
        {
            new LeadPrint(CurrentLead.Lead).Print();
        }

        #endregion
    }
}
