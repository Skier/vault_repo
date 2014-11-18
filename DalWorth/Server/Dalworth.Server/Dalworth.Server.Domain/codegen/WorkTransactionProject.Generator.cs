
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


      public partial class WorkTransactionProject : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WorkTransactionProject ( " +
      
        " WorkTransactionId, " +
      
        " ProjectId, " +
      
        " IsModified, " +
      
        " IsCreated " +
      
      ") Values (" +
      
        " ?WorkTransactionId, " +
      
        " ?ProjectId, " +
      
        " ?IsModified, " +
      
        " ?IsCreated " +
      
      ")";

      public static void Insert(WorkTransactionProject workTransactionProject, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionProject.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?ProjectId", workTransactionProject.ProjectId);
      
        Database.PutParameter(dbCommand,"?IsModified", workTransactionProject.IsModified);
      
        Database.PutParameter(dbCommand,"?IsCreated", workTransactionProject.IsCreated);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(WorkTransactionProject workTransactionProject)
      {
        Insert(workTransactionProject, null);
      }


      public static void Insert(List<WorkTransactionProject>  workTransactionProjectList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionProject workTransactionProject in  workTransactionProjectList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionProject.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?ProjectId", workTransactionProject.ProjectId);
      
        Database.PutParameter(dbCommand,"?IsModified", workTransactionProject.IsModified);
      
        Database.PutParameter(dbCommand,"?IsCreated", workTransactionProject.IsCreated);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WorkTransactionId",workTransactionProject.WorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"?ProjectId",workTransactionProject.ProjectId);
      
        Database.UpdateParameter(dbCommand,"?IsModified",workTransactionProject.IsModified);
      
        Database.UpdateParameter(dbCommand,"?IsCreated",workTransactionProject.IsCreated);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<WorkTransactionProject>  workTransactionProjectList)
      {
        Insert(workTransactionProjectList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WorkTransactionProject Set "
      
        + " IsModified = ?IsModified, "
      
        + " IsCreated = ?IsCreated "
      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId and  "
        
          + " ProjectId = ?ProjectId "
        
      ;

      public static void Update(WorkTransactionProject workTransactionProject, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionProject.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?ProjectId", workTransactionProject.ProjectId);
      
        Database.PutParameter(dbCommand,"?IsModified", workTransactionProject.IsModified);
      
        Database.PutParameter(dbCommand,"?IsCreated", workTransactionProject.IsCreated);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionProject workTransactionProject)
      {
        Update(workTransactionProject, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " WorkTransactionId, "
      
        + " ProjectId, "
      
        + " IsModified, "
      
        + " IsCreated "
      

      + " From WorkTransactionProject "

      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId and  "
        
          + " ProjectId = ?ProjectId "
        
      ;

      public static WorkTransactionProject FindByPrimaryKey(
      int workTransactionId,int projectId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionId);
      
        Database.PutParameter(dbCommand,"?ProjectId", projectId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("WorkTransactionProject not found, search by primary key");

      }

      public static WorkTransactionProject FindByPrimaryKey(
      int workTransactionId,int projectId
      )
      {
      return FindByPrimaryKey(
      workTransactionId,projectId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WorkTransactionProject workTransactionProject, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId",workTransactionProject.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?ProjectId",workTransactionProject.ProjectId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionProject workTransactionProject)
      {
      return Exists(workTransactionProject, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WorkTransactionProject limit 1";

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

      public static WorkTransactionProject Load(IDataReader dataReader, int offset)
      {
      WorkTransactionProject workTransactionProject = new WorkTransactionProject();

      workTransactionProject.WorkTransactionId = dataReader.GetInt32(0 + offset);
          workTransactionProject.ProjectId = dataReader.GetInt32(1 + offset);
          workTransactionProject.IsModified = dataReader.GetBoolean(2 + offset);
          workTransactionProject.IsCreated = dataReader.GetBoolean(3 + offset);
          

      return workTransactionProject;
      }

      public static WorkTransactionProject Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WorkTransactionProject "

      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId and  "
        
          + " ProjectId = ?ProjectId "
        
      ;
      public static void Delete(WorkTransactionProject workTransactionProject, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionProject.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?ProjectId", workTransactionProject.ProjectId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionProject workTransactionProject)
      {
        Delete(workTransactionProject, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WorkTransactionProject ";

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

      
        + " WorkTransactionId, "
      
        + " ProjectId, "
      
        + " IsModified, "
      
        + " IsCreated "
      

      + " From WorkTransactionProject ";
      public static List<WorkTransactionProject> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WorkTransactionProject> rv = new List<WorkTransactionProject>();

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

      public static List<WorkTransactionProject> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionProject> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WorkTransactionProject obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return WorkTransactionId == obj.WorkTransactionId && ProjectId == obj.ProjectId && IsModified == obj.IsModified && IsCreated == obj.IsCreated;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionProject> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionProject));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionProject item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionProject>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionProject));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionProject> itemsList
      = new List<WorkTransactionProject>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionProject)
      itemsList.Add(deserializedObject as WorkTransactionProject);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_workTransactionId;
      
        protected int m_projectId;
      
        protected bool m_isModified;
      
        protected bool m_isCreated;
      
      #endregion

      #region Constructors
      public WorkTransactionProject(
      int 
          workTransactionId,int 
          projectId
      ) : this()
      {
      
        m_workTransactionId = workTransactionId;
      
        m_projectId = projectId;
      
      }

      


        public WorkTransactionProject(
        int 
          workTransactionId,int 
          projectId,bool 
          isModified,bool 
          isCreated
        ) : this()
        {
        
          m_workTransactionId = workTransactionId;
        
          m_projectId = projectId;
        
          m_isModified = isModified;
        
          m_isCreated = isCreated;
        
        }


      
      #endregion

      
        [XmlElement]
        public int WorkTransactionId
        {
        get { return m_workTransactionId;}
        set { m_workTransactionId = value; }
        }
      
        [XmlElement]
        public int ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
        }
      
        [XmlElement]
        public bool IsModified
        {
        get { return m_isModified;}
        set { m_isModified = value; }
        }
      
        [XmlElement]
        public bool IsCreated
        {
        get { return m_isCreated;}
        set { m_isCreated = value; }
        }
      

      public static int FieldsCount
      {
      get { return 4; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    