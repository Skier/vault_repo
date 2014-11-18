
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


      public partial class LeadStatus : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into LeadStatus ( " +
      
        " ID, " +
      
        " Status, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Status, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(LeadStatus leadStatus, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", leadStatus.ID);
      
        Database.PutParameter(dbCommand,"?Status", leadStatus.Status);
      
        Database.PutParameter(dbCommand,"?Description", leadStatus.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(LeadStatus leadStatus)
      {
        Insert(leadStatus, null);
      }


      public static void Insert(List<LeadStatus>  leadStatusList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(LeadStatus leadStatus in  leadStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", leadStatus.ID);
      
        Database.PutParameter(dbCommand,"?Status", leadStatus.Status);
      
        Database.PutParameter(dbCommand,"?Description", leadStatus.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",leadStatus.ID);
      
        Database.UpdateParameter(dbCommand,"?Status",leadStatus.Status);
      
        Database.UpdateParameter(dbCommand,"?Description",leadStatus.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<LeadStatus>  leadStatusList)
      {
        Insert(leadStatusList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update LeadStatus Set "
      
        + " Status = ?Status, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(LeadStatus leadStatus, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", leadStatus.ID);
      
        Database.PutParameter(dbCommand,"?Status", leadStatus.Status);
      
        Database.PutParameter(dbCommand,"?Description", leadStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(LeadStatus leadStatus)
      {
        Update(leadStatus, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Status, "
      
        + " Description "
      

      + " From LeadStatus "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static LeadStatus FindByPrimaryKey(
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
      throw new DataNotFoundException("LeadStatus not found, search by primary key");

      }

      public static LeadStatus FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(LeadStatus leadStatus, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",leadStatus.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(LeadStatus leadStatus)
      {
      return Exists(leadStatus, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from LeadStatus limit 1";

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

      public static LeadStatus Load(IDataReader dataReader, int offset)
      {
      LeadStatus leadStatus = new LeadStatus();

      leadStatus.ID = dataReader.GetInt32(0 + offset);
          leadStatus.Status = dataReader.GetString(1 + offset);
          leadStatus.Description = dataReader.GetString(2 + offset);
          

      return leadStatus;
      }

      public static LeadStatus Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From LeadStatus "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(LeadStatus leadStatus, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", leadStatus.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(LeadStatus leadStatus)
      {
        Delete(leadStatus, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From LeadStatus ";

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
      
        + " Status, "
      
        + " Description "
      

      + " From LeadStatus ";
      public static List<LeadStatus> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<LeadStatus> rv = new List<LeadStatus>();

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

      public static List<LeadStatus> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<LeadStatus> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (LeadStatus obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Status == obj.Status && Description == obj.Description;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<LeadStatus> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadStatus));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(LeadStatus item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<LeadStatus>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeadStatus));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<LeadStatus> itemsList
      = new List<LeadStatus>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is LeadStatus)
      itemsList.Add(deserializedObject as LeadStatus);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_status;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public LeadStatus(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public LeadStatus(
        int 
          iD,String 
          status,String 
          description
        ) : this()
        {
        
          m_iD = iD;
        
          m_status = status;
        
          m_description = description;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String Status
        {
        get { return m_status;}
        set { m_status = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      

      public static int FieldsCount
      {
      get { return 3; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    