
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class Route
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Route ( " +
      
        " LocationId, " +
      
        " RouteNumber, " +
      
        " RouteStatusId, " +
      
        " RouteTypeId, " +
      
        " EmployeeId, " +
      
        " Name, " +
      
        " DocumentNumberPrefix, " +
      
        " DocumentNumberSequence, " +
      
        " Active, " +
      
        " InventorySync " +
      
      ") Values (" +
      
        " @LocationId, " +
      
        " @RouteNumber, " +
      
        " @RouteStatusId, " +
      
        " @RouteTypeId, " +
      
        " @EmployeeId, " +
      
        " @Name, " +
      
        " @DocumentNumberPrefix, " +
      
        " @DocumentNumberSequence, " +
      
        " @Active, " +
      
        " @InventorySync " +
      
      ")";

      public static void Insert(Route route)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", route.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", route.RouteNumber);
      
        Database.PutParameter(dbCommand,"@RouteStatusId", route.RouteStatusId);
      
        Database.PutParameter(dbCommand,"@RouteTypeId", route.RouteTypeId);
      
        Database.PutParameter(dbCommand,"@EmployeeId", route.EmployeeId);
      
        Database.PutParameter(dbCommand,"@Name", route.Name);
      
        Database.PutParameter(dbCommand,"@DocumentNumberPrefix", route.DocumentNumberPrefix);
      
        Database.PutParameter(dbCommand,"@DocumentNumberSequence", route.DocumentNumberSequence);
      
        Database.PutParameter(dbCommand,"@Active", route.Active);
      
        Database.PutParameter(dbCommand,"@InventorySync", route.InventorySync);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Route>  routeList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Route route in  routeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@LocationId", route.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", route.RouteNumber);
      
        Database.PutParameter(dbCommand,"@RouteStatusId", route.RouteStatusId);
      
        Database.PutParameter(dbCommand,"@RouteTypeId", route.RouteTypeId);
      
        Database.PutParameter(dbCommand,"@EmployeeId", route.EmployeeId);
      
        Database.PutParameter(dbCommand,"@Name", route.Name);
      
        Database.PutParameter(dbCommand,"@DocumentNumberPrefix", route.DocumentNumberPrefix);
      
        Database.PutParameter(dbCommand,"@DocumentNumberSequence", route.DocumentNumberSequence);
      
        Database.PutParameter(dbCommand,"@Active", route.Active);
      
        Database.PutParameter(dbCommand,"@InventorySync", route.InventorySync);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@LocationId",route.LocationId);
      
        Database.UpdateParameter(dbCommand,"@RouteNumber",route.RouteNumber);
      
        Database.UpdateParameter(dbCommand,"@RouteStatusId",route.RouteStatusId);
      
        Database.UpdateParameter(dbCommand,"@RouteTypeId",route.RouteTypeId);
      
        Database.UpdateParameter(dbCommand,"@EmployeeId",route.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"@Name",route.Name);
      
        Database.UpdateParameter(dbCommand,"@DocumentNumberPrefix",route.DocumentNumberPrefix);
      
        Database.UpdateParameter(dbCommand,"@DocumentNumberSequence",route.DocumentNumberSequence);
      
        Database.UpdateParameter(dbCommand,"@Active",route.Active);
      
        Database.UpdateParameter(dbCommand,"@InventorySync",route.InventorySync);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update Route Set "
      
        + " RouteStatusId = @RouteStatusId, "
      
        + " RouteTypeId = @RouteTypeId, "
      
        + " EmployeeId = @EmployeeId, "
      
        + " Name = @Name, "
      
        + " DocumentNumberPrefix = @DocumentNumberPrefix, "
      
        + " DocumentNumberSequence = @DocumentNumberSequence, "
      
        + " Active = @Active, "
      
        + " InventorySync = @InventorySync "
      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber "
        
      ;

      public static void Update(Route route)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", route.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", route.RouteNumber);
      
        Database.PutParameter(dbCommand,"@RouteStatusId", route.RouteStatusId);
      
        Database.PutParameter(dbCommand,"@RouteTypeId", route.RouteTypeId);
      
        Database.PutParameter(dbCommand,"@EmployeeId", route.EmployeeId);
      
        Database.PutParameter(dbCommand,"@Name", route.Name);
      
        Database.PutParameter(dbCommand,"@DocumentNumberPrefix", route.DocumentNumberPrefix);
      
        Database.PutParameter(dbCommand,"@DocumentNumberSequence", route.DocumentNumberSequence);
      
        Database.PutParameter(dbCommand,"@Active", route.Active);
      
        Database.PutParameter(dbCommand,"@InventorySync", route.InventorySync);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " LocationId, "
      
        + " RouteNumber, "
      
        + " RouteStatusId, "
      
        + " RouteTypeId, "
      
        + " EmployeeId, "
      
        + " Name, "
      
        + " DocumentNumberPrefix, "
      
        + " DocumentNumberSequence, "
      
        + " Active, "
      
        + " InventorySync "
      

      + " From Route "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber "
        
      ;

      public static Route FindByPrimaryKey(
      int locationId,int routeNumber
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", locationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeNumber);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Route not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Route route)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId",route.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber",route.RouteNumber);
      

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

#if WIN32
          if(!Database.IsSchemaExist())
            return false;
#endif
      String sql = "select 1 from Route";

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

      public static Route Load(IDataReader dataReader)
      {
      Route route = new Route();

      route.LocationId = dataReader.GetInt32(0);
          route.RouteNumber = dataReader.GetInt32(1);
          route.RouteStatusId = dataReader.GetInt16(2);
          route.RouteTypeId = dataReader.GetInt16(3);
          route.EmployeeId = dataReader.GetInt32(4);
          
            if(!dataReader.IsDBNull(5))
            route.Name = dataReader.GetString(5);
          route.DocumentNumberPrefix = dataReader.GetInt32(6);
          route.DocumentNumberSequence = dataReader.GetInt32(7);
          route.Active = dataReader.GetBoolean(8);
          route.InventorySync = dataReader.GetBoolean(9);
          

      return route;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Route "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber "
        
      ;
      public static void Delete(Route route)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@LocationId", route.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", route.RouteNumber);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From Route ";

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
      
        + " RouteStatusId, "
      
        + " RouteTypeId, "
      
        + " EmployeeId, "
      
        + " Name, "
      
        + " DocumentNumberPrefix, "
      
        + " DocumentNumberSequence, "
      
        + " Active, "
      
        + " InventorySync "
      

      + " From Route ";
      public static List<Route> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<Route> rv = new List<Route>();

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
        List<Route> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<Route> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Route));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(Route item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Route>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(Route));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<Route> itemsList
      = new List<Route>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Route)
        itemsList.Add(deserializedObject as Route);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_locationId;
        
          protected int m_routeNumber;
        
          protected int m_routeStatusId;
        
          protected int m_routeTypeId;
        
          protected int m_employeeId;
        
          protected String m_name;
        
          protected int m_documentNumberPrefix;
        
          protected int m_documentNumberSequence;
        
          protected bool m_active;
        
          protected bool m_inventorySync;
        
        #endregion
        
        #region Constructors
        public Route(
        int 
          locationId,int 
          routeNumber
         )
        {
        
          m_locationId = locationId;
        
          m_routeNumber = routeNumber;
        
        }
        
        


        public Route(
        int 
          locationId,int 
          routeNumber,int 
          routeStatusId,int 
          routeTypeId,int 
          employeeId,String 
          name,int 
          documentNumberPrefix,int 
          documentNumberSequence,bool 
          active,bool 
          inventorySync
        )
        {
        
          m_locationId = locationId;
        
          m_routeNumber = routeNumber;
        
          m_routeStatusId = routeStatusId;
        
          m_routeTypeId = routeTypeId;
        
          m_employeeId = employeeId;
        
          m_name = name;
        
          m_documentNumberPrefix = documentNumberPrefix;
        
          m_documentNumberSequence = documentNumberSequence;
        
          m_active = active;
        
          m_inventorySync = inventorySync;
        
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
        public int RouteStatusId
        {
          get { return m_routeStatusId;}
          set { m_routeStatusId = value; }
        }
      
        [XmlElement]
        public int RouteTypeId
        {
          get { return m_routeTypeId;}
          set { m_routeTypeId = value; }
        }
      
        [XmlElement]
        public int EmployeeId
        {
          get { return m_employeeId;}
          set { m_employeeId = value; }
        }
      
        [XmlElement]
        public String Name
        {
          get { return m_name;}
          set { m_name = value; }
        }
      
        [XmlElement]
        public int DocumentNumberPrefix
        {
          get { return m_documentNumberPrefix;}
          set { m_documentNumberPrefix = value; }
        }
      
        [XmlElement]
        public int DocumentNumberSequence
        {
          get { return m_documentNumberSequence;}
          set { m_documentNumberSequence = value; }
        }
      
        [XmlElement]
        public bool Active
        {
          get { return m_active;}
          set { m_active = value; }
        }
      
        [XmlElement]
        public bool InventorySync
        {
          get { return m_inventorySync;}
          set { m_inventorySync = value; }
        }
      
      }
      #endregion
      }

    