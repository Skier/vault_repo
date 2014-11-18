using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class TeamMember
{
    private const string SQL_SELECT_BY_TEAM_ID =
        @"select a.*
            from TeamMember a
           where a.TeamId = @TeamId";

    public Asset MemberAsset = null;

    public TeamMember() {}

    public static List<TeamMember> findByTeam(Team team, bool populate)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                TeamMember.SQL_SELECT_BY_TEAM_ID))
        {
            Database.PutParameter(dbCommand, "@TeamId", team.TeamId);
            List<TeamMember> result = new List<TeamMember>();
            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    result.Add(Load(dataReader));
                }
            }

            if ( populate ) {
                foreach (TeamMember member in result) {
                    member.MemberAsset = Asset.FindByPrimaryKey(member.AssetId);
                }
            }

            return result;
        }
    }

    public static void Save(TeamMember member) {
        Database.Begin();
        try
        {
            if ( 0 != member.TeamMemberId ) {
                TeamMember.Update(member);
            } else {
                TeamMember.Insert(member);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

    public static void Remove(TeamMember member) {
        Database.Begin();
        try
        {
            TeamMember.Delete(member);
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }
}
}
      