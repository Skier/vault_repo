using System;
  
namespace Dalworth.Server.Domain
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
        public VisitStatus(){ }

        #region GetText

        public static string GetText(VisitStatusEnum visitStatus)
        {
            if (visitStatus == VisitStatusEnum.Accepted)
                return "Accepted";
            else if (visitStatus == VisitStatusEnum.Arrived)
                return "Arrived";
            else if (visitStatus == VisitStatusEnum.Assigned)
                return "Assigned";
            else if (visitStatus == VisitStatusEnum.AssignedForExecution)
                return "Dispatched";
            else if (visitStatus == VisitStatusEnum.Completed)
                return "Completed";
            else if (visitStatus == VisitStatusEnum.Declined)
                return "Declined";
            else if (visitStatus == VisitStatusEnum.NoGo)
                return "No Go";
            else if (visitStatus == VisitStatusEnum.Pending)
                return "Pending";

            return string.Empty;            
        }

        #endregion
    }
}
      