using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class ClientDataMapper
    {

        #region Constants

        //optimized
        private const string SQL_GET_BY_USERID = @"
            SELECT [Client].ClientId,
                   [Client].ClientName as [Name]
              FROM [Client]
             WHERE [Client].ClientId in (
                SELECT [Contract].ClientId 
                  FROM [Contract]
                    inner join Project on Project.ContractId = Contract.ContractId
                    inner join TeamAssignment ta on Project.ProjectId=ta.ProjectId
                    inner join TeamMember tm on ta.TeamId = tm.TeamId         
                    join [Asset] on [Asset].AssetId = tm.AssetId
                    join [User] on [User].PersonId = [Asset].PersonId
                 WHERE [User].UserId = @UserId
                  AND tm.StartDate < getDate()
                  AND (tm.EndDate is null or tm.EndDate > getDate())
                 GROUP BY [Contract].ClientId )
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
