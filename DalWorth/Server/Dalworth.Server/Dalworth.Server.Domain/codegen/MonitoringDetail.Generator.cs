
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


      public partial class MonitoringDetail : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into MonitoringDetail ( " +
      
        " MonitoringTaskId, " +
      
        " IsAllEquipmentOn, " +
      
        " IsPlacementCorrect, " +
      
        " IsCarpetRaked, " +
      
        " IsFurnitureBlocked, " +
      
        " IsOdorsPresent, " +
      
        " IsAreaClean, " +
      
        " IsBasePulled, " +
      
        " BaseLocation, " +
      
        " IsReadingAndMoistureMapFilledOut, " +
      
        " IsConstructionNeeded, " +
      
        " ConstructionNeededNotes, " +
      
        " CheckPadAndSubFloor, " +
      
        " WallSurface, " +
      
        " IsNoReadings " +
      
      ") Values (" +
      
        " ?MonitoringTaskId, " +
      
        " ?IsAllEquipmentOn, " +
      
        " ?IsPlacementCorrect, " +
      
        " ?IsCarpetRaked, " +
      
        " ?IsFurnitureBlocked, " +
      
        " ?IsOdorsPresent, " +
      
        " ?IsAreaClean, " +
      
        " ?IsBasePulled, " +
      
        " ?BaseLocation, " +
      
        " ?IsReadingAndMoistureMapFilledOut, " +
      
        " ?IsConstructionNeeded, " +
      
        " ?ConstructionNeededNotes, " +
      
        " ?CheckPadAndSubFloor, " +
      
        " ?WallSurface, " +
      
        " ?IsNoReadings " +
      
      ")";

      public static void Insert(MonitoringDetail monitoringDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?MonitoringTaskId", monitoringDetail.MonitoringTaskId);
      
        Database.PutParameter(dbCommand,"?IsAllEquipmentOn", monitoringDetail.IsAllEquipmentOn);
      
        Database.PutParameter(dbCommand,"?IsPlacementCorrect", monitoringDetail.IsPlacementCorrect);
      
        Database.PutParameter(dbCommand,"?IsCarpetRaked", monitoringDetail.IsCarpetRaked);
      
        Database.PutParameter(dbCommand,"?IsFurnitureBlocked", monitoringDetail.IsFurnitureBlocked);
      
        Database.PutParameter(dbCommand,"?IsOdorsPresent", monitoringDetail.IsOdorsPresent);
      
        Database.PutParameter(dbCommand,"?IsAreaClean", monitoringDetail.IsAreaClean);
      
        Database.PutParameter(dbCommand,"?IsBasePulled", monitoringDetail.IsBasePulled);
      
        Database.PutParameter(dbCommand,"?BaseLocation", monitoringDetail.BaseLocation);
      
        Database.PutParameter(dbCommand,"?IsReadingAndMoistureMapFilledOut", monitoringDetail.IsReadingAndMoistureMapFilledOut);
      
        Database.PutParameter(dbCommand,"?IsConstructionNeeded", monitoringDetail.IsConstructionNeeded);
      
        Database.PutParameter(dbCommand,"?ConstructionNeededNotes", monitoringDetail.ConstructionNeededNotes);
      
        Database.PutParameter(dbCommand,"?CheckPadAndSubFloor", monitoringDetail.CheckPadAndSubFloor);
      
        Database.PutParameter(dbCommand,"?WallSurface", monitoringDetail.WallSurface);
      
        Database.PutParameter(dbCommand,"?IsNoReadings", monitoringDetail.IsNoReadings);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(MonitoringDetail monitoringDetail)
      {
        Insert(monitoringDetail, null);
      }


      public static void Insert(List<MonitoringDetail>  monitoringDetailList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(MonitoringDetail monitoringDetail in  monitoringDetailList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?MonitoringTaskId", monitoringDetail.MonitoringTaskId);
      
        Database.PutParameter(dbCommand,"?IsAllEquipmentOn", monitoringDetail.IsAllEquipmentOn);
      
        Database.PutParameter(dbCommand,"?IsPlacementCorrect", monitoringDetail.IsPlacementCorrect);
      
        Database.PutParameter(dbCommand,"?IsCarpetRaked", monitoringDetail.IsCarpetRaked);
      
        Database.PutParameter(dbCommand,"?IsFurnitureBlocked", monitoringDetail.IsFurnitureBlocked);
      
        Database.PutParameter(dbCommand,"?IsOdorsPresent", monitoringDetail.IsOdorsPresent);
      
        Database.PutParameter(dbCommand,"?IsAreaClean", monitoringDetail.IsAreaClean);
      
        Database.PutParameter(dbCommand,"?IsBasePulled", monitoringDetail.IsBasePulled);
      
        Database.PutParameter(dbCommand,"?BaseLocation", monitoringDetail.BaseLocation);
      
        Database.PutParameter(dbCommand,"?IsReadingAndMoistureMapFilledOut", monitoringDetail.IsReadingAndMoistureMapFilledOut);
      
        Database.PutParameter(dbCommand,"?IsConstructionNeeded", monitoringDetail.IsConstructionNeeded);
      
        Database.PutParameter(dbCommand,"?ConstructionNeededNotes", monitoringDetail.ConstructionNeededNotes);
      
        Database.PutParameter(dbCommand,"?CheckPadAndSubFloor", monitoringDetail.CheckPadAndSubFloor);
      
        Database.PutParameter(dbCommand,"?WallSurface", monitoringDetail.WallSurface);
      
        Database.PutParameter(dbCommand,"?IsNoReadings", monitoringDetail.IsNoReadings);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?MonitoringTaskId",monitoringDetail.MonitoringTaskId);
      
        Database.UpdateParameter(dbCommand,"?IsAllEquipmentOn",monitoringDetail.IsAllEquipmentOn);
      
        Database.UpdateParameter(dbCommand,"?IsPlacementCorrect",monitoringDetail.IsPlacementCorrect);
      
        Database.UpdateParameter(dbCommand,"?IsCarpetRaked",monitoringDetail.IsCarpetRaked);
      
        Database.UpdateParameter(dbCommand,"?IsFurnitureBlocked",monitoringDetail.IsFurnitureBlocked);
      
        Database.UpdateParameter(dbCommand,"?IsOdorsPresent",monitoringDetail.IsOdorsPresent);
      
        Database.UpdateParameter(dbCommand,"?IsAreaClean",monitoringDetail.IsAreaClean);
      
        Database.UpdateParameter(dbCommand,"?IsBasePulled",monitoringDetail.IsBasePulled);
      
        Database.UpdateParameter(dbCommand,"?BaseLocation",monitoringDetail.BaseLocation);
      
        Database.UpdateParameter(dbCommand,"?IsReadingAndMoistureMapFilledOut",monitoringDetail.IsReadingAndMoistureMapFilledOut);
      
        Database.UpdateParameter(dbCommand,"?IsConstructionNeeded",monitoringDetail.IsConstructionNeeded);
      
        Database.UpdateParameter(dbCommand,"?ConstructionNeededNotes",monitoringDetail.ConstructionNeededNotes);
      
        Database.UpdateParameter(dbCommand,"?CheckPadAndSubFloor",monitoringDetail.CheckPadAndSubFloor);
      
        Database.UpdateParameter(dbCommand,"?WallSurface",monitoringDetail.WallSurface);
      
        Database.UpdateParameter(dbCommand,"?IsNoReadings",monitoringDetail.IsNoReadings);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<MonitoringDetail>  monitoringDetailList)
      {
        Insert(monitoringDetailList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update MonitoringDetail Set "
      
        + " IsAllEquipmentOn = ?IsAllEquipmentOn, "
      
        + " IsPlacementCorrect = ?IsPlacementCorrect, "
      
        + " IsCarpetRaked = ?IsCarpetRaked, "
      
        + " IsFurnitureBlocked = ?IsFurnitureBlocked, "
      
        + " IsOdorsPresent = ?IsOdorsPresent, "
      
        + " IsAreaClean = ?IsAreaClean, "
      
        + " IsBasePulled = ?IsBasePulled, "
      
        + " BaseLocation = ?BaseLocation, "
      
        + " IsReadingAndMoistureMapFilledOut = ?IsReadingAndMoistureMapFilledOut, "
      
        + " IsConstructionNeeded = ?IsConstructionNeeded, "
      
        + " ConstructionNeededNotes = ?ConstructionNeededNotes, "
      
        + " CheckPadAndSubFloor = ?CheckPadAndSubFloor, "
      
        + " WallSurface = ?WallSurface, "
      
        + " IsNoReadings = ?IsNoReadings "
      
        + " Where "
        
          + " MonitoringTaskId = ?MonitoringTaskId "
        
      ;

      public static void Update(MonitoringDetail monitoringDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?MonitoringTaskId", monitoringDetail.MonitoringTaskId);
      
        Database.PutParameter(dbCommand,"?IsAllEquipmentOn", monitoringDetail.IsAllEquipmentOn);
      
        Database.PutParameter(dbCommand,"?IsPlacementCorrect", monitoringDetail.IsPlacementCorrect);
      
        Database.PutParameter(dbCommand,"?IsCarpetRaked", monitoringDetail.IsCarpetRaked);
      
        Database.PutParameter(dbCommand,"?IsFurnitureBlocked", monitoringDetail.IsFurnitureBlocked);
      
        Database.PutParameter(dbCommand,"?IsOdorsPresent", monitoringDetail.IsOdorsPresent);
      
        Database.PutParameter(dbCommand,"?IsAreaClean", monitoringDetail.IsAreaClean);
      
        Database.PutParameter(dbCommand,"?IsBasePulled", monitoringDetail.IsBasePulled);
      
        Database.PutParameter(dbCommand,"?BaseLocation", monitoringDetail.BaseLocation);
      
        Database.PutParameter(dbCommand,"?IsReadingAndMoistureMapFilledOut", monitoringDetail.IsReadingAndMoistureMapFilledOut);
      
        Database.PutParameter(dbCommand,"?IsConstructionNeeded", monitoringDetail.IsConstructionNeeded);
      
        Database.PutParameter(dbCommand,"?ConstructionNeededNotes", monitoringDetail.ConstructionNeededNotes);
      
        Database.PutParameter(dbCommand,"?CheckPadAndSubFloor", monitoringDetail.CheckPadAndSubFloor);
      
        Database.PutParameter(dbCommand,"?WallSurface", monitoringDetail.WallSurface);
      
        Database.PutParameter(dbCommand,"?IsNoReadings", monitoringDetail.IsNoReadings);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(MonitoringDetail monitoringDetail)
      {
        Update(monitoringDetail, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " MonitoringTaskId, "
      
        + " IsAllEquipmentOn, "
      
        + " IsPlacementCorrect, "
      
        + " IsCarpetRaked, "
      
        + " IsFurnitureBlocked, "
      
        + " IsOdorsPresent, "
      
        + " IsAreaClean, "
      
        + " IsBasePulled, "
      
        + " BaseLocation, "
      
        + " IsReadingAndMoistureMapFilledOut, "
      
        + " IsConstructionNeeded, "
      
        + " ConstructionNeededNotes, "
      
        + " CheckPadAndSubFloor, "
      
        + " WallSurface, "
      
        + " IsNoReadings "
      

      + " From MonitoringDetail "

      
        + " Where "
        
          + " MonitoringTaskId = ?MonitoringTaskId "
        
      ;

      public static MonitoringDetail FindByPrimaryKey(
      int monitoringTaskId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?MonitoringTaskId", monitoringTaskId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("MonitoringDetail not found, search by primary key");

      }

      public static MonitoringDetail FindByPrimaryKey(
      int monitoringTaskId
      )
      {
      return FindByPrimaryKey(
      monitoringTaskId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(MonitoringDetail monitoringDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?MonitoringTaskId",monitoringDetail.MonitoringTaskId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(MonitoringDetail monitoringDetail)
      {
      return Exists(monitoringDetail, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from MonitoringDetail limit 1";

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

      public static MonitoringDetail Load(IDataReader dataReader, int offset)
      {
      MonitoringDetail monitoringDetail = new MonitoringDetail();

      monitoringDetail.MonitoringTaskId = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            monitoringDetail.IsAllEquipmentOn = dataReader.GetBoolean(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            monitoringDetail.IsPlacementCorrect = dataReader.GetBoolean(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            monitoringDetail.IsCarpetRaked = dataReader.GetBoolean(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            monitoringDetail.IsFurnitureBlocked = dataReader.GetBoolean(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            monitoringDetail.IsOdorsPresent = dataReader.GetBoolean(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            monitoringDetail.IsAreaClean = dataReader.GetBoolean(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            monitoringDetail.IsBasePulled = dataReader.GetBoolean(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            monitoringDetail.BaseLocation = dataReader.GetString(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            monitoringDetail.IsReadingAndMoistureMapFilledOut = dataReader.GetBoolean(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            monitoringDetail.IsConstructionNeeded = dataReader.GetBoolean(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            monitoringDetail.ConstructionNeededNotes = dataReader.GetString(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            monitoringDetail.CheckPadAndSubFloor = dataReader.GetString(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            monitoringDetail.WallSurface = dataReader.GetString(13 + offset);
          
            if(!dataReader.IsDBNull(14 + offset))
            monitoringDetail.IsNoReadings = dataReader.GetBoolean(14 + offset);
          

      return monitoringDetail;
      }

      public static MonitoringDetail Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From MonitoringDetail "

      
        + " Where "
        
          + " MonitoringTaskId = ?MonitoringTaskId "
        
      ;
      public static void Delete(MonitoringDetail monitoringDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?MonitoringTaskId", monitoringDetail.MonitoringTaskId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(MonitoringDetail monitoringDetail)
      {
        Delete(monitoringDetail, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From MonitoringDetail ";

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

      
        + " MonitoringTaskId, "
      
        + " IsAllEquipmentOn, "
      
        + " IsPlacementCorrect, "
      
        + " IsCarpetRaked, "
      
        + " IsFurnitureBlocked, "
      
        + " IsOdorsPresent, "
      
        + " IsAreaClean, "
      
        + " IsBasePulled, "
      
        + " BaseLocation, "
      
        + " IsReadingAndMoistureMapFilledOut, "
      
        + " IsConstructionNeeded, "
      
        + " ConstructionNeededNotes, "
      
        + " CheckPadAndSubFloor, "
      
        + " WallSurface, "
      
        + " IsNoReadings "
      

      + " From MonitoringDetail ";
      public static List<MonitoringDetail> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<MonitoringDetail> rv = new List<MonitoringDetail>();

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

      public static List<MonitoringDetail> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<MonitoringDetail> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (MonitoringDetail obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return MonitoringTaskId == obj.MonitoringTaskId && IsAllEquipmentOn == obj.IsAllEquipmentOn && IsPlacementCorrect == obj.IsPlacementCorrect && IsCarpetRaked == obj.IsCarpetRaked && IsFurnitureBlocked == obj.IsFurnitureBlocked && IsOdorsPresent == obj.IsOdorsPresent && IsAreaClean == obj.IsAreaClean && IsBasePulled == obj.IsBasePulled && BaseLocation == obj.BaseLocation && IsReadingAndMoistureMapFilledOut == obj.IsReadingAndMoistureMapFilledOut && IsConstructionNeeded == obj.IsConstructionNeeded && ConstructionNeededNotes == obj.ConstructionNeededNotes && CheckPadAndSubFloor == obj.CheckPadAndSubFloor && WallSurface == obj.WallSurface && IsNoReadings == obj.IsNoReadings;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<MonitoringDetail> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(MonitoringDetail));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(MonitoringDetail item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<MonitoringDetail>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(MonitoringDetail));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<MonitoringDetail> itemsList
      = new List<MonitoringDetail>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is MonitoringDetail)
      itemsList.Add(deserializedObject as MonitoringDetail);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_monitoringTaskId;
      
        protected bool m_isAllEquipmentOn;
      
        protected bool m_isPlacementCorrect;
      
        protected bool m_isCarpetRaked;
      
        protected bool m_isFurnitureBlocked;
      
        protected bool m_isOdorsPresent;
      
        protected bool m_isAreaClean;
      
        protected bool m_isBasePulled;
      
        protected String m_baseLocation;
      
        protected bool m_isReadingAndMoistureMapFilledOut;
      
        protected bool m_isConstructionNeeded;
      
        protected String m_constructionNeededNotes;
      
        protected String m_checkPadAndSubFloor;
      
        protected String m_wallSurface;
      
        protected bool m_isNoReadings;
      
      #endregion

      #region Constructors
      public MonitoringDetail(
      int 
          monitoringTaskId
      ) : this()
      {
      
        m_monitoringTaskId = monitoringTaskId;
      
      }

      


        public MonitoringDetail(
        int 
          monitoringTaskId,bool 
          isAllEquipmentOn,bool 
          isPlacementCorrect,bool 
          isCarpetRaked,bool 
          isFurnitureBlocked,bool 
          isOdorsPresent,bool 
          isAreaClean,bool 
          isBasePulled,String 
          baseLocation,bool 
          isReadingAndMoistureMapFilledOut,bool 
          isConstructionNeeded,String 
          constructionNeededNotes,String 
          checkPadAndSubFloor,String 
          wallSurface,bool 
          isNoReadings
        ) : this()
        {
        
          m_monitoringTaskId = monitoringTaskId;
        
          m_isAllEquipmentOn = isAllEquipmentOn;
        
          m_isPlacementCorrect = isPlacementCorrect;
        
          m_isCarpetRaked = isCarpetRaked;
        
          m_isFurnitureBlocked = isFurnitureBlocked;
        
          m_isOdorsPresent = isOdorsPresent;
        
          m_isAreaClean = isAreaClean;
        
          m_isBasePulled = isBasePulled;
        
          m_baseLocation = baseLocation;
        
          m_isReadingAndMoistureMapFilledOut = isReadingAndMoistureMapFilledOut;
        
          m_isConstructionNeeded = isConstructionNeeded;
        
          m_constructionNeededNotes = constructionNeededNotes;
        
          m_checkPadAndSubFloor = checkPadAndSubFloor;
        
          m_wallSurface = wallSurface;
        
          m_isNoReadings = isNoReadings;
        
        }


      
      #endregion

      
        [XmlElement]
        public int MonitoringTaskId
        {
        get { return m_monitoringTaskId;}
        set { m_monitoringTaskId = value; }
        }
      
        [XmlElement]
        public bool IsAllEquipmentOn
        {
        get { return m_isAllEquipmentOn;}
        set { m_isAllEquipmentOn = value; }
        }
      
        [XmlElement]
        public bool IsPlacementCorrect
        {
        get { return m_isPlacementCorrect;}
        set { m_isPlacementCorrect = value; }
        }
      
        [XmlElement]
        public bool IsCarpetRaked
        {
        get { return m_isCarpetRaked;}
        set { m_isCarpetRaked = value; }
        }
      
        [XmlElement]
        public bool IsFurnitureBlocked
        {
        get { return m_isFurnitureBlocked;}
        set { m_isFurnitureBlocked = value; }
        }
      
        [XmlElement]
        public bool IsOdorsPresent
        {
        get { return m_isOdorsPresent;}
        set { m_isOdorsPresent = value; }
        }
      
        [XmlElement]
        public bool IsAreaClean
        {
        get { return m_isAreaClean;}
        set { m_isAreaClean = value; }
        }
      
        [XmlElement]
        public bool IsBasePulled
        {
        get { return m_isBasePulled;}
        set { m_isBasePulled = value; }
        }
      
        [XmlElement]
        public String BaseLocation
        {
        get { return m_baseLocation;}
        set { m_baseLocation = value; }
        }
      
        [XmlElement]
        public bool IsReadingAndMoistureMapFilledOut
        {
        get { return m_isReadingAndMoistureMapFilledOut;}
        set { m_isReadingAndMoistureMapFilledOut = value; }
        }
      
        [XmlElement]
        public bool IsConstructionNeeded
        {
        get { return m_isConstructionNeeded;}
        set { m_isConstructionNeeded = value; }
        }
      
        [XmlElement]
        public String ConstructionNeededNotes
        {
        get { return m_constructionNeededNotes;}
        set { m_constructionNeededNotes = value; }
        }
      
        [XmlElement]
        public String CheckPadAndSubFloor
        {
        get { return m_checkPadAndSubFloor;}
        set { m_checkPadAndSubFloor = value; }
        }
      
        [XmlElement]
        public String WallSurface
        {
        get { return m_wallSurface;}
        set { m_wallSurface = value; }
        }
      
        [XmlElement]
        public bool IsNoReadings
        {
        get { return m_isNoReadings;}
        set { m_isNoReadings = value; }
        }
      

      public static int FieldsCount
      {
      get { return 15; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    