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
            WHERE ur.UserId = @UserId 
        ";

        private const string SQL_UPDATE =
            @"
            UPDATE [Modulle] set 
                Description = @Description 
            WHERE ModuleId = @ModuleId
        ";

        private const string SQL_CREATE =
            @"
            INSERT INTO [Module] (
                Description
                ) 
            VALUES ( Description )

            select scope_identity();
        ";

        public List<ModuleInfo> GetModuleListByUserId(int userId)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@UserId", userId));

            List<ModuleInfo> result = new List<ModuleInfo>();
            
            using (
                SqlDataReader dataReader = SQLHelper.ExecuteReader(CommandType.Text, SQL_SELECT_BY_USER_ID, paramList.ToArray()))
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
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Description", moduleInfo.Description));

            moduleInfo.ModuleId =
                int.Parse(SQLHelper.ExecuteScalar(tran, CommandType.Text, SQL_CREATE, paramList.ToArray()).ToString());

            return moduleInfo;
        }

        public void Update(SqlTransaction tran, ModuleInfo moduleInfo)
        {
            List<SqlParameter> paramList = new List<SqlParameter>();

            paramList.Add(new SqlParameter("@Description", moduleInfo.Description));
            paramList.Add(new SqlParameter("@ModuleId", moduleInfo.ModuleId));

            SQLHelper.ExecuteNonQuery(tran, CommandType.Text, SQL_UPDATE, paramList.ToArray());
        }
    }
}