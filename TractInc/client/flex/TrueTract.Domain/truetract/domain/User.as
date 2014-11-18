package truetract.domain
{
    [Bindable]
	[RemoteClass(alias="TractInc.TrueTract.Entity.UserInfo")]
	public class User
	{
        public var UserId:int;      
        public var Login:String;
        public var FirstName:String;
        public var LastName:String;
        public var PhoneNumber:String;
        public var Password:String;
        public var Email:String;
        public var IsActive:Boolean;
        public var DefaultSite:String;
        public var ClientId:int;
	}
}