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
        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;

            var realmId = request["realmId"];
            var callSid = request["CallSid"];
            var callDuration = request["DialCallDuration"];
            var recordingUrl = request["RecordingUrl"];

            var servmanCustomer = ServmanCustomerService.FindByRealmId(realmId);

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {

                var call = PhoneCall.GetByCallSid(callSid, connection);
                if (call != null)
                {
                    if (recordingUrl != null)
                        call.RecordingUrl = recordingUrl;
                    PhoneCall.Update(call, connection);

                    BillingService.CreatePhoneDialTransaction(call, callDuration, TransactionTypeEnum.OutcomeCall, connection);
                }
            }

/*
            response.ContentType = "text/xml";
            response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n");
            response.End();
*/
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