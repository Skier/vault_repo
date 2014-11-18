
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


      public partial class page_svc
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into page_svc ( " +
      
        " serv_id, " +
      
        " service, " +
      
        " phone, " +
      
        " max_baud, " +
      
        " email_dom " +
      
      ") Values (" +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(page_svc page_svc)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@serv_id", page_svc.serv_id);
      
        Database.PutParameter(dbCommand,"@service", page_svc.service);
      
        Database.PutParameter(dbCommand,"@phone", page_svc.phone);
      
        Database.PutParameter(dbCommand,"@max_baud", page_svc.max_baud);
      
        Database.PutParameter(dbCommand,"@email_dom", page_svc.email_dom);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<page_svc>  page_svcList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(page_svc page_svc in  page_svcList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@serv_id", page_svc.serv_id);
      
        Database.PutParameter(dbCommand,"@service", page_svc.service);
      
        Database.PutParameter(dbCommand,"@phone", page_svc.phone);
      
        Database.PutParameter(dbCommand,"@max_baud", page_svc.max_baud);
      
        Database.PutParameter(dbCommand,"@email_dom", page_svc.email_dom);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@serv_id",page_svc.serv_id);
      
        Database.UpdateParameter(dbCommand,"@service",page_svc.service);
      
        Database.UpdateParameter(dbCommand,"@phone",page_svc.phone);
      
        Database.UpdateParameter(dbCommand,"@max_baud",page_svc.max_baud);
      
        Database.UpdateParameter(dbCommand,"@email_dom",page_svc.email_dom);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update page_svc Set "
      
        + " page_svc.service = ? , "
      
        + " page_svc.phone = ? , "
      
        + " page_svc.max_baud = ? , "
      
        + " page_svc.email_dom = ?  "
      
        + " Where "
        
          + " page_svc.serv_id = ?  "
        
      ;

      public static void Update(page_svc page_svc)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@service", page_svc.service);
      
        Database.PutParameter(dbCommand,"@phone", page_svc.phone);
      
        Database.PutParameter(dbCommand,"@max_baud", page_svc.max_baud);
      
        Database.PutParameter(dbCommand,"@email_dom", page_svc.email_dom);
      
        Database.PutParameter(dbCommand,"@serv_id", page_svc.serv_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " page_svc.serv_id, "
      
        + " page_svc.service, "
      
        + " page_svc.phone, "
      
        + " page_svc.max_baud, "
      
        + " page_svc.email_dom "
      

      + " From page_svc "

      
        + " Where "
        
          + " page_svc.serv_id = ?  "
        
      ;

      public static page_svc FindByPrimaryKey(
      int serv_id
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@serv_id", serv_id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("page_svc not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(page_svc page_svc)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@serv_id",page_svc.serv_id);
      

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
      String sql = "select 1 from page_svc";

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

      public static page_svc Load(IDataReader dataReader)
      {
      page_svc page_svc = new page_svc();

      page_svc.serv_id = dataReader.GetInt32(0);
          page_svc.service = dataReader.GetString(1);
          page_svc.phone = dataReader.GetString(2);
          page_svc.max_baud = dataReader.GetInt32(3);
          page_svc.email_dom = dataReader.GetString(4);
          

      return page_svc;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [page_svc] "

      
        + " Where "
        
          + " serv_id = ?  "
        
      ;
      public static void Delete(page_svc page_svc)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@serv_id", page_svc.serv_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [page_svc] ";

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

      
        + " page_svc.serv_id, "
      
        + " page_svc.service, "
      
        + " page_svc.phone, "
      
        + " page_svc.max_baud, "
      
        + " page_svc.email_dom "
      

      + " From page_svc ";
      public static List<page_svc> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<page_svc> rv = new List<page_svc>();

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
      List<page_svc> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<page_svc> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(page_svc));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(page_svc item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<page_svc>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(page_svc));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<page_svc> itemsList
      = new List<page_svc>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is page_svc)
      itemsList.Add(deserializedObject as page_svc);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_serv_id;
      
        protected String m_service;
      
        protected String m_phone;
      
        protected int m_max_baud;
      
        protected String m_email_dom;
      
      #endregion

      #region Constructors
      public page_svc(
      int 
          serv_id
      )
      {
      
        m_serv_id = serv_id;
      
      }

      


        public page_svc(
        int 
          serv_id,String 
          service,String 
          phone,int 
          max_baud,String 
          email_dom
        )
        {
        
          m_serv_id = serv_id;
        
          m_service = service;
        
          m_phone = phone;
        
          m_max_baud = max_baud;
        
          m_email_dom = email_dom;
        
        }


      
      #endregion

      
        [XmlElement]
        public int serv_id
        {
        get { return m_serv_id;}
        set { m_serv_id = value; }
        }
      
        [XmlElement]
        public String service
        {
        get { return m_service;}
        set { m_service = value; }
        }
      
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
        public String email_dom
        {
        get { return m_email_dom;}
        set { m_email_dom = value; }
        }
      
      }
      #endregion
      }

    