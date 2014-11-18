
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
      public partial class BucketConstraint : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into BucketConstraint ( " +
      
        " AreaId, " +
      
        " TimeFrameId, " +
      
        " MaxBucketCapacity " +
      
      ") Values (" +
      
        " ?AreaId, " +
      
        " ?TimeFrameId, " +
      
        " ?MaxBucketCapacity " +
      
      ")";

      public static void Insert(BucketConstraint bucketConstraint, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?AreaId", bucketConstraint.AreaId);
      
        Database.PutParameter(dbCommand,"?TimeFrameId", bucketConstraint.TimeFrameId);
      
        Database.PutParameter(dbCommand,"?MaxBucketCapacity", bucketConstraint.MaxBucketCapacity);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(BucketConstraint bucketConstraint)
      {
        Insert(bucketConstraint, null);
      }


      public static void Insert(List<BucketConstraint>  bucketConstraintList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(BucketConstraint bucketConstraint in  bucketConstraintList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?AreaId", bucketConstraint.AreaId);
      
        Database.PutParameter(dbCommand,"?TimeFrameId", bucketConstraint.TimeFrameId);
      
        Database.PutParameter(dbCommand,"?MaxBucketCapacity", bucketConstraint.MaxBucketCapacity);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?AreaId",bucketConstraint.AreaId);
      
        Database.UpdateParameter(dbCommand,"?TimeFrameId",bucketConstraint.TimeFrameId);
      
        Database.UpdateParameter(dbCommand,"?MaxBucketCapacity",bucketConstraint.MaxBucketCapacity);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<BucketConstraint>  bucketConstraintList)
      {
        Insert(bucketConstraintList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update BucketConstraint Set "
      
        + " MaxBucketCapacity = ?MaxBucketCapacity "
      
        + " Where "
        
          + " AreaId = ?AreaId and  "
        
          + " TimeFrameId = ?TimeFrameId "
        
      ;

      public static void Update(BucketConstraint bucketConstraint, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?AreaId", bucketConstraint.AreaId);
      
        Database.PutParameter(dbCommand,"?TimeFrameId", bucketConstraint.TimeFrameId);
      
        Database.PutParameter(dbCommand,"?MaxBucketCapacity", bucketConstraint.MaxBucketCapacity);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(BucketConstraint bucketConstraint)
      {
        Update(bucketConstraint, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " AreaId, "
      
        + " TimeFrameId, "
      
        + " MaxBucketCapacity "
      

      + " From BucketConstraint "

      
        + " Where "
        
          + " AreaId = ?AreaId and  "
        
          + " TimeFrameId = ?TimeFrameId "
        
      ;

      public static BucketConstraint FindByPrimaryKey(
      int areaId,int timeFrameId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?AreaId", areaId);
      
        Database.PutParameter(dbCommand,"?TimeFrameId", timeFrameId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("BucketConstraint not found, search by primary key");

      }

      public static BucketConstraint FindByPrimaryKey(
      int areaId,int timeFrameId
      )
      {
      return FindByPrimaryKey(
      areaId,timeFrameId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(BucketConstraint bucketConstraint, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?AreaId",bucketConstraint.AreaId);
      
        Database.PutParameter(dbCommand,"?TimeFrameId",bucketConstraint.TimeFrameId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(BucketConstraint bucketConstraint)
      {
      return Exists(bucketConstraint, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from BucketConstraint limit 1";

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

      public static BucketConstraint Load(IDataReader dataReader, int offset)
      {
      BucketConstraint bucketConstraint = new BucketConstraint();

      bucketConstraint.AreaId = dataReader.GetInt32(0 + offset);
          bucketConstraint.TimeFrameId = dataReader.GetInt32(1 + offset);
          bucketConstraint.MaxBucketCapacity = dataReader.GetInt32(2 + offset);
          

      return bucketConstraint;
      }

      public static BucketConstraint Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From BucketConstraint "

      
        + " Where "
        
          + " AreaId = ?AreaId and  "
        
          + " TimeFrameId = ?TimeFrameId "
        
      ;
      public static void Delete(BucketConstraint bucketConstraint, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?AreaId", bucketConstraint.AreaId);
      
        Database.PutParameter(dbCommand,"?TimeFrameId", bucketConstraint.TimeFrameId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(BucketConstraint bucketConstraint)
      {
        Delete(bucketConstraint, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From BucketConstraint ";

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

      
        + " AreaId, "
      
        + " TimeFrameId, "
      
        + " MaxBucketCapacity "
      

      + " From BucketConstraint ";
      public static List<BucketConstraint> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<BucketConstraint> rv = new List<BucketConstraint>();

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

      public static List<BucketConstraint> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<BucketConstraint> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<BucketConstraint> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(BucketConstraint));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(BucketConstraint item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<BucketConstraint>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(BucketConstraint));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<BucketConstraint> itemsList
      = new List<BucketConstraint>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is BucketConstraint)
      itemsList.Add(deserializedObject as BucketConstraint);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_areaId;
      
        protected int m_timeFrameId;
      
        protected int m_maxBucketCapacity;
      
      #endregion

      #region Constructors
      public BucketConstraint(
      int 
          areaId,int 
          timeFrameId
      ) : this()
      {
      
        m_areaId = areaId;
      
        m_timeFrameId = timeFrameId;
      
      }

      


        public BucketConstraint(
        int 
          areaId,int 
          timeFrameId,int 
          maxBucketCapacity
        ) : this()
        {
        
          m_areaId = areaId;
        
          m_timeFrameId = timeFrameId;
        
          m_maxBucketCapacity = maxBucketCapacity;
        
        }


      
      #endregion

      
        [DataMember]
        public int AreaId
        {
        get { return m_areaId;}
        set { m_areaId = value; }
        }
      
        [DataMember]
        public int TimeFrameId
        {
        get { return m_timeFrameId;}
        set { m_timeFrameId = value; }
        }
      
        [DataMember]
        public int MaxBucketCapacity
        {
        get { return m_maxBucketCapacity;}
        set { m_maxBucketCapacity = value; }
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

    