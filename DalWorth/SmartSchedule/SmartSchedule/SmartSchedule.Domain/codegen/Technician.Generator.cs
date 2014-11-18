
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
      public partial class Technician : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Technician ( " +
      
        " TechnicianDefaultId, " +
      
        " ScheduleDate, " +
      
        " ServmanId, " +
      
        " Name, " +
      
        " HourlyRate, " +
      
        " HourlyRate150to300, " +
      
        " HourlyRateMore300, " +
      
        " DisplaySequence, " +
      
        " CompanyId, " +
      
        " DepotAddress, " +
      
        " DepotLatitude, " +
      
        " DepotLongitude, " +
      
        " DriveTimeMinutes, " +
      
        " IsContractor, " +
      
        " MaxVisitsCount, " +
      
        " MaxNonExclusiveVisitsCount " +
      
      ") Values (" +
      
        " ?TechnicianDefaultId, " +
      
        " ?ScheduleDate, " +
      
        " ?ServmanId, " +
      
        " ?Name, " +
      
        " ?HourlyRate, " +
      
        " ?HourlyRate150to300, " +
      
        " ?HourlyRateMore300, " +
      
        " ?DisplaySequence, " +
      
        " ?CompanyId, " +
      
        " ?DepotAddress, " +
      
        " ?DepotLatitude, " +
      
        " ?DepotLongitude, " +
      
        " ?DriveTimeMinutes, " +
      
        " ?IsContractor, " +
      
        " ?MaxVisitsCount, " +
      
        " ?MaxNonExclusiveVisitsCount " +
      
      ")";

      public static void Insert(Technician technician, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianDefaultId", technician.TechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?ScheduleDate", technician.ScheduleDate);
      
        Database.PutParameter(dbCommand,"?ServmanId", technician.ServmanId);
      
        Database.PutParameter(dbCommand,"?Name", technician.Name);
      
        Database.PutParameter(dbCommand,"?HourlyRate", technician.HourlyRate);
      
        Database.PutParameter(dbCommand,"?HourlyRate150to300", technician.HourlyRate150to300);
      
        Database.PutParameter(dbCommand,"?HourlyRateMore300", technician.HourlyRateMore300);
      
        Database.PutParameter(dbCommand,"?DisplaySequence", technician.DisplaySequence);
      
        Database.PutParameter(dbCommand,"?CompanyId", technician.CompanyId);
      
        Database.PutParameter(dbCommand,"?DepotAddress", technician.DepotAddress);
      
        Database.PutParameter(dbCommand,"?DepotLatitude", technician.DepotLatitude);
      
        Database.PutParameter(dbCommand,"?DepotLongitude", technician.DepotLongitude);
      
        Database.PutParameter(dbCommand,"?DriveTimeMinutes", technician.DriveTimeMinutes);
      
        Database.PutParameter(dbCommand,"?IsContractor", technician.IsContractor);
      
        Database.PutParameter(dbCommand,"?MaxVisitsCount", technician.MaxVisitsCount);
      
        Database.PutParameter(dbCommand,"?MaxNonExclusiveVisitsCount", technician.MaxNonExclusiveVisitsCount);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        technician.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Technician technician)
      {
        Insert(technician, null);
      }


      public static void Insert(List<Technician>  technicianList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Technician technician in  technicianList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TechnicianDefaultId", technician.TechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?ScheduleDate", technician.ScheduleDate);
      
        Database.PutParameter(dbCommand,"?ServmanId", technician.ServmanId);
      
        Database.PutParameter(dbCommand,"?Name", technician.Name);
      
        Database.PutParameter(dbCommand,"?HourlyRate", technician.HourlyRate);
      
        Database.PutParameter(dbCommand,"?HourlyRate150to300", technician.HourlyRate150to300);
      
        Database.PutParameter(dbCommand,"?HourlyRateMore300", technician.HourlyRateMore300);
      
        Database.PutParameter(dbCommand,"?DisplaySequence", technician.DisplaySequence);
      
        Database.PutParameter(dbCommand,"?CompanyId", technician.CompanyId);
      
        Database.PutParameter(dbCommand,"?DepotAddress", technician.DepotAddress);
      
        Database.PutParameter(dbCommand,"?DepotLatitude", technician.DepotLatitude);
      
        Database.PutParameter(dbCommand,"?DepotLongitude", technician.DepotLongitude);
      
        Database.PutParameter(dbCommand,"?DriveTimeMinutes", technician.DriveTimeMinutes);
      
        Database.PutParameter(dbCommand,"?IsContractor", technician.IsContractor);
      
        Database.PutParameter(dbCommand,"?MaxVisitsCount", technician.MaxVisitsCount);
      
        Database.PutParameter(dbCommand,"?MaxNonExclusiveVisitsCount", technician.MaxNonExclusiveVisitsCount);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TechnicianDefaultId",technician.TechnicianDefaultId);
      
        Database.UpdateParameter(dbCommand,"?ScheduleDate",technician.ScheduleDate);
      
        Database.UpdateParameter(dbCommand,"?ServmanId",technician.ServmanId);
      
        Database.UpdateParameter(dbCommand,"?Name",technician.Name);
      
        Database.UpdateParameter(dbCommand,"?HourlyRate",technician.HourlyRate);
      
        Database.UpdateParameter(dbCommand,"?HourlyRate150to300",technician.HourlyRate150to300);
      
        Database.UpdateParameter(dbCommand,"?HourlyRateMore300",technician.HourlyRateMore300);
      
        Database.UpdateParameter(dbCommand,"?DisplaySequence",technician.DisplaySequence);
      
        Database.UpdateParameter(dbCommand,"?CompanyId",technician.CompanyId);
      
        Database.UpdateParameter(dbCommand,"?DepotAddress",technician.DepotAddress);
      
        Database.UpdateParameter(dbCommand,"?DepotLatitude",technician.DepotLatitude);
      
        Database.UpdateParameter(dbCommand,"?DepotLongitude",technician.DepotLongitude);
      
        Database.UpdateParameter(dbCommand,"?DriveTimeMinutes",technician.DriveTimeMinutes);
      
        Database.UpdateParameter(dbCommand,"?IsContractor",technician.IsContractor);
      
        Database.UpdateParameter(dbCommand,"?MaxVisitsCount",technician.MaxVisitsCount);
      
        Database.UpdateParameter(dbCommand,"?MaxNonExclusiveVisitsCount",technician.MaxNonExclusiveVisitsCount);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        technician.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Technician>  technicianList)
      {
        Insert(technicianList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Technician Set "
      
        + " TechnicianDefaultId = ?TechnicianDefaultId, "
      
        + " ScheduleDate = ?ScheduleDate, "
      
        + " ServmanId = ?ServmanId, "
      
        + " Name = ?Name, "
      
        + " HourlyRate = ?HourlyRate, "
      
        + " HourlyRate150to300 = ?HourlyRate150to300, "
      
        + " HourlyRateMore300 = ?HourlyRateMore300, "
      
        + " DisplaySequence = ?DisplaySequence, "
      
        + " CompanyId = ?CompanyId, "
      
        + " DepotAddress = ?DepotAddress, "
      
        + " DepotLatitude = ?DepotLatitude, "
      
        + " DepotLongitude = ?DepotLongitude, "
      
        + " DriveTimeMinutes = ?DriveTimeMinutes, "
      
        + " IsContractor = ?IsContractor, "
      
        + " MaxVisitsCount = ?MaxVisitsCount, "
      
        + " MaxNonExclusiveVisitsCount = ?MaxNonExclusiveVisitsCount "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Technician technician, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", technician.ID);
      
        Database.PutParameter(dbCommand,"?TechnicianDefaultId", technician.TechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?ScheduleDate", technician.ScheduleDate);
      
        Database.PutParameter(dbCommand,"?ServmanId", technician.ServmanId);
      
        Database.PutParameter(dbCommand,"?Name", technician.Name);
      
        Database.PutParameter(dbCommand,"?HourlyRate", technician.HourlyRate);
      
        Database.PutParameter(dbCommand,"?HourlyRate150to300", technician.HourlyRate150to300);
      
        Database.PutParameter(dbCommand,"?HourlyRateMore300", technician.HourlyRateMore300);
      
        Database.PutParameter(dbCommand,"?DisplaySequence", technician.DisplaySequence);
      
        Database.PutParameter(dbCommand,"?CompanyId", technician.CompanyId);
      
        Database.PutParameter(dbCommand,"?DepotAddress", technician.DepotAddress);
      
        Database.PutParameter(dbCommand,"?DepotLatitude", technician.DepotLatitude);
      
        Database.PutParameter(dbCommand,"?DepotLongitude", technician.DepotLongitude);
      
        Database.PutParameter(dbCommand,"?DriveTimeMinutes", technician.DriveTimeMinutes);
      
        Database.PutParameter(dbCommand,"?IsContractor", technician.IsContractor);
      
        Database.PutParameter(dbCommand,"?MaxVisitsCount", technician.MaxVisitsCount);
      
        Database.PutParameter(dbCommand,"?MaxNonExclusiveVisitsCount", technician.MaxNonExclusiveVisitsCount);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Technician technician)
      {
        Update(technician, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " TechnicianDefaultId, "
      
        + " ScheduleDate, "
      
        + " ServmanId, "
      
        + " Name, "
      
        + " HourlyRate, "
      
        + " HourlyRate150to300, "
      
        + " HourlyRateMore300, "
      
        + " DisplaySequence, "
      
        + " CompanyId, "
      
        + " DepotAddress, "
      
        + " DepotLatitude, "
      
        + " DepotLongitude, "
      
        + " DriveTimeMinutes, "
      
        + " IsContractor, "
      
        + " MaxVisitsCount, "
      
        + " MaxNonExclusiveVisitsCount "
      

      + " From Technician "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Technician FindByPrimaryKey(
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
      throw new DataNotFoundException("Technician not found, search by primary key");

      }

      public static Technician FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Technician technician, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",technician.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Technician technician)
      {
      return Exists(technician, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Technician limit 1";

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

      public static Technician Load(IDataReader dataReader, int offset)
      {
      Technician technician = new Technician();

      technician.ID = dataReader.GetInt32(0 + offset);
          technician.TechnicianDefaultId = dataReader.GetInt32(1 + offset);
          technician.ScheduleDate = dataReader.GetDateTime(2 + offset);
          technician.ServmanId = dataReader.GetString(3 + offset);
          technician.Name = dataReader.GetString(4 + offset);
          technician.HourlyRate = dataReader.GetDecimal(5 + offset);
          technician.HourlyRate150to300 = dataReader.GetDecimal(6 + offset);
          technician.HourlyRateMore300 = dataReader.GetDecimal(7 + offset);
          technician.DisplaySequence = dataReader.GetInt32(8 + offset);
          technician.CompanyId = dataReader.GetInt32(9 + offset);
          technician.DepotAddress = dataReader.GetString(10 + offset);
          technician.DepotLatitude = dataReader.GetFloat(11 + offset);
          technician.DepotLongitude = dataReader.GetFloat(12 + offset);
          technician.DriveTimeMinutes = dataReader.GetInt32(13 + offset);
          technician.IsContractor = dataReader.GetBoolean(14 + offset);
          technician.MaxVisitsCount = dataReader.GetInt32(15 + offset);
          technician.MaxNonExclusiveVisitsCount = dataReader.GetInt32(16 + offset);
          

      return technician;
      }

      public static Technician Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Technician "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Technician technician, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", technician.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Technician technician)
      {
        Delete(technician, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Technician ";

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
      
        + " TechnicianDefaultId, "
      
        + " ScheduleDate, "
      
        + " ServmanId, "
      
        + " Name, "
      
        + " HourlyRate, "
      
        + " HourlyRate150to300, "
      
        + " HourlyRateMore300, "
      
        + " DisplaySequence, "
      
        + " CompanyId, "
      
        + " DepotAddress, "
      
        + " DepotLatitude, "
      
        + " DepotLongitude, "
      
        + " DriveTimeMinutes, "
      
        + " IsContractor, "
      
        + " MaxVisitsCount, "
      
        + " MaxNonExclusiveVisitsCount "
      

      + " From Technician ";
      public static List<Technician> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Technician> rv = new List<Technician>();

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

      public static List<Technician> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Technician> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Technician> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Technician));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Technician item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Technician>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Technician));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Technician> itemsList
      = new List<Technician>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Technician)
      itemsList.Add(deserializedObject as Technician);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_technicianDefaultId;
      
        protected DateTime m_scheduleDate;
      
        protected String m_servmanId;
      
        protected String m_name;
      
        protected decimal m_hourlyRate;
      
        protected decimal m_hourlyRate150to300;
      
        protected decimal m_hourlyRateMore300;
      
        protected int m_displaySequence;
      
        protected int m_companyId;
      
        protected String m_depotAddress;
      
        protected float m_depotLatitude;
      
        protected float m_depotLongitude;
      
        protected int m_driveTimeMinutes;
      
        protected bool m_isContractor;
      
        protected int m_maxVisitsCount;
      
        protected int m_maxNonExclusiveVisitsCount;
      
      #endregion

      #region Constructors
      public Technician(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Technician(
        int 
          iD,int 
          technicianDefaultId,DateTime 
          scheduleDate,String 
          servmanId,String 
          name,decimal 
          hourlyRate,decimal 
          hourlyRate150to300,decimal 
          hourlyRateMore300,int 
          displaySequence,int 
          companyId,String 
          depotAddress,float 
          depotLatitude,float 
          depotLongitude,int 
          driveTimeMinutes,bool 
          isContractor,int 
          maxVisitsCount,int 
          maxNonExclusiveVisitsCount
        ) : this()
        {
        
          m_iD = iD;
        
          m_technicianDefaultId = technicianDefaultId;
        
          m_scheduleDate = scheduleDate;
        
          m_servmanId = servmanId;
        
          m_name = name;
        
          m_hourlyRate = hourlyRate;
        
          m_hourlyRate150to300 = hourlyRate150to300;
        
          m_hourlyRateMore300 = hourlyRateMore300;
        
          m_displaySequence = displaySequence;
        
          m_companyId = companyId;
        
          m_depotAddress = depotAddress;
        
          m_depotLatitude = depotLatitude;
        
          m_depotLongitude = depotLongitude;
        
          m_driveTimeMinutes = driveTimeMinutes;
        
          m_isContractor = isContractor;
        
          m_maxVisitsCount = maxVisitsCount;
        
          m_maxNonExclusiveVisitsCount = maxNonExclusiveVisitsCount;
        
        }


      
      #endregion

      
        [DataMember]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [DataMember]
        public int TechnicianDefaultId
        {
        get { return m_technicianDefaultId;}
        set { m_technicianDefaultId = value; }
        }
      
        [DataMember]
        public DateTime ScheduleDate
        {
        get { return m_scheduleDate;}
        set { m_scheduleDate = value; }
        }
      
        [DataMember]
        public String ServmanId
        {
        get { return m_servmanId;}
        set { m_servmanId = value; }
        }
      
        [DataMember]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [DataMember]
        public decimal HourlyRate
        {
        get { return m_hourlyRate;}
        set { m_hourlyRate = value; }
        }
      
        [DataMember]
        public decimal HourlyRate150to300
        {
        get { return m_hourlyRate150to300;}
        set { m_hourlyRate150to300 = value; }
        }
      
        [DataMember]
        public decimal HourlyRateMore300
        {
        get { return m_hourlyRateMore300;}
        set { m_hourlyRateMore300 = value; }
        }
      
        [DataMember]
        public int DisplaySequence
        {
        get { return m_displaySequence;}
        set { m_displaySequence = value; }
        }
      
        [DataMember]
        public int CompanyId
        {
        get { return m_companyId;}
        set { m_companyId = value; }
        }
      
        [DataMember]
        public String DepotAddress
        {
        get { return m_depotAddress;}
        set { m_depotAddress = value; }
        }
      
        [DataMember]
        public float DepotLatitude
        {
        get { return m_depotLatitude;}
        set { m_depotLatitude = value; }
        }
      
        [DataMember]
        public float DepotLongitude
        {
        get { return m_depotLongitude;}
        set { m_depotLongitude = value; }
        }
      
        [DataMember]
        public int DriveTimeMinutes
        {
        get { return m_driveTimeMinutes;}
        set { m_driveTimeMinutes = value; }
        }
      
        [DataMember]
        public bool IsContractor
        {
        get { return m_isContractor;}
        set { m_isContractor = value; }
        }
      
        [DataMember]
        public int MaxVisitsCount
        {
        get { return m_maxVisitsCount;}
        set { m_maxVisitsCount = value; }
        }
      
        [DataMember]
        public int MaxNonExclusiveVisitsCount
        {
        get { return m_maxNonExclusiveVisitsCount;}
        set { m_maxNonExclusiveVisitsCount = value; }
        }
      

      public static int FieldsCount
      {
      get { return 17; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    