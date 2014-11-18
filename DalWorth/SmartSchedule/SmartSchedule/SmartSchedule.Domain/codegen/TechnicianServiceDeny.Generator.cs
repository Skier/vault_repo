
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
      public partial class TechnicianServiceDeny : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TechnicianServiceDeny ( " +
      
        " TechnicianId, " +
      
        " ServiceId, " +
      
        " IsForNonExclusive " +
      
      ") Values (" +
      
        " ?TechnicianId, " +
      
        " ?ServiceId, " +
      
        " ?IsForNonExclusive " +
      
      ")";

      public static void Insert(TechnicianServiceDeny technicianServiceDeny, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianServiceDeny.TechnicianId);
      
        Database.PutParameter(dbCommand,"?ServiceId", technicianServiceDeny.ServiceId);
      
        Database.PutParameter(dbCommand,"?IsForNonExclusive", technicianServiceDeny.IsForNonExclusive);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(TechnicianServiceDeny technicianServiceDeny)
      {
        Insert(technicianServiceDeny, null);
      }


      public static void Insert(List<TechnicianServiceDeny>  technicianServiceDenyList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TechnicianServiceDeny technicianServiceDeny in  technicianServiceDenyList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianServiceDeny.TechnicianId);
      
        Database.PutParameter(dbCommand,"?ServiceId", technicianServiceDeny.ServiceId);
      
        Database.PutParameter(dbCommand,"?IsForNonExclusive", technicianServiceDeny.IsForNonExclusive);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TechnicianId",technicianServiceDeny.TechnicianId);
      
        Database.UpdateParameter(dbCommand,"?ServiceId",technicianServiceDeny.ServiceId);
      
        Database.UpdateParameter(dbCommand,"?IsForNonExclusive",technicianServiceDeny.IsForNonExclusive);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<TechnicianServiceDeny>  technicianServiceDenyList)
      {
        Insert(technicianServiceDenyList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TechnicianServiceDeny Set "
      
        + " IsForNonExclusive = ?IsForNonExclusive "
      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " ServiceId = ?ServiceId "
        
      ;

      public static void Update(TechnicianServiceDeny technicianServiceDeny, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianServiceDeny.TechnicianId);
      
        Database.PutParameter(dbCommand,"?ServiceId", technicianServiceDeny.ServiceId);
      
        Database.PutParameter(dbCommand,"?IsForNonExclusive", technicianServiceDeny.IsForNonExclusive);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TechnicianServiceDeny technicianServiceDeny)
      {
        Update(technicianServiceDeny, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TechnicianId, "
      
        + " ServiceId, "
      
        + " IsForNonExclusive "
      

      + " From TechnicianServiceDeny "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " ServiceId = ?ServiceId "
        
      ;

      public static TechnicianServiceDeny FindByPrimaryKey(
      int technicianId,int serviceId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianId);
      
        Database.PutParameter(dbCommand,"?ServiceId", serviceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("TechnicianServiceDeny not found, search by primary key");

      }

      public static TechnicianServiceDeny FindByPrimaryKey(
      int technicianId,int serviceId
      )
      {
      return FindByPrimaryKey(
      technicianId,serviceId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TechnicianServiceDeny technicianServiceDeny, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TechnicianId",technicianServiceDeny.TechnicianId);
      
        Database.PutParameter(dbCommand,"?ServiceId",technicianServiceDeny.ServiceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TechnicianServiceDeny technicianServiceDeny)
      {
      return Exists(technicianServiceDeny, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TechnicianServiceDeny limit 1";

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

      public static TechnicianServiceDeny Load(IDataReader dataReader, int offset)
      {
      TechnicianServiceDeny technicianServiceDeny = new TechnicianServiceDeny();

      technicianServiceDeny.TechnicianId = dataReader.GetInt32(0 + offset);
          technicianServiceDeny.ServiceId = dataReader.GetInt32(1 + offset);
          technicianServiceDeny.IsForNonExclusive = dataReader.GetBoolean(2 + offset);
          

      return technicianServiceDeny;
      }

      public static TechnicianServiceDeny Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TechnicianServiceDeny "

      
        + " Where "
        
          + " TechnicianId = ?TechnicianId and  "
        
          + " ServiceId = ?ServiceId "
        
      ;
      public static void Delete(TechnicianServiceDeny technicianServiceDeny, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TechnicianId", technicianServiceDeny.TechnicianId);
      
        Database.PutParameter(dbCommand,"?ServiceId", technicianServiceDeny.ServiceId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TechnicianServiceDeny technicianServiceDeny)
      {
        Delete(technicianServiceDeny, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TechnicianServiceDeny ";

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
      
        + " ServiceId, "
      
        + " IsForNonExclusive "
      

      + " From TechnicianServiceDeny ";
      public static List<TechnicianServiceDeny> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TechnicianServiceDeny> rv = new List<TechnicianServiceDeny>();

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

      public static List<TechnicianServiceDeny> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TechnicianServiceDeny> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TechnicianServiceDeny> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianServiceDeny));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TechnicianServiceDeny item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TechnicianServiceDeny>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TechnicianServiceDeny));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TechnicianServiceDeny> itemsList
      = new List<TechnicianServiceDeny>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TechnicianServiceDeny)
      itemsList.Add(deserializedObject as TechnicianServiceDeny);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_technicianId;
      
        protected int m_serviceId;
      
        protected bool m_isForNonExclusive;
      
      #endregion

      #region Constructors
      public TechnicianServiceDeny(
      int 
          technicianId,int 
          serviceId
      ) : this()
      {
      
        m_technicianId = technicianId;
      
        m_serviceId = serviceId;
      
      }

      


        public TechnicianServiceDeny(
        int 
          technicianId,int 
          serviceId,bool 
          isForNonExclusive
        ) : this()
        {
        
          m_technicianId = technicianId;
        
          m_serviceId = serviceId;
        
          m_isForNonExclusive = isForNonExclusive;
        
        }


      
      #endregion

      
        [DataMember]
        public int TechnicianId
        {
        get { return m_technicianId;}
        set { m_technicianId = value; }
        }
      
        [DataMember]
        public int ServiceId
        {
        get { return m_serviceId;}
        set { m_serviceId = value; }
        }
      
        [DataMember]
        public bool IsForNonExclusive
        {
        get { return m_isForNonExclusive;}
        set { m_isForNonExclusive = value; }
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

    