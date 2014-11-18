package tractInc.domain.packages
{
    [Bindable]
    [RemoteClass(alias="TractInc.Server.Domain.Package.UserManager.UserManagerPackage")]
    public class UserManagerPackage
    {
        private var _roleList:Array;
        
        public function get RoleList():Array 
        { 
            return _roleList; 
        }
        
        public function set RoleList(value:Array):void 
        { 
            _roleList = value;
        }
        
/*        
        private var _permissionList:Array;
        
        public function get PermissionList():Array 
        { 
            return _permissionList; 
        }
        
        public function set PermissionList(value:Array):void 
        { 
            _permissionList = value;
        }
*/
        
        private var _moduleList:Array;
        
        public function get ModuleList():Array 
        { 
            return _moduleList; 
        }
        
        public function set ModuleList(value:Array):void 
        { 
            _moduleList = value;
        }
        
        private var _userList:Array;
        
        public function get UserList():Array 
        { 
            return _userList; 
        }
        
        public function set UserList(value:Array):void 
        { 
            _userList = value;
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
        
        private var _companyList:Array;
        
        public function get CompanyList():Array 
        { 
            return _companyList; 
        }
        
        public function set CompanyList(value:Array):void 
        { 
            _companyList = value;
        }
        
        public var canManageUsers:Boolean;
        public var canManageRoles:Boolean;
        
    }
}