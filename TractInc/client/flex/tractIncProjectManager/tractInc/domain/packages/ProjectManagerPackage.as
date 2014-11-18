package tractInc.domain.packages
{
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Package.ProjectManager.ProjectManagerPackage")]
    public class ProjectManagerPackage
    {
        private var _projectStatusList:Array;
        
        public function get ProjectStatusList():Array 
        { 
            return _projectStatusList; 
        }
        
        public function set ProjectStatusList(value:Array):void 
        { 
            _projectStatusList = value;
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

        private var _contractList:Array;
        
        public function get ContractList():Array 
        { 
            return _contractList; 
        }
        
        public function set ContractList(value:Array):void 
        { 
            _contractList = value;
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

        private var _projectList:Array;
        
        public function get ProjectList():Array 
        { 
            return _projectList; 
        }
        
        public function set ProjectList(value:Array):void 
        { 
            _projectList = value;
        }

        public var CanAssignAccount:Boolean;
    }
}