
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


      public partial class ProjectType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectType ( " +
      
        " ID, " +
      
        " Type, " +
      
        " Description, " +
      
        " QbClassListId " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Type, " +
      
        " ?Description, " +
      
        " ?QbClassListId " +
      
      ")";

      public static void Insert(ProjectType projectType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectType.ID);
      
        Database.PutParameter(dbCommand,"?Type", projectType.Type);
      
        Database.PutParameter(dbCommand,"?Description", projectType.Description);
      
        Database.PutParameter(dbCommand,"?QbClassListId", projectType.QbClassListId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ProjectType projectType)
      {
        Insert(projectType, null);
      }


      public static void Insert(List<ProjectType>  projectTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectType projectType in  projectTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", projectType.ID);
      
        Database.PutParameter(dbCommand,"?Type", projectType.Type);
      
        Database.PutParameter(dbCommand,"?Description", projectType.Description);
      
        Database.PutParameter(dbCommand,"?QbClassListId", projectType.QbClassListId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",projectType.ID);
      
        Database.UpdateParameter(dbCommand,"?Type",projectType.Type);
      
        Database.UpdateParameter(dbCommand,"?Description",projectType.Description);
      
        Database.UpdateParameter(dbCommand,"?QbClassListId",projectType.QbClassListId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ProjectType>  projectTypeList)
      {
        Insert(projectTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectType Set "
      
        + " Type = ?Type, "
      
        + " Description = ?Description, "
      
        + " QbClassListId = ?QbClassListId "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(ProjectType projectType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectType.ID);
      
        Database.PutParameter(dbCommand,"?Type", projectType.Type);
      
        Database.PutParameter(dbCommand,"?Description", projectType.Description);
      
        Database.PutParameter(dbCommand,"?QbClassListId", projectType.QbClassListId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectType projectType)
      {
        Update(projectType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Type, "
      
        + " Description, "
      
        + " QbClassListId "
      

      + " From ProjectType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static ProjectType FindByPrimaryKey(
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
      throw new DataNotFoundException("ProjectType not found, search by primary key");

      }

      public static ProjectType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectType projectType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",projectType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectType projectType)
      {
      return Exists(projectType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectType limit 1";

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

      public static ProjectType Load(IDataReader dataReader, int offset)
      {
      ProjectType projectType = new ProjectType();

      projectType.ID = dataReader.GetInt32(0 + offset);
          projectType.Type = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            projectType.Description = dataReader.GetString(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            projectType.QbClassListId = dataReader.GetString(3 + offset);
          

      return projectType;
      }

      public static ProjectType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(ProjectType projectType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", projectType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectType projectType)
      {
        Delete(projectType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectType ";

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
      
        + " Type, "
      
        + " Description, "
      
        + " QbClassListId "
      

      + " From ProjectType ";
      public static List<ProjectType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectType> rv = new List<ProjectType>();

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

      public static List<ProjectType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectType obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Type == obj.Type && Description == obj.Description && QbClassListId == obj.QbClassListId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectType> itemsList
      = new List<ProjectType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectType)
      itemsList.Add(deserializedObject as ProjectType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_type;
      
        protected String m_description;
      
        protected String m_qbClassListId;
      
      #endregion

      #region Constructors
      public ProjectType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public ProjectType(
        int 
          iD,String 
          type,String 
          description,String 
          qbClassListId
        ) : this()
        {
        
          m_iD = iD;
        
          m_type = type;
        
          m_description = description;
        
          m_qbClassListId = qbClassListId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String Type
        {
        get { return m_type;}
        set { m_type = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      
        [XmlElement]
        public String QbClassListId
        {
        get { return m_qbClassListId;}
        set { m_qbClassListId = value; }
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

    