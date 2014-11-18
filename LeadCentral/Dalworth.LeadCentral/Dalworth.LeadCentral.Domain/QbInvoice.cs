using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;
using Intuit.Sb.Cdm;
using IdsCustomer = Intuit.Sb.Cdm.Customer;


namespace Dalworth.LeadCentral.Domain
{
    public partial class QbInvoice
    {
        public QbInvoice()
        {
        }

        public Invoice RelatedIdsInvoice { get; set; }
        public IdsCustomer RelatedIdsCustomer { get; set; }

        public int MatchLevel { get; set; }
        public bool IsMatched { get; set; }

        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }

        public string InvoiceStatus { get; set; }

        public int Index { get; set; }

        public string ColorByMatchLevel
        {
            get
            {
                switch(MatchLevel)
                {
                    case 4:
                        return "#DFD";
                    case 3:
                        return "#DDF";
                    case 2:
                        return "#FFD";
                    case 1:
                        return "#FDD";
                    default:
                        return "#FFF";
                }
            }
        }

        public string IdsInvoiceNumber
        {
            get
            {
                if (RelatedIdsInvoice != null && RelatedIdsInvoice.Header != null)
                    return RelatedIdsInvoice.Header.DocNumber;
                return string.Empty;
            }
        }

        public string IdsInvoiceDateCreatedStr
        {
            get
            {
                if (RelatedIdsInvoice != null && RelatedIdsInvoice.MetaData != null)
                    return string.Format("{0:d}", RelatedIdsInvoice.MetaData.CreateTime);
                return string.Empty;
            }
        }

        public string IdsCustomerName
        { 
            get
            {
                return RelatedIdsCustomer != null ? RelatedIdsCustomer.ShowAs : string.Empty;
            }
        }

        public string IdsCustomerAddress
        {
            get
            {
                var result = string.Empty;
                if (RelatedIdsCustomer != null && RelatedIdsCustomer.Address != null && RelatedIdsCustomer.Address.Length > 0)
                {
                    var address = RelatedIdsCustomer.Address[0];
                    result += address.Line1;
                    result += ("\n" + address.Line2);
                    result += ("\n" + address.Line3);
                    result += ("\n" + address.Line4);
                }
                return result;
            }
        }

        public string IdsCustomerPhoneStr
        {
            get
            {
                var result = string.Empty;
                if (RelatedIdsCustomer != null && RelatedIdsCustomer.Phone != null && RelatedIdsCustomer.Phone.Length > 0)
                {
                    foreach(var phone in RelatedIdsCustomer.Phone)
                    {
                        result += (string.IsNullOrEmpty(result) ? phone.FreeFormNumber : (string.Format(", {0}", phone.FreeFormNumber)));
                    }
                }
                return result;
            }
        }

        private const string SqlSelectByIdsJobId = @"
SELECT * 
  FROM QbInvoice
 WHERE QbInvoiceId = ?QbInvoiceId ; ";

        public static QbInvoice GetByQbInvoiceId(string invoiceId, IDbConnection connection)
        {
            using (var dbCommand = Database.PrepareCommand(SqlSelectByIdsJobId, connection))
            {
                Database.PutParameter(dbCommand, "?QbInvoiceId", invoiceId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        private const string SqlSelectByLeadId = @"
SELECT * 
  FROM QbInvoice
 WHERE LeadId = ?LeadId ; ";

        public static List<QbInvoice> GetByLeadId(int leadId, IDbConnection connection)
        {
            var result = new List<QbInvoice>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectByLeadId, connection))
            {
                Database.PutParameter(dbCommand, "?LeadId", leadId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            return result;
        }

        private const string SqlSelectJobsBase = @"
SELECT * FROM QbInvoice 
 WHERE 1=0 ";

        public static List<QbInvoice> GetByLeadIds(int[] leadIds, IDbConnection connection)
        {
            var sql = SqlSelectJobsBase;

            if (leadIds != null && leadIds.Length > 0)
            {
                sql += " OR LeadId IN ( 0 ";
                sql += CreateInContent(leadIds);
                sql += " ) ;";
            }

            using (IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
            {
                var result = new List<QbInvoice>();
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
                return result;
            }
        }

        private static string CreateInContent(IEnumerable<int> leadIds)
        {
            var result = string.Empty;
            foreach (var id in leadIds)
            {
                result += ", ";
                result += id.ToString();
            }
            return result;
        }

        public static void Delete(int qbInvoiceId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
            {
                Database.PutParameter(dbCommand, "?Id", qbInvoiceId);
                dbCommand.ExecuteNonQuery();
            }
        }

    }
}
      