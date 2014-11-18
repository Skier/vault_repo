using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;

namespace TractInc.Expense
{

    class BaseService
    {

        private const string SMTP_SERVER_KEY = "smtp";
        private const string SMTP_PORT_KEY = "port";
        private const string SMTP_USER_KEY = "username";
        private const string SMTP_PASSWORD_KEY = "password";
        private const string EMAIL_FROM_KEY = "from";
        private const string EMAIL_SUBJECT_KEY = "subject";
        private const int DEFAULT_SMTP_PORT = 25;
        private const string DEFAULT_EMAIL_FROM = "admin@truetract.com";

        public String GetStorageUrl()
        {
            return Uploader.StorageUrl;
        }

        public String GetUploaderUrl()
        {
            return Uploader.UploaderUrl;
        }

        public String GetGUID()
        {
            return Guid.NewGuid().ToString();
        }

        public String GetInvoicePDFUrl(int invoiceId)
        {
            InvoiceProcessor processor = new InvoiceProcessor();
            return processor.ProcessInvoice(invoiceId);
        }

        public String GetCoverPDFUrl(int invoiceId)
        {
            InvoiceProcessor processor = new InvoiceProcessor();
            return processor.ProcessCover(invoiceId);
        }

        public bool SendEmail(String email, String subject, String body)
        {
            SmtpClient smtpClient = new SmtpClient();

            string host = ConfigurationManager.AppSettings[SMTP_SERVER_KEY];
            if (null == host || host.Length == 0)
            {
                throw new ConfigurationErrorsException("SMTP hostname not found");
            }
            smtpClient.Host = host;

            string port_str = ConfigurationManager.AppSettings[SMTP_PORT_KEY];
            if (null == port_str || port_str.Length == 0)
            {
                smtpClient.Port = DEFAULT_SMTP_PORT;
            }
            else
            {
                smtpClient.Port = Int32.Parse(ConfigurationManager.AppSettings[SMTP_PORT_KEY]);
            }

            string username = ConfigurationManager.AppSettings[SMTP_USER_KEY];
            string password = ConfigurationManager.AppSettings[SMTP_PASSWORD_KEY];
            if (username != null && password != null)
            {
                smtpClient.Credentials = new NetworkCredential(username, password);
            }

            MailAddress to = new MailAddress(email);
            MailAddress from;

            if (ConfigurationManager.AppSettings[EMAIL_FROM_KEY] == null)
            {
                from = new MailAddress(DEFAULT_EMAIL_FROM);
            }
            else
            {
                from = new MailAddress(ConfigurationManager.AppSettings[EMAIL_FROM_KEY]);
            }

            MailMessage message = new MailMessage(from, to);

            message.Subject = subject;
            message.IsBodyHtml = false;
            message.Body = body;

            smtpClient.Send(message);

            return true;
        }

}

}
