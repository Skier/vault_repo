using System;
using System.Data;
using System.Web;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Phone
{
    /// <summary>
    /// Summary description for ReceiveCall
    /// </summary>
    public class ReceiveCall : IHttpHandler
    {

        private const int WebAccessDurationMin = 3;

        private const string RedirectCallBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response>
	<Dial action=""{0}"" record=""true"">{1}</Dial>
</Response>
";

        private const string RejectCallBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response><Reject/></Response>
";

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var realmId = request["realmId"];
            var phoneNumber = request["To"];

            var customer = CustomerService.FindByRealmId(realmId);

            PhoneCall phoneCall;
            TrackingPhone phone;

            using (var connection = CustomerService.GetConnection(customer))
            {
                phone = TrackingPhone.GetByPhoneNumber(phoneNumber, connection);
                if (phone == null)
                {
                    var response = context.Response;
                    response.ContentType = "text/xml";
                    response.Write(RejectCallBody);
                    response.End();
                    return;
                }

                phoneCall = CreatePhoneCall(phone, context, connection, customer);
                var phoneBlackList = PhoneBlackList.GetByCallerPhone(phoneCall.FromPhone, connection);

                if (phoneBlackList.Count > 0)
                {
                    phoneCall.Notes = string.Format("Rejected by Black List Phone [{0}]", phoneBlackList[0].PhoneNumber);
                    phoneCall.PhoneBlackListId = phoneBlackList[0].Id;

                    RejectCall(phoneCall, context, connection);
                    return;
                }
                
                if (phone.IsSuspended 
                    || !IsBillingStatusOk(customer)
                    || BillingService.GetCurrentBalance(connection) <= 0)
                {
                    RejectCall(phoneCall, context, connection);
                    return;
                }
            }

            RedirectCall(context, phone, phoneCall, customer);
        }

        private static bool IsBillingStatusOk(Customer servmanCustomer)
        {
            return BillingService.IsBillingStatusOk(servmanCustomer);
        }

        private static void RejectCall(PhoneCall phoneCall, HttpContext context, IDbConnection connection)
        {
            phoneCall.Status = "rejected";
            PhoneCall.Save(phoneCall, connection);

            var response = context.Response;
            response.ContentType = "text/xml";
            response.Write(RejectCallBody);
            response.End();
        }

        private static void RedirectCall(HttpContext context, TrackingPhone phone, PhoneCall phoneCall, Customer servmanCustomer)
        {
            using (var connection =  CustomerService.GetConnection(servmanCustomer))
            {
                if (string.IsNullOrEmpty(phone.RedirectPhoneNumber))
                    RejectCall(phoneCall, context, connection);

                CreateLead(phoneCall, servmanCustomer, connection);
                PhoneCall.Update(phoneCall, connection);
            }

            phoneCall.Status = "emergency";
            var realmId = servmanCustomer.RealmId;
            var response = context.Response;
            var commitDialUrl = string.Format("CommitDial.ashx?realmId={0}", realmId);
            response.ContentType = "text/xml";
            response.Write(string.Format(RedirectCallBody, commitDialUrl, phone.RedirectPhoneNumber));
            response.End();
        }

        private static PhoneCall CreatePhoneCall(TrackingPhone phone, HttpContext context, IDbConnection connection, Customer servmanCustomer)
        {
            var request = context.Request;

            var call = new PhoneCall
            {
                TwilioCallId = request["CallSid"],
                TrackingPhoneNumber = request["To"],
                FromPhone = request["From"],
                Status = request["CallStatus"],
                FromCity = request["FromCity"],
                FromState = request["FromState"],
                FromZip = request["FromZip"],
                FromCountry = request["FromCountry"],
                CallerName = request["CallerName"] ?? "unknown",
                TrackingPhoneId = phone.Id,
                DateCreated = DateTime.Now
            };

            var campaign = FindByCampaignByPhoneCall(call, connection);
            if (campaign != null)
                call.CampaignId = campaign.Id;

            PhoneCall.Save(call, connection);

            SyncWithPhoneRotation(call, servmanCustomer);

            if (phone.CallerIdLookup)
                BillingService.CreateTransaction(connection, TransactionTypeEnum.CallerIdLookup, 1, null, call.CampaignId);

            return call;
        }

        private static void CreateLead(PhoneCall phoneCall, Customer servmanCustomer, IDbConnection connection)
        {
            var source = CreateSource(phoneCall, connection);

            var lead = new Lead
            {
                LeadStatusId = (int)LeadStatusEnum.New,
                Phone = phoneCall.FromPhone,
                DateCreated = DateTime.Now,
                CampaignId = phoneCall.CampaignId,
                FirstName = phoneCall.CallerName,
                LastName = string.Empty,
                CustomerNotes = string.Empty,
                SourceId = source.Id
            };

            lead.BusinessPartnerId = GetBusinessPartnerIdByPhoneCall(phoneCall, connection);
            Lead.Insert(lead, connection);
        }

        private static Source CreateSource(PhoneCall phoneCall, IDbConnection connection)
        {
            var source = new Source { PhoneCallId = phoneCall.Id };
            return Source.Save(source, connection);
        }

        private static Campaign FindByCampaignByPhoneCall(PhoneCall phoneCall, IDbConnection connection)
        {
            var campaign = CampaignService.GetCampaignByTrackingPhoneAndDate(phoneCall.TrackingPhoneId, DateTime.Now, connection);
            
            if (campaign != null 
                && campaign.DateStart < DateTime.Now 
                && (campaign.DateEnd == null || campaign.DateEnd > DateTime.Now))
                return campaign;

            return null;
        }

        private static int? GetBusinessPartnerIdByPhoneCall(PhoneCall phoneCall, IDbConnection connection)
        {
            var businessPartner = BusinessPartnerService.FindByCallerNumber(phoneCall.FromPhone, connection);
            if (businessPartner != null)
                return businessPartner.Id;

            var campaign = FindByCampaignByPhoneCall(phoneCall, connection);

            return campaign != null ? campaign.BusinessPartnerId : null;
        }

        private static void SyncWithPhoneRotation(PhoneCall phoneCall, Customer servmanCustomer)
        {
            var duration = new TimeSpan(0, WebAccessDurationMin, 0);
            using (var connection = CustomerService.GetConnection(servmanCustomer))
            {
                var trackingPhoneRotation = TrackingPhoneService.GetLastPhoneRotationByPhoneId(servmanCustomer,
                                                                      phoneCall.TrackingPhoneId, connection);
                if (trackingPhoneRotation != null
                    && trackingPhoneRotation.TimeRotation.Add(duration) >= DateTime.Now)
                {
                    phoneCall.TrackingPhoneRotationId = trackingPhoneRotation.Id;

                    PhoneCall.Save(phoneCall, connection);
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