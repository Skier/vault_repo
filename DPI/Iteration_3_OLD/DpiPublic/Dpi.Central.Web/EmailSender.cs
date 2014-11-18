using System;
using System.Globalization;
using DPI.Components;

namespace Dpi.Central.Web.Account
{
    internal abstract class EmailSender
    {
        private const string EMAIL_FROM = "customerservice@dpiteleconnect.com";

        #region PasswordReminder

        private const string PASSWORD_REMINDER_EMAIL_SUBJECT = "dPi Teleconnect Web Access - Password Reset";

        private const string PASSWORD_REMINDER_EMAIL_BODY =
            @"
Hello {0}. 

As per your request, your password has been reset for access to the dPi Teleconnect Web Site; www.dpiteleconnect.com 

Your temporary password is: {1} 

Upon login you will be asked to reset your password. Please do so immediately. 

If you have any questions, please contact us at your earliest convenience.

Thank you for choosing dPi Teleconnect. 

dPi Teleconnect Customer Service 
Email: customerservice@dpiteleconnect.com 
Visit: www.dpiteleconnect.com 
Customer Service Team: 1-800-350-4009";

        public static void SendPasswordRemider(string emailAddress, string firstName, string webPassword)
        {
            MailMessage msg = new MailMessage();

            msg.AddEmailTo(emailAddress);
            msg.EmailMessageType = MessageType.Text;
            msg.EmailFrom = EMAIL_FROM;
            msg.EmailSubject = PASSWORD_REMINDER_EMAIL_SUBJECT;
            msg.EmailMessage = string.Format(
                PASSWORD_REMINDER_EMAIL_BODY,
                CustInfo.CapitalizeName(firstName),
                webPassword);

            msg.SendMail();
        }

        #endregion

        #region Sign Up

        private const string NEW_ACCOUNT_EMAIL_SUBJECT = "dPi Teleconnect Web Access - Account Sign Up";

        private const string NEW_ACCOUNT_EMAIL_BODY =
            @"
Hello {0}, 

Thank you for signing up to use dPi Teleconnect Web Access! 

Our customer website provides you with the ability to view and validate account information such as your most recent billing statement, current payment due date, and amount owed. You also have the ability to view related order progress and request additional time to pay your bill. 

dPi Teleconnect always welcomes your suggestions on how we may improve the website to better serve YOUR needs. Please feel free to email us; customerservice@dpiteleconnect.com . 

During account registration, we required that you submit a valid email address. dPi Teleconnect will not disclose your email address to any third party. This email address will only be used to notify you of important account information, special offers from dPi Teleconnect, and to keep you informed of web access activity on your account. 

If you have any questions, please contact us at your earliest convenience. 

Thank you for choosing dPi Teleconnect. 

dPi Teleconnect Customer Service 

Email: customerservice@dpiteleconnect.com 
Visit: www.dpiteleconnect.com 
Customer Service Team: 1-800-350-4009";

        public static void SendNewAccountNotification(string emailAddress, string firstName)
        {
            MailMessage msg = new MailMessage();

            msg.AddEmailTo(emailAddress);
            msg.EmailMessageType = MessageType.Text;
            msg.EmailFrom = EMAIL_FROM;
            msg.EmailSubject = NEW_ACCOUNT_EMAIL_SUBJECT;
            msg.EmailMessage = string.Format(
                NEW_ACCOUNT_EMAIL_BODY, CustInfo.CapitalizeName(firstName));

            msg.SendMail();
        }

        #endregion

        #region AccountChange

        private const string ACCOUT_CHANGE_NOTIFICATION_EMAIL_SUBJECT = "dPi Teleconnect Web Access - Account Settings";

        private const string ACCOUT_CHANGE_NOTIFICATION_EMAIL_BODY =
            @"
Hello {0}, 

This email confirms that your account settings have been modified using www.dpiteleconnect.com. 

If you have any questions, please contact us at your earliest convenience. 

Thank you for choosing dPi Teleconnect. 

dPi Teleconnect Customer Service 

Email: customerservice@dpiteleconnect.com 
Visit: www.dpiteleconnect.com 
Customer Service Team: 1-800-350-4009";

