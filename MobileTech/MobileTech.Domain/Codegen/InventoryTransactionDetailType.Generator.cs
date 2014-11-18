
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class InventoryTransactionDetailType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into InventoryTransactionDetailType ( " +
      
        " InventoryTransactionDetailTypeId, " +
      
        " Name " +
      
      ") Values (" +
      
        " @InventoryTransactionDetailTypeId, " +
      
        " @Name " +
      
      ")";

      public static void Insert(InventoryTransactionDetailType inventoryTransactionDetailType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetailType.InventoryTransactionDetailTypeId);
      
        Database.PutParameter(dbCommand,"@Name", inventoryTransactionDetailType.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<InventoryTransactionDetailType>  inventoryTransactionDetailTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(InventoryTransactionDetailType inventoryTransactionDetailType in  inventoryTransactionDetailTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetailType.InventoryTransactionDetailTypeId);
      
        Database.PutParameter(dbCommand,"@Name", inventoryTransactionDetailType.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@InventoryTransactionDetailTypeId",inventoryTransactionDetailType.InventoryTransactionDetailTypeId);
      
        Database.UpdateParameter(dbCommand,"@Name",inventoryTransactionDetailType.Name);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update InventoryTransactionDetailType Set "
      
        + " Name = @Name "
      
        + " Where "
        
          + " InventoryTransactionDetailTypeId = @InventoryTransactionDetailTypeId "
        
      ;

      public static void Update(InventoryTransactionDetailType inventoryTransactionDetailType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetailType.InventoryTransactionDetailTypeId);
      
        Database.PutParameter(dbCommand,"@Name", inventoryTransactionDetailType.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " InventoryTransactionDetailTypeId, "
      
        + " Name "
      

      + " From InventoryTransactionDetailType "

      
        + " Where "
        
          + " InventoryTransactionDetailTypeId = @InventoryTransactionDetailTypeId "
        
      ;

      public static InventoryTransactionDetailType FindByPrimaryKey(
      int inventoryTransactionDetailTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetailTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("InventoryTransactionDetailType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(InventoryTransactionDetailType inventoryTransactionDetailType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId",inventoryTransactionDetailType.InventoryTransactionDetailTypeId);
      

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
      String sql = "select 1 from InventoryTransactionDetailType";

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

      public static InventoryTransactionDetailType Load(IDataReader dataReader)
      {
      InventoryTransactionDetailType inventoryTransactionDetailType = new InventoryTransactionDetailType();

      inventoryTransactionDetailType.InventoryTransactionDetailTypeId = dataReader.GetInt16(0);
          inventoryTransactionDetailType.Name = dataReader.GetString(1);
          

      return inventoryTransactionDetailType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From InventoryTransactionDetailType "

      
        + " Where "
        
          + " InventoryTransactionDetailTypeId = @InventoryTransactionDetailTypeId "
        
      ;
      public static void Delete(InventoryTransactionDetailType inventoryTransactionDetailType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetailType.InventoryTransactionDetailTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From InventoryTransactionDetailType ";

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
      
        + " Name "
      

      + " From InventoryTransactionDetailType ";
      public static List<InventoryTransactionDetailType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<InventoryTransactionDetailType> rv = new List<InventoryTransactionDetailType>();

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
        List<InventoryTransactionDetailType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<InventoryTransactionDetailType> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryTransactionDetailType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(InventoryTransactionDetailType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<InventoryTransactionDetailType>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryTransactionDetailType));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<InventoryTransactionDetailType> itemsList
      = new List<InventoryTransactionDetailType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is InventoryTransactionDetailType)
        itemsList.Add(deserializedObject as InventoryTransactionDetailType);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_inventoryTransactionDetailTypeId;
        
          protected String m_name;
        
        #endregion
        
        #region Constructors
        public InventoryTransactionDetailType(
        int 
          inventoryTransactionDetailTypeId
         )
        {
        
          m_inventoryTransactionDetailTypeId = inventoryTransactionDetailTypeId;
        
        }
        
        


        public InventoryTransactionDetailType(
        int 
          inventoryTransactionDetailTypeId,String 
          name
        )
        {
        
          m_inventoryTransactionDetailTypeId = inventoryTransactionDetailTypeId;
        
          m_name = name;
        
          }


        
      #endregion

      
        [XmlElement]
        public int InventoryTransactionDetailTypeId
        {
          get { return m_inventoryTransactionDetailTypeId;}
          set { m_inventoryTransactionDetailTypeId = value; }
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

    