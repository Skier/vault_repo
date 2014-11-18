using System;
  
namespace Dalworth.Domain
{
    public enum WorkTransactionTypeEnum
    {
        StartDayDone = 1,
        VisitCompleted = 2,
        VisitDeclined = 3,
        VisitAccepted = 4,
        VisitArrived = 5,
        Completed = 6,
        SubmitETC = 7,
        NoGo = 8,
        GPS = 9
    }
    public partial class WorkTransactionType
    {
        public WorkTransactionType(){ }

        #region GetText

        public static string GetText(WorkTransactionTypeEnum type)
        {
            switch (type)
            {
                case WorkTransactionTypeEnum.Completed:
                    return "Completed";
                case WorkTransactionTypeEnum.GPS:
                    return "GPS";
                case WorkTransactionTypeEnum.NoGo:
                    return "No Go";
                case WorkTransactionTypeEnum.StartDayDone:
                    return "Start Day Done";
                case WorkTransactionTypeEnum.SubmitETC:
                    return "Submit ETC";
                case WorkTransactionTypeEnum.VisitAccepted:
                    return "Visit Accepted";
                case WorkTransactionTypeEnum.VisitArrived:
                    return "Visit Arrived";
                case WorkTransactionTypeEnum.VisitCompleted:
                    return "Visit Completed";
                case WorkTransactionTypeEnum.VisitDeclined:
                    return "Visit Declined";                    
            }
            return string.Empty;
        }

        #endregion
    }
}
      