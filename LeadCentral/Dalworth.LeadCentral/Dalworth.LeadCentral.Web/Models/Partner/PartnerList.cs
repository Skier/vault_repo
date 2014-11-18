using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Partner
{
    public class PartnerList
    {
        private List<Domain.User> SalesReps;
        public bool ShowRemoved { get; set; }
        public List<BusinessPartner> Partners { get; set; }

        public PartnerList()
        {
            
        }

        #region salesreps

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

        public void Load(IDbConnection connection)
        {
            SalesReps = UserService.FindStaff(connection);

            var filter = new PartnerFilter {SalesRepId = SalesRepId, ShowRemoved = ShowRemoved};
            Partners = BusinessPartnerService.LoadPartners(filter, connection);
        }
    }
}