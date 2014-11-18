using System.Web;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Phone
{
    public class CommitRecord : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            var realmId = request["realmId"];
            var callSid = request["CallSid"];
            var recordingUrl = request["RecordingUrl"];

            var servmanCustomer = ServmanCustomerService.FindByRealmId(realmId);

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {

                var call = PhoneCall.GetByCallSid(callSid, connection);
                if (call != null && recordingUrl != null)
                {
                    call.RecordingUrl = recordingUrl;
                    PhoneCall.Update(call, connection);
                }
            }

            response.ContentType = "text/xml";
            response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n");
            response.Write("<Response><Say>Thank you for your call.  Goodbye.</Say></Response>");
            response.End();
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