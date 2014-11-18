
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


      public partial class TestResult : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TestResult ( " +
      
        " TestBatchId, " +
      
        " SearchEngineId, " +
      
        " SearchEngineKeywordId, " +
      
        " DateRun, " +
      
        " AdPosition " +
      
      ") Values (" +
      
        " ?TestBatchId, " +
      
        " ?SearchEngineId, " +
      
        " ?SearchEngineKeywordId, " +
      
        " ?DateRun, " +
      
        " ?AdPosition " +
      
      ")";

      public static void Insert(TestResult testResult, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TestBatchId", testResult.TestBatchId);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", testResult.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", testResult.SearchEngineKeywordId);
      
        Database.PutParameter(dbCommand,"?DateRun", testResult.DateRun);
      
        Database.PutParameter(dbCommand,"?AdPosition", testResult.AdPosition);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        testResult.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(TestResult testResult)
      {
        Insert(testResult, null);
      }


      public static void Insert(List<TestResult>  testResultList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TestResult testResult in  testResultList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TestBatchId", testResult.TestBatchId);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", testResult.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", testResult.SearchEngineKeywordId);
      
        Database.PutParameter(dbCommand,"?DateRun", testResult.DateRun);
      
        Database.PutParameter(dbCommand,"?AdPosition", testResult.AdPosition);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TestBatchId",testResult.TestBatchId);
      
        Database.UpdateParameter(dbCommand,"?SearchEngineId",testResult.SearchEngineId);
      
        Database.UpdateParameter(dbCommand,"?SearchEngineKeywordId",testResult.SearchEngineKeywordId);
      
        Database.UpdateParameter(dbCommand,"?DateRun",testResult.DateRun);
      
        Database.UpdateParameter(dbCommand,"?AdPosition",testResult.AdPosition);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        testResult.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<TestResult>  testResultList)
      {
        Insert(testResultList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TestResult Set "
      
        + " TestBatchId = ?TestBatchId, "
      
        + " SearchEngineId = ?SearchEngineId, "
      
        + " SearchEngineKeywordId = ?SearchEngineKeywordId, "
      
        + " DateRun = ?DateRun, "
      
        + " AdPosition = ?AdPosition "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(TestResult testResult, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", testResult.Id);
      
        Database.PutParameter(dbCommand,"?TestBatchId", testResult.TestBatchId);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", testResult.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?SearchEngineKeywordId", testResult.SearchEngineKeywordId);
      
        Database.PutParameter(dbCommand,"?DateRun", testResult.DateRun);
      
        Database.PutParameter(dbCommand,"?AdPosition", testResult.AdPosition);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TestResult testResult)
      {
        Update(testResult, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " TestBatchId, "
      
        + " SearchEngineId, "
      
        + " SearchEngineKeywordId, "
      
        + " DateRun, "
      
        + " AdPosition "
      

      + " From TestResult "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static TestResult FindByPrimaryKey(
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
      throw new DataNotFoundException("TestResult not found, search by primary key");

      }

      public static TestResult FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TestResult testResult, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",testResult.Id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TestResult testResult)
      {
      return Exists(testResult, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TestResult limit 1";

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

      public static TestResult Load(IDataReader dataReader, int offset)
      {
      TestResult testResult = new TestResult();

      testResult.Id = dataReader.GetInt32(0 + offset);
          testResult.TestBatchId = dataReader.GetInt32(1 + offset);
          testResult.SearchEngineId = dataReader.GetInt32(2 + offset);
          testResult.SearchEngineKeywordId = dataReader.GetInt64(3 + offset);
          testResult.DateRun = dataReader.GetDateTime(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            testResult.AdPosition = dataReader.GetInt32(5 + offset);
          

      return testResult;
      }

      public static TestResult Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TestResult "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(TestResult testResult, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", testResult.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TestResult testResult)
      {
        Delete(testResult, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TestResult ";

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
      
        + " TestBatchId, "
      
        + " SearchEngineId, "
      
        + " SearchEngineKeywordId, "
      
        + " DateRun, "
      
        + " AdPosition "
      

      + " From TestResult ";
      public static List<TestResult> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TestResult> rv = new List<TestResult>();

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

      public static List<TestResult> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TestResult> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (TestResult obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return Id == obj.Id && TestBatchId == obj.TestBatchId && SearchEngineId == obj.SearchEngineId && SearchEngineKeywordId == obj.SearchEngineKeywordId && DateRun == obj.DateRun && AdPosition == obj.AdPosition;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<TestResult> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TestResult));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TestResult item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TestResult>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TestResult));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TestResult> itemsList
      = new List<TestResult>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TestResult)
      itemsList.Add(deserializedObject as TestResult);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_id;
      
        protected int m_testBatchId;
      
        protected int m_searchEngineId;
      
        protected long m_searchEngineKeywordId;
      
        protected DateTime m_dateRun;
      
        protected int? m_adPosition;
      
      #endregion

      #region Constructors
      public TestResult(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public TestResult(
        int 
          id,int 
          testBatchId,int 
          searchEngineId,long 
          searchEngineKeywordId,DateTime 
          dateRun,int? 
          adPosition
        ) : this()
        {
        
          m_id = id;
        
          m_testBatchId = testBatchId;
        
          m_searchEngineId = searchEngineId;
        
          m_searchEngineKeywordId = searchEngineKeywordId;
        
          m_dateRun = dateRun;
        
          m_adPosition = adPosition;
        
        }


      
      #endregion

      
        [XmlElement]
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        [XmlElement]
        public int TestBatchId
        {
        get { return m_testBatchId;}
        set { m_testBatchId = value; }
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
        public DateTime DateRun
        {
        get { return m_dateRun;}
        set { m_dateRun = value; }
        }
      
        [XmlElement]
        public int? AdPosition
        {
        get { return m_adPosition;}
        set { m_adPosition = value; }
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

    