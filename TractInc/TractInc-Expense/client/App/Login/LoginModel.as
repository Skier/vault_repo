package App.Login
{
	import App.Entity.UserDataObject;
	
	[Bindable]
	public class LoginModel
	{

		public var user:UserDataObject = new UserDataObject();
		public var attempts:int = 4;
	
	}
}