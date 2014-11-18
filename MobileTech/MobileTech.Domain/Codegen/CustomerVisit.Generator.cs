
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class CustomerVisit
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into CustomerVisit ( " +
      
        " SessionId, " +
      
        " CustomerVisitId, " +
      
        " CustomerId, " +
      
        " RouteNumber, " +
      
        " LocationId, " +
      
        " DateStarted, " +
      
        " DateFinished " +
      
      ") Values (" +
      
        " @SessionId, " +
      
        " @CustomerVisitId, " +
      
        " @CustomerId, " +
      
        " @RouteNumber, " +
      
        " @LocationId, " +
      
        " @DateStarted, " +
      
        " @DateFinished " +
      
      ")";

      public static void Insert(CustomerVisit customerVisit)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", customerVisit.SessionId);
      
        Database.PutParameter(dbCommand,"@CustomerVisitId", customerVisit.CustomerVisitId);
      
        Database.PutParameter(dbCommand,"@CustomerId", customerVisit.CustomerId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", customerVisit.RouteNumber);
      
        Database.PutParameter(dbCommand,"@LocationId", customerVisit.LocationId);
      
        Database.PutParameter(dbCommand,"@DateStarted", customerVisit.DateStarted);
      
        Database.PutParameter(dbCommand,"@DateFinished", customerVisit.DateFinished);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<CustomerVisit>  customerVisitList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(CustomerVisit customerVisit in  customerVisitList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@SessionId", customerVisit.SessionId);
      
        Database.PutParameter(dbCommand,"@CustomerVisitId", customerVisit.CustomerVisitId);
      
        Database.PutParameter(dbCommand,"@CustomerId", customerVisit.CustomerId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", customerVisit.RouteNumber);
      
        Database.PutParameter(dbCommand,"@LocationId", customerVisit.LocationId);
      
        Database.PutParameter(dbCommand,"@DateStarted", customerVisit.DateStarted);
      
        Database.PutParameter(dbCommand,"@DateFinished", customerVisit.DateFinished);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@SessionId",customerVisit.SessionId);
      
        Database.UpdateParameter(dbCommand,"@CustomerVisitId",customerVisit.CustomerVisitId);
      
        Database.UpdateParameter(dbCommand,"@CustomerId",customerVisit.CustomerId);
      
        Database.UpdateParameter(dbCommand,"@RouteNumber",customerVisit.RouteNumber);
      
        Database.UpdateParameter(dbCommand,"@LocationId",customerVisit.LocationId);
      
        Database.UpdateParameter(dbCommand,"@DateStarted",customerVisit.DateStarted);
      
        Database.UpdateParameter(dbCommand,"@DateFinished",customerVisit.DateFinished);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update CustomerVisit Set "
      
        + " CustomerId = @CustomerId, "
      
        + " RouteNumber = @RouteNumber, "
      
        + " LocationId = @LocationId, "
      
        + " DateStarted = @DateStarted, "
      
        + " DateFinished = @DateFinished "
      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " CustomerVisitId = @CustomerVisitId "
        
      ;

      public static void Update(CustomerVisit customerVisit)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", customerVisit.SessionId);
      
        Database.PutParameter(dbCommand,"@CustomerVisitId", customerVisit.CustomerVisitId);
      
        Database.PutParameter(dbCommand,"@CustomerId", customerVisit.CustomerId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", customerVisit.RouteNumber);
      
        Database.PutParameter(dbCommand,"@LocationId", customerVisit.LocationId);
      
        Database.PutParameter(dbCommand,"@DateStarted", customerVisit.DateStarted);
      
        Database.PutParameter(dbCommand,"@DateFinished", customerVisit.DateFinished);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " SessionId, "
      
        + " CustomerVisitId, "
      
        + " CustomerId, "
      
        + " RouteNumber, "
      
        + " LocationId, "
      
        + " DateStarted, "
      
        + " DateFinished "
      

      + " From CustomerVisit "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " CustomerVisitId = @CustomerVisitId "
        
      ;

      public static CustomerVisit FindByPrimaryKey(
      long sessionId,int customerVisitId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", sessionId);
      
        Database.PutParameter(dbCommand,"@CustomerVisitId", customerVisitId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("CustomerVisit not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(CustomerVisit customerVisit)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId",customerVisit.SessionId);
      
        Database.PutParameter(dbCommand,"@CustomerVisitId",customerVisit.CustomerVisitId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData()
      {
      String sql = "select 1 from CustomerVisit";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static CustomerVisit Load(IDataReader dataReader)
      {
      CustomerVisit customerVisit = new CustomerVisit();

      customerVisit.SessionId = dataReader.GetInt64(0);
          customerVisit.CustomerVisitId = dataReader.GetInt32(1);
          customerVisit.CustomerId = dataReader.GetInt32(2);
          customerVisit.RouteNumber = dataReader.GetInt32(3);
          customerVisit.LocationId = dataReader.GetInt32(4);
          customerVisit.DateStarted = dataReader.GetDateTime(5);
          
            if(!dataReader.IsDBNull(6))
            customerVisit.DateFinished = dataReader.GetDateTime(6);
          

      return customerVisit;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From CustomerVisit "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " CustomerVisitId = @CustomerVisitId "
        
      ;
      public static void Delete(CustomerVisit customerVisit)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@SessionId", customerVisit.SessionId);
      
        Database.PutParameter(dbCommand,"@CustomerVisitId", customerVisit.CustomerVisitId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From CustomerVisit ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " SessionId, "
      
        + " CustomerVisitId, "
      
        + " CustomerId, "
      
        + " RouteNumber, "
      
        + " LocationId, "
      
        + " DateStarted, "
      
        + " DateFinished "
      

      + " From CustomerVisit ";
      public static List<CustomerVisit> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<CustomerVisit> rv = new List<CustomerVisit>();

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
      #endregion

      #region Import from file
      
      public static int Import(String xmlFilePath)
      {
        List<CustomerVisit> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<CustomerVisit> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerVisit));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(CustomerVisit item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CustomerVisit>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerVisit));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<CustomerVisit> itemsList
      = new List<CustomerVisit>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CustomerVisit)
        itemsList.Add(deserializedObject as CustomerVisit);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected long m_sessionId;
        
          protected int m_customerVisitId;
        
          protected int m_customerId;
        
          protected int m_routeNumber;
        
          protected int m_locationId;
        
          protected DateTime m_dateStarted;
        
          protected DateTime? m_dateFinished;
        
        #endregion
        
        #region Constructors
        public CustomerVisit(
        long 
          sessionId,int 
          customerVisitId
         )
        {
        
          m_sessionId = sessionId;
        
          m_customerVisitId = customerVisitId;
        
        }
        
        


        public CustomerVisit(
        long 
          sessionId,int 
          customerVisitId,int 
          customerId,int 
          routeNumber,int 
          locationId,DateTime 
          dateStarted,DateTime? 
          dateFinished
        )
        {
        
          m_sessionId = sessionId;
        
          m_customerVisitId = customerVisitId;
        
          m_customerId = customerId;
        
          m_routeNumber = routeNumber;
        
          m_locationId = locationId;
        
          m_dateStarted = dateStarted;
        
          m_dateFinished = dateFinished;
        
          }


        
      #endregion

      
        [XmlElement]
        public long SessionId
        {
          get { return m_sessionId;}
          set { m_sessionId = value; }
        }
      
        [XmlElement]
        public int CustomerVisitId
        {
          get { return m_customerVisitId;}
          set { m_customerVisitId = value; }
        }
      
        [XmlElement]
        public int CustomerId
        {
          get { return m_customerId;}
          set { m_customerId = value; }
        }
      
        [XmlElement]
        public int RouteNumber
        {
          get { return m_routeNumber;}
          set { m_routeNumber = value; }
        }
      
        [XmlElement]
        public int LocationId
        {
          get { return m_locationId;}
          set { m_locationId = value; }
        }
      
        [XmlElement]
        public DateTime DateStarted
        {
          get { return m_dateStarted;}
          set { m_dateStarted = value; }
        }
      
        [XmlElement]
        public DateTime? DateFinished
        {
          get { return m_dateFinished;}
          set { m_dateFinished = value; }
        }
      
      }
      #endregion
      }

    