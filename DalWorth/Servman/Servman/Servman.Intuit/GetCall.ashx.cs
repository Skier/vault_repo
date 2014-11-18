using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using Servman.Domain;
using Servman.Service;

namespace Servman.Intuit
{
    /// <summary>
    /// Summary description for GetCall
    /// </summary>
    public class GetCall : IHttpHandler
    {

        private const string EmergencyPhoneNumber = "+12143354143";

        const string EmergencyCallBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response>
	<Dial action=""{0}"" record=""true"">{1}</Dial>
</Response>
";

        const string VoiceMailBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response>
	<Say>Please leave your message after tone.</Say>
	<Record maxLength=""30"" action=""{0}"" transcribe=""true"" transcribeCallback=""{1}""/>
</Response>
";

        const string QueueCallBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response>
    <Say>Hello, thank you for calling to Dalworth restoration.</Say>
    <Say>Please wait for available dispatcher.</Say>
    <Play>ith_chopin-15-2_sample.wav</Play>
</Response>
";

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var realmId = request["realmId"];

            SDK.Configuration.ConnectionString = ConfigurationManager.AppSettings["mainConnectionString"];
            var servmanCustomer = ServmanCustomer.FindByRealmId(realmId);

            CallWorkflow workflow;
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                var phoneCall = StorePhoneCall(context, connection);
                workflow = CallWorkflow.GetByPhoneCall(phoneCall, connection);
            }


            switch (workflow.Id)
            {
                case 1:
                    SetVoicemailResponse(context, CreateLead(context).Id);
                    break;
                case 2:
                    SetEmergencyCallResponse(context, CreateLead(context).Id);
                    break;
                case 3:
                    SetQueueCallResponse(context);
                    break;
            }
        }

        private static PhoneCall StorePhoneCall(HttpContext context, IDbConnection connection)
        {
            var request = context.Request;

            var phoneNumber = request["To"];
            var phone = Phone.GetByPhoneNumber(phoneNumber, connection);

            var call = new PhoneCall
                           {
                               CallSid = request["CallSid"],
                               AccountSid = request["AccountSid"],
                               PhoneFrom = request["From"],
                               PhoneTo = request["To"],
                               CallStatus = request["CallStatus"],
                               ApiVersion = request["ApiVersion"],
                               Direction = request["Direction"],
                               ForwardedFrom = request["ForwardedFrom"],
                               FromCity = request["FromCity"],
                               FromState = request["FromState"],
                               FromZip = request["FromZip"],
                               FromCountry = request["FromCountry"],
                               ToCity = request["ToCity"],
                               ToState = request["ToState"],
                               ToZip = request["ToZip"],
                               ToCountry = request["ToCountry"],
                               IsAnsweredByUser = false,
                               ToPhoneId = phone.Id
                           };


            PhoneCall.Insert(call, connection);

            return call;
        }

        private static Lead CreateLead(HttpContext httpContext)
        {
            var request = httpContext.Request;

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

            return LeadService.Save(lead, servmanCustomer);
        }

        private static void SetVoicemailResponse(HttpContext context, int leadId)
        {
            var request = context.Request;
            var response = context.Response;

            var realmId = request["realmId"];

            var commitLeadUrl = string.Format("CommitLead.ashx?realmId={0}&amp;leadId={1}", realmId, leadId);
            var commitTranscribeUrl = string.Format("CommitTranscribe.ashx?realmId={0}&amp;leadId={1}", realmId, leadId);

            response.ContentType = "text/xml";
            response.Write(string.Format(VoiceMailBody, commitLeadUrl, commitTranscribeUrl));
            response.End();
        }

        private static void SetEmergencyCallResponse(HttpContext context, int leadId)
        {
            var request = context.Request;
            var response = context.Response;

            var realmId = request["realmId"];

            var commitLeadUrl = string.Format("CommitLead.ashx?realmId={0}&amp;leadId={1}", realmId, leadId);

            response.ContentType = "text/xml";
            response.Write(string.Format(EmergencyCallBody, commitLeadUrl, EmergencyPhoneNumber));
            response.End();
        }

        private static void SetQueueCallResponse(HttpContext context)
        {
            var response = context.Response;

            response.ContentType = "text/xml";
            response.Write(QueueCallBody);
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