using System.Web;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Phone
{
    /// <summary>
    /// Summary description for CommitLead
    /// </summary>
    public class CommitLead : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StoreTranscription(context);
        }

        private static void StoreTranscription(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            var realmId = request["realmId"];
            var leadId = request["leadId"];
            var callSid = request["CallSid"];
            var transcriptionText = request["TranscriptionText"];
            var transcriptionStatus = request["TranscriptionStatus"];

            if (transcriptionStatus.ToLower() != "completed")
                return;

            var servmanCustomer = ServmanCustomerService.FindByRealmId(realmId);

            using(var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                var phoneCall = PhoneCall.GetByCallSid(callSid, connection);
                var lead = Lead.FindByPrimaryKey(int.Parse(leadId), connection);
                lead.CustomerNotes = transcriptionText;

                Lead.Save(lead, connection);

                BillingService.CreateTranscribeTransaction(phoneCall, connection);
            }

            response.ContentType = "text/xml";
            response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n");
            response.Write("<Response><Say>Thank you for your call.  Goodbye.</Say></Response>");
            response.End();
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}