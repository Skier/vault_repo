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
        public static List<Transaction> GetAllTransactions(string ticket)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };

            if (ContextHelper.ActionDenied(ticket, allowedRoles))
                throw new SecurityException();

            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            List<Transaction> result;
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = Transaction.Find(connection);
            }
            return result;
        }

        public static List<Transaction> GetTransactions(string ticket, int offset, int limit)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };

            if (ContextHelper.ActionDenied(ticket, allowedRoles))
                throw new SecurityException();

            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            List<Transaction> result;
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                result = Transaction.GetTransactions(offset, limit, connection);
            }
            return result;
        }

        public static int GetTransactionsCount(string ticket)
        {
            var allowedRoles = new List<UserRoleEnum>
                                   {
                                       UserRoleEnum.Administrator
                                   };

            if (ContextHelper.ActionDenied(ticket, allowedRoles))
                throw new SecurityException();

            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);

            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                return Transaction.GetTransactionsCount(connection);
            }
        }

        public static Transaction CreatePhoneCallTransaction(ServmanCustomer servmanCustomer, PhoneCall phoneCall, TransactionTypeEnum typeEnum)
        {
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                return CreatePhoneCallTransaction(phoneCall, typeEnum, connection);
            }
        }

        public static Transaction CreatePhoneCallTransaction(PhoneCall phoneCall, TransactionTypeEnum typeEnum, IDbConnection connection)
        {
            var transactionType = TransactionType.FindByPrimaryKey((int) typeEnum, connection);
            decimal callDuration;
            decimal.TryParse(phoneCall.CallDuration, out callDuration);
            callDuration = Math.Ceiling(callDuration/60);
            var transaction = new Transaction
                                  {
                                      TrackingPhoneId = phoneCall.TrackingPhoneId,
                                      PhoneCallId = phoneCall.Id,
                                      PhoneSmsId = null,
                                      TransactionDate = DateTime.Now,
                                      TransactionTypeId = transactionType.Id,
                                      Quantity = callDuration
                                  };
            transaction.Amount = (transactionType.Cost * transaction.Quantity);
            transaction.CurrentBalance = transaction.Amount + Transaction.GetCurrentBalance(connection);

            return Transaction.Save(transaction, connection);
        }

        public static Transaction CreatePhoneDialTransaction(PhoneCall phoneCall, string durationStr, TransactionTypeEnum typeEnum, IDbConnection connection)
        {
            var transactionType = TransactionType.FindByPrimaryKey((int)typeEnum, connection);
            decimal callDuration;
            decimal.TryParse(durationStr, out callDuration);
            callDuration = Math.Ceiling(callDuration / 60);
            var transaction = new Transaction
            {
                TrackingPhoneId = phoneCall.TrackingPhoneId,
                PhoneCallId = phoneCall.Id,
                PhoneSmsId = null,
                TransactionDate = DateTime.Now,
                TransactionTypeId = transactionType.Id,
                Quantity = callDuration
            };
            transaction.Amount = (transactionType.Cost * transaction.Quantity);
            transaction.CurrentBalance = transaction.Amount + Transaction.GetCurrentBalance(connection);

            return Transaction.Save(transaction, connection);
        }

        public static Transaction CreatePhoneSmsTransaction(ServmanCustomer servmanCustomer, PhoneSms phoneSms)
        {
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                return CreatePhoneSmsTransaction(phoneSms, connection);
            }
        }

        public static Transaction CreatePhoneSmsTransaction(PhoneSms phoneSms, IDbConnection connection)
        {
            var transactionType = TransactionType.FindByPrimaryKey((int)TransactionTypeEnum.IncomeSms, connection);
            var transaction = new Transaction
            {
                TrackingPhoneId = phoneSms.TrackingPhoneId,
                PhoneCallId = null,
                PhoneSmsId = phoneSms.Id,
                TransactionDate = DateTime.Now,
                TransactionTypeId = transactionType.Id,
                Quantity = 1
            };
            transaction.Amount = (transactionType.Cost * transaction.Quantity);
            transaction.CurrentBalance = transaction.Amount + Transaction.GetCurrentBalance(connection);

            return Transaction.Save(transaction, connection);
        }

        public static Transaction CreateCallerIdLookupTransaction(PhoneCall phoneCall, IDbConnection connection)
        {
            var transactionType = TransactionType.FindByPrimaryKey((int)TransactionTypeEnum.CallerIdLookup, connection);
            var transaction = new Transaction
            {
                TrackingPhoneId = phoneCall.TrackingPhoneId,
                PhoneCallId = phoneCall.Id,
                PhoneSmsId = null,
                TransactionDate = DateTime.Now,
                TransactionTypeId = transactionType.Id,
                Quantity = 1
            };
            transaction.Amount = (transactionType.Cost * transaction.Quantity);
            transaction.CurrentBalance = transaction.Amount + Transaction.GetCurrentBalance(connection);

            return Transaction.Save(transaction, connection);
        }

        public static Transaction CreateTranscribeTransaction(PhoneCall phoneCall, IDbConnection connection)
        {
            var transactionType = TransactionType.FindByPrimaryKey((int)TransactionTypeEnum.VoiceTranscribe, connection);
            decimal callDuration;
            decimal.TryParse(phoneCall.CallDuration, out callDuration);
            callDuration = Math.Ceiling(callDuration / 60);
            var transaction = new Transaction
            {
                TrackingPhoneId = phoneCall.TrackingPhoneId,
                PhoneCallId = phoneCall.Id,
                PhoneSmsId = null,
                TransactionDate = DateTime.Now,
                TransactionTypeId = transactionType.Id,
                Quantity = callDuration
            };
            transaction.Amount = (transactionType.Cost * transaction.Quantity);
            transaction.CurrentBalance = transaction.Amount + Transaction.GetCurrentBalance(connection);

            return Transaction.Save(transaction, connection);
        }

        public static Transaction CreatePhoneNumberTransaction(TrackingPhone phone, IDbConnection connection)
        {
            var transactionType = TransactionType.FindByPrimaryKey((int)TransactionTypeEnum.PhoneNumberCharge, connection);
            var transaction = new Transaction
            {
                TrackingPhoneId = phone.Id,
                PhoneCallId = null,
                PhoneSmsId = null,
                TransactionDate = DateTime.Now,
                TransactionTypeId = transactionType.Id,
                Quantity = 1
            };
            transaction.Amount = (transactionType.Cost * transaction.Quantity);
            transaction.CurrentBalance = transaction.Amount + Transaction.GetCurrentBalance(connection);

            return Transaction.Save(transaction, connection);
        }

        public static Transaction CreateApplicationChargeTransaction(IDbConnection connection)
        {
            var transactionType = TransactionType.FindByPrimaryKey((int)TransactionTypeEnum.ApplicationCharge, connection);
            var transaction = new Transaction
            {
                TrackingPhoneId = null,
                PhoneCallId = null,
                PhoneSmsId = null,
                TransactionDate = DateTime.Now,
                TransactionTypeId = transactionType.Id,
                Quantity = 1
            };
            transaction.Amount = (transactionType.Cost * transaction.Quantity);
            transaction.CurrentBalance = transaction.Amount + Transaction.GetCurrentBalance(connection);

            return Transaction.Save(transaction, connection);
        }

        public static Transaction CreateExtraPaymentTransaction(DateTime date, decimal amount, int qbmsTransactionId, IDbConnection connection)
        {
            var transaction = new Transaction
            {
                TrackingPhoneId = null,
                PhoneCallId = null,
                PhoneSmsId = null,
                TransactionDate = date,
                TransactionTypeId = (int)TransactionTypeEnum.ExtraPayment,
                Quantity = 1,
                Amount = amount,
                QbmsTransactionId = qbmsTransactionId
            };

            transaction.CurrentBalance = transaction.Amount + Transaction.GetCurrentBalance(connection);

            return Transaction.Save(transaction, connection);
        }

        public static Transaction CreateRecurringPaymentTransaction(DateTime date, decimal amount, IDbConnection connection)
        {
            var result = new Transaction
            {
                TrackingPhoneId = null,
                PhoneCallId = null,
                PhoneSmsId = null,
                TransactionDate = date,
                TransactionTypeId = (int)TransactionTypeEnum.ExtraPayment,
                Quantity = 1,
                Amount = amount
            };
            result.CurrentBalance = result.Amount + Transaction.GetCurrentBalance(connection);
            result = Transaction.Save(result, connection);

            CreateApplicationChargeTransaction(connection);

            var phones = PhoneService.GetPhoneNumbers(connection);
            foreach (var phone in phones)
            {
                if (!phone.IsRemoved)
                    CreatePhoneNumberTransaction(phone, connection);
            }

            return result;
        }

        public static decimal GetCurrentBalance(string ticket)
        {
            var servmanCustomer = ContextHelper.GetCurrentCustomer(ticket);
            return GetCurrentBalance(servmanCustomer);
        }

        public static decimal GetCurrentBalance(ServmanCustomer servmanCustomer)
        {
            using (var connection = ServmanCustomerService.GetConnection(servmanCustomer))
            {
                return GetCurrentBalance(connection);
            }
        }

        public static decimal GetCurrentBalance(IDbConnection connection)
        {
            return Transaction.GetCurrentBalance(connection);
        }

        public static BillingInfo GetStatus(PlatformSessionContext context, string dbId)
        {
            return context.GetBillingStatus(dbId);
        }

    }
}
