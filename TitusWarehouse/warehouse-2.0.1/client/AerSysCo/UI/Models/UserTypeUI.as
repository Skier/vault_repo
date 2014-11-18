package AerSysCo.UI.Models
{
	import AerSysCo.Server.UserType;
	
	[Bindable]
	public class UserTypeUI
	{
		public var userTypeId:int;
		public var userTypeName:String;
		
		public function populateFromUserType(value:UserType):void 
		{
			this.userTypeId = value.userTypeId;
			this.userTypeName = value.userTypeName;
		}
		
		public function toUserType():UserType 
		{
			var result:UserType = new UserType();
			result.userTypeId = this.userTypeId;
			result.userTypeName = this.userTypeName;
			
			return result;
		}
	}
}