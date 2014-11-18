
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
      public partial class ZipCode : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ZipCode ( " +
      
        " Zip, " +
      
        " Latitude, " +
      
        " Longitude, " +
      
        " AreaId " +
      
      ") Values (" +
      
        " ?Zip, " +
      
        " ?Latitude, " +
      
        " ?Longitude, " +
      
        " ?AreaId " +
      
      ")";

      public static void Insert(ZipCode zipCode, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?Zip", zipCode.Zip);
      
        Database.PutParameter(dbCommand,"?Latitude", zipCode.Latitude);
      
        Database.PutParameter(dbCommand,"?Longitude", zipCode.Longitude);
      
        Database.PutParameter(dbCommand,"?AreaId", zipCode.AreaId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ZipCode zipCode)
      {
        Insert(zipCode, null);
      }


      public static void Insert(List<ZipCode>  zipCodeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ZipCode zipCode in  zipCodeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?Zip", zipCode.Zip);
      
        Database.PutParameter(dbCommand,"?Latitude", zipCode.Latitude);
      
        Database.PutParameter(dbCommand,"?Longitude", zipCode.Longitude);
      
        Database.PutParameter(dbCommand,"?AreaId", zipCode.AreaId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?Zip",zipCode.Zip);
      
        Database.UpdateParameter(dbCommand,"?Latitude",zipCode.Latitude);
      
        Database.UpdateParameter(dbCommand,"?Longitude",zipCode.Longitude);
      
        Database.UpdateParameter(dbCommand,"?AreaId",zipCode.AreaId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ZipCode>  zipCodeList)
      {
        Insert(zipCodeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ZipCode Set "
      
        + " Latitude = ?Latitude, "
      
        + " Longitude = ?Longitude, "
      
        + " AreaId = ?AreaId "
      
        + " Where "
        
          + " Zip = ?Zip "
        
      ;

      public static void Update(ZipCode zipCode, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Zip", zipCode.Zip);
      
        Database.PutParameter(dbCommand,"?Latitude", zipCode.Latitude);
      
        Database.PutParameter(dbCommand,"?Longitude", zipCode.Longitude);
      
        Database.PutParameter(dbCommand,"?AreaId", zipCode.AreaId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ZipCode zipCode)
      {
        Update(zipCode, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Zip, "
      
        + " Latitude, "
      
        + " Longitude, "
      
        + " AreaId "
      

      + " From ZipCode "

      
        + " Where "
        
          + " Zip = ?Zip "
        
      ;

      public static ZipCode FindByPrimaryKey(
      String zip, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Zip", zip);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ZipCode not found, search by primary key");

      }

      public static ZipCode FindByPrimaryKey(
      String zip
      )
      {
      return FindByPrimaryKey(
      zip, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ZipCode zipCode, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Zip",zipCode.Zip);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ZipCode zipCode)
      {
      return Exists(zipCode, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ZipCode limit 1";

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

      public static ZipCode Load(IDataReader dataReader, int offset)
      {
      ZipCode zipCode = new ZipCode();

      zipCode.Zip = dataReader.GetString(0 + offset);
          zipCode.Latitude = dataReader.GetFloat(1 + offset);
          zipCode.Longitude = dataReader.GetFloat(2 + offset);
          zipCode.AreaId = dataReader.GetInt32(3 + offset);
          

      return zipCode;
      }

      public static ZipCode Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ZipCode "

      
        + " Where "
        
          + " Zip = ?Zip "
        
      ;
      public static void Delete(ZipCode zipCode, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Zip", zipCode.Zip);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ZipCode zipCode)
      {
        Delete(zipCode, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ZipCode ";

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

      
        + " Zip, "
      
        + " Latitude, "
      
        + " Longitude, "
      
        + " AreaId "
      

      + " From ZipCode ";
      public static List<ZipCode> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ZipCode> rv = new List<ZipCode>();

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

      public static List<ZipCode> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ZipCode> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<ZipCode> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ZipCode));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ZipCode item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ZipCode>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ZipCode));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ZipCode> itemsList
      = new List<ZipCode>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ZipCode)
      itemsList.Add(deserializedObject as ZipCode);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_zip;
      
        protected float m_latitude;
      
        protected float m_longitude;
      
        protected int m_areaId;
      
      #endregion

      #region Constructors
      public ZipCode(
      String 
          zip
      ) : this()
      {
      
        m_zip = zip;
      
      }

      


        public ZipCode(
        String 
          zip,float 
          latitude,float 
          longitude,int 
          areaId
        ) : this()
        {
        
          m_zip = zip;
        
          m_latitude = latitude;
        
          m_longitude = longitude;
        
          m_areaId = areaId;
        
        }


      
      #endregion

      
        [DataMember]
        public String Zip
        {
        get { return m_zip;}
        set { m_zip = value; }
        }
      
        [DataMember]
        public float Latitude
        {
        get { return m_latitude;}
        set { m_latitude = value; }
        }
      
        [DataMember]
        public float Longitude
        {
        get { return m_longitude;}
        set { m_longitude = value; }
        }
      
        [DataMember]
        public int AreaId
        {
        get { return m_areaId;}
        set { m_areaId = value; }
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

    