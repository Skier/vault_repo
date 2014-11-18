
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class CustomerTransaction
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into CustomerTransaction ( " +
      
        " SessionId, " +
      
        " BusinessTransactionId, " +
      
        " CustomerTransactionTypeId, " +
      
        " CustomerVisitId " +
      
      ") Values (" +
      
        " @SessionId, " +
      
        " @BusinessTransactionId, " +
      
        " @CustomerTransactionTypeId, " +
      
        " @CustomerVisitId " +
      
      ")";

      public static void Insert(CustomerTransaction customerTransaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", customerTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", customerTransaction.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@CustomerTransactionTypeId", customerTransaction.CustomerTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@CustomerVisitId", customerTransaction.CustomerVisitId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<CustomerTransaction>  customerTransactionList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(CustomerTransaction customerTransaction in  customerTransactionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@SessionId", customerTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", customerTransaction.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@CustomerTransactionTypeId", customerTransaction.CustomerTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@CustomerVisitId", customerTransaction.CustomerVisitId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@SessionId",customerTransaction.SessionId);
      
        Database.UpdateParameter(dbCommand,"@BusinessTransactionId",customerTransaction.BusinessTransactionId);
      
        Database.UpdateParameter(dbCommand,"@CustomerTransactionTypeId",customerTransaction.CustomerTransactionTypeId);
      
        Database.UpdateParameter(dbCommand,"@CustomerVisitId",customerTransaction.CustomerVisitId);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update CustomerTransaction Set "
      
        + " CustomerTransactionTypeId = @CustomerTransactionTypeId, "
      
        + " CustomerVisitId = @CustomerVisitId "
      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId "
        
      ;

      public static void Update(CustomerTransaction customerTransaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", customerTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", customerTransaction.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@CustomerTransactionTypeId", customerTransaction.CustomerTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@CustomerVisitId", customerTransaction.CustomerVisitId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " SessionId, "
      
        + " BusinessTransactionId, "
      
        + " CustomerTransactionTypeId, "
      
        + " CustomerVisitId "
      

      + " From CustomerTransaction "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId "
        
      ;

      public static CustomerTransaction FindByPrimaryKey(
      long sessionId,int businessTransactionId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", sessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", businessTransactionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("CustomerTransaction not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(CustomerTransaction customerTransaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId",customerTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId",customerTransaction.BusinessTransactionId);
      

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
      String sql = "select 1 from CustomerTransaction";

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

      public static CustomerTransaction Load(IDataReader dataReader)
      {
      CustomerTransaction customerTransaction = new CustomerTransaction();

      customerTransaction.SessionId = dataReader.GetInt64(0);
          customerTransaction.BusinessTransactionId = dataReader.GetInt32(1);
          customerTransaction.CustomerTransactionTypeId = dataReader.GetInt16(2);
          customerTransaction.CustomerVisitId = dataReader.GetInt32(3);
          

      return customerTransaction;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From CustomerTransaction "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId "
        
      ;
      public static void Delete(CustomerTransaction customerTransaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@SessionId", customerTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", customerTransaction.BusinessTransactionId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From CustomerTransaction ";

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

      
        + " SessionId, "
      
        + " BusinessTransactionId, "
      
        + " CustomerTransactionTypeId, "
      
        + " CustomerVisitId "
      

      + " From CustomerTransaction ";
      public static List<CustomerTransaction> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<CustomerTransaction> rv = new List<CustomerTransaction>();

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
        List<CustomerTransaction> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<CustomerTransaction> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerTransaction));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(CustomerTransaction item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CustomerTransaction>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerTransaction));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<CustomerTransaction> itemsList
      = new List<CustomerTransaction>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CustomerTransaction)
        itemsList.Add(deserializedObject as CustomerTransaction);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected long m_sessionId;
        
          protected int m_businessTransactionId;
        
          protected int m_customerTransactionTypeId;
        
          protected int m_customerVisitId;
        
        #endregion
        
        #region Constructors
        public CustomerTransaction(
        long 
          sessionId,int 
          businessTransactionId
         )
        {
        
          m_sessionId = sessionId;
        
          m_businessTransactionId = businessTransactionId;
        
        }
        
        


        public CustomerTransaction(
        long 
          sessionId,int 
          businessTransactionId,int 
          customerTransactionTypeId,int 
          customerVisitId
        )
        {
        
          m_sessionId = sessionId;
        
          m_businessTransactionId = businessTransactionId;
        
          m_customerTransactionTypeId = customerTransactionTypeId;
        
          m_customerVisitId = customerVisitId;
        
          }


        
      #endregion

      
        [XmlElement]
        public long SessionId
        {
          get { return m_sessionId;}
          set { m_sessionId = value; }
        }
      
        [XmlElement]
        public int BusinessTransactionId
        {
          get { return m_businessTransactionId;}
          set { m_businessTransactionId = value; }
        }
      
        [XmlElement]
        public int CustomerTransactionTypeId
        {
          get { return m_customerTransactionTypeId;}
          set { m_customerTransactionTypeId = value; }
        }
      
        [XmlElement]
        public int CustomerVisitId
        {
          get { return m_customerVisitId;}
          set { m_customerVisitId = value; }
        }
      
      }
      #endregion
      }

    