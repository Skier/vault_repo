using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Campaign
{
    public class AssignPhone
    {
        public AssignPhone ()
        {
        }

        public List<TrackingPhone> UnassignedPhones { get; private set; }

        public int CampaignId { get; private set; }

        public Domain.Campaign Campaign { get; private set; }

        public void LoadUnassigned(int campaignId, IDbConnection connection)
        {
            CampaignId = campaignId;
            Campaign = CampaignService.FindByPrimaryKey(campaignId, connection);
            UnassignedPhones = TrackingPhoneService.GetUnassignedPhones(connection);
        }
    }
}