
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


      public partial class ProjectTypeQbTemplate : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectTypeQbTemplate ( " +
      
        " ProjectTypeId, " +
      
        " QbTemplateListId, " +
      
        " IsDefault " +
      
      ") Values (" +
      
        " ?ProjectTypeId, " +
      
        " ?QbTemplateListId, " +
      
        " ?IsDefault " +
      
      ")";

      public static void Insert(ProjectTypeQbTemplate projectTypeQbTemplate, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", projectTypeQbTemplate.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?QbTemplateListId", projectTypeQbTemplate.QbTemplateListId);
      
        Database.PutParameter(dbCommand,"?IsDefault", projectTypeQbTemplate.IsDefault);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ProjectTypeQbTemplate projectTypeQbTemplate)
      {
        Insert(projectTypeQbTemplate, null);
      }


      public static void Insert(List<ProjectTypeQbTemplate>  projectTypeQbTemplateList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectTypeQbTemplate projectTypeQbTemplate in  projectTypeQbTemplateList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", projectTypeQbTemplate.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?QbTemplateListId", projectTypeQbTemplate.QbTemplateListId);
      
        Database.PutParameter(dbCommand,"?IsDefault", projectTypeQbTemplate.IsDefault);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ProjectTypeId",projectTypeQbTemplate.ProjectTypeId);
      
        Database.UpdateParameter(dbCommand,"?QbTemplateListId",projectTypeQbTemplate.QbTemplateListId);
      
        Database.UpdateParameter(dbCommand,"?IsDefault",projectTypeQbTemplate.IsDefault);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ProjectTypeQbTemplate>  projectTypeQbTemplateList)
      {
        Insert(projectTypeQbTemplateList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectTypeQbTemplate Set "
      
        + " IsDefault = ?IsDefault "
      
        + " Where "
        
          + " ProjectTypeId = ?ProjectTypeId and  "
        
          + " QbTemplateListId = ?QbTemplateListId "
        
      ;

      public static void Update(ProjectTypeQbTemplate projectTypeQbTemplate, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", projectTypeQbTemplate.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?QbTemplateListId", projectTypeQbTemplate.QbTemplateListId);
      
        Database.PutParameter(dbCommand,"?IsDefault", projectTypeQbTemplate.IsDefault);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectTypeQbTemplate projectTypeQbTemplate)
      {
        Update(projectTypeQbTemplate, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ProjectTypeId, "
      
        + " QbTemplateListId, "
      
        + " IsDefault "
      

      + " From ProjectTypeQbTemplate "

      
        + " Where "
        
          + " ProjectTypeId = ?ProjectTypeId and  "
        
          + " QbTemplateListId = ?QbTemplateListId "
        
      ;

      public static ProjectTypeQbTemplate FindByPrimaryKey(
      int projectTypeId,String qbTemplateListId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", projectTypeId);
      
        Database.PutParameter(dbCommand,"?QbTemplateListId", qbTemplateListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ProjectTypeQbTemplate not found, search by primary key");

      }

      public static ProjectTypeQbTemplate FindByPrimaryKey(
      int projectTypeId,String qbTemplateListId
      )
      {
      return FindByPrimaryKey(
      projectTypeId,qbTemplateListId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectTypeQbTemplate projectTypeQbTemplate, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectTypeId",projectTypeQbTemplate.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?QbTemplateListId",projectTypeQbTemplate.QbTemplateListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectTypeQbTemplate projectTypeQbTemplate)
      {
      return Exists(projectTypeQbTemplate, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectTypeQbTemplate limit 1";

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

      public static ProjectTypeQbTemplate Load(IDataReader dataReader, int offset)
      {
      ProjectTypeQbTemplate projectTypeQbTemplate = new ProjectTypeQbTemplate();

      projectTypeQbTemplate.ProjectTypeId = dataReader.GetInt32(0 + offset);
          projectTypeQbTemplate.QbTemplateListId = dataReader.GetString(1 + offset);
          projectTypeQbTemplate.IsDefault = dataReader.GetBoolean(2 + offset);
          

      return projectTypeQbTemplate;
      }

      public static ProjectTypeQbTemplate Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectTypeQbTemplate "

      
        + " Where "
        
          + " ProjectTypeId = ?ProjectTypeId and  "
        
          + " QbTemplateListId = ?QbTemplateListId "
        
      ;
      public static void Delete(ProjectTypeQbTemplate projectTypeQbTemplate, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ProjectTypeId", projectTypeQbTemplate.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?QbTemplateListId", projectTypeQbTemplate.QbTemplateListId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectTypeQbTemplate projectTypeQbTemplate)
      {
        Delete(projectTypeQbTemplate, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectTypeQbTemplate ";

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

      
        + " ProjectTypeId, "
      
        + " QbTemplateListId, "
      
        + " IsDefault "
      

      + " From ProjectTypeQbTemplate ";
      public static List<ProjectTypeQbTemplate> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectTypeQbTemplate> rv = new List<ProjectTypeQbTemplate>();

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

      public static List<ProjectTypeQbTemplate> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectTypeQbTemplate> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectTypeQbTemplate obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ProjectTypeId == obj.ProjectTypeId && QbTemplateListId == obj.QbTemplateListId && IsDefault == obj.IsDefault;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectTypeQbTemplate> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectTypeQbTemplate));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectTypeQbTemplate item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectTypeQbTemplate>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectTypeQbTemplate));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectTypeQbTemplate> itemsList
      = new List<ProjectTypeQbTemplate>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectTypeQbTemplate)
      itemsList.Add(deserializedObject as ProjectTypeQbTemplate);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_projectTypeId;
      
        protected String m_qbTemplateListId;
      
        protected bool m_isDefault;
      
      #endregion

      #region Constructors
      public ProjectTypeQbTemplate(
      int 
          projectTypeId,String 
          qbTemplateListId
      ) : this()
      {
      
        m_projectTypeId = projectTypeId;
      
        m_qbTemplateListId = qbTemplateListId;
      
      }

      


        public ProjectTypeQbTemplate(
        int 
          projectTypeId,String 
          qbTemplateListId,bool 
          isDefault
        ) : this()
        {
        
          m_projectTypeId = projectTypeId;
        
          m_qbTemplateListId = qbTemplateListId;
        
          m_isDefault = isDefault;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ProjectTypeId
        {
        get { return m_projectTypeId;}
        set { m_projectTypeId = value; }
        }
      
        [XmlElement]
        public String QbTemplateListId
        {
        get { return m_qbTemplateListId;}
        set { m_qbTemplateListId = value; }
        }
      
        [XmlElement]
        public bool IsDefault
        {
        get { return m_isDefault;}
        set { m_isDefault = value; }
        }
      

      public static int FieldsCount
      {
      get { return 3; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    