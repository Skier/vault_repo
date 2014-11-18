
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


      public partial class WorkTransactionQbInvoice : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WorkTransactionQbInvoice ( " +
      
        " WorkTransactionId, " +
      
        " QbInvoiceId, " +
      
        " QbCustomerId, " +
      
        " QbProjectId, " +
      
        " IsModified, " +
      
        " IsCreated " +
      
      ") Values (" +
      
        " ?WorkTransactionId, " +
      
        " ?QbInvoiceId, " +
      
        " ?QbCustomerId, " +
      
        " ?QbProjectId, " +
      
        " ?IsModified, " +
      
        " ?IsCreated " +
      
      ")";

      public static void Insert(WorkTransactionQbInvoice workTransactionQbInvoice, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionQbInvoice.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", workTransactionQbInvoice.QbInvoiceId);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", workTransactionQbInvoice.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?QbProjectId", workTransactionQbInvoice.QbProjectId);
      
        Database.PutParameter(dbCommand,"?IsModified", workTransactionQbInvoice.IsModified);
      
        Database.PutParameter(dbCommand,"?IsCreated", workTransactionQbInvoice.IsCreated);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(WorkTransactionQbInvoice workTransactionQbInvoice)
      {
        Insert(workTransactionQbInvoice, null);
      }


      public static void Insert(List<WorkTransactionQbInvoice>  workTransactionQbInvoiceList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionQbInvoice workTransactionQbInvoice in  workTransactionQbInvoiceList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionQbInvoice.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", workTransactionQbInvoice.QbInvoiceId);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", workTransactionQbInvoice.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?QbProjectId", workTransactionQbInvoice.QbProjectId);
      
        Database.PutParameter(dbCommand,"?IsModified", workTransactionQbInvoice.IsModified);
      
        Database.PutParameter(dbCommand,"?IsCreated", workTransactionQbInvoice.IsCreated);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WorkTransactionId",workTransactionQbInvoice.WorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"?QbInvoiceId",workTransactionQbInvoice.QbInvoiceId);
      
        Database.UpdateParameter(dbCommand,"?QbCustomerId",workTransactionQbInvoice.QbCustomerId);
      
        Database.UpdateParameter(dbCommand,"?QbProjectId",workTransactionQbInvoice.QbProjectId);
      
        Database.UpdateParameter(dbCommand,"?IsModified",workTransactionQbInvoice.IsModified);
      
        Database.UpdateParameter(dbCommand,"?IsCreated",workTransactionQbInvoice.IsCreated);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<WorkTransactionQbInvoice>  workTransactionQbInvoiceList)
      {
        Insert(workTransactionQbInvoiceList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WorkTransactionQbInvoice Set "
      
        + " QbCustomerId = ?QbCustomerId, "
      
        + " QbProjectId = ?QbProjectId, "
      
        + " IsModified = ?IsModified, "
      
        + " IsCreated = ?IsCreated "
      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId and  "
        
          + " QbInvoiceId = ?QbInvoiceId "
        
      ;

      public static void Update(WorkTransactionQbInvoice workTransactionQbInvoice, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionQbInvoice.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", workTransactionQbInvoice.QbInvoiceId);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", workTransactionQbInvoice.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?QbProjectId", workTransactionQbInvoice.QbProjectId);
      
        Database.PutParameter(dbCommand,"?IsModified", workTransactionQbInvoice.IsModified);
      
        Database.PutParameter(dbCommand,"?IsCreated", workTransactionQbInvoice.IsCreated);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionQbInvoice workTransactionQbInvoice)
      {
        Update(workTransactionQbInvoice, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " WorkTransactionId, "
      
        + " QbInvoiceId, "
      
        + " QbCustomerId, "
      
        + " QbProjectId, "
      
        + " IsModified, "
      
        + " IsCreated "
      

      + " From WorkTransactionQbInvoice "

      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId and  "
        
          + " QbInvoiceId = ?QbInvoiceId "
        
      ;

      public static WorkTransactionQbInvoice FindByPrimaryKey(
      int workTransactionId,int qbInvoiceId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", qbInvoiceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("WorkTransactionQbInvoice not found, search by primary key");

      }

      public static WorkTransactionQbInvoice FindByPrimaryKey(
      int workTransactionId,int qbInvoiceId
      )
      {
      return FindByPrimaryKey(
      workTransactionId,qbInvoiceId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WorkTransactionQbInvoice workTransactionQbInvoice, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId",workTransactionQbInvoice.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId",workTransactionQbInvoice.QbInvoiceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionQbInvoice workTransactionQbInvoice)
      {
      return Exists(workTransactionQbInvoice, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WorkTransactionQbInvoice limit 1";

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

      public static WorkTransactionQbInvoice Load(IDataReader dataReader, int offset)
      {
      WorkTransactionQbInvoice workTransactionQbInvoice = new WorkTransactionQbInvoice();

      workTransactionQbInvoice.WorkTransactionId = dataReader.GetInt32(0 + offset);
          workTransactionQbInvoice.QbInvoiceId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            workTransactionQbInvoice.QbCustomerId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            workTransactionQbInvoice.QbProjectId = dataReader.GetInt32(3 + offset);
          workTransactionQbInvoice.IsModified = dataReader.GetBoolean(4 + offset);
          workTransactionQbInvoice.IsCreated = dataReader.GetBoolean(5 + offset);
          

      return workTransactionQbInvoice;
      }

      public static WorkTransactionQbInvoice Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WorkTransactionQbInvoice "

      
        + " Where "
        
          + " WorkTransactionId = ?WorkTransactionId and  "
        
          + " QbInvoiceId = ?QbInvoiceId "
        
      ;
      public static void Delete(WorkTransactionQbInvoice workTransactionQbInvoice, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionQbInvoice.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", workTransactionQbInvoice.QbInvoiceId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionQbInvoice workTransactionQbInvoice)
      {
        Delete(workTransactionQbInvoice, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WorkTransactionQbInvoice ";

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

      
        + " WorkTransactionId, "
      
        + " QbInvoiceId, "
      
        + " QbCustomerId, "
      
        + " QbProjectId, "
      
        + " IsModified, "
      
        + " IsCreated "
      

      + " From WorkTransactionQbInvoice ";
      public static List<WorkTransactionQbInvoice> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WorkTransactionQbInvoice> rv = new List<WorkTransactionQbInvoice>();

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

      public static List<WorkTransactionQbInvoice> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionQbInvoice> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WorkTransactionQbInvoice obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return WorkTransactionId == obj.WorkTransactionId && QbInvoiceId == obj.QbInvoiceId && QbCustomerId == obj.QbCustomerId && QbProjectId == obj.QbProjectId && IsModified == obj.IsModified && IsCreated == obj.IsCreated;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionQbInvoice> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionQbInvoice));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionQbInvoice item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionQbInvoice>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionQbInvoice));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionQbInvoice> itemsList
      = new List<WorkTransactionQbInvoice>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionQbInvoice)
      itemsList.Add(deserializedObject as WorkTransactionQbInvoice);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_workTransactionId;
      
        protected int m_qbInvoiceId;
      
        protected int? m_qbCustomerId;
      
        protected int? m_qbProjectId;
      
        protected bool m_isModified;
      
        protected bool m_isCreated;
      
      #endregion

      #region Constructors
      public WorkTransactionQbInvoice(
      int 
          workTransactionId,int 
          qbInvoiceId
      ) : this()
      {
      
        m_workTransactionId = workTransactionId;
      
        m_qbInvoiceId = qbInvoiceId;
      
      }

      


        public WorkTransactionQbInvoice(
        int 
          workTransactionId,int 
          qbInvoiceId,int? 
          qbCustomerId,int? 
          qbProjectId,bool 
          isModified,bool 
          isCreated
        ) : this()
        {
        
          m_workTransactionId = workTransactionId;
        
          m_qbInvoiceId = qbInvoiceId;
        
          m_qbCustomerId = qbCustomerId;
        
          m_qbProjectId = qbProjectId;
        
          m_isModified = isModified;
        
          m_isCreated = isCreated;
        
        }


      
      #endregion

      
        [XmlElement]
        public int WorkTransactionId
        {
        get { return m_workTransactionId;}
        set { m_workTransactionId = value; }
        }
      
        [XmlElement]
        public int QbInvoiceId
        {
        get { return m_qbInvoiceId;}
        set { m_qbInvoiceId = value; }
        }
      
        [XmlElement]
        public int? QbCustomerId
        {
        get { return m_qbCustomerId;}
        set { m_qbCustomerId = value; }
        }
      
        [XmlElement]
        public int? QbProjectId
        {
        get { return m_qbProjectId;}
        set { m_qbProjectId = value; }
        }
      
        [XmlElement]
        public bool IsModified
        {
        get { return m_isModified;}
        set { m_isModified = value; }
        }
      
        [XmlElement]
        public bool IsCreated
        {
        get { return m_isCreated;}
        set { m_isCreated = value; }
        }
      

      public static int FieldsCount
      {
      get { return 6; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    