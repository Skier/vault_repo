
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class InventoryTransaction
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into InventoryTransaction ( " +
      
        " SessionId, " +
      
        " BusinessTransactionId, " +
      
        " InventoryTransactionTypeId " +
      
      ") Values (" +
      
        " @SessionId, " +
      
        " @BusinessTransactionId, " +
      
        " @InventoryTransactionTypeId " +
      
      ")";

      public static void Insert(InventoryTransaction inventoryTransaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", inventoryTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", inventoryTransaction.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransaction.InventoryTransactionTypeId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<InventoryTransaction>  inventoryTransactionList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(InventoryTransaction inventoryTransaction in  inventoryTransactionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@SessionId", inventoryTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", inventoryTransaction.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransaction.InventoryTransactionTypeId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@SessionId",inventoryTransaction.SessionId);
      
        Database.UpdateParameter(dbCommand,"@BusinessTransactionId",inventoryTransaction.BusinessTransactionId);
      
        Database.UpdateParameter(dbCommand,"@InventoryTransactionTypeId",inventoryTransaction.InventoryTransactionTypeId);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update InventoryTransaction Set "
      
        + " InventoryTransactionTypeId = @InventoryTransactionTypeId "
      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId "
        
      ;

      public static void Update(InventoryTransaction inventoryTransaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", inventoryTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", inventoryTransaction.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransaction.InventoryTransactionTypeId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " SessionId, "
      
        + " BusinessTransactionId, "
      
        + " InventoryTransactionTypeId "
      

      + " From InventoryTransaction "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId "
        
      ;

      public static InventoryTransaction FindByPrimaryKey(
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
      throw new DataNotFoundException("InventoryTransaction not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(InventoryTransaction inventoryTransaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId",inventoryTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId",inventoryTransaction.BusinessTransactionId);
      

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
      String sql = "select 1 from InventoryTransaction";

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

      public static InventoryTransaction Load(IDataReader dataReader)
      {
      InventoryTransaction inventoryTransaction = new InventoryTransaction();

      inventoryTransaction.SessionId = dataReader.GetInt64(0);
          inventoryTransaction.BusinessTransactionId = dataReader.GetInt32(1);
          inventoryTransaction.InventoryTransactionTypeId = dataReader.GetInt16(2);
          

      return inventoryTransaction;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From InventoryTransaction "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId "
        
      ;
      public static void Delete(InventoryTransaction inventoryTransaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@SessionId", inventoryTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", inventoryTransaction.BusinessTransactionId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From InventoryTransaction ";

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
      
        + " InventoryTransactionTypeId "
      

      + " From InventoryTransaction ";
      public static List<InventoryTransaction> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<InventoryTransaction> rv = new List<InventoryTransaction>();

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
        List<InventoryTransaction> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<InventoryTransaction> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryTransaction));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(InventoryTransaction item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<InventoryTransaction>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryTransaction));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<InventoryTransaction> itemsList
      = new List<InventoryTransaction>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is InventoryTransaction)
        itemsList.Add(deserializedObject as InventoryTransaction);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected long m_sessionId;
        
          protected int m_businessTransactionId;
        
          protected int m_inventoryTransactionTypeId;
        
        #endregion
        
        #region Constructors
        public InventoryTransaction(
        long 
          sessionId,int 
          businessTransactionId
         )
        {
        
          m_sessionId = sessionId;
        
          m_businessTransactionId = businessTransactionId;
        
        }
        
        


        public InventoryTransaction(
        long 
          sessionId,int 
          businessTransactionId,int 
          inventoryTransactionTypeId
        )
        {
        
          m_sessionId = sessionId;
        
          m_businessTransactionId = businessTransactionId;
        
          m_inventoryTransactionTypeId = inventoryTransactionTypeId;
        
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
        public int InventoryTransactionTypeId
        {
          get { return m_inventoryTransactionTypeId;}
          set { m_inventoryTransactionTypeId = value; }
        }
      
      }
      #endregion
      }

    