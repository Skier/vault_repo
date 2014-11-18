using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class ContractRate
{
    private const string SQL_SELECT_BY_CONTRACT_ID =
        @"select c.*
            from ContractRate c
           where c.ContractId = @ContractId";
        
    private const string SQL_DELETE_BY_CONTRACT_ID =
        @"delete from ContractRate where ContractId={0}";
        
    public ContractRate()
    {
    }

    public static List<ContractRate> findByContract(Contract contractRate)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                ContractRate.SQL_SELECT_BY_CONTRACT_ID))
        {
            Database.PutParameter(dbCommand, "@ContractId", contractRate.ContractId);
            List<ContractRate> result = new List<ContractRate>();
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

    public static void Save(ContractRate contractRate) {
        Database.Begin();
        try
        {
            if ( 0 != contractRate.ContractRateId ) {
                ContractRate.Update(contractRate);
            } else {
                ContractRate.Insert(contractRate);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

    public static void Remove(ContractRate contractRate) {
        Database.Begin();
        try
        {
            ContractRate.Delete(contractRate);
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

    public static void DeleteByContract(Contract contract) 
    {
        string sql = String.Format(ContractRate.SQL_DELETE_BY_CONTRACT_ID, contract.ContractId);
        Database.PrepareCommand(sql).ExecuteNonQuery();
    }

}
}
      