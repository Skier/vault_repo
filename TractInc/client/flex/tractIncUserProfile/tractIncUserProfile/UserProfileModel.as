package tractIncUserProfile
{
    import tractInc.domain.packages.UserProfilePackage;
    
    [Bindable]
    public class UserProfileModel
    {
        public var isBusy:Boolean = false;
        public var userProfilePackage:UserProfilePackage = null;
        
        public function init(up:UserProfilePackage):void {
            userProfilePackage = up;
        }
        
    }
}