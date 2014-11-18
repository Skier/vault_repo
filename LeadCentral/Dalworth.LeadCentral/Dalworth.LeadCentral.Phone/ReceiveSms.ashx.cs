using System;
using System.Data;
using System.Web;
using Dalworth.Common.SDK;
using Dalworth.LeadCentral.Domain;
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

            var servmanCustomer = CustomerService.FindByRealmId(realmId);

            using (var connection = CustomerService.GetConnection(servmanCustomer))
            {
                var phone = TrackingPhone.GetByPhoneNumber(phoneTo, connection);
                if (phone == null)
                    throw new DalworthException("Phone Number doesn't present in database");

                var sms = new PhoneSms
                {
                    TwilioSmsId = request["SmsSid"],
                    FromPhone = request["From"],
                    TrackingPhoneNumber = request["To"],
                    Message = request["Body"],
                    TrackingPhoneId = phone.Id,
                    DateCreated = DateTime.Now
                };

                var campaign = GetCampaignByPhoneSms(sms, connection);
                if (campaign != null)
                    sms.CampaignId = campaign.Id;

                PhoneSms.Save(sms, connection);
                
                SyncWithPhoneRotation(sms, servmanCustomer, connection);

                CreateLead(sms, servmanCustomer, connection);

                BillingService.CreateTransaction(connection, TransactionTypeEnum.IncomeSms, 1, null, sms.CampaignId);

/* remove it and replace new notification policy
                Configuration.EmailTemplateDir = ConfigurationManager.AppSettings["EmailTemplateDir"];
                Configuration.EmailTemplateCreateLead = ConfigurationManager.AppSettings["EmailTemplateCreateLead"];
                Configuration.EmailNotificationFrom = ConfigurationManager.AppSettings["EmailNotificationFrom"];
                Configuration.AwsAccessKey = ConfigurationManager.AppSettings["AwsAccessKey"];
                Configuration.AwsSecretKey = ConfigurationManager.AppSettings["AwsSecretKey"];
                
                NotificationService.SendCreateLeadNotification(servmanCustomer, Lead.GetBySmsId(sms.Id, connection));
*/
            }

            context.Response.ContentType = "text/xml";
            context.Response.Write(string.Format(SmsResponseBody, "Thank you for your interest"));
            context.Response.End();
        }

        private static void CreateLead(PhoneSms phoneSms, Customer servmanCustomer, IDbConnection connection)
        {

            var source = CreateSource(phoneSms, connection);

            var lead = new Lead
            {
                LeadStatusId = (int)LeadStatusEnum.New,
                Phone = phoneSms.FromPhone,
                DateCreated = DateTime.Now,
                CampaignId = phoneSms.CampaignId,
                FirstName = string.Empty,
                LastName = string.Empty,
                CustomerNotes = phoneSms.Message,
                SourceId = source.Id
            };

            lead.BusinessPartnerId = GetBusinessPartnerIdByPhoneSms(phoneSms, connection);

            lead.DateCreated = DateTime.Now;
            Lead.Insert(lead, connection);
        }

        private static Source CreateSource(PhoneSms phoneSms, IDbConnection connection)
        {
            var source = new Source { PhoneSmsId = phoneSms.Id };
            return Source.Save(source, connection);
        }

        private static Campaign GetCampaignByPhoneSms(PhoneSms phoneSms, IDbConnection connection)
        {
            var campaign = CampaignService.GetCampaignByTrackingPhoneAndDate(phoneSms.TrackingPhoneId, DateTime.Now, connection);

            if (campaign != null
                && campaign.DateStart < DateTime.Now
                && (campaign.DateEnd == null || campaign.DateEnd > DateTime.Now))
                return campaign;

            return null;
        }

        private static int? GetBusinessPartnerIdByPhoneSms(PhoneSms phoneSms, IDbConnection connection)
        {
            var businessPartner = BusinessPartnerService.FindByCallerNumber(phoneSms.FromPhone, connection);
            if (businessPartner != null)
                return businessPartner.Id;

            var campaign = GetCampaignByPhoneSms(phoneSms, connection);

            return campaign != null ? campaign.BusinessPartnerId : null;
        }

        private static void SyncWithPhoneRotation(PhoneSms phoneSms, Customer servmanCustomer, IDbConnection connection)
        {
            var duration = new TimeSpan(0, WebAccessDurationMin, 0);
            var trackingPhoneRotation = TrackingPhoneService.GetLastPhoneRotationByPhoneId(servmanCustomer, phoneSms.TrackingPhoneId, connection);
            if (trackingPhoneRotation != null
                && trackingPhoneRotation.TimeRotation.Add(duration) >= DateTime.Now)
            {
                phoneSms.TrackingPhoneRotationId = trackingPhoneRotation.Id;
                PhoneSms.Save(phoneSms, connection);
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