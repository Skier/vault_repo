package src.deedplotter.domain
{
    [Bindable]
	[RemoteClass(alias="TractInc.ScopeScetch.Entity.User")]
	public class User
	{
        public var UserId:int;      
        public var Login:String;
        public var Password:String;
        public var Email:String;
        public var IsActive:Boolean;
        public var NewTracts:int;
	}
}