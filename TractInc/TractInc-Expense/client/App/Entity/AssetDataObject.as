package App.Entity
{
	
	import App.Domain.User;
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.AssetDataObject")]
	public class AssetDataObject
	{
		
        public var AssetId:int;

        public var Type:String;

        public var ChiefAssetId:int;

        public var BusinessName:String;

        public var FirstName:String;

        public var MiddleName:String;

        public var LastName:String;

        public var SSN:String;

        public var Deleted:Boolean;

        public var Bills:Array;
        
        public var UserInfo:UserDataObject;
        
        public var DefaultRates:Array;
        
        public var Assignments:Array;

        public var HasActiveProjects:Boolean;

        public var HasActiveLandmans:Boolean;

	}
	
}
