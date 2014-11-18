using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.LeadCentral.Data;

namespace Dalworth.LeadCentral.Domain
{
    public partial class User
    {
        public ServmanCustomer RelatedCustomer { get; set; }

        public User()
        {

        }

        private const String SqlSelectByQbUserId = "Select * From User Where QbUserId = ?UserId; ";

        public static User GetByQbUserId(string userId, IDbConnection connection)
        {

            User result = null;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByQbUserId, connection))
            {
                Database.PutParameter(dbCommand, "?UserId", userId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        result = Load(dataReader);
                }
            }

            return result;
        }

        public static List<User> GetAll(IDbConnection connection)
        {
            return Find(connection);
        }

        public bool IsBusinessPartner()
        {
            return RoleName == UserRoleEnum.BusinessPartner.ToString();
        }

        public bool IsAdmin()
        {
            return RoleName == UserRoleEnum.Administrator.ToString();
        }
    }

    public enum UserRoleEnum
    {
        Administrator,
        Staff,
        BusinessPartner
    }
}
      