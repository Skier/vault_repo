
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


      public partial class TaskEquipmentRequirement : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [TaskEquipmentRequirement] ( " +
      
        " ID, " +
      
        " TaskId, " +
      
        " EquipmentTypeId, " +
      
        " ServiceQuantity, " +
      
        " LeaveQuantity " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @TaskId, " +
      
        " @EquipmentTypeId, " +
      
        " @ServiceQuantity, " +
      
        " @LeaveQuantity " +
      
      ")";

      public static void Insert(TaskEquipmentRequirement taskEquipmentRequirement, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", taskEquipmentRequirement.ID);
      
        Database.PutParameter(dbCommand,"@TaskId", taskEquipmentRequirement.TaskId);
      
        Database.PutParameter(dbCommand,"@EquipmentTypeId", taskEquipmentRequirement.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"@ServiceQuantity", taskEquipmentRequirement.ServiceQuantity);
      
        Database.PutParameter(dbCommand,"@LeaveQuantity", taskEquipmentRequirement.LeaveQuantity);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(TaskEquipmentRequirement taskEquipmentRequirement)
      {
        Insert(taskEquipmentRequirement, null);
      }

      public static void Insert(List<TaskEquipmentRequirement>  taskEquipmentRequirementList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(TaskEquipmentRequirement taskEquipmentRequirement in  taskEquipmentRequirementList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", taskEquipmentRequirement.ID);
      
        Database.PutParameter(dbCommand,"@TaskId", taskEquipmentRequirement.TaskId);
      
        Database.PutParameter(dbCommand,"@EquipmentTypeId", taskEquipmentRequirement.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"@ServiceQuantity", taskEquipmentRequirement.ServiceQuantity);
      
        Database.PutParameter(dbCommand,"@LeaveQuantity", taskEquipmentRequirement.LeaveQuantity);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",taskEquipmentRequirement.ID);
      
        Database.UpdateParameter(dbCommand,"@TaskId",taskEquipmentRequirement.TaskId);
      
        Database.UpdateParameter(dbCommand,"@EquipmentTypeId",taskEquipmentRequirement.EquipmentTypeId);
      
        Database.UpdateParameter(dbCommand,"@ServiceQuantity",taskEquipmentRequirement.ServiceQuantity);
      
        Database.UpdateParameter(dbCommand,"@LeaveQuantity",taskEquipmentRequirement.LeaveQuantity);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<TaskEquipmentRequirement>  taskEquipmentRequirementList)
      {
      Insert(taskEquipmentRequirementList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [TaskEquipmentRequirement] Set "
      
        + " TaskId = @TaskId, "
      
        + " EquipmentTypeId = @EquipmentTypeId, "
      
        + " ServiceQuantity = @ServiceQuantity, "
      
        + " LeaveQuantity = @LeaveQuantity "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(TaskEquipmentRequirement taskEquipmentRequirement, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", taskEquipmentRequirement.ID);
      
        Database.PutParameter(dbCommand,"@TaskId", taskEquipmentRequirement.TaskId);
      
        Database.PutParameter(dbCommand,"@EquipmentTypeId", taskEquipmentRequirement.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"@ServiceQuantity", taskEquipmentRequirement.ServiceQuantity);
      
        Database.PutParameter(dbCommand,"@LeaveQuantity", taskEquipmentRequirement.LeaveQuantity);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TaskEquipmentRequirement taskEquipmentRequirement)
      {
      Update(taskEquipmentRequirement, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " TaskId, "
      
        + " EquipmentTypeId, "
      
        + " ServiceQuantity, "
      
        + " LeaveQuantity "
      

      + " From [TaskEquipmentRequirement] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static TaskEquipmentRequirement FindByPrimaryKey(
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
      throw new DataNotFoundException("TaskEquipmentRequirement not found, search by primary key");

      }

      public static TaskEquipmentRequirement FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(TaskEquipmentRequirement taskEquipmentRequirement, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",taskEquipmentRequirement.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TaskEquipmentRequirement taskEquipmentRequirement)
      {
      return Exists(taskEquipmentRequirement, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from TaskEquipmentRequirement";

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

      public static TaskEquipmentRequirement Load(IDataReader dataReader)
      {
      TaskEquipmentRequirement taskEquipmentRequirement = new TaskEquipmentRequirement();

      taskEquipmentRequirement.ID = dataReader.GetInt32(0);
          taskEquipmentRequirement.TaskId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            taskEquipmentRequirement.EquipmentTypeId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            taskEquipmentRequirement.ServiceQuantity = dataReader.GetInt32(3);
          
            if(!dataReader.IsDBNull(4))
            taskEquipmentRequirement.LeaveQuantity = dataReader.GetInt32(4);
          

      return taskEquipmentRequirement;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [TaskEquipmentRequirement] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(TaskEquipmentRequirement taskEquipmentRequirement, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", taskEquipmentRequirement.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TaskEquipmentRequirement taskEquipmentRequirement)
      {
      Delete(taskEquipmentRequirement, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [TaskEquipmentRequirement] ";

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
      
        + " EquipmentTypeId, "
      
        + " ServiceQuantity, "
      
        + " LeaveQuantity "
      

      + " From [TaskEquipmentRequirement] ";
      public static List<TaskEquipmentRequirement> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<TaskEquipmentRequirement> rv = new List<TaskEquipmentRequirement>();

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

      public static List<TaskEquipmentRequirement> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TaskEquipmentRequirement> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TaskEquipmentRequirement> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskEquipmentRequirement));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TaskEquipmentRequirement item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TaskEquipmentRequirement>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskEquipmentRequirement));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TaskEquipmentRequirement> itemsList
      = new List<TaskEquipmentRequirement>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TaskEquipmentRequirement)
      itemsList.Add(deserializedObject as TaskEquipmentRequirement);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_taskId;
      
        protected int? m_equipmentTypeId;
      
        protected int? m_serviceQuantity;
      
        protected int? m_leaveQuantity;
      
      #endregion

      #region Constructors
      public TaskEquipmentRequirement(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public TaskEquipmentRequirement(
        int 
          iD,int 
          taskId,int? 
          equipmentTypeId,int? 
          serviceQuantity,int? 
          leaveQuantity
        )
        {
        
          m_iD = iD;
        
          m_taskId = taskId;
        
          m_equipmentTypeId = equipmentTypeId;
        
          m_serviceQuantity = serviceQuantity;
        
          m_leaveQuantity = leaveQuantity;
        
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
        public int? EquipmentTypeId
        {
        get { return m_equipmentTypeId;}
        set { m_equipmentTypeId = value; }
        }
      
        [XmlElement]
        public int? ServiceQuantity
        {
        get { return m_serviceQuantity;}
        set { m_serviceQuantity = value; }
        }
      
        [XmlElement]
        public int? LeaveQuantity
        {
        get { return m_leaveQuantity;}
        set { m_leaveQuantity = value; }
        }
      
      }
      #endregion
      }

    