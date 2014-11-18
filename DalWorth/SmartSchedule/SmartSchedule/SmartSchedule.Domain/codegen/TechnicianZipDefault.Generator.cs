
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
      public partial class TechnicianZipDefault : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TechnicianZipDefault ( " +
      
        " TechnicianId, " +
      
        " Zip, " +
      
        " IsPrimaryZip " +
      
      ") Values (" +
      
        " ?TechnicianId, " +
      
        " ?Zip, " +
      
        " ?IsPrimaryZip " +
      
      ")";

      public static void Insert(TechnicianZipDefault technicianZipDefault, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianZipDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?Zip", technicianZipDefault.Zip);
      
        Database.PutParameter(dbCommand,"?IsPrimaryZip", technicianZipDefault.IsPrimaryZip);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(TechnicianZipDefault technicianZipDefault)
      {
        Insert(technicianZipDefault, null);
      }


      public static void Insert(List<TechnicianZipDefault>  technicianZipDefaultList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TechnicianZipDefault technicianZipDefault in  technicianZipDefaultList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianZipDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?Zip", technicianZipDefault.Zip);
      
        Database.PutParameter(dbCommand,"?IsPrimaryZip", technicianZipDefault.IsPrimaryZip);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TechnicianId",technicianZipDefault.TechnicianId);
      
        Database.UpdateParameter(dbCommand,"?Zip",technicianZipDefault.Zip);
      
        Database.UpdateParameter(dbCommand,"?IsPrimaryZip",technicianZipDefault.IsPrimaryZip);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<TechnicianZipDefault>  technicianZipDefaultList)
      {
        Insert(technicianZipDefaultList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TechnicianZipDefault Set "
      
        + " IsPrimaryZip = ?IsPrimaryZip "
      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " Zip = ?Zip "
        
      ;

      public static void Update(TechnicianZipDefault technicianZipDefault, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianZipDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?Zip", technicianZipDefault.Zip);
      
        Database.PutParameter(dbCommand,"?IsPrimaryZip", technicianZipDefault.IsPrimaryZip);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TechnicianZipDefault technicianZipDefault)
      {
        Update(technicianZipDefault, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TechnicianId, "
      
        + " Zip, "
      
        + " IsPrimaryZip "
      

      + " From TechnicianZipDefault "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " Zip = ?Zip "
        
      ;

      public static TechnicianZipDefault FindByPrimaryKey(
      int technicianId,String zip, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianId);
      
        Database.PutParameter(dbCommand,"?Zip", zip);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("TechnicianZipDefault not found, search by primary key");

      }

      public static TechnicianZipDefault FindByPrimaryKey(
      int technicianId,String zip
      )
      {
      return FindByPrimaryKey(
      technicianId,zip, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TechnicianZipDefault technicianZipDefault, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId",technicianZipDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?Zip",technicianZipDefault.Zip);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TechnicianZipDefault technicianZipDefault)
      {
      return Exists(technicianZipDefault, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TechnicianZipDefault limit 1";

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

      public static TechnicianZipDefault Load(IDataReader dataReader, int offset)
      {
      TechnicianZipDefault technicianZipDefault = new TechnicianZipDefault();

      technicianZipDefault.TechnicianId = dataReader.GetInt32(0 + offset);
          technicianZipDefault.Zip = dataReader.GetString(1 + offset);
          technicianZipDefault.IsPrimaryZip = dataReader.GetBoolean(2 + offset);
          

      return technicianZipDefault;
      }

      public static TechnicianZipDefault Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TechnicianZipDefault "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " Zip = ?Zip "
        
      ;
      public static void Delete(TechnicianZipDefault technicianZipDefault, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianZipDefault.TechnicianId);
      
        Database.PutParameter(dbCommand,"?Zip", technicianZipDefault.Zip);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TechnicianZipDefault technicianZipDefault)
      {
        Delete(technicianZipDefault, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TechnicianZipDefault ";

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
      
        + " Zip, "
      
        + " IsPrimaryZip "
      

      + " From TechnicianZipDefault ";
      public static List<TechnicianZipDefault> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TechnicianZipDefault> rv = new List<TechnicianZipDefault>();

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

      public static List<TechnicianZipDefault> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TechnicianZipDefault> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TechnicianZipDefault> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianZipDefault));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TechnicianZipDefault item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TechnicianZipDefault>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianZipDefault));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TechnicianZipDefault> itemsList
      = new List<TechnicianZipDefault>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TechnicianZipDefault)
      itemsList.Add(deserializedObject as TechnicianZipDefault);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_technicianId;
      
        protected String m_zip;
      
        protected bool m_isPrimaryZip;
      
      #endregion

      #region Constructors
      public TechnicianZipDefault(
      int 
          technicianId,String 
          zip
      ) : this()
      {
      
        m_technicianId = technicianId;
      
        m_zip = zip;
      
      }

      


        public TechnicianZipDefault(
        int 
          technicianId,String 
          zip,bool 
          isPrimaryZip
        ) : this()
        {
        
          m_technicianId = technicianId;
        
          m_zip = zip;
        
          m_isPrimaryZip = isPrimaryZip;
        
        }


      
      #endregion

      
        [DataMember]
        public int TechnicianId
        {
        get { return m_technicianId;}
        set { m_technicianId = value; }
        }
      
        [DataMember]
        public String Zip
        {
        get { return m_zip;}
        set { m_zip = value; }
        }
      
        [DataMember]
        public bool IsPrimaryZip
        {
        get { return m_isPrimaryZip;}
        set { m_isPrimaryZip = value; }
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

    