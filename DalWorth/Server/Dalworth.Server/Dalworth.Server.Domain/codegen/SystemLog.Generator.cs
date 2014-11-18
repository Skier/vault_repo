
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


      public partial class SystemLog : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into SystemLog ( " +
      
        " EmployeeId, " +
      
        " DateCreated, " +
      
        " SystemOperationId, " +
      
        " Description, " +
      
        " TimeTakenMiliseconds " +
      
      ") Values (" +
      
        " ?EmployeeId, " +
      
        " ?DateCreated, " +
      
        " ?SystemOperationId, " +
      
        " ?Description, " +
      
        " ?TimeTakenMiliseconds " +
      
      ")";

      public static void Insert(SystemLog systemLog, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?EmployeeId", systemLog.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", systemLog.DateCreated);
      
        Database.PutParameter(dbCommand,"?SystemOperationId", systemLog.SystemOperationId);
      
        Database.PutParameter(dbCommand,"?Description", systemLog.Description);
      
        Database.PutParameter(dbCommand,"?TimeTakenMiliseconds", systemLog.TimeTakenMiliseconds);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        systemLog.ID = Convert.ToInt64(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(SystemLog systemLog)
      {
        Insert(systemLog, null);
      }


      public static void Insert(List<SystemLog>  systemLogList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(SystemLog systemLog in  systemLogList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?EmployeeId", systemLog.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", systemLog.DateCreated);
      
        Database.PutParameter(dbCommand,"?SystemOperationId", systemLog.SystemOperationId);
      
        Database.PutParameter(dbCommand,"?Description", systemLog.Description);
      
        Database.PutParameter(dbCommand,"?TimeTakenMiliseconds", systemLog.TimeTakenMiliseconds);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?EmployeeId",systemLog.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"?DateCreated",systemLog.DateCreated);
      
        Database.UpdateParameter(dbCommand,"?SystemOperationId",systemLog.SystemOperationId);
      
        Database.UpdateParameter(dbCommand,"?Description",systemLog.Description);
      
        Database.UpdateParameter(dbCommand,"?TimeTakenMiliseconds",systemLog.TimeTakenMiliseconds);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        systemLog.ID = Convert.ToInt64(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<SystemLog>  systemLogList)
      {
        Insert(systemLogList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update SystemLog Set "
      
        + " EmployeeId = ?EmployeeId, "
      
        + " DateCreated = ?DateCreated, "
      
        + " SystemOperationId = ?SystemOperationId, "
      
        + " Description = ?Description, "
      
        + " TimeTakenMiliseconds = ?TimeTakenMiliseconds "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(SystemLog systemLog, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", systemLog.ID);
      
        Database.PutParameter(dbCommand,"?EmployeeId", systemLog.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", systemLog.DateCreated);
      
        Database.PutParameter(dbCommand,"?SystemOperationId", systemLog.SystemOperationId);
      
        Database.PutParameter(dbCommand,"?Description", systemLog.Description);
      
        Database.PutParameter(dbCommand,"?TimeTakenMiliseconds", systemLog.TimeTakenMiliseconds);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(SystemLog systemLog)
      {
        Update(systemLog, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " EmployeeId, "
      
        + " DateCreated, "
      
        + " SystemOperationId, "
      
        + " Description, "
      
        + " TimeTakenMiliseconds "
      

      + " From SystemLog "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static SystemLog FindByPrimaryKey(
      long iD, IDbConnection connection
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
      throw new DataNotFoundException("SystemLog not found, search by primary key");

      }

      public static SystemLog FindByPrimaryKey(
      long iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(SystemLog systemLog, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",systemLog.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(SystemLog systemLog)
      {
      return Exists(systemLog, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from SystemLog limit 1";

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

      public static SystemLog Load(IDataReader dataReader, int offset)
      {
      SystemLog systemLog = new SystemLog();

      systemLog.ID = dataReader.GetInt64(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            systemLog.EmployeeId = dataReader.GetInt32(1 + offset);
          systemLog.DateCreated = dataReader.GetDateTime(2 + offset);
          systemLog.SystemOperationId = dataReader.GetInt32(3 + offset);
          systemLog.Description = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            systemLog.TimeTakenMiliseconds = dataReader.GetInt32(5 + offset);
          

      return systemLog;
      }

      public static SystemLog Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From SystemLog "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(SystemLog systemLog, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", systemLog.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(SystemLog systemLog)
      {
        Delete(systemLog, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From SystemLog ";

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
      
        + " EmployeeId, "
      
        + " DateCreated, "
      
        + " SystemOperationId, "
      
        + " Description, "
      
        + " TimeTakenMiliseconds "
      

      + " From SystemLog ";
      public static List<SystemLog> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<SystemLog> rv = new List<SystemLog>();

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

      public static List<SystemLog> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<SystemLog> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (SystemLog obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && EmployeeId == obj.EmployeeId && DateCreated == obj.DateCreated && SystemOperationId == obj.SystemOperationId && Description == obj.Description && TimeTakenMiliseconds == obj.TimeTakenMiliseconds;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<SystemLog> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SystemLog));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(SystemLog item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<SystemLog>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SystemLog));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<SystemLog> itemsList
      = new List<SystemLog>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is SystemLog)
      itemsList.Add(deserializedObject as SystemLog);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected long m_iD;
      
        protected int? m_employeeId;
      
        protected DateTime m_dateCreated;
      
        protected int m_systemOperationId;
      
        protected String m_description;
      
        protected int? m_timeTakenMiliseconds;
      
      #endregion

      #region Constructors
      public SystemLog(
      long 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public SystemLog(
        long 
          iD,int? 
          employeeId,DateTime 
          dateCreated,int 
          systemOperationId,String 
          description,int? 
          timeTakenMiliseconds
        ) : this()
        {
        
          m_iD = iD;
        
          m_employeeId = employeeId;
        
          m_dateCreated = dateCreated;
        
          m_systemOperationId = systemOperationId;
        
          m_description = description;
        
          m_timeTakenMiliseconds = timeTakenMiliseconds;
        
        }


      
      #endregion

      
        [XmlElement]
        public long ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int? EmployeeId
        {
        get { return m_employeeId;}
        set { m_employeeId = value; }
        }
      
        [XmlElement]
        public DateTime DateCreated
        {
        get { return m_dateCreated;}
        set { m_dateCreated = value; }
        }
      
        [XmlElement]
        public int SystemOperationId
        {
        get { return m_systemOperationId;}
        set { m_systemOperationId = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
        [XmlElement]
        public int? TimeTakenMiliseconds
        {
        get { return m_timeTakenMiliseconds;}
        set { m_timeTakenMiliseconds = value; }
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

    