
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


      public partial class BackgroundJobPending : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into BackgroundJobPending ( " +
      
        " BackgroundJobTypeId, " +
      
        " LeadId, " +
      
        " ProjectId, " +
      
        " ProjectFeedbackId, " +
      
        " PartnerInvitationKey " +
      
      ") Values (" +
      
        " ?BackgroundJobTypeId, " +
      
        " ?LeadId, " +
      
        " ?ProjectId, " +
      
        " ?ProjectFeedbackId, " +
      
        " ?PartnerInvitationKey " +
      
      ")";

      public static void Insert(BackgroundJobPending backgroundJobPending, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?BackgroundJobTypeId", backgroundJobPending.BackgroundJobTypeId);
      
        Database.PutParameter(dbCommand,"?LeadId", backgroundJobPending.LeadId);
      
        Database.PutParameter(dbCommand,"?ProjectId", backgroundJobPending.ProjectId);
      
        Database.PutParameter(dbCommand,"?ProjectFeedbackId", backgroundJobPending.ProjectFeedbackId);
      
        Database.PutParameter(dbCommand,"?PartnerInvitationKey", backgroundJobPending.PartnerInvitationKey);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        backgroundJobPending.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(BackgroundJobPending backgroundJobPending)
      {
        Insert(backgroundJobPending, null);
      }


      public static void Insert(List<BackgroundJobPending>  backgroundJobPendingList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(BackgroundJobPending backgroundJobPending in  backgroundJobPendingList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?BackgroundJobTypeId", backgroundJobPending.BackgroundJobTypeId);
      
        Database.PutParameter(dbCommand,"?LeadId", backgroundJobPending.LeadId);
      
        Database.PutParameter(dbCommand,"?ProjectId", backgroundJobPending.ProjectId);
      
        Database.PutParameter(dbCommand,"?ProjectFeedbackId", backgroundJobPending.ProjectFeedbackId);
      
        Database.PutParameter(dbCommand,"?PartnerInvitationKey", backgroundJobPending.PartnerInvitationKey);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?BackgroundJobTypeId",backgroundJobPending.BackgroundJobTypeId);
      
        Database.UpdateParameter(dbCommand,"?LeadId",backgroundJobPending.LeadId);
      
        Database.UpdateParameter(dbCommand,"?ProjectId",backgroundJobPending.ProjectId);
      
        Database.UpdateParameter(dbCommand,"?ProjectFeedbackId",backgroundJobPending.ProjectFeedbackId);
      
        Database.UpdateParameter(dbCommand,"?PartnerInvitationKey",backgroundJobPending.PartnerInvitationKey);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        backgroundJobPending.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<BackgroundJobPending>  backgroundJobPendingList)
      {
        Insert(backgroundJobPendingList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update BackgroundJobPending Set "
      
        + " BackgroundJobTypeId = ?BackgroundJobTypeId, "
      
        + " LeadId = ?LeadId, "
      
        + " ProjectId = ?ProjectId, "
      
        + " ProjectFeedbackId = ?ProjectFeedbackId, "
      
        + " PartnerInvitationKey = ?PartnerInvitationKey "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(BackgroundJobPending backgroundJobPending, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", backgroundJobPending.ID);
      
        Database.PutParameter(dbCommand,"?BackgroundJobTypeId", backgroundJobPending.BackgroundJobTypeId);
      
        Database.PutParameter(dbCommand,"?LeadId", backgroundJobPending.LeadId);
      
        Database.PutParameter(dbCommand,"?ProjectId", backgroundJobPending.ProjectId);
      
        Database.PutParameter(dbCommand,"?ProjectFeedbackId", backgroundJobPending.ProjectFeedbackId);
      
        Database.PutParameter(dbCommand,"?PartnerInvitationKey", backgroundJobPending.PartnerInvitationKey);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(BackgroundJobPending backgroundJobPending)
      {
        Update(backgroundJobPending, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " BackgroundJobTypeId, "
      
        + " LeadId, "
      
        + " ProjectId, "
      
        + " ProjectFeedbackId, "
      
        + " PartnerInvitationKey "
      

      + " From BackgroundJobPending "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static BackgroundJobPending FindByPrimaryKey(
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
      throw new DataNotFoundException("BackgroundJobPending not found, search by primary key");

      }

      public static BackgroundJobPending FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(BackgroundJobPending backgroundJobPending, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",backgroundJobPending.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(BackgroundJobPending backgroundJobPending)
      {
      return Exists(backgroundJobPending, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from BackgroundJobPending limit 1";

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

      public static BackgroundJobPending Load(IDataReader dataReader, int offset)
      {
      BackgroundJobPending backgroundJobPending = new BackgroundJobPending();

      backgroundJobPending.ID = dataReader.GetInt32(0 + offset);
          backgroundJobPending.BackgroundJobTypeId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            backgroundJobPending.LeadId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            backgroundJobPending.ProjectId = dataReader.GetInt32(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            backgroundJobPending.ProjectFeedbackId = dataReader.GetInt32(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            backgroundJobPending.PartnerInvitationKey = dataReader.GetString(5 + offset);
          

      return backgroundJobPending;
      }

      public static BackgroundJobPending Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From BackgroundJobPending "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(BackgroundJobPending backgroundJobPending, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", backgroundJobPending.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(BackgroundJobPending backgroundJobPending)
      {
        Delete(backgroundJobPending, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From BackgroundJobPending ";

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
      
        + " BackgroundJobTypeId, "
      
        + " LeadId, "
      
        + " ProjectId, "
      
        + " ProjectFeedbackId, "
      
        + " PartnerInvitationKey "
      

      + " From BackgroundJobPending ";
      public static List<BackgroundJobPending> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<BackgroundJobPending> rv = new List<BackgroundJobPending>();

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

      public static List<BackgroundJobPending> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<BackgroundJobPending> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (BackgroundJobPending obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && BackgroundJobTypeId == obj.BackgroundJobTypeId && LeadId == obj.LeadId && ProjectId == obj.ProjectId && ProjectFeedbackId == obj.ProjectFeedbackId && PartnerInvitationKey == obj.PartnerInvitationKey;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<BackgroundJobPending> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(BackgroundJobPending));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(BackgroundJobPending item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<BackgroundJobPending>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(BackgroundJobPending));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<BackgroundJobPending> itemsList
      = new List<BackgroundJobPending>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is BackgroundJobPending)
      itemsList.Add(deserializedObject as BackgroundJobPending);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_backgroundJobTypeId;
      
        protected int? m_leadId;
      
        protected int? m_projectId;
      
        protected int? m_projectFeedbackId;
      
        protected String m_partnerInvitationKey;
      
      #endregion

      #region Constructors
      public BackgroundJobPending(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public BackgroundJobPending(
        int 
          iD,int 
          backgroundJobTypeId,int? 
          leadId,int? 
          projectId,int? 
          projectFeedbackId,String 
          partnerInvitationKey
        ) : this()
        {
        
          m_iD = iD;
        
          m_backgroundJobTypeId = backgroundJobTypeId;
        
          m_leadId = leadId;
        
          m_projectId = projectId;
        
          m_projectFeedbackId = projectFeedbackId;
        
          m_partnerInvitationKey = partnerInvitationKey;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int BackgroundJobTypeId
        {
        get { return m_backgroundJobTypeId;}
        set { m_backgroundJobTypeId = value; }
        }
      
        [XmlElement]
        public int? LeadId
        {
        get { return m_leadId;}
        set { m_leadId = value; }
        }
      
        [XmlElement]
        public int? ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
        }
      
        [XmlElement]
        public int? ProjectFeedbackId
        {
        get { return m_projectFeedbackId;}
        set { m_projectFeedbackId = value; }
        }
      
        [XmlElement]
        public String PartnerInvitationKey
        {
        get { return m_partnerInvitationKey;}
        set { m_partnerInvitationKey = value; }
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

    