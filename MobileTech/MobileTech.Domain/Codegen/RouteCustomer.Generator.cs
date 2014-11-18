
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class RouteCustomer
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into RouteCustomer ( " +
      
        " LocationId, " +
      
        " RouteNumber, " +
      
        " CustomerId " +
      
      ") Values (" +
      
        " @LocationId, " +
      
        " @RouteNumber, " +
      
        " @CustomerId " +
      
      ")";

      public static void Insert(RouteCustomer routeCustomer)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", routeCustomer.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeCustomer.RouteNumber);
      
        Database.PutParameter(dbCommand,"@CustomerId", routeCustomer.CustomerId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<RouteCustomer>  routeCustomerList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(RouteCustomer routeCustomer in  routeCustomerList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@LocationId", routeCustomer.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeCustomer.RouteNumber);
      
        Database.PutParameter(dbCommand,"@CustomerId", routeCustomer.CustomerId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@LocationId",routeCustomer.LocationId);
      
        Database.UpdateParameter(dbCommand,"@RouteNumber",routeCustomer.RouteNumber);
      
        Database.UpdateParameter(dbCommand,"@CustomerId",routeCustomer.CustomerId);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update RouteCustomer Set "
      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " CustomerId = @CustomerId "
        
      ;

      public static void Update(RouteCustomer routeCustomer)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", routeCustomer.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeCustomer.RouteNumber);
      
        Database.PutParameter(dbCommand,"@CustomerId", routeCustomer.CustomerId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " LocationId, "
      
        + " RouteNumber, "
      
        + " CustomerId "
      

      + " From RouteCustomer "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " CustomerId = @CustomerId "
        
      ;

      public static RouteCustomer FindByPrimaryKey(
      int locationId,int routeNumber,int customerId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId", locationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeNumber);
      
        Database.PutParameter(dbCommand,"@CustomerId", customerId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("RouteCustomer not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(RouteCustomer routeCustomer)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@LocationId",routeCustomer.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber",routeCustomer.RouteNumber);
      
        Database.PutParameter(dbCommand,"@CustomerId",routeCustomer.CustomerId);
      

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
      String sql = "select 1 from RouteCustomer";

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

      public static RouteCustomer Load(IDataReader dataReader)
      {
      RouteCustomer routeCustomer = new RouteCustomer();

      routeCustomer.LocationId = dataReader.GetInt32(0);
          routeCustomer.RouteNumber = dataReader.GetInt32(1);
          routeCustomer.CustomerId = dataReader.GetInt32(2);
          

      return routeCustomer;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From RouteCustomer "

      
        + " Where "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " CustomerId = @CustomerId "
        
      ;
      public static void Delete(RouteCustomer routeCustomer)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@LocationId", routeCustomer.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeCustomer.RouteNumber);
      
        Database.PutParameter(dbCommand,"@CustomerId", routeCustomer.CustomerId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From RouteCustomer ";

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
      
        + " CustomerId "
      

      + " From RouteCustomer ";
      public static List<RouteCustomer> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<RouteCustomer> rv = new List<RouteCustomer>();

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
        List<RouteCustomer> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<RouteCustomer> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteCustomer));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(RouteCustomer item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<RouteCustomer>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteCustomer));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<RouteCustomer> itemsList
      = new List<RouteCustomer>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is RouteCustomer)
        itemsList.Add(deserializedObject as RouteCustomer);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_locationId;
        
          protected int m_routeNumber;
        
          protected int m_customerId;
        
        #endregion
        
        #region Constructors
        public RouteCustomer(
        int 
          locationId,int 
          routeNumber,int 
          customerId
         )
        {
        
          m_locationId = locationId;
        
          m_routeNumber = routeNumber;
        
          m_customerId = customerId;
        
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
        public int CustomerId
        {
          get { return m_customerId;}
          set { m_customerId = value; }
        }
      
      }
      #endregion
      }

    