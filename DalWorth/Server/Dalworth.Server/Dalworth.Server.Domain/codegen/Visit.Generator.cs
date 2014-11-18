
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


      public partial class Visit : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Visit ( " +
      
        " VisitStatusId, " +
      
        " CreateDate, " +
      
        " ServiceDate, " +
      
        " DurationMin, " +
      
        " PreferedTimeFrom, " +
      
        " PreferedTimeTo, " +
      
        " CustomerId, " +
      
        " ServiceAddressId, " +
      
        " Notes, " +
      
        " ConfirmDateTime, " +
      
        " ConfirmedFrameBegin, " +
      
        " ConfirmedFrameEnd, " +
      
        " ConfirmLeftMessage, " +
      
        " ConfirmBusy, " +
      
        " IsCallOnYourWay, " +
      
        " ClosedDollarAmount, " +
      
        " SyncToolPrintDate, " +
      
        " IsWillCall " +
      
      ") Values (" +
      
        " ?VisitStatusId, " +
      
        " ?CreateDate, " +
      
        " ?ServiceDate, " +
      
        " ?DurationMin, " +
      
        " ?PreferedTimeFrom, " +
      
        " ?PreferedTimeTo, " +
      
        " ?CustomerId, " +
      
        " ?ServiceAddressId, " +
      
        " ?Notes, " +
      
        " ?ConfirmDateTime, " +
      
        " ?ConfirmedFrameBegin, " +
      
        " ?ConfirmedFrameEnd, " +
      
        " ?ConfirmLeftMessage, " +
      
        " ?ConfirmBusy, " +
      
        " ?IsCallOnYourWay, " +
      
        " ?ClosedDollarAmount, " +
      
        " ?SyncToolPrintDate, " +
      
        " ?IsWillCall " +
      
      ")";

      public static void Insert(Visit visit, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?VisitStatusId", visit.VisitStatusId);
      
        Database.PutParameter(dbCommand,"?CreateDate", visit.CreateDate);
      
        Database.PutParameter(dbCommand,"?ServiceDate", visit.ServiceDate);
      
        Database.PutParameter(dbCommand,"?DurationMin", visit.DurationMin);
      
        Database.PutParameter(dbCommand,"?PreferedTimeFrom", visit.PreferedTimeFrom);
      
        Database.PutParameter(dbCommand,"?PreferedTimeTo", visit.PreferedTimeTo);
      
        Database.PutParameter(dbCommand,"?CustomerId", visit.CustomerId);
      
        Database.PutParameter(dbCommand,"?ServiceAddressId", visit.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"?Notes", visit.Notes);
      
        Database.PutParameter(dbCommand,"?ConfirmDateTime", visit.ConfirmDateTime);
      
        Database.PutParameter(dbCommand,"?ConfirmedFrameBegin", visit.ConfirmedFrameBegin);
      
        Database.PutParameter(dbCommand,"?ConfirmedFrameEnd", visit.ConfirmedFrameEnd);
      
        Database.PutParameter(dbCommand,"?ConfirmLeftMessage", visit.ConfirmLeftMessage);
      
        Database.PutParameter(dbCommand,"?ConfirmBusy", visit.ConfirmBusy);
      
        Database.PutParameter(dbCommand,"?IsCallOnYourWay", visit.IsCallOnYourWay);
      
        Database.PutParameter(dbCommand,"?ClosedDollarAmount", visit.ClosedDollarAmount);
      
        Database.PutParameter(dbCommand,"?SyncToolPrintDate", visit.SyncToolPrintDate);
      
        Database.PutParameter(dbCommand,"?IsWillCall", visit.IsWillCall);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        visit.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Visit visit)
      {
        Insert(visit, null);
      }


      public static void Insert(List<Visit>  visitList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Visit visit in  visitList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?VisitStatusId", visit.VisitStatusId);
      
        Database.PutParameter(dbCommand,"?CreateDate", visit.CreateDate);
      
        Database.PutParameter(dbCommand,"?ServiceDate", visit.ServiceDate);
      
        Database.PutParameter(dbCommand,"?DurationMin", visit.DurationMin);
      
        Database.PutParameter(dbCommand,"?PreferedTimeFrom", visit.PreferedTimeFrom);
      
        Database.PutParameter(dbCommand,"?PreferedTimeTo", visit.PreferedTimeTo);
      
        Database.PutParameter(dbCommand,"?CustomerId", visit.CustomerId);
      
        Database.PutParameter(dbCommand,"?ServiceAddressId", visit.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"?Notes", visit.Notes);
      
        Database.PutParameter(dbCommand,"?ConfirmDateTime", visit.ConfirmDateTime);
      
        Database.PutParameter(dbCommand,"?ConfirmedFrameBegin", visit.ConfirmedFrameBegin);
      
        Database.PutParameter(dbCommand,"?ConfirmedFrameEnd", visit.ConfirmedFrameEnd);
      
        Database.PutParameter(dbCommand,"?ConfirmLeftMessage", visit.ConfirmLeftMessage);
      
        Database.PutParameter(dbCommand,"?ConfirmBusy", visit.ConfirmBusy);
      
        Database.PutParameter(dbCommand,"?IsCallOnYourWay", visit.IsCallOnYourWay);
      
        Database.PutParameter(dbCommand,"?ClosedDollarAmount", visit.ClosedDollarAmount);
      
        Database.PutParameter(dbCommand,"?SyncToolPrintDate", visit.SyncToolPrintDate);
      
        Database.PutParameter(dbCommand,"?IsWillCall", visit.IsWillCall);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?VisitStatusId",visit.VisitStatusId);
      
        Database.UpdateParameter(dbCommand,"?CreateDate",visit.CreateDate);
      
        Database.UpdateParameter(dbCommand,"?ServiceDate",visit.ServiceDate);
      
        Database.UpdateParameter(dbCommand,"?DurationMin",visit.DurationMin);
      
        Database.UpdateParameter(dbCommand,"?PreferedTimeFrom",visit.PreferedTimeFrom);
      
        Database.UpdateParameter(dbCommand,"?PreferedTimeTo",visit.PreferedTimeTo);
      
        Database.UpdateParameter(dbCommand,"?CustomerId",visit.CustomerId);
      
        Database.UpdateParameter(dbCommand,"?ServiceAddressId",visit.ServiceAddressId);
      
        Database.UpdateParameter(dbCommand,"?Notes",visit.Notes);
      
        Database.UpdateParameter(dbCommand,"?ConfirmDateTime",visit.ConfirmDateTime);
      
        Database.UpdateParameter(dbCommand,"?ConfirmedFrameBegin",visit.ConfirmedFrameBegin);
      
        Database.UpdateParameter(dbCommand,"?ConfirmedFrameEnd",visit.ConfirmedFrameEnd);
      
        Database.UpdateParameter(dbCommand,"?ConfirmLeftMessage",visit.ConfirmLeftMessage);
      
        Database.UpdateParameter(dbCommand,"?ConfirmBusy",visit.ConfirmBusy);
      
        Database.UpdateParameter(dbCommand,"?IsCallOnYourWay",visit.IsCallOnYourWay);
      
        Database.UpdateParameter(dbCommand,"?ClosedDollarAmount",visit.ClosedDollarAmount);
      
        Database.UpdateParameter(dbCommand,"?SyncToolPrintDate",visit.SyncToolPrintDate);
      
        Database.UpdateParameter(dbCommand,"?IsWillCall",visit.IsWillCall);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        visit.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Visit>  visitList)
      {
        Insert(visitList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Visit Set "
      
        + " VisitStatusId = ?VisitStatusId, "
      
        + " CreateDate = ?CreateDate, "
      
        + " ServiceDate = ?ServiceDate, "
      
        + " DurationMin = ?DurationMin, "
      
        + " PreferedTimeFrom = ?PreferedTimeFrom, "
      
        + " PreferedTimeTo = ?PreferedTimeTo, "
      
        + " CustomerId = ?CustomerId, "
      
        + " ServiceAddressId = ?ServiceAddressId, "
      
        + " Notes = ?Notes, "
      
        + " ConfirmDateTime = ?ConfirmDateTime, "
      
        + " ConfirmedFrameBegin = ?ConfirmedFrameBegin, "
      
        + " ConfirmedFrameEnd = ?ConfirmedFrameEnd, "
      
        + " ConfirmLeftMessage = ?ConfirmLeftMessage, "
      
        + " ConfirmBusy = ?ConfirmBusy, "
      
        + " IsCallOnYourWay = ?IsCallOnYourWay, "
      
        + " ClosedDollarAmount = ?ClosedDollarAmount, "
      
        + " SyncToolPrintDate = ?SyncToolPrintDate, "
      
        + " IsWillCall = ?IsWillCall "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Visit visit, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", visit.ID);
      
        Database.PutParameter(dbCommand,"?VisitStatusId", visit.VisitStatusId);
      
        Database.PutParameter(dbCommand,"?CreateDate", visit.CreateDate);
      
        Database.PutParameter(dbCommand,"?ServiceDate", visit.ServiceDate);
      
        Database.PutParameter(dbCommand,"?DurationMin", visit.DurationMin);
      
        Database.PutParameter(dbCommand,"?PreferedTimeFrom", visit.PreferedTimeFrom);
      
        Database.PutParameter(dbCommand,"?PreferedTimeTo", visit.PreferedTimeTo);
      
        Database.PutParameter(dbCommand,"?CustomerId", visit.CustomerId);
      
        Database.PutParameter(dbCommand,"?ServiceAddressId", visit.ServiceAddressId);
      
        Database.PutParameter(dbCommand,"?Notes", visit.Notes);
      
        Database.PutParameter(dbCommand,"?ConfirmDateTime", visit.ConfirmDateTime);
      
        Database.PutParameter(dbCommand,"?ConfirmedFrameBegin", visit.ConfirmedFrameBegin);
      
        Database.PutParameter(dbCommand,"?ConfirmedFrameEnd", visit.ConfirmedFrameEnd);
      
        Database.PutParameter(dbCommand,"?ConfirmLeftMessage", visit.ConfirmLeftMessage);
      
        Database.PutParameter(dbCommand,"?ConfirmBusy", visit.ConfirmBusy);
      
        Database.PutParameter(dbCommand,"?IsCallOnYourWay", visit.IsCallOnYourWay);
      
        Database.PutParameter(dbCommand,"?ClosedDollarAmount", visit.ClosedDollarAmount);
      
        Database.PutParameter(dbCommand,"?SyncToolPrintDate", visit.SyncToolPrintDate);
      
        Database.PutParameter(dbCommand,"?IsWillCall", visit.IsWillCall);
      

        dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Visit visit)
      {
        Update(visit, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " VisitStatusId, "
      
        + " CreateDate, "
      
        + " ServiceDate, "
      
        + " DurationMin, "
      
        + " PreferedTimeFrom, "
      
        + " PreferedTimeTo, "
      
        + " CustomerId, "
      
        + " ServiceAddressId, "
      
        + " Notes, "
      
        + " ConfirmDateTime, "
      
        + " ConfirmedFrameBegin, "
      
        + " ConfirmedFrameEnd, "
      
        + " ConfirmLeftMessage, "
      
        + " ConfirmBusy, "
      
        + " IsCallOnYourWay, "
      
        + " ClosedDollarAmount, "
      
        + " SyncToolPrintDate, "
      
        + " IsWillCall "
      

      + " From Visit "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Visit FindByPrimaryKey(
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
      throw new DataNotFoundException("Visit not found, search by primary key");

      }

      public static Visit FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Visit visit, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",visit.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Visit visit)
      {
      return Exists(visit, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Visit limit 1";

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

      public static Visit Load(IDataReader dataReader, int offset)
      {
      Visit visit = new Visit();

      visit.ID = dataReader.GetInt32(0 + offset);
          visit.VisitStatusId = dataReader.GetInt32(1 + offset);
          visit.CreateDate = dataReader.GetDateTime(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            visit.ServiceDate = dataReader.GetDateTime(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            visit.DurationMin = dataReader.GetInt32(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            visit.PreferedTimeFrom = dataReader.GetDateTime(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            visit.PreferedTimeTo = dataReader.GetDateTime(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            visit.CustomerId = dataReader.GetInt32(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            visit.ServiceAddressId = dataReader.GetInt32(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            visit.Notes = dataReader.GetString(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            visit.ConfirmDateTime = dataReader.GetDateTime(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            visit.ConfirmedFrameBegin = dataReader.GetDateTime(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            visit.ConfirmedFrameEnd = dataReader.GetDateTime(12 + offset);
          visit.ConfirmLeftMessage = dataReader.GetBoolean(13 + offset);
          visit.ConfirmBusy = dataReader.GetBoolean(14 + offset);
          visit.IsCallOnYourWay = dataReader.GetBoolean(15 + offset);
          visit.ClosedDollarAmount = dataReader.GetDecimal(16 + offset);
          
            if(!dataReader.IsDBNull(17 + offset))
            visit.SyncToolPrintDate = dataReader.GetDateTime(17 + offset);
          visit.IsWillCall = dataReader.GetBoolean(18 + offset);
          

      return visit;
      }

      public static Visit Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Visit "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Visit visit, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", visit.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Visit visit)
      {
        Delete(visit, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Visit ";

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
      
        + " VisitStatusId, "
      
        + " CreateDate, "
      
        + " ServiceDate, "
      
        + " DurationMin, "
      
        + " PreferedTimeFrom, "
      
        + " PreferedTimeTo, "
      
        + " CustomerId, "
      
        + " ServiceAddressId, "
      
        + " Notes, "
      
        + " ConfirmDateTime, "
      
        + " ConfirmedFrameBegin, "
      
        + " ConfirmedFrameEnd, "
      
        + " ConfirmLeftMessage, "
      
        + " ConfirmBusy, "
      
        + " IsCallOnYourWay, "
      
        + " ClosedDollarAmount, "
      
        + " SyncToolPrintDate, "
      
        + " IsWillCall "
      

      + " From Visit ";
      public static List<Visit> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Visit> rv = new List<Visit>();

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

      public static List<Visit> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Visit> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Visit obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && VisitStatusId == obj.VisitStatusId && CreateDate == obj.CreateDate && ServiceDate == obj.ServiceDate && DurationMin == obj.DurationMin && PreferedTimeFrom == obj.PreferedTimeFrom && PreferedTimeTo == obj.PreferedTimeTo && CustomerId == obj.CustomerId && ServiceAddressId == obj.ServiceAddressId && Notes == obj.Notes && ConfirmDateTime == obj.ConfirmDateTime && ConfirmedFrameBegin == obj.ConfirmedFrameBegin && ConfirmedFrameEnd == obj.ConfirmedFrameEnd && ConfirmLeftMessage == obj.ConfirmLeftMessage && ConfirmBusy == obj.ConfirmBusy && IsCallOnYourWay == obj.IsCallOnYourWay && ClosedDollarAmount == obj.ClosedDollarAmount && SyncToolPrintDate == obj.SyncToolPrintDate && IsWillCall == obj.IsWillCall;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Visit> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Visit));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Visit item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Visit>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Visit));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Visit> itemsList
      = new List<Visit>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Visit)
      itemsList.Add(deserializedObject as Visit);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_visitStatusId;
      
        protected DateTime m_createDate;
      
        protected DateTime? m_serviceDate;
      
        protected int? m_durationMin;
      
        protected DateTime? m_preferedTimeFrom;
      
        protected DateTime? m_preferedTimeTo;
      
        protected int? m_customerId;
      
        protected int? m_serviceAddressId;
      
        protected String m_notes;
      
        protected DateTime? m_confirmDateTime;
      
        protected DateTime? m_confirmedFrameBegin;
      
        protected DateTime? m_confirmedFrameEnd;
      
        protected bool m_confirmLeftMessage;
      
        protected bool m_confirmBusy;
      
        protected bool m_isCallOnYourWay;
      
        protected decimal m_closedDollarAmount;
      
        protected DateTime? m_syncToolPrintDate;
      
        protected bool m_isWillCall;
      
      #endregion

      #region Constructors
      public Visit(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Visit(
        int 
          iD,int 
          visitStatusId,DateTime 
          createDate,DateTime? 
          serviceDate,int? 
          durationMin,DateTime? 
          preferedTimeFrom,DateTime? 
          preferedTimeTo,int? 
          customerId,int? 
          serviceAddressId,String 
          notes,DateTime? 
          confirmDateTime,DateTime? 
          confirmedFrameBegin,DateTime? 
          confirmedFrameEnd,bool 
          confirmLeftMessage,bool 
          confirmBusy,bool 
          isCallOnYourWay,decimal 
          closedDollarAmount,DateTime? 
          syncToolPrintDate,bool 
          isWillCall
        ) : this()
        {
        
          m_iD = iD;
        
          m_visitStatusId = visitStatusId;
        
          m_createDate = createDate;
        
          m_serviceDate = serviceDate;
        
          m_durationMin = durationMin;
        
          m_preferedTimeFrom = preferedTimeFrom;
        
          m_preferedTimeTo = preferedTimeTo;
        
          m_customerId = customerId;
        
          m_serviceAddressId = serviceAddressId;
        
          m_notes = notes;
        
          m_confirmDateTime = confirmDateTime;
        
          m_confirmedFrameBegin = confirmedFrameBegin;
        
          m_confirmedFrameEnd = confirmedFrameEnd;
        
          m_confirmLeftMessage = confirmLeftMessage;
        
          m_confirmBusy = confirmBusy;
        
          m_isCallOnYourWay = isCallOnYourWay;
        
          m_closedDollarAmount = closedDollarAmount;
        
          m_syncToolPrintDate = syncToolPrintDate;
        
          m_isWillCall = isWillCall;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int VisitStatusId
        {
        get { return m_visitStatusId;}
        set { m_visitStatusId = value; }
        }
      
        [XmlElement]
        public DateTime CreateDate
        {
        get { return m_createDate;}
        set { m_createDate = value; }
        }
      
        [XmlElement]
        public DateTime? ServiceDate
        {
        get { return m_serviceDate;}
        set { m_serviceDate = value; }
        }
      
        [XmlElement]
        public int? DurationMin
        {
        get { return m_durationMin;}
        set { m_durationMin = value; }
        }
      
        [XmlElement]
        public DateTime? PreferedTimeFrom
        {
        get { return m_preferedTimeFrom;}
        set { m_preferedTimeFrom = value; }
        }
      
        [XmlElement]
        public DateTime? PreferedTimeTo
        {
        get { return m_preferedTimeTo;}
        set { m_preferedTimeTo = value; }
        }
      
        [XmlElement]
        public int? CustomerId
        {
        get { return m_customerId;}
        set { m_customerId = value; }
        }
      
        [XmlElement]
        public int? ServiceAddressId
        {
        get { return m_serviceAddressId;}
        set { m_serviceAddressId = value; }
        }
      
        [XmlElement]
        public String Notes
        {
        get { return m_notes;}
        set { m_notes = value; }
        }
      
        [XmlElement]
        public DateTime? ConfirmDateTime
        {
        get { return m_confirmDateTime;}
        set { m_confirmDateTime = value; }
        }
      
        [XmlElement]
        public DateTime? ConfirmedFrameBegin
        {
        get { return m_confirmedFrameBegin;}
        set { m_confirmedFrameBegin = value; }
        }
      
        [XmlElement]
        public DateTime? ConfirmedFrameEnd
        {
        get { return m_confirmedFrameEnd;}
        set { m_confirmedFrameEnd = value; }
        }
      
        [XmlElement]
        public bool ConfirmLeftMessage
        {
        get { return m_confirmLeftMessage;}
        set { m_confirmLeftMessage = value; }
        }
      
        [XmlElement]
        public bool ConfirmBusy
        {
        get { return m_confirmBusy;}
        set { m_confirmBusy = value; }
        }
      
        [XmlElement]
        public bool IsCallOnYourWay
        {
        get { return m_isCallOnYourWay;}
        set { m_isCallOnYourWay = value; }
        }
      
        [XmlElement]
        public decimal ClosedDollarAmount
        {
        get { return m_closedDollarAmount;}
        set { m_closedDollarAmount = value; }
        }
      
        [XmlElement]
        public DateTime? SyncToolPrintDate
        {
        get { return m_syncToolPrintDate;}
        set { m_syncToolPrintDate = value; }
        }
      
        [XmlElement]
        public bool IsWillCall
        {
        get { return m_isWillCall;}
        set { m_isWillCall = value; }
        }
      

      public static int FieldsCount
      {
      get { return 19; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    