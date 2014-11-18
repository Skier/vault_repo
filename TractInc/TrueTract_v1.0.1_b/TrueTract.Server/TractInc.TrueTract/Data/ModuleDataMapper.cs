using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TractInc.TrueTract.Entity;

namespace TractInc.TrueTract.Data
{
    internal class ModuleDataMapper
    {
        private const string SQL_SELECT_BY_USER_ID =
            @"
            select distinct m.moduleId, m.description
              from userrole ur
                join PermissionAssignment pa on pa.RoleId = ur.roleId
                join Permission p on p.PermissionId = pa.PermissionId 
	            join Module m on m.ModuleId = p.ModuleId
            WHERE ur.UserId = {0} 
        ";

        private const string SQL_UPDATE =
            @"
            UPDATE [Modulle] set 
                Description = '{0}' 
            WHERE ModuleId = {1}
        ";

        private const string SQL_CREATE =
            @"
            INSERT INTO [Module] (
                Description
                ) 
            VALUES ( '{0}' )

            select scope_identity();
        ";

        public List<ModuleInfo> GetModuleListByUserId(int userId)
        {
            string sql = String.Format(SQL_SELECT_BY_USER_ID, userId);

            List<ModuleInfo> result = new List<ModuleInfo>();
            
            using (
                SqlDataReader dataReader = SQLHelper.ExecuteReader(CommandType.Text, sql, null))
            {
                while (dataReader.Read())
                {
                    int moduleId = dataReader.GetInt32(0);
                    string description = dataReader.GetString(1);
                    
                    result.Add(new ModuleInfo(moduleId, description));
                }
            }

            return result;
        }

        public ModuleInfo Create(SqlTransaction tran, ModuleInfo moduleInfo)
        {
            string sql = String.Format(SQL_CREATE,
                                       moduleInfo.Description);

            moduleInfo.ModuleId =
                int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, sql, null).ToString());

            return moduleInfo;
        }

        public void Update(SqlTransaction tran, ModuleInfo moduleInfo)
        {
            string sql =
                String.Format(SQL_UPDATE, 
                              moduleInfo.Description, moduleInfo.ModuleId);

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, sql, null);
        }
    }
}