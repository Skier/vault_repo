package tractInc.domain.packages
{
    import TractInc.Domain.Person;
    import TractInc.Domain.User;
    import TractInc.Domain.UserPreference;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Package.UserProfile.UserProfilePackage")]
    public class UserProfilePackage
    {
        public var person:Person;
        public var user:User;
        public var userPreference:UserPreference;

        public var canView:Boolean;
        public var canEdit:Boolean;
    }
}