
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
      public partial class TechnicianWorkTime : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TechnicianWorkTime ( " +
      
        " TechnicianId, " +
      
        " TimeStart, " +
      
        " TimeEnd " +
      
      ") Values (" +
      
        " ?TechnicianId, " +
      
        " ?TimeStart, " +
      
        " ?TimeEnd " +
      
      ")";

      public static void Insert(TechnicianWorkTime technicianWorkTime, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianWorkTime.TechnicianId);
      
        Database.PutParameter(dbCommand,"?TimeStart", technicianWorkTime.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", technicianWorkTime.TimeEnd);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(TechnicianWorkTime technicianWorkTime)
      {
        Insert(technicianWorkTime, null);
      }


      public static void Insert(List<TechnicianWorkTime>  technicianWorkTimeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TechnicianWorkTime technicianWorkTime in  technicianWorkTimeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianWorkTime.TechnicianId);
      
        Database.PutParameter(dbCommand,"?TimeStart", technicianWorkTime.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", technicianWorkTime.TimeEnd);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TechnicianId",technicianWorkTime.TechnicianId);
      
        Database.UpdateParameter(dbCommand,"?TimeStart",technicianWorkTime.TimeStart);
      
        Database.UpdateParameter(dbCommand,"?TimeEnd",technicianWorkTime.TimeEnd);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<TechnicianWorkTime>  technicianWorkTimeList)
      {
        Insert(technicianWorkTimeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TechnicianWorkTime Set "
      
        + " TimeEnd = ?TimeEnd "
      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " TimeStart = ?TimeStart "
        
      ;

      public static void Update(TechnicianWorkTime technicianWorkTime, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianWorkTime.TechnicianId);
      
        Database.PutParameter(dbCommand,"?TimeStart", technicianWorkTime.TimeStart);
      
        Database.PutParameter(dbCommand,"?TimeEnd", technicianWorkTime.TimeEnd);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TechnicianWorkTime technicianWorkTime)
      {
        Update(technicianWorkTime, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TechnicianId, "
      
        + " TimeStart, "
      
        + " TimeEnd "
      

      + " From TechnicianWorkTime "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " TimeStart = ?TimeStart "
        
      ;

      public static TechnicianWorkTime FindByPrimaryKey(
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
      throw new DataNotFoundException("TechnicianWorkTime not found, search by primary key");

      }

      public static TechnicianWorkTime FindByPrimaryKey(
      int technicianId,DateTime timeStart
      )
      {
      return FindByPrimaryKey(
      technicianId,timeStart, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TechnicianWorkTime technicianWorkTime, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId",technicianWorkTime.TechnicianId);
      
        Database.PutParameter(dbCommand,"?TimeStart",technicianWorkTime.TimeStart);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TechnicianWorkTime technicianWorkTime)
      {
      return Exists(technicianWorkTime, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TechnicianWorkTime limit 1";

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

      public static TechnicianWorkTime Load(IDataReader dataReader, int offset)
      {
      TechnicianWorkTime technicianWorkTime = new TechnicianWorkTime();

      technicianWorkTime.TechnicianId = dataReader.GetInt32(0 + offset);
          technicianWorkTime.TimeStart = dataReader.GetDateTime(1 + offset);
          technicianWorkTime.TimeEnd = dataReader.GetDateTime(2 + offset);
          

      return technicianWorkTime;
      }

      public static TechnicianWorkTime Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TechnicianWorkTime "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " TimeStart = ?TimeStart "
        
      ;
      public static void Delete(TechnicianWorkTime technicianWorkTime, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianWorkTime.TechnicianId);
      
        Database.PutParameter(dbCommand,"?TimeStart", technicianWorkTime.TimeStart);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TechnicianWorkTime technicianWorkTime)
      {
        Delete(technicianWorkTime, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TechnicianWorkTime ";

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
      

      + " From TechnicianWorkTime ";
      public static List<TechnicianWorkTime> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TechnicianWorkTime> rv = new List<TechnicianWorkTime>();

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

      public static List<TechnicianWorkTime> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TechnicianWorkTime> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TechnicianWorkTime> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianWorkTime));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TechnicianWorkTime item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TechnicianWorkTime>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianWorkTime));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TechnicianWorkTime> itemsList
      = new List<TechnicianWorkTime>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TechnicianWorkTime)
      itemsList.Add(deserializedObject as TechnicianWorkTime);
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
      public TechnicianWorkTime(
      int 
          technicianId,DateTime 
          timeStart
      ) : this()
      {
      
        m_technicianId = technicianId;
      
        m_timeStart = timeStart;
      
      }

      


        public TechnicianWorkTime(
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

    