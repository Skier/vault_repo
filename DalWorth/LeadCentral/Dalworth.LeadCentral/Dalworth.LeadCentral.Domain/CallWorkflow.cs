using System;
using System.Collections;
using System.Data;
using Dalworth.LeadCentral.Data;

namespace Dalworth.LeadCentral.Domain
{
    public partial class CallWorkflow
    {
        public CallWorkflow()
        {
        }

        public WorkflowDetail[] RelatedDetails { get; set; }
        public Hashtable DetailsHash { get; set; }
/*

        private const String SqlSelectByPhoneNumbers = @"
SELECT cw.* FROM CallWorkflow cw
    INNER JOIN PhoneCallWorkflow pcw ON cw.Id = pcw.CallWorkflowId
    INNER JOIN TrackingPhone pTo ON pcw.TrackingPhoneId = pTo.Id
    LEFT OUTER JOIN Phone pFrom ON pcw.FromPhoneId = pFrom.Id
 WHERE pTo.Number = ?PhoneTo AND (pFrom.Number = ?PhoneFrom OR pcw.FromPhoneId Is NULL)
 ORDER BY pcw.FromPhoneId DESC; ";

        public static CallWorkflow GetByPhoneCall(PhoneCall phoneCall, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPhoneNumbers, connection))
            {
                Database.PutParameter(dbCommand, "?PhoneTo", phoneCall.PhoneTo);
                Database.PutParameter(dbCommand, "?PhoneFrom", phoneCall.PhoneFrom);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }
*/

        public static CallWorkflow GetByPrimaryKey(int callWorkflowId, IDbConnection connection)
        {
            var result = FindByPrimaryKey(callWorkflowId, connection);
            var details = WorkflowDetail.GetByWorkflowId(result.Id, connection);
            result.DetailsHash = new Hashtable();
            foreach (var workflowDetail in details)
            {
                result.DetailsHash[workflowDetail.PropertyName] = workflowDetail.PropertyValue;
            }
            return result;
        }
    }
}
