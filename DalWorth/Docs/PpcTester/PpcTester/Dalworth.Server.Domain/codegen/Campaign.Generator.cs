
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


      public partial class Campaign : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Campaign ( " +
      
        " Id, " +
      
        " SearchEngineId, " +
      
        " CompanyId, " +
      
        " Name, " +
      
        " Status, " +
      
        " TestDomain " +
      
      ") Values (" +
      
        " ?Id, " +
      
        " ?SearchEngineId, " +
      
        " ?CompanyId, " +
      
        " ?Name, " +
      
        " ?Status, " +
      
        " ?TestDomain " +
      
      ")";

      public static void Insert(Campaign campaign, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", campaign.Id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", campaign.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?CompanyId", campaign.CompanyId);
      
        Database.PutParameter(dbCommand,"?Name", campaign.Name);
      
        Database.PutParameter(dbCommand,"?Status", campaign.Status);
      
        Database.PutParameter(dbCommand,"?TestDomain", campaign.TestDomain);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(Campaign campaign)
      {
        Insert(campaign, null);
      }


      public static void Insert(List<Campaign>  campaignList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Campaign campaign in  campaignList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?Id", campaign.Id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", campaign.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?CompanyId", campaign.CompanyId);
      
        Database.PutParameter(dbCommand,"?Name", campaign.Name);
      
        Database.PutParameter(dbCommand,"?Status", campaign.Status);
      
        Database.PutParameter(dbCommand,"?TestDomain", campaign.TestDomain);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?Id",campaign.Id);
      
        Database.UpdateParameter(dbCommand,"?SearchEngineId",campaign.SearchEngineId);
      
        Database.UpdateParameter(dbCommand,"?CompanyId",campaign.CompanyId);
      
        Database.UpdateParameter(dbCommand,"?Name",campaign.Name);
      
        Database.UpdateParameter(dbCommand,"?Status",campaign.Status);
      
        Database.UpdateParameter(dbCommand,"?TestDomain",campaign.TestDomain);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<Campaign>  campaignList)
      {
        Insert(campaignList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Campaign Set "
      
        + " CompanyId = ?CompanyId, "
      
        + " Name = ?Name, "
      
        + " Status = ?Status, "
      
        + " TestDomain = ?TestDomain "
      
        + " Where "
        
          + " Id = ?Id and  "
        
          + " SearchEngineId = ?SearchEngineId "
        
      ;

      public static void Update(Campaign campaign, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", campaign.Id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", campaign.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?CompanyId", campaign.CompanyId);
      
        Database.PutParameter(dbCommand,"?Name", campaign.Name);
      
        Database.PutParameter(dbCommand,"?Status", campaign.Status);
      
        Database.PutParameter(dbCommand,"?TestDomain", campaign.TestDomain);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Campaign campaign)
      {
        Update(campaign, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " SearchEngineId, "
      
        + " CompanyId, "
      
        + " Name, "
      
        + " Status, "
      
        + " TestDomain "
      

      + " From Campaign "

      
        + " Where "
        
          + " Id = ?Id and  "
        
          + " SearchEngineId = ?SearchEngineId "
        
      ;

      public static Campaign FindByPrimaryKey(
      long id,int searchEngineId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", searchEngineId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Campaign not found, search by primary key");

      }

      public static Campaign FindByPrimaryKey(
      long id,int searchEngineId
      )
      {
      return FindByPrimaryKey(
      id,searchEngineId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Campaign campaign, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",campaign.Id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId",campaign.SearchEngineId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Campaign campaign)
      {
      return Exists(campaign, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Campaign limit 1";

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

      public static Campaign Load(IDataReader dataReader, int offset)
      {
      Campaign campaign = new Campaign();

      campaign.Id = dataReader.GetInt64(0 + offset);
          campaign.SearchEngineId = dataReader.GetInt32(1 + offset);
          campaign.CompanyId = dataReader.GetInt64(2 + offset);
          campaign.Name = dataReader.GetString(3 + offset);
          campaign.Status = dataReader.GetInt32(4 + offset);
          campaign.TestDomain = dataReader.GetString(5 + offset);
          

      return campaign;
      }

      public static Campaign Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Campaign "

      
        + " Where "
        
          + " Id = ?Id and  "
        
          + " SearchEngineId = ?SearchEngineId "
        
      ;
      public static void Delete(Campaign campaign, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", campaign.Id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", campaign.SearchEngineId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Campaign campaign)
      {
        Delete(campaign, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Campaign ";

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
      
        + " CompanyId, "
      
        + " Name, "
      
        + " Status, "
      
        + " TestDomain "
      

      + " From Campaign ";
      public static List<Campaign> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Campaign> rv = new List<Campaign>();

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

      public static List<Campaign> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Campaign> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Campaign obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return Id == obj.Id && SearchEngineId == obj.SearchEngineId && CompanyId == obj.CompanyId && Name == obj.Name && Status == obj.Status && TestDomain == obj.TestDomain;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Campaign> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Campaign));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Campaign item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Campaign>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Campaign));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Campaign> itemsList
      = new List<Campaign>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Campaign)
      itemsList.Add(deserializedObject as Campaign);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected long m_id;
      
        protected int m_searchEngineId;
      
        protected long m_companyId;
      
        protected String m_name;
      
        protected int m_status;
      
        protected String m_testDomain;
      
      #endregion

      #region Constructors
      public Campaign(
      long 
          id,int 
          searchEngineId
      ) : this()
      {
      
        m_id = id;
      
        m_searchEngineId = searchEngineId;
      
      }

      


        public Campaign(
        long 
          id,int 
          searchEngineId,long 
          companyId,String 
          name,int 
          status,String 
          testDomain
        ) : this()
        {
        
          m_id = id;
        
          m_searchEngineId = searchEngineId;
        
          m_companyId = companyId;
        
          m_name = name;
        
          m_status = status;
        
          m_testDomain = testDomain;
        
        }


      
      #endregion

      
        [XmlElement]
        public long Id
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
        public long CompanyId
        {
        get { return m_companyId;}
        set { m_companyId = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public int Status
        {
        get { return m_status;}
        set { m_status = value; }
        }
      
        [XmlElement]
        public String TestDomain
        {
        get { return m_testDomain;}
        set { m_testDomain = value; }
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

    