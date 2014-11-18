
using System;
  

namespace Dalworth.LeadCentral.Domain
{
    public partial class LeadStatus
    {
        public LeadStatus()
        {
        }
    }

    public enum LeadStatusEnum
    {
        New = 1,
        Pending = 2,
        Cancelled = 3,
        Converted = 4
    }

}
      