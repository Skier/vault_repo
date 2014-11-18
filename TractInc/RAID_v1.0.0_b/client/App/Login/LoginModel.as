package App.Login
{
	import App.Domain.User;
	
	[Bindable]
	public class LoginModel
	{

		public var user:User = new User();
		public var attempts:int = 4;
	
	}
}