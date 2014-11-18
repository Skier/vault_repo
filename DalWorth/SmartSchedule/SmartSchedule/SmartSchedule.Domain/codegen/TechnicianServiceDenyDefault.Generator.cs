
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using SmartSchedule.Data;
    using SmartSchedule.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace SmartSchedule.Domain
      {

      [DataContract]
      public partial class TechnicianServiceDenyDefault : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TechnicianServiceDenyDefault ( " +
      
        " TechnicianId, " +
      
        " ServiceId, " +
      
        " IsForNonExclusive " +
      
      ") Values (" +
      
        " ?TechnicianId, " +
      
        " ?ServiceId, " +
      
        " ?IsForNonExclusive " +
      
      ")";

      public static void Insert(TechnicianServiceDenyDefault technicianServiceDenyDefault, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianServiceDenyDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?ServiceId", technicianServiceDenyDefault.ServiceId);
      
        Database.PutParameter(dbCommand,"?IsForNonExclusive", technicianServiceDenyDefault.IsForNonExclusive);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(TechnicianServiceDenyDefault technicianServiceDenyDefault)
      {
        Insert(technicianServiceDenyDefault, null);
      }


      public static void Insert(List<TechnicianServiceDenyDefault>  technicianServiceDenyDefaultList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TechnicianServiceDenyDefault technicianServiceDenyDefault in  technicianServiceDenyDefaultList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianServiceDenyDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?ServiceId", technicianServiceDenyDefault.ServiceId);
      
        Database.PutParameter(dbCommand,"?IsForNonExclusive", technicianServiceDenyDefault.IsForNonExclusive);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TechnicianId",technicianServiceDenyDefault.TechnicianId);
      
        Database.UpdateParameter(dbCommand,"?ServiceId",technicianServiceDenyDefault.ServiceId);
      
        Database.UpdateParameter(dbCommand,"?IsForNonExclusive",technicianServiceDenyDefault.IsForNonExclusive);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<TechnicianServiceDenyDefault>  technicianServiceDenyDefaultList)
      {
        Insert(technicianServiceDenyDefaultList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TechnicianServiceDenyDefault Set "
      
        + " IsForNonExclusive = ?IsForNonExclusive "
      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " ServiceId = ?ServiceId "
        
      ;

      public static void Update(TechnicianServiceDenyDefault technicianServiceDenyDefault, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianServiceDenyDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?ServiceId", technicianServiceDenyDefault.ServiceId);
      
        Database.PutParameter(dbCommand,"?IsForNonExclusive", technicianServiceDenyDefault.IsForNonExclusive);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TechnicianServiceDenyDefault technicianServiceDenyDefault)
      {
        Update(technicianServiceDenyDefault, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TechnicianId, "
      
        + " ServiceId, "
      
        + " IsForNonExclusive "
      

      + " From TechnicianServiceDenyDefault "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " ServiceId = ?ServiceId "
        
      ;

      public static TechnicianServiceDenyDefault FindByPrimaryKey(
      int technicianId,int serviceId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianId);
      
        Database.PutParameter(dbCommand,"?ServiceId", serviceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("TechnicianServiceDenyDefault not found, search by primary key");

      }

      public static TechnicianServiceDenyDefault FindByPrimaryKey(
      int technicianId,int serviceId
      )
      {
      return FindByPrimaryKey(
      technicianId,serviceId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TechnicianServiceDenyDefault technicianServiceDenyDefault, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId",technicianServiceDenyDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?ServiceId",technicianServiceDenyDefault.ServiceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TechnicianServiceDenyDefault technicianServiceDenyDefault)
      {
      return Exists(technicianServiceDenyDefault, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TechnicianServiceDenyDefault limit 1";

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

      public static TechnicianServiceDenyDefault Load(IDataReader dataReader, int offset)
      {
      TechnicianServiceDenyDefault technicianServiceDenyDefault = new TechnicianServiceDenyDefault();

      technicianServiceDenyDefault.TechnicianId = dataReader.GetInt32(0 + offset);
          technicianServiceDenyDefault.ServiceId = dataReader.GetInt32(1 + offset);
          technicianServiceDenyDefault.IsForNonExclusive = dataReader.GetBoolean(2 + offset);
          

      return technicianServiceDenyDefault;
      }

      public static TechnicianServiceDenyDefault Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TechnicianServiceDenyDefault "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " ServiceId = ?ServiceId "
        
      ;
      public static void Delete(TechnicianServiceDenyDefault technicianServiceDenyDefault, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianServiceDenyDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?ServiceId", technicianServiceDenyDefault.ServiceId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TechnicianServiceDenyDefault technicianServiceDenyDefault)
      {
        Delete(technicianServiceDenyDefault, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TechnicianServiceDenyDefault ";

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

      
        + " TechnicianId, "
      
        + " ServiceId, "
      
        + " IsForNonExclusive "
      

      + " From TechnicianServiceDenyDefault ";
      public static List<TechnicianServiceDenyDefault> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TechnicianServiceDenyDefault> rv = new List<TechnicianServiceDenyDefault>();

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

      public static List<TechnicianServiceDenyDefault> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TechnicianServiceDenyDefault> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TechnicianServiceDenyDefault> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianServiceDenyDefault));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TechnicianServiceDenyDefault item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TechnicianServiceDenyDefault>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianServiceDenyDefault));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TechnicianServiceDenyDefault> itemsList
      = new List<TechnicianServiceDenyDefault>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TechnicianServiceDenyDefault)
      itemsList.Add(deserializedObject as TechnicianServiceDenyDefault);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_technicianId;
      
        protected int m_serviceId;
      
        protected bool m_isForNonExclusive;
      
      #endregion

      #region Constructors
      public TechnicianServiceDenyDefault(
      int 
          technicianId,int 
          serviceId
      ) : this()
      {
      
        m_technicianId = technicianId;
      
        m_serviceId = serviceId;
      
      }

      


        public TechnicianServiceDenyDefault(
        int 
          technicianId,int 
          serviceId,bool 
          isForNonExclusive
        ) : this()
        {
        
          m_technicianId = technicianId;
        
          m_serviceId = serviceId;
        
          m_isForNonExclusive = isForNonExclusive;
        
        }


      
      #endregion

      
        [DataMember]
        public int TechnicianId
        {
        get { return m_technicianId;}
        set { m_technicianId = value; }
        }
      
        [DataMember]
        public int ServiceId
        {
        get { return m_serviceId;}
        set { m_serviceId = value; }
        }
      
        [DataMember]
        public bool IsForNonExclusive
        {
        get { return m_isForNonExclusive;}
        set { m_isForNonExclusive = value; }
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

    