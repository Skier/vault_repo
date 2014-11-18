namespace TractInc.TrueTract.Entity
{
    class UserRoleInfo
    {
        public int UserRoleId;
        public int UserId;
        public int RoleId;

        public UserRoleInfo() {
        }

        public UserRoleInfo(int userId, int roleId) {
            UserId = userId;
            RoleId = roleId;
        }

        public UserRoleInfo(int userRoleId, int userId, int roleId)
            : this(userId, roleId) {
            
            UserRoleId = userRoleId;

        }
    }
}
