using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;


namespace TractInc.Server.Domain
{
public partial class CompanyContact
{
    private const string SQL_SELECT_BY_CONTRACT_ID =
        @"select c.*
            from CompanyContact c
           where c.ContractId = @ContractId";

    public Person ContactPerson;
    
    public CompanyContact()
    {
    }

    public static List<CompanyContact> findByContract(Contract contract)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                CompanyContact.SQL_SELECT_BY_CONTRACT_ID))
        {
            Database.PutParameter(dbCommand, "@ContractId", contract.ContractId);
            List<CompanyContact> result = new List<CompanyContact>();
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

    public static void Save(CompanyContact companyContact) {
        Database.Begin();
        try
        {
            Person person = companyContact.ContactPerson;
            if ( 0 != person.PersonId ) {
                Person.Update(person);
            } else {
                Person.Insert(person);
            }

            companyContact.PersonId = person.PersonId;
            if ( 0 != companyContact.CompanyContactId ) {
                CompanyContact.Update(companyContact);
            } else {
                CompanyContact.Insert(companyContact);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

    public static void Remove(CompanyContact companyContact) {
        Database.Begin();
        try
        {
            CompanyContact.Delete(companyContact);
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
      