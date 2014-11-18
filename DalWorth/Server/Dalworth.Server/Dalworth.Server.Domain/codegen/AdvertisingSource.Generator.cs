
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


      public partial class AdvertisingSource : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into AdvertisingSource ( " +
      
        " ID, " +
      
        " AreaId, " +
      
        " Name, " +
      
        " IsActive, " +
      
        " IsTechnicianReference, " +
      
        " Acronym, " +
      
        " IsRestoration, " +
      
        " TrackingUrl " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?AreaId, " +
      
        " ?Name, " +
      
        " ?IsActive, " +
      
        " ?IsTechnicianReference, " +
      
        " ?Acronym, " +
      
        " ?IsRestoration, " +
      
        " ?TrackingUrl " +
      
      ")";

      public static void Insert(AdvertisingSource advertisingSource, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", advertisingSource.ID);
      
        Database.PutParameter(dbCommand,"?AreaId", advertisingSource.AreaId);
      
        Database.PutParameter(dbCommand,"?Name", advertisingSource.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", advertisingSource.IsActive);
      
        Database.PutParameter(dbCommand,"?IsTechnicianReference", advertisingSource.IsTechnicianReference);
      
        Database.PutParameter(dbCommand,"?Acronym", advertisingSource.Acronym);
      
        Database.PutParameter(dbCommand,"?IsRestoration", advertisingSource.IsRestoration);
      
        Database.PutParameter(dbCommand,"?TrackingUrl", advertisingSource.TrackingUrl);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(AdvertisingSource advertisingSource)
      {
        Insert(advertisingSource, null);
      }


      public static void Insert(List<AdvertisingSource>  advertisingSourceList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(AdvertisingSource advertisingSource in  advertisingSourceList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", advertisingSource.ID);
      
        Database.PutParameter(dbCommand,"?AreaId", advertisingSource.AreaId);
      
        Database.PutParameter(dbCommand,"?Name", advertisingSource.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", advertisingSource.IsActive);
      
        Database.PutParameter(dbCommand,"?IsTechnicianReference", advertisingSource.IsTechnicianReference);
      
        Database.PutParameter(dbCommand,"?Acronym", advertisingSource.Acronym);
      
        Database.PutParameter(dbCommand,"?IsRestoration", advertisingSource.IsRestoration);
      
        Database.PutParameter(dbCommand,"?TrackingUrl", advertisingSource.TrackingUrl);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",advertisingSource.ID);
      
        Database.UpdateParameter(dbCommand,"?AreaId",advertisingSource.AreaId);
      
        Database.UpdateParameter(dbCommand,"?Name",advertisingSource.Name);
      
        Database.UpdateParameter(dbCommand,"?IsActive",advertisingSource.IsActive);
      
        Database.UpdateParameter(dbCommand,"?IsTechnicianReference",advertisingSource.IsTechnicianReference);
      
        Database.UpdateParameter(dbCommand,"?Acronym",advertisingSource.Acronym);
      
        Database.UpdateParameter(dbCommand,"?IsRestoration",advertisingSource.IsRestoration);
      
        Database.UpdateParameter(dbCommand,"?TrackingUrl",advertisingSource.TrackingUrl);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<AdvertisingSource>  advertisingSourceList)
      {
        Insert(advertisingSourceList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update AdvertisingSource Set "
      
        + " AreaId = ?AreaId, "
      
        + " Name = ?Name, "
      
        + " IsActive = ?IsActive, "
      
        + " IsTechnicianReference = ?IsTechnicianReference, "
      
        + " Acronym = ?Acronym, "
      
        + " IsRestoration = ?IsRestoration, "
      
        + " TrackingUrl = ?TrackingUrl "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(AdvertisingSource advertisingSource, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", advertisingSource.ID);
      
        Database.PutParameter(dbCommand,"?AreaId", advertisingSource.AreaId);
      
        Database.PutParameter(dbCommand,"?Name", advertisingSource.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", advertisingSource.IsActive);
      
        Database.PutParameter(dbCommand,"?IsTechnicianReference", advertisingSource.IsTechnicianReference);
      
        Database.PutParameter(dbCommand,"?Acronym", advertisingSource.Acronym);
      
        Database.PutParameter(dbCommand,"?IsRestoration", advertisingSource.IsRestoration);
      
        Database.PutParameter(dbCommand,"?TrackingUrl", advertisingSource.TrackingUrl);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(AdvertisingSource advertisingSource)
      {
        Update(advertisingSource, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " AreaId, "
      
        + " Name, "
      
        + " IsActive, "
      
        + " IsTechnicianReference, "
      
        + " Acronym, "
      
        + " IsRestoration, "
      
        + " TrackingUrl "
      

      + " From AdvertisingSource "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static AdvertisingSource FindByPrimaryKey(
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
      throw new DataNotFoundException("AdvertisingSource not found, search by primary key");

      }

      public static AdvertisingSource FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(AdvertisingSource advertisingSource, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",advertisingSource.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(AdvertisingSource advertisingSource)
      {
      return Exists(advertisingSource, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from AdvertisingSource limit 1";

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

      public static AdvertisingSource Load(IDataReader dataReader, int offset)
      {
      AdvertisingSource advertisingSource = new AdvertisingSource();

      advertisingSource.ID = dataReader.GetInt32(0 + offset);
          advertisingSource.AreaId = dataReader.GetByte(1 + offset);
          advertisingSource.Name = dataReader.GetString(2 + offset);
          advertisingSource.IsActive = dataReader.GetBoolean(3 + offset);
          advertisingSource.IsTechnicianReference = dataReader.GetBoolean(4 + offset);
          advertisingSource.Acronym = dataReader.GetString(5 + offset);
          advertisingSource.IsRestoration = dataReader.GetBoolean(6 + offset);
          advertisingSource.TrackingUrl = dataReader.GetString(7 + offset);
          

      return advertisingSource;
      }

      public static AdvertisingSource Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From AdvertisingSource "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(AdvertisingSource advertisingSource, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", advertisingSource.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(AdvertisingSource advertisingSource)
      {
        Delete(advertisingSource, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From AdvertisingSource ";

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
      
        + " AreaId, "
      
        + " Name, "
      
        + " IsActive, "
      
        + " IsTechnicianReference, "
      
        + " Acronym, "
      
        + " IsRestoration, "
      
        + " TrackingUrl "
      

      + " From AdvertisingSource ";
      public static List<AdvertisingSource> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<AdvertisingSource> rv = new List<AdvertisingSource>();

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

      public static List<AdvertisingSource> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<AdvertisingSource> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (AdvertisingSource obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && AreaId == obj.AreaId && Name == obj.Name && IsActive == obj.IsActive && IsTechnicianReference == obj.IsTechnicianReference && Acronym == obj.Acronym && IsRestoration == obj.IsRestoration && TrackingUrl == obj.TrackingUrl;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<AdvertisingSource> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(AdvertisingSource));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(AdvertisingSource item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<AdvertisingSource>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(AdvertisingSource));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<AdvertisingSource> itemsList
      = new List<AdvertisingSource>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is AdvertisingSource)
      itemsList.Add(deserializedObject as AdvertisingSource);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected byte m_areaId;
      
        protected String m_name;
      
        protected bool m_isActive;
      
        protected bool m_isTechnicianReference;
      
        protected String m_acronym;
      
        protected bool m_isRestoration;
      
        protected String m_trackingUrl;
      
      #endregion

      #region Constructors
      public AdvertisingSource(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public AdvertisingSource(
        int 
          iD,byte 
          areaId,String 
          name,bool 
          isActive,bool 
          isTechnicianReference,String 
          acronym,bool 
          isRestoration,String 
          trackingUrl
        ) : this()
        {
        
          m_iD = iD;
        
          m_areaId = areaId;
        
          m_name = name;
        
          m_isActive = isActive;
        
          m_isTechnicianReference = isTechnicianReference;
        
          m_acronym = acronym;
        
          m_isRestoration = isRestoration;
        
          m_trackingUrl = trackingUrl;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public byte AreaId
        {
        get { return m_areaId;}
        set { m_areaId = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
        }
      
        [XmlElement]
        public bool IsTechnicianReference
        {
        get { return m_isTechnicianReference;}
        set { m_isTechnicianReference = value; }
        }
      
        [XmlElement]
        public String Acronym
        {
        get { return m_acronym;}
        set { m_acronym = value; }
        }
      
        [XmlElement]
        public bool IsRestoration
        {
        get { return m_isRestoration;}
        set { m_isRestoration = value; }
        }
      
        [XmlElement]
        public String TrackingUrl
        {
        get { return m_trackingUrl;}
        set { m_trackingUrl = value; }
        }
      

      public static int FieldsCount
      {
      get { return 8; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    