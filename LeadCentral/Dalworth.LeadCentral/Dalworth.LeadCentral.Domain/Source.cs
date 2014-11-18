
using System;
  

namespace Dalworth.LeadCentral.Domain
{
    public partial class Source
    {
        public User RelatedUser { get; set; }
        public PhoneCall RelatedPhoneCall { get; set; }
        public PhoneSms RelatedPhoneSms { get; set; }
        public WebForm RelatedWebForm { get; set; }

        public Source()
        {
        }

        public SourceEnum Type 
        {
            get 
            {
                if (PhoneCallId != null)
                    return SourceEnum.PhoneCall;
                if (PhoneSmsId != null)
                    return SourceEnum.PhoneSms;
                if (WebFormId != null)
                    return SourceEnum.WebForm;

                return SourceEnum.User;
            }
        }
    }

    public enum SourceEnum
    {
        User,
        PhoneCall,
        PhoneSms,
        WebForm
    }
}
      