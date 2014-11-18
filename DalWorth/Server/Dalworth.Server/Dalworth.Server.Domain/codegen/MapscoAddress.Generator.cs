
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


      public partial class MapscoAddress : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into MapscoAddress ( " +
      
        " ID, " +
      
        " Prefix, " +
      
        " Street, " +
      
        " Suffix, " +
      
        " City, " +
      
        " State, " +
      
        " Zip, " +
      
        " BlockBegin, " +
      
        " BlockEnd, " +
      
        " MapPage, " +
      
        " MapLetter " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Prefix, " +
      
        " ?Street, " +
      
        " ?Suffix, " +
      
        " ?City, " +
      
        " ?State, " +
      
        " ?Zip, " +
      
        " ?BlockBegin, " +
      
        " ?BlockEnd, " +
      
        " ?MapPage, " +
      
        " ?MapLetter " +
      
      ")";

      public static void Insert(MapscoAddress mapscoAddress, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", mapscoAddress.ID);
      
        Database.PutParameter(dbCommand,"?Prefix", mapscoAddress.Prefix);
      
        Database.PutParameter(dbCommand,"?Street", mapscoAddress.Street);
      
        Database.PutParameter(dbCommand,"?Suffix", mapscoAddress.Suffix);
      
        Database.PutParameter(dbCommand,"?City", mapscoAddress.City);
      
        Database.PutParameter(dbCommand,"?State", mapscoAddress.State);
      
        Database.PutParameter(dbCommand,"?Zip", mapscoAddress.Zip);
      
        Database.PutParameter(dbCommand,"?BlockBegin", mapscoAddress.BlockBegin);
      
        Database.PutParameter(dbCommand,"?BlockEnd", mapscoAddress.BlockEnd);
      
        Database.PutParameter(dbCommand,"?MapPage", mapscoAddress.MapPage);
      
        Database.PutParameter(dbCommand,"?MapLetter", mapscoAddress.MapLetter);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(MapscoAddress mapscoAddress)
      {
        Insert(mapscoAddress, null);
      }


      public static void Insert(List<MapscoAddress>  mapscoAddressList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(MapscoAddress mapscoAddress in  mapscoAddressList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", mapscoAddress.ID);
      
        Database.PutParameter(dbCommand,"?Prefix", mapscoAddress.Prefix);
      
        Database.PutParameter(dbCommand,"?Street", mapscoAddress.Street);
      
        Database.PutParameter(dbCommand,"?Suffix", mapscoAddress.Suffix);
      
        Database.PutParameter(dbCommand,"?City", mapscoAddress.City);
      
        Database.PutParameter(dbCommand,"?State", mapscoAddress.State);
      
        Database.PutParameter(dbCommand,"?Zip", mapscoAddress.Zip);
      
        Database.PutParameter(dbCommand,"?BlockBegin", mapscoAddress.BlockBegin);
      
        Database.PutParameter(dbCommand,"?BlockEnd", mapscoAddress.BlockEnd);
      
        Database.PutParameter(dbCommand,"?MapPage", mapscoAddress.MapPage);
      
        Database.PutParameter(dbCommand,"?MapLetter", mapscoAddress.MapLetter);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",mapscoAddress.ID);
      
        Database.UpdateParameter(dbCommand,"?Prefix",mapscoAddress.Prefix);
      
        Database.UpdateParameter(dbCommand,"?Street",mapscoAddress.Street);
      
        Database.UpdateParameter(dbCommand,"?Suffix",mapscoAddress.Suffix);
      
        Database.UpdateParameter(dbCommand,"?City",mapscoAddress.City);
      
        Database.UpdateParameter(dbCommand,"?State",mapscoAddress.State);
      
        Database.UpdateParameter(dbCommand,"?Zip",mapscoAddress.Zip);
      
        Database.UpdateParameter(dbCommand,"?BlockBegin",mapscoAddress.BlockBegin);
      
        Database.UpdateParameter(dbCommand,"?BlockEnd",mapscoAddress.BlockEnd);
      
        Database.UpdateParameter(dbCommand,"?MapPage",mapscoAddress.MapPage);
      
        Database.UpdateParameter(dbCommand,"?MapLetter",mapscoAddress.MapLetter);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<MapscoAddress>  mapscoAddressList)
      {
        Insert(mapscoAddressList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update MapscoAddress Set "
      
        + " Prefix = ?Prefix, "
      
        + " Street = ?Street, "
      
        + " Suffix = ?Suffix, "
      
        + " City = ?City, "
      
        + " State = ?State, "
      
        + " Zip = ?Zip, "
      
        + " BlockBegin = ?BlockBegin, "
      
        + " BlockEnd = ?BlockEnd, "
      
        + " MapPage = ?MapPage, "
      
        + " MapLetter = ?MapLetter "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(MapscoAddress mapscoAddress, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", mapscoAddress.ID);
      
        Database.PutParameter(dbCommand,"?Prefix", mapscoAddress.Prefix);
      
        Database.PutParameter(dbCommand,"?Street", mapscoAddress.Street);
      
        Database.PutParameter(dbCommand,"?Suffix", mapscoAddress.Suffix);
      
        Database.PutParameter(dbCommand,"?City", mapscoAddress.City);
      
        Database.PutParameter(dbCommand,"?State", mapscoAddress.State);
      
        Database.PutParameter(dbCommand,"?Zip", mapscoAddress.Zip);
      
        Database.PutParameter(dbCommand,"?BlockBegin", mapscoAddress.BlockBegin);
      
        Database.PutParameter(dbCommand,"?BlockEnd", mapscoAddress.BlockEnd);
      
        Database.PutParameter(dbCommand,"?MapPage", mapscoAddress.MapPage);
      
        Database.PutParameter(dbCommand,"?MapLetter", mapscoAddress.MapLetter);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(MapscoAddress mapscoAddress)
      {
        Update(mapscoAddress, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Prefix, "
      
        + " Street, "
      
        + " Suffix, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " BlockBegin, "
      
        + " BlockEnd, "
      
        + " MapPage, "
      
        + " MapLetter "
      

      + " From MapscoAddress "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static MapscoAddress FindByPrimaryKey(
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
      throw new DataNotFoundException("MapscoAddress not found, search by primary key");

      }

      public static MapscoAddress FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(MapscoAddress mapscoAddress, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",mapscoAddress.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(MapscoAddress mapscoAddress)
      {
      return Exists(mapscoAddress, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from MapscoAddress limit 1";

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

      public static MapscoAddress Load(IDataReader dataReader, int offset)
      {
      MapscoAddress mapscoAddress = new MapscoAddress();

      mapscoAddress.ID = dataReader.GetInt32(0 + offset);
          mapscoAddress.Prefix = dataReader.GetString(1 + offset);
          mapscoAddress.Street = dataReader.GetString(2 + offset);
          mapscoAddress.Suffix = dataReader.GetString(3 + offset);
          mapscoAddress.City = dataReader.GetString(4 + offset);
          mapscoAddress.State = dataReader.GetString(5 + offset);
          mapscoAddress.Zip = dataReader.GetString(6 + offset);
          mapscoAddress.BlockBegin = dataReader.GetInt32(7 + offset);
          mapscoAddress.BlockEnd = dataReader.GetInt32(8 + offset);
          mapscoAddress.MapPage = dataReader.GetString(9 + offset);
          mapscoAddress.MapLetter = dataReader.GetString(10 + offset);
          

      return mapscoAddress;
      }

      public static MapscoAddress Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From MapscoAddress "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(MapscoAddress mapscoAddress, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", mapscoAddress.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(MapscoAddress mapscoAddress)
      {
        Delete(mapscoAddress, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From MapscoAddress ";

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
      
        + " Prefix, "
      
        + " Street, "
      
        + " Suffix, "
      
        + " City, "
      
        + " State, "
      
        + " Zip, "
      
        + " BlockBegin, "
      
        + " BlockEnd, "
      
        + " MapPage, "
      
        + " MapLetter "
      

      + " From MapscoAddress ";
      public static List<MapscoAddress> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<MapscoAddress> rv = new List<MapscoAddress>();

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

      public static List<MapscoAddress> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<MapscoAddress> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (MapscoAddress obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Prefix == obj.Prefix && Street == obj.Street && Suffix == obj.Suffix && City == obj.City && State == obj.State && Zip == obj.Zip && BlockBegin == obj.BlockBegin && BlockEnd == obj.BlockEnd && MapPage == obj.MapPage && MapLetter == obj.MapLetter;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<MapscoAddress> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(MapscoAddress));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(MapscoAddress item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<MapscoAddress>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(MapscoAddress));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<MapscoAddress> itemsList
      = new List<MapscoAddress>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is MapscoAddress)
      itemsList.Add(deserializedObject as MapscoAddress);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_prefix;
      
        protected String m_street;
      
        protected String m_suffix;
      
        protected String m_city;
      
        protected String m_state;
      
        protected String m_zip;
      
        protected int m_blockBegin;
      
        protected int m_blockEnd;
      
        protected String m_mapPage;
      
        protected String m_mapLetter;
      
      #endregion

      #region Constructors
      public MapscoAddress(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public MapscoAddress(
        int 
          iD,String 
          prefix,String 
          street,String 
          suffix,String 
          city,String 
          state,String 
          zip,int 
          blockBegin,int 
          blockEnd,String 
          mapPage,String 
          mapLetter
        ) : this()
        {
        
          m_iD = iD;
        
          m_prefix = prefix;
        
          m_street = street;
        
          m_suffix = suffix;
        
          m_city = city;
        
          m_state = state;
        
          m_zip = zip;
        
          m_blockBegin = blockBegin;
        
          m_blockEnd = blockEnd;
        
          m_mapPage = mapPage;
        
          m_mapLetter = mapLetter;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
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
        public int BlockBegin
        {
        get { return m_blockBegin;}
        set { m_blockBegin = value; }
        }
      
        [XmlElement]
        public int BlockEnd
        {
        get { return m_blockEnd;}
        set { m_blockEnd = value; }
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

    