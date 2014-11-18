using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace TractInc.TrueTract.Data
{
    internal class RoleDataMapper
    {
        private const string SQL_SELECT_BY_NAME =
            @"
            SELECT * 
              FROM Role 
			 WHERE [Name] = @Name 
        ";

        private const string SQL_UPDATE =
            @"
            UPDATE [Role] set 
                [Name] = @Name 
            WHERE RoleId = @RoleId
        ";

        private const string SQL_CREATE =
            @"
            INSERT INTO [Role] (
                [Name]
                ) 
            VALUES ( @Name )

            select scope_identity();
        ";

        public RoleInfo GetRoleByName(SqlTransaction tran, string name)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Name", name));

            RoleInfo roleInfo = null;
            
            using (
                SqlDataReader dataReader =
                    SQLHelper.ExecuteReader(tran, CommandType.Text, SQL_SELECT_BY_NAME, paramList.ToArray()))
            {
                if (dataReader.Read())
                {
                    int roleId = dataReader.GetInt32(0);
                    roleInfo = new RoleInfo(roleId, name);
                }
            }

            return roleInfo;
        }

        public RoleInfo Create(SqlTransaction tran, RoleInfo roleInfo)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Name", roleInfo.Name));

            roleInfo.RoleId =
                int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());

            return roleInfo;
        }

        public void Update(SqlTransaction tran, RoleInfo roleInfo)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Name", roleInfo.Name));
            paramList.Add(new SqlParameter("@RoleId", roleInfo.RoleId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());
        }
    }
}
