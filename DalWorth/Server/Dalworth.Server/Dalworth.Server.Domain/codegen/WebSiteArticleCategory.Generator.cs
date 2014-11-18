
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


      public partial class WebSiteArticleCategory : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WebSiteArticleCategory ( " +
      
        " WebSiteId, " +
      
        " ParentWebSiteArticleCategoryId, " +
      
        " LandingPageWebSiteArticleId, " +
      
        " Name " +
      
      ") Values (" +
      
        " ?WebSiteId, " +
      
        " ?ParentWebSiteArticleCategoryId, " +
      
        " ?LandingPageWebSiteArticleId, " +
      
        " ?Name " +
      
      ")";

      public static void Insert(WebSiteArticleCategory webSiteArticleCategory, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WebSiteId", webSiteArticleCategory.WebSiteId);
      
        Database.PutParameter(dbCommand,"?ParentWebSiteArticleCategoryId", webSiteArticleCategory.ParentWebSiteArticleCategoryId);
      
        Database.PutParameter(dbCommand,"?LandingPageWebSiteArticleId", webSiteArticleCategory.LandingPageWebSiteArticleId);
      
        Database.PutParameter(dbCommand,"?Name", webSiteArticleCategory.Name);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        webSiteArticleCategory.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(WebSiteArticleCategory webSiteArticleCategory)
      {
        Insert(webSiteArticleCategory, null);
      }


      public static void Insert(List<WebSiteArticleCategory>  webSiteArticleCategoryList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WebSiteArticleCategory webSiteArticleCategory in  webSiteArticleCategoryList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WebSiteId", webSiteArticleCategory.WebSiteId);
      
        Database.PutParameter(dbCommand,"?ParentWebSiteArticleCategoryId", webSiteArticleCategory.ParentWebSiteArticleCategoryId);
      
        Database.PutParameter(dbCommand,"?LandingPageWebSiteArticleId", webSiteArticleCategory.LandingPageWebSiteArticleId);
      
        Database.PutParameter(dbCommand,"?Name", webSiteArticleCategory.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WebSiteId",webSiteArticleCategory.WebSiteId);
      
        Database.UpdateParameter(dbCommand,"?ParentWebSiteArticleCategoryId",webSiteArticleCategory.ParentWebSiteArticleCategoryId);
      
        Database.UpdateParameter(dbCommand,"?LandingPageWebSiteArticleId",webSiteArticleCategory.LandingPageWebSiteArticleId);
      
        Database.UpdateParameter(dbCommand,"?Name",webSiteArticleCategory.Name);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        webSiteArticleCategory.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<WebSiteArticleCategory>  webSiteArticleCategoryList)
      {
        Insert(webSiteArticleCategoryList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WebSiteArticleCategory Set "
      
        + " WebSiteId = ?WebSiteId, "
      
        + " ParentWebSiteArticleCategoryId = ?ParentWebSiteArticleCategoryId, "
      
        + " LandingPageWebSiteArticleId = ?LandingPageWebSiteArticleId, "
      
        + " Name = ?Name "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WebSiteArticleCategory webSiteArticleCategory, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", webSiteArticleCategory.ID);
      
        Database.PutParameter(dbCommand,"?WebSiteId", webSiteArticleCategory.WebSiteId);
      
        Database.PutParameter(dbCommand,"?ParentWebSiteArticleCategoryId", webSiteArticleCategory.ParentWebSiteArticleCategoryId);
      
        Database.PutParameter(dbCommand,"?LandingPageWebSiteArticleId", webSiteArticleCategory.LandingPageWebSiteArticleId);
      
        Database.PutParameter(dbCommand,"?Name", webSiteArticleCategory.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WebSiteArticleCategory webSiteArticleCategory)
      {
        Update(webSiteArticleCategory, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WebSiteId, "
      
        + " ParentWebSiteArticleCategoryId, "
      
        + " LandingPageWebSiteArticleId, "
      
        + " Name "
      

      + " From WebSiteArticleCategory "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WebSiteArticleCategory FindByPrimaryKey(
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
      throw new DataNotFoundException("WebSiteArticleCategory not found, search by primary key");

      }

      public static WebSiteArticleCategory FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WebSiteArticleCategory webSiteArticleCategory, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",webSiteArticleCategory.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WebSiteArticleCategory webSiteArticleCategory)
      {
      return Exists(webSiteArticleCategory, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WebSiteArticleCategory limit 1";

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

      public static WebSiteArticleCategory Load(IDataReader dataReader, int offset)
      {
      WebSiteArticleCategory webSiteArticleCategory = new WebSiteArticleCategory();

      webSiteArticleCategory.ID = dataReader.GetInt32(0 + offset);
          webSiteArticleCategory.WebSiteId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            webSiteArticleCategory.ParentWebSiteArticleCategoryId = dataReader.GetInt32(2 + offset);
          webSiteArticleCategory.LandingPageWebSiteArticleId = dataReader.GetInt32(3 + offset);
          webSiteArticleCategory.Name = dataReader.GetString(4 + offset);
          

      return webSiteArticleCategory;
      }

      public static WebSiteArticleCategory Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WebSiteArticleCategory "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WebSiteArticleCategory webSiteArticleCategory, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", webSiteArticleCategory.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WebSiteArticleCategory webSiteArticleCategory)
      {
        Delete(webSiteArticleCategory, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WebSiteArticleCategory ";

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
      
        + " WebSiteId, "
      
        + " ParentWebSiteArticleCategoryId, "
      
        + " LandingPageWebSiteArticleId, "
      
        + " Name "
      

      + " From WebSiteArticleCategory ";
      public static List<WebSiteArticleCategory> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WebSiteArticleCategory> rv = new List<WebSiteArticleCategory>();

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

      public static List<WebSiteArticleCategory> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WebSiteArticleCategory> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WebSiteArticleCategory obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && WebSiteId == obj.WebSiteId && ParentWebSiteArticleCategoryId == obj.ParentWebSiteArticleCategoryId && LandingPageWebSiteArticleId == obj.LandingPageWebSiteArticleId && Name == obj.Name;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WebSiteArticleCategory> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebSiteArticleCategory));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WebSiteArticleCategory item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WebSiteArticleCategory>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebSiteArticleCategory));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WebSiteArticleCategory> itemsList
      = new List<WebSiteArticleCategory>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WebSiteArticleCategory)
      itemsList.Add(deserializedObject as WebSiteArticleCategory);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_webSiteId;
      
        protected int? m_parentWebSiteArticleCategoryId;
      
        protected int m_landingPageWebSiteArticleId;
      
        protected String m_name;
      
      #endregion

      #region Constructors
      public WebSiteArticleCategory(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WebSiteArticleCategory(
        int 
          iD,int 
          webSiteId,int? 
          parentWebSiteArticleCategoryId,int 
          landingPageWebSiteArticleId,String 
          name
        ) : this()
        {
        
          m_iD = iD;
        
          m_webSiteId = webSiteId;
        
          m_parentWebSiteArticleCategoryId = parentWebSiteArticleCategoryId;
        
          m_landingPageWebSiteArticleId = landingPageWebSiteArticleId;
        
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
        public int WebSiteId
        {
        get { return m_webSiteId;}
        set { m_webSiteId = value; }
        }
      
        [XmlElement]
        public int? ParentWebSiteArticleCategoryId
        {
        get { return m_parentWebSiteArticleCategoryId;}
        set { m_parentWebSiteArticleCategoryId = value; }
        }
      
        [XmlElement]
        public int LandingPageWebSiteArticleId
        {
        get { return m_landingPageWebSiteArticleId;}
        set { m_landingPageWebSiteArticleId = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      

      public static int FieldsCount
      {
      get { return 5; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    