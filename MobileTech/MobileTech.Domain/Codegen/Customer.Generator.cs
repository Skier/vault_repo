
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class Customer
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Customer ( " +
      
        " CustomerId, " +
      
        " Name " +
      
      ") Values (" +
      
        " @CustomerId, " +
      
        " @Name " +
      
      ")";

      public static void Insert(Customer customer)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@CustomerId", customer.CustomerId);
      
        Database.PutParameter(dbCommand,"@Name", customer.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<Customer>  customerList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(Customer customer in  customerList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@CustomerId", customer.CustomerId);
      
        Database.PutParameter(dbCommand,"@Name", customer.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@CustomerId",customer.CustomerId);
      
        Database.UpdateParameter(dbCommand,"@Name",customer.Name);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update Customer Set "
      
        + " Name = @Name "
      
        + " Where "
        
          + " CustomerId = @CustomerId "
        
      ;

      public static void Update(Customer customer)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@CustomerId", customer.CustomerId);
      
        Database.PutParameter(dbCommand,"@Name", customer.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " CustomerId, "
      
        + " Name "
      

      + " From Customer "

      
        + " Where "
        
          + " CustomerId = @CustomerId "
        
      ;

      public static Customer FindByPrimaryKey(
      int customerId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CustomerId", customerId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Customer not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(Customer customer)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@CustomerId",customer.CustomerId);
      

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
      String sql = "select 1 from Customer";

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

      public static Customer Load(IDataReader dataReader)
      {
      Customer customer = new Customer();

      customer.CustomerId = dataReader.GetInt32(0);
          customer.Name = dataReader.GetString(1);
          

      return customer;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Customer "

      
        + " Where "
        
          + " CustomerId = @CustomerId "
        
      ;
      public static void Delete(Customer customer)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@CustomerId", customer.CustomerId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From Customer ";

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

      
        + " CustomerId, "
      
        + " Name "
      

      + " From Customer ";
      public static List<Customer> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
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
        
          protected int m_customerId;
        
          protected String m_name;
        
        #endregion
        
        #region Constructors
        public Customer(
        int 
          customerId
         )
        {
        
          m_customerId = customerId;
        
        }
        
        


        public Customer(
        int 
          customerId,String 
          name
        )
        {
        
          m_customerId = customerId;
        
          m_name = name;
        
          }


        
      #endregion

      
        [XmlElement]
        public int CustomerId
        {
          get { return m_customerId;}
          set { m_customerId = value; }
        }
      
        [XmlElement]
        public String Name
        {
          get { return m_name;}
          set { m_name = value; }
        }
      
      }
      #endregion
      }

    