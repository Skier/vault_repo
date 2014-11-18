
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class RouteScheduleQueue
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into RouteScheduleQueue ( " +
      
        " RouteScheduleQueueId, " +
      
        " LocationId, " +
      
        " RouteScheduleId, " +
      
        " RouteScheduleQueueStatusId, " +
      
        " DateCreated " +
      
      ") Values (" +
      
        " @RouteScheduleQueueId, " +
      
        " @LocationId, " +
      
        " @RouteScheduleId, " +
      
        " @RouteScheduleQueueStatusId, " +
      
        " @DateCreated " +
      
      ")";

      public static void Insert(RouteScheduleQueue routeScheduleQueue)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueId", routeScheduleQueue.RouteScheduleQueueId);
      
        Database.PutParameter(dbCommand,"@LocationId", routeScheduleQueue.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteScheduleId", routeScheduleQueue.RouteScheduleId);
      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueStatusId", routeScheduleQueue.RouteScheduleQueueStatusId);
      
        Database.PutParameter(dbCommand,"@DateCreated", routeScheduleQueue.DateCreated);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<RouteScheduleQueue>  routeScheduleQueueList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(RouteScheduleQueue routeScheduleQueue in  routeScheduleQueueList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueId", routeScheduleQueue.RouteScheduleQueueId);
      
        Database.PutParameter(dbCommand,"@LocationId", routeScheduleQueue.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteScheduleId", routeScheduleQueue.RouteScheduleId);
      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueStatusId", routeScheduleQueue.RouteScheduleQueueStatusId);
      
        Database.PutParameter(dbCommand,"@DateCreated", routeScheduleQueue.DateCreated);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@RouteScheduleQueueId",routeScheduleQueue.RouteScheduleQueueId);
      
        Database.UpdateParameter(dbCommand,"@LocationId",routeScheduleQueue.LocationId);
      
        Database.UpdateParameter(dbCommand,"@RouteScheduleId",routeScheduleQueue.RouteScheduleId);
      
        Database.UpdateParameter(dbCommand,"@RouteScheduleQueueStatusId",routeScheduleQueue.RouteScheduleQueueStatusId);
      
        Database.UpdateParameter(dbCommand,"@DateCreated",routeScheduleQueue.DateCreated);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update RouteScheduleQueue Set "
      
        + " LocationId = @LocationId, "
      
        + " RouteScheduleId = @RouteScheduleId, "
      
        + " RouteScheduleQueueStatusId = @RouteScheduleQueueStatusId, "
      
        + " DateCreated = @DateCreated "
      
        + " Where "
        
          + " RouteScheduleQueueId = @RouteScheduleQueueId "
        
      ;

      public static void Update(RouteScheduleQueue routeScheduleQueue)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueId", routeScheduleQueue.RouteScheduleQueueId);
      
        Database.PutParameter(dbCommand,"@LocationId", routeScheduleQueue.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteScheduleId", routeScheduleQueue.RouteScheduleId);
      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueStatusId", routeScheduleQueue.RouteScheduleQueueStatusId);
      
        Database.PutParameter(dbCommand,"@DateCreated", routeScheduleQueue.DateCreated);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " RouteScheduleQueueId, "
      
        + " LocationId, "
      
        + " RouteScheduleId, "
      
        + " RouteScheduleQueueStatusId, "
      
        + " DateCreated "
      

      + " From RouteScheduleQueue "

      
        + " Where "
        
          + " RouteScheduleQueueId = @RouteScheduleQueueId "
        
      ;

      public static RouteScheduleQueue FindByPrimaryKey(
      int routeScheduleQueueId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueId", routeScheduleQueueId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("RouteScheduleQueue not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(RouteScheduleQueue routeScheduleQueue)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueId",routeScheduleQueue.RouteScheduleQueueId);
      

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
      String sql = "select 1 from RouteScheduleQueue";

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

      public static RouteScheduleQueue Load(IDataReader dataReader)
      {
      RouteScheduleQueue routeScheduleQueue = new RouteScheduleQueue();

      routeScheduleQueue.RouteScheduleQueueId = dataReader.GetInt32(0);
          routeScheduleQueue.LocationId = dataReader.GetInt32(1);
          routeScheduleQueue.RouteScheduleId = dataReader.GetInt32(2);
          routeScheduleQueue.RouteScheduleQueueStatusId = dataReader.GetInt16(3);
          routeScheduleQueue.DateCreated = dataReader.GetDateTime(4);
          

      return routeScheduleQueue;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From RouteScheduleQueue "

      
        + " Where "
        
          + " RouteScheduleQueueId = @RouteScheduleQueueId "
        
      ;
      public static void Delete(RouteScheduleQueue routeScheduleQueue)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueId", routeScheduleQueue.RouteScheduleQueueId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From RouteScheduleQueue ";

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

      
        + " RouteScheduleQueueId, "
      
        + " LocationId, "
      
        + " RouteScheduleId, "
      
        + " RouteScheduleQueueStatusId, "
      
        + " DateCreated "
      

      + " From RouteScheduleQueue ";
      public static List<RouteScheduleQueue> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<RouteScheduleQueue> rv = new List<RouteScheduleQueue>();

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
        List<RouteScheduleQueue> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<RouteScheduleQueue> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteScheduleQueue));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(RouteScheduleQueue item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<RouteScheduleQueue>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteScheduleQueue));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<RouteScheduleQueue> itemsList
      = new List<RouteScheduleQueue>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is RouteScheduleQueue)
        itemsList.Add(deserializedObject as RouteScheduleQueue);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_routeScheduleQueueId;
        
          protected int m_locationId;
        
          protected int m_routeScheduleId;
        
          protected int m_routeScheduleQueueStatusId;
        
          protected DateTime m_dateCreated;
        
        #endregion
        
        #region Constructors
        public RouteScheduleQueue(
        int 
          routeScheduleQueueId
         )
        {
        
          m_routeScheduleQueueId = routeScheduleQueueId;
        
        }
        
        


        public RouteScheduleQueue(
        int 
          routeScheduleQueueId,int 
          locationId,int 
          routeScheduleId,int 
          routeScheduleQueueStatusId,DateTime 
          dateCreated
        )
        {
        
          m_routeScheduleQueueId = routeScheduleQueueId;
        
          m_locationId = locationId;
        
          m_routeScheduleId = routeScheduleId;
        
          m_routeScheduleQueueStatusId = routeScheduleQueueStatusId;
        
          m_dateCreated = dateCreated;
        
          }


        
      #endregion

      
        [XmlElement]
        public int RouteScheduleQueueId
        {
          get { return m_routeScheduleQueueId;}
          set { m_routeScheduleQueueId = value; }
        }
      
        [XmlElement]
        public int LocationId
        {
          get { return m_locationId;}
          set { m_locationId = value; }
        }
      
        [XmlElement]
        public int RouteScheduleId
        {
          get { return m_routeScheduleId;}
          set { m_routeScheduleId = value; }
        }
      
        [XmlElement]
        public int RouteScheduleQueueStatusId
        {
          get { return m_routeScheduleQueueStatusId;}
          set { m_routeScheduleQueueStatusId = value; }
        }
      
        [XmlElement]
        public DateTime DateCreated
        {
          get { return m_dateCreated;}
          set { m_dateCreated = value; }
        }
      
      }
      #endregion
      }

    