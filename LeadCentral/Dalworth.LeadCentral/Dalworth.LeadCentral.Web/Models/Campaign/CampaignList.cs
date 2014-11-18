using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Campaign
{
    public class CampaignList
    {
        public bool ShowClosed { get; set; }

        public List<Domain.Campaign> Campaigns { get; set; }

        public CampaignList()
        {
            
        }

        #region salesreps

        private List<Domain.User> SalesReps;
        

        public int SalesRepId { get; set; }
        public List<Domain.User> SalesRepList
        {
            get
            {
                var result = new List<Domain.User> { new Domain.User { Id = 0, ScreenName = "All" } };
                if (SalesReps != null && SalesReps.Count > 0)
                    result.AddRange(SalesReps);

                return result;
            }
        }

        #endregion

        #region partners
        private List<BusinessPartner> Partners;
        
        public int PartnerId { get; set; }
        public List<BusinessPartner> PartnerList
        {
            get
            {
                var result = new List<BusinessPartner> { new BusinessPartner { Id = 0, PartnerName = "All" } };
                if (Partners != null && Partners.Count > 0)
                    result.AddRange(Partners);

                return result;
            }
        }

        #endregion
        
        public void Load(IDbConnection connection)
        {
            SalesReps = UserService.FindStaff(connection);
            Partners = BusinessPartnerService.Find(connection);
            var filter = new CampaignFilter {PartnerId = PartnerId, SalesRepId = SalesRepId, ShowClosed = ShowClosed};
            Campaigns = CampaignService.LoadCampaigns(filter, connection);
        }
    }
}