
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


      public partial class Address : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Address] ( " +
      
        " ID, " +
      
        " Address1, " +
      
        " Address2, " +
      
        " City, " +
      
        " State, " +
      
        " Zip, " +
      
        " Map " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @Address1, " +
      
        " @Address2, " +
      
        " @City, " +
      
        " @State, " +
      
        " @Zip, " +
      
        " @Map " +
      
      ")";

      public static void Insert(Address address, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", address.ID);
      
        Database.PutParameter(dbCommand,"@Address1", address.Address1);
      
        Database.PutParameter(dbCommand,"@Address2", address.Address2);
      
        Database.PutParameter(dbCommand,"@City", address.City);
      
        Database.PutParameter(dbCommand,"@State", address.State);
      
        Database.PutParameter(dbCommand,"@Zip", address.Zip);
      
        Database.PutParameter(dbCommand,"@Map", address.Map);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(Address address)
      {
        Insert(address, null);
      }

      public static void Insert(List<Address>  addressList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(Address address in  addressList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", address.ID);
      
        Database.PutParameter(dbCommand,"@Address1", address.Address1);
      
        Database.PutParameter(dbCommand,"@Address2", address.Address2);
      
        Database.PutParameter(dbCommand,"@City", address.City);
      
        Database.PutParameter(dbCommand,"@State", address.State);
      
        Database.PutParameter(dbCommand,"@Zip", address.Zip);
      
        Database.PutParameter(dbCommand,"@Map", address.Map);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",address.ID);
      
        Database.UpdateParameter(dbCommand,"@Address1",address.Address1);
      
        Database.UpdateParameter(dbCommand,"@Address2",address.Address2);
      
        Database.UpdateParameter(dbCommand,"@City",address.City);
      
        Database.UpdateParameter(dbCommand,"@State",address.State);
      
        Database.UpdateParameter(dbCommand,"@Zip",address.Zip);
      
        Database.UpdateParameter(dbCommand,"@Map",address.Map);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<Address>  addressList)
      {
      Insert(addressList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Address] Set "
      
        + " Address1 = @Address1, "
      
        + " Address2 = @Address2, "
      
        + " City = @City, "
      
        + " State = @State, "
      
        + " Zip = @Zip, "
      
        + " Map = @Map "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(Address address, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", address.ID);
      
        Database.PutParameter(dbCommand,"@Address1", address.Address1);
      
        Database.PutParameter(dbCommand,"@Address2", address.Address2);
      
        Database.PutParameter(dbCommand,"@City", address.City);
      
        Database.PutParameter(dbCommand,"@State", address.State);
      
        Database.PutParameter(dbCommand,"@Zip", address.Zip);
      
        Database.PutParameter(dbCommand,"@Map", address.Map);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Address address)
      {
      Update(address, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Address1, "
      
        + " Address2, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " Map "
      

      + " From [Address] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static Address FindByPrimaryKey(
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
      throw new DataNotFoundException("Address not found, search by primary key");

      }

      public static Address FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(Address address, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",address.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Address address)
      {
      return Exists(address, null);
      }
      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from Address";

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

      public static Address Load(IDataReader dataReader)
      {
      Address address = new Address();

      address.ID = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            address.Address1 = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            address.Address2 = dataReader.GetString(2);
          
            if(!dataReader.IsDBNull(3))
            address.City = dataReader.GetString(3);
          
            if(!dataReader.IsDBNull(4))
            address.State = dataReader.GetString(4);
          
            if(!dataReader.IsDBNull(5))
            address.Zip = dataReader.GetString(5);
          
            if(!dataReader.IsDBNull(6))
            address.Map = dataReader.GetString(6);
          

      return address;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Address] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(Address address, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", address.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Address address)
      {
      Delete(address, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Address] ";

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
      
        + " Address1, "
      
        + " Address2, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " Map "
      

      + " From [Address] ";
      public static List<Address> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
      {
      List<Address> rv = new List<Address>();

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

      public static List<Address> Find()
      {
        return Find(null);
      }

      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Address> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<Address> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Address));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Address item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Address>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Address));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Address> itemsList
      = new List<Address>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Address)
      itemsList.Add(deserializedObject as Address);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_address1;
      
        protected String m_address2;
      
        protected String m_city;
      
        protected String m_state;
      
        protected String m_zip;
      
        protected String m_map;
      
      #endregion

      #region Constructors
      public Address(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public Address(
        int 
          iD,String 
          address1,String 
          address2,String 
          city,String 
          state,String 
          zip,String 
          map
        )
        {
        
          m_iD = iD;
        
          m_address1 = address1;
        
          m_address2 = address2;
        
          m_city = city;
        
          m_state = state;
        
          m_zip = zip;
        
          m_map = map;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
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
        public String Zip
        {
        get { return m_zip;}
        set { m_zip = value; }
        }
      
        [XmlElement]
        public String Map
        {
        get { return m_map;}
        set { m_map = value; }
        }
      
      }
      #endregion
      }

    