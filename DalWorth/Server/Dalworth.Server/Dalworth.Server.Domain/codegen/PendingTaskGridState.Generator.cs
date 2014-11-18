
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


      public partial class PendingTaskGridState : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into PendingTaskGridState ( " +
      
        " EmployeeId, " +
      
        " DateCreated " +
      
      ") Values (" +
      
        " ?EmployeeId, " +
      
        " ?DateCreated " +
      
      ")";

      public static void Insert(PendingTaskGridState pendingTaskGridState, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?EmployeeId", pendingTaskGridState.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", pendingTaskGridState.DateCreated);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        pendingTaskGridState.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(PendingTaskGridState pendingTaskGridState)
      {
        Insert(pendingTaskGridState, null);
      }


      public static void Insert(List<PendingTaskGridState>  pendingTaskGridStateList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(PendingTaskGridState pendingTaskGridState in  pendingTaskGridStateList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?EmployeeId", pendingTaskGridState.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", pendingTaskGridState.DateCreated);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?EmployeeId",pendingTaskGridState.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"?DateCreated",pendingTaskGridState.DateCreated);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        pendingTaskGridState.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<PendingTaskGridState>  pendingTaskGridStateList)
      {
        Insert(pendingTaskGridStateList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update PendingTaskGridState Set "
      
        + " EmployeeId = ?EmployeeId, "
      
        + " DateCreated = ?DateCreated "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(PendingTaskGridState pendingTaskGridState, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", pendingTaskGridState.ID);
      
        Database.PutParameter(dbCommand,"?EmployeeId", pendingTaskGridState.EmployeeId);
      
        Database.PutParameter(dbCommand,"?DateCreated", pendingTaskGridState.DateCreated);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(PendingTaskGridState pendingTaskGridState)
      {
        Update(pendingTaskGridState, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " EmployeeId, "
      
        + " DateCreated "
      

      + " From PendingTaskGridState "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static PendingTaskGridState FindByPrimaryKey(
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
      throw new DataNotFoundException("PendingTaskGridState not found, search by primary key");

      }

      public static PendingTaskGridState FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(PendingTaskGridState pendingTaskGridState, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",pendingTaskGridState.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(PendingTaskGridState pendingTaskGridState)
      {
      return Exists(pendingTaskGridState, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from PendingTaskGridState limit 1";

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

      public static PendingTaskGridState Load(IDataReader dataReader, int offset)
      {
      PendingTaskGridState pendingTaskGridState = new PendingTaskGridState();

      pendingTaskGridState.ID = dataReader.GetInt32(0 + offset);
          pendingTaskGridState.EmployeeId = dataReader.GetInt32(1 + offset);
          pendingTaskGridState.DateCreated = dataReader.GetDateTime(2 + offset);
          

      return pendingTaskGridState;
      }

      public static PendingTaskGridState Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From PendingTaskGridState "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(PendingTaskGridState pendingTaskGridState, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", pendingTaskGridState.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(PendingTaskGridState pendingTaskGridState)
      {
        Delete(pendingTaskGridState, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From PendingTaskGridState ";

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
      

      + " From PendingTaskGridState ";
      public static List<PendingTaskGridState> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<PendingTaskGridState> rv = new List<PendingTaskGridState>();

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

      public static List<PendingTaskGridState> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<PendingTaskGridState> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (PendingTaskGridState obj)
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

      List<PendingTaskGridState> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PendingTaskGridState));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(PendingTaskGridState item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<PendingTaskGridState>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(PendingTaskGridState));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<PendingTaskGridState> itemsList
      = new List<PendingTaskGridState>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is PendingTaskGridState)
      itemsList.Add(deserializedObject as PendingTaskGridState);
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
      public PendingTaskGridState(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public PendingTaskGridState(
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

    