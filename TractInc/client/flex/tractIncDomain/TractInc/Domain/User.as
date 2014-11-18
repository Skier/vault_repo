package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.User")]
    public class User
    {
        public var UserId:int;      
        public var PersonId:int;      
        public var Login:String;
        public var Password:String;
        public var IsActive:Boolean;
        public var HackingAttempts:int;

        public var Personal:Person;
        public var Preference:UserPreference;
/*
        private var _roles:ArrayCollection = new ArrayCollection();
        public function get Roles():ArrayCollection { return _roles; }
*/    
        private var _roleList:Array;
        public function get RoleList():Array { return _roleList; }
        public function set RoleList(value:Array):void 
        { 
            _roleList = value;
        }
    }
}