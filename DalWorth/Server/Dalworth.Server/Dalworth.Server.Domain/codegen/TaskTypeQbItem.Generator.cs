
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


      public partial class TaskTypeQbItem : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TaskTypeQbItem ( " +
      
        " TaskTypeId, " +
      
        " QbItemListId " +
      
      ") Values (" +
      
        " ?TaskTypeId, " +
      
        " ?QbItemListId " +
      
      ")";

      public static void Insert(TaskTypeQbItem taskTypeQbItem, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TaskTypeId", taskTypeQbItem.TaskTypeId);
      
        Database.PutParameter(dbCommand,"?QbItemListId", taskTypeQbItem.QbItemListId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(TaskTypeQbItem taskTypeQbItem)
      {
        Insert(taskTypeQbItem, null);
      }


      public static void Insert(List<TaskTypeQbItem>  taskTypeQbItemList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TaskTypeQbItem taskTypeQbItem in  taskTypeQbItemList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TaskTypeId", taskTypeQbItem.TaskTypeId);
      
        Database.PutParameter(dbCommand,"?QbItemListId", taskTypeQbItem.QbItemListId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TaskTypeId",taskTypeQbItem.TaskTypeId);
      
        Database.UpdateParameter(dbCommand,"?QbItemListId",taskTypeQbItem.QbItemListId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<TaskTypeQbItem>  taskTypeQbItemList)
      {
        Insert(taskTypeQbItemList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TaskTypeQbItem Set "
      
        + " Where "
        
          + " TaskTypeId = ?TaskTypeId and  "
        
          + " QbItemListId = ?QbItemListId "
        
      ;

      public static void Update(TaskTypeQbItem taskTypeQbItem, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TaskTypeId", taskTypeQbItem.TaskTypeId);
      
        Database.PutParameter(dbCommand,"?QbItemListId", taskTypeQbItem.QbItemListId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TaskTypeQbItem taskTypeQbItem)
      {
        Update(taskTypeQbItem, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TaskTypeId, "
      
        + " QbItemListId "
      

      + " From TaskTypeQbItem "

      
        + " Where "
        
          + " TaskTypeId = ?TaskTypeId and  "
        
          + " QbItemListId = ?QbItemListId "
        
      ;

      public static TaskTypeQbItem FindByPrimaryKey(
      int taskTypeId,String qbItemListId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TaskTypeId", taskTypeId);
      
        Database.PutParameter(dbCommand,"?QbItemListId", qbItemListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("TaskTypeQbItem not found, search by primary key");

      }

      public static TaskTypeQbItem FindByPrimaryKey(
      int taskTypeId,String qbItemListId
      )
      {
      return FindByPrimaryKey(
      taskTypeId,qbItemListId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TaskTypeQbItem taskTypeQbItem, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TaskTypeId",taskTypeQbItem.TaskTypeId);
      
        Database.PutParameter(dbCommand,"?QbItemListId",taskTypeQbItem.QbItemListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TaskTypeQbItem taskTypeQbItem)
      {
      return Exists(taskTypeQbItem, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TaskTypeQbItem limit 1";

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

      public static TaskTypeQbItem Load(IDataReader dataReader, int offset)
      {
      TaskTypeQbItem taskTypeQbItem = new TaskTypeQbItem();

      taskTypeQbItem.TaskTypeId = dataReader.GetInt32(0 + offset);
          taskTypeQbItem.QbItemListId = dataReader.GetString(1 + offset);
          

      return taskTypeQbItem;
      }

      public static TaskTypeQbItem Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TaskTypeQbItem "

      
        + " Where "
        
          + " TaskTypeId = ?TaskTypeId and  "
        
          + " QbItemListId = ?QbItemListId "
        
      ;
      public static void Delete(TaskTypeQbItem taskTypeQbItem, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TaskTypeId", taskTypeQbItem.TaskTypeId);
      
        Database.PutParameter(dbCommand,"?QbItemListId", taskTypeQbItem.QbItemListId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TaskTypeQbItem taskTypeQbItem)
      {
        Delete(taskTypeQbItem, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TaskTypeQbItem ";

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

      
        + " TaskTypeId, "
      
        + " QbItemListId "
      

      + " From TaskTypeQbItem ";
      public static List<TaskTypeQbItem> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TaskTypeQbItem> rv = new List<TaskTypeQbItem>();

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

      public static List<TaskTypeQbItem> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TaskTypeQbItem> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (TaskTypeQbItem obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return TaskTypeId == obj.TaskTypeId && QbItemListId == obj.QbItemListId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<TaskTypeQbItem> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskTypeQbItem));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TaskTypeQbItem item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TaskTypeQbItem>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TaskTypeQbItem));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TaskTypeQbItem> itemsList
      = new List<TaskTypeQbItem>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TaskTypeQbItem)
      itemsList.Add(deserializedObject as TaskTypeQbItem);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_taskTypeId;
      
        protected String m_qbItemListId;
      
      #endregion

      #region Constructors
      public TaskTypeQbItem(
      int 
          taskTypeId,String 
          qbItemListId
      ) : this()
      {
      
        m_taskTypeId = taskTypeId;
      
        m_qbItemListId = qbItemListId;
      
      }

      
      #endregion

      
        [XmlElement]
        public int TaskTypeId
        {
        get { return m_taskTypeId;}
        set { m_taskTypeId = value; }
        }
      
        [XmlElement]
        public String QbItemListId
        {
        get { return m_qbItemListId;}
        set { m_qbItemListId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 2; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    