
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


      public partial class ClientContact
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [ClientContact] ( " +
      
        " PersonId, " +
      
        " ContractId, " +
      
        " StartDate, " +
      
        " EndDate " +
      
      ") Values (" +
      
        " @PersonId, " +
      
        " @ContractId, " +
      
        " @StartDate, " +
      
        " @EndDate " +
      
      ")";

      public static void Insert(ClientContact clientContact)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@PersonId", clientContact.PersonId);
      
        Database.PutParameter(dbCommand,"@ContractId", clientContact.ContractId);
      
        Database.PutParameter(dbCommand,"@StartDate", clientContact.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", clientContact.EndDate);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          clientContact.ClientContactId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<ClientContact>  clientContactList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(ClientContact clientContact in  clientContactList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@PersonId", clientContact.PersonId);
      
        Database.PutParameter(dbCommand,"@ContractId", clientContact.ContractId);
      
        Database.PutParameter(dbCommand,"@StartDate", clientContact.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", clientContact.EndDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@PersonId",clientContact.PersonId);
      
        Database.UpdateParameter(dbCommand,"@ContractId",clientContact.ContractId);
      
        Database.UpdateParameter(dbCommand,"@StartDate",clientContact.StartDate);
      
        Database.UpdateParameter(dbCommand,"@EndDate",clientContact.EndDate);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        clientContact.ClientContactId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [ClientContact] Set "
      
        + " PersonId = @PersonId, "
      
        + " ContractId = @ContractId, "
      
        + " StartDate = @StartDate, "
      
        + " EndDate = @EndDate "
      
        + " Where "
        
          + " ClientContactId = @ClientContactId "
        
      ;

      public static void Update(ClientContact clientContact)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@ClientContactId", clientContact.ClientContactId);
      
        Database.PutParameter(dbCommand,"@PersonId", clientContact.PersonId);
      
        Database.PutParameter(dbCommand,"@ContractId", clientContact.ContractId);
      
        Database.PutParameter(dbCommand,"@StartDate", clientContact.StartDate);
      
        Database.PutParameter(dbCommand,"@EndDate", clientContact.EndDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ClientContactId, "
      
        + " PersonId, "
      
        + " ContractId, "
      
        + " StartDate, "
      
        + " EndDate "
      

      + " From [ClientContact] "

      
        + " Where "
        
          + " ClientContactId = @ClientContactId "
        
      ;

      public static ClientContact FindByPrimaryKey(
      int clientContactId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ClientContactId", clientContactId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ClientContact not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(ClientContact clientContact)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ClientContactId",clientContact.ClientContactId);
      

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
      String sql = "select 1 from ClientContact";

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

      public static ClientContact Load(IDataReader dataReader)
      {
      ClientContact clientContact = new ClientContact();

      clientContact.ClientContactId = dataReader.GetInt32(0);
          clientContact.PersonId = dataReader.GetInt32(1);
          clientContact.ContractId = dataReader.GetInt32(2);
          clientContact.StartDate = dataReader.GetDateTime(3);
          if ( dataReader[4] != DBNull.Value ) {
            clientContact.EndDate = dataReader.GetDateTime(4);
          }
          

      return clientContact;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [ClientContact] "

      
        + " Where "
        
          + " ClientContactId = @ClientContactId "
        
      ;
      public static void Delete(ClientContact clientContact)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@ClientContactId", clientContact.ClientContactId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [ClientContact] ";

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

      
        + " ClientContactId, "
      
        + " PersonId, "
      
        + " ContractId, "
      
        + " StartDate, "
      
        + " EndDate "
      

      + " From [ClientContact] ";
      public static List<ClientContact> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<ClientContact> rv = new List<ClientContact>();

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
      List<ClientContact> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<ClientContact> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ClientContact));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ClientContact item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ClientContact>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ClientContact));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ClientContact> itemsList
      = new List<ClientContact>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ClientContact)
      itemsList.Add(deserializedObject as ClientContact);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_clientContactId;
      
        protected int m_personId;
      
        protected int m_contractId;
      
        protected DateTime m_startDate;
      
        protected DateTime m_endDate;
      
      #endregion

      #region Constructors
      public ClientContact(
      int 
          clientContactId
      )
      {
      
        m_clientContactId = clientContactId;
      
      }

      


        public ClientContact(
        int 
          clientContactId,int 
          personId,int 
          contractId,DateTime 
          startDate,DateTime 
          endDate
        )
        {
        
          m_clientContactId = clientContactId;
        
          m_personId = personId;
        
          m_contractId = contractId;
        
          m_startDate = startDate;
        
          m_endDate = endDate;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ClientContactId
        {
        get { return m_clientContactId;}
        set { m_clientContactId = value; }
        }
      
        [XmlElement]
        public int PersonId
        {
        get { return m_personId;}
        set { m_personId = value; }
        }
      
        [XmlElement]
        public int ContractId
        {
        get { return m_contractId;}
        set { m_contractId = value; }
        }
      
        [XmlElement]
        public DateTime StartDate
        {
        get { return m_startDate;}
        set { m_startDate = value; }
        }
      
        [XmlElement]
        public DateTime EndDate
        {
        get { return m_endDate;}
        set { m_endDate = value; }
        }
      
      }
      #endregion
      }

    