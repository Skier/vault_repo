using System.Web;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Phone
{
    /// <summary>
    /// Summary description for CommitCall
    /// </summary>
    public class CommitCall : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;

            var realmId = request["realmId"];
            var callSid = request["CallSid"];
            var recordingUrl = request["RecordingUrl"];
            var recordingDuration = request["RecordingDuration"];
            var callDuration = request["CallDuration"];
            var callStatus = request["CallStatus"];

            var servmanCustomer = ServmanCustomerService.FindByRealmId(realmId);

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {

                var call = PhoneCall.GetByCallSid(callSid, connection);
                if (call != null)
                {
                    if (recordingUrl != null)
                        call.RecordingUrl = recordingUrl;
                    call.CallDuration = callDuration ?? recordingDuration;
                    call.CallStatus = callStatus;
                    PhoneCall.Update(call, connection);

                    var phone = TrackingPhone.FindByPrimaryKey(call.TrackingPhoneId, connection);
                    BillingService.CreatePhoneCallTransaction(call,
                                                              phone.IsTollFree
                                                                  ? TransactionTypeEnum.IncomeTollFreeCall
                                                                  : TransactionTypeEnum.IncomeCall, connection);
                }
            }

/*
            response.ContentType = "text/xml";
            response.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n");
            response.Write("<Response><Say>Thank you for your call.  Goodbye.</Say></Response>");
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