using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class TeamAssignment
{
    private const string SQL_SELECT_BY_TEAM_ID =
        @"select a.*
            from TeamAssignment a
           where a.TeamId = @TeamId";

    public Asset MemberAsset = null;

    public TeamAssignment() {}

    public static List<TeamAssignment> findByTeam(Team team)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                TeamAssignment.SQL_SELECT_BY_TEAM_ID))
        {
            Database.PutParameter(dbCommand, "@TeamId", team.TeamId);
            List<TeamAssignment> result = new List<TeamAssignment>();
            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    result.Add(Load(dataReader));
                }
            }
/*
            if ( populate ) {
                foreach (TeamAssignment assignment in result) {
                    assignment.MemberAsset = Asset.FindByPrimaryKey(assignment.AssetId);
                }
            }
*/
            return result;
        }
    }

    public static void Save(TeamAssignment assignment) {
        Database.Begin();
        try
        {
            if ( 0 != assignment.TeamAssignmentId ) {
                TeamAssignment.Update(assignment);
            } else {
                TeamAssignment.Insert(assignment);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

    public static void Remove(TeamAssignment assignment) {
        Database.Begin();
        try
        {
            TeamAssignment.Delete(assignment);
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
      