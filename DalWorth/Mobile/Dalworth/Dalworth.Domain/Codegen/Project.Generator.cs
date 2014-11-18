
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


      public partial class Project : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Project] ( " +
      
        " ID, " +
      
        " CustomerId, " +
      
        " ServiceAddressId, " +
      
        " ProjectTypeId, " +
      
        " ProjectStatusId, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @CustomerId, " +
      
        " @ServiceAddressId, " +
      
        " @ProjectTypeId, " +
      
        " @ProjectStatusId, " +
      
        " @Description " +
      
      ")";

      public static void Insert(Project project, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", project.ID);
      
        Database.PutParameter(dbCommand,"@CustomerId", project.CustomerId);
      
        Database.PutParameter(dbCommand,"@ServiceAddressId", project.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"@ProjectTypeId", project.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"@ProjectStatusId", project.ProjectStatusId);
      
        Database.PutParameter(dbCommand,"@Description", project.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(Project project)
      {
        Insert(project, null);
      }

      public static void Insert(List<Project>  projectList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(Project project in  projectList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", project.ID);
      
        Database.PutParameter(dbCommand,"@CustomerId", project.CustomerId);
      
        Database.PutParameter(dbCommand,"@ServiceAddressId", project.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"@ProjectTypeId", project.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"@ProjectStatusId", project.ProjectStatusId);
      
        Database.PutParameter(dbCommand,"@Description", project.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",project.ID);
      
        Database.UpdateParameter(dbCommand,"@CustomerId",project.CustomerId);
      
        Database.UpdateParameter(dbCommand,"@ServiceAddressId",project.ServiceAddressId);
      
        Database.UpdateParameter(dbCommand,"@ProjectTypeId",project.ProjectTypeId);
      
        Database.UpdateParameter(dbCommand,"@ProjectStatusId",project.ProjectStatusId);
      
        Database.UpdateParameter(dbCommand,"@Description",project.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<Project>  projectList)
      {
      Insert(projectList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Project] Set "
      
        + " CustomerId = @CustomerId, "
      
        + " ServiceAddressId = @ServiceAddressId, "
      
        + " ProjectTypeId = @ProjectTypeId, "
      
        + " ProjectStatusId = @ProjectStatusId, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(Project project, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", project.ID);
      
        Database.PutParameter(dbCommand,"@CustomerId", project.CustomerId);
      
        Database.PutParameter(dbCommand,"@ServiceAddressId", project.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"@ProjectTypeId", project.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"@ProjectStatusId", project.ProjectStatusId);
      
        Database.PutParameter(dbCommand,"@Description", project.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Project project)
      {
      Update(project, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " CustomerId, "
      
        + " ServiceAddressId, "
      
        + " ProjectTypeId, "
      
        + " ProjectStatusId, "
      
        + " Description "
      

      + " From [Project] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static Project FindByPrimaryKey(
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
      throw new DataNotFoundException("Project not found, search by primary key");

      }

      public static Project FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(Project project, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",project.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Project project)
      {
      return Exists(project, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from Project";

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

      public static Project Load(IDataReader dataReader)
      {
      Project project = new Project();

      project.ID = dataReader.GetInt32(0);
          project.CustomerId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            project.ServiceAddressId = dataReader.GetInt32(2);
          project.ProjectTypeId = dataReader.GetInt32(3);
          
            if(!dataReader.IsDBNull(4))
            project.ProjectStatusId = dataReader.GetInt32(4);
          
            if(!dataReader.IsDBNull(5))
            project.Description = dataReader.GetString(5);
          

      return project;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Project] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(Project project, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", project.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Project project)
      {
      Delete(project, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Project] ";

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
      
        + " CustomerId, "
      
        + " ServiceAddressId, "
      
        + " ProjectTypeId, "
      
        + " ProjectStatusId, "
      
        + " Description "
      

      + " From [Project] ";
      public static List<Project> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<Project> rv = new List<Project>();

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

      public static List<Project> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Project> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Project> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Project));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Project item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Project>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Project));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Project> itemsList
      = new List<Project>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Project)
      itemsList.Add(deserializedObject as Project);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_customerId;
      
        protected int? m_serviceAddressId;
      
        protected int m_projectTypeId;
      
        protected int? m_projectStatusId;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public Project(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public Project(
        int 
          iD,int 
          customerId,int? 
          serviceAddressId,int 
          projectTypeId,int? 
          projectStatusId,String 
          description
        )
        {
        
          m_iD = iD;
        
          m_customerId = customerId;
        
          m_serviceAddressId = serviceAddressId;
        
          m_projectTypeId = projectTypeId;
        
          m_projectStatusId = projectStatusId;
        
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
        public int CustomerId
        {
        get { return m_customerId;}
        set { m_customerId = value; }
        }
      
        [XmlElement]
        public int? ServiceAddressId
        {
        get { return m_serviceAddressId;}
        set { m_serviceAddressId = value; }
        }
      
        [XmlElement]
        public int ProjectTypeId
        {
        get { return m_projectTypeId;}
        set { m_projectTypeId = value; }
        }
      
        [XmlElement]
        public int? ProjectStatusId
        {
        get { return m_projectStatusId;}
        set { m_projectStatusId = value; }
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

    