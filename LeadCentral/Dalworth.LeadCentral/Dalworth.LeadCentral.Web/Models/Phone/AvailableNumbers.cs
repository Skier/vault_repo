using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Phone
{
    public class AvailableNumbers
    {
        public List<TrackingPhone> AvailablePhoneNumbers { get; private set; }

        public string SelectedArea { get; private set; }

        public void LoadAvailableNumbers(string areaCode, bool isTollFree)
        {
            SelectedArea = areaCode;

            AvailablePhoneNumbers = new List<TrackingPhone>();

            if (!string.IsNullOrEmpty(areaCode))
                AvailablePhoneNumbers = TrackingPhoneService.GetAvailablePhoneNumbers(areaCode, isTollFree);
        }

    }
}