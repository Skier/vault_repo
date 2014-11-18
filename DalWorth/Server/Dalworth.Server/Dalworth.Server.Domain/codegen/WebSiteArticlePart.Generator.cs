
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


      public partial class WebSiteArticlePart : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WebSiteArticlePart ( " +
      
        " WebSiteArticleId, " +
      
        " WebSiteArticlePartTypeId, " +
      
        " ContentText " +
      
      ") Values (" +
      
        " ?WebSiteArticleId, " +
      
        " ?WebSiteArticlePartTypeId, " +
      
        " ?ContentText " +
      
      ")";

      public static void Insert(WebSiteArticlePart webSiteArticlePart, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WebSiteArticleId", webSiteArticlePart.WebSiteArticleId);
      
        Database.PutParameter(dbCommand,"?WebSiteArticlePartTypeId", webSiteArticlePart.WebSiteArticlePartTypeId);
      
        Database.PutParameter(dbCommand,"?ContentText", webSiteArticlePart.ContentText);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(WebSiteArticlePart webSiteArticlePart)
      {
        Insert(webSiteArticlePart, null);
      }


      public static void Insert(List<WebSiteArticlePart>  webSiteArticlePartList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WebSiteArticlePart webSiteArticlePart in  webSiteArticlePartList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WebSiteArticleId", webSiteArticlePart.WebSiteArticleId);
      
        Database.PutParameter(dbCommand,"?WebSiteArticlePartTypeId", webSiteArticlePart.WebSiteArticlePartTypeId);
      
        Database.PutParameter(dbCommand,"?ContentText", webSiteArticlePart.ContentText);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WebSiteArticleId",webSiteArticlePart.WebSiteArticleId);
      
        Database.UpdateParameter(dbCommand,"?WebSiteArticlePartTypeId",webSiteArticlePart.WebSiteArticlePartTypeId);
      
        Database.UpdateParameter(dbCommand,"?ContentText",webSiteArticlePart.ContentText);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<WebSiteArticlePart>  webSiteArticlePartList)
      {
        Insert(webSiteArticlePartList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WebSiteArticlePart Set "
      
        + " ContentText = ?ContentText "
      
        + " Where "
        
          + " WebSiteArticleId = ?WebSiteArticleId and  "
        
          + " WebSiteArticlePartTypeId = ?WebSiteArticlePartTypeId "
        
      ;

      public static void Update(WebSiteArticlePart webSiteArticlePart, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?WebSiteArticleId", webSiteArticlePart.WebSiteArticleId);
      
        Database.PutParameter(dbCommand,"?WebSiteArticlePartTypeId", webSiteArticlePart.WebSiteArticlePartTypeId);
      
        Database.PutParameter(dbCommand,"?ContentText", webSiteArticlePart.ContentText);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WebSiteArticlePart webSiteArticlePart)
      {
        Update(webSiteArticlePart, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " WebSiteArticleId, "
      
        + " WebSiteArticlePartTypeId, "
      
        + " ContentText "
      

      + " From WebSiteArticlePart "

      
        + " Where "
        
          + " WebSiteArticleId = ?WebSiteArticleId and  "
        
          + " WebSiteArticlePartTypeId = ?WebSiteArticlePartTypeId "
        
      ;

      public static WebSiteArticlePart FindByPrimaryKey(
      int webSiteArticleId,int webSiteArticlePartTypeId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?WebSiteArticleId", webSiteArticleId);
      
        Database.PutParameter(dbCommand,"?WebSiteArticlePartTypeId", webSiteArticlePartTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("WebSiteArticlePart not found, search by primary key");

      }

      public static WebSiteArticlePart FindByPrimaryKey(
      int webSiteArticleId,int webSiteArticlePartTypeId
      )
      {
      return FindByPrimaryKey(
      webSiteArticleId,webSiteArticlePartTypeId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WebSiteArticlePart webSiteArticlePart, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?WebSiteArticleId",webSiteArticlePart.WebSiteArticleId);
      
        Database.PutParameter(dbCommand,"?WebSiteArticlePartTypeId",webSiteArticlePart.WebSiteArticlePartTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WebSiteArticlePart webSiteArticlePart)
      {
      return Exists(webSiteArticlePart, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WebSiteArticlePart limit 1";

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

      public static WebSiteArticlePart Load(IDataReader dataReader, int offset)
      {
      WebSiteArticlePart webSiteArticlePart = new WebSiteArticlePart();

      webSiteArticlePart.WebSiteArticleId = dataReader.GetInt32(0 + offset);
          webSiteArticlePart.WebSiteArticlePartTypeId = dataReader.GetInt32(1 + offset);
          webSiteArticlePart.ContentText = dataReader.GetString(2 + offset);
          

      return webSiteArticlePart;
      }

      public static WebSiteArticlePart Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WebSiteArticlePart "

      
        + " Where "
        
          + " WebSiteArticleId = ?WebSiteArticleId and  "
        
          + " WebSiteArticlePartTypeId = ?WebSiteArticlePartTypeId "
        
      ;
      public static void Delete(WebSiteArticlePart webSiteArticlePart, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?WebSiteArticleId", webSiteArticlePart.WebSiteArticleId);
      
        Database.PutParameter(dbCommand,"?WebSiteArticlePartTypeId", webSiteArticlePart.WebSiteArticlePartTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WebSiteArticlePart webSiteArticlePart)
      {
        Delete(webSiteArticlePart, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WebSiteArticlePart ";

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

      
        + " WebSiteArticleId, "
      
        + " WebSiteArticlePartTypeId, "
      
        + " ContentText "
      

      + " From WebSiteArticlePart ";
      public static List<WebSiteArticlePart> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WebSiteArticlePart> rv = new List<WebSiteArticlePart>();

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

      public static List<WebSiteArticlePart> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WebSiteArticlePart> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WebSiteArticlePart obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return WebSiteArticleId == obj.WebSiteArticleId && WebSiteArticlePartTypeId == obj.WebSiteArticlePartTypeId && ContentText == obj.ContentText;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WebSiteArticlePart> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebSiteArticlePart));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WebSiteArticlePart item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WebSiteArticlePart>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebSiteArticlePart));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WebSiteArticlePart> itemsList
      = new List<WebSiteArticlePart>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WebSiteArticlePart)
      itemsList.Add(deserializedObject as WebSiteArticlePart);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_webSiteArticleId;
      
        protected int m_webSiteArticlePartTypeId;
      
        protected String m_contentText;
      
      #endregion

      #region Constructors
      public WebSiteArticlePart(
      int 
          webSiteArticleId,int 
          webSiteArticlePartTypeId
      ) : this()
      {
      
        m_webSiteArticleId = webSiteArticleId;
      
        m_webSiteArticlePartTypeId = webSiteArticlePartTypeId;
      
      }

      


        public WebSiteArticlePart(
        int 
          webSiteArticleId,int 
          webSiteArticlePartTypeId,String 
          contentText
        ) : this()
        {
        
          m_webSiteArticleId = webSiteArticleId;
        
          m_webSiteArticlePartTypeId = webSiteArticlePartTypeId;
        
          m_contentText = contentText;
        
        }


      
      #endregion

      
        [XmlElement]
        public int WebSiteArticleId
        {
        get { return m_webSiteArticleId;}
        set { m_webSiteArticleId = value; }
        }
      
        [XmlElement]
        public int WebSiteArticlePartTypeId
        {
        get { return m_webSiteArticlePartTypeId;}
        set { m_webSiteArticlePartTypeId = value; }
        }
      
        [XmlElement]
        public String ContentText
        {
        get { return m_contentText;}
        set { m_contentText = value; }
        }
      

      public static int FieldsCount
      {
      get { return 3; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    