package tractInc.domain.packages
{
    import TractInc.Domain.Contract;
    import TractInc.Domain.Client;
    import TractInc.Domain.Company;
    import TractInc.Domain.ContractRate;
    import TractInc.Domain.ClientContact;
    import TractInc.Domain.CompanyContact;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Package.ContractManager.ContractPackage")]
    public class ContractPackage
    {
        private var _contractRateList:Array;
        
        public function get ContractRateList():Array 
        { 
            return _contractRateList; 
        }
        
        public function set ContractRateList(value:Array):void 
        { 
            _contractRateList = value;
        }

        private var _clientContactList:Array;
        
        public function get ClientContactList():Array 
        { 
            return _clientContactList; 
        }
        
        public function set ClientContactList(value:Array):void 
        { 
            _clientContactList = value;
        }

        private var _companyContactList:Array;
        
        public function get CompanyContactList():Array 
        { 
            return _companyContactList; 
        }
        
        public function set CompanyContactList(value:Array):void 
        { 
            _companyContactList = value;
        }

        public var Main:Contract;
        
        public var ContractClient:Client;

        public var ContractCompany:Company;
    }
}