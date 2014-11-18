using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class Account
{
    private const string SQL_SELECT_BY_COMPANY_ID =
        @"select a.*
            from Account a
           where a.CompanyId = @CompanyId";

    public Account()
    {
    }

    public static List<Account> findByCompany(Company company)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Account.SQL_SELECT_BY_COMPANY_ID))
        {
            Database.PutParameter(dbCommand, "@CompanyId", company.CompanyId);
            List<Account> result = new List<Account>();
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

    public static void Save(Account account) {
        Database.Begin();
        try
        {
            if ( 0 != account.AccountId ) {
                Account.Update(account);
            } else {
                Account.Insert(account);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

    public static void Remove(Account account) {
        Database.Begin();
        try
        {
            Account.Delete(account);
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
      