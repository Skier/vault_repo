using System;
using System.Web;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Phone
{
    /// <summary>
    /// Summary description for CommitDial
    /// </summary>
    public class CommitDial : IHttpHandler
    {
        private const string CommitBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response>
</Response>
";

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;

            var realmId = request["realmId"];
            var callSid = request["CallSid"];
            var callDuration = request["DialCallDuration"];
            var recordingUrl = request["RecordingUrl"];

            var servmanCustomer = CustomerService.FindByRealmId(realmId);
            using (var connection = CustomerService.GetConnection(servmanCustomer))
            {

                var phoneCall = PhoneCall.GetByTwilioCallId(callSid, connection);
                if (phoneCall != null)
                {
                    phoneCall.TwilioRecordingUrl = recordingUrl;
                    phoneCall.IsProcessed = false;

                    PhoneCall.Update(phoneCall, connection);

                    decimal quantity;
                    decimal.TryParse(callDuration, out quantity);
                    quantity = Math.Ceiling(quantity / 60);
                    
                    BillingService.CreateTransaction(connection, TransactionTypeEnum.OutcomeCall, quantity, null, phoneCall.CampaignId);
                }
            }
            
            context.Response.ContentType = "text/xml";
            context.Response.Write(CommitBody);
            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}