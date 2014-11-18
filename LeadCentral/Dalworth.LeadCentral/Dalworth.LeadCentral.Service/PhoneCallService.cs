using System;
using System.Net;
using System.Data;

using Dalworth.Common;
using Dalworth.Common.SDK;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class PhoneCallService
    {
        #region ProcessPhoneCalls

        public static void ProcessPhoneCalls()
        {
            var customers = Customer.Find();

            if (customers.Count == 0)
                return;

            foreach (var customer in customers)
            {
                using (var connection = CustomerService.GetConnection(customer))
                {
                    ProcessCustomerCalls(customer, connection);
                }
            }
        }

        #endregion

        #region ProcessPhoneCalls

        private static void ProcessCustomerCalls(Customer customer,IDbConnection connection)
        {
            var phoneCalls = PhoneCall.FindPending(connection);

            foreach (var phoneCall in phoneCalls)
            {
                Host.Trace("PhoneCallService",
                            string.Format("Processing phone call: from {0} [{1}].", phoneCall.FromPhone, phoneCall.Id));

                if (!string.IsNullOrEmpty(phoneCall.TwilioRecordingUrl))
                    phoneCall.RecordingUrl = SaveToFile(phoneCall.TwilioRecordingUrl);

                phoneCall.IsProcessed = true;
                PhoneCall.Update(phoneCall, connection);

                var lead = Lead.FindByCallId(phoneCall.Id, connection);
                NotificationService.SendCreateLeadNotification(customer,lead, connection);

                Host.Trace("PhoneCallService",
                                string.Format("Processing phone call: from {0} [{1}]. Done.", phoneCall.FromPhone,
                                phoneCall.Id));
            }
        }

        #endregion 

        #region SaveToFile

        private static string SaveToFile(string recordingUrl)
        {
            var fileName = Guid.NewGuid() + ".mp3";
            var webClient = new WebClient();

            webClient.DownloadFile(recordingUrl + ".mp3", Configuration.Sync.CallDirectory + "/" + fileName);

            return  fileName;
        }

        #endregion
    }
}
