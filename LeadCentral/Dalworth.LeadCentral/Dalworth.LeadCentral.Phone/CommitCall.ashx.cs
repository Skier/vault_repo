using System;
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

            var servmanCustomer = CustomerService.FindByRealmId(realmId);

            using (var connection = CustomerService.GetConnection(servmanCustomer))
            {

                var call = PhoneCall.GetByTwilioCallId(callSid, connection);
                if (call != null)
                {
                    if (recordingUrl != null)
                        call.RecordingUrl = recordingUrl;

                    var durationStr = callDuration ?? recordingDuration;
                    decimal quantity;
                    decimal.TryParse(durationStr, out quantity);
                    
                    call.CallDuration = quantity;
                    call.Status = callStatus;
                    
                    PhoneCall.Update(call, connection);

                    quantity = Math.Ceiling(quantity / 60);

                    var phone = TrackingPhone.FindByPrimaryKey(call.TrackingPhoneId, connection);
                    BillingService.CreateTransaction(connection, phone.IsTollFree
                                                                  ? TransactionTypeEnum.IncomeTollFreeCall
                                                                  : TransactionTypeEnum.IncomeCall, quantity, null, call.CampaignId);
                }
            }
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