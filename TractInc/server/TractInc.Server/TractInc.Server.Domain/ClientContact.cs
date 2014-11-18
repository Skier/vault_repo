using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{
public partial class ClientContact
{
    private const string SQL_SELECT_BY_CONTRACT_ID =
        @"select c.*
            from ClientContact c
           where c.ContractId = @ContractId";

    public Person ContactPerson;
    
    public ClientContact()
    {
    }

    public static List<ClientContact> findByContract(Contract contract)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                ClientContact.SQL_SELECT_BY_CONTRACT_ID))
        {
            Database.PutParameter(dbCommand, "@ContractId", contract.ContractId);
            List<ClientContact> result = new List<ClientContact>();
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

    public static void Save(ClientContact clientContact) {
        Database.Begin();
        try
        {
            Person person = clientContact.ContactPerson;
            if ( 0 != person.PersonId ) {
                Person.Update(person);
            } else {
                Person.Insert(person);
            }

            clientContact.PersonId = person.PersonId;
            if ( 0 != clientContact.ClientContactId ) {
                ClientContact.Update(clientContact);
            } else {
                ClientContact.Insert(clientContact);
            }
        }
        catch (Exception ex)
        {
            Database.Rollback();
            throw ex;
        }
        Database.Commit();
    }

    public static void Remove(ClientContact clientContact) {
        Database.Begin();
        try
        {
            ClientContact.Delete(clientContact);
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
      