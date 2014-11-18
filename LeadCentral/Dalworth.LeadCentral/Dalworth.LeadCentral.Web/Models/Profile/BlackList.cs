using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Profile
{
    public class BlackList
    {
        public List<PhoneBlackList> Phones { get; set; }

        public void Load(IDbConnection connection)
        {
            Phones = PhoneBlackList.Find(connection);
        }
    }
}