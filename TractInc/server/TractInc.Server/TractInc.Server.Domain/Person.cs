using System;
using System.Data;
using System.Collections.Generic;
using TractInc.Server.Data;

namespace TractInc.Server.Domain
{

public partial class Person
{
    private const string SQL_SELECT_BY_CLIENT_ID =
        @"
        SELECT *
          FROM [Person] 
         WHERE ClientId = @ClientId";
    
    private const string SQL_SELECT_BY_COMPANY_ID =
        @"
        SELECT *
          FROM [Person] 
         WHERE CompanyId = @CompanyId";
    
    private const string SQL_SELECT_BY_USER_ID =
        @"
        SELECT 
            PersonId, UserId, AssetId, FirstName, MiddleName, LastName, Phone, Email, SSN
          FROM [Person] 
         WHERE UserId = {0}";
    
    public Person()
    {
    }

    public static List<Person> findByClient(Client client)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Person.SQL_SELECT_BY_CLIENT_ID))
        {
            Database.PutParameter(dbCommand, "@ClientId", client.ClientId);
            List<Person> result = new List<Person>();
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

    public static List<Person> findByCompany(Company company)
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(
                Person.SQL_SELECT_BY_COMPANY_ID))
        {
            Database.PutParameter(dbCommand, "@CompanyId", company.CompanyId);
            List<Person> result = new List<Person>();
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

    public static Person GetPersonByUserId(int userId)
    {
        string sql = String.Format(SQL_SELECT_BY_USER_ID, userId);

        Person personInfo = null;
        using(IDataReader dataReader = Database.PrepareCommand(sql).ExecuteReader()) {
            if (dataReader.Read()) {
                personInfo = Person.Load(dataReader);
            }
        }

        return personInfo;
    }
        
    public static void SavePerson(Person person)
    {
        Person existingPerson = Person.FindByPrimaryKey(person.PersonId);
        if (null == existingPerson)
        {
            throw new Exception("Person is not found");
        }

        Database.Begin();

        try
        {
            Person.Update(person);
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
