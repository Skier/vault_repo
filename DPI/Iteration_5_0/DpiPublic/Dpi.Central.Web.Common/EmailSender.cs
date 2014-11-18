using System;
using System.Collections;
using System.Globalization;
using DPI.Components;

namespace Dpi.Central.Web
{
    public class EmailSender
    {
        #region Constants

        private const string EMAIL_FROM = "customerservice@dpiteleconnect.com";
        private const string CONCLUSION = @"
<p>If you have any questions, please contact us at your 
earliest convenience. Thank you for choosing dPi Teleconnect.</p>
<h4>dPi Teleconnect Customer Service</h4>
Email: <a href=""mailto: customerservice@dpiteleconnect.com"">
customerservice@dpiteleconnect.com</a><br>
Visit: <a href=""http://www.dpiteleconnect.com"">
http://www.dpiteleconnect.com</a><br>
Customer Service Team: 1-800-350-4009 </p>";

        #endregion

        #region PasswordReminder

        private const string PASSWORD_REMINDER_EMAIL_SUBJECT = "Password Reset";
        private const string PASSWORD_REMINDER_EMAIL_BODY = @"
<h3>Hello {0},</h3>
<p>As per your request, your password has been reset for access to the dPi 
Teleconnect Web Site; <a href=""http://www.dpiteleconnect.com"">
http://www.dpiteleconnect.com</a>. Your temporary password is: {1} 
Upon login you will be asked to reset your password. Please do so immediately.</p>";

        public static void SendPasswordRemider(string emailAddress, string name, string webPassword)
        {
            string body = string.Format(PASSWORD_REMINDER_EMAIL_BODY, name, webPassword);
            SendMessage(emailAddress, PASSWORD_REMINDER_EMAIL_SUBJECT, body, true);
        }

        #endregion

        #region Sign Up

        private const string NEW_ACCOUNT_EMAIL_SUBJECT = "Account Sign Up";
        private const string NEW_ACCOUNT_EMAIL_BODY = @"
<h3>Hello {0},</h3>
<p>Thank you for signing up to use dPi Teleconnect Web Access!</p>
<p>Our customer website provides you with the ability to view and validate account information 
such as your most recent billing statement, current payment due date, and amount owed.
You also have the ability to view related order progress and request additional time to 
pay your bill.</p>
<p>dPi Teleconnect always welcomes your suggestions on how we may improve the 
website to better serve YOUR needs. Please feel free to email us; <a href=""mailto: customerservice@dpiteleconnect.com"">customerservice@dpiteleconnect.com</a>.</p>
<p>During account registration, we required that you submit a valid email address. dPi Teleconnect 
will not disclose your email address to any third party. This email address will only be 
used to notify you of important account information, special offers from dPi Teleconnect, 
and to keep you informed of web access activity on your account.</p>";

        public static void SendNewAccountNotification(string emailAddress, string name)
        {
            string body = string.Format(NEW_ACCOUNT_EMAIL_BODY, name);
            SendMessage(emailAddress, NEW_ACCOUNT_EMAIL_SUBJECT, body, true);
        }

        #endregion

        #region AccountChange

        private const string ACCOUT_CHANGE_NOTIFICATION_EMAIL_SUBJECT = "Account Settings";
        private const string ACCOUT_CHANGE_NOTIFICATION_EMAIL_BODY = @"
<h3>Hello {0},</h3>
<p>This email confirms that your account settings have been modified using <a href=""http://www.dpiteleconnect.com"">
http://www.dpiteleconnect.com</a>.</p>";

        public static void SendAccountChangeNotification(string emailAddress, string oldEmailAddress, string name)
        {
            string body = string.Format(ACCOUT_CHANGE_NOTIFICATION_EMAIL_BODY, name);
            SendMessage(emailAddress, ACCOUT_CHANGE_NOTIFICATION_EMAIL_SUBJECT, body, true);

            if (oldEmailAddress != null && oldEmailAddress.Length > 0 && oldEmailAddress != emailAddress) {
                SendMessage(oldEmailAddress, ACCOUT_CHANGE_NOTIFICATION_EMAIL_SUBJECT, body, true);
            }
        }

