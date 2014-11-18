
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


      public partial class Order : DomainObject, ICloneable
      {

      #region Store


      #region Insert

          private const String SqlInsert = "Insert Into `Order` ( " +
      
        " TicketNumber, " +
      
        " CustomerId, " +
      
        " OrderSourceId, " +
      
        " AdvertisingSourceId, " +
      
        " ScheduleDate, " +
      
        " TransNum, " +
      
        " ServiceType, " +
      
        " TransactionType, " +
      
        " TransactionStatus, " +
      
        " CompletionType, " +
      
        " Amount, " +
      
        " DateCompleted " +
      
      ") Values (" +
      
        " ?TicketNumber, " +
      
        " ?CustomerId, " +
      
        " ?OrderSourceId, " +
      
        " ?AdvertisingSourceId, " +
      
        " ?ScheduleDate, " +
      
        " ?TransNum, " +
      
        " ?ServiceType, " +
      
        " ?TransactionType, " +
      
        " ?TransactionStatus, " +
      
        " ?CompletionType, " +
      
        " ?Amount, " +
      
        " ?DateCompleted " +
      
      ")";

      public static void Insert(Order order, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TicketNumber", order.TicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerId", order.CustomerId);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", order.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", order.AdvertisingSourceId);
      
        Database.PutParameter(dbCommand,"?ScheduleDate", order.ScheduleDate);
      
        Database.PutParameter(dbCommand,"?TransNum", order.TransNum);
      
        Database.PutParameter(dbCommand,"?ServiceType", order.ServiceType);
      
        Database.PutParameter(dbCommand,"?TransactionType", order.TransactionType);
      
        Database.PutParameter(dbCommand,"?TransactionStatus", order.TransactionStatus);
      
        Database.PutParameter(dbCommand,"?CompletionType", order.CompletionType);
      
        Database.PutParameter(dbCommand,"?Amount", order.Amount);
      
        Database.PutParameter(dbCommand,"?DateCompleted", order.DateCompleted);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(Order order)
      {
        Insert(order, null);
      }


      public static void Insert(List<Order>  orderList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Order order in  orderList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TicketNumber", order.TicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerId", order.CustomerId);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", order.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", order.AdvertisingSourceId);
      
        Database.PutParameter(dbCommand,"?ScheduleDate", order.ScheduleDate);
      
        Database.PutParameter(dbCommand,"?TransNum", order.TransNum);
      
        Database.PutParameter(dbCommand,"?ServiceType", order.ServiceType);
      
        Database.PutParameter(dbCommand,"?TransactionType", order.TransactionType);
      
        Database.PutParameter(dbCommand,"?TransactionStatus", order.TransactionStatus);
      
        Database.PutParameter(dbCommand,"?CompletionType", order.CompletionType);
      
        Database.PutParameter(dbCommand,"?Amount", order.Amount);
      
        Database.PutParameter(dbCommand,"?DateCompleted", order.DateCompleted);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TicketNumber",order.TicketNumber);
      
        Database.UpdateParameter(dbCommand,"?CustomerId",order.CustomerId);
      
        Database.UpdateParameter(dbCommand,"?OrderSourceId",order.OrderSourceId);
      
        Database.UpdateParameter(dbCommand,"?AdvertisingSourceId",order.AdvertisingSourceId);
      
        Database.UpdateParameter(dbCommand,"?ScheduleDate",order.ScheduleDate);
      
        Database.UpdateParameter(dbCommand,"?TransNum",order.TransNum);
      
        Database.UpdateParameter(dbCommand,"?ServiceType",order.ServiceType);
      
        Database.UpdateParameter(dbCommand,"?TransactionType",order.TransactionType);
      
        Database.UpdateParameter(dbCommand,"?TransactionStatus",order.TransactionStatus);
      
        Database.UpdateParameter(dbCommand,"?CompletionType",order.CompletionType);
      
        Database.UpdateParameter(dbCommand,"?Amount",order.Amount);
      
        Database.UpdateParameter(dbCommand,"?DateCompleted",order.DateCompleted);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<Order>  orderList)
      {
        Insert(orderList, null);
    }

    #endregion

    #region Update


      private const String SqlUpdate = "Update `Order` Set "
      
        + " CustomerId = ?CustomerId, "
      
        + " OrderSourceId = ?OrderSourceId, "
      
        + " AdvertisingSourceId = ?AdvertisingSourceId, "
      
        + " ScheduleDate = ?ScheduleDate, "
      
        + " TransNum = ?TransNum, "
      
        + " ServiceType = ?ServiceType, "
      
        + " TransactionType = ?TransactionType, "
      
        + " TransactionStatus = ?TransactionStatus, "
      
        + " CompletionType = ?CompletionType, "
      
        + " Amount = ?Amount, "
      
        + " DateCompleted = ?DateCompleted "
      
        + " Where "
        
          + " TicketNumber = ?TicketNumber "
        
      ;

      public static void Update(Order order, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TicketNumber", order.TicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerId", order.CustomerId);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", order.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?AdvertisingSourceId", order.AdvertisingSourceId);
      
        Database.PutParameter(dbCommand,"?ScheduleDate", order.ScheduleDate);
      
        Database.PutParameter(dbCommand,"?TransNum", order.TransNum);
      
        Database.PutParameter(dbCommand,"?ServiceType", order.ServiceType);
      
        Database.PutParameter(dbCommand,"?TransactionType", order.TransactionType);
      
        Database.PutParameter(dbCommand,"?TransactionStatus", order.TransactionStatus);
      
        Database.PutParameter(dbCommand,"?CompletionType", order.CompletionType);
      
        Database.PutParameter(dbCommand,"?Amount", order.Amount);
      
        Database.PutParameter(dbCommand,"?DateCompleted", order.DateCompleted);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Order order)
      {
        Update(order, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TicketNumber, "
      
        + " CustomerId, "
      
        + " OrderSourceId, "
      
        + " AdvertisingSourceId, "
      
        + " ScheduleDate, "
      
        + " TransNum, "
      
        + " ServiceType, "
      
        + " TransactionType, "
      
        + " TransactionStatus, "
      
        + " CompletionType, "
      
        + " Amount, "
      
        + " DateCompleted "


      + " From `Order` "

      
        + " Where "
        
          + " TicketNumber = ?TicketNumber "
        
      ;

      public static Order FindByPrimaryKey(
      String ticketNumber, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TicketNumber", ticketNumber);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Order not found, search by primary key");

      }

      public static Order FindByPrimaryKey(
      String ticketNumber
      )
      {
      return FindByPrimaryKey(
      ticketNumber, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Order order, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TicketNumber",order.TicketNumber);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Order order)
      {
      return Exists(order, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
          String sql = "select * from `Order` limit 1";

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

      public static Order Load(IDataReader dataReader, int offset)
      {
      Order order = new Order();

      order.TicketNumber = dataReader.GetString(0 + offset);
          order.CustomerId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            order.OrderSourceId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            order.AdvertisingSourceId = dataReader.GetInt32(3 + offset);
          order.ScheduleDate = dataReader.GetDateTime(4 + offset);
          order.TransNum = dataReader.GetString(5 + offset);
          order.ServiceType = dataReader.GetInt32(6 + offset);
          order.TransactionType = dataReader.GetInt32(7 + offset);
          order.TransactionStatus = dataReader.GetInt32(8 + offset);
          order.CompletionType = dataReader.GetInt32(9 + offset);
          order.Amount = dataReader.GetDecimal(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            order.DateCompleted = dataReader.GetDateTime(11 + offset);
          

      return order;
      }

      public static Order Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From `Order` "

      
        + " Where "
        
          + " TicketNumber = ?TicketNumber "
        
      ;
      public static void Delete(Order order, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TicketNumber", order.TicketNumber);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Order order)
      {
        Delete(order, null);
    }

    #endregion

    #region Clear

      private const String SqlDeleteAll = "Delete From `Order` ";

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

      
        + " TicketNumber, "
      
        + " CustomerId, "
      
        + " OrderSourceId, "
      
        + " AdvertisingSourceId, "
      
        + " ScheduleDate, "
      
        + " TransNum, "
      
        + " ServiceType, "
      
        + " TransactionType, "
      
        + " TransactionStatus, "
      
        + " CompletionType, "
      
        + " Amount, "
      
        + " DateCompleted "


      + " From `Order` ";
      public static List<Order> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Order> rv = new List<Order>();

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

      public static List<Order> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Order> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Order obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return TicketNumber == obj.TicketNumber && CustomerId == obj.CustomerId && OrderSourceId == obj.OrderSourceId && AdvertisingSourceId == obj.AdvertisingSourceId && ScheduleDate == obj.ScheduleDate && TransNum == obj.TransNum && ServiceType == obj.ServiceType && TransactionType == obj.TransactionType && TransactionStatus == obj.TransactionStatus && CompletionType == obj.CompletionType && Amount == obj.Amount && DateCompleted == obj.DateCompleted;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Order> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Order item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Order>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Order));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Order> itemsList
      = new List<Order>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Order)
      itemsList.Add(deserializedObject as Order);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_ticketNumber;
      
        protected int m_customerId;
      
        protected int? m_orderSourceId;
      
        protected int? m_advertisingSourceId;
      
        protected DateTime m_scheduleDate;
      
        protected String m_transNum;
      
        protected int m_serviceType;
      
        protected int m_transactionType;
      
        protected int m_transactionStatus;
      
        protected int m_completionType;
      
        protected decimal m_amount;
      
        protected DateTime? m_dateCompleted;
      
      #endregion

      #region Constructors
      public Order(
      String 
          ticketNumber
      ) : this()
      {
      
        m_ticketNumber = ticketNumber;
      
      }

      


        public Order(
        String 
          ticketNumber,int 
          customerId,int? 
          orderSourceId,int? 
          advertisingSourceId,DateTime 
          scheduleDate,String 
          transNum,int 
          serviceType,int 
          transactionType,int 
          transactionStatus,int 
          completionType,decimal 
          amount,DateTime? 
          dateCompleted
        ) : this()
        {
        
          m_ticketNumber = ticketNumber;
        
          m_customerId = customerId;
        
          m_orderSourceId = orderSourceId;
        
          m_advertisingSourceId = advertisingSourceId;
        
          m_scheduleDate = scheduleDate;
        
          m_transNum = transNum;
        
          m_serviceType = serviceType;
        
          m_transactionType = transactionType;
        
          m_transactionStatus = transactionStatus;
        
          m_completionType = completionType;
        
          m_amount = amount;
        
          m_dateCompleted = dateCompleted;
        
        }


      
      #endregion

      
        [XmlElement]
        public String TicketNumber
        {
        get { return m_ticketNumber;}
        set { m_ticketNumber = value; }
        }
      
        [XmlElement]
        public int CustomerId
        {
        get { return m_customerId;}
        set { m_customerId = value; }
        }
      
        [XmlElement]
        public int? OrderSourceId
        {
        get { return m_orderSourceId;}
        set { m_orderSourceId = value; }
        }
      
        [XmlElement]
        public int? AdvertisingSourceId
        {
        get { return m_advertisingSourceId;}
        set { m_advertisingSourceId = value; }
        }
      
        [XmlElement]
        public DateTime ScheduleDate
        {
        get { return m_scheduleDate;}
        set { m_scheduleDate = value; }
        }
      
        [XmlElement]
        public String TransNum
        {
        get { return m_transNum;}
        set { m_transNum = value; }
        }
      
        [XmlElement]
        public int ServiceType
        {
        get { return m_serviceType;}
        set { m_serviceType = value; }
        }
      
        [XmlElement]
        public int TransactionType
        {
        get { return m_transactionType;}
        set { m_transactionType = value; }
        }
      
        [XmlElement]
        public int TransactionStatus
        {
        get { return m_transactionStatus;}
        set { m_transactionStatus = value; }
        }
      
        [XmlElement]
        public int CompletionType
        {
        get { return m_completionType;}
        set { m_completionType = value; }
        }
      
        [XmlElement]
        public decimal Amount
        {
        get { return m_amount;}
        set { m_amount = value; }
        }
      
        [XmlElement]
        public DateTime? DateCompleted
        {
        get { return m_dateCompleted;}
        set { m_dateCompleted = value; }
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

    