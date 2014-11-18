
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


      public partial class ProjectConstructionScopeType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectConstructionScopeType ( " +
      
        " ID, " +
      
        " ScopeType " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?ScopeType " +
      
      ")";

      public static void Insert(ProjectConstructionScopeType projectConstructionScopeType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectConstructionScopeType.ID);
      
        Database.PutParameter(dbCommand,"?ScopeType", projectConstructionScopeType.ScopeType);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ProjectConstructionScopeType projectConstructionScopeType)
      {
        Insert(projectConstructionScopeType, null);
      }


      public static void Insert(List<ProjectConstructionScopeType>  projectConstructionScopeTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectConstructionScopeType projectConstructionScopeType in  projectConstructionScopeTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", projectConstructionScopeType.ID);
      
        Database.PutParameter(dbCommand,"?ScopeType", projectConstructionScopeType.ScopeType);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",projectConstructionScopeType.ID);
      
        Database.UpdateParameter(dbCommand,"?ScopeType",projectConstructionScopeType.ScopeType);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ProjectConstructionScopeType>  projectConstructionScopeTypeList)
      {
        Insert(projectConstructionScopeTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectConstructionScopeType Set "
      
        + " ScopeType = ?ScopeType "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(ProjectConstructionScopeType projectConstructionScopeType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectConstructionScopeType.ID);
      
        Database.PutParameter(dbCommand,"?ScopeType", projectConstructionScopeType.ScopeType);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectConstructionScopeType projectConstructionScopeType)
      {
        Update(projectConstructionScopeType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ScopeType "
      

      + " From ProjectConstructionScopeType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static ProjectConstructionScopeType FindByPrimaryKey(
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
      throw new DataNotFoundException("ProjectConstructionScopeType not found, search by primary key");

      }

      public static ProjectConstructionScopeType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectConstructionScopeType projectConstructionScopeType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",projectConstructionScopeType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectConstructionScopeType projectConstructionScopeType)
      {
      return Exists(projectConstructionScopeType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectConstructionScopeType limit 1";

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

      public static ProjectConstructionScopeType Load(IDataReader dataReader, int offset)
      {
      ProjectConstructionScopeType projectConstructionScopeType = new ProjectConstructionScopeType();

      projectConstructionScopeType.ID = dataReader.GetInt32(0 + offset);
          projectConstructionScopeType.ScopeType = dataReader.GetString(1 + offset);
          

      return projectConstructionScopeType;
      }

      public static ProjectConstructionScopeType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectConstructionScopeType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(ProjectConstructionScopeType projectConstructionScopeType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", projectConstructionScopeType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectConstructionScopeType projectConstructionScopeType)
      {
        Delete(projectConstructionScopeType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectConstructionScopeType ";

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
      
        + " ScopeType "
      

      + " From ProjectConstructionScopeType ";
      public static List<ProjectConstructionScopeType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectConstructionScopeType> rv = new List<ProjectConstructionScopeType>();

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

      public static List<ProjectConstructionScopeType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectConstructionScopeType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectConstructionScopeType obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && ScopeType == obj.ScopeType;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectConstructionScopeType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectConstructionScopeType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectConstructionScopeType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectConstructionScopeType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectConstructionScopeType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectConstructionScopeType> itemsList
      = new List<ProjectConstructionScopeType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectConstructionScopeType)
      itemsList.Add(deserializedObject as ProjectConstructionScopeType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_scopeType;
      
      #endregion

      #region Constructors
      public ProjectConstructionScopeType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public ProjectConstructionScopeType(
        int 
          iD,String 
          scopeType
        ) : this()
        {
        
          m_iD = iD;
        
          m_scopeType = scopeType;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String ScopeType
        {
        get { return m_scopeType;}
        set { m_scopeType = value; }
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

    