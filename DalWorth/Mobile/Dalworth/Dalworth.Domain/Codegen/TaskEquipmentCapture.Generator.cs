
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


      public partial class TaskEquipmentCapture : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [TaskEquipmentCapture] ( " +
      
        " ID, " +
      
        " TaskId, " +
      
        " EquipmentId " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @TaskId, " +
      
        " @EquipmentId " +
      
      ")";

      public static void Insert(TaskEquipmentCapture taskEquipmentCapture, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", taskEquipmentCapture.ID);
      
        Database.PutParameter(dbCommand,"@TaskId", taskEquipmentCapture.TaskId);
      
        Database.PutParameter(dbCommand,"@EquipmentId", taskEquipmentCapture.EquipmentId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(TaskEquipmentCapture taskEquipmentCapture)
      {
        Insert(taskEquipmentCapture, null);
      }

      public static void Insert(List<TaskEquipmentCapture>  taskEquipmentCaptureList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(TaskEquipmentCapture taskEquipmentCapture in  taskEquipmentCaptureList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", taskEquipmentCapture.ID);
      
        Database.PutParameter(dbCommand,"@TaskId", taskEquipmentCapture.TaskId);
      
        Database.PutParameter(dbCommand,"@EquipmentId", taskEquipmentCapture.EquipmentId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",taskEquipmentCapture.ID);
      
        Database.UpdateParameter(dbCommand,"@TaskId",taskEquipmentCapture.TaskId);
      
        Database.UpdateParameter(dbCommand,"@EquipmentId",taskEquipmentCapture.EquipmentId);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<TaskEquipmentCapture>  taskEquipmentCaptureList)
      {
      Insert(taskEquipmentCaptureList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [TaskEquipmentCapture] Set "
      
        + " TaskId = @TaskId, "
      
        + " EquipmentId = @EquipmentId "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(TaskEquipmentCapture taskEquipmentCapture, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", taskEquipmentCapture.ID);
      
        Database.PutParameter(dbCommand,"@TaskId", taskEquipmentCapture.TaskId);
      
        Database.PutParameter(dbCommand,"@EquipmentId", taskEquipmentCapture.EquipmentId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TaskEquipmentCapture taskEquipmentCapture)
      {
      Update(taskEquipmentCapture, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " TaskId, "
      
        + " EquipmentId "
      

      + " From [TaskEquipmentCapture] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static TaskEquipmentCapture FindByPrimaryKey(
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
      throw new DataNotFoundException("TaskEquipmentCapture not found, search by primary key");

      }

      public static TaskEquipmentCapture FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(TaskEquipmentCapture taskEquipmentCapture, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",taskEquipmentCapture.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TaskEquipmentCapture taskEquipmentCapture)
      {
      return Exists(taskEquipmentCapture, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from TaskEquipmentCapture";

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

      public static TaskEquipmentCapture Load(IDataReader dataReader)
      {
      TaskEquipmentCapture taskEquipmentCapture = new TaskEquipmentCapture();

      taskEquipmentCapture.ID = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            taskEquipmentCapture.TaskId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            taskEquipmentCapture.EquipmentId = dataReader.GetInt32(2);
          

      return taskEquipmentCapture;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [TaskEquipmentCapture] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(TaskEquipmentCapture taskEquipmentCapture, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", taskEquipmentCapture.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TaskEquipmentCapture taskEquipmentCapture)
      {
      Delete(taskEquipmentCapture, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [TaskEquipmentCapture] ";

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
      
        + " EquipmentId "
      

      + " From [TaskEquipmentCapture] ";
      public static List<TaskEquipmentCapture> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<TaskEquipmentCapture> rv = new List<TaskEquipmentCapture>();

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

      public static List<TaskEquipmentCapture> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TaskEquipmentCapture> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TaskEquipmentCapture> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskEquipmentCapture));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TaskEquipmentCapture item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TaskEquipmentCapture>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskEquipmentCapture));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TaskEquipmentCapture> itemsList
      = new List<TaskEquipmentCapture>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TaskEquipmentCapture)
      itemsList.Add(deserializedObject as TaskEquipmentCapture);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_taskId;
      
        protected int? m_equipmentId;
      
      #endregion

      #region Constructors
      public TaskEquipmentCapture(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public TaskEquipmentCapture(
        int 
          iD,int? 
          taskId,int? 
          equipmentId
        )
        {
        
          m_iD = iD;
        
          m_taskId = taskId;
        
          m_equipmentId = equipmentId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int? TaskId
        {
        get { return m_taskId;}
        set { m_taskId = value; }
        }
      
        [XmlElement]
        public int? EquipmentId
        {
        get { return m_equipmentId;}
        set { m_equipmentId = value; }
        }
      
      }
      #endregion
      }

    