
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


      public partial class TrackingPhoneOrderSource : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TrackingPhoneOrderSource ( " +
      
        " TrackingPhoneId, " +
      
        " OrderSourceId " +
      
      ") Values (" +
      
        " ?TrackingPhoneId, " +
      
        " ?OrderSourceId " +
      
      ")";

      public static void Insert(TrackingPhoneOrderSource trackingPhoneOrderSource, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TrackingPhoneId", trackingPhoneOrderSource.TrackingPhoneId);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", trackingPhoneOrderSource.OrderSourceId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(TrackingPhoneOrderSource trackingPhoneOrderSource)
      {
        Insert(trackingPhoneOrderSource, null);
      }


      public static void Insert(List<TrackingPhoneOrderSource>  trackingPhoneOrderSourceList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TrackingPhoneOrderSource trackingPhoneOrderSource in  trackingPhoneOrderSourceList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TrackingPhoneId", trackingPhoneOrderSource.TrackingPhoneId);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", trackingPhoneOrderSource.OrderSourceId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TrackingPhoneId",trackingPhoneOrderSource.TrackingPhoneId);
      
        Database.UpdateParameter(dbCommand,"?OrderSourceId",trackingPhoneOrderSource.OrderSourceId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<TrackingPhoneOrderSource>  trackingPhoneOrderSourceList)
      {
        Insert(trackingPhoneOrderSourceList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TrackingPhoneOrderSource Set "
      
        + " Where "
        
          + " TrackingPhoneId = ?TrackingPhoneId and  "
        
          + " OrderSourceId = ?OrderSourceId "
        
      ;

      public static void Update(TrackingPhoneOrderSource trackingPhoneOrderSource, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TrackingPhoneId", trackingPhoneOrderSource.TrackingPhoneId);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", trackingPhoneOrderSource.OrderSourceId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TrackingPhoneOrderSource trackingPhoneOrderSource)
      {
        Update(trackingPhoneOrderSource, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TrackingPhoneId, "
      
        + " OrderSourceId "
      

      + " From TrackingPhoneOrderSource "

      
        + " Where "
        
          + " TrackingPhoneId = ?TrackingPhoneId and  "
        
          + " OrderSourceId = ?OrderSourceId "
        
      ;

      public static TrackingPhoneOrderSource FindByPrimaryKey(
      int trackingPhoneId,int orderSourceId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TrackingPhoneId", trackingPhoneId);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", orderSourceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("TrackingPhoneOrderSource not found, search by primary key");

      }

      public static TrackingPhoneOrderSource FindByPrimaryKey(
      int trackingPhoneId,int orderSourceId
      )
      {
      return FindByPrimaryKey(
      trackingPhoneId,orderSourceId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TrackingPhoneOrderSource trackingPhoneOrderSource, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TrackingPhoneId",trackingPhoneOrderSource.TrackingPhoneId);
      
        Database.PutParameter(dbCommand,"?OrderSourceId",trackingPhoneOrderSource.OrderSourceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TrackingPhoneOrderSource trackingPhoneOrderSource)
      {
      return Exists(trackingPhoneOrderSource, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TrackingPhoneOrderSource limit 1";

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

      public static TrackingPhoneOrderSource Load(IDataReader dataReader, int offset)
      {
      TrackingPhoneOrderSource trackingPhoneOrderSource = new TrackingPhoneOrderSource();

      trackingPhoneOrderSource.TrackingPhoneId = dataReader.GetInt32(0 + offset);
          trackingPhoneOrderSource.OrderSourceId = dataReader.GetInt32(1 + offset);
          

      return trackingPhoneOrderSource;
      }

      public static TrackingPhoneOrderSource Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TrackingPhoneOrderSource "

      
        + " Where "
        
          + " TrackingPhoneId = ?TrackingPhoneId and  "
        
          + " OrderSourceId = ?OrderSourceId "
        
      ;
      public static void Delete(TrackingPhoneOrderSource trackingPhoneOrderSource, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TrackingPhoneId", trackingPhoneOrderSource.TrackingPhoneId);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", trackingPhoneOrderSource.OrderSourceId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TrackingPhoneOrderSource trackingPhoneOrderSource)
      {
        Delete(trackingPhoneOrderSource, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TrackingPhoneOrderSource ";

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

      
        + " TrackingPhoneId, "
      
        + " OrderSourceId "
      

      + " From TrackingPhoneOrderSource ";
      public static List<TrackingPhoneOrderSource> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TrackingPhoneOrderSource> rv = new List<TrackingPhoneOrderSource>();

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

      public static List<TrackingPhoneOrderSource> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TrackingPhoneOrderSource> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TrackingPhoneOrderSource> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TrackingPhoneOrderSource));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TrackingPhoneOrderSource item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TrackingPhoneOrderSource>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TrackingPhoneOrderSource));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TrackingPhoneOrderSource> itemsList
      = new List<TrackingPhoneOrderSource>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TrackingPhoneOrderSource)
      itemsList.Add(deserializedObject as TrackingPhoneOrderSource);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_trackingPhoneId;
      
        protected int m_orderSourceId;
      
      #endregion

      #region Constructors
      public TrackingPhoneOrderSource(
      int 
          trackingPhoneId,int 
          orderSourceId
      ) : this()
      {
      
        m_trackingPhoneId = trackingPhoneId;
      
        m_orderSourceId = orderSourceId;
      
      }

      
      #endregion

      
        [XmlElement]
        public int TrackingPhoneId
        {
        get { return m_trackingPhoneId;}
        set { m_trackingPhoneId = value; }
        }
      
        [XmlElement]
        public int OrderSourceId
        {
        get { return m_orderSourceId;}
        set { m_orderSourceId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 2; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    