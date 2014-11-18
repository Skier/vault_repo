
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Servman.Domain
      {


      public partial class m_alt_ad
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into m_alt_ad ( " +
      
        " ticket_num, " +
      
        " block, " +
      
        " prefix, " +
      
        " street, " +
      
        " suffix, " +
      
        " unit, " +
      
        " address2, " +
      
        " city, " +
      
        " state, " +
      
        " zip, " +
      
        " grid, " +
      
        " area_id " +
      
      ") Values (" +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(m_alt_ad m_alt_ad)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", m_alt_ad.ticket_num);
      
        Database.PutParameter(dbCommand,"@block", m_alt_ad.block);
      
        Database.PutParameter(dbCommand,"@prefix", m_alt_ad.prefix);
      
        Database.PutParameter(dbCommand,"@street", m_alt_ad.street);
      
        Database.PutParameter(dbCommand,"@suffix", m_alt_ad.suffix);
      
        Database.PutParameter(dbCommand,"@unit", m_alt_ad.unit);
      
        Database.PutParameter(dbCommand,"@address2", m_alt_ad.address2);
      
        Database.PutParameter(dbCommand,"@city", m_alt_ad.city);
      
        Database.PutParameter(dbCommand,"@state", m_alt_ad.state);
      
        Database.PutParameter(dbCommand,"@zip", m_alt_ad.zip);
      
        Database.PutParameter(dbCommand,"@grid", m_alt_ad.grid);
      
        Database.PutParameter(dbCommand,"@area_id", m_alt_ad.area_id);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<m_alt_ad>  m_alt_adList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(m_alt_ad m_alt_ad in  m_alt_adList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", m_alt_ad.ticket_num);
      
        Database.PutParameter(dbCommand,"@block", m_alt_ad.block);
      
        Database.PutParameter(dbCommand,"@prefix", m_alt_ad.prefix);
      
        Database.PutParameter(dbCommand,"@street", m_alt_ad.street);
      
        Database.PutParameter(dbCommand,"@suffix", m_alt_ad.suffix);
      
        Database.PutParameter(dbCommand,"@unit", m_alt_ad.unit);
      
        Database.PutParameter(dbCommand,"@address2", m_alt_ad.address2);
      
        Database.PutParameter(dbCommand,"@city", m_alt_ad.city);
      
        Database.PutParameter(dbCommand,"@state", m_alt_ad.state);
      
        Database.PutParameter(dbCommand,"@zip", m_alt_ad.zip);
      
        Database.PutParameter(dbCommand,"@grid", m_alt_ad.grid);
      
        Database.PutParameter(dbCommand,"@area_id", m_alt_ad.area_id);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ticket_num",m_alt_ad.ticket_num);
      
        Database.UpdateParameter(dbCommand,"@block",m_alt_ad.block);
      
        Database.UpdateParameter(dbCommand,"@prefix",m_alt_ad.prefix);
      
        Database.UpdateParameter(dbCommand,"@street",m_alt_ad.street);
      
        Database.UpdateParameter(dbCommand,"@suffix",m_alt_ad.suffix);
      
        Database.UpdateParameter(dbCommand,"@unit",m_alt_ad.unit);
      
        Database.UpdateParameter(dbCommand,"@address2",m_alt_ad.address2);
      
        Database.UpdateParameter(dbCommand,"@city",m_alt_ad.city);
      
        Database.UpdateParameter(dbCommand,"@state",m_alt_ad.state);
      
        Database.UpdateParameter(dbCommand,"@zip",m_alt_ad.zip);
      
        Database.UpdateParameter(dbCommand,"@grid",m_alt_ad.grid);
      
        Database.UpdateParameter(dbCommand,"@area_id",m_alt_ad.area_id);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update m_alt_ad Set "
      
        + " m_alt_ad.block = ? , "
      
        + " m_alt_ad.prefix = ? , "
      
        + " m_alt_ad.street = ? , "
      
        + " m_alt_ad.suffix = ? , "
      
        + " m_alt_ad.unit = ? , "
      
        + " m_alt_ad.address2 = ? , "
      
        + " m_alt_ad.city = ? , "
      
        + " m_alt_ad.state = ? , "
      
        + " m_alt_ad.zip = ? , "
      
        + " m_alt_ad.grid = ? , "
      
        + " m_alt_ad.area_id = ?  "
      
        + " Where "
        
          + " m_alt_ad.ticket_num = ?  "
        
      ;

      public static void Update(m_alt_ad m_alt_ad)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@block", m_alt_ad.block);
      
        Database.PutParameter(dbCommand,"@prefix", m_alt_ad.prefix);
      
        Database.PutParameter(dbCommand,"@street", m_alt_ad.street);
      
        Database.PutParameter(dbCommand,"@suffix", m_alt_ad.suffix);
      
        Database.PutParameter(dbCommand,"@unit", m_alt_ad.unit);
      
        Database.PutParameter(dbCommand,"@address2", m_alt_ad.address2);
      
        Database.PutParameter(dbCommand,"@city", m_alt_ad.city);
      
        Database.PutParameter(dbCommand,"@state", m_alt_ad.state);
      
        Database.PutParameter(dbCommand,"@zip", m_alt_ad.zip);
      
        Database.PutParameter(dbCommand,"@grid", m_alt_ad.grid);
      
        Database.PutParameter(dbCommand,"@area_id", m_alt_ad.area_id);
      
        Database.PutParameter(dbCommand,"@ticket_num", m_alt_ad.ticket_num);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " m_alt_ad.ticket_num, "
      
        + " m_alt_ad.block, "
      
        + " m_alt_ad.prefix, "
      
        + " m_alt_ad.street, "
      
        + " m_alt_ad.suffix, "
      
        + " m_alt_ad.unit, "
      
        + " m_alt_ad.address2, "
      
        + " m_alt_ad.city, "
      
        + " m_alt_ad.state, "
      
        + " m_alt_ad.zip, "
      
        + " m_alt_ad.grid, "
      
        + " m_alt_ad.area_id "
      

      + " From m_alt_ad "

      
        + " Where "
        
          + " m_alt_ad.ticket_num = ?  "
        
      ;

      public static m_alt_ad FindByPrimaryKey(
      String ticket_num
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", ticket_num);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("m_alt_ad not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(m_alt_ad m_alt_ad)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num",m_alt_ad.ticket_num);
      

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
      String sql = "select 1 from m_alt_ad";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, ConnectionKeyEnum.Servman))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static m_alt_ad Load(IDataReader dataReader)
      {
      m_alt_ad m_alt_ad = new m_alt_ad();

      m_alt_ad.ticket_num = dataReader.GetString(0);
          m_alt_ad.block = dataReader.GetString(1);
          m_alt_ad.prefix = dataReader.GetString(2);
          m_alt_ad.street = dataReader.GetString(3);
          m_alt_ad.suffix = dataReader.GetString(4);
          m_alt_ad.unit = dataReader.GetString(5);
          m_alt_ad.address2 = dataReader.GetString(6);
          m_alt_ad.city = dataReader.GetString(7);
          m_alt_ad.state = dataReader.GetString(8);
          m_alt_ad.zip = dataReader.GetString(9);
          m_alt_ad.grid = dataReader.GetString(10);
          m_alt_ad.area_id = dataReader.GetString(11);
          

      return m_alt_ad;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [m_alt_ad] "

      
        + " Where "
        
          + " ticket_num = ?  "
        
      ;
      public static void Delete(m_alt_ad m_alt_ad)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@ticket_num", m_alt_ad.ticket_num);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [m_alt_ad] ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, ConnectionKeyEnum.Servman))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " m_alt_ad.ticket_num, "
      
        + " m_alt_ad.block, "
      
        + " m_alt_ad.prefix, "
      
        + " m_alt_ad.street, "
      
        + " m_alt_ad.suffix, "
      
        + " m_alt_ad.unit, "
      
        + " m_alt_ad.address2, "
      
        + " m_alt_ad.city, "
      
        + " m_alt_ad.state, "
      
        + " m_alt_ad.zip, "
      
        + " m_alt_ad.grid, "
      
        + " m_alt_ad.area_id "
      

      + " From m_alt_ad ";
      public static List<m_alt_ad> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<m_alt_ad> rv = new List<m_alt_ad>();

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
      List<m_alt_ad> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<m_alt_ad> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(m_alt_ad));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(m_alt_ad item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<m_alt_ad>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(m_alt_ad));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<m_alt_ad> itemsList
      = new List<m_alt_ad>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is m_alt_ad)
      itemsList.Add(deserializedObject as m_alt_ad);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_ticket_num;
      
        protected String m_block;
      
        protected String m_prefix;
      
        protected String m_street;
      
        protected String m_suffix;
      
        protected String m_unit;
      
        protected String m_address2;
      
        protected String m_city;
      
        protected String m_state;
      
        protected String m_zip;
      
        protected String m_grid;
      
        protected String m_area_id;
      
      #endregion

      #region Constructors
      public m_alt_ad(
      String 
          ticket_num
      )
      {
      
        m_ticket_num = ticket_num;
      
      }

      


        public m_alt_ad(
        String 
          ticket_num,String 
          block,String 
          prefix,String 
          street,String 
          suffix,String 
          unit,String 
          address2,String 
          city,String 
          state,String 
          zip,String 
          grid,String 
          area_id
        )
        {
        
          m_ticket_num = ticket_num;
        
          m_block = block;
        
          m_prefix = prefix;
        
          m_street = street;
        
          m_suffix = suffix;
        
          m_unit = unit;
        
          m_address2 = address2;
        
          m_city = city;
        
          m_state = state;
        
          m_zip = zip;
        
          m_grid = grid;
        
          m_area_id = area_id;
        
        }


      
      #endregion

      
        [XmlElement]
        public String ticket_num
        {
        get { return m_ticket_num;}
        set { m_ticket_num = value; }
        }
      
        [XmlElement]
        public String block
        {
        get { return m_block;}
        set { m_block = value; }
        }
      
        [XmlElement]
        public String prefix
        {
        get { return m_prefix;}
        set { m_prefix = value; }
        }
      
        [XmlElement]
        public String street
        {
        get { return m_street;}
        set { m_street = value; }
        }
      
        [XmlElement]
        public String suffix
        {
        get { return m_suffix;}
        set { m_suffix = value; }
        }
      
        [XmlElement]
        public String unit
        {
        get { return m_unit;}
        set { m_unit = value; }
        }
      
        [XmlElement]
        public String address2
        {
        get { return m_address2;}
        set { m_address2 = value; }
        }
      
        [XmlElement]
        public String city
        {
        get { return m_city;}
        set { m_city = value; }
        }
      
        [XmlElement]
        public String state
        {
        get { return m_state;}
        set { m_state = value; }
        }
      
        [XmlElement]
        public String zip
        {
        get { return m_zip;}
        set { m_zip = value; }
        }
      
        [XmlElement]
        public String grid
        {
        get { return m_grid;}
        set { m_grid = value; }
        }
      
        [XmlElement]
        public String area_id
        {
        get { return m_area_id;}
        set { m_area_id = value; }
        }
      
      }
      #endregion
      }

    