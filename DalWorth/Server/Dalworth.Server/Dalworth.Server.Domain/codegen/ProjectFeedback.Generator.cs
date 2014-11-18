
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


      public partial class ProjectFeedback : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ProjectFeedback ( " +
      
        " ProjectId, " +
      
        " CustomerNote, " +
      
        " EditedCustomerNote, " +
      
        " DispatcherNote, " +
      
        " DateCreated, " +
      
        " DateReviewed, " +
      
        " ReviewedByEmployeeId, " +
      
        " CanBePostedOnWebSite, " +
      
        " CanBeUsedAsReferral, " +
      
        " IsSubscribeToMailingList, " +
      
        " RateId, " +
      
        " IsCallbackSelected, " +
      
        " CallbackDaysInterval, " +
      
        " IsDoNotCallSelected, " +
      
        " ReminderEmailSentDate, " +
      
        " ReminderPostCardSentDate " +
      
      ") Values (" +
      
        " ?ProjectId, " +
      
        " ?CustomerNote, " +
      
        " ?EditedCustomerNote, " +
      
        " ?DispatcherNote, " +
      
        " ?DateCreated, " +
      
        " ?DateReviewed, " +
      
        " ?ReviewedByEmployeeId, " +
      
        " ?CanBePostedOnWebSite, " +
      
        " ?CanBeUsedAsReferral, " +
      
        " ?IsSubscribeToMailingList, " +
      
        " ?RateId, " +
      
        " ?IsCallbackSelected, " +
      
        " ?CallbackDaysInterval, " +
      
        " ?IsDoNotCallSelected, " +
      
        " ?ReminderEmailSentDate, " +
      
        " ?ReminderPostCardSentDate " +
      
      ")";

      public static void Insert(ProjectFeedback projectFeedback, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectFeedback.ProjectId);
      
        Database.PutParameter(dbCommand,"?CustomerNote", projectFeedback.CustomerNote);
      
        Database.PutParameter(dbCommand,"?EditedCustomerNote", projectFeedback.EditedCustomerNote);
      
        Database.PutParameter(dbCommand,"?DispatcherNote", projectFeedback.DispatcherNote);
      
        Database.PutParameter(dbCommand,"?DateCreated", projectFeedback.DateCreated);
      
        Database.PutParameter(dbCommand,"?DateReviewed", projectFeedback.DateReviewed);
      
        Database.PutParameter(dbCommand,"?ReviewedByEmployeeId", projectFeedback.ReviewedByEmployeeId);
      
        Database.PutParameter(dbCommand,"?CanBePostedOnWebSite", projectFeedback.CanBePostedOnWebSite);
      
        Database.PutParameter(dbCommand,"?CanBeUsedAsReferral", projectFeedback.CanBeUsedAsReferral);
      
        Database.PutParameter(dbCommand,"?IsSubscribeToMailingList", projectFeedback.IsSubscribeToMailingList);
      
        Database.PutParameter(dbCommand,"?RateId", projectFeedback.RateId);
      
        Database.PutParameter(dbCommand,"?IsCallbackSelected", projectFeedback.IsCallbackSelected);
      
        Database.PutParameter(dbCommand,"?CallbackDaysInterval", projectFeedback.CallbackDaysInterval);
      
        Database.PutParameter(dbCommand,"?IsDoNotCallSelected", projectFeedback.IsDoNotCallSelected);
      
        Database.PutParameter(dbCommand,"?ReminderEmailSentDate", projectFeedback.ReminderEmailSentDate);
      
        Database.PutParameter(dbCommand,"?ReminderPostCardSentDate", projectFeedback.ReminderPostCardSentDate);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        projectFeedback.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(ProjectFeedback projectFeedback)
      {
        Insert(projectFeedback, null);
      }


      public static void Insert(List<ProjectFeedback>  projectFeedbackList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ProjectFeedback projectFeedback in  projectFeedbackList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ProjectId", projectFeedback.ProjectId);
      
        Database.PutParameter(dbCommand,"?CustomerNote", projectFeedback.CustomerNote);
      
        Database.PutParameter(dbCommand,"?EditedCustomerNote", projectFeedback.EditedCustomerNote);
      
        Database.PutParameter(dbCommand,"?DispatcherNote", projectFeedback.DispatcherNote);
      
        Database.PutParameter(dbCommand,"?DateCreated", projectFeedback.DateCreated);
      
        Database.PutParameter(dbCommand,"?DateReviewed", projectFeedback.DateReviewed);
      
        Database.PutParameter(dbCommand,"?ReviewedByEmployeeId", projectFeedback.ReviewedByEmployeeId);
      
        Database.PutParameter(dbCommand,"?CanBePostedOnWebSite", projectFeedback.CanBePostedOnWebSite);
      
        Database.PutParameter(dbCommand,"?CanBeUsedAsReferral", projectFeedback.CanBeUsedAsReferral);
      
        Database.PutParameter(dbCommand,"?IsSubscribeToMailingList", projectFeedback.IsSubscribeToMailingList);
      
        Database.PutParameter(dbCommand,"?RateId", projectFeedback.RateId);
      
        Database.PutParameter(dbCommand,"?IsCallbackSelected", projectFeedback.IsCallbackSelected);
      
        Database.PutParameter(dbCommand,"?CallbackDaysInterval", projectFeedback.CallbackDaysInterval);
      
        Database.PutParameter(dbCommand,"?IsDoNotCallSelected", projectFeedback.IsDoNotCallSelected);
      
        Database.PutParameter(dbCommand,"?ReminderEmailSentDate", projectFeedback.ReminderEmailSentDate);
      
        Database.PutParameter(dbCommand,"?ReminderPostCardSentDate", projectFeedback.ReminderPostCardSentDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ProjectId",projectFeedback.ProjectId);
      
        Database.UpdateParameter(dbCommand,"?CustomerNote",projectFeedback.CustomerNote);
      
        Database.UpdateParameter(dbCommand,"?EditedCustomerNote",projectFeedback.EditedCustomerNote);
      
        Database.UpdateParameter(dbCommand,"?DispatcherNote",projectFeedback.DispatcherNote);
      
        Database.UpdateParameter(dbCommand,"?DateCreated",projectFeedback.DateCreated);
      
        Database.UpdateParameter(dbCommand,"?DateReviewed",projectFeedback.DateReviewed);
      
        Database.UpdateParameter(dbCommand,"?ReviewedByEmployeeId",projectFeedback.ReviewedByEmployeeId);
      
        Database.UpdateParameter(dbCommand,"?CanBePostedOnWebSite",projectFeedback.CanBePostedOnWebSite);
      
        Database.UpdateParameter(dbCommand,"?CanBeUsedAsReferral",projectFeedback.CanBeUsedAsReferral);
      
        Database.UpdateParameter(dbCommand,"?IsSubscribeToMailingList",projectFeedback.IsSubscribeToMailingList);
      
        Database.UpdateParameter(dbCommand,"?RateId",projectFeedback.RateId);
      
        Database.UpdateParameter(dbCommand,"?IsCallbackSelected",projectFeedback.IsCallbackSelected);
      
        Database.UpdateParameter(dbCommand,"?CallbackDaysInterval",projectFeedback.CallbackDaysInterval);
      
        Database.UpdateParameter(dbCommand,"?IsDoNotCallSelected",projectFeedback.IsDoNotCallSelected);
      
        Database.UpdateParameter(dbCommand,"?ReminderEmailSentDate",projectFeedback.ReminderEmailSentDate);
      
        Database.UpdateParameter(dbCommand,"?ReminderPostCardSentDate",projectFeedback.ReminderPostCardSentDate);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        projectFeedback.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<ProjectFeedback>  projectFeedbackList)
      {
        Insert(projectFeedbackList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ProjectFeedback Set "
      
        + " ProjectId = ?ProjectId, "
      
        + " CustomerNote = ?CustomerNote, "
      
        + " EditedCustomerNote = ?EditedCustomerNote, "
      
        + " DispatcherNote = ?DispatcherNote, "
      
        + " DateCreated = ?DateCreated, "
      
        + " DateReviewed = ?DateReviewed, "
      
        + " ReviewedByEmployeeId = ?ReviewedByEmployeeId, "
      
        + " CanBePostedOnWebSite = ?CanBePostedOnWebSite, "
      
        + " CanBeUsedAsReferral = ?CanBeUsedAsReferral, "
      
        + " IsSubscribeToMailingList = ?IsSubscribeToMailingList, "
      
        + " RateId = ?RateId, "
      
        + " IsCallbackSelected = ?IsCallbackSelected, "
      
        + " CallbackDaysInterval = ?CallbackDaysInterval, "
      
        + " IsDoNotCallSelected = ?IsDoNotCallSelected, "
      
        + " ReminderEmailSentDate = ?ReminderEmailSentDate, "
      
        + " ReminderPostCardSentDate = ?ReminderPostCardSentDate "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(ProjectFeedback projectFeedback, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", projectFeedback.ID);
      
        Database.PutParameter(dbCommand,"?ProjectId", projectFeedback.ProjectId);
      
        Database.PutParameter(dbCommand,"?CustomerNote", projectFeedback.CustomerNote);
      
        Database.PutParameter(dbCommand,"?EditedCustomerNote", projectFeedback.EditedCustomerNote);
      
        Database.PutParameter(dbCommand,"?DispatcherNote", projectFeedback.DispatcherNote);
      
        Database.PutParameter(dbCommand,"?DateCreated", projectFeedback.DateCreated);
      
        Database.PutParameter(dbCommand,"?DateReviewed", projectFeedback.DateReviewed);
      
        Database.PutParameter(dbCommand,"?ReviewedByEmployeeId", projectFeedback.ReviewedByEmployeeId);
      
        Database.PutParameter(dbCommand,"?CanBePostedOnWebSite", projectFeedback.CanBePostedOnWebSite);
      
        Database.PutParameter(dbCommand,"?CanBeUsedAsReferral", projectFeedback.CanBeUsedAsReferral);
      
        Database.PutParameter(dbCommand,"?IsSubscribeToMailingList", projectFeedback.IsSubscribeToMailingList);
      
        Database.PutParameter(dbCommand,"?RateId", projectFeedback.RateId);
      
        Database.PutParameter(dbCommand,"?IsCallbackSelected", projectFeedback.IsCallbackSelected);
      
        Database.PutParameter(dbCommand,"?CallbackDaysInterval", projectFeedback.CallbackDaysInterval);
      
        Database.PutParameter(dbCommand,"?IsDoNotCallSelected", projectFeedback.IsDoNotCallSelected);
      
        Database.PutParameter(dbCommand,"?ReminderEmailSentDate", projectFeedback.ReminderEmailSentDate);
      
        Database.PutParameter(dbCommand,"?ReminderPostCardSentDate", projectFeedback.ReminderPostCardSentDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ProjectFeedback projectFeedback)
      {
        Update(projectFeedback, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ProjectId, "
      
        + " CustomerNote, "
      
        + " EditedCustomerNote, "
      
        + " DispatcherNote, "
      
        + " DateCreated, "
      
        + " DateReviewed, "
      
        + " ReviewedByEmployeeId, "
      
        + " CanBePostedOnWebSite, "
      
        + " CanBeUsedAsReferral, "
      
        + " IsSubscribeToMailingList, "
      
        + " RateId, "
      
        + " IsCallbackSelected, "
      
        + " CallbackDaysInterval, "
      
        + " IsDoNotCallSelected, "
      
        + " ReminderEmailSentDate, "
      
        + " ReminderPostCardSentDate "
      

      + " From ProjectFeedback "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static ProjectFeedback FindByPrimaryKey(
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
      throw new DataNotFoundException("ProjectFeedback not found, search by primary key");

      }

      public static ProjectFeedback FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ProjectFeedback projectFeedback, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",projectFeedback.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ProjectFeedback projectFeedback)
      {
      return Exists(projectFeedback, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ProjectFeedback limit 1";

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

      public static ProjectFeedback Load(IDataReader dataReader, int offset)
      {
      ProjectFeedback projectFeedback = new ProjectFeedback();

      projectFeedback.ID = dataReader.GetInt32(0 + offset);
          projectFeedback.ProjectId = dataReader.GetInt32(1 + offset);
          projectFeedback.CustomerNote = dataReader.GetString(2 + offset);
          projectFeedback.EditedCustomerNote = dataReader.GetString(3 + offset);
          projectFeedback.DispatcherNote = dataReader.GetString(4 + offset);
          projectFeedback.DateCreated = dataReader.GetDateTime(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            projectFeedback.DateReviewed = dataReader.GetDateTime(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            projectFeedback.ReviewedByEmployeeId = dataReader.GetInt32(7 + offset);
          projectFeedback.CanBePostedOnWebSite = dataReader.GetBoolean(8 + offset);
          projectFeedback.CanBeUsedAsReferral = dataReader.GetBoolean(9 + offset);
          projectFeedback.IsSubscribeToMailingList = dataReader.GetBoolean(10 + offset);
          projectFeedback.RateId = dataReader.GetInt32(11 + offset);
          projectFeedback.IsCallbackSelected = dataReader.GetBoolean(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            projectFeedback.CallbackDaysInterval = dataReader.GetInt32(13 + offset);
          projectFeedback.IsDoNotCallSelected = dataReader.GetBoolean(14 + offset);
          
            if(!dataReader.IsDBNull(15 + offset))
            projectFeedback.ReminderEmailSentDate = dataReader.GetDateTime(15 + offset);
          
            if(!dataReader.IsDBNull(16 + offset))
            projectFeedback.ReminderPostCardSentDate = dataReader.GetDateTime(16 + offset);
          

      return projectFeedback;
      }

      public static ProjectFeedback Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ProjectFeedback "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(ProjectFeedback projectFeedback, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", projectFeedback.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ProjectFeedback projectFeedback)
      {
        Delete(projectFeedback, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ProjectFeedback ";

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
      
        + " ProjectId, "
      
        + " CustomerNote, "
      
        + " EditedCustomerNote, "
      
        + " DispatcherNote, "
      
        + " DateCreated, "
      
        + " DateReviewed, "
      
        + " ReviewedByEmployeeId, "
      
        + " CanBePostedOnWebSite, "
      
        + " CanBeUsedAsReferral, "
      
        + " IsSubscribeToMailingList, "
      
        + " RateId, "
      
        + " IsCallbackSelected, "
      
        + " CallbackDaysInterval, "
      
        + " IsDoNotCallSelected, "
      
        + " ReminderEmailSentDate, "
      
        + " ReminderPostCardSentDate "
      

      + " From ProjectFeedback ";
      public static List<ProjectFeedback> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ProjectFeedback> rv = new List<ProjectFeedback>();

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

      public static List<ProjectFeedback> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ProjectFeedback> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ProjectFeedback obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && ProjectId == obj.ProjectId && CustomerNote == obj.CustomerNote && EditedCustomerNote == obj.EditedCustomerNote && DispatcherNote == obj.DispatcherNote && DateCreated == obj.DateCreated && DateReviewed == obj.DateReviewed && ReviewedByEmployeeId == obj.ReviewedByEmployeeId && CanBePostedOnWebSite == obj.CanBePostedOnWebSite && CanBeUsedAsReferral == obj.CanBeUsedAsReferral && IsSubscribeToMailingList == obj.IsSubscribeToMailingList && RateId == obj.RateId && IsCallbackSelected == obj.IsCallbackSelected && CallbackDaysInterval == obj.CallbackDaysInterval && IsDoNotCallSelected == obj.IsDoNotCallSelected && ReminderEmailSentDate == obj.ReminderEmailSentDate && ReminderPostCardSentDate == obj.ReminderPostCardSentDate;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ProjectFeedback> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectFeedback));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ProjectFeedback item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ProjectFeedback>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ProjectFeedback));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ProjectFeedback> itemsList
      = new List<ProjectFeedback>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ProjectFeedback)
      itemsList.Add(deserializedObject as ProjectFeedback);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_projectId;
      
        protected String m_customerNote;
      
        protected String m_editedCustomerNote;
      
        protected String m_dispatcherNote;
      
        protected DateTime m_dateCreated;
      
        protected DateTime? m_dateReviewed;
      
        protected int? m_reviewedByEmployeeId;
      
        protected bool m_canBePostedOnWebSite;
      
        protected bool m_canBeUsedAsReferral;
      
        protected bool m_isSubscribeToMailingList;
      
        protected int m_rateId;
      
        protected bool m_isCallbackSelected;
      
        protected int? m_callbackDaysInterval;
      
        protected bool m_isDoNotCallSelected;
      
        protected DateTime? m_reminderEmailSentDate;
      
        protected DateTime? m_reminderPostCardSentDate;
      
      #endregion

      #region Constructors
      public ProjectFeedback(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public ProjectFeedback(
        int 
          iD,int 
          projectId,String 
          customerNote,String 
          editedCustomerNote,String 
          dispatcherNote,DateTime 
          dateCreated,DateTime? 
          dateReviewed,int? 
          reviewedByEmployeeId,bool 
          canBePostedOnWebSite,bool 
          canBeUsedAsReferral,bool 
          isSubscribeToMailingList,int 
          rateId,bool 
          isCallbackSelected,int? 
          callbackDaysInterval,bool 
          isDoNotCallSelected,DateTime? 
          reminderEmailSentDate,DateTime? 
          reminderPostCardSentDate
        ) : this()
        {
        
          m_iD = iD;
        
          m_projectId = projectId;
        
          m_customerNote = customerNote;
        
          m_editedCustomerNote = editedCustomerNote;
        
          m_dispatcherNote = dispatcherNote;
        
          m_dateCreated = dateCreated;
        
          m_dateReviewed = dateReviewed;
        
          m_reviewedByEmployeeId = reviewedByEmployeeId;
        
          m_canBePostedOnWebSite = canBePostedOnWebSite;
        
          m_canBeUsedAsReferral = canBeUsedAsReferral;
        
          m_isSubscribeToMailingList = isSubscribeToMailingList;
        
          m_rateId = rateId;
        
          m_isCallbackSelected = isCallbackSelected;
        
          m_callbackDaysInterval = callbackDaysInterval;
        
          m_isDoNotCallSelected = isDoNotCallSelected;
        
          m_reminderEmailSentDate = reminderEmailSentDate;
        
          m_reminderPostCardSentDate = reminderPostCardSentDate;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int ProjectId
        {
        get { return m_projectId;}
        set { m_projectId = value; }
        }
      
        [XmlElement]
        public String CustomerNote
        {
        get { return m_customerNote;}
        set { m_customerNote = value; }
        }
      
        [XmlElement]
        public String EditedCustomerNote
        {
        get { return m_editedCustomerNote;}
        set { m_editedCustomerNote = value; }
        }
      
        [XmlElement]
        public String DispatcherNote
        {
        get { return m_dispatcherNote;}
        set { m_dispatcherNote = value; }
        }
      
        [XmlElement]
        public DateTime DateCreated
        {
        get { return m_dateCreated;}
        set { m_dateCreated = value; }
        }
      
        [XmlElement]
        public DateTime? DateReviewed
        {
        get { return m_dateReviewed;}
        set { m_dateReviewed = value; }
        }
      
        [XmlElement]
        public int? ReviewedByEmployeeId
        {
        get { return m_reviewedByEmployeeId;}
        set { m_reviewedByEmployeeId = value; }
        }
      
        [XmlElement]
        public bool CanBePostedOnWebSite
        {
        get { return m_canBePostedOnWebSite;}
        set { m_canBePostedOnWebSite = value; }
        }
      
        [XmlElement]
        public bool CanBeUsedAsReferral
        {
        get { return m_canBeUsedAsReferral;}
        set { m_canBeUsedAsReferral = value; }
        }
      
        [XmlElement]
        public bool IsSubscribeToMailingList
        {
        get { return m_isSubscribeToMailingList;}
        set { m_isSubscribeToMailingList = value; }
        }
      
        [XmlElement]
        public int RateId
        {
        get { return m_rateId;}
        set { m_rateId = value; }
        }
      
        [XmlElement]
        public bool IsCallbackSelected
        {
        get { return m_isCallbackSelected;}
        set { m_isCallbackSelected = value; }
        }
      
        [XmlElement]
        public int? CallbackDaysInterval
        {
        get { return m_callbackDaysInterval;}
        set { m_callbackDaysInterval = value; }
        }
      
        [XmlElement]
        public bool IsDoNotCallSelected
        {
        get { return m_isDoNotCallSelected;}
        set { m_isDoNotCallSelected = value; }
        }
      
        [XmlElement]
        public DateTime? ReminderEmailSentDate
        {
        get { return m_reminderEmailSentDate;}
        set { m_reminderEmailSentDate = value; }
        }
      
        [XmlElement]
        public DateTime? ReminderPostCardSentDate
        {
        get { return m_reminderPostCardSentDate;}
        set { m_reminderPostCardSentDate = value; }
        }
      

      public static int FieldsCount
      {
      get { return 17; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    