using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Phone
{
    public class PhoneList
    {
        public bool ShowRemoved { get; private set; }
        public List<TrackingPhone> Phones { get; set; }

        public void LoadActive(IDbConnection connection)
        {
            Phones = TrackingPhoneService.GetAllActive(connection);
            ShowRemoved = false;
        }

        public void LoadAll(IDbConnection connection)
        {
            Phones = TrackingPhoneService.GetAll(connection);
            ShowRemoved = true;
        }
    }
}