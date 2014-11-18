
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


      public partial class timeslot
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into timeslot ( " +
      
        " timeslotid, " +
      
        " servgrpid, " +
      
        " desc, " +
      
        " order, " +
      
        " misctime " +
      
      ") Values (" +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(timeslot timeslot)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@timeslotid", timeslot.timeslotid);
      
        Database.PutParameter(dbCommand,"@servgrpid", timeslot.servgrpid);
      
        Database.PutParameter(dbCommand,"@desc", timeslot.desc);
      
        Database.PutParameter(dbCommand,"@order", timeslot.order);
      
        Database.PutParameter(dbCommand,"@misctime", timeslot.misctime);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<timeslot>  timeslotList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(timeslot timeslot in  timeslotList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@timeslotid", timeslot.timeslotid);
      
        Database.PutParameter(dbCommand,"@servgrpid", timeslot.servgrpid);
      
        Database.PutParameter(dbCommand,"@desc", timeslot.desc);
      
        Database.PutParameter(dbCommand,"@order", timeslot.order);
      
        Database.PutParameter(dbCommand,"@misctime", timeslot.misctime);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@timeslotid",timeslot.timeslotid);
      
        Database.UpdateParameter(dbCommand,"@servgrpid",timeslot.servgrpid);
      
        Database.UpdateParameter(dbCommand,"@desc",timeslot.desc);
      
        Database.UpdateParameter(dbCommand,"@order",timeslot.order);
      
        Database.UpdateParameter(dbCommand,"@misctime",timeslot.misctime);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update timeslot Set "
      
        + " timeslot.servgrpid = ? , "
      
        + " timeslot.desc = ? , "
      
        + " timeslot.order = ? , "
      
        + " timeslot.misctime = ?  "
      
        + " Where "
        
          + " timeslot.timeslotid = ?  "
        
      ;

      public static void Update(timeslot timeslot)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@servgrpid", timeslot.servgrpid);
      
        Database.PutParameter(dbCommand,"@desc", timeslot.desc);
      
        Database.PutParameter(dbCommand,"@order", timeslot.order);
      
        Database.PutParameter(dbCommand,"@misctime", timeslot.misctime);
      
        Database.PutParameter(dbCommand,"@timeslotid", timeslot.timeslotid);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " timeslot.timeslotid, "
      
        + " timeslot.servgrpid, "
      
        + " timeslot.desc, "
      
        + " timeslot.order, "
      
        + " timeslot.misctime "
      

      + " From timeslot "

      
        + " Where "
        
          + " timeslot.timeslotid = ?  "
        
      ;

      public static timeslot FindByPrimaryKey(
      int timeslotid
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@timeslotid", timeslotid);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("timeslot not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(timeslot timeslot)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@timeslotid",timeslot.timeslotid);
      

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
      String sql = "select 1 from timeslot";

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

      public static timeslot Load(IDataReader dataReader)
      {
      timeslot timeslot = new timeslot();

      timeslot.timeslotid = dataReader.GetInt32(0);
          timeslot.servgrpid = dataReader.GetInt32(1);
          timeslot.desc = dataReader.GetString(2);
          timeslot.order = dataReader.GetInt32(3);
          timeslot.misctime = dataReader.GetBoolean(4);
          

      return timeslot;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [timeslot] "

      
        + " Where "
        
          + " timeslotid = ?  "
        
      ;
      public static void Delete(timeslot timeslot)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@timeslotid", timeslot.timeslotid);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [timeslot] ";

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

      
        + " timeslot.timeslotid, "
      
        + " timeslot.servgrpid, "
      
        + " timeslot.desc, "
      
        + " timeslot.order, "
      
        + " timeslot.misctime "
      

      + " From timeslot ";
      public static List<timeslot> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<timeslot> rv = new List<timeslot>();

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
      List<timeslot> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<timeslot> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(timeslot));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(timeslot item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<timeslot>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(timeslot));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<timeslot> itemsList
      = new List<timeslot>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is timeslot)
      itemsList.Add(deserializedObject as timeslot);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_timeslotid;
      
        protected int m_servgrpid;
      
        protected String m_desc;
      
        protected int m_order;
      
        protected bool m_misctime;
      
      #endregion

      #region Constructors
      public timeslot(
      int 
          timeslotid
      )
      {
      
        m_timeslotid = timeslotid;
      
      }

      


        public timeslot(
        int 
          timeslotid,int 
          servgrpid,String 
          desc,int 
          order,bool 
          misctime
        )
        {
        
          m_timeslotid = timeslotid;
        
          m_servgrpid = servgrpid;
        
          m_desc = desc;
        
          m_order = order;
        
          m_misctime = misctime;
        
        }


      
      #endregion

      
        [XmlElement]
        public int timeslotid
        {
        get { return m_timeslotid;}
        set { m_timeslotid = value; }
        }
      
        [XmlElement]
        public int servgrpid
        {
        get { return m_servgrpid;}
        set { m_servgrpid = value; }
        }
      
        [XmlElement]
        public String desc
        {
        get { return m_desc;}
        set { m_desc = value; }
        }
      
        [XmlElement]
        public int order
        {
        get { return m_order;}
        set { m_order = value; }
        }
      
        [XmlElement]
        public bool misctime
        {
        get { return m_misctime;}
        set { m_misctime = value; }
        }
      
      }
      #endregion
      }

    