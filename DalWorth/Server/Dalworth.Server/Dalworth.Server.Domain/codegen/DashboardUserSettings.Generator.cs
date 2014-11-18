
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


      public partial class DashboardUserSettings : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into DashboardUserSettings ( " +
      
        " DispatchId, " +
      
        " TechnicianId, " +
      
        " IsVisible, " +
      
        " Sequence " +
      
      ") Values (" +
      
        " ?DispatchId, " +
      
        " ?TechnicianId, " +
      
        " ?IsVisible, " +
      
        " ?Sequence " +
      
      ")";

      public static void Insert(DashboardUserSettings dashboardUserSettings, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?DispatchId", dashboardUserSettings.DispatchId);
      
        Database.PutParameter(dbCommand,"?TechnicianId", dashboardUserSettings.TechnicianId);
      
        Database.PutParameter(dbCommand,"?IsVisible", dashboardUserSettings.IsVisible);
      
        Database.PutParameter(dbCommand,"?Sequence", dashboardUserSettings.Sequence);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(DashboardUserSettings dashboardUserSettings)
      {
        Insert(dashboardUserSettings, null);
      }


      public static void Insert(List<DashboardUserSettings>  dashboardUserSettingsList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(DashboardUserSettings dashboardUserSettings in  dashboardUserSettingsList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?DispatchId", dashboardUserSettings.DispatchId);
      
        Database.PutParameter(dbCommand,"?TechnicianId", dashboardUserSettings.TechnicianId);
      
        Database.PutParameter(dbCommand,"?IsVisible", dashboardUserSettings.IsVisible);
      
        Database.PutParameter(dbCommand,"?Sequence", dashboardUserSettings.Sequence);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?DispatchId",dashboardUserSettings.DispatchId);
      
        Database.UpdateParameter(dbCommand,"?TechnicianId",dashboardUserSettings.TechnicianId);
      
        Database.UpdateParameter(dbCommand,"?IsVisible",dashboardUserSettings.IsVisible);
      
        Database.UpdateParameter(dbCommand,"?Sequence",dashboardUserSettings.Sequence);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<DashboardUserSettings>  dashboardUserSettingsList)
      {
        Insert(dashboardUserSettingsList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update DashboardUserSettings Set "
      
        + " IsVisible = ?IsVisible, "
      
        + " Sequence = ?Sequence "
      
        + " Where "
        
          + " DispatchId = ?DispatchId and  "
        
          + " TechnicianId = ?TechnicianId "
        
      ;

      public static void Update(DashboardUserSettings dashboardUserSettings, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?DispatchId", dashboardUserSettings.DispatchId);
      
        Database.PutParameter(dbCommand,"?TechnicianId", dashboardUserSettings.TechnicianId);
      
        Database.PutParameter(dbCommand,"?IsVisible", dashboardUserSettings.IsVisible);
      
        Database.PutParameter(dbCommand,"?Sequence", dashboardUserSettings.Sequence);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(DashboardUserSettings dashboardUserSettings)
      {
        Update(dashboardUserSettings, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " DispatchId, "
      
        + " TechnicianId, "
      
        + " IsVisible, "
      
        + " Sequence "
      

      + " From DashboardUserSettings "

      
        + " Where "
        
          + " DispatchId = ?DispatchId and  "
        
          + " TechnicianId = ?TechnicianId "
        
      ;

      public static DashboardUserSettings FindByPrimaryKey(
      int dispatchId,int technicianId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?DispatchId", dispatchId);
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("DashboardUserSettings not found, search by primary key");

      }

      public static DashboardUserSettings FindByPrimaryKey(
      int dispatchId,int technicianId
      )
      {
      return FindByPrimaryKey(
      dispatchId,technicianId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(DashboardUserSettings dashboardUserSettings, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?DispatchId",dashboardUserSettings.DispatchId);
      
        Database.PutParameter(dbCommand,"?TechnicianId",dashboardUserSettings.TechnicianId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(DashboardUserSettings dashboardUserSettings)
      {
      return Exists(dashboardUserSettings, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from DashboardUserSettings limit 1";

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

      public static DashboardUserSettings Load(IDataReader dataReader, int offset)
      {
      DashboardUserSettings dashboardUserSettings = new DashboardUserSettings();

      dashboardUserSettings.DispatchId = dataReader.GetInt32(0 + offset);
          dashboardUserSettings.TechnicianId = dataReader.GetInt32(1 + offset);
          dashboardUserSettings.IsVisible = dataReader.GetBoolean(2 + offset);
          dashboardUserSettings.Sequence = dataReader.GetInt32(3 + offset);
          

      return dashboardUserSettings;
      }

      public static DashboardUserSettings Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From DashboardUserSettings "

      
        + " Where "
        
          + " DispatchId = ?DispatchId and  "
        
          + " TechnicianId = ?TechnicianId "
        
      ;
      public static void Delete(DashboardUserSettings dashboardUserSettings, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?DispatchId", dashboardUserSettings.DispatchId);
      
        Database.PutParameter(dbCommand,"?TechnicianId", dashboardUserSettings.TechnicianId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(DashboardUserSettings dashboardUserSettings)
      {
        Delete(dashboardUserSettings, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From DashboardUserSettings ";

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

      
        + " DispatchId, "
      
        + " TechnicianId, "
      
        + " IsVisible, "
      
        + " Sequence "
      

      + " From DashboardUserSettings ";
      public static List<DashboardUserSettings> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<DashboardUserSettings> rv = new List<DashboardUserSettings>();

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

      public static List<DashboardUserSettings> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<DashboardUserSettings> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (DashboardUserSettings obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return DispatchId == obj.DispatchId && TechnicianId == obj.TechnicianId && IsVisible == obj.IsVisible && Sequence == obj.Sequence;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<DashboardUserSettings> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DashboardUserSettings));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(DashboardUserSettings item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<DashboardUserSettings>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DashboardUserSettings));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<DashboardUserSettings> itemsList
      = new List<DashboardUserSettings>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is DashboardUserSettings)
      itemsList.Add(deserializedObject as DashboardUserSettings);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_dispatchId;
      
        protected int m_technicianId;
      
        protected bool m_isVisible;
      
        protected int m_sequence;
      
      #endregion

      #region Constructors
      public DashboardUserSettings(
      int 
          dispatchId,int 
          technicianId
      ) : this()
      {
      
        m_dispatchId = dispatchId;
      
        m_technicianId = technicianId;
      
      }

      


        public DashboardUserSettings(
        int 
          dispatchId,int 
          technicianId,bool 
          isVisible,int 
          sequence
        ) : this()
        {
        
          m_dispatchId = dispatchId;
        
          m_technicianId = technicianId;
        
          m_isVisible = isVisible;
        
          m_sequence = sequence;
        
        }


      
      #endregion

      
        [XmlElement]
        public int DispatchId
        {
        get { return m_dispatchId;}
        set { m_dispatchId = value; }
        }
      
        [XmlElement]
        public int TechnicianId
        {
        get { return m_technicianId;}
        set { m_technicianId = value; }
        }
      
        [XmlElement]
        public bool IsVisible
        {
        get { return m_isVisible;}
        set { m_isVisible = value; }
        }
      
        [XmlElement]
        public int Sequence
        {
        get { return m_sequence;}
        set { m_sequence = value; }
        }
      

      public static int FieldsCount
      {
      get { return 4; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    