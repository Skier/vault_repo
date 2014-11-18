
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class ItemType
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ItemType ( " +
      
        " ItemTypeId, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ItemTypeId, " +
      
        " @Description " +
      
      ")";

      public static void Insert(ItemType itemType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@ItemTypeId", itemType.ItemTypeId);
      
        Database.PutParameter(dbCommand,"@Description", itemType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<ItemType>  itemTypeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(ItemType itemType in  itemTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ItemTypeId", itemType.ItemTypeId);
      
        Database.PutParameter(dbCommand,"@Description", itemType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ItemTypeId",itemType.ItemTypeId);
      
        Database.UpdateParameter(dbCommand,"@Description",itemType.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update ItemType Set "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ItemTypeId = @ItemTypeId "
        
      ;

      public static void Update(ItemType itemType)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@ItemTypeId", itemType.ItemTypeId);
      
        Database.PutParameter(dbCommand,"@Description", itemType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ItemTypeId, "
      
        + " Description "
      

      + " From ItemType "

      
        + " Where "
        
          + " ItemTypeId = @ItemTypeId "
        
      ;

      public static ItemType FindByPrimaryKey(
      int itemTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ItemTypeId", itemTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ItemType not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(ItemType itemType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ItemTypeId",itemType.ItemTypeId);
      

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
      String sql = "select 1 from ItemType";

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

      public static ItemType Load(IDataReader dataReader)
      {
      ItemType itemType = new ItemType();

      itemType.ItemTypeId = dataReader.GetInt32(0);
          itemType.Description = dataReader.GetString(1);
          

      return itemType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ItemType "

      
        + " Where "
        
          + " ItemTypeId = @ItemTypeId "
        
      ;
      public static void Delete(ItemType itemType)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@ItemTypeId", itemType.ItemTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From ItemType ";

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

      
        + " ItemTypeId, "
      
        + " Description "
      

      + " From ItemType ";
      public static List<ItemType> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<ItemType> rv = new List<ItemType>();

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
        List<ItemType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<ItemType> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(ItemType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ItemType>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemType));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<ItemType> itemsList
      = new List<ItemType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ItemType)
        itemsList.Add(deserializedObject as ItemType);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_itemTypeId;
        
          protected String m_description;
        
        #endregion
        
        #region Constructors
        public ItemType(
        int 
          itemTypeId
         )
        {
        
          m_itemTypeId = itemTypeId;
        
        }
        
        


        public ItemType(
        int 
          itemTypeId,String 
          description
        )
        {
        
          m_itemTypeId = itemTypeId;
        
          m_description = description;
        
          }


        
      #endregion

      
        [XmlElement]
        public int ItemTypeId
        {
          get { return m_itemTypeId;}
          set { m_itemTypeId = value; }
        }
      
        [XmlElement]
        public String Description
        {
          get { return m_description;}
          set { m_description = value; }
        }
      
      }
      #endregion
      }

    