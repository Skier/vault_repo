
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


      public partial class ProjectType : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [ProjectType] ( " +
      
        " ID, " +
      
        " Type, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @Type, " +
      
        " @Description " +
      
      ")";

      public static void Insert(ProjectType projectType, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", projectType.ID);
      
        Database.PutParameter(dbCommand,"@Type", projectType.Type);
      
        Database.PutParameter(dbCommand,"@Description", projectType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(ProjectType projectType)
      {
        Insert(projectType, null);
      }

      public static void Insert(List<ProjectType>  projectTypeList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(ProjectType projectType in  projectTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", projectType.ID);
      
        Database.PutParameter(dbCommand,"@Type", projectType.Type);
      
        Database.PutParameter(dbCommand,"@Description", projectType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",projectType.ID);
      
        Database.UpdateParameter(dbCommand,"@Type",projectType.Type);
      
        Database.UpdateParameter(dbCommand,"@Description",projectType.Description);
      
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


      private const String SqlUpdate = "Update [ProjectType] Set "
      
        + " Type = @Type, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(ProjectType projectType, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", projectType.ID);
      
        Database.PutParameter(dbCommand,"@Type", projectType.Type);
      
        Database.PutParameter(dbCommand,"@Description", projectType.Description);
      

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
      
        + " Description "
      

      + " From [ProjectType] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static ProjectType FindByPrimaryKey(
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
      throw new DataNotFoundException("ProjectType not found, search by primary key");

      }

      public static ProjectType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(ProjectType projectType, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",projectType.ID);
      

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

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from ProjectType";

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

      public static ProjectType Load(IDataReader dataReader)
      {
      ProjectType projectType = new ProjectType();

      projectType.ID = dataReader.GetInt32(0);
          projectType.Type = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            projectType.Description = dataReader.GetString(2);
          

      return projectType;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [ProjectType] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(ProjectType projectType, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", projectType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectType projectType)
      {
      Delete(projectType, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [ProjectType] ";

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
      
        + " Type, "
      
        + " Description "
      

      + " From [ProjectType] ";
      public static List<ProjectType> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
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
      
        protected int m_iD;
      
        protected String m_type;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public ProjectType(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public ProjectType(
        int 
          iD,String 
          type,String 
          description
        )
        {
        
          m_iD = iD;
        
          m_type = type;
        
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
      
      }
      #endregion
      }

    