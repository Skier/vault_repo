using System;
using System.Collections.Generic;
using System.Data;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class Module
{
    private const String SqlSelectByUserId = @"
        SELECT DISTINCT m.ModuleId, m.ShortName, m.Description, m.Url, m.ModuleTypeId 
        FROM [Module] m 
            INNER JOIN Permission p ON p.ModuleId = m.ModuleId 
            INNER JOIN PermissionAssignment pa ON pa.PermissionId = p.PermissionId
            INNER JOIN Role r ON pa.RoleId = r.RoleId
            INNER JOIN UserRole ur ON ur.RoleId = r.RoleId
        WHERE ur.UserId = @UserId 
        ";

    public List<Permission> PermissionList = null;

    public Module()
    {
    }
        
    public static List<Module> findByUser(User user)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByUserId))
        {
            Database.PutParameter(dbCommand,"@UserId", user.UserId);
            
            List<Module> result = new List<Module>();
            
            using(IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while(dataReader.Read())
                {
                    result.Add(Load(dataReader));
                }
            }
            return result;
        }
    }

    public static List<Module> findAll(bool populate)
    {
        List<Module> result = Module.Find();
        if ( populate ) {
            foreach(Module m in result) {
                m.PermissionList = Permission.findByModule(m);
            }
        }
        return result;
    }
}
}
      