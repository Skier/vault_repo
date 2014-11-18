using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit
{
    /// <summary>
    /// Summary description for CreateLead
    /// </summary>
    public class CreateLead : IHttpHandler
    {
        private const string ActionSelectTarget = "selectTarget";
        private const string ActionStoreRecording = "storeRecording";

        const string WelcomeBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response>
	<Say>Hello. Welcome to Dalworth restoration.</Say>
	<Gather numDigits=""1"" action=""{0}"" method=""POST"">
		<Say>To speak with a dispatcher, press 1.  Press 2 to record your message. Press any other key to start over.</Say>
	</Gather>
</Response>
";

        const string SelectDispatcherBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response>
	<Dial action=""{0}"" record=""true"">2143354143</Dial>
</Response>
";

        const string SelectVoiceMailBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response>
	<Say>Please leave your message after tone.</Say>
	<Record maxLength=""30"" action=""{0}"" transcribe=""true"" transcribeCallback=""{1}""/>
</Response>
";

        public void ProcessRequest(HttpContext context)
        {
            var action = GetAction(context.Request);

            if (action == ActionSelectTarget)
                SelectTarget(context);
            else if (action == ActionStoreRecording)
                StoreRecording(context);
            else
                StoreLead(context);
        }

        private static string GetAction(HttpRequest request)
        {
            var actionStr = request.QueryString["action"];
            if (string.IsNullOrEmpty(actionStr))
                return "";
            
            var str = actionStr.Split(':');
            if (str.Length > 0)
                return str[0];
            
            return "";
        }

        private static string GetLeadId(HttpRequest request)
        {
            var actionStr = request.QueryString["action"];
            if (string.IsNullOrEmpty(actionStr))
                return "";

            var str = actionStr.Split(':');
            if (str.Length > 1)
                return str[1];
            return "";
        }

        private static string GetRealmId(HttpRequest request)
        {
            var actionStr = request.QueryString["action"];
            if (string.IsNullOrEmpty(actionStr))
                return "";

            var str = actionStr.Split(':');
            if (str.Length > 2)
                return str[2];
            return "";
        }

        private static void StoreRecording(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            var realmId = GetRealmId(request);
            var leadId = GetLeadId(request);
            var recordingUrl = request["RecordingUrl"];

            SDK.Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            var servmanCustomer = ServmanCustomer.FindByRealmId(realmId);

            var lead = Lead.FindByPrimaryKey(int.Parse(leadId), ServmanCustomerService.GetConnection(servmanCustomer));
            lead.VoiceFileUrl = recordingUrl;
            LeadService.Save(lead, servmanCustomer);

            //response.Clear();
            response.ContentType = "text/xml";
            response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n");
            response.Write("<Response><Say>Thank you for your call.  Goodbye.</Say></Response>");
            response.End();
        }

        private static void SelectTarget(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            var realmId = GetRealmId(request);
            var leadId = GetLeadId(request);
            var digits = request["Digits"];

            const string url = "CreateLead.ashx?action={0}";
            const string transcribeCallbackUrl = "GetTranscribe.ashx?target={0}";

            string body;
            if (digits == "1")
                body = string.Format(SelectDispatcherBody, string.Format(url, (ActionStoreRecording + ":" + leadId + ":" + realmId)));
            else if (digits == "2")
                body = string.Format(SelectVoiceMailBody, 
                    string.Format(url, (ActionStoreRecording + ":" + leadId + ":" + realmId)),
                    string.Format(transcribeCallbackUrl, (leadId + ":" + realmId))
                    );
            else
                body = string.Format(WelcomeBody, string.Format(url, (ActionSelectTarget + ":" + leadId + ":" + realmId)));

            response.ContentType = "text/xml";
            response.Write(body);
            response.End();
        }

        private static void StoreLead(HttpContext httpContext)
        {
            var request = httpContext.Request;
            var response = httpContext.Response;

            var realmId = request["realmId"];
            var phoneFrom = request["From"];
            var phoneTo = request["To"];
            var callerName = request["CallerName"];

            SDK.Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            var servmanCustomer = ServmanCustomer.FindByRealmId(realmId);

            var lead = new Lead
                           {
                               ProjectTypeId = 1,
                               LeadStatusId = 1,
                               IsCreatedByPhoneCall = true,
                               Phone = phoneFrom,
                               ToPhoneNumber = phoneTo,
                               BusinessPartnerId = GetBusinessPartnerId(phoneFrom, phoneTo, servmanCustomer),
                               DateCreated = DateTime.Now,
                               FirstName = callerName ?? "unknown",
                               LastName = "",
                               CustomerNotes = ""
                           };

            LeadService.Save(lead, servmanCustomer);

            var url = string.Format("CreateLead.ashx?action={0}", (ActionSelectTarget + ":" + lead.Id + ":" + realmId) );
            //response.Clear();
            response.ContentType = "text/xml";
            response.Write(string.Format(WelcomeBody, url));
            response.End();
        }

        private static int? GetBusinessPartnerId(string phoneFrom, string phoneTo, ServmanCustomer servmanCustomer)
        {
            List<BusinessPartner> businessPartners = BusinessPartnerService.GetByPhoneNumbers(phoneFrom, phoneTo, servmanCustomer);
            if (businessPartners.Count == 1)
                return businessPartners[0].Id;

            return null;
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