        #endregion

        #region PromiseToPay

        private const string PROMISE_TO_PAY_SUBJECT = "Promise To Pay";
        private const string PROMISE_TO_PAY_BODY = @"
<h3>Hello {0},</h3>
<p>This email serves as a confirmation that we have processed your
Promise To Pay in the amount of {1};
scheduled to be received for no later than {2}.</p>";

        public static void SendPromiseToPayNotification(string emailAddress, DateTime payDate, decimal payAmount, string name)
        {
            string body = string.Format(PROMISE_TO_PAY_BODY, name, payAmount.ToString("C"), payDate.ToString("MMM dd, yyyy", DateTimeFormatInfo.CurrentInfo));
            SendMessage(emailAddress, PROMISE_TO_PAY_SUBJECT, body, true);
        }

        #endregion

        #region Payment

        private const string PAYMENT_SUBJECT = "Payment Confirmation";
        private const string PAYMENT_SUBJECT_DECLINED = "Payment Declined";
        private const string PAYMENT_BODY = @"
<h3>Hello {0},</h3>
<p>This email serves as confirmation of your {1} payment processed online in 
the amount of {2} on {3}. The Approval/Confirmation number for the payment transaction is {4}.</p>";
        
        private const string PAYMENT_CC_DECLINED_BODY = @"
<h3>Hello {0},</h3>
<p>Thank you for choosing dPi as your home telephone service provider. Unfortunately we were unable to process your debit/credit card. The transaction declined for one of the following reasons: insufficient funds, card reported lost/stolen or reason undetermined. However, your order “is Still Approved.” We encourage you to attempt your purchase again with an alternate debit/credit card or give us a call at 1-800-350-4009 with a different credit card. You may also visit any Western Union or ACE Check Cashing location and pay {1}. Please call us after you have made your payment or mail in a money order payable to: dPi Teleconnect, 2997 LBJ Freeway, Suite 225 ATTN: dPi Customer Service Team Dallas, TX 75234.</p>";
        
        private const string PAYMENT_CHECK_DECLINED_BODY = @"
<h3>Hello {0},</h3>
<p>Thank you for choosing dPi as your home telephone service provider. Unfortunately we were unable to process your check. The transaction declined for one of the following reasons: insufficient funds/reason undetermined. However, your order “is Still Approved.” We encourage you to attempt your purchase again with an alternate debit/credit card or give us a call at 1-800-350-4009. You may also visit any Western Union or ACE Check Cashing location and pay {1}. Please call us after you have made your payment or mail in a money order payable to: dPi Teleconnect, 2997 LBJ Freeway, Suite 225 ATTN: dPi Customer Service Team Dallas, TX 75234.</p>";

        public static void SendCreditCardPaymentNotification(string emailAddress, string name, decimal payAmount, int confNum)
        {
            string body = String.Format(PAYMENT_BODY, name, "Credit Card", payAmount.ToString("C"), DateTime.Now.ToShortDateString(), confNum);
            SendMessage(emailAddress, PAYMENT_SUBJECT, body, true);
        }

        public static void SendCheckPaymentNotification(string emailAddress, string name, decimal payAmount, int confNum)
        {
            string body = String.Format(PAYMENT_BODY, name, "Check", payAmount.ToString("C"), DateTime.Now.ToShortDateString(), confNum);
            SendMessage(emailAddress, PAYMENT_SUBJECT, body, true);
        }
        
        public static void SendCreditCardPaymentDeclinedNotification(string emailAddress, string name, decimal payAmount)
        {
            string body = String.Format(PAYMENT_CC_DECLINED_BODY, name, payAmount.ToString("C"));
            SendMessage(emailAddress, PAYMENT_SUBJECT_DECLINED, body, true);
        }

