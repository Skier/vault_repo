using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit
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
            var servmanCustomer = ServmanCustomer.FindByRealmId(realmId);

            var lead = Lead.FindByPrimaryKey(int.Parse(leadId), ServmanCustomerService.GetConnection(servmanCustomer));
            lead.CustomerNotes = transcriptionText;
            LeadService.Save(lead, servmanCustomer);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}