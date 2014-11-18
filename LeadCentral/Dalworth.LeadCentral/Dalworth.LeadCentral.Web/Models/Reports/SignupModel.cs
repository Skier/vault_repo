using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Reports
{
    public class SignupModel : DateRangeModel
    {
        public List<BusinessPartner> Partners { get; private set; }

        public void Load(IDbConnection connection)
        {
            if (DateTo == null)
                DateTo = DateTime.Now;

            if (DateFrom == null)
                DateFrom = DateTime.Now.AddDays(-7);

            Partners = BusinessPartnerService.FindByDatePeriod(DateFrom, DateTo, connection);
        }
    }
}