
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


      public partial class Work : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Work ( " +
      
        " DispatchEmployeeId, " +
      
        " TechnicianEmployeeId, " +
      
        " VanId, " +
      
        " StartDate, " +
      
        " WorkStatusId, " +
      
        " StartMessage, " +
      
        " EndMessage, " +
      
        " EquipmentNotes, " +
      
        " IsSentToServman, " +
      
        " CreateDate, " +
      
        " StartDayDate, " +
      
        " EndDayDate, " +
      
        " ClosedDollarAmount " +
      
      ") Values (" +
      
        " ?DispatchEmployeeId, " +
      
        " ?TechnicianEmployeeId, " +
      
        " ?VanId, " +
      
        " ?StartDate, " +
      
        " ?WorkStatusId, " +
      
        " ?StartMessage, " +
      
        " ?EndMessage, " +
      
        " ?EquipmentNotes, " +
      
        " ?IsSentToServman, " +
      
        " ?CreateDate, " +
      
        " ?StartDayDate, " +
      
        " ?EndDayDate, " +
      
        " ?ClosedDollarAmount " +
      
      ")";

      public static void Insert(Work work, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?DispatchEmployeeId", work.DispatchEmployeeId);
      
        Database.PutParameter(dbCommand,"?TechnicianEmployeeId", work.TechnicianEmployeeId);
      
        Database.PutParameter(dbCommand,"?VanId", work.VanId);
      
        Database.PutParameter(dbCommand,"?StartDate", work.StartDate);
      
        Database.PutParameter(dbCommand,"?WorkStatusId", work.WorkStatusId);
      
        Database.PutParameter(dbCommand,"?StartMessage", work.StartMessage);
      
        Database.PutParameter(dbCommand,"?EndMessage", work.EndMessage);
      
        Database.PutParameter(dbCommand,"?EquipmentNotes", work.EquipmentNotes);
      
        Database.PutParameter(dbCommand,"?IsSentToServman", work.IsSentToServman);
      
        Database.PutParameter(dbCommand,"?CreateDate", work.CreateDate);
      
        Database.PutParameter(dbCommand,"?StartDayDate", work.StartDayDate);
      
        Database.PutParameter(dbCommand,"?EndDayDate", work.EndDayDate);
      
        Database.PutParameter(dbCommand,"?ClosedDollarAmount", work.ClosedDollarAmount);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        work.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Work work)
      {
        Insert(work, null);
      }


      public static void Insert(List<Work>  workList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Work work in  workList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?DispatchEmployeeId", work.DispatchEmployeeId);
      
        Database.PutParameter(dbCommand,"?TechnicianEmployeeId", work.TechnicianEmployeeId);
      
        Database.PutParameter(dbCommand,"?VanId", work.VanId);
      
        Database.PutParameter(dbCommand,"?StartDate", work.StartDate);
      
        Database.PutParameter(dbCommand,"?WorkStatusId", work.WorkStatusId);
      
        Database.PutParameter(dbCommand,"?StartMessage", work.StartMessage);
      
        Database.PutParameter(dbCommand,"?EndMessage", work.EndMessage);
      
        Database.PutParameter(dbCommand,"?EquipmentNotes", work.EquipmentNotes);
      
        Database.PutParameter(dbCommand,"?IsSentToServman", work.IsSentToServman);
      
        Database.PutParameter(dbCommand,"?CreateDate", work.CreateDate);
      
        Database.PutParameter(dbCommand,"?StartDayDate", work.StartDayDate);
      
        Database.PutParameter(dbCommand,"?EndDayDate", work.EndDayDate);
      
        Database.PutParameter(dbCommand,"?ClosedDollarAmount", work.ClosedDollarAmount);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?DispatchEmployeeId",work.DispatchEmployeeId);
      
        Database.UpdateParameter(dbCommand,"?TechnicianEmployeeId",work.TechnicianEmployeeId);
      
        Database.UpdateParameter(dbCommand,"?VanId",work.VanId);
      
        Database.UpdateParameter(dbCommand,"?StartDate",work.StartDate);
      
        Database.UpdateParameter(dbCommand,"?WorkStatusId",work.WorkStatusId);
      
        Database.UpdateParameter(dbCommand,"?StartMessage",work.StartMessage);
      
        Database.UpdateParameter(dbCommand,"?EndMessage",work.EndMessage);
      
        Database.UpdateParameter(dbCommand,"?EquipmentNotes",work.EquipmentNotes);
      
        Database.UpdateParameter(dbCommand,"?IsSentToServman",work.IsSentToServman);
      
        Database.UpdateParameter(dbCommand,"?CreateDate",work.CreateDate);
      
        Database.UpdateParameter(dbCommand,"?StartDayDate",work.StartDayDate);
      
        Database.UpdateParameter(dbCommand,"?EndDayDate",work.EndDayDate);
      
        Database.UpdateParameter(dbCommand,"?ClosedDollarAmount",work.ClosedDollarAmount);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        work.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Work>  workList)
      {
        Insert(workList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Work Set "
      
        + " DispatchEmployeeId = ?DispatchEmployeeId, "
      
        + " TechnicianEmployeeId = ?TechnicianEmployeeId, "
      
        + " VanId = ?VanId, "
      
        + " StartDate = ?StartDate, "
      
        + " WorkStatusId = ?WorkStatusId, "
      
        + " StartMessage = ?StartMessage, "
      
        + " EndMessage = ?EndMessage, "
      
        + " EquipmentNotes = ?EquipmentNotes, "
      
        + " IsSentToServman = ?IsSentToServman, "
      
        + " CreateDate = ?CreateDate, "
      
        + " StartDayDate = ?StartDayDate, "
      
        + " EndDayDate = ?EndDayDate, "
      
        + " ClosedDollarAmount = ?ClosedDollarAmount "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Work work, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", work.ID);
      
        Database.PutParameter(dbCommand,"?DispatchEmployeeId", work.DispatchEmployeeId);
      
        Database.PutParameter(dbCommand,"?TechnicianEmployeeId", work.TechnicianEmployeeId);
      
        Database.PutParameter(dbCommand,"?VanId", work.VanId);
      
        Database.PutParameter(dbCommand,"?StartDate", work.StartDate);
      
        Database.PutParameter(dbCommand,"?WorkStatusId", work.WorkStatusId);
      
        Database.PutParameter(dbCommand,"?StartMessage", work.StartMessage);
      
        Database.PutParameter(dbCommand,"?EndMessage", work.EndMessage);
      
        Database.PutParameter(dbCommand,"?EquipmentNotes", work.EquipmentNotes);
      
        Database.PutParameter(dbCommand,"?IsSentToServman", work.IsSentToServman);
      
        Database.PutParameter(dbCommand,"?CreateDate", work.CreateDate);
      
        Database.PutParameter(dbCommand,"?StartDayDate", work.StartDayDate);
      
        Database.PutParameter(dbCommand,"?EndDayDate", work.EndDayDate);
      
        Database.PutParameter(dbCommand,"?ClosedDollarAmount", work.ClosedDollarAmount);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Work work)
      {
        Update(work, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " DispatchEmployeeId, "
      
        + " TechnicianEmployeeId, "
      
        + " VanId, "
      
        + " StartDate, "
      
        + " WorkStatusId, "
      
        + " StartMessage, "
      
        + " EndMessage, "
      
        + " EquipmentNotes, "
      
        + " IsSentToServman, "
      
        + " CreateDate, "
      
        + " StartDayDate, "
      
        + " EndDayDate, "
      
        + " ClosedDollarAmount "
      

      + " From Work "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Work FindByPrimaryKey(
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
      throw new DataNotFoundException("Work not found, search by primary key");

      }

      public static Work FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Work work, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",work.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Work work)
      {
      return Exists(work, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Work limit 1";

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

      public static Work Load(IDataReader dataReader, int offset)
      {
      Work work = new Work();

      work.ID = dataReader.GetInt32(0 + offset);
          work.DispatchEmployeeId = dataReader.GetInt32(1 + offset);
          work.TechnicianEmployeeId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            work.VanId = dataReader.GetInt32(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            work.StartDate = dataReader.GetDateTime(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            work.WorkStatusId = dataReader.GetInt32(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            work.StartMessage = dataReader.GetString(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            work.EndMessage = dataReader.GetString(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            work.EquipmentNotes = dataReader.GetString(8 + offset);
          work.IsSentToServman = dataReader.GetBoolean(9 + offset);
          work.CreateDate = dataReader.GetDateTime(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            work.StartDayDate = dataReader.GetDateTime(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            work.EndDayDate = dataReader.GetDateTime(12 + offset);
          work.ClosedDollarAmount = dataReader.GetDecimal(13 + offset);
          

      return work;
      }

      public static Work Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Work "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Work work, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", work.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Work work)
      {
        Delete(work, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Work ";

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
      
        + " DispatchEmployeeId, "
      
        + " TechnicianEmployeeId, "
      
        + " VanId, "
      
        + " StartDate, "
      
        + " WorkStatusId, "
      
        + " StartMessage, "
      
        + " EndMessage, "
      
        + " EquipmentNotes, "
      
        + " IsSentToServman, "
      
        + " CreateDate, "
      
        + " StartDayDate, "
      
        + " EndDayDate, "
      
        + " ClosedDollarAmount "
      

      + " From Work ";
      public static List<Work> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Work> rv = new List<Work>();

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

      public static List<Work> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Work> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Work obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && DispatchEmployeeId == obj.DispatchEmployeeId && TechnicianEmployeeId == obj.TechnicianEmployeeId && VanId == obj.VanId && StartDate == obj.StartDate && WorkStatusId == obj.WorkStatusId && StartMessage == obj.StartMessage && EndMessage == obj.EndMessage && EquipmentNotes == obj.EquipmentNotes && IsSentToServman == obj.IsSentToServman && CreateDate == obj.CreateDate && StartDayDate == obj.StartDayDate && EndDayDate == obj.EndDayDate && ClosedDollarAmount == obj.ClosedDollarAmount;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Work> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Work));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Work item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Work>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Work));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Work> itemsList
      = new List<Work>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Work)
      itemsList.Add(deserializedObject as Work);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_dispatchEmployeeId;
      
        protected int m_technicianEmployeeId;
      
        protected int? m_vanId;
      
        protected DateTime? m_startDate;
      
        protected int? m_workStatusId;
      
        protected String m_startMessage;
      
        protected String m_endMessage;
      
        protected String m_equipmentNotes;
      
        protected bool m_isSentToServman;
      
        protected DateTime m_createDate;
      
        protected DateTime? m_startDayDate;
      
        protected DateTime? m_endDayDate;
      
        protected decimal m_closedDollarAmount;
      
      #endregion

      #region Constructors
      public Work(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Work(
        int 
          iD,int 
          dispatchEmployeeId,int 
          technicianEmployeeId,int? 
          vanId,DateTime? 
          startDate,int? 
          workStatusId,String 
          startMessage,String 
          endMessage,String 
          equipmentNotes,bool 
          isSentToServman,DateTime 
          createDate,DateTime? 
          startDayDate,DateTime? 
          endDayDate,decimal 
          closedDollarAmount
        ) : this()
        {
        
          m_iD = iD;
        
          m_dispatchEmployeeId = dispatchEmployeeId;
        
          m_technicianEmployeeId = technicianEmployeeId;
        
          m_vanId = vanId;
        
          m_startDate = startDate;
        
          m_workStatusId = workStatusId;
        
          m_startMessage = startMessage;
        
          m_endMessage = endMessage;
        
          m_equipmentNotes = equipmentNotes;
        
          m_isSentToServman = isSentToServman;
        
          m_createDate = createDate;
        
          m_startDayDate = startDayDate;
        
          m_endDayDate = endDayDate;
        
          m_closedDollarAmount = closedDollarAmount;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int DispatchEmployeeId
        {
        get { return m_dispatchEmployeeId;}
        set { m_dispatchEmployeeId = value; }
        }
      
        [XmlElement]
        public int TechnicianEmployeeId
        {
        get { return m_technicianEmployeeId;}
        set { m_technicianEmployeeId = value; }
        }
      
        [XmlElement]
        public int? VanId
        {
        get { return m_vanId;}
        set { m_vanId = value; }
        }
      
        [XmlElement]
        public DateTime? StartDate
        {
        get { return m_startDate;}
        set { m_startDate = value; }
        }
      
        [XmlElement]
        public int? WorkStatusId
        {
        get { return m_workStatusId;}
        set { m_workStatusId = value; }
        }
      
        [XmlElement]
        public String StartMessage
        {
        get { return m_startMessage;}
        set { m_startMessage = value; }
        }
      
        [XmlElement]
        public String EndMessage
        {
        get { return m_endMessage;}
        set { m_endMessage = value; }
        }
      
        [XmlElement]
        public String EquipmentNotes
        {
        get { return m_equipmentNotes;}
        set { m_equipmentNotes = value; }
        }
      
        [XmlElement]
        public bool IsSentToServman
        {
        get { return m_isSentToServman;}
        set { m_isSentToServman = value; }
        }
      
        [XmlElement]
        public DateTime CreateDate
        {
        get { return m_createDate;}
        set { m_createDate = value; }
        }
      
        [XmlElement]
        public DateTime? StartDayDate
        {
        get { return m_startDayDate;}
        set { m_startDayDate = value; }
        }
      
        [XmlElement]
        public DateTime? EndDayDate
        {
        get { return m_endDayDate;}
        set { m_endDayDate = value; }
        }
      
        [XmlElement]
        public decimal ClosedDollarAmount
        {
        get { return m_closedDollarAmount;}
        set { m_closedDollarAmount = value; }
        }
      

      public static int FieldsCount
      {
      get { return 14; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    