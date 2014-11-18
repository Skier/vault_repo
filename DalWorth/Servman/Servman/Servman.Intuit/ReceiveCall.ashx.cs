using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Servman.Domain;
using Servman.SDK;
using Servman.Service;

namespace Servman.Intuit
{
    /// <summary>
    /// Summary description for ReceiveCall
    /// </summary>
    public class ReceiveCall : IHttpHandler
    {

        private const int WebAccessDurationMin = 3;

        private const string EmergencyCallBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response>
	<Dial action=""{0}"" record=""true"">{1}</Dial>
</Response>
";

        private const string RejectCallBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response><Reject/></Response>
";

        private const string VoiceMailBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response>
	<Say>{0}</Say>
	<Say>{1}</Say>
	<Record maxLength=""30"" action=""{2}"" transcribe=""{3}"" transcribeCallback=""{4}""/>
</Response>
";

        private const string QueueCallBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response>
    <Say>{0}</Say>
    <Say>{1}</Say>
    <Play>{2}</Play>
</Response>
";

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var realmId = request["realmId"];
            var phoneNumber = request["To"];

            var servmanCustomer = ServmanCustomerService.FindByRealmId(realmId);

            PhoneCall phoneCall;
            CallWorkflow workflow;
            TrackingPhone phone;

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                phone = TrackingPhone.GetByPhoneNumber(phoneNumber, connection);
                if (phone == null)
                    throw new DalworthException("Phone Number doesn't present in database");

                phoneCall = CreatePhoneCall(phone, context, connection, servmanCustomer);

                if (!IsBillingStatusOk(servmanCustomer))
                {
                    RejectCall(phoneCall, context, connection);
                    return;
                }
                
                if (phone.IsSuspended || IsBalanceOverdraw(servmanCustomer))
                {
                    RejectCall(phoneCall, context, connection);
                    return;
                }

                workflow = GetCallWorkflow(phoneCall, connection) ?? CallWorkflow.GetByPrimaryKey(1, connection);
            }


