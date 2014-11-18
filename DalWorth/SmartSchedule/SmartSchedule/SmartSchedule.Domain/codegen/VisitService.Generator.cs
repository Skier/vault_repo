
    using System;
    using System.Data;
    using System.Collections.Generic;
    using SmartSchedule.Data;
    using SmartSchedule.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace SmartSchedule.Domain
      {


      public partial class VisitService : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into VisitService ( " +
      
        " VisitId, " +
      
        " ServiceId " +
      
      ") Values (" +
      
        " ?VisitId, " +
      
        " ?ServiceId " +
      
      ")";

      public static void Insert(VisitService visitService, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?VisitId", visitService.VisitId);
      
        Database.PutParameter(dbCommand,"?ServiceId", visitService.ServiceId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(VisitService visitService)
      {
        Insert(visitService, null);
      }


      public static void Insert(List<VisitService>  visitServiceList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(VisitService visitService in  visitServiceList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?VisitId", visitService.VisitId);
      
        Database.PutParameter(dbCommand,"?ServiceId", visitService.ServiceId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?VisitId",visitService.VisitId);
      
        Database.UpdateParameter(dbCommand,"?ServiceId",visitService.ServiceId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<VisitService>  visitServiceList)
      {
        Insert(visitServiceList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update VisitService Set "
      
        + " Where "
        
          + " VisitId = ?VisitId and  "
        
          + " ServiceId = ?ServiceId "
        
      ;

      public static void Update(VisitService visitService, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?VisitId", visitService.VisitId);
      
        Database.PutParameter(dbCommand,"?ServiceId", visitService.ServiceId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(VisitService visitService)
      {
        Update(visitService, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " VisitId, "
      
        + " ServiceId "
      

      + " From VisitService "

      
        + " Where "
        
          + " VisitId = ?VisitId and  "
        
          + " ServiceId = ?ServiceId "
        
      ;

      public static VisitService FindByPrimaryKey(
      int visitId,int serviceId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?VisitId", visitId);
      
        Database.PutParameter(dbCommand,"?ServiceId", serviceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("VisitService not found, search by primary key");

      }

      public static VisitService FindByPrimaryKey(
      int visitId,int serviceId
      )
      {
      return FindByPrimaryKey(
      visitId,serviceId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(VisitService visitService, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?VisitId",visitService.VisitId);
      
        Database.PutParameter(dbCommand,"?ServiceId",visitService.ServiceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(VisitService visitService)
      {
      return Exists(visitService, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from VisitService limit 1";

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

      public static VisitService Load(IDataReader dataReader, int offset)
      {
      VisitService visitService = new VisitService();

      visitService.VisitId = dataReader.GetInt32(0 + offset);
          visitService.ServiceId = dataReader.GetInt32(1 + offset);
          

      return visitService;
      }

      public static VisitService Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From VisitService "

      
        + " Where "
        
          + " VisitId = ?VisitId and  "
        
          + " ServiceId = ?ServiceId "
        
      ;
      public static void Delete(VisitService visitService, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?VisitId", visitService.VisitId);
      
        Database.PutParameter(dbCommand,"?ServiceId", visitService.ServiceId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(VisitService visitService)
      {
        Delete(visitService, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From VisitService ";

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

      
        + " VisitId, "
      
        + " ServiceId "
      

      + " From VisitService ";
      public static List<VisitService> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<VisitService> rv = new List<VisitService>();

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

      public static List<VisitService> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<VisitService> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<VisitService> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(VisitService));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(VisitService item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<VisitService>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(VisitService));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<VisitService> itemsList
      = new List<VisitService>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is VisitService)
      itemsList.Add(deserializedObject as VisitService);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_visitId;
      
        protected int m_serviceId;
      
      #endregion

      #region Constructors
      public VisitService(
      int 
          visitId,int 
          serviceId
      ) : this()
      {
      
        m_visitId = visitId;
      
        m_serviceId = serviceId;
      
      }

      
      #endregion

      
        [XmlElement]
        public int VisitId
        {
        get { return m_visitId;}
        set { m_visitId = value; }
        }
      
        [XmlElement]
        public int ServiceId
        {
        get { return m_serviceId;}
        set { m_serviceId = value; }
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

    