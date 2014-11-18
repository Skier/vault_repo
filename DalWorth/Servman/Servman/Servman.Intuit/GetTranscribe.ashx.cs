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
    /// Summary description for GetTranscribe
    /// </summary>
    public class GetTranscribe : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            StoreTranscribe(context);
        }

        private static string GetLeadId(HttpRequest request)
        {
            var targetStr = request.QueryString["target"];
            if (string.IsNullOrEmpty(targetStr))
                return "";

            var str = targetStr.Split(':');
            
            return str.Length > 0 ? str[0] : "";
        }

        private static string GetRealmId(HttpRequest request)
        {
            var targetStr = request.QueryString["target"];
            if (string.IsNullOrEmpty(targetStr))
                return "";

            var str = targetStr.Split(':');
            
            return str.Length > 1 ? str[1] : "";
        }

        private static void StoreTranscribe(HttpContext context)
        {
            var request = context.Request;

            var realmId = GetRealmId(request);
            var leadId = GetLeadId(request);
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