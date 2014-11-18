
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
      public partial class ChangeRecord : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ChangeRecord ( " +
      
        " ID, " +
      
        " DashboardDate, " +
      
        " DateCreated, " +
      
        " ChangeText, " +
      
        " IsAllPreviousChangesOptimized " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?DashboardDate, " +
      
        " ?DateCreated, " +
      
        " ?ChangeText, " +
      
        " ?IsAllPreviousChangesOptimized " +
      
      ")";

      public static void Insert(ChangeRecord changeRecord, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", changeRecord.ID);
      
        Database.PutParameter(dbCommand,"?DashboardDate", changeRecord.DashboardDate);
      
        Database.PutParameter(dbCommand,"?DateCreated", changeRecord.DateCreated);
      
        Database.PutParameter(dbCommand,"?ChangeText", changeRecord.ChangeText);
      
        Database.PutParameter(dbCommand,"?IsAllPreviousChangesOptimized", changeRecord.IsAllPreviousChangesOptimized);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ChangeRecord changeRecord)
      {
        Insert(changeRecord, null);
      }


      public static void Insert(List<ChangeRecord>  changeRecordList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ChangeRecord changeRecord in  changeRecordList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", changeRecord.ID);
      
        Database.PutParameter(dbCommand,"?DashboardDate", changeRecord.DashboardDate);
      
        Database.PutParameter(dbCommand,"?DateCreated", changeRecord.DateCreated);
      
        Database.PutParameter(dbCommand,"?ChangeText", changeRecord.ChangeText);
      
        Database.PutParameter(dbCommand,"?IsAllPreviousChangesOptimized", changeRecord.IsAllPreviousChangesOptimized);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",changeRecord.ID);
      
        Database.UpdateParameter(dbCommand,"?DashboardDate",changeRecord.DashboardDate);
      
        Database.UpdateParameter(dbCommand,"?DateCreated",changeRecord.DateCreated);
      
        Database.UpdateParameter(dbCommand,"?ChangeText",changeRecord.ChangeText);
      
        Database.UpdateParameter(dbCommand,"?IsAllPreviousChangesOptimized",changeRecord.IsAllPreviousChangesOptimized);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ChangeRecord>  changeRecordList)
      {
        Insert(changeRecordList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ChangeRecord Set "
      
        + " DateCreated = ?DateCreated, "
      
        + " ChangeText = ?ChangeText, "
      
        + " IsAllPreviousChangesOptimized = ?IsAllPreviousChangesOptimized "
      
        + " Where "
        
          + " ID = ?ID and  "
        
          + " DashboardDate = ?DashboardDate "
        
      ;

      public static void Update(ChangeRecord changeRecord, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", changeRecord.ID);
      
        Database.PutParameter(dbCommand,"?DashboardDate", changeRecord.DashboardDate);
      
        Database.PutParameter(dbCommand,"?DateCreated", changeRecord.DateCreated);
      
        Database.PutParameter(dbCommand,"?ChangeText", changeRecord.ChangeText);
      
        Database.PutParameter(dbCommand,"?IsAllPreviousChangesOptimized", changeRecord.IsAllPreviousChangesOptimized);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ChangeRecord changeRecord)
      {
        Update(changeRecord, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " DashboardDate, "
      
        + " DateCreated, "
      
        + " ChangeText, "
      
        + " IsAllPreviousChangesOptimized "
      

      + " From ChangeRecord "

      
        + " Where "
        
          + " ID = ?ID and  "
        
          + " DashboardDate = ?DashboardDate "
        
      ;

      public static ChangeRecord FindByPrimaryKey(
      int iD,DateTime dashboardDate, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", iD);
      
        Database.PutParameter(dbCommand,"?DashboardDate", dashboardDate);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ChangeRecord not found, search by primary key");

      }

      public static ChangeRecord FindByPrimaryKey(
      int iD,DateTime dashboardDate
      )
      {
      return FindByPrimaryKey(
      iD,dashboardDate, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ChangeRecord changeRecord, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",changeRecord.ID);
      
        Database.PutParameter(dbCommand,"?DashboardDate",changeRecord.DashboardDate);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ChangeRecord changeRecord)
      {
      return Exists(changeRecord, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ChangeRecord limit 1";

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

      public static ChangeRecord Load(IDataReader dataReader, int offset)
      {
      ChangeRecord changeRecord = new ChangeRecord();

      changeRecord.ID = dataReader.GetInt32(0 + offset);
          changeRecord.DashboardDate = dataReader.GetDateTime(1 + offset);
          changeRecord.DateCreated = dataReader.GetDateTime(2 + offset);
          changeRecord.ChangeText = dataReader.GetString(3 + offset);
          changeRecord.IsAllPreviousChangesOptimized = dataReader.GetBoolean(4 + offset);
          

      return changeRecord;
      }

      public static ChangeRecord Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ChangeRecord "

      
        + " Where "
        
          + " ID = ?ID and  "
        
          + " DashboardDate = ?DashboardDate "
        
      ;
      public static void Delete(ChangeRecord changeRecord, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", changeRecord.ID);
      
        Database.PutParameter(dbCommand,"?DashboardDate", changeRecord.DashboardDate);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ChangeRecord changeRecord)
      {
        Delete(changeRecord, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ChangeRecord ";

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
      
        + " DashboardDate, "
      
        + " DateCreated, "
      
        + " ChangeText, "
      
        + " IsAllPreviousChangesOptimized "
      

      + " From ChangeRecord ";
      public static List<ChangeRecord> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ChangeRecord> rv = new List<ChangeRecord>();

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

      public static List<ChangeRecord> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ChangeRecord> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<ChangeRecord> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ChangeRecord));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ChangeRecord item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ChangeRecord>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ChangeRecord));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ChangeRecord> itemsList
      = new List<ChangeRecord>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ChangeRecord)
      itemsList.Add(deserializedObject as ChangeRecord);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected DateTime m_dashboardDate;
      
        protected DateTime m_dateCreated;
      
        protected String m_changeText;
      
        protected bool m_isAllPreviousChangesOptimized;
      
      #endregion

      #region Constructors
      public ChangeRecord(
      int 
          iD,DateTime 
          dashboardDate
      ) : this()
      {
      
        m_iD = iD;
      
        m_dashboardDate = dashboardDate;
      
      }

      


        public ChangeRecord(
        int 
          iD,DateTime 
          dashboardDate,DateTime 
          dateCreated,String 
          changeText,bool 
          isAllPreviousChangesOptimized
        ) : this()
        {
        
          m_iD = iD;
        
          m_dashboardDate = dashboardDate;
        
          m_dateCreated = dateCreated;
        
          m_changeText = changeText;
        
          m_isAllPreviousChangesOptimized = isAllPreviousChangesOptimized;
        
        }


      
      #endregion

      
        [DataMember]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [DataMember]
        public DateTime DashboardDate
        {
        get { return m_dashboardDate;}
        set { m_dashboardDate = value; }
        }
      
        [DataMember]
        public DateTime DateCreated
        {
        get { return m_dateCreated;}
        set { m_dateCreated = value; }
        }
      
        [DataMember]
        public String ChangeText
        {
        get { return m_changeText;}
        set { m_changeText = value; }
        }
      
        [DataMember]
        public bool IsAllPreviousChangesOptimized
        {
        get { return m_isAllPreviousChangesOptimized;}
        set { m_isAllPreviousChangesOptimized = value; }
        }
      

      public static int FieldsCount
      {
      get { return 5; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    