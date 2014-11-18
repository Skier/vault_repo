using System;
using Dalworth.Common.SDK;
using Intuit.Platform.Client.Core;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class SyncService
    {
        public static void SyncCustomers()
        {
            var customers = Customer.Find();
            foreach (var customer in customers)
            {
                SyncCustomer(customer);
            }
        }

        public static void SyncCustomer(Customer customer)
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

        private static void SetCurrentConnectionInactive(Customer customer)
        {
            var oAuthConnection = OAuthConnection.GetByCustomerId(customer.Id);
            if (oAuthConnection != null)
            {
                oAuthConnection.IsActive = false;
                OAuthConnection.Save(oAuthConnection);
            }
        }

        private static void SyncCustomerBalance(Customer customer, PlatformSessionContext context)
        {
            //context.ForceAuthentication();
            var billingInfo = context.GetBillingStatus(customer.AppDbId);
            if (customer.LastPaymentDate == null || billingInfo.LastPaymentDate > customer.LastPaymentDate)
            {
                //add recurring payment
                var entitlementInfo = context.GetEntitlementValues(customer.AppDbId);
                var price = BillingPlan.GetPriceByName(entitlementInfo.PlanName);
                using (var connection = CustomerService.GetConnection(customer))
                {
                    BillingService.CreateRecurringPaymentTransaction(connection, price);
                }
                customer.LastPaymentDate = billingInfo.LastPaymentDate;
            }

            customer.BillingStatus = billingInfo.Status;
            Customer.Save(customer);
        }

        private static PlatformSessionContext GetContext(Customer customer)
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
