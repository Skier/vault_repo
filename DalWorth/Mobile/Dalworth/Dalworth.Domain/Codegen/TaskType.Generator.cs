
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


      public partial class TaskType : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [TaskType] ( " +
      
        " ID, " +
      
        " Type, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @Type, " +
      
        " @Description " +
      
      ")";

      public static void Insert(TaskType taskType, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", taskType.ID);
      
        Database.PutParameter(dbCommand,"@Type", taskType.Type);
      
        Database.PutParameter(dbCommand,"@Description", taskType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(TaskType taskType)
      {
        Insert(taskType, null);
      }

      public static void Insert(List<TaskType>  taskTypeList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(TaskType taskType in  taskTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", taskType.ID);
      
        Database.PutParameter(dbCommand,"@Type", taskType.Type);
      
        Database.PutParameter(dbCommand,"@Description", taskType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",taskType.ID);
      
        Database.UpdateParameter(dbCommand,"@Type",taskType.Type);
      
        Database.UpdateParameter(dbCommand,"@Description",taskType.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<TaskType>  taskTypeList)
      {
      Insert(taskTypeList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [TaskType] Set "
      
        + " Type = @Type, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(TaskType taskType, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", taskType.ID);
      
        Database.PutParameter(dbCommand,"@Type", taskType.Type);
      
        Database.PutParameter(dbCommand,"@Description", taskType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TaskType taskType)
      {
      Update(taskType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Type, "
      
        + " Description "
      

      + " From [TaskType] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static TaskType FindByPrimaryKey(
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
      throw new DataNotFoundException("TaskType not found, search by primary key");

      }

      public static TaskType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(TaskType taskType, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",taskType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TaskType taskType)
      {
      return Exists(taskType, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from TaskType";

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

      public static TaskType Load(IDataReader dataReader)
      {
      TaskType taskType = new TaskType();

      taskType.ID = dataReader.GetInt32(0);
          taskType.Type = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            taskType.Description = dataReader.GetString(2);
          

      return taskType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [TaskType] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(TaskType taskType, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", taskType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TaskType taskType)
      {
      Delete(taskType, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [TaskType] ";

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
      
        + " Type, "
      
        + " Description "
      

      + " From [TaskType] ";
      public static List<TaskType> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<TaskType> rv = new List<TaskType>();

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

      public static List<TaskType> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TaskType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<TaskType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TaskType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TaskType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TaskType> itemsList
      = new List<TaskType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TaskType)
      itemsList.Add(deserializedObject as TaskType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_type;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public TaskType(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public TaskType(
        int 
          iD,String 
          type,String 
          description
        )
        {
        
          m_iD = iD;
        
          m_type = type;
        
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
        public String Type
        {
        get { return m_type;}
        set { m_type = value; }
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

    