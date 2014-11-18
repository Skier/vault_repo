using System;
using System.Data;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class UserRole
{
    public const int INITIAL_USER_ROLE = 1;

    private const string SQL_DELETE_BY_USER_ID =
        @"delete from UserRole where UserId={0}";
        
    private const string SQL_CREATE =
        @"
        INSERT INTO [UserRole] (
            UserId,
            RoleId
            ) 
        VALUES ( {0}, {1})

        select scope_identity();
    ";

    public UserRole()
    {
    }

    public static void DeleteByUser(User user) 
    {
        string sql = String.Format(UserRole.SQL_DELETE_BY_USER_ID, user.UserId);
        Database.PrepareCommand(sql).ExecuteNonQuery();
    }

    public void Create()
    {
        string sql = String.Format(SQL_CREATE,
                                   UserId, RoleId);

        UserId =
            int.Parse(Database.PrepareCommand(sql).ExecuteScalar().ToString());
    }

}
}
      