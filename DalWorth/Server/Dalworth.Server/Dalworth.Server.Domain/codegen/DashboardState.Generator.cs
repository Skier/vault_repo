
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


      public partial class DashboardState : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into DashboardState ( " +
      
        " EmployeeId, " +
      
        " DateCreated " +
      
      ") Values (" +
      
        " ?EmployeeId, " +
      
        " ?DateCreated " +
      
      ")";

      public static void Insert(DashboardState dashboardState, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?EmployeeId", dashboardState.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", dashboardState.DateCreated);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        dashboardState.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(DashboardState dashboardState)
      {
        Insert(dashboardState, null);
      }


      public static void Insert(List<DashboardState>  dashboardStateList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(DashboardState dashboardState in  dashboardStateList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?EmployeeId", dashboardState.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", dashboardState.DateCreated);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?EmployeeId",dashboardState.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"?DateCreated",dashboardState.DateCreated);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        dashboardState.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<DashboardState>  dashboardStateList)
      {
        Insert(dashboardStateList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update DashboardState Set "
      
        + " EmployeeId = ?EmployeeId, "
      
        + " DateCreated = ?DateCreated "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(DashboardState dashboardState, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", dashboardState.ID);
      
        Database.PutParameter(dbCommand,"?EmployeeId", dashboardState.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", dashboardState.DateCreated);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(DashboardState dashboardState)
      {
        Update(dashboardState, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " EmployeeId, "
      
        + " DateCreated "
      

      + " From DashboardState "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static DashboardState FindByPrimaryKey(
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
      throw new DataNotFoundException("DashboardState not found, search by primary key");

      }

      public static DashboardState FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(DashboardState dashboardState, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",dashboardState.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(DashboardState dashboardState)
      {
      return Exists(dashboardState, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from DashboardState limit 1";

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

      public static DashboardState Load(IDataReader dataReader, int offset)
      {
      DashboardState dashboardState = new DashboardState();

      dashboardState.ID = dataReader.GetInt32(0 + offset);
          dashboardState.EmployeeId = dataReader.GetInt32(1 + offset);
          dashboardState.DateCreated = dataReader.GetDateTime(2 + offset);
          

      return dashboardState;
      }

      public static DashboardState Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From DashboardState "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(DashboardState dashboardState, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", dashboardState.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(DashboardState dashboardState)
      {
        Delete(dashboardState, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From DashboardState ";

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
      
        + " EmployeeId, "
      
        + " DateCreated "
      

      + " From DashboardState ";
      public static List<DashboardState> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<DashboardState> rv = new List<DashboardState>();

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

      public static List<DashboardState> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<DashboardState> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (DashboardState obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && EmployeeId == obj.EmployeeId && DateCreated == obj.DateCreated;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<DashboardState> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DashboardState));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(DashboardState item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<DashboardState>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(DashboardState));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<DashboardState> itemsList
      = new List<DashboardState>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is DashboardState)
      itemsList.Add(deserializedObject as DashboardState);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_employeeId;
      
        protected DateTime m_dateCreated;
      
      #endregion

      #region Constructors
      public DashboardState(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public DashboardState(
        int 
          iD,int 
          employeeId,DateTime 
          dateCreated
        ) : this()
        {
        
          m_iD = iD;
        
          m_employeeId = employeeId;
        
          m_dateCreated = dateCreated;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int EmployeeId
        {
        get { return m_employeeId;}
        set { m_employeeId = value; }
        }
      
        [XmlElement]
        public DateTime DateCreated
        {
        get { return m_dateCreated;}
        set { m_dateCreated = value; }
        }
      

      public static int FieldsCount
      {
      get { return 3; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    