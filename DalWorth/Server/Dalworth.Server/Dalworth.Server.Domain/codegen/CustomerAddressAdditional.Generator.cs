
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


      public partial class CustomerAddressAdditional : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into CustomerAddressAdditional ( " +
      
        " CustomerId, " +
      
        " AddressId " +
      
      ") Values (" +
      
        " ?CustomerId, " +
      
        " ?AddressId " +
      
      ")";

      public static void Insert(CustomerAddressAdditional customerAddressAdditional, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?CustomerId", customerAddressAdditional.CustomerId);
      
        Database.PutParameter(dbCommand,"?AddressId", customerAddressAdditional.AddressId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(CustomerAddressAdditional customerAddressAdditional)
      {
        Insert(customerAddressAdditional, null);
      }


      public static void Insert(List<CustomerAddressAdditional>  customerAddressAdditionalList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(CustomerAddressAdditional customerAddressAdditional in  customerAddressAdditionalList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?CustomerId", customerAddressAdditional.CustomerId);
      
        Database.PutParameter(dbCommand,"?AddressId", customerAddressAdditional.AddressId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?CustomerId",customerAddressAdditional.CustomerId);
      
        Database.UpdateParameter(dbCommand,"?AddressId",customerAddressAdditional.AddressId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<CustomerAddressAdditional>  customerAddressAdditionalList)
      {
        Insert(customerAddressAdditionalList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update CustomerAddressAdditional Set "
      
        + " Where "
        
          + " CustomerId = ?CustomerId and  "
        
          + " AddressId = ?AddressId "
        
      ;

      public static void Update(CustomerAddressAdditional customerAddressAdditional, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?CustomerId", customerAddressAdditional.CustomerId);
      
        Database.PutParameter(dbCommand,"?AddressId", customerAddressAdditional.AddressId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(CustomerAddressAdditional customerAddressAdditional)
      {
        Update(customerAddressAdditional, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " CustomerId, "
      
        + " AddressId "
      

      + " From CustomerAddressAdditional "

      
        + " Where "
        
          + " CustomerId = ?CustomerId and  "
        
          + " AddressId = ?AddressId "
        
      ;

      public static CustomerAddressAdditional FindByPrimaryKey(
      int customerId,int addressId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?CustomerId", customerId);
      
        Database.PutParameter(dbCommand,"?AddressId", addressId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("CustomerAddressAdditional not found, search by primary key");

      }

      public static CustomerAddressAdditional FindByPrimaryKey(
      int customerId,int addressId
      )
      {
      return FindByPrimaryKey(
      customerId,addressId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(CustomerAddressAdditional customerAddressAdditional, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?CustomerId",customerAddressAdditional.CustomerId);
      
        Database.PutParameter(dbCommand,"?AddressId",customerAddressAdditional.AddressId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(CustomerAddressAdditional customerAddressAdditional)
      {
      return Exists(customerAddressAdditional, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from CustomerAddressAdditional limit 1";

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

      public static CustomerAddressAdditional Load(IDataReader dataReader, int offset)
      {
      CustomerAddressAdditional customerAddressAdditional = new CustomerAddressAdditional();

      customerAddressAdditional.CustomerId = dataReader.GetInt32(0 + offset);
          customerAddressAdditional.AddressId = dataReader.GetInt32(1 + offset);
          

      return customerAddressAdditional;
      }

      public static CustomerAddressAdditional Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From CustomerAddressAdditional "

      
        + " Where "
        
          + " CustomerId = ?CustomerId and  "
        
          + " AddressId = ?AddressId "
        
      ;
      public static void Delete(CustomerAddressAdditional customerAddressAdditional, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?CustomerId", customerAddressAdditional.CustomerId);
      
        Database.PutParameter(dbCommand,"?AddressId", customerAddressAdditional.AddressId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(CustomerAddressAdditional customerAddressAdditional)
      {
        Delete(customerAddressAdditional, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From CustomerAddressAdditional ";

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

      
        + " CustomerId, "
      
        + " AddressId "
      

      + " From CustomerAddressAdditional ";
      public static List<CustomerAddressAdditional> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<CustomerAddressAdditional> rv = new List<CustomerAddressAdditional>();

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

      public static List<CustomerAddressAdditional> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<CustomerAddressAdditional> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (CustomerAddressAdditional obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return CustomerId == obj.CustomerId && AddressId == obj.AddressId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<CustomerAddressAdditional> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerAddressAdditional));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(CustomerAddressAdditional item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<CustomerAddressAdditional>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(CustomerAddressAdditional));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<CustomerAddressAdditional> itemsList
      = new List<CustomerAddressAdditional>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is CustomerAddressAdditional)
      itemsList.Add(deserializedObject as CustomerAddressAdditional);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_customerId;
      
        protected int m_addressId;
      
      #endregion

      #region Constructors
      public CustomerAddressAdditional(
      int 
          customerId,int 
          addressId
      ) : this()
      {
      
        m_customerId = customerId;
      
        m_addressId = addressId;
      
      }

      
      #endregion

      
        [XmlElement]
        public int CustomerId
        {
        get { return m_customerId;}
        set { m_customerId = value; }
        }
      
        [XmlElement]
        public int AddressId
        {
        get { return m_addressId;}
        set { m_addressId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 2; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    