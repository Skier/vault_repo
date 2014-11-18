
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class ItemCategory
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ItemCategory ( " +
      
        " ItemCategoryId, " +
      
        " ItemTypeId, " +
      
        " Name, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ItemCategoryId, " +
      
        " @ItemTypeId, " +
      
        " @Name, " +
      
        " @Description " +
      
      ")";

      public static void Insert(ItemCategory itemCategory)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@ItemCategoryId", itemCategory.ItemCategoryId);
      
        Database.PutParameter(dbCommand,"@ItemTypeId", itemCategory.ItemTypeId);
      
        Database.PutParameter(dbCommand,"@Name", itemCategory.Name);
      
        Database.PutParameter(dbCommand,"@Description", itemCategory.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<ItemCategory>  itemCategoryList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(ItemCategory itemCategory in  itemCategoryList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ItemCategoryId", itemCategory.ItemCategoryId);
      
        Database.PutParameter(dbCommand,"@ItemTypeId", itemCategory.ItemTypeId);
      
        Database.PutParameter(dbCommand,"@Name", itemCategory.Name);
      
        Database.PutParameter(dbCommand,"@Description", itemCategory.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ItemCategoryId",itemCategory.ItemCategoryId);
      
        Database.UpdateParameter(dbCommand,"@ItemTypeId",itemCategory.ItemTypeId);
      
        Database.UpdateParameter(dbCommand,"@Name",itemCategory.Name);
      
        Database.UpdateParameter(dbCommand,"@Description",itemCategory.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update ItemCategory Set "
      
        + " Name = @Name, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ItemCategoryId = @ItemCategoryId and  "
        
          + " ItemTypeId = @ItemTypeId "
        
      ;

      public static void Update(ItemCategory itemCategory)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@ItemCategoryId", itemCategory.ItemCategoryId);
      
        Database.PutParameter(dbCommand,"@ItemTypeId", itemCategory.ItemTypeId);
      
        Database.PutParameter(dbCommand,"@Name", itemCategory.Name);
      
        Database.PutParameter(dbCommand,"@Description", itemCategory.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ItemCategoryId, "
      
        + " ItemTypeId, "
      
        + " Name, "
      
        + " Description "
      

      + " From ItemCategory "

      
        + " Where "
        
          + " ItemCategoryId = @ItemCategoryId and  "
        
          + " ItemTypeId = @ItemTypeId "
        
      ;

      public static ItemCategory FindByPrimaryKey(
      int itemCategoryId,int itemTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ItemCategoryId", itemCategoryId);
      
        Database.PutParameter(dbCommand,"@ItemTypeId", itemTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ItemCategory not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(ItemCategory itemCategory)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ItemCategoryId",itemCategory.ItemCategoryId);
      
        Database.PutParameter(dbCommand,"@ItemTypeId",itemCategory.ItemTypeId);
      

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
      String sql = "select 1 from ItemCategory";

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

      public static ItemCategory Load(IDataReader dataReader)
      {
      ItemCategory itemCategory = new ItemCategory();

      itemCategory.ItemCategoryId = dataReader.GetInt32(0);
          itemCategory.ItemTypeId = dataReader.GetInt32(1);
          itemCategory.Name = dataReader.GetString(2);
          itemCategory.Description = dataReader.GetString(3);
          

      return itemCategory;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ItemCategory "

      
        + " Where "
        
          + " ItemCategoryId = @ItemCategoryId and  "
        
          + " ItemTypeId = @ItemTypeId "
        
      ;
      public static void Delete(ItemCategory itemCategory)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@ItemCategoryId", itemCategory.ItemCategoryId);
      
        Database.PutParameter(dbCommand,"@ItemTypeId", itemCategory.ItemTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From ItemCategory ";

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

      
        + " ItemCategoryId, "
      
        + " ItemTypeId, "
      
        + " Name, "
      
        + " Description "
      

      + " From ItemCategory ";
      public static List<ItemCategory> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<ItemCategory> rv = new List<ItemCategory>();

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
        List<ItemCategory> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<ItemCategory> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemCategory));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(ItemCategory item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ItemCategory>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemCategory));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<ItemCategory> itemsList
      = new List<ItemCategory>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ItemCategory)
        itemsList.Add(deserializedObject as ItemCategory);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_itemCategoryId;
        
          protected int m_itemTypeId;
        
          protected String m_name;
        
          protected String m_description;
        
        #endregion
        
        #region Constructors
        public ItemCategory(
        int 
          itemCategoryId,int 
          itemTypeId
         )
        {
        
          m_itemCategoryId = itemCategoryId;
        
          m_itemTypeId = itemTypeId;
        
        }
        
        


        public ItemCategory(
        int 
          itemCategoryId,int 
          itemTypeId,String 
          name,String 
          description
        )
        {
        
          m_itemCategoryId = itemCategoryId;
        
          m_itemTypeId = itemTypeId;
        
          m_name = name;
        
          m_description = description;
        
          }


        
      #endregion

      
        [XmlElement]
        public int ItemCategoryId
        {
          get { return m_itemCategoryId;}
          set { m_itemCategoryId = value; }
        }
      
        [XmlElement]
        public int ItemTypeId
        {
          get { return m_itemTypeId;}
          set { m_itemTypeId = value; }
        }
      
        [XmlElement]
        public String Name
        {
          get { return m_name;}
          set { m_name = value; }
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

    