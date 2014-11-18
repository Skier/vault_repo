
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


      public partial class ProjectFeedbackRate : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectFeedbackRate ( " +
      
        " ID, " +
      
        " Rate " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Rate " +
      
      ")";

      public static void Insert(ProjectFeedbackRate projectFeedbackRate, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectFeedbackRate.ID);
      
        Database.PutParameter(dbCommand,"?Rate", projectFeedbackRate.Rate);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ProjectFeedbackRate projectFeedbackRate)
      {
        Insert(projectFeedbackRate, null);
      }


      public static void Insert(List<ProjectFeedbackRate>  projectFeedbackRateList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectFeedbackRate projectFeedbackRate in  projectFeedbackRateList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", projectFeedbackRate.ID);
      
        Database.PutParameter(dbCommand,"?Rate", projectFeedbackRate.Rate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",projectFeedbackRate.ID);
      
        Database.UpdateParameter(dbCommand,"?Rate",projectFeedbackRate.Rate);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ProjectFeedbackRate>  projectFeedbackRateList)
      {
        Insert(projectFeedbackRateList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectFeedbackRate Set "
      
        + " Rate = ?Rate "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(ProjectFeedbackRate projectFeedbackRate, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectFeedbackRate.ID);
      
        Database.PutParameter(dbCommand,"?Rate", projectFeedbackRate.Rate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectFeedbackRate projectFeedbackRate)
      {
        Update(projectFeedbackRate, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Rate "
      

      + " From ProjectFeedbackRate "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static ProjectFeedbackRate FindByPrimaryKey(
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
      throw new DataNotFoundException("ProjectFeedbackRate not found, search by primary key");

      }

      public static ProjectFeedbackRate FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectFeedbackRate projectFeedbackRate, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",projectFeedbackRate.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectFeedbackRate projectFeedbackRate)
      {
      return Exists(projectFeedbackRate, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectFeedbackRate limit 1";

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

      public static ProjectFeedbackRate Load(IDataReader dataReader, int offset)
      {
      ProjectFeedbackRate projectFeedbackRate = new ProjectFeedbackRate();

      projectFeedbackRate.ID = dataReader.GetInt32(0 + offset);
          projectFeedbackRate.Rate = dataReader.GetString(1 + offset);
          

      return projectFeedbackRate;
      }

      public static ProjectFeedbackRate Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectFeedbackRate "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(ProjectFeedbackRate projectFeedbackRate, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", projectFeedbackRate.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectFeedbackRate projectFeedbackRate)
      {
        Delete(projectFeedbackRate, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectFeedbackRate ";

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
      
        + " Rate "
      

      + " From ProjectFeedbackRate ";
      public static List<ProjectFeedbackRate> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectFeedbackRate> rv = new List<ProjectFeedbackRate>();

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

      public static List<ProjectFeedbackRate> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectFeedbackRate> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectFeedbackRate obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Rate == obj.Rate;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectFeedbackRate> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectFeedbackRate));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectFeedbackRate item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectFeedbackRate>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectFeedbackRate));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectFeedbackRate> itemsList
      = new List<ProjectFeedbackRate>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectFeedbackRate)
      itemsList.Add(deserializedObject as ProjectFeedbackRate);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_rate;
      
      #endregion

      #region Constructors
      public ProjectFeedbackRate(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public ProjectFeedbackRate(
        int 
          iD,String 
          rate
        ) : this()
        {
        
          m_iD = iD;
        
          m_rate = rate;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String Rate
        {
        get { return m_rate;}
        set { m_rate = value; }
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

    