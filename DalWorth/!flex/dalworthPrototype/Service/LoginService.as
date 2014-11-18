package Service
{
	import Domain.Employee;
	public class LoginService
	{
		public static function validateEmployee(login:String, password:String):Employee
		{
			return Database.Instance.findEmployeeByLoginAndPassword(login, password);
		}
	}
}