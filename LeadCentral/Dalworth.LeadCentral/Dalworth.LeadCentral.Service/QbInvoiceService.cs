using System;
using System.Collections.Generic;
using System.Data;
using Intuit.Sb.Cdm;
using Dalworth.LeadCentral.Domain;
using Dalworth.Common.SDK;
using IdsCustomer = Intuit.Sb.Cdm.Customer;
using IntuitCore = Intuit.Platform.Client.Core;

namespace Dalworth.LeadCentral.Service
{
    public class QbInvoiceService
    {
        #region GetUnmatchedByLead

        public static List<QbInvoice> GetUnmatchedByLead(Lead lead, DateTime? dateFrom, IDbConnection connection)
        {
            var context = ContextHelper.GetCurrentQbContext();
            var servmanCustomer = ContextHelper.GetCurrentCustomer();

            var result = new List<QbInvoice>();

            var date = dateFrom ?? lead.DateCreated;

            var invoices = IdsInvoiceService.GetAfterDate(context, servmanCustomer.RealmId, date);
            if (invoices == null)
                return result;

            var customerIds = new List<IdType>();
            foreach (var invoice in invoices)
            {
                customerIds.Add(invoice.Header.CustomerId);
            }
            var customersHash = IdsCustomerService.GetCustomersHashtableByIdList(context, servmanCustomer.RealmId, customerIds);

            int index = 0;
            foreach (var invoice in invoices)
            {
                var qbInvoice = QbInvoice.GetByQbInvoiceId(IdTypeUtil.GetQbIdString(invoice.Id), connection);
                if (qbInvoice == null)
                {
                    qbInvoice = new QbInvoice
                    {
                        QbInvoiceId = IdTypeUtil.GetQbIdString(invoice.Id),
                        IsMatched = false,
                        RelatedIdsInvoice = invoice,
                        RelatedIdsCustomer = customersHash[IdTypeUtil.GetQbIdString(invoice.Header.CustomerId)] as IdsCustomer,
                        Amount = invoice.Header.SubTotalAmt,
                        TaxAmount = invoice.Header.TaxAmt,
                        TotalAmount = invoice.Header.TotalAmt,
                        Index = index++
                    };
                    SetInvoiceMatchLevel(qbInvoice, lead);
                    result.Add(qbInvoice);
                }
            }

            result.Sort(delegate(QbInvoice a, QbInvoice b)
            {
                return b.MatchLevel - a.MatchLevel;
            });

            return result;
        }

        #endregion

        #region FindByLeadId

        public static List<QbInvoice> FindByLeadId(int leadId, IDbConnection connection)
        {
            var result = QbInvoice.GetByLeadId(leadId, connection);

            if (result.Count == 0)
                return result;

            var context = ContextHelper.GetCurrentQbContext();
            var servmanCustomer = ContextHelper.GetCurrentCustomer();

            var invoiceIds = new List<IdType>();

            foreach (var qbInvoice in result)
            {
                invoiceIds.Add(IdTypeUtil.ParseQbIdString(qbInvoice.QbInvoiceId));
            }
            var invoices = IdsInvoiceService.GetByIdList(context, servmanCustomer.RealmId, invoiceIds);
            var invoicesHashtable = IdsInvoiceService.GetInvoicesHashtable(invoices);

            var customerIds = new List<IdType>();
            foreach(Invoice invoice in invoices)
            {
                customerIds.Add(invoice.Header.CustomerId);
            }
            var customersHashtable = IdsCustomerService.GetCustomersHashtableByIdList(context, servmanCustomer.RealmId, customerIds);

            var index = 0;
            foreach (var qbInvoice in result)
            {
                qbInvoice.RelatedIdsInvoice = invoicesHashtable[qbInvoice.QbInvoiceId] as Invoice;
                if (qbInvoice.RelatedIdsInvoice != null)
                {
                    qbInvoice.IsMatched = true;
                    qbInvoice.Amount = qbInvoice.RelatedIdsInvoice.Header.SubTotalAmt;
                    qbInvoice.TaxAmount = qbInvoice.RelatedIdsInvoice.Header.TaxAmt;
                    qbInvoice.TotalAmount = qbInvoice.RelatedIdsInvoice.Header.TotalAmt;
                    qbInvoice.RelatedIdsCustomer = 
                        customersHashtable[IdTypeUtil.GetQbIdString(qbInvoice.RelatedIdsInvoice.Header.CustomerId)] as IdsCustomer;
                    qbInvoice.Index = index++;
                }
            }

            return result;
        }

