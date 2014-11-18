
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


      public partial class TestBatch : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TestBatch ( " +
      
        " SearchEngineId, " +
      
        " CampaignId, " +
      
        " AdgroupId, " +
      
        " DateRun " +
      
      ") Values (" +
      
        " ?SearchEngineId, " +
      
        " ?CampaignId, " +
      
        " ?AdgroupId, " +
      
        " ?DateRun " +
      
      ")";

      public static void Insert(TestBatch testBatch, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?SearchEngineId", testBatch.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?CampaignId", testBatch.CampaignId);
      
        Database.PutParameter(dbCommand,"?AdgroupId", testBatch.AdgroupId);
      
        Database.PutParameter(dbCommand,"?DateRun", testBatch.DateRun);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        testBatch.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(TestBatch testBatch)
      {
        Insert(testBatch, null);
      }


      public static void Insert(List<TestBatch>  testBatchList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TestBatch testBatch in  testBatchList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?SearchEngineId", testBatch.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?CampaignId", testBatch.CampaignId);
      
        Database.PutParameter(dbCommand,"?AdgroupId", testBatch.AdgroupId);
      
        Database.PutParameter(dbCommand,"?DateRun", testBatch.DateRun);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?SearchEngineId",testBatch.SearchEngineId);
      
        Database.UpdateParameter(dbCommand,"?CampaignId",testBatch.CampaignId);
      
        Database.UpdateParameter(dbCommand,"?AdgroupId",testBatch.AdgroupId);
      
        Database.UpdateParameter(dbCommand,"?DateRun",testBatch.DateRun);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        testBatch.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<TestBatch>  testBatchList)
      {
        Insert(testBatchList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TestBatch Set "
      
        + " SearchEngineId = ?SearchEngineId, "
      
        + " CampaignId = ?CampaignId, "
      
        + " AdgroupId = ?AdgroupId, "
      
        + " DateRun = ?DateRun "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(TestBatch testBatch, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", testBatch.Id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", testBatch.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?CampaignId", testBatch.CampaignId);
      
        Database.PutParameter(dbCommand,"?AdgroupId", testBatch.AdgroupId);
      
        Database.PutParameter(dbCommand,"?DateRun", testBatch.DateRun);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TestBatch testBatch)
      {
        Update(testBatch, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " SearchEngineId, "
      
        + " CampaignId, "
      
        + " AdgroupId, "
      
        + " DateRun "
      

      + " From TestBatch "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static TestBatch FindByPrimaryKey(
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
      throw new DataNotFoundException("TestBatch not found, search by primary key");

      }

      public static TestBatch FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TestBatch testBatch, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",testBatch.Id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TestBatch testBatch)
      {
      return Exists(testBatch, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TestBatch limit 1";

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

      public static TestBatch Load(IDataReader dataReader, int offset)
      {
      TestBatch testBatch = new TestBatch();

      testBatch.Id = dataReader.GetInt32(0 + offset);
          testBatch.SearchEngineId = dataReader.GetInt32(1 + offset);
          testBatch.CampaignId = dataReader.GetInt64(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            testBatch.AdgroupId = dataReader.GetInt64(3 + offset);
          testBatch.DateRun = dataReader.GetDateTime(4 + offset);
          

      return testBatch;
      }

      public static TestBatch Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TestBatch "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(TestBatch testBatch, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", testBatch.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TestBatch testBatch)
      {
        Delete(testBatch, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TestBatch ";

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
      
        + " SearchEngineId, "
      
        + " CampaignId, "
      
        + " AdgroupId, "
      
        + " DateRun "
      

      + " From TestBatch ";
      public static List<TestBatch> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TestBatch> rv = new List<TestBatch>();

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

      public static List<TestBatch> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TestBatch> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (TestBatch obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return Id == obj.Id && SearchEngineId == obj.SearchEngineId && CampaignId == obj.CampaignId && AdgroupId == obj.AdgroupId && DateRun == obj.DateRun;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<TestBatch> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TestBatch));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TestBatch item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TestBatch>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TestBatch));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TestBatch> itemsList
      = new List<TestBatch>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TestBatch)
      itemsList.Add(deserializedObject as TestBatch);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_id;
      
        protected int m_searchEngineId;
      
        protected long m_campaignId;
      
        protected long m_adgroupId;
      
        protected DateTime m_dateRun;
      
      #endregion

      #region Constructors
      public TestBatch(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public TestBatch(
        int 
          id,int 
          searchEngineId,long 
          campaignId,long 
          adgroupId,DateTime 
          dateRun
        ) : this()
        {
        
          m_id = id;
        
          m_searchEngineId = searchEngineId;
        
          m_campaignId = campaignId;
        
          m_adgroupId = adgroupId;
        
          m_dateRun = dateRun;
        
        }


      
      #endregion

      
        [XmlElement]
        public int Id
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
        public long CampaignId
        {
        get { return m_campaignId;}
        set { m_campaignId = value; }
        }
      
        [XmlElement]
        public long AdgroupId
        {
        get { return m_adgroupId;}
        set { m_adgroupId = value; }
        }
      
        [XmlElement]
        public DateTime DateRun
        {
        get { return m_dateRun;}
        set { m_dateRun = value; }
        }
      

      public static int FieldsCount
      {
      get { return 5; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    