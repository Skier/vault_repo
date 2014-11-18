package tractInc.domain.packages
{
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Package.ContractManager.ContractManagerPackage")]
    public class ContractManagerPackage
    {
        private var _contractStatusList:Array;
        
        public function get ContractStatusList():Array 
        { 
            return _contractStatusList; 
        }
        
        public function set ContractStatusList(value:Array):void 
        { 
            _contractStatusList = value;
        }

        private var _invoiceItemTypeList:Array;
        
        public function get InvoiceItemTypeList():Array 
        { 
            return _invoiceItemTypeList; 
        }
        
        public function set InvoiceItemTypeList(value:Array):void 
        { 
            _invoiceItemTypeList = value;
        }

        private var _accountTypeList:Array;
        
        public function get AccountTypeList():Array 
        { 
            return _accountTypeList; 
        }
        
        public function set AccountTypeList(value:Array):void 
        { 
            _accountTypeList = value;
        }

        private var _companyList:Array;
        
        public function get CompanyList():Array 
        { 
            return _companyList; 
        }
        
        public function set CompanyList(value:Array):void 
        { 
            _companyList = value;
        }

        private var _clientList:Array;
        
        public function get ClientList():Array 
        { 
            return _clientList; 
        }
        
        public function set ClientList(value:Array):void 
        { 
            _clientList = value;
        }

        private var _accountList:Array;
        
        public function get AccountList():Array 
        { 
            return _accountList; 
        }
        
        public function set AccountList(value:Array):void 
        { 
            _accountList = value;
        }

        private var _contractList:Array;
        
        public function get ContractList():Array 
        { 
            return _contractList; 
        }
        
        public function set ContractList(value:Array):void 
        { 
            _contractList = value;
        }

        public var ActAsCompany:Boolean;
    }
}