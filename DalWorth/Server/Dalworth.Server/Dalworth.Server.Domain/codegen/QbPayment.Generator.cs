
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


      public partial class QbPayment : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbPayment ( " +
      
        " TxnID, " +
      
        " QbCustomerId, " +
      
        " TimeCreatedInQb, " +
      
        " TimeModifiedInQb, " +
      
        " EditSequence, " +
      
        " QbAccountListId, " +
      
        " TxnNumber, " +
      
        " TxnDate, " +
      
        " RefNumber, " +
      
        " TotalAmount, " +
      
        " QbPaymentMethodListId, " +
      
        " Memo, " +
      
        " DepositToAccountListId " +
      
      ") Values (" +
      
        " ?TxnID, " +
      
        " ?QbCustomerId, " +
      
        " ?TimeCreatedInQb, " +
      
        " ?TimeModifiedInQb, " +
      
        " ?EditSequence, " +
      
        " ?QbAccountListId, " +
      
        " ?TxnNumber, " +
      
        " ?TxnDate, " +
      
        " ?RefNumber, " +
      
        " ?TotalAmount, " +
      
        " ?QbPaymentMethodListId, " +
      
        " ?Memo, " +
      
        " ?DepositToAccountListId " +
      
      ")";

      public static void Insert(QbPayment qbPayment, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TxnID", qbPayment.TxnID);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbPayment.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?TimeCreatedInQb", qbPayment.TimeCreatedInQb);
      
        Database.PutParameter(dbCommand,"?TimeModifiedInQb", qbPayment.TimeModifiedInQb);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbPayment.EditSequence);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", qbPayment.QbAccountListId);
      
        Database.PutParameter(dbCommand,"?TxnNumber", qbPayment.TxnNumber);
      
        Database.PutParameter(dbCommand,"?TxnDate", qbPayment.TxnDate);
      
        Database.PutParameter(dbCommand,"?RefNumber", qbPayment.RefNumber);
      
        Database.PutParameter(dbCommand,"?TotalAmount", qbPayment.TotalAmount);
      
        Database.PutParameter(dbCommand,"?QbPaymentMethodListId", qbPayment.QbPaymentMethodListId);
      
        Database.PutParameter(dbCommand,"?Memo", qbPayment.Memo);
      
        Database.PutParameter(dbCommand,"?DepositToAccountListId", qbPayment.DepositToAccountListId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbPayment qbPayment)
      {
        Insert(qbPayment, null);
      }


      public static void Insert(List<QbPayment>  qbPaymentList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbPayment qbPayment in  qbPaymentList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TxnID", qbPayment.TxnID);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbPayment.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?TimeCreatedInQb", qbPayment.TimeCreatedInQb);
      
        Database.PutParameter(dbCommand,"?TimeModifiedInQb", qbPayment.TimeModifiedInQb);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbPayment.EditSequence);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", qbPayment.QbAccountListId);
      
        Database.PutParameter(dbCommand,"?TxnNumber", qbPayment.TxnNumber);
      
        Database.PutParameter(dbCommand,"?TxnDate", qbPayment.TxnDate);
      
        Database.PutParameter(dbCommand,"?RefNumber", qbPayment.RefNumber);
      
        Database.PutParameter(dbCommand,"?TotalAmount", qbPayment.TotalAmount);
      
        Database.PutParameter(dbCommand,"?QbPaymentMethodListId", qbPayment.QbPaymentMethodListId);
      
        Database.PutParameter(dbCommand,"?Memo", qbPayment.Memo);
      
        Database.PutParameter(dbCommand,"?DepositToAccountListId", qbPayment.DepositToAccountListId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TxnID",qbPayment.TxnID);
      
        Database.UpdateParameter(dbCommand,"?QbCustomerId",qbPayment.QbCustomerId);
      
        Database.UpdateParameter(dbCommand,"?TimeCreatedInQb",qbPayment.TimeCreatedInQb);
      
        Database.UpdateParameter(dbCommand,"?TimeModifiedInQb",qbPayment.TimeModifiedInQb);
      
        Database.UpdateParameter(dbCommand,"?EditSequence",qbPayment.EditSequence);
      
        Database.UpdateParameter(dbCommand,"?QbAccountListId",qbPayment.QbAccountListId);
      
        Database.UpdateParameter(dbCommand,"?TxnNumber",qbPayment.TxnNumber);
      
        Database.UpdateParameter(dbCommand,"?TxnDate",qbPayment.TxnDate);
      
        Database.UpdateParameter(dbCommand,"?RefNumber",qbPayment.RefNumber);
      
        Database.UpdateParameter(dbCommand,"?TotalAmount",qbPayment.TotalAmount);
      
        Database.UpdateParameter(dbCommand,"?QbPaymentMethodListId",qbPayment.QbPaymentMethodListId);
      
        Database.UpdateParameter(dbCommand,"?Memo",qbPayment.Memo);
      
        Database.UpdateParameter(dbCommand,"?DepositToAccountListId",qbPayment.DepositToAccountListId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbPayment>  qbPaymentList)
      {
        Insert(qbPaymentList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbPayment Set "
      
        + " QbCustomerId = ?QbCustomerId, "
      
        + " TimeCreatedInQb = ?TimeCreatedInQb, "
      
        + " TimeModifiedInQb = ?TimeModifiedInQb, "
      
        + " EditSequence = ?EditSequence, "
      
        + " QbAccountListId = ?QbAccountListId, "
      
        + " TxnNumber = ?TxnNumber, "
      
        + " TxnDate = ?TxnDate, "
      
        + " RefNumber = ?RefNumber, "
      
        + " TotalAmount = ?TotalAmount, "
      
        + " QbPaymentMethodListId = ?QbPaymentMethodListId, "
      
        + " Memo = ?Memo, "
      
        + " DepositToAccountListId = ?DepositToAccountListId "
      
        + " Where "
        
          + " TxnID = ?TxnID "
        
      ;

      public static void Update(QbPayment qbPayment, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TxnID", qbPayment.TxnID);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", qbPayment.QbCustomerId);
      
        Database.PutParameter(dbCommand,"?TimeCreatedInQb", qbPayment.TimeCreatedInQb);
      
        Database.PutParameter(dbCommand,"?TimeModifiedInQb", qbPayment.TimeModifiedInQb);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbPayment.EditSequence);
      
        Database.PutParameter(dbCommand,"?QbAccountListId", qbPayment.QbAccountListId);
      
        Database.PutParameter(dbCommand,"?TxnNumber", qbPayment.TxnNumber);
      
        Database.PutParameter(dbCommand,"?TxnDate", qbPayment.TxnDate);
      
        Database.PutParameter(dbCommand,"?RefNumber", qbPayment.RefNumber);
      
        Database.PutParameter(dbCommand,"?TotalAmount", qbPayment.TotalAmount);
      
        Database.PutParameter(dbCommand,"?QbPaymentMethodListId", qbPayment.QbPaymentMethodListId);
      
        Database.PutParameter(dbCommand,"?Memo", qbPayment.Memo);
      
        Database.PutParameter(dbCommand,"?DepositToAccountListId", qbPayment.DepositToAccountListId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbPayment qbPayment)
      {
        Update(qbPayment, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TxnID, "
      
        + " QbCustomerId, "
      
        + " TimeCreatedInQb, "
      
        + " TimeModifiedInQb, "
      
        + " EditSequence, "
      
        + " QbAccountListId, "
      
        + " TxnNumber, "
      
        + " TxnDate, "
      
        + " RefNumber, "
      
        + " TotalAmount, "
      
        + " QbPaymentMethodListId, "
      
        + " Memo, "
      
        + " DepositToAccountListId "
      

      + " From QbPayment "

      
        + " Where "
        
          + " TxnID = ?TxnID "
        
      ;

      public static QbPayment FindByPrimaryKey(
      String txnID, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TxnID", txnID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("QbPayment not found, search by primary key");

      }

      public static QbPayment FindByPrimaryKey(
      String txnID
      )
      {
      return FindByPrimaryKey(
      txnID, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbPayment qbPayment, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TxnID",qbPayment.TxnID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbPayment qbPayment)
      {
      return Exists(qbPayment, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbPayment limit 1";

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

      public static QbPayment Load(IDataReader dataReader, int offset)
      {
      QbPayment qbPayment = new QbPayment();

      qbPayment.TxnID = dataReader.GetString(0 + offset);
          qbPayment.QbCustomerId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            qbPayment.TimeCreatedInQb = dataReader.GetDateTime(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            qbPayment.TimeModifiedInQb = dataReader.GetDateTime(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            qbPayment.EditSequence = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            qbPayment.QbAccountListId = dataReader.GetString(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            qbPayment.TxnNumber = dataReader.GetInt32(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            qbPayment.TxnDate = dataReader.GetDateTime(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            qbPayment.RefNumber = dataReader.GetString(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            qbPayment.TotalAmount = dataReader.GetDecimal(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            qbPayment.QbPaymentMethodListId = dataReader.GetString(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            qbPayment.Memo = dataReader.GetString(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            qbPayment.DepositToAccountListId = dataReader.GetString(12 + offset);
          

      return qbPayment;
      }

      public static QbPayment Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbPayment "

      
        + " Where "
        
          + " TxnID = ?TxnID "
        
      ;
      public static void Delete(QbPayment qbPayment, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TxnID", qbPayment.TxnID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbPayment qbPayment)
      {
        Delete(qbPayment, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbPayment ";

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

      
        + " TxnID, "
      
        + " QbCustomerId, "
      
        + " TimeCreatedInQb, "
      
        + " TimeModifiedInQb, "
      
        + " EditSequence, "
      
        + " QbAccountListId, "
      
        + " TxnNumber, "
      
        + " TxnDate, "
      
        + " RefNumber, "
      
        + " TotalAmount, "
      
        + " QbPaymentMethodListId, "
      
        + " Memo, "
      
        + " DepositToAccountListId "
      

      + " From QbPayment ";
      public static List<QbPayment> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbPayment> rv = new List<QbPayment>();

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

      public static List<QbPayment> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbPayment> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbPayment obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return TxnID == obj.TxnID && QbCustomerId == obj.QbCustomerId && TimeCreatedInQb == obj.TimeCreatedInQb && TimeModifiedInQb == obj.TimeModifiedInQb && EditSequence == obj.EditSequence && QbAccountListId == obj.QbAccountListId && TxnNumber == obj.TxnNumber && TxnDate == obj.TxnDate && RefNumber == obj.RefNumber && TotalAmount == obj.TotalAmount && QbPaymentMethodListId == obj.QbPaymentMethodListId && Memo == obj.Memo && DepositToAccountListId == obj.DepositToAccountListId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbPayment> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbPayment));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbPayment item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbPayment>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbPayment));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbPayment> itemsList
      = new List<QbPayment>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbPayment)
      itemsList.Add(deserializedObject as QbPayment);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_txnID;
      
        protected int m_qbCustomerId;
      
        protected DateTime? m_timeCreatedInQb;
      
        protected DateTime? m_timeModifiedInQb;
      
        protected String m_editSequence;
      
        protected String m_qbAccountListId;
      
        protected int? m_txnNumber;
      
        protected DateTime? m_txnDate;
      
        protected String m_refNumber;
      
        protected decimal m_totalAmount;
      
        protected String m_qbPaymentMethodListId;
      
        protected String m_memo;
      
        protected String m_depositToAccountListId;
      
      #endregion

      #region Constructors
      public QbPayment(
      String 
          txnID
      ) : this()
      {
      
        m_txnID = txnID;
      
      }

      


        public QbPayment(
        String 
          txnID,int 
          qbCustomerId,DateTime? 
          timeCreatedInQb,DateTime? 
          timeModifiedInQb,String 
          editSequence,String 
          qbAccountListId,int? 
          txnNumber,DateTime? 
          txnDate,String 
          refNumber,decimal 
          totalAmount,String 
          qbPaymentMethodListId,String 
          memo,String 
          depositToAccountListId
        ) : this()
        {
        
          m_txnID = txnID;
        
          m_qbCustomerId = qbCustomerId;
        
          m_timeCreatedInQb = timeCreatedInQb;
        
          m_timeModifiedInQb = timeModifiedInQb;
        
          m_editSequence = editSequence;
        
          m_qbAccountListId = qbAccountListId;
        
          m_txnNumber = txnNumber;
        
          m_txnDate = txnDate;
        
          m_refNumber = refNumber;
        
          m_totalAmount = totalAmount;
        
          m_qbPaymentMethodListId = qbPaymentMethodListId;
        
          m_memo = memo;
        
          m_depositToAccountListId = depositToAccountListId;
        
        }


      
      #endregion

      
        [XmlElement]
        public String TxnID
        {
        get { return m_txnID;}
        set { m_txnID = value; }
        }
      
        [XmlElement]
        public int QbCustomerId
        {
        get { return m_qbCustomerId;}
        set { m_qbCustomerId = value; }
        }
      
        [XmlElement]
        public DateTime? TimeCreatedInQb
        {
        get { return m_timeCreatedInQb;}
        set { m_timeCreatedInQb = value; }
        }
      
        [XmlElement]
        public DateTime? TimeModifiedInQb
        {
        get { return m_timeModifiedInQb;}
        set { m_timeModifiedInQb = value; }
        }
      
        [XmlElement]
        public String EditSequence
        {
        get { return m_editSequence;}
        set { m_editSequence = value; }
        }
      
        [XmlElement]
        public String QbAccountListId
        {
        get { return m_qbAccountListId;}
        set { m_qbAccountListId = value; }
        }
      
        [XmlElement]
        public int? TxnNumber
        {
        get { return m_txnNumber;}
        set { m_txnNumber = value; }
        }
      
        [XmlElement]
        public DateTime? TxnDate
        {
        get { return m_txnDate;}
        set { m_txnDate = value; }
        }
      
        [XmlElement]
        public String RefNumber
        {
        get { return m_refNumber;}
        set { m_refNumber = value; }
        }
      
        [XmlElement]
        public decimal TotalAmount
        {
        get { return m_totalAmount;}
        set { m_totalAmount = value; }
        }
      
        [XmlElement]
        public String QbPaymentMethodListId
        {
        get { return m_qbPaymentMethodListId;}
        set { m_qbPaymentMethodListId = value; }
        }
      
        [XmlElement]
        public String Memo
        {
        get { return m_memo;}
        set { m_memo = value; }
        }
      
        [XmlElement]
        public String DepositToAccountListId
        {
        get { return m_depositToAccountListId;}
        set { m_depositToAccountListId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 13; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    