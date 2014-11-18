package tractIncUserManager
{
    import tractInc.domain.packages.UserManagerPackage;
    
    [Bindable]
    public class UserManagerModel
    {
        public var isBusy:Boolean = false;
        public var userManagerPackage:UserManagerPackage = null;
        
        public function init(up:UserManagerPackage):void {
            userManagerPackage = up;
        }
        
    }
}