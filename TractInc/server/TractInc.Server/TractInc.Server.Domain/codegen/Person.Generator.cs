
    using System;
    using System.Data;
    using System.Collections.Generic;
    using TractInc.Server.Data;
    using TractInc.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace TractInc.Server.Domain
      {


      public partial class Person
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Person] ( " +
      
        " ClientId, " +
      
        " CompanyId, " +
      
        " FirstName, " +
      
        " MiddleName, " +
      
        " LastName, " +
      
        " PhoneNumber, " +
      
        " Email, " +
      
        " SSN " +
      
      ") Values (" +
      
        " @ClientId, " +
      
        " @CompanyId, " +
      
        " @FirstName, " +
      
        " @MiddleName, " +
      
        " @LastName, " +
      
        " @PhoneNumber, " +
      
        " @Email, " +
      
        " @SSN " +
      
      ")";

      public static void Insert(Person person)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
Database.PutParameter(dbCommand,"@ClientId", 0 != person.ClientId ? person.ClientId : null);

Database.PutParameter(dbCommand,"@CompanyId", 0 != person.CompanyId ? person.CompanyId : null);
      
        Database.PutParameter(dbCommand,"@FirstName", person.FirstName);
      
        Database.PutParameter(dbCommand,"@MiddleName", person.MiddleName);
      
        Database.PutParameter(dbCommand,"@LastName", person.LastName);
      
        Database.PutParameter(dbCommand,"@PhoneNumber", person.PhoneNumber);
      
        Database.PutParameter(dbCommand,"@Email", person.Email);
      
        Database.PutParameter(dbCommand,"@SSN", person.SSN);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          person.PersonId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<Person>  personList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Person person in  personList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ClientId", person.ClientId);
      
        Database.PutParameter(dbCommand,"@CompanyId", person.CompanyId);
      
        Database.PutParameter(dbCommand,"@FirstName", person.FirstName);
      
        Database.PutParameter(dbCommand,"@MiddleName", person.MiddleName);
      
        Database.PutParameter(dbCommand,"@LastName", person.LastName);
      
        Database.PutParameter(dbCommand,"@PhoneNumber", person.PhoneNumber);
      
        Database.PutParameter(dbCommand,"@Email", person.Email);
      
        Database.PutParameter(dbCommand,"@SSN", person.SSN);
      
      parametersAdded = true;
      }
      else
      {

      
Database.UpdateParameter(dbCommand,"@ClientId", 0 != person.ClientId ? person.ClientId : null);

Database.UpdateParameter(dbCommand,"@CompanyId", 0 != person.CompanyId ? person.CompanyId : null);
      
        Database.UpdateParameter(dbCommand,"@FirstName",person.FirstName);
      
        Database.UpdateParameter(dbCommand,"@MiddleName",person.MiddleName);
      
        Database.UpdateParameter(dbCommand,"@LastName",person.LastName);
      
        Database.UpdateParameter(dbCommand,"@PhoneNumber",person.PhoneNumber);
      
        Database.UpdateParameter(dbCommand,"@Email",person.Email);
      
        Database.UpdateParameter(dbCommand,"@SSN",person.SSN);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        person.PersonId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Person] Set "
      
        + " ClientId = @ClientId, "
      
        + " CompanyId = @CompanyId, "
      
        + " FirstName = @FirstName, "
      
        + " MiddleName = @MiddleName, "
      
        + " LastName = @LastName, "
      
        + " PhoneNumber = @PhoneNumber, "
      
        + " Email = @Email, "
      
        + " SSN = @SSN "
      
        + " Where "
        
          + " PersonId = @PersonId "
        
      ;

      public static void Update(Person person)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@PersonId", person.PersonId);
      
Database.PutParameter(dbCommand,"@ClientId", 0 != person.ClientId ? person.ClientId : null);

Database.PutParameter(dbCommand,"@CompanyId", 0 != person.CompanyId ? person.CompanyId : null);
      
      
        Database.PutParameter(dbCommand,"@FirstName", person.FirstName);
      
        Database.PutParameter(dbCommand,"@MiddleName", person.MiddleName);
      
        Database.PutParameter(dbCommand,"@LastName", person.LastName);
      
        Database.PutParameter(dbCommand,"@PhoneNumber", person.PhoneNumber);
      
        Database.PutParameter(dbCommand,"@Email", person.Email);
      
        Database.PutParameter(dbCommand,"@SSN", person.SSN);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " PersonId, "
      
        + " ClientId, "
      
        + " CompanyId, "
      
        + " FirstName, "
      
        + " MiddleName, "
      
        + " LastName, "
      
        + " PhoneNumber, "
      
        + " Email, "
      
        + " SSN "
      

      + " From [Person] "

      
        + " Where "
        
          + " PersonId = @PersonId "
        
      ;

      public static Person FindByPrimaryKey(
      int personId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@PersonId", personId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Person not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Person person)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@PersonId",person.PersonId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData()
      {
      String sql = "select 1 from Person";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static Person Load(IDataReader dataReader)
      {
      Person person = new Person();

      person.PersonId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            person.ClientId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            person.CompanyId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            person.FirstName = dataReader.GetString(3);
          
            if(!dataReader.IsDBNull(4))
            person.MiddleName = dataReader.GetString(4);
          
            if(!dataReader.IsDBNull(5))
            person.LastName = dataReader.GetString(5);
          
            if(!dataReader.IsDBNull(6))
            person.PhoneNumber = dataReader.GetString(6);
          person.Email = dataReader.GetString(7);
          
            if(!dataReader.IsDBNull(8))
            person.SSN = dataReader.GetString(8);
          

      return person;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Person] "

      
        + " Where "
        
          + " PersonId = @PersonId "
        
      ;
      public static void Delete(Person person)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@PersonId", person.PersonId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Person] ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " PersonId, "
      
        + " ClientId, "
      
        + " CompanyId, "
      
        + " FirstName, "
      
        + " MiddleName, "
      
        + " LastName, "
      
        + " PhoneNumber, "
      
        + " Email, "
      
        + " SSN "
      

      + " From [Person] ";
      public static List<Person> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Person> rv = new List<Person>();

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      while(dataReader.Read())
      {
      rv.Add(Load(dataReader));
      }

      }

      return rv;
      }

      }
      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Person> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Person> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Person item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Person>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Person> itemsList
      = new List<Person>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Person)
      itemsList.Add(deserializedObject as Person);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_personId;
      
        protected int? m_clientId;
      
        protected int? m_companyId;
      
        protected String m_firstName;
      
        protected String m_middleName;
      
        protected String m_lastName;
      
        protected String m_phoneNumber;
      
        protected String m_email;
      
        protected String m_sSN;
      
      #endregion

      #region Constructors
      public Person(
      int 
          personId
      )
      {
      
        m_personId = personId;
      
      }

      


        public Person(
        int 
          personId,int? 
          clientId,int? 
          companyId,String 
          firstName,String 
          middleName,String 
          lastName,String 
          phoneNumber,String 
          email,String 
          sSN
        )
        {
        
          m_personId = personId;
        
          m_clientId = clientId;
        
          m_companyId = companyId;
        
          m_firstName = firstName;
        
          m_middleName = middleName;
        
          m_lastName = lastName;
        
          m_phoneNumber = phoneNumber;
        
          m_email = email;
        
          m_sSN = sSN;
        
        }


      
      #endregion

      
        [XmlElement]
        public int PersonId
        {
        get { return m_personId;}
        set { m_personId = value; }
        }
      
        [XmlElement]
        public int? ClientId
        {
        get { return m_clientId;}
        set { m_clientId = value; }
        }
      
        [XmlElement]
        public int? CompanyId
        {
        get { return m_companyId;}
        set { m_companyId = value; }
        }
      
        [XmlElement]
        public String FirstName
        {
        get { return m_firstName;}
        set { m_firstName = value; }
        }
      
        [XmlElement]
        public String MiddleName
        {
        get { return m_middleName;}
        set { m_middleName = value; }
        }
      
        [XmlElement]
        public String LastName
        {
        get { return m_lastName;}
        set { m_lastName = value; }
        }
      
        [XmlElement]
        public String PhoneNumber
        {
        get { return m_phoneNumber;}
        set { m_phoneNumber = value; }
        }
      
        [XmlElement]
        public String Email
        {
        get { return m_email;}
        set { m_email = value; }
        }
      
        [XmlElement]
        public String SSN
        {
        get { return m_sSN;}
        set { m_sSN = value; }
        }
      
      }
      #endregion
      }

    