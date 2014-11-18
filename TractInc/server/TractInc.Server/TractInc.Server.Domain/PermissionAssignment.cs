using System;
using System.Data;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class PermissionAssignment
{
    private const string SQL_DELETE_BY_ROLE_ID =
        @"delete from PermissionAssignment where RoleId={0}";
        
    private const string SQL_SELECT_BY_ROLE_AND_PERMISSION_ID =
        @"
        SELECT PermissionAssignmentId
          FROM PermissionAssignment
         WHERE RoleId={0} and PermissionId = {1}";
        
    public PermissionAssignment()
    {
    }

    public static void DeleteByRole(Role role) 
    {
        string sql = String.Format(PermissionAssignment.SQL_DELETE_BY_ROLE_ID, role.RoleId);
        Database.PrepareCommand(sql).ExecuteNonQuery();
    }

    public static bool HasPermission(Role role, int permissionId)
    {
        string sql = String.Format(PermissionAssignment.SQL_SELECT_BY_ROLE_AND_PERMISSION_ID, role.RoleId, permissionId);

        using(IDataReader dataReader = Database.PrepareCommand(sql).ExecuteReader()) {
            if (dataReader.Read()) {
                return true;
            } else {
                return false;
            }
        }
    }
        

}
}
      