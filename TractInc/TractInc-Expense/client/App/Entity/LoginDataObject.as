package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.LoginDataObject")]
	public class LoginDataObject
	{
		
        public var UserInfo:UserDataObject;

        public var AssetInfo:AssetDataObject;

        public var UserRoleInfo:UserRoleDataObject;

        public var UserAssetInfo:UserAssetDataObject;
        
        public var AFEs:Array;

        public var Projects:Array;

	}
	
}
