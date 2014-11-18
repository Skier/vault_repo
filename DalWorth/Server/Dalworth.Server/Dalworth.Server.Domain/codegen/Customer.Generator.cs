
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


      public partial class Customer : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Customer ( " +
      
        " ServmanCustId, " +
      
        " CustomerTypeId, " +
      
        " AddressId, " +
      
        " FirstName, " +
      
        " LastName, " +
      
        " Phone1, " +
      
        " Phone2, " +
      
        " Email, " +
      
        " Modified, " +
      
        " LastSyncDate " +
      
      ") Values (" +
      
        " ?ServmanCustId, " +
      
        " ?CustomerTypeId, " +
      
        " ?AddressId, " +
      
        " ?FirstName, " +
      
        " ?LastName, " +
      
        " ?Phone1, " +
      
        " ?Phone2, " +
      
        " ?Email, " +
      
        " ?Modified, " +
      
        " ?LastSyncDate " +
      
      ")";

      public static void Insert(Customer customer, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ServmanCustId", customer.ServmanCustId);
      
        Database.PutParameter(dbCommand,"?CustomerTypeId", customer.CustomerTypeId);
      
        Database.PutParameter(dbCommand,"?AddressId", customer.AddressId);
      
        Database.PutParameter(dbCommand,"?FirstName", customer.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", customer.LastName);
      
        Database.PutParameter(dbCommand,"?Phone1", customer.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", customer.Phone2);
      
        Database.PutParameter(dbCommand,"?Email", customer.Email);
      
        Database.PutParameter(dbCommand,"?Modified", customer.Modified);
      
        Database.PutParameter(dbCommand,"?LastSyncDate", customer.LastSyncDate);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        customer.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Customer customer)
      {
        Insert(customer, null);
      }


      public static void Insert(List<Customer>  customerList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Customer customer in  customerList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ServmanCustId", customer.ServmanCustId);
      
        Database.PutParameter(dbCommand,"?CustomerTypeId", customer.CustomerTypeId);
      
        Database.PutParameter(dbCommand,"?AddressId", customer.AddressId);
      
        Database.PutParameter(dbCommand,"?FirstName", customer.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", customer.LastName);
      
        Database.PutParameter(dbCommand,"?Phone1", customer.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", customer.Phone2);
      
        Database.PutParameter(dbCommand,"?Email", customer.Email);
      
        Database.PutParameter(dbCommand,"?Modified", customer.Modified);
      
        Database.PutParameter(dbCommand,"?LastSyncDate", customer.LastSyncDate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ServmanCustId",customer.ServmanCustId);
      
        Database.UpdateParameter(dbCommand,"?CustomerTypeId",customer.CustomerTypeId);
      
        Database.UpdateParameter(dbCommand,"?AddressId",customer.AddressId);
      
        Database.UpdateParameter(dbCommand,"?FirstName",customer.FirstName);
      
        Database.UpdateParameter(dbCommand,"?LastName",customer.LastName);
      
        Database.UpdateParameter(dbCommand,"?Phone1",customer.Phone1);
      
        Database.UpdateParameter(dbCommand,"?Phone2",customer.Phone2);
      
        Database.UpdateParameter(dbCommand,"?Email",customer.Email);
      
        Database.UpdateParameter(dbCommand,"?Modified",customer.Modified);
      
        Database.UpdateParameter(dbCommand,"?LastSyncDate",customer.LastSyncDate);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        customer.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Customer>  customerList)
      {
        Insert(customerList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Customer Set "
      
        + " ServmanCustId = ?ServmanCustId, "
      
        + " CustomerTypeId = ?CustomerTypeId, "
      
        + " AddressId = ?AddressId, "
      
        + " FirstName = ?FirstName, "
      
        + " LastName = ?LastName, "
      
        + " Phone1 = ?Phone1, "
      
        + " Phone2 = ?Phone2, "
      
        + " Email = ?Email, "
      
        + " Modified = ?Modified, "
      
        + " LastSyncDate = ?LastSyncDate "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Customer customer, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", customer.ID);
      
        Database.PutParameter(dbCommand,"?ServmanCustId", customer.ServmanCustId);
      
        Database.PutParameter(dbCommand,"?CustomerTypeId", customer.CustomerTypeId);
      
        Database.PutParameter(dbCommand,"?AddressId", customer.AddressId);
      
        Database.PutParameter(dbCommand,"?FirstName", customer.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", customer.LastName);
      
        Database.PutParameter(dbCommand,"?Phone1", customer.Phone1);
      
        Database.PutParameter(dbCommand,"?Phone2", customer.Phone2);
      
        Database.PutParameter(dbCommand,"?Email", customer.Email);
      
        Database.PutParameter(dbCommand,"?Modified", customer.Modified);
      
        Database.PutParameter(dbCommand,"?LastSyncDate", customer.LastSyncDate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Customer customer)
      {
        Update(customer, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ServmanCustId, "
      
        + " CustomerTypeId, "
      
        + " AddressId, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Phone1, "
      
        + " Phone2, "
      
        + " Email, "
      
        + " Modified, "
      
        + " LastSyncDate "
      

      + " From Customer "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Customer FindByPrimaryKey(
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
      throw new DataNotFoundException("Customer not found, search by primary key");

      }

      public static Customer FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Customer customer, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",customer.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Customer customer)
      {
      return Exists(customer, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Customer limit 1";

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

      public static Customer Load(IDataReader dataReader, int offset)
      {
      Customer customer = new Customer();

      customer.ID = dataReader.GetInt32(0 + offset);
          customer.ServmanCustId = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            customer.CustomerTypeId = dataReader.GetByte(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            customer.AddressId = dataReader.GetInt32(3 + offset);
          customer.FirstName = dataReader.GetString(4 + offset);
          customer.LastName = dataReader.GetString(5 + offset);
          customer.Phone1 = dataReader.GetString(6 + offset);
          customer.Phone2 = dataReader.GetString(7 + offset);
          customer.Email = dataReader.GetString(8 + offset);
          customer.Modified = dataReader.GetDateTime(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            customer.LastSyncDate = dataReader.GetDateTime(10 + offset);
          

      return customer;
      }

      public static Customer Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Customer "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Customer customer, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", customer.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Customer customer)
      {
        Delete(customer, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Customer ";

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
      
        + " ServmanCustId, "
      
        + " CustomerTypeId, "
      
        + " AddressId, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Phone1, "
      
        + " Phone2, "
      
        + " Email, "
      
        + " Modified, "
      
        + " LastSyncDate "
      

      + " From Customer ";
      public static List<Customer> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Customer> rv = new List<Customer>();

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

      public static List<Customer> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Customer> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Customer obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && ServmanCustId == obj.ServmanCustId && CustomerTypeId == obj.CustomerTypeId && AddressId == obj.AddressId && FirstName == obj.FirstName && LastName == obj.LastName && Phone1 == obj.Phone1 && Phone2 == obj.Phone2 && Email == obj.Email && Modified == obj.Modified && LastSyncDate == obj.LastSyncDate;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Customer> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Customer));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Customer item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Customer>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Customer));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Customer> itemsList
      = new List<Customer>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Customer)
      itemsList.Add(deserializedObject as Customer);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_servmanCustId;
      
        protected byte? m_customerTypeId;
      
        protected int? m_addressId;
      
        protected String m_firstName;
      
        protected String m_lastName;
      
        protected String m_phone1;
      
        protected String m_phone2;
      
        protected String m_email;
      
        protected DateTime m_modified;
      
        protected DateTime? m_lastSyncDate;
      
      #endregion

      #region Constructors
      public Customer(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Customer(
        int 
          iD,String 
          servmanCustId,byte? 
          customerTypeId,int? 
          addressId,String 
          firstName,String 
          lastName,String 
          phone1,String 
          phone2,String 
          email,DateTime 
          modified,DateTime? 
          lastSyncDate
        ) : this()
        {
        
          m_iD = iD;
        
          m_servmanCustId = servmanCustId;
        
          m_customerTypeId = customerTypeId;
        
          m_addressId = addressId;
        
          m_firstName = firstName;
        
          m_lastName = lastName;
        
          m_phone1 = phone1;
        
          m_phone2 = phone2;
        
          m_email = email;
        
          m_modified = modified;
        
          m_lastSyncDate = lastSyncDate;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String ServmanCustId
        {
        get { return m_servmanCustId;}
        set { m_servmanCustId = value; }
        }
      
        [XmlElement]
        public byte? CustomerTypeId
        {
        get { return m_customerTypeId;}
        set { m_customerTypeId = value; }
        }
      
        [XmlElement]
        public int? AddressId
        {
        get { return m_addressId;}
        set { m_addressId = value; }
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
        public DateTime Modified
        {
        get { return m_modified;}
        set { m_modified = value; }
        }
      
        [XmlElement]
        public DateTime? LastSyncDate
        {
        get { return m_lastSyncDate;}
        set { m_lastSyncDate = value; }
        }
      

      public static int FieldsCount
      {
      get { return 11; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    