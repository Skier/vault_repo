
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Data;
    using Dalworth.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Domain
      {


      public partial class WorkTransactionTask : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [WorkTransactionTask] ( " +
      
        " ID, " +
      
        " WorkTransactionId, " +
      
        " TaskId, " +
      
        " TaskStatusId, " +
      
        " AmountCollected " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @WorkTransactionId, " +
      
        " @TaskId, " +
      
        " @TaskStatusId, " +
      
        " @AmountCollected " +
      
      ")";

      public static void Insert(WorkTransactionTask workTransactionTask, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionTask.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionId", workTransactionTask.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"@TaskId", workTransactionTask.TaskId);
      
        Database.PutParameter(dbCommand,"@TaskStatusId", workTransactionTask.TaskStatusId);
      
        Database.PutParameter(dbCommand,"@AmountCollected", workTransactionTask.AmountCollected);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(WorkTransactionTask workTransactionTask)
      {
        Insert(workTransactionTask, null);
      }

      public static void Insert(List<WorkTransactionTask>  workTransactionTaskList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionTask workTransactionTask in  workTransactionTaskList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionTask.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionId", workTransactionTask.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"@TaskId", workTransactionTask.TaskId);
      
        Database.PutParameter(dbCommand,"@TaskStatusId", workTransactionTask.TaskStatusId);
      
        Database.PutParameter(dbCommand,"@AmountCollected", workTransactionTask.AmountCollected);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",workTransactionTask.ID);
      
        Database.UpdateParameter(dbCommand,"@WorkTransactionId",workTransactionTask.WorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"@TaskId",workTransactionTask.TaskId);
      
        Database.UpdateParameter(dbCommand,"@TaskStatusId",workTransactionTask.TaskStatusId);
      
        Database.UpdateParameter(dbCommand,"@AmountCollected",workTransactionTask.AmountCollected);
      
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


      private const String SqlUpdate = "Update [WorkTransactionTask] Set "
      
        + " WorkTransactionId = @WorkTransactionId, "
      
        + " TaskId = @TaskId, "
      
        + " TaskStatusId = @TaskStatusId, "
      
        + " AmountCollected = @AmountCollected "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(WorkTransactionTask workTransactionTask, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", workTransactionTask.ID);
      
        Database.PutParameter(dbCommand,"@WorkTransactionId", workTransactionTask.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"@TaskId", workTransactionTask.TaskId);
      
        Database.PutParameter(dbCommand,"@TaskStatusId", workTransactionTask.TaskStatusId);
      
        Database.PutParameter(dbCommand,"@AmountCollected", workTransactionTask.AmountCollected);
      

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

      
        + " ID, "
      
        + " WorkTransactionId, "
      
        + " TaskId, "
      
        + " TaskStatusId, "
      
        + " AmountCollected "
      

      + " From [WorkTransactionTask] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static WorkTransactionTask FindByPrimaryKey(
      int iD, IDbTransaction transaction
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("WorkTransactionTask not found, search by primary key");

      }

      public static WorkTransactionTask FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(WorkTransactionTask workTransactionTask, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",workTransactionTask.ID);
      

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

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from WorkTransactionTask";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, transaction))
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

      public static WorkTransactionTask Load(IDataReader dataReader)
      {
      WorkTransactionTask workTransactionTask = new WorkTransactionTask();

      workTransactionTask.ID = dataReader.GetInt32(0);
          workTransactionTask.WorkTransactionId = dataReader.GetInt32(1);
          workTransactionTask.TaskId = dataReader.GetInt32(2);
          workTransactionTask.TaskStatusId = dataReader.GetInt32(3);
          workTransactionTask.AmountCollected = dataReader.GetDecimal(4);
          

      return workTransactionTask;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [WorkTransactionTask] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(WorkTransactionTask workTransactionTask, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", workTransactionTask.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionTask workTransactionTask)
      {
      Delete(workTransactionTask, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [WorkTransactionTask] ";

      public static void Clear(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, transaction))
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
      
        + " WorkTransactionId, "
      
        + " TaskId, "
      
        + " TaskStatusId, "
      
        + " AmountCollected "
      

      + " From [WorkTransactionTask] ";
      public static List<WorkTransactionTask> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
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
      
        protected int m_iD;
      
        protected int m_workTransactionId;
      
        protected int m_taskId;
      
        protected int m_taskStatusId;
      
        protected decimal m_amountCollected;
      
      #endregion

      #region Constructors
      public WorkTransactionTask(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public WorkTransactionTask(
        int 
          iD,int 
          workTransactionId,int 
          taskId,int 
          taskStatusId,decimal 
          amountCollected
        )
        {
        
          m_iD = iD;
        
          m_workTransactionId = workTransactionId;
        
          m_taskId = taskId;
        
          m_taskStatusId = taskStatusId;
        
          m_amountCollected = amountCollected;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
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
        public int TaskStatusId
        {
        get { return m_taskStatusId;}
        set { m_taskStatusId = value; }
        }
      
        [XmlElement]
        public decimal AmountCollected
        {
        get { return m_amountCollected;}
        set { m_amountCollected = value; }
        }
      
      }
      #endregion
      }

    