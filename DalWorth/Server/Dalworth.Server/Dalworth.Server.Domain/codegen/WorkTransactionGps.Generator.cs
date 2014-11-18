
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


      public partial class WorkTransactionGps : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WorkTransactionGps ( " +
      
        " WorkTransactionId, " +
      
        " Latitude, " +
      
        " Longitude, " +
      
        " GpsTime " +
      
      ") Values (" +
      
        " ?WorkTransactionId, " +
      
        " ?Latitude, " +
      
        " ?Longitude, " +
      
        " ?GpsTime " +
      
      ")";

      public static void Insert(WorkTransactionGps workTransactionGps, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionGps.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?Latitude", workTransactionGps.Latitude);
      
        Database.PutParameter(dbCommand,"?Longitude", workTransactionGps.Longitude);
      
        Database.PutParameter(dbCommand,"?GpsTime", workTransactionGps.GpsTime);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(WorkTransactionGps workTransactionGps)
      {
        Insert(workTransactionGps, null);
      }


      public static void Insert(List<WorkTransactionGps>  workTransactionGpsList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionGps workTransactionGps in  workTransactionGpsList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionGps.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?Latitude", workTransactionGps.Latitude);
      
        Database.PutParameter(dbCommand,"?Longitude", workTransactionGps.Longitude);
      
        Database.PutParameter(dbCommand,"?GpsTime", workTransactionGps.GpsTime);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WorkTransactionId",workTransactionGps.WorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"?Latitude",workTransactionGps.Latitude);
      
        Database.UpdateParameter(dbCommand,"?Longitude",workTransactionGps.Longitude);
      
        Database.UpdateParameter(dbCommand,"?GpsTime",workTransactionGps.GpsTime);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<WorkTransactionGps>  workTransactionGpsList)
      {
        Insert(workTransactionGpsList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WorkTransactionGps Set "
      
        + " Latitude = ?Latitude, "
      
        + " Longitude = ?Longitude, "
      
        + " GpsTime = ?GpsTime "
      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId "
        
      ;

      public static void Update(WorkTransactionGps workTransactionGps, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionGps.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?Latitude", workTransactionGps.Latitude);
      
        Database.PutParameter(dbCommand,"?Longitude", workTransactionGps.Longitude);
      
        Database.PutParameter(dbCommand,"?GpsTime", workTransactionGps.GpsTime);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionGps workTransactionGps)
      {
        Update(workTransactionGps, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " WorkTransactionId, "
      
        + " Latitude, "
      
        + " Longitude, "
      
        + " GpsTime "
      

      + " From WorkTransactionGps "

      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId "
        
      ;

      public static WorkTransactionGps FindByPrimaryKey(
      int workTransactionId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("WorkTransactionGps not found, search by primary key");

      }

      public static WorkTransactionGps FindByPrimaryKey(
      int workTransactionId
      )
      {
      return FindByPrimaryKey(
      workTransactionId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WorkTransactionGps workTransactionGps, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId",workTransactionGps.WorkTransactionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionGps workTransactionGps)
      {
      return Exists(workTransactionGps, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WorkTransactionGps limit 1";

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

      public static WorkTransactionGps Load(IDataReader dataReader, int offset)
      {
      WorkTransactionGps workTransactionGps = new WorkTransactionGps();

      workTransactionGps.WorkTransactionId = dataReader.GetInt32(0 + offset);
          workTransactionGps.Latitude = dataReader.GetFloat(1 + offset);
          workTransactionGps.Longitude = dataReader.GetFloat(2 + offset);
          workTransactionGps.GpsTime = dataReader.GetDateTime(3 + offset);
          

      return workTransactionGps;
      }

      public static WorkTransactionGps Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WorkTransactionGps "

      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId "
        
      ;
      public static void Delete(WorkTransactionGps workTransactionGps, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionGps.WorkTransactionId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionGps workTransactionGps)
      {
        Delete(workTransactionGps, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WorkTransactionGps ";

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

      
        + " WorkTransactionId, "
      
        + " Latitude, "
      
        + " Longitude, "
      
        + " GpsTime "
      

      + " From WorkTransactionGps ";
      public static List<WorkTransactionGps> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WorkTransactionGps> rv = new List<WorkTransactionGps>();

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

      public static List<WorkTransactionGps> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionGps> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WorkTransactionGps obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return WorkTransactionId == obj.WorkTransactionId && Latitude == obj.Latitude && Longitude == obj.Longitude && GpsTime == obj.GpsTime;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionGps> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionGps));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionGps item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionGps>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionGps));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionGps> itemsList
      = new List<WorkTransactionGps>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionGps)
      itemsList.Add(deserializedObject as WorkTransactionGps);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_workTransactionId;
      
        protected float m_latitude;
      
        protected float m_longitude;
      
        protected DateTime m_gpsTime;
      
      #endregion

      #region Constructors
      public WorkTransactionGps(
      int 
          workTransactionId
      ) : this()
      {
      
        m_workTransactionId = workTransactionId;
      
      }

      


        public WorkTransactionGps(
        int 
          workTransactionId,float 
          latitude,float 
          longitude,DateTime 
          gpsTime
        ) : this()
        {
        
          m_workTransactionId = workTransactionId;
        
          m_latitude = latitude;
        
          m_longitude = longitude;
        
          m_gpsTime = gpsTime;
        
        }


      
      #endregion

      
        [XmlElement]
        public int WorkTransactionId
        {
        get { return m_workTransactionId;}
        set { m_workTransactionId = value; }
        }
      
        [XmlElement]
        public float Latitude
        {
        get { return m_latitude;}
        set { m_latitude = value; }
        }
      
        [XmlElement]
        public float Longitude
        {
        get { return m_longitude;}
        set { m_longitude = value; }
        }
      
        [XmlElement]
        public DateTime GpsTime
        {
        get { return m_gpsTime;}
        set { m_gpsTime = value; }
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

    