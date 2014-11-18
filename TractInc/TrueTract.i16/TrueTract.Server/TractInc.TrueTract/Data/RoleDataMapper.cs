using System;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    class RoleDataMapper
    {
        private const string SQL_SELECT_BY_NAME =
            @"
            SELECT * 
              FROM Role 
			 WHERE [Name] = {0} 
        ";

        private const string SQL_UPDATE =
            @"
            UPDATE [Role] set 
                [Name] = '{0}' 
            WHERE RoleId = {1}
        ";

        private const string SQL_CREATE =
            @"
            INSERT INTO [Role] (
                [Name]
                ) 
            VALUES ( '{0}' )

            select scope_identity();
        ";

        public RoleInfo GetRoleByName(SqlTransaction tran, string name)
        {
            string sql = String.Format(SQL_SELECT_BY_NAME, name);

            RoleInfo roleInfo = null;
            
            using (
                SqlDataReader dataReader =
                    SQLHelper.ExecuteReader(tran, CommandType.Text, sql, null))
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
            string sql = String.Format(SQL_CREATE,
                                       roleInfo.Name);

            roleInfo.RoleId =
                int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            return roleInfo;
        }

        public void Update(SqlTransaction tran, RoleInfo roleInfo)
        {
            string sql =
                String.Format(SQL_UPDATE,
                              roleInfo.Name, roleInfo.RoleId);

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }
    }
}
