using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class Team
{
    private const string SQL_SELECT_BY_COMPANY_ID =
        @"select a.*
            from Team a
           where a.CompanyId = @CompanyId";

    public List<TeamMember> MemberList = null;
    public List<TeamAssignment> TeamAssignmentList = null;

    public Team() {}

    public static List<Team> findByCompany(Company company, bool populate)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Team.SQL_SELECT_BY_COMPANY_ID))
        {
            Database.PutParameter(dbCommand, "@CompanyId", company.CompanyId);
            List<Team> result = new List<Team>();
            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    Team team = Load(dataReader);
                    result.Add(team);
                }
            }

            if ( populate ) {
                foreach (Team team in result) {
                    team.MemberList = TeamMember.findByTeam(team, true);
                    team.TeamAssignmentList = TeamAssignment.findByTeam(team);
                }
            }

            return result;
        }
    }

    public static void Save(Team team) {
        Database.Begin();
        try
        {
            if ( 0 != team.TeamId ) {
                Team.Update(team);
            } else {
                Team.Insert(team);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

    public static void Remove(Team team) {
        Database.Begin();
        try
        {
            Team.Delete(team);
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
      