package tractIncContractManager
{
    import tractInc.domain.packages.ContractManagerPackage;
    
    [Bindable]
    public class ContractManagerModel
    {
        public var isBusy:Boolean = false;
        
        public var contractManagerPackage:ContractManagerPackage = null;
    }
}