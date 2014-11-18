package App.Entity
{
	
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Entity.UserDataObject")]
	public class UserDataObject
	{
		
        public var UserId:int;

        public var Login:String;

        public var Password:String;

        public var Email:String;

        public var IsActive:Boolean;

        public var HackingAttempts:int;
        
        public var Deleted:Boolean;
		
	}
	
}
