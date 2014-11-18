using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Web.Models.Dashboard
{
    public class StartupModel
    {
        public bool IsCompanyProfileInited { get; private set; }
        public bool IsTrackingPhonesInited { get; private set; }
        public bool IsCampaignsInited { get; private set; }
        public bool IsOAuthInited { get; private set; }

        public StartupModel()
        {
            var customer = ContextHelper.GetCurrentCustomer();
            IsCompanyProfileInited = customer.IsCompanyProfileInited;
            IsTrackingPhonesInited = customer.IsTrackingPhonesInited;
            IsCampaignsInited = customer.IsCampaignsInited;
            IsOAuthInited = customer.IsOAuthInited;
        }
    }
}