            switch (workflow.Id)
            {
                case 1:
                    SetVoicemailResponse(workflow, context, phone, phoneCall, servmanCustomer);
                    break;
                case 2:
                    SetEmergencyCallResponse(workflow, context, phone, phoneCall, servmanCustomer);
                    break;
                case 3:
                    SetQueueCallResponse(workflow, context, phoneCall, servmanCustomer);
                    break;
                default:
                    SetVoicemailResponse(workflow, context, phone, phoneCall, servmanCustomer);
                    break;
            }
        }

        private static bool IsBalanceOverdraw(ServmanCustomer servmanCustomer)
        {
            return BaseService.GetCurrentBalance(servmanCustomer) <= 0;
        }

        private static bool IsBillingStatusOk(ServmanCustomer servmanCustomer)
        {
            return BaseService.IsBillingStatusOk(servmanCustomer);
        }

        private static void RejectCall(PhoneCall phoneCall, HttpContext context, IDbConnection connection)
        {
            phoneCall.CallStatus = "rejected";
            PhoneCall.Save(phoneCall, connection);

            var response = context.Response;
            response.ContentType = "text/xml";
            response.Write(RejectCallBody);
            response.End();
        }

        private static CallWorkflow GetCallWorkflow(PhoneCall phoneCall, IDbConnection connection)
        {
            List<PhoneCallWorkflow> workflowRules = PhoneCallWorkflow.GetByTrackingPhoneId(phoneCall.TrackingPhoneId, connection);
            foreach (var rule in workflowRules)
            {
                if (rule.IsMatch(phoneCall))
                    return rule.RelatedWorkflow;
            }
            
            return null;
        }

        private static void SetVoicemailResponse(CallWorkflow workflow, HttpContext context, TrackingPhone phone, PhoneCall phoneCall, ServmanCustomer servmanCustomer)
        {
            var response = context.Response;

            var realmId = servmanCustomer.RealmId;
            var lead = CreateLead(phoneCall, servmanCustomer);

            var commitRecordUrl = string.Format("CommitRecord.ashx?realmId={0}&amp;callId={1}", realmId, phoneCall.Id);
            var commitLeadUrl = string.Format("CommitLead.ashx?realmId={0}&amp;leadId={1}", realmId, lead.Id);

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                phoneCall.CallStatus = "recording";
                phoneCall.IsAnsweredByUser = true;
                phoneCall.AnsweredByUserId = null;
                phoneCall.Direction = "inbound";
                PhoneCall.Save(phoneCall, connection);
            }

            response.ContentType = "text/xml";
            response.Write(string.Format(VoiceMailBody, 
                                         workflow.DetailsHash[WorkflowDetail.VoiceMailWelcomeMessageKey],
                                         workflow.DetailsHash[WorkflowDetail.VoiceMailMessageKey],
                                         commitRecordUrl, 
                                         phone.DenyTranscription ? "false" : "true",
                                         phone.DenyTranscription ? "" : commitLeadUrl
                                         ));
            response.End();
        }

        private static void SetEmergencyCallResponse(CallWorkflow workflow, HttpContext context, TrackingPhone phone, PhoneCall phoneCall, ServmanCustomer servmanCustomer)
        {
            if (string.IsNullOrEmpty((string)workflow.DetailsHash[WorkflowDetail.RedirectPhoneNumberKey]))
            {
                SetVoicemailResponse(workflow, context, phone, phoneCall, servmanCustomer);
                return;
            }

            var response = context.Response;
            var realmId = servmanCustomer.RealmId;

            CreateLead(phoneCall, servmanCustomer);

            var commitLeadUrl = string.Format("CommitDial.ashx?realmId={0}", realmId);

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                phoneCall.CallStatus = "emergency";
                phoneCall.IsAnsweredByUser = true;
                phoneCall.AnsweredByUserId = null;
                phoneCall.Direction = "outbound-dial";
                PhoneCall.Update(phoneCall, connection);
            }

            response.ContentType = "text/xml";
            response.Write(string.Format(EmergencyCallBody, commitLeadUrl, workflow.DetailsHash[WorkflowDetail.RedirectPhoneNumberKey]));
            response.End();
        }

        private static void SetQueueCallResponse(CallWorkflow workflow, HttpContext context, PhoneCall phoneCall, ServmanCustomer servmanCustomer)
        {
            var response = context.Response;

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                phoneCall.CallStatus = "queued";
                phoneCall.IsAnsweredByUser = false;
                phoneCall.AnsweredByUserId = null;
                phoneCall.Direction = "outbound-dial";
                PhoneCall.Update(phoneCall, connection);
            }

            response.ContentType = "text/xml";
            response.Write(string.Format(QueueCallBody, 
                                         workflow.DetailsHash[WorkflowDetail.QueueWelcomeMessageFirstKey],
                                         workflow.DetailsHash[WorkflowDetail.QueueWelcomeMessageSecondKey],
                                         workflow.DetailsHash[WorkflowDetail.QueueSoundUrlKey]));
            response.End();
        }

        private static PhoneCall CreatePhoneCall(TrackingPhone phone, HttpContext context, IDbConnection connection, ServmanCustomer servmanCustomer)
        {
            var request = context.Request;

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
                CallerName = request["CallerName"] ?? "unknown",
                IsAnsweredByUser = false,
                TrackingPhoneId = phone.Id,
                DateCreated = DateTime.Now
            };
            
            call.LeadSourceId = GetLeadSourceId(call, servmanCustomer);

            PhoneCall.Save(call, connection);

            SyncWithPhoneRotation(call, servmanCustomer);

            BillingService.CreateCallerIdLookupTransaction(call, connection);

            return call;
        }

        private static Lead CreateLead(PhoneCall phoneCall, ServmanCustomer servmanCustomer)
        {
            var lead = new Lead
            {
                LeadStatusId = (int)LeadStatusEnum.New,
                Phone = phoneCall.PhoneFrom,
                DateCreated = DateTime.Now,
                LeadSourceId = phoneCall.LeadSourceId,
                FirstName = phoneCall.CallerName,
                LastName = "",
                CustomerNotes = "",
                PhoneCallId = phoneCall.Id
            };

            lead.UpdateNullable();

            return LeadService.Save(lead, servmanCustomer);
        }

        private static int? GetLeadSourceId(PhoneCall phoneCall, ServmanCustomer servmanCustomer)
        {
            List<LeadSource> leadSources =
                LeadSourceService.GetByPhoneNumber(phoneCall.PhoneFrom, servmanCustomer);
            if (leadSources.Count == 1 && leadSources[0].IsActive)
                return leadSources[0].Id;

            leadSources = LeadSourceService.GetByTrackingPhoneNumber(phoneCall.TrackingPhoneId, servmanCustomer);
            if (leadSources.Count == 1 && leadSources[0].IsActive)
                return leadSources[0].Id;

            return null;
        }

        private static void SyncWithPhoneRotation(PhoneCall phoneCall, ServmanCustomer servmanCustomer)
        {
            var duration = new TimeSpan(0, WebAccessDurationMin, 0);
            TrackingPhoneRotation trackingPhoneRotation = PhoneService.GetLastPhoneRotationByPhoneId(phoneCall.TrackingPhoneId, servmanCustomer);
            if (trackingPhoneRotation != null 
                && trackingPhoneRotation.TimeDisplay.Add(duration) >= DateTime.Now)
            {
                trackingPhoneRotation.PhoneCallId = phoneCall.Id;
                trackingPhoneRotation.LeadSourceId = phoneCall.LeadSourceId;
                PhoneService.SavePhoneRotation(trackingPhoneRotation, servmanCustomer);
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