using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Servman.Domain
{
    public partial class techschd
    {
        public techschd(){ }

        #region FindByPrimaryKey overloaded

        public static techschd FindByPrimaryKey(String tech_id, DateTime date, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
                Database.PutParameter(dbCommand, "@tech_id", tech_id);
                Database.PutParameter(dbCommand, "@date", date);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            throw new DataNotFoundException("techschd not found, search by primary key");
        }

        #endregion

    }
}
