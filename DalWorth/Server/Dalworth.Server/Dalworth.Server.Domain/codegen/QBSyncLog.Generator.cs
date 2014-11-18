
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


      public partial class QBSyncLog : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QBSyncLog ( " +
      
        " LastRunDate " +
      
      ") Values (" +
      
        " ?LastRunDate " +
      
      ")";

      public static void Insert(QBSyncLog qBSyncLog, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?LastRunDate", qBSyncLog.LastRunDate);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        qBSyncLog.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(QBSyncLog qBSyncLog)
      {
        Insert(qBSyncLog, null);
      }


      public static void Insert(List<QBSyncLog>  qBSyncLogList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QBSyncLog qBSyncLog in  qBSyncLogList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?LastRunDate", qBSyncLog.LastRunDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?LastRunDate",qBSyncLog.LastRunDate);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        qBSyncLog.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<QBSyncLog>  qBSyncLogList)
      {
        Insert(qBSyncLogList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QBSyncLog Set "
      
        + " LastRunDate = ?LastRunDate "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(QBSyncLog qBSyncLog, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qBSyncLog.ID);
      
        Database.PutParameter(dbCommand,"?LastRunDate", qBSyncLog.LastRunDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QBSyncLog qBSyncLog)
      {
        Update(qBSyncLog, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " LastRunDate "
      

      + " From QBSyncLog "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static QBSyncLog FindByPrimaryKey(
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
      throw new DataNotFoundException("QBSyncLog not found, search by primary key");

      }

      public static QBSyncLog FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QBSyncLog qBSyncLog, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",qBSyncLog.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QBSyncLog qBSyncLog)
      {
      return Exists(qBSyncLog, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QBSyncLog limit 1";

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

      public static QBSyncLog Load(IDataReader dataReader, int offset)
      {
      QBSyncLog qBSyncLog = new QBSyncLog();

      qBSyncLog.ID = dataReader.GetInt32(0 + offset);
          qBSyncLog.LastRunDate = dataReader.GetDateTime(1 + offset);
          

      return qBSyncLog;
      }

      public static QBSyncLog Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QBSyncLog "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(QBSyncLog qBSyncLog, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", qBSyncLog.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QBSyncLog qBSyncLog)
      {
        Delete(qBSyncLog, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QBSyncLog ";

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
      
        + " LastRunDate "
      

      + " From QBSyncLog ";
      public static List<QBSyncLog> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QBSyncLog> rv = new List<QBSyncLog>();

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

      public static List<QBSyncLog> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QBSyncLog> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QBSyncLog obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && LastRunDate == obj.LastRunDate;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QBSyncLog> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QBSyncLog));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QBSyncLog item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QBSyncLog>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QBSyncLog));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QBSyncLog> itemsList
      = new List<QBSyncLog>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QBSyncLog)
      itemsList.Add(deserializedObject as QBSyncLog);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected DateTime m_lastRunDate;
      
      #endregion

      #region Constructors
      public QBSyncLog(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public QBSyncLog(
        int 
          iD,DateTime 
          lastRunDate
        ) : this()
        {
        
          m_iD = iD;
        
          m_lastRunDate = lastRunDate;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public DateTime LastRunDate
        {
        get { return m_lastRunDate;}
        set { m_lastRunDate = value; }
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

    