
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


      public partial class truck
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into truck ( " +
      
        " truck_id, " +
      
        " truck_num, " +
      
        " group_id, " +
      
        " area_num, " +
      
        " pager_num, " +
      
        " serv_id, " +
      
        " max_chars " +
      
      ") Values (" +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(truck truck)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@truck_id", truck.truck_id);
      
        Database.PutParameter(dbCommand,"@truck_num", truck.truck_num);
      
        Database.PutParameter(dbCommand,"@group_id", truck.group_id);
      
        Database.PutParameter(dbCommand,"@area_num", truck.area_num);
      
        Database.PutParameter(dbCommand,"@pager_num", truck.pager_num);
      
        Database.PutParameter(dbCommand,"@serv_id", truck.serv_id);
      
        Database.PutParameter(dbCommand,"@max_chars", truck.max_chars);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<truck>  truckList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(truck truck in  truckList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@truck_id", truck.truck_id);
      
        Database.PutParameter(dbCommand,"@truck_num", truck.truck_num);
      
        Database.PutParameter(dbCommand,"@group_id", truck.group_id);
      
        Database.PutParameter(dbCommand,"@area_num", truck.area_num);
      
        Database.PutParameter(dbCommand,"@pager_num", truck.pager_num);
      
        Database.PutParameter(dbCommand,"@serv_id", truck.serv_id);
      
        Database.PutParameter(dbCommand,"@max_chars", truck.max_chars);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@truck_id",truck.truck_id);
      
        Database.UpdateParameter(dbCommand,"@truck_num",truck.truck_num);
      
        Database.UpdateParameter(dbCommand,"@group_id",truck.group_id);
      
        Database.UpdateParameter(dbCommand,"@area_num",truck.area_num);
      
        Database.UpdateParameter(dbCommand,"@pager_num",truck.pager_num);
      
        Database.UpdateParameter(dbCommand,"@serv_id",truck.serv_id);
      
        Database.UpdateParameter(dbCommand,"@max_chars",truck.max_chars);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update truck Set "
      
        + " truck.truck_num = ? , "
      
        + " truck.group_id = ? , "
      
        + " truck.area_num = ? , "
      
        + " truck.pager_num = ? , "
      
        + " truck.serv_id = ? , "
      
        + " truck.max_chars = ?  "
      
        + " Where "
        
          + " truck.truck_id = ?  "
        
      ;

      public static void Update(truck truck)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@truck_num", truck.truck_num);
      
        Database.PutParameter(dbCommand,"@group_id", truck.group_id);
      
        Database.PutParameter(dbCommand,"@area_num", truck.area_num);
      
        Database.PutParameter(dbCommand,"@pager_num", truck.pager_num);
      
        Database.PutParameter(dbCommand,"@serv_id", truck.serv_id);
      
        Database.PutParameter(dbCommand,"@max_chars", truck.max_chars);
      
        Database.PutParameter(dbCommand,"@truck_id", truck.truck_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " truck.truck_id, "
      
        + " truck.truck_num, "
      
        + " truck.group_id, "
      
        + " truck.area_num, "
      
        + " truck.pager_num, "
      
        + " truck.serv_id, "
      
        + " truck.max_chars "
      

      + " From truck "

      
        + " Where "
        
          + " truck.truck_id = ?  "
        
      ;

      public static truck FindByPrimaryKey(
      String truck_id
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@truck_id", truck_id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("truck not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(truck truck)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@truck_id",truck.truck_id);
      

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
      String sql = "select 1 from truck";

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

      public static truck Load(IDataReader dataReader)
      {
      truck truck = new truck();

      truck.truck_id = dataReader.GetString(0);
          truck.truck_num = dataReader.GetString(1);
          truck.group_id = dataReader.GetInt32(2);
          truck.area_num = dataReader.GetString(3);
          truck.pager_num = dataReader.GetString(4);
          truck.serv_id = dataReader.GetInt32(5);
          truck.max_chars = dataReader.GetInt32(6);
          

      return truck;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [truck] "

      
        + " Where "
        
          + " truck_id = ?  "
        
      ;
      public static void Delete(truck truck)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@truck_id", truck.truck_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [truck] ";

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

      
        + " truck.truck_id, "
      
        + " truck.truck_num, "
      
        + " truck.group_id, "
      
        + " truck.area_num, "
      
        + " truck.pager_num, "
      
        + " truck.serv_id, "
      
        + " truck.max_chars "
      

      + " From truck ";
      public static List<truck> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<truck> rv = new List<truck>();

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
      List<truck> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<truck> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(truck));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(truck item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<truck>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(truck));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<truck> itemsList
      = new List<truck>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is truck)
      itemsList.Add(deserializedObject as truck);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_truck_id;
      
        protected String m_truck_num;
      
        protected int m_group_id;
      
        protected String m_area_num;
      
        protected String m_pager_num;
      
        protected int m_serv_id;
      
        protected int m_max_chars;
      
      #endregion

      #region Constructors
      public truck(
      String 
          truck_id
      )
      {
      
        m_truck_id = truck_id;
      
      }

      


        public truck(
        String 
          truck_id,String 
          truck_num,int 
          group_id,String 
          area_num,String 
          pager_num,int 
          serv_id,int 
          max_chars
        )
        {
        
          m_truck_id = truck_id;
        
          m_truck_num = truck_num;
        
          m_group_id = group_id;
        
          m_area_num = area_num;
        
          m_pager_num = pager_num;
        
          m_serv_id = serv_id;
        
          m_max_chars = max_chars;
        
        }


      
      #endregion

      
        [XmlElement]
        public String truck_id
        {
        get { return m_truck_id;}
        set { m_truck_id = value; }
        }
      
        [XmlElement]
        public String truck_num
        {
        get { return m_truck_num;}
        set { m_truck_num = value; }
        }
      
        [XmlElement]
        public int group_id
        {
        get { return m_group_id;}
        set { m_group_id = value; }
        }
      
        [XmlElement]
        public String area_num
        {
        get { return m_area_num;}
        set { m_area_num = value; }
        }
      
        [XmlElement]
        public String pager_num
        {
        get { return m_pager_num;}
        set { m_pager_num = value; }
        }
      
        [XmlElement]
        public int serv_id
        {
        get { return m_serv_id;}
        set { m_serv_id = value; }
        }
      
        [XmlElement]
        public int max_chars
        {
        get { return m_max_chars;}
        set { m_max_chars = value; }
        }
      
      }
      #endregion
      }

    