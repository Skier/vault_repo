using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Data;

namespace Dalworth.LeadCentral.Domain
{
    public partial class PhoneCall
    {
        public LeadSource RelatedLeadSource { get; set; }

        public PhoneCall()
        {
            
        }
    
        private const String SqlSelectActiveCalls = @"
SELECT * 
  FROM PhoneCall
 WHERE IsAnsweredByUser = 0
   AND CallStatus = 'queued'; ";

        public static List<PhoneCall> GetActiveCalls(IDbConnection connection)
        {
            var result = new List<PhoneCall>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectActiveCalls, connection))
            {
                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }

        private const String SqlSelectByCallSid = @"
SELECT * 
  FROM PhoneCall 
 WHERE CallSid = ?CallSid; ";

        public static PhoneCall GetByCallSid(string callSid, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByCallSid, connection))
            {
                Database.PutParameter(dbCommand, "?CallSid", callSid);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        private const String SqlSelectCallsByPhoneId = @"
SELECT * 
  FROM PhoneCall
 WHERE TrackingPhoneId = ?TrackingPhoneId; ";

        public static List<PhoneCall> GetCallsByPhoneId(int phoneId, IDbConnection connection)
        {
            var result = new List<PhoneCall>();
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectCallsByPhoneId, connection))
            {
                Database.PutParameter(dbCommand, "?TrackingPhoneId", phoneId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                        result.Add(Load(dataReader));
                }
            }

            return result;
        }
    }
}
