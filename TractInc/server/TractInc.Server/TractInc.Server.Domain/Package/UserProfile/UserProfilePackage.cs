using System.Collections.Generic;

namespace TractInc.Server.Domain.Package.UserProfile
{
public class UserProfilePackage
{
    public const int USER_PROFILE_MODULE_ID = 1;
    public const int USER_PROFILE_RUN_PERM_ID = 1;
    public const int USER_PROFILE_VIEW_PERM_ID = 2;
    public const int USER_PROFILE_EDIT_PERM_ID = 3;

    #region Person

    private Person m_person;
    public Person person
    {
        get { return m_person; }
        set { m_person = value; }
    }
    #endregion

    #region User
    private User m_user;
    public User user
    {
        get { return m_user; }
        set { m_user = value; }
    }

    #endregion

    #region UserPreference
    private UserPreference m_userPreference;
    public UserPreference userPreference
    {
        get { return m_userPreference; }
        set { m_userPreference = value; }
    }

    #endregion

    #region canView
    private bool m_canView;
    public bool canView
    {
        get { return m_canView; }
        set { m_canView = value; }
    }

    #endregion

    #region canEdit
    private bool m_canEdit;
    public bool canEdit
    {
        get { return m_canEdit; }
        set { m_canEdit = value; }
    }

    #endregion


}
}
