using System;

namespace Servman.Domain
{
    public class LeadFilter
    {
		public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int LeadSourceId { get; set; }
        public int AssignedToUserId { get; set; }
        public int CreatedByUserId { get; set; }
        public int LeadTypeId { get; set; }
        public int[] LeadStatuses { get; set; }
    }
}
