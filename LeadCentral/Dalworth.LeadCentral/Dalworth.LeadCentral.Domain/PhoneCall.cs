using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Common.Data;
using Configuration = Dalworth.Common.SDK.Configuration;

namespace Dalworth.LeadCentral.Domain
{
    public partial class PhoneCall
    {
        public List<TrackingPhoneRotation> TrackingPhoneRotations { get; set; }

        #region Constructor 

        public PhoneCall()
        {
        }

        #endregion

        #region Properties 

        public string CallDurationStr
        {
            get
            {
                var t = TimeSpan.FromSeconds(decimal.ToDouble(CallDuration));
                return string.Format("{0:00}:{1:00}", t.Minutes, t.Seconds);
            } 
        }

        public string FullRecordingUrl
        {
            get
            {
                return Configuration.LeadCentralCommon.CallUrl + "/" + RecordingUrl;
            }
        }

        public bool IsFromPhoneBlackListed { get; set; }

        #endregion

        #region GetByTwilioCallId

        private const string SqlSelectByCallSid = @"
        SELECT * 
        FROM PhoneCall 
        WHERE TwilioCallId = ?TwilioCallId; ";

        public static PhoneCall GetByTwilioCallId(string twilioCallId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByCallSid, connection))
            {
                Database.PutParameter(dbCommand, "?TwilioCallId", twilioCallId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        #endregion

        #region GetUnprocessed

        private const string SqlSelectPending = @"
        SELECT * 
        FROM PhoneCall 
        WHERE `IsProcessed` = 0 AND `Status` = 'completed' ; ";

        public static List<PhoneCall> FindPending(IDbConnection connection)
        {
            var result = new List<PhoneCall>();

            using (var dbCommand = Database.PrepareCommand(SqlSelectPending, connection))
            {
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

        #endregion
    }
}
      