using System;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Servman.Domain
{
    public partial class page_svc
    {
        public page_svc(){}

        #region FindByPrimaryKey

        public static page_svc FindByPrimaryKey(int serv_id, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
                Database.PutParameter(dbCommand, "@serv_id", serv_id);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }

            }

            throw new DataNotFoundException("page_svc not found, search by primary key");
        }

        #endregion
    }
}
      