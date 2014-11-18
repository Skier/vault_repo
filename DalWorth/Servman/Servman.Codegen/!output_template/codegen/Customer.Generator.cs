
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Servman.Data;
    using Servman.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Servman.Domain
      {

      public partial class Customer : ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Customer ( " +
      
        " BusinessPartnerId, " +
      
        " QbCustomerId " +
      
      ") Values (" +
      
        " ?BusinessPartnerId, " +
      
        " ?QbCustomerId " +
      
      ")";

      public static void Insert(Customer customer, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", customer.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", customer.QbCustomerId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        customer.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
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
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", customer.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", customer.QbCustomerId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?BusinessPartnerId",customer.BusinessPartnerId);
      
        Database.UpdateParameter(dbCommand,"?QbCustomerId",customer.QbCustomerId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        customer.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
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
      
        + " BusinessPartnerId = ?BusinessPartnerId, "
      
        + " QbCustomerId = ?QbCustomerId "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(Customer customer, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", customer.Id);
      
        Database.PutParameter(dbCommand,"?BusinessPartnerId", customer.BusinessPartnerId);
      
        Database.PutParameter(dbCommand,"?QbCustomerId", customer.QbCustomerId);
      

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

      
        + " Id, "
      
        + " BusinessPartnerId, "
      
        + " QbCustomerId "
      

      + " From Customer "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static Customer FindByPrimaryKey(
      int id, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Customer not found, search by primary key");

      }

      public static Customer FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Customer customer, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",customer.Id);
      

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

      customer.Id = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            customer.BusinessPartnerId = dataReader.GetInt32(1 + offset);
          customer.QbCustomerId = dataReader.GetString(2 + offset);
          

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
        
          + " Id = ?Id "
        
      ;
      public static void Delete(Customer customer, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", customer.Id);
      


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

      
        + " Id, "
      
        + " BusinessPartnerId, "
      
        + " QbCustomerId "
      

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
      
        protected int m_id;
      
        protected int? m_businessPartnerId;
      
        protected String m_qbCustomerId;
      
      #endregion

      #region Constructors
      public Customer(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public Customer(
        int 
          id,int? 
          businessPartnerId,String 
          qbCustomerId
        ) : this()
        {
        
          m_id = id;
        
          m_businessPartnerId = businessPartnerId;
        
          m_qbCustomerId = qbCustomerId;
        
        }


      
      #endregion

      
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        public int? BusinessPartnerId
        {
        get { return m_businessPartnerId;}
        set { m_businessPartnerId = value; }
        }
      
        public String QbCustomerId
        {
        get { return m_qbCustomerId;}
        set { m_qbCustomerId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 3; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    