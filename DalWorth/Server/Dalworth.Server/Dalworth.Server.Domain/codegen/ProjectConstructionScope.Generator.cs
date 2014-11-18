
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


      public partial class ProjectConstructionScope : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectConstructionScope ( " +
      
        " ProjectId, " +
      
        " ProjectConstructionScopeTypeId, " +
      
        " ScopeDate, " +
      
        " JobType, " +
      
        " IsVoided, " +
      
        " Notes, " +
      
        " Amount " +
      
      ") Values (" +
      
        " ?ProjectId, " +
      
        " ?ProjectConstructionScopeTypeId, " +
      
        " ?ScopeDate, " +
      
        " ?JobType, " +
      
        " ?IsVoided, " +
      
        " ?Notes, " +
      
        " ?Amount " +
      
      ")";

      public static void Insert(ProjectConstructionScope projectConstructionScope, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectConstructionScope.ProjectId);
      
        Database.PutParameter(dbCommand,"?ProjectConstructionScopeTypeId", projectConstructionScope.ProjectConstructionScopeTypeId);
      
        Database.PutParameter(dbCommand,"?ScopeDate", projectConstructionScope.ScopeDate);
      
        Database.PutParameter(dbCommand,"?JobType", projectConstructionScope.JobType);
      
        Database.PutParameter(dbCommand,"?IsVoided", projectConstructionScope.IsVoided);
      
        Database.PutParameter(dbCommand,"?Notes", projectConstructionScope.Notes);
      
        Database.PutParameter(dbCommand,"?Amount", projectConstructionScope.Amount);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        projectConstructionScope.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(ProjectConstructionScope projectConstructionScope)
      {
        Insert(projectConstructionScope, null);
      }


      public static void Insert(List<ProjectConstructionScope>  projectConstructionScopeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectConstructionScope projectConstructionScope in  projectConstructionScopeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectConstructionScope.ProjectId);
      
        Database.PutParameter(dbCommand,"?ProjectConstructionScopeTypeId", projectConstructionScope.ProjectConstructionScopeTypeId);
      
        Database.PutParameter(dbCommand,"?ScopeDate", projectConstructionScope.ScopeDate);
      
        Database.PutParameter(dbCommand,"?JobType", projectConstructionScope.JobType);
      
        Database.PutParameter(dbCommand,"?IsVoided", projectConstructionScope.IsVoided);
      
        Database.PutParameter(dbCommand,"?Notes", projectConstructionScope.Notes);
      
        Database.PutParameter(dbCommand,"?Amount", projectConstructionScope.Amount);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ProjectId",projectConstructionScope.ProjectId);
      
        Database.UpdateParameter(dbCommand,"?ProjectConstructionScopeTypeId",projectConstructionScope.ProjectConstructionScopeTypeId);
      
        Database.UpdateParameter(dbCommand,"?ScopeDate",projectConstructionScope.ScopeDate);
      
        Database.UpdateParameter(dbCommand,"?JobType",projectConstructionScope.JobType);
      
        Database.UpdateParameter(dbCommand,"?IsVoided",projectConstructionScope.IsVoided);
      
        Database.UpdateParameter(dbCommand,"?Notes",projectConstructionScope.Notes);
      
        Database.UpdateParameter(dbCommand,"?Amount",projectConstructionScope.Amount);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        projectConstructionScope.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<ProjectConstructionScope>  projectConstructionScopeList)
      {
        Insert(projectConstructionScopeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectConstructionScope Set "
      
        + " ProjectId = ?ProjectId, "
      
        + " ProjectConstructionScopeTypeId = ?ProjectConstructionScopeTypeId, "
      
        + " ScopeDate = ?ScopeDate, "
      
        + " JobType = ?JobType, "
      
        + " IsVoided = ?IsVoided, "
      
        + " Notes = ?Notes, "
      
        + " Amount = ?Amount "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(ProjectConstructionScope projectConstructionScope, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectConstructionScope.ID);
      
        Database.PutParameter(dbCommand,"?ProjectId", projectConstructionScope.ProjectId);
      
        Database.PutParameter(dbCommand,"?ProjectConstructionScopeTypeId", projectConstructionScope.ProjectConstructionScopeTypeId);
      
        Database.PutParameter(dbCommand,"?ScopeDate", projectConstructionScope.ScopeDate);
      
        Database.PutParameter(dbCommand,"?JobType", projectConstructionScope.JobType);
      
        Database.PutParameter(dbCommand,"?IsVoided", projectConstructionScope.IsVoided);
      
        Database.PutParameter(dbCommand,"?Notes", projectConstructionScope.Notes);
      
        Database.PutParameter(dbCommand,"?Amount", projectConstructionScope.Amount);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectConstructionScope projectConstructionScope)
      {
        Update(projectConstructionScope, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ProjectId, "
      
        + " ProjectConstructionScopeTypeId, "
      
        + " ScopeDate, "
      
        + " JobType, "
      
        + " IsVoided, "
      
        + " Notes, "
      
        + " Amount "
      

      + " From ProjectConstructionScope "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static ProjectConstructionScope FindByPrimaryKey(
      int iD, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ProjectConstructionScope not found, search by primary key");

      }

      public static ProjectConstructionScope FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectConstructionScope projectConstructionScope, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",projectConstructionScope.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectConstructionScope projectConstructionScope)
      {
      return Exists(projectConstructionScope, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectConstructionScope limit 1";

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

      public static ProjectConstructionScope Load(IDataReader dataReader, int offset)
      {
      ProjectConstructionScope projectConstructionScope = new ProjectConstructionScope();

      projectConstructionScope.ID = dataReader.GetInt32(0 + offset);
          projectConstructionScope.ProjectId = dataReader.GetInt32(1 + offset);
          projectConstructionScope.ProjectConstructionScopeTypeId = dataReader.GetInt32(2 + offset);
          projectConstructionScope.ScopeDate = dataReader.GetDateTime(3 + offset);
          projectConstructionScope.JobType = dataReader.GetString(4 + offset);
          projectConstructionScope.IsVoided = dataReader.GetBoolean(5 + offset);
          projectConstructionScope.Notes = dataReader.GetString(6 + offset);
          projectConstructionScope.Amount = dataReader.GetDecimal(7 + offset);
          

      return projectConstructionScope;
      }

      public static ProjectConstructionScope Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectConstructionScope "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(ProjectConstructionScope projectConstructionScope, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", projectConstructionScope.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectConstructionScope projectConstructionScope)
      {
        Delete(projectConstructionScope, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectConstructionScope ";

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

      
        + " ID, "
      
        + " ProjectId, "
      
        + " ProjectConstructionScopeTypeId, "
      
        + " ScopeDate, "
      
        + " JobType, "
      
        + " IsVoided, "
      
        + " Notes, "
      
        + " Amount "
      

      + " From ProjectConstructionScope ";
      public static List<ProjectConstructionScope> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectConstructionScope> rv = new List<ProjectConstructionScope>();

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

      public static List<ProjectConstructionScope> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectConstructionScope> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectConstructionScope obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && ProjectId == obj.ProjectId && ProjectConstructionScopeTypeId == obj.ProjectConstructionScopeTypeId && ScopeDate == obj.ScopeDate && JobType == obj.JobType && IsVoided == obj.IsVoided && Notes == obj.Notes && Amount == obj.Amount;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectConstructionScope> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectConstructionScope));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectConstructionScope item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectConstructionScope>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectConstructionScope));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectConstructionScope> itemsList
      = new List<ProjectConstructionScope>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectConstructionScope)
      itemsList.Add(deserializedObject as ProjectConstructionScope);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_projectId;
      
        protected int m_projectConstructionScopeTypeId;
      
        protected DateTime m_scopeDate;
      
        protected String m_jobType;
      
        protected bool m_isVoided;
      
        protected String m_notes;
      
        protected decimal m_amount;
      
      #endregion

      #region Constructors
      public ProjectConstructionScope(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public ProjectConstructionScope(
        int 
          iD,int 
          projectId,int 
          projectConstructionScopeTypeId,DateTime 
          scopeDate,String 
          jobType,bool 
          isVoided,String 
          notes,decimal 
          amount
        ) : this()
        {
        
          m_iD = iD;
        
          m_projectId = projectId;
        
          m_projectConstructionScopeTypeId = projectConstructionScopeTypeId;
        
          m_scopeDate = scopeDate;
        
          m_jobType = jobType;
        
          m_isVoided = isVoided;
        
          m_notes = notes;
        
          m_amount = amount;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
        }
      
        [XmlElement]
        public int ProjectConstructionScopeTypeId
        {
        get { return m_projectConstructionScopeTypeId;}
        set { m_projectConstructionScopeTypeId = value; }
        }
      
        [XmlElement]
        public DateTime ScopeDate
        {
        get { return m_scopeDate;}
        set { m_scopeDate = value; }
        }
      
        [XmlElement]
        public String JobType
        {
        get { return m_jobType;}
        set { m_jobType = value; }
        }
      
        [XmlElement]
        public bool IsVoided
        {
        get { return m_isVoided;}
        set { m_isVoided = value; }
        }
      
        [XmlElement]
        public String Notes
        {
        get { return m_notes;}
        set { m_notes = value; }
        }
      
        [XmlElement]
        public decimal Amount
        {
        get { return m_amount;}
        set { m_amount = value; }
        }
      

      public static int FieldsCount
      {
      get { return 8; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    