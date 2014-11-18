
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


      public partial class WorkTransactionTask : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WorkTransactionTask ( " +
      
        " WorkTransactionId, " +
      
        " TaskId, " +
      
        " IsModified, " +
      
        " IsCreated, " +
      
        " WorkTransactionTaskActionId " +
      
      ") Values (" +
      
        " ?WorkTransactionId, " +
      
        " ?TaskId, " +
      
        " ?IsModified, " +
      
        " ?IsCreated, " +
      
        " ?WorkTransactionTaskActionId " +
      
      ")";

      public static void Insert(WorkTransactionTask workTransactionTask, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionTask.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?TaskId", workTransactionTask.TaskId);
      
        Database.PutParameter(dbCommand,"?IsModified", workTransactionTask.IsModified);
      
        Database.PutParameter(dbCommand,"?IsCreated", workTransactionTask.IsCreated);
      
        Database.PutParameter(dbCommand,"?WorkTransactionTaskActionId", workTransactionTask.WorkTransactionTaskActionId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(WorkTransactionTask workTransactionTask)
      {
        Insert(workTransactionTask, null);
      }


      public static void Insert(List<WorkTransactionTask>  workTransactionTaskList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionTask workTransactionTask in  workTransactionTaskList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionTask.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?TaskId", workTransactionTask.TaskId);
      
        Database.PutParameter(dbCommand,"?IsModified", workTransactionTask.IsModified);
      
        Database.PutParameter(dbCommand,"?IsCreated", workTransactionTask.IsCreated);
      
        Database.PutParameter(dbCommand,"?WorkTransactionTaskActionId", workTransactionTask.WorkTransactionTaskActionId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WorkTransactionId",workTransactionTask.WorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"?TaskId",workTransactionTask.TaskId);
      
        Database.UpdateParameter(dbCommand,"?IsModified",workTransactionTask.IsModified);
      
        Database.UpdateParameter(dbCommand,"?IsCreated",workTransactionTask.IsCreated);
      
        Database.UpdateParameter(dbCommand,"?WorkTransactionTaskActionId",workTransactionTask.WorkTransactionTaskActionId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<WorkTransactionTask>  workTransactionTaskList)
      {
        Insert(workTransactionTaskList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WorkTransactionTask Set "
      
        + " IsModified = ?IsModified, "
      
        + " IsCreated = ?IsCreated, "
      
        + " WorkTransactionTaskActionId = ?WorkTransactionTaskActionId "
      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId and  "
        
          + " TaskId = ?TaskId "
        
      ;

      public static void Update(WorkTransactionTask workTransactionTask, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionTask.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?TaskId", workTransactionTask.TaskId);
      
        Database.PutParameter(dbCommand,"?IsModified", workTransactionTask.IsModified);
      
        Database.PutParameter(dbCommand,"?IsCreated", workTransactionTask.IsCreated);
      
        Database.PutParameter(dbCommand,"?WorkTransactionTaskActionId", workTransactionTask.WorkTransactionTaskActionId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionTask workTransactionTask)
      {
        Update(workTransactionTask, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " WorkTransactionId, "
      
        + " TaskId, "
      
        + " IsModified, "
      
        + " IsCreated, "
      
        + " WorkTransactionTaskActionId "
      

      + " From WorkTransactionTask "

      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId and  "
        
          + " TaskId = ?TaskId "
        
      ;

      public static WorkTransactionTask FindByPrimaryKey(
      int workTransactionId,int taskId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionId);
      
        Database.PutParameter(dbCommand,"?TaskId", taskId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("WorkTransactionTask not found, search by primary key");

      }

      public static WorkTransactionTask FindByPrimaryKey(
      int workTransactionId,int taskId
      )
      {
      return FindByPrimaryKey(
      workTransactionId,taskId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WorkTransactionTask workTransactionTask, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId",workTransactionTask.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?TaskId",workTransactionTask.TaskId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionTask workTransactionTask)
      {
      return Exists(workTransactionTask, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WorkTransactionTask limit 1";

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

      public static WorkTransactionTask Load(IDataReader dataReader, int offset)
      {
      WorkTransactionTask workTransactionTask = new WorkTransactionTask();

      workTransactionTask.WorkTransactionId = dataReader.GetInt32(0 + offset);
          workTransactionTask.TaskId = dataReader.GetInt32(1 + offset);
          workTransactionTask.IsModified = dataReader.GetBoolean(2 + offset);
          workTransactionTask.IsCreated = dataReader.GetBoolean(3 + offset);
          workTransactionTask.WorkTransactionTaskActionId = dataReader.GetInt32(4 + offset);
          

      return workTransactionTask;
      }

      public static WorkTransactionTask Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WorkTransactionTask "

      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId and  "
        
          + " TaskId = ?TaskId "
        
      ;
      public static void Delete(WorkTransactionTask workTransactionTask, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionTask.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?TaskId", workTransactionTask.TaskId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionTask workTransactionTask)
      {
        Delete(workTransactionTask, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WorkTransactionTask ";

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

      
        + " WorkTransactionId, "
      
        + " TaskId, "
      
        + " IsModified, "
      
        + " IsCreated, "
      
        + " WorkTransactionTaskActionId "
      

      + " From WorkTransactionTask ";
      public static List<WorkTransactionTask> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WorkTransactionTask> rv = new List<WorkTransactionTask>();

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

      public static List<WorkTransactionTask> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionTask> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WorkTransactionTask obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return WorkTransactionId == obj.WorkTransactionId && TaskId == obj.TaskId && IsModified == obj.IsModified && IsCreated == obj.IsCreated && WorkTransactionTaskActionId == obj.WorkTransactionTaskActionId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionTask> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionTask));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionTask item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionTask>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionTask));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionTask> itemsList
      = new List<WorkTransactionTask>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionTask)
      itemsList.Add(deserializedObject as WorkTransactionTask);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_workTransactionId;
      
        protected int m_taskId;
      
        protected bool m_isModified;
      
        protected bool m_isCreated;
      
        protected int m_workTransactionTaskActionId;
      
      #endregion

      #region Constructors
      public WorkTransactionTask(
      int 
          workTransactionId,int 
          taskId
      ) : this()
      {
      
        m_workTransactionId = workTransactionId;
      
        m_taskId = taskId;
      
      }

      


        public WorkTransactionTask(
        int 
          workTransactionId,int 
          taskId,bool 
          isModified,bool 
          isCreated,int 
          workTransactionTaskActionId
        ) : this()
        {
        
          m_workTransactionId = workTransactionId;
        
          m_taskId = taskId;
        
          m_isModified = isModified;
        
          m_isCreated = isCreated;
        
          m_workTransactionTaskActionId = workTransactionTaskActionId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int WorkTransactionId
        {
        get { return m_workTransactionId;}
        set { m_workTransactionId = value; }
        }
      
        [XmlElement]
        public int TaskId
        {
        get { return m_taskId;}
        set { m_taskId = value; }
        }
      
        [XmlElement]
        public bool IsModified
        {
        get { return m_isModified;}
        set { m_isModified = value; }
        }
      
        [XmlElement]
        public bool IsCreated
        {
        get { return m_isCreated;}
        set { m_isCreated = value; }
        }
      
        [XmlElement]
        public int WorkTransactionTaskActionId
        {
        get { return m_workTransactionTaskActionId;}
        set { m_workTransactionTaskActionId = value; }
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

    