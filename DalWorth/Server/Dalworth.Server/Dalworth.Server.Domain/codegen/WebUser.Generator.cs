
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


      public partial class WebUser : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into WebUser ( " +
      
        " OrderSourceId, " +
      
        " Login, " +
      
        " PasswordHash, " +
      
        " EmployeeId, " +
      
        " FirstName, " +
      
        " LastName, " +
      
        " Email, " +
      
        " IsOwner " +
      
      ") Values (" +
      
        " ?OrderSourceId, " +
      
        " ?Login, " +
      
        " ?PasswordHash, " +
      
        " ?EmployeeId, " +
      
        " ?FirstName, " +
      
        " ?LastName, " +
      
        " ?Email, " +
      
        " ?IsOwner " +
      
      ")";

      public static void Insert(WebUser webUser, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?OrderSourceId", webUser.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?Login", webUser.Login);
      
        Database.PutParameter(dbCommand,"?PasswordHash", webUser.PasswordHash);
      
        Database.PutParameter(dbCommand,"?EmployeeId", webUser.EmployeeId);
      
        Database.PutParameter(dbCommand,"?FirstName", webUser.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", webUser.LastName);
      
        Database.PutParameter(dbCommand,"?Email", webUser.Email);
      
        Database.PutParameter(dbCommand,"?IsOwner", webUser.IsOwner);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        webUser.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(WebUser webUser)
      {
        Insert(webUser, null);
      }


      public static void Insert(List<WebUser>  webUserList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(WebUser webUser in  webUserList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?OrderSourceId", webUser.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?Login", webUser.Login);
      
        Database.PutParameter(dbCommand,"?PasswordHash", webUser.PasswordHash);
      
        Database.PutParameter(dbCommand,"?EmployeeId", webUser.EmployeeId);
      
        Database.PutParameter(dbCommand,"?FirstName", webUser.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", webUser.LastName);
      
        Database.PutParameter(dbCommand,"?Email", webUser.Email);
      
        Database.PutParameter(dbCommand,"?IsOwner", webUser.IsOwner);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?OrderSourceId",webUser.OrderSourceId);
      
        Database.UpdateParameter(dbCommand,"?Login",webUser.Login);
      
        Database.UpdateParameter(dbCommand,"?PasswordHash",webUser.PasswordHash);
      
        Database.UpdateParameter(dbCommand,"?EmployeeId",webUser.EmployeeId);
      
        Database.UpdateParameter(dbCommand,"?FirstName",webUser.FirstName);
      
        Database.UpdateParameter(dbCommand,"?LastName",webUser.LastName);
      
        Database.UpdateParameter(dbCommand,"?Email",webUser.Email);
      
        Database.UpdateParameter(dbCommand,"?IsOwner",webUser.IsOwner);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        webUser.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<WebUser>  webUserList)
      {
        Insert(webUserList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update WebUser Set "
      
        + " OrderSourceId = ?OrderSourceId, "
      
        + " Login = ?Login, "
      
        + " PasswordHash = ?PasswordHash, "
      
        + " EmployeeId = ?EmployeeId, "
      
        + " FirstName = ?FirstName, "
      
        + " LastName = ?LastName, "
      
        + " Email = ?Email, "
      
        + " IsOwner = ?IsOwner "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(WebUser webUser, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", webUser.ID);
      
        Database.PutParameter(dbCommand,"?OrderSourceId", webUser.OrderSourceId);
      
        Database.PutParameter(dbCommand,"?Login", webUser.Login);
      
        Database.PutParameter(dbCommand,"?PasswordHash", webUser.PasswordHash);
      
        Database.PutParameter(dbCommand,"?EmployeeId", webUser.EmployeeId);
      
        Database.PutParameter(dbCommand,"?FirstName", webUser.FirstName);
      
        Database.PutParameter(dbCommand,"?LastName", webUser.LastName);
      
        Database.PutParameter(dbCommand,"?Email", webUser.Email);
      
        Database.PutParameter(dbCommand,"?IsOwner", webUser.IsOwner);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(WebUser webUser)
      {
        Update(webUser, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " OrderSourceId, "
      
        + " Login, "
      
        + " PasswordHash, "
      
        + " EmployeeId, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Email, "
      
        + " IsOwner "
      

      + " From WebUser "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static WebUser FindByPrimaryKey(
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
      throw new DataNotFoundException("WebUser not found, search by primary key");

      }

      public static WebUser FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(WebUser webUser, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",webUser.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(WebUser webUser)
      {
      return Exists(webUser, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from WebUser limit 1";

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

      public static WebUser Load(IDataReader dataReader, int offset)
      {
      WebUser webUser = new WebUser();

      webUser.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            webUser.OrderSourceId = dataReader.GetInt32(1 + offset);
          webUser.Login = dataReader.GetString(2 + offset);
          webUser.PasswordHash = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            webUser.EmployeeId = dataReader.GetInt32(4 + offset);
          webUser.FirstName = dataReader.GetString(5 + offset);
          webUser.LastName = dataReader.GetString(6 + offset);
          webUser.Email = dataReader.GetString(7 + offset);
          webUser.IsOwner = dataReader.GetBoolean(8 + offset);
          

      return webUser;
      }

      public static WebUser Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From WebUser "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(WebUser webUser, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", webUser.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(WebUser webUser)
      {
        Delete(webUser, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From WebUser ";

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
      
        + " OrderSourceId, "
      
        + " Login, "
      
        + " PasswordHash, "
      
        + " EmployeeId, "
      
        + " FirstName, "
      
        + " LastName, "
      
        + " Email, "
      
        + " IsOwner "
      

      + " From WebUser ";
      public static List<WebUser> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<WebUser> rv = new List<WebUser>();

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

      public static List<WebUser> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<WebUser> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<WebUser> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebUser));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(WebUser item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<WebUser>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(WebUser));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<WebUser> itemsList
      = new List<WebUser>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is WebUser)
      itemsList.Add(deserializedObject as WebUser);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int? m_orderSourceId;
      
        protected String m_login;
      
        protected String m_passwordHash;
      
        protected int? m_employeeId;
      
        protected String m_firstName;
      
        protected String m_lastName;
      
        protected String m_email;
      
        protected bool m_isOwner;
      
      #endregion

      #region Constructors
      public WebUser(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public WebUser(
        int 
          iD,int? 
          orderSourceId,String 
          login,String 
          passwordHash,int? 
          employeeId,String 
          firstName,String 
          lastName,String 
          email,bool 
          isOwner
        ) : this()
        {
        
          m_iD = iD;
        
          m_orderSourceId = orderSourceId;
        
          m_login = login;
        
          m_passwordHash = passwordHash;
        
          m_employeeId = employeeId;
        
          m_firstName = firstName;
        
          m_lastName = lastName;
        
          m_email = email;
        
          m_isOwner = isOwner;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int? OrderSourceId
        {
        get { return m_orderSourceId;}
        set { m_orderSourceId = value; }
        }
      
        [XmlElement]
        public String Login
        {
        get { return m_login;}
        set { m_login = value; }
        }
      
        [XmlElement]
        public String PasswordHash
        {
        get { return m_passwordHash;}
        set { m_passwordHash = value; }
        }
      
        [XmlElement]
        public int? EmployeeId
        {
        get { return m_employeeId;}
        set { m_employeeId = value; }
        }
      
        [XmlElement]
        public String FirstName
        {
        get { return m_firstName;}
        set { m_firstName = value; }
        }
      
        [XmlElement]
        public String LastName
        {
        get { return m_lastName;}
        set { m_lastName = value; }
        }
      
        [XmlElement]
        public String Email
        {
        get { return m_email;}
        set { m_email = value; }
        }
      
        [XmlElement]
        public bool IsOwner
        {
        get { return m_isOwner;}
        set { m_isOwner = value; }
        }
      

      public static int FieldsCount
      {
      get { return 9; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    