using System;
using System.Data;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class UserPreference
{
    public UserPreference()
    {

    }

    private const string SQL_SELECT_BY_USER_ID =
        @"
        SELECT UserPereferenceId, UserId, DefaultSite, NewTracts
          FROM UserPreference 
         WHERE UserId = {0}";
        
    public static UserPreference GetUserPreferenceByUserId(int userId)
    {
        string sql = String.Format(UserPreference.SQL_SELECT_BY_USER_ID, userId);

        UserPreference result = null;
        using(IDataReader dataReader = Database.PrepareCommand(sql).ExecuteReader()) {
            if (dataReader.Read()) {
                result = UserPreference.Load(dataReader);
            }
        }

        return result;
    }
        
}
}
      