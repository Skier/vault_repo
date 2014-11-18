using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Partner.Models
{
    public class LeadsList
    {
        public List<LeadItem> LeadItems { get; set; }

        public void LoadLeadItems(User user)
        {
            LeadItems = new List<LeadItem>();

            var leadFilter = new LeadFilter();
            if (user.IsBusinessPartner())
                leadFilter.UserId = user.Id;

            var leads = LeadService.GetLeadsLimit(user.RelatedCustomer, leadFilter, null, null);

            foreach (var lead in leads)
            {
                LeadItems.Add(new LeadItem(lead));
            }
        }

        public List<int> LeadSourceIds { get; set; }
        
        public List<LeadStatusEnum> LeadStatuses { get; set; }
        
        public int? LeadSourceId { get; set; }
        
        public DateTime? DateFrom { get; set; }
        
        public DateTime? DateTo { get; set; }

    }
}