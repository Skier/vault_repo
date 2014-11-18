
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


      public partial class AdGroup : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into AdGroup ( " +
      
        " Id, " +
      
        " SearchEngineId, " +
      
        " CampaignId, " +
      
        " Name, " +
      
        " Status " +
      
      ") Values (" +
      
        " ?Id, " +
      
        " ?SearchEngineId, " +
      
        " ?CampaignId, " +
      
        " ?Name, " +
      
        " ?Status " +
      
      ")";

      public static void Insert(AdGroup adGroup, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", adGroup.Id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", adGroup.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?CampaignId", adGroup.CampaignId);
      
        Database.PutParameter(dbCommand,"?Name", adGroup.Name);
      
        Database.PutParameter(dbCommand,"?Status", adGroup.Status);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(AdGroup adGroup)
      {
        Insert(adGroup, null);
      }


      public static void Insert(List<AdGroup>  adGroupList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(AdGroup adGroup in  adGroupList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?Id", adGroup.Id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", adGroup.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?CampaignId", adGroup.CampaignId);
      
        Database.PutParameter(dbCommand,"?Name", adGroup.Name);
      
        Database.PutParameter(dbCommand,"?Status", adGroup.Status);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?Id",adGroup.Id);
      
        Database.UpdateParameter(dbCommand,"?SearchEngineId",adGroup.SearchEngineId);
      
        Database.UpdateParameter(dbCommand,"?CampaignId",adGroup.CampaignId);
      
        Database.UpdateParameter(dbCommand,"?Name",adGroup.Name);
      
        Database.UpdateParameter(dbCommand,"?Status",adGroup.Status);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<AdGroup>  adGroupList)
      {
        Insert(adGroupList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update AdGroup Set "
      
        + " CampaignId = ?CampaignId, "
      
        + " Name = ?Name, "
      
        + " Status = ?Status "
      
        + " Where "
        
          + " Id = ?Id and  "
        
          + " SearchEngineId = ?SearchEngineId "
        
      ;

      public static void Update(AdGroup adGroup, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", adGroup.Id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", adGroup.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?CampaignId", adGroup.CampaignId);
      
        Database.PutParameter(dbCommand,"?Name", adGroup.Name);
      
        Database.PutParameter(dbCommand,"?Status", adGroup.Status);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(AdGroup adGroup)
      {
        Update(adGroup, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " SearchEngineId, "
      
        + " CampaignId, "
      
        + " Name, "
      
        + " Status "
      

      + " From AdGroup "

      
        + " Where "
        
          + " Id = ?Id and  "
        
          + " SearchEngineId = ?SearchEngineId "
        
      ;

      public static AdGroup FindByPrimaryKey(
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
      throw new DataNotFoundException("AdGroup not found, search by primary key");

      }

      public static AdGroup FindByPrimaryKey(
      long id,int searchEngineId
      )
      {
      return FindByPrimaryKey(
      id,searchEngineId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(AdGroup adGroup, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",adGroup.Id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId",adGroup.SearchEngineId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(AdGroup adGroup)
      {
      return Exists(adGroup, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from AdGroup limit 1";

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

      public static AdGroup Load(IDataReader dataReader, int offset)
      {
      AdGroup adGroup = new AdGroup();

      adGroup.Id = dataReader.GetInt64(0 + offset);
          adGroup.SearchEngineId = dataReader.GetInt32(1 + offset);
          adGroup.CampaignId = dataReader.GetInt64(2 + offset);
          adGroup.Name = dataReader.GetString(3 + offset);
          adGroup.Status = dataReader.GetInt32(4 + offset);
          

      return adGroup;
      }

      public static AdGroup Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From AdGroup "

      
        + " Where "
        
          + " Id = ?Id and  "
        
          + " SearchEngineId = ?SearchEngineId "
        
      ;
      public static void Delete(AdGroup adGroup, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", adGroup.Id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", adGroup.SearchEngineId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(AdGroup adGroup)
      {
        Delete(adGroup, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From AdGroup ";

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
      
        + " Name, "
      
        + " Status "
      

      + " From AdGroup ";
      public static List<AdGroup> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<AdGroup> rv = new List<AdGroup>();

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

      public static List<AdGroup> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<AdGroup> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (AdGroup obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return Id == obj.Id && SearchEngineId == obj.SearchEngineId && CampaignId == obj.CampaignId && Name == obj.Name && Status == obj.Status;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<AdGroup> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(AdGroup));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(AdGroup item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<AdGroup>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(AdGroup));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<AdGroup> itemsList
      = new List<AdGroup>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is AdGroup)
      itemsList.Add(deserializedObject as AdGroup);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected long m_id;
      
        protected int m_searchEngineId;
      
        protected long m_campaignId;
      
        protected String m_name;
      
        protected int m_status;
      
      #endregion

      #region Constructors
      public AdGroup(
      long 
          id,int 
          searchEngineId
      ) : this()
      {
      
        m_id = id;
      
        m_searchEngineId = searchEngineId;
      
      }

      


        public AdGroup(
        long 
          id,int 
          searchEngineId,long 
          campaignId,String 
          name,int 
          status
        ) : this()
        {
        
          m_id = id;
        
          m_searchEngineId = searchEngineId;
        
          m_campaignId = campaignId;
        
          m_name = name;
        
          m_status = status;
        
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
        public long CampaignId
        {
        get { return m_campaignId;}
        set { m_campaignId = value; }
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

    