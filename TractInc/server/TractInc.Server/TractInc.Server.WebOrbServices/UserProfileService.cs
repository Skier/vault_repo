using TractInc.Server.Domain;
using TractInc.Server.Domain.Package.UserProfile;

namespace TractInc.Server.WebOrbServices
{

public class UserProfileService
{
    public UserProfilePackage GetUserProfilePackage(int userId) {
        UserProfilePackage result = new UserProfilePackage();
        
        result.user = User.FindByPrimaryKey(userId);
        result.userPreference = UserPreference.GetUserPreferenceByUserId(userId);
        result.person = Person.FindByPrimaryKey(result.user.PersonId);
        result.canView = true;
        result.canEdit = false;
        foreach (Role role in Role.findByUser(result.user)) {
            if ( PermissionAssignment.HasPermission(role, 
                    UserProfilePackage.USER_PROFILE_EDIT_PERM_ID) ) {
                result.canEdit = true;
            }
        }

        return result;
    }

    public void ChangePassword(int userId, 
            string oldPassword,
            string newPassword)
    {
        User.ChangePassword(userId, oldPassword, newPassword);
    }

    public void SavePerson(Person person)
    {
        Person.SavePerson(person);
    }
}

}
