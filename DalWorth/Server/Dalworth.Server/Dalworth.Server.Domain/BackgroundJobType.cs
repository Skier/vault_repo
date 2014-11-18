using System;
  
namespace Dalworth.Server.Domain
{
    public enum BackgroundJobTypeEnum
    {
        LeadRecievedEmail = 1,
        LeadRecievedPrint = 2,
        NotifiyOnCallNewLead = 3,
        ProjectCompletedEmail = 4,
        ProjectFeedbackReceived = 5,
        ProjectFeedbackProcessed = 6,
        PartnerSiteInvitation = 7,
        PartnerSitePasswordReminder = 8,
        PartnerSiteSummaryReport = 9
    }

    public partial class BackgroundJobType
    {
        public BackgroundJobType(){}
    }
}
      