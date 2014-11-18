
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


      public partial class ad_src
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ad_src ( " +
      
        " acronym, " +
      
        " descript, " +
      
        " group_id, " +
      
        " area_id, " +
      
        " id_code, " +
      
        " active, " +
      
        " refertech, " +
      
        " techrefer, " +
      
        " canvas, " +
      
        " comp_refer, " +
      
        " refercomp, " +
      
        " carefree, " +
      
        " custmtc, " +
      
        " internet, " +
      
        " invisible, " +
      
        " inetdesc, " +
      
        " expc_flag " +
      
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
      
        " ? " +
      
      ")";

      public static void Insert(ad_src ad_src)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@acronym", ad_src.acronym);
      
        Database.PutParameter(dbCommand,"@descript", ad_src.descript);
      
        Database.PutParameter(dbCommand,"@group_id", ad_src.group_id);
      
        Database.PutParameter(dbCommand,"@area_id", ad_src.area_id);
      
        Database.PutParameter(dbCommand,"@id_code", ad_src.id_code);
      
        Database.PutParameter(dbCommand,"@active", ad_src.active);
      
        Database.PutParameter(dbCommand,"@refertech", ad_src.refertech);
      
        Database.PutParameter(dbCommand,"@techrefer", ad_src.techrefer);
      
        Database.PutParameter(dbCommand,"@canvas", ad_src.canvas);
      
        Database.PutParameter(dbCommand,"@comp_refer", ad_src.comp_refer);
      
        Database.PutParameter(dbCommand,"@refercomp", ad_src.refercomp);
      
        Database.PutParameter(dbCommand,"@carefree", ad_src.carefree);
      
        Database.PutParameter(dbCommand,"@custmtc", ad_src.custmtc);
      
        Database.PutParameter(dbCommand,"@internet", ad_src.internet);
      
        Database.PutParameter(dbCommand,"@invisible", ad_src.invisible);
      
        Database.PutParameter(dbCommand,"@inetdesc", ad_src.inetdesc);
      
        Database.PutParameter(dbCommand,"@expc_flag", ad_src.expc_flag);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<ad_src>  ad_srcList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(ad_src ad_src in  ad_srcList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@acronym", ad_src.acronym);
      
        Database.PutParameter(dbCommand,"@descript", ad_src.descript);
      
        Database.PutParameter(dbCommand,"@group_id", ad_src.group_id);
      
        Database.PutParameter(dbCommand,"@area_id", ad_src.area_id);
      
        Database.PutParameter(dbCommand,"@id_code", ad_src.id_code);
      
        Database.PutParameter(dbCommand,"@active", ad_src.active);
      
        Database.PutParameter(dbCommand,"@refertech", ad_src.refertech);
      
        Database.PutParameter(dbCommand,"@techrefer", ad_src.techrefer);
      
        Database.PutParameter(dbCommand,"@canvas", ad_src.canvas);
      
        Database.PutParameter(dbCommand,"@comp_refer", ad_src.comp_refer);
      
        Database.PutParameter(dbCommand,"@refercomp", ad_src.refercomp);
      
        Database.PutParameter(dbCommand,"@carefree", ad_src.carefree);
      
        Database.PutParameter(dbCommand,"@custmtc", ad_src.custmtc);
      
        Database.PutParameter(dbCommand,"@internet", ad_src.internet);
      
        Database.PutParameter(dbCommand,"@invisible", ad_src.invisible);
      
        Database.PutParameter(dbCommand,"@inetdesc", ad_src.inetdesc);
      
        Database.PutParameter(dbCommand,"@expc_flag", ad_src.expc_flag);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@acronym",ad_src.acronym);
      
        Database.UpdateParameter(dbCommand,"@descript",ad_src.descript);
      
        Database.UpdateParameter(dbCommand,"@group_id",ad_src.group_id);
      
        Database.UpdateParameter(dbCommand,"@area_id",ad_src.area_id);
      
        Database.UpdateParameter(dbCommand,"@id_code",ad_src.id_code);
      
        Database.UpdateParameter(dbCommand,"@active",ad_src.active);
      
        Database.UpdateParameter(dbCommand,"@refertech",ad_src.refertech);
      
        Database.UpdateParameter(dbCommand,"@techrefer",ad_src.techrefer);
      
        Database.UpdateParameter(dbCommand,"@canvas",ad_src.canvas);
      
        Database.UpdateParameter(dbCommand,"@comp_refer",ad_src.comp_refer);
      
        Database.UpdateParameter(dbCommand,"@refercomp",ad_src.refercomp);
      
        Database.UpdateParameter(dbCommand,"@carefree",ad_src.carefree);
      
        Database.UpdateParameter(dbCommand,"@custmtc",ad_src.custmtc);
      
        Database.UpdateParameter(dbCommand,"@internet",ad_src.internet);
      
        Database.UpdateParameter(dbCommand,"@invisible",ad_src.invisible);
      
        Database.UpdateParameter(dbCommand,"@inetdesc",ad_src.inetdesc);
      
        Database.UpdateParameter(dbCommand,"@expc_flag",ad_src.expc_flag);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update ad_src Set "
      
        + " ad_src.acronym = ? , "
      
        + " ad_src.descript = ? , "
      
        + " ad_src.group_id = ? , "
      
        + " ad_src.area_id = ? , "
      
        + " ad_src.active = ? , "
      
        + " ad_src.refertech = ? , "
      
        + " ad_src.techrefer = ? , "
      
        + " ad_src.canvas = ? , "
      
        + " ad_src.comp_refer = ? , "
      
        + " ad_src.refercomp = ? , "
      
        + " ad_src.carefree = ? , "
      
        + " ad_src.custmtc = ? , "
      
        + " ad_src.internet = ? , "
      
        + " ad_src.invisible = ? , "
      
        + " ad_src.inetdesc = ? , "
      
        + " ad_src.expc_flag = ?  "
      
        + " Where "
        
          + " ad_src.id_code = ?  "
        
      ;

      public static void Update(ad_src ad_src)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@acronym", ad_src.acronym);
      
        Database.PutParameter(dbCommand,"@descript", ad_src.descript);
      
        Database.PutParameter(dbCommand,"@group_id", ad_src.group_id);
      
        Database.PutParameter(dbCommand,"@area_id", ad_src.area_id);
      
        Database.PutParameter(dbCommand,"@active", ad_src.active);
      
        Database.PutParameter(dbCommand,"@refertech", ad_src.refertech);
      
        Database.PutParameter(dbCommand,"@techrefer", ad_src.techrefer);
      
        Database.PutParameter(dbCommand,"@canvas", ad_src.canvas);
      
        Database.PutParameter(dbCommand,"@comp_refer", ad_src.comp_refer);
      
        Database.PutParameter(dbCommand,"@refercomp", ad_src.refercomp);
      
        Database.PutParameter(dbCommand,"@carefree", ad_src.carefree);
      
        Database.PutParameter(dbCommand,"@custmtc", ad_src.custmtc);
      
        Database.PutParameter(dbCommand,"@internet", ad_src.internet);
      
        Database.PutParameter(dbCommand,"@invisible", ad_src.invisible);
      
        Database.PutParameter(dbCommand,"@inetdesc", ad_src.inetdesc);
      
        Database.PutParameter(dbCommand,"@expc_flag", ad_src.expc_flag);
      
        Database.PutParameter(dbCommand,"@id_code", ad_src.id_code);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ad_src.acronym, "
      
        + " ad_src.descript, "
      
        + " ad_src.group_id, "
      
        + " ad_src.area_id, "
      
        + " ad_src.id_code, "
      
        + " ad_src.active, "
      
        + " ad_src.refertech, "
      
        + " ad_src.techrefer, "
      
        + " ad_src.canvas, "
      
        + " ad_src.comp_refer, "
      
        + " ad_src.refercomp, "
      
        + " ad_src.carefree, "
      
        + " ad_src.custmtc, "
      
        + " ad_src.internet, "
      
        + " ad_src.invisible, "
      
        + " ad_src.inetdesc, "
      
        + " ad_src.expc_flag "
      

      + " From ad_src "

      
        + " Where "
        
          + " ad_src.id_code = ?  "
        
      ;

      public static ad_src FindByPrimaryKey(
      String id_code
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@id_code", id_code);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ad_src not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(ad_src ad_src)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@id_code",ad_src.id_code);
      

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
      String sql = "select 1 from ad_src";

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

      public static ad_src Load(IDataReader dataReader)
      {
      ad_src ad_src = new ad_src();

      ad_src.acronym = dataReader.GetString(0);
          ad_src.descript = dataReader.GetString(1);
          ad_src.group_id = dataReader.GetString(2);
          ad_src.area_id = dataReader.GetString(3);
          ad_src.id_code = dataReader.GetString(4);
          ad_src.active = dataReader.GetBoolean(5);
          ad_src.refertech = dataReader.GetInt32(6);
          ad_src.techrefer = dataReader.GetBoolean(7);
          ad_src.canvas = dataReader.GetBoolean(8);
          ad_src.comp_refer = dataReader.GetBoolean(9);
          ad_src.refercomp = dataReader.GetInt32(10);
          ad_src.carefree = dataReader.GetBoolean(11);
          ad_src.custmtc = dataReader.GetInt32(12);
          ad_src.internet = dataReader.GetBoolean(13);
          ad_src.invisible = dataReader.GetBoolean(14);
          ad_src.inetdesc = dataReader.GetString(15);
          ad_src.expc_flag = dataReader.GetBoolean(16);
          

      return ad_src;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [ad_src] "

      
        + " Where "
        
          + " id_code = ?  "
        
      ;
      public static void Delete(ad_src ad_src)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@id_code", ad_src.id_code);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [ad_src] ";

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

      
        + " ad_src.acronym, "
      
        + " ad_src.descript, "
      
        + " ad_src.group_id, "
      
        + " ad_src.area_id, "
      
        + " ad_src.id_code, "
      
        + " ad_src.active, "
      
        + " ad_src.refertech, "
      
        + " ad_src.techrefer, "
      
        + " ad_src.canvas, "
      
        + " ad_src.comp_refer, "
      
        + " ad_src.refercomp, "
      
        + " ad_src.carefree, "
      
        + " ad_src.custmtc, "
      
        + " ad_src.internet, "
      
        + " ad_src.invisible, "
      
        + " ad_src.inetdesc, "
      
        + " ad_src.expc_flag "
      

      + " From ad_src ";
      public static List<ad_src> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<ad_src> rv = new List<ad_src>();

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
      List<ad_src> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<ad_src> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ad_src));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ad_src item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ad_src>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ad_src));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ad_src> itemsList
      = new List<ad_src>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ad_src)
      itemsList.Add(deserializedObject as ad_src);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_acronym;
      
        protected String m_descript;
      
        protected String m_group_id;
      
        protected String m_area_id;
      
        protected String m_id_code;
      
        protected bool m_active;
      
        protected int m_refertech;
      
        protected bool m_techrefer;
      
        protected bool m_canvas;
      
        protected bool m_comp_refer;
      
        protected int m_refercomp;
      
        protected bool m_carefree;
      
        protected int m_custmtc;
      
        protected bool m_internet;
      
        protected bool m_invisible;
      
        protected String m_inetdesc;
      
        protected bool m_expc_flag;
      
      #endregion

      #region Constructors
      public ad_src(
      String 
          id_code
      )
      {
      
        m_id_code = id_code;
      
      }

      


        public ad_src(
        String 
          acronym,String 
          descript,String 
          group_id,String 
          area_id,String 
          id_code,bool 
          active,int 
          refertech,bool 
          techrefer,bool 
          canvas,bool 
          comp_refer,int 
          refercomp,bool 
          carefree,int 
          custmtc,bool 
          internet,bool 
          invisible,String 
          inetdesc,bool 
          expc_flag
        )
        {
        
          m_acronym = acronym;
        
          m_descript = descript;
        
          m_group_id = group_id;
        
          m_area_id = area_id;
        
          m_id_code = id_code;
        
          m_active = active;
        
          m_refertech = refertech;
        
          m_techrefer = techrefer;
        
          m_canvas = canvas;
        
          m_comp_refer = comp_refer;
        
          m_refercomp = refercomp;
        
          m_carefree = carefree;
        
          m_custmtc = custmtc;
        
          m_internet = internet;
        
          m_invisible = invisible;
        
          m_inetdesc = inetdesc;
        
          m_expc_flag = expc_flag;
        
        }


      
      #endregion

      
        [XmlElement]
        public String acronym
        {
        get { return m_acronym;}
        set { m_acronym = value; }
        }
      
        [XmlElement]
        public String descript
        {
        get { return m_descript;}
        set { m_descript = value; }
        }
      
        [XmlElement]
        public String group_id
        {
        get { return m_group_id;}
        set { m_group_id = value; }
        }
      
        [XmlElement]
        public String area_id
        {
        get { return m_area_id;}
        set { m_area_id = value; }
        }
      
        [XmlElement]
        public String id_code
        {
        get { return m_id_code;}
        set { m_id_code = value; }
        }
      
        [XmlElement]
        public bool active
        {
        get { return m_active;}
        set { m_active = value; }
        }
      
        [XmlElement]
        public int refertech
        {
        get { return m_refertech;}
        set { m_refertech = value; }
        }
      
        [XmlElement]
        public bool techrefer
        {
        get { return m_techrefer;}
        set { m_techrefer = value; }
        }
      
        [XmlElement]
        public bool canvas
        {
        get { return m_canvas;}
        set { m_canvas = value; }
        }
      
        [XmlElement]
        public bool comp_refer
        {
        get { return m_comp_refer;}
        set { m_comp_refer = value; }
        }
      
        [XmlElement]
        public int refercomp
        {
        get { return m_refercomp;}
        set { m_refercomp = value; }
        }
      
        [XmlElement]
        public bool carefree
        {
        get { return m_carefree;}
        set { m_carefree = value; }
        }
      
        [XmlElement]
        public int custmtc
        {
        get { return m_custmtc;}
        set { m_custmtc = value; }
        }
      
        [XmlElement]
        public bool internet
        {
        get { return m_internet;}
        set { m_internet = value; }
        }
      
        [XmlElement]
        public bool invisible
        {
        get { return m_invisible;}
        set { m_invisible = value; }
        }
      
        [XmlElement]
        public String inetdesc
        {
        get { return m_inetdesc;}
        set { m_inetdesc = value; }
        }
      
        [XmlElement]
        public bool expc_flag
        {
        get { return m_expc_flag;}
        set { m_expc_flag = value; }
        }
      
      }
      #endregion
      }

    