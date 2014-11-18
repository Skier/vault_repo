using System;
using System.Data;
using Servman.Data;


namespace Servman.Domain
{
    public partial class PhoneToSalesRep
    {
        public PhoneToSalesRep()
        {
            
        }

        private const String SqlDeleteBySalesRepId = "Delete From PhoneToSalesRep Where SalesRepId = ?SalesRepId ";

        public static void RemoveBySalesRepId(int salesRepId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteBySalesRepId, connection))
            {
                Database.PutParameter(dbCommand, "?SalesRepId", salesRepId);

                dbCommand.ExecuteNonQuery();
            }
        }
    }
}
