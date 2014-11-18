
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


      public partial class TaskItemRequirement : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [TaskItemRequirement] ( " +
      
        " ID, " +
      
        " TaskId, " +
      
        " ItemType, " +
      
        " ServiceQuantity, " +
      
        " CaptureQuantity " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @TaskId, " +
      
        " @ItemType, " +
      
        " @ServiceQuantity, " +
      
        " @CaptureQuantity " +
      
      ")";

      public static void Insert(TaskItemRequirement taskItemRequirement, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", taskItemRequirement.ID);
      
        Database.PutParameter(dbCommand,"@TaskId", taskItemRequirement.TaskId);
      
        Database.PutParameter(dbCommand,"@ItemType", taskItemRequirement.ItemType);
      
        Database.PutParameter(dbCommand,"@ServiceQuantity", taskItemRequirement.ServiceQuantity);
      
        Database.PutParameter(dbCommand,"@CaptureQuantity", taskItemRequirement.CaptureQuantity);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(TaskItemRequirement taskItemRequirement)
      {
        Insert(taskItemRequirement, null);
      }

      public static void Insert(List<TaskItemRequirement>  taskItemRequirementList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(TaskItemRequirement taskItemRequirement in  taskItemRequirementList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", taskItemRequirement.ID);
      
        Database.PutParameter(dbCommand,"@TaskId", taskItemRequirement.TaskId);
      
        Database.PutParameter(dbCommand,"@ItemType", taskItemRequirement.ItemType);
      
        Database.PutParameter(dbCommand,"@ServiceQuantity", taskItemRequirement.ServiceQuantity);
      
        Database.PutParameter(dbCommand,"@CaptureQuantity", taskItemRequirement.CaptureQuantity);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",taskItemRequirement.ID);
      
        Database.UpdateParameter(dbCommand,"@TaskId",taskItemRequirement.TaskId);
      
        Database.UpdateParameter(dbCommand,"@ItemType",taskItemRequirement.ItemType);
      
        Database.UpdateParameter(dbCommand,"@ServiceQuantity",taskItemRequirement.ServiceQuantity);
      
        Database.UpdateParameter(dbCommand,"@CaptureQuantity",taskItemRequirement.CaptureQuantity);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<TaskItemRequirement>  taskItemRequirementList)
      {
      Insert(taskItemRequirementList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [TaskItemRequirement] Set "
      
        + " TaskId = @TaskId, "
      
        + " ItemType = @ItemType, "
      
        + " ServiceQuantity = @ServiceQuantity, "
      
        + " CaptureQuantity = @CaptureQuantity "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(TaskItemRequirement taskItemRequirement, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", taskItemRequirement.ID);
      
        Database.PutParameter(dbCommand,"@TaskId", taskItemRequirement.TaskId);
      
        Database.PutParameter(dbCommand,"@ItemType", taskItemRequirement.ItemType);
      
        Database.PutParameter(dbCommand,"@ServiceQuantity", taskItemRequirement.ServiceQuantity);
      
        Database.PutParameter(dbCommand,"@CaptureQuantity", taskItemRequirement.CaptureQuantity);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TaskItemRequirement taskItemRequirement)
      {
      Update(taskItemRequirement, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " TaskId, "
      
        + " ItemType, "
      
        + " ServiceQuantity, "
      
        + " CaptureQuantity "
      

      + " From [TaskItemRequirement] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static TaskItemRequirement FindByPrimaryKey(
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
      throw new DataNotFoundException("TaskItemRequirement not found, search by primary key");

      }

      public static TaskItemRequirement FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(TaskItemRequirement taskItemRequirement, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",taskItemRequirement.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TaskItemRequirement taskItemRequirement)
      {
      return Exists(taskItemRequirement, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from TaskItemRequirement";

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

      public static TaskItemRequirement Load(IDataReader dataReader)
      {
      TaskItemRequirement taskItemRequirement = new TaskItemRequirement();

      taskItemRequirement.ID = dataReader.GetInt32(0);
          taskItemRequirement.TaskId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            taskItemRequirement.ItemType = dataReader.GetString(2);
          
            if(!dataReader.IsDBNull(3))
            taskItemRequirement.ServiceQuantity = dataReader.GetInt32(3);
          
            if(!dataReader.IsDBNull(4))
            taskItemRequirement.CaptureQuantity = dataReader.GetInt32(4);
          

      return taskItemRequirement;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [TaskItemRequirement] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(TaskItemRequirement taskItemRequirement, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", taskItemRequirement.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TaskItemRequirement taskItemRequirement)
      {
      Delete(taskItemRequirement, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [TaskItemRequirement] ";

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
      
        + " TaskId, "
      
        + " ItemType, "
      
        + " ServiceQuantity, "
      
        + " CaptureQuantity "
      

      + " From [TaskItemRequirement] ";
      public static List<TaskItemRequirement> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<TaskItemRequirement> rv = new List<TaskItemRequirement>();

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

      public static List<TaskItemRequirement> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TaskItemRequirement> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TaskItemRequirement> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskItemRequirement));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TaskItemRequirement item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TaskItemRequirement>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskItemRequirement));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TaskItemRequirement> itemsList
      = new List<TaskItemRequirement>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TaskItemRequirement)
      itemsList.Add(deserializedObject as TaskItemRequirement);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_taskId;
      
        protected String m_itemType;
      
        protected int? m_serviceQuantity;
      
        protected int? m_captureQuantity;
      
      #endregion

      #region Constructors
      public TaskItemRequirement(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public TaskItemRequirement(
        int 
          iD,int 
          taskId,String 
          itemType,int? 
          serviceQuantity,int? 
          captureQuantity
        )
        {
        
          m_iD = iD;
        
          m_taskId = taskId;
        
          m_itemType = itemType;
        
          m_serviceQuantity = serviceQuantity;
        
          m_captureQuantity = captureQuantity;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int TaskId
        {
        get { return m_taskId;}
        set { m_taskId = value; }
        }
      
        [XmlElement]
        public String ItemType
        {
        get { return m_itemType;}
        set { m_itemType = value; }
        }
      
        [XmlElement]
        public int? ServiceQuantity
        {
        get { return m_serviceQuantity;}
        set { m_serviceQuantity = value; }
        }
      
        [XmlElement]
        public int? CaptureQuantity
        {
        get { return m_captureQuantity;}
        set { m_captureQuantity = value; }
        }
      
      }
      #endregion
      }

    