
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using SmartSchedule.Data;
    using SmartSchedule.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace SmartSchedule.Domain
      {

      [DataContract]
      public partial class OrderHistory : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into OrderHistory ( " +
      
        " TicketNumber, " +
      
        " CustomerName, " +
      
        " Street, " +
      
        " City, " +
      
        " State, " +
      
        " Zip, " +
      
        " Latitude, " +
      
        " Longitude, " +
      
        " DateTimeCall, " +
      
        " DateSchedule, " +
      
        " TimeFrameId, " +
      
        " Cost, " +
      
        " ExclusiveCompanyId " +
      
      ") Values (" +
      
        " ?TicketNumber, " +
      
        " ?CustomerName, " +
      
        " ?Street, " +
      
        " ?City, " +
      
        " ?State, " +
      
        " ?Zip, " +
      
        " ?Latitude, " +
      
        " ?Longitude, " +
      
        " ?DateTimeCall, " +
      
        " ?DateSchedule, " +
      
        " ?TimeFrameId, " +
      
        " ?Cost, " +
      
        " ?ExclusiveCompanyId " +
      
      ")";

      public static void Insert(OrderHistory orderHistory, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TicketNumber", orderHistory.TicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerName", orderHistory.CustomerName);
      
        Database.PutParameter(dbCommand,"?Street", orderHistory.Street);
      
        Database.PutParameter(dbCommand,"?City", orderHistory.City);
      
        Database.PutParameter(dbCommand,"?State", orderHistory.State);
      
        Database.PutParameter(dbCommand,"?Zip", orderHistory.Zip);
      
        Database.PutParameter(dbCommand,"?Latitude", orderHistory.Latitude);
      
        Database.PutParameter(dbCommand,"?Longitude", orderHistory.Longitude);
      
        Database.PutParameter(dbCommand,"?DateTimeCall", orderHistory.DateTimeCall);
      
        Database.PutParameter(dbCommand,"?DateSchedule", orderHistory.DateSchedule);
      
        Database.PutParameter(dbCommand,"?TimeFrameId", orderHistory.TimeFrameId);
      
        Database.PutParameter(dbCommand,"?Cost", orderHistory.Cost);
      
        Database.PutParameter(dbCommand,"?ExclusiveCompanyId", orderHistory.ExclusiveCompanyId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(OrderHistory orderHistory)
      {
        Insert(orderHistory, null);
      }


      public static void Insert(List<OrderHistory>  orderHistoryList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(OrderHistory orderHistory in  orderHistoryList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TicketNumber", orderHistory.TicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerName", orderHistory.CustomerName);
      
        Database.PutParameter(dbCommand,"?Street", orderHistory.Street);
      
        Database.PutParameter(dbCommand,"?City", orderHistory.City);
      
        Database.PutParameter(dbCommand,"?State", orderHistory.State);
      
        Database.PutParameter(dbCommand,"?Zip", orderHistory.Zip);
      
        Database.PutParameter(dbCommand,"?Latitude", orderHistory.Latitude);
      
        Database.PutParameter(dbCommand,"?Longitude", orderHistory.Longitude);
      
        Database.PutParameter(dbCommand,"?DateTimeCall", orderHistory.DateTimeCall);
      
        Database.PutParameter(dbCommand,"?DateSchedule", orderHistory.DateSchedule);
      
        Database.PutParameter(dbCommand,"?TimeFrameId", orderHistory.TimeFrameId);
      
        Database.PutParameter(dbCommand,"?Cost", orderHistory.Cost);
      
        Database.PutParameter(dbCommand,"?ExclusiveCompanyId", orderHistory.ExclusiveCompanyId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TicketNumber",orderHistory.TicketNumber);
      
        Database.UpdateParameter(dbCommand,"?CustomerName",orderHistory.CustomerName);
      
        Database.UpdateParameter(dbCommand,"?Street",orderHistory.Street);
      
        Database.UpdateParameter(dbCommand,"?City",orderHistory.City);
      
        Database.UpdateParameter(dbCommand,"?State",orderHistory.State);
      
        Database.UpdateParameter(dbCommand,"?Zip",orderHistory.Zip);
      
        Database.UpdateParameter(dbCommand,"?Latitude",orderHistory.Latitude);
      
        Database.UpdateParameter(dbCommand,"?Longitude",orderHistory.Longitude);
      
        Database.UpdateParameter(dbCommand,"?DateTimeCall",orderHistory.DateTimeCall);
      
        Database.UpdateParameter(dbCommand,"?DateSchedule",orderHistory.DateSchedule);
      
        Database.UpdateParameter(dbCommand,"?TimeFrameId",orderHistory.TimeFrameId);
      
        Database.UpdateParameter(dbCommand,"?Cost",orderHistory.Cost);
      
        Database.UpdateParameter(dbCommand,"?ExclusiveCompanyId",orderHistory.ExclusiveCompanyId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<OrderHistory>  orderHistoryList)
      {
        Insert(orderHistoryList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update OrderHistory Set "
      
        + " CustomerName = ?CustomerName, "
      
        + " Street = ?Street, "
      
        + " City = ?City, "
      
        + " State = ?State, "
      
        + " Zip = ?Zip, "
      
        + " Latitude = ?Latitude, "
      
        + " Longitude = ?Longitude, "
      
        + " DateTimeCall = ?DateTimeCall, "
      
        + " DateSchedule = ?DateSchedule, "
      
        + " TimeFrameId = ?TimeFrameId, "
      
        + " Cost = ?Cost, "
      
        + " ExclusiveCompanyId = ?ExclusiveCompanyId "
      
        + " Where "
        
          + " TicketNumber = ?TicketNumber "
        
      ;

      public static void Update(OrderHistory orderHistory, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?TicketNumber", orderHistory.TicketNumber);
      
        Database.PutParameter(dbCommand,"?CustomerName", orderHistory.CustomerName);
      
        Database.PutParameter(dbCommand,"?Street", orderHistory.Street);
      
        Database.PutParameter(dbCommand,"?City", orderHistory.City);
      
        Database.PutParameter(dbCommand,"?State", orderHistory.State);
      
        Database.PutParameter(dbCommand,"?Zip", orderHistory.Zip);
      
        Database.PutParameter(dbCommand,"?Latitude", orderHistory.Latitude);
      
        Database.PutParameter(dbCommand,"?Longitude", orderHistory.Longitude);
      
        Database.PutParameter(dbCommand,"?DateTimeCall", orderHistory.DateTimeCall);
      
        Database.PutParameter(dbCommand,"?DateSchedule", orderHistory.DateSchedule);
      
        Database.PutParameter(dbCommand,"?TimeFrameId", orderHistory.TimeFrameId);
      
        Database.PutParameter(dbCommand,"?Cost", orderHistory.Cost);
      
        Database.PutParameter(dbCommand,"?ExclusiveCompanyId", orderHistory.ExclusiveCompanyId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(OrderHistory orderHistory)
      {
        Update(orderHistory, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " TicketNumber, "
      
        + " CustomerName, "
      
        + " Street, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " Latitude, "
      
        + " Longitude, "
      
        + " DateTimeCall, "
      
        + " DateSchedule, "
      
        + " TimeFrameId, "
      
        + " Cost, "
      
        + " ExclusiveCompanyId "
      

      + " From OrderHistory "

      
        + " Where "
        
          + " TicketNumber = ?TicketNumber "
        
      ;

      public static OrderHistory FindByPrimaryKey(
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
      throw new DataNotFoundException("OrderHistory not found, search by primary key");

      }

      public static OrderHistory FindByPrimaryKey(
      String ticketNumber
      )
      {
      return FindByPrimaryKey(
      ticketNumber, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(OrderHistory orderHistory, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?TicketNumber",orderHistory.TicketNumber);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(OrderHistory orderHistory)
      {
      return Exists(orderHistory, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from OrderHistory limit 1";

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

      public static OrderHistory Load(IDataReader dataReader, int offset)
      {
      OrderHistory orderHistory = new OrderHistory();

      orderHistory.TicketNumber = dataReader.GetString(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            orderHistory.CustomerName = dataReader.GetString(1 + offset);
          orderHistory.Street = dataReader.GetString(2 + offset);
          orderHistory.City = dataReader.GetString(3 + offset);
          orderHistory.State = dataReader.GetString(4 + offset);
          orderHistory.Zip = dataReader.GetString(5 + offset);
          orderHistory.Latitude = dataReader.GetFloat(6 + offset);
          orderHistory.Longitude = dataReader.GetFloat(7 + offset);
          orderHistory.DateTimeCall = dataReader.GetDateTime(8 + offset);
          orderHistory.DateSchedule = dataReader.GetDateTime(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            orderHistory.TimeFrameId = dataReader.GetInt32(10 + offset);
          orderHistory.Cost = dataReader.GetDecimal(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            orderHistory.ExclusiveCompanyId = dataReader.GetInt32(12 + offset);
          

      return orderHistory;
      }

      public static OrderHistory Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From OrderHistory "

      
        + " Where "
        
          + " TicketNumber = ?TicketNumber "
        
      ;
      public static void Delete(OrderHistory orderHistory, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?TicketNumber", orderHistory.TicketNumber);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(OrderHistory orderHistory)
      {
        Delete(orderHistory, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From OrderHistory ";

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
      
        + " CustomerName, "
      
        + " Street, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " Latitude, "
      
        + " Longitude, "
      
        + " DateTimeCall, "
      
        + " DateSchedule, "
      
        + " TimeFrameId, "
      
        + " Cost, "
      
        + " ExclusiveCompanyId "
      

      + " From OrderHistory ";
      public static List<OrderHistory> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<OrderHistory> rv = new List<OrderHistory>();

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

      public static List<OrderHistory> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<OrderHistory> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<OrderHistory> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderHistory));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(OrderHistory item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<OrderHistory>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(OrderHistory));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<OrderHistory> itemsList
      = new List<OrderHistory>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is OrderHistory)
      itemsList.Add(deserializedObject as OrderHistory);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_ticketNumber;
      
        protected String m_customerName;
      
        protected String m_street;
      
        protected String m_city;
      
        protected String m_state;
      
        protected String m_zip;
      
        protected float m_latitude;
      
        protected float m_longitude;
      
        protected DateTime m_dateTimeCall;
      
        protected DateTime m_dateSchedule;
      
        protected int? m_timeFrameId;
      
        protected decimal m_cost;
      
        protected int? m_exclusiveCompanyId;
      
      #endregion

      #region Constructors
      public OrderHistory(
      String 
          ticketNumber
      ) : this()
      {
      
        m_ticketNumber = ticketNumber;
      
      }

      


        public OrderHistory(
        String 
          ticketNumber,String 
          customerName,String 
          street,String 
          city,String 
          state,String 
          zip,float 
          latitude,float 
          longitude,DateTime 
          dateTimeCall,DateTime 
          dateSchedule,int? 
          timeFrameId,decimal 
          cost,int? 
          exclusiveCompanyId
        ) : this()
        {
        
          m_ticketNumber = ticketNumber;
        
          m_customerName = customerName;
        
          m_street = street;
        
          m_city = city;
        
          m_state = state;
        
          m_zip = zip;
        
          m_latitude = latitude;
        
          m_longitude = longitude;
        
          m_dateTimeCall = dateTimeCall;
        
          m_dateSchedule = dateSchedule;
        
          m_timeFrameId = timeFrameId;
        
          m_cost = cost;
        
          m_exclusiveCompanyId = exclusiveCompanyId;
        
        }


      
      #endregion

      
        [DataMember]
        public String TicketNumber
        {
        get { return m_ticketNumber;}
        set { m_ticketNumber = value; }
        }
      
        [DataMember]
        public String CustomerName
        {
        get { return m_customerName;}
        set { m_customerName = value; }
        }
      
        [DataMember]
        public String Street
        {
        get { return m_street;}
        set { m_street = value; }
        }
      
        [DataMember]
        public String City
        {
        get { return m_city;}
        set { m_city = value; }
        }
      
        [DataMember]
        public String State
        {
        get { return m_state;}
        set { m_state = value; }
        }
      
        [DataMember]
        public String Zip
        {
        get { return m_zip;}
        set { m_zip = value; }
        }
      
        [DataMember]
        public float Latitude
        {
        get { return m_latitude;}
        set { m_latitude = value; }
        }
      
        [DataMember]
        public float Longitude
        {
        get { return m_longitude;}
        set { m_longitude = value; }
        }
      
        [DataMember]
        public DateTime DateTimeCall
        {
        get { return m_dateTimeCall;}
        set { m_dateTimeCall = value; }
        }
      
        [DataMember]
        public DateTime DateSchedule
        {
        get { return m_dateSchedule;}
        set { m_dateSchedule = value; }
        }
      
        [DataMember]
        public int? TimeFrameId
        {
        get { return m_timeFrameId;}
        set { m_timeFrameId = value; }
        }
      
        [DataMember]
        public decimal Cost
        {
        get { return m_cost;}
        set { m_cost = value; }
        }
      
        [DataMember]
        public int? ExclusiveCompanyId
        {
        get { return m_exclusiveCompanyId;}
        set { m_exclusiveCompanyId = value; }
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

    