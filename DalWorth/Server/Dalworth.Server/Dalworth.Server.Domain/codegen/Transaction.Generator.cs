
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


      public partial class Transaction : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Transaction ( " +
      
        " ServmanTransactionId, " +
      
        " ServmanWorkflowId, " +
      
        " ManualRecordedCallerId, " +
      
        " TicketNumber, " +
      
        " CustomerId, " +
      
        " CustomerName, " +
      
        " ServiceType, " +
      
        " TransactionCode, " +
      
        " UserName, " +
      
        " Extension, " +
      
        " TimeCreated, " +
      
        " DigiumLogItemId, " +
      
        " MatchCriteria " +
      
      ") Values (" +
      
        " ?ServmanTransactionId, " +
      
        " ?ServmanWorkflowId, " +
      
        " ?ManualRecordedCallerId, " +
      
        " ?TicketNumber, " +
      
        " ?CustomerId, " +
      
        " ?CustomerName, " +
      
        " ?ServiceType, " +
      
        " ?TransactionCode, " +
      
        " ?UserName, " +
      
        " ?Extension, " +
      
        " ?TimeCreated, " +
      
        " ?DigiumLogItemId, " +
      
        " ?MatchCriteria " +
      
      ")";

      public static void Insert(Transaction transaction, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ServmanTransactionId", transaction.ServmanTransactionId);
      
        Database.PutParameter(dbCommand,"?ServmanWorkflowId", transaction.ServmanWorkflowId);
      
        Database.PutParameter(dbCommand,"?ManualRecordedCallerId", transaction.ManualRecordedCallerId);
      
        Database.PutParameter(dbCommand,"?TicketNumber", transaction.TicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerId", transaction.CustomerId);
      
        Database.PutParameter(dbCommand,"?CustomerName", transaction.CustomerName);
      
        Database.PutParameter(dbCommand,"?ServiceType", transaction.ServiceType);
      
        Database.PutParameter(dbCommand,"?TransactionCode", transaction.TransactionCode);
      
        Database.PutParameter(dbCommand,"?UserName", transaction.UserName);
      
        Database.PutParameter(dbCommand,"?Extension", transaction.Extension);
      
        Database.PutParameter(dbCommand,"?TimeCreated", transaction.TimeCreated);
      
        Database.PutParameter(dbCommand,"?DigiumLogItemId", transaction.DigiumLogItemId);
      
        Database.PutParameter(dbCommand,"?MatchCriteria", transaction.MatchCriteria);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        transaction.ID = Convert.ToInt64(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Transaction transaction)
      {
        Insert(transaction, null);
      }


      public static void Insert(List<Transaction>  transactionList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Transaction transaction in  transactionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ServmanTransactionId", transaction.ServmanTransactionId);
      
        Database.PutParameter(dbCommand,"?ServmanWorkflowId", transaction.ServmanWorkflowId);
      
        Database.PutParameter(dbCommand,"?ManualRecordedCallerId", transaction.ManualRecordedCallerId);
      
        Database.PutParameter(dbCommand,"?TicketNumber", transaction.TicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerId", transaction.CustomerId);
      
        Database.PutParameter(dbCommand,"?CustomerName", transaction.CustomerName);
      
        Database.PutParameter(dbCommand,"?ServiceType", transaction.ServiceType);
      
        Database.PutParameter(dbCommand,"?TransactionCode", transaction.TransactionCode);
      
        Database.PutParameter(dbCommand,"?UserName", transaction.UserName);
      
        Database.PutParameter(dbCommand,"?Extension", transaction.Extension);
      
        Database.PutParameter(dbCommand,"?TimeCreated", transaction.TimeCreated);
      
        Database.PutParameter(dbCommand,"?DigiumLogItemId", transaction.DigiumLogItemId);
      
        Database.PutParameter(dbCommand,"?MatchCriteria", transaction.MatchCriteria);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ServmanTransactionId",transaction.ServmanTransactionId);
      
        Database.UpdateParameter(dbCommand,"?ServmanWorkflowId",transaction.ServmanWorkflowId);
      
        Database.UpdateParameter(dbCommand,"?ManualRecordedCallerId",transaction.ManualRecordedCallerId);
      
        Database.UpdateParameter(dbCommand,"?TicketNumber",transaction.TicketNumber);
      
        Database.UpdateParameter(dbCommand,"?CustomerId",transaction.CustomerId);
      
        Database.UpdateParameter(dbCommand,"?CustomerName",transaction.CustomerName);
      
        Database.UpdateParameter(dbCommand,"?ServiceType",transaction.ServiceType);
      
        Database.UpdateParameter(dbCommand,"?TransactionCode",transaction.TransactionCode);
      
        Database.UpdateParameter(dbCommand,"?UserName",transaction.UserName);
      
        Database.UpdateParameter(dbCommand,"?Extension",transaction.Extension);
      
        Database.UpdateParameter(dbCommand,"?TimeCreated",transaction.TimeCreated);
      
        Database.UpdateParameter(dbCommand,"?DigiumLogItemId",transaction.DigiumLogItemId);
      
        Database.UpdateParameter(dbCommand,"?MatchCriteria",transaction.MatchCriteria);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        transaction.ID = Convert.ToInt64(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Transaction>  transactionList)
      {
        Insert(transactionList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Transaction Set "
      
        + " ServmanTransactionId = ?ServmanTransactionId, "
      
        + " ServmanWorkflowId = ?ServmanWorkflowId, "
      
        + " ManualRecordedCallerId = ?ManualRecordedCallerId, "
      
        + " TicketNumber = ?TicketNumber, "
      
        + " CustomerId = ?CustomerId, "
      
        + " CustomerName = ?CustomerName, "
      
        + " ServiceType = ?ServiceType, "
      
        + " TransactionCode = ?TransactionCode, "
      
        + " UserName = ?UserName, "
      
        + " Extension = ?Extension, "
      
        + " TimeCreated = ?TimeCreated, "
      
        + " DigiumLogItemId = ?DigiumLogItemId, "
      
        + " MatchCriteria = ?MatchCriteria "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Transaction transaction, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", transaction.ID);
      
        Database.PutParameter(dbCommand,"?ServmanTransactionId", transaction.ServmanTransactionId);
      
        Database.PutParameter(dbCommand,"?ServmanWorkflowId", transaction.ServmanWorkflowId);
      
        Database.PutParameter(dbCommand,"?ManualRecordedCallerId", transaction.ManualRecordedCallerId);
      
        Database.PutParameter(dbCommand,"?TicketNumber", transaction.TicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerId", transaction.CustomerId);
      
        Database.PutParameter(dbCommand,"?CustomerName", transaction.CustomerName);
      
        Database.PutParameter(dbCommand,"?ServiceType", transaction.ServiceType);
      
        Database.PutParameter(dbCommand,"?TransactionCode", transaction.TransactionCode);
      
        Database.PutParameter(dbCommand,"?UserName", transaction.UserName);
      
        Database.PutParameter(dbCommand,"?Extension", transaction.Extension);
      
        Database.PutParameter(dbCommand,"?TimeCreated", transaction.TimeCreated);
      
        Database.PutParameter(dbCommand,"?DigiumLogItemId", transaction.DigiumLogItemId);
      
        Database.PutParameter(dbCommand,"?MatchCriteria", transaction.MatchCriteria);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Transaction transaction)
      {
        Update(transaction, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ServmanTransactionId, "
      
        + " ServmanWorkflowId, "
      
        + " ManualRecordedCallerId, "
      
        + " TicketNumber, "
      
        + " CustomerId, "
      
        + " CustomerName, "
      
        + " ServiceType, "
      
        + " TransactionCode, "
      
        + " UserName, "
      
        + " Extension, "
      
        + " TimeCreated, "
      
        + " DigiumLogItemId, "
      
        + " MatchCriteria "
      

      + " From Transaction "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Transaction FindByPrimaryKey(
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
      throw new DataNotFoundException("Transaction not found, search by primary key");

      }

      public static Transaction FindByPrimaryKey(
      long iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Transaction transaction, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",transaction.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Transaction transaction)
      {
      return Exists(transaction, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Transaction limit 1";

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

      public static Transaction Load(IDataReader dataReader, int offset)
      {
      Transaction transaction = new Transaction();

      transaction.ID = dataReader.GetInt64(0 + offset);
          transaction.ServmanTransactionId = dataReader.GetString(1 + offset);
          transaction.ServmanWorkflowId = dataReader.GetString(2 + offset);
          transaction.ManualRecordedCallerId = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            transaction.TicketNumber = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            transaction.CustomerId = dataReader.GetInt32(5 + offset);
          transaction.CustomerName = dataReader.GetString(6 + offset);
          transaction.ServiceType = dataReader.GetInt32(7 + offset);
          transaction.TransactionCode = dataReader.GetString(8 + offset);
          transaction.UserName = dataReader.GetString(9 + offset);
          transaction.Extension = dataReader.GetString(10 + offset);
          transaction.TimeCreated = dataReader.GetDateTime(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            transaction.DigiumLogItemId = dataReader.GetInt64(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            transaction.MatchCriteria = dataReader.GetInt32(13 + offset);
          

      return transaction;
      }

      public static Transaction Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Transaction "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Transaction transaction, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", transaction.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Transaction transaction)
      {
        Delete(transaction, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Transaction ";

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
      
        + " ServmanTransactionId, "
      
        + " ServmanWorkflowId, "
      
        + " ManualRecordedCallerId, "
      
        + " TicketNumber, "
      
        + " CustomerId, "
      
        + " CustomerName, "
      
        + " ServiceType, "
      
        + " TransactionCode, "
      
        + " UserName, "
      
        + " Extension, "
      
        + " TimeCreated, "
      
        + " DigiumLogItemId, "
      
        + " MatchCriteria "
      

      + " From Transaction ";
      public static List<Transaction> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Transaction> rv = new List<Transaction>();

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

      public static List<Transaction> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Transaction> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Transaction obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && ServmanTransactionId == obj.ServmanTransactionId && ServmanWorkflowId == obj.ServmanWorkflowId && ManualRecordedCallerId == obj.ManualRecordedCallerId && TicketNumber == obj.TicketNumber && CustomerId == obj.CustomerId && CustomerName == obj.CustomerName && ServiceType == obj.ServiceType && TransactionCode == obj.TransactionCode && UserName == obj.UserName && Extension == obj.Extension && TimeCreated == obj.TimeCreated && DigiumLogItemId == obj.DigiumLogItemId && MatchCriteria == obj.MatchCriteria;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Transaction> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Transaction));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Transaction item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Transaction>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Transaction));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Transaction> itemsList
      = new List<Transaction>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Transaction)
      itemsList.Add(deserializedObject as Transaction);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected long m_iD;
      
        protected String m_servmanTransactionId;
      
        protected String m_servmanWorkflowId;
      
        protected String m_manualRecordedCallerId;
      
        protected String m_ticketNumber;
      
        protected int? m_customerId;
      
        protected String m_customerName;
      
        protected int m_serviceType;
      
        protected String m_transactionCode;
      
        protected String m_userName;
      
        protected String m_extension;
      
        protected DateTime m_timeCreated;
      
        protected long? m_digiumLogItemId;
      
        protected int? m_matchCriteria;
      
      #endregion

      #region Constructors
      public Transaction(
      long 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Transaction(
        long 
          iD,String 
          servmanTransactionId,String 
          servmanWorkflowId,String 
          manualRecordedCallerId,String 
          ticketNumber,int? 
          customerId,String 
          customerName,int 
          serviceType,String 
          transactionCode,String 
          userName,String 
          extension,DateTime 
          timeCreated,long? 
          digiumLogItemId,int? 
          matchCriteria
        ) : this()
        {
        
          m_iD = iD;
        
          m_servmanTransactionId = servmanTransactionId;
        
          m_servmanWorkflowId = servmanWorkflowId;
        
          m_manualRecordedCallerId = manualRecordedCallerId;
        
          m_ticketNumber = ticketNumber;
        
          m_customerId = customerId;
        
          m_customerName = customerName;
        
          m_serviceType = serviceType;
        
          m_transactionCode = transactionCode;
        
          m_userName = userName;
        
          m_extension = extension;
        
          m_timeCreated = timeCreated;
        
          m_digiumLogItemId = digiumLogItemId;
        
          m_matchCriteria = matchCriteria;
        
        }


      
      #endregion

      
        [XmlElement]
        public long ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String ServmanTransactionId
        {
        get { return m_servmanTransactionId;}
        set { m_servmanTransactionId = value; }
        }
      
        [XmlElement]
        public String ServmanWorkflowId
        {
        get { return m_servmanWorkflowId;}
        set { m_servmanWorkflowId = value; }
        }
      
        [XmlElement]
        public String ManualRecordedCallerId
        {
        get { return m_manualRecordedCallerId;}
        set { m_manualRecordedCallerId = value; }
        }
      
        [XmlElement]
        public String TicketNumber
        {
        get { return m_ticketNumber;}
        set { m_ticketNumber = value; }
        }
      
        [XmlElement]
        public int? CustomerId
        {
        get { return m_customerId;}
        set { m_customerId = value; }
        }
      
        [XmlElement]
        public String CustomerName
        {
        get { return m_customerName;}
        set { m_customerName = value; }
        }
      
        [XmlElement]
        public int ServiceType
        {
        get { return m_serviceType;}
        set { m_serviceType = value; }
        }
      
        [XmlElement]
        public String TransactionCode
        {
        get { return m_transactionCode;}
        set { m_transactionCode = value; }
        }
      
        [XmlElement]
        public String UserName
        {
        get { return m_userName;}
        set { m_userName = value; }
        }
      
        [XmlElement]
        public String Extension
        {
        get { return m_extension;}
        set { m_extension = value; }
        }
      
        [XmlElement]
        public DateTime TimeCreated
        {
        get { return m_timeCreated;}
        set { m_timeCreated = value; }
        }
      
        [XmlElement]
        public long? DigiumLogItemId
        {
        get { return m_digiumLogItemId;}
        set { m_digiumLogItemId = value; }
        }
      
        [XmlElement]
        public int? MatchCriteria
        {
        get { return m_matchCriteria;}
        set { m_matchCriteria = value; }
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

    