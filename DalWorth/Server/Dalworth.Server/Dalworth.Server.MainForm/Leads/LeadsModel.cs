using System.ComponentModel;
using Dalworth.Server.Domain;

namespace Dalworth.Server.MainForm.Leads
{
    public class LeadsModel
    {
        #region Leads

        private BindingList<LeadWrapper> m_leadWrappers;
        public BindingList<LeadWrapper> LeadWrappers
        {
            get { return m_leadWrappers; }
        }

        #endregion

        #region Init

        public void Init()
        {
            UpdateLeadWrappers(null, string.Empty, string.Empty, string.Empty, string.Empty,
                string.Empty, string.Empty, string.Empty, null, null);
        }

        #endregion

        #region UpdateLeadWrappers

        public void UpdateLeadWrappers(int? exactLeadId, 
            string firstName, string lastName, string phoneNo,
            string city, string zip, string street, string block, 
            int? status, DateRange dateRange)
        {
            if (exactLeadId != null)
            {
                m_leadWrappers = Lead.FindLeadWrappers((int)exactLeadId);
                return;
            } 

            if (firstName.Trim() == string.Empty
                && lastName.Trim() == string.Empty
                && phoneNo.Trim() == string.Empty
                && city.Trim() == string.Empty
                && zip.Trim() == string.Empty
                && street.Trim() == string.Empty
                && block.Trim() == string.Empty
                && status == null
                && (dateRange == null || dateRange.IsNull))
            {
                m_leadWrappers = new BindingList<LeadWrapper>();
            }
            else
            {
                m_leadWrappers = Lead.FindLeadWrappers(
                    firstName, lastName, phoneNo,
                    city, zip, street, block,
                    status, dateRange);
            }
        }

        #endregion

        #region SaveLead

        public void SaveLead(Lead lead)
        {
            if (lead.ID == 0)
            {
                Lead.Insert(lead);
            }
            else
            {
                Lead.Update(lead);
            }
        }

        #endregion
    }
}
