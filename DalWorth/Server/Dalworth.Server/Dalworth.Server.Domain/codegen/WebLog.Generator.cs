
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Domain
      {


      public partial class WebLog : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WebLog ( " +
      
        " WebSiteId, " +
      
        " SessionId, " +
      
        " DateCreated, " +
      
        " ReferrerHost, " +
      
        " URL, " +
      
        " Keyword, " +
      
        " KeywordId " +
      
      ") Values (" +
      
        " ?WebSiteId, " +
      
        " ?SessionId, " +
      
        " ?DateCreated, " +
      
        " ?ReferrerHost, " +
      
        " ?URL, " +
      
        " ?Keyword, " +
      
        " ?KeywordId " +
      
      ")";

      public static void Insert(WebLog webLog, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WebSiteId", webLog.WebSiteId);
      
        Database.PutParameter(dbCommand,"?SessionId", webLog.SessionId);
      
        Database.PutParameter(dbCommand,"?DateCreated", webLog.DateCreated);
      
        Database.PutParameter(dbCommand,"?ReferrerHost", webLog.ReferrerHost);
      
        Database.PutParameter(dbCommand,"?URL", webLog.URL);
      
        Database.PutParameter(dbCommand,"?Keyword", webLog.Keyword);
      
        Database.PutParameter(dbCommand,"?KeywordId", webLog.KeywordId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        webLog.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(WebLog webLog)
      {
        Insert(webLog, null);
      }


      public static void Insert(List<WebLog>  webLogList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WebLog webLog in  webLogList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WebSiteId", webLog.WebSiteId);
      
        Database.PutParameter(dbCommand,"?SessionId", webLog.SessionId);
      
        Database.PutParameter(dbCommand,"?DateCreated", webLog.DateCreated);
      
        Database.PutParameter(dbCommand,"?ReferrerHost", webLog.ReferrerHost);
      
        Database.PutParameter(dbCommand,"?URL", webLog.URL);
      
        Database.PutParameter(dbCommand,"?Keyword", webLog.Keyword);
      
        Database.PutParameter(dbCommand,"?KeywordId", webLog.KeywordId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WebSiteId",webLog.WebSiteId);
      
        Database.UpdateParameter(dbCommand,"?SessionId",webLog.SessionId);
      
        Database.UpdateParameter(dbCommand,"?DateCreated",webLog.DateCreated);
      
        Database.UpdateParameter(dbCommand,"?ReferrerHost",webLog.ReferrerHost);
      
        Database.UpdateParameter(dbCommand,"?URL",webLog.URL);
      
        Database.UpdateParameter(dbCommand,"?Keyword",webLog.Keyword);
      
        Database.UpdateParameter(dbCommand,"?KeywordId",webLog.KeywordId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        webLog.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<WebLog>  webLogList)
      {
        Insert(webLogList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WebLog Set "
      
        + " WebSiteId = ?WebSiteId, "
      
        + " SessionId = ?SessionId, "
      
        + " DateCreated = ?DateCreated, "
      
        + " ReferrerHost = ?ReferrerHost, "
      
        + " URL = ?URL, "
      
        + " Keyword = ?Keyword, "
      
        + " KeywordId = ?KeywordId "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WebLog webLog, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", webLog.ID);
      
        Database.PutParameter(dbCommand,"?WebSiteId", webLog.WebSiteId);
      
        Database.PutParameter(dbCommand,"?SessionId", webLog.SessionId);
      
        Database.PutParameter(dbCommand,"?DateCreated", webLog.DateCreated);
      
        Database.PutParameter(dbCommand,"?ReferrerHost", webLog.ReferrerHost);
      
        Database.PutParameter(dbCommand,"?URL", webLog.URL);
      
        Database.PutParameter(dbCommand,"?Keyword", webLog.Keyword);
      
        Database.PutParameter(dbCommand,"?KeywordId", webLog.KeywordId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WebLog webLog)
      {
        Update(webLog, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WebSiteId, "
      
        + " SessionId, "
      
        + " DateCreated, "
      
        + " ReferrerHost, "
      
        + " URL, "
      
        + " Keyword, "
      
        + " KeywordId "
      

      + " From WebLog "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WebLog FindByPrimaryKey(
      int iD, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("WebLog not found, search by primary key");

      }

      public static WebLog FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WebLog webLog, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",webLog.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WebLog webLog)
      {
      return Exists(webLog, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WebLog limit 1";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      public static bool IsContainsData()
      {
      return IsContainsData(null);
      }

      #endregion

      #region Load

      public static WebLog Load(IDataReader dataReader, int offset)
      {
      WebLog webLog = new WebLog();

      webLog.ID = dataReader.GetInt32(0 + offset);
          webLog.WebSiteId = dataReader.GetInt32(1 + offset);
          webLog.SessionId = dataReader.GetString(2 + offset);
          webLog.DateCreated = dataReader.GetDateTime(3 + offset);
          webLog.ReferrerHost = dataReader.GetString(4 + offset);
          webLog.URL = dataReader.GetString(5 + offset);
          webLog.Keyword = dataReader.GetString(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            webLog.KeywordId = dataReader.GetInt32(7 + offset);
          

      return webLog;
      }

      public static WebLog Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WebLog "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WebLog webLog, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", webLog.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WebLog webLog)
      {
        Delete(webLog, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WebLog ";

      public static void Clear(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, connection))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Clear()
      {
        Clear(null);
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " ID, "
      
        + " WebSiteId, "
      
        + " SessionId, "
      
        + " DateCreated, "
      
        + " ReferrerHost, "
      
        + " URL, "
      
        + " Keyword, "
      
        + " KeywordId "
      

      + " From WebLog ";
      public static List<WebLog> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WebLog> rv = new List<WebLog>();

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

      public static List<WebLog> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WebLog> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WebLog obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && WebSiteId == obj.WebSiteId && SessionId == obj.SessionId && DateCreated == obj.DateCreated && ReferrerHost == obj.ReferrerHost && URL == obj.URL && Keyword == obj.Keyword && KeywordId == obj.KeywordId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WebLog> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebLog));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WebLog item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WebLog>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebLog));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WebLog> itemsList
      = new List<WebLog>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WebLog)
      itemsList.Add(deserializedObject as WebLog);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_webSiteId;
      
        protected String m_sessionId;
      
        protected DateTime m_dateCreated;
      
        protected String m_referrerHost;
      
        protected String m_uRL;
      
        protected String m_keyword;
      
        protected int? m_keywordId;
      
      #endregion

      #region Constructors
      public WebLog(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WebLog(
        int 
          iD,int 
          webSiteId,String 
          sessionId,DateTime 
          dateCreated,String 
          referrerHost,String 
          uRL,String 
          keyword,int? 
          keywordId
        ) : this()
        {
        
          m_iD = iD;
        
          m_webSiteId = webSiteId;
        
          m_sessionId = sessionId;
        
          m_dateCreated = dateCreated;
        
          m_referrerHost = referrerHost;
        
          m_uRL = uRL;
        
          m_keyword = keyword;
        
          m_keywordId = keywordId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int WebSiteId
        {
        get { return m_webSiteId;}
        set { m_webSiteId = value; }
        }
      
        [XmlElement]
        public String SessionId
        {
        get { return m_sessionId;}
        set { m_sessionId = value; }
        }
      
        [XmlElement]
        public DateTime DateCreated
        {
        get { return m_dateCreated;}
        set { m_dateCreated = value; }
        }
      
        [XmlElement]
        public String ReferrerHost
        {
        get { return m_referrerHost;}
        set { m_referrerHost = value; }
        }
      
        [XmlElement]
        public String URL
        {
        get { return m_uRL;}
        set { m_uRL = value; }
        }
      
        [XmlElement]
        public String Keyword
        {
        get { return m_keyword;}
        set { m_keyword = value; }
        }
      
        [XmlElement]
        public int? KeywordId
        {
        get { return m_keywordId;}
        set { m_keywordId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 8; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    