using System;
  
namespace Dalworth.Server.Domain
{
    public enum WorkStatusEnum
    {
        ReadyForStartDay = 1,
        StartDayDone = 2,
        Pending = 3,
        Completed = 4
    }

    public partial class WorkStatus
    {
        public WorkStatus() {}
    }
}
      