using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using Servman.Domain;
using Servman.SDK;
using Twilio;
using Twilio.Model;

namespace Servman.Service
{
    public class PhoneService
    {
        private const string ReceiveCallHandler = "ReceiveCall.ashx";
        private const string ReceiveSmsHandler = "ReceiveSms.ashx";
        private const string CommitCallHandler = "CommitCall.ashx";

        private const string TwilioAccountSidKey = "TWILIO_ACCOUNT_SID";
        private const string TwilioAppTokenKey = "TWILIO_APP_TOKEN";

        private const string TwilioHandlerHostKey = "TWILIO_HANDLER_BASE";

        private const string WebTimeoutKey = "WEB__TIMEOUT";

        private static string GetBaseUrl()
        {
            return ConfigurationManager.AppSettings[TwilioHandlerHostKey];
        }

        private static string ReceiveCallHandlerUrl
        {
            get { return GetBaseUrl() + ReceiveCallHandler; }
        }

        private static string ReceiveSmsHandlerUrl
        {
            get { return GetBaseUrl() + ReceiveSmsHandler; }
        }

        private static string CommitCallHandlerUrl
        {
            get { return GetBaseUrl() + CommitCallHandler; }
        }

        private static string GetTwilioAccountSid()
        {
            return ConfigurationManager.AppSettings[TwilioAccountSidKey];
        }

        private static string GetTwilioAppToken()
        {
            return ConfigurationManager.AppSettings[TwilioAppTokenKey];
        }

        public static List<PhoneCall> GetActiveCalls()
        {
            List<PhoneCall> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = PhoneCall.GetActiveCalls(connection);
            }
            return result;
        }

        public static void HandleCall(string callSid, User user, string redirectPhoneNumber)
        {
            string redirectUrl;

            using (IDbConnection connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                PhoneCall currentCall = PhoneCall.GetByCallSid(callSid, connection);
                if (currentCall == null)
                    throw (new DalworthException(string.Format("Can't find call with Sid = {0}", callSid)));
                
                if (currentCall.IsAnsweredByUser)
                    throw (new DalworthException("Current call already answered by another user."));

                currentCall.IsAnsweredByUser = true;
                currentCall.AnsweredByUserId = user.Id;
                PhoneCall.Update(currentCall, connection);

                var redirectHandler = string.Format("RedirectCall.ashx?realmId={0}&callId={1}&phoneNumber={2}", 
                    ContextHelper.GetRealmId(), currentCall.Id, redirectPhoneNumber);
                redirectUrl = GetBaseUrl() + redirectHandler;
            }

            var twilio = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());

