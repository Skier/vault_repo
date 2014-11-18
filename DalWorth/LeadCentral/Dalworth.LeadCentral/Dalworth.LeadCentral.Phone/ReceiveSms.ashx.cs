using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.SDK;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Phone
{
    /// <summary>
    /// Summary description for ReceiveSms
    /// </summary>
    public class ReceiveSms : IHttpHandler
    {

        private const int WebAccessDurationMin = 3;

        private const string SmsResponseBody = @"<?xml version=""1.0"" encoding=""UTF-8"" ?>
<Response>
	<Sms>{0}</Sms>
</Response>
";

        public void ProcessRequest(HttpContext context)
        {
            var request = context.Request;
            var realmId = request["realmId"];
            var phoneTo = request["To"];

            var servmanCustomer = ServmanCustomerService.FindByRealmId(realmId);

            TrackingPhone phone;

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                phone = TrackingPhone.GetByPhoneNumber(phoneTo, connection);
                if (phone == null)
                    throw new DalworthException("Phone Number doesn't present in database");

                var sms = new PhoneSms
                {
                    SmsSid = request["SmsSid"],
                    AccountSid = request["AccountSid"],
                    PhoneFrom = request["From"],
                    PhoneTo = request["To"],
                    Message = request["Body"],
                    TrackingPhoneId = phone.Id,
                    DateCreated = DateTime.Now
                };

                sms.LeadSourceId = GetLeadSourceId(sms, servmanCustomer);

                PhoneSms.Save(sms, connection);
                
                SyncWithPhoneRotation(sms, servmanCustomer);
                
                CreateLead(sms, connection);

                BillingService.CreatePhoneSmsTransaction(sms, connection);
            }

            context.Response.ContentType = "text/xml";
            context.Response.Write(string.Format(SmsResponseBody, phone.SmsResponse));
            context.Response.End();
        }

        private static int? GetLeadSourceId(PhoneSms phoneSms, ServmanCustomer servmanCustomer)
        {
            List<LeadSource> leadSources =
                LeadSourceService.GetByPhoneNumber(servmanCustomer, phoneSms.PhoneFrom);
            if (leadSources.Count == 1)
                return leadSources[0].Id;

            leadSources = LeadSourceService.GetByTrackingPhoneNumber(servmanCustomer, phoneSms.TrackingPhoneId);
            if (leadSources.Count == 1)
                return leadSources[0].Id;

            return null;
        }

        private static void CreateLead(PhoneSms phoneSms, IDbConnection connection)
        {
            var lead = new Lead
            {
                LeadStatusId = (int)LeadStatusEnum.New,
                Phone = phoneSms.PhoneFrom,
                DateCreated = DateTime.Now,
                LeadSourceId = phoneSms.LeadSourceId,
                CustomerNotes = phoneSms.Message,
                FirstName = "",
                LastName = "",
                PhoneSmsId = phoneSms.Id
            };

            lead.UpdateNullable();

            Lead.Save(lead, connection);
        }

        private static void SyncWithPhoneRotation(PhoneSms phoneSms, ServmanCustomer servmanCustomer)
        {
            var duration = new TimeSpan(0, WebAccessDurationMin, 0);
            var trackingPhoneRotation = PhoneService.GetLastPhoneRotationByPhoneId(servmanCustomer, phoneSms.TrackingPhoneId);
            
            if (trackingPhoneRotation == null || trackingPhoneRotation.TimeDisplay.Add(duration) < DateTime.Now) 
                return;
            
            trackingPhoneRotation.PhoneCallId = phoneSms.Id;
            trackingPhoneRotation.LeadSourceId = phoneSms.LeadSourceId;
            PhoneService.SavePhoneRotation(servmanCustomer, trackingPhoneRotation);
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