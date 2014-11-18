
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


      public partial class CustomerWebAccount : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into CustomerWebAccount ( " +
      
        " CustomerId, " +
      
        " BusinessPartnerId, " +
      
        " Company, " +
      
        " FirstName, " +
      
        " LastName, " +
      
        " Address1, " +
      
        " Address2, " +
      
        " City, " +
      
        " State, " +
      
        " Zip, " +
      
        " Phone1, " +
      
        " Phone2, " +
      
        " Email, " +
      
        " LastModifiedDate, " +
      
        " Password " +
      
      ") Values (" +
      
        " ?CustomerId, " +
      
        " ?BusinessPartnerId, " +
      
        " ?Company, " +
      
        " ?FirstName, " +
      
        " ?LastName, " +
      
        " ?Address1, " +
      
        " ?Address2, " +
      
        " ?City, " +
      
        " ?State, " +
      
        " ?Zip, " +
      
        " ?Phone1, " +
      
        " ?Phone2, " +
      
        " ?Email, " +
      
        " ?LastModifiedDate, " +
      
        " ?Password " +
      
      ")";

      public static void Insert(CustomerWebAccount customerWebAccount, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?CustomerId", customerWebAccount.CustomerId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", customerWebAccount.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?Company", customerWebAccount.Company);
      
        Database.PutParameter(dbCommand,"?FirstName", customerWebAccount.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", customerWebAccount.LastName);
      
        Database.PutParameter(dbCommand,"?Address1", customerWebAccount.Address1);
      
        Database.PutParameter(dbCommand,"?Address2", customerWebAccount.Address2);
      
        Database.PutParameter(dbCommand,"?City", customerWebAccount.City);
      
        Database.PutParameter(dbCommand,"?State", customerWebAccount.State);
      
        Database.PutParameter(dbCommand,"?Zip", customerWebAccount.Zip);
      
        Database.PutParameter(dbCommand,"?Phone1", customerWebAccount.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", customerWebAccount.Phone2);
      
        Database.PutParameter(dbCommand,"?Email", customerWebAccount.Email);
      
        Database.PutParameter(dbCommand,"?LastModifiedDate", customerWebAccount.LastModifiedDate);
      
        Database.PutParameter(dbCommand,"?Password", customerWebAccount.Password);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        customerWebAccount.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(CustomerWebAccount customerWebAccount)
      {
        Insert(customerWebAccount, null);
      }


      public static void Insert(List<CustomerWebAccount>  customerWebAccountList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(CustomerWebAccount customerWebAccount in  customerWebAccountList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?CustomerId", customerWebAccount.CustomerId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", customerWebAccount.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?Company", customerWebAccount.Company);
      
        Database.PutParameter(dbCommand,"?FirstName", customerWebAccount.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", customerWebAccount.LastName);
      
        Database.PutParameter(dbCommand,"?Address1", customerWebAccount.Address1);
      
        Database.PutParameter(dbCommand,"?Address2", customerWebAccount.Address2);
      
        Database.PutParameter(dbCommand,"?City", customerWebAccount.City);
      
        Database.PutParameter(dbCommand,"?State", customerWebAccount.State);
      
        Database.PutParameter(dbCommand,"?Zip", customerWebAccount.Zip);
      
        Database.PutParameter(dbCommand,"?Phone1", customerWebAccount.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", customerWebAccount.Phone2);
      
        Database.PutParameter(dbCommand,"?Email", customerWebAccount.Email);
      
        Database.PutParameter(dbCommand,"?LastModifiedDate", customerWebAccount.LastModifiedDate);
      
        Database.PutParameter(dbCommand,"?Password", customerWebAccount.Password);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?CustomerId",customerWebAccount.CustomerId);
      
        Database.UpdateParameter(dbCommand,"?BusinessPartnerId",customerWebAccount.BusinessPartnerId);
      
        Database.UpdateParameter(dbCommand,"?Company",customerWebAccount.Company);
      
        Database.UpdateParameter(dbCommand,"?FirstName",customerWebAccount.FirstName);
      
        Database.UpdateParameter(dbCommand,"?LastName",customerWebAccount.LastName);
      
        Database.UpdateParameter(dbCommand,"?Address1",customerWebAccount.Address1);
      
        Database.UpdateParameter(dbCommand,"?Address2",customerWebAccount.Address2);
      
        Database.UpdateParameter(dbCommand,"?City",customerWebAccount.City);
      
        Database.UpdateParameter(dbCommand,"?State",customerWebAccount.State);
      
        Database.UpdateParameter(dbCommand,"?Zip",customerWebAccount.Zip);
      
        Database.UpdateParameter(dbCommand,"?Phone1",customerWebAccount.Phone1);
      
        Database.UpdateParameter(dbCommand,"?Phone2",customerWebAccount.Phone2);
      
        Database.UpdateParameter(dbCommand,"?Email",customerWebAccount.Email);
      
        Database.UpdateParameter(dbCommand,"?LastModifiedDate",customerWebAccount.LastModifiedDate);
      
        Database.UpdateParameter(dbCommand,"?Password",customerWebAccount.Password);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        customerWebAccount.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<CustomerWebAccount>  customerWebAccountList)
      {
        Insert(customerWebAccountList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update CustomerWebAccount Set "
      
        + " CustomerId = ?CustomerId, "
      
        + " BusinessPartnerId = ?BusinessPartnerId, "
      
        + " Company = ?Company, "
      
        + " FirstName = ?FirstName, "
      
        + " LastName = ?LastName, "
      
        + " Address1 = ?Address1, "
      
        + " Address2 = ?Address2, "
      
        + " City = ?City, "
      
        + " State = ?State, "
      
        + " Zip = ?Zip, "
      
        + " Phone1 = ?Phone1, "
      
        + " Phone2 = ?Phone2, "
      
        + " Email = ?Email, "
      
        + " LastModifiedDate = ?LastModifiedDate, "
      
        + " Password = ?Password "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(CustomerWebAccount customerWebAccount, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", customerWebAccount.ID);
      
        Database.PutParameter(dbCommand,"?CustomerId", customerWebAccount.CustomerId);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", customerWebAccount.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?Company", customerWebAccount.Company);
      
        Database.PutParameter(dbCommand,"?FirstName", customerWebAccount.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", customerWebAccount.LastName);
      
        Database.PutParameter(dbCommand,"?Address1", customerWebAccount.Address1);
      
        Database.PutParameter(dbCommand,"?Address2", customerWebAccount.Address2);
      
        Database.PutParameter(dbCommand,"?City", customerWebAccount.City);
      
        Database.PutParameter(dbCommand,"?State", customerWebAccount.State);
      
        Database.PutParameter(dbCommand,"?Zip", customerWebAccount.Zip);
      
        Database.PutParameter(dbCommand,"?Phone1", customerWebAccount.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", customerWebAccount.Phone2);
      
        Database.PutParameter(dbCommand,"?Email", customerWebAccount.Email);
      
        Database.PutParameter(dbCommand,"?LastModifiedDate", customerWebAccount.LastModifiedDate);
      
        Database.PutParameter(dbCommand,"?Password", customerWebAccount.Password);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(CustomerWebAccount customerWebAccount)
      {
        Update(customerWebAccount, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " CustomerId, "
      
        + " BusinessPartnerId, "
      
        + " Company, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Address1, "
      
        + " Address2, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " Phone1, "
      
        + " Phone2, "
      
        + " Email, "
      
        + " LastModifiedDate, "
      
        + " Password "
      

      + " From CustomerWebAccount "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static CustomerWebAccount FindByPrimaryKey(
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
      throw new DataNotFoundException("CustomerWebAccount not found, search by primary key");

      }

      public static CustomerWebAccount FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(CustomerWebAccount customerWebAccount, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",customerWebAccount.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(CustomerWebAccount customerWebAccount)
      {
      return Exists(customerWebAccount, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from CustomerWebAccount limit 1";

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

      public static CustomerWebAccount Load(IDataReader dataReader, int offset)
      {
      CustomerWebAccount customerWebAccount = new CustomerWebAccount();

      customerWebAccount.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            customerWebAccount.CustomerId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            customerWebAccount.BusinessPartnerId = dataReader.GetInt32(2 + offset);
          customerWebAccount.Company = dataReader.GetString(3 + offset);
          customerWebAccount.FirstName = dataReader.GetString(4 + offset);
          customerWebAccount.LastName = dataReader.GetString(5 + offset);
          customerWebAccount.Address1 = dataReader.GetString(6 + offset);
          customerWebAccount.Address2 = dataReader.GetString(7 + offset);
          customerWebAccount.City = dataReader.GetString(8 + offset);
          customerWebAccount.State = dataReader.GetString(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            customerWebAccount.Zip = dataReader.GetInt32(10 + offset);
          customerWebAccount.Phone1 = dataReader.GetString(11 + offset);
          customerWebAccount.Phone2 = dataReader.GetString(12 + offset);
          customerWebAccount.Email = dataReader.GetString(13 + offset);
          customerWebAccount.LastModifiedDate = dataReader.GetDateTime(14 + offset);
          customerWebAccount.Password = dataReader.GetString(15 + offset);
          

      return customerWebAccount;
      }

      public static CustomerWebAccount Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From CustomerWebAccount "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(CustomerWebAccount customerWebAccount, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", customerWebAccount.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(CustomerWebAccount customerWebAccount)
      {
        Delete(customerWebAccount, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From CustomerWebAccount ";

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
      
        + " CustomerId, "
      
        + " BusinessPartnerId, "
      
        + " Company, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Address1, "
      
        + " Address2, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " Phone1, "
      
        + " Phone2, "
      
        + " Email, "
      
        + " LastModifiedDate, "
      
        + " Password "
      

      + " From CustomerWebAccount ";
      public static List<CustomerWebAccount> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<CustomerWebAccount> rv = new List<CustomerWebAccount>();

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

      public static List<CustomerWebAccount> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<CustomerWebAccount> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (CustomerWebAccount obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && CustomerId == obj.CustomerId && BusinessPartnerId == obj.BusinessPartnerId && Company == obj.Company && FirstName == obj.FirstName && LastName == obj.LastName && Address1 == obj.Address1 && Address2 == obj.Address2 && City == obj.City && State == obj.State && Zip == obj.Zip && Phone1 == obj.Phone1 && Phone2 == obj.Phone2 && Email == obj.Email && LastModifiedDate == obj.LastModifiedDate && Password == obj.Password;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<CustomerWebAccount> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerWebAccount));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(CustomerWebAccount item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CustomerWebAccount>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerWebAccount));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<CustomerWebAccount> itemsList
      = new List<CustomerWebAccount>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CustomerWebAccount)
      itemsList.Add(deserializedObject as CustomerWebAccount);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_customerId;
      
        protected int? m_businessPartnerId;
      
        protected String m_company;
      
        protected String m_firstName;
      
        protected String m_lastName;
      
        protected String m_address1;
      
        protected String m_address2;
      
        protected String m_city;
      
        protected String m_state;
      
        protected int? m_zip;
      
        protected String m_phone1;
      
        protected String m_phone2;
      
        protected String m_email;
      
        protected DateTime m_lastModifiedDate;
      
        protected String m_password;
      
      #endregion

      #region Constructors
      public CustomerWebAccount(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public CustomerWebAccount(
        int 
          iD,int? 
          customerId,int? 
          businessPartnerId,String 
          company,String 
          firstName,String 
          lastName,String 
          address1,String 
          address2,String 
          city,String 
          state,int? 
          zip,String 
          phone1,String 
          phone2,String 
          email,DateTime 
          lastModifiedDate,String 
          password
        ) : this()
        {
        
          m_iD = iD;
        
          m_customerId = customerId;
        
          m_businessPartnerId = businessPartnerId;
        
          m_company = company;
        
          m_firstName = firstName;
        
          m_lastName = lastName;
        
          m_address1 = address1;
        
          m_address2 = address2;
        
          m_city = city;
        
          m_state = state;
        
          m_zip = zip;
        
          m_phone1 = phone1;
        
          m_phone2 = phone2;
        
          m_email = email;
        
          m_lastModifiedDate = lastModifiedDate;
        
          m_password = password;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int? CustomerId
        {
        get { return m_customerId;}
        set { m_customerId = value; }
        }
      
        [XmlElement]
        public int? BusinessPartnerId
        {
        get { return m_businessPartnerId;}
        set { m_businessPartnerId = value; }
        }
      
        [XmlElement]
        public String Company
        {
        get { return m_company;}
        set { m_company = value; }
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
        public String Address1
        {
        get { return m_address1;}
        set { m_address1 = value; }
        }
      
        [XmlElement]
        public String Address2
        {
        get { return m_address2;}
        set { m_address2 = value; }
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
        public int? Zip
        {
        get { return m_zip;}
        set { m_zip = value; }
        }
      
        [XmlElement]
        public String Phone1
        {
        get { return m_phone1;}
        set { m_phone1 = value; }
        }
      
        [XmlElement]
        public String Phone2
        {
        get { return m_phone2;}
        set { m_phone2 = value; }
        }
      
        [XmlElement]
        public String Email
        {
        get { return m_email;}
        set { m_email = value; }
        }
      
        [XmlElement]
        public DateTime LastModifiedDate
        {
        get { return m_lastModifiedDate;}
        set { m_lastModifiedDate = value; }
        }
      
        [XmlElement]
        public String Password
        {
        get { return m_password;}
        set { m_password = value; }
        }
      

      public static int FieldsCount
      {
      get { return 16; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    