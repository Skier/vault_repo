using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;

namespace Dalworth.LeadCentral.Notification
{
    public class Notifier
    {
        private const string AwsAssessKeyConfigKey = "AwsAccessKey";
        private const string AwsSecretKeyConfigKey = "AwsSecretKey";

        public static void SendNotify(NotifyMessage message)
        {
            var destinationAddresses = new List<string>();

            var verifiedAddresses = GetVerifiedEmailAddresses();

            if (!verifiedAddresses.Contains(message.From))
            {
                VerifyEmailAddress(message.From);
                return;
            }

            foreach (var email in message.To)
            {
                destinationAddresses.Add(email);
            }
            
            var charset = Encoding.UTF8.WebName;

            var subjectContent = new Content();
            subjectContent.WithCharset(charset);
            subjectContent.WithData(message.Subject);

            var plainContent = new Content();
            plainContent.WithCharset(charset);
            plainContent.WithData(message.Body);

            var htmlContent = new Content();
            htmlContent.WithCharset(charset);
            htmlContent.WithData(message.HtmlBody ?? message.Body);

            var body = new Body();
            body.WithText(plainContent);
            body.WithHtml(htmlContent);

            var source = message.From;
            var destination = new Destination(destinationAddresses);
            var awsMessage = new Message(subjectContent, body);

            var request = new SendEmailRequest(source, destination, awsMessage);
            var sesClient = GetSesClient();
            sesClient.SendEmail(request);
        }

        public static void VerifyEmailAddress(string emailAddress)
        {
            var verifiedEmailAddresses = GetVerifiedEmailAddresses();
            if (!verifiedEmailAddresses.Contains(emailAddress))
            {
                var sesClient = GetSesClient();

                var request = new VerifyEmailAddressRequest();
                request.WithEmailAddress(emailAddress);
                sesClient.VerifyEmailAddress(request);
            }
        }

        public static void DeleteVerifiedEmailAddress(string emailAddress)
        {
            var verifiedEmailAddresses = GetVerifiedEmailAddresses();
            if (verifiedEmailAddresses.Contains(emailAddress))
            {
                var sesClient = GetSesClient();

                var request = new DeleteVerifiedEmailAddressRequest();
                request.WithEmailAddress(emailAddress);
                sesClient.DeleteVerifiedEmailAddress(request);
            }
        }

        public static List<string> GetVerifiedEmailAddresses()
        {
            var sesClient = GetSesClient();
            
            var request = new ListVerifiedEmailAddressesRequest();
            var response = sesClient.ListVerifiedEmailAddresses(request);
            var result = response.ListVerifiedEmailAddressesResult;

            return result.VerifiedEmailAddresses;
        }

        private static AmazonSimpleEmailService GetSesClient()
        {
            var awsAccessKey = ConfigurationManager.AppSettings[AwsAssessKeyConfigKey];
            var awsSecretKey = ConfigurationManager.AppSettings[AwsSecretKeyConfigKey];

            return AWSClientFactory.CreateAmazonSimpleEmailServiceClient(awsAccessKey, awsSecretKey);
        }

    }
}
