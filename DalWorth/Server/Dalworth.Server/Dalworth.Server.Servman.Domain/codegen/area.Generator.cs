
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


      public partial class area
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into area ( " +
      
        " area_id, " +
      
        " area_name, " +
      
        " h_scale, " +
      
        " v_scale, " +
      
        " rcc_min, " +
      
        " dc_min, " +
      
        " df_min, " +
      
        " ccc_min, " +
      
        " maxdiscjob, " +
      
        " area_num, " +
      
        " dealer_ad " +
      
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
      
        " ? " +
      
      ")";

      public static void Insert(area area)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@area_id", area.area_id);
      
        Database.PutParameter(dbCommand,"@area_name", area.area_name);
      
        Database.PutParameter(dbCommand,"@h_scale", area.h_scale);
      
        Database.PutParameter(dbCommand,"@v_scale", area.v_scale);
      
        Database.PutParameter(dbCommand,"@rcc_min", area.rcc_min);
      
        Database.PutParameter(dbCommand,"@dc_min", area.dc_min);
      
        Database.PutParameter(dbCommand,"@df_min", area.df_min);
      
        Database.PutParameter(dbCommand,"@ccc_min", area.ccc_min);
      
        Database.PutParameter(dbCommand,"@maxdiscjob", area.maxdiscjob);
      
        Database.PutParameter(dbCommand,"@area_num", area.area_num);
      
        Database.PutParameter(dbCommand,"@dealer_ad", area.dealer_ad);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<area>  areaList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(area area in  areaList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@area_id", area.area_id);
      
        Database.PutParameter(dbCommand,"@area_name", area.area_name);
      
        Database.PutParameter(dbCommand,"@h_scale", area.h_scale);
      
        Database.PutParameter(dbCommand,"@v_scale", area.v_scale);
      
        Database.PutParameter(dbCommand,"@rcc_min", area.rcc_min);
      
        Database.PutParameter(dbCommand,"@dc_min", area.dc_min);
      
        Database.PutParameter(dbCommand,"@df_min", area.df_min);
      
        Database.PutParameter(dbCommand,"@ccc_min", area.ccc_min);
      
        Database.PutParameter(dbCommand,"@maxdiscjob", area.maxdiscjob);
      
        Database.PutParameter(dbCommand,"@area_num", area.area_num);
      
        Database.PutParameter(dbCommand,"@dealer_ad", area.dealer_ad);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@area_id",area.area_id);
      
        Database.UpdateParameter(dbCommand,"@area_name",area.area_name);
      
        Database.UpdateParameter(dbCommand,"@h_scale",area.h_scale);
      
        Database.UpdateParameter(dbCommand,"@v_scale",area.v_scale);
      
        Database.UpdateParameter(dbCommand,"@rcc_min",area.rcc_min);
      
        Database.UpdateParameter(dbCommand,"@dc_min",area.dc_min);
      
        Database.UpdateParameter(dbCommand,"@df_min",area.df_min);
      
        Database.UpdateParameter(dbCommand,"@ccc_min",area.ccc_min);
      
        Database.UpdateParameter(dbCommand,"@maxdiscjob",area.maxdiscjob);
      
        Database.UpdateParameter(dbCommand,"@area_num",area.area_num);
      
        Database.UpdateParameter(dbCommand,"@dealer_ad",area.dealer_ad);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update area Set "
      
        + " area.area_name = ? , "
      
        + " area.h_scale = ? , "
      
        + " area.v_scale = ? , "
      
        + " area.rcc_min = ? , "
      
        + " area.dc_min = ? , "
      
        + " area.df_min = ? , "
      
        + " area.ccc_min = ? , "
      
        + " area.maxdiscjob = ? , "
      
        + " area.area_num = ? , "
      
        + " area.dealer_ad = ?  "
      
        + " Where "
        
          + " area.area_id = ?  "
        
      ;

      public static void Update(area area)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@area_name", area.area_name);
      
        Database.PutParameter(dbCommand,"@h_scale", area.h_scale);
      
        Database.PutParameter(dbCommand,"@v_scale", area.v_scale);
      
        Database.PutParameter(dbCommand,"@rcc_min", area.rcc_min);
      
        Database.PutParameter(dbCommand,"@dc_min", area.dc_min);
      
        Database.PutParameter(dbCommand,"@df_min", area.df_min);
      
        Database.PutParameter(dbCommand,"@ccc_min", area.ccc_min);
      
        Database.PutParameter(dbCommand,"@maxdiscjob", area.maxdiscjob);
      
        Database.PutParameter(dbCommand,"@area_num", area.area_num);
      
        Database.PutParameter(dbCommand,"@dealer_ad", area.dealer_ad);
      
        Database.PutParameter(dbCommand,"@area_id", area.area_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " area.area_id, "
      
        + " area.area_name, "
      
        + " area.h_scale, "
      
        + " area.v_scale, "
      
        + " area.rcc_min, "
      
        + " area.dc_min, "
      
        + " area.df_min, "
      
        + " area.ccc_min, "
      
        + " area.maxdiscjob, "
      
        + " area.area_num, "
      
        + " area.dealer_ad "
      

      + " From area "

      
        + " Where "
        
          + " area.area_id = ?  "
        
      ;

      public static area FindByPrimaryKey(
      String area_id
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@area_id", area_id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("area not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(area area)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@area_id",area.area_id);
      

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
      String sql = "select 1 from area";

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

      public static area Load(IDataReader dataReader)
      {
      area area = new area();

      area.area_id = dataReader.GetString(0);
          area.area_name = dataReader.GetString(1);
          area.h_scale = dataReader.GetFloat(2);
          area.v_scale = dataReader.GetFloat(3);
          area.rcc_min = dataReader.GetFloat(4);
          area.dc_min = dataReader.GetFloat(5);
          area.df_min = dataReader.GetFloat(6);
          area.ccc_min = dataReader.GetFloat(7);
          area.maxdiscjob = dataReader.GetInt32(8);
          area.area_num = dataReader.GetString(9);
          area.dealer_ad = dataReader.GetString(10);
          

      return area;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [area] "

      
        + " Where "
        
          + " area_id = ?  "
        
      ;
      public static void Delete(area area)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@area_id", area.area_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [area] ";

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

      
        + " area.area_id, "
      
        + " area.area_name, "
      
        + " area.h_scale, "
      
        + " area.v_scale, "
      
        + " area.rcc_min, "
      
        + " area.dc_min, "
      
        + " area.df_min, "
      
        + " area.ccc_min, "
      
        + " area.maxdiscjob, "
      
        + " area.area_num, "
      
        + " area.dealer_ad "
      

      + " From area ";
      public static List<area> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<area> rv = new List<area>();

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
      List<area> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<area> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(area));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(area item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<area>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(area));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<area> itemsList
      = new List<area>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is area)
      itemsList.Add(deserializedObject as area);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_area_id;
      
        protected String m_area_name;
      
        protected float m_h_scale;
      
        protected float m_v_scale;
      
        protected float m_rcc_min;
      
        protected float m_dc_min;
      
        protected float m_df_min;
      
        protected float m_ccc_min;
      
        protected int m_maxdiscjob;
      
        protected String m_area_num;
      
        protected String m_dealer_ad;
      
      #endregion

      #region Constructors
      public area(
      String 
          area_id
      )
      {
      
        m_area_id = area_id;
      
      }

      


        public area(
        String 
          area_id,String 
          area_name,float 
          h_scale,float 
          v_scale,float 
          rcc_min,float 
          dc_min,float 
          df_min,float 
          ccc_min,int 
          maxdiscjob,String 
          area_num,String 
          dealer_ad
        )
        {
        
          m_area_id = area_id;
        
          m_area_name = area_name;
        
          m_h_scale = h_scale;
        
          m_v_scale = v_scale;
        
          m_rcc_min = rcc_min;
        
          m_dc_min = dc_min;
        
          m_df_min = df_min;
        
          m_ccc_min = ccc_min;
        
          m_maxdiscjob = maxdiscjob;
        
          m_area_num = area_num;
        
          m_dealer_ad = dealer_ad;
        
        }


      
      #endregion

      
        [XmlElement]
        public String area_id
        {
        get { return m_area_id;}
        set { m_area_id = value; }
        }
      
        [XmlElement]
        public String area_name
        {
        get { return m_area_name;}
        set { m_area_name = value; }
        }
      
        [XmlElement]
        public float h_scale
        {
        get { return m_h_scale;}
        set { m_h_scale = value; }
        }
      
        [XmlElement]
        public float v_scale
        {
        get { return m_v_scale;}
        set { m_v_scale = value; }
        }
      
        [XmlElement]
        public float rcc_min
        {
        get { return m_rcc_min;}
        set { m_rcc_min = value; }
        }
      
        [XmlElement]
        public float dc_min
        {
        get { return m_dc_min;}
        set { m_dc_min = value; }
        }
      
        [XmlElement]
        public float df_min
        {
        get { return m_df_min;}
        set { m_df_min = value; }
        }
      
        [XmlElement]
        public float ccc_min
        {
        get { return m_ccc_min;}
        set { m_ccc_min = value; }
        }
      
        [XmlElement]
        public int maxdiscjob
        {
        get { return m_maxdiscjob;}
        set { m_maxdiscjob = value; }
        }
      
        [XmlElement]
        public String area_num
        {
        get { return m_area_num;}
        set { m_area_num = value; }
        }
      
        [XmlElement]
        public String dealer_ad
        {
        get { return m_dealer_ad;}
        set { m_dealer_ad = value; }
        }
      
      }
      #endregion
      }

    