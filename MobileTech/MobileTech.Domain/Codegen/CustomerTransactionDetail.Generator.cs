
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class CustomerTransactionDetail
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into CustomerTransactionDetail ( " +
      
        " SessionId, " +
      
        " BusinessTransactionId, " +
      
        " ItemNumber, " +
      
        " RouteNumber, " +
      
        " LocationId, " +
      
        " Quantity, " +
      
        " DateCreated " +
      
      ") Values (" +
      
        " @SessionId, " +
      
        " @BusinessTransactionId, " +
      
        " @ItemNumber, " +
      
        " @RouteNumber, " +
      
        " @LocationId, " +
      
        " @Quantity, " +
      
        " @DateCreated " +
      
      ")";

      public static void Insert(CustomerTransactionDetail customerTransactionDetail)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", customerTransactionDetail.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", customerTransactionDetail.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemNumber", customerTransactionDetail.ItemNumber);
      
        Database.PutParameter(dbCommand,"@RouteNumber", customerTransactionDetail.RouteNumber);
      
        Database.PutParameter(dbCommand,"@LocationId", customerTransactionDetail.LocationId);
      
        Database.PutParameter(dbCommand,"@Quantity", customerTransactionDetail.Quantity);
      
        Database.PutParameter(dbCommand,"@DateCreated", customerTransactionDetail.DateCreated);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<CustomerTransactionDetail>  customerTransactionDetailList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(CustomerTransactionDetail customerTransactionDetail in  customerTransactionDetailList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@SessionId", customerTransactionDetail.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", customerTransactionDetail.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemNumber", customerTransactionDetail.ItemNumber);
      
        Database.PutParameter(dbCommand,"@RouteNumber", customerTransactionDetail.RouteNumber);
      
        Database.PutParameter(dbCommand,"@LocationId", customerTransactionDetail.LocationId);
      
        Database.PutParameter(dbCommand,"@Quantity", customerTransactionDetail.Quantity);
      
        Database.PutParameter(dbCommand,"@DateCreated", customerTransactionDetail.DateCreated);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@SessionId",customerTransactionDetail.SessionId);
      
        Database.UpdateParameter(dbCommand,"@BusinessTransactionId",customerTransactionDetail.BusinessTransactionId);
      
        Database.UpdateParameter(dbCommand,"@ItemNumber",customerTransactionDetail.ItemNumber);
      
        Database.UpdateParameter(dbCommand,"@RouteNumber",customerTransactionDetail.RouteNumber);
      
        Database.UpdateParameter(dbCommand,"@LocationId",customerTransactionDetail.LocationId);
      
        Database.UpdateParameter(dbCommand,"@Quantity",customerTransactionDetail.Quantity);
      
        Database.UpdateParameter(dbCommand,"@DateCreated",customerTransactionDetail.DateCreated);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update CustomerTransactionDetail Set "
      
        + " Quantity = @Quantity, "
      
        + " DateCreated = @DateCreated "
      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId and  "
        
          + " ItemNumber = @ItemNumber and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " LocationId = @LocationId "
        
      ;

      public static void Update(CustomerTransactionDetail customerTransactionDetail)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", customerTransactionDetail.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", customerTransactionDetail.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemNumber", customerTransactionDetail.ItemNumber);
      
        Database.PutParameter(dbCommand,"@RouteNumber", customerTransactionDetail.RouteNumber);
      
        Database.PutParameter(dbCommand,"@LocationId", customerTransactionDetail.LocationId);
      
        Database.PutParameter(dbCommand,"@Quantity", customerTransactionDetail.Quantity);
      
        Database.PutParameter(dbCommand,"@DateCreated", customerTransactionDetail.DateCreated);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " SessionId, "
      
        + " BusinessTransactionId, "
      
        + " ItemNumber, "
      
        + " RouteNumber, "
      
        + " LocationId, "
      
        + " Quantity, "
      
        + " DateCreated "
      

      + " From CustomerTransactionDetail "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId and  "
        
          + " ItemNumber = @ItemNumber and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " LocationId = @LocationId "
        
      ;

      public static CustomerTransactionDetail FindByPrimaryKey(
      long sessionId,int businessTransactionId,String itemNumber,int routeNumber,int locationId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", sessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", businessTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemNumber", itemNumber);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeNumber);
      
        Database.PutParameter(dbCommand,"@LocationId", locationId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("CustomerTransactionDetail not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(CustomerTransactionDetail customerTransactionDetail)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId",customerTransactionDetail.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId",customerTransactionDetail.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemNumber",customerTransactionDetail.ItemNumber);
      
        Database.PutParameter(dbCommand,"@RouteNumber",customerTransactionDetail.RouteNumber);
      
        Database.PutParameter(dbCommand,"@LocationId",customerTransactionDetail.LocationId);
      

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
      String sql = "select 1 from CustomerTransactionDetail";

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

      public static CustomerTransactionDetail Load(IDataReader dataReader)
      {
      CustomerTransactionDetail customerTransactionDetail = new CustomerTransactionDetail();

      customerTransactionDetail.SessionId = dataReader.GetInt64(0);
          customerTransactionDetail.BusinessTransactionId = dataReader.GetInt32(1);
          customerTransactionDetail.ItemNumber = dataReader.GetString(2);
          customerTransactionDetail.RouteNumber = dataReader.GetInt32(3);
          customerTransactionDetail.LocationId = dataReader.GetInt32(4);
          customerTransactionDetail.Quantity = dataReader.GetInt32(5);
          customerTransactionDetail.DateCreated = dataReader.GetDateTime(6);
          

      return customerTransactionDetail;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From CustomerTransactionDetail "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId and  "
        
          + " ItemNumber = @ItemNumber and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " LocationId = @LocationId "
        
      ;
      public static void Delete(CustomerTransactionDetail customerTransactionDetail)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@SessionId", customerTransactionDetail.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", customerTransactionDetail.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemNumber", customerTransactionDetail.ItemNumber);
      
        Database.PutParameter(dbCommand,"@RouteNumber", customerTransactionDetail.RouteNumber);
      
        Database.PutParameter(dbCommand,"@LocationId", customerTransactionDetail.LocationId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From CustomerTransactionDetail ";

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
      
        + " BusinessTransactionId, "
      
        + " ItemNumber, "
      
        + " RouteNumber, "
      
        + " LocationId, "
      
        + " Quantity, "
      
        + " DateCreated "
      

      + " From CustomerTransactionDetail ";
      public static List<CustomerTransactionDetail> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<CustomerTransactionDetail> rv = new List<CustomerTransactionDetail>();

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
        List<CustomerTransactionDetail> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<CustomerTransactionDetail> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerTransactionDetail));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(CustomerTransactionDetail item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CustomerTransactionDetail>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerTransactionDetail));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<CustomerTransactionDetail> itemsList
      = new List<CustomerTransactionDetail>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CustomerTransactionDetail)
        itemsList.Add(deserializedObject as CustomerTransactionDetail);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected long m_sessionId;
        
          protected int m_businessTransactionId;
        
          protected String m_itemNumber;
        
          protected int m_routeNumber;
        
          protected int m_locationId;
        
          protected int m_quantity;
        
          protected DateTime m_dateCreated;
        
        #endregion
        
        #region Constructors
        public CustomerTransactionDetail(
        long 
          sessionId,int 
          businessTransactionId,String 
          itemNumber,int 
          routeNumber,int 
          locationId
         )
        {
        
          m_sessionId = sessionId;
        
          m_businessTransactionId = businessTransactionId;
        
          m_itemNumber = itemNumber;
        
          m_routeNumber = routeNumber;
        
          m_locationId = locationId;
        
        }
        
        


        public CustomerTransactionDetail(
        long 
          sessionId,int 
          businessTransactionId,String 
          itemNumber,int 
          routeNumber,int 
          locationId,int 
          quantity,DateTime 
          dateCreated
        )
        {
        
          m_sessionId = sessionId;
        
          m_businessTransactionId = businessTransactionId;
        
          m_itemNumber = itemNumber;
        
          m_routeNumber = routeNumber;
        
          m_locationId = locationId;
        
          m_quantity = quantity;
        
          m_dateCreated = dateCreated;
        
          }


        
      #endregion

      
        [XmlElement]
        public long SessionId
        {
          get { return m_sessionId;}
          set { m_sessionId = value; }
        }
      
        [XmlElement]
        public int BusinessTransactionId
        {
          get { return m_businessTransactionId;}
          set { m_businessTransactionId = value; }
        }
      
        [XmlElement]
        public String ItemNumber
        {
          get { return m_itemNumber;}
          set { m_itemNumber = value; }
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
        public int Quantity
        {
          get { return m_quantity;}
          set { m_quantity = value; }
        }
      
        [XmlElement]
        public DateTime DateCreated
        {
          get { return m_dateCreated;}
          set { m_dateCreated = value; }
        }
      
      }
      #endregion
      }

    