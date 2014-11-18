
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


      public partial class contmast
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into contmast ( " +
      
        " cont_id, " +
      
        " company, " +
      
        " address1, " +
      
        " address2, " +
      
        " city, " +
      
        " state, " +
      
        " zip, " +
      
        " area_id, " +
      
        " cont_type, " +
      
        " active, " +
      
        " referby, " +
      
        " referperc, " +
      
        " companyid, " +
      
        " contract, " +
      
        " prime_tech, " +
      
        " mrkt_area, " +
      
        " fran_date, " +
      
        " excl_ratio, " +
      
        " rev_client, " +
      
        " eref_in, " +
      
        " excl_30, " +
      
        " excl_90, " +
      
        " rev_c30, " +
      
        " rev_c90 " +
      
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

      public static void Insert(contmast contmast)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cont_id", contmast.cont_id);
      
        Database.PutParameter(dbCommand,"@company", contmast.company);
      
        Database.PutParameter(dbCommand,"@address1", contmast.address1);
      
        Database.PutParameter(dbCommand,"@address2", contmast.address2);
      
        Database.PutParameter(dbCommand,"@city", contmast.city);
      
        Database.PutParameter(dbCommand,"@state", contmast.state);
      
        Database.PutParameter(dbCommand,"@zip", contmast.zip);
      
        Database.PutParameter(dbCommand,"@area_id", contmast.area_id);
      
        Database.PutParameter(dbCommand,"@cont_type", contmast.cont_type);
      
        Database.PutParameter(dbCommand,"@active", contmast.active);
      
        Database.PutParameter(dbCommand,"@referby", contmast.referby);
      
        Database.PutParameter(dbCommand,"@referperc", contmast.referperc);
      
        Database.PutParameter(dbCommand,"@companyid", contmast.companyid);
      
        Database.PutParameter(dbCommand,"@contract", contmast.contract);
      
        Database.PutParameter(dbCommand,"@prime_tech", contmast.prime_tech);
      
        Database.PutParameter(dbCommand,"@mrkt_area", contmast.mrkt_area);
      
        Database.PutParameter(dbCommand,"@fran_date", contmast.fran_date);
      
        Database.PutParameter(dbCommand,"@excl_ratio", contmast.excl_ratio);
      
        Database.PutParameter(dbCommand,"@rev_client", contmast.rev_client);
      
        Database.PutParameter(dbCommand,"@eref_in", contmast.eref_in);
      
        Database.PutParameter(dbCommand,"@excl_30", contmast.excl_30);
      
        Database.PutParameter(dbCommand,"@excl_90", contmast.excl_90);
      
        Database.PutParameter(dbCommand,"@rev_c30", contmast.rev_c30);
      
        Database.PutParameter(dbCommand,"@rev_c90", contmast.rev_c90);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<contmast>  contmastList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(contmast contmast in  contmastList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@cont_id", contmast.cont_id);
      
        Database.PutParameter(dbCommand,"@company", contmast.company);
      
        Database.PutParameter(dbCommand,"@address1", contmast.address1);
      
        Database.PutParameter(dbCommand,"@address2", contmast.address2);
      
        Database.PutParameter(dbCommand,"@city", contmast.city);
      
        Database.PutParameter(dbCommand,"@state", contmast.state);
      
        Database.PutParameter(dbCommand,"@zip", contmast.zip);
      
        Database.PutParameter(dbCommand,"@area_id", contmast.area_id);
      
        Database.PutParameter(dbCommand,"@cont_type", contmast.cont_type);
      
        Database.PutParameter(dbCommand,"@active", contmast.active);
      
        Database.PutParameter(dbCommand,"@referby", contmast.referby);
      
        Database.PutParameter(dbCommand,"@referperc", contmast.referperc);
      
        Database.PutParameter(dbCommand,"@companyid", contmast.companyid);
      
        Database.PutParameter(dbCommand,"@contract", contmast.contract);
      
        Database.PutParameter(dbCommand,"@prime_tech", contmast.prime_tech);
      
        Database.PutParameter(dbCommand,"@mrkt_area", contmast.mrkt_area);
      
        Database.PutParameter(dbCommand,"@fran_date", contmast.fran_date);
      
        Database.PutParameter(dbCommand,"@excl_ratio", contmast.excl_ratio);
      
        Database.PutParameter(dbCommand,"@rev_client", contmast.rev_client);
      
        Database.PutParameter(dbCommand,"@eref_in", contmast.eref_in);
      
        Database.PutParameter(dbCommand,"@excl_30", contmast.excl_30);
      
        Database.PutParameter(dbCommand,"@excl_90", contmast.excl_90);
      
        Database.PutParameter(dbCommand,"@rev_c30", contmast.rev_c30);
      
        Database.PutParameter(dbCommand,"@rev_c90", contmast.rev_c90);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@cont_id",contmast.cont_id);
      
        Database.UpdateParameter(dbCommand,"@company",contmast.company);
      
        Database.UpdateParameter(dbCommand,"@address1",contmast.address1);
      
        Database.UpdateParameter(dbCommand,"@address2",contmast.address2);
      
        Database.UpdateParameter(dbCommand,"@city",contmast.city);
      
        Database.UpdateParameter(dbCommand,"@state",contmast.state);
      
        Database.UpdateParameter(dbCommand,"@zip",contmast.zip);
      
        Database.UpdateParameter(dbCommand,"@area_id",contmast.area_id);
      
        Database.UpdateParameter(dbCommand,"@cont_type",contmast.cont_type);
      
        Database.UpdateParameter(dbCommand,"@active",contmast.active);
      
        Database.UpdateParameter(dbCommand,"@referby",contmast.referby);
      
        Database.UpdateParameter(dbCommand,"@referperc",contmast.referperc);
      
        Database.UpdateParameter(dbCommand,"@companyid",contmast.companyid);
      
        Database.UpdateParameter(dbCommand,"@contract",contmast.contract);
      
        Database.UpdateParameter(dbCommand,"@prime_tech",contmast.prime_tech);
      
        Database.UpdateParameter(dbCommand,"@mrkt_area",contmast.mrkt_area);
      
        Database.UpdateParameter(dbCommand,"@fran_date",contmast.fran_date);
      
        Database.UpdateParameter(dbCommand,"@excl_ratio",contmast.excl_ratio);
      
        Database.UpdateParameter(dbCommand,"@rev_client",contmast.rev_client);
      
        Database.UpdateParameter(dbCommand,"@eref_in",contmast.eref_in);
      
        Database.UpdateParameter(dbCommand,"@excl_30",contmast.excl_30);
      
        Database.UpdateParameter(dbCommand,"@excl_90",contmast.excl_90);
      
        Database.UpdateParameter(dbCommand,"@rev_c30",contmast.rev_c30);
      
        Database.UpdateParameter(dbCommand,"@rev_c90",contmast.rev_c90);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update contmast Set "
      
        + " contmast.company = ? , "
      
        + " contmast.address1 = ? , "
      
        + " contmast.address2 = ? , "
      
        + " contmast.city = ? , "
      
        + " contmast.state = ? , "
      
        + " contmast.zip = ? , "
      
        + " contmast.area_id = ? , "
      
        + " contmast.cont_type = ? , "
      
        + " contmast.active = ? , "
      
        + " contmast.referby = ? , "
      
        + " contmast.referperc = ? , "
      
        + " contmast.companyid = ? , "
      
        + " contmast.contract = ? , "
      
        + " contmast.prime_tech = ? , "
      
        + " contmast.mrkt_area = ? , "
      
        + " contmast.fran_date = ? , "
      
        + " contmast.excl_ratio = ? , "
      
        + " contmast.rev_client = ? , "
      
        + " contmast.eref_in = ? , "
      
        + " contmast.excl_30 = ? , "
      
        + " contmast.excl_90 = ? , "
      
        + " contmast.rev_c30 = ? , "
      
        + " contmast.rev_c90 = ?  "
      
        + " Where "
        
          + " contmast.cont_id = ?  "
        
      ;

      public static void Update(contmast contmast)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@company", contmast.company);
      
        Database.PutParameter(dbCommand,"@address1", contmast.address1);
      
        Database.PutParameter(dbCommand,"@address2", contmast.address2);
      
        Database.PutParameter(dbCommand,"@city", contmast.city);
      
        Database.PutParameter(dbCommand,"@state", contmast.state);
      
        Database.PutParameter(dbCommand,"@zip", contmast.zip);
      
        Database.PutParameter(dbCommand,"@area_id", contmast.area_id);
      
        Database.PutParameter(dbCommand,"@cont_type", contmast.cont_type);
      
        Database.PutParameter(dbCommand,"@active", contmast.active);
      
        Database.PutParameter(dbCommand,"@referby", contmast.referby);
      
        Database.PutParameter(dbCommand,"@referperc", contmast.referperc);
      
        Database.PutParameter(dbCommand,"@companyid", contmast.companyid);
      
        Database.PutParameter(dbCommand,"@contract", contmast.contract);
      
        Database.PutParameter(dbCommand,"@prime_tech", contmast.prime_tech);
      
        Database.PutParameter(dbCommand,"@mrkt_area", contmast.mrkt_area);
      
        Database.PutParameter(dbCommand,"@fran_date", contmast.fran_date);
      
        Database.PutParameter(dbCommand,"@excl_ratio", contmast.excl_ratio);
      
        Database.PutParameter(dbCommand,"@rev_client", contmast.rev_client);
      
        Database.PutParameter(dbCommand,"@eref_in", contmast.eref_in);
      
        Database.PutParameter(dbCommand,"@excl_30", contmast.excl_30);
      
        Database.PutParameter(dbCommand,"@excl_90", contmast.excl_90);
      
        Database.PutParameter(dbCommand,"@rev_c30", contmast.rev_c30);
      
        Database.PutParameter(dbCommand,"@rev_c90", contmast.rev_c90);
      
        Database.PutParameter(dbCommand,"@cont_id", contmast.cont_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " contmast.cont_id, "
      
        + " contmast.company, "
      
        + " contmast.address1, "
      
        + " contmast.address2, "
      
        + " contmast.city, "
      
        + " contmast.state, "
      
        + " contmast.zip, "
      
        + " contmast.area_id, "
      
        + " contmast.cont_type, "
      
        + " contmast.active, "
      
        + " contmast.referby, "
      
        + " contmast.referperc, "
      
        + " contmast.companyid, "
      
        + " contmast.contract, "
      
        + " contmast.prime_tech, "
      
        + " contmast.mrkt_area, "
      
        + " contmast.fran_date, "
      
        + " contmast.excl_ratio, "
      
        + " contmast.rev_client, "
      
        + " contmast.eref_in, "
      
        + " contmast.excl_30, "
      
        + " contmast.excl_90, "
      
        + " contmast.rev_c30, "
      
        + " contmast.rev_c90 "
      

      + " From contmast "

      
        + " Where "
        
          + " contmast.cont_id = ?  "
        
      ;

      public static contmast FindByPrimaryKey(
      String cont_id
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cont_id", cont_id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("contmast not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(contmast contmast)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cont_id",contmast.cont_id);
      

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
      String sql = "select 1 from contmast";

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

      public static contmast Load(IDataReader dataReader)
      {
      contmast contmast = new contmast();

      contmast.cont_id = dataReader.GetString(0);
          contmast.company = dataReader.GetString(1);
          contmast.address1 = dataReader.GetString(2);
          contmast.address2 = dataReader.GetString(3);
          contmast.city = dataReader.GetString(4);
          contmast.state = dataReader.GetString(5);
          contmast.zip = dataReader.GetString(6);
          contmast.area_id = dataReader.GetString(7);
          contmast.cont_type = dataReader.GetInt32(8);
          contmast.active = dataReader.GetBoolean(9);
          contmast.referby = dataReader.GetString(10);
          contmast.referperc = dataReader.GetFloat(11);
          contmast.companyid = dataReader.GetInt32(12);
          contmast.contract = dataReader.GetString(13);
          contmast.prime_tech = dataReader.GetString(14);
          contmast.mrkt_area = dataReader.GetBoolean(15);
          contmast.fran_date = dataReader.GetDateTime(16);
          contmast.excl_ratio = dataReader.GetFloat(17);
          contmast.rev_client = dataReader.GetFloat(18);
          contmast.eref_in = dataReader.GetBoolean(19);
          contmast.excl_30 = dataReader.GetFloat(20);
          contmast.excl_90 = dataReader.GetFloat(21);
          contmast.rev_c30 = dataReader.GetFloat(22);
          contmast.rev_c90 = dataReader.GetFloat(23);
          

      return contmast;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [contmast] "

      
        + " Where "
        
          + " cont_id = ?  "
        
      ;
      public static void Delete(contmast contmast)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@cont_id", contmast.cont_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [contmast] ";

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

      
        + " contmast.cont_id, "
      
        + " contmast.company, "
      
        + " contmast.address1, "
      
        + " contmast.address2, "
      
        + " contmast.city, "
      
        + " contmast.state, "
      
        + " contmast.zip, "
      
        + " contmast.area_id, "
      
        + " contmast.cont_type, "
      
        + " contmast.active, "
      
        + " contmast.referby, "
      
        + " contmast.referperc, "
      
        + " contmast.companyid, "
      
        + " contmast.contract, "
      
        + " contmast.prime_tech, "
      
        + " contmast.mrkt_area, "
      
        + " contmast.fran_date, "
      
        + " contmast.excl_ratio, "
      
        + " contmast.rev_client, "
      
        + " contmast.eref_in, "
      
        + " contmast.excl_30, "
      
        + " contmast.excl_90, "
      
        + " contmast.rev_c30, "
      
        + " contmast.rev_c90 "
      

      + " From contmast ";
      public static List<contmast> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<contmast> rv = new List<contmast>();

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
      List<contmast> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<contmast> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(contmast));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(contmast item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<contmast>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(contmast));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<contmast> itemsList
      = new List<contmast>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is contmast)
      itemsList.Add(deserializedObject as contmast);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_cont_id;
      
        protected String m_company;
      
        protected String m_address1;
      
        protected String m_address2;
      
        protected String m_city;
      
        protected String m_state;
      
        protected String m_zip;
      
        protected String m_area_id;
      
        protected int m_cont_type;
      
        protected bool m_active;
      
        protected String m_referby;
      
        protected float m_referperc;
      
        protected int m_companyid;
      
        protected String m_contract;
      
        protected String m_prime_tech;
      
        protected bool m_mrkt_area;
      
        protected DateTime m_fran_date;
      
        protected float m_excl_ratio;
      
        protected float m_rev_client;
      
        protected bool m_eref_in;
      
        protected float m_excl_30;
      
        protected float m_excl_90;
      
        protected float m_rev_c30;
      
        protected float m_rev_c90;
      
      #endregion

      #region Constructors
      public contmast(
      String 
          cont_id
      )
      {
      
        m_cont_id = cont_id;
      
      }

      


        public contmast(
        String 
          cont_id,String 
          company,String 
          address1,String 
          address2,String 
          city,String 
          state,String 
          zip,String 
          area_id,int 
          cont_type,bool 
          active,String 
          referby,float 
          referperc,int 
          companyid,String 
          contract,String 
          prime_tech,bool 
          mrkt_area,DateTime 
          fran_date,float 
          excl_ratio,float 
          rev_client,bool 
          eref_in,float 
          excl_30,float 
          excl_90,float 
          rev_c30,float 
          rev_c90
        )
        {
        
          m_cont_id = cont_id;
        
          m_company = company;
        
          m_address1 = address1;
        
          m_address2 = address2;
        
          m_city = city;
        
          m_state = state;
        
          m_zip = zip;
        
          m_area_id = area_id;
        
          m_cont_type = cont_type;
        
          m_active = active;
        
          m_referby = referby;
        
          m_referperc = referperc;
        
          m_companyid = companyid;
        
          m_contract = contract;
        
          m_prime_tech = prime_tech;
        
          m_mrkt_area = mrkt_area;
        
          m_fran_date = fran_date;
        
          m_excl_ratio = excl_ratio;
        
          m_rev_client = rev_client;
        
          m_eref_in = eref_in;
        
          m_excl_30 = excl_30;
        
          m_excl_90 = excl_90;
        
          m_rev_c30 = rev_c30;
        
          m_rev_c90 = rev_c90;
        
        }


      
      #endregion

      
        [XmlElement]
        public String cont_id
        {
        get { return m_cont_id;}
        set { m_cont_id = value; }
        }
      
        [XmlElement]
        public String company
        {
        get { return m_company;}
        set { m_company = value; }
        }
      
        [XmlElement]
        public String address1
        {
        get { return m_address1;}
        set { m_address1 = value; }
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
        public String area_id
        {
        get { return m_area_id;}
        set { m_area_id = value; }
        }
      
        [XmlElement]
        public int cont_type
        {
        get { return m_cont_type;}
        set { m_cont_type = value; }
        }
      
        [XmlElement]
        public bool active
        {
        get { return m_active;}
        set { m_active = value; }
        }
      
        [XmlElement]
        public String referby
        {
        get { return m_referby;}
        set { m_referby = value; }
        }
      
        [XmlElement]
        public float referperc
        {
        get { return m_referperc;}
        set { m_referperc = value; }
        }
      
        [XmlElement]
        public int companyid
        {
        get { return m_companyid;}
        set { m_companyid = value; }
        }
      
        [XmlElement]
        public String contract
        {
        get { return m_contract;}
        set { m_contract = value; }
        }
      
        [XmlElement]
        public String prime_tech
        {
        get { return m_prime_tech;}
        set { m_prime_tech = value; }
        }
      
        [XmlElement]
        public bool mrkt_area
        {
        get { return m_mrkt_area;}
        set { m_mrkt_area = value; }
        }
      
        [XmlElement]
        public DateTime fran_date
        {
        get { return m_fran_date;}
        set { m_fran_date = value; }
        }
      
        [XmlElement]
        public float excl_ratio
        {
        get { return m_excl_ratio;}
        set { m_excl_ratio = value; }
        }
      
        [XmlElement]
        public float rev_client
        {
        get { return m_rev_client;}
        set { m_rev_client = value; }
        }
      
        [XmlElement]
        public bool eref_in
        {
        get { return m_eref_in;}
        set { m_eref_in = value; }
        }
      
        [XmlElement]
        public float excl_30
        {
        get { return m_excl_30;}
        set { m_excl_30 = value; }
        }
      
        [XmlElement]
        public float excl_90
        {
        get { return m_excl_90;}
        set { m_excl_90 = value; }
        }
      
        [XmlElement]
        public float rev_c30
        {
        get { return m_rev_c30;}
        set { m_rev_c30 = value; }
        }
      
        [XmlElement]
        public float rev_c90
        {
        get { return m_rev_c90;}
        set { m_rev_c90 = value; }
        }
      
      }
      #endregion
      }

    