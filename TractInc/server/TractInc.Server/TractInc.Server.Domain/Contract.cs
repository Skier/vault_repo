using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class Contract
{
    private const string SQL_SELECT_BY_CLIENT_ID =
        @"select c.*
            from Contract c
           where c.ClientId = @ClientId";

    private const string SQL_SELECT_BY_COMPANY_ID =
        @"select c.*
            from Contract c
           where c.CompanyId = @CompanyId";

    public List<ContractRate> ContractRateList = null;
/*
    public List<ClientContact> ClientContactList = null;
    public List<CompanyContact> CompanyContactList = null;
*/
    public Contract()
    {
    }

    public static List<Contract> findByClient(Client client)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Contract.SQL_SELECT_BY_CLIENT_ID))
        {
            Database.PutParameter(dbCommand, "@ClientId", client.ClientId);
            List<Contract> result = new List<Contract>();
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

    public static List<Contract> findByCompany(Company company)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Contract.SQL_SELECT_BY_COMPANY_ID))
        {
            Database.PutParameter(dbCommand, "@CompanyId", company.CompanyId);
            List<Contract> result = new List<Contract>();
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

    public static void Save(Contract contract) {
        Database.Begin();
        try
        {
            if ( 0 != contract.ContractId ) {
                Contract.Update(contract);
                ContractRate.DeleteByContract(contract);
            } else {
                Contract.Insert(contract);
            }
            foreach (ContractRate cr in contract.ContractRateList) {
                cr.ContractId = contract.ContractId;
                ContractRate.Insert(cr);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

    public static void Remove(Contract contract) {
        Database.Begin();
        try
        {
            Contract.Delete(contract);
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
      