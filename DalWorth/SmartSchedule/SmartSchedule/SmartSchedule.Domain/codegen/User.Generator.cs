
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using SmartSchedule.Data;
    using SmartSchedule.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace SmartSchedule.Domain
      {

      [DataContract]
      public partial class User : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into User ( " +
      
        " UserRoleId, " +
      
        " Login, " +
      
        " Password, " +
      
        " IsActive " +
      
      ") Values (" +
      
        " ?UserRoleId, " +
      
        " ?Login, " +
      
        " ?Password, " +
      
        " ?IsActive " +
      
      ")";

      public static void Insert(User user, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?UserRoleId", user.UserRoleId);
      
        Database.PutParameter(dbCommand,"?Login", user.Login);
      
        Database.PutParameter(dbCommand,"?Password", user.Password);
      
        Database.PutParameter(dbCommand,"?IsActive", user.IsActive);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        user.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(User user)
      {
        Insert(user, null);
      }


      public static void Insert(List<User>  userList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(User user in  userList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?UserRoleId", user.UserRoleId);
      
        Database.PutParameter(dbCommand,"?Login", user.Login);
      
        Database.PutParameter(dbCommand,"?Password", user.Password);
      
        Database.PutParameter(dbCommand,"?IsActive", user.IsActive);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?UserRoleId",user.UserRoleId);
      
        Database.UpdateParameter(dbCommand,"?Login",user.Login);
      
        Database.UpdateParameter(dbCommand,"?Password",user.Password);
      
        Database.UpdateParameter(dbCommand,"?IsActive",user.IsActive);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        user.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<User>  userList)
      {
        Insert(userList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update User Set "
      
        + " UserRoleId = ?UserRoleId, "
      
        + " Login = ?Login, "
      
        + " Password = ?Password, "
      
        + " IsActive = ?IsActive "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(User user, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", user.ID);
      
        Database.PutParameter(dbCommand,"?UserRoleId", user.UserRoleId);
      
        Database.PutParameter(dbCommand,"?Login", user.Login);
      
        Database.PutParameter(dbCommand,"?Password", user.Password);
      
        Database.PutParameter(dbCommand,"?IsActive", user.IsActive);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(User user)
      {
        Update(user, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " UserRoleId, "
      
        + " Login, "
      
        + " Password, "
      
        + " IsActive "
      

      + " From User "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static User FindByPrimaryKey(
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
      throw new DataNotFoundException("User not found, search by primary key");

      }

      public static User FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(User user, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",user.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(User user)
      {
      return Exists(user, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from User limit 1";

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

      public static User Load(IDataReader dataReader, int offset)
      {
      User user = new User();

      user.ID = dataReader.GetInt32(0 + offset);
          user.UserRoleId = dataReader.GetInt32(1 + offset);
          user.Login = dataReader.GetString(2 + offset);
          user.Password = dataReader.GetString(3 + offset);
          user.IsActive = dataReader.GetBoolean(4 + offset);
          

      return user;
      }

      public static User Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From User "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(User user, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", user.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(User user)
      {
        Delete(user, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From User ";

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
      
        + " UserRoleId, "
      
        + " Login, "
      
        + " Password, "
      
        + " IsActive "
      

      + " From User ";
      public static List<User> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<User> rv = new List<User>();

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

      public static List<User> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<User> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<User> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(User item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<User>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(User));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<User> itemsList
      = new List<User>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is User)
      itemsList.Add(deserializedObject as User);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_userRoleId;
      
        protected String m_login;
      
        protected String m_password;
      
        protected bool m_isActive;
      
      #endregion

      #region Constructors
      public User(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public User(
        int 
          iD,int 
          userRoleId,String 
          login,String 
          password,bool 
          isActive
        ) : this()
        {
        
          m_iD = iD;
        
          m_userRoleId = userRoleId;
        
          m_login = login;
        
          m_password = password;
        
          m_isActive = isActive;
        
        }


      
      #endregion

      
        [DataMember]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [DataMember]
        public int UserRoleId
        {
        get { return m_userRoleId;}
        set { m_userRoleId = value; }
        }
      
        [DataMember]
        public String Login
        {
        get { return m_login;}
        set { m_login = value; }
        }
      
        [DataMember]
        public String Password
        {
        get { return m_password;}
        set { m_password = value; }
        }
      
        [DataMember]
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
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

    