
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


      public partial class TaskItemDelivery : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [TaskItemDelivery] ( " +
      
        " ID, " +
      
        " TaskId, " +
      
        " ItemId " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @TaskId, " +
      
        " @ItemId " +
      
      ")";

      public static void Insert(TaskItemDelivery taskItemDelivery, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", taskItemDelivery.ID);
      
        Database.PutParameter(dbCommand,"@TaskId", taskItemDelivery.TaskId);
      
        Database.PutParameter(dbCommand,"@ItemId", taskItemDelivery.ItemId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(TaskItemDelivery taskItemDelivery)
      {
        Insert(taskItemDelivery, null);
      }

      public static void Insert(List<TaskItemDelivery>  taskItemDeliveryList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(TaskItemDelivery taskItemDelivery in  taskItemDeliveryList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", taskItemDelivery.ID);
      
        Database.PutParameter(dbCommand,"@TaskId", taskItemDelivery.TaskId);
      
        Database.PutParameter(dbCommand,"@ItemId", taskItemDelivery.ItemId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",taskItemDelivery.ID);
      
        Database.UpdateParameter(dbCommand,"@TaskId",taskItemDelivery.TaskId);
      
        Database.UpdateParameter(dbCommand,"@ItemId",taskItemDelivery.ItemId);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<TaskItemDelivery>  taskItemDeliveryList)
      {
      Insert(taskItemDeliveryList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [TaskItemDelivery] Set "
      
        + " TaskId = @TaskId, "
      
        + " ItemId = @ItemId "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(TaskItemDelivery taskItemDelivery, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", taskItemDelivery.ID);
      
        Database.PutParameter(dbCommand,"@TaskId", taskItemDelivery.TaskId);
      
        Database.PutParameter(dbCommand,"@ItemId", taskItemDelivery.ItemId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TaskItemDelivery taskItemDelivery)
      {
      Update(taskItemDelivery, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " TaskId, "
      
        + " ItemId "
      

      + " From [TaskItemDelivery] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static TaskItemDelivery FindByPrimaryKey(
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
      throw new DataNotFoundException("TaskItemDelivery not found, search by primary key");

      }

      public static TaskItemDelivery FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(TaskItemDelivery taskItemDelivery, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",taskItemDelivery.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TaskItemDelivery taskItemDelivery)
      {
      return Exists(taskItemDelivery, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from TaskItemDelivery";

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

      public static TaskItemDelivery Load(IDataReader dataReader)
      {
      TaskItemDelivery taskItemDelivery = new TaskItemDelivery();

      taskItemDelivery.ID = dataReader.GetInt32(0);
          taskItemDelivery.TaskId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            taskItemDelivery.ItemId = dataReader.GetInt32(2);
          

      return taskItemDelivery;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [TaskItemDelivery] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(TaskItemDelivery taskItemDelivery, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", taskItemDelivery.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TaskItemDelivery taskItemDelivery)
      {
      Delete(taskItemDelivery, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [TaskItemDelivery] ";

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
      
        + " ItemId "
      

      + " From [TaskItemDelivery] ";
      public static List<TaskItemDelivery> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<TaskItemDelivery> rv = new List<TaskItemDelivery>();

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

      public static List<TaskItemDelivery> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TaskItemDelivery> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TaskItemDelivery> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskItemDelivery));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TaskItemDelivery item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TaskItemDelivery>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskItemDelivery));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TaskItemDelivery> itemsList
      = new List<TaskItemDelivery>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TaskItemDelivery)
      itemsList.Add(deserializedObject as TaskItemDelivery);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_taskId;
      
        protected int? m_itemId;
      
      #endregion

      #region Constructors
      public TaskItemDelivery(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public TaskItemDelivery(
        int 
          iD,int 
          taskId,int? 
          itemId
        )
        {
        
          m_iD = iD;
        
          m_taskId = taskId;
        
          m_itemId = itemId;
        
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
        public int? ItemId
        {
        get { return m_itemId;}
        set { m_itemId = value; }
        }
      
      }
      #endregion
      }

    