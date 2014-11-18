
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
      public partial class TechnicianZip : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TechnicianZip ( " +
      
        " TechnicianId, " +
      
        " Zip, " +
      
        " IsPrimaryZip " +
      
      ") Values (" +
      
        " ?TechnicianId, " +
      
        " ?Zip, " +
      
        " ?IsPrimaryZip " +
      
      ")";

      public static void Insert(TechnicianZip technicianZip, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianZip.TechnicianId);
      
        Database.PutParameter(dbCommand,"?Zip", technicianZip.Zip);
      
        Database.PutParameter(dbCommand,"?IsPrimaryZip", technicianZip.IsPrimaryZip);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(TechnicianZip technicianZip)
      {
        Insert(technicianZip, null);
      }


      public static void Insert(List<TechnicianZip>  technicianZipList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TechnicianZip technicianZip in  technicianZipList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianZip.TechnicianId);
      
        Database.PutParameter(dbCommand,"?Zip", technicianZip.Zip);
      
        Database.PutParameter(dbCommand,"?IsPrimaryZip", technicianZip.IsPrimaryZip);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TechnicianId",technicianZip.TechnicianId);
      
        Database.UpdateParameter(dbCommand,"?Zip",technicianZip.Zip);
      
        Database.UpdateParameter(dbCommand,"?IsPrimaryZip",technicianZip.IsPrimaryZip);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<TechnicianZip>  technicianZipList)
      {
        Insert(technicianZipList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TechnicianZip Set "
      
        + " IsPrimaryZip = ?IsPrimaryZip "
      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " Zip = ?Zip "
        
      ;

      public static void Update(TechnicianZip technicianZip, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianZip.TechnicianId);
      
        Database.PutParameter(dbCommand,"?Zip", technicianZip.Zip);
      
        Database.PutParameter(dbCommand,"?IsPrimaryZip", technicianZip.IsPrimaryZip);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TechnicianZip technicianZip)
      {
        Update(technicianZip, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TechnicianId, "
      
        + " Zip, "
      
        + " IsPrimaryZip "
      

      + " From TechnicianZip "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " Zip = ?Zip "
        
      ;

      public static TechnicianZip FindByPrimaryKey(
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
      throw new DataNotFoundException("TechnicianZip not found, search by primary key");

      }

      public static TechnicianZip FindByPrimaryKey(
      int technicianId,String zip
      )
      {
      return FindByPrimaryKey(
      technicianId,zip, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TechnicianZip technicianZip, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId",technicianZip.TechnicianId);
      
        Database.PutParameter(dbCommand,"?Zip",technicianZip.Zip);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TechnicianZip technicianZip)
      {
      return Exists(technicianZip, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TechnicianZip limit 1";

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

      public static TechnicianZip Load(IDataReader dataReader, int offset)
      {
      TechnicianZip technicianZip = new TechnicianZip();

      technicianZip.TechnicianId = dataReader.GetInt32(0 + offset);
          technicianZip.Zip = dataReader.GetString(1 + offset);
          technicianZip.IsPrimaryZip = dataReader.GetBoolean(2 + offset);
          

      return technicianZip;
      }

      public static TechnicianZip Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TechnicianZip "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " Zip = ?Zip "
        
      ;
      public static void Delete(TechnicianZip technicianZip, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianZip.TechnicianId);
      
        Database.PutParameter(dbCommand,"?Zip", technicianZip.Zip);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TechnicianZip technicianZip)
      {
        Delete(technicianZip, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TechnicianZip ";

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
      

      + " From TechnicianZip ";
      public static List<TechnicianZip> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TechnicianZip> rv = new List<TechnicianZip>();

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

      public static List<TechnicianZip> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TechnicianZip> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TechnicianZip> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianZip));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TechnicianZip item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TechnicianZip>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianZip));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TechnicianZip> itemsList
      = new List<TechnicianZip>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TechnicianZip)
      itemsList.Add(deserializedObject as TechnicianZip);
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
      public TechnicianZip(
      int 
          technicianId,String 
          zip
      ) : this()
      {
      
        m_technicianId = technicianId;
      
        m_zip = zip;
      
      }

      


        public TechnicianZip(
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

    