using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.User
{
    public class UserList
    {
        public bool ShowInactive { get; set; }
        public List<Domain.User> Users { get; set; }
        public int PartnerId { get; set; }

        public UserList(){}

        private List<BusinessPartner> m_partners;

        public List<BusinessPartner> PartnerList
        {
            get
            {
                var result = new List<BusinessPartner> { new BusinessPartner { Id = 0, PartnerName = "All" } };
                if (m_partners != null && m_partners.Count > 0)
                    result.AddRange(m_partners);

                return result;
            }
        }
        

        public void Load(IDbConnection connection)
        {
            m_partners = BusinessPartnerService.Find(connection);
            var filter = new UserFilter { PartnerId = PartnerId, ShowInactive = ShowInactive };
            Users = UserService.Find(filter, connection);
        }
    }
}