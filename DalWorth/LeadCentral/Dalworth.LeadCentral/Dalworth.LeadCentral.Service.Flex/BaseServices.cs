using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service.Flex
{
    public class BaseServices
    {
        public User GetCurrentUser(string ticket)
        {
            return BaseService.GetCurrentUser(ticket);
        }

        public string Init(string intuitTicket, string realmId, string dbId)
        {
            if (string.IsNullOrEmpty(intuitTicket))
            {
                var testDb = 1;
                switch (testDb)
                {
                    case 1: //qb account
                        realmId = "182801782";
                        dbId = "bfit8895q";
                        break;
                    case 2: //qbo account
                        realmId = "188178376";
                        dbId = "bfzu3u4tn";
                        break;
                    default: //test account
                        realmId = "189154046";
                        dbId = "bfyuhfvza";
                        break;
                }
            }

            return BaseService.Init(intuitTicket, realmId, dbId);
        }

        public decimal GetCurrentBalance(string ticket)
        {
            return Service.BillingService.GetCurrentBalance(ticket);
        }

        public bool GetOAuthConnectionStatus(string ticket)
        {
            return BaseService.GetOAuthConnectionStatus(ticket);
        }

        public string GetOAuthUrl(string ticket)
        {
            return BaseService.GetOAuthUrl(ticket);
        }

        public string GetPaymentUrl(string ticket)
        {
            return BaseService.GetPaymentUrl(ticket);
        }

/*
        public static ServmanCustomer UpdateServmanCustomer(string ticket, ServmanCustomer servmanCustomer)
        {
            return BaseService.UpdateServmanCustomer(ticket, servmanCustomer);
        }
*/
        
    }
}