using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class Client
{
    private const string SQL_SELECT_BY_COMPANY_ID =
        @"select c.*
            from Client c 
                 inner join ClientCompany cc on c.ClientId = cc.ClientId
           where cc.CompanyId = @CompanyId";
        
    private const string SQL_SELECT_BY_USER_ID =
        @"select c.*
            from Client c
                 inner join Person p on c.ClientId = p.ClientId
                 inner join [User] u on p.PersonId = u.PersonId
           where u.UserId = @UserId";
        
    public List<Company> CompanyList = null;
    public List<Person> PersonList = null;

    public Client()
    {
    }

    public static Client findUserClient(User user)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Client.SQL_SELECT_BY_USER_ID))
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

    public static List<Client> findByCompany(Company company)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Client.SQL_SELECT_BY_COMPANY_ID))
        {
            Database.PutParameter(dbCommand, "@CompanyId", company.CompanyId);
            List<Client> result = new List<Client>();
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

    public static void Save(Client client) {
        Database.Begin();
        try
        {
            if ( 0 != client.ClientId ) {
                Client.Update(client);
                ClientCompany.DeleteByClient(client);
            } else {
                Client.Insert(client);
            }

            foreach (Company c in client.CompanyList) {
                ClientCompany cc = new ClientCompany();
                cc.CompanyId = c.CompanyId;
                cc.ClientId = client.ClientId;
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

    public static void Remove(Client client) {
        Database.Begin();
        try
        {
            Client.Delete(client);
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
      