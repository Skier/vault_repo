
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


      public partial class TestResultDetail : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TestResultDetail ( " +
      
        " TestResultId, " +
      
        " AdPosition, " +
      
        " Headline, " +
      
        " DescriptionLine1, " +
      
        " DescriptionLine2, " +
      
        " DisplayUrl " +
      
      ") Values (" +
      
        " ?TestResultId, " +
      
        " ?AdPosition, " +
      
        " ?Headline, " +
      
        " ?DescriptionLine1, " +
      
        " ?DescriptionLine2, " +
      
        " ?DisplayUrl " +
      
      ")";

      public static void Insert(TestResultDetail testResultDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TestResultId", testResultDetail.TestResultId);
      
        Database.PutParameter(dbCommand,"?AdPosition", testResultDetail.AdPosition);
      
        Database.PutParameter(dbCommand,"?Headline", testResultDetail.Headline);
      
        Database.PutParameter(dbCommand,"?DescriptionLine1", testResultDetail.DescriptionLine1);
      
        Database.PutParameter(dbCommand,"?DescriptionLine2", testResultDetail.DescriptionLine2);
      
        Database.PutParameter(dbCommand,"?DisplayUrl", testResultDetail.DisplayUrl);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        testResultDetail.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(TestResultDetail testResultDetail)
      {
        Insert(testResultDetail, null);
      }


      public static void Insert(List<TestResultDetail>  testResultDetailList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TestResultDetail testResultDetail in  testResultDetailList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TestResultId", testResultDetail.TestResultId);
      
        Database.PutParameter(dbCommand,"?AdPosition", testResultDetail.AdPosition);
      
        Database.PutParameter(dbCommand,"?Headline", testResultDetail.Headline);
      
        Database.PutParameter(dbCommand,"?DescriptionLine1", testResultDetail.DescriptionLine1);
      
        Database.PutParameter(dbCommand,"?DescriptionLine2", testResultDetail.DescriptionLine2);
      
        Database.PutParameter(dbCommand,"?DisplayUrl", testResultDetail.DisplayUrl);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TestResultId",testResultDetail.TestResultId);
      
        Database.UpdateParameter(dbCommand,"?AdPosition",testResultDetail.AdPosition);
      
        Database.UpdateParameter(dbCommand,"?Headline",testResultDetail.Headline);
      
        Database.UpdateParameter(dbCommand,"?DescriptionLine1",testResultDetail.DescriptionLine1);
      
        Database.UpdateParameter(dbCommand,"?DescriptionLine2",testResultDetail.DescriptionLine2);
      
        Database.UpdateParameter(dbCommand,"?DisplayUrl",testResultDetail.DisplayUrl);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        testResultDetail.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<TestResultDetail>  testResultDetailList)
      {
        Insert(testResultDetailList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TestResultDetail Set "
      
        + " TestResultId = ?TestResultId, "
      
        + " AdPosition = ?AdPosition, "
      
        + " Headline = ?Headline, "
      
        + " DescriptionLine1 = ?DescriptionLine1, "
      
        + " DescriptionLine2 = ?DescriptionLine2, "
      
        + " DisplayUrl = ?DisplayUrl "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(TestResultDetail testResultDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", testResultDetail.ID);
      
        Database.PutParameter(dbCommand,"?TestResultId", testResultDetail.TestResultId);
      
        Database.PutParameter(dbCommand,"?AdPosition", testResultDetail.AdPosition);
      
        Database.PutParameter(dbCommand,"?Headline", testResultDetail.Headline);
      
        Database.PutParameter(dbCommand,"?DescriptionLine1", testResultDetail.DescriptionLine1);
      
        Database.PutParameter(dbCommand,"?DescriptionLine2", testResultDetail.DescriptionLine2);
      
        Database.PutParameter(dbCommand,"?DisplayUrl", testResultDetail.DisplayUrl);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TestResultDetail testResultDetail)
      {
        Update(testResultDetail, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " TestResultId, "
      
        + " AdPosition, "
      
        + " Headline, "
      
        + " DescriptionLine1, "
      
        + " DescriptionLine2, "
      
        + " DisplayUrl "
      

      + " From TestResultDetail "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static TestResultDetail FindByPrimaryKey(
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
      throw new DataNotFoundException("TestResultDetail not found, search by primary key");

      }

      public static TestResultDetail FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TestResultDetail testResultDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",testResultDetail.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TestResultDetail testResultDetail)
      {
      return Exists(testResultDetail, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TestResultDetail limit 1";

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

      public static TestResultDetail Load(IDataReader dataReader, int offset)
      {
      TestResultDetail testResultDetail = new TestResultDetail();

      testResultDetail.ID = dataReader.GetInt32(0 + offset);
          testResultDetail.TestResultId = dataReader.GetInt32(1 + offset);
          testResultDetail.AdPosition = dataReader.GetInt32(2 + offset);
          testResultDetail.Headline = dataReader.GetString(3 + offset);
          testResultDetail.DescriptionLine1 = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            testResultDetail.DescriptionLine2 = dataReader.GetString(5 + offset);
          testResultDetail.DisplayUrl = dataReader.GetString(6 + offset);
          

      return testResultDetail;
      }

      public static TestResultDetail Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TestResultDetail "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(TestResultDetail testResultDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", testResultDetail.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TestResultDetail testResultDetail)
      {
        Delete(testResultDetail, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TestResultDetail ";

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
      
        + " TestResultId, "
      
        + " AdPosition, "
      
        + " Headline, "
      
        + " DescriptionLine1, "
      
        + " DescriptionLine2, "
      
        + " DisplayUrl "
      

      + " From TestResultDetail ";
      public static List<TestResultDetail> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TestResultDetail> rv = new List<TestResultDetail>();

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

      public static List<TestResultDetail> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TestResultDetail> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (TestResultDetail obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && TestResultId == obj.TestResultId && AdPosition == obj.AdPosition && Headline == obj.Headline && DescriptionLine1 == obj.DescriptionLine1 && DescriptionLine2 == obj.DescriptionLine2 && DisplayUrl == obj.DisplayUrl;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<TestResultDetail> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TestResultDetail));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TestResultDetail item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TestResultDetail>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TestResultDetail));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TestResultDetail> itemsList
      = new List<TestResultDetail>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TestResultDetail)
      itemsList.Add(deserializedObject as TestResultDetail);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_testResultId;
      
        protected int m_adPosition;
      
        protected String m_headline;
      
        protected String m_descriptionLine1;
      
        protected String m_descriptionLine2;
      
        protected String m_displayUrl;
      
      #endregion

      #region Constructors
      public TestResultDetail(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public TestResultDetail(
        int 
          iD,int 
          testResultId,int 
          adPosition,String 
          headline,String 
          descriptionLine1,String 
          descriptionLine2,String 
          displayUrl
        ) : this()
        {
        
          m_iD = iD;
        
          m_testResultId = testResultId;
        
          m_adPosition = adPosition;
        
          m_headline = headline;
        
          m_descriptionLine1 = descriptionLine1;
        
          m_descriptionLine2 = descriptionLine2;
        
          m_displayUrl = displayUrl;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int TestResultId
        {
        get { return m_testResultId;}
        set { m_testResultId = value; }
        }
      
        [XmlElement]
        public int AdPosition
        {
        get { return m_adPosition;}
        set { m_adPosition = value; }
        }
      
        [XmlElement]
        public String Headline
        {
        get { return m_headline;}
        set { m_headline = value; }
        }
      
        [XmlElement]
        public String DescriptionLine1
        {
        get { return m_descriptionLine1;}
        set { m_descriptionLine1 = value; }
        }
      
        [XmlElement]
        public String DescriptionLine2
        {
        get { return m_descriptionLine2;}
        set { m_descriptionLine2 = value; }
        }
      
        [XmlElement]
        public String DisplayUrl
        {
        get { return m_displayUrl;}
        set { m_displayUrl = value; }
        }
      

      public static int FieldsCount
      {
      get { return 7; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    