using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Lead
{
    public class MatchInvoice
    {
        public MatchInvoice()
        {
        }

        public MatchInvoice(int leadId)
        {
            LeadId = leadId;

            var currentUser = ContextHelper.GetCurrentUser();
            using (var connection = CustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                CurrentLead = LeadService.FindByPrimaryKey(leadId, currentUser, connection);
            }

            FirstName = CurrentLead.FirstName;
            LastName = CurrentLead.LastName;
            Phone = CurrentLead.Phone;
            DateStart = CurrentLead.DateCreated;
        }

        public int LeadId { get; set; }

        public Domain.Lead CurrentLead { get; set; }

        public List<QbInvoice> Invoices { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Phone { get; set; }
        
        public DateTime DateStart { get; set; }

        public void RetrieveInvoices(Domain.User currentUser, IDbConnection connection)
        {
            CurrentLead = LeadService.FindByPrimaryKey(LeadId, currentUser, connection);
            CurrentLead.FirstName = FirstName;
            CurrentLead.LastName = LastName;
            CurrentLead.Phone = Phone;

            Invoices = QbInvoiceService.GetUnmatchedByLead(CurrentLead, DateStart, connection);
        }
    }
}