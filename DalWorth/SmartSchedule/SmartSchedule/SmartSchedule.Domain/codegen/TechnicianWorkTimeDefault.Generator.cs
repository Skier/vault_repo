
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
      public partial class TechnicianWorkTimeDefault : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TechnicianWorkTimeDefault ( " +
      
        " TechnicianId, " +
      
        " TimeStart, " +
      
        " TimeEnd " +
      
      ") Values (" +
      
        " ?TechnicianId, " +
      
        " ?TimeStart, " +
      
        " ?TimeEnd " +
      
      ")";

      public static void Insert(TechnicianWorkTimeDefault technicianWorkTimeDefault, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianWorkTimeDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?TimeStart", technicianWorkTimeDefault.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", technicianWorkTimeDefault.TimeEnd);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(TechnicianWorkTimeDefault technicianWorkTimeDefault)
      {
        Insert(technicianWorkTimeDefault, null);
      }


      public static void Insert(List<TechnicianWorkTimeDefault>  technicianWorkTimeDefaultList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TechnicianWorkTimeDefault technicianWorkTimeDefault in  technicianWorkTimeDefaultList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianWorkTimeDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?TimeStart", technicianWorkTimeDefault.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", technicianWorkTimeDefault.TimeEnd);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TechnicianId",technicianWorkTimeDefault.TechnicianId);
      
        Database.UpdateParameter(dbCommand,"?TimeStart",technicianWorkTimeDefault.TimeStart);
      
        Database.UpdateParameter(dbCommand,"?TimeEnd",technicianWorkTimeDefault.TimeEnd);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<TechnicianWorkTimeDefault>  technicianWorkTimeDefaultList)
      {
        Insert(technicianWorkTimeDefaultList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TechnicianWorkTimeDefault Set "
      
        + " TimeEnd = ?TimeEnd "
      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " TimeStart = ?TimeStart "
        
      ;

      public static void Update(TechnicianWorkTimeDefault technicianWorkTimeDefault, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianWorkTimeDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?TimeStart", technicianWorkTimeDefault.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", technicianWorkTimeDefault.TimeEnd);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TechnicianWorkTimeDefault technicianWorkTimeDefault)
      {
        Update(technicianWorkTimeDefault, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TechnicianId, "
      
        + " TimeStart, "
      
        + " TimeEnd "
      

      + " From TechnicianWorkTimeDefault "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " TimeStart = ?TimeStart "
        
      ;

      public static TechnicianWorkTimeDefault FindByPrimaryKey(
      int technicianId,DateTime timeStart, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianId);
      
        Database.PutParameter(dbCommand,"?TimeStart", timeStart);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("TechnicianWorkTimeDefault not found, search by primary key");

      }

      public static TechnicianWorkTimeDefault FindByPrimaryKey(
      int technicianId,DateTime timeStart
      )
      {
      return FindByPrimaryKey(
      technicianId,timeStart, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TechnicianWorkTimeDefault technicianWorkTimeDefault, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId",technicianWorkTimeDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?TimeStart",technicianWorkTimeDefault.TimeStart);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TechnicianWorkTimeDefault technicianWorkTimeDefault)
      {
      return Exists(technicianWorkTimeDefault, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TechnicianWorkTimeDefault limit 1";

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

      public static TechnicianWorkTimeDefault Load(IDataReader dataReader, int offset)
      {
      TechnicianWorkTimeDefault technicianWorkTimeDefault = new TechnicianWorkTimeDefault();

      technicianWorkTimeDefault.TechnicianId = dataReader.GetInt32(0 + offset);
          technicianWorkTimeDefault.TimeStart = dataReader.GetDateTime(1 + offset);
          technicianWorkTimeDefault.TimeEnd = dataReader.GetDateTime(2 + offset);
          

      return technicianWorkTimeDefault;
      }

      public static TechnicianWorkTimeDefault Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TechnicianWorkTimeDefault "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " TimeStart = ?TimeStart "
        
      ;
      public static void Delete(TechnicianWorkTimeDefault technicianWorkTimeDefault, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianWorkTimeDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?TimeStart", technicianWorkTimeDefault.TimeStart);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TechnicianWorkTimeDefault technicianWorkTimeDefault)
      {
        Delete(technicianWorkTimeDefault, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TechnicianWorkTimeDefault ";

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
      
        + " TimeStart, "
      
        + " TimeEnd "
      

      + " From TechnicianWorkTimeDefault ";
      public static List<TechnicianWorkTimeDefault> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TechnicianWorkTimeDefault> rv = new List<TechnicianWorkTimeDefault>();

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

      public static List<TechnicianWorkTimeDefault> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TechnicianWorkTimeDefault> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TechnicianWorkTimeDefault> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianWorkTimeDefault));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TechnicianWorkTimeDefault item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TechnicianWorkTimeDefault>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianWorkTimeDefault));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TechnicianWorkTimeDefault> itemsList
      = new List<TechnicianWorkTimeDefault>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TechnicianWorkTimeDefault)
      itemsList.Add(deserializedObject as TechnicianWorkTimeDefault);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_technicianId;
      
        protected DateTime m_timeStart;
      
        protected DateTime m_timeEnd;
      
      #endregion

      #region Constructors
      public TechnicianWorkTimeDefault(
      int 
          technicianId,DateTime 
          timeStart
      ) : this()
      {
      
        m_technicianId = technicianId;
      
        m_timeStart = timeStart;
      
      }

      


        public TechnicianWorkTimeDefault(
        int 
          technicianId,DateTime 
          timeStart,DateTime 
          timeEnd
        ) : this()
        {
        
          m_technicianId = technicianId;
        
          m_timeStart = timeStart;
        
          m_timeEnd = timeEnd;
        
        }


      
      #endregion

      
        [DataMember]
        public int TechnicianId
        {
        get { return m_technicianId;}
        set { m_technicianId = value; }
        }
      
        [DataMember]
        public DateTime TimeStart
        {
        get { return m_timeStart;}
        set { m_timeStart = value; }
        }
      
        [DataMember]
        public DateTime TimeEnd
        {
        get { return m_timeEnd;}
        set { m_timeEnd = value; }
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

    