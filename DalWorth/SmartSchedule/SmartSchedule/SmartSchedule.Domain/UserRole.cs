using System;
using System.Runtime.Serialization;

namespace SmartSchedule.Domain
{
    [DataContract]
    public enum UserRoleEnum
    {
        [EnumMember]
        Anonymous = 1,
        [EnumMember]
        Dispatrcher = 2,
        [EnumMember]
        Supervisor = 3,
        [EnumMember]
        Administrator = 4
    }

    public partial class UserRole
    {
        public UserRole(){}
    }
}
      