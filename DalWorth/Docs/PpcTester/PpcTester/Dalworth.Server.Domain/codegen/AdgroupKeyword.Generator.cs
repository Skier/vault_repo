
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


      public partial class AdgroupKeyword : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into AdgroupKeyword ( " +
      
        " SearchEngineId, " +
      
        " SearchEngineKeywordId, " +
      
        " AdGroupId, " +
      
        " MatchType, " +
      
        " Status, " +
      
        " KeywordId " +
      
      ") Values (" +
      
        " ?SearchEngineId, " +
      
        " ?SearchEngineKeywordId, " +
      
        " ?AdGroupId, " +
      
        " ?MatchType, " +
      
        " ?Status, " +
      
        " ?KeywordId " +
      
      ")";

      public static void Insert(AdgroupKeyword adgroupKeyword, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?SearchEngineId", adgroupKeyword.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", adgroupKeyword.SearchEngineKeywordId);
      
        Database.PutParameter(dbCommand,"?AdGroupId", adgroupKeyword.AdGroupId);
      
        Database.PutParameter(dbCommand,"?MatchType", adgroupKeyword.MatchType);
      
        Database.PutParameter(dbCommand,"?Status", adgroupKeyword.Status);
      
        Database.PutParameter(dbCommand,"?KeywordId", adgroupKeyword.KeywordId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(AdgroupKeyword adgroupKeyword)
      {
        Insert(adgroupKeyword, null);
      }


      public static void Insert(List<AdgroupKeyword>  adgroupKeywordList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(AdgroupKeyword adgroupKeyword in  adgroupKeywordList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?SearchEngineId", adgroupKeyword.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", adgroupKeyword.SearchEngineKeywordId);
      
        Database.PutParameter(dbCommand,"?AdGroupId", adgroupKeyword.AdGroupId);
      
        Database.PutParameter(dbCommand,"?MatchType", adgroupKeyword.MatchType);
      
        Database.PutParameter(dbCommand,"?Status", adgroupKeyword.Status);
      
        Database.PutParameter(dbCommand,"?KeywordId", adgroupKeyword.KeywordId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?SearchEngineId",adgroupKeyword.SearchEngineId);
      
        Database.UpdateParameter(dbCommand,"?SearchEngineKeywordId",adgroupKeyword.SearchEngineKeywordId);
      
        Database.UpdateParameter(dbCommand,"?AdGroupId",adgroupKeyword.AdGroupId);
      
        Database.UpdateParameter(dbCommand,"?MatchType",adgroupKeyword.MatchType);
      
        Database.UpdateParameter(dbCommand,"?Status",adgroupKeyword.Status);
      
        Database.UpdateParameter(dbCommand,"?KeywordId",adgroupKeyword.KeywordId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<AdgroupKeyword>  adgroupKeywordList)
      {
        Insert(adgroupKeywordList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update AdgroupKeyword Set "
      
        + " AdGroupId = ?AdGroupId, "
      
        + " MatchType = ?MatchType, "
      
        + " Status = ?Status, "
      
        + " KeywordId = ?KeywordId "
      
        + " Where "
        
          + " SearchEngineId = ?SearchEngineId and  "
        
          + " SearchEngineKeywordId = ?SearchEngineKeywordId "
        
      ;

      public static void Update(AdgroupKeyword adgroupKeyword, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?SearchEngineId", adgroupKeyword.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", adgroupKeyword.SearchEngineKeywordId);
      
        Database.PutParameter(dbCommand,"?AdGroupId", adgroupKeyword.AdGroupId);
      
        Database.PutParameter(dbCommand,"?MatchType", adgroupKeyword.MatchType);
      
        Database.PutParameter(dbCommand,"?Status", adgroupKeyword.Status);
      
        Database.PutParameter(dbCommand,"?KeywordId", adgroupKeyword.KeywordId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(AdgroupKeyword adgroupKeyword)
      {
        Update(adgroupKeyword, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " SearchEngineId, "
      
        + " SearchEngineKeywordId, "
      
        + " AdGroupId, "
      
        + " MatchType, "
      
        + " Status, "
      
        + " KeywordId "
      

      + " From AdgroupKeyword "

      
        + " Where "
        
          + " SearchEngineId = ?SearchEngineId and  "
        
          + " SearchEngineKeywordId = ?SearchEngineKeywordId "
        
      ;

      public static AdgroupKeyword FindByPrimaryKey(
      int searchEngineId,long searchEngineKeywordId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?SearchEngineId", searchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", searchEngineKeywordId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("AdgroupKeyword not found, search by primary key");

      }

      public static AdgroupKeyword FindByPrimaryKey(
      int searchEngineId,long searchEngineKeywordId
      )
      {
      return FindByPrimaryKey(
      searchEngineId,searchEngineKeywordId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(AdgroupKeyword adgroupKeyword, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?SearchEngineId",adgroupKeyword.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId",adgroupKeyword.SearchEngineKeywordId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(AdgroupKeyword adgroupKeyword)
      {
      return Exists(adgroupKeyword, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from AdgroupKeyword limit 1";

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

      public static AdgroupKeyword Load(IDataReader dataReader, int offset)
      {
      AdgroupKeyword adgroupKeyword = new AdgroupKeyword();

      adgroupKeyword.SearchEngineId = dataReader.GetInt32(0 + offset);
          adgroupKeyword.SearchEngineKeywordId = dataReader.GetInt64(1 + offset);
          adgroupKeyword.AdGroupId = dataReader.GetInt64(2 + offset);
          adgroupKeyword.MatchType = dataReader.GetInt32(3 + offset);
          adgroupKeyword.Status = dataReader.GetInt32(4 + offset);
          adgroupKeyword.KeywordId = dataReader.GetInt32(5 + offset);
          

      return adgroupKeyword;
      }

      public static AdgroupKeyword Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From AdgroupKeyword "

      
        + " Where "
        
          + " SearchEngineId = ?SearchEngineId and  "
        
          + " SearchEngineKeywordId = ?SearchEngineKeywordId "
        
      ;
      public static void Delete(AdgroupKeyword adgroupKeyword, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?SearchEngineId", adgroupKeyword.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", adgroupKeyword.SearchEngineKeywordId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(AdgroupKeyword adgroupKeyword)
      {
        Delete(adgroupKeyword, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From AdgroupKeyword ";

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

      
        + " SearchEngineId, "
      
        + " SearchEngineKeywordId, "
      
        + " AdGroupId, "
      
        + " MatchType, "
      
        + " Status, "
      
        + " KeywordId "
      

      + " From AdgroupKeyword ";
      public static List<AdgroupKeyword> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<AdgroupKeyword> rv = new List<AdgroupKeyword>();

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

      public static List<AdgroupKeyword> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<AdgroupKeyword> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (AdgroupKeyword obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return SearchEngineId == obj.SearchEngineId && SearchEngineKeywordId == obj.SearchEngineKeywordId && AdGroupId == obj.AdGroupId && MatchType == obj.MatchType && Status == obj.Status && KeywordId == obj.KeywordId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<AdgroupKeyword> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(AdgroupKeyword));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(AdgroupKeyword item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<AdgroupKeyword>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(AdgroupKeyword));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<AdgroupKeyword> itemsList
      = new List<AdgroupKeyword>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is AdgroupKeyword)
      itemsList.Add(deserializedObject as AdgroupKeyword);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_searchEngineId;
      
        protected long m_searchEngineKeywordId;
      
        protected long m_adGroupId;
      
        protected int m_matchType;
      
        protected int m_status;
      
        protected int m_keywordId;
      
      #endregion

      #region Constructors
      public AdgroupKeyword(
      int 
          searchEngineId,long 
          searchEngineKeywordId
      ) : this()
      {
      
        m_searchEngineId = searchEngineId;
      
        m_searchEngineKeywordId = searchEngineKeywordId;
      
      }

      


        public AdgroupKeyword(
        int 
          searchEngineId,long 
          searchEngineKeywordId,long 
          adGroupId,int 
          matchType,int 
          status,int 
          keywordId
        ) : this()
        {
        
          m_searchEngineId = searchEngineId;
        
          m_searchEngineKeywordId = searchEngineKeywordId;
        
          m_adGroupId = adGroupId;
        
          m_matchType = matchType;
        
          m_status = status;
        
          m_keywordId = keywordId;
        
        }


      
      #endregion

      
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
        public long AdGroupId
        {
        get { return m_adGroupId;}
        set { m_adGroupId = value; }
        }
      
        [XmlElement]
        public int MatchType
        {
        get { return m_matchType;}
        set { m_matchType = value; }
        }
      
        [XmlElement]
        public int Status
        {
        get { return m_status;}
        set { m_status = value; }
        }
      
        [XmlElement]
        public int KeywordId
        {
        get { return m_keywordId;}
        set { m_keywordId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 6; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    