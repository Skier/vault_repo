namespace TractInc.TrueTract.Data
{
    class RoleInfo
    {
        public static int ROLE_USER = 2;
        
        public int RoleId;
        public string Name;

        public RoleInfo()
        {
        }

        public RoleInfo(string name)
        {
            Name = name;
        }

        public RoleInfo(int roleId, string name)
            : this(name)
        {

            RoleId = roleId;

        }
    }
}
