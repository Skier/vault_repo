
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


      public partial class zip_data
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into zip_data ( " +
      
        " zip, " +
      
        " income, " +
      
        " area_id, " +
      
        " mapbook, " +
      
        " sg1zoneid, " +
      
        " sg2zoneid, " +
      
        " sg3zoneid, " +
      
        " sg4zoneid, " +
      
        " sg5zoneid, " +
      
        " message_id, " +
      
        " city, " +
      
        " state, " +
      
        " kic_area " +
      
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
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(zip_data zip_data)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@zip", zip_data.zip);
      
        Database.PutParameter(dbCommand,"@income", zip_data.income);
      
        Database.PutParameter(dbCommand,"@area_id", zip_data.area_id);
      
        Database.PutParameter(dbCommand,"@mapbook", zip_data.mapbook);
      
        Database.PutParameter(dbCommand,"@sg1zoneid", zip_data.sg1zoneid);
      
        Database.PutParameter(dbCommand,"@sg2zoneid", zip_data.sg2zoneid);
      
        Database.PutParameter(dbCommand,"@sg3zoneid", zip_data.sg3zoneid);
      
        Database.PutParameter(dbCommand,"@sg4zoneid", zip_data.sg4zoneid);
      
        Database.PutParameter(dbCommand,"@sg5zoneid", zip_data.sg5zoneid);
      
        Database.PutParameter(dbCommand,"@message_id", zip_data.message_id);
      
        Database.PutParameter(dbCommand,"@city", zip_data.city);
      
        Database.PutParameter(dbCommand,"@state", zip_data.state);
      
        Database.PutParameter(dbCommand,"@kic_area", zip_data.kic_area);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<zip_data>  zip_dataList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(zip_data zip_data in  zip_dataList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@zip", zip_data.zip);
      
        Database.PutParameter(dbCommand,"@income", zip_data.income);
      
        Database.PutParameter(dbCommand,"@area_id", zip_data.area_id);
      
        Database.PutParameter(dbCommand,"@mapbook", zip_data.mapbook);
      
        Database.PutParameter(dbCommand,"@sg1zoneid", zip_data.sg1zoneid);
      
        Database.PutParameter(dbCommand,"@sg2zoneid", zip_data.sg2zoneid);
      
        Database.PutParameter(dbCommand,"@sg3zoneid", zip_data.sg3zoneid);
      
        Database.PutParameter(dbCommand,"@sg4zoneid", zip_data.sg4zoneid);
      
        Database.PutParameter(dbCommand,"@sg5zoneid", zip_data.sg5zoneid);
      
        Database.PutParameter(dbCommand,"@message_id", zip_data.message_id);
      
        Database.PutParameter(dbCommand,"@city", zip_data.city);
      
        Database.PutParameter(dbCommand,"@state", zip_data.state);
      
        Database.PutParameter(dbCommand,"@kic_area", zip_data.kic_area);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@zip",zip_data.zip);
      
        Database.UpdateParameter(dbCommand,"@income",zip_data.income);
      
        Database.UpdateParameter(dbCommand,"@area_id",zip_data.area_id);
      
        Database.UpdateParameter(dbCommand,"@mapbook",zip_data.mapbook);
      
        Database.UpdateParameter(dbCommand,"@sg1zoneid",zip_data.sg1zoneid);
      
        Database.UpdateParameter(dbCommand,"@sg2zoneid",zip_data.sg2zoneid);
      
        Database.UpdateParameter(dbCommand,"@sg3zoneid",zip_data.sg3zoneid);
      
        Database.UpdateParameter(dbCommand,"@sg4zoneid",zip_data.sg4zoneid);
      
        Database.UpdateParameter(dbCommand,"@sg5zoneid",zip_data.sg5zoneid);
      
        Database.UpdateParameter(dbCommand,"@message_id",zip_data.message_id);
      
        Database.UpdateParameter(dbCommand,"@city",zip_data.city);
      
        Database.UpdateParameter(dbCommand,"@state",zip_data.state);
      
        Database.UpdateParameter(dbCommand,"@kic_area",zip_data.kic_area);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update zip_data Set "
      
        + " zip_data.income = ? , "
      
        + " zip_data.area_id = ? , "
      
        + " zip_data.mapbook = ? , "
      
        + " zip_data.sg1zoneid = ? , "
      
        + " zip_data.sg2zoneid = ? , "
      
        + " zip_data.sg3zoneid = ? , "
      
        + " zip_data.sg4zoneid = ? , "
      
        + " zip_data.sg5zoneid = ? , "
      
        + " zip_data.message_id = ? , "
      
        + " zip_data.city = ? , "
      
        + " zip_data.state = ? , "
      
        + " zip_data.kic_area = ?  "
      
        + " Where "
        
          + " zip_data.zip = ?  "
        
      ;

      public static void Update(zip_data zip_data)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@income", zip_data.income);
      
        Database.PutParameter(dbCommand,"@area_id", zip_data.area_id);
      
        Database.PutParameter(dbCommand,"@mapbook", zip_data.mapbook);
      
        Database.PutParameter(dbCommand,"@sg1zoneid", zip_data.sg1zoneid);
      
        Database.PutParameter(dbCommand,"@sg2zoneid", zip_data.sg2zoneid);
      
        Database.PutParameter(dbCommand,"@sg3zoneid", zip_data.sg3zoneid);
      
        Database.PutParameter(dbCommand,"@sg4zoneid", zip_data.sg4zoneid);
      
        Database.PutParameter(dbCommand,"@sg5zoneid", zip_data.sg5zoneid);
      
        Database.PutParameter(dbCommand,"@message_id", zip_data.message_id);
      
        Database.PutParameter(dbCommand,"@city", zip_data.city);
      
        Database.PutParameter(dbCommand,"@state", zip_data.state);
      
        Database.PutParameter(dbCommand,"@kic_area", zip_data.kic_area);
      
        Database.PutParameter(dbCommand,"@zip", zip_data.zip);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " zip_data.zip, "
      
        + " zip_data.income, "
      
        + " zip_data.area_id, "
      
        + " zip_data.mapbook, "
      
        + " zip_data.sg1zoneid, "
      
        + " zip_data.sg2zoneid, "
      
        + " zip_data.sg3zoneid, "
      
        + " zip_data.sg4zoneid, "
      
        + " zip_data.sg5zoneid, "
      
        + " zip_data.message_id, "
      
        + " zip_data.city, "
      
        + " zip_data.state, "
      
        + " zip_data.kic_area "
      

      + " From zip_data "

      
        + " Where "
        
          + " zip_data.zip = ?  "
        
      ;

      public static zip_data FindByPrimaryKey(
      String zip
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@zip", zip);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("zip_data not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(zip_data zip_data)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@zip",zip_data.zip);
      

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
      String sql = "select 1 from zip_data";

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

      public static zip_data Load(IDataReader dataReader)
      {
      zip_data zip_data = new zip_data();

      zip_data.zip = dataReader.GetString(0);
          zip_data.income = dataReader.GetInt32(1);
          zip_data.area_id = dataReader.GetString(2);
          zip_data.mapbook = dataReader.GetString(3);
          zip_data.sg1zoneid = dataReader.GetInt32(4);
          zip_data.sg2zoneid = dataReader.GetInt32(5);
          zip_data.sg3zoneid = dataReader.GetInt32(6);
          zip_data.sg4zoneid = dataReader.GetInt32(7);
          zip_data.sg5zoneid = dataReader.GetInt32(8);
          zip_data.message_id = dataReader.GetInt32(9);
          zip_data.city = dataReader.GetString(10);
          zip_data.state = dataReader.GetString(11);
          zip_data.kic_area = dataReader.GetBoolean(12);
          

      return zip_data;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [zip_data] "

      
        + " Where "
        
          + " zip = ?  "
        
      ;
      public static void Delete(zip_data zip_data)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@zip", zip_data.zip);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [zip_data] ";

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

      
        + " zip_data.zip, "
      
        + " zip_data.income, "
      
        + " zip_data.area_id, "
      
        + " zip_data.mapbook, "
      
        + " zip_data.sg1zoneid, "
      
        + " zip_data.sg2zoneid, "
      
        + " zip_data.sg3zoneid, "
      
        + " zip_data.sg4zoneid, "
      
        + " zip_data.sg5zoneid, "
      
        + " zip_data.message_id, "
      
        + " zip_data.city, "
      
        + " zip_data.state, "
      
        + " zip_data.kic_area "
      

      + " From zip_data ";
      public static List<zip_data> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<zip_data> rv = new List<zip_data>();

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
      List<zip_data> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<zip_data> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(zip_data));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(zip_data item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<zip_data>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(zip_data));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<zip_data> itemsList
      = new List<zip_data>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is zip_data)
      itemsList.Add(deserializedObject as zip_data);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_zip;
      
        protected int m_income;
      
        protected String m_area_id;
      
        protected String m_mapbook;
      
        protected int m_sg1zoneid;
      
        protected int m_sg2zoneid;
      
        protected int m_sg3zoneid;
      
        protected int m_sg4zoneid;
      
        protected int m_sg5zoneid;
      
        protected int m_message_id;
      
        protected String m_city;
      
        protected String m_state;
      
        protected bool m_kic_area;
      
      #endregion

      #region Constructors
      public zip_data(
      String 
          zip
      )
      {
      
        m_zip = zip;
      
      }

      


        public zip_data(
        String 
          zip,int 
          income,String 
          area_id,String 
          mapbook,int 
          sg1zoneid,int 
          sg2zoneid,int 
          sg3zoneid,int 
          sg4zoneid,int 
          sg5zoneid,int 
          message_id,String 
          city,String 
          state,bool 
          kic_area
        )
        {
        
          m_zip = zip;
        
          m_income = income;
        
          m_area_id = area_id;
        
          m_mapbook = mapbook;
        
          m_sg1zoneid = sg1zoneid;
        
          m_sg2zoneid = sg2zoneid;
        
          m_sg3zoneid = sg3zoneid;
        
          m_sg4zoneid = sg4zoneid;
        
          m_sg5zoneid = sg5zoneid;
        
          m_message_id = message_id;
        
          m_city = city;
        
          m_state = state;
        
          m_kic_area = kic_area;
        
        }


      
      #endregion

      
        [XmlElement]
        public String zip
        {
        get { return m_zip;}
        set { m_zip = value; }
        }
      
        [XmlElement]
        public int income
        {
        get { return m_income;}
        set { m_income = value; }
        }
      
        [XmlElement]
        public String area_id
        {
        get { return m_area_id;}
        set { m_area_id = value; }
        }
      
        [XmlElement]
        public String mapbook
        {
        get { return m_mapbook;}
        set { m_mapbook = value; }
        }
      
        [XmlElement]
        public int sg1zoneid
        {
        get { return m_sg1zoneid;}
        set { m_sg1zoneid = value; }
        }
      
        [XmlElement]
        public int sg2zoneid
        {
        get { return m_sg2zoneid;}
        set { m_sg2zoneid = value; }
        }
      
        [XmlElement]
        public int sg3zoneid
        {
        get { return m_sg3zoneid;}
        set { m_sg3zoneid = value; }
        }
      
        [XmlElement]
        public int sg4zoneid
        {
        get { return m_sg4zoneid;}
        set { m_sg4zoneid = value; }
        }
      
        [XmlElement]
        public int sg5zoneid
        {
        get { return m_sg5zoneid;}
        set { m_sg5zoneid = value; }
        }
      
        [XmlElement]
        public int message_id
        {
        get { return m_message_id;}
        set { m_message_id = value; }
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
        public bool kic_area
        {
        get { return m_kic_area;}
        set { m_kic_area = value; }
        }
      
      }
      #endregion
      }

    