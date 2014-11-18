
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


      public partial class ProjectConstructionProgress : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectConstructionProgress ( " +
      
        " ID, " +
      
        " Progress " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Progress " +
      
      ")";

      public static void Insert(ProjectConstructionProgress projectConstructionProgress, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectConstructionProgress.ID);
      
        Database.PutParameter(dbCommand,"?Progress", projectConstructionProgress.Progress);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ProjectConstructionProgress projectConstructionProgress)
      {
        Insert(projectConstructionProgress, null);
      }


      public static void Insert(List<ProjectConstructionProgress>  projectConstructionProgressList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectConstructionProgress projectConstructionProgress in  projectConstructionProgressList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", projectConstructionProgress.ID);
      
        Database.PutParameter(dbCommand,"?Progress", projectConstructionProgress.Progress);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",projectConstructionProgress.ID);
      
        Database.UpdateParameter(dbCommand,"?Progress",projectConstructionProgress.Progress);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ProjectConstructionProgress>  projectConstructionProgressList)
      {
        Insert(projectConstructionProgressList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectConstructionProgress Set "
      
        + " Progress = ?Progress "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(ProjectConstructionProgress projectConstructionProgress, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectConstructionProgress.ID);
      
        Database.PutParameter(dbCommand,"?Progress", projectConstructionProgress.Progress);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectConstructionProgress projectConstructionProgress)
      {
        Update(projectConstructionProgress, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Progress "
      

      + " From ProjectConstructionProgress "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static ProjectConstructionProgress FindByPrimaryKey(
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
      throw new DataNotFoundException("ProjectConstructionProgress not found, search by primary key");

      }

      public static ProjectConstructionProgress FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectConstructionProgress projectConstructionProgress, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",projectConstructionProgress.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectConstructionProgress projectConstructionProgress)
      {
      return Exists(projectConstructionProgress, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectConstructionProgress limit 1";

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

      public static ProjectConstructionProgress Load(IDataReader dataReader, int offset)
      {
      ProjectConstructionProgress projectConstructionProgress = new ProjectConstructionProgress();

      projectConstructionProgress.ID = dataReader.GetInt32(0 + offset);
          projectConstructionProgress.Progress = dataReader.GetString(1 + offset);
          

      return projectConstructionProgress;
      }

      public static ProjectConstructionProgress Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectConstructionProgress "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(ProjectConstructionProgress projectConstructionProgress, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", projectConstructionProgress.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectConstructionProgress projectConstructionProgress)
      {
        Delete(projectConstructionProgress, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectConstructionProgress ";

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
      
        + " Progress "
      

      + " From ProjectConstructionProgress ";
      public static List<ProjectConstructionProgress> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectConstructionProgress> rv = new List<ProjectConstructionProgress>();

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

      public static List<ProjectConstructionProgress> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectConstructionProgress> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectConstructionProgress obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Progress == obj.Progress;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectConstructionProgress> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectConstructionProgress));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectConstructionProgress item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectConstructionProgress>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectConstructionProgress));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectConstructionProgress> itemsList
      = new List<ProjectConstructionProgress>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectConstructionProgress)
      itemsList.Add(deserializedObject as ProjectConstructionProgress);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_progress;
      
      #endregion

      #region Constructors
      public ProjectConstructionProgress(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public ProjectConstructionProgress(
        int 
          iD,String 
          progress
        ) : this()
        {
        
          m_iD = iD;
        
          m_progress = progress;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String Progress
        {
        get { return m_progress;}
        set { m_progress = value; }
        }
      

      public static int FieldsCount
      {
      get { return 2; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    