        public static void SendCheckPaymentDeclinedNotification(string emailAddress, string name, decimal payAmount)
        {
            string body = String.Format(PAYMENT_CHECK_DECLINED_BODY, name, payAmount.ToString("C"));
            SendMessage(emailAddress, PAYMENT_SUBJECT_DECLINED, body, true);
        }

        #endregion

        #region Recurring Payment

        private const string REC_PAYMENT_SUBJECT = "Recurring Payment Confirmation";
        private const string REC_PAYMENT_BODY = @"
<h3>Hello {0},</h3>
<p>This email confirms that you have setup a recurring payment using your {1}.</p>";

        public static void SendRecurringPaymentConfirmation(string emailAddress, string name, bool ccPayment)
        {
            string body = string.Format(REC_PAYMENT_BODY, name, ccPayment ? "credit card" : "bank account");
            SendMessage(emailAddress, REC_PAYMENT_SUBJECT, body, true);
        }

        #endregion

        #region Create Account

        private const string ZOOM_TERMS = @"<p>Your use of iZoomOnline's services 
		through the software is also subject to iZoomOnline's Terms of Use<br>
		as amended from time to time by iZoomOnline and located at 
		www.izoomonline.com/company.<br>
		Technical Support contact information:
		<br>
		Customer Service: 877-4-ZOOMERS (877-496-6637)
		<br>
		Web Site: www.izoomonline.com
		<br>
		Email: support@izoomonline.com</p>";

        private const string STARTER_KIT = @"<p>A Starter Kit will be mailed to you 
            upon Activation of your telephone service with dPi Teleconnect.
            <br>
            Please allow 3-5 business days from the date of Activation to receive 
            the Installation CD and Starter Kit.</p>";

        private const string CREATE_ACCOUNT_SUBJECT = "New Account SignUp";
        private const string CREATE_ACCOUNT_BODY = @"

<h3>Welcome {0} to dPi Teleconnect!</h3>
{1}
<table border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"">
<tr>
<td colspan=""2""><h4>Account Information</h4>
</td>
</tr>
<tr>
<td width=""30%"">Account Number:</td>
<td>{2}</td>
</tr>
<tr>
<td>Account Name:</td>
<td>{3}</td>
</tr>
</table>
<br>
<table border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"">
<tr>
<td colspan=""2""><h4>Payor Information</h4>
</td>
</tr>
<tr>
<td width=""30%"">Payor Name:</td>
<td>{4}</td>
</tr>
<tr>
<td>Street Address:</td>
<td>{5}</td>
</tr>
<tr>
<td>City State Zip:</td>
<td>{6}</td>
</tr>
<tr>
<td>E-Mail:</td>
<td>{7}</td>
</tr>
</table>
<br>
{17}
<br>
<table border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"">
<tr>
<td colspan=""2""><h4>Payment Information</h4></td>
</tr>
<tr>
<td width=""30%"">Payment Type:</td>
<td>{8}</td>
</tr>
<tr>
<td>{9}:</td>
<td>{10}</td>
</tr>
<tr>
<td>Payment Amount:</td>
<td>{11}</td>
</tr>
<tr>
<td>Payment Date:</td>
<td>{12}</td>
</tr>
<tr>
<td>Approval/Confirmation Number:</td>
<td>{13}</td>
</tr>
</table>
{14}
{15}
{16}
{18}";

        private static string CreateProductsSection(ArrayList products)
        {
            if (products.Count == 0) {
                return string.Empty;    
            }

            string productsSection = @"<table border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%""><tr><td colspan=""2""><h4>Product Information</h4></td></tr>";

            foreach (DictionaryEntry product in products) {
                productsSection += @"<tr><td width=""30%"">" + product.Key + @"</td><td>" + product.Value + @"</td></tr>";
            }

            productsSection += "</table>";

            return productsSection;
        }

