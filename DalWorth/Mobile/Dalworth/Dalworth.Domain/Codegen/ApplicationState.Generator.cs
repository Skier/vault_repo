
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


      public partial class ApplicationState : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [ApplicationState] ( " +
      
        " ID, " +
      
        " State, " +
      
        " Description " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @State, " +
      
        " @Description " +
      
      ")";

      public static void Insert(ApplicationState applicationState, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", applicationState.ID);
      
        Database.PutParameter(dbCommand,"@State", applicationState.State);
      
        Database.PutParameter(dbCommand,"@Description", applicationState.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(ApplicationState applicationState)
      {
        Insert(applicationState, null);
      }

      public static void Insert(List<ApplicationState>  applicationStateList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(ApplicationState applicationState in  applicationStateList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", applicationState.ID);
      
        Database.PutParameter(dbCommand,"@State", applicationState.State);
      
        Database.PutParameter(dbCommand,"@Description", applicationState.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",applicationState.ID);
      
        Database.UpdateParameter(dbCommand,"@State",applicationState.State);
      
        Database.UpdateParameter(dbCommand,"@Description",applicationState.Description);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<ApplicationState>  applicationStateList)
      {
      Insert(applicationStateList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [ApplicationState] Set "
      
        + " State = @State, "
      
        + " Description = @Description "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(ApplicationState applicationState, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", applicationState.ID);
      
        Database.PutParameter(dbCommand,"@State", applicationState.State);
      
        Database.PutParameter(dbCommand,"@Description", applicationState.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ApplicationState applicationState)
      {
      Update(applicationState, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " State, "
      
        + " Description "
      

      + " From [ApplicationState] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static ApplicationState FindByPrimaryKey(
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
      throw new DataNotFoundException("ApplicationState not found, search by primary key");

      }

      public static ApplicationState FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(ApplicationState applicationState, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",applicationState.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ApplicationState applicationState)
      {
      return Exists(applicationState, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from ApplicationState";

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

      public static ApplicationState Load(IDataReader dataReader)
      {
      ApplicationState applicationState = new ApplicationState();

      applicationState.ID = dataReader.GetInt32(0);
          applicationState.State = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            applicationState.Description = dataReader.GetString(2);
          

      return applicationState;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [ApplicationState] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(ApplicationState applicationState, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", applicationState.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ApplicationState applicationState)
      {
      Delete(applicationState, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [ApplicationState] ";

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
      
        + " State, "
      
        + " Description "
      

      + " From [ApplicationState] ";
      public static List<ApplicationState> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<ApplicationState> rv = new List<ApplicationState>();

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

      public static List<ApplicationState> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ApplicationState> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<ApplicationState> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ApplicationState));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ApplicationState item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ApplicationState>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ApplicationState));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ApplicationState> itemsList
      = new List<ApplicationState>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ApplicationState)
      itemsList.Add(deserializedObject as ApplicationState);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_state;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public ApplicationState(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public ApplicationState(
        int 
          iD,String 
          state,String 
          description
        )
        {
        
          m_iD = iD;
        
          m_state = state;
        
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
        public String State
        {
        get { return m_state;}
        set { m_state = value; }
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

    