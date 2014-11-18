using System;
using System.Collections.Generic;
using System.Data;
using Servman.Data;

namespace Servman.Domain
{
    public partial class User
    {
        public static string AdministartorRoleName = "Administrator";
        public static string StaffRoleName = "Staff";
        public static string BusinessPartnerRoleName = "BusinessPartner";

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
    }
}
      