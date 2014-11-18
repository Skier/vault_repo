
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


      public partial class SyncToolInfo : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into SyncToolInfo ( " +
      
        " ID, " +
      
        " LastCustomerImportDate, " +
      
        " LastImportedTicketNumber, " +
      
        " LastTomorrowPrintJobDate, " +
      
        " LastImportAdSourceDate, " +
      
        " LastImportedTruckId " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?LastCustomerImportDate, " +
      
        " ?LastImportedTicketNumber, " +
      
        " ?LastTomorrowPrintJobDate, " +
      
        " ?LastImportAdSourceDate, " +
      
        " ?LastImportedTruckId " +
      
      ")";

      public static void Insert(SyncToolInfo syncToolInfo, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", syncToolInfo.ID);
      
        Database.PutParameter(dbCommand,"?LastCustomerImportDate", syncToolInfo.LastCustomerImportDate);
      
        Database.PutParameter(dbCommand,"?LastImportedTicketNumber", syncToolInfo.LastImportedTicketNumber);
      
        Database.PutParameter(dbCommand,"?LastTomorrowPrintJobDate", syncToolInfo.LastTomorrowPrintJobDate);
      
        Database.PutParameter(dbCommand,"?LastImportAdSourceDate", syncToolInfo.LastImportAdSourceDate);
      
        Database.PutParameter(dbCommand,"?LastImportedTruckId", syncToolInfo.LastImportedTruckId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(SyncToolInfo syncToolInfo)
      {
        Insert(syncToolInfo, null);
      }


      public static void Insert(List<SyncToolInfo>  syncToolInfoList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(SyncToolInfo syncToolInfo in  syncToolInfoList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", syncToolInfo.ID);
      
        Database.PutParameter(dbCommand,"?LastCustomerImportDate", syncToolInfo.LastCustomerImportDate);
      
        Database.PutParameter(dbCommand,"?LastImportedTicketNumber", syncToolInfo.LastImportedTicketNumber);
      
        Database.PutParameter(dbCommand,"?LastTomorrowPrintJobDate", syncToolInfo.LastTomorrowPrintJobDate);
      
        Database.PutParameter(dbCommand,"?LastImportAdSourceDate", syncToolInfo.LastImportAdSourceDate);
      
        Database.PutParameter(dbCommand,"?LastImportedTruckId", syncToolInfo.LastImportedTruckId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",syncToolInfo.ID);
      
        Database.UpdateParameter(dbCommand,"?LastCustomerImportDate",syncToolInfo.LastCustomerImportDate);
      
        Database.UpdateParameter(dbCommand,"?LastImportedTicketNumber",syncToolInfo.LastImportedTicketNumber);
      
        Database.UpdateParameter(dbCommand,"?LastTomorrowPrintJobDate",syncToolInfo.LastTomorrowPrintJobDate);
      
        Database.UpdateParameter(dbCommand,"?LastImportAdSourceDate",syncToolInfo.LastImportAdSourceDate);
      
        Database.UpdateParameter(dbCommand,"?LastImportedTruckId",syncToolInfo.LastImportedTruckId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<SyncToolInfo>  syncToolInfoList)
      {
        Insert(syncToolInfoList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update SyncToolInfo Set "
      
        + " LastCustomerImportDate = ?LastCustomerImportDate, "
      
        + " LastImportedTicketNumber = ?LastImportedTicketNumber, "
      
        + " LastTomorrowPrintJobDate = ?LastTomorrowPrintJobDate, "
      
        + " LastImportAdSourceDate = ?LastImportAdSourceDate, "
      
        + " LastImportedTruckId = ?LastImportedTruckId "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(SyncToolInfo syncToolInfo, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", syncToolInfo.ID);
      
        Database.PutParameter(dbCommand,"?LastCustomerImportDate", syncToolInfo.LastCustomerImportDate);
      
        Database.PutParameter(dbCommand,"?LastImportedTicketNumber", syncToolInfo.LastImportedTicketNumber);
      
        Database.PutParameter(dbCommand,"?LastTomorrowPrintJobDate", syncToolInfo.LastTomorrowPrintJobDate);
      
        Database.PutParameter(dbCommand,"?LastImportAdSourceDate", syncToolInfo.LastImportAdSourceDate);
      
        Database.PutParameter(dbCommand,"?LastImportedTruckId", syncToolInfo.LastImportedTruckId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(SyncToolInfo syncToolInfo)
      {
        Update(syncToolInfo, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " LastCustomerImportDate, "
      
        + " LastImportedTicketNumber, "
      
        + " LastTomorrowPrintJobDate, "
      
        + " LastImportAdSourceDate, "
      
        + " LastImportedTruckId "
      

      + " From SyncToolInfo "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static SyncToolInfo FindByPrimaryKey(
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
      throw new DataNotFoundException("SyncToolInfo not found, search by primary key");

      }

      public static SyncToolInfo FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(SyncToolInfo syncToolInfo, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",syncToolInfo.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(SyncToolInfo syncToolInfo)
      {
      return Exists(syncToolInfo, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from SyncToolInfo limit 1";

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

      public static SyncToolInfo Load(IDataReader dataReader, int offset)
      {
      SyncToolInfo syncToolInfo = new SyncToolInfo();

      syncToolInfo.ID = dataReader.GetInt32(0 + offset);
          syncToolInfo.LastCustomerImportDate = dataReader.GetDateTime(1 + offset);
          syncToolInfo.LastImportedTicketNumber = dataReader.GetString(2 + offset);
          syncToolInfo.LastTomorrowPrintJobDate = dataReader.GetDateTime(3 + offset);
          syncToolInfo.LastImportAdSourceDate = dataReader.GetDateTime(4 + offset);
          syncToolInfo.LastImportedTruckId = dataReader.GetString(5 + offset);
          

      return syncToolInfo;
      }

      public static SyncToolInfo Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From SyncToolInfo "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(SyncToolInfo syncToolInfo, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", syncToolInfo.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(SyncToolInfo syncToolInfo)
      {
        Delete(syncToolInfo, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From SyncToolInfo ";

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
      
        + " LastCustomerImportDate, "
      
        + " LastImportedTicketNumber, "
      
        + " LastTomorrowPrintJobDate, "
      
        + " LastImportAdSourceDate, "
      
        + " LastImportedTruckId "
      

      + " From SyncToolInfo ";
      public static List<SyncToolInfo> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<SyncToolInfo> rv = new List<SyncToolInfo>();

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

      public static List<SyncToolInfo> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<SyncToolInfo> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (SyncToolInfo obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && LastCustomerImportDate == obj.LastCustomerImportDate && LastImportedTicketNumber == obj.LastImportedTicketNumber && LastTomorrowPrintJobDate == obj.LastTomorrowPrintJobDate && LastImportAdSourceDate == obj.LastImportAdSourceDate && LastImportedTruckId == obj.LastImportedTruckId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<SyncToolInfo> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SyncToolInfo));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(SyncToolInfo item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<SyncToolInfo>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SyncToolInfo));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<SyncToolInfo> itemsList
      = new List<SyncToolInfo>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is SyncToolInfo)
      itemsList.Add(deserializedObject as SyncToolInfo);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected DateTime m_lastCustomerImportDate;
      
        protected String m_lastImportedTicketNumber;
      
        protected DateTime m_lastTomorrowPrintJobDate;

        protected DateTime m_LastImportAdSourceDate;
      
        protected String m_lastImportedTruckId;
      
      #endregion

      #region Constructors
      public SyncToolInfo(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public SyncToolInfo(
        int 
          iD,DateTime 
          lastCustomerImportDate,String 
          lastImportedTicketNumber,DateTime 
          lastTomorrowPrintJobDate,DateTime 
          LastImportAdSourceDate,String 
          lastImportedTruckId
        ) : this()
        {
        
          m_iD = iD;
        
          m_lastCustomerImportDate = lastCustomerImportDate;
        
          m_lastImportedTicketNumber = lastImportedTicketNumber;
        
          m_lastTomorrowPrintJobDate = lastTomorrowPrintJobDate;
        
          m_LastImportAdSourceDate = LastImportAdSourceDate;
        
          m_lastImportedTruckId = lastImportedTruckId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public DateTime LastCustomerImportDate
        {
        get { return m_lastCustomerImportDate;}
        set { m_lastCustomerImportDate = value; }
        }
      
        [XmlElement]
        public String LastImportedTicketNumber
        {
        get { return m_lastImportedTicketNumber;}
        set { m_lastImportedTicketNumber = value; }
        }
      
        [XmlElement]
        public DateTime LastTomorrowPrintJobDate
        {
        get { return m_lastTomorrowPrintJobDate;}
        set { m_lastTomorrowPrintJobDate = value; }
        }
      
        [XmlElement]
        public DateTime LastImportAdSourceDate
        {
        get { return m_LastImportAdSourceDate;}
        set { m_LastImportAdSourceDate = value; }
        }
      
        [XmlElement]
        public String LastImportedTruckId
        {
        get { return m_lastImportedTruckId;}
        set { m_lastImportedTruckId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 6; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    