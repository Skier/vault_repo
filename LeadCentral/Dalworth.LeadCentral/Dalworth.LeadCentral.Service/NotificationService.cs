using System;   
using System.Collections.Generic;
using System.Net.Mail;
using System.Data;

using Dalworth.Common.SDK;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class NotificationService
    {
        #region Add

        public static void Add(NotificationTypeEnum type, string from, string to, string message, IDbConnection connection)
        {
            var notification = new Notification
                                   {
                                       NotificationTypeId = (int) type,
                                       FromEmail = from,
                                       ToEmail = to,
                                       Message = message,
                                       IsProcessed = false,
                                       DateCreated = DateTime.Now
                                   };

            Notification.Insert(notification, connection);
        }

        #endregion

        #region Find

        public static List<Notification> Find(NotificationFilter filter, IDbConnection connection)
        {
            return Notification.Find(filter, connection);
        }

        #endregion

        #region GetTypes

        public static List<NotificationType> GetTypes(IDbConnection connection)
        {
            return NotificationType.Find(connection);
        }

        #endregion

        #region UpdateSettings

        public static void UpdateSettings(List<NotificationType> notificationSettings, IDbConnection connection)
        {
            foreach(var type in notificationSettings)
            {
                NotificationType.Update(type, connection);
            }
        }

        #endregion

        #region SendCreateLeadNotification

        public static void SendCreateLeadNotification(Customer customer, Lead lead, IDbConnection connection)
        {
            var body = System.IO.File.ReadAllText(Configuration.Sync.EmailTemplateDir + "/" + Configuration.Sync.EmailTemplateCreateLead);

            body = body.Replace("{LEAD_TRANSPORT}", lead.RelatedSource.Type.ToString());
            body = body.Replace("{LEAD_CAMPAIGN}", lead.RelatedCampaign.CampaignName);
            body = body.Replace("{LEAD_CUSTOMER}", (lead.FirstName + " " + lead.LastName));
            body = body.Replace("{LEAD_DATE_CREATED}", lead.DateCreated.ToLongDateString());
            body = body.Replace("{CAMPAIGN_NAME}", lead.RelatedCampaign.CampaignName);
            body = body.Replace("{LEAD_PHONE_NUMBER}", lead.Phone);
            body = body.Replace("{LEAD_DETAILS}", lead.CustomerNotes);
            body = body.Replace("{PHONE_CALL_RECORDING}", FormatRecordingUrl(lead));

            var message = new MailMessage
                              {
                                  Subject = string.Format("New Lead from {0} was created",
                                                          (lead.FirstName + " " + lead.LastName)),
                                  Body = body,
                                  IsBodyHtml = true
                              };
            AddToAddresses(message, lead, connection);

            SendEmail(message);
        }

        #endregion

        #region SendEmail 

        private static void SendEmail(MailMessage message)
        {
            var smtpClient = new SmtpClient
            {
                Host = "localhost",
                Port = 25,
                Credentials = new System.Net.NetworkCredential("administrator", "Ariel1o1"),
                EnableSsl = false
            };

            message.From = new MailAddress(Configuration.Sync.EmailNotificationFrom);
            smtpClient.Send(message);
        }

        #endregion 

        #region FormatRecordingUrl

        private static string FormatRecordingUrl(Lead lead)
        {
            var result = string.Empty;

            if (lead.RelatedSource.RelatedPhoneCall == null || string.IsNullOrEmpty(lead.RelatedSource.RelatedPhoneCall.RecordingUrl))
                return result;

            result += "<a href=\"";
            result += lead.RelatedSource.RelatedPhoneCall.FullRecordingUrl;
            result += "\">Click here to listen recorded phone call.";
            result += "</a>";
            
            return result;
        }

        #endregion

        #region AddToAddress

        private static void AddToAddresses(MailMessage mailMessage, Lead lead, IDbConnection connection)
        {
            BusinessPartner partner = null;
            if (lead.RelatedCampaign.BusinessPartnerId != null)
                partner = BusinessPartner.FindByPrimaryKey(lead.RelatedCampaign.BusinessPartnerId.Value, connection);

            var users = User.GetAllActive(connection);

            foreach (var user in users)
            {
                if (string.IsNullOrEmpty(user.Email))
                    continue;

                if (user.IsAdmin())
                {
                    mailMessage.To.Add(user.Email);
                    continue;
                }

                if (partner != null && user.BusinessPartnerId.HasValue && user.BusinessPartnerId == partner.Id)
                {
                    mailMessage.To.Add(user.Email);
                    continue;
                }

                if (lead.RelatedCampaign != null && 
                    lead.RelatedCampaign.RelatedUser != null && 
                    lead.RelatedCampaign.RelatedUser.Id == user.Id)
                        mailMessage.To.Add(user.Email);
            }
        }

        #endregion

        #region Not Tested Yet

        #region SendLowBalanceNotification

        public static void SendLowBalanceNotification(Customer servmanCustomer, IDbConnection connection)
        {
            var emailTemplateDir = Configuration.Sync.EmailTemplateDir;
            var emailTemplate = Configuration.Sync.EmailTemplateLowBalance;

            var balance = string.Format("{0:0.00}", BillingService.GetCurrentBalance(connection));

            var body = System.IO.File.ReadAllText(emailTemplateDir + emailTemplate);
            body = body.Replace("{CURRENT_BALANCE}", balance);
            body = body.Replace("{DATE_CREATED}", DateTime.Now.ToString());


            /*var message = new NotifyMessage
            {
                From = Configuration.EmailNotificationFrom,
                Subject = "Low balance warning",
                Body = body,
                To = GetAdminEmails(servmanCustomer)
            };*/

            /*SendEmail(message);*/
        }

        #endregion
        #endregion 
    }
}
