using System;
using System.Data;
using Servman.Data;

namespace Servman.Domain
{
    public partial class Owner
    {
        public Owner()
        {}

        public static string OwnerRoleName = "Owner";

        public User RelatedUser { set; get; }

        private const String SqlSelectByUserId = "Select * From Owner Where UserId = ?UserId; ";

        public static Owner GetByUserId(int userId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByUserId, connection))
            {
                Database.PutParameter(dbCommand, "?UserId", userId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }
    }
}