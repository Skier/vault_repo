package tractIncClientManager
{
    import tractInc.domain.packages.ClientManagerPackage;
    
    [Bindable]
    public class ClientManagerModel
    {
        public var isBusy:Boolean = false;
        
        public var clientManagerPackage:ClientManagerPackage = null;
        public var clientList:Array = null;
        public var companyList:Array = null;
        public var contractList:Array = null;
    }
}