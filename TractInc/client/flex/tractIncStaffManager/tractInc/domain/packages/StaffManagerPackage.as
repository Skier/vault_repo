package tractInc.domain.packages
{
    import TractInc.Domain.Company;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Package.StaffManager.StaffManagerPackage")]
    public class StaffManagerPackage
    {
        private var _assetTypeList:Array;
        
        public function get AssetTypeList():Array 
        { 
            return _assetTypeList; 
        }
        
        public function set AssetTypeList(value:Array):void 
        { 
            _assetTypeList = value;
        }

        private var _billItemTypeList:Array;
        
        public function get BillItemTypeList():Array 
        { 
            return _billItemTypeList; 
        }
        
        public function set BillItemTypeList(value:Array):void 
        { 
            _billItemTypeList = value;
        }

        private var _teamList:Array;
        
        public function get TeamList():Array 
        { 
            return _teamList; 
        }
        
        public function set TeamList(value:Array):void 
        { 
            _teamList = value;
        }

        private var _personList:Array;
        
        public function get PersonList():Array 
        { 
            return _personList; 
        }
        
        public function set PersonList(value:Array):void 
        { 
            _personList = value;
        }

        private var _assetList:Array;
        
        public function get AssetList():Array 
        { 
            return _assetList; 
        }
        
        public function set AssetList(value:Array):void 
        { 
            _assetList = value;
        }

        private var _projectList:Array;
        
        public function get ProjectList():Array 
        { 
            return _projectList; 
        }
        
        public function set ProjectList(value:Array):void 
        { 
            _projectList = value;
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

        private var _clientList:Array;
        
        public function get ClientList():Array 
        { 
            return _clientList; 
        }
        
        public function set ClientList(value:Array):void 
        { 
            _clientList = value;
        }

        public var StaffCompany:Company;
    }
}
