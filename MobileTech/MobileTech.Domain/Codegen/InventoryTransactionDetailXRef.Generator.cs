
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class InventoryTransactionDetailXRef
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into InventoryTransactionDetailXRef ( " +
      
        " InventoryTransactionDetailTypeId, " +
      
        " InventoryTransactionTypeId " +
      
      ") Values (" +
      
        " @InventoryTransactionDetailTypeId, " +
      
        " @InventoryTransactionTypeId " +
      
      ")";

      public static void Insert(InventoryTransactionDetailXRef inventoryTransactionDetailXRef)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetailXRef.InventoryTransactionDetailTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransactionDetailXRef.InventoryTransactionTypeId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<InventoryTransactionDetailXRef>  inventoryTransactionDetailXRefList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(InventoryTransactionDetailXRef inventoryTransactionDetailXRef in  inventoryTransactionDetailXRefList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetailXRef.InventoryTransactionDetailTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransactionDetailXRef.InventoryTransactionTypeId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@InventoryTransactionDetailTypeId",inventoryTransactionDetailXRef.InventoryTransactionDetailTypeId);
      
        Database.UpdateParameter(dbCommand,"@InventoryTransactionTypeId",inventoryTransactionDetailXRef.InventoryTransactionTypeId);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update InventoryTransactionDetailXRef Set "
      
        + " Where "
        
          + " InventoryTransactionDetailTypeId = @InventoryTransactionDetailTypeId and  "
        
          + " InventoryTransactionTypeId = @InventoryTransactionTypeId "
        
      ;

      public static void Update(InventoryTransactionDetailXRef inventoryTransactionDetailXRef)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetailXRef.InventoryTransactionDetailTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransactionDetailXRef.InventoryTransactionTypeId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " InventoryTransactionDetailTypeId, "
      
        + " InventoryTransactionTypeId "
      

      + " From InventoryTransactionDetailXRef "

      
        + " Where "
        
          + " InventoryTransactionDetailTypeId = @InventoryTransactionDetailTypeId and  "
        
          + " InventoryTransactionTypeId = @InventoryTransactionTypeId "
        
      ;

      public static InventoryTransactionDetailXRef FindByPrimaryKey(
      int inventoryTransactionDetailTypeId,int inventoryTransactionTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetailTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransactionTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("InventoryTransactionDetailXRef not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(InventoryTransactionDetailXRef inventoryTransactionDetailXRef)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId",inventoryTransactionDetailXRef.InventoryTransactionDetailTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId",inventoryTransactionDetailXRef.InventoryTransactionTypeId);
      

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
      String sql = "select 1 from InventoryTransactionDetailXRef";

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

      public static InventoryTransactionDetailXRef Load(IDataReader dataReader)
      {
      InventoryTransactionDetailXRef inventoryTransactionDetailXRef = new InventoryTransactionDetailXRef();

      inventoryTransactionDetailXRef.InventoryTransactionDetailTypeId = dataReader.GetInt16(0);
          inventoryTransactionDetailXRef.InventoryTransactionTypeId = dataReader.GetInt16(1);
          

      return inventoryTransactionDetailXRef;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From InventoryTransactionDetailXRef "

      
        + " Where "
        
          + " InventoryTransactionDetailTypeId = @InventoryTransactionDetailTypeId and  "
        
          + " InventoryTransactionTypeId = @InventoryTransactionTypeId "
        
      ;
      public static void Delete(InventoryTransactionDetailXRef inventoryTransactionDetailXRef)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetailXRef.InventoryTransactionDetailTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransactionDetailXRef.InventoryTransactionTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From InventoryTransactionDetailXRef ";

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

      
        + " InventoryTransactionDetailTypeId, "
      
        + " InventoryTransactionTypeId "
      

      + " From InventoryTransactionDetailXRef ";
      public static List<InventoryTransactionDetailXRef> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<InventoryTransactionDetailXRef> rv = new List<InventoryTransactionDetailXRef>();

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
        List<InventoryTransactionDetailXRef> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<InventoryTransactionDetailXRef> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryTransactionDetailXRef));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(InventoryTransactionDetailXRef item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<InventoryTransactionDetailXRef>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryTransactionDetailXRef));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<InventoryTransactionDetailXRef> itemsList
      = new List<InventoryTransactionDetailXRef>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is InventoryTransactionDetailXRef)
        itemsList.Add(deserializedObject as InventoryTransactionDetailXRef);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_inventoryTransactionDetailTypeId;
        
          protected int m_inventoryTransactionTypeId;
        
        #endregion
        
        #region Constructors
        public InventoryTransactionDetailXRef(
        int 
          inventoryTransactionDetailTypeId,int 
          inventoryTransactionTypeId
         )
        {
        
          m_inventoryTransactionDetailTypeId = inventoryTransactionDetailTypeId;
        
          m_inventoryTransactionTypeId = inventoryTransactionTypeId;
        
        }
        
        
      #endregion

      
        [XmlElement]
        public int InventoryTransactionDetailTypeId
        {
          get { return m_inventoryTransactionDetailTypeId;}
          set { m_inventoryTransactionDetailTypeId = value; }
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

    