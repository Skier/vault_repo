
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
      public partial class TechnicianDefault : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TechnicianDefault ( " +
      
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
      
        " MaxNonExclusiveVisitsCount, " +
      
        " Email " +
      
      ") Values (" +
      
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
      
        " ?MaxNonExclusiveVisitsCount, " +
      
        " ?Email " +
      
      ")";

      public static void Insert(TechnicianDefault technicianDefault, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ServmanId", technicianDefault.ServmanId);
      
        Database.PutParameter(dbCommand,"?Name", technicianDefault.Name);
      
        Database.PutParameter(dbCommand,"?HourlyRate", technicianDefault.HourlyRate);
      
        Database.PutParameter(dbCommand,"?HourlyRate150to300", technicianDefault.HourlyRate150to300);
      
        Database.PutParameter(dbCommand,"?HourlyRateMore300", technicianDefault.HourlyRateMore300);
      
        Database.PutParameter(dbCommand,"?DisplaySequence", technicianDefault.DisplaySequence);
      
        Database.PutParameter(dbCommand,"?CompanyId", technicianDefault.CompanyId);
      
        Database.PutParameter(dbCommand,"?DepotAddress", technicianDefault.DepotAddress);
      
        Database.PutParameter(dbCommand,"?DepotLatitude", technicianDefault.DepotLatitude);
      
        Database.PutParameter(dbCommand,"?DepotLongitude", technicianDefault.DepotLongitude);
      
        Database.PutParameter(dbCommand,"?DriveTimeMinutes", technicianDefault.DriveTimeMinutes);
      
        Database.PutParameter(dbCommand,"?IsContractor", technicianDefault.IsContractor);
      
        Database.PutParameter(dbCommand,"?MaxVisitsCount", technicianDefault.MaxVisitsCount);
      
        Database.PutParameter(dbCommand,"?MaxNonExclusiveVisitsCount", technicianDefault.MaxNonExclusiveVisitsCount);
      
        Database.PutParameter(dbCommand,"?Email", technicianDefault.Email);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        technicianDefault.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(TechnicianDefault technicianDefault)
      {
        Insert(technicianDefault, null);
      }


      public static void Insert(List<TechnicianDefault>  technicianDefaultList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TechnicianDefault technicianDefault in  technicianDefaultList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ServmanId", technicianDefault.ServmanId);
      
        Database.PutParameter(dbCommand,"?Name", technicianDefault.Name);
      
        Database.PutParameter(dbCommand,"?HourlyRate", technicianDefault.HourlyRate);
      
        Database.PutParameter(dbCommand,"?HourlyRate150to300", technicianDefault.HourlyRate150to300);
      
        Database.PutParameter(dbCommand,"?HourlyRateMore300", technicianDefault.HourlyRateMore300);
      
        Database.PutParameter(dbCommand,"?DisplaySequence", technicianDefault.DisplaySequence);
      
        Database.PutParameter(dbCommand,"?CompanyId", technicianDefault.CompanyId);
      
        Database.PutParameter(dbCommand,"?DepotAddress", technicianDefault.DepotAddress);
      
        Database.PutParameter(dbCommand,"?DepotLatitude", technicianDefault.DepotLatitude);
      
        Database.PutParameter(dbCommand,"?DepotLongitude", technicianDefault.DepotLongitude);
      
        Database.PutParameter(dbCommand,"?DriveTimeMinutes", technicianDefault.DriveTimeMinutes);
      
        Database.PutParameter(dbCommand,"?IsContractor", technicianDefault.IsContractor);
      
        Database.PutParameter(dbCommand,"?MaxVisitsCount", technicianDefault.MaxVisitsCount);
      
        Database.PutParameter(dbCommand,"?MaxNonExclusiveVisitsCount", technicianDefault.MaxNonExclusiveVisitsCount);
      
        Database.PutParameter(dbCommand,"?Email", technicianDefault.Email);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ServmanId",technicianDefault.ServmanId);
      
        Database.UpdateParameter(dbCommand,"?Name",technicianDefault.Name);
      
        Database.UpdateParameter(dbCommand,"?HourlyRate",technicianDefault.HourlyRate);
      
        Database.UpdateParameter(dbCommand,"?HourlyRate150to300",technicianDefault.HourlyRate150to300);
      
        Database.UpdateParameter(dbCommand,"?HourlyRateMore300",technicianDefault.HourlyRateMore300);
      
        Database.UpdateParameter(dbCommand,"?DisplaySequence",technicianDefault.DisplaySequence);
      
        Database.UpdateParameter(dbCommand,"?CompanyId",technicianDefault.CompanyId);
      
        Database.UpdateParameter(dbCommand,"?DepotAddress",technicianDefault.DepotAddress);
      
        Database.UpdateParameter(dbCommand,"?DepotLatitude",technicianDefault.DepotLatitude);
      
        Database.UpdateParameter(dbCommand,"?DepotLongitude",technicianDefault.DepotLongitude);
      
        Database.UpdateParameter(dbCommand,"?DriveTimeMinutes",technicianDefault.DriveTimeMinutes);
      
        Database.UpdateParameter(dbCommand,"?IsContractor",technicianDefault.IsContractor);
      
        Database.UpdateParameter(dbCommand,"?MaxVisitsCount",technicianDefault.MaxVisitsCount);
      
        Database.UpdateParameter(dbCommand,"?MaxNonExclusiveVisitsCount",technicianDefault.MaxNonExclusiveVisitsCount);
      
        Database.UpdateParameter(dbCommand,"?Email",technicianDefault.Email);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        technicianDefault.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<TechnicianDefault>  technicianDefaultList)
      {
        Insert(technicianDefaultList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TechnicianDefault Set "
      
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
      
        + " MaxNonExclusiveVisitsCount = ?MaxNonExclusiveVisitsCount, "
      
        + " Email = ?Email "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(TechnicianDefault technicianDefault, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", technicianDefault.ID);
      
        Database.PutParameter(dbCommand,"?ServmanId", technicianDefault.ServmanId);
      
        Database.PutParameter(dbCommand,"?Name", technicianDefault.Name);
      
        Database.PutParameter(dbCommand,"?HourlyRate", technicianDefault.HourlyRate);
      
        Database.PutParameter(dbCommand,"?HourlyRate150to300", technicianDefault.HourlyRate150to300);
      
        Database.PutParameter(dbCommand,"?HourlyRateMore300", technicianDefault.HourlyRateMore300);
      
        Database.PutParameter(dbCommand,"?DisplaySequence", technicianDefault.DisplaySequence);
      
        Database.PutParameter(dbCommand,"?CompanyId", technicianDefault.CompanyId);
      
        Database.PutParameter(dbCommand,"?DepotAddress", technicianDefault.DepotAddress);
      
        Database.PutParameter(dbCommand,"?DepotLatitude", technicianDefault.DepotLatitude);
      
        Database.PutParameter(dbCommand,"?DepotLongitude", technicianDefault.DepotLongitude);
      
        Database.PutParameter(dbCommand,"?DriveTimeMinutes", technicianDefault.DriveTimeMinutes);
      
        Database.PutParameter(dbCommand,"?IsContractor", technicianDefault.IsContractor);
      
        Database.PutParameter(dbCommand,"?MaxVisitsCount", technicianDefault.MaxVisitsCount);
      
        Database.PutParameter(dbCommand,"?MaxNonExclusiveVisitsCount", technicianDefault.MaxNonExclusiveVisitsCount);
      
        Database.PutParameter(dbCommand,"?Email", technicianDefault.Email);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TechnicianDefault technicianDefault)
      {
        Update(technicianDefault, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
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
      
        + " MaxNonExclusiveVisitsCount, "
      
        + " Email "
      

      + " From TechnicianDefault "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static TechnicianDefault FindByPrimaryKey(
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
      throw new DataNotFoundException("TechnicianDefault not found, search by primary key");

      }

      public static TechnicianDefault FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TechnicianDefault technicianDefault, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",technicianDefault.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TechnicianDefault technicianDefault)
      {
      return Exists(technicianDefault, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TechnicianDefault limit 1";

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

      public static TechnicianDefault Load(IDataReader dataReader, int offset)
      {
      TechnicianDefault technicianDefault = new TechnicianDefault();

      technicianDefault.ID = dataReader.GetInt32(0 + offset);
          technicianDefault.ServmanId = dataReader.GetString(1 + offset);
          technicianDefault.Name = dataReader.GetString(2 + offset);
          technicianDefault.HourlyRate = dataReader.GetDecimal(3 + offset);
          technicianDefault.HourlyRate150to300 = dataReader.GetDecimal(4 + offset);
          technicianDefault.HourlyRateMore300 = dataReader.GetDecimal(5 + offset);
          technicianDefault.DisplaySequence = dataReader.GetInt32(6 + offset);
          technicianDefault.CompanyId = dataReader.GetInt32(7 + offset);
          technicianDefault.DepotAddress = dataReader.GetString(8 + offset);
          technicianDefault.DepotLatitude = dataReader.GetFloat(9 + offset);
          technicianDefault.DepotLongitude = dataReader.GetFloat(10 + offset);
          technicianDefault.DriveTimeMinutes = dataReader.GetInt32(11 + offset);
          technicianDefault.IsContractor = dataReader.GetBoolean(12 + offset);
          technicianDefault.MaxVisitsCount = dataReader.GetInt32(13 + offset);
          technicianDefault.MaxNonExclusiveVisitsCount = dataReader.GetInt32(14 + offset);
          technicianDefault.Email = dataReader.GetString(15 + offset);
          

      return technicianDefault;
      }

      public static TechnicianDefault Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TechnicianDefault "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(TechnicianDefault technicianDefault, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", technicianDefault.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TechnicianDefault technicianDefault)
      {
        Delete(technicianDefault, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TechnicianDefault ";

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
      
        + " MaxNonExclusiveVisitsCount, "
      
        + " Email "
      

      + " From TechnicianDefault ";
      public static List<TechnicianDefault> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TechnicianDefault> rv = new List<TechnicianDefault>();

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

      public static List<TechnicianDefault> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TechnicianDefault> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TechnicianDefault> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianDefault));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TechnicianDefault item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TechnicianDefault>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianDefault));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TechnicianDefault> itemsList
      = new List<TechnicianDefault>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TechnicianDefault)
      itemsList.Add(deserializedObject as TechnicianDefault);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
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
      
        protected String m_email;
      
      #endregion

      #region Constructors
      public TechnicianDefault(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public TechnicianDefault(
        int 
          iD,String 
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
          maxNonExclusiveVisitsCount,String 
          email
        ) : this()
        {
        
          m_iD = iD;
        
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
        
          m_email = email;
        
        }


      
      #endregion

      
        [DataMember]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
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
      
        [DataMember]
        public String Email
        {
        get { return m_email;}
        set { m_email = value; }
        }
      

      public static int FieldsCount
      {
      get { return 16; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    