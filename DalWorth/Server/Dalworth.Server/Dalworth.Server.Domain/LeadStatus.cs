using System;

namespace Dalworth.Server.Domain
{
    public enum LeadStatusEnum
    {
        New = 1,
        Pending = 2,
        Converted = 3,
        Cancelled = 4
    }
    
    public partial class LeadStatus
    {
        public LeadStatus()
        {
            
        }

        public static string GetText(LeadStatusEnum leadStatus)
        {
            return leadStatus.ToString();
        }
    }
}
      