
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Servman.Data;
    using Servman.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Servman.Domain
      {

      public partial class Project : ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Project ( " +
      
        " LeadId, " +
      
        " CustomerId, " +
      
        " QbJobId " +
      
      ") Values (" +
      
        " ?LeadId, " +
      
        " ?CustomerId, " +
      
        " ?QbJobId " +
      
      ")";

      public static void Insert(Project project, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?LeadId", project.LeadId);
      
        Database.PutParameter(dbCommand,"?CustomerId", project.CustomerId);
      
        Database.PutParameter(dbCommand,"?QbJobId", project.QbJobId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        project.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Project project)
      {
        Insert(project, null);
      }


      public static void Insert(List<Project>  projectList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Project project in  projectList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?LeadId", project.LeadId);
      
        Database.PutParameter(dbCommand,"?CustomerId", project.CustomerId);
      
        Database.PutParameter(dbCommand,"?QbJobId", project.QbJobId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?LeadId",project.LeadId);
      
        Database.UpdateParameter(dbCommand,"?CustomerId",project.CustomerId);
      
        Database.UpdateParameter(dbCommand,"?QbJobId",project.QbJobId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        project.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Project>  projectList)
      {
        Insert(projectList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Project Set "
      
        + " LeadId = ?LeadId, "
      
        + " CustomerId = ?CustomerId, "
      
        + " QbJobId = ?QbJobId "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(Project project, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", project.Id);
      
        Database.PutParameter(dbCommand,"?LeadId", project.LeadId);
      
        Database.PutParameter(dbCommand,"?CustomerId", project.CustomerId);
      
        Database.PutParameter(dbCommand,"?QbJobId", project.QbJobId);
      

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

      
        + " Id, "
      
        + " LeadId, "
      
        + " CustomerId, "
      
        + " QbJobId "
      

      + " From Project "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static Project FindByPrimaryKey(
      int id, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Project not found, search by primary key");

      }

      public static Project FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Project project, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",project.Id);
      

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

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Project limit 1";

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

      public static Project Load(IDataReader dataReader, int offset)
      {
      Project project = new Project();

      project.Id = dataReader.GetInt32(0 + offset);
          project.LeadId = dataReader.GetInt32(1 + offset);
          project.CustomerId = dataReader.GetInt32(2 + offset);
          project.QbJobId = dataReader.GetString(3 + offset);
          

      return project;
      }

      public static Project Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Project "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(Project project, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", project.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Project project)
      {
        Delete(project, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Project ";

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

      
        + " Id, "
      
        + " LeadId, "
      
        + " CustomerId, "
      
        + " QbJobId "
      

      + " From Project ";
      public static List<Project> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
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
      
        protected int m_id;
      
        protected int m_leadId;
      
        protected int m_customerId;
      
        protected String m_qbJobId;
      
      #endregion

      #region Constructors
      public Project(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public Project(
        int 
          id,int 
          leadId,int 
          customerId,String 
          qbJobId
        ) : this()
        {
        
          m_id = id;
        
          m_leadId = leadId;
        
          m_customerId = customerId;
        
          m_qbJobId = qbJobId;
        
        }


      
      #endregion

      
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        public int LeadId
        {
        get { return m_leadId;}
        set { m_leadId = value; }
        }
      
        public int CustomerId
        {
        get { return m_customerId;}
        set { m_customerId = value; }
        }
      
        public String QbJobId
        {
        get { return m_qbJobId;}
        set { m_qbJobId = value; }
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

    