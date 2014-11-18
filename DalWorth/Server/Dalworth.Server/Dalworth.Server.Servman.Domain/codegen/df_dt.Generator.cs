
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


      public partial class df_dt
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into df_dt ( " +
      
        " type, " +
      
        " type_id, " +
      
        " book, " +
      
        " active, " +
      
        " revenue, " +
      
        " apply2mp, " +
      
        " extrasale, " +
      
        " servcharge, " +
      
        " defcomm, " +
      
        " invpickup, " +
      
        " cf_spotcln, " +
      
        " cf_filter, " +
      
        " group_id, " +
      
        " schddisc, " +
      
        " internet, " +
      
        " visible, " +
      
        " def_desc, " +
      
        " notes " +
      
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
      
        " ? " +
      
      ")";

      public static void Insert(df_dt df_dt)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@type", df_dt.type);
      
        Database.PutParameter(dbCommand,"@type_id", df_dt.type_id);
      
        Database.PutParameter(dbCommand,"@book", df_dt.book);
      
        Database.PutParameter(dbCommand,"@active", df_dt.active);
      
        Database.PutParameter(dbCommand,"@revenue", df_dt.revenue);
      
        Database.PutParameter(dbCommand,"@apply2mp", df_dt.apply2mp);
      
        Database.PutParameter(dbCommand,"@extrasale", df_dt.extrasale);
      
        Database.PutParameter(dbCommand,"@servcharge", df_dt.servcharge);
      
        Database.PutParameter(dbCommand,"@defcomm", df_dt.defcomm);
      
        Database.PutParameter(dbCommand,"@invpickup", df_dt.invpickup);
      
        Database.PutParameter(dbCommand,"@cf_spotcln", df_dt.cf_spotcln);
      
        Database.PutParameter(dbCommand,"@cf_filter", df_dt.cf_filter);
      
        Database.PutParameter(dbCommand,"@group_id", df_dt.group_id);
      
        Database.PutParameter(dbCommand,"@schddisc", df_dt.schddisc);
      
        Database.PutParameter(dbCommand,"@internet", df_dt.internet);
      
        Database.PutParameter(dbCommand,"@visible", df_dt.visible);
      
        Database.PutParameter(dbCommand,"@def_desc", df_dt.def_desc);
      
        Database.PutParameter(dbCommand,"@notes", df_dt.notes);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<df_dt>  df_dtList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(df_dt df_dt in  df_dtList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@type", df_dt.type);
      
        Database.PutParameter(dbCommand,"@type_id", df_dt.type_id);
      
        Database.PutParameter(dbCommand,"@book", df_dt.book);
      
        Database.PutParameter(dbCommand,"@active", df_dt.active);
      
        Database.PutParameter(dbCommand,"@revenue", df_dt.revenue);
      
        Database.PutParameter(dbCommand,"@apply2mp", df_dt.apply2mp);
      
        Database.PutParameter(dbCommand,"@extrasale", df_dt.extrasale);
      
        Database.PutParameter(dbCommand,"@servcharge", df_dt.servcharge);
      
        Database.PutParameter(dbCommand,"@defcomm", df_dt.defcomm);
      
        Database.PutParameter(dbCommand,"@invpickup", df_dt.invpickup);
      
        Database.PutParameter(dbCommand,"@cf_spotcln", df_dt.cf_spotcln);
      
        Database.PutParameter(dbCommand,"@cf_filter", df_dt.cf_filter);
      
        Database.PutParameter(dbCommand,"@group_id", df_dt.group_id);
      
        Database.PutParameter(dbCommand,"@schddisc", df_dt.schddisc);
      
        Database.PutParameter(dbCommand,"@internet", df_dt.internet);
      
        Database.PutParameter(dbCommand,"@visible", df_dt.visible);
      
        Database.PutParameter(dbCommand,"@def_desc", df_dt.def_desc);
      
        Database.PutParameter(dbCommand,"@notes", df_dt.notes);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@type",df_dt.type);
      
        Database.UpdateParameter(dbCommand,"@type_id",df_dt.type_id);
      
        Database.UpdateParameter(dbCommand,"@book",df_dt.book);
      
        Database.UpdateParameter(dbCommand,"@active",df_dt.active);
      
        Database.UpdateParameter(dbCommand,"@revenue",df_dt.revenue);
      
        Database.UpdateParameter(dbCommand,"@apply2mp",df_dt.apply2mp);
      
        Database.UpdateParameter(dbCommand,"@extrasale",df_dt.extrasale);
      
        Database.UpdateParameter(dbCommand,"@servcharge",df_dt.servcharge);
      
        Database.UpdateParameter(dbCommand,"@defcomm",df_dt.defcomm);
      
        Database.UpdateParameter(dbCommand,"@invpickup",df_dt.invpickup);
      
        Database.UpdateParameter(dbCommand,"@cf_spotcln",df_dt.cf_spotcln);
      
        Database.UpdateParameter(dbCommand,"@cf_filter",df_dt.cf_filter);
      
        Database.UpdateParameter(dbCommand,"@group_id",df_dt.group_id);
      
        Database.UpdateParameter(dbCommand,"@schddisc",df_dt.schddisc);
      
        Database.UpdateParameter(dbCommand,"@internet",df_dt.internet);
      
        Database.UpdateParameter(dbCommand,"@visible",df_dt.visible);
      
        Database.UpdateParameter(dbCommand,"@def_desc",df_dt.def_desc);
      
        Database.UpdateParameter(dbCommand,"@notes",df_dt.notes);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update df_dt Set "
      
        + " df_dt.type = ? , "
      
        + " df_dt.book = ? , "
      
        + " df_dt.active = ? , "
      
        + " df_dt.revenue = ? , "
      
        + " df_dt.apply2mp = ? , "
      
        + " df_dt.extrasale = ? , "
      
        + " df_dt.servcharge = ? , "
      
        + " df_dt.defcomm = ? , "
      
        + " df_dt.invpickup = ? , "
      
        + " df_dt.cf_spotcln = ? , "
      
        + " df_dt.cf_filter = ? , "
      
        + " df_dt.group_id = ? , "
      
        + " df_dt.schddisc = ? , "
      
        + " df_dt.internet = ? , "
      
        + " df_dt.visible = ? , "
      
        + " df_dt.def_desc = ? , "
      
        + " df_dt.notes = ?  "
      
        + " Where "
        
          + " df_dt.type_id = ?  "
        
      ;

      public static void Update(df_dt df_dt)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@type", df_dt.type);
      
        Database.PutParameter(dbCommand,"@book", df_dt.book);
      
        Database.PutParameter(dbCommand,"@active", df_dt.active);
      
        Database.PutParameter(dbCommand,"@revenue", df_dt.revenue);
      
        Database.PutParameter(dbCommand,"@apply2mp", df_dt.apply2mp);
      
        Database.PutParameter(dbCommand,"@extrasale", df_dt.extrasale);
      
        Database.PutParameter(dbCommand,"@servcharge", df_dt.servcharge);
      
        Database.PutParameter(dbCommand,"@defcomm", df_dt.defcomm);
      
        Database.PutParameter(dbCommand,"@invpickup", df_dt.invpickup);
      
        Database.PutParameter(dbCommand,"@cf_spotcln", df_dt.cf_spotcln);
      
        Database.PutParameter(dbCommand,"@cf_filter", df_dt.cf_filter);
      
        Database.PutParameter(dbCommand,"@group_id", df_dt.group_id);
      
        Database.PutParameter(dbCommand,"@schddisc", df_dt.schddisc);
      
        Database.PutParameter(dbCommand,"@internet", df_dt.internet);
      
        Database.PutParameter(dbCommand,"@visible", df_dt.visible);
      
        Database.PutParameter(dbCommand,"@def_desc", df_dt.def_desc);
      
        Database.PutParameter(dbCommand,"@notes", df_dt.notes);
      
        Database.PutParameter(dbCommand,"@type_id", df_dt.type_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " df_dt.type, "
      
        + " df_dt.type_id, "
      
        + " df_dt.book, "
      
        + " df_dt.active, "
      
        + " df_dt.revenue, "
      
        + " df_dt.apply2mp, "
      
        + " df_dt.extrasale, "
      
        + " df_dt.servcharge, "
      
        + " df_dt.defcomm, "
      
        + " df_dt.invpickup, "
      
        + " df_dt.cf_spotcln, "
      
        + " df_dt.cf_filter, "
      
        + " df_dt.group_id, "
      
        + " df_dt.schddisc, "
      
        + " df_dt.internet, "
      
        + " df_dt.visible, "
      
        + " df_dt.def_desc, "
      
        + " df_dt.notes "
      

      + " From df_dt "

      
        + " Where "
        
          + " df_dt.type_id = ?  "
        
      ;

      public static df_dt FindByPrimaryKey(
      String type_id
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@type_id", type_id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("df_dt not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(df_dt df_dt)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@type_id",df_dt.type_id);
      

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
      String sql = "select 1 from df_dt";

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

      public static df_dt Load(IDataReader dataReader)
      {
      df_dt df_dt = new df_dt();

      df_dt.type = dataReader.GetString(0);
          df_dt.type_id = dataReader.GetString(1);
          df_dt.book = dataReader.GetBoolean(2);
          df_dt.active = dataReader.GetBoolean(3);
          df_dt.revenue = dataReader.GetBoolean(4);
          df_dt.apply2mp = dataReader.GetBoolean(5);
          df_dt.extrasale = dataReader.GetBoolean(6);
          df_dt.servcharge = dataReader.GetBoolean(7);
          df_dt.defcomm = dataReader.GetString(8);
          df_dt.invpickup = dataReader.GetBoolean(9);
          df_dt.cf_spotcln = dataReader.GetBoolean(10);
          df_dt.cf_filter = dataReader.GetBoolean(11);
          df_dt.group_id = dataReader.GetInt32(12);
          df_dt.schddisc = dataReader.GetBoolean(13);
          df_dt.internet = dataReader.GetBoolean(14);
          df_dt.visible = dataReader.GetBoolean(15);
          df_dt.def_desc = dataReader.GetString(16);
          df_dt.notes = dataReader.GetString(17);
          

      return df_dt;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [df_dt] "

      
        + " Where "
        
          + " type_id = ?  "
        
      ;
      public static void Delete(df_dt df_dt)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@type_id", df_dt.type_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [df_dt] ";

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

      
        + " df_dt.type, "
      
        + " df_dt.type_id, "
      
        + " df_dt.book, "
      
        + " df_dt.active, "
      
        + " df_dt.revenue, "
      
        + " df_dt.apply2mp, "
      
        + " df_dt.extrasale, "
      
        + " df_dt.servcharge, "
      
        + " df_dt.defcomm, "
      
        + " df_dt.invpickup, "
      
        + " df_dt.cf_spotcln, "
      
        + " df_dt.cf_filter, "
      
        + " df_dt.group_id, "
      
        + " df_dt.schddisc, "
      
        + " df_dt.internet, "
      
        + " df_dt.visible, "
      
        + " df_dt.def_desc, "
      
        + " df_dt.notes "
      

      + " From df_dt ";
      public static List<df_dt> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<df_dt> rv = new List<df_dt>();

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
      List<df_dt> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<df_dt> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(df_dt));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(df_dt item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<df_dt>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(df_dt));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<df_dt> itemsList
      = new List<df_dt>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is df_dt)
      itemsList.Add(deserializedObject as df_dt);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_type;
      
        protected String m_type_id;
      
        protected bool m_book;
      
        protected bool m_active;
      
        protected bool m_revenue;
      
        protected bool m_apply2mp;
      
        protected bool m_extrasale;
      
        protected bool m_servcharge;
      
        protected String m_defcomm;
      
        protected bool m_invpickup;
      
        protected bool m_cf_spotcln;
      
        protected bool m_cf_filter;
      
        protected int m_group_id;
      
        protected bool m_schddisc;
      
        protected bool m_internet;
      
        protected bool m_visible;
      
        protected String m_def_desc;
      
        protected String m_notes;
      
      #endregion

      #region Constructors
      public df_dt(
      String 
          type_id
      )
      {
      
        m_type_id = type_id;
      
      }

      


        public df_dt(
        String 
          type,String 
          type_id,bool 
          book,bool 
          active,bool 
          revenue,bool 
          apply2mp,bool 
          extrasale,bool 
          servcharge,String 
          defcomm,bool 
          invpickup,bool 
          cf_spotcln,bool 
          cf_filter,int 
          group_id,bool 
          schddisc,bool 
          internet,bool 
          visible,String 
          def_desc,String 
          notes
        )
        {
        
          m_type = type;
        
          m_type_id = type_id;
        
          m_book = book;
        
          m_active = active;
        
          m_revenue = revenue;
        
          m_apply2mp = apply2mp;
        
          m_extrasale = extrasale;
        
          m_servcharge = servcharge;
        
          m_defcomm = defcomm;
        
          m_invpickup = invpickup;
        
          m_cf_spotcln = cf_spotcln;
        
          m_cf_filter = cf_filter;
        
          m_group_id = group_id;
        
          m_schddisc = schddisc;
        
          m_internet = internet;
        
          m_visible = visible;
        
          m_def_desc = def_desc;
        
          m_notes = notes;
        
        }


      
      #endregion

      
        [XmlElement]
        public String type
        {
        get { return m_type;}
        set { m_type = value; }
        }
      
        [XmlElement]
        public String type_id
        {
        get { return m_type_id;}
        set { m_type_id = value; }
        }
      
        [XmlElement]
        public bool book
        {
        get { return m_book;}
        set { m_book = value; }
        }
      
        [XmlElement]
        public bool active
        {
        get { return m_active;}
        set { m_active = value; }
        }
      
        [XmlElement]
        public bool revenue
        {
        get { return m_revenue;}
        set { m_revenue = value; }
        }
      
        [XmlElement]
        public bool apply2mp
        {
        get { return m_apply2mp;}
        set { m_apply2mp = value; }
        }
      
        [XmlElement]
        public bool extrasale
        {
        get { return m_extrasale;}
        set { m_extrasale = value; }
        }
      
        [XmlElement]
        public bool servcharge
        {
        get { return m_servcharge;}
        set { m_servcharge = value; }
        }
      
        [XmlElement]
        public String defcomm
        {
        get { return m_defcomm;}
        set { m_defcomm = value; }
        }
      
        [XmlElement]
        public bool invpickup
        {
        get { return m_invpickup;}
        set { m_invpickup = value; }
        }
      
        [XmlElement]
        public bool cf_spotcln
        {
        get { return m_cf_spotcln;}
        set { m_cf_spotcln = value; }
        }
      
        [XmlElement]
        public bool cf_filter
        {
        get { return m_cf_filter;}
        set { m_cf_filter = value; }
        }
      
        [XmlElement]
        public int group_id
        {
        get { return m_group_id;}
        set { m_group_id = value; }
        }
      
        [XmlElement]
        public bool schddisc
        {
        get { return m_schddisc;}
        set { m_schddisc = value; }
        }
      
        [XmlElement]
        public bool internet
        {
        get { return m_internet;}
        set { m_internet = value; }
        }
      
        [XmlElement]
        public bool visible
        {
        get { return m_visible;}
        set { m_visible = value; }
        }
      
        [XmlElement]
        public String def_desc
        {
        get { return m_def_desc;}
        set { m_def_desc = value; }
        }
      
        [XmlElement]
        public String notes
        {
        get { return m_notes;}
        set { m_notes = value; }
        }
      
      }
      #endregion
      }

    