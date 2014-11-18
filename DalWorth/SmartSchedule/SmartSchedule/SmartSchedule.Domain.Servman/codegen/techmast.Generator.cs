
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Odbc;
using SmartSchedule.Data;
using SmartSchedule.SDK;
using System.Xml;
using System.Xml.Serialization;
using System.Text;


namespace SmartSchedule.Domain.Servman
      {


      public partial class techmast
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into techmast ( " +
      
        " cont_id, " +
      
        " tech_id, " +
      
        " tech_name, " +
      
        " address1, " +
      
        " address2, " +
      
        " area_id, " +
      
        " home_num, " +
      
        " pager_num, " +
      
        " active, " +
      
        " pag_max, " +
      
        " old_id, " +
      
        " truck_num, " +
      
        " tech_num, " +
      
        " pass_code, " +
      
        " cell, " +
      
        " emailaddr, " +
      
        " web_name, " +
      
        " web_page, " +
      
        " countycode, " +
      
        " web_image, " +
      
        " web_info, " +
      
        " extension, " +
      
        " int_email, " +
      
        " startdate " +
      
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

      public static void Insert(techmast techmast)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cont_id", techmast.cont_id);
      
        Database.PutParameter(dbCommand,"@tech_id", techmast.tech_id);
      
        Database.PutParameter(dbCommand,"@tech_name", techmast.tech_name);
      
        Database.PutParameter(dbCommand,"@address1", techmast.address1);
      
        Database.PutParameter(dbCommand,"@address2", techmast.address2);
      
        Database.PutParameter(dbCommand,"@area_id", techmast.area_id);
      
        Database.PutParameter(dbCommand,"@home_num", techmast.home_num);
      
        Database.PutParameter(dbCommand,"@pager_num", techmast.pager_num);
      
        Database.PutParameter(dbCommand,"@active", techmast.active);
      
        Database.PutParameter(dbCommand,"@pag_max", techmast.pag_max);
      
        Database.PutParameter(dbCommand,"@old_id", techmast.old_id);
      
        Database.PutParameter(dbCommand,"@truck_num", techmast.truck_num);
      
        Database.PutParameter(dbCommand,"@tech_num", techmast.tech_num);
      
        Database.PutParameter(dbCommand,"@pass_code", techmast.pass_code);
      
        Database.PutParameter(dbCommand,"@cell", techmast.cell);
      
        Database.PutParameter(dbCommand,"@emailaddr", techmast.emailaddr);
      
        Database.PutParameter(dbCommand,"@web_name", techmast.web_name);
      
        Database.PutParameter(dbCommand,"@web_page", techmast.web_page);
      
        Database.PutParameter(dbCommand,"@countycode", techmast.countycode);
      
        Database.PutParameter(dbCommand,"@web_image", techmast.web_image);
      
        Database.PutParameter(dbCommand,"@web_info", techmast.web_info);
      
        Database.PutParameter(dbCommand,"@extension", techmast.extension);
      
        Database.PutParameter(dbCommand,"@int_email", techmast.int_email);
      
        Database.PutParameter(dbCommand,"@startdate", techmast.startdate);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<techmast>  techmastList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(techmast techmast in  techmastList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@cont_id", techmast.cont_id);
      
        Database.PutParameter(dbCommand,"@tech_id", techmast.tech_id);
      
        Database.PutParameter(dbCommand,"@tech_name", techmast.tech_name);
      
        Database.PutParameter(dbCommand,"@address1", techmast.address1);
      
        Database.PutParameter(dbCommand,"@address2", techmast.address2);
      
        Database.PutParameter(dbCommand,"@area_id", techmast.area_id);
      
        Database.PutParameter(dbCommand,"@home_num", techmast.home_num);
      
        Database.PutParameter(dbCommand,"@pager_num", techmast.pager_num);
      
        Database.PutParameter(dbCommand,"@active", techmast.active);
      
        Database.PutParameter(dbCommand,"@pag_max", techmast.pag_max);
      
        Database.PutParameter(dbCommand,"@old_id", techmast.old_id);
      
        Database.PutParameter(dbCommand,"@truck_num", techmast.truck_num);
      
        Database.PutParameter(dbCommand,"@tech_num", techmast.tech_num);
      
        Database.PutParameter(dbCommand,"@pass_code", techmast.pass_code);
      
        Database.PutParameter(dbCommand,"@cell", techmast.cell);
      
        Database.PutParameter(dbCommand,"@emailaddr", techmast.emailaddr);
      
        Database.PutParameter(dbCommand,"@web_name", techmast.web_name);
      
        Database.PutParameter(dbCommand,"@web_page", techmast.web_page);
      
        Database.PutParameter(dbCommand,"@countycode", techmast.countycode);
      
        Database.PutParameter(dbCommand,"@web_image", techmast.web_image);
      
        Database.PutParameter(dbCommand,"@web_info", techmast.web_info);
      
        Database.PutParameter(dbCommand,"@extension", techmast.extension);
      
        Database.PutParameter(dbCommand,"@int_email", techmast.int_email);
      
        Database.PutParameter(dbCommand,"@startdate", techmast.startdate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@cont_id",techmast.cont_id);
      
        Database.UpdateParameter(dbCommand,"@tech_id",techmast.tech_id);
      
        Database.UpdateParameter(dbCommand,"@tech_name",techmast.tech_name);
      
        Database.UpdateParameter(dbCommand,"@address1",techmast.address1);
      
        Database.UpdateParameter(dbCommand,"@address2",techmast.address2);
      
        Database.UpdateParameter(dbCommand,"@area_id",techmast.area_id);
      
        Database.UpdateParameter(dbCommand,"@home_num",techmast.home_num);
      
        Database.UpdateParameter(dbCommand,"@pager_num",techmast.pager_num);
      
        Database.UpdateParameter(dbCommand,"@active",techmast.active);
      
        Database.UpdateParameter(dbCommand,"@pag_max",techmast.pag_max);
      
        Database.UpdateParameter(dbCommand,"@old_id",techmast.old_id);
      
        Database.UpdateParameter(dbCommand,"@truck_num",techmast.truck_num);
      
        Database.UpdateParameter(dbCommand,"@tech_num",techmast.tech_num);
      
        Database.UpdateParameter(dbCommand,"@pass_code",techmast.pass_code);
      
        Database.UpdateParameter(dbCommand,"@cell",techmast.cell);
      
        Database.UpdateParameter(dbCommand,"@emailaddr",techmast.emailaddr);
      
        Database.UpdateParameter(dbCommand,"@web_name",techmast.web_name);
      
        Database.UpdateParameter(dbCommand,"@web_page",techmast.web_page);
      
        Database.UpdateParameter(dbCommand,"@countycode",techmast.countycode);
      
        Database.UpdateParameter(dbCommand,"@web_image",techmast.web_image);
      
        Database.UpdateParameter(dbCommand,"@web_info",techmast.web_info);
      
        Database.UpdateParameter(dbCommand,"@extension",techmast.extension);
      
        Database.UpdateParameter(dbCommand,"@int_email",techmast.int_email);
      
        Database.UpdateParameter(dbCommand,"@startdate",techmast.startdate);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update techmast Set "
      
        + " techmast.cont_id = ? , "
      
        + " techmast.tech_name = ? , "
      
        + " techmast.address1 = ? , "
      
        + " techmast.address2 = ? , "
      
        + " techmast.area_id = ? , "
      
        + " techmast.home_num = ? , "
      
        + " techmast.pager_num = ? , "
      
        + " techmast.active = ? , "
      
        + " techmast.pag_max = ? , "
      
        + " techmast.old_id = ? , "
      
        + " techmast.truck_num = ? , "
      
        + " techmast.tech_num = ? , "
      
        + " techmast.pass_code = ? , "
      
        + " techmast.cell = ? , "
      
        + " techmast.emailaddr = ? , "
      
        + " techmast.web_name = ? , "
      
        + " techmast.web_page = ? , "
      
        + " techmast.countycode = ? , "
      
        + " techmast.web_image = ? , "
      
        + " techmast.web_info = ? , "
      
        + " techmast.extension = ? , "
      
        + " techmast.int_email = ? , "
      
        + " techmast.startdate = ?  "
      
        + " Where "
        
          + " techmast.tech_id = ?  "
        
      ;

      public static void Update(techmast techmast)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@cont_id", techmast.cont_id);
      
        Database.PutParameter(dbCommand,"@tech_name", techmast.tech_name);
      
        Database.PutParameter(dbCommand,"@address1", techmast.address1);
      
        Database.PutParameter(dbCommand,"@address2", techmast.address2);
      
        Database.PutParameter(dbCommand,"@area_id", techmast.area_id);
      
        Database.PutParameter(dbCommand,"@home_num", techmast.home_num);
      
        Database.PutParameter(dbCommand,"@pager_num", techmast.pager_num);
      
        Database.PutParameter(dbCommand,"@active", techmast.active);
      
        Database.PutParameter(dbCommand,"@pag_max", techmast.pag_max);
      
        Database.PutParameter(dbCommand,"@old_id", techmast.old_id);
      
        Database.PutParameter(dbCommand,"@truck_num", techmast.truck_num);
      
        Database.PutParameter(dbCommand,"@tech_num", techmast.tech_num);
      
        Database.PutParameter(dbCommand,"@pass_code", techmast.pass_code);
      
        Database.PutParameter(dbCommand,"@cell", techmast.cell);
      
        Database.PutParameter(dbCommand,"@emailaddr", techmast.emailaddr);
      
        Database.PutParameter(dbCommand,"@web_name", techmast.web_name);
      
        Database.PutParameter(dbCommand,"@web_page", techmast.web_page);
      
        Database.PutParameter(dbCommand,"@countycode", techmast.countycode);
      
        Database.PutParameter(dbCommand,"@web_image", techmast.web_image);
      
        Database.PutParameter(dbCommand,"@web_info", techmast.web_info);
      
        Database.PutParameter(dbCommand,"@extension", techmast.extension);
      
        Database.PutParameter(dbCommand,"@int_email", techmast.int_email);
      
        Database.PutParameter(dbCommand,"@startdate", techmast.startdate);
      
        Database.PutParameter(dbCommand,"@tech_id", techmast.tech_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " techmast.cont_id, "
      
        + " techmast.tech_id, "
      
        + " techmast.tech_name, "
      
        + " techmast.address1, "
      
        + " techmast.address2, "
      
        + " techmast.area_id, "
      
        + " techmast.home_num, "
      
        + " techmast.pager_num, "
      
        + " techmast.active, "
      
        + " techmast.pag_max, "
      
        + " techmast.old_id, "
      
        + " techmast.truck_num, "
      
        + " techmast.tech_num, "
      
        + " techmast.pass_code, "
      
        + " techmast.cell, "
      
        + " techmast.emailaddr, "
      
        + " techmast.web_name, "
      
        + " techmast.web_page, "
      
        + " techmast.countycode, "
      
        + " techmast.web_image, "
      
        + " techmast.web_info, "
      
        + " techmast.extension, "
      
        + " techmast.int_email, "
      
        + " techmast.startdate "
      

      + " From techmast "

      
        + " Where "
        
          + " techmast.tech_id = ?  "
        
      ;

      public static techmast FindByPrimaryKey(
      String tech_id
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@tech_id", tech_id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("techmast not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(techmast techmast)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@tech_id",techmast.tech_id);
      

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
      String sql = "select 1 from techmast";

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

      public static techmast Load(IDataReader dataReader)
      {
      techmast techmast = new techmast();

      techmast.cont_id = dataReader.GetString(0);
          techmast.tech_id = dataReader.GetString(1);
          techmast.tech_name = dataReader.GetString(2);
          techmast.address1 = dataReader.GetString(3);
          techmast.address2 = dataReader.GetString(4);
          techmast.area_id = dataReader.GetString(5);
          techmast.home_num = dataReader.GetString(6);
          techmast.pager_num = dataReader.GetString(7);
          techmast.active = dataReader.GetBoolean(8);
          techmast.pag_max = dataReader.GetInt32(9);
          techmast.old_id = dataReader.GetString(10);
          techmast.truck_num = dataReader.GetString(11);
          techmast.tech_num = dataReader.GetInt32(12);
          techmast.pass_code = dataReader.GetInt32(13);
          techmast.cell = dataReader.GetString(14);
          techmast.emailaddr = dataReader.GetString(15);
          techmast.web_name = dataReader.GetString(16);
          techmast.web_page = dataReader.GetString(17);
          techmast.countycode = dataReader.GetString(18);
          techmast.web_image = dataReader.GetString(19);
          techmast.web_info = dataReader.GetString(20);
          techmast.extension = dataReader.GetString(21);
          techmast.int_email = dataReader.GetString(22);
          techmast.startdate = dataReader.GetDateTime(23);
          

      return techmast;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [techmast] "

      
        + " Where "
        
          + " tech_id = ?  "
        
      ;
      public static void Delete(techmast techmast)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@tech_id", techmast.tech_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [techmast] ";

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

      
        + " techmast.cont_id, "
      
        + " techmast.tech_id, "
      
        + " techmast.tech_name, "
      
        + " techmast.address1, "
      
        + " techmast.address2, "
      
        + " techmast.area_id, "
      
        + " techmast.home_num, "
      
        + " techmast.pager_num, "
      
        + " techmast.active, "
      
        + " techmast.pag_max, "
      
        + " techmast.old_id, "
      
        + " techmast.truck_num, "
      
        + " techmast.tech_num, "
      
        + " techmast.pass_code, "
      
        + " techmast.cell, "
      
        + " techmast.emailaddr, "
      
        + " techmast.web_name, "
      
        + " techmast.web_page, "
      
        + " techmast.countycode, "
      
        + " techmast.web_image, "
      
        + " techmast.web_info, "
      
        + " techmast.extension, "
      
        + " techmast.int_email, "
      
        + " techmast.startdate "
      

      + " From techmast ";
      public static List<techmast> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<techmast> rv = new List<techmast>();

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
      List<techmast> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<techmast> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(techmast));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(techmast item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<techmast>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(techmast));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<techmast> itemsList
      = new List<techmast>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is techmast)
      itemsList.Add(deserializedObject as techmast);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_cont_id;
      
        protected String m_tech_id;
      
        protected String m_tech_name;
      
        protected String m_address1;
      
        protected String m_address2;
      
        protected String m_area_id;
      
        protected String m_home_num;
      
        protected String m_pager_num;
      
        protected bool m_active;
      
        protected int m_pag_max;
      
        protected String m_old_id;
      
        protected String m_truck_num;
      
        protected int m_tech_num;
      
        protected int m_pass_code;
      
        protected String m_cell;
      
        protected String m_emailaddr;
      
        protected String m_web_name;
      
        protected String m_web_page;
      
        protected String m_countycode;
      
        protected String m_web_image;
      
        protected String m_web_info;
      
        protected String m_extension;
      
        protected String m_int_email;
      
        protected DateTime m_startdate;
      
      #endregion

      #region Constructors
      public techmast(
      String 
          tech_id
      )
      {
      
        m_tech_id = tech_id;
      
      }

      


        public techmast(
        String 
          cont_id,String 
          tech_id,String 
          tech_name,String 
          address1,String 
          address2,String 
          area_id,String 
          home_num,String 
          pager_num,bool 
          active,int 
          pag_max,String 
          old_id,String 
          truck_num,int 
          tech_num,int 
          pass_code,String 
          cell,String 
          emailaddr,String 
          web_name,String 
          web_page,String 
          countycode,String 
          web_image,String 
          web_info,String 
          extension,String 
          int_email,DateTime 
          startdate
        )
        {
        
          m_cont_id = cont_id;
        
          m_tech_id = tech_id;
        
          m_tech_name = tech_name;
        
          m_address1 = address1;
        
          m_address2 = address2;
        
          m_area_id = area_id;
        
          m_home_num = home_num;
        
          m_pager_num = pager_num;
        
          m_active = active;
        
          m_pag_max = pag_max;
        
          m_old_id = old_id;
        
          m_truck_num = truck_num;
        
          m_tech_num = tech_num;
        
          m_pass_code = pass_code;
        
          m_cell = cell;
        
          m_emailaddr = emailaddr;
        
          m_web_name = web_name;
        
          m_web_page = web_page;
        
          m_countycode = countycode;
        
          m_web_image = web_image;
        
          m_web_info = web_info;
        
          m_extension = extension;
        
          m_int_email = int_email;
        
          m_startdate = startdate;
        
        }


      
      #endregion

      
        [XmlElement]
        public String cont_id
        {
        get { return m_cont_id;}
        set { m_cont_id = value; }
        }
      
        [XmlElement]
        public String tech_id
        {
        get { return m_tech_id;}
        set { m_tech_id = value; }
        }
      
        [XmlElement]
        public String tech_name
        {
        get { return m_tech_name;}
        set { m_tech_name = value; }
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
        public String area_id
        {
        get { return m_area_id;}
        set { m_area_id = value; }
        }
      
        [XmlElement]
        public String home_num
        {
        get { return m_home_num;}
        set { m_home_num = value; }
        }
      
        [XmlElement]
        public String pager_num
        {
        get { return m_pager_num;}
        set { m_pager_num = value; }
        }
      
        [XmlElement]
        public bool active
        {
        get { return m_active;}
        set { m_active = value; }
        }
      
        [XmlElement]
        public int pag_max
        {
        get { return m_pag_max;}
        set { m_pag_max = value; }
        }
      
        [XmlElement]
        public String old_id
        {
        get { return m_old_id;}
        set { m_old_id = value; }
        }
      
        [XmlElement]
        public String truck_num
        {
        get { return m_truck_num;}
        set { m_truck_num = value; }
        }
      
        [XmlElement]
        public int tech_num
        {
        get { return m_tech_num;}
        set { m_tech_num = value; }
        }
      
        [XmlElement]
        public int pass_code
        {
        get { return m_pass_code;}
        set { m_pass_code = value; }
        }
      
        [XmlElement]
        public String cell
        {
        get { return m_cell;}
        set { m_cell = value; }
        }
      
        [XmlElement]
        public String emailaddr
        {
        get { return m_emailaddr;}
        set { m_emailaddr = value; }
        }
      
        [XmlElement]
        public String web_name
        {
        get { return m_web_name;}
        set { m_web_name = value; }
        }
      
        [XmlElement]
        public String web_page
        {
        get { return m_web_page;}
        set { m_web_page = value; }
        }
      
        [XmlElement]
        public String countycode
        {
        get { return m_countycode;}
        set { m_countycode = value; }
        }
      
        [XmlElement]
        public String web_image
        {
        get { return m_web_image;}
        set { m_web_image = value; }
        }
      
        [XmlElement]
        public String web_info
        {
        get { return m_web_info;}
        set { m_web_info = value; }
        }
      
        [XmlElement]
        public String extension
        {
        get { return m_extension;}
        set { m_extension = value; }
        }
      
        [XmlElement]
        public String int_email
        {
        get { return m_int_email;}
        set { m_int_email = value; }
        }
      
        [XmlElement]
        public DateTime startdate
        {
        get { return m_startdate;}
        set { m_startdate = value; }
        }
      
      }
      #endregion
      }

    