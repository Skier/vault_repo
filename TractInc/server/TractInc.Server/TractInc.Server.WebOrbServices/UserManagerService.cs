using System;
using System.Collections.Generic;
using TractInc.Server.Domain;
using TractInc.Server.Domain.Package.UserManager;

namespace TractInc.Server.WebOrbServices
{

public class UserManagerService
{
    public UserManagerPackage GetUserManagerPackage(int userId) {
        User currentUser = User.FindByPrimaryKey(userId);
        if ( null == currentUser ) {
            throw new InvalidOperationException("userId is not valid");
        }

        UserManagerPackage result = new UserManagerPackage();
        
        result.RoleList = Role.Find();
        foreach(Role role in result.RoleList) {
            role.PermissionList = Permission.findByRole(role);
        }

        result.UserList = User.Find();
        foreach(User user in result.UserList) {
            user.Personal = Person.FindByPrimaryKey(user.PersonId);
            user.Preference = UserPreference.GetUserPreferenceByUserId(user.UserId);
            user.RoleList = Role.findByUser(user);
        }

        //result.PermissionList = Permission.Find();

        result.ModuleList = Module.findAll(true);

        result.ClientList = Client.Find();

        result.CompanyList = Company.Find();

        result.canManageUsers = false;
        result.canManageRoles = false;
        foreach (Role role in Role.findByUser(currentUser)) {
            if ( PermissionAssignment.HasPermission(role, 
                    UserManagerPackage.USER_MANAGER_MANAGE_USERS_PERM_ID) ) {
                result.canManageUsers = true;
            }
            if ( PermissionAssignment.HasPermission(role, 
                    UserManagerPackage.USER_MANAGER_MANAGE_ROLES_PERM_ID) ) {
                result.canManageRoles = true;
            }
        }

        return result;
    }

    public void SaveUser(User user) {
        User.Save(user);
    }

    public List<User> SearchUser(String login, String firstName, String lastName, int roleId, bool isActive, int companyId, int clientId) {
        List<User> result = User.Search(login, firstName, lastName, roleId, isActive, companyId, clientId);
        foreach(User user in result) {
            user.Personal = Person.FindByPrimaryKey(user.PersonId);
            user.Preference = UserPreference.GetUserPreferenceByUserId(user.UserId);
            user.RoleList = Role.findByUser(user);
        }
        return result;
    }

    public void SaveRole(Role role) {
        Role.Save(role);
    }

    public void RemoveRole(Role role) {
        Role.Remove(role);
    }

}
}
