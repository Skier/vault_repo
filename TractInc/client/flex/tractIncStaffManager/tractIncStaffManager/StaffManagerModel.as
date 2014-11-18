package tractIncStaffManager
{
    import tractInc.domain.packages.StaffManagerPackage;
    
    [Bindable]
    public class StaffManagerModel
    {
        public var isBusy:Boolean = false;
        
        public var staffManagerPackage:StaffManagerPackage = null;
    }
}