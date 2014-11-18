
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


      public partial class WebSiteArticle : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WebSiteArticle ( " +
      
        " WebSiteArticleCategoryId, " +
      
        " WebSiteArticleTypeId, " +
      
        " Url, " +
      
        " MenuId, " +
      
        " DatePublished " +
      
      ") Values (" +
      
        " ?WebSiteArticleCategoryId, " +
      
        " ?WebSiteArticleTypeId, " +
      
        " ?Url, " +
      
        " ?MenuId, " +
      
        " ?DatePublished " +
      
      ")";

      public static void Insert(WebSiteArticle webSiteArticle, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WebSiteArticleCategoryId", webSiteArticle.WebSiteArticleCategoryId);
      
        Database.PutParameter(dbCommand,"?WebSiteArticleTypeId", webSiteArticle.WebSiteArticleTypeId);
      
        Database.PutParameter(dbCommand,"?Url", webSiteArticle.Url);
      
        Database.PutParameter(dbCommand,"?MenuId", webSiteArticle.MenuId);
      
        Database.PutParameter(dbCommand,"?DatePublished", webSiteArticle.DatePublished);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        webSiteArticle.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(WebSiteArticle webSiteArticle)
      {
        Insert(webSiteArticle, null);
      }


      public static void Insert(List<WebSiteArticle>  webSiteArticleList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WebSiteArticle webSiteArticle in  webSiteArticleList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WebSiteArticleCategoryId", webSiteArticle.WebSiteArticleCategoryId);
      
        Database.PutParameter(dbCommand,"?WebSiteArticleTypeId", webSiteArticle.WebSiteArticleTypeId);
      
        Database.PutParameter(dbCommand,"?Url", webSiteArticle.Url);
      
        Database.PutParameter(dbCommand,"?MenuId", webSiteArticle.MenuId);
      
        Database.PutParameter(dbCommand,"?DatePublished", webSiteArticle.DatePublished);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WebSiteArticleCategoryId",webSiteArticle.WebSiteArticleCategoryId);
      
        Database.UpdateParameter(dbCommand,"?WebSiteArticleTypeId",webSiteArticle.WebSiteArticleTypeId);
      
        Database.UpdateParameter(dbCommand,"?Url",webSiteArticle.Url);
      
        Database.UpdateParameter(dbCommand,"?MenuId",webSiteArticle.MenuId);
      
        Database.UpdateParameter(dbCommand,"?DatePublished",webSiteArticle.DatePublished);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        webSiteArticle.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<WebSiteArticle>  webSiteArticleList)
      {
        Insert(webSiteArticleList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WebSiteArticle Set "
      
        + " WebSiteArticleCategoryId = ?WebSiteArticleCategoryId, "
      
        + " WebSiteArticleTypeId = ?WebSiteArticleTypeId, "
      
        + " Url = ?Url, "
      
        + " MenuId = ?MenuId, "
      
        + " DatePublished = ?DatePublished "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WebSiteArticle webSiteArticle, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", webSiteArticle.ID);
      
        Database.PutParameter(dbCommand,"?WebSiteArticleCategoryId", webSiteArticle.WebSiteArticleCategoryId);
      
        Database.PutParameter(dbCommand,"?WebSiteArticleTypeId", webSiteArticle.WebSiteArticleTypeId);
      
        Database.PutParameter(dbCommand,"?Url", webSiteArticle.Url);
      
        Database.PutParameter(dbCommand,"?MenuId", webSiteArticle.MenuId);
      
        Database.PutParameter(dbCommand,"?DatePublished", webSiteArticle.DatePublished);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WebSiteArticle webSiteArticle)
      {
        Update(webSiteArticle, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WebSiteArticleCategoryId, "
      
        + " WebSiteArticleTypeId, "
      
        + " Url, "
      
        + " MenuId, "
      
        + " DatePublished "
      

      + " From WebSiteArticle "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WebSiteArticle FindByPrimaryKey(
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
      throw new DataNotFoundException("WebSiteArticle not found, search by primary key");

      }

      public static WebSiteArticle FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WebSiteArticle webSiteArticle, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",webSiteArticle.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WebSiteArticle webSiteArticle)
      {
      return Exists(webSiteArticle, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WebSiteArticle limit 1";

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

      public static WebSiteArticle Load(IDataReader dataReader, int offset)
      {
      WebSiteArticle webSiteArticle = new WebSiteArticle();

      webSiteArticle.ID = dataReader.GetInt32(0 + offset);
          webSiteArticle.WebSiteArticleCategoryId = dataReader.GetInt32(1 + offset);
          webSiteArticle.WebSiteArticleTypeId = dataReader.GetInt32(2 + offset);
          webSiteArticle.Url = dataReader.GetString(3 + offset);
          webSiteArticle.MenuId = dataReader.GetInt32(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            webSiteArticle.DatePublished = dataReader.GetDateTime(5 + offset);
          

      return webSiteArticle;
      }

      public static WebSiteArticle Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WebSiteArticle "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WebSiteArticle webSiteArticle, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", webSiteArticle.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WebSiteArticle webSiteArticle)
      {
        Delete(webSiteArticle, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WebSiteArticle ";

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
      
        + " WebSiteArticleCategoryId, "
      
        + " WebSiteArticleTypeId, "
      
        + " Url, "
      
        + " MenuId, "
      
        + " DatePublished "
      

      + " From WebSiteArticle ";
      public static List<WebSiteArticle> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WebSiteArticle> rv = new List<WebSiteArticle>();

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

      public static List<WebSiteArticle> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WebSiteArticle> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WebSiteArticle obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && WebSiteArticleCategoryId == obj.WebSiteArticleCategoryId && WebSiteArticleTypeId == obj.WebSiteArticleTypeId && Url == obj.Url && MenuId == obj.MenuId && DatePublished == obj.DatePublished;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WebSiteArticle> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebSiteArticle));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WebSiteArticle item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WebSiteArticle>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebSiteArticle));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WebSiteArticle> itemsList
      = new List<WebSiteArticle>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WebSiteArticle)
      itemsList.Add(deserializedObject as WebSiteArticle);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_webSiteArticleCategoryId;
      
        protected int m_webSiteArticleTypeId;
      
        protected String m_url;
      
        protected int m_menuId;
      
        protected DateTime? m_datePublished;
      
      #endregion

      #region Constructors
      public WebSiteArticle(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WebSiteArticle(
        int 
          iD,int 
          webSiteArticleCategoryId,int 
          webSiteArticleTypeId,String 
          url,int 
          menuId,DateTime? 
          datePublished
        ) : this()
        {
        
          m_iD = iD;
        
          m_webSiteArticleCategoryId = webSiteArticleCategoryId;
        
          m_webSiteArticleTypeId = webSiteArticleTypeId;
        
          m_url = url;
        
          m_menuId = menuId;
        
          m_datePublished = datePublished;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int WebSiteArticleCategoryId
        {
        get { return m_webSiteArticleCategoryId;}
        set { m_webSiteArticleCategoryId = value; }
        }
      
        [XmlElement]
        public int WebSiteArticleTypeId
        {
        get { return m_webSiteArticleTypeId;}
        set { m_webSiteArticleTypeId = value; }
        }
      
        [XmlElement]
        public String Url
        {
        get { return m_url;}
        set { m_url = value; }
        }
      
        [XmlElement]
        public int MenuId
        {
        get { return m_menuId;}
        set { m_menuId = value; }
        }
      
        [XmlElement]
        public DateTime? DatePublished
        {
        get { return m_datePublished;}
        set { m_datePublished = value; }
        }
      

      public static int FieldsCount
      {
      get { return 6; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    