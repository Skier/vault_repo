
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


      public partial class ProjectStatus
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [ProjectStatus] ( " +
      
        " StatusName " +
      
      ") Values (" +
      
        " @StatusName " +
      
      ")";

      public static void Insert(ProjectStatus projectStatus)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@StatusName", projectStatus.StatusName);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          projectStatus.ProjectStatusId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<ProjectStatus>  projectStatusList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(ProjectStatus projectStatus in  projectStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@StatusName", projectStatus.StatusName);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@StatusName",projectStatus.StatusName);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        projectStatus.ProjectStatusId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [ProjectStatus] Set "
      
        + " StatusName = @StatusName "
      
        + " Where "
        
          + " ProjectStatusId = @ProjectStatusId "
        
      ;

      public static void Update(ProjectStatus projectStatus)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@ProjectStatusId", projectStatus.ProjectStatusId);
      
        Database.PutParameter(dbCommand,"@StatusName", projectStatus.StatusName);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ProjectStatusId, "
      
        + " StatusName "
      

      + " From [ProjectStatus] "

      
        + " Where "
        
          + " ProjectStatusId = @ProjectStatusId "
        
      ;

      public static ProjectStatus FindByPrimaryKey(
      int projectStatusId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ProjectStatusId", projectStatusId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ProjectStatus not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(ProjectStatus projectStatus)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@ProjectStatusId",projectStatus.ProjectStatusId);
      

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
      String sql = "select 1 from ProjectStatus";

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

      public static ProjectStatus Load(IDataReader dataReader)
      {
      ProjectStatus projectStatus = new ProjectStatus();

      projectStatus.ProjectStatusId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            projectStatus.StatusName = dataReader.GetString(1);
          

      return projectStatus;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [ProjectStatus] "

      
        + " Where "
        
          + " ProjectStatusId = @ProjectStatusId "
        
      ;
      public static void Delete(ProjectStatus projectStatus)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@ProjectStatusId", projectStatus.ProjectStatusId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [ProjectStatus] ";

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

      
        + " ProjectStatusId, "
      
        + " StatusName "
      

      + " From [ProjectStatus] ";
      public static List<ProjectStatus> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<ProjectStatus> rv = new List<ProjectStatus>();

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
      List<ProjectStatus> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<ProjectStatus> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectStatus));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectStatus item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectStatus>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectStatus));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectStatus> itemsList
      = new List<ProjectStatus>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectStatus)
      itemsList.Add(deserializedObject as ProjectStatus);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_projectStatusId;
      
        protected String m_statusName;
      
      #endregion

      #region Constructors
      public ProjectStatus(
      int 
          projectStatusId
      )
      {
      
        m_projectStatusId = projectStatusId;
      
      }

      


        public ProjectStatus(
        int 
          projectStatusId,String 
          statusName
        )
        {
        
          m_projectStatusId = projectStatusId;
        
          m_statusName = statusName;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ProjectStatusId
        {
        get { return m_projectStatusId;}
        set { m_projectStatusId = value; }
        }
      
        [XmlElement]
        public String StatusName
        {
        get { return m_statusName;}
        set { m_statusName = value; }
        }
      
      }
      #endregion
      }

    