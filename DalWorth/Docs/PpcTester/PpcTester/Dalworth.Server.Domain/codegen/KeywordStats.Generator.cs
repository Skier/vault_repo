
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


      public partial class KeywordStats : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into KeywordStats ( " +
      
        " SearchEngineId, " +
      
        " SearchEngineKeywordId, " +
      
        " DateStart, " +
      
        " DateEnd, " +
      
        " Clicks, " +
      
        " Impressions, " +
      
        " CTR, " +
      
        " QualityScore, " +
      
        " IsKeywordAdRelevanceAcceptable, " +
      
        " IsLandingPageQualityAcceptable, " +
      
        " IsLandingPageLatencyAcceptable, " +
      
        " MaxCPC, " +
      
        " FirstPageCPC, " +
      
        " SystemServingStatus, " +
      
        " AveragePosition " +
      
      ") Values (" +
      
        " ?SearchEngineId, " +
      
        " ?SearchEngineKeywordId, " +
      
        " ?DateStart, " +
      
        " ?DateEnd, " +
      
        " ?Clicks, " +
      
        " ?Impressions, " +
      
        " ?CTR, " +
      
        " ?QualityScore, " +
      
        " ?IsKeywordAdRelevanceAcceptable, " +
      
        " ?IsLandingPageQualityAcceptable, " +
      
        " ?IsLandingPageLatencyAcceptable, " +
      
        " ?MaxCPC, " +
      
        " ?FirstPageCPC, " +
      
        " ?SystemServingStatus, " +
      
        " ?AveragePosition " +
      
      ")";

      public static void Insert(KeywordStats keywordStats, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?SearchEngineId", keywordStats.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", keywordStats.SearchEngineKeywordId);
      
        Database.PutParameter(dbCommand,"?DateStart", keywordStats.DateStart);
      
        Database.PutParameter(dbCommand,"?DateEnd", keywordStats.DateEnd);
      
        Database.PutParameter(dbCommand,"?Clicks", keywordStats.Clicks);
      
        Database.PutParameter(dbCommand,"?Impressions", keywordStats.Impressions);
      
        Database.PutParameter(dbCommand,"?CTR", keywordStats.CTR);
      
        Database.PutParameter(dbCommand,"?QualityScore", keywordStats.QualityScore);
      
        Database.PutParameter(dbCommand,"?IsKeywordAdRelevanceAcceptable", keywordStats.IsKeywordAdRelevanceAcceptable);
      
        Database.PutParameter(dbCommand,"?IsLandingPageQualityAcceptable", keywordStats.IsLandingPageQualityAcceptable);
      
        Database.PutParameter(dbCommand,"?IsLandingPageLatencyAcceptable", keywordStats.IsLandingPageLatencyAcceptable);
      
        Database.PutParameter(dbCommand,"?MaxCPC", keywordStats.MaxCPC);
      
        Database.PutParameter(dbCommand,"?FirstPageCPC", keywordStats.FirstPageCPC);
      
        Database.PutParameter(dbCommand,"?SystemServingStatus", keywordStats.SystemServingStatus);
      
        Database.PutParameter(dbCommand,"?AveragePosition", keywordStats.AveragePosition);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        keywordStats.id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(KeywordStats keywordStats)
      {
        Insert(keywordStats, null);
      }


      public static void Insert(List<KeywordStats>  keywordStatsList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(KeywordStats keywordStats in  keywordStatsList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?SearchEngineId", keywordStats.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", keywordStats.SearchEngineKeywordId);
      
        Database.PutParameter(dbCommand,"?DateStart", keywordStats.DateStart);
      
        Database.PutParameter(dbCommand,"?DateEnd", keywordStats.DateEnd);
      
        Database.PutParameter(dbCommand,"?Clicks", keywordStats.Clicks);
      
        Database.PutParameter(dbCommand,"?Impressions", keywordStats.Impressions);
      
        Database.PutParameter(dbCommand,"?CTR", keywordStats.CTR);
      
        Database.PutParameter(dbCommand,"?QualityScore", keywordStats.QualityScore);
      
        Database.PutParameter(dbCommand,"?IsKeywordAdRelevanceAcceptable", keywordStats.IsKeywordAdRelevanceAcceptable);
      
        Database.PutParameter(dbCommand,"?IsLandingPageQualityAcceptable", keywordStats.IsLandingPageQualityAcceptable);
      
        Database.PutParameter(dbCommand,"?IsLandingPageLatencyAcceptable", keywordStats.IsLandingPageLatencyAcceptable);
      
        Database.PutParameter(dbCommand,"?MaxCPC", keywordStats.MaxCPC);
      
        Database.PutParameter(dbCommand,"?FirstPageCPC", keywordStats.FirstPageCPC);
      
        Database.PutParameter(dbCommand,"?SystemServingStatus", keywordStats.SystemServingStatus);
      
        Database.PutParameter(dbCommand,"?AveragePosition", keywordStats.AveragePosition);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?SearchEngineId",keywordStats.SearchEngineId);
      
        Database.UpdateParameter(dbCommand,"?SearchEngineKeywordId",keywordStats.SearchEngineKeywordId);
      
        Database.UpdateParameter(dbCommand,"?DateStart",keywordStats.DateStart);
      
        Database.UpdateParameter(dbCommand,"?DateEnd",keywordStats.DateEnd);
      
        Database.UpdateParameter(dbCommand,"?Clicks",keywordStats.Clicks);
      
        Database.UpdateParameter(dbCommand,"?Impressions",keywordStats.Impressions);
      
        Database.UpdateParameter(dbCommand,"?CTR",keywordStats.CTR);
      
        Database.UpdateParameter(dbCommand,"?QualityScore",keywordStats.QualityScore);
      
        Database.UpdateParameter(dbCommand,"?IsKeywordAdRelevanceAcceptable",keywordStats.IsKeywordAdRelevanceAcceptable);
      
        Database.UpdateParameter(dbCommand,"?IsLandingPageQualityAcceptable",keywordStats.IsLandingPageQualityAcceptable);
      
        Database.UpdateParameter(dbCommand,"?IsLandingPageLatencyAcceptable",keywordStats.IsLandingPageLatencyAcceptable);
      
        Database.UpdateParameter(dbCommand,"?MaxCPC",keywordStats.MaxCPC);
      
        Database.UpdateParameter(dbCommand,"?FirstPageCPC",keywordStats.FirstPageCPC);
      
        Database.UpdateParameter(dbCommand,"?SystemServingStatus",keywordStats.SystemServingStatus);
      
        Database.UpdateParameter(dbCommand,"?AveragePosition",keywordStats.AveragePosition);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        keywordStats.id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<KeywordStats>  keywordStatsList)
      {
        Insert(keywordStatsList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update KeywordStats Set "
      
        + " SearchEngineId = ?SearchEngineId, "
      
        + " SearchEngineKeywordId = ?SearchEngineKeywordId, "
      
        + " DateStart = ?DateStart, "
      
        + " DateEnd = ?DateEnd, "
      
        + " Clicks = ?Clicks, "
      
        + " Impressions = ?Impressions, "
      
        + " CTR = ?CTR, "
      
        + " QualityScore = ?QualityScore, "
      
        + " IsKeywordAdRelevanceAcceptable = ?IsKeywordAdRelevanceAcceptable, "
      
        + " IsLandingPageQualityAcceptable = ?IsLandingPageQualityAcceptable, "
      
        + " IsLandingPageLatencyAcceptable = ?IsLandingPageLatencyAcceptable, "
      
        + " MaxCPC = ?MaxCPC, "
      
        + " FirstPageCPC = ?FirstPageCPC, "
      
        + " SystemServingStatus = ?SystemServingStatus, "
      
        + " AveragePosition = ?AveragePosition "
      
        + " Where "
        
          + " id = ?id "
        
      ;

      public static void Update(KeywordStats keywordStats, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?id", keywordStats.id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", keywordStats.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", keywordStats.SearchEngineKeywordId);
      
        Database.PutParameter(dbCommand,"?DateStart", keywordStats.DateStart);
      
        Database.PutParameter(dbCommand,"?DateEnd", keywordStats.DateEnd);
      
        Database.PutParameter(dbCommand,"?Clicks", keywordStats.Clicks);
      
        Database.PutParameter(dbCommand,"?Impressions", keywordStats.Impressions);
      
        Database.PutParameter(dbCommand,"?CTR", keywordStats.CTR);
      
        Database.PutParameter(dbCommand,"?QualityScore", keywordStats.QualityScore);
      
        Database.PutParameter(dbCommand,"?IsKeywordAdRelevanceAcceptable", keywordStats.IsKeywordAdRelevanceAcceptable);
      
        Database.PutParameter(dbCommand,"?IsLandingPageQualityAcceptable", keywordStats.IsLandingPageQualityAcceptable);
      
        Database.PutParameter(dbCommand,"?IsLandingPageLatencyAcceptable", keywordStats.IsLandingPageLatencyAcceptable);
      
        Database.PutParameter(dbCommand,"?MaxCPC", keywordStats.MaxCPC);
      
        Database.PutParameter(dbCommand,"?FirstPageCPC", keywordStats.FirstPageCPC);
      
        Database.PutParameter(dbCommand,"?SystemServingStatus", keywordStats.SystemServingStatus);
      
        Database.PutParameter(dbCommand,"?AveragePosition", keywordStats.AveragePosition);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(KeywordStats keywordStats)
      {
        Update(keywordStats, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " id, "
      
        + " SearchEngineId, "
      
        + " SearchEngineKeywordId, "
      
        + " DateStart, "
      
        + " DateEnd, "
      
        + " Clicks, "
      
        + " Impressions, "
      
        + " CTR, "
      
        + " QualityScore, "
      
        + " IsKeywordAdRelevanceAcceptable, "
      
        + " IsLandingPageQualityAcceptable, "
      
        + " IsLandingPageLatencyAcceptable, "
      
        + " MaxCPC, "
      
        + " FirstPageCPC, "
      
        + " SystemServingStatus, "
      
        + " AveragePosition "
      

      + " From KeywordStats "

      
        + " Where "
        
          + " id = ?id "
        
      ;

      public static KeywordStats FindByPrimaryKey(
      int id, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?id", id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("KeywordStats not found, search by primary key");

      }

      public static KeywordStats FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(KeywordStats keywordStats, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?id",keywordStats.id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(KeywordStats keywordStats)
      {
      return Exists(keywordStats, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from KeywordStats limit 1";

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

      public static KeywordStats Load(IDataReader dataReader, int offset)
      {
      KeywordStats keywordStats = new KeywordStats();

      keywordStats.id = dataReader.GetInt32(0 + offset);
          keywordStats.SearchEngineId = dataReader.GetInt32(1 + offset);
          keywordStats.SearchEngineKeywordId = dataReader.GetInt64(2 + offset);
          keywordStats.DateStart = dataReader.GetDateTime(3 + offset);
          keywordStats.DateEnd = dataReader.GetDateTime(4 + offset);
          keywordStats.Clicks = dataReader.GetInt64(5 + offset);
          keywordStats.Impressions = dataReader.GetInt64(6 + offset);
          keywordStats.CTR = dataReader.GetFloat(7 + offset);
          keywordStats.QualityScore = dataReader.GetInt32(8 + offset);
          keywordStats.IsKeywordAdRelevanceAcceptable = dataReader.GetBoolean(9 + offset);
          keywordStats.IsLandingPageQualityAcceptable = dataReader.GetBoolean(10 + offset);
          keywordStats.IsLandingPageLatencyAcceptable = dataReader.GetBoolean(11 + offset);
          keywordStats.MaxCPC = dataReader.GetDecimal(12 + offset);
          keywordStats.FirstPageCPC = dataReader.GetDecimal(13 + offset);
          keywordStats.SystemServingStatus = dataReader.GetInt32(14 + offset);
          keywordStats.AveragePosition = dataReader.GetInt64(15 + offset);
          

      return keywordStats;
      }

      public static KeywordStats Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From KeywordStats "

      
        + " Where "
        
          + " id = ?id "
        
      ;
      public static void Delete(KeywordStats keywordStats, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?id", keywordStats.id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(KeywordStats keywordStats)
      {
        Delete(keywordStats, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From KeywordStats ";

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

      
        + " id, "
      
        + " SearchEngineId, "
      
        + " SearchEngineKeywordId, "
      
        + " DateStart, "
      
        + " DateEnd, "
      
        + " Clicks, "
      
        + " Impressions, "
      
        + " CTR, "
      
        + " QualityScore, "
      
        + " IsKeywordAdRelevanceAcceptable, "
      
        + " IsLandingPageQualityAcceptable, "
      
        + " IsLandingPageLatencyAcceptable, "
      
        + " MaxCPC, "
      
        + " FirstPageCPC, "
      
        + " SystemServingStatus, "
      
        + " AveragePosition "
      

      + " From KeywordStats ";
      public static List<KeywordStats> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<KeywordStats> rv = new List<KeywordStats>();

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

      public static List<KeywordStats> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<KeywordStats> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (KeywordStats obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return id == obj.id && SearchEngineId == obj.SearchEngineId && SearchEngineKeywordId == obj.SearchEngineKeywordId && DateStart == obj.DateStart && DateEnd == obj.DateEnd && Clicks == obj.Clicks && Impressions == obj.Impressions && CTR == obj.CTR && QualityScore == obj.QualityScore && IsKeywordAdRelevanceAcceptable == obj.IsKeywordAdRelevanceAcceptable && IsLandingPageQualityAcceptable == obj.IsLandingPageQualityAcceptable && IsLandingPageLatencyAcceptable == obj.IsLandingPageLatencyAcceptable && MaxCPC == obj.MaxCPC && FirstPageCPC == obj.FirstPageCPC && SystemServingStatus == obj.SystemServingStatus && AveragePosition == obj.AveragePosition;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<KeywordStats> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(KeywordStats));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(KeywordStats item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<KeywordStats>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(KeywordStats));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<KeywordStats> itemsList
      = new List<KeywordStats>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is KeywordStats)
      itemsList.Add(deserializedObject as KeywordStats);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_id;
      
        protected int m_searchEngineId;
      
        protected long m_searchEngineKeywordId;
      
        protected DateTime m_dateStart;
      
        protected DateTime m_dateEnd;
      
        protected long m_clicks;
      
        protected long m_impressions;
      
        protected float m_cTR;
      
        protected int m_qualityScore;
      
        protected bool m_isKeywordAdRelevanceAcceptable;
      
        protected bool m_isLandingPageQualityAcceptable;
      
        protected bool m_isLandingPageLatencyAcceptable;
      
        protected decimal m_maxCPC;
      
        protected decimal m_firstPageCPC;
      
        protected int m_systemServingStatus;
      
        protected long m_averagePosition;
      
      #endregion

      #region Constructors
      public KeywordStats(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public KeywordStats(
        int 
          id,int 
          searchEngineId,long 
          searchEngineKeywordId,DateTime 
          dateStart,DateTime 
          dateEnd,long 
          clicks,long 
          impressions,float 
          cTR,int 
          qualityScore,bool 
          isKeywordAdRelevanceAcceptable,bool 
          isLandingPageQualityAcceptable,bool 
          isLandingPageLatencyAcceptable,decimal 
          maxCPC,decimal 
          firstPageCPC,int 
          systemServingStatus,long 
          averagePosition
        ) : this()
        {
        
          m_id = id;
        
          m_searchEngineId = searchEngineId;
        
          m_searchEngineKeywordId = searchEngineKeywordId;
        
          m_dateStart = dateStart;
        
          m_dateEnd = dateEnd;
        
          m_clicks = clicks;
        
          m_impressions = impressions;
        
          m_cTR = cTR;
        
          m_qualityScore = qualityScore;
        
          m_isKeywordAdRelevanceAcceptable = isKeywordAdRelevanceAcceptable;
        
          m_isLandingPageQualityAcceptable = isLandingPageQualityAcceptable;
        
          m_isLandingPageLatencyAcceptable = isLandingPageLatencyAcceptable;
        
          m_maxCPC = maxCPC;
        
          m_firstPageCPC = firstPageCPC;
        
          m_systemServingStatus = systemServingStatus;
        
          m_averagePosition = averagePosition;
        
        }


      
      #endregion

      
        [XmlElement]
        public int id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        [XmlElement]
        public int SearchEngineId
        {
        get { return m_searchEngineId;}
        set { m_searchEngineId = value; }
        }
      
        [XmlElement]
        public long SearchEngineKeywordId
        {
        get { return m_searchEngineKeywordId;}
        set { m_searchEngineKeywordId = value; }
        }
      
        [XmlElement]
        public DateTime DateStart
        {
        get { return m_dateStart;}
        set { m_dateStart = value; }
        }
      
        [XmlElement]
        public DateTime DateEnd
        {
        get { return m_dateEnd;}
        set { m_dateEnd = value; }
        }
      
        [XmlElement]
        public long Clicks
        {
        get { return m_clicks;}
        set { m_clicks = value; }
        }
      
        [XmlElement]
        public long Impressions
        {
        get { return m_impressions;}
        set { m_impressions = value; }
        }
      
        [XmlElement]
        public float CTR
        {
        get { return m_cTR;}
        set { m_cTR = value; }
        }
      
        [XmlElement]
        public int QualityScore
        {
        get { return m_qualityScore;}
        set { m_qualityScore = value; }
        }
      
        [XmlElement]
        public bool IsKeywordAdRelevanceAcceptable
        {
        get { return m_isKeywordAdRelevanceAcceptable;}
        set { m_isKeywordAdRelevanceAcceptable = value; }
        }
      
        [XmlElement]
        public bool IsLandingPageQualityAcceptable
        {
        get { return m_isLandingPageQualityAcceptable;}
        set { m_isLandingPageQualityAcceptable = value; }
        }
      
        [XmlElement]
        public bool IsLandingPageLatencyAcceptable
        {
        get { return m_isLandingPageLatencyAcceptable;}
        set { m_isLandingPageLatencyAcceptable = value; }
        }
      
        [XmlElement]
        public decimal MaxCPC
        {
        get { return m_maxCPC;}
        set { m_maxCPC = value; }
        }
      
        [XmlElement]
        public decimal FirstPageCPC
        {
        get { return m_firstPageCPC;}
        set { m_firstPageCPC = value; }
        }
      
        [XmlElement]
        public int SystemServingStatus
        {
        get { return m_systemServingStatus;}
        set { m_systemServingStatus = value; }
        }
      
        [XmlElement]
        public long AveragePosition
        {
        get { return m_averagePosition;}
        set { m_averagePosition = value; }
        }
      

      public static int FieldsCount
      {
      get { return 16; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    