        #endregion

        #region SetInvoiceMatchLevel

        private static void SetInvoiceMatchLevel(QbInvoice qbInvoice, Lead lead)
        {
            bool phoneMatched = false;
            bool firstNameMatched = false;
            bool lastNameMatched = false;

            qbInvoice.MatchLevel = 0;

            if (lead == null)
                return;

            if (qbInvoice.RelatedIdsInvoice == null)
                return;

            if (qbInvoice.RelatedIdsCustomer != null)
            {
                var parentCustomer = qbInvoice.RelatedIdsCustomer;
                if (parentCustomer.Phone != null)
                {
                    foreach (var phone in parentCustomer.Phone)
                    {
                        if (StringUtil.ExtractLastSevenDigits(lead.Phone) == StringUtil.ExtractLastSevenDigits(phone.FreeFormNumber))
                        {
                            phoneMatched = true;
                            break;
                        }
                    }
                }
            }

            if (qbInvoice.RelatedIdsCustomer != null 
                && qbInvoice.RelatedIdsCustomer.GivenName != null 
                && lead.FirstName != null
                && qbInvoice.RelatedIdsCustomer.GivenName.Trim().ToUpper() == lead.FirstName.Trim().ToUpper())
                firstNameMatched = true;

            if (qbInvoice.RelatedIdsCustomer != null
                && qbInvoice.RelatedIdsCustomer.FamilyName != null
                && lead.LastName != null
                && qbInvoice.RelatedIdsCustomer.FamilyName.Trim().ToUpper() == lead.LastName.Trim().ToUpper())
                lastNameMatched = true;
            
            if (lastNameMatched && !firstNameMatched && !phoneMatched)
                qbInvoice.MatchLevel = 1;

            if (lastNameMatched && firstNameMatched && !phoneMatched)
                qbInvoice.MatchLevel = 2;

            if (!lastNameMatched && phoneMatched)
                qbInvoice.MatchLevel = 3;

            if (lastNameMatched && phoneMatched)
                qbInvoice.MatchLevel = 4;
        }

        #endregion

        #region MatchIdsInvoiceToLeadId

        public static void MatchIdsInvoiceToLeadId(string qbInvoiceId, int leadId, IDbConnection connection)
        {
            var qbInvoice = new QbInvoice();
            qbInvoice.LeadId = leadId;
            qbInvoice.QbInvoiceId = qbInvoiceId;
            QbInvoice.Insert(qbInvoice, connection);
        }

        #endregion 

        #region MatchToLead

        public static void MatchToLead(QbInvoice qbInvoice, Lead lead, IDbConnection connection)
        {
            qbInvoice.LeadId = lead.Id;
            QbInvoice.Insert(qbInvoice, connection);

            if (lead.LeadStatusId == (int)LeadStatusEnum.Converted)
                return;

            lead.LeadStatusId = (int)LeadStatusEnum.Converted;
            Lead.Update(lead, connection);
        }

        #endregion

        #region GetByLeadIds

        public static List<QbInvoice> GetByLeadIds(int[] leadIds, IDbConnection connection)
        {
            return QbInvoice.GetByLeadIds(leadIds, connection);
        }

        #endregion

        #region UnMatchFromLead

        public static void UnMatchFromLead(QbInvoice qbInvoice, Lead lead, IDbConnection connection)
        {
            QbInvoice.Delete(qbInvoice, connection);
        }

        #endregion

        #region UnMatchQbInvoice

        public static void UnMatchQbInvoice(int qbInvoiceId, IDbConnection connection)
        {
            QbInvoice.Delete(qbInvoiceId, connection);
        }

        #endregion 
    }
}
