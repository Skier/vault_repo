
    using System;
    using System.Data;
    using System.Collections.Generic;
    using TractInc.Server.Data;
    using TractInc.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace TractInc.Server.Domain
      {


      public partial class State
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [State] ( " +
      
        " StateId, " +
      
        " Name, " +
      
        " StateFips, " +
      
        " StateAbbr " +
      
      ") Values (" +
      
        " @StateId, " +
      
        " @Name, " +
      
        " @StateFips, " +
      
        " @StateAbbr " +
      
      ")";

      public static void Insert(State state)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@StateId", state.StateId);
      
        Database.PutParameter(dbCommand,"@Name", state.Name);
      
        Database.PutParameter(dbCommand,"@StateFips", state.StateFips);
      
        Database.PutParameter(dbCommand,"@StateAbbr", state.StateAbbr);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<State>  stateList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(State state in  stateList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@StateId", state.StateId);
      
        Database.PutParameter(dbCommand,"@Name", state.Name);
      
        Database.PutParameter(dbCommand,"@StateFips", state.StateFips);
      
        Database.PutParameter(dbCommand,"@StateAbbr", state.StateAbbr);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@StateId",state.StateId);
      
        Database.UpdateParameter(dbCommand,"@Name",state.Name);
      
        Database.UpdateParameter(dbCommand,"@StateFips",state.StateFips);
      
        Database.UpdateParameter(dbCommand,"@StateAbbr",state.StateAbbr);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [State] Set "
      
        + " Name = @Name, "
      
        + " StateFips = @StateFips, "
      
        + " StateAbbr = @StateAbbr "
      
        + " Where "
        
          + " StateId = @StateId "
        
      ;

      public static void Update(State state)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@StateId", state.StateId);
      
        Database.PutParameter(dbCommand,"@Name", state.Name);
      
        Database.PutParameter(dbCommand,"@StateFips", state.StateFips);
      
        Database.PutParameter(dbCommand,"@StateAbbr", state.StateAbbr);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " StateId, "
      
        + " Name, "
      
        + " StateFips, "
      
        + " StateAbbr "
      

      + " From [State] "

      
        + " Where "
        
          + " StateId = @StateId "
        
      ;

      public static State FindByPrimaryKey(
      int stateId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@StateId", stateId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("State not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(State state)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@StateId",state.StateId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData()
      {
      String sql = "select 1 from State";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static State Load(IDataReader dataReader)
      {
      State state = new State();

      state.StateId = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            state.Name = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            state.StateFips = dataReader.GetString(2);
          
            if(!dataReader.IsDBNull(3))
            state.StateAbbr = dataReader.GetString(3);
          

      return state;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [State] "

      
        + " Where "
        
          + " StateId = @StateId "
        
      ;
      public static void Delete(State state)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@StateId", state.StateId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [State] ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " StateId, "
      
        + " Name, "
      
        + " StateFips, "
      
        + " StateAbbr "
      

      + " From [State] ";
      public static List<State> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<State> rv = new List<State>();

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
      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<State> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<State> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(State));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(State item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<State>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(State));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<State> itemsList
      = new List<State>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is State)
      itemsList.Add(deserializedObject as State);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_stateId;
      
        protected String m_name;
      
        protected String m_stateFips;
      
        protected String m_stateAbbr;
      
      #endregion

      #region Constructors
      public State(
      int 
          stateId
      )
      {
      
        m_stateId = stateId;
      
      }

      


        public State(
        int 
          stateId,String 
          name,String 
          stateFips,String 
          stateAbbr
        )
        {
        
          m_stateId = stateId;
        
          m_name = name;
        
          m_stateFips = stateFips;
        
          m_stateAbbr = stateAbbr;
        
        }


      
      #endregion

      
        [XmlElement]
        public int StateId
        {
        get { return m_stateId;}
        set { m_stateId = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public String StateFips
        {
        get { return m_stateFips;}
        set { m_stateFips = value; }
        }
      
        [XmlElement]
        public String StateAbbr
        {
        get { return m_stateAbbr;}
        set { m_stateAbbr = value; }
        }
      
      }
      #endregion
      }

    