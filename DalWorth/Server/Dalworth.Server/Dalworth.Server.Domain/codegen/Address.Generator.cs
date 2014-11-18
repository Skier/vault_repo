
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


      public partial class Address : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Address ( " +
      
        " AreaId, " +
      
        " Block, " +
      
        " Prefix, " +
      
        " Street, " +
      
        " Suffix, " +
      
        " Unit, " +
      
        " Address2, " +
      
        " City, " +
      
        " State, " +
      
        " Zip, " +
      
        " MapPage, " +
      
        " MapLetter, " +
      
        " Modified " +
      
      ") Values (" +
      
        " ?AreaId, " +
      
        " ?Block, " +
      
        " ?Prefix, " +
      
        " ?Street, " +
      
        " ?Suffix, " +
      
        " ?Unit, " +
      
        " ?Address2, " +
      
        " ?City, " +
      
        " ?State, " +
      
        " ?Zip, " +
      
        " ?MapPage, " +
      
        " ?MapLetter, " +
      
        " ?Modified " +
      
      ")";

      public static void Insert(Address address, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?AreaId", address.AreaId);
      
        Database.PutParameter(dbCommand,"?Block", address.Block);
      
        Database.PutParameter(dbCommand,"?Prefix", address.Prefix);
      
        Database.PutParameter(dbCommand,"?Street", address.Street);
      
        Database.PutParameter(dbCommand,"?Suffix", address.Suffix);
      
        Database.PutParameter(dbCommand,"?Unit", address.Unit);
      
        Database.PutParameter(dbCommand,"?Address2", address.Address2);
      
        Database.PutParameter(dbCommand,"?City", address.City);
      
        Database.PutParameter(dbCommand,"?State", address.State);
      
        Database.PutParameter(dbCommand,"?Zip", address.Zip);
      
        Database.PutParameter(dbCommand,"?MapPage", address.MapPage);
      
        Database.PutParameter(dbCommand,"?MapLetter", address.MapLetter);
      
        Database.PutParameter(dbCommand,"?Modified", address.Modified);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        address.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Address address)
      {
        Insert(address, null);
      }


      public static void Insert(List<Address>  addressList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Address address in  addressList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?AreaId", address.AreaId);
      
        Database.PutParameter(dbCommand,"?Block", address.Block);
      
        Database.PutParameter(dbCommand,"?Prefix", address.Prefix);
      
        Database.PutParameter(dbCommand,"?Street", address.Street);
      
        Database.PutParameter(dbCommand,"?Suffix", address.Suffix);
      
        Database.PutParameter(dbCommand,"?Unit", address.Unit);
      
        Database.PutParameter(dbCommand,"?Address2", address.Address2);
      
        Database.PutParameter(dbCommand,"?City", address.City);
      
        Database.PutParameter(dbCommand,"?State", address.State);
      
        Database.PutParameter(dbCommand,"?Zip", address.Zip);
      
        Database.PutParameter(dbCommand,"?MapPage", address.MapPage);
      
        Database.PutParameter(dbCommand,"?MapLetter", address.MapLetter);
      
        Database.PutParameter(dbCommand,"?Modified", address.Modified);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?AreaId",address.AreaId);
      
        Database.UpdateParameter(dbCommand,"?Block",address.Block);
      
        Database.UpdateParameter(dbCommand,"?Prefix",address.Prefix);
      
        Database.UpdateParameter(dbCommand,"?Street",address.Street);
      
        Database.UpdateParameter(dbCommand,"?Suffix",address.Suffix);
      
        Database.UpdateParameter(dbCommand,"?Unit",address.Unit);
      
        Database.UpdateParameter(dbCommand,"?Address2",address.Address2);
      
        Database.UpdateParameter(dbCommand,"?City",address.City);
      
        Database.UpdateParameter(dbCommand,"?State",address.State);
      
        Database.UpdateParameter(dbCommand,"?Zip",address.Zip);
      
        Database.UpdateParameter(dbCommand,"?MapPage",address.MapPage);
      
        Database.UpdateParameter(dbCommand,"?MapLetter",address.MapLetter);
      
        Database.UpdateParameter(dbCommand,"?Modified",address.Modified);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        address.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Address>  addressList)
      {
        Insert(addressList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Address Set "
      
        + " AreaId = ?AreaId, "
      
        + " Block = ?Block, "
      
        + " Prefix = ?Prefix, "
      
        + " Street = ?Street, "
      
        + " Suffix = ?Suffix, "
      
        + " Unit = ?Unit, "
      
        + " Address2 = ?Address2, "
      
        + " City = ?City, "
      
        + " State = ?State, "
      
        + " Zip = ?Zip, "
      
        + " MapPage = ?MapPage, "
      
        + " MapLetter = ?MapLetter, "
      
        + " Modified = ?Modified "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Address address, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", address.ID);
      
        Database.PutParameter(dbCommand,"?AreaId", address.AreaId);
      
        Database.PutParameter(dbCommand,"?Block", address.Block);
      
        Database.PutParameter(dbCommand,"?Prefix", address.Prefix);
      
        Database.PutParameter(dbCommand,"?Street", address.Street);
      
        Database.PutParameter(dbCommand,"?Suffix", address.Suffix);
      
        Database.PutParameter(dbCommand,"?Unit", address.Unit);
      
        Database.PutParameter(dbCommand,"?Address2", address.Address2);
      
        Database.PutParameter(dbCommand,"?City", address.City);
      
        Database.PutParameter(dbCommand,"?State", address.State);
      
        Database.PutParameter(dbCommand,"?Zip", address.Zip);
      
        Database.PutParameter(dbCommand,"?MapPage", address.MapPage);
      
        Database.PutParameter(dbCommand,"?MapLetter", address.MapLetter);
      
        Database.PutParameter(dbCommand,"?Modified", address.Modified);
      

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
      
        + " AreaId, "
      
        + " Block, "
      
        + " Prefix, "
      
        + " Street, "
      
        + " Suffix, "
      
        + " Unit, "
      
        + " Address2, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " MapPage, "
      
        + " MapLetter, "
      
        + " Modified "
      

      + " From Address "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Address FindByPrimaryKey(
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
      throw new DataNotFoundException("Address not found, search by primary key");

      }

      public static Address FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Address address, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",address.ID);
      

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

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Address limit 1";

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

      public static Address Load(IDataReader dataReader, int offset)
      {
      Address address = new Address();

      address.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            address.AreaId = dataReader.GetByte(1 + offset);
          address.Block = dataReader.GetString(2 + offset);
          address.Prefix = dataReader.GetString(3 + offset);
          address.Street = dataReader.GetString(4 + offset);
          address.Suffix = dataReader.GetString(5 + offset);
          address.Unit = dataReader.GetString(6 + offset);
          address.Address2 = dataReader.GetString(7 + offset);
          address.City = dataReader.GetString(8 + offset);
          address.State = dataReader.GetString(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            address.Zip = dataReader.GetInt32(10 + offset);
          address.MapPage = dataReader.GetString(11 + offset);
          address.MapLetter = dataReader.GetString(12 + offset);
          address.Modified = dataReader.GetDateTime(13 + offset);
          

      return address;
      }

      public static Address Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Address "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Address address, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", address.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Address address)
      {
        Delete(address, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Address ";

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
      
        + " AreaId, "
      
        + " Block, "
      
        + " Prefix, "
      
        + " Street, "
      
        + " Suffix, "
      
        + " Unit, "
      
        + " Address2, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " MapPage, "
      
        + " MapLetter, "
      
        + " Modified "
      

      + " From Address ";
      public static List<Address> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
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

      #region ValueEquals

      public bool ValueEquals (Address obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && AreaId == obj.AreaId && Block == obj.Block && Prefix == obj.Prefix && Street == obj.Street && Suffix == obj.Suffix && Unit == obj.Unit && Address2 == obj.Address2 && City == obj.City && State == obj.State && Zip == obj.Zip && MapPage == obj.MapPage && MapLetter == obj.MapLetter && Modified == obj.Modified;
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
      
        protected byte? m_areaId;
      
        protected String m_block;
      
        protected String m_prefix;
      
        protected String m_street;
      
        protected String m_suffix;
      
        protected String m_unit;
      
        protected String m_address2;
      
        protected String m_city;
      
        protected String m_state;
      
        protected int? m_zip;
      
        protected String m_mapPage;
      
        protected String m_mapLetter;
      
        protected DateTime m_modified;
      
      #endregion

      #region Constructors
      public Address(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Address(
        int 
          iD,byte? 
          areaId,String 
          block,String 
          prefix,String 
          street,String 
          suffix,String 
          unit,String 
          address2,String 
          city,String 
          state,int? 
          zip,String 
          mapPage,String 
          mapLetter,DateTime 
          modified
        ) : this()
        {
        
          m_iD = iD;
        
          m_areaId = areaId;
        
          m_block = block;
        
          m_prefix = prefix;
        
          m_street = street;
        
          m_suffix = suffix;
        
          m_unit = unit;
        
          m_address2 = address2;
        
          m_city = city;
        
          m_state = state;
        
          m_zip = zip;
        
          m_mapPage = mapPage;
        
          m_mapLetter = mapLetter;
        
          m_modified = modified;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public byte? AreaId
        {
        get { return m_areaId;}
        set { m_areaId = value; }
        }
      
        [XmlElement]
        public String Block
        {
        get { return m_block;}
        set { m_block = value; }
        }
      
        [XmlElement]
        public String Prefix
        {
        get { return m_prefix;}
        set { m_prefix = value; }
        }
      
        [XmlElement]
        public String Street
        {
        get { return m_street;}
        set { m_street = value; }
        }
      
        [XmlElement]
        public String Suffix
        {
        get { return m_suffix;}
        set { m_suffix = value; }
        }
      
        [XmlElement]
        public String Unit
        {
        get { return m_unit;}
        set { m_unit = value; }
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
        public String MapPage
        {
        get { return m_mapPage;}
        set { m_mapPage = value; }
        }
      
        [XmlElement]
        public String MapLetter
        {
        get { return m_mapLetter;}
        set { m_mapLetter = value; }
        }
      
        [XmlElement]
        public DateTime Modified
        {
        get { return m_modified;}
        set { m_modified = value; }
        }
      

      public static int FieldsCount
      {
      get { return 14; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    