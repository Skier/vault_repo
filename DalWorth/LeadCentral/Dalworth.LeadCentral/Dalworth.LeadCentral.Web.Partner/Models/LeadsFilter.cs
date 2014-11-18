using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Web.Partner.Models
{
    public class LeadsFilter
    {
        public User User { get; set; }
        public List<int> LeadSourceIds { get; set; }
        public List<LeadStatusEnum> LeadStatuses { get; set; }
        public int? LeadSourceId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}