
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class Item
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Item ( " +
      
        " LocationId, " +
      
        " RouteNumber, " +
      
        " ItemNumber, " +
      
        " ItemCategoryId, " +
      
        " ItemTypeId, " +
      
        " Name, " +
      
        " Description, " +
      
        " NameSortIndex, " +
      
        " ItemNumberSortIndex " +
      
      ") Values (" +
      
        " @LocationId, " +
      
        " @RouteNumber, " +
      
        " @ItemNumber, " +
      
        " @ItemCategoryId, " +
      
        " @ItemTypeId, " +
      
        " @Name, " +
      
        " @Description, " +
      
        " @NameSortIndex, " +
      
        " @ItemNumberSortIndex " +
      
      ")";

      public static void Insert(Item item)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", item.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", item.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", item.ItemNumber);
      
        Database.PutParameter(dbCommand,"@ItemCategoryId", item.ItemCategoryId);
      
        Database.PutParameter(dbCommand,"@ItemTypeId", item.ItemTypeId);
      
        Database.PutParameter(dbCommand,"@Name", item.Name);
      
        Database.PutParameter(dbCommand,"@Description", item.Description);
      
        Database.PutParameter(dbCommand,"@NameSortIndex", item.NameSortIndex);
      
        Database.PutParameter(dbCommand,"@ItemNumberSortIndex", item.ItemNumberSortIndex);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Item>  itemList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Item item in  itemList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@LocationId", item.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", item.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", item.ItemNumber);
      
        Database.PutParameter(dbCommand,"@ItemCategoryId", item.ItemCategoryId);
      
        Database.PutParameter(dbCommand,"@ItemTypeId", item.ItemTypeId);
      
        Database.PutParameter(dbCommand,"@Name", item.Name);
      
        Database.PutParameter(dbCommand,"@Description", item.Description);
      
        Database.PutParameter(dbCommand,"@NameSortIndex", item.NameSortIndex);
      
        Database.PutParameter(dbCommand,"@ItemNumberSortIndex", item.ItemNumberSortIndex);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@LocationId",item.LocationId);
      
        Database.UpdateParameter(dbCommand,"@RouteNumber",item.RouteNumber);
      
        Database.UpdateParameter(dbCommand,"@ItemNumber",item.ItemNumber);
      
        Database.UpdateParameter(dbCommand,"@ItemCategoryId",item.ItemCategoryId);
      
        Database.UpdateParameter(dbCommand,"@ItemTypeId",item.ItemTypeId);
      
        Database.UpdateParameter(dbCommand,"@Name",item.Name);
      
        Database.UpdateParameter(dbCommand,"@Description",item.Description);
      
        Database.UpdateParameter(dbCommand,"@NameSortIndex",item.NameSortIndex);
      
        Database.UpdateParameter(dbCommand,"@ItemNumberSortIndex",item.ItemNumberSortIndex);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update Item Set "
      
        + " ItemCategoryId = @ItemCategoryId, "
      
        + " ItemTypeId = @ItemTypeId, "
      
        + " Name = @Name, "
      
        + " Description = @Description, "
      
        + " NameSortIndex = @NameSortIndex, "
      
        + " ItemNumberSortIndex = @ItemNumberSortIndex "
      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " ItemNumber = @ItemNumber "
        
      ;

      public static void Update(Item item)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", item.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", item.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", item.ItemNumber);
      
        Database.PutParameter(dbCommand,"@ItemCategoryId", item.ItemCategoryId);
      
        Database.PutParameter(dbCommand,"@ItemTypeId", item.ItemTypeId);
      
        Database.PutParameter(dbCommand,"@Name", item.Name);
      
        Database.PutParameter(dbCommand,"@Description", item.Description);
      
        Database.PutParameter(dbCommand,"@NameSortIndex", item.NameSortIndex);
      
        Database.PutParameter(dbCommand,"@ItemNumberSortIndex", item.ItemNumberSortIndex);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " LocationId, "
      
        + " RouteNumber, "
      
        + " ItemNumber, "
      
        + " ItemCategoryId, "
      
        + " ItemTypeId, "
      
        + " Name, "
      
        + " Description, "
      
        + " NameSortIndex, "
      
        + " ItemNumberSortIndex "
      

      + " From Item "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " ItemNumber = @ItemNumber "
        
      ;

      public static Item FindByPrimaryKey(
      int locationId,int routeNumber,String itemNumber
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", locationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", itemNumber);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Item not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Item item)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId",item.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber",item.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber",item.ItemNumber);
      

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
      String sql = "select 1 from Item";

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

      public static Item Load(IDataReader dataReader)
      {
      Item item = new Item();

      item.LocationId = dataReader.GetInt32(0);
          item.RouteNumber = dataReader.GetInt32(1);
          item.ItemNumber = dataReader.GetString(2);
          item.ItemCategoryId = dataReader.GetInt32(3);
          item.ItemTypeId = dataReader.GetInt32(4);
          item.Name = dataReader.GetString(5);
          item.Description = dataReader.GetString(6);
          item.NameSortIndex = dataReader.GetInt32(7);
          item.ItemNumberSortIndex = dataReader.GetInt32(8);
          

      return item;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Item "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " ItemNumber = @ItemNumber "
        
      ;
      public static void Delete(Item item)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@LocationId", item.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", item.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", item.ItemNumber);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From Item ";

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

      
        + " LocationId, "
      
        + " RouteNumber, "
      
        + " ItemNumber, "
      
        + " ItemCategoryId, "
      
        + " ItemTypeId, "
      
        + " Name, "
      
        + " Description, "
      
        + " NameSortIndex, "
      
        + " ItemNumberSortIndex "
      

      + " From Item ";
      public static List<Item> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<Item> rv = new List<Item>();

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
        List<Item> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<Item> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Item));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(Item item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Item>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Item));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<Item> itemsList
      = new List<Item>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Item)
        itemsList.Add(deserializedObject as Item);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_locationId;
        
          protected int m_routeNumber;
        
          protected String m_itemNumber;
        
          protected int m_itemCategoryId;
        
          protected int m_itemTypeId;
        
          protected String m_name;
        
          protected String m_description;
        
          protected int m_nameSortIndex;
        
          protected int m_itemNumberSortIndex;
        
        #endregion
        
        #region Constructors
        public Item(
        int 
          locationId,int 
          routeNumber,String 
          itemNumber
         )
        {
        
          m_locationId = locationId;
        
          m_routeNumber = routeNumber;
        
          m_itemNumber = itemNumber;
        
        }
        
        


        public Item(
        int 
          locationId,int 
          routeNumber,String 
          itemNumber,int 
          itemCategoryId,int 
          itemTypeId,String 
          name,String 
          description,int 
          nameSortIndex,int 
          itemNumberSortIndex
        )
        {
        
          m_locationId = locationId;
        
          m_routeNumber = routeNumber;
        
          m_itemNumber = itemNumber;
        
          m_itemCategoryId = itemCategoryId;
        
          m_itemTypeId = itemTypeId;
        
          m_name = name;
        
          m_description = description;
        
          m_nameSortIndex = nameSortIndex;
        
          m_itemNumberSortIndex = itemNumberSortIndex;
        
          }


        
      #endregion

      
        [XmlElement]
        public int LocationId
        {
          get { return m_locationId;}
          set { m_locationId = value; }
        }
      
        [XmlElement]
        public int RouteNumber
        {
          get { return m_routeNumber;}
          set { m_routeNumber = value; }
        }
      
        [XmlElement]
        public String ItemNumber
        {
          get { return m_itemNumber;}
          set { m_itemNumber = value; }
        }
      
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
      
        [XmlElement]
        public int NameSortIndex
        {
          get { return m_nameSortIndex;}
          set { m_nameSortIndex = value; }
        }
      
        [XmlElement]
        public int ItemNumberSortIndex
        {
          get { return m_itemNumberSortIndex;}
          set { m_itemNumberSortIndex = value; }
        }
      
      }
      #endregion
      }

    