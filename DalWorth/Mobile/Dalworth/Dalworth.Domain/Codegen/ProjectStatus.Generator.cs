
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


      public partial class ProjectStatus : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [ProjectStatus] ( " +
      
        " ID, " +
      
        " Status, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @Status, " +
      
        " @Description " +
      
      ")";

      public static void Insert(ProjectStatus projectStatus, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", projectStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", projectStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", projectStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(ProjectStatus projectStatus)
      {
        Insert(projectStatus, null);
      }

      public static void Insert(List<ProjectStatus>  projectStatusList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(ProjectStatus projectStatus in  projectStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", projectStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", projectStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", projectStatus.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",projectStatus.ID);
      
        Database.UpdateParameter(dbCommand,"@Status",projectStatus.Status);
      
        Database.UpdateParameter(dbCommand,"@Description",projectStatus.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<ProjectStatus>  projectStatusList)
      {
      Insert(projectStatusList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [ProjectStatus] Set "
      
        + " Status = @Status, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(ProjectStatus projectStatus, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", projectStatus.ID);
      
        Database.PutParameter(dbCommand,"@Status", projectStatus.Status);
      
        Database.PutParameter(dbCommand,"@Description", projectStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectStatus projectStatus)
      {
      Update(projectStatus, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Status, "
      
        + " Description "
      

      + " From [ProjectStatus] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static ProjectStatus FindByPrimaryKey(
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
      throw new DataNotFoundException("ProjectStatus not found, search by primary key");

      }

      public static ProjectStatus FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(ProjectStatus projectStatus, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",projectStatus.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectStatus projectStatus)
      {
      return Exists(projectStatus, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from ProjectStatus";

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

      public static ProjectStatus Load(IDataReader dataReader)
      {
      ProjectStatus projectStatus = new ProjectStatus();

      projectStatus.ID = dataReader.GetInt32(0);
          projectStatus.Status = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            projectStatus.Description = dataReader.GetString(2);
          

      return projectStatus;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [ProjectStatus] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(ProjectStatus projectStatus, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", projectStatus.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectStatus projectStatus)
      {
      Delete(projectStatus, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [ProjectStatus] ";

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
      

      + " From [ProjectStatus] ";
      public static List<ProjectStatus> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
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

      public static List<ProjectStatus> Find()
      {
        return Find(null);
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
      
        protected int m_iD;
      
        protected String m_status;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public ProjectStatus(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public ProjectStatus(
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

    