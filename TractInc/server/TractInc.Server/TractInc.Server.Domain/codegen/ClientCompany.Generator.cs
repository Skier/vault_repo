
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


      public partial class ClientCompany
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [ClientCompany] ( " +
      
        " ClientId, " +
      
        " CompanyId " +
      
      ") Values (" +
      
        " @ClientId, " +
      
        " @CompanyId " +
      
      ")";

      public static void Insert(ClientCompany clientCompany)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@ClientId", clientCompany.ClientId);
      
        Database.PutParameter(dbCommand,"@CompanyId", clientCompany.CompanyId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          clientCompany.ClientCompanyId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<ClientCompany>  clientCompanyList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(ClientCompany clientCompany in  clientCompanyList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ClientId", clientCompany.ClientId);
      
        Database.PutParameter(dbCommand,"@CompanyId", clientCompany.CompanyId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ClientId",clientCompany.ClientId);
      
        Database.UpdateParameter(dbCommand,"@CompanyId",clientCompany.CompanyId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        clientCompany.ClientCompanyId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [ClientCompany] Set "
      
        + " ClientId = @ClientId, "
      
        + " CompanyId = @CompanyId "
      
        + " Where "
        
          + " ClientCompanyId = @ClientCompanyId "
        
      ;

      public static void Update(ClientCompany clientCompany)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@ClientCompanyId", clientCompany.ClientCompanyId);
      
        Database.PutParameter(dbCommand,"@ClientId", clientCompany.ClientId);
      
        Database.PutParameter(dbCommand,"@CompanyId", clientCompany.CompanyId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ClientCompanyId, "
      
        + " ClientId, "
      
        + " CompanyId "
      

      + " From [ClientCompany] "

      
        + " Where "
        
          + " ClientCompanyId = @ClientCompanyId "
        
      ;

      public static ClientCompany FindByPrimaryKey(
      int clientCompanyId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ClientCompanyId", clientCompanyId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ClientCompany not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(ClientCompany clientCompany)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ClientCompanyId",clientCompany.ClientCompanyId);
      

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
      String sql = "select 1 from ClientCompany";

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

      public static ClientCompany Load(IDataReader dataReader)
      {
      ClientCompany clientCompany = new ClientCompany();

      clientCompany.ClientCompanyId = dataReader.GetInt32(0);
          clientCompany.ClientId = dataReader.GetInt32(1);
          clientCompany.CompanyId = dataReader.GetInt32(2);
          

      return clientCompany;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [ClientCompany] "

      
        + " Where "
        
          + " ClientCompanyId = @ClientCompanyId "
        
      ;
      public static void Delete(ClientCompany clientCompany)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@ClientCompanyId", clientCompany.ClientCompanyId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [ClientCompany] ";

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

      
        + " ClientCompanyId, "
      
        + " ClientId, "
      
        + " CompanyId "
      

      + " From [ClientCompany] ";
      public static List<ClientCompany> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<ClientCompany> rv = new List<ClientCompany>();

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
      List<ClientCompany> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<ClientCompany> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ClientCompany));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ClientCompany item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ClientCompany>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ClientCompany));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ClientCompany> itemsList
      = new List<ClientCompany>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ClientCompany)
      itemsList.Add(deserializedObject as ClientCompany);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_clientCompanyId;
      
        protected int m_clientId;
      
        protected int m_companyId;
      
      #endregion

      #region Constructors
      public ClientCompany(
      int 
          clientCompanyId
      )
      {
      
        m_clientCompanyId = clientCompanyId;
      
      }

      


        public ClientCompany(
        int 
          clientCompanyId,int 
          clientId,int 
          companyId
        )
        {
        
          m_clientCompanyId = clientCompanyId;
        
          m_clientId = clientId;
        
          m_companyId = companyId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ClientCompanyId
        {
        get { return m_clientCompanyId;}
        set { m_clientCompanyId = value; }
        }
      
        [XmlElement]
        public int ClientId
        {
        get { return m_clientId;}
        set { m_clientId = value; }
        }
      
        [XmlElement]
        public int CompanyId
        {
        get { return m_companyId;}
        set { m_companyId = value; }
        }
      
      }
      #endregion
      }

    