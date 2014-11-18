using System;
using System.Collections.Specialized;
using Dalworth.LeadCentral.Domain;
using Dalworth.LeadCentral.Service;

namespace Dalworth.LeadCentral.Intuit.Payment
{
    public partial class Confirm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var transaction = UpdateQbmsTransaction(Request.QueryString);
            if (transaction.Status.ToUpper() != "OK")
                return;

            var servmanCustomer = ServmanCustomer.FindByPrimaryKey(transaction.ServmanCustomerId);
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                BillingService.CreateExtraPaymentTransaction(DateTime.Now, transaction.Amount, transaction.Id,
                                                             connection);
            }

            Response.Redirect("Finish.aspx");
        }

        private static QbmsTransaction UpdateQbmsTransaction(NameValueCollection queryString)
        {
            var opId = queryString["OpId"];

            var qbmsTransaction = QbmsTransaction.GetByOpId(opId);
                qbmsTransaction.OpType = queryString["OpType"];
                qbmsTransaction.Status = queryString["Status"];
                qbmsTransaction.StatusCode = queryString["StatusCode"];
                qbmsTransaction.StatusMessage = queryString["StatusMessage"];
                qbmsTransaction.TxnType = queryString["TxnType"];
                qbmsTransaction.TxnTimestamp = queryString["TxnTimestamp"];
                qbmsTransaction.MaskedCCN = queryString["MaskedCCN"];
                qbmsTransaction.AuthCode = queryString["AuthCode"];
                qbmsTransaction.Amount = decimal.Parse(queryString["Amount"]);
                qbmsTransaction.TxnId = queryString["TxnId"];
            
            return ServmanCustomerService.SaveQbmsTransaction(qbmsTransaction);
        }
    }
}