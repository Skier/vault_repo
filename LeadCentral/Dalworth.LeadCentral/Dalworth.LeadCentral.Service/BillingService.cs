using System;
using System.Collections.Generic;
using System.Data;
using System.Security;
using Intuit.Platform.Client.Core;
using Dalworth.LeadCentral.Domain;

namespace Dalworth.LeadCentral.Service
{
    public class BillingService
    {
        #region GetAllTransactions

        public static List<Transaction> FIndTransactions(IDbConnection connection)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };

            if (ContextHelper.ActionDenied(allowedRoles))
                throw new SecurityException();

            var result = Transaction.GetAll(connection);
            return result;
        }

        #endregion 

        #region CreateTransaction

        public static Transaction CreateTransaction(IDbConnection connection, TransactionTypeEnum type, decimal quantity = 1)
        {
            return CreateTransaction(connection, type, quantity, null);
        }

        public static Transaction CreateTransaction(IDbConnection connection, TransactionTypeEnum type, decimal quantity, int? sourceId)
        {
            return CreateTransaction(connection, type, quantity, sourceId, null);
        }

        public static Transaction CreateTransaction(IDbConnection connection, TransactionTypeEnum type, decimal quantity, int? sourceId, int? campaignId)
        {
            return CreateTransaction(connection, type, quantity, sourceId, campaignId, null);
        }

        public static Transaction CreateTransaction(IDbConnection connection, TransactionTypeEnum type, decimal quantity, int? sourceId, int? campaignId, int? qbmsTransactionId)
        {
            var transactionType = TransactionType.FindByPrimaryKey((int)type, connection);

            var transaction = new Transaction
            {
                SourceId = sourceId,
                CampaignId = campaignId,
                DateCreated = DateTime.Now,
                TransactionTypeId = transactionType.Id,
                Description = type.ToString(),
                Quantity = quantity,
                QbmsTransactionId = qbmsTransactionId
            };
            transaction.Amount = (transactionType.BaseCost * transaction.Quantity);
            transaction.CurrentBalance = transaction.Amount + Transaction.GetCurrentBalance(connection);

            return Transaction.Save(transaction, connection);
        }

        #endregion 

        #region IsmBillingStatusOk

        public static bool IsBillingStatusOk(Customer customer)
        {
            var status = customer.BillingStatus;
            return (status == null || status == "OK");
        }

        #endregion

        #region GetCurrentBalance

        public static decimal GetCurrentBalance(IDbConnection connection)
        {
            return Transaction.GetCurrentBalance(connection);
        }

        #endregion 

        #region GetStatus

        public static BillingInfo GetStatus(PlatformSessionContext context, string dbId)
        {
            return context.GetBillingStatus(dbId);
        }

        #endregion

        #region CreatePaymentTransaction

        public static Transaction CreatePaymentTransaction(IDbConnection connection, decimal amount, int qbmsTransactionId)
        {
            var transactionType = TransactionType.FindByPrimaryKey((int)TransactionTypeEnum.ExtraPayment, connection);

            var transaction = new Transaction
            {
                DateCreated = DateTime.Now,
                TransactionTypeId = transactionType.Id,
                Quantity = 1,
                QbmsTransactionId = qbmsTransactionId,
                Description = TransactionTypeEnum.ExtraPayment.ToString(),
                Amount = amount
            };

            transaction.CurrentBalance = transaction.Amount + Transaction.GetCurrentBalance(connection);

            return Transaction.Save(transaction, connection);
        }

        #endregion

        #region CreateRecurringPaymentTransaction

        public static Transaction CreateRecurringPaymentTransaction(IDbConnection connection, decimal amount)
        {
            var transactionType = TransactionType.FindByPrimaryKey((int)TransactionTypeEnum.RecurringPayment, connection);

            var transaction = new Transaction
            {
                DateCreated = DateTime.Now,
                TransactionTypeId = transactionType.Id,
                Quantity = 1,
                Description = TransactionTypeEnum.ExtraPayment.ToString(),
                Amount = amount
            };

            transaction.CurrentBalance = transaction.Amount + Transaction.GetCurrentBalance(connection);

            return Transaction.Save(transaction, connection);
        }

        #endregion

        #region FindTransactions

        public static List<Transaction> FindTransactions(TransactionFilter filter, IDbConnection connection)
        {
            return Transaction.FindTransactions(filter, connection);
        }

        #endregion

        #region GetBillingStatus

        public static string GetBillingStatus()
        {
            var customer = ContextHelper.GetCurrentCustomer();
            return customer.BillingStatus;
        }

        #endregion 
    }
}
