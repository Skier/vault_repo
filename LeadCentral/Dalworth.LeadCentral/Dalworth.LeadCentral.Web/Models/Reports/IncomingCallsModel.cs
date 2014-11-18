using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Reports
{
    public class IncomingCallsModel : DateRangeModel
    {
        public List<Domain.Lead> Leads { get; private set; }

        public void Load(IDbConnection connection)
        {
            if (DateTo == null)
                DateTo = DateTime.Now;

            if (DateFrom == null)
                DateFrom = DateTime.Now.AddDays(-7);

            Leads = LeadService.FindPhoneLeadsByPeriod(DateFrom.Value, DateTo.Value, connection);
        }
    }
}