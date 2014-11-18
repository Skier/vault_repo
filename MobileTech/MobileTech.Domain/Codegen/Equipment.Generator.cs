
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class Equipment
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Equipment ( " +
      
        " LocationId, " +
      
        " RouteNumber, " +
      
        " ItemNumber " +
      
      ") Values (" +
      
        " @LocationId, " +
      
        " @RouteNumber, " +
      
        " @ItemNumber " +
      
      ")";

      public static void Insert(Equipment equipment)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", equipment.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", equipment.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", equipment.ItemNumber);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Equipment>  equipmentList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Equipment equipment in  equipmentList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@LocationId", equipment.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", equipment.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", equipment.ItemNumber);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@LocationId",equipment.LocationId);
      
        Database.UpdateParameter(dbCommand,"@RouteNumber",equipment.RouteNumber);
      
        Database.UpdateParameter(dbCommand,"@ItemNumber",equipment.ItemNumber);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update Equipment Set "
      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " ItemNumber = @ItemNumber "
        
      ;

      public static void Update(Equipment equipment)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", equipment.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", equipment.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", equipment.ItemNumber);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " LocationId, "
      
        + " RouteNumber, "
      
        + " ItemNumber "
      

      + " From Equipment "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " ItemNumber = @ItemNumber "
        
      ;

      public static Equipment FindByPrimaryKey(
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
      throw new DataNotFoundException("Equipment not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Equipment equipment)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId",equipment.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber",equipment.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber",equipment.ItemNumber);
      

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
      String sql = "select 1 from Equipment";

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

      public static Equipment Load(IDataReader dataReader)
      {
      Equipment equipment = new Equipment();

      equipment.LocationId = dataReader.GetInt32(0);
          equipment.RouteNumber = dataReader.GetInt32(1);
          equipment.ItemNumber = dataReader.GetString(2);
          

      return equipment;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Equipment "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " ItemNumber = @ItemNumber "
        
      ;
      public static void Delete(Equipment equipment)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@LocationId", equipment.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", equipment.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", equipment.ItemNumber);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From Equipment ";

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
      
        + " ItemNumber "
      

      + " From Equipment ";
      public static List<Equipment> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<Equipment> rv = new List<Equipment>();

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
        List<Equipment> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<Equipment> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Equipment));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(Equipment item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Equipment>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Equipment));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<Equipment> itemsList
      = new List<Equipment>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Equipment)
        itemsList.Add(deserializedObject as Equipment);
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
        
        #endregion
        
        #region Constructors
        public Equipment(
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
      
      }
      #endregion
      }

    