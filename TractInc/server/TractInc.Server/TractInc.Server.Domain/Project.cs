using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class Project
{
    private const string SQL_SELECT_BY_COMPANY_ID =
        @"select p.*
            from Project p
                 inner join Contract c on p.ContractId = c.ContractId
           where c.CompanyId = @CompanyId";

    private const string SQL_SELECT_BY_COMPANY_AND_CLIENT_ID =
        @"select p.*
            from Project p
                 inner join Contract c on p.ContractId = c.ContractId
           where c.CompanyId = @CompanyId and c.ClientId = @ClientId";

    public Project()
    {
    }

    public static List<Project> findByCompany(Company company)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Project.SQL_SELECT_BY_COMPANY_ID))
        {
            Database.PutParameter(dbCommand, "@CompanyId", company.CompanyId);
            List<Project> result = new List<Project>();
            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    result.Add(Load(dataReader));
                }
            }
            return result;
        }
    }

    public static List<Project> findByCompanyAndClient(Company company, int clientId)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Project.SQL_SELECT_BY_COMPANY_AND_CLIENT_ID))
        {
            Database.PutParameter(dbCommand, "@CompanyId", company.CompanyId);
            Database.PutParameter(dbCommand, "@ClientId", clientId);
            List<Project> result = new List<Project>();
            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    result.Add(Load(dataReader));
                }
            }
            return result;
        }
    }

    public static void Save(Project project) {
        Database.Begin();
        try
        {
            if ( 0 != project.ProjectId ) {
                Project.Update(project);
            } else {
                Project.Insert(project);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

    public static void Remove(Project project) {
        Database.Begin();
        try
        {
            Project.Delete(project);
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
