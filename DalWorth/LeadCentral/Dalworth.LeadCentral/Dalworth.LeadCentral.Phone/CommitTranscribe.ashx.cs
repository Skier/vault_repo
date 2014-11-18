using System.Configuration;
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

            SDK.Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            var servmanCustomer = ServmanCustomerService.FindByRealmId(realmId);

            var lead = Lead.FindByPrimaryKey(int.Parse(leadId), ServmanCustomerService.GetConnection(servmanCustomer));
            lead.CustomerNotes = transcriptionText;
            LeadService.Save(servmanCustomer, lead);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}