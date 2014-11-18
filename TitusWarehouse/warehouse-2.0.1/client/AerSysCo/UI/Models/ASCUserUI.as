package AerSysCo.UI.Models
{
	import AerSysCo.Server.ASCUser;
	
	[Bindable]
	public class ASCUserUI
	{
		public var userId:int;
		public var userTypeId:int;
		public var brandId:int;
		public var login:String;
		public var password:String;
		
		public var userType:UserTypeUI;
		public var brand:BrandUI;
		
		public function populateFromASCUser(value:ASCUser):void 
		{
			this.userId = value.userId;
			this.userTypeId = value.userTypeId;
			this.brandId = value.brandId;
			this.login = value.login;
			this.password = value.password;
			
			if (value.userType) 
			{
				this.userType = new UserTypeUI();
				this.userType.populateFromUserType(value.userType);
			}
			
			if (value.brand) 
			{
				this.brand = new BrandUI();
				this.brand.populateFromBrand(value.brand);
			}
		}
		
		public function toASCUser():ASCUser 
		{
			var result:ASCUser = new ASCUser();
			result.userId = this.userId;
			result.userTypeId = this.userTypeId;
			result.brandId = this.brandId;
			result.login = this.login;
			result.password = this.password;
			result.dateCreated = new Date();
			result.lastUpdateDate = new Date();
			
			if (userType) 
				result.userType = userType.toUserType();
			
			if (brand) 
				result.brand = brand.toBrand();
			
			return result;
		}
	}
}