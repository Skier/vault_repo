
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


      public partial class company
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into company ( " +
      
        " companyid, " +
      
        " longname, " +
      
        " shortname " +
      
      ") Values (" +
      
        " ?, " +
      
        " ?, " +
      
        " ? " +
      
      ")";

      public static void Insert(company company)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@companyid", company.companyid);
      
        Database.PutParameter(dbCommand,"@longname", company.longname);
      
        Database.PutParameter(dbCommand,"@shortname", company.shortname);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(List<company>  companyList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, ConnectionKeyEnum.Servman))
      {
      bool parametersAdded = false;

      foreach(company company in  companyList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@companyid", company.companyid);
      
        Database.PutParameter(dbCommand,"@longname", company.longname);
      
        Database.PutParameter(dbCommand,"@shortname", company.shortname);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@companyid",company.companyid);
      
        Database.UpdateParameter(dbCommand,"@longname",company.longname);
      
        Database.UpdateParameter(dbCommand,"@shortname",company.shortname);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update company Set "
      
        + " company.longname = ? , "
      
        + " company.shortname = ?  "
      
        + " Where "
        
          + " company.companyid = ?  "
        
      ;

      public static void Update(company company)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@longname", company.longname);
      
        Database.PutParameter(dbCommand,"@shortname", company.shortname);
      
        Database.PutParameter(dbCommand,"@companyid", company.companyid);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " company.companyid, "
      
        + " company.longname, "
      
        + " company.shortname "
      

      + " From company "

      
        + " Where "
        
          + " company.companyid = ?  "
        
      ;

      public static company FindByPrimaryKey(
      int companyid
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@companyid", companyid);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("company not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(company company)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, ConnectionKeyEnum.Servman))
      {
      
        Database.PutParameter(dbCommand,"@companyid",company.companyid);
      

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
      String sql = "select 1 from company";

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

      public static company Load(IDataReader dataReader)
      {
      company company = new company();

      company.companyid = dataReader.GetInt32(0);
          company.longname = dataReader.GetString(1);
          company.shortname = dataReader.GetString(2);
          

      return company;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [company] "

      
        + " Where "
        
          + " companyid = ?  "
        
      ;
      public static void Delete(company company)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, ConnectionKeyEnum.Servman))
      {

      
        Database.PutParameter(dbCommand,"@companyid", company.companyid);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [company] ";

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

      
        + " company.companyid, "
      
        + " company.longname, "
      
        + " company.shortname "
      

      + " From company ";
      public static List<company> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, ConnectionKeyEnum.Servman))
      {
      List<company> rv = new List<company>();

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
      List<company> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<company> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(company));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(company item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<company>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(company));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<company> itemsList
      = new List<company>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is company)
      itemsList.Add(deserializedObject as company);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_companyid;
      
        protected String m_longname;
      
        protected String m_shortname;
      
      #endregion

      #region Constructors
      public company(
      int 
          companyid
      )
      {
      
        m_companyid = companyid;
      
      }

      


        public company(
        int 
          companyid,String 
          longname,String 
          shortname
        )
        {
        
          m_companyid = companyid;
        
          m_longname = longname;
        
          m_shortname = shortname;
        
        }


      
      #endregion

      
        [XmlElement]
        public int companyid
        {
        get { return m_companyid;}
        set { m_companyid = value; }
        }
      
        [XmlElement]
        public String longname
        {
        get { return m_longname;}
        set { m_longname = value; }
        }
      
        [XmlElement]
        public String shortname
        {
        get { return m_shortname;}
        set { m_shortname = value; }
        }
      
      }
      #endregion
      }

    