        public static void SendAccountChangeNotification(string emailAddress, string oldEmailAddress, string firstName)
        {
            MailMessage msg = new MailMessage();
            msg.AddEmailTo(emailAddress);
            if (oldEmailAddress != null && oldEmailAddress.Length > 0 && oldEmailAddress != emailAddress) {
                msg.AddEmailTo(oldEmailAddress);
            }
            msg.EmailMessageType = MessageType.Text;
            msg.EmailFrom = EMAIL_FROM;
            msg.EmailSubject = ACCOUT_CHANGE_NOTIFICATION_EMAIL_SUBJECT;
            msg.EmailMessage = string.Format(
                ACCOUT_CHANGE_NOTIFICATION_EMAIL_BODY,
                CustInfo.CapitalizeName(firstName));

            msg.SendMail();
        }

        #endregion

        #region PromiseToPay

        private const string PROMISE_TO_PAY_SUBJ = "dPi Teleconnect Web Access - Promise To Pay";
        private const string PROMISE_TO_PAY_BODY = @"
Hello {0}. 

This email serves as a confirmation that we have processed your
Promise To Pay in the amount of {1};
scheduled to be received for no later than {2}. 

If you have any questions, please contact us at your earliest convenience. 

Thank you for choosing dPi Teleconnect. 

dPi Teleconnect Customer Service 

Email: customerservice@dpiteleconnect.com 
Visit: www.dpiteleconnect.com 
Customer Service Team: 1-800-350-4009";

        public static void SendPromiseToPayNotification(
            string emailAddress, DateTime payDate, decimal payAmount, string firstName)
        {
            MailMessage msg = new MailMessage();

            msg.AddEmailTo(emailAddress);

            msg.EmailMessageType = MessageType.Text;
            msg.EmailFrom = EMAIL_FROM;
            msg.EmailSubject = PROMISE_TO_PAY_SUBJ;
            msg.EmailMessage = string.Format(
                PROMISE_TO_PAY_BODY,
                CustInfo.CapitalizeName(firstName),
                payAmount.ToString("C"),
                payDate.ToString("MMM dd, yyyy", DateTimeFormatInfo.CurrentInfo));

            msg.SendMail();
        }

        #endregion

        #region Payment

        private const string PAYMENT_SUBJECT = "dPi Teleconnect Web Access - Payment Confirmation";
        private const string PAYMENT_BODY = @"
Hello {0}, 
This email serves as confirmation of your {1} payment processed online in 
the amount of {2} on {3}. The approval number for the payment transaction is {4}.

If you have any questions, please contact us at your earliest convenience.

Thank you for choosing dPi Teleconnect. 

dPi Teleconnect Customer Service 
email:    customerservice@dpiteleconnect.com 
web:      www.dpiteleconnect.com 
phone:  1-800-350-4009 ";

        public static void SendCreditCardPaymentNotification(string emailAddress, string firstName, decimal payAmount, int paymentId)
        {
            MailMessage msg = new MailMessage();

            msg.AddEmailTo(emailAddress);

            msg.EmailMessageType = MessageType.Text;
            msg.EmailFrom = EMAIL_FROM;
            msg.EmailSubject = PAYMENT_SUBJECT;
            msg.EmailMessage = String.Format(PAYMENT_BODY, CustInfo.CapitalizeName(firstName), "Credit Card", payAmount.ToString("C"), DateTime.Now.ToShortDateString(), paymentId);

            msg.SendMail();
        }
		
		public static void SendCheckPaymentNotification(string emailAddress, string firstName, decimal payAmount, int paymentId) 
		{
			MailMessage msg = new MailMessage();

			msg.AddEmailTo(emailAddress);

			msg.EmailMessageType = MessageType.Text;
			msg.EmailFrom = EMAIL_FROM;
			msg.EmailSubject = PAYMENT_SUBJECT;
			msg.EmailMessage = String.Format(PAYMENT_BODY, CustInfo.CapitalizeName(firstName), "Check", payAmount.ToString("C"), DateTime.Now.ToShortDateString(), paymentId);

			msg.SendMail();
		}

		#endregion
    }
}