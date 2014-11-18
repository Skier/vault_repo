using System;
using System.Data;
using System.Collections.Generic;
using Dalworth.Server.Data;
using Dalworth.Server.SDK;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
  
namespace Dalworth.Server.Domain
{
    public partial class WorkTransactionQbInvoice
    {
        public WorkTransactionQbInvoice()
        {

        }

        #region FindByWorkTransactionId

        private const String SqlSelectByWorkTransactionId = SqlSelectAll +
            " where WorkTransactionId = ?WorkTransactionId";

        public static WorkTransactionQbInvoice FindByWorkTransactionId(
            int workTransactionId, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByWorkTransactionId, connection))
            {

                Database.PutParameter(dbCommand, "?WorkTransactionId", workTransactionId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("WorkTransactionQbInvoice not found, search by primary key");

        }

        
        #endregion
    }
}
      