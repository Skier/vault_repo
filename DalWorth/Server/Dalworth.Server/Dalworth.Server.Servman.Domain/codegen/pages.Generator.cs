
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


      public partial class pages
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into pages ( " +
      
        " phone, " +
      
        " max_baud, " +
      
        " d_start, " +
      
        " t_start, " +
      
        " d_end, " +
      
        " t_end, " +
      
        " pager_num, " +
      
        " message, " +
      
        " response, " +
      
        " status, " +
      
        " seq_num, " +
      
        " ticket_num, " +
      
        " count, " +
      
        " station, " +
      
        " email_dom " +
      
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
      
        " ? " +
      
      ")";

      public static void Insert(pages pages)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@phone", pages.phone);
      
        Database.PutParameter(dbCommand,"@max_baud", pages.max_baud);
      
        Database.PutParameter(dbCommand,"@d_start", pages.d_start);
      
        Database.PutParameter(dbCommand,"@t_start", pages.t_start);
      
        Database.PutParameter(dbCommand,"@d_end", pages.d_end);
      
        Database.PutParameter(dbCommand,"@t_end", pages.t_end);
      
        Database.PutParameter(dbCommand,"@pager_num", pages.pager_num);
      
        Database.PutParameter(dbCommand,"@message", pages.message);
      
        Database.PutParameter(dbCommand,"@response", pages.response);
      
        Database.PutParameter(dbCommand,"@status", pages.status);
      
        Database.PutParameter(dbCommand,"@seq_num", pages.seq_num);
      
        Database.PutParameter(dbCommand,"@ticket_num", pages.ticket_num);
      
        Database.PutParameter(dbCommand,"@count", pages.count);
      
        Database.PutParameter(dbCommand,"@station", pages.station);
      
        Database.PutParameter(dbCommand,"@email_dom", pages.email_dom);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<pages>  pagesList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(pages pages in  pagesList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@phone", pages.phone);
      
        Database.PutParameter(dbCommand,"@max_baud", pages.max_baud);
      
        Database.PutParameter(dbCommand,"@d_start", pages.d_start);
      
        Database.PutParameter(dbCommand,"@t_start", pages.t_start);
      
        Database.PutParameter(dbCommand,"@d_end", pages.d_end);
      
        Database.PutParameter(dbCommand,"@t_end", pages.t_end);
      
        Database.PutParameter(dbCommand,"@pager_num", pages.pager_num);
      
        Database.PutParameter(dbCommand,"@message", pages.message);
      
        Database.PutParameter(dbCommand,"@response", pages.response);
      
        Database.PutParameter(dbCommand,"@status", pages.status);
      
        Database.PutParameter(dbCommand,"@seq_num", pages.seq_num);
      
        Database.PutParameter(dbCommand,"@ticket_num", pages.ticket_num);
      
        Database.PutParameter(dbCommand,"@count", pages.count);
      
        Database.PutParameter(dbCommand,"@station", pages.station);
      
        Database.PutParameter(dbCommand,"@email_dom", pages.email_dom);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@phone",pages.phone);
      
        Database.UpdateParameter(dbCommand,"@max_baud",pages.max_baud);
      
        Database.UpdateParameter(dbCommand,"@d_start",pages.d_start);
      
        Database.UpdateParameter(dbCommand,"@t_start",pages.t_start);
      
        Database.UpdateParameter(dbCommand,"@d_end",pages.d_end);
      
        Database.UpdateParameter(dbCommand,"@t_end",pages.t_end);
      
        Database.UpdateParameter(dbCommand,"@pager_num",pages.pager_num);
      
        Database.UpdateParameter(dbCommand,"@message",pages.message);
      
        Database.UpdateParameter(dbCommand,"@response",pages.response);
      
        Database.UpdateParameter(dbCommand,"@status",pages.status);
      
        Database.UpdateParameter(dbCommand,"@seq_num",pages.seq_num);
      
        Database.UpdateParameter(dbCommand,"@ticket_num",pages.ticket_num);
      
        Database.UpdateParameter(dbCommand,"@count",pages.count);
      
        Database.UpdateParameter(dbCommand,"@station",pages.station);
      
        Database.UpdateParameter(dbCommand,"@email_dom",pages.email_dom);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update pages Set "
      
        + " pages.phone = ? , "
      
        + " pages.max_baud = ? , "
      
        + " pages.d_start = ? , "
      
        + " pages.t_start = ? , "
      
        + " pages.d_end = ? , "
      
        + " pages.t_end = ? , "
      
        + " pages.pager_num = ? , "
      
        + " pages.message = ? , "
      
        + " pages.response = ? , "
      
        + " pages.status = ? , "
      
        + " pages.seq_num = ? , "
      
        + " pages.ticket_num = ? , "
      
        + " pages.count = ? , "
      
        + " pages.station = ? , "
      
        + " pages.email_dom = ?  "
      
        + " Where "
        
      ;

      public static void Update(pages pages)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " pages.phone, "
      
        + " pages.max_baud, "
      
        + " pages.d_start, "
      
        + " pages.t_start, "
      
        + " pages.d_end, "
      
        + " pages.t_end, "
      
        + " pages.pager_num, "
      
        + " pages.message, "
      
        + " pages.response, "
      
        + " pages.status, "
      
        + " pages.seq_num, "
      
        + " pages.ticket_num, "
      
        + " pages.count, "
      
        + " pages.station, "
      
        + " pages.email_dom "
      

      + " From pages "

      
        + " Where "
        
      ;

      public static pages FindByPrimaryKey(
      
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
      throw new DataNotFoundException("pages not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(pages pages)
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
      String sql = "select 1 from pages";

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

      public static pages Load(IDataReader dataReader)
      {
      pages pages = new pages();

      pages.phone = dataReader.GetString(0);
          pages.max_baud = dataReader.GetInt32(1);
          pages.d_start = dataReader.GetDateTime(2);
          pages.t_start = dataReader.GetString(3);
          pages.d_end = dataReader.GetDateTime(4);
          pages.t_end = dataReader.GetString(5);
          pages.pager_num = dataReader.GetString(6);
          pages.message = dataReader.GetString(7);
          pages.response = dataReader.GetString(8);
          pages.status = dataReader.GetInt32(9);
          pages.seq_num = dataReader.GetInt32(10);
          pages.ticket_num = dataReader.GetString(11);
          pages.count = dataReader.GetInt32(12);
          pages.station = dataReader.GetInt32(13);
          pages.email_dom = dataReader.GetString(14);
          

      return pages;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [pages] "

      
        + " Where "
        
      ;
      public static void Delete(pages pages)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [pages] ";

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

      
        + " pages.phone, "
      
        + " pages.max_baud, "
      
        + " pages.d_start, "
      
        + " pages.t_start, "
      
        + " pages.d_end, "
      
        + " pages.t_end, "
      
        + " pages.pager_num, "
      
        + " pages.message, "
      
        + " pages.response, "
      
        + " pages.status, "
      
        + " pages.seq_num, "
      
        + " pages.ticket_num, "
      
        + " pages.count, "
      
        + " pages.station, "
      
        + " pages.email_dom "
      

      + " From pages ";
      public static List<pages> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<pages> rv = new List<pages>();

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
      List<pages> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<pages> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(pages));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(pages item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<pages>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(pages));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<pages> itemsList
      = new List<pages>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is pages)
      itemsList.Add(deserializedObject as pages);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_phone;
      
        protected int m_max_baud;
      
        protected DateTime m_d_start;
      
        protected String m_t_start;
      
        protected DateTime m_d_end;
      
        protected String m_t_end;
      
        protected String m_pager_num;
      
        protected String m_message;
      
        protected String m_response;
      
        protected int m_status;
      
        protected int m_seq_num;
      
        protected String m_ticket_num;
      
        protected int m_count;
      
        protected int m_station;
      
        protected String m_email_dom;
      
      #endregion

      #region Constructors
      public pages(
      
      )
      {
      
      }

      


        public pages(
        String 
          phone,int 
          max_baud,DateTime 
          d_start,String 
          t_start,DateTime 
          d_end,String 
          t_end,String 
          pager_num,String 
          message,String 
          response,int 
          status,int 
          seq_num,String 
          ticket_num,int 
          count,int 
          station,String 
          email_dom
        )
        {
        
          m_phone = phone;
        
          m_max_baud = max_baud;
        
          m_d_start = d_start;
        
          m_t_start = t_start;
        
          m_d_end = d_end;
        
          m_t_end = t_end;
        
          m_pager_num = pager_num;
        
          m_message = message;
        
          m_response = response;
        
          m_status = status;
        
          m_seq_num = seq_num;
        
          m_ticket_num = ticket_num;
        
          m_count = count;
        
          m_station = station;
        
          m_email_dom = email_dom;
        
        }


      
      #endregion

      
        [XmlElement]
        public String phone
        {
        get { return m_phone;}
        set { m_phone = value; }
        }
      
        [XmlElement]
        public int max_baud
        {
        get { return m_max_baud;}
        set { m_max_baud = value; }
        }
      
        [XmlElement]
        public DateTime d_start
        {
        get { return m_d_start;}
        set { m_d_start = value; }
        }
      
        [XmlElement]
        public String t_start
        {
        get { return m_t_start;}
        set { m_t_start = value; }
        }
      
        [XmlElement]
        public DateTime d_end
        {
        get { return m_d_end;}
        set { m_d_end = value; }
        }
      
        [XmlElement]
        public String t_end
        {
        get { return m_t_end;}
        set { m_t_end = value; }
        }
      
        [XmlElement]
        public String pager_num
        {
        get { return m_pager_num;}
        set { m_pager_num = value; }
        }
      
        [XmlElement]
        public String message
        {
        get { return m_message;}
        set { m_message = value; }
        }
      
        [XmlElement]
        public String response
        {
        get { return m_response;}
        set { m_response = value; }
        }
      
        [XmlElement]
        public int status
        {
        get { return m_status;}
        set { m_status = value; }
        }
      
        [XmlElement]
        public int seq_num
        {
        get { return m_seq_num;}
        set { m_seq_num = value; }
        }
      
        [XmlElement]
        public String ticket_num
        {
        get { return m_ticket_num;}
        set { m_ticket_num = value; }
        }
      
        [XmlElement]
        public int count
        {
        get { return m_count;}
        set { m_count = value; }
        }
      
        [XmlElement]
        public int station
        {
        get { return m_station;}
        set { m_station = value; }
        }
      
        [XmlElement]
        public String email_dom
        {
        get { return m_email_dom;}
        set { m_email_dom = value; }
        }
      
      }
      #endregion
      }

    