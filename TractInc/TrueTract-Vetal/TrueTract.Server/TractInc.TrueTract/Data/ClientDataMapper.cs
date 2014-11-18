using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class ClientDataMapper
    {

        #region Constants

        private const string SQL_GET_BY_USERID = @"
            SELECT DISTINCT [Client].*
              FROM [Client]
                join [Project] on [Project].ClientId = [Client].ClientId
                join [AssetAssignment] on [AssetAssignment].ProjectId = [Project].ProjectId
                join [User] on [User].AssetId = [AssetAssignment].AssetId
             WHERE [User].UserId = @UserId
        ";

        #endregion

        #region Methods

        public List<ClientInfo> GetByUser(SqlTransaction tran, int userId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@UserId", userId));
            
            return Select(tran, SQL_GET_BY_USERID, paramList);
        }

        private List<ClientInfo> Select(SqlTransaction tran, string sql, List<SqlParameter> paramList)
        {
            List<ClientInfo> result = new List<ClientInfo>();
            
            using (SqlDataReader dataReader = SQLHelper.ExecuteReader(tran, CommandType.Text, sql, paramList.ToArray()))
            {
                while (dataReader.Read())
                {
                    ClientInfo info = new ClientInfo();
                    info.ClientId = dataReader.GetInt32(dataReader.GetOrdinal("ClientId"));
                    info.Name = dataReader.GetSqlString(dataReader.GetOrdinal("Name")).ToString();

                    result.Add(info);
                }
            }

            return result;
        }
        
        #endregion

    }
}
