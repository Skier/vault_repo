using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class ClientCompany
{
    private const string SQL_DELETE_BY_CLIENT_ID =
        @"delete from ClientCompany where ClientId={0}";
        
    private const string SQL_DELETE_BY_COMPANY_ID =
        @"delete from ClientCompany where CompanyId={0}";
        
    public ClientCompany()
    {
    }

    public static void DeleteByClient(Client client) 
    {
        string sql = String.Format(ClientCompany.SQL_DELETE_BY_CLIENT_ID, client.ClientId);
        Database.PrepareCommand(sql).ExecuteNonQuery();
    }

    public static void DeleteByCompany(Company company) 
    {
        string sql = String.Format(ClientCompany.SQL_DELETE_BY_COMPANY_ID, company.CompanyId);
        Database.PrepareCommand(sql).ExecuteNonQuery();
    }

}
}
      