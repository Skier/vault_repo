
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


      public partial class WorkTransactionPayment : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WorkTransactionPayment ( " +
      
        " WorkTransactionId, " +
      
        " WorkTransactionPaymentTypeId, " +
      
        " PaymentAmount, " +
      
        " FirstName, " +
      
        " LastName, " +
      
        " Address, " +
      
        " City, " +
      
        " State, " +
      
        " Zip, " +
      
        " CreditCardTypeId, " +
      
        " CreditCardNumber, " +
      
        " CreditCardExpirationDate, " +
      
        " CreditCardCVV2TypeId, " +
      
        " CreditCardCVV2, " +
      
        " BankCheckAccountTypeId, " +
      
        " BankCheckNumber, " +
      
        " BankRouteNumber, " +
      
        " BankCheckCompany, " +
      
        " BankCheckBankName, " +
      
        " BankCheckAccountNumber, " +
      
        " IsAccepted, " +
      
        " ServerResponse " +
      
      ") Values (" +
      
        " ?WorkTransactionId, " +
      
        " ?WorkTransactionPaymentTypeId, " +
      
        " ?PaymentAmount, " +
      
        " ?FirstName, " +
      
        " ?LastName, " +
      
        " ?Address, " +
      
        " ?City, " +
      
        " ?State, " +
      
        " ?Zip, " +
      
        " ?CreditCardTypeId, " +
      
        " ?CreditCardNumber, " +
      
        " ?CreditCardExpirationDate, " +
      
        " ?CreditCardCVV2TypeId, " +
      
        " ?CreditCardCVV2, " +
      
        " ?BankCheckAccountTypeId, " +
      
        " ?BankCheckNumber, " +
      
        " ?BankRouteNumber, " +
      
        " ?BankCheckCompany, " +
      
        " ?BankCheckBankName, " +
      
        " ?BankCheckAccountNumber, " +
      
        " ?IsAccepted, " +
      
        " ?ServerResponse " +
      
      ")";

      public static void Insert(WorkTransactionPayment workTransactionPayment, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionPayment.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?WorkTransactionPaymentTypeId", workTransactionPayment.WorkTransactionPaymentTypeId);
      
        Database.PutParameter(dbCommand,"?PaymentAmount", workTransactionPayment.PaymentAmount);
      
        Database.PutParameter(dbCommand,"?FirstName", workTransactionPayment.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", workTransactionPayment.LastName);
      
        Database.PutParameter(dbCommand,"?Address", workTransactionPayment.Address);
      
        Database.PutParameter(dbCommand,"?City", workTransactionPayment.City);
      
        Database.PutParameter(dbCommand,"?State", workTransactionPayment.State);
      
        Database.PutParameter(dbCommand,"?Zip", workTransactionPayment.Zip);
      
        Database.PutParameter(dbCommand,"?CreditCardTypeId", workTransactionPayment.CreditCardTypeId);
      
        Database.PutParameter(dbCommand,"?CreditCardNumber", workTransactionPayment.CreditCardNumber);
      
        Database.PutParameter(dbCommand,"?CreditCardExpirationDate", workTransactionPayment.CreditCardExpirationDate);
      
        Database.PutParameter(dbCommand,"?CreditCardCVV2TypeId", workTransactionPayment.CreditCardCVV2TypeId);
      
        Database.PutParameter(dbCommand,"?CreditCardCVV2", workTransactionPayment.CreditCardCVV2);
      
        Database.PutParameter(dbCommand,"?BankCheckAccountTypeId", workTransactionPayment.BankCheckAccountTypeId);
      
        Database.PutParameter(dbCommand,"?BankCheckNumber", workTransactionPayment.BankCheckNumber);
      
        Database.PutParameter(dbCommand,"?BankRouteNumber", workTransactionPayment.BankRouteNumber);
      
        Database.PutParameter(dbCommand,"?BankCheckCompany", workTransactionPayment.BankCheckCompany);
      
        Database.PutParameter(dbCommand,"?BankCheckBankName", workTransactionPayment.BankCheckBankName);
      
        Database.PutParameter(dbCommand,"?BankCheckAccountNumber", workTransactionPayment.BankCheckAccountNumber);
      
        Database.PutParameter(dbCommand,"?IsAccepted", workTransactionPayment.IsAccepted);
      
        Database.PutParameter(dbCommand,"?ServerResponse", workTransactionPayment.ServerResponse);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        workTransactionPayment.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(WorkTransactionPayment workTransactionPayment)
      {
        Insert(workTransactionPayment, null);
      }


      public static void Insert(List<WorkTransactionPayment>  workTransactionPaymentList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WorkTransactionPayment workTransactionPayment in  workTransactionPaymentList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionPayment.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?WorkTransactionPaymentTypeId", workTransactionPayment.WorkTransactionPaymentTypeId);
      
        Database.PutParameter(dbCommand,"?PaymentAmount", workTransactionPayment.PaymentAmount);
      
        Database.PutParameter(dbCommand,"?FirstName", workTransactionPayment.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", workTransactionPayment.LastName);
      
        Database.PutParameter(dbCommand,"?Address", workTransactionPayment.Address);
      
        Database.PutParameter(dbCommand,"?City", workTransactionPayment.City);
      
        Database.PutParameter(dbCommand,"?State", workTransactionPayment.State);
      
        Database.PutParameter(dbCommand,"?Zip", workTransactionPayment.Zip);
      
        Database.PutParameter(dbCommand,"?CreditCardTypeId", workTransactionPayment.CreditCardTypeId);
      
        Database.PutParameter(dbCommand,"?CreditCardNumber", workTransactionPayment.CreditCardNumber);
      
        Database.PutParameter(dbCommand,"?CreditCardExpirationDate", workTransactionPayment.CreditCardExpirationDate);
      
        Database.PutParameter(dbCommand,"?CreditCardCVV2TypeId", workTransactionPayment.CreditCardCVV2TypeId);
      
        Database.PutParameter(dbCommand,"?CreditCardCVV2", workTransactionPayment.CreditCardCVV2);
      
        Database.PutParameter(dbCommand,"?BankCheckAccountTypeId", workTransactionPayment.BankCheckAccountTypeId);
      
        Database.PutParameter(dbCommand,"?BankCheckNumber", workTransactionPayment.BankCheckNumber);
      
        Database.PutParameter(dbCommand,"?BankRouteNumber", workTransactionPayment.BankRouteNumber);
      
        Database.PutParameter(dbCommand,"?BankCheckCompany", workTransactionPayment.BankCheckCompany);
      
        Database.PutParameter(dbCommand,"?BankCheckBankName", workTransactionPayment.BankCheckBankName);
      
        Database.PutParameter(dbCommand,"?BankCheckAccountNumber", workTransactionPayment.BankCheckAccountNumber);
      
        Database.PutParameter(dbCommand,"?IsAccepted", workTransactionPayment.IsAccepted);
      
        Database.PutParameter(dbCommand,"?ServerResponse", workTransactionPayment.ServerResponse);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WorkTransactionId",workTransactionPayment.WorkTransactionId);
      
        Database.UpdateParameter(dbCommand,"?WorkTransactionPaymentTypeId",workTransactionPayment.WorkTransactionPaymentTypeId);
      
        Database.UpdateParameter(dbCommand,"?PaymentAmount",workTransactionPayment.PaymentAmount);
      
        Database.UpdateParameter(dbCommand,"?FirstName",workTransactionPayment.FirstName);
      
        Database.UpdateParameter(dbCommand,"?LastName",workTransactionPayment.LastName);
      
        Database.UpdateParameter(dbCommand,"?Address",workTransactionPayment.Address);
      
        Database.UpdateParameter(dbCommand,"?City",workTransactionPayment.City);
      
        Database.UpdateParameter(dbCommand,"?State",workTransactionPayment.State);
      
        Database.UpdateParameter(dbCommand,"?Zip",workTransactionPayment.Zip);
      
        Database.UpdateParameter(dbCommand,"?CreditCardTypeId",workTransactionPayment.CreditCardTypeId);
      
        Database.UpdateParameter(dbCommand,"?CreditCardNumber",workTransactionPayment.CreditCardNumber);
      
        Database.UpdateParameter(dbCommand,"?CreditCardExpirationDate",workTransactionPayment.CreditCardExpirationDate);
      
        Database.UpdateParameter(dbCommand,"?CreditCardCVV2TypeId",workTransactionPayment.CreditCardCVV2TypeId);
      
        Database.UpdateParameter(dbCommand,"?CreditCardCVV2",workTransactionPayment.CreditCardCVV2);
      
        Database.UpdateParameter(dbCommand,"?BankCheckAccountTypeId",workTransactionPayment.BankCheckAccountTypeId);
      
        Database.UpdateParameter(dbCommand,"?BankCheckNumber",workTransactionPayment.BankCheckNumber);
      
        Database.UpdateParameter(dbCommand,"?BankRouteNumber",workTransactionPayment.BankRouteNumber);
      
        Database.UpdateParameter(dbCommand,"?BankCheckCompany",workTransactionPayment.BankCheckCompany);
      
        Database.UpdateParameter(dbCommand,"?BankCheckBankName",workTransactionPayment.BankCheckBankName);
      
        Database.UpdateParameter(dbCommand,"?BankCheckAccountNumber",workTransactionPayment.BankCheckAccountNumber);
      
        Database.UpdateParameter(dbCommand,"?IsAccepted",workTransactionPayment.IsAccepted);
      
        Database.UpdateParameter(dbCommand,"?ServerResponse",workTransactionPayment.ServerResponse);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        workTransactionPayment.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<WorkTransactionPayment>  workTransactionPaymentList)
      {
        Insert(workTransactionPaymentList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WorkTransactionPayment Set "
      
        + " WorkTransactionId = ?WorkTransactionId, "
      
        + " WorkTransactionPaymentTypeId = ?WorkTransactionPaymentTypeId, "
      
        + " PaymentAmount = ?PaymentAmount, "
      
        + " FirstName = ?FirstName, "
      
        + " LastName = ?LastName, "
      
        + " Address = ?Address, "
      
        + " City = ?City, "
      
        + " State = ?State, "
      
        + " Zip = ?Zip, "
      
        + " CreditCardTypeId = ?CreditCardTypeId, "
      
        + " CreditCardNumber = ?CreditCardNumber, "
      
        + " CreditCardExpirationDate = ?CreditCardExpirationDate, "
      
        + " CreditCardCVV2TypeId = ?CreditCardCVV2TypeId, "
      
        + " CreditCardCVV2 = ?CreditCardCVV2, "
      
        + " BankCheckAccountTypeId = ?BankCheckAccountTypeId, "
      
        + " BankCheckNumber = ?BankCheckNumber, "
      
        + " BankRouteNumber = ?BankRouteNumber, "
      
        + " BankCheckCompany = ?BankCheckCompany, "
      
        + " BankCheckBankName = ?BankCheckBankName, "
      
        + " BankCheckAccountNumber = ?BankCheckAccountNumber, "
      
        + " IsAccepted = ?IsAccepted, "
      
        + " ServerResponse = ?ServerResponse "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WorkTransactionPayment workTransactionPayment, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", workTransactionPayment.ID);
      
        Database.PutParameter(dbCommand,"?WorkTransactionId", workTransactionPayment.WorkTransactionId);
      
        Database.PutParameter(dbCommand,"?WorkTransactionPaymentTypeId", workTransactionPayment.WorkTransactionPaymentTypeId);
      
        Database.PutParameter(dbCommand,"?PaymentAmount", workTransactionPayment.PaymentAmount);
      
        Database.PutParameter(dbCommand,"?FirstName", workTransactionPayment.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", workTransactionPayment.LastName);
      
        Database.PutParameter(dbCommand,"?Address", workTransactionPayment.Address);
      
        Database.PutParameter(dbCommand,"?City", workTransactionPayment.City);
      
        Database.PutParameter(dbCommand,"?State", workTransactionPayment.State);
      
        Database.PutParameter(dbCommand,"?Zip", workTransactionPayment.Zip);
      
        Database.PutParameter(dbCommand,"?CreditCardTypeId", workTransactionPayment.CreditCardTypeId);
      
        Database.PutParameter(dbCommand,"?CreditCardNumber", workTransactionPayment.CreditCardNumber);
      
        Database.PutParameter(dbCommand,"?CreditCardExpirationDate", workTransactionPayment.CreditCardExpirationDate);
      
        Database.PutParameter(dbCommand,"?CreditCardCVV2TypeId", workTransactionPayment.CreditCardCVV2TypeId);
      
        Database.PutParameter(dbCommand,"?CreditCardCVV2", workTransactionPayment.CreditCardCVV2);
      
        Database.PutParameter(dbCommand,"?BankCheckAccountTypeId", workTransactionPayment.BankCheckAccountTypeId);
      
        Database.PutParameter(dbCommand,"?BankCheckNumber", workTransactionPayment.BankCheckNumber);
      
        Database.PutParameter(dbCommand,"?BankRouteNumber", workTransactionPayment.BankRouteNumber);
      
        Database.PutParameter(dbCommand,"?BankCheckCompany", workTransactionPayment.BankCheckCompany);
      
        Database.PutParameter(dbCommand,"?BankCheckBankName", workTransactionPayment.BankCheckBankName);
      
        Database.PutParameter(dbCommand,"?BankCheckAccountNumber", workTransactionPayment.BankCheckAccountNumber);
      
        Database.PutParameter(dbCommand,"?IsAccepted", workTransactionPayment.IsAccepted);
      
        Database.PutParameter(dbCommand,"?ServerResponse", workTransactionPayment.ServerResponse);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WorkTransactionPayment workTransactionPayment)
      {
        Update(workTransactionPayment, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WorkTransactionId, "
      
        + " WorkTransactionPaymentTypeId, "
      
        + " PaymentAmount, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Address, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " CreditCardTypeId, "
      
        + " CreditCardNumber, "
      
        + " CreditCardExpirationDate, "
      
        + " CreditCardCVV2TypeId, "
      
        + " CreditCardCVV2, "
      
        + " BankCheckAccountTypeId, "
      
        + " BankCheckNumber, "
      
        + " BankRouteNumber, "
      
        + " BankCheckCompany, "
      
        + " BankCheckBankName, "
      
        + " BankCheckAccountNumber, "
      
        + " IsAccepted, "
      
        + " ServerResponse "
      

      + " From WorkTransactionPayment "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WorkTransactionPayment FindByPrimaryKey(
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
      throw new DataNotFoundException("WorkTransactionPayment not found, search by primary key");

      }

      public static WorkTransactionPayment FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WorkTransactionPayment workTransactionPayment, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",workTransactionPayment.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WorkTransactionPayment workTransactionPayment)
      {
      return Exists(workTransactionPayment, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WorkTransactionPayment limit 1";

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

      public static WorkTransactionPayment Load(IDataReader dataReader, int offset)
      {
      WorkTransactionPayment workTransactionPayment = new WorkTransactionPayment();

      workTransactionPayment.ID = dataReader.GetInt32(0 + offset);
          workTransactionPayment.WorkTransactionId = dataReader.GetInt32(1 + offset);
          workTransactionPayment.WorkTransactionPaymentTypeId = dataReader.GetInt32(2 + offset);
          workTransactionPayment.PaymentAmount = dataReader.GetDecimal(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            workTransactionPayment.FirstName = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            workTransactionPayment.LastName = dataReader.GetString(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            workTransactionPayment.Address = dataReader.GetString(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            workTransactionPayment.City = dataReader.GetString(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            workTransactionPayment.State = dataReader.GetString(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            workTransactionPayment.Zip = dataReader.GetString(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            workTransactionPayment.CreditCardTypeId = dataReader.GetInt32(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            workTransactionPayment.CreditCardNumber = dataReader.GetString(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            workTransactionPayment.CreditCardExpirationDate = dataReader.GetDateTime(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            workTransactionPayment.CreditCardCVV2TypeId = dataReader.GetInt32(13 + offset);
          
            if(!dataReader.IsDBNull(14 + offset))
            workTransactionPayment.CreditCardCVV2 = dataReader.GetString(14 + offset);
          
            if(!dataReader.IsDBNull(15 + offset))
            workTransactionPayment.BankCheckAccountTypeId = dataReader.GetInt32(15 + offset);
          
            if(!dataReader.IsDBNull(16 + offset))
            workTransactionPayment.BankCheckNumber = dataReader.GetString(16 + offset);
          
            if(!dataReader.IsDBNull(17 + offset))
            workTransactionPayment.BankRouteNumber = dataReader.GetString(17 + offset);
          
            if(!dataReader.IsDBNull(18 + offset))
            workTransactionPayment.BankCheckCompany = dataReader.GetString(18 + offset);
          
            if(!dataReader.IsDBNull(19 + offset))
            workTransactionPayment.BankCheckBankName = dataReader.GetString(19 + offset);
          
            if(!dataReader.IsDBNull(20 + offset))
            workTransactionPayment.BankCheckAccountNumber = dataReader.GetString(20 + offset);
          workTransactionPayment.IsAccepted = dataReader.GetBoolean(21 + offset);
          workTransactionPayment.ServerResponse = dataReader.GetString(22 + offset);
          

      return workTransactionPayment;
      }

      public static WorkTransactionPayment Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WorkTransactionPayment "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WorkTransactionPayment workTransactionPayment, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", workTransactionPayment.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WorkTransactionPayment workTransactionPayment)
      {
        Delete(workTransactionPayment, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WorkTransactionPayment ";

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
      
        + " WorkTransactionId, "
      
        + " WorkTransactionPaymentTypeId, "
      
        + " PaymentAmount, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Address, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " CreditCardTypeId, "
      
        + " CreditCardNumber, "
      
        + " CreditCardExpirationDate, "
      
        + " CreditCardCVV2TypeId, "
      
        + " CreditCardCVV2, "
      
        + " BankCheckAccountTypeId, "
      
        + " BankCheckNumber, "
      
        + " BankRouteNumber, "
      
        + " BankCheckCompany, "
      
        + " BankCheckBankName, "
      
        + " BankCheckAccountNumber, "
      
        + " IsAccepted, "
      
        + " ServerResponse "
      

      + " From WorkTransactionPayment ";
      public static List<WorkTransactionPayment> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WorkTransactionPayment> rv = new List<WorkTransactionPayment>();

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

      public static List<WorkTransactionPayment> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WorkTransactionPayment> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WorkTransactionPayment obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && WorkTransactionId == obj.WorkTransactionId && WorkTransactionPaymentTypeId == obj.WorkTransactionPaymentTypeId && PaymentAmount == obj.PaymentAmount && FirstName == obj.FirstName && LastName == obj.LastName && Address == obj.Address && City == obj.City && State == obj.State && Zip == obj.Zip && CreditCardTypeId == obj.CreditCardTypeId && CreditCardNumber == obj.CreditCardNumber && CreditCardExpirationDate == obj.CreditCardExpirationDate && CreditCardCVV2TypeId == obj.CreditCardCVV2TypeId && CreditCardCVV2 == obj.CreditCardCVV2 && BankCheckAccountTypeId == obj.BankCheckAccountTypeId && BankCheckNumber == obj.BankCheckNumber && BankRouteNumber == obj.BankRouteNumber && BankCheckCompany == obj.BankCheckCompany && BankCheckBankName == obj.BankCheckBankName && BankCheckAccountNumber == obj.BankCheckAccountNumber && IsAccepted == obj.IsAccepted && ServerResponse == obj.ServerResponse;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WorkTransactionPayment> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionPayment));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WorkTransactionPayment item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WorkTransactionPayment>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WorkTransactionPayment));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WorkTransactionPayment> itemsList
      = new List<WorkTransactionPayment>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WorkTransactionPayment)
      itemsList.Add(deserializedObject as WorkTransactionPayment);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_workTransactionId;
      
        protected int m_workTransactionPaymentTypeId;
      
        protected decimal m_paymentAmount;
      
        protected String m_firstName;
      
        protected String m_lastName;
      
        protected String m_address;
      
        protected String m_city;
      
        protected String m_state;
      
        protected String m_zip;
      
        protected int? m_creditCardTypeId;
      
        protected String m_creditCardNumber;
      
        protected DateTime? m_creditCardExpirationDate;
      
        protected int? m_creditCardCVV2TypeId;
      
        protected String m_creditCardCVV2;
      
        protected int? m_bankCheckAccountTypeId;
      
        protected String m_bankCheckNumber;
      
        protected String m_bankRouteNumber;
      
        protected String m_bankCheckCompany;
      
        protected String m_bankCheckBankName;
      
        protected String m_bankCheckAccountNumber;
      
        protected bool m_isAccepted;
      
        protected String m_serverResponse;
      
      #endregion

      #region Constructors
      public WorkTransactionPayment(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WorkTransactionPayment(
        int 
          iD,int 
          workTransactionId,int 
          workTransactionPaymentTypeId,decimal 
          paymentAmount,String 
          firstName,String 
          lastName,String 
          address,String 
          city,String 
          state,String 
          zip,int? 
          creditCardTypeId,String 
          creditCardNumber,DateTime? 
          creditCardExpirationDate,int? 
          creditCardCVV2TypeId,String 
          creditCardCVV2,int? 
          bankCheckAccountTypeId,String 
          bankCheckNumber,String 
          bankRouteNumber,String 
          bankCheckCompany,String 
          bankCheckBankName,String 
          bankCheckAccountNumber,bool 
          isAccepted,String 
          serverResponse
        ) : this()
        {
        
          m_iD = iD;
        
          m_workTransactionId = workTransactionId;
        
          m_workTransactionPaymentTypeId = workTransactionPaymentTypeId;
        
          m_paymentAmount = paymentAmount;
        
          m_firstName = firstName;
        
          m_lastName = lastName;
        
          m_address = address;
        
          m_city = city;
        
          m_state = state;
        
          m_zip = zip;
        
          m_creditCardTypeId = creditCardTypeId;
        
          m_creditCardNumber = creditCardNumber;
        
          m_creditCardExpirationDate = creditCardExpirationDate;
        
          m_creditCardCVV2TypeId = creditCardCVV2TypeId;
        
          m_creditCardCVV2 = creditCardCVV2;
        
          m_bankCheckAccountTypeId = bankCheckAccountTypeId;
        
          m_bankCheckNumber = bankCheckNumber;
        
          m_bankRouteNumber = bankRouteNumber;
        
          m_bankCheckCompany = bankCheckCompany;
        
          m_bankCheckBankName = bankCheckBankName;
        
          m_bankCheckAccountNumber = bankCheckAccountNumber;
        
          m_isAccepted = isAccepted;
        
          m_serverResponse = serverResponse;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int WorkTransactionId
        {
        get { return m_workTransactionId;}
        set { m_workTransactionId = value; }
        }
      
        [XmlElement]
        public int WorkTransactionPaymentTypeId
        {
        get { return m_workTransactionPaymentTypeId;}
        set { m_workTransactionPaymentTypeId = value; }
        }
      
        [XmlElement]
        public decimal PaymentAmount
        {
        get { return m_paymentAmount;}
        set { m_paymentAmount = value; }
        }
      
        [XmlElement]
        public String FirstName
        {
        get { return m_firstName;}
        set { m_firstName = value; }
        }
      
        [XmlElement]
        public String LastName
        {
        get { return m_lastName;}
        set { m_lastName = value; }
        }
      
        [XmlElement]
        public String Address
        {
        get { return m_address;}
        set { m_address = value; }
        }
      
        [XmlElement]
        public String City
        {
        get { return m_city;}
        set { m_city = value; }
        }
      
        [XmlElement]
        public String State
        {
        get { return m_state;}
        set { m_state = value; }
        }
      
        [XmlElement]
        public String Zip
        {
        get { return m_zip;}
        set { m_zip = value; }
        }
      
        [XmlElement]
        public int? CreditCardTypeId
        {
        get { return m_creditCardTypeId;}
        set { m_creditCardTypeId = value; }
        }
      
        [XmlElement]
        public String CreditCardNumber
        {
        get { return m_creditCardNumber;}
        set { m_creditCardNumber = value; }
        }
      
        [XmlElement]
        public DateTime? CreditCardExpirationDate
        {
        get { return m_creditCardExpirationDate;}
        set { m_creditCardExpirationDate = value; }
        }
      
        [XmlElement]
        public int? CreditCardCVV2TypeId
        {
        get { return m_creditCardCVV2TypeId;}
        set { m_creditCardCVV2TypeId = value; }
        }
      
        [XmlElement]
        public String CreditCardCVV2
        {
        get { return m_creditCardCVV2;}
        set { m_creditCardCVV2 = value; }
        }
      
        [XmlElement]
        public int? BankCheckAccountTypeId
        {
        get { return m_bankCheckAccountTypeId;}
        set { m_bankCheckAccountTypeId = value; }
        }
      
        [XmlElement]
        public String BankCheckNumber
        {
        get { return m_bankCheckNumber;}
        set { m_bankCheckNumber = value; }
        }
      
        [XmlElement]
        public String BankRouteNumber
        {
        get { return m_bankRouteNumber;}
        set { m_bankRouteNumber = value; }
        }
      
        [XmlElement]
        public String BankCheckCompany
        {
        get { return m_bankCheckCompany;}
        set { m_bankCheckCompany = value; }
        }
      
        [XmlElement]
        public String BankCheckBankName
        {
        get { return m_bankCheckBankName;}
        set { m_bankCheckBankName = value; }
        }
      
        [XmlElement]
        public String BankCheckAccountNumber
        {
        get { return m_bankCheckAccountNumber;}
        set { m_bankCheckAccountNumber = value; }
        }
      
        [XmlElement]
        public bool IsAccepted
        {
        get { return m_isAccepted;}
        set { m_isAccepted = value; }
        }
      
        [XmlElement]
        public String ServerResponse
        {
        get { return m_serverResponse;}
        set { m_serverResponse = value; }
        }
      

      public static int FieldsCount
      {
      get { return 23; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    