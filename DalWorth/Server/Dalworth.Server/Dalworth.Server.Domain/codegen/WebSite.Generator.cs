
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Server.Data;
    using Dalworth.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Server.Domain
      {


      public partial class WebSite : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WebSite ( " +
      
        " ID, " +
      
        " Name " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Name " +
      
      ")";

      public static void Insert(WebSite webSite, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", webSite.ID);
      
        Database.PutParameter(dbCommand,"?Name", webSite.Name);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(WebSite webSite)
      {
        Insert(webSite, null);
      }


      public static void Insert(List<WebSite>  webSiteList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WebSite webSite in  webSiteList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", webSite.ID);
      
        Database.PutParameter(dbCommand,"?Name", webSite.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",webSite.ID);
      
        Database.UpdateParameter(dbCommand,"?Name",webSite.Name);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<WebSite>  webSiteList)
      {
        Insert(webSiteList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WebSite Set "
      
        + " Name = ?Name "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WebSite webSite, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", webSite.ID);
      
        Database.PutParameter(dbCommand,"?Name", webSite.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WebSite webSite)
      {
        Update(webSite, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Name "
      

      + " From WebSite "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WebSite FindByPrimaryKey(
      int iD, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", iD);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("WebSite not found, search by primary key");

      }

      public static WebSite FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WebSite webSite, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",webSite.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WebSite webSite)
      {
      return Exists(webSite, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WebSite limit 1";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      public static bool IsContainsData()
      {
      return IsContainsData(null);
      }

      #endregion

      #region Load

      public static WebSite Load(IDataReader dataReader, int offset)
      {
      WebSite webSite = new WebSite();

      webSite.ID = dataReader.GetInt32(0 + offset);
          webSite.Name = dataReader.GetString(1 + offset);
          

      return webSite;
      }

      public static WebSite Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WebSite "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WebSite webSite, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", webSite.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WebSite webSite)
      {
        Delete(webSite, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WebSite ";

      public static void Clear(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, connection))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Clear()
      {
        Clear(null);
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " ID, "
      
        + " Name "
      

      + " From WebSite ";
      public static List<WebSite> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WebSite> rv = new List<WebSite>();

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

      public static List<WebSite> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WebSite> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WebSite obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Name == obj.Name;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WebSite> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebSite));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WebSite item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WebSite>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebSite));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WebSite> itemsList
      = new List<WebSite>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WebSite)
      itemsList.Add(deserializedObject as WebSite);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_name;
      
      #endregion

      #region Constructors
      public WebSite(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WebSite(
        int 
          iD,String 
          name
        ) : this()
        {
        
          m_iD = iD;
        
          m_name = name;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      

      public static int FieldsCount
      {
      get { return 2; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    