            twilio.RedirectCall(callSid, redirectUrl, RestSharp.Method.POST);
        }

        public static void HandleCall(string callSid, User user)
        {
            HandleCall(callSid, user, user.Phone);
        }

        public static List<TrackingPhone> GetPhoneNumbers()
        {
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                return GetPhoneNumbers(connection);
            }
        }

        public static List<TrackingPhone> GetPhoneNumbers(IDbConnection connection)
        {
            var result = TrackingPhone.GetPhoneNumbers(connection);

            foreach (var phone in result)
            {
                phone.LeadSourceTrackingPhones = LeadSourceTrackingPhone.GetByTrackingPhoneId(phone.Id, connection).ToArray();
            }

            return result;
        }

        public static List<TrackingPhone> GetAvailablePhoneNumbers(string areaCode)
        {
            var result = new List<TrackingPhone>();

            var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
            var twilioResult = twilioApi.ListAvailableLocalPhoneNumbers("US", new AvailablePhoneNumberListRequest { AreaCode = areaCode });
            if (twilioResult != null)
            {
                var phoneList = twilioResult.AvailablePhoneNumbers;
                foreach (var availablePhone in phoneList)
                {
                    var phone = new TrackingPhone
                                    {
                                        Number = availablePhone.PhoneNumber,
                                        ScreenNumber = availablePhone.FriendlyName,
                                        Description = availablePhone.FriendlyName
                                    };
                    result.Add(phone);
                }
            }

            return result;
        }

        public static List<TrackingPhone> GetAvailableTollFreePhoneNumbers(string areaCode)
        {
            var result = new List<TrackingPhone>();

            var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
            var twilioResult = twilioApi.ListAvailableTollFreePhoneNumbers("US", areaCode);
            if (twilioResult != null)
            {
                var phoneList = twilioResult.AvailablePhoneNumbers;
                foreach (var availablePhone in phoneList)
                {
                    var phone = new TrackingPhone
                                    {
                                        Number = availablePhone.PhoneNumber,
                                        ScreenNumber = availablePhone.FriendlyName,
                                        Description = availablePhone.FriendlyName
                                    };
                    result.Add(phone);
                }
            }

            return result;
        }

        public static TrackingPhone PurchasePhoneNumber(TrackingPhone phone)
        {
            var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
            var options = new PhoneNumberOptions
                              {
                                  PhoneNumber = phone.Number,
                                  FriendlyName = phone.ScreenNumber,
                                  VoiceCallerIdLookup = !phone.DenyCallerId,
                                  VoiceUrl =
                                      string.Format("{0}?realmId={1}", ReceiveCallHandlerUrl, ContextHelper.GetCurrentCustomer().RealmId),
                                  VoiceMethod = HttpMethod.Post,
                                  SmsUrl = 
                                      string.Format("{0}?realmId={1}", ReceiveSmsHandlerUrl, ContextHelper.GetCurrentCustomer().RealmId),
                                  SmsMethod = HttpMethod.Post,
                                  StatusCallback =
                                      string.Format("{0}?realmId={1}", CommitCallHandlerUrl, ContextHelper.GetCurrentCustomer().RealmId),
                                  StatusCallbackMethod = HttpMethod.Post
                              };

            var phoneNumber = twilioApi.AddLocalPhoneNumber(options);
            phone.TwilioId = phoneNumber.Sid;

            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                phone.IsTollFree = false;
                TrackingPhone.Insert(phone, connection);
                BillingService.CreatePhoneNumberTransaction(phone, connection);
            }

            return phone;
        }

        public static TrackingPhone PurchaseTollFreePhoneNumber(TrackingPhone phone)
        {
            var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
            var options = new PhoneNumberOptions();
            options.PhoneNumber = phone.Number;
            options.FriendlyName = phone.ScreenNumber;
            options.VoiceCallerIdLookup = !phone.DenyCallerId;
            options.VoiceUrl = string.Format("{0}?realmId={1}", ReceiveCallHandlerUrl, ContextHelper.GetCurrentCustomer().RealmId);
            options.VoiceMethod = HttpMethod.Post;
            options.SmsUrl = string.Format("{0}?realmId={1}", ReceiveSmsHandlerUrl, ContextHelper.GetCurrentCustomer().RealmId);
            options.SmsMethod = HttpMethod.Post;
            options.StatusCallback = string.Format("{0}?realmId={1}", CommitCallHandlerUrl, ContextHelper.GetCurrentCustomer().RealmId);
            options.StatusCallbackMethod = HttpMethod.Post;

            var phoneNumber = twilioApi.AddTollFreePhoneNumber(options);
            phone.TwilioId = phoneNumber.Sid;

            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                phone.IsTollFree = true;
                TrackingPhone.Insert(phone, connection);
                BillingService.CreatePhoneNumberTransaction(phone, connection);
            }

            return phone;
        }

        public static TrackingPhone Save(TrackingPhone phone)
        {
            var twilioPhone = UpdateCallerIdLookup(phone.TwilioId, !phone.DenyCallerId);
            if (twilioPhone != null)
                phone.DenyCallerId = !twilioPhone.VoiceCallerIdLookup;

            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                TrackingPhone.Save(phone, connection);
                phone.LeadSourceTrackingPhones = LeadSourceTrackingPhone.GetByTrackingPhoneId(phone.Id, connection).ToArray();
                return phone;
            }
        }

        private static IncomingPhoneNumber UpdateCallerIdLookup(string twilioSid, bool lookup)
        {
            if (!string.IsNullOrEmpty(twilioSid))
            {
                var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
                var twilioPhone = twilioApi.GetIncomingPhoneNumber(twilioSid);
                var options = new PhoneNumberOptions {VoiceCallerIdLookup = lookup};

                return twilioApi.UpdateIncomingPhoneNumber(twilioPhone.Sid, options);
            }
            return null;
        }

        public static TrackingPhone SuspendPhoneNumber(TrackingPhone phone)
        {
            if (!string.IsNullOrEmpty(phone.TwilioId))
            {
                var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
                var twilioPhone = twilioApi.GetIncomingPhoneNumber(phone.TwilioId);
                var options = GetOptions(twilioPhone);
                
                options.SmsUrl = "";
                options.SmsMethod = null;
                options.SmsFallbackUrl = null;
                options.SmsFallbackMethod = null;

                twilioApi.UpdateIncomingPhoneNumber(twilioPhone.Sid, options);
            }

            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                phone.IsSuspended = true;
                TrackingPhone.Save(phone, connection);
                phone.LeadSourceTrackingPhones = LeadSourceTrackingPhone.GetByTrackingPhoneId(phone.Id, connection).ToArray();
                return phone;
            }
        }

        public static TrackingPhone ActivatePhoneNumber(TrackingPhone phone)
        {
            if (!string.IsNullOrEmpty(phone.TwilioId))
            {
                var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
                var twilioPhone = twilioApi.GetIncomingPhoneNumber(phone.TwilioId);
                var options = GetOptions(twilioPhone);
                
                options.SmsUrl = string.Format("{0}?realmId={1}", ReceiveSmsHandlerUrl, ContextHelper.GetCurrentCustomer().RealmId);
                
                twilioApi.UpdateIncomingPhoneNumber(twilioPhone.Sid, options);
            }

            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                phone.IsSuspended = false;
                TrackingPhone.Save(phone, connection);
                phone.LeadSourceTrackingPhones = LeadSourceTrackingPhone.GetByTrackingPhoneId(phone.Id, connection).ToArray();
                return phone;
            }
        }

        private static PhoneNumberOptions GetOptions(IncomingPhoneNumber phone)
        {
            var result = new PhoneNumberOptions();
            
            result.FriendlyName = phone.FriendlyName;
            result.PhoneNumber = phone.PhoneNumber;
            result.SmsFallbackMethod = HttpMethod.Post;
            result.SmsFallbackUrl = phone.SmsFallbackUrl;
            result.SmsMethod = HttpMethod.Post;
            result.SmsUrl = phone.SmsUrl;
            result.StatusCallback = phone.StatusCallback;
            result.StatusCallbackMethod = HttpMethod.Post;
            result.VoiceCallerIdLookup = phone.VoiceCallerIdLookup;
            result.VoiceFallbackMethod = HttpMethod.Post;
            result.VoiceFallbackUrl = phone.VoiceFallbackUrl;
            result.VoiceMethod = HttpMethod.Post;
            result.VoiceUrl = phone.VoiceUrl;

            return result;
        }

        public static void RemovePhoneNumber(TrackingPhone phone)
        {
            var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
            twilioApi.DeleteIncomingPhoneNumber(phone.TwilioId);

            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                phone.IsRemoved = true;
                TrackingPhone.Save(phone, connection);
            }
        }

        public static List<PhoneCallWorkflow> GetRulesByTrackingPhoneId(int trackingPhoneId)
        {
            List<PhoneCallWorkflow> result;
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = PhoneCallWorkflow.GetByTrackingPhoneId(trackingPhoneId, connection);
            }
            return result;
        }

        public static List<LeadSourcePhone> GetLeadSourcePhones(int leadSourceId)
        {
            List<LeadSourcePhone> result;
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = LeadSourcePhone.GetByLeadSourceId(leadSourceId, connection);
            }
            return result;
        }

        public static List<TrackingPhone> GetTrackingPhonesByLeadSourceId(int leadSourceId)
        {
            List<TrackingPhone> result;
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = TrackingPhone.GetByLeadSourceId(leadSourceId, connection);
                foreach (var phone in result)
                {
                    phone.LeadSourceTrackingPhones = LeadSourceTrackingPhone.GetByTrackingPhoneId(phone.Id, connection).ToArray();
                }
            }
            return result;
        }

        public static List<TrackingPhone> GetTrackingPhonesByLeadSourceIds(int[] leadSourceIds)
        {
            List<TrackingPhone> result;
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = TrackingPhone.GetByLeadSourceIds(leadSourceIds, connection);
                foreach (var phone in result)
                {
                    phone.LeadSourceTrackingPhones = LeadSourceTrackingPhone.GetByTrackingPhoneId(phone.Id, connection).ToArray();
                }
            }
            return result;
        }

        public static List<PhoneCall> GetCallsByPhoneId(int phoneId)
        {
            List<PhoneCall> result;
            using (IDbConnection connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = PhoneCall.GetCallsByPhoneId(phoneId, connection);

                foreach (var phoneCall in result)
                {
                    if (phoneCall.LeadSourceId != null)
                        phoneCall.RelatedLeadSource = LeadSource.FindByPrimaryKey(phoneCall.LeadSourceId.Value, connection);
                }
            }
            return result;
        }

        public static List<CallWorkflow> RetrieveWorkflows()
        {
            List<CallWorkflow> result;

            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = CallWorkflow.Find(connection);

                foreach(var callWorkflow in result)
                {
                    callWorkflow.RelatedDetails = WorkflowDetail.GetByWorkflowId(callWorkflow.Id, connection).ToArray();
                }
            }

            return result;
        }

        public static void CommitWorkflows(CallWorkflow[] workflows)
        {
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                foreach (var callWorkflow in workflows)
                {
                    CallWorkflow.Update(callWorkflow, connection);

                    if (callWorkflow.RelatedDetails == null) 
                        continue;

                    foreach (var workflowDetail in callWorkflow.RelatedDetails)
                        WorkflowDetail.Update(workflowDetail, connection);
                }
            }
        }

        public static List<LeadSourcePhone> GetAllLeadSourcePhones()
        {
            List<LeadSourcePhone> result;
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = LeadSourcePhone.Find(connection);
            }
            return result;
        }

        public static List<CallWorkflow> GetAllWorkflows()
        {
            List<CallWorkflow> result;
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = CallWorkflow.Find(connection);
            }
            return result;
        }

        public static PhoneCallWorkflow SavePhoneCallWorkflow(PhoneCallWorkflow phoneCallWorkflow)
        {
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                if (phoneCallWorkflow.Id > 0)
                    PhoneCallWorkflow.Update(phoneCallWorkflow, connection);
                else
                    PhoneCallWorkflow.Insert(phoneCallWorkflow, connection);
            }

            return phoneCallWorkflow;
        }

        public static void RemovePhoneCallWorkflow(PhoneCallWorkflow phoneCallWorkflow)
        {
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                PhoneCallWorkflow.Delete(phoneCallWorkflow, connection);
            }
        }

        public static void UpdatePhoneCallWorkflows(PhoneCallWorkflow[] phoneCallWorkflows)
        {
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                foreach (var phoneCallWorkflow in phoneCallWorkflows)
                {
                    PhoneCallWorkflow.Update(phoneCallWorkflow, connection);
                }
            }
        }

        private static int GetSessionTimeout()
        {
            int result;
            return int.TryParse(ConfigurationManager.AppSettings[WebTimeoutKey], out result) ? result : 1;
        }

        public static TrackingPhone GetDynamicWebPhone(int leadSourceId, string userHostAddress, string pageUrl, string referralUri, ServmanCustomer servmanCustomer)
        {
            TrackingPhone result = null;
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {

                var phones = TrackingPhone.GetByLeadSourceId(leadSourceId, connection);
                foreach (var phone in phones)
                {
                    if (phone.TimeLastDisplay == null)
                    {
                        result = phone;
                        break;
                    }
                }
                if (result == null && phones.Count > 0)
                    result = phones[0];

                string sessionId = TrackingPhoneRotation.GetSessionIdByHostAddress(userHostAddress, GetSessionTimeout(), connection);
                
                if (sessionId == null)
                    sessionId = Guid.NewGuid().ToString();

                var rotation = new TrackingPhoneRotation
                                 {
                                     ReferralUri = pageUrl,
                                     ParentReferralUri = referralUri,
                                     UserHostAddress = userHostAddress,
                                     TimeDisplay = DateTime.Now,
                                     LeadSourceId = leadSourceId,
                                     SessionIdUid = sessionId
                                 };

                if (result != null)
                {
                    rotation.TrackingPhoneId = result.Id;
                    result.TimeLastDisplay = DateTime.Now;
                    TrackingPhone.Save(result, connection);
                }

                TrackingPhoneRotation.Save(rotation, connection);
            }

            return result;
        }

        public static TrackingPhoneRotation GetLastPhoneRotationByPhoneId(int trackingPhoneId, ServmanCustomer servmanCustomer)
        {
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                return TrackingPhoneRotation.GetLastPhoneRotationByPhoneId(trackingPhoneId, connection);
            }
        }

        public static TrackingPhoneRotation GetLastPhoneRotationByReferral(string referralUri, ServmanCustomer servmanCustomer)
        {
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                return TrackingPhoneRotation.GetLastPhoneRotationByReferral(referralUri, connection);
            }
        }

        public static void SavePhoneRotation(TrackingPhoneRotation trackingPhoneRotation, ServmanCustomer servmanCustomer)
        {
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                TrackingPhoneRotation.Save(trackingPhoneRotation, connection);
            }
        }

        public static List<TrackingPhoneRotation> GetTrackingPhoneRotations(LeadSource[] leadSources, DateTime? startDate, DateTime? endDate)
        {
            List<TrackingPhoneRotation> result;
            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = TrackingPhoneRotation.FindByLeadSourcesAndDatePeriod(leadSources, startDate, endDate, connection);
                foreach (var item in result)
                {
                    UpdateRelations(item, connection);
                }
            }
            return result;
        }

        public static List<TrackingPhoneRotation> GetTrackingPhoneRotationsByPhoneCall(PhoneCall phoneCall)
        {
            List<TrackingPhoneRotation> result;
            if (phoneCall == null)
                return new List<TrackingPhoneRotation>();

            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = TrackingPhoneRotation.FindByPhoneCallId(phoneCall.Id, connection);
                foreach (var item in result)
                {
                    UpdateRelations(item, connection);
                }
            }
            return result;
        }

        public static List<TrackingPhoneRotation> GetTrackingPhoneRotationsByPhoneSms(PhoneSms phoneSms)
        {
            List<TrackingPhoneRotation> result;
            if (phoneSms == null)
                return new List<TrackingPhoneRotation>();

            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = TrackingPhoneRotation.FindByPhoneSmsId(phoneSms.Id, connection);
                foreach (var item in result)
                {
                    UpdateRelations(item, connection);
                }
            }
            return result;
        }

        public static List<TrackingPhoneRotation> GetTrackingPhoneRotationsByLeadForm(LeadForm leadForm)
        {
            List<TrackingPhoneRotation> result;
            if (leadForm == null)
                return new List<TrackingPhoneRotation>();

            using (var connection = ServmanCustomerService.GetConnection(ContextHelper.GetCurrentCustomer()))
            {
                result = TrackingPhoneRotation.FindByLeadFormIdId(leadForm.Id, connection);
                foreach (var item in result)
                {
                    UpdateRelations(item, connection);
                }
            }
            return result;
        }

        private static void UpdateRelations(TrackingPhoneRotation item, IDbConnection connection)
        {
            if (item.PhoneCallId != null)
                item.RelatedPhoneCall = PhoneCall.FindByPrimaryKey(item.PhoneCallId.Value, connection);

            if (item.PhoneSmsId != null)
                item.RelatedPhoneSms = PhoneSms.FindByPrimaryKey(item.PhoneSmsId.Value, connection);

            if (item.LeadFormId != null)
                item.RelatedWebForm = LeadForm.FindByPrimaryKey(item.LeadFormId.Value, connection);
        }

        public static void SyncTwilioPhones(ServmanCustomer currentCustomer)
        {
            var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
            var twilioPhones = twilioApi.GetIncomingPhoneNumbers();
            if (twilioPhones == null || twilioPhones.IncomingPhoneNumbers == null)
                return;

            var phoneNumbers = new List<TrackingPhone>();

            foreach (var twilioPhone in twilioPhones.IncomingPhoneNumbers)
            {
                var voiceUrl = twilioPhone.VoiceUrl;

                var iqs = voiceUrl.IndexOf('?');
                if (iqs < 0) 
                    continue;

                var querystring = (iqs < voiceUrl.Length - 1) ? voiceUrl.Substring(iqs + 1) : String.Empty;

                var parameters = HttpUtility.ParseQueryString(querystring);

                if (parameters["RealmId"] != null && parameters["RealmId"] == currentCustomer.RealmId)
                    phoneNumbers.Add(CreateTrackingPhone(twilioPhone));
            }

            using (var connection = ServmanCustomerService.GetConnection(currentCustomer))
            {
                TrackingPhone.Insert(phoneNumbers, connection);
            }
        }

        private static TrackingPhone CreateTrackingPhone(IncomingPhoneNumber twilioPhone)
        {
            var result = new TrackingPhone
                             {
                                 TwilioId = twilioPhone.Sid,
                                 Number = twilioPhone.PhoneNumber,
                                 ScreenNumber = twilioPhone.FriendlyName,
                                 Description = twilioPhone.FriendlyName,
                                 DenyCallerId = !twilioPhone.VoiceCallerIdLookup,
                                 IsTollFree = false
                             };

            return result;
        }
    }
}
