using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Reports
{
    public class PartnersRevenueModel : DateRangeModel
    {
        public List<Domain.Lead> Leads { get; private set; }

        public void Load(IDbConnection connection)
        {
            SalesReps = UserService.FindStaff(connection);

            if (DateTo == null)
                DateTo = DateTime.Now;

            if (DateFrom == null)
                DateFrom = DateTime.Now.AddDays(-7);
            
             Leads = LeadService.FindInvoicedLeads(DateFrom.Value, DateTo.Value, SalesRepId, connection);
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
                {
                    foreach (var user in SalesReps)
                    {
                        result.Add(user);
                    }
                }

                return result;
            }
        }

        #endregion
    }
}