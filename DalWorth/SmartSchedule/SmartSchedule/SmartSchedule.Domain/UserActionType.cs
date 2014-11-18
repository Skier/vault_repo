using System;
using System.Runtime.Serialization;

namespace SmartSchedule.Domain
{
    [DataContract]
    public enum UserActionTypeEnum
    {
        [EnumMember]
        BucketResolve = 1,
        [EnumMember]
        TechnicianSettingsEditDaily = 2,
        [EnumMember]
        TechnicianSettingsEditDefault = 3,
        [EnumMember]
        Visit = 4,
        [EnumMember]
        Blockout = 6,
        [EnumMember]
        SuspendRecommendationsModify = 8
    }

    public partial class UserActionType
    {
        public UserActionType(){}

        #region GetText

        public static string GetText(UserActionTypeEnum actionType)
        {
            if (actionType == UserActionTypeEnum.BucketResolve)
                return "Bucket Resolve";
            if (actionType == UserActionTypeEnum.TechnicianSettingsEditDaily)
                return "Technician Daily Settings";
            if (actionType == UserActionTypeEnum.TechnicianSettingsEditDefault)
                return "Technician Default Settings";
            if (actionType == UserActionTypeEnum.Visit)
                return "Visit";
            if (actionType == UserActionTypeEnum.Blockout)
                return "Blockout";
            if (actionType == UserActionTypeEnum.SuspendRecommendationsModify)
                return "Suspend Recommendations";
            throw new Exception("Action text is not defined");
        }

        #endregion
    }
}
      