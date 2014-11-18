
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Data;
    using Dalworth.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Domain
      {


      public partial class Customer : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Customer] ( " +
      
        " ID, " +
      
        " AddressId, " +
      
        " FirstName, " +
      
        " LastName, " +
      
        " Phone1, " +
      
        " Phone2 " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @AddressId, " +
      
        " @FirstName, " +
      
        " @LastName, " +
      
        " @Phone1, " +
      
        " @Phone2 " +
      
      ")";

      public static void Insert(Customer customer, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", customer.ID);
      
        Database.PutParameter(dbCommand,"@AddressId", customer.AddressId);
      
        Database.PutParameter(dbCommand,"@FirstName", customer.FirstName);
      
        Database.PutParameter(dbCommand,"@LastName", customer.LastName);
      
        Database.PutParameter(dbCommand,"@Phone1", customer.Phone1);
      
        Database.PutParameter(dbCommand,"@Phone2", customer.Phone2);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(Customer customer)
      {
        Insert(customer, null);
      }

      public static void Insert(List<Customer>  customerList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(Customer customer in  customerList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", customer.ID);
      
        Database.PutParameter(dbCommand,"@AddressId", customer.AddressId);
      
        Database.PutParameter(dbCommand,"@FirstName", customer.FirstName);
      
        Database.PutParameter(dbCommand,"@LastName", customer.LastName);
      
        Database.PutParameter(dbCommand,"@Phone1", customer.Phone1);
      
        Database.PutParameter(dbCommand,"@Phone2", customer.Phone2);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",customer.ID);
      
        Database.UpdateParameter(dbCommand,"@AddressId",customer.AddressId);
      
        Database.UpdateParameter(dbCommand,"@FirstName",customer.FirstName);
      
        Database.UpdateParameter(dbCommand,"@LastName",customer.LastName);
      
        Database.UpdateParameter(dbCommand,"@Phone1",customer.Phone1);
      
        Database.UpdateParameter(dbCommand,"@Phone2",customer.Phone2);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<Customer>  customerList)
      {
      Insert(customerList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Customer] Set "
      
        + " AddressId = @AddressId, "
      
        + " FirstName = @FirstName, "
      
        + " LastName = @LastName, "
      
        + " Phone1 = @Phone1, "
      
        + " Phone2 = @Phone2 "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(Customer customer, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", customer.ID);
      
        Database.PutParameter(dbCommand,"@AddressId", customer.AddressId);
      
        Database.PutParameter(dbCommand,"@FirstName", customer.FirstName);
      
        Database.PutParameter(dbCommand,"@LastName", customer.LastName);
      
        Database.PutParameter(dbCommand,"@Phone1", customer.Phone1);
      
        Database.PutParameter(dbCommand,"@Phone2", customer.Phone2);
      

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
      
        + " AddressId, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Phone1, "
      
        + " Phone2 "
      

      + " From [Customer] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static Customer FindByPrimaryKey(
      int iD, IDbTransaction transaction
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", iD);
      

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
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(Customer customer, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",customer.ID);
      

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

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from Customer";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, transaction))
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

      public static Customer Load(IDataReader dataReader)
      {
      Customer customer = new Customer();

      customer.ID = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            customer.AddressId = dataReader.GetInt32(1);
          
            if(!dataReader.IsDBNull(2))
            customer.FirstName = dataReader.GetString(2);
          
            if(!dataReader.IsDBNull(3))
            customer.LastName = dataReader.GetString(3);
          
            if(!dataReader.IsDBNull(4))
            customer.Phone1 = dataReader.GetString(4);
          
            if(!dataReader.IsDBNull(5))
            customer.Phone2 = dataReader.GetString(5);
          

      return customer;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Customer] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(Customer customer, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", customer.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Customer customer)
      {
      Delete(customer, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Customer] ";

      public static void Clear(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, transaction))
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
      
        + " AddressId, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Phone1, "
      
        + " Phone2 "
      

      + " From [Customer] ";
      public static List<Customer> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
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
      
        protected int? m_addressId;
      
        protected String m_firstName;
      
        protected String m_lastName;
      
        protected String m_phone1;
      
        protected String m_phone2;
      
      #endregion

      #region Constructors
      public Customer(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public Customer(
        int 
          iD,int? 
          addressId,String 
          firstName,String 
          lastName,String 
          phone1,String 
          phone2
        )
        {
        
          m_iD = iD;
        
          m_addressId = addressId;
        
          m_firstName = firstName;
        
          m_lastName = lastName;
        
          m_phone1 = phone1;
        
          m_phone2 = phone2;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
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
      
      }
      #endregion
      }

    