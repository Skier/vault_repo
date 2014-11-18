using System;
using System.Data;
using Servman.Data;


namespace Servman.Domain
{
    public partial class CustomerServiceRep
    {
        public static string CustomerServiceRepRoleName = "CustomerServiceRep";

        public User RelatedUser { set; get; }

        public CustomerServiceRep()
        {
            
        }

        private const String SqlSelectByUserId = "Select * From CustomerServiceRep Where UserId = ?UserId; ";

        public static CustomerServiceRep GetByUserId(int userId, IDbConnection connection)
        {
            using (var dbCommand = Database.PrepareCommand(SqlSelectByUserId, connection))
            {
                Database.PutParameter(dbCommand, "?UserId", userId);

                using (var dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }
    }
}
      