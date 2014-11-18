package common
{
	import flash.events.EventDispatcher;
	import App.Domain.*;
	import mx.events.CollectionEvent;
	import weborb.data.*;
	import flash.events.Event;
	import mx.collections.ArrayCollection;
	import App.Entity.UserDataObject;
	
	public class PermissionsRegistry extends EventDispatcher
	{
		private static var _instance:PermissionsRegistry;

		private var _user:User;
		private var _userRoles:ArrayCollection;
		private var _roles:ArrayCollection;
		private var _permissionAssignments:ArrayCollection;
		private var _permissions:ArrayCollection;
		private var _modules:ArrayCollection;
		
		private var _sql:String;
		
		public var isLoaded:Boolean = false;

		public function PermissionsRegistry()
		{
            /* if (PermissionsRegistry._instance != null) {
                throw new Error( "Only one PermissionsRegistry instance should be instantiated" ); 
            } */
		}
		
		public static function get instance():PermissionsRegistry 
		{
			if (_instance == null) {
				_instance = new PermissionsRegistry();
			} 
			return _instance;
		}
		
		public function init(user:UserDataObject):void {
			// _user = user;
			// loadUserRoles();
		}
		/*
		private function loadUserRoles():void 
		{
			_sql = "select * from UserRole where UserId = " + _user.UserId.toString();
			_userRoles = ActiveRecords.UserRole.findBySql(_sql);
			
			// _userRoles.addEventListener("loaded", onUserRolesLoaded);
			
			_userRoles.addEventListener("loaded", onModulesLoaded);
		}
		
		private function onUserRolesLoaded(e:*):void 
		{
			_userRoles.removeEventListener("loaded", onUserRolesLoaded);
			_sql = "select r.* from Role r " +
				" inner join UserRole ur on ur.RoleId = r.RoleId " + 
				" where ur.UserId = " + _user.UserId.toString();
			_roles = ActiveRecords.Role.findBySql(_sql);
			_roles.addEventListener("loaded", onRolesLoaded);
		}
		
		private function onRolesLoaded(e:*):void 
		{
			_roles.removeEventListener("loaded", onRolesLoaded);
			_sql = "select pa.* from PermissionAssignment pa " + 
				" inner join Role r on pa.RoleId = r.RoleId " + 
				" inner join UserRole ur on ur.RoleId = r.RoleId " + 
				" where ur.UserId = " + _user.UserId.toString();
			_permissionAssignments = ActiveRecords.PermissionAssignment.findBySql(_sql);
			_permissionAssignments.addEventListener("loaded", onPermissionAssignmentsLoaded);
		}
		
		private function onPermissionAssignmentsLoaded(e:*):void 
		{
			_permissionAssignments.removeEventListener("loaded", onPermissionAssignmentsLoaded);
			_sql = "select p.* from Permission p " + 
				" inner join PermissionAssignment pa on pa.PermissionId = p.PermissionId " + 
				" inner join Role r on pa.RoleId = r.RoleId " + 
				" inner join UserRole ur on ur.RoleId = r.RoleId " + 
				" where ur.UserId = " + _user.UserId.toString();
			_permissions = ActiveRecords.Permission.findBySql(_sql);
			_permissions.addEventListener("loaded", onPermissionsLoaded);
		}
		
		private function onPermissionsLoaded(e:*):void 
		{
			_permissions.removeEventListener("loaded", onPermissionsLoaded);
			_sql = "select m.* from Module m " + 
				" inner join Permission p on p.ModuleId = m.ModuleId " + 
				" inner join PermissionAssignment pa on pa.PermissionId = p.PermissionId " + 
				" inner join Role r on pa.RoleId = r.RoleId " + 
				" inner join UserRole ur on ur.RoleId = r.RoleId " + 
				" where ur.UserId = " + _user.UserId.toString();
			_modules = ActiveRecords.Module.findBySql(_sql);
			_modules.addEventListener("loaded", onModulesLoaded);
		}
		
		private function onModulesLoaded(e:*):void {
			// _modules.removeEventListener("loaded", onModulesLoaded);
			_userRoles.removeEventListener("loaded", onModulesLoaded);
			
			processPermissions();
			isLoaded = true;
			dispatchEvent(new Event("user_permissions_loaded"));
		}
		
		private function processPermissions():void 
		{
			for each (var module:Module in _modules) {
				for each (var permission:Permission in _permissions) {
					if (permission.ModuleId == module.ModuleId) {
						module.RelatedPermission.addItem(permission);
						permission.RelatedModule = module;
					}
					for each (var pa:PermissionAssignment in _permissionAssignments) {
						if (permission.PermissionId == pa.PermissionId) {
							permission.RelatedPermissionAssignment.addItem(pa);
							pa.RelatedPermission = permission;
						}
					}
					permission.RelatedPermissionAssignment.IsLoaded = true;
				}
				module.RelatedPermission.IsLoaded = true;
			}

			for each (var role:Role in _roles) {
				for each (var pass:PermissionAssignment in _permissionAssignments) {
					if (role.RoleId == pass.RoleId) {
						role.RelatedPermissionAssignment.addItem(pass);
						pass.RelatedRole = role;
					}
				}
				role.RelatedPermissionAssignment.IsLoaded = true;
			}
		}
		*/
					
		public function get user():User {
			return _user;
		}
		
		public function get userRoles():ArrayCollection {
			return _userRoles;
		}
		
		public function get roles():ArrayCollection {
			return _roles;
		}
		
		public function get permissionAssignments():ArrayCollection {
			return _permissionAssignments;
		}
		
		public function get permissions():ArrayCollection {
			return _permissions;
		}
		
		public function get modules():ArrayCollection {
			return _modules;
		}
		
		public function ExistsRole(roleName:String, moduleId:int):Boolean {
			for each (var r:Role in _roles) {
				if (r.Name == roleName) {
					for each (var p_ass:PermissionAssignment in r.RelatedPermissionAssignment) {
						if (p_ass.RelatedPermission.RelatedModule.ModuleId == moduleId) {
							return true;
						}
					}
				}
			}
			return false;
		}
		
	}
}