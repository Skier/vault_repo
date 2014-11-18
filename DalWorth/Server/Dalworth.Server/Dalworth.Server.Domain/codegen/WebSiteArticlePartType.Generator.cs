
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


      public partial class WebSiteArticlePartType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WebSiteArticlePartType ( " +
      
        " Name, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?Name, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(WebSiteArticlePartType webSiteArticlePartType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?Name", webSiteArticlePartType.Name);
      
        Database.PutParameter(dbCommand,"?Description", webSiteArticlePartType.Description);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        webSiteArticlePartType.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(WebSiteArticlePartType webSiteArticlePartType)
      {
        Insert(webSiteArticlePartType, null);
      }


      public static void Insert(List<WebSiteArticlePartType>  webSiteArticlePartTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WebSiteArticlePartType webSiteArticlePartType in  webSiteArticlePartTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?Name", webSiteArticlePartType.Name);
      
        Database.PutParameter(dbCommand,"?Description", webSiteArticlePartType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?Name",webSiteArticlePartType.Name);
      
        Database.UpdateParameter(dbCommand,"?Description",webSiteArticlePartType.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        webSiteArticlePartType.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<WebSiteArticlePartType>  webSiteArticlePartTypeList)
      {
        Insert(webSiteArticlePartTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WebSiteArticlePartType Set "
      
        + " Name = ?Name, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WebSiteArticlePartType webSiteArticlePartType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", webSiteArticlePartType.ID);
      
        Database.PutParameter(dbCommand,"?Name", webSiteArticlePartType.Name);
      
        Database.PutParameter(dbCommand,"?Description", webSiteArticlePartType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WebSiteArticlePartType webSiteArticlePartType)
      {
        Update(webSiteArticlePartType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Name, "
      
        + " Description "
      

      + " From WebSiteArticlePartType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WebSiteArticlePartType FindByPrimaryKey(
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
      throw new DataNotFoundException("WebSiteArticlePartType not found, search by primary key");

      }

      public static WebSiteArticlePartType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WebSiteArticlePartType webSiteArticlePartType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",webSiteArticlePartType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WebSiteArticlePartType webSiteArticlePartType)
      {
      return Exists(webSiteArticlePartType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WebSiteArticlePartType limit 1";

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

      public static WebSiteArticlePartType Load(IDataReader dataReader, int offset)
      {
      WebSiteArticlePartType webSiteArticlePartType = new WebSiteArticlePartType();

      webSiteArticlePartType.ID = dataReader.GetInt32(0 + offset);
          webSiteArticlePartType.Name = dataReader.GetString(1 + offset);
          webSiteArticlePartType.Description = dataReader.GetString(2 + offset);
          

      return webSiteArticlePartType;
      }

      public static WebSiteArticlePartType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WebSiteArticlePartType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WebSiteArticlePartType webSiteArticlePartType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", webSiteArticlePartType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WebSiteArticlePartType webSiteArticlePartType)
      {
        Delete(webSiteArticlePartType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WebSiteArticlePartType ";

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
      
        + " Name, "
      
        + " Description "
      

      + " From WebSiteArticlePartType ";
      public static List<WebSiteArticlePartType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WebSiteArticlePartType> rv = new List<WebSiteArticlePartType>();

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

      public static List<WebSiteArticlePartType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WebSiteArticlePartType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WebSiteArticlePartType obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Name == obj.Name && Description == obj.Description;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WebSiteArticlePartType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebSiteArticlePartType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WebSiteArticlePartType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WebSiteArticlePartType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebSiteArticlePartType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WebSiteArticlePartType> itemsList
      = new List<WebSiteArticlePartType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WebSiteArticlePartType)
      itemsList.Add(deserializedObject as WebSiteArticlePartType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_name;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public WebSiteArticlePartType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WebSiteArticlePartType(
        int 
          iD,String 
          name,String 
          description
        ) : this()
        {
        
          m_iD = iD;
        
          m_name = name;
        
          m_description = description;
        
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
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
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

    