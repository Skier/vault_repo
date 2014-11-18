
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


      public partial class city_tax
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into city_tax ( " +
      
        " city, " +
      
        " tax_amount, " +
      
        " group_id, " +
      
        " area_id " +
      
      ") Values (" +
      
        " ?, " +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(city_tax city_tax)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@city", city_tax.city);
      
        Database.PutParameter(dbCommand,"@tax_amount", city_tax.tax_amount);
      
        Database.PutParameter(dbCommand,"@group_id", city_tax.group_id);
      
        Database.PutParameter(dbCommand,"@area_id", city_tax.area_id);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<city_tax>  city_taxList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(city_tax city_tax in  city_taxList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@city", city_tax.city);
      
        Database.PutParameter(dbCommand,"@tax_amount", city_tax.tax_amount);
      
        Database.PutParameter(dbCommand,"@group_id", city_tax.group_id);
      
        Database.PutParameter(dbCommand,"@area_id", city_tax.area_id);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@city",city_tax.city);
      
        Database.UpdateParameter(dbCommand,"@tax_amount",city_tax.tax_amount);
      
        Database.UpdateParameter(dbCommand,"@group_id",city_tax.group_id);
      
        Database.UpdateParameter(dbCommand,"@area_id",city_tax.area_id);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update city_tax Set "
      
        + " city_tax.tax_amount = ? , "
      
        + " city_tax.group_id = ?  "
      
        + " Where "
        
          + " city_tax.city = ?  and  "
        
          + " city_tax.area_id = ?  "
        
      ;

      public static void Update(city_tax city_tax)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@city", city_tax.city);
      
        Database.PutParameter(dbCommand,"@tax_amount", city_tax.tax_amount);
      
        Database.PutParameter(dbCommand,"@group_id", city_tax.group_id);
      
        Database.PutParameter(dbCommand,"@area_id", city_tax.area_id);
      
        Database.PutParameter(dbCommand,"@city", city_tax.city);
      
        Database.PutParameter(dbCommand,"@area_id", city_tax.area_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " city_tax.city, "
      
        + " city_tax.tax_amount, "
      
        + " city_tax.group_id, "
      
        + " city_tax.area_id "
      

      + " From city_tax "

      
        + " Where "
        
          + " city_tax.city = ?  and  "
        
          + " city_tax.area_id = ?  "
        
      ;

      public static city_tax FindByPrimaryKey(
      String city,String area_id
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@city", city);
      
        Database.PutParameter(dbCommand,"@area_id", area_id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("city_tax not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(city_tax city_tax)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@city",city_tax.city);
      
        Database.PutParameter(dbCommand,"@area_id",city_tax.area_id);
      

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
      String sql = "select 1 from city_tax";

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

      public static city_tax Load(IDataReader dataReader)
      {
      city_tax city_tax = new city_tax();

      city_tax.city = dataReader.GetString(0);
          city_tax.tax_amount = dataReader.GetFloat(1);
          city_tax.group_id = dataReader.GetInt32(2);
          city_tax.area_id = dataReader.GetString(3);
          

      return city_tax;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [city_tax] "

      
        + " Where "
        
          + " city = ?  and  "
        
          + " area_id = ?  "
        
      ;
      public static void Delete(city_tax city_tax)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@city", city_tax.city);
      
        Database.PutParameter(dbCommand,"@area_id", city_tax.area_id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [city_tax] ";

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

      
        + " city_tax.city, "
      
        + " city_tax.tax_amount, "
      
        + " city_tax.group_id, "
      
        + " city_tax.area_id "
      

      + " From city_tax ";
      public static List<city_tax> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<city_tax> rv = new List<city_tax>();

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
      List<city_tax> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<city_tax> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(city_tax));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(city_tax item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<city_tax>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(city_tax));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<city_tax> itemsList
      = new List<city_tax>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is city_tax)
      itemsList.Add(deserializedObject as city_tax);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_city;
      
        protected float m_tax_amount;
      
        protected int m_group_id;
      
        protected String m_area_id;
      
      #endregion

      #region Constructors
      public city_tax(
      String 
          city,String 
          area_id
      )
      {
      
        m_city = city;
      
        m_area_id = area_id;
      
      }

      


        public city_tax(
        String 
          city,float 
          tax_amount,int 
          group_id,String 
          area_id
        )
        {
        
          m_city = city;
        
          m_tax_amount = tax_amount;
        
          m_group_id = group_id;
        
          m_area_id = area_id;
        
        }


      
      #endregion

      
        [XmlElement]
        public String city
        {
        get { return m_city;}
        set { m_city = value; }
        }
      
        [XmlElement]
        public float tax_amount
        {
        get { return m_tax_amount;}
        set { m_tax_amount = value; }
        }
      
        [XmlElement]
        public int group_id
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
      
      }
      #endregion
      }

    