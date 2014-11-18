
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


      public partial class VisitTask : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into VisitTask ( " +
      
        " VisitId, " +
      
        " TaskId " +
      
      ") Values (" +
      
        " ?VisitId, " +
      
        " ?TaskId " +
      
      ")";

      public static void Insert(VisitTask visitTask, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?VisitId", visitTask.VisitId);
      
        Database.PutParameter(dbCommand,"?TaskId", visitTask.TaskId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(VisitTask visitTask)
      {
        Insert(visitTask, null);
      }


      public static void Insert(List<VisitTask>  visitTaskList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(VisitTask visitTask in  visitTaskList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?VisitId", visitTask.VisitId);
      
        Database.PutParameter(dbCommand,"?TaskId", visitTask.TaskId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?VisitId",visitTask.VisitId);
      
        Database.UpdateParameter(dbCommand,"?TaskId",visitTask.TaskId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<VisitTask>  visitTaskList)
      {
        Insert(visitTaskList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update VisitTask Set "
      
        + " Where "
        
          + " VisitId = ?VisitId and  "
        
          + " TaskId = ?TaskId "
        
      ;

      public static void Update(VisitTask visitTask, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?VisitId", visitTask.VisitId);
      
        Database.PutParameter(dbCommand,"?TaskId", visitTask.TaskId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(VisitTask visitTask)
      {
        Update(visitTask, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " VisitId, "
      
        + " TaskId "
      

      + " From VisitTask "

      
        + " Where "
        
          + " VisitId = ?VisitId and  "
        
          + " TaskId = ?TaskId "
        
      ;

      public static VisitTask FindByPrimaryKey(
      int visitId,int taskId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?VisitId", visitId);
      
        Database.PutParameter(dbCommand,"?TaskId", taskId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("VisitTask not found, search by primary key");

      }

      public static VisitTask FindByPrimaryKey(
      int visitId,int taskId
      )
      {
      return FindByPrimaryKey(
      visitId,taskId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(VisitTask visitTask, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?VisitId",visitTask.VisitId);
      
        Database.PutParameter(dbCommand,"?TaskId",visitTask.TaskId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(VisitTask visitTask)
      {
      return Exists(visitTask, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from VisitTask limit 1";

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

      public static VisitTask Load(IDataReader dataReader, int offset)
      {
      VisitTask visitTask = new VisitTask();

      visitTask.VisitId = dataReader.GetInt32(0 + offset);
          visitTask.TaskId = dataReader.GetInt32(1 + offset);
          

      return visitTask;
      }

      public static VisitTask Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From VisitTask "

      
        + " Where "
        
          + " VisitId = ?VisitId and  "
        
          + " TaskId = ?TaskId "
        
      ;
      public static void Delete(VisitTask visitTask, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?VisitId", visitTask.VisitId);
      
        Database.PutParameter(dbCommand,"?TaskId", visitTask.TaskId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(VisitTask visitTask)
      {
        Delete(visitTask, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From VisitTask ";

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

      
        + " VisitId, "
      
        + " TaskId "
      

      + " From VisitTask ";
      public static List<VisitTask> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<VisitTask> rv = new List<VisitTask>();

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

      public static List<VisitTask> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<VisitTask> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (VisitTask obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return VisitId == obj.VisitId && TaskId == obj.TaskId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<VisitTask> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(VisitTask));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(VisitTask item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<VisitTask>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(VisitTask));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<VisitTask> itemsList
      = new List<VisitTask>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is VisitTask)
      itemsList.Add(deserializedObject as VisitTask);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_visitId;
      
        protected int m_taskId;
      
      #endregion

      #region Constructors
      public VisitTask(
      int 
          visitId,int 
          taskId
      ) : this()
      {
      
        m_visitId = visitId;
      
        m_taskId = taskId;
      
      }

      
      #endregion

      
        [XmlElement]
        public int VisitId
        {
        get { return m_visitId;}
        set { m_visitId = value; }
        }
      
        [XmlElement]
        public int TaskId
        {
        get { return m_taskId;}
        set { m_taskId = value; }
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

    