        public static void SendCreateAccountNotification(
            string emailAddress,
            string accountName,
            string accountNumber,
            string payorName,
            string streetAddress,
            string cityStateZip,
            string email,
            string paymentType,
            string paymentCaption,
            string paymentMeanNumber,
            string paymentAmount,
            string paymentDate,
            string confirmationNumber,
            string productInstruction,
            ArrayList products,
            bool containsInternetProduct)
        {
            string productsSection = CreateProductsSection(products);

            string body = string.Format(CREATE_ACCOUNT_BODY,
                                        accountName,
                                        @"<p>To manage your account you may access our Customer website: <a href=""http://www.dpiteleconnect.com"">http://www.dpiteleconnect.com</a>. Within 3 to 5 business days you can access your account to obtain your Activation date and Telephone Number. Please allow up to 7 business days for service connection.</p>",
                                        accountNumber,
                                        accountName,
                                        payorName.ToUpper(),
                                        streetAddress.ToUpper(),
                                        cityStateZip.ToUpper(),
                                        email,
                                        paymentType,
                                        paymentCaption,
                                        paymentMeanNumber,
                                        paymentAmount,
                                        paymentDate,
                                        confirmationNumber,
                                        @"<p>dPi also offers Long Distance, Wireless and Internet services for your ""one-stop"" convenience.</p>",
                                        CONCLUSION,
                                        productInstruction,
                                        productsSection,
                containsInternetProduct ? ZOOM_TERMS : string.Empty);

            if (containsInternetProduct) {
                body += STARTER_KIT;
            }

            SendMessage(emailAddress, CREATE_ACCOUNT_SUBJECT, body, false);
        }

        public static void SendCreateAccountNotificationToPayor(
            string emailAddress,
            string accountName,
            string accountNumber,
            string payorName,
            string streetAddress,
            string cityStateZip,
            string email,
            string paymentType,
            string paymentCaption,
            string paymentMeanNumber,
            string paymentAmount,
            string paymentDate,
            string confirmationNumber) 
        {
            string body = string.Format(CREATE_ACCOUNT_BODY,
                                        payorName,
                                        @"<p>This email serves as confirmation of you " + paymentType + " payment processed on line for New Service with dPi Teleconnect.</p>",
                                        accountNumber,
                                        accountName,
                                        payorName.ToUpper(),
                                        streetAddress.ToUpper(),
                                        cityStateZip.ToUpper(),
                                        email,
                                        paymentType,
                                        paymentCaption,
                                        paymentMeanNumber,
                                        paymentAmount,
                                        paymentDate,
                                        confirmationNumber,
                                        string.Empty,
                                        CONCLUSION,
                                        string.Empty,
                                        string.Empty,
                                        string.Empty);

            SendMessage(emailAddress, CREATE_ACCOUNT_SUBJECT, body, false);
        }

        private const string CREATE_ACCOUNT_BODY_SHORT = @"

<h3>Welcome {0} to dPi Teleconnect!</h3>
{1}
<table border=""0"" cellspacing=""0"" cellpadding=""0"" width=""100%"">
<tr>
<td colspan=""2""><h4>Account Information</h4>
</td>
</tr>
<tr>
<td width=""30%"">Account Number:</td>
<td>{2}</td>
</tr>
<tr>
<td>Account Name:</td>
<td>{3}</td>
</tr>
</table>
<br>
{7}
{4}
{5}
{6}
{8}";

