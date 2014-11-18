
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


      public partial class DashboardSharedSetting : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into DashboardSharedSetting ( " +
      
        " DashboardDate, " +
      
        " TechnicianId, " +
      
        " UnknownTechnicianId, " +
      
        " IsVisible, " +
      
        " Sequence " +
      
      ") Values (" +
      
        " ?DashboardDate, " +
      
        " ?TechnicianId, " +
      
        " ?UnknownTechnicianId, " +
      
        " ?IsVisible, " +
      
        " ?Sequence " +
      
      ")";

      public static void Insert(DashboardSharedSetting dashboardSharedSetting, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?DashboardDate", dashboardSharedSetting.DashboardDate);
      
        Database.PutParameter(dbCommand,"?TechnicianId", dashboardSharedSetting.TechnicianId);
      
        Database.PutParameter(dbCommand,"?UnknownTechnicianId", dashboardSharedSetting.UnknownTechnicianId);
      
        Database.PutParameter(dbCommand,"?IsVisible", dashboardSharedSetting.IsVisible);
      
        Database.PutParameter(dbCommand,"?Sequence", dashboardSharedSetting.Sequence);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(DashboardSharedSetting dashboardSharedSetting)
      {
        Insert(dashboardSharedSetting, null);
      }


      public static void Insert(List<DashboardSharedSetting>  dashboardSharedSettingList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(DashboardSharedSetting dashboardSharedSetting in  dashboardSharedSettingList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?DashboardDate", dashboardSharedSetting.DashboardDate);
      
        Database.PutParameter(dbCommand,"?TechnicianId", dashboardSharedSetting.TechnicianId);
      
        Database.PutParameter(dbCommand,"?UnknownTechnicianId", dashboardSharedSetting.UnknownTechnicianId);
      
        Database.PutParameter(dbCommand,"?IsVisible", dashboardSharedSetting.IsVisible);
      
        Database.PutParameter(dbCommand,"?Sequence", dashboardSharedSetting.Sequence);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?DashboardDate",dashboardSharedSetting.DashboardDate);
      
        Database.UpdateParameter(dbCommand,"?TechnicianId",dashboardSharedSetting.TechnicianId);
      
        Database.UpdateParameter(dbCommand,"?UnknownTechnicianId",dashboardSharedSetting.UnknownTechnicianId);
      
        Database.UpdateParameter(dbCommand,"?IsVisible",dashboardSharedSetting.IsVisible);
      
        Database.UpdateParameter(dbCommand,"?Sequence",dashboardSharedSetting.Sequence);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<DashboardSharedSetting>  dashboardSharedSettingList)
      {
        Insert(dashboardSharedSettingList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update DashboardSharedSetting Set "
      
        + " UnknownTechnicianId = ?UnknownTechnicianId, "
      
        + " IsVisible = ?IsVisible, "
      
        + " Sequence = ?Sequence "
      
        + " Where "
        
          + " DashboardDate = ?DashboardDate and  "
        
          + " TechnicianId = ?TechnicianId "
        
      ;

      public static void Update(DashboardSharedSetting dashboardSharedSetting, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?DashboardDate", dashboardSharedSetting.DashboardDate);
      
        Database.PutParameter(dbCommand,"?TechnicianId", dashboardSharedSetting.TechnicianId);
      
        Database.PutParameter(dbCommand,"?UnknownTechnicianId", dashboardSharedSetting.UnknownTechnicianId);
      
        Database.PutParameter(dbCommand,"?IsVisible", dashboardSharedSetting.IsVisible);
      
        Database.PutParameter(dbCommand,"?Sequence", dashboardSharedSetting.Sequence);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(DashboardSharedSetting dashboardSharedSetting)
      {
        Update(dashboardSharedSetting, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " DashboardDate, "
      
        + " TechnicianId, "
      
        + " UnknownTechnicianId, "
      
        + " IsVisible, "
      
        + " Sequence "
      

      + " From DashboardSharedSetting "

      
        + " Where "
        
          + " DashboardDate = ?DashboardDate and  "
        
          + " TechnicianId = ?TechnicianId "
        
      ;

      public static DashboardSharedSetting FindByPrimaryKey(
      DateTime dashboardDate,int technicianId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?DashboardDate", dashboardDate);
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("DashboardSharedSetting not found, search by primary key");

      }

      public static DashboardSharedSetting FindByPrimaryKey(
      DateTime dashboardDate,int technicianId
      )
      {
      return FindByPrimaryKey(
      dashboardDate,technicianId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(DashboardSharedSetting dashboardSharedSetting, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?DashboardDate",dashboardSharedSetting.DashboardDate);
      
        Database.PutParameter(dbCommand,"?TechnicianId",dashboardSharedSetting.TechnicianId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(DashboardSharedSetting dashboardSharedSetting)
      {
      return Exists(dashboardSharedSetting, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from DashboardSharedSetting limit 1";

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

      public static DashboardSharedSetting Load(IDataReader dataReader, int offset)
      {
      DashboardSharedSetting dashboardSharedSetting = new DashboardSharedSetting();

      dashboardSharedSetting.DashboardDate = dataReader.GetDateTime(0 + offset);
          dashboardSharedSetting.TechnicianId = dataReader.GetInt32(1 + offset);
          dashboardSharedSetting.UnknownTechnicianId = dataReader.GetInt32(2 + offset);
          dashboardSharedSetting.IsVisible = dataReader.GetBoolean(3 + offset);
          dashboardSharedSetting.Sequence = dataReader.GetInt32(4 + offset);
          

      return dashboardSharedSetting;
      }

      public static DashboardSharedSetting Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From DashboardSharedSetting "

      
        + " Where "
        
          + " DashboardDate = ?DashboardDate and  "
        
          + " TechnicianId = ?TechnicianId "
        
      ;
      public static void Delete(DashboardSharedSetting dashboardSharedSetting, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?DashboardDate", dashboardSharedSetting.DashboardDate);
      
        Database.PutParameter(dbCommand,"?TechnicianId", dashboardSharedSetting.TechnicianId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(DashboardSharedSetting dashboardSharedSetting)
      {
        Delete(dashboardSharedSetting, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From DashboardSharedSetting ";

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

      
        + " DashboardDate, "
      
        + " TechnicianId, "
      
        + " UnknownTechnicianId, "
      
        + " IsVisible, "
      
        + " Sequence "
      

      + " From DashboardSharedSetting ";
      public static List<DashboardSharedSetting> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<DashboardSharedSetting> rv = new List<DashboardSharedSetting>();

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

      public static List<DashboardSharedSetting> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<DashboardSharedSetting> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (DashboardSharedSetting obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return DashboardDate == obj.DashboardDate && TechnicianId == obj.TechnicianId && UnknownTechnicianId == obj.UnknownTechnicianId && IsVisible == obj.IsVisible && Sequence == obj.Sequence;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<DashboardSharedSetting> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DashboardSharedSetting));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(DashboardSharedSetting item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<DashboardSharedSetting>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DashboardSharedSetting));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<DashboardSharedSetting> itemsList
      = new List<DashboardSharedSetting>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is DashboardSharedSetting)
      itemsList.Add(deserializedObject as DashboardSharedSetting);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected DateTime m_dashboardDate;
      
        protected int m_technicianId;
      
        protected int m_unknownTechnicianId;
      
        protected bool m_isVisible;
      
        protected int m_sequence;
      
      #endregion

      #region Constructors
      public DashboardSharedSetting(
      DateTime 
          dashboardDate,int 
          technicianId
      ) : this()
      {
      
        m_dashboardDate = dashboardDate;
      
        m_technicianId = technicianId;
      
      }

      


        public DashboardSharedSetting(
        DateTime 
          dashboardDate,int 
          technicianId,int 
          unknownTechnicianId,bool 
          isVisible,int 
          sequence
        ) : this()
        {
        
          m_dashboardDate = dashboardDate;
        
          m_technicianId = technicianId;
        
          m_unknownTechnicianId = unknownTechnicianId;
        
          m_isVisible = isVisible;
        
          m_sequence = sequence;
        
        }


      
      #endregion

      
        [XmlElement]
        public DateTime DashboardDate
        {
        get { return m_dashboardDate;}
        set { m_dashboardDate = value; }
        }
      
        [XmlElement]
        public int TechnicianId
        {
        get { return m_technicianId;}
        set { m_technicianId = value; }
        }
      
        [XmlElement]
        public int UnknownTechnicianId
        {
        get { return m_unknownTechnicianId;}
        set { m_unknownTechnicianId = value; }
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
      get { return 5; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    