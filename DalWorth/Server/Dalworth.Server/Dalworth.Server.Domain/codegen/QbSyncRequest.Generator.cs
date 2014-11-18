
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


      public partial class QbSyncRequest : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbSyncRequest ( " +
      
        " RequestDate, " +
      
        " QbSyncActionId, " +
      
        " QbCustomerId, " +
      
        " QbInvoiceId " +
      
      ") Values (" +
      
        " ?RequestDate, " +
      
        " ?QbSyncActionId, " +
      
        " ?QbCustomerId, " +
      
        " ?QbInvoiceId " +
      
      ")";

      public static void Insert(QbSyncRequest qbSyncRequest, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?RequestDate", qbSyncRequest.RequestDate);
      
        Database.PutParameter(dbCommand,"?QbSyncActionId", qbSyncRequest.QbSyncActionId);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbSyncRequest.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", qbSyncRequest.QbInvoiceId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        qbSyncRequest.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(QbSyncRequest qbSyncRequest)
      {
        Insert(qbSyncRequest, null);
      }


      public static void Insert(List<QbSyncRequest>  qbSyncRequestList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbSyncRequest qbSyncRequest in  qbSyncRequestList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?RequestDate", qbSyncRequest.RequestDate);
      
        Database.PutParameter(dbCommand,"?QbSyncActionId", qbSyncRequest.QbSyncActionId);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbSyncRequest.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", qbSyncRequest.QbInvoiceId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?RequestDate",qbSyncRequest.RequestDate);
      
        Database.UpdateParameter(dbCommand,"?QbSyncActionId",qbSyncRequest.QbSyncActionId);
      
        Database.UpdateParameter(dbCommand,"?QbCustomerId",qbSyncRequest.QbCustomerId);
      
        Database.UpdateParameter(dbCommand,"?QbInvoiceId",qbSyncRequest.QbInvoiceId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        qbSyncRequest.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<QbSyncRequest>  qbSyncRequestList)
      {
        Insert(qbSyncRequestList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbSyncRequest Set "
      
        + " RequestDate = ?RequestDate, "
      
        + " QbSyncActionId = ?QbSyncActionId, "
      
        + " QbCustomerId = ?QbCustomerId, "
      
        + " QbInvoiceId = ?QbInvoiceId "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(QbSyncRequest qbSyncRequest, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qbSyncRequest.ID);
      
        Database.PutParameter(dbCommand,"?RequestDate", qbSyncRequest.RequestDate);
      
        Database.PutParameter(dbCommand,"?QbSyncActionId", qbSyncRequest.QbSyncActionId);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbSyncRequest.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?QbInvoiceId", qbSyncRequest.QbInvoiceId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbSyncRequest qbSyncRequest)
      {
        Update(qbSyncRequest, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " RequestDate, "
      
        + " QbSyncActionId, "
      
        + " QbCustomerId, "
      
        + " QbInvoiceId "
      

      + " From QbSyncRequest "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static QbSyncRequest FindByPrimaryKey(
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
      throw new DataNotFoundException("QbSyncRequest not found, search by primary key");

      }

      public static QbSyncRequest FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbSyncRequest qbSyncRequest, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",qbSyncRequest.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbSyncRequest qbSyncRequest)
      {
      return Exists(qbSyncRequest, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbSyncRequest limit 1";

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

      public static QbSyncRequest Load(IDataReader dataReader, int offset)
      {
      QbSyncRequest qbSyncRequest = new QbSyncRequest();

      qbSyncRequest.ID = dataReader.GetInt32(0 + offset);
          qbSyncRequest.RequestDate = dataReader.GetDateTime(1 + offset);
          qbSyncRequest.QbSyncActionId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            qbSyncRequest.QbCustomerId = dataReader.GetInt32(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            qbSyncRequest.QbInvoiceId = dataReader.GetInt32(4 + offset);
          

      return qbSyncRequest;
      }

      public static QbSyncRequest Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbSyncRequest "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(QbSyncRequest qbSyncRequest, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", qbSyncRequest.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbSyncRequest qbSyncRequest)
      {
        Delete(qbSyncRequest, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbSyncRequest ";

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
      
        + " RequestDate, "
      
        + " QbSyncActionId, "
      
        + " QbCustomerId, "
      
        + " QbInvoiceId "
      

      + " From QbSyncRequest ";
      public static List<QbSyncRequest> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbSyncRequest> rv = new List<QbSyncRequest>();

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

      public static List<QbSyncRequest> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbSyncRequest> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbSyncRequest obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && RequestDate == obj.RequestDate && QbSyncActionId == obj.QbSyncActionId && QbCustomerId == obj.QbCustomerId && QbInvoiceId == obj.QbInvoiceId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbSyncRequest> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbSyncRequest));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbSyncRequest item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbSyncRequest>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbSyncRequest));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbSyncRequest> itemsList
      = new List<QbSyncRequest>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbSyncRequest)
      itemsList.Add(deserializedObject as QbSyncRequest);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected DateTime m_requestDate;
      
        protected int m_qbSyncActionId;
      
        protected int? m_qbCustomerId;
      
        protected int? m_qbInvoiceId;
      
      #endregion

      #region Constructors
      public QbSyncRequest(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public QbSyncRequest(
        int 
          iD,DateTime 
          requestDate,int 
          qbSyncActionId,int? 
          qbCustomerId,int? 
          qbInvoiceId
        ) : this()
        {
        
          m_iD = iD;
        
          m_requestDate = requestDate;
        
          m_qbSyncActionId = qbSyncActionId;
        
          m_qbCustomerId = qbCustomerId;
        
          m_qbInvoiceId = qbInvoiceId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public DateTime RequestDate
        {
        get { return m_requestDate;}
        set { m_requestDate = value; }
        }
      
        [XmlElement]
        public int QbSyncActionId
        {
        get { return m_qbSyncActionId;}
        set { m_qbSyncActionId = value; }
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
      

      public static int FieldsCount
      {
      get { return 5; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    