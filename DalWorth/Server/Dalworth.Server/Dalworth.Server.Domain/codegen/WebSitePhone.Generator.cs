
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


      public partial class WebSitePhone : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WebSitePhone ( " +
      
        " WebSiteId, " +
      
        " PhoneNumber, " +
      
        " PhoneKey " +
      
      ") Values (" +
      
        " ?WebSiteId, " +
      
        " ?PhoneNumber, " +
      
        " ?PhoneKey " +
      
      ")";

      public static void Insert(WebSitePhone webSitePhone, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?WebSiteId", webSitePhone.WebSiteId);
      
        Database.PutParameter(dbCommand,"?PhoneNumber", webSitePhone.PhoneNumber);
      
        Database.PutParameter(dbCommand,"?PhoneKey", webSitePhone.PhoneKey);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        webSitePhone.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(WebSitePhone webSitePhone)
      {
        Insert(webSitePhone, null);
      }


      public static void Insert(List<WebSitePhone>  webSitePhoneList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WebSitePhone webSitePhone in  webSitePhoneList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?WebSiteId", webSitePhone.WebSiteId);
      
        Database.PutParameter(dbCommand,"?PhoneNumber", webSitePhone.PhoneNumber);
      
        Database.PutParameter(dbCommand,"?PhoneKey", webSitePhone.PhoneKey);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?WebSiteId",webSitePhone.WebSiteId);
      
        Database.UpdateParameter(dbCommand,"?PhoneNumber",webSitePhone.PhoneNumber);
      
        Database.UpdateParameter(dbCommand,"?PhoneKey",webSitePhone.PhoneKey);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        webSitePhone.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<WebSitePhone>  webSitePhoneList)
      {
        Insert(webSitePhoneList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WebSitePhone Set "
      
        + " WebSiteId = ?WebSiteId, "
      
        + " PhoneNumber = ?PhoneNumber, "
      
        + " PhoneKey = ?PhoneKey "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WebSitePhone webSitePhone, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", webSitePhone.ID);
      
        Database.PutParameter(dbCommand,"?WebSiteId", webSitePhone.WebSiteId);
      
        Database.PutParameter(dbCommand,"?PhoneNumber", webSitePhone.PhoneNumber);
      
        Database.PutParameter(dbCommand,"?PhoneKey", webSitePhone.PhoneKey);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WebSitePhone webSitePhone)
      {
        Update(webSitePhone, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " WebSiteId, "
      
        + " PhoneNumber, "
      
        + " PhoneKey "
      

      + " From WebSitePhone "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WebSitePhone FindByPrimaryKey(
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
      throw new DataNotFoundException("WebSitePhone not found, search by primary key");

      }

      public static WebSitePhone FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WebSitePhone webSitePhone, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",webSitePhone.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WebSitePhone webSitePhone)
      {
      return Exists(webSitePhone, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WebSitePhone limit 1";

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

      public static WebSitePhone Load(IDataReader dataReader, int offset)
      {
      WebSitePhone webSitePhone = new WebSitePhone();

      webSitePhone.ID = dataReader.GetInt32(0 + offset);
          webSitePhone.WebSiteId = dataReader.GetInt32(1 + offset);
          webSitePhone.PhoneNumber = dataReader.GetString(2 + offset);
          webSitePhone.PhoneKey = dataReader.GetString(3 + offset);
          

      return webSitePhone;
      }

      public static WebSitePhone Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WebSitePhone "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WebSitePhone webSitePhone, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", webSitePhone.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WebSitePhone webSitePhone)
      {
        Delete(webSitePhone, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WebSitePhone ";

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
      
        + " PhoneNumber, "
      
        + " PhoneKey "
      

      + " From WebSitePhone ";
      public static List<WebSitePhone> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WebSitePhone> rv = new List<WebSitePhone>();

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

      public static List<WebSitePhone> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WebSitePhone> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (WebSitePhone obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && WebSiteId == obj.WebSiteId && PhoneNumber == obj.PhoneNumber && PhoneKey == obj.PhoneKey;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<WebSitePhone> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebSitePhone));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WebSitePhone item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WebSitePhone>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebSitePhone));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WebSitePhone> itemsList
      = new List<WebSitePhone>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WebSitePhone)
      itemsList.Add(deserializedObject as WebSitePhone);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_webSiteId;
      
        protected String m_phoneNumber;
      
        protected String m_phoneKey;
      
      #endregion

      #region Constructors
      public WebSitePhone(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WebSitePhone(
        int 
          iD,int 
          webSiteId,String 
          phoneNumber,String 
          phoneKey
        ) : this()
        {
        
          m_iD = iD;
        
          m_webSiteId = webSiteId;
        
          m_phoneNumber = phoneNumber;
        
          m_phoneKey = phoneKey;
        
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
        public String PhoneNumber
        {
        get { return m_phoneNumber;}
        set { m_phoneNumber = value; }
        }
      
        [XmlElement]
        public String PhoneKey
        {
        get { return m_phoneKey;}
        set { m_phoneKey = value; }
        }
      

      public static int FieldsCount
      {
      get { return 4; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    