        public static void SendCreateAccountNotificationShort(
            string emailAddress,
            string accountName,
            string accountNumber,
            string payorName,
            string streetAddress,
            string cityStateZip,
            string email,
            string paymentType,
            string paymentCaption,
            string paymentMeanNumber,
            string paymentAmount,
            string paymentDate,
            string confirmationNumber,
            string productInstruction,
            ArrayList products,
            bool containsInternetProduct) 
        {
            string productsSection = CreateProductsSection(products);

            string body = string.Format(CREATE_ACCOUNT_BODY_SHORT,
                                        accountName,
                                        @"<p>To manage your account you may access our Customer website: <a href=""http://www.dpiteleconnect.com"">http://www.dpiteleconnect.com</a>. Within 3 to 5 business days you can access your account to obtain your Activation date and Telephone Number. Please allow up to 7 business days for service connection.</p>",
                                        accountNumber,
                                        accountName,
                                        @"<p>dPi also offers Long Distance, Wireless and Internet services for your ""one-stop"" convenience.</p>",
                                        CONCLUSION,
                                        productInstruction, 
                                        productsSection,
                containsInternetProduct ? ZOOM_TERMS : string.Empty);

            if (containsInternetProduct) {
                body += STARTER_KIT;
            }

            SendMessage(emailAddress, CREATE_ACCOUNT_SUBJECT, body, false);
        }

        #endregion

        #region Wireless

        private const string WIRELESS_SIGN_UP_EMAIL_SUBJECT = "Wireless Account Sign Up";
        private const string WIRELESS_SIGN_UP_EMAIL_BODY = @"
Hello {0},
Thank you for signing up to use dPi Teleconnect Web Access!
Your password is {1}.
Our customer website provides you with the ability to view and validate account information 
such as your most recent billing statement, current payment due date, and amount owed.
You also have the ability to view related order progress and request additional time to 
pay your bill.";

        public static void SendWirelessSignUpNotification(string emailAddress, string name, string password)
        {
            string body = string.Format(WIRELESS_SIGN_UP_EMAIL_BODY, name, password);
            SendMessage(emailAddress, WIRELESS_SIGN_UP_EMAIL_SUBJECT, body, true);
        }

        private const string WIRELESS_ACCOUT_CHANGE_NOTIFICATION_EMAIL_SUBJECT = "Account Settings";
        private const string WIRELESS_ACCOUT_CHANGE_NOTIFICATION_EMAIL_BODY = @"
Hello {0}, 
this email confirms that your account settings have been modified using http://www.dpiteleconnect.com";

        public static void SendWirelessAccountChangeNotification(string emailAddress, string oldEmailAddress, string name) 
        {
            string body = string.Format(WIRELESS_ACCOUT_CHANGE_NOTIFICATION_EMAIL_BODY, name);
            SendMessage(emailAddress, WIRELESS_ACCOUT_CHANGE_NOTIFICATION_EMAIL_SUBJECT, body, true);

            if (oldEmailAddress != null && oldEmailAddress.Length > 0 && oldEmailAddress != emailAddress) {
                SendMessage(oldEmailAddress, WIRELESS_ACCOUT_CHANGE_NOTIFICATION_EMAIL_SUBJECT, body, true);
            }
        }

        private const string WIRELESS_PASSWORD_REMINDER_EMAIL_SUBJECT = "Password Reset";
        private const string WIRELESS_PASSWORD_REMINDER_EMAIL_BODY = @"
Hello {0},
As per your request, your password has been reset for access to the dPi 
Teleconnect Web Site http://www.dpiteleconnect.com. Your temporary password is: {1} 
Upon login you will be asked to reset your password. Please do so immediately.";

        public static void SendWirelessPasswordRemider(string emailAddress, string name, string webPassword) 
        {
            string body = string.Format(WIRELESS_PASSWORD_REMINDER_EMAIL_BODY, name, webPassword);
            SendMessage(emailAddress, WIRELESS_PASSWORD_REMINDER_EMAIL_SUBJECT, body, true);
        }

        #endregion

        #region Private Methods

        private static void SendMessage(string to, string subject, string body, bool appendConclustion)
        {
            MailMessage msg = new MailMessage();

            msg.AddEmailTo(to);

            msg.EmailMessageType = MessageType.HTML;
            msg.EmailFrom = EMAIL_FROM;
            msg.EmailSubject = "dPi Teleconnect Web Access - " + subject;
            msg.EmailMessage = body + (appendConclustion ? CONCLUSION : string.Empty);

            msg.SendMail();
        }

        #endregion
    }
}