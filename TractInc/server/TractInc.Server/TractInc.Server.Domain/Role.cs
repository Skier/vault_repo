using System;
using System.Collections.Generic;
using System.Data;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class Role
{
    private const String SqlSelectByUserId = @"
        SELECT DISTINCT r.* FROM [Role] r 
        INNER JOIN UserRole ur ON ur.RoleId = r.RoleId
        WHERE ur.UserId = @UserId";

    public List<Permission> PermissionList = null;

    public Role()
    {
    }

    public static List<Role> findByUser(User user)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByUserId))
        {
            Database.PutParameter(dbCommand, "@UserId", user.UserId);

            List<Role> result = new List<Role>();

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

    public static void Save(Role role) {
        Database.Begin();

        try
        {
            if ( 0 != role.RoleId ) {
                Role.Update(role);
                PermissionAssignment.DeleteByRole(role);
            } else {
                Role.Insert(role);
            }

            foreach (Permission perm in role.PermissionList) {
                PermissionAssignment pa = new PermissionAssignment();
                pa.RoleId = role.RoleId;
                pa.PermissionId = perm.PermissionId;
                PermissionAssignment.Insert(pa);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        
        Database.Commit();
    }

    public static void Remove(Role role) {
        Database.Begin();

        try
        {
            Role.Delete(role);
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        
        Database.Commit();
    }

}
}
      