
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


      public partial class QbSyncLogDetail : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbSyncLogDetail ( " +
      
        " QbSyncLogId, " +
      
        " CompletedDate, " +
      
        " QbSyncActionId, " +
      
        " IsSuccess, " +
      
        " ErrorMessage, " +
      
        " QbCustomerId, " +
      
        " QbInvoiceId, " +
      
        " QbXmlRequest, " +
      
        " QbXmlResponse, " +
      
        " TxnID " +
      
      ") Values (" +
      
        " ?QbSyncLogId, " +
      
        " ?CompletedDate, " +
      
        " ?QbSyncActionId, " +
      
        " ?IsSuccess, " +
      
        " ?ErrorMessage, " +
      
        " ?QbCustomerId, " +
      
        " ?QbInvoiceId, " +
      
        " ?QbXmlRequest, " +
      
        " ?QbXmlResponse, " +
      
        " ?TxnID " +
      
      ")";

      public static void Insert(QbSyncLogDetail qbSyncLogDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?QbSyncLogId", qbSyncLogDetail.QbSyncLogId);
      
        Database.PutParameter(dbCommand,"?CompletedDate", qbSyncLogDetail.CompletedDate);
      
        Database.PutParameter(dbCommand,"?QbSyncActionId", qbSyncLogDetail.QbSyncActionId);
      
        Database.PutParameter(dbCommand,"?IsSuccess", qbSyncLogDetail.IsSuccess);
      
        Database.PutParameter(dbCommand,"?ErrorMessage", qbSyncLogDetail.ErrorMessage);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbSyncLogDetail.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", qbSyncLogDetail.QbInvoiceId);
      
        Database.PutParameter(dbCommand,"?QbXmlRequest", qbSyncLogDetail.QbXmlRequest);
      
        Database.PutParameter(dbCommand,"?QbXmlResponse", qbSyncLogDetail.QbXmlResponse);
      
        Database.PutParameter(dbCommand,"?TxnID", qbSyncLogDetail.TxnID);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        qbSyncLogDetail.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(QbSyncLogDetail qbSyncLogDetail)
      {
        Insert(qbSyncLogDetail, null);
      }


      public static void Insert(List<QbSyncLogDetail>  qbSyncLogDetailList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbSyncLogDetail qbSyncLogDetail in  qbSyncLogDetailList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?QbSyncLogId", qbSyncLogDetail.QbSyncLogId);
      
        Database.PutParameter(dbCommand,"?CompletedDate", qbSyncLogDetail.CompletedDate);
      
        Database.PutParameter(dbCommand,"?QbSyncActionId", qbSyncLogDetail.QbSyncActionId);
      
        Database.PutParameter(dbCommand,"?IsSuccess", qbSyncLogDetail.IsSuccess);
      
        Database.PutParameter(dbCommand,"?ErrorMessage", qbSyncLogDetail.ErrorMessage);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbSyncLogDetail.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", qbSyncLogDetail.QbInvoiceId);
      
        Database.PutParameter(dbCommand,"?QbXmlRequest", qbSyncLogDetail.QbXmlRequest);
      
        Database.PutParameter(dbCommand,"?QbXmlResponse", qbSyncLogDetail.QbXmlResponse);
      
        Database.PutParameter(dbCommand,"?TxnID", qbSyncLogDetail.TxnID);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?QbSyncLogId",qbSyncLogDetail.QbSyncLogId);
      
        Database.UpdateParameter(dbCommand,"?CompletedDate",qbSyncLogDetail.CompletedDate);
      
        Database.UpdateParameter(dbCommand,"?QbSyncActionId",qbSyncLogDetail.QbSyncActionId);
      
        Database.UpdateParameter(dbCommand,"?IsSuccess",qbSyncLogDetail.IsSuccess);
      
        Database.UpdateParameter(dbCommand,"?ErrorMessage",qbSyncLogDetail.ErrorMessage);
      
        Database.UpdateParameter(dbCommand,"?QbCustomerId",qbSyncLogDetail.QbCustomerId);
      
        Database.UpdateParameter(dbCommand,"?QbInvoiceId",qbSyncLogDetail.QbInvoiceId);
      
        Database.UpdateParameter(dbCommand,"?QbXmlRequest",qbSyncLogDetail.QbXmlRequest);
      
        Database.UpdateParameter(dbCommand,"?QbXmlResponse",qbSyncLogDetail.QbXmlResponse);
      
        Database.UpdateParameter(dbCommand,"?TxnID",qbSyncLogDetail.TxnID);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        qbSyncLogDetail.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<QbSyncLogDetail>  qbSyncLogDetailList)
      {
        Insert(qbSyncLogDetailList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbSyncLogDetail Set "
      
        + " QbSyncLogId = ?QbSyncLogId, "
      
        + " CompletedDate = ?CompletedDate, "
      
        + " QbSyncActionId = ?QbSyncActionId, "
      
        + " IsSuccess = ?IsSuccess, "
      
        + " ErrorMessage = ?ErrorMessage, "
      
        + " QbCustomerId = ?QbCustomerId, "
      
        + " QbInvoiceId = ?QbInvoiceId, "
      
        + " QbXmlRequest = ?QbXmlRequest, "
      
        + " QbXmlResponse = ?QbXmlResponse, "
      
        + " TxnID = ?TxnID "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(QbSyncLogDetail qbSyncLogDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qbSyncLogDetail.ID);
      
        Database.PutParameter(dbCommand,"?QbSyncLogId", qbSyncLogDetail.QbSyncLogId);
      
        Database.PutParameter(dbCommand,"?CompletedDate", qbSyncLogDetail.CompletedDate);
      
        Database.PutParameter(dbCommand,"?QbSyncActionId", qbSyncLogDetail.QbSyncActionId);
      
        Database.PutParameter(dbCommand,"?IsSuccess", qbSyncLogDetail.IsSuccess);
      
        Database.PutParameter(dbCommand,"?ErrorMessage", qbSyncLogDetail.ErrorMessage);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbSyncLogDetail.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", qbSyncLogDetail.QbInvoiceId);
      
        Database.PutParameter(dbCommand,"?QbXmlRequest", qbSyncLogDetail.QbXmlRequest);
      
        Database.PutParameter(dbCommand,"?QbXmlResponse", qbSyncLogDetail.QbXmlResponse);
      
        Database.PutParameter(dbCommand,"?TxnID", qbSyncLogDetail.TxnID);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbSyncLogDetail qbSyncLogDetail)
      {
        Update(qbSyncLogDetail, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " QbSyncLogId, "
      
        + " CompletedDate, "
      
        + " QbSyncActionId, "
      
        + " IsSuccess, "
      
        + " ErrorMessage, "
      
        + " QbCustomerId, "
      
        + " QbInvoiceId, "
      
        + " QbXmlRequest, "
      
        + " QbXmlResponse, "
      
        + " TxnID "
      

      + " From QbSyncLogDetail "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static QbSyncLogDetail FindByPrimaryKey(
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
      throw new DataNotFoundException("QbSyncLogDetail not found, search by primary key");

      }

      public static QbSyncLogDetail FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbSyncLogDetail qbSyncLogDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",qbSyncLogDetail.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbSyncLogDetail qbSyncLogDetail)
      {
      return Exists(qbSyncLogDetail, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbSyncLogDetail limit 1";

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

      public static QbSyncLogDetail Load(IDataReader dataReader, int offset)
      {
      QbSyncLogDetail qbSyncLogDetail = new QbSyncLogDetail();

      qbSyncLogDetail.ID = dataReader.GetInt32(0 + offset);
          qbSyncLogDetail.QbSyncLogId = dataReader.GetInt32(1 + offset);
          qbSyncLogDetail.CompletedDate = dataReader.GetDateTime(2 + offset);
          qbSyncLogDetail.QbSyncActionId = dataReader.GetInt32(3 + offset);
          qbSyncLogDetail.IsSuccess = dataReader.GetBoolean(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            qbSyncLogDetail.ErrorMessage = dataReader.GetString(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            qbSyncLogDetail.QbCustomerId = dataReader.GetInt32(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            qbSyncLogDetail.QbInvoiceId = dataReader.GetInt32(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            qbSyncLogDetail.QbXmlRequest = dataReader.GetString(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            qbSyncLogDetail.QbXmlResponse = dataReader.GetString(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            qbSyncLogDetail.TxnID = dataReader.GetString(10 + offset);
          

      return qbSyncLogDetail;
      }

      public static QbSyncLogDetail Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbSyncLogDetail "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(QbSyncLogDetail qbSyncLogDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", qbSyncLogDetail.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbSyncLogDetail qbSyncLogDetail)
      {
        Delete(qbSyncLogDetail, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbSyncLogDetail ";

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
      
        + " QbSyncLogId, "
      
        + " CompletedDate, "
      
        + " QbSyncActionId, "
      
        + " IsSuccess, "
      
        + " ErrorMessage, "
      
        + " QbCustomerId, "
      
        + " QbInvoiceId, "
      
        + " QbXmlRequest, "
      
        + " QbXmlResponse, "
      
        + " TxnID "
      

      + " From QbSyncLogDetail ";
      public static List<QbSyncLogDetail> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbSyncLogDetail> rv = new List<QbSyncLogDetail>();

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

      public static List<QbSyncLogDetail> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbSyncLogDetail> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbSyncLogDetail obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && QbSyncLogId == obj.QbSyncLogId && CompletedDate == obj.CompletedDate && QbSyncActionId == obj.QbSyncActionId && IsSuccess == obj.IsSuccess && ErrorMessage == obj.ErrorMessage && QbCustomerId == obj.QbCustomerId && QbInvoiceId == obj.QbInvoiceId && QbXmlRequest == obj.QbXmlRequest && QbXmlResponse == obj.QbXmlResponse && TxnID == obj.TxnID;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbSyncLogDetail> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbSyncLogDetail));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbSyncLogDetail item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbSyncLogDetail>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbSyncLogDetail));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbSyncLogDetail> itemsList
      = new List<QbSyncLogDetail>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbSyncLogDetail)
      itemsList.Add(deserializedObject as QbSyncLogDetail);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_qbSyncLogId;
      
        protected DateTime m_completedDate;
      
        protected int m_qbSyncActionId;
      
        protected bool m_isSuccess;
      
        protected String m_errorMessage;
      
        protected int? m_qbCustomerId;
      
        protected int? m_qbInvoiceId;
      
        protected String m_qbXmlRequest;
      
        protected String m_qbXmlResponse;
      
        protected String m_txnID;
      
      #endregion

      #region Constructors
      public QbSyncLogDetail(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public QbSyncLogDetail(
        int 
          iD,int 
          qbSyncLogId,DateTime 
          completedDate,int 
          qbSyncActionId,bool 
          isSuccess,String 
          errorMessage,int? 
          qbCustomerId,int? 
          qbInvoiceId,String 
          qbXmlRequest,String 
          qbXmlResponse,String 
          txnID
        ) : this()
        {
        
          m_iD = iD;
        
          m_qbSyncLogId = qbSyncLogId;
        
          m_completedDate = completedDate;
        
          m_qbSyncActionId = qbSyncActionId;
        
          m_isSuccess = isSuccess;
        
          m_errorMessage = errorMessage;
        
          m_qbCustomerId = qbCustomerId;
        
          m_qbInvoiceId = qbInvoiceId;
        
          m_qbXmlRequest = qbXmlRequest;
        
          m_qbXmlResponse = qbXmlResponse;
        
          m_txnID = txnID;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int QbSyncLogId
        {
        get { return m_qbSyncLogId;}
        set { m_qbSyncLogId = value; }
        }
      
        [XmlElement]
        public DateTime CompletedDate
        {
        get { return m_completedDate;}
        set { m_completedDate = value; }
        }
      
        [XmlElement]
        public int QbSyncActionId
        {
        get { return m_qbSyncActionId;}
        set { m_qbSyncActionId = value; }
        }
      
        [XmlElement]
        public bool IsSuccess
        {
        get { return m_isSuccess;}
        set { m_isSuccess = value; }
        }
      
        [XmlElement]
        public String ErrorMessage
        {
        get { return m_errorMessage;}
        set { m_errorMessage = value; }
        }
      
        [XmlElement]
        public int? QbCustomerId
        {
        get { return m_qbCustomerId;}
        set { m_qbCustomerId = value; }
        }
      
        [XmlElement]
        public int? QbInvoiceId
        {
        get { return m_qbInvoiceId;}
        set { m_qbInvoiceId = value; }
        }
      
        [XmlElement]
        public String QbXmlRequest
        {
        get { return m_qbXmlRequest;}
        set { m_qbXmlRequest = value; }
        }
      
        [XmlElement]
        public String QbXmlResponse
        {
        get { return m_qbXmlResponse;}
        set { m_qbXmlResponse = value; }
        }
      
        [XmlElement]
        public String TxnID
        {
        get { return m_txnID;}
        set { m_txnID = value; }
        }
      

      public static int FieldsCount
      {
      get { return 11; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    