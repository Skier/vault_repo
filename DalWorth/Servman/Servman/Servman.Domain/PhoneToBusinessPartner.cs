using System;
using System.Data;
using Servman.Data;


namespace Servman.Domain
{
    public partial class PhoneToBusinessPartner
    {
        public PhoneToBusinessPartner()
        {
            
        }

        private const String SqlDeleteByBusinessPartnerId = "Delete From PhoneToBusinessPartner Where BusinessPartnerId = ?BusinessPartnerId ";

        public static void RemoveByBusinessPartnerId(int businessPartnerId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteByBusinessPartnerId, connection))
            {
                Database.PutParameter(dbCommand, "?BusinessPartnerId", businessPartnerId);
                
                dbCommand.ExecuteNonQuery();
            }
        }
    }
}
