using System;
using System.Collections.Generic;
using System.Data;
using Servman.Data;
using Servman.SDK;

namespace Servman.Domain
{
    public partial class PhoneCallWorkflow
    {
        public TrackingPhone RelatedPhoneTo { get; set; }
        public CallWorkflow RelatedWorkflow { get; set; }

        private const String SqlSelectByTrackingPhoneId = @"
SELECT * 
  FROM PhoneCallWorkflow
 WHERE TrackingPhoneId = ?TrackingPhoneId
ORDER BY Priority ; ";

        public static List<PhoneCallWorkflow> GetByTrackingPhoneId(int trackingPhoneId, IDbConnection connection)
        {
            var result = new List<PhoneCallWorkflow>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByTrackingPhoneId, connection))
            {
                Database.PutParameter(dbCommand, "?TrackingPhoneId", trackingPhoneId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        result.Add(Load(dataReader));
                    }
                }
            }

            foreach (var workflow in result)
            {
                workflow.RelatedPhoneTo = TrackingPhone.FindByPrimaryKey(workflow.TrackingPhoneId, connection);
                workflow.RelatedWorkflow = CallWorkflow.GetByPrimaryKey(workflow.CallWorkflowId, connection);
            }

            return result;
        }

        public bool IsMatch(PhoneCall call)
        {
            bool isPhoneFromMatch;

            if (string.IsNullOrEmpty(FromPhoneNumber))
                isPhoneFromMatch = true;
            else if (StringUtil.ExtractLastSevenDigits(FromPhoneNumber) == StringUtil.ExtractLastSevenDigits(call.PhoneFrom))
                isPhoneFromMatch = true;
            else
                isPhoneFromMatch = false;

            var dateTime = call.DateCreated;
            var isDayMatch = IsDayOfWeekMatch(dateTime);
            var isTimeMatch = IsTimeMatch(dateTime);

            return (isPhoneFromMatch && isDayMatch && isTimeMatch);
        }

        private bool IsDayOfWeekMatch(DateTime time)
        {
            if (FromWeekDay == null || ToWeekDay == null)
                return true;

            if (FromWeekDay.Value <= ToWeekDay.Value)
            {
                for (var i = FromWeekDay.Value - 1; i < ToWeekDay.Value; i++)
                {
                    if (time.DayOfWeek == (DayOfWeek)i)
                        return true;
                }
                return false;
            }

            for (var j = ToWeekDay.Value; j < FromWeekDay.Value - 1; j++)
            {
                if (time.DayOfWeek == (DayOfWeek)j)
                    return false;
            }
            return true;
        }

        private bool IsTimeMatch(DateTime time)
        {
            if (FromTime == null || ToTime == null)
                return true;

            var fromTimeValue = new DateTime(1900, 1, 1, int.Parse(FromTime.Split(':')[0]), int.Parse(FromTime.Split(':')[1]),0,0);
            var toTimeValue = new DateTime(1900, 1, 1, int.Parse(ToTime.Split(':')[0]), int.Parse(ToTime.Split(':')[1]), 0, 0);
            if (fromTimeValue <= toTimeValue)
            {
                if (time.TimeOfDay >= fromTimeValue.TimeOfDay && time.TimeOfDay <= toTimeValue.TimeOfDay)
                    return true;

                return false;
            }

            if (time.TimeOfDay > fromTimeValue.TimeOfDay && time.TimeOfDay < toTimeValue.TimeOfDay)
                return false;

            return true;
        }

        public void UpdateNullableFields()
        {
            if (FromWeekDay == 0) FromWeekDay = null;
            if (ToWeekDay == 0) ToWeekDay = null;
        }

        public PhoneCallWorkflow()
        {
            
        }

    }
}
      