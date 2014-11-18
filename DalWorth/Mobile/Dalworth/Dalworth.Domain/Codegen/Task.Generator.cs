
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


      public partial class Task : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Task] ( " +
      
        " ID, " +
      
        " ServerId, " +
      
        " ProjectId, " +
      
        " VisitId, " +
      
        " TaskTypeId, " +
      
        " TaskStatusId, " +
      
        " Number, " +
      
        " Sequence, " +
      
        " CreateDate, " +
      
        " ServiceDate, " +
      
        " DurationMin, " +
      
        " Description, " +
      
        " Message, " +
      
        " Notes " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @ServerId, " +
      
        " @ProjectId, " +
      
        " @VisitId, " +
      
        " @TaskTypeId, " +
      
        " @TaskStatusId, " +
      
        " @Number, " +
      
        " @Sequence, " +
      
        " @CreateDate, " +
      
        " @ServiceDate, " +
      
        " @DurationMin, " +
      
        " @Description, " +
      
        " @Message, " +
      
        " @Notes " +
      
      ")";

      public static void Insert(Task task, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", task.ID);
      
        Database.PutParameter(dbCommand,"@ServerId", task.ServerId);
      
        Database.PutParameter(dbCommand,"@ProjectId", task.ProjectId);
      
        Database.PutParameter(dbCommand,"@VisitId", task.VisitId);
      
        Database.PutParameter(dbCommand,"@TaskTypeId", task.TaskTypeId);
      
        Database.PutParameter(dbCommand,"@TaskStatusId", task.TaskStatusId);
      
        Database.PutParameter(dbCommand,"@Number", task.Number);
      
        Database.PutParameter(dbCommand,"@Sequence", task.Sequence);
      
        Database.PutParameter(dbCommand,"@CreateDate", task.CreateDate);
      
        Database.PutParameter(dbCommand,"@ServiceDate", task.ServiceDate);
      
        Database.PutParameter(dbCommand,"@DurationMin", task.DurationMin);
      
        Database.PutParameter(dbCommand,"@Description", task.Description);
      
        Database.PutParameter(dbCommand,"@Message", task.Message);
      
        Database.PutParameter(dbCommand,"@Notes", task.Notes);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(Task task)
      {
        Insert(task, null);
      }

      public static void Insert(List<Task>  taskList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(Task task in  taskList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", task.ID);
      
        Database.PutParameter(dbCommand,"@ServerId", task.ServerId);
      
        Database.PutParameter(dbCommand,"@ProjectId", task.ProjectId);
      
        Database.PutParameter(dbCommand,"@VisitId", task.VisitId);
      
        Database.PutParameter(dbCommand,"@TaskTypeId", task.TaskTypeId);
      
        Database.PutParameter(dbCommand,"@TaskStatusId", task.TaskStatusId);
      
        Database.PutParameter(dbCommand,"@Number", task.Number);
      
        Database.PutParameter(dbCommand,"@Sequence", task.Sequence);
      
        Database.PutParameter(dbCommand,"@CreateDate", task.CreateDate);
      
        Database.PutParameter(dbCommand,"@ServiceDate", task.ServiceDate);
      
        Database.PutParameter(dbCommand,"@DurationMin", task.DurationMin);
      
        Database.PutParameter(dbCommand,"@Description", task.Description);
      
        Database.PutParameter(dbCommand,"@Message", task.Message);
      
        Database.PutParameter(dbCommand,"@Notes", task.Notes);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",task.ID);
      
        Database.UpdateParameter(dbCommand,"@ServerId",task.ServerId);
      
        Database.UpdateParameter(dbCommand,"@ProjectId",task.ProjectId);
      
        Database.UpdateParameter(dbCommand,"@VisitId",task.VisitId);
      
        Database.UpdateParameter(dbCommand,"@TaskTypeId",task.TaskTypeId);
      
        Database.UpdateParameter(dbCommand,"@TaskStatusId",task.TaskStatusId);
      
        Database.UpdateParameter(dbCommand,"@Number",task.Number);
      
        Database.UpdateParameter(dbCommand,"@Sequence",task.Sequence);
      
        Database.UpdateParameter(dbCommand,"@CreateDate",task.CreateDate);
      
        Database.UpdateParameter(dbCommand,"@ServiceDate",task.ServiceDate);
      
        Database.UpdateParameter(dbCommand,"@DurationMin",task.DurationMin);
      
        Database.UpdateParameter(dbCommand,"@Description",task.Description);
      
        Database.UpdateParameter(dbCommand,"@Message",task.Message);
      
        Database.UpdateParameter(dbCommand,"@Notes",task.Notes);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<Task>  taskList)
      {
      Insert(taskList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Task] Set "
      
        + " ServerId = @ServerId, "
      
        + " ProjectId = @ProjectId, "
      
        + " VisitId = @VisitId, "
      
        + " TaskTypeId = @TaskTypeId, "
      
        + " TaskStatusId = @TaskStatusId, "
      
        + " Number = @Number, "
      
        + " Sequence = @Sequence, "
      
        + " CreateDate = @CreateDate, "
      
        + " ServiceDate = @ServiceDate, "
      
        + " DurationMin = @DurationMin, "
      
        + " Description = @Description, "
      
        + " Message = @Message, "
      
        + " Notes = @Notes "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(Task task, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", task.ID);
      
        Database.PutParameter(dbCommand,"@ServerId", task.ServerId);
      
        Database.PutParameter(dbCommand,"@ProjectId", task.ProjectId);
      
        Database.PutParameter(dbCommand,"@VisitId", task.VisitId);
      
        Database.PutParameter(dbCommand,"@TaskTypeId", task.TaskTypeId);
      
        Database.PutParameter(dbCommand,"@TaskStatusId", task.TaskStatusId);
      
        Database.PutParameter(dbCommand,"@Number", task.Number);
      
        Database.PutParameter(dbCommand,"@Sequence", task.Sequence);
      
        Database.PutParameter(dbCommand,"@CreateDate", task.CreateDate);
      
        Database.PutParameter(dbCommand,"@ServiceDate", task.ServiceDate);
      
        Database.PutParameter(dbCommand,"@DurationMin", task.DurationMin);
      
        Database.PutParameter(dbCommand,"@Description", task.Description);
      
        Database.PutParameter(dbCommand,"@Message", task.Message);
      
        Database.PutParameter(dbCommand,"@Notes", task.Notes);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Task task)
      {
      Update(task, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ServerId, "
      
        + " ProjectId, "
      
        + " VisitId, "
      
        + " TaskTypeId, "
      
        + " TaskStatusId, "
      
        + " Number, "
      
        + " Sequence, "
      
        + " CreateDate, "
      
        + " ServiceDate, "
      
        + " DurationMin, "
      
        + " Description, "
      
        + " Message, "
      
        + " Notes "
      

      + " From [Task] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static Task FindByPrimaryKey(
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
      throw new DataNotFoundException("Task not found, search by primary key");

      }

      public static Task FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(Task task, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",task.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Task task)
      {
      return Exists(task, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from Task";

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

      public static Task Load(IDataReader dataReader)
      {
      Task task = new Task();

      task.ID = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            task.ServerId = dataReader.GetInt32(1);
          task.ProjectId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            task.VisitId = dataReader.GetInt32(3);
          task.TaskTypeId = dataReader.GetInt32(4);
          
            if(!dataReader.IsDBNull(5))
            task.TaskStatusId = dataReader.GetInt32(5);
          
            if(!dataReader.IsDBNull(6))
            task.Number = dataReader.GetString(6);
          
            if(!dataReader.IsDBNull(7))
            task.Sequence = dataReader.GetInt32(7);
          
            if(!dataReader.IsDBNull(8))
            task.CreateDate = dataReader.GetDateTime(8);
          
            if(!dataReader.IsDBNull(9))
            task.ServiceDate = dataReader.GetDateTime(9);
          
            if(!dataReader.IsDBNull(10))
            task.DurationMin = dataReader.GetInt32(10);
          
            if(!dataReader.IsDBNull(11))
            task.Description = dataReader.GetString(11);
          
            if(!dataReader.IsDBNull(12))
            task.Message = dataReader.GetString(12);
          
            if(!dataReader.IsDBNull(13))
            task.Notes = dataReader.GetString(13);
          

      return task;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Task] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(Task task, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", task.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Task task)
      {
      Delete(task, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Task] ";

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
      
        + " ServerId, "
      
        + " ProjectId, "
      
        + " VisitId, "
      
        + " TaskTypeId, "
      
        + " TaskStatusId, "
      
        + " Number, "
      
        + " Sequence, "
      
        + " CreateDate, "
      
        + " ServiceDate, "
      
        + " DurationMin, "
      
        + " Description, "
      
        + " Message, "
      
        + " Notes "
      

      + " From [Task] ";
      public static List<Task> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<Task> rv = new List<Task>();

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

      public static List<Task> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Task> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Task> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Task));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Task item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Task>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Task));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Task> itemsList
      = new List<Task>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Task)
      itemsList.Add(deserializedObject as Task);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_serverId;
      
        protected int m_projectId;
      
        protected int? m_visitId;
      
        protected int m_taskTypeId;
      
        protected int? m_taskStatusId;
      
        protected String m_number;
      
        protected int? m_sequence;
      
        protected DateTime? m_createDate;
      
        protected DateTime? m_serviceDate;
      
        protected int? m_durationMin;
      
        protected String m_description;
      
        protected String m_message;
      
        protected String m_notes;
      
      #endregion

      #region Constructors
      public Task(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public Task(
        int 
          iD,int? 
          serverId,int 
          projectId,int? 
          visitId,int 
          taskTypeId,int? 
          taskStatusId,String 
          number,int? 
          sequence,DateTime? 
          createDate,DateTime? 
          serviceDate,int? 
          durationMin,String 
          description,String 
          message,String 
          notes
        )
        {
        
          m_iD = iD;
        
          m_serverId = serverId;
        
          m_projectId = projectId;
        
          m_visitId = visitId;
        
          m_taskTypeId = taskTypeId;
        
          m_taskStatusId = taskStatusId;
        
          m_number = number;
        
          m_sequence = sequence;
        
          m_createDate = createDate;
        
          m_serviceDate = serviceDate;
        
          m_durationMin = durationMin;
        
          m_description = description;
        
          m_message = message;
        
          m_notes = notes;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int? ServerId
        {
        get { return m_serverId;}
        set { m_serverId = value; }
        }
      
        [XmlElement]
        public int ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
        }
      
        [XmlElement]
        public int? VisitId
        {
        get { return m_visitId;}
        set { m_visitId = value; }
        }
      
        [XmlElement]
        public int TaskTypeId
        {
        get { return m_taskTypeId;}
        set { m_taskTypeId = value; }
        }
      
        [XmlElement]
        public int? TaskStatusId
        {
        get { return m_taskStatusId;}
        set { m_taskStatusId = value; }
        }
      
        [XmlElement]
        public String Number
        {
        get { return m_number;}
        set { m_number = value; }
        }
      
        [XmlElement]
        public int? Sequence
        {
        get { return m_sequence;}
        set { m_sequence = value; }
        }
      
        [XmlElement]
        public DateTime? CreateDate
        {
        get { return m_createDate;}
        set { m_createDate = value; }
        }
      
        [XmlElement]
        public DateTime? ServiceDate
        {
        get { return m_serviceDate;}
        set { m_serviceDate = value; }
        }
      
        [XmlElement]
        public int? DurationMin
        {
        get { return m_durationMin;}
        set { m_durationMin = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
        [XmlElement]
        public String Message
        {
        get { return m_message;}
        set { m_message = value; }
        }
      
        [XmlElement]
        public String Notes
        {
        get { return m_notes;}
        set { m_notes = value; }
        }
      
      }
      #endregion
      }

    