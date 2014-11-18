
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


      public partial class mapsco
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into mapsco ( " +
      
        " prefix, " +
      
        " street, " +
      
        " suffix, " +
      
        " city, " +
      
        " state, " +
      
        " zip, " +
      
        " b_beg, " +
      
        " b_end, " +
      
        " page, " +
      
        " grid, " +
      
        " map, " +
      
        " owner " +
      
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

      public static void Insert(mapsco mapsco)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@prefix", mapsco.prefix);
      
        Database.PutParameter(dbCommand,"@street", mapsco.street);
      
        Database.PutParameter(dbCommand,"@suffix", mapsco.suffix);
      
        Database.PutParameter(dbCommand,"@city", mapsco.city);
      
        Database.PutParameter(dbCommand,"@state", mapsco.state);
      
        Database.PutParameter(dbCommand,"@zip", mapsco.zip);
      
        Database.PutParameter(dbCommand,"@b_beg", mapsco.b_beg);
      
        Database.PutParameter(dbCommand,"@b_end", mapsco.b_end);
      
        Database.PutParameter(dbCommand,"@page", mapsco.page);
      
        Database.PutParameter(dbCommand,"@grid", mapsco.grid);
      
        Database.PutParameter(dbCommand,"@map", mapsco.map);
      
        Database.PutParameter(dbCommand,"@owner", mapsco.owner);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<mapsco>  mapscoList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(mapsco mapsco in  mapscoList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@prefix", mapsco.prefix);
      
        Database.PutParameter(dbCommand,"@street", mapsco.street);
      
        Database.PutParameter(dbCommand,"@suffix", mapsco.suffix);
      
        Database.PutParameter(dbCommand,"@city", mapsco.city);
      
        Database.PutParameter(dbCommand,"@state", mapsco.state);
      
        Database.PutParameter(dbCommand,"@zip", mapsco.zip);
      
        Database.PutParameter(dbCommand,"@b_beg", mapsco.b_beg);
      
        Database.PutParameter(dbCommand,"@b_end", mapsco.b_end);
      
        Database.PutParameter(dbCommand,"@page", mapsco.page);
      
        Database.PutParameter(dbCommand,"@grid", mapsco.grid);
      
        Database.PutParameter(dbCommand,"@map", mapsco.map);
      
        Database.PutParameter(dbCommand,"@owner", mapsco.owner);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@prefix",mapsco.prefix);
      
        Database.UpdateParameter(dbCommand,"@street",mapsco.street);
      
        Database.UpdateParameter(dbCommand,"@suffix",mapsco.suffix);
      
        Database.UpdateParameter(dbCommand,"@city",mapsco.city);
      
        Database.UpdateParameter(dbCommand,"@state",mapsco.state);
      
        Database.UpdateParameter(dbCommand,"@zip",mapsco.zip);
      
        Database.UpdateParameter(dbCommand,"@b_beg",mapsco.b_beg);
      
        Database.UpdateParameter(dbCommand,"@b_end",mapsco.b_end);
      
        Database.UpdateParameter(dbCommand,"@page",mapsco.page);
      
        Database.UpdateParameter(dbCommand,"@grid",mapsco.grid);
      
        Database.UpdateParameter(dbCommand,"@map",mapsco.map);
      
        Database.UpdateParameter(dbCommand,"@owner",mapsco.owner);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update mapsco Set "
      
        + " mapsco.prefix = ? , "
      
        + " mapsco.street = ? , "
      
        + " mapsco.suffix = ? , "
      
        + " mapsco.city = ? , "
      
        + " mapsco.state = ? , "
      
        + " mapsco.zip = ? , "
      
        + " mapsco.b_beg = ? , "
      
        + " mapsco.b_end = ? , "
      
        + " mapsco.page = ? , "
      
        + " mapsco.grid = ? , "
      
        + " mapsco.map = ? , "
      
        + " mapsco.owner = ?  "
      
        + " Where "
        
      ;

      public static void Update(mapsco mapsco)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " mapsco.prefix, "
      
        + " mapsco.street, "
      
        + " mapsco.suffix, "
      
        + " mapsco.city, "
      
        + " mapsco.state, "
      
        + " mapsco.zip, "
      
        + " mapsco.b_beg, "
      
        + " mapsco.b_end, "
      
        + " mapsco.page, "
      
        + " mapsco.grid, "
      
        + " mapsco.map, "
      
        + " mapsco.owner "
      

      + " From mapsco "

      
        + " Where "
        
      ;

      public static mapsco FindByPrimaryKey(
      
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("mapsco not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(mapsco mapsco)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      

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
      String sql = "select 1 from mapsco";

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

      public static mapsco Load(IDataReader dataReader)
      {
      mapsco mapsco = new mapsco();

      mapsco.prefix = dataReader.GetString(0);
          mapsco.street = dataReader.GetString(1);
          mapsco.suffix = dataReader.GetString(2);
          mapsco.city = dataReader.GetString(3);
          mapsco.state = dataReader.GetString(4);
          mapsco.zip = dataReader.GetString(5);
          mapsco.b_beg = dataReader.GetString(6);
          mapsco.b_end = dataReader.GetString(7);
          mapsco.page = dataReader.GetString(8);
          mapsco.grid = dataReader.GetString(9);
          mapsco.map = dataReader.GetString(10);
          mapsco.owner = dataReader.GetString(11);
          

      return mapsco;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [mapsco] "

      
        + " Where "
        
      ;
      public static void Delete(mapsco mapsco)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [mapsco] ";

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

      
        + " mapsco.prefix, "
      
        + " mapsco.street, "
      
        + " mapsco.suffix, "
      
        + " mapsco.city, "
      
        + " mapsco.state, "
      
        + " mapsco.zip, "
      
        + " mapsco.b_beg, "
      
        + " mapsco.b_end, "
      
        + " mapsco.page, "
      
        + " mapsco.grid, "
      
        + " mapsco.map, "
      
        + " mapsco.owner "
      

      + " From mapsco ";
      public static List<mapsco> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<mapsco> rv = new List<mapsco>();

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
      List<mapsco> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<mapsco> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(mapsco));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(mapsco item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<mapsco>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(mapsco));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<mapsco> itemsList
      = new List<mapsco>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is mapsco)
      itemsList.Add(deserializedObject as mapsco);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_prefix;
      
        protected String m_street;
      
        protected String m_suffix;
      
        protected String m_city;
      
        protected String m_state;
      
        protected String m_zip;
      
        protected String m_b_beg;
      
        protected String m_b_end;
      
        protected String m_page;
      
        protected String m_grid;
      
        protected String m_map;
      
        protected String m_owner;
      
      #endregion

      #region Constructors
      public mapsco(
      
      )
      {
      
      }

      


        public mapsco(
        String 
          prefix,String 
          street,String 
          suffix,String 
          city,String 
          state,String 
          zip,String 
          b_beg,String 
          b_end,String 
          page,String 
          grid,String 
          map,String 
          owner
        )
        {
        
          m_prefix = prefix;
        
          m_street = street;
        
          m_suffix = suffix;
        
          m_city = city;
        
          m_state = state;
        
          m_zip = zip;
        
          m_b_beg = b_beg;
        
          m_b_end = b_end;
        
          m_page = page;
        
          m_grid = grid;
        
          m_map = map;
        
          m_owner = owner;
        
        }


      
      #endregion

      
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
        public String b_beg
        {
        get { return m_b_beg;}
        set { m_b_beg = value; }
        }
      
        [XmlElement]
        public String b_end
        {
        get { return m_b_end;}
        set { m_b_end = value; }
        }
      
        [XmlElement]
        public String page
        {
        get { return m_page;}
        set { m_page = value; }
        }
      
        [XmlElement]
        public String grid
        {
        get { return m_grid;}
        set { m_grid = value; }
        }
      
        [XmlElement]
        public String map
        {
        get { return m_map;}
        set { m_map = value; }
        }
      
        [XmlElement]
        public String owner
        {
        get { return m_owner;}
        set { m_owner = value; }
        }
      
      }
      #endregion
      }

    