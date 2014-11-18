using System.Web;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Phone
{
    /// <summary>
    /// Summary description for CommitTranscribe
    /// </summary>
    public class CommitTranscribe : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            StoreTranscribe(context);
        }

        private static void StoreTranscribe(HttpContext context)
        {
            var request = context.Request;

            var realmId = request["realmId"];
            var leadId = request["leadId"];
            var transcriptionText = request["TranscriptionText"];
            var transcriptionStatus = request["TranscriptionStatus"];

            if (transcriptionStatus.ToLower() != "completed")
                return;

            var servmanCustomer = CustomerService.FindByRealmId(realmId);

            using (var connection = CustomerService.GetConnection(servmanCustomer))
            {
                var lead = Lead.FindByPrimaryKey(int.Parse(leadId), connection);
                lead.CustomerNotes = transcriptionText;
                Lead.Update(lead, connection);
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}