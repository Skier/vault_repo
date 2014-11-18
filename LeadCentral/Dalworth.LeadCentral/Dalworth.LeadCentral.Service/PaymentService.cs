using System.Configuration;
using System.IO;
using System.Net;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class PaymentService
    {
        private const string QbmsGetTicketUrlKey = "QbmsGetTicketUrl";
        private const string QbmsAppLoginKey = "QbmsAppLogin";
        private const string QbmsAuthTicketKey = "QbmsAuthTicket";
        private const string QbmsPaymentUrlKey = "QbmsPaymentUrl";

        public static void CreateQbmsTransaction(PaymentTicketParams paymentParams)
        {
            var customer = ContextHelper.GetCurrentCustomer();
            var transaction = new QbmsTransaction
                                  {
                                      CustomerId = customer.Id,
                                      OpId = paymentParams.OpId,
                                      Ticket = paymentParams.Ticket,
                                      Amount = paymentParams.Amount,
                                      StatusCode = paymentParams.StatusCode,
                                      StatusMessage = paymentParams.StatusMessage
                                  };
            QbmsTransaction.Save(transaction);
        }

        private static string GetTicketUrl(decimal amount)
        {
            var result = ConfigurationManager.AppSettings[QbmsGetTicketUrlKey];

            result += string.Format("?AppLogin={0}", ConfigurationManager.AppSettings[QbmsAppLoginKey]);
            result += string.Format("&AuthTicket={0}", ConfigurationManager.AppSettings[QbmsAuthTicketKey]);
            result += string.Format("&TxnType={0}", "Sale");
            result += string.Format("&Amount={0}", amount);

            return result;
        }

        public static string GetPaymentUrl(PaymentTicketParams paymentParams)
        {
            var result = string.Empty;

            result += ConfigurationManager.AppSettings[QbmsPaymentUrlKey];
            result += string.Format("?Ticket={0}&OpId={1}", paymentParams.Ticket, paymentParams.OpId);

            return result;
        }

        public static PaymentTicketParams GetPaymentParams(decimal amount)
        {
            var result = new PaymentTicketParams();

            var request = WebRequest.Create(GetTicketUrl(amount));
            var response = request.GetResponse();

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                while (!streamReader.EndOfStream)
                {
                    var line = streamReader.ReadLine().Trim();
                    var key = line.Substring(0, line.IndexOf('='));
                    var value = line.Substring(line.IndexOf('=') + 1);
                    switch (key.ToLower())
                    {
                        case "ticket":
                            result.Ticket = value;
                            break;
                        case "opid":
                            result.OpId = value;
                            break;
                        case "statuscode":
                            result.StatusCode = value;
                            break;
                        case "statusmessage":
                            result.StatusMessage = value;
                            break;
                    }
                }
            }

            result.Amount = amount;

            return result;
        }
    }

    public class PaymentTicketParams
    {
        public string Ticket { get; set; }
        public string OpId { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public decimal Amount { get; set; }
    }

}
