
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


      public partial class MonitoringReading : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into MonitoringReading ( " +
      
        " MonitoringTaskId, " +
      
        " MonitoringReadingTypeId, " +
      
        " EquipmentSerialNumber, " +
      
        " Temperature, " +
      
        " RelativeHumidity, " +
      
        " BtuTonnage, " +
      
        " Notes, " +
      
        " Gpp " +
      
      ") Values (" +
      
        " ?MonitoringTaskId, " +
      
        " ?MonitoringReadingTypeId, " +
      
        " ?EquipmentSerialNumber, " +
      
        " ?Temperature, " +
      
        " ?RelativeHumidity, " +
      
        " ?BtuTonnage, " +
      
        " ?Notes, " +
      
        " ?Gpp " +
      
      ")";

      public static void Insert(MonitoringReading monitoringReading, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?MonitoringTaskId", monitoringReading.MonitoringTaskId);
      
        Database.PutParameter(dbCommand,"?MonitoringReadingTypeId", monitoringReading.MonitoringReadingTypeId);
      
        Database.PutParameter(dbCommand,"?EquipmentSerialNumber", monitoringReading.EquipmentSerialNumber);
      
        Database.PutParameter(dbCommand,"?Temperature", monitoringReading.Temperature);
      
        Database.PutParameter(dbCommand,"?RelativeHumidity", monitoringReading.RelativeHumidity);
      
        Database.PutParameter(dbCommand,"?BtuTonnage", monitoringReading.BtuTonnage);
      
        Database.PutParameter(dbCommand,"?Notes", monitoringReading.Notes);
      
        Database.PutParameter(dbCommand,"?Gpp", monitoringReading.Gpp);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        monitoringReading.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(MonitoringReading monitoringReading)
      {
        Insert(monitoringReading, null);
      }


      public static void Insert(List<MonitoringReading>  monitoringReadingList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(MonitoringReading monitoringReading in  monitoringReadingList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?MonitoringTaskId", monitoringReading.MonitoringTaskId);
      
        Database.PutParameter(dbCommand,"?MonitoringReadingTypeId", monitoringReading.MonitoringReadingTypeId);
      
        Database.PutParameter(dbCommand,"?EquipmentSerialNumber", monitoringReading.EquipmentSerialNumber);
      
        Database.PutParameter(dbCommand,"?Temperature", monitoringReading.Temperature);
      
        Database.PutParameter(dbCommand,"?RelativeHumidity", monitoringReading.RelativeHumidity);
      
        Database.PutParameter(dbCommand,"?BtuTonnage", monitoringReading.BtuTonnage);
      
        Database.PutParameter(dbCommand,"?Notes", monitoringReading.Notes);
      
        Database.PutParameter(dbCommand,"?Gpp", monitoringReading.Gpp);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?MonitoringTaskId",monitoringReading.MonitoringTaskId);
      
        Database.UpdateParameter(dbCommand,"?MonitoringReadingTypeId",monitoringReading.MonitoringReadingTypeId);
      
        Database.UpdateParameter(dbCommand,"?EquipmentSerialNumber",monitoringReading.EquipmentSerialNumber);
      
        Database.UpdateParameter(dbCommand,"?Temperature",monitoringReading.Temperature);
      
        Database.UpdateParameter(dbCommand,"?RelativeHumidity",monitoringReading.RelativeHumidity);
      
        Database.UpdateParameter(dbCommand,"?BtuTonnage",monitoringReading.BtuTonnage);
      
        Database.UpdateParameter(dbCommand,"?Notes",monitoringReading.Notes);
      
        Database.UpdateParameter(dbCommand,"?Gpp",monitoringReading.Gpp);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        monitoringReading.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<MonitoringReading>  monitoringReadingList)
      {
        Insert(monitoringReadingList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update MonitoringReading Set "
      
        + " MonitoringTaskId = ?MonitoringTaskId, "
      
        + " MonitoringReadingTypeId = ?MonitoringReadingTypeId, "
      
        + " EquipmentSerialNumber = ?EquipmentSerialNumber, "
      
        + " Temperature = ?Temperature, "
      
        + " RelativeHumidity = ?RelativeHumidity, "
      
        + " BtuTonnage = ?BtuTonnage, "
      
        + " Notes = ?Notes, "
      
        + " Gpp = ?Gpp "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(MonitoringReading monitoringReading, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", monitoringReading.ID);
      
        Database.PutParameter(dbCommand,"?MonitoringTaskId", monitoringReading.MonitoringTaskId);
      
        Database.PutParameter(dbCommand,"?MonitoringReadingTypeId", monitoringReading.MonitoringReadingTypeId);
      
        Database.PutParameter(dbCommand,"?EquipmentSerialNumber", monitoringReading.EquipmentSerialNumber);
      
        Database.PutParameter(dbCommand,"?Temperature", monitoringReading.Temperature);
      
        Database.PutParameter(dbCommand,"?RelativeHumidity", monitoringReading.RelativeHumidity);
      
        Database.PutParameter(dbCommand,"?BtuTonnage", monitoringReading.BtuTonnage);
      
        Database.PutParameter(dbCommand,"?Notes", monitoringReading.Notes);
      
        Database.PutParameter(dbCommand,"?Gpp", monitoringReading.Gpp);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(MonitoringReading monitoringReading)
      {
        Update(monitoringReading, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " MonitoringTaskId, "
      
        + " MonitoringReadingTypeId, "
      
        + " EquipmentSerialNumber, "
      
        + " Temperature, "
      
        + " RelativeHumidity, "
      
        + " BtuTonnage, "
      
        + " Notes, "
      
        + " Gpp "
      

      + " From MonitoringReading "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static MonitoringReading FindByPrimaryKey(
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
      throw new DataNotFoundException("MonitoringReading not found, search by primary key");

      }

      public static MonitoringReading FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(MonitoringReading monitoringReading, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",monitoringReading.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(MonitoringReading monitoringReading)
      {
      return Exists(monitoringReading, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from MonitoringReading limit 1";

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

      public static MonitoringReading Load(IDataReader dataReader, int offset)
      {
      MonitoringReading monitoringReading = new MonitoringReading();

      monitoringReading.ID = dataReader.GetInt32(0 + offset);
          monitoringReading.MonitoringTaskId = dataReader.GetInt32(1 + offset);
          monitoringReading.MonitoringReadingTypeId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            monitoringReading.EquipmentSerialNumber = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            monitoringReading.Temperature = dataReader.GetDecimal(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            monitoringReading.RelativeHumidity = dataReader.GetDecimal(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            monitoringReading.BtuTonnage = dataReader.GetDecimal(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            monitoringReading.Notes = dataReader.GetString(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            monitoringReading.Gpp = dataReader.GetDecimal(8 + offset);
          

      return monitoringReading;
      }

      public static MonitoringReading Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From MonitoringReading "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(MonitoringReading monitoringReading, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", monitoringReading.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(MonitoringReading monitoringReading)
      {
        Delete(monitoringReading, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From MonitoringReading ";

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
      
        + " MonitoringTaskId, "
      
        + " MonitoringReadingTypeId, "
      
        + " EquipmentSerialNumber, "
      
        + " Temperature, "
      
        + " RelativeHumidity, "
      
        + " BtuTonnage, "
      
        + " Notes, "
      
        + " Gpp "
      

      + " From MonitoringReading ";
      public static List<MonitoringReading> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<MonitoringReading> rv = new List<MonitoringReading>();

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

      public static List<MonitoringReading> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<MonitoringReading> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (MonitoringReading obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && MonitoringTaskId == obj.MonitoringTaskId && MonitoringReadingTypeId == obj.MonitoringReadingTypeId && EquipmentSerialNumber == obj.EquipmentSerialNumber && Temperature == obj.Temperature && RelativeHumidity == obj.RelativeHumidity && BtuTonnage == obj.BtuTonnage && Notes == obj.Notes && Gpp == obj.Gpp;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<MonitoringReading> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(MonitoringReading));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(MonitoringReading item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<MonitoringReading>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(MonitoringReading));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<MonitoringReading> itemsList
      = new List<MonitoringReading>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is MonitoringReading)
      itemsList.Add(deserializedObject as MonitoringReading);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_monitoringTaskId;
      
        protected int m_monitoringReadingTypeId;
      
        protected String m_equipmentSerialNumber;
      
        protected decimal m_temperature;
      
        protected decimal m_relativeHumidity;
      
        protected decimal m_btuTonnage;
      
        protected String m_notes;
      
        protected decimal m_gpp;
      
      #endregion

      #region Constructors
      public MonitoringReading(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public MonitoringReading(
        int 
          iD,int 
          monitoringTaskId,int 
          monitoringReadingTypeId,String 
          equipmentSerialNumber,decimal 
          temperature,decimal 
          relativeHumidity,decimal 
          btuTonnage,String 
          notes,decimal 
          gpp
        ) : this()
        {
        
          m_iD = iD;
        
          m_monitoringTaskId = monitoringTaskId;
        
          m_monitoringReadingTypeId = monitoringReadingTypeId;
        
          m_equipmentSerialNumber = equipmentSerialNumber;
        
          m_temperature = temperature;
        
          m_relativeHumidity = relativeHumidity;
        
          m_btuTonnage = btuTonnage;
        
          m_notes = notes;
        
          m_gpp = gpp;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int MonitoringTaskId
        {
        get { return m_monitoringTaskId;}
        set { m_monitoringTaskId = value; }
        }
      
        [XmlElement]
        public int MonitoringReadingTypeId
        {
        get { return m_monitoringReadingTypeId;}
        set { m_monitoringReadingTypeId = value; }
        }
      
        [XmlElement]
        public String EquipmentSerialNumber
        {
        get { return m_equipmentSerialNumber;}
        set { m_equipmentSerialNumber = value; }
        }
      
        [XmlElement]
        public decimal Temperature
        {
        get { return m_temperature;}
        set { m_temperature = value; }
        }
      
        [XmlElement]
        public decimal RelativeHumidity
        {
        get { return m_relativeHumidity;}
        set { m_relativeHumidity = value; }
        }
      
        [XmlElement]
        public decimal BtuTonnage
        {
        get { return m_btuTonnage;}
        set { m_btuTonnage = value; }
        }
      
        [XmlElement]
        public String Notes
        {
        get { return m_notes;}
        set { m_notes = value; }
        }
      
        [XmlElement]
        public decimal Gpp
        {
        get { return m_gpp;}
        set { m_gpp = value; }
        }
      

      public static int FieldsCount
      {
      get { return 9; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    