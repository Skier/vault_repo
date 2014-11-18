
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


      public partial class TaskStatus : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [TaskStatus] ( " +
      
        " ID, " +
      
        " Status, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @Status, " +
      
        " @Description " +
      
      ")";

      public static void Insert(TaskStatus taskStatus, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", taskStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", taskStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", taskStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(TaskStatus taskStatus)
      {
        Insert(taskStatus, null);
      }

      public static void Insert(List<TaskStatus>  taskStatusList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(TaskStatus taskStatus in  taskStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", taskStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", taskStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", taskStatus.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",taskStatus.ID);
      
        Database.UpdateParameter(dbCommand,"@Status",taskStatus.Status);
      
        Database.UpdateParameter(dbCommand,"@Description",taskStatus.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<TaskStatus>  taskStatusList)
      {
      Insert(taskStatusList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [TaskStatus] Set "
      
        + " Status = @Status, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(TaskStatus taskStatus, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", taskStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", taskStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", taskStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TaskStatus taskStatus)
      {
      Update(taskStatus, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Status, "
      
        + " Description "
      

      + " From [TaskStatus] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static TaskStatus FindByPrimaryKey(
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
      throw new DataNotFoundException("TaskStatus not found, search by primary key");

      }

      public static TaskStatus FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(TaskStatus taskStatus, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",taskStatus.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TaskStatus taskStatus)
      {
      return Exists(taskStatus, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from TaskStatus";

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

      public static TaskStatus Load(IDataReader dataReader)
      {
      TaskStatus taskStatus = new TaskStatus();

      taskStatus.ID = dataReader.GetInt32(0);
          taskStatus.Status = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            taskStatus.Description = dataReader.GetString(2);
          

      return taskStatus;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [TaskStatus] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(TaskStatus taskStatus, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", taskStatus.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TaskStatus taskStatus)
      {
      Delete(taskStatus, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [TaskStatus] ";

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
      
        + " Status, "
      
        + " Description "
      

      + " From [TaskStatus] ";
      public static List<TaskStatus> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<TaskStatus> rv = new List<TaskStatus>();

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

      public static List<TaskStatus> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TaskStatus> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TaskStatus> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskStatus));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TaskStatus item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TaskStatus>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskStatus));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TaskStatus> itemsList
      = new List<TaskStatus>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TaskStatus)
      itemsList.Add(deserializedObject as TaskStatus);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_status;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public TaskStatus(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public TaskStatus(
        int 
          iD,String 
          status,String 
          description
        )
        {
        
          m_iD = iD;
        
          m_status = status;
        
          m_description = description;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String Status
        {
        get { return m_status;}
        set { m_status = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
      }
      #endregion
      }

    