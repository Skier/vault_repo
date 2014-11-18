using System;
  
namespace Dalworth.Domain
{
    public enum VisitStatusEnum
    {
        Pending = 1,
        Completed = 2,
        Assigned = 3,
        AssignedForExecution = 4,
        Accepted = 5,
        Declined = 6,
        Arrived = 7,
        NoGo = 8
    }

    public partial class VisitStatus
    {
        public VisitStatus(){}
    }
}
      