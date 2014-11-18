package tractIncUserManager
{
    import flash.events.Event;
	import mx.events.ItemClickEvent;
    import mx.rpc.Responder;
    import mx.rpc.events.ResultEvent;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
	import mx.collections.ArrayCollection;
    
    import TractInc.Domain.Company;
    import TractInc.Domain.Client;
    import TractInc.Domain.User;
    import tractInc.domain.packages.UserManagerPackage;
    import tractInc.domain.storage.IUserManagerStorage;
    import tractInc.domain.storage.UserManagerStorage;
    import TractInc.Domain.Person;
    import TractInc.Domain.Role;
    
    [Bindable]
    public class UserManagerController
    {
        private static var instance:UserManagerController = null;
        
        public static function getInstance():UserManagerController
        {
            return instance;
        }
        
        private var userTab:Object = {label:"System Users", data:"SystemUsers"}; 
        private var roleTab:Object = {label:"System Roles", data:"SystemRoles"}; 
        public var tabData:ArrayCollection = new ArrayCollection();
        
        public var user:User = null;
        public var model:UserManagerModel = null;
        public var storage:IUserManagerStorage = null;
        public var view:UserManagerView = null;
        
        private var roleView:RoleView = null;
        private var userView:UserView = null;
        
        public function UserManagerController():void 
        {
            instance = this;    
        }
        
        public function init(u:User):void 
        {
            user = u;
            model = new UserManagerModel();
            storage = UserManagerStorage.instance;

            tabData.addItem(userTab);
            tabData.addItem(roleTab);
                
            reloadModel();
        }

        private function reloadModel():void
        {
            var responder:Responder = new Responder(
                    getUserManagerPackageResultHandler, 
                    getUserManagerPackageFaultHandler);
            model.isBusy = true;
            storage.getUserManagerPackage(user.UserId, responder);
        }
        
        public function logout():Boolean 
        {
            if (model.isBusy) {
                Alert.show("User profile service is running");
                return false;
            }
            
            if (isDirty) {
                Alert.show("User profile changes not saved");
                return false;
            }
            
            return true;
        }
        
        public function tabChanged(event:ItemClickEvent):void 
        {
            view.tabStack.selectedChild.visible = false;
            view.tabStack.selectedIndex = event.index;
            view.tabStack.selectedChild.visible = true;
        }
        
        public function addUserButtonOnClickHandler(event:Event):void 
        {
            if ( model.userManagerPackage.canManageUsers ) {
                userView = UserView.open(this, null, true);
            } else {
                Alert.show("No permissions to manage Users");
            }
        }
        
        public function searchButtonOnClickHandler(event:Event):void 
        {
            var responder:Responder = new Responder(
                    searchUserResultHandler, 
                    searchUserFaultHandler);

            model.isBusy = true;
            storage.searchUser(this.view.txtLoginFilter.text,
                    this.view.txtFirstNameFilter.text,
                    this.view.txtLastNameFilter.text,
                    (null != this.view.cbRoleFilter.selectedItem 
                            ? (this.view.cbRoleFilter.selectedItem as Role).RoleId
                            : 0),
                    (0 == this.view.cbIsActiveFilter.selectedIndex ? true : false),
                    (null != this.view.cbCompanyFilter.selectedItem 
                            ? (this.view.cbCompanyFilter.selectedItem as Company).CompanyId
                            : 0),
                    (null != this.view.cbClientFilter.selectedItem 
                            ? (this.view.cbClientFilter.selectedItem as Client).ClientId
                            : 0), responder);
        }
        
        public function addRoleButtonOnClickHandler(event:Event):void 
        {
            if ( model.userManagerPackage.canManageRoles ) {
                roleView = RoleView.open(this, null, true);
            } else {
                Alert.show("No permissions to manage Roles");
            }
        }
/*        
        public function roleGridOnClickHandler(event:Event):void 
        {
            trace("UserManagerController.roleGridOnClickHandler: event=" + event);
                var role:Role = model.userManagerPackage.RoleList[view.roleGrid.selectedIndex] as Role;
                openRole(role);    
        }
*/        
        public function saveUser(user:User):void 
        {
            var responder:Responder = new Responder(
                    saveUserResultHandler, 
                    saveUserFaultHandler);

            model.isBusy = true;
            trace("UserManagerController.saveUser: password=" + user.Password);
            storage.saveUser(user, responder);
        }
        
        public function openUser(user:User):void
        {
            trace("UserManagerController.openUser: user=" + user);
            userView = UserView.open(this, user, true);
        }
        
        public function saveRole(role:Role):void 
        {
            var responder:Responder = new Responder(
                    saveRoleResultHandler, 
                    saveRoleFaultHandler);

            model.isBusy = true;
            storage.saveRole(role, responder);
        }
        
        public function openRole(role:Role):void
        {
            trace("UserManagerController.openRole: role=" + role);
            roleView = RoleView.open(this, role, true);
        }
        
        public function removeRole(role:Role):void
        {
            trace("UserManagerController.removeRole: role=" + role);
            var responder:Responder = new Responder(
                    removeRoleResultHandler, 
                    removeRoleFaultHandler);

            model.isBusy = true;
            storage.removeRole(role, responder);
        }
        
        private function get isDirty():Boolean 
        {
            return false;
        }
        
        private function getUserManagerPackageResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.init(event.result as UserManagerPackage);
            view.roleGrid.dataProvider = new ArrayCollection(
                    model.userManagerPackage.RoleList);
            view.userGrid.dataProvider = new ArrayCollection(
                    model.userManagerPackage.UserList);
                    
            var roles:Array = [null];
            for each (var r:Role in this.model.userManagerPackage.RoleList) {
                roles.push(r);
            }
            this.view.cbRoleFilter.dataProvider = roles;
            this.view.cbRoleFilter.labelField = "Name";

            var companies:Array = [null];
            for each (var co:Company in this.model.userManagerPackage.CompanyList) {
                companies.push(co);
            }
            this.view.cbCompanyFilter.dataProvider = companies;
            this.view.cbCompanyFilter.labelField = "CompanyName";

            var clients:Array = [null];
            for each (var cl:Client in this.model.userManagerPackage.ClientList) {
                clients.push(cl);
            }
            this.view.cbClientFilter.dataProvider = clients;
            this.view.cbClientFilter.labelField = "ClientName";
        }
        
        private function getUserManagerPackageFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.message);
        }
        
        private function saveUserResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            userView.close();
            reloadModel();
        }
        
        private function saveUserFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
        private function searchUserResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            model.userManagerPackage.UserList = event.result as Array;
            view.userGrid.dataProvider = new ArrayCollection(
                    model.userManagerPackage.UserList);
        }
        
        private function searchUserFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
        private function saveRoleResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            roleView.close();
            reloadModel();
        }
        
        private function saveRoleFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
        private function removeRoleResultHandler(event:ResultEvent):void 
        {
            model.isBusy = false;
            reloadModel();
        }
        
        private function removeRoleFaultHandler(event:FaultEvent):void 
        {
            model.isBusy = false;
            Alert.show(event.fault.faultString);
        }
        
    }
}