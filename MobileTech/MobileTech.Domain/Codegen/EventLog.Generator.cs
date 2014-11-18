
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class EventLog
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into EventLog ( " +
      
        " SessionId, " +
      
        " EventLogId, " +
      
        " EventType, " +
      
        " Message, " +
      
        " Source, " +
      
        " AssemblyName, " +
      
        " CreateDate " +
      
      ") Values (" +
      
        " @SessionId, " +
      
        " @EventLogId, " +
      
        " @EventType, " +
      
        " @Message, " +
      
        " @Source, " +
      
        " @AssemblyName, " +
      
        " @CreateDate " +
      
      ")";

      public static void Insert(EventLog eventLog)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", eventLog.SessionId);
      
        Database.PutParameter(dbCommand,"@EventLogId", eventLog.EventLogId);
      
        Database.PutParameter(dbCommand,"@EventType", eventLog.EventType);
      
        Database.PutParameter(dbCommand,"@Message", eventLog.Message);
      
        Database.PutParameter(dbCommand,"@Source", eventLog.Source);
      
        Database.PutParameter(dbCommand,"@AssemblyName", eventLog.AssemblyName);
      
        Database.PutParameter(dbCommand,"@CreateDate", eventLog.CreateDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<EventLog>  eventLogList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(EventLog eventLog in  eventLogList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@SessionId", eventLog.SessionId);
      
        Database.PutParameter(dbCommand,"@EventLogId", eventLog.EventLogId);
      
        Database.PutParameter(dbCommand,"@EventType", eventLog.EventType);
      
        Database.PutParameter(dbCommand,"@Message", eventLog.Message);
      
        Database.PutParameter(dbCommand,"@Source", eventLog.Source);
      
        Database.PutParameter(dbCommand,"@AssemblyName", eventLog.AssemblyName);
      
        Database.PutParameter(dbCommand,"@CreateDate", eventLog.CreateDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@SessionId",eventLog.SessionId);
      
        Database.UpdateParameter(dbCommand,"@EventLogId",eventLog.EventLogId);
      
        Database.UpdateParameter(dbCommand,"@EventType",eventLog.EventType);
      
        Database.UpdateParameter(dbCommand,"@Message",eventLog.Message);
      
        Database.UpdateParameter(dbCommand,"@Source",eventLog.Source);
      
        Database.UpdateParameter(dbCommand,"@AssemblyName",eventLog.AssemblyName);
      
        Database.UpdateParameter(dbCommand,"@CreateDate",eventLog.CreateDate);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update EventLog Set "
      
        + " EventType = @EventType, "
      
        + " Message = @Message, "
      
        + " Source = @Source, "
      
        + " AssemblyName = @AssemblyName, "
      
        + " CreateDate = @CreateDate "
      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " EventLogId = @EventLogId "
        
      ;

      public static void Update(EventLog eventLog)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", eventLog.SessionId);
      
        Database.PutParameter(dbCommand,"@EventLogId", eventLog.EventLogId);
      
        Database.PutParameter(dbCommand,"@EventType", eventLog.EventType);
      
        Database.PutParameter(dbCommand,"@Message", eventLog.Message);
      
        Database.PutParameter(dbCommand,"@Source", eventLog.Source);
      
        Database.PutParameter(dbCommand,"@AssemblyName", eventLog.AssemblyName);
      
        Database.PutParameter(dbCommand,"@CreateDate", eventLog.CreateDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " SessionId, "
      
        + " EventLogId, "
      
        + " EventType, "
      
        + " Message, "
      
        + " Source, "
      
        + " AssemblyName, "
      
        + " CreateDate "
      

      + " From EventLog "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " EventLogId = @EventLogId "
        
      ;

      public static EventLog FindByPrimaryKey(
      long sessionId,int eventLogId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", sessionId);
      
        Database.PutParameter(dbCommand,"@EventLogId", eventLogId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("EventLog not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(EventLog eventLog)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId",eventLog.SessionId);
      
        Database.PutParameter(dbCommand,"@EventLogId",eventLog.EventLogId);
      

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
      String sql = "select 1 from EventLog";

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

      public static EventLog Load(IDataReader dataReader)
      {
      EventLog eventLog = new EventLog();

      eventLog.SessionId = dataReader.GetInt64(0);
          eventLog.EventLogId = dataReader.GetInt32(1);
          eventLog.EventType = dataReader.GetInt32(2);
          eventLog.Message = dataReader.GetString(3);
          
            if(!dataReader.IsDBNull(4))
            eventLog.Source = dataReader.GetString(4);
          
            if(!dataReader.IsDBNull(5))
            eventLog.AssemblyName = dataReader.GetString(5);
          eventLog.CreateDate = dataReader.GetDateTime(6);
          

      return eventLog;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From EventLog "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " EventLogId = @EventLogId "
        
      ;
      public static void Delete(EventLog eventLog)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@SessionId", eventLog.SessionId);
      
        Database.PutParameter(dbCommand,"@EventLogId", eventLog.EventLogId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From EventLog ";

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

      
        + " SessionId, "
      
        + " EventLogId, "
      
        + " EventType, "
      
        + " Message, "
      
        + " Source, "
      
        + " AssemblyName, "
      
        + " CreateDate "
      

      + " From EventLog ";
      public static List<EventLog> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<EventLog> rv = new List<EventLog>();

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
        List<EventLog> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<EventLog> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(EventLog));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(EventLog item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<EventLog>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(EventLog));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<EventLog> itemsList
      = new List<EventLog>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is EventLog)
        itemsList.Add(deserializedObject as EventLog);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected long m_sessionId;
        
          protected int m_eventLogId;
        
          protected int m_eventType;
        
          protected String m_message;
        
          protected String m_source;
        
          protected String m_assemblyName;
        
          protected DateTime m_createDate;
        
        #endregion
        
        #region Constructors
        public EventLog(
        long 
          sessionId,int 
          eventLogId
         )
        {
        
          m_sessionId = sessionId;
        
          m_eventLogId = eventLogId;
        
        }
        
        


        public EventLog(
        long 
          sessionId,int 
          eventLogId,int 
          eventType,String 
          message,String 
          source,String 
          assemblyName,DateTime 
          createDate
        )
        {
        
          m_sessionId = sessionId;
        
          m_eventLogId = eventLogId;
        
          m_eventType = eventType;
        
          m_message = message;
        
          m_source = source;
        
          m_assemblyName = assemblyName;
        
          m_createDate = createDate;
        
          }


        
      #endregion

      
        [XmlElement]
        public long SessionId
        {
          get { return m_sessionId;}
          set { m_sessionId = value; }
        }
      
        [XmlElement]
        public int EventLogId
        {
          get { return m_eventLogId;}
          set { m_eventLogId = value; }
        }
      
        [XmlElement]
        public int EventType
        {
          get { return m_eventType;}
          set { m_eventType = value; }
        }
      
        [XmlElement]
        public String Message
        {
          get { return m_message;}
          set { m_message = value; }
        }
      
        [XmlElement]
        public String Source
        {
          get { return m_source;}
          set { m_source = value; }
        }
      
        [XmlElement]
        public String AssemblyName
        {
          get { return m_assemblyName;}
          set { m_assemblyName = value; }
        }
      
        [XmlElement]
        public DateTime CreateDate
        {
          get { return m_createDate;}
          set { m_createDate = value; }
        }
      
      }
      #endregion
      }

    