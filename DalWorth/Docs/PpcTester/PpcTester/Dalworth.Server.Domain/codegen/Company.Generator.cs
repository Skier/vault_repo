
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Domain
      {


      public partial class Company : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Company ( " +
      
        " Name, " +
      
        " GoogleClientEmail " +
      
      ") Values (" +
      
        " ?Name, " +
      
        " ?GoogleClientEmail " +
      
      ")";

      public static void Insert(Company company, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?Name", company.Name);
      
        Database.PutParameter(dbCommand,"?GoogleClientEmail", company.GoogleClientEmail);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        company.Id = Convert.ToInt64(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Company company)
      {
        Insert(company, null);
      }


      public static void Insert(List<Company>  companyList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Company company in  companyList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?Name", company.Name);
      
        Database.PutParameter(dbCommand,"?GoogleClientEmail", company.GoogleClientEmail);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?Name",company.Name);
      
        Database.UpdateParameter(dbCommand,"?GoogleClientEmail",company.GoogleClientEmail);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        company.Id = Convert.ToInt64(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Company>  companyList)
      {
        Insert(companyList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Company Set "
      
        + " Name = ?Name, "
      
        + " GoogleClientEmail = ?GoogleClientEmail "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(Company company, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", company.Id);
      
        Database.PutParameter(dbCommand,"?Name", company.Name);
      
        Database.PutParameter(dbCommand,"?GoogleClientEmail", company.GoogleClientEmail);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Company company)
      {
        Update(company, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " Name, "
      
        + " GoogleClientEmail "
      

      + " From Company "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static Company FindByPrimaryKey(
      long id, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Company not found, search by primary key");

      }

      public static Company FindByPrimaryKey(
      long id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Company company, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",company.Id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Company company)
      {
      return Exists(company, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Company limit 1";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      public static bool IsContainsData()
      {
      return IsContainsData(null);
      }

      #endregion

      #region Load

      public static Company Load(IDataReader dataReader, int offset)
      {
      Company company = new Company();

      company.Id = dataReader.GetInt64(0 + offset);
          company.Name = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            company.GoogleClientEmail = dataReader.GetString(2 + offset);
          

      return company;
      }

      public static Company Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Company "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(Company company, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", company.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Company company)
      {
        Delete(company, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Company ";

      public static void Clear(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, connection))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Clear()
      {
        Clear(null);
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " Id, "
      
        + " Name, "
      
        + " GoogleClientEmail "
      

      + " From Company ";
      public static List<Company> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Company> rv = new List<Company>();

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

      public static List<Company> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Company> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Company obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return Id == obj.Id && Name == obj.Name && GoogleClientEmail == obj.GoogleClientEmail;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Company> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Company));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Company item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Company>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Company));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Company> itemsList
      = new List<Company>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Company)
      itemsList.Add(deserializedObject as Company);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected long m_id;
      
        protected String m_name;
      
        protected String m_googleClientEmail;
      
      #endregion

      #region Constructors
      public Company(
      long 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public Company(
        long 
          id,String 
          name,String 
          googleClientEmail
        ) : this()
        {
        
          m_id = id;
        
          m_name = name;
        
          m_googleClientEmail = googleClientEmail;
        
        }


      
      #endregion

      
        [XmlElement]
        public long Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public String GoogleClientEmail
        {
        get { return m_googleClientEmail;}
        set { m_googleClientEmail = value; }
        }
      

      public static int FieldsCount
      {
      get { return 3; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    