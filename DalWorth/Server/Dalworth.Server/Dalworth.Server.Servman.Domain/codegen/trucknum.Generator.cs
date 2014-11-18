
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


      public partial class trucknum
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into trucknum ( " +
      
        " truck_id, " +
      
        " truck_num, " +
      
        " pager_id, " +
      
        " group_id, " +
      
        " area_num " +
      
      ") Values (" +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(trucknum trucknum)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@truck_id", trucknum.truck_id);
      
        Database.PutParameter(dbCommand,"@truck_num", trucknum.truck_num);
      
        Database.PutParameter(dbCommand,"@pager_id", trucknum.pager_id);
      
        Database.PutParameter(dbCommand,"@group_id", trucknum.group_id);
      
        Database.PutParameter(dbCommand,"@area_num", trucknum.area_num);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<trucknum>  trucknumList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(trucknum trucknum in  trucknumList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@truck_id", trucknum.truck_id);
      
        Database.PutParameter(dbCommand,"@truck_num", trucknum.truck_num);
      
        Database.PutParameter(dbCommand,"@pager_id", trucknum.pager_id);
      
        Database.PutParameter(dbCommand,"@group_id", trucknum.group_id);
      
        Database.PutParameter(dbCommand,"@area_num", trucknum.area_num);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@truck_id",trucknum.truck_id);
      
        Database.UpdateParameter(dbCommand,"@truck_num",trucknum.truck_num);
      
        Database.UpdateParameter(dbCommand,"@pager_id",trucknum.pager_id);
      
        Database.UpdateParameter(dbCommand,"@group_id",trucknum.group_id);
      
        Database.UpdateParameter(dbCommand,"@area_num",trucknum.area_num);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update trucknum Set "
      
        + " trucknum.truck_num = ? , "
      
        + " trucknum.pager_id = ? , "
      
        + " trucknum.group_id = ? , "
      
        + " trucknum.area_num = ?  "
      
        + " Where "
        
          + " trucknum.truck_id = ?  "
        
      ;

      public static void Update(trucknum trucknum)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@truck_num", trucknum.truck_num);
      
        Database.PutParameter(dbCommand,"@pager_id", trucknum.pager_id);
      
        Database.PutParameter(dbCommand,"@group_id", trucknum.group_id);
      
        Database.PutParameter(dbCommand,"@area_num", trucknum.area_num);
      
        Database.PutParameter(dbCommand,"@truck_id", trucknum.truck_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " trucknum.truck_id, "
      
        + " trucknum.truck_num, "
      
        + " trucknum.pager_id, "
      
        + " trucknum.group_id, "
      
        + " trucknum.area_num "
      

      + " From trucknum "

      
        + " Where "
        
          + " trucknum.truck_id = ?  "
        
      ;

      public static trucknum FindByPrimaryKey(
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
      throw new DataNotFoundException("trucknum not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(trucknum trucknum)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@truck_id",trucknum.truck_id);
      

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
      String sql = "select 1 from trucknum";

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

      public static trucknum Load(IDataReader dataReader)
      {
      trucknum trucknum = new trucknum();

      trucknum.truck_id = dataReader.GetString(0);
          trucknum.truck_num = dataReader.GetString(1);
          trucknum.pager_id = dataReader.GetString(2);
          trucknum.group_id = dataReader.GetInt32(3);
          trucknum.area_num = dataReader.GetString(4);
          

      return trucknum;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [trucknum] "

      
        + " Where "
        
          + " truck_id = ?  "
        
      ;
      public static void Delete(trucknum trucknum)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@truck_id", trucknum.truck_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [trucknum] ";

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

      
        + " trucknum.truck_id, "
      
        + " trucknum.truck_num, "
      
        + " trucknum.pager_id, "
      
        + " trucknum.group_id, "
      
        + " trucknum.area_num "
      

      + " From trucknum ";
      public static List<trucknum> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<trucknum> rv = new List<trucknum>();

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
      List<trucknum> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<trucknum> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(trucknum));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(trucknum item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<trucknum>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(trucknum));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<trucknum> itemsList
      = new List<trucknum>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is trucknum)
      itemsList.Add(deserializedObject as trucknum);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_truck_id;
      
        protected String m_truck_num;
      
        protected String m_pager_id;
      
        protected int m_group_id;
      
        protected String m_area_num;
      
      #endregion

      #region Constructors
      public trucknum(
      String 
          truck_id
      )
      {
      
        m_truck_id = truck_id;
      
      }

      


        public trucknum(
        String 
          truck_id,String 
          truck_num,String 
          pager_id,int 
          group_id,String 
          area_num
        )
        {
        
          m_truck_id = truck_id;
        
          m_truck_num = truck_num;
        
          m_pager_id = pager_id;
        
          m_group_id = group_id;
        
          m_area_num = area_num;
        
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
        public String pager_id
        {
        get { return m_pager_id;}
        set { m_pager_id = value; }
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
      
      }
      #endregion
      }

    