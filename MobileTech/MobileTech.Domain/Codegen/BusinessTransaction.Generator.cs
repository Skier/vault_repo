
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class BusinessTransaction
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into BusinessTransaction ( " +
      
        " SessionId, " +
      
        " BusinessTransactionId, " +
      
        " BusinessTransactionTypeId, " +
      
        " BusinessTransactionStatusId, " +
      
        " LocationId, " +
      
        " RouteNumber, " +
      
        " PeriodIndex, " +
      
        " EmployeeId, " +
      
        " DocumentNumber, " +
      
        " Odometer, " +
      
        " Password, " +
      
        " DateCreated, " +
      
        " DateCommited " +
      
      ") Values (" +
      
        " @SessionId, " +
      
        " @BusinessTransactionId, " +
      
        " @BusinessTransactionTypeId, " +
      
        " @BusinessTransactionStatusId, " +
      
        " @LocationId, " +
      
        " @RouteNumber, " +
      
        " @PeriodIndex, " +
      
        " @EmployeeId, " +
      
        " @DocumentNumber, " +
      
        " @Odometer, " +
      
        " @Password, " +
      
        " @DateCreated, " +
      
        " @DateCommited " +
      
      ")";

      public static void Insert(BusinessTransaction businessTransaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", businessTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", businessTransaction.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionTypeId", businessTransaction.BusinessTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionStatusId", businessTransaction.BusinessTransactionStatusId);
      
        Database.PutParameter(dbCommand,"@LocationId", businessTransaction.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", businessTransaction.RouteNumber);
      
        Database.PutParameter(dbCommand,"@PeriodIndex", businessTransaction.PeriodIndex);
      
        Database.PutParameter(dbCommand,"@EmployeeId", businessTransaction.EmployeeId);
      
        Database.PutParameter(dbCommand,"@DocumentNumber", businessTransaction.DocumentNumber);
      
        Database.PutParameter(dbCommand,"@Odometer", businessTransaction.Odometer);
      
        Database.PutParameter(dbCommand,"@Password", businessTransaction.Password);
      
        Database.PutParameter(dbCommand,"@DateCreated", businessTransaction.DateCreated);
      
        Database.PutParameter(dbCommand,"@DateCommited", businessTransaction.DateCommited);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<BusinessTransaction>  businessTransactionList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(BusinessTransaction businessTransaction in  businessTransactionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@SessionId", businessTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", businessTransaction.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionTypeId", businessTransaction.BusinessTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionStatusId", businessTransaction.BusinessTransactionStatusId);
      
        Database.PutParameter(dbCommand,"@LocationId", businessTransaction.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", businessTransaction.RouteNumber);
      
        Database.PutParameter(dbCommand,"@PeriodIndex", businessTransaction.PeriodIndex);
      
        Database.PutParameter(dbCommand,"@EmployeeId", businessTransaction.EmployeeId);
      
        Database.PutParameter(dbCommand,"@DocumentNumber", businessTransaction.DocumentNumber);
      
        Database.PutParameter(dbCommand,"@Odometer", businessTransaction.Odometer);
      
        Database.PutParameter(dbCommand,"@Password", businessTransaction.Password);
      
        Database.PutParameter(dbCommand,"@DateCreated", businessTransaction.DateCreated);
      
        Database.PutParameter(dbCommand,"@DateCommited", businessTransaction.DateCommited);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@SessionId",businessTransaction.SessionId);
      
        Database.UpdateParameter(dbCommand,"@BusinessTransactionId",businessTransaction.BusinessTransactionId);
      
        Database.UpdateParameter(dbCommand,"@BusinessTransactionTypeId",businessTransaction.BusinessTransactionTypeId);
      
        Database.UpdateParameter(dbCommand,"@BusinessTransactionStatusId",businessTransaction.BusinessTransactionStatusId);
      
        Database.UpdateParameter(dbCommand,"@LocationId",businessTransaction.LocationId);
      
        Database.UpdateParameter(dbCommand,"@RouteNumber",businessTransaction.RouteNumber);
      
        Database.UpdateParameter(dbCommand,"@PeriodIndex",businessTransaction.PeriodIndex);
      
        Database.UpdateParameter(dbCommand,"@EmployeeId",businessTransaction.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"@DocumentNumber",businessTransaction.DocumentNumber);
      
        Database.UpdateParameter(dbCommand,"@Odometer",businessTransaction.Odometer);
      
        Database.UpdateParameter(dbCommand,"@Password",businessTransaction.Password);
      
        Database.UpdateParameter(dbCommand,"@DateCreated",businessTransaction.DateCreated);
      
        Database.UpdateParameter(dbCommand,"@DateCommited",businessTransaction.DateCommited);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update BusinessTransaction Set "
      
        + " BusinessTransactionTypeId = @BusinessTransactionTypeId, "
      
        + " BusinessTransactionStatusId = @BusinessTransactionStatusId, "
      
        + " LocationId = @LocationId, "
      
        + " RouteNumber = @RouteNumber, "
      
        + " PeriodIndex = @PeriodIndex, "
      
        + " EmployeeId = @EmployeeId, "
      
        + " DocumentNumber = @DocumentNumber, "
      
        + " Odometer = @Odometer, "
      
        + " Password = @Password, "
      
        + " DateCreated = @DateCreated, "
      
        + " DateCommited = @DateCommited "
      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId "
        
      ;

      public static void Update(BusinessTransaction businessTransaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", businessTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", businessTransaction.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionTypeId", businessTransaction.BusinessTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionStatusId", businessTransaction.BusinessTransactionStatusId);
      
        Database.PutParameter(dbCommand,"@LocationId", businessTransaction.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", businessTransaction.RouteNumber);
      
        Database.PutParameter(dbCommand,"@PeriodIndex", businessTransaction.PeriodIndex);
      
        Database.PutParameter(dbCommand,"@EmployeeId", businessTransaction.EmployeeId);
      
        Database.PutParameter(dbCommand,"@DocumentNumber", businessTransaction.DocumentNumber);
      
        Database.PutParameter(dbCommand,"@Odometer", businessTransaction.Odometer);
      
        Database.PutParameter(dbCommand,"@Password", businessTransaction.Password);
      
        Database.PutParameter(dbCommand,"@DateCreated", businessTransaction.DateCreated);
      
        Database.PutParameter(dbCommand,"@DateCommited", businessTransaction.DateCommited);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " SessionId, "
      
        + " BusinessTransactionId, "
      
        + " BusinessTransactionTypeId, "
      
        + " BusinessTransactionStatusId, "
      
        + " LocationId, "
      
        + " RouteNumber, "
      
        + " PeriodIndex, "
      
        + " EmployeeId, "
      
        + " DocumentNumber, "
      
        + " Odometer, "
      
        + " Password, "
      
        + " DateCreated, "
      
        + " DateCommited "
      

      + " From BusinessTransaction "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId "
        
      ;

      public static BusinessTransaction FindByPrimaryKey(
      long sessionId,int businessTransactionId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", sessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", businessTransactionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("BusinessTransaction not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(BusinessTransaction businessTransaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId",businessTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId",businessTransaction.BusinessTransactionId);
      

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
      String sql = "select 1 from BusinessTransaction";

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

      public static BusinessTransaction Load(IDataReader dataReader)
      {
      BusinessTransaction businessTransaction = new BusinessTransaction();

      businessTransaction.SessionId = dataReader.GetInt64(0);
          businessTransaction.BusinessTransactionId = dataReader.GetInt32(1);
          businessTransaction.BusinessTransactionTypeId = dataReader.GetInt16(2);
          businessTransaction.BusinessTransactionStatusId = dataReader.GetInt16(3);
          businessTransaction.LocationId = dataReader.GetInt32(4);
          businessTransaction.RouteNumber = dataReader.GetInt32(5);
          businessTransaction.PeriodIndex = dataReader.GetInt32(6);
          businessTransaction.EmployeeId = dataReader.GetInt32(7);
          businessTransaction.DocumentNumber = dataReader.GetString(8);
          
            if(!dataReader.IsDBNull(9))
            businessTransaction.Odometer = dataReader.GetInt32(9);
          businessTransaction.Password = dataReader.GetString(10);
          businessTransaction.DateCreated = dataReader.GetDateTime(11);
          
            if(!dataReader.IsDBNull(12))
            businessTransaction.DateCommited = dataReader.GetDateTime(12);
          

      return businessTransaction;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From BusinessTransaction "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId "
        
      ;
      public static void Delete(BusinessTransaction businessTransaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@SessionId", businessTransaction.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", businessTransaction.BusinessTransactionId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From BusinessTransaction ";

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
      
        + " BusinessTransactionTypeId, "
      
        + " BusinessTransactionStatusId, "
      
        + " LocationId, "
      
        + " RouteNumber, "
      
        + " PeriodIndex, "
      
        + " EmployeeId, "
      
        + " DocumentNumber, "
      
        + " Odometer, "
      
        + " Password, "
      
        + " DateCreated, "
      
        + " DateCommited "
      

      + " From BusinessTransaction ";
      public static List<BusinessTransaction> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<BusinessTransaction> rv = new List<BusinessTransaction>();

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
        List<BusinessTransaction> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<BusinessTransaction> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(BusinessTransaction));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(BusinessTransaction item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<BusinessTransaction>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(BusinessTransaction));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<BusinessTransaction> itemsList
      = new List<BusinessTransaction>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is BusinessTransaction)
        itemsList.Add(deserializedObject as BusinessTransaction);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected long m_sessionId;
        
          protected int m_businessTransactionId;
        
          protected int m_businessTransactionTypeId;
        
          protected int m_businessTransactionStatusId;
        
          protected int m_locationId;
        
          protected int m_routeNumber;
        
          protected int m_periodIndex;
        
          protected int m_employeeId;
        
          protected String m_documentNumber;
        
          protected int? m_odometer;
        
          protected String m_password;
        
          protected DateTime m_dateCreated;
        
          protected DateTime? m_dateCommited;
        
        #endregion
        
        #region Constructors
        public BusinessTransaction(
        long 
          sessionId,int 
          businessTransactionId
         )
        {
        
          m_sessionId = sessionId;
        
          m_businessTransactionId = businessTransactionId;
        
        }
        
        


        public BusinessTransaction(
        long 
          sessionId,int 
          businessTransactionId,int 
          businessTransactionTypeId,int 
          businessTransactionStatusId,int 
          locationId,int 
          routeNumber,int 
          periodIndex,int 
          employeeId,String 
          documentNumber,int? 
          odometer,String 
          password,DateTime 
          dateCreated,DateTime? 
          dateCommited
        )
        {
        
          m_sessionId = sessionId;
        
          m_businessTransactionId = businessTransactionId;
        
          m_businessTransactionTypeId = businessTransactionTypeId;
        
          m_businessTransactionStatusId = businessTransactionStatusId;
        
          m_locationId = locationId;
        
          m_routeNumber = routeNumber;
        
          m_periodIndex = periodIndex;
        
          m_employeeId = employeeId;
        
          m_documentNumber = documentNumber;
        
          m_odometer = odometer;
        
          m_password = password;
        
          m_dateCreated = dateCreated;
        
          m_dateCommited = dateCommited;
        
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
        public int BusinessTransactionTypeId
        {
          get { return m_businessTransactionTypeId;}
          set { m_businessTransactionTypeId = value; }
        }
      
        [XmlElement]
        public int BusinessTransactionStatusId
        {
          get { return m_businessTransactionStatusId;}
          set { m_businessTransactionStatusId = value; }
        }
      
        [XmlElement]
        public int LocationId
        {
          get { return m_locationId;}
          set { m_locationId = value; }
        }
      
        [XmlElement]
        public int RouteNumber
        {
          get { return m_routeNumber;}
          set { m_routeNumber = value; }
        }
      
        [XmlElement]
        public int PeriodIndex
        {
          get { return m_periodIndex;}
          set { m_periodIndex = value; }
        }
      
        [XmlElement]
        public int EmployeeId
        {
          get { return m_employeeId;}
          set { m_employeeId = value; }
        }
      
        [XmlElement]
        public String DocumentNumber
        {
          get { return m_documentNumber;}
          set { m_documentNumber = value; }
        }
      
        [XmlElement]
        public int? Odometer
        {
          get { return m_odometer;}
          set { m_odometer = value; }
        }
      
        [XmlElement]
        public String Password
        {
          get { return m_password;}
          set { m_password = value; }
        }
      
        [XmlElement]
        public DateTime DateCreated
        {
          get { return m_dateCreated;}
          set { m_dateCreated = value; }
        }
      
        [XmlElement]
        public DateTime? DateCommited
        {
          get { return m_dateCommited;}
          set { m_dateCommited = value; }
        }
      
      }
      #endregion
      }

    