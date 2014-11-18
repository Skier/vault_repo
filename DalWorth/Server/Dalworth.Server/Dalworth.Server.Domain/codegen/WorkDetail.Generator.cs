
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


      public partial class WorkDetail : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WorkDetail ( " +
      
        " WorkId, " +
      
        " VisitId, " +
      
        " TimeBegin, " +
      
        " TimeEnd, " +
      
        " Sequence, " +
      
        " WorkDetailStatusId, " +
      
        " TimeDispatch, " +
      
        " TimeArrive, " +
      
        " TimeComplete, " +
      
        " TimeBeginAssigned, " +
      
        " TimeEndAssigned " +
      
      ") Values (" +
      
        " ?WorkId, " +
      
        " ?VisitId, " +
      
        " ?TimeBegin, " +
      
        " ?TimeEnd, " +
      
        " ?Sequence, " +
      
        " ?WorkDetailStatusId, " +
      
        " ?TimeDispatch, " +
      
        " ?TimeArrive, " +
      
        " ?TimeComplete, " +
      
        " ?TimeBeginAssigned, " +
      
        " ?TimeEndAssigned " +
      
      ")";

      public static void Insert(WorkDetail workDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkId", workDetail.WorkId);
      
        Database.PutParameter(dbCommand,"?VisitId", workDetail.VisitId);
      
        Database.PutParameter(dbCommand,"?TimeBegin", workDetail.TimeBegin);
      
        Database.PutParameter(dbCommand,"?TimeEnd", workDetail.TimeEnd);
      
        Database.PutParameter(dbCommand,"?Sequence", workDetail.Sequence);
      
        Database.PutParameter(dbCommand,"?WorkDetailStatusId", workDetail.WorkDetailStatusId);
      
        Database.PutParameter(dbCommand,"?TimeDispatch", workDetail.TimeDispatch);
      
        Database.PutParameter(dbCommand,"?TimeArrive", workDetail.TimeArrive);
      
        Database.PutParameter(dbCommand,"?TimeComplete", workDetail.TimeComplete);
      
        Database.PutParameter(dbCommand,"?TimeBeginAssigned", workDetail.TimeBeginAssigned);
      
        Database.PutParameter(dbCommand,"?TimeEndAssigned", workDetail.TimeEndAssigned);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        workDetail.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(WorkDetail workDetail)
      {
        Insert(workDetail, null);
      }


      public static void Insert(List<WorkDetail>  workDetailList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WorkDetail workDetail in  workDetailList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WorkId", workDetail.WorkId);
      
        Database.PutParameter(dbCommand,"?VisitId", workDetail.VisitId);
      
        Database.PutParameter(dbCommand,"?TimeBegin", workDetail.TimeBegin);
      
        Database.PutParameter(dbCommand,"?TimeEnd", workDetail.TimeEnd);
      
        Database.PutParameter(dbCommand,"?Sequence", workDetail.Sequence);
      
        Database.PutParameter(dbCommand,"?WorkDetailStatusId", workDetail.WorkDetailStatusId);
      
        Database.PutParameter(dbCommand,"?TimeDispatch", workDetail.TimeDispatch);
      
        Database.PutParameter(dbCommand,"?TimeArrive", workDetail.TimeArrive);
      
        Database.PutParameter(dbCommand,"?TimeComplete", workDetail.TimeComplete);
      
        Database.PutParameter(dbCommand,"?TimeBeginAssigned", workDetail.TimeBeginAssigned);
      
        Database.PutParameter(dbCommand,"?TimeEndAssigned", workDetail.TimeEndAssigned);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WorkId",workDetail.WorkId);
      
        Database.UpdateParameter(dbCommand,"?VisitId",workDetail.VisitId);
      
        Database.UpdateParameter(dbCommand,"?TimeBegin",workDetail.TimeBegin);
      
        Database.UpdateParameter(dbCommand,"?TimeEnd",workDetail.TimeEnd);
      
        Database.UpdateParameter(dbCommand,"?Sequence",workDetail.Sequence);
      
        Database.UpdateParameter(dbCommand,"?WorkDetailStatusId",workDetail.WorkDetailStatusId);
      
        Database.UpdateParameter(dbCommand,"?TimeDispatch",workDetail.TimeDispatch);
      
        Database.UpdateParameter(dbCommand,"?TimeArrive",workDetail.TimeArrive);
      
        Database.UpdateParameter(dbCommand,"?TimeComplete",workDetail.TimeComplete);
      
        Database.UpdateParameter(dbCommand,"?TimeBeginAssigned",workDetail.TimeBeginAssigned);
      
        Database.UpdateParameter(dbCommand,"?TimeEndAssigned",workDetail.TimeEndAssigned);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        workDetail.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<WorkDetail>  workDetailList)
      {
        Insert(workDetailList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WorkDetail Set "
      
        + " WorkId = ?WorkId, "
      
        + " VisitId = ?VisitId, "
      
        + " TimeBegin = ?TimeBegin, "
      
        + " TimeEnd = ?TimeEnd, "
      
        + " Sequence = ?Sequence, "
      
        + " WorkDetailStatusId = ?WorkDetailStatusId, "
      
        + " TimeDispatch = ?TimeDispatch, "
      
        + " TimeArrive = ?TimeArrive, "
      
        + " TimeComplete = ?TimeComplete, "
      
        + " TimeBeginAssigned = ?TimeBeginAssigned, "
      
        + " TimeEndAssigned = ?TimeEndAssigned "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WorkDetail workDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", workDetail.ID);
      
        Database.PutParameter(dbCommand,"?WorkId", workDetail.WorkId);
      
        Database.PutParameter(dbCommand,"?VisitId", workDetail.VisitId);
      
        Database.PutParameter(dbCommand,"?TimeBegin", workDetail.TimeBegin);
      
        Database.PutParameter(dbCommand,"?TimeEnd", workDetail.TimeEnd);
      
        Database.PutParameter(dbCommand,"?Sequence", workDetail.Sequence);
      
        Database.PutParameter(dbCommand,"?WorkDetailStatusId", workDetail.WorkDetailStatusId);
      
        Database.PutParameter(dbCommand,"?TimeDispatch", workDetail.TimeDispatch);
      
        Database.PutParameter(dbCommand,"?TimeArrive", workDetail.TimeArrive);
      
        Database.PutParameter(dbCommand,"?TimeComplete", workDetail.TimeComplete);
      
        Database.PutParameter(dbCommand,"?TimeBeginAssigned", workDetail.TimeBeginAssigned);
      
        Database.PutParameter(dbCommand,"?TimeEndAssigned", workDetail.TimeEndAssigned);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkDetail workDetail)
      {
        Update(workDetail, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WorkId, "
      
        + " VisitId, "
      
        + " TimeBegin, "
      
        + " TimeEnd, "
      
        + " Sequence, "
      
        + " WorkDetailStatusId, "
      
        + " TimeDispatch, "
      
        + " TimeArrive, "
      
        + " TimeComplete, "
      
        + " TimeBeginAssigned, "
      
        + " TimeEndAssigned "
      

      + " From WorkDetail "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WorkDetail FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkDetail not found, search by primary key");

      }

      public static WorkDetail FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WorkDetail workDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",workDetail.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkDetail workDetail)
      {
      return Exists(workDetail, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WorkDetail limit 1";

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

      public static WorkDetail Load(IDataReader dataReader, int offset)
      {
      WorkDetail workDetail = new WorkDetail();

      workDetail.ID = dataReader.GetInt32(0 + offset);
          workDetail.WorkId = dataReader.GetInt32(1 + offset);
          workDetail.VisitId = dataReader.GetInt32(2 + offset);
          workDetail.TimeBegin = dataReader.GetDateTime(3 + offset);
          workDetail.TimeEnd = dataReader.GetDateTime(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            workDetail.Sequence = dataReader.GetInt32(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            workDetail.WorkDetailStatusId = dataReader.GetInt32(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            workDetail.TimeDispatch = dataReader.GetDateTime(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            workDetail.TimeArrive = dataReader.GetDateTime(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            workDetail.TimeComplete = dataReader.GetDateTime(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            workDetail.TimeBeginAssigned = dataReader.GetDateTime(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            workDetail.TimeEndAssigned = dataReader.GetDateTime(11 + offset);
          

      return workDetail;
      }

      public static WorkDetail Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WorkDetail "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WorkDetail workDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", workDetail.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkDetail workDetail)
      {
        Delete(workDetail, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WorkDetail ";

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
      
        + " WorkId, "
      
        + " VisitId, "
      
        + " TimeBegin, "
      
        + " TimeEnd, "
      
        + " Sequence, "
      
        + " WorkDetailStatusId, "
      
        + " TimeDispatch, "
      
        + " TimeArrive, "
      
        + " TimeComplete, "
      
        + " TimeBeginAssigned, "
      
        + " TimeEndAssigned "
      

      + " From WorkDetail ";
      public static List<WorkDetail> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WorkDetail> rv = new List<WorkDetail>();

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

      public static List<WorkDetail> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkDetail> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WorkDetail obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && WorkId == obj.WorkId && VisitId == obj.VisitId && TimeBegin == obj.TimeBegin && TimeEnd == obj.TimeEnd && Sequence == obj.Sequence && WorkDetailStatusId == obj.WorkDetailStatusId && TimeDispatch == obj.TimeDispatch && TimeArrive == obj.TimeArrive && TimeComplete == obj.TimeComplete && TimeBeginAssigned == obj.TimeBeginAssigned && TimeEndAssigned == obj.TimeEndAssigned;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WorkDetail> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkDetail));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkDetail item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkDetail>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkDetail));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkDetail> itemsList
      = new List<WorkDetail>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkDetail)
      itemsList.Add(deserializedObject as WorkDetail);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_workId;
      
        protected int m_visitId;
      
        protected DateTime m_timeBegin;
      
        protected DateTime m_timeEnd;
      
        protected int? m_sequence;
      
        protected int? m_workDetailStatusId;
      
        protected DateTime? m_timeDispatch;
      
        protected DateTime? m_timeArrive;
      
        protected DateTime? m_timeComplete;
      
        protected DateTime? m_timeBeginAssigned;
      
        protected DateTime? m_timeEndAssigned;
      
      #endregion

      #region Constructors
      public WorkDetail(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WorkDetail(
        int 
          iD,int 
          workId,int 
          visitId,DateTime 
          timeBegin,DateTime 
          timeEnd,int? 
          sequence,int? 
          workDetailStatusId,DateTime? 
          timeDispatch,DateTime? 
          timeArrive,DateTime? 
          timeComplete,DateTime? 
          timeBeginAssigned,DateTime? 
          timeEndAssigned
        ) : this()
        {
        
          m_iD = iD;
        
          m_workId = workId;
        
          m_visitId = visitId;
        
          m_timeBegin = timeBegin;
        
          m_timeEnd = timeEnd;
        
          m_sequence = sequence;
        
          m_workDetailStatusId = workDetailStatusId;
        
          m_timeDispatch = timeDispatch;
        
          m_timeArrive = timeArrive;
        
          m_timeComplete = timeComplete;
        
          m_timeBeginAssigned = timeBeginAssigned;
        
          m_timeEndAssigned = timeEndAssigned;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int WorkId
        {
        get { return m_workId;}
        set { m_workId = value; }
        }
      
        [XmlElement]
        public int VisitId
        {
        get { return m_visitId;}
        set { m_visitId = value; }
        }
      
        [XmlElement]
        public DateTime TimeBegin
        {
        get { return m_timeBegin;}
        set { m_timeBegin = value; }
        }
      
        [XmlElement]
        public DateTime TimeEnd
        {
        get { return m_timeEnd;}
        set { m_timeEnd = value; }
        }
      
        [XmlElement]
        public int? Sequence
        {
        get { return m_sequence;}
        set { m_sequence = value; }
        }
      
        [XmlElement]
        public int? WorkDetailStatusId
        {
        get { return m_workDetailStatusId;}
        set { m_workDetailStatusId = value; }
        }
      
        [XmlElement]
        public DateTime? TimeDispatch
        {
        get { return m_timeDispatch;}
        set { m_timeDispatch = value; }
        }
      
        [XmlElement]
        public DateTime? TimeArrive
        {
        get { return m_timeArrive;}
        set { m_timeArrive = value; }
        }
      
        [XmlElement]
        public DateTime? TimeComplete
        {
        get { return m_timeComplete;}
        set { m_timeComplete = value; }
        }
      
        [XmlElement]
        public DateTime? TimeBeginAssigned
        {
        get { return m_timeBeginAssigned;}
        set { m_timeBeginAssigned = value; }
        }
      
        [XmlElement]
        public DateTime? TimeEndAssigned
        {
        get { return m_timeEndAssigned;}
        set { m_timeEndAssigned = value; }
        }
      

      public static int FieldsCount
      {
      get { return 12; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    