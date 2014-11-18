
    using System;
    using System.Data;
    using System.Collections.Generic;
    using TractInc.Server.Data;
    using TractInc.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace TractInc.Server.Domain
      {


      public partial class Project
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Project] ( " +
      
        " ContractId, " +
      
        " AccountId, " +
      
        " ProjectStatusId, " +
      
        " ProjectName, " +
      
        " ShortName, " +
      
        " Description, " +
      
        " CreatedBy " +
      
      ") Values (" +
      
        " @ContractId, " +
      
        " @AccountId, " +
      
        " @ProjectStatusId, " +
      
        " @ProjectName, " +
      
        " @ShortName, " +
      
        " @Description, " +
      
        " @CreatedBy " +
      
      ")";

      public static void Insert(Project project)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@ContractId", project.ContractId);
      
          Database.PutParameter(dbCommand,"@AccountId", (0 != project.AccountId ? project.AccountId : null));
      
        Database.PutParameter(dbCommand,"@ProjectStatusId", project.ProjectStatusId);
      
        Database.PutParameter(dbCommand,"@ProjectName", project.ProjectName);
      
        Database.PutParameter(dbCommand,"@ShortName", project.ShortName);
      
        Database.PutParameter(dbCommand,"@Description", project.Description);
      
        Database.PutParameter(dbCommand,"@CreatedBy", project.CreatedBy);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          project.ProjectId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<Project>  projectList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Project project in  projectList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ContractId", project.ContractId);
      
        Database.PutParameter(dbCommand,"@AccountId", project.AccountId);
      
        Database.PutParameter(dbCommand,"@ProjectStatusId", project.ProjectStatusId);
      
        Database.PutParameter(dbCommand,"@ProjectName", project.ProjectName);
      
        Database.PutParameter(dbCommand,"@ShortName", project.ShortName);
      
        Database.PutParameter(dbCommand,"@Description", project.Description);
      
        Database.PutParameter(dbCommand,"@CreatedBy", project.CreatedBy);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ContractId",project.ContractId);
      
        Database.UpdateParameter(dbCommand,"@AccountId",project.AccountId);
      
        Database.UpdateParameter(dbCommand,"@ProjectStatusId",project.ProjectStatusId);
      
        Database.UpdateParameter(dbCommand,"@ProjectName",project.ProjectName);
      
        Database.UpdateParameter(dbCommand,"@ShortName",project.ShortName);
      
        Database.UpdateParameter(dbCommand,"@Description",project.Description);
      
        Database.UpdateParameter(dbCommand,"@CreatedBy",project.CreatedBy);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        project.ProjectId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Project] Set "
      
        + " ContractId = @ContractId, "
      
        + " AccountId = @AccountId, "
      
        + " ProjectStatusId = @ProjectStatusId, "
      
        + " ProjectName = @ProjectName, "
      
        + " ShortName = @ShortName, "
      
        + " Description = @Description, "
      
        + " CreatedBy = @CreatedBy "
      
        + " Where "
        
          + " ProjectId = @ProjectId "
        
      ;

      public static void Update(Project project)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@ProjectId", project.ProjectId);
      
        Database.PutParameter(dbCommand,"@ContractId", project.ContractId);
      
        Database.PutParameter(dbCommand,"@AccountId", (0 != project.AccountId ? project.AccountId : null));
      
        Database.PutParameter(dbCommand,"@ProjectStatusId", project.ProjectStatusId);
      
        Database.PutParameter(dbCommand,"@ProjectName", project.ProjectName);
      
        Database.PutParameter(dbCommand,"@ShortName", project.ShortName);
      
        Database.PutParameter(dbCommand,"@Description", project.Description);
      
        Database.PutParameter(dbCommand,"@CreatedBy", project.CreatedBy);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ProjectId, "
      
        + " ContractId, "
      
        + " AccountId, "
      
        + " ProjectStatusId, "
      
        + " ProjectName, "
      
        + " ShortName, "
      
        + " Description, "
      
        + " CreatedBy "
      

      + " From [Project] "

      
        + " Where "
        
          + " ProjectId = @ProjectId "
        
      ;

      public static Project FindByPrimaryKey(
      int projectId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ProjectId", projectId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Project not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Project project)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ProjectId",project.ProjectId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData()
      {
      String sql = "select 1 from Project";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static Project Load(IDataReader dataReader)
      {
      Project project = new Project();

      project.ProjectId = dataReader.GetInt32(0);
          project.ContractId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            project.AccountId = dataReader.GetInt32(2);
          project.ProjectStatusId = dataReader.GetInt32(3);
          project.ProjectName = dataReader.GetString(4);
          project.ShortName = dataReader.GetString(5);
          project.Description = dataReader.GetString(6);
          project.CreatedBy = dataReader.GetString(7);
          

      return project;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Project] "

      
        + " Where "
        
          + " ProjectId = @ProjectId "
        
      ;
      public static void Delete(Project project)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@ProjectId", project.ProjectId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Project] ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " ProjectId, "
      
        + " ContractId, "
      
        + " AccountId, "
      
        + " ProjectStatusId, "
      
        + " ProjectName, "
      
        + " ShortName, "
      
        + " Description, "
      
        + " CreatedBy "
      

      + " From [Project] ";
      public static List<Project> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
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
      
        protected int m_projectId;
      
        protected int m_contractId;
      
        protected int? m_accountId;
      
        protected int m_projectStatusId;
      
        protected String m_projectName;
      
        protected String m_shortName;
      
        protected String m_description;
      
        protected String m_createdBy;
      
      #endregion

      #region Constructors
      public Project(
      int 
          projectId
      )
      {
      
        m_projectId = projectId;
      
      }

      


        public Project(
        int 
          projectId,int 
          contractId,int? 
          accountId,int 
          projectStatusId,String 
          projectName,String 
          shortName,String 
          description,String 
          createdBy
        )
        {
        
          m_projectId = projectId;
        
          m_contractId = contractId;
        
          m_accountId = accountId;
        
          m_projectStatusId = projectStatusId;
        
          m_projectName = projectName;
        
          m_shortName = shortName;
        
          m_description = description;
        
          m_createdBy = createdBy;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
        }
      
        [XmlElement]
        public int ContractId
        {
        get { return m_contractId;}
        set { m_contractId = value; }
        }
      
        [XmlElement]
        public int? AccountId
        {
        get { return m_accountId;}
        set { m_accountId = value; }
        }
      
        [XmlElement]
        public int ProjectStatusId
        {
        get { return m_projectStatusId;}
        set { m_projectStatusId = value; }
        }
      
        [XmlElement]
        public String ProjectName
        {
        get { return m_projectName;}
        set { m_projectName = value; }
        }
      
        [XmlElement]
        public String ShortName
        {
        get { return m_shortName;}
        set { m_shortName = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
        [XmlElement]
        public String CreatedBy
        {
        get { return m_createdBy;}
        set { m_createdBy = value; }
        }
      
      }
      #endregion
      }

    