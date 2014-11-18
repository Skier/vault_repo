using System;
using System.Collections;
using System.Collections.Generic;
using Dalworth.Common.SDK;
using Intuit.Platform.Client.Core;
using Intuit.Sb.Cdm;
using Intuit.Sb.Cdm.Common;
using IntuitCore = Intuit.Platform.Client.Core;

namespace Dalworth.LeadCentral.Service
{
    public class IdsInvoiceService
    {
        public static List<Invoice> GetAfterDate(PlatformSessionContext context, string realmId, DateTime date)
        {
            var service =GetInvoiceService(context);
            var invoiceQuery = new QBQBOInvoiceQuery
                                   {
                                       StartCreatedTMS = date, 
                                       StartCreatedTMSSpecified = true
                                   };

            return service.GetInvoices(context, realmId, invoiceQuery);
        }

        public static Invoice GetById(PlatformSessionContext context, string realmId, IdType id)
        {
            var service = GetInvoiceService(context);
            return service.FindById(context, realmId, id);
        }

        public static List<Invoice> GetByIdList(PlatformSessionContext context, string realmId, List<IdType> ids)
        {
            if (context.ServiceType == IntuitServicesType.QBO)
                return QboGetByIdList(context, realmId, ids);

            return QbGetByIdList(context, realmId, ids);
        }

        public static List<Invoice> QbGetByIdList(PlatformSessionContext context, string realmId, List<IdType> ids)
        {
            if (ids == null || ids.Count == 0)
                return null;

            var idList = new List<IdType>();
            foreach (var id in ids)
            {
                idList.Add(id);
            }

            var idSet = new IdSet { Id = idList.ToArray() };

            var itemElementName = new List<ItemsChoiceType4>();
            itemElementName.Add(ItemsChoiceType4.ListIdSet);

            var invoiceQuery = new QBQBOInvoiceQuery
            {
                Item1 = idSet,
                Item1ElementName = Item1ChoiceType4.TransactionIdSet
            };

            var service = GetInvoiceService(context);
            return service.GetInvoices(context, realmId, invoiceQuery);
        }

        public static List<Invoice> QboGetByIdList(PlatformSessionContext context, string realmId, List<IdType> ids)
        {
            if (ids == null || ids.Count == 0)
                return null;

            var service = GetInvoiceService(context);

            var result = new List<Invoice>();
            foreach (var id in ids)
            {
                var invoice = service.FindById(context, realmId, id);
                result.Add(invoice);
            }
            return result;
        }

        public static Hashtable GetInvoicesHashtableByIdList(PlatformSessionContext context, string realmId, List<IdType> ids)
        {
            var invoices = GetByIdList(context, realmId, ids);
            return GetInvoicesHashtable(invoices);
        }

        public static Hashtable GetInvoicesHashtable(List<Invoice> invoices)
        {
            var result = new Hashtable();

            if (invoices != null)
            {
                foreach (var invoice in invoices)
                {
                    result[IdTypeUtil.GetQbIdString(invoice.Id)] = invoice;
                }
            }

            return result;
        }

        private static InvoiceService GetInvoiceService(PlatformSessionContext context)
        {
            var service = new InvoiceService();
            try
            {
                context.GetCurrentUserInfo();
                return service;
            } catch(Exception)
            {
                throw new DalworthException("Session expired");
            }
        }

    }
}
