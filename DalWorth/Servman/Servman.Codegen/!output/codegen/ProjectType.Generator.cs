
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

      public partial class ProjectType : ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectType ( " +
      
        " Name " +
      
      ") Values (" +
      
        " ?Name " +
      
      ")";

      public static void Insert(ProjectType projectType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?Name", projectType.Name);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        projectType.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

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
      
        Database.PutParameter(dbCommand,"?Name", projectType.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?Name",projectType.Name);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        projectType.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

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
      
        + " Name = ?Name "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(ProjectType projectType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", projectType.Id);
      
        Database.PutParameter(dbCommand,"?Name", projectType.Name);
      

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

      
        + " Id, "
      
        + " Name "
      

      + " From ProjectType "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static ProjectType FindByPrimaryKey(
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
      throw new DataNotFoundException("ProjectType not found, search by primary key");

      }

      public static ProjectType FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectType projectType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",projectType.Id);
      

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

      projectType.Id = dataReader.GetInt32(0 + offset);
          projectType.Name = dataReader.GetString(1 + offset);
          

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
        
          + " Id = ?Id "
        
      ;
      public static void Delete(ProjectType projectType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", projectType.Id);
      


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

      
        + " Id, "
      
        + " Name "
      

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
      
        protected int m_id;
      
        protected String m_name;
      
      #endregion

      #region Constructors
      public ProjectType(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public ProjectType(
        int 
          id,String 
          name
        ) : this()
        {
        
          m_id = id;
        
          m_name = name;
        
        }


      
      #endregion

      
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
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

    