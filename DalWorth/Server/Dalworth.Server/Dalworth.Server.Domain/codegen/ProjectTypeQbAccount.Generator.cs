
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


      public partial class ProjectTypeQbAccount : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectTypeQbAccount ( " +
      
        " ProjectTypeId, " +
      
        " QbAccountListId, " +
      
        " IsDefault " +
      
      ") Values (" +
      
        " ?ProjectTypeId, " +
      
        " ?QbAccountListId, " +
      
        " ?IsDefault " +
      
      ")";

      public static void Insert(ProjectTypeQbAccount projectTypeQbAccount, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", projectTypeQbAccount.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", projectTypeQbAccount.QbAccountListId);
      
        Database.PutParameter(dbCommand,"?IsDefault", projectTypeQbAccount.IsDefault);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ProjectTypeQbAccount projectTypeQbAccount)
      {
        Insert(projectTypeQbAccount, null);
      }


      public static void Insert(List<ProjectTypeQbAccount>  projectTypeQbAccountList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectTypeQbAccount projectTypeQbAccount in  projectTypeQbAccountList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", projectTypeQbAccount.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", projectTypeQbAccount.QbAccountListId);
      
        Database.PutParameter(dbCommand,"?IsDefault", projectTypeQbAccount.IsDefault);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ProjectTypeId",projectTypeQbAccount.ProjectTypeId);
      
        Database.UpdateParameter(dbCommand,"?QbAccountListId",projectTypeQbAccount.QbAccountListId);
      
        Database.UpdateParameter(dbCommand,"?IsDefault",projectTypeQbAccount.IsDefault);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ProjectTypeQbAccount>  projectTypeQbAccountList)
      {
        Insert(projectTypeQbAccountList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectTypeQbAccount Set "
      
        + " IsDefault = ?IsDefault "
      
        + " Where "
        
          + " ProjectTypeId = ?ProjectTypeId and  "
        
          + " QbAccountListId = ?QbAccountListId "
        
      ;

      public static void Update(ProjectTypeQbAccount projectTypeQbAccount, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", projectTypeQbAccount.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", projectTypeQbAccount.QbAccountListId);
      
        Database.PutParameter(dbCommand,"?IsDefault", projectTypeQbAccount.IsDefault);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectTypeQbAccount projectTypeQbAccount)
      {
        Update(projectTypeQbAccount, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ProjectTypeId, "
      
        + " QbAccountListId, "
      
        + " IsDefault "
      

      + " From ProjectTypeQbAccount "

      
        + " Where "
        
          + " ProjectTypeId = ?ProjectTypeId and  "
        
          + " QbAccountListId = ?QbAccountListId "
        
      ;

      public static ProjectTypeQbAccount FindByPrimaryKey(
      int projectTypeId,String qbAccountListId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectTypeId", projectTypeId);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", qbAccountListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ProjectTypeQbAccount not found, search by primary key");

      }

      public static ProjectTypeQbAccount FindByPrimaryKey(
      int projectTypeId,String qbAccountListId
      )
      {
      return FindByPrimaryKey(
      projectTypeId,qbAccountListId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectTypeQbAccount projectTypeQbAccount, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectTypeId",projectTypeQbAccount.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?QbAccountListId",projectTypeQbAccount.QbAccountListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectTypeQbAccount projectTypeQbAccount)
      {
      return Exists(projectTypeQbAccount, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectTypeQbAccount limit 1";

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

      public static ProjectTypeQbAccount Load(IDataReader dataReader, int offset)
      {
      ProjectTypeQbAccount projectTypeQbAccount = new ProjectTypeQbAccount();

      projectTypeQbAccount.ProjectTypeId = dataReader.GetInt32(0 + offset);
          projectTypeQbAccount.QbAccountListId = dataReader.GetString(1 + offset);
          projectTypeQbAccount.IsDefault = dataReader.GetBoolean(2 + offset);
          

      return projectTypeQbAccount;
      }

      public static ProjectTypeQbAccount Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectTypeQbAccount "

      
        + " Where "
        
          + " ProjectTypeId = ?ProjectTypeId and  "
        
          + " QbAccountListId = ?QbAccountListId "
        
      ;
      public static void Delete(ProjectTypeQbAccount projectTypeQbAccount, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ProjectTypeId", projectTypeQbAccount.ProjectTypeId);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", projectTypeQbAccount.QbAccountListId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectTypeQbAccount projectTypeQbAccount)
      {
        Delete(projectTypeQbAccount, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectTypeQbAccount ";

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
      
        + " QbAccountListId, "
      
        + " IsDefault "
      

      + " From ProjectTypeQbAccount ";
      public static List<ProjectTypeQbAccount> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectTypeQbAccount> rv = new List<ProjectTypeQbAccount>();

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

      public static List<ProjectTypeQbAccount> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectTypeQbAccount> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectTypeQbAccount obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ProjectTypeId == obj.ProjectTypeId && QbAccountListId == obj.QbAccountListId && IsDefault == obj.IsDefault;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectTypeQbAccount> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectTypeQbAccount));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectTypeQbAccount item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectTypeQbAccount>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectTypeQbAccount));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectTypeQbAccount> itemsList
      = new List<ProjectTypeQbAccount>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectTypeQbAccount)
      itemsList.Add(deserializedObject as ProjectTypeQbAccount);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_projectTypeId;
      
        protected String m_qbAccountListId;
      
        protected bool m_isDefault;
      
      #endregion

      #region Constructors
      public ProjectTypeQbAccount(
      int 
          projectTypeId,String 
          qbAccountListId
      ) : this()
      {
      
        m_projectTypeId = projectTypeId;
      
        m_qbAccountListId = qbAccountListId;
      
      }

      


        public ProjectTypeQbAccount(
        int 
          projectTypeId,String 
          qbAccountListId,bool 
          isDefault
        ) : this()
        {
        
          m_projectTypeId = projectTypeId;
        
          m_qbAccountListId = qbAccountListId;
        
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
        public String QbAccountListId
        {
        get { return m_qbAccountListId;}
        set { m_qbAccountListId = value; }
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

    