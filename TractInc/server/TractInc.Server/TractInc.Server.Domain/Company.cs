using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class Company
{
    private const string SQL_SELECT_BY_CLIENT_ID =
        @"select c.*
            from Company c
                 inner join ClientCompany cc on c.CompanyId = cc.CompanyId
           where cc.ClientId = @ClientId";
        
    private const string SQL_SELECT_BY_USER_ID =
        @"select c.*
            from Company c
                 inner join Person p on c.CompanyId = p.CompanyId
                 inner join [User] u on p.PersonId = u.PersonId
           where u.UserId = @UserId";
        
    public List<Client> ClientList = null;
    public List<Person> PersonList = null;

    public Company()
    {
    }

    public static Company findUserCompany(User user)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Company.SQL_SELECT_BY_USER_ID))
        {
            Database.PutParameter(dbCommand, "@UserId", user.UserId);
            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                if ( dataReader.Read() )
                {
                    return Load(dataReader);
                } else {
                    return null;
                }
            }
        }
    }

    public static List<Company> findByClient(Client client)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Company.SQL_SELECT_BY_CLIENT_ID))
        {
            Database.PutParameter(dbCommand, "@ClientId", client.ClientId);
            List<Company> result = new List<Company>();
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

    public static void Save(Company company) {
        Database.Begin();
        try
        {
            if ( 0 != company.CompanyId ) {
                Company.Update(company);
                ClientCompany.DeleteByCompany(company);
            } else {
                Company.Insert(company);
            }

            foreach (Client c in company.ClientList) {
                ClientCompany cc = new ClientCompany();
                cc.CompanyId = company.CompanyId;
                cc.ClientId = c.ClientId;
                ClientCompany.Insert(cc);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

    public static void Remove(Company company) {
        Database.Begin();
        try
        {
            Company.Delete(company);
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
      