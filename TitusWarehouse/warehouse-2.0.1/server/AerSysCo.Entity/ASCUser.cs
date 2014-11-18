namespace AerSysCo.Entity
{
    public class ASCUser : Traceable
    {
        public int userId = 0;
		public int userTypeId = 0;
        public int brandId = 0;
		public string login = "";
		public string password = "";

        public Brand brand;
        public UserType userType;
    }
}
