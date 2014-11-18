
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


      public partial class Application : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Application] ( " +
      
        " ID, " +
      
        " ApplicationStateId, " +
      
        " WorkId " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @ApplicationStateId, " +
      
        " @WorkId " +
      
      ")";

      public static void Insert(Application application, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", application.ID);
      
        Database.PutParameter(dbCommand,"@ApplicationStateId", application.ApplicationStateId);
      
        Database.PutParameter(dbCommand,"@WorkId", application.WorkId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(Application application)
      {
        Insert(application, null);
      }

      public static void Insert(List<Application>  applicationList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(Application application in  applicationList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", application.ID);
      
        Database.PutParameter(dbCommand,"@ApplicationStateId", application.ApplicationStateId);
      
        Database.PutParameter(dbCommand,"@WorkId", application.WorkId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",application.ID);
      
        Database.UpdateParameter(dbCommand,"@ApplicationStateId",application.ApplicationStateId);
      
        Database.UpdateParameter(dbCommand,"@WorkId",application.WorkId);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<Application>  applicationList)
      {
      Insert(applicationList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Application] Set "
      
        + " ApplicationStateId = @ApplicationStateId, "
      
        + " WorkId = @WorkId "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(Application application, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", application.ID);
      
        Database.PutParameter(dbCommand,"@ApplicationStateId", application.ApplicationStateId);
      
        Database.PutParameter(dbCommand,"@WorkId", application.WorkId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Application application)
      {
      Update(application, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ApplicationStateId, "
      
        + " WorkId "
      

      + " From [Application] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static Application FindByPrimaryKey(
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
      throw new DataNotFoundException("Application not found, search by primary key");

      }

      public static Application FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(Application application, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",application.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Application application)
      {
      return Exists(application, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from Application";

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

      public static Application Load(IDataReader dataReader)
      {
      Application application = new Application();

      application.ID = dataReader.GetInt32(0);
          application.ApplicationStateId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            application.WorkId = dataReader.GetInt32(2);
          

      return application;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Application] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(Application application, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", application.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Application application)
      {
      Delete(application, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Application] ";

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
      
        + " ApplicationStateId, "
      
        + " WorkId "
      

      + " From [Application] ";
      public static List<Application> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<Application> rv = new List<Application>();

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

      public static List<Application> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Application> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Application> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Application));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Application item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Application>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Application));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Application> itemsList
      = new List<Application>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Application)
      itemsList.Add(deserializedObject as Application);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_applicationStateId;
      
        protected int? m_workId;
      
      #endregion

      #region Constructors
      public Application(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public Application(
        int 
          iD,int 
          applicationStateId,int? 
          workId
        )
        {
        
          m_iD = iD;
        
          m_applicationStateId = applicationStateId;
        
          m_workId = workId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int ApplicationStateId
        {
        get { return m_applicationStateId;}
        set { m_applicationStateId = value; }
        }
      
        [XmlElement]
        public int? WorkId
        {
        get { return m_workId;}
        set { m_workId = value; }
        }
      
      }
      #endregion
      }

    