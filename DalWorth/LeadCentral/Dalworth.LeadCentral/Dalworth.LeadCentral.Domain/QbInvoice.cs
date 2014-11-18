using System.Collections.Generic;
using System.Data;
using Intuit.Sb.Cdm;
using Dalworth.LeadCentral.Data;

namespace Dalworth.LeadCentral.Domain
{
    public partial class QbInvoice
    {
        public QbInvoice()
        {
        }
        public Invoice RelatedIdsInvoice { get; set; }
        public Customer RelatedIdsCustomer { get; set; }

        public int MatchLevel { get; set; }
        public bool IsMatched { get; set; }

        private const string SqlSelectByIdsJobId = @"
SELECT * 
  FROM QbInvoice
 WHERE QbInvoiceId = ?QbInvoiceId ; ";

        public static QbInvoice GetByQbJobRecordId(string invoiceId, IDbConnection connection)
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

    }
}
      