using System;
using System.Configuration;
using System.IO;
using System.Net;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Intuit.Payment
{
    public partial class AddFunds : System.Web.UI.Page
    {
        private const string QbmsGetTicketUrlKey = "QbmsGetTicketUrl";
        private const string QbmsAppLoginKey = "QbmsAppLogin";
        private const string QbmsAuthTicketKey = "QbmsAuthTicket";
        private const string QbmsPaymentUrlKey = "QbmsPaymentUrl";

        protected void Page_Load(object sender, EventArgs e)
        {
            var realmId = Request.QueryString["realmId"];
            var amountStr = Request.QueryString["amount"];

            decimal amount;
            if (!decimal.TryParse(amountStr, out amount)) 
                return;

            var paymentParams = GetPaymentParams(amount);

            if (paymentParams.StatusCode != null && paymentParams.StatusCode == "0")
            {
                CreateQbmsTransaction(realmId, paymentParams);
                Response.Redirect(GetPaymentUrl(paymentParams));
            } else
            {
                Response.Redirect("~/ErrorPage.htm");
            }
        }

        private static void CreateQbmsTransaction(string realmId, PaymentTicketParams paymentParams)
        {
            var servmanCustomer = ServmanCustomerService.FindByRealmId(realmId);
            var transaction = new QbmsTransaction
                                  {
                                      ServmanCustomerId = servmanCustomer.Id,
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

        private static string GetPaymentUrl(PaymentTicketParams paymentParams)
        {
            var result = string.Empty;

            result += ConfigurationManager.AppSettings[QbmsPaymentUrlKey];
            result += string.Format("?Ticket={0}&OpId={1}", paymentParams.Ticket, paymentParams.OpId);

            return result;
        }

        private static PaymentTicketParams GetPaymentParams(decimal amount)
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

    class PaymentTicketParams
    {
        public string Ticket { get; set; }
        public string OpId { get; set; }
        public string StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public decimal Amount { get; set; }
    }

}