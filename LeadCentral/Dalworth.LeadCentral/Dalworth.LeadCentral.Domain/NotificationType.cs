namespace Dalworth.LeadCentral.Domain
{
    public partial class NotificationType
    {
        public NotificationType()
        {
        }
    }

    public enum NotificationTypeEnum
    {
        LeadCreate,
        LeadCancel,
        LeadSetPending,
        LeadSetCompleted,
        LeadMatch,
        LowBalance,
        EmptyBalance,
        RejectCall,
        PurchasePhone
    }
}
      