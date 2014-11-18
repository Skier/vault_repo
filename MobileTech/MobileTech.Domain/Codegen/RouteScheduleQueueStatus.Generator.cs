
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class RouteScheduleQueueStatus
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into RouteScheduleQueueStatus ( " +
      
        " RouteScheduleQueueStatusId, " +
      
        " Name, " +
      
        " Description " +
      
      ") Values (" +
      
        " @RouteScheduleQueueStatusId, " +
      
        " @Name, " +
      
        " @Description " +
      
      ")";

      public static void Insert(RouteScheduleQueueStatus routeScheduleQueueStatus)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueStatusId", routeScheduleQueueStatus.RouteScheduleQueueStatusId);
      
        Database.PutParameter(dbCommand,"@Name", routeScheduleQueueStatus.Name);
      
        Database.PutParameter(dbCommand,"@Description", routeScheduleQueueStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<RouteScheduleQueueStatus>  routeScheduleQueueStatusList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(RouteScheduleQueueStatus routeScheduleQueueStatus in  routeScheduleQueueStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueStatusId", routeScheduleQueueStatus.RouteScheduleQueueStatusId);
      
        Database.PutParameter(dbCommand,"@Name", routeScheduleQueueStatus.Name);
      
        Database.PutParameter(dbCommand,"@Description", routeScheduleQueueStatus.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@RouteScheduleQueueStatusId",routeScheduleQueueStatus.RouteScheduleQueueStatusId);
      
        Database.UpdateParameter(dbCommand,"@Name",routeScheduleQueueStatus.Name);
      
        Database.UpdateParameter(dbCommand,"@Description",routeScheduleQueueStatus.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update RouteScheduleQueueStatus Set "
      
        + " Name = @Name, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " RouteScheduleQueueStatusId = @RouteScheduleQueueStatusId "
        
      ;

      public static void Update(RouteScheduleQueueStatus routeScheduleQueueStatus)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueStatusId", routeScheduleQueueStatus.RouteScheduleQueueStatusId);
      
        Database.PutParameter(dbCommand,"@Name", routeScheduleQueueStatus.Name);
      
        Database.PutParameter(dbCommand,"@Description", routeScheduleQueueStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " RouteScheduleQueueStatusId, "
      
        + " Name, "
      
        + " Description "
      

      + " From RouteScheduleQueueStatus "

      
        + " Where "
        
          + " RouteScheduleQueueStatusId = @RouteScheduleQueueStatusId "
        
      ;

      public static RouteScheduleQueueStatus FindByPrimaryKey(
      int routeScheduleQueueStatusId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueStatusId", routeScheduleQueueStatusId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("RouteScheduleQueueStatus not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(RouteScheduleQueueStatus routeScheduleQueueStatus)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueStatusId",routeScheduleQueueStatus.RouteScheduleQueueStatusId);
      

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
      String sql = "select 1 from RouteScheduleQueueStatus";

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

      public static RouteScheduleQueueStatus Load(IDataReader dataReader)
      {
      RouteScheduleQueueStatus routeScheduleQueueStatus = new RouteScheduleQueueStatus();

      routeScheduleQueueStatus.RouteScheduleQueueStatusId = dataReader.GetInt16(0);
          routeScheduleQueueStatus.Name = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            routeScheduleQueueStatus.Description = dataReader.GetString(2);
          

      return routeScheduleQueueStatus;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From RouteScheduleQueueStatus "

      
        + " Where "
        
          + " RouteScheduleQueueStatusId = @RouteScheduleQueueStatusId "
        
      ;
      public static void Delete(RouteScheduleQueueStatus routeScheduleQueueStatus)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@RouteScheduleQueueStatusId", routeScheduleQueueStatus.RouteScheduleQueueStatusId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From RouteScheduleQueueStatus ";

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

      
        + " RouteScheduleQueueStatusId, "
      
        + " Name, "
      
        + " Description "
      

      + " From RouteScheduleQueueStatus ";
      public static List<RouteScheduleQueueStatus> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<RouteScheduleQueueStatus> rv = new List<RouteScheduleQueueStatus>();

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
        List<RouteScheduleQueueStatus> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<RouteScheduleQueueStatus> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteScheduleQueueStatus));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(RouteScheduleQueueStatus item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<RouteScheduleQueueStatus>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteScheduleQueueStatus));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<RouteScheduleQueueStatus> itemsList
      = new List<RouteScheduleQueueStatus>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is RouteScheduleQueueStatus)
        itemsList.Add(deserializedObject as RouteScheduleQueueStatus);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_routeScheduleQueueStatusId;
        
          protected String m_name;
        
          protected String m_description;
        
        #endregion
        
        #region Constructors
        public RouteScheduleQueueStatus(
        int 
          routeScheduleQueueStatusId
         )
        {
        
          m_routeScheduleQueueStatusId = routeScheduleQueueStatusId;
        
        }
        
        


        public RouteScheduleQueueStatus(
        int 
          routeScheduleQueueStatusId,String 
          name,String 
          description
        )
        {
        
          m_routeScheduleQueueStatusId = routeScheduleQueueStatusId;
        
          m_name = name;
        
          m_description = description;
        
          }


        
      #endregion

      
        [XmlElement]
        public int RouteScheduleQueueStatusId
        {
          get { return m_routeScheduleQueueStatusId;}
          set { m_routeScheduleQueueStatusId = value; }
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

    