
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


      public partial class WorkDetailLog : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WorkDetailLog ( " +
      
        " EmployeeId, " +
      
        " DateCreated, " +
      
        " Trace, " +
      
        " WorkDetailId, " +
      
        " WorkId, " +
      
        " VisitId, " +
      
        " TimeBegin, " +
      
        " TimeEnd, " +
      
        " Sequence, " +
      
        " WorkDetailStatusId, " +
      
        " TimeDispatch, " +
      
        " TimeArrive, " +
      
        " TimeComplete, " +
      
        " TimeBeginAssigned, " +
      
        " TimeEndAssigned " +
      
      ") Values (" +
      
        " ?EmployeeId, " +
      
        " ?DateCreated, " +
      
        " ?Trace, " +
      
        " ?WorkDetailId, " +
      
        " ?WorkId, " +
      
        " ?VisitId, " +
      
        " ?TimeBegin, " +
      
        " ?TimeEnd, " +
      
        " ?Sequence, " +
      
        " ?WorkDetailStatusId, " +
      
        " ?TimeDispatch, " +
      
        " ?TimeArrive, " +
      
        " ?TimeComplete, " +
      
        " ?TimeBeginAssigned, " +
      
        " ?TimeEndAssigned " +
      
      ")";

      public static void Insert(WorkDetailLog workDetailLog, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?EmployeeId", workDetailLog.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", workDetailLog.DateCreated);
      
        Database.PutParameter(dbCommand,"?Trace", workDetailLog.Trace);
      
        Database.PutParameter(dbCommand,"?WorkDetailId", workDetailLog.WorkDetailId);
      
        Database.PutParameter(dbCommand,"?WorkId", workDetailLog.WorkId);
      
        Database.PutParameter(dbCommand,"?VisitId", workDetailLog.VisitId);
      
        Database.PutParameter(dbCommand,"?TimeBegin", workDetailLog.TimeBegin);
      
        Database.PutParameter(dbCommand,"?TimeEnd", workDetailLog.TimeEnd);
      
        Database.PutParameter(dbCommand,"?Sequence", workDetailLog.Sequence);
      
        Database.PutParameter(dbCommand,"?WorkDetailStatusId", workDetailLog.WorkDetailStatusId);
      
        Database.PutParameter(dbCommand,"?TimeDispatch", workDetailLog.TimeDispatch);
      
        Database.PutParameter(dbCommand,"?TimeArrive", workDetailLog.TimeArrive);
      
        Database.PutParameter(dbCommand,"?TimeComplete", workDetailLog.TimeComplete);
      
        Database.PutParameter(dbCommand,"?TimeBeginAssigned", workDetailLog.TimeBeginAssigned);
      
        Database.PutParameter(dbCommand,"?TimeEndAssigned", workDetailLog.TimeEndAssigned);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        workDetailLog.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(WorkDetailLog workDetailLog)
      {
        Insert(workDetailLog, null);
      }


      public static void Insert(List<WorkDetailLog>  workDetailLogList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WorkDetailLog workDetailLog in  workDetailLogList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?EmployeeId", workDetailLog.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", workDetailLog.DateCreated);
      
        Database.PutParameter(dbCommand,"?Trace", workDetailLog.Trace);
      
        Database.PutParameter(dbCommand,"?WorkDetailId", workDetailLog.WorkDetailId);
      
        Database.PutParameter(dbCommand,"?WorkId", workDetailLog.WorkId);
      
        Database.PutParameter(dbCommand,"?VisitId", workDetailLog.VisitId);
      
        Database.PutParameter(dbCommand,"?TimeBegin", workDetailLog.TimeBegin);
      
        Database.PutParameter(dbCommand,"?TimeEnd", workDetailLog.TimeEnd);
      
        Database.PutParameter(dbCommand,"?Sequence", workDetailLog.Sequence);
      
        Database.PutParameter(dbCommand,"?WorkDetailStatusId", workDetailLog.WorkDetailStatusId);
      
        Database.PutParameter(dbCommand,"?TimeDispatch", workDetailLog.TimeDispatch);
      
        Database.PutParameter(dbCommand,"?TimeArrive", workDetailLog.TimeArrive);
      
        Database.PutParameter(dbCommand,"?TimeComplete", workDetailLog.TimeComplete);
      
        Database.PutParameter(dbCommand,"?TimeBeginAssigned", workDetailLog.TimeBeginAssigned);
      
        Database.PutParameter(dbCommand,"?TimeEndAssigned", workDetailLog.TimeEndAssigned);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?EmployeeId",workDetailLog.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"?DateCreated",workDetailLog.DateCreated);
      
        Database.UpdateParameter(dbCommand,"?Trace",workDetailLog.Trace);
      
        Database.UpdateParameter(dbCommand,"?WorkDetailId",workDetailLog.WorkDetailId);
      
        Database.UpdateParameter(dbCommand,"?WorkId",workDetailLog.WorkId);
      
        Database.UpdateParameter(dbCommand,"?VisitId",workDetailLog.VisitId);
      
        Database.UpdateParameter(dbCommand,"?TimeBegin",workDetailLog.TimeBegin);
      
        Database.UpdateParameter(dbCommand,"?TimeEnd",workDetailLog.TimeEnd);
      
        Database.UpdateParameter(dbCommand,"?Sequence",workDetailLog.Sequence);
      
        Database.UpdateParameter(dbCommand,"?WorkDetailStatusId",workDetailLog.WorkDetailStatusId);
      
        Database.UpdateParameter(dbCommand,"?TimeDispatch",workDetailLog.TimeDispatch);
      
        Database.UpdateParameter(dbCommand,"?TimeArrive",workDetailLog.TimeArrive);
      
        Database.UpdateParameter(dbCommand,"?TimeComplete",workDetailLog.TimeComplete);
      
        Database.UpdateParameter(dbCommand,"?TimeBeginAssigned",workDetailLog.TimeBeginAssigned);
      
        Database.UpdateParameter(dbCommand,"?TimeEndAssigned",workDetailLog.TimeEndAssigned);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        workDetailLog.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<WorkDetailLog>  workDetailLogList)
      {
        Insert(workDetailLogList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WorkDetailLog Set "
      
        + " EmployeeId = ?EmployeeId, "
      
        + " DateCreated = ?DateCreated, "
      
        + " Trace = ?Trace, "
      
        + " WorkDetailId = ?WorkDetailId, "
      
        + " WorkId = ?WorkId, "
      
        + " VisitId = ?VisitId, "
      
        + " TimeBegin = ?TimeBegin, "
      
        + " TimeEnd = ?TimeEnd, "
      
        + " Sequence = ?Sequence, "
      
        + " WorkDetailStatusId = ?WorkDetailStatusId, "
      
        + " TimeDispatch = ?TimeDispatch, "
      
        + " TimeArrive = ?TimeArrive, "
      
        + " TimeComplete = ?TimeComplete, "
      
        + " TimeBeginAssigned = ?TimeBeginAssigned, "
      
        + " TimeEndAssigned = ?TimeEndAssigned "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WorkDetailLog workDetailLog, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", workDetailLog.ID);
      
        Database.PutParameter(dbCommand,"?EmployeeId", workDetailLog.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", workDetailLog.DateCreated);
      
        Database.PutParameter(dbCommand,"?Trace", workDetailLog.Trace);
      
        Database.PutParameter(dbCommand,"?WorkDetailId", workDetailLog.WorkDetailId);
      
        Database.PutParameter(dbCommand,"?WorkId", workDetailLog.WorkId);
      
        Database.PutParameter(dbCommand,"?VisitId", workDetailLog.VisitId);
      
        Database.PutParameter(dbCommand,"?TimeBegin", workDetailLog.TimeBegin);
      
        Database.PutParameter(dbCommand,"?TimeEnd", workDetailLog.TimeEnd);
      
        Database.PutParameter(dbCommand,"?Sequence", workDetailLog.Sequence);
      
        Database.PutParameter(dbCommand,"?WorkDetailStatusId", workDetailLog.WorkDetailStatusId);
      
        Database.PutParameter(dbCommand,"?TimeDispatch", workDetailLog.TimeDispatch);
      
        Database.PutParameter(dbCommand,"?TimeArrive", workDetailLog.TimeArrive);
      
        Database.PutParameter(dbCommand,"?TimeComplete", workDetailLog.TimeComplete);
      
        Database.PutParameter(dbCommand,"?TimeBeginAssigned", workDetailLog.TimeBeginAssigned);
      
        Database.PutParameter(dbCommand,"?TimeEndAssigned", workDetailLog.TimeEndAssigned);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkDetailLog workDetailLog)
      {
        Update(workDetailLog, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " EmployeeId, "
      
        + " DateCreated, "
      
        + " Trace, "
      
        + " WorkDetailId, "
      
        + " WorkId, "
      
        + " VisitId, "
      
        + " TimeBegin, "
      
        + " TimeEnd, "
      
        + " Sequence, "
      
        + " WorkDetailStatusId, "
      
        + " TimeDispatch, "
      
        + " TimeArrive, "
      
        + " TimeComplete, "
      
        + " TimeBeginAssigned, "
      
        + " TimeEndAssigned "
      

      + " From WorkDetailLog "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WorkDetailLog FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkDetailLog not found, search by primary key");

      }

      public static WorkDetailLog FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WorkDetailLog workDetailLog, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",workDetailLog.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkDetailLog workDetailLog)
      {
      return Exists(workDetailLog, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WorkDetailLog limit 1";

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

      public static WorkDetailLog Load(IDataReader dataReader, int offset)
      {
      WorkDetailLog workDetailLog = new WorkDetailLog();

      workDetailLog.ID = dataReader.GetInt32(0 + offset);
          workDetailLog.EmployeeId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            workDetailLog.DateCreated = dataReader.GetDateTime(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            workDetailLog.Trace = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            workDetailLog.WorkDetailId = dataReader.GetInt32(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            workDetailLog.WorkId = dataReader.GetInt32(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            workDetailLog.VisitId = dataReader.GetInt32(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            workDetailLog.TimeBegin = dataReader.GetDateTime(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            workDetailLog.TimeEnd = dataReader.GetDateTime(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            workDetailLog.Sequence = dataReader.GetInt32(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            workDetailLog.WorkDetailStatusId = dataReader.GetInt32(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            workDetailLog.TimeDispatch = dataReader.GetDateTime(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            workDetailLog.TimeArrive = dataReader.GetDateTime(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            workDetailLog.TimeComplete = dataReader.GetDateTime(13 + offset);
          
            if(!dataReader.IsDBNull(14 + offset))
            workDetailLog.TimeBeginAssigned = dataReader.GetDateTime(14 + offset);
          
            if(!dataReader.IsDBNull(15 + offset))
            workDetailLog.TimeEndAssigned = dataReader.GetDateTime(15 + offset);
          

      return workDetailLog;
      }

      public static WorkDetailLog Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WorkDetailLog "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WorkDetailLog workDetailLog, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", workDetailLog.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkDetailLog workDetailLog)
      {
        Delete(workDetailLog, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WorkDetailLog ";

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
      
        + " Trace, "
      
        + " WorkDetailId, "
      
        + " WorkId, "
      
        + " VisitId, "
      
        + " TimeBegin, "
      
        + " TimeEnd, "
      
        + " Sequence, "
      
        + " WorkDetailStatusId, "
      
        + " TimeDispatch, "
      
        + " TimeArrive, "
      
        + " TimeComplete, "
      
        + " TimeBeginAssigned, "
      
        + " TimeEndAssigned "
      

      + " From WorkDetailLog ";
      public static List<WorkDetailLog> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WorkDetailLog> rv = new List<WorkDetailLog>();

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

      public static List<WorkDetailLog> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkDetailLog> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WorkDetailLog obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && EmployeeId == obj.EmployeeId && DateCreated == obj.DateCreated && Trace == obj.Trace && WorkDetailId == obj.WorkDetailId && WorkId == obj.WorkId && VisitId == obj.VisitId && TimeBegin == obj.TimeBegin && TimeEnd == obj.TimeEnd && Sequence == obj.Sequence && WorkDetailStatusId == obj.WorkDetailStatusId && TimeDispatch == obj.TimeDispatch && TimeArrive == obj.TimeArrive && TimeComplete == obj.TimeComplete && TimeBeginAssigned == obj.TimeBeginAssigned && TimeEndAssigned == obj.TimeEndAssigned;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WorkDetailLog> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkDetailLog));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkDetailLog item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkDetailLog>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkDetailLog));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkDetailLog> itemsList
      = new List<WorkDetailLog>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkDetailLog)
      itemsList.Add(deserializedObject as WorkDetailLog);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_employeeId;
      
        protected DateTime? m_dateCreated;
      
        protected String m_trace;
      
        protected int? m_workDetailId;
      
        protected int? m_workId;
      
        protected int? m_visitId;
      
        protected DateTime? m_timeBegin;
      
        protected DateTime? m_timeEnd;
      
        protected int? m_sequence;
      
        protected int? m_workDetailStatusId;
      
        protected DateTime? m_timeDispatch;
      
        protected DateTime? m_timeArrive;
      
        protected DateTime? m_timeComplete;
      
        protected DateTime? m_timeBeginAssigned;
      
        protected DateTime? m_timeEndAssigned;
      
      #endregion

      #region Constructors
      public WorkDetailLog(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WorkDetailLog(
        int 
          iD,int 
          employeeId,DateTime? 
          dateCreated,String 
          trace,int? 
          workDetailId,int? 
          workId,int? 
          visitId,DateTime? 
          timeBegin,DateTime? 
          timeEnd,int? 
          sequence,int? 
          workDetailStatusId,DateTime? 
          timeDispatch,DateTime? 
          timeArrive,DateTime? 
          timeComplete,DateTime? 
          timeBeginAssigned,DateTime? 
          timeEndAssigned
        ) : this()
        {
        
          m_iD = iD;
        
          m_employeeId = employeeId;
        
          m_dateCreated = dateCreated;
        
          m_trace = trace;
        
          m_workDetailId = workDetailId;
        
          m_workId = workId;
        
          m_visitId = visitId;
        
          m_timeBegin = timeBegin;
        
          m_timeEnd = timeEnd;
        
          m_sequence = sequence;
        
          m_workDetailStatusId = workDetailStatusId;
        
          m_timeDispatch = timeDispatch;
        
          m_timeArrive = timeArrive;
        
          m_timeComplete = timeComplete;
        
          m_timeBeginAssigned = timeBeginAssigned;
        
          m_timeEndAssigned = timeEndAssigned;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int EmployeeId
        {
        get { return m_employeeId;}
        set { m_employeeId = value; }
        }
      
        [XmlElement]
        public DateTime? DateCreated
        {
        get { return m_dateCreated;}
        set { m_dateCreated = value; }
        }
      
        [XmlElement]
        public String Trace
        {
        get { return m_trace;}
        set { m_trace = value; }
        }
      
        [XmlElement]
        public int? WorkDetailId
        {
        get { return m_workDetailId;}
        set { m_workDetailId = value; }
        }
      
        [XmlElement]
        public int? WorkId
        {
        get { return m_workId;}
        set { m_workId = value; }
        }
      
        [XmlElement]
        public int? VisitId
        {
        get { return m_visitId;}
        set { m_visitId = value; }
        }
      
        [XmlElement]
        public DateTime? TimeBegin
        {
        get { return m_timeBegin;}
        set { m_timeBegin = value; }
        }
      
        [XmlElement]
        public DateTime? TimeEnd
        {
        get { return m_timeEnd;}
        set { m_timeEnd = value; }
        }
      
        [XmlElement]
        public int? Sequence
        {
        get { return m_sequence;}
        set { m_sequence = value; }
        }
      
        [XmlElement]
        public int? WorkDetailStatusId
        {
        get { return m_workDetailStatusId;}
        set { m_workDetailStatusId = value; }
        }
      
        [XmlElement]
        public DateTime? TimeDispatch
        {
        get { return m_timeDispatch;}
        set { m_timeDispatch = value; }
        }
      
        [XmlElement]
        public DateTime? TimeArrive
        {
        get { return m_timeArrive;}
        set { m_timeArrive = value; }
        }
      
        [XmlElement]
        public DateTime? TimeComplete
        {
        get { return m_timeComplete;}
        set { m_timeComplete = value; }
        }
      
        [XmlElement]
        public DateTime? TimeBeginAssigned
        {
        get { return m_timeBeginAssigned;}
        set { m_timeBeginAssigned = value; }
        }
      
        [XmlElement]
        public DateTime? TimeEndAssigned
        {
        get { return m_timeEndAssigned;}
        set { m_timeEndAssigned = value; }
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

    