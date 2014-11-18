
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


      public partial class Action : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Action ( " +
      
        " DateCreated, " +
      
        " DateExecuted, " +
      
        " SearchEngineId, " +
      
        " SearchEngineKeywordId, " +
      
        " MatchType, " +
      
        " TestResultId, " +
      
        " OriginalMaxCPC, " +
      
        " NewMaxCPC, " +
      
        " Note, " +
      
        " DateCompleted " +
      
      ") Values (" +
      
        " ?DateCreated, " +
      
        " ?DateExecuted, " +
      
        " ?SearchEngineId, " +
      
        " ?SearchEngineKeywordId, " +
      
        " ?MatchType, " +
      
        " ?TestResultId, " +
      
        " ?OriginalMaxCPC, " +
      
        " ?NewMaxCPC, " +
      
        " ?Note, " +
      
        " ?DateCompleted " +
      
      ")";

      public static void Insert(Action action, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?DateCreated", action.DateCreated);
      
        Database.PutParameter(dbCommand,"?DateExecuted", action.DateExecuted);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", action.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", action.SearchEngineKeywordId);
      
        Database.PutParameter(dbCommand,"?MatchType", action.MatchType);
      
        Database.PutParameter(dbCommand,"?TestResultId", action.TestResultId);
      
        Database.PutParameter(dbCommand,"?OriginalMaxCPC", action.OriginalMaxCPC);
      
        Database.PutParameter(dbCommand,"?NewMaxCPC", action.NewMaxCPC);
      
        Database.PutParameter(dbCommand,"?Note", action.Note);
      
        Database.PutParameter(dbCommand,"?DateCompleted", action.DateCompleted);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        action.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Action action)
      {
        Insert(action, null);
      }


      public static void Insert(List<Action>  actionList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Action action in  actionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?DateCreated", action.DateCreated);
      
        Database.PutParameter(dbCommand,"?DateExecuted", action.DateExecuted);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", action.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", action.SearchEngineKeywordId);
      
        Database.PutParameter(dbCommand,"?MatchType", action.MatchType);
      
        Database.PutParameter(dbCommand,"?TestResultId", action.TestResultId);
      
        Database.PutParameter(dbCommand,"?OriginalMaxCPC", action.OriginalMaxCPC);
      
        Database.PutParameter(dbCommand,"?NewMaxCPC", action.NewMaxCPC);
      
        Database.PutParameter(dbCommand,"?Note", action.Note);
      
        Database.PutParameter(dbCommand,"?DateCompleted", action.DateCompleted);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?DateCreated",action.DateCreated);
      
        Database.UpdateParameter(dbCommand,"?DateExecuted",action.DateExecuted);
      
        Database.UpdateParameter(dbCommand,"?SearchEngineId",action.SearchEngineId);
      
        Database.UpdateParameter(dbCommand,"?SearchEngineKeywordId",action.SearchEngineKeywordId);
      
        Database.UpdateParameter(dbCommand,"?MatchType",action.MatchType);
      
        Database.UpdateParameter(dbCommand,"?TestResultId",action.TestResultId);
      
        Database.UpdateParameter(dbCommand,"?OriginalMaxCPC",action.OriginalMaxCPC);
      
        Database.UpdateParameter(dbCommand,"?NewMaxCPC",action.NewMaxCPC);
      
        Database.UpdateParameter(dbCommand,"?Note",action.Note);
      
        Database.UpdateParameter(dbCommand,"?DateCompleted",action.DateCompleted);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        action.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Action>  actionList)
      {
        Insert(actionList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Action Set "
      
        + " DateCreated = ?DateCreated, "
      
        + " DateExecuted = ?DateExecuted, "
      
        + " SearchEngineId = ?SearchEngineId, "
      
        + " SearchEngineKeywordId = ?SearchEngineKeywordId, "
      
        + " MatchType = ?MatchType, "
      
        + " TestResultId = ?TestResultId, "
      
        + " OriginalMaxCPC = ?OriginalMaxCPC, "
      
        + " NewMaxCPC = ?NewMaxCPC, "
      
        + " Note = ?Note, "
      
        + " DateCompleted = ?DateCompleted "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(Action action, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", action.Id);
      
        Database.PutParameter(dbCommand,"?DateCreated", action.DateCreated);
      
        Database.PutParameter(dbCommand,"?DateExecuted", action.DateExecuted);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", action.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", action.SearchEngineKeywordId);
      
        Database.PutParameter(dbCommand,"?MatchType", action.MatchType);
      
        Database.PutParameter(dbCommand,"?TestResultId", action.TestResultId);
      
        Database.PutParameter(dbCommand,"?OriginalMaxCPC", action.OriginalMaxCPC);
      
        Database.PutParameter(dbCommand,"?NewMaxCPC", action.NewMaxCPC);
      
        Database.PutParameter(dbCommand,"?Note", action.Note);
      
        Database.PutParameter(dbCommand,"?DateCompleted", action.DateCompleted);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Action action)
      {
        Update(action, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " DateCreated, "
      
        + " DateExecuted, "
      
        + " SearchEngineId, "
      
        + " SearchEngineKeywordId, "
      
        + " MatchType, "
      
        + " TestResultId, "
      
        + " OriginalMaxCPC, "
      
        + " NewMaxCPC, "
      
        + " Note, "
      
        + " DateCompleted "
      

      + " From Action "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static Action FindByPrimaryKey(
      int id, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Action not found, search by primary key");

      }

      public static Action FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Action action, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",action.Id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Action action)
      {
      return Exists(action, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Action limit 1";

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

      public static Action Load(IDataReader dataReader, int offset)
      {
      Action action = new Action();

      action.Id = dataReader.GetInt32(0 + offset);
          action.DateCreated = dataReader.GetDateTime(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            action.DateExecuted = dataReader.GetDateTime(2 + offset);
          action.SearchEngineId = dataReader.GetInt32(3 + offset);
          action.SearchEngineKeywordId = dataReader.GetInt64(4 + offset);
          action.MatchType = dataReader.GetInt32(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            action.TestResultId = dataReader.GetInt32(6 + offset);
          action.OriginalMaxCPC = dataReader.GetDecimal(7 + offset);
          action.NewMaxCPC = dataReader.GetDecimal(8 + offset);
          action.Note = dataReader.GetString(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            action.DateCompleted = dataReader.GetDateTime(10 + offset);
          

      return action;
      }

      public static Action Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Action "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(Action action, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", action.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Action action)
      {
        Delete(action, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Action ";

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

      
        + " Id, "
      
        + " DateCreated, "
      
        + " DateExecuted, "
      
        + " SearchEngineId, "
      
        + " SearchEngineKeywordId, "
      
        + " MatchType, "
      
        + " TestResultId, "
      
        + " OriginalMaxCPC, "
      
        + " NewMaxCPC, "
      
        + " Note, "
      
        + " DateCompleted "
      

      + " From Action ";
      public static List<Action> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Action> rv = new List<Action>();

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

      public static List<Action> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Action> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Action obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return Id == obj.Id && DateCreated == obj.DateCreated && DateExecuted == obj.DateExecuted && SearchEngineId == obj.SearchEngineId && SearchEngineKeywordId == obj.SearchEngineKeywordId && MatchType == obj.MatchType && TestResultId == obj.TestResultId && OriginalMaxCPC == obj.OriginalMaxCPC && NewMaxCPC == obj.NewMaxCPC && Note == obj.Note && DateCompleted == obj.DateCompleted;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Action> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Action));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Action item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Action>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Action));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Action> itemsList
      = new List<Action>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Action)
      itemsList.Add(deserializedObject as Action);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_id;
      
        protected DateTime m_dateCreated;
      
        protected DateTime? m_dateExecuted;
      
        protected int m_searchEngineId;
      
        protected long m_searchEngineKeywordId;
      
        protected int m_matchType;
      
        protected int? m_testResultId;
      
        protected decimal m_originalMaxCPC;
      
        protected decimal m_newMaxCPC;
      
        protected String m_note;
      
        protected DateTime? m_dateCompleted;
      
      #endregion

      #region Constructors
      public Action(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public Action(
        int 
          id,DateTime 
          dateCreated,DateTime? 
          dateExecuted,int 
          searchEngineId,long 
          searchEngineKeywordId,int 
          matchType,int? 
          testResultId,decimal 
          originalMaxCPC,decimal 
          newMaxCPC,String 
          note,DateTime? 
          dateCompleted
        ) : this()
        {
        
          m_id = id;
        
          m_dateCreated = dateCreated;
        
          m_dateExecuted = dateExecuted;
        
          m_searchEngineId = searchEngineId;
        
          m_searchEngineKeywordId = searchEngineKeywordId;
        
          m_matchType = matchType;
        
          m_testResultId = testResultId;
        
          m_originalMaxCPC = originalMaxCPC;
        
          m_newMaxCPC = newMaxCPC;
        
          m_note = note;
        
          m_dateCompleted = dateCompleted;
        
        }


      
      #endregion

      
        [XmlElement]
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        [XmlElement]
        public DateTime DateCreated
        {
        get { return m_dateCreated;}
        set { m_dateCreated = value; }
        }
      
        [XmlElement]
        public DateTime? DateExecuted
        {
        get { return m_dateExecuted;}
        set { m_dateExecuted = value; }
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
        public int MatchType
        {
        get { return m_matchType;}
        set { m_matchType = value; }
        }
      
        [XmlElement]
        public int? TestResultId
        {
        get { return m_testResultId;}
        set { m_testResultId = value; }
        }
      
        [XmlElement]
        public decimal OriginalMaxCPC
        {
        get { return m_originalMaxCPC;}
        set { m_originalMaxCPC = value; }
        }
      
        [XmlElement]
        public decimal NewMaxCPC
        {
        get { return m_newMaxCPC;}
        set { m_newMaxCPC = value; }
        }
      
        [XmlElement]
        public String Note
        {
        get { return m_note;}
        set { m_note = value; }
        }
      
        [XmlElement]
        public DateTime? DateCompleted
        {
        get { return m_dateCompleted;}
        set { m_dateCompleted = value; }
        }
      

      public static int FieldsCount
      {
      get { return 11; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    