using System;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Servman.Domain
{
    public partial class CustomerCounter
    {
        public CustomerCounter(){}        

        #region FindNextCustomerIdAndIncrement

        private const String SqlLockCustId
            = @"SELECT RLOCK('1', 'cust_id') FROM cust_id";

        private const String SqlFindCustId
            = @"SELECT * from cust_id";

        private const String SqlIncrementAndSave
            = @"update cust_id set cust_id.cust_id = ? where cust_id.cust_id = ?";

        public static string FindNextCustomerIdAndIncrement()
        {
            using (IDbConnection servmanConnection = Connection.Instance.GetTemporaryDbConnection(ConnectionKeyEnum.Servman))
            {
                servmanConnection.Open();

                bool isCustomerLocked = false;
                while (!isCustomerLocked)
                {
                    using (IDbCommand dbCommand = Database.PrepareCommand(SqlLockCustId, servmanConnection))
                    {
                        isCustomerLocked = (bool)dbCommand.ExecuteScalar();
                    }
                }

                string custIdString;
                using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindCustId, servmanConnection))
                {
                    custIdString = (string)dbCommand.ExecuteScalar();
                }

                int custId = int.Parse(custIdString);

                using (IDbCommand dbCommand = Database.PrepareCommand(SqlIncrementAndSave, servmanConnection))
                {
                    Database.PutParameter(dbCommand, "@custid", (custId + 1).ToString("000000"));
                    Database.PutParameter(dbCommand, "@custid", custIdString);

                    dbCommand.ExecuteNonQuery();
                }

                return custIdString;
            }
        }

        #endregion
    }
}
      