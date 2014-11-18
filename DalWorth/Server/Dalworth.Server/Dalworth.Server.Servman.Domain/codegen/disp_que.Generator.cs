
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


      public partial class disp_que
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into disp_que ( " +
      
        " ticket_num, " +
      
        " customer, " +
      
        " d_dispatch, " +
      
        " t_dispatch, " +
      
        " t_estcomp, " +
      
        " t_complete, " +
      
        " tag, " +
      
        " tech_id, " +
      
        " error, " +
      
        " serv_type, " +
      
        " amount, " +
      
        " comp_type, " +
      
        " phone, " +
      
        " arival, " +
      
        " span, " +
      
        " time_stat, " +
      
        " note, " +
      
        " locked, " +
      
        " order, " +
      
        " grid, " +
      
        " t_expire, " +
      
        " hold, " +
      
        " auto_disp, " +
      
        " auto_time, " +
      
        " auto_dispd, " +
      
        " auto_arrvd, " +
      
        " auto_compd " +
      
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
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(disp_que disp_que)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", disp_que.ticket_num);
      
        Database.PutParameter(dbCommand,"@customer", disp_que.customer);
      
        Database.PutParameter(dbCommand,"@d_dispatch", disp_que.d_dispatch);
      
        Database.PutParameter(dbCommand,"@t_dispatch", disp_que.t_dispatch);
      
        Database.PutParameter(dbCommand,"@t_estcomp", disp_que.t_estcomp);
      
        Database.PutParameter(dbCommand,"@t_complete", disp_que.t_complete);
      
        Database.PutParameter(dbCommand,"@tag", disp_que.tag);
      
        Database.PutParameter(dbCommand,"@tech_id", disp_que.tech_id);
      
        Database.PutParameter(dbCommand,"@error", disp_que.error);
      
        Database.PutParameter(dbCommand,"@serv_type", disp_que.serv_type);
      
        Database.PutParameter(dbCommand,"@amount", disp_que.amount);
      
        Database.PutParameter(dbCommand,"@comp_type", disp_que.comp_type);
      
        Database.PutParameter(dbCommand,"@phone", disp_que.phone);
      
        Database.PutParameter(dbCommand,"@arival", disp_que.arival);
      
        Database.PutParameter(dbCommand,"@span", disp_que.span);
      
        Database.PutParameter(dbCommand,"@time_stat", disp_que.time_stat);
      
        Database.PutParameter(dbCommand,"@note", disp_que.note);
      
        Database.PutParameter(dbCommand,"@locked", disp_que.locked);
      
        Database.PutParameter(dbCommand,"@order", disp_que.order);
      
        Database.PutParameter(dbCommand,"@grid", disp_que.grid);
      
        Database.PutParameter(dbCommand,"@t_expire", disp_que.t_expire);
      
        Database.PutParameter(dbCommand,"@hold", disp_que.hold);
      
        Database.PutParameter(dbCommand,"@auto_disp", disp_que.auto_disp);
      
        Database.PutParameter(dbCommand,"@auto_time", disp_que.auto_time);
      
        Database.PutParameter(dbCommand,"@auto_dispd", disp_que.auto_dispd);
      
        Database.PutParameter(dbCommand,"@auto_arrvd", disp_que.auto_arrvd);
      
        Database.PutParameter(dbCommand,"@auto_compd", disp_que.auto_compd);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<disp_que>  disp_queList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(disp_que disp_que in  disp_queList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ticket_num", disp_que.ticket_num);
      
        Database.PutParameter(dbCommand,"@customer", disp_que.customer);
      
        Database.PutParameter(dbCommand,"@d_dispatch", disp_que.d_dispatch);
      
        Database.PutParameter(dbCommand,"@t_dispatch", disp_que.t_dispatch);
      
        Database.PutParameter(dbCommand,"@t_estcomp", disp_que.t_estcomp);
      
        Database.PutParameter(dbCommand,"@t_complete", disp_que.t_complete);
      
        Database.PutParameter(dbCommand,"@tag", disp_que.tag);
      
        Database.PutParameter(dbCommand,"@tech_id", disp_que.tech_id);
      
        Database.PutParameter(dbCommand,"@error", disp_que.error);
      
        Database.PutParameter(dbCommand,"@serv_type", disp_que.serv_type);
      
        Database.PutParameter(dbCommand,"@amount", disp_que.amount);
      
        Database.PutParameter(dbCommand,"@comp_type", disp_que.comp_type);
      
        Database.PutParameter(dbCommand,"@phone", disp_que.phone);
      
        Database.PutParameter(dbCommand,"@arival", disp_que.arival);
      
        Database.PutParameter(dbCommand,"@span", disp_que.span);
      
        Database.PutParameter(dbCommand,"@time_stat", disp_que.time_stat);
      
        Database.PutParameter(dbCommand,"@note", disp_que.note);
      
        Database.PutParameter(dbCommand,"@locked", disp_que.locked);
      
        Database.PutParameter(dbCommand,"@order", disp_que.order);
      
        Database.PutParameter(dbCommand,"@grid", disp_que.grid);
      
        Database.PutParameter(dbCommand,"@t_expire", disp_que.t_expire);
      
        Database.PutParameter(dbCommand,"@hold", disp_que.hold);
      
        Database.PutParameter(dbCommand,"@auto_disp", disp_que.auto_disp);
      
        Database.PutParameter(dbCommand,"@auto_time", disp_que.auto_time);
      
        Database.PutParameter(dbCommand,"@auto_dispd", disp_que.auto_dispd);
      
        Database.PutParameter(dbCommand,"@auto_arrvd", disp_que.auto_arrvd);
      
        Database.PutParameter(dbCommand,"@auto_compd", disp_que.auto_compd);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ticket_num",disp_que.ticket_num);
      
        Database.UpdateParameter(dbCommand,"@customer",disp_que.customer);
      
        Database.UpdateParameter(dbCommand,"@d_dispatch",disp_que.d_dispatch);
      
        Database.UpdateParameter(dbCommand,"@t_dispatch",disp_que.t_dispatch);
      
        Database.UpdateParameter(dbCommand,"@t_estcomp",disp_que.t_estcomp);
      
        Database.UpdateParameter(dbCommand,"@t_complete",disp_que.t_complete);
      
        Database.UpdateParameter(dbCommand,"@tag",disp_que.tag);
      
        Database.UpdateParameter(dbCommand,"@tech_id",disp_que.tech_id);
      
        Database.UpdateParameter(dbCommand,"@error",disp_que.error);
      
        Database.UpdateParameter(dbCommand,"@serv_type",disp_que.serv_type);
      
        Database.UpdateParameter(dbCommand,"@amount",disp_que.amount);
      
        Database.UpdateParameter(dbCommand,"@comp_type",disp_que.comp_type);
      
        Database.UpdateParameter(dbCommand,"@phone",disp_que.phone);
      
        Database.UpdateParameter(dbCommand,"@arival",disp_que.arival);
      
        Database.UpdateParameter(dbCommand,"@span",disp_que.span);
      
        Database.UpdateParameter(dbCommand,"@time_stat",disp_que.time_stat);
      
        Database.UpdateParameter(dbCommand,"@note",disp_que.note);
      
        Database.UpdateParameter(dbCommand,"@locked",disp_que.locked);
      
        Database.UpdateParameter(dbCommand,"@order",disp_que.order);
      
        Database.UpdateParameter(dbCommand,"@grid",disp_que.grid);
      
        Database.UpdateParameter(dbCommand,"@t_expire",disp_que.t_expire);
      
        Database.UpdateParameter(dbCommand,"@hold",disp_que.hold);
      
        Database.UpdateParameter(dbCommand,"@auto_disp",disp_que.auto_disp);
      
        Database.UpdateParameter(dbCommand,"@auto_time",disp_que.auto_time);
      
        Database.UpdateParameter(dbCommand,"@auto_dispd",disp_que.auto_dispd);
      
        Database.UpdateParameter(dbCommand,"@auto_arrvd",disp_que.auto_arrvd);
      
        Database.UpdateParameter(dbCommand,"@auto_compd",disp_que.auto_compd);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update disp_que Set "
      
        + " disp_que.customer = ? , "
      
        + " disp_que.d_dispatch = ? , "
      
        + " disp_que.t_dispatch = ? , "
      
        + " disp_que.t_estcomp = ? , "
      
        + " disp_que.t_complete = ? , "
      
        + " disp_que.tag = ? , "
      
        + " disp_que.tech_id = ? , "
      
        + " disp_que.error = ? , "
      
        + " disp_que.serv_type = ? , "
      
        + " disp_que.amount = ? , "
      
        + " disp_que.comp_type = ? , "
      
        + " disp_que.phone = ? , "
      
        + " disp_que.arival = ? , "
      
        + " disp_que.span = ? , "
      
        + " disp_que.time_stat = ? , "
      
        + " disp_que.note = ? , "
      
        + " disp_que.locked = ? , "
      
        + " disp_que.order = ? , "
      
        + " disp_que.grid = ? , "
      
        + " disp_que.t_expire = ? , "
      
        + " disp_que.hold = ? , "
      
        + " disp_que.auto_disp = ? , "
      
        + " disp_que.auto_time = ? , "
      
        + " disp_que.auto_dispd = ? , "
      
        + " disp_que.auto_arrvd = ? , "
      
        + " disp_que.auto_compd = ?  "
      
        + " Where "
        
          + " disp_que.ticket_num = ?  "
        
      ;

      public static void Update(disp_que disp_que)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@customer", disp_que.customer);
      
        Database.PutParameter(dbCommand,"@d_dispatch", disp_que.d_dispatch);
      
        Database.PutParameter(dbCommand,"@t_dispatch", disp_que.t_dispatch);
      
        Database.PutParameter(dbCommand,"@t_estcomp", disp_que.t_estcomp);
      
        Database.PutParameter(dbCommand,"@t_complete", disp_que.t_complete);
      
        Database.PutParameter(dbCommand,"@tag", disp_que.tag);
      
        Database.PutParameter(dbCommand,"@tech_id", disp_que.tech_id);
      
        Database.PutParameter(dbCommand,"@error", disp_que.error);
      
        Database.PutParameter(dbCommand,"@serv_type", disp_que.serv_type);
      
        Database.PutParameter(dbCommand,"@amount", disp_que.amount);
      
        Database.PutParameter(dbCommand,"@comp_type", disp_que.comp_type);
      
        Database.PutParameter(dbCommand,"@phone", disp_que.phone);
      
        Database.PutParameter(dbCommand,"@arival", disp_que.arival);
      
        Database.PutParameter(dbCommand,"@span", disp_que.span);
      
        Database.PutParameter(dbCommand,"@time_stat", disp_que.time_stat);
      
        Database.PutParameter(dbCommand,"@note", disp_que.note);
      
        Database.PutParameter(dbCommand,"@locked", disp_que.locked);
      
        Database.PutParameter(dbCommand,"@order", disp_que.order);
      
        Database.PutParameter(dbCommand,"@grid", disp_que.grid);
      
        Database.PutParameter(dbCommand,"@t_expire", disp_que.t_expire);
      
        Database.PutParameter(dbCommand,"@hold", disp_que.hold);
      
        Database.PutParameter(dbCommand,"@auto_disp", disp_que.auto_disp);
      
        Database.PutParameter(dbCommand,"@auto_time", disp_que.auto_time);
      
        Database.PutParameter(dbCommand,"@auto_dispd", disp_que.auto_dispd);
      
        Database.PutParameter(dbCommand,"@auto_arrvd", disp_que.auto_arrvd);
      
        Database.PutParameter(dbCommand,"@auto_compd", disp_que.auto_compd);
      
        Database.PutParameter(dbCommand,"@ticket_num", disp_que.ticket_num);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " disp_que.ticket_num, "
      
        + " disp_que.customer, "
      
        + " disp_que.d_dispatch, "
      
        + " disp_que.t_dispatch, "
      
        + " disp_que.t_estcomp, "
      
        + " disp_que.t_complete, "
      
        + " disp_que.tag, "
      
        + " disp_que.tech_id, "
      
        + " disp_que.error, "
      
        + " disp_que.serv_type, "
      
        + " disp_que.amount, "
      
        + " disp_que.comp_type, "
      
        + " disp_que.phone, "
      
        + " disp_que.arival, "
      
        + " disp_que.span, "
      
        + " disp_que.time_stat, "
      
        + " disp_que.note, "
      
        + " disp_que.locked, "
      
        + " disp_que.order, "
      
        + " disp_que.grid, "
      
        + " disp_que.t_expire, "
      
        + " disp_que.hold, "
      
        + " disp_que.auto_disp, "
      
        + " disp_que.auto_time, "
      
        + " disp_que.auto_dispd, "
      
        + " disp_que.auto_arrvd, "
      
        + " disp_que.auto_compd "
      

      + " From disp_que "

      
        + " Where "

          + " disp_que.ticket_num = ?  "
        
      ;

      public static disp_que FindByPrimaryKey(
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
      throw new DataNotFoundException("disp_que not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(disp_que disp_que)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@ticket_num",disp_que.ticket_num);
      

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
      String sql = "select 1 from disp_que";

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

      public static disp_que Load(IDataReader dataReader)
      {
      disp_que disp_que = new disp_que();

          disp_que.ticket_num = dataReader.GetString(0);
          disp_que.customer = dataReader.GetString(1);
          disp_que.d_dispatch = dataReader.GetDateTime(2);
          disp_que.t_dispatch = dataReader.GetString(3);
          disp_que.t_estcomp = dataReader.GetString(4);
          disp_que.t_complete = dataReader.GetString(5);
          disp_que.tag = dataReader.GetBoolean(6);
          disp_que.tech_id = dataReader.GetString(7);
          disp_que.error = dataReader.GetBoolean(8);
          disp_que.serv_type = dataReader.GetInt32(9);
          disp_que.amount = dataReader.GetFloat(10);
          disp_que.comp_type = dataReader.GetInt32(11);
          disp_que.phone = dataReader.GetString(12);
          disp_que.arival = dataReader.GetString(13);
          disp_que.span = dataReader.GetString(14);
          disp_que.time_stat = dataReader.GetString(15);
          disp_que.note = dataReader.GetString(16);
          disp_que.locked = dataReader.GetBoolean(17);
          disp_que.order = dataReader.GetString(18);
          disp_que.grid = dataReader.GetString(19);
          disp_que.t_expire = dataReader.GetInt32(20);
          disp_que.hold = dataReader.GetBoolean(21);
          disp_que.auto_disp = dataReader.GetBoolean(22);
          disp_que.auto_time = dataReader.GetString(23);
          disp_que.auto_dispd = dataReader.GetBoolean(24);
          disp_que.auto_arrvd = dataReader.GetBoolean(25);
          disp_que.auto_compd = dataReader.GetBoolean(26);
          

      return disp_que;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [disp_que] "

      
        + " Where "
        
          + " ticket_num = ?  "
        
      ;
      public static void Delete(disp_que disp_que)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@ticket_num", disp_que.ticket_num);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [disp_que] ";

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

      
        + " disp_que.ticket_num, "
      
        + " disp_que.customer, "
      
        + " disp_que.d_dispatch, "
      
        + " disp_que.t_dispatch, "
      
        + " disp_que.t_estcomp, "
      
        + " disp_que.t_complete, "
      
        + " disp_que.tag, "
      
        + " disp_que.tech_id, "
      
        + " disp_que.error, "
      
        + " disp_que.serv_type, "
      
        + " disp_que.amount, "
      
        + " disp_que.comp_type, "
      
        + " disp_que.phone, "
      
        + " disp_que.arival, "
      
        + " disp_que.span, "
      
        + " disp_que.time_stat, "
      
        + " disp_que.note, "
      
        + " disp_que.locked, "
      
        + " disp_que.order, "
      
        + " disp_que.grid, "
      
        + " disp_que.t_expire, "
      
        + " disp_que.hold, "
      
        + " disp_que.auto_disp, "
      
        + " disp_que.auto_time, "
      
        + " disp_que.auto_dispd, "
      
        + " disp_que.auto_arrvd, "
      
        + " disp_que.auto_compd "
      

      + " From disp_que ";
      public static List<disp_que> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<disp_que> rv = new List<disp_que>();

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
      List<disp_que> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<disp_que> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(disp_que));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(disp_que item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<disp_que>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(disp_que));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<disp_que> itemsList
      = new List<disp_que>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is disp_que)
      itemsList.Add(deserializedObject as disp_que);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_ticket_num;
      
        protected String m_customer;
      
        protected DateTime m_d_dispatch;
      
        protected String m_t_dispatch;
      
        protected String m_t_estcomp;
      
        protected String m_t_complete;
      
        protected bool m_tag;
      
        protected String m_tech_id;
      
        protected bool m_error;
      
        protected int m_serv_type;
      
        protected float m_amount;
      
        protected int m_comp_type;
      
        protected String m_phone;
      
        protected String m_arival;
      
        protected String m_span;
      
        protected String m_time_stat;
      
        protected String m_note;
      
        protected bool m_locked;
      
        protected String m_order;
      
        protected String m_grid;
      
        protected int m_t_expire;
      
        protected bool m_hold;
      
        protected bool m_auto_disp;
      
        protected String m_auto_time;
      
        protected bool m_auto_dispd;
      
        protected bool m_auto_arrvd;
      
        protected bool m_auto_compd;
      
      #endregion

      #region Constructors
      public disp_que(
      String 
          ticket_num
      )
      {
      
        m_ticket_num = ticket_num;
      
      }

      


        public disp_que(
        String 
          ticket_num,String 
          customer,DateTime 
          d_dispatch,String 
          t_dispatch,String 
          t_estcomp,String 
          t_complete,bool 
          tag,String 
          tech_id,bool 
          error,int 
          serv_type,float 
          amount,int 
          comp_type,String 
          phone,String 
          arival,String 
          span,String 
          time_stat,String 
          note,bool 
          locked,String 
          order,String 
          grid,int 
          t_expire,bool 
          hold,bool 
          auto_disp,String 
          auto_time,bool 
          auto_dispd,bool 
          auto_arrvd,bool 
          auto_compd
        )
        {
        
          m_ticket_num = ticket_num;
        
          m_customer = customer;
        
          m_d_dispatch = d_dispatch;
        
          m_t_dispatch = t_dispatch;
        
          m_t_estcomp = t_estcomp;
        
          m_t_complete = t_complete;
        
          m_tag = tag;
        
          m_tech_id = tech_id;
        
          m_error = error;
        
          m_serv_type = serv_type;
        
          m_amount = amount;
        
          m_comp_type = comp_type;
        
          m_phone = phone;
        
          m_arival = arival;
        
          m_span = span;
        
          m_time_stat = time_stat;
        
          m_note = note;
        
          m_locked = locked;
        
          m_order = order;
        
          m_grid = grid;
        
          m_t_expire = t_expire;
        
          m_hold = hold;
        
          m_auto_disp = auto_disp;
        
          m_auto_time = auto_time;
        
          m_auto_dispd = auto_dispd;
        
          m_auto_arrvd = auto_arrvd;
        
          m_auto_compd = auto_compd;
        
        }


      
      #endregion

      
        [XmlElement]
        public String ticket_num
        {
        get { return m_ticket_num;}
        set { m_ticket_num = value; }
        }
      
        [XmlElement]
        public String customer
        {
        get { return m_customer;}
        set { m_customer = value; }
        }
      
        [XmlElement]
        public DateTime d_dispatch
        {
        get { return m_d_dispatch;}
        set { m_d_dispatch = value; }
        }
      
        [XmlElement]
        public String t_dispatch
        {
        get { return m_t_dispatch;}
        set { m_t_dispatch = value; }
        }
      
        [XmlElement]
        public String t_estcomp
        {
        get { return m_t_estcomp;}
        set { m_t_estcomp = value; }
        }
      
        [XmlElement]
        public String t_complete
        {
        get { return m_t_complete;}
        set { m_t_complete = value; }
        }
      
        [XmlElement]
        public bool tag
        {
        get { return m_tag;}
        set { m_tag = value; }
        }
      
        [XmlElement]
        public String tech_id
        {
        get { return m_tech_id;}
        set { m_tech_id = value; }
        }
      
        [XmlElement]
        public bool error
        {
        get { return m_error;}
        set { m_error = value; }
        }
      
        [XmlElement]
        public int serv_type
        {
        get { return m_serv_type;}
        set { m_serv_type = value; }
        }
      
        [XmlElement]
        public float amount
        {
        get { return m_amount;}
        set { m_amount = value; }
        }
      
        [XmlElement]
        public int comp_type
        {
        get { return m_comp_type;}
        set { m_comp_type = value; }
        }
      
        [XmlElement]
        public String phone
        {
        get { return m_phone;}
        set { m_phone = value; }
        }
      
        [XmlElement]
        public String arival
        {
        get { return m_arival;}
        set { m_arival = value; }
        }
      
        [XmlElement]
        public String span
        {
        get { return m_span;}
        set { m_span = value; }
        }
      
        [XmlElement]
        public String time_stat
        {
        get { return m_time_stat;}
        set { m_time_stat = value; }
        }
      
        [XmlElement]
        public String note
        {
        get { return m_note;}
        set { m_note = value; }
        }
      
        [XmlElement]
        public bool locked
        {
        get { return m_locked;}
        set { m_locked = value; }
        }
      
        [XmlElement]
        public String order
        {
        get { return m_order;}
        set { m_order = value; }
        }
      
        [XmlElement]
        public String grid
        {
        get { return m_grid;}
        set { m_grid = value; }
        }
      
        [XmlElement]
        public int t_expire
        {
        get { return m_t_expire;}
        set { m_t_expire = value; }
        }
      
        [XmlElement]
        public bool hold
        {
        get { return m_hold;}
        set { m_hold = value; }
        }
      
        [XmlElement]
        public bool auto_disp
        {
        get { return m_auto_disp;}
        set { m_auto_disp = value; }
        }
      
        [XmlElement]
        public String auto_time
        {
        get { return m_auto_time;}
        set { m_auto_time = value; }
        }
      
        [XmlElement]
        public bool auto_dispd
        {
        get { return m_auto_dispd;}
        set { m_auto_dispd = value; }
        }
      
        [XmlElement]
        public bool auto_arrvd
        {
        get { return m_auto_arrvd;}
        set { m_auto_arrvd = value; }
        }
      
        [XmlElement]
        public bool auto_compd
        {
        get { return m_auto_compd;}
        set { m_auto_compd = value; }
        }
      
      }
      #endregion
      }

    