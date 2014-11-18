
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using SmartSchedule.Data;
    using SmartSchedule.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace SmartSchedule.Domain
      {

      [DataContract]
      public partial class UserAction : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into UserAction ( " +
      
        " UserId, " +
      
        " UserActionTypeId, " +
      
        " TechnicianDefaultId, " +
      
        " TicketNumber, " +
      
        " DashboardDate, " +
      
        " ActionDate, " +
      
        " Text " +
      
      ") Values (" +
      
        " ?UserId, " +
      
        " ?UserActionTypeId, " +
      
        " ?TechnicianDefaultId, " +
      
        " ?TicketNumber, " +
      
        " ?DashboardDate, " +
      
        " ?ActionDate, " +
      
        " ?Text " +
      
      ")";

      public static void Insert(UserAction userAction, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?UserId", userAction.UserId);
      
        Database.PutParameter(dbCommand,"?UserActionTypeId", userAction.UserActionTypeId);
      
        Database.PutParameter(dbCommand,"?TechnicianDefaultId", userAction.TechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?TicketNumber", userAction.TicketNumber);
      
        Database.PutParameter(dbCommand,"?DashboardDate", userAction.DashboardDate);
      
        Database.PutParameter(dbCommand,"?ActionDate", userAction.ActionDate);
      
        Database.PutParameter(dbCommand,"?Text", userAction.Text);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        userAction.ID = Convert.ToInt64(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(UserAction userAction)
      {
        Insert(userAction, null);
      }


      public static void Insert(List<UserAction>  userActionList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(UserAction userAction in  userActionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?UserId", userAction.UserId);
      
        Database.PutParameter(dbCommand,"?UserActionTypeId", userAction.UserActionTypeId);
      
        Database.PutParameter(dbCommand,"?TechnicianDefaultId", userAction.TechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?TicketNumber", userAction.TicketNumber);
      
        Database.PutParameter(dbCommand,"?DashboardDate", userAction.DashboardDate);
      
        Database.PutParameter(dbCommand,"?ActionDate", userAction.ActionDate);
      
        Database.PutParameter(dbCommand,"?Text", userAction.Text);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?UserId",userAction.UserId);
      
        Database.UpdateParameter(dbCommand,"?UserActionTypeId",userAction.UserActionTypeId);
      
        Database.UpdateParameter(dbCommand,"?TechnicianDefaultId",userAction.TechnicianDefaultId);
      
        Database.UpdateParameter(dbCommand,"?TicketNumber",userAction.TicketNumber);
      
        Database.UpdateParameter(dbCommand,"?DashboardDate",userAction.DashboardDate);
      
        Database.UpdateParameter(dbCommand,"?ActionDate",userAction.ActionDate);
      
        Database.UpdateParameter(dbCommand,"?Text",userAction.Text);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        userAction.ID = Convert.ToInt64(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<UserAction>  userActionList)
      {
        Insert(userActionList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update UserAction Set "
      
        + " UserId = ?UserId, "
      
        + " UserActionTypeId = ?UserActionTypeId, "
      
        + " TechnicianDefaultId = ?TechnicianDefaultId, "
      
        + " TicketNumber = ?TicketNumber, "
      
        + " DashboardDate = ?DashboardDate, "
      
        + " ActionDate = ?ActionDate, "
      
        + " Text = ?Text "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(UserAction userAction, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", userAction.ID);
      
        Database.PutParameter(dbCommand,"?UserId", userAction.UserId);
      
        Database.PutParameter(dbCommand,"?UserActionTypeId", userAction.UserActionTypeId);
      
        Database.PutParameter(dbCommand,"?TechnicianDefaultId", userAction.TechnicianDefaultId);
      
        Database.PutParameter(dbCommand,"?TicketNumber", userAction.TicketNumber);
      
        Database.PutParameter(dbCommand,"?DashboardDate", userAction.DashboardDate);
      
        Database.PutParameter(dbCommand,"?ActionDate", userAction.ActionDate);
      
        Database.PutParameter(dbCommand,"?Text", userAction.Text);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(UserAction userAction)
      {
        Update(userAction, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " UserId, "
      
        + " UserActionTypeId, "
      
        + " TechnicianDefaultId, "
      
        + " TicketNumber, "
      
        + " DashboardDate, "
      
        + " ActionDate, "
      
        + " Text "
      

      + " From UserAction "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static UserAction FindByPrimaryKey(
      long iD, IDbConnection connection
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
      throw new DataNotFoundException("UserAction not found, search by primary key");

      }

      public static UserAction FindByPrimaryKey(
      long iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(UserAction userAction, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",userAction.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(UserAction userAction)
      {
      return Exists(userAction, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from UserAction limit 1";

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

      public static UserAction Load(IDataReader dataReader, int offset)
      {
      UserAction userAction = new UserAction();

      userAction.ID = dataReader.GetInt64(0 + offset);
          userAction.UserId = dataReader.GetInt32(1 + offset);
          userAction.UserActionTypeId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            userAction.TechnicianDefaultId = dataReader.GetInt32(3 + offset);
          userAction.TicketNumber = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            userAction.DashboardDate = dataReader.GetDateTime(5 + offset);
          userAction.ActionDate = dataReader.GetDateTime(6 + offset);
          userAction.Text = dataReader.GetString(7 + offset);
          

      return userAction;
      }

      public static UserAction Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From UserAction "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(UserAction userAction, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", userAction.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(UserAction userAction)
      {
        Delete(userAction, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From UserAction ";

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
      
        + " UserId, "
      
        + " UserActionTypeId, "
      
        + " TechnicianDefaultId, "
      
        + " TicketNumber, "
      
        + " DashboardDate, "
      
        + " ActionDate, "
      
        + " Text "
      

      + " From UserAction ";
      public static List<UserAction> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<UserAction> rv = new List<UserAction>();

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

      public static List<UserAction> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<UserAction> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<UserAction> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserAction));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(UserAction item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<UserAction>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserAction));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<UserAction> itemsList
      = new List<UserAction>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is UserAction)
      itemsList.Add(deserializedObject as UserAction);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected long m_iD;
      
        protected int m_userId;
      
        protected int m_userActionTypeId;
      
        protected int? m_technicianDefaultId;
      
        protected String m_ticketNumber;
      
        protected DateTime? m_dashboardDate;
      
        protected DateTime m_actionDate;
      
        protected String m_text;
      
      #endregion

      #region Constructors
      public UserAction(
      long 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public UserAction(
        long 
          iD,int 
          userId,int 
          userActionTypeId,int? 
          technicianDefaultId,String 
          ticketNumber,DateTime? 
          dashboardDate,DateTime 
          actionDate,String 
          text
        ) : this()
        {
        
          m_iD = iD;
        
          m_userId = userId;
        
          m_userActionTypeId = userActionTypeId;
        
          m_technicianDefaultId = technicianDefaultId;
        
          m_ticketNumber = ticketNumber;
        
          m_dashboardDate = dashboardDate;
        
          m_actionDate = actionDate;
        
          m_text = text;
        
        }


      
      #endregion

      
        [DataMember]
        public long ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [DataMember]
        public int UserId
        {
        get { return m_userId;}
        set { m_userId = value; }
        }
      
        [DataMember]
        public int UserActionTypeId
        {
        get { return m_userActionTypeId;}
        set { m_userActionTypeId = value; }
        }
      
        [DataMember]
        public int? TechnicianDefaultId
        {
        get { return m_technicianDefaultId;}
        set { m_technicianDefaultId = value; }
        }
      
        [DataMember]
        public String TicketNumber
        {
        get { return m_ticketNumber;}
        set { m_ticketNumber = value; }
        }
      
        [DataMember]
        public DateTime? DashboardDate
        {
        get { return m_dashboardDate;}
        set { m_dashboardDate = value; }
        }
      
        [DataMember]
        public DateTime ActionDate
        {
        get { return m_actionDate;}
        set { m_actionDate = value; }
        }
      
        [DataMember]
        public String Text
        {
        get { return m_text;}
        set { m_text = value; }
        }
      

      public static int FieldsCount
      {
      get { return 8; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    