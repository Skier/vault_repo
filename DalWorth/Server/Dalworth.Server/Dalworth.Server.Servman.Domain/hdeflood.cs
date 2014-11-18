using System;
using System.Data;
using Dalworth.Server.Data;

namespace Dalworth.Server.Servman.Domain
{
    public partial class hdeflood
    {
        public hdeflood(){ }

        #region FindByPrimaryKey

        public static hdeflood FindByPrimaryKey(String ticket_num, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
            {
                Database.PutParameter(dbCommand, "@ticket_num", ticket_num);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }
            throw new DataNotFoundException("hdeflood not found, search by primary key");
        }

        #endregion
    }
}
      