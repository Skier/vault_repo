using System;
namespace Servman.Domain
{
    public enum LeadStatusEnum
    {
        New = 1,
        Pending = 2,
        Cancelled = 3,
        Converted = 4
    }

    public partial class LeadStatus
    {
        public LeadStatus()
        {

        }
    }
}
