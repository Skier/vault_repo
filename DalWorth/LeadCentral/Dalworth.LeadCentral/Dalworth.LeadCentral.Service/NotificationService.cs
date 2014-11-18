using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Notification;

namespace Dalworth.LeadCentral.Service
{
    public class NotificationService
    {
        private const string EmailTemplateDirKey = "EmailTemplateDir";
        private const string EmailTemplateCreateLeadKey = "EmailTemplateCreateLead";
        private const string EmailTemplateLowBalanceKey = "EmailTemplateLowBalance";
        private const string EmailNotificationFromKey = "EmailNotificationFrom";

        public static void SendLowBalanceNotification(ServmanCustomer servmanCustomer)
        {
            var message = GetLowBalanceMessage(servmanCustomer);
            SendNotification(message);
        }

        public static void SendCreateLeadNotification(ServmanCustomer servmanCustomer, Lead lead)
        {
            var message = GetCreateLeadMessage(servmanCustomer, lead);
            SendNotification(message);
        }

        internal static void SendNotification(NotifyMessage message)
        {
            try
            {
                Notifier.SendNotify(message);
            }
            catch (Exception)
            {
            }
        }

        internal static NotifyMessage GetCreateLeadMessage(ServmanCustomer servmanCustomer, Lead lead)
        {
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                return GetCreateLeadMessage(servmanCustomer, lead, connection);
            }
        }

        internal static NotifyMessage GetCreateLeadMessage(ServmanCustomer servmanCustomer, Lead lead, IDbConnection connection)
        {
            var dbId = servmanCustomer.AppDbId;

            var emailTemplateDir = ConfigurationManager.AppSettings[EmailTemplateDirKey];
            var emailTemplate = ConfigurationManager.AppSettings[EmailTemplateCreateLeadKey];

            string transport;

            if (lead.PhoneCallId != null)
                transport = "Phone Call";
            else if (lead.PhoneSmsId != null)
                transport = "Phone SMS";
            else if (lead.WebFormId != null)
                transport = "Web Form";
            else
                transport = "Lead Central";

            var body = System.IO.File.ReadAllText(emailTemplateDir + emailTemplate);
            body = body.Replace("{LEAD_TRANSPORT}", transport);
            body = body.Replace("{LEAD_CUSTOMER}", (lead.FirstName + " " + lead.LastName));
            body = body.Replace("{LEAD_DATE_CREATED}", lead.DateCreated.ToLongDateString());
            body = body.Replace("{CURRENT_DB_ID}", dbId);

            var message = new NotifyMessage
            {
                From = ConfigurationManager.AppSettings[EmailNotificationFromKey],
                Subject =
                    string.Format("New Lead from {0} was created",
                                  (lead.FirstName + " " + lead.LastName)),
                Body = body,
                To = GetCreateLeadAddresses(lead, connection)
            };


            return message;
        }

        internal static NotifyMessage GetLowBalanceMessage(ServmanCustomer servmanCustomer)
        {
            var emailTemplateDir = ConfigurationManager.AppSettings[EmailTemplateDirKey];
            var emailTemplate = ConfigurationManager.AppSettings[EmailTemplateLowBalanceKey];

            var balance = string.Format("{0:0.00}", BillingService.GetCurrentBalance(servmanCustomer));

            var body = System.IO.File.ReadAllText(emailTemplateDir + emailTemplate);
            body = body.Replace("{CURRENT_BALANCE}", balance);
            body = body.Replace("{DATE_CREATED}", DateTime.Now.ToString());

            var message = new NotifyMessage
                              {
                                  From = ConfigurationManager.AppSettings[EmailNotificationFromKey],
                                  Subject = "Low balance warning",
                                  Body = body,
                                  To = GetAdminEmails(servmanCustomer)
                              };


            return message;
        }

        private static List<string> GetCreateLeadAddresses(Lead lead, IDbConnection connection)
        {
            var result = new List<string>();

            if (lead.LeadSourceId == null)
                return result;

            var leadSource = LeadSource.FindByPrimaryKey(lead.LeadSourceId.Value, connection);
            var email = leadSource.GetNotificationEmail(connection);
            if (!string.IsNullOrEmpty(email) && !result.Contains(email))
                result.Add(email);

            //add parents emails
            if (leadSource.OwnedByUserId != null)
            {
                var user = User.FindByPrimaryKey(leadSource.OwnedByUserId.Value, connection);
                if (!string.IsNullOrEmpty(user.Email) && !result.Contains(user.Email))
                    result.Add(user.Email);
            }

            result.AddRange(GetAdminEmails(connection));

            return result;
        }

        private static List<string> GetAdminEmails(ServmanCustomer servmanCustomer)
        {
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                return GetAdminEmails(connection);
            }
        }

        private static List<string> GetAdminEmails(IDbConnection connection)
        {
            var result = new List<string>();

            var users = User.Find(connection);
            foreach (var user in users)
            {
                if (user.IsActive && !string.IsNullOrEmpty(user.Email) && user.IsAdmin())
                    result.Add(user.Email);
            }

            return result;
        }
    }
}
