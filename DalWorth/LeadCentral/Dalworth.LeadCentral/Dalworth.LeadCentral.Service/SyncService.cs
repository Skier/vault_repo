using System;
using System.Threading;
using Dalworth.LeadCentral.SDK;
using Intuit.Platform.Client.Core;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class SyncService
    {
        public static void SyncCustomers()
        {
            var customers = ServmanCustomer.Find();
            foreach (var customer in customers)
            {
                SyncCustomer(customer);
            }
        }

        public static void SyncCustomer(ServmanCustomer customer)
        {
            var context = GetContext(customer);
            if (context.RequestAuthorizer == null)
                SetCurrentConnectionInactive(customer);

            try
            {
                SyncCustomerBalance(customer, context);
            }
            catch (Exception ex)
            {
                throw new DalworthException(ex);
            }
        }

        private static void SetCurrentConnectionInactive(ServmanCustomer customer)
        {
            var oAuthConnection = OAuthConnection.GetByCustomerId(customer.Id);
            if (oAuthConnection != null)
            {
                oAuthConnection.IsActive = false;
                OAuthConnection.Save(oAuthConnection);
            }
        }

        private static void SyncCustomerBalance(ServmanCustomer customer, PlatformSessionContext context)
        {
            context.ForceAuthentication();

            var billingInfo = context.GetBillingStatus(customer.AppDbId);

            var appStatus = ApplicationStatus.GetByServmanCustomerId(customer.Id);

            if (billingInfo.LastPaymentDate > appStatus.LastPaymentDate)
            {
                //add recurring payment
                var entitlementInfo = context.GetEntitlementValues(customer.AppDbId);
                var price = BillingPlan.GetPriceByName(entitlementInfo.PlanName);
                BillingService.CreateRecurringPaymentTransaction(billingInfo.LastPaymentDate, price,
                                                                 ServmanCustomerService.GetConnection(customer));
                appStatus.LastPaymentDate = billingInfo.LastPaymentDate;
            }

            appStatus.BillingStatus = billingInfo.Status;
            ApplicationStatus.Save(appStatus);
        }

        private static PlatformSessionContext GetContext(ServmanCustomer customer)
        {
            var result = new PlatformSessionContext();

            var oAuthContext = OAuthContext.CreateContext(customer.RealmId);

            if (oAuthContext == null)
                return null;

            result.AppToken = oAuthContext.GetAppToken();
            result.Host = PlatformHost.WorkPlaceSecure;
            result.RequestAuthorizer = oAuthContext.Connector.IsAuthenticated ? oAuthContext.Connector : null;

            return result;
        }

    }
}
