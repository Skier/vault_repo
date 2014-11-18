
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


      public partial class Client
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Client] ( " +
      
        " ClientName, " +
      
        " ClientAddress " +
      
      ") Values (" +
      
        " @ClientName, " +
      
        " @ClientAddress " +
      
      ")";

      public static void Insert(Client client)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@ClientName", client.ClientName);
      
        Database.PutParameter(dbCommand,"@ClientAddress", client.ClientAddress);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          client.ClientId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<Client>  clientList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Client client in  clientList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ClientName", client.ClientName);
      
        Database.PutParameter(dbCommand,"@ClientAddress", client.ClientAddress);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ClientName",client.ClientName);
      
        Database.UpdateParameter(dbCommand,"@ClientAddress",client.ClientAddress);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        client.ClientId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Client] Set "
      
        + " ClientName = @ClientName, "
      
        + " ClientAddress = @ClientAddress "
      
        + " Where "
        
          + " ClientId = @ClientId "
        
      ;

      public static void Update(Client client)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@ClientId", client.ClientId);
      
        Database.PutParameter(dbCommand,"@ClientName", client.ClientName);
      
        Database.PutParameter(dbCommand,"@ClientAddress", client.ClientAddress);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ClientId, "
      
        + " ClientName, "
      
        + " ClientAddress "
      

      + " From [Client] "

      
        + " Where "
        
          + " ClientId = @ClientId "
        
      ;

      public static Client FindByPrimaryKey(
      int clientId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ClientId", clientId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Client not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Client client)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ClientId",client.ClientId);
      

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
      String sql = "select 1 from Client";

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

      public static Client Load(IDataReader dataReader)
      {
      Client client = new Client();

      client.ClientId = dataReader.GetInt32(0);
          client.ClientName = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            client.ClientAddress = dataReader.GetString(2);
          

      return client;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Client] "

      
        + " Where "
        
          + " ClientId = @ClientId "
        
      ;
      public static void Delete(Client client)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@ClientId", client.ClientId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Client] ";

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

      
        + " ClientId, "
      
        + " ClientName, "
      
        + " ClientAddress "
      

      + " From [Client] ";
      public static List<Client> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<Client> rv = new List<Client>();

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
      List<Client> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Client> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Client));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Client item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Client>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Client));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Client> itemsList
      = new List<Client>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Client)
      itemsList.Add(deserializedObject as Client);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_clientId;
      
        protected String m_clientName;
      
        protected String m_clientAddress;
      
      #endregion

      #region Constructors
      public Client(
      int 
          clientId
      )
      {
      
        m_clientId = clientId;
      
      }

      


        public Client(
        int 
          clientId,String 
          clientName,String 
          clientAddress
        )
        {
        
          m_clientId = clientId;
        
          m_clientName = clientName;
        
          m_clientAddress = clientAddress;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ClientId
        {
        get { return m_clientId;}
        set { m_clientId = value; }
        }
      
        [XmlElement]
        public String ClientName
        {
        get { return m_clientName;}
        set { m_clientName = value; }
        }
      
        [XmlElement]
        public String ClientAddress
        {
        get { return m_clientAddress;}
        set { m_clientAddress = value; }
        }
      
      }
      #endregion
      }

    