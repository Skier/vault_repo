package src.login
{
	import src.deedplotter.domain.User;
	
	[Bindable]
	public class LoginModel
	{

		public var user:User = new User();
		public var attempts:int = 4;
	
	}
}