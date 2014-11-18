using System.Collections.Generic;

namespace Dalworth.LeadCentral.Domain
{
    public partial class PhoneSms
    {
        public List<TrackingPhoneRotation> TrackingPhoneRotations { get; set; }

        public PhoneSms()
        {
        }

        public bool IsFromPhoneBlackListed { get; set; }
    }
}
      