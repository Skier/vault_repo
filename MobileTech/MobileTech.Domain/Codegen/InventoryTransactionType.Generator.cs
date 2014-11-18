
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class InventoryTransactionType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into InventoryTransactionType ( " +
      
        " InventoryTransactionTypeId, " +
      
        " Name " +
      
      ") Values (" +
      
        " @InventoryTransactionTypeId, " +
      
        " @Name " +
      
      ")";

      public static void Insert(InventoryTransactionType inventoryTransactionType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransactionType.InventoryTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@Name", inventoryTransactionType.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<InventoryTransactionType>  inventoryTransactionTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(InventoryTransactionType inventoryTransactionType in  inventoryTransactionTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransactionType.InventoryTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@Name", inventoryTransactionType.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@InventoryTransactionTypeId",inventoryTransactionType.InventoryTransactionTypeId);
      
        Database.UpdateParameter(dbCommand,"@Name",inventoryTransactionType.Name);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update InventoryTransactionType Set "
      
        + " Name = @Name "
      
        + " Where "
        
          + " InventoryTransactionTypeId = @InventoryTransactionTypeId "
        
      ;

      public static void Update(InventoryTransactionType inventoryTransactionType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransactionType.InventoryTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@Name", inventoryTransactionType.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " InventoryTransactionTypeId, "
      
        + " Name "
      

      + " From InventoryTransactionType "

      
        + " Where "
        
          + " InventoryTransactionTypeId = @InventoryTransactionTypeId "
        
      ;

      public static InventoryTransactionType FindByPrimaryKey(
      int inventoryTransactionTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransactionTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("InventoryTransactionType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(InventoryTransactionType inventoryTransactionType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId",inventoryTransactionType.InventoryTransactionTypeId);
      

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
      String sql = "select 1 from InventoryTransactionType";

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

      public static InventoryTransactionType Load(IDataReader dataReader)
      {
      InventoryTransactionType inventoryTransactionType = new InventoryTransactionType();

      inventoryTransactionType.InventoryTransactionTypeId = dataReader.GetInt16(0);
          inventoryTransactionType.Name = dataReader.GetString(1);
          

      return inventoryTransactionType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From InventoryTransactionType "

      
        + " Where "
        
          + " InventoryTransactionTypeId = @InventoryTransactionTypeId "
        
      ;
      public static void Delete(InventoryTransactionType inventoryTransactionType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransactionType.InventoryTransactionTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From InventoryTransactionType ";

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

      
        + " InventoryTransactionTypeId, "
      
        + " Name "
      

      + " From InventoryTransactionType ";
      public static List<InventoryTransactionType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<InventoryTransactionType> rv = new List<InventoryTransactionType>();

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
        List<InventoryTransactionType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<InventoryTransactionType> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryTransactionType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(InventoryTransactionType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<InventoryTransactionType>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryTransactionType));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<InventoryTransactionType> itemsList
      = new List<InventoryTransactionType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is InventoryTransactionType)
        itemsList.Add(deserializedObject as InventoryTransactionType);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_inventoryTransactionTypeId;
        
          protected String m_name;
        
        #endregion
        
        #region Constructors
        public InventoryTransactionType(
        int 
          inventoryTransactionTypeId
         )
        {
        
          m_inventoryTransactionTypeId = inventoryTransactionTypeId;
        
        }
        
        


        public InventoryTransactionType(
        int 
          inventoryTransactionTypeId,String 
          name
        )
        {
        
          m_inventoryTransactionTypeId = inventoryTransactionTypeId;
        
          m_name = name;
        
          }


        
      #endregion

      
        [XmlElement]
        public int InventoryTransactionTypeId
        {
          get { return m_inventoryTransactionTypeId;}
          set { m_inventoryTransactionTypeId = value; }
        }
      
        [XmlElement]
        public String Name
        {
          get { return m_name;}
          set { m_name = value; }
        }
      
      }
      #endregion
      }

    