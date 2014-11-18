package TractInc.Domain
{
    import mx.collections.ArrayCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.UserPreference")]
    public class UserPreference
    {
        public var UserPreferenceId:int;      
        public var UserId:int;      
        public var DefaultSite:String;
        public var NewTracts:int;
    }
}