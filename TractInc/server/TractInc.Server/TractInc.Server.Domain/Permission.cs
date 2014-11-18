using System;
using System.Collections.Generic;
using System.Data;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class Permission
{
    private const String SqlSelectByRoleId = @"
        SELECT DISTINCT p.* FROM Permission p 
        INNER JOIN PermissionAssignment pa ON pa.PermissionId = p.PermissionId
        WHERE pa.RoleId = @RoleId";

    private const string SQL_SELECT_BY_MODULE_ID =
        @"select c.*
            from Permission c 
           where c.ModuleId = @ModuleId";
        
    public Permission()
    {
    }

    public static List<Permission> findByModule(Module module)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Permission.SQL_SELECT_BY_MODULE_ID))
        {
            Database.PutParameter(dbCommand, "@ModuleId", module.ModuleId);
            List<Permission> result = new List<Permission>();
            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    result.Add(Load(dataReader));
                }
            }
            return result;
        }
    }

    public static List<Permission> findByRole(Role role)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(Permission.SqlSelectByRoleId))
        {
            Database.PutParameter(dbCommand, "@RoleId", role.RoleId);

            List<Permission> result = new List<Permission>();

            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    result.Add(Load(dataReader));
                }
            }
            return result;
        }
    } 
}
}
      