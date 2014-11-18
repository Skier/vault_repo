using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Security;
using System.Web;
using Dalworth.Common.SDK;
using Dalworth.LeadCentral.Domain;
using Twilio;
using Twilio.Model;

namespace Dalworth.LeadCentral.Service
{
    public class TrackingPhoneService
    {
        private const string ReceiveCallHandler = "ReceiveCall.ashx";
        private const string ReceiveSmsHandler = "ReceiveSms.ashx";
        private const string CommitCallHandler = "CommitCall.ashx";

        private const string TwilioAccountSidKey = "TWILIO_ACCOUNT_SID";
        private const string TwilioAppTokenKey = "TWILIO_APP_TOKEN";

        private const string TwilioHandlerHostKey = "TWILIO_HANDLER_BASE";

        private const string TempStorageKey = "TempStorage";

        private const string AwsAccessKey = "AwsAccessKey";
        private const string AwsSecretKey = "AwsSecretKey";

        private const string AwsBucketNameKey = "AwsBucketName";

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

        public static List<TrackingPhone> GetAllActive(IDbConnection connection)
        {
            var user = ContextHelper.GetCurrentUser();

            if (user.IsBusinessPartner)
                return user.BusinessPartnerId != null ? TrackingPhone.GetAllActive(user.BusinessPartnerId.Value, connection) : new List<TrackingPhone>();

            return TrackingPhone.GetAllActive(connection);
        }

        public static List<TrackingPhone> GetAll(IDbConnection connection)
        {
            var user = ContextHelper.GetCurrentUser();

            if (user.IsBusinessPartner)
                return user.BusinessPartnerId != null ? TrackingPhone.GetAll(user.BusinessPartnerId.Value, connection) : new List<TrackingPhone>();
                    
            return TrackingPhone.Find(connection);
        }

        public static TrackingPhone GetById(int id, IDbConnection connection)
        {
            var user = ContextHelper.GetCurrentUser();

            if (user.IsBusinessPartner)
                return null;

            var trackingPhone =  TrackingPhone.FindByPrimaryKey(id, connection);

            var assignments =
                CompaignTrackingPhone.GetCurrentAssignments(trackingPhone.Id, connection);

            if (assignments.Count > 0)
                trackingPhone.AssignedCampaign = Campaign.FindByPrimaryKey(assignments[0].CampaignId, connection);

            return trackingPhone;
        }

        public static TrackingPhone Save(TrackingPhone phone, Customer customer, IDbConnection connection)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };

            if (ContextHelper.ActionDenied(allowedRoles))
                throw new SecurityException();

            var twilioPhone = UpdateCallerIdLookup(phone.TwilioNumberId, phone.CallerIdLookup);
            if (twilioPhone != null)
                phone.CallerIdLookup = twilioPhone.VoiceCallerIdLookup;
           
            TrackingPhone.Save(phone, connection);
            return phone;
           
        }

        public static TrackingPhone Save(TrackingPhone phone, IDbConnection connection  )
        {
            return Save(phone, ContextHelper.GetCurrentCustomer(), connection);
        }

        private static IncomingPhoneNumber UpdateCallerIdLookup(string twilioSid, bool lookup)
        {
            if (!string.IsNullOrEmpty(twilioSid))
            {
                var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
                var twilioPhone = twilioApi.GetIncomingPhoneNumber(twilioSid);
                var options = new PhoneNumberOptions { VoiceCallerIdLookup = lookup };

                return twilioApi.UpdateIncomingPhoneNumber(twilioPhone.Sid, options);
            }
            return null;
        }

        public static TrackingPhone SuspendPhoneNumber(int phoneId, IDbConnection connection)
        {
            var phone = GetById(phoneId, connection);
            return SuspendPhoneNumber(phone, connection);
        }

        public static TrackingPhone SuspendPhoneNumber(TrackingPhone phone, IDbConnection connection)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };

            if (ContextHelper.ActionDenied(allowedRoles))
                throw new SecurityException();

            if (!string.IsNullOrEmpty(phone.TwilioNumberId))
            {
                var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
                var twilioPhone = twilioApi.GetIncomingPhoneNumber(phone.TwilioNumberId);
                var options = GetOptions(twilioPhone);

                options.SmsUrl = "";
                options.SmsMethod = null;
                options.SmsFallbackUrl = null;
                options.SmsFallbackMethod = null;

                twilioApi.UpdateIncomingPhoneNumber(twilioPhone.Sid, options);
            }

            phone.IsSuspended = true;
            TrackingPhone.Save(phone, connection);
            return phone;
        }

        public static TrackingPhone ActivatePhoneNumber(int phoneId, IDbConnection connection)
        {
            var phone = GetById(phoneId, connection);
            return ActivatePhoneNumber(phone, connection);
        }

        public static TrackingPhone ActivatePhoneNumber(TrackingPhone phone, IDbConnection connection)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };

            if (ContextHelper.ActionDenied(allowedRoles))
                throw new SecurityException();

            var customer = ContextHelper.GetCurrentCustomer();

            if (!string.IsNullOrEmpty(phone.TwilioNumberId))
            {
                var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
                var twilioPhone = twilioApi.GetIncomingPhoneNumber(phone.TwilioNumberId);
                var options = GetOptions(twilioPhone);

                options.SmsUrl = string.Format("{0}?realmId={1}", ReceiveSmsHandlerUrl, customer.RealmId);

                twilioApi.UpdateIncomingPhoneNumber(twilioPhone.Sid, options);
            }
           
            phone.IsSuspended = false;
            TrackingPhone.Save(phone, connection);
            return phone;
           
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

        public static void RemovePhoneNumber(int phoneId, IDbConnection connection)
        {
            GetById(phoneId, connection);
            RemovePhoneNumber(phoneId, connection);
        }

        public static void RemovePhoneNumber(TrackingPhone phone, IDbConnection connection)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };

            if (ContextHelper.ActionDenied(allowedRoles))
                throw new SecurityException();

            var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
            twilioApi.DeleteIncomingPhoneNumber(phone.TwilioNumberId);

            var campaigns = GetCurrentAssignments(phone.Id, connection);
            foreach (var campaign in campaigns)
            {
                RemovePhoneFromCampaign(phone.Id, campaign.Id, connection);
            }

            phone.IsRemoved = true;
            TrackingPhone.Save(phone, connection);
        }

        public static List<TrackingPhone> GetAvailablePhoneNumbers(string areaCode, bool isTollFree)
        {
            var result = new List<TrackingPhone>();

            var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
            
            var twilioResult = isTollFree ? 
                twilioApi.ListAvailableTollFreePhoneNumbers("US", areaCode) 
                : twilioApi.ListAvailableLocalPhoneNumbers("US", new AvailablePhoneNumberListRequest { AreaCode = areaCode });

            if (twilioResult != null)
            {
                var phoneList = twilioResult.AvailablePhoneNumbers;
                var redirectNo = ContextHelper.GetCurrentCustomer().Phone;
                if (phoneList != null && phoneList.Count > 0)
                {
                    foreach (var availablePhone in phoneList)
                    {
                        var phone = new TrackingPhone
                        {
                            PhoneNumber = availablePhone.PhoneNumber,
                            FriendlyNumber = availablePhone.FriendlyName,
                            RedirectPhoneNumber = redirectNo,
                            Description = availablePhone.FriendlyName,
                            IsTollFree = isTollFree
                        };
                        result.Add(phone);
                    }
                }
            }

            return result;
        }

        public static List<TrackingPhone> GetUnassignedPhones(IDbConnection connection)
        {
            return TrackingPhone.GetUnassignedPhones(connection);
        }

        public static TrackingPhone PurchasePhoneNumber(TrackingPhone phone, IDbConnection connection)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator,
                                       UserRoleEnum.Staff
                                   };

            if (ContextHelper.ActionDenied(allowedRoles))
                throw new SecurityException();

            var customer = ContextHelper.GetCurrentCustomer();

            var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
            var options = new PhoneNumberOptions
            {
                PhoneNumber = phone.PhoneNumber,
                FriendlyName = phone.FriendlyNumber,
                VoiceCallerIdLookup = phone.CallerIdLookup,
                VoiceUrl =
                    string.Format("{0}?realmId={1}", ReceiveCallHandlerUrl, customer.RealmId),
                VoiceMethod = HttpMethod.Post,
                SmsUrl =
                    string.Format("{0}?realmId={1}", ReceiveSmsHandlerUrl, customer.RealmId),
                SmsMethod = HttpMethod.Post,
                StatusCallback =
                    string.Format("{0}?realmId={1}", CommitCallHandlerUrl, customer.RealmId),
                StatusCallbackMethod = HttpMethod.Post
            };

            var phoneNumber = twilioApi.AddLocalPhoneNumber(options);
            phone.TwilioNumberId = phoneNumber.Sid;
            phone.RedirectPhoneNumber = customer.Phone;
           
            TrackingPhone.Insert(phone, connection);
            BillingService.CreateTransaction(connection, TransactionTypeEnum.PhoneNumberCharge);
            
            if (!customer.IsTrackingPhonesInited)
            {
                customer.IsTrackingPhonesInited = true;
                Customer.Save(customer);
            }
            
            return phone;
        }

        private static bool IsPhoneUnassigned(int trackingPhoneId, IDbConnection connection)
        {
            var unassignedPhones = GetUnassignedPhones(connection);
            
            foreach(var phone in unassignedPhones)
            {
                if (phone.Id == trackingPhoneId)
                    return true;
            }
            
            return false;
        }

        public static void AssignPhoneToCampaign(int trackingPhoneId, int campaignId, IDbConnection connection)
        {
            if (IsPhoneUnassigned(trackingPhoneId, connection))
            {
                var assignment = new CompaignTrackingPhone
                                     {
                                         CampaignId = campaignId,
                                         TrackingPhoneId = trackingPhoneId,
                                         DateAssigned = DateTime.Now,
                                         DateReleased = null
                                     };
               
                CompaignTrackingPhone.Save(assignment, connection);
            }
        }

        public static void RemovePhoneFromCampaign(int trackingPhoneId, int campaignId, IDbConnection connection)
        {
            var currentAssignment = CompaignTrackingPhone.GetCurrentAssignment(trackingPhoneId, campaignId, connection);
            if (currentAssignment == null)
                throw new DalworthException("Tracking Phone is not assigned to current Campaign");

            currentAssignment.DateReleased = DateTime.Now;
            CompaignTrackingPhone.Save(currentAssignment, connection);
        }

        public static List<Campaign> GetCurrentAssignments(int trackingPhoneId, IDbConnection connection)
        {
            var result = new List<Campaign>();

            var assignments = CompaignTrackingPhone.GetCurrentAssignments(trackingPhoneId, connection);
            if (assignments != null && assignments.Count > 0)
            {
                foreach (var assignment in assignments)
                {
                    result.Add(Campaign.FindByPrimaryKey(assignment.CampaignId, connection));
                }
            }

            return result;
        }

        public static TrackingPhoneRotation GetLastPhoneRotationByPhoneId(Customer servmanCustomer, int trackingPhoneId, IDbConnection connection)
        {
            return TrackingPhoneRotation.GetLastPhoneRotationByPhoneId(trackingPhoneId, connection);
        }

        public static void TransmitTwilioPhones(IDbConnection connection)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer();

            var twilioApi = new TwilioApi(GetTwilioAccountSid(), GetTwilioAppToken());
            var twilioPhones = twilioApi.GetIncomingPhoneNumbers();
            if (twilioPhones == null || twilioPhones.IncomingPhoneNumbers == null)
                return;

            foreach (var twilioPhone in twilioPhones.IncomingPhoneNumbers)
            {
                var voiceUrl = twilioPhone.VoiceUrl;
                var iqs = voiceUrl.IndexOf('?');
                if (iqs < 0)
                    continue;

                var querystring = (iqs < voiceUrl.Length - 1) ? voiceUrl.Substring(iqs + 1) : String.Empty;
                var parameters = HttpUtility.ParseQueryString(querystring);
                    
                if (parameters["RealmId"] == null || parameters["RealmId"] != servmanCustomer.RealmId) 
                    continue;

                //update twilio phone
                var options = GetOptions(twilioPhone);
                options.SmsUrl = string.Format("{0}?realmId={1}", ReceiveSmsHandlerUrl, servmanCustomer.RealmId);
                options.VoiceUrl = string.Format("{0}?realmId={1}", ReceiveCallHandlerUrl, servmanCustomer.RealmId);
                options.SmsUrl = string.Format("{0}?realmId={1}", ReceiveSmsHandlerUrl, servmanCustomer.RealmId);
                options.StatusCallback = string.Format("{0}?realmId={1}", CommitCallHandlerUrl, servmanCustomer.RealmId);
                options.VoiceMethod = HttpMethod.Post;
                options.SmsMethod = HttpMethod.Post;

                twilioApi.UpdateIncomingPhoneNumber(twilioPhone.Sid, options);

                //update local phone
                var phone = CreateTrackingPhone(twilioPhone);
                phone.RedirectPhoneNumber = servmanCustomer.Phone;
                var area = phone.PhoneNumber.Substring(2, 3);
                phone.IsTollFree = (area == "800" ||
                                    area == "855" ||
                                    area == "866" ||
                                    area == "877" ||
                                    area == "888");

                TrackingPhone.Save(phone, connection);
            }
        }

        private static TrackingPhone CreateTrackingPhone(IncomingPhoneNumber twilioPhone)
        {
            var result = new TrackingPhone
            {
                TwilioNumberId = twilioPhone.Sid,
                PhoneNumber = twilioPhone.PhoneNumber,
                FriendlyNumber = twilioPhone.FriendlyName,
                Description = twilioPhone.FriendlyName,
                CallerIdLookup = !twilioPhone.VoiceCallerIdLookup,
                DateCreated = twilioPhone.DateCreated
            };

            return result;
        }
    }
}
