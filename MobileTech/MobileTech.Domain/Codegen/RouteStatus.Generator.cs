
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class RouteStatus
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into RouteStatus ( " +
      
        " RouteStatusId, " +
      
        " Name, " +
      
        " Description " +
      
      ") Values (" +
      
        " @RouteStatusId, " +
      
        " @Name, " +
      
        " @Description " +
      
      ")";

      public static void Insert(RouteStatus routeStatus)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@RouteStatusId", routeStatus.RouteStatusId);
      
        Database.PutParameter(dbCommand,"@Name", routeStatus.Name);
      
        Database.PutParameter(dbCommand,"@Description", routeStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<RouteStatus>  routeStatusList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(RouteStatus routeStatus in  routeStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@RouteStatusId", routeStatus.RouteStatusId);
      
        Database.PutParameter(dbCommand,"@Name", routeStatus.Name);
      
        Database.PutParameter(dbCommand,"@Description", routeStatus.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@RouteStatusId",routeStatus.RouteStatusId);
      
        Database.UpdateParameter(dbCommand,"@Name",routeStatus.Name);
      
        Database.UpdateParameter(dbCommand,"@Description",routeStatus.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update RouteStatus Set "
      
        + " Name = @Name, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " RouteStatusId = @RouteStatusId "
        
      ;

      public static void Update(RouteStatus routeStatus)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@RouteStatusId", routeStatus.RouteStatusId);
      
        Database.PutParameter(dbCommand,"@Name", routeStatus.Name);
      
        Database.PutParameter(dbCommand,"@Description", routeStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " RouteStatusId, "
      
        + " Name, "
      
        + " Description "
      

      + " From RouteStatus "

      
        + " Where "
        
          + " RouteStatusId = @RouteStatusId "
        
      ;

      public static RouteStatus FindByPrimaryKey(
      int routeStatusId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@RouteStatusId", routeStatusId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("RouteStatus not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(RouteStatus routeStatus)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@RouteStatusId",routeStatus.RouteStatusId);
      

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
      String sql = "select 1 from RouteStatus";

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

      public static RouteStatus Load(IDataReader dataReader)
      {
      RouteStatus routeStatus = new RouteStatus();

      routeStatus.RouteStatusId = dataReader.GetInt16(0);
          routeStatus.Name = dataReader.GetString(1);
          routeStatus.Description = dataReader.GetString(2);
          

      return routeStatus;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From RouteStatus "

      
        + " Where "
        
          + " RouteStatusId = @RouteStatusId "
        
      ;
      public static void Delete(RouteStatus routeStatus)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@RouteStatusId", routeStatus.RouteStatusId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From RouteStatus ";

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

      
        + " RouteStatusId, "
      
        + " Name, "
      
        + " Description "
      

      + " From RouteStatus ";
      public static List<RouteStatus> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<RouteStatus> rv = new List<RouteStatus>();

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
        List<RouteStatus> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<RouteStatus> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteStatus));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(RouteStatus item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<RouteStatus>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteStatus));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<RouteStatus> itemsList
      = new List<RouteStatus>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is RouteStatus)
        itemsList.Add(deserializedObject as RouteStatus);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected int m_routeStatusId;
        
          protected String m_name;
        
          protected String m_description;
        
        #endregion
        
        #region Constructors
        public RouteStatus(
        int 
          routeStatusId
         )
        {
        
          m_routeStatusId = routeStatusId;
        
        }
        
        


        public RouteStatus(
        int 
          routeStatusId,String 
          name,String 
          description
        )
        {
        
          m_routeStatusId = routeStatusId;
        
          m_name = name;
        
          m_description = description;
        
          }


        
      #endregion

      
        [XmlElement]
        public int RouteStatusId
        {
          get { return m_routeStatusId;}
          set { m_routeStatusId = value; }
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

    