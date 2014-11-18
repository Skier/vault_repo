using System;
using System.Collections.Generic;
using System.Data;
using Servman.Data;

namespace Servman.Domain
{
    public partial class Job
    {
        public Intuit.Sb.Cdm.Job RelatedIdsJob { get; set;}
        public Intuit.Sb.Cdm.Customer RelatedIdsCustomer {get; set; }

        public int MatchLevel { get; set; }
        public bool IsMatched { get; set; }

        public Job()
        {
            
        }

        private const String SqlSelectByIdsJobId = @"
SELECT * 
  FROM Job
 WHERE QbJobRecordId = ?QbJobRecordId ; ";

        public static Job GetByQbJobRecordId(string qbJobRecordId, IDbConnection connection)
        {
            using (var dbCommand = Database.PrepareCommand(SqlSelectByIdsJobId, connection))
            {
                Database.PutParameter(dbCommand, "?QbJobRecordId", qbJobRecordId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        private const String SqlSelectByLeadId = @"
SELECT * 
  FROM Job
 WHERE LeadId = ?LeadId ; ";

        public static List<Job> GetByLead(Lead lead, IDbConnection connection)
        {
            var result = new List<Job>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectByLeadId, connection))
            {
                Database.PutParameter(dbCommand, "?LeadId", lead.Id);

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

        private const String SqlSelectJobsBase = @"
SELECT * FROM Job 
 WHERE 1=0 ";

        public static List<Job> GetByLeadIds(int[] leadIds, IDbConnection connection)
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
                var result = new List<Job>();
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
      