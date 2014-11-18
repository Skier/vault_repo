
    using System;
    using System.Data;
    using System.Collections.Generic;
    using TractInc.Server.Data;
    using TractInc.Server.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace TractInc.Server.Domain
      {


      public partial class User
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [User] ( " +
      
        " PersonId, " +
      
        " Login, " +
      
        " Password, " +
      
        " IsActive, " +
      
        " HackingAttempts " +
      
      ") Values (" +
      
        " @PersonId, " +
      
        " @Login, " +
      
        " @Password, " +
      
        " @IsActive, " +
      
        " @HackingAttempts " +
      
      ")";

      public static void Insert(User user)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@PersonId", user.PersonId);
      
        Database.PutParameter(dbCommand,"@Login", user.Login);
      
        Database.PutParameter(dbCommand,"@Password", user.Password);
      
        Database.PutParameter(dbCommand,"@IsActive", user.IsActive);
      
        Database.PutParameter(dbCommand,"@HackingAttempts", user.HackingAttempts);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          user.UserId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<User>  userList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(User user in  userList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@PersonId", user.PersonId);
      
        Database.PutParameter(dbCommand,"@Login", user.Login);
      
        Database.PutParameter(dbCommand,"@Password", user.Password);
      
        Database.PutParameter(dbCommand,"@IsActive", user.IsActive);
      
        Database.PutParameter(dbCommand,"@HackingAttempts", user.HackingAttempts);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@PersonId",user.PersonId);
      
        Database.UpdateParameter(dbCommand,"@Login",user.Login);
      
        Database.UpdateParameter(dbCommand,"@Password",user.Password);
      
        Database.UpdateParameter(dbCommand,"@IsActive",user.IsActive);
      
        Database.UpdateParameter(dbCommand,"@HackingAttempts",user.HackingAttempts);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        user.UserId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [User] Set "
      
        + " PersonId = @PersonId, "
      
        + " Login = @Login, "
      
        + " Password = @Password, "
      
        + " IsActive = @IsActive, "
      
        + " HackingAttempts = @HackingAttempts "
      
        + " Where "
        
          + " UserId = @UserId "
        
      ;

      public static void Update(User user)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@UserId", user.UserId);
      
        Database.PutParameter(dbCommand,"@PersonId", user.PersonId);
      
        Database.PutParameter(dbCommand,"@Login", user.Login);
      
        Database.PutParameter(dbCommand,"@Password", user.Password);
      
        Database.PutParameter(dbCommand,"@IsActive", user.IsActive);
      
        Database.PutParameter(dbCommand,"@HackingAttempts", user.HackingAttempts);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " UserId, "
      
        + " PersonId, "
      
        + " Login, "
      
        + " Password, "
      
        + " IsActive, "
      
        + " HackingAttempts "
      

      + " From [User] "

      
        + " Where "
        
          + " UserId = @UserId "
        
      ;

      public static User FindByPrimaryKey(
      int userId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@UserId", userId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("User not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(User user)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@UserId",user.UserId);
      

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
      String sql = "select 1 from User";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static User Load(IDataReader dataReader)
      {
      User user = new User();

      user.UserId = dataReader.GetInt32(0);
          user.PersonId = dataReader.GetInt32(1);
          user.Login = dataReader.GetString(2);
          user.Password = dataReader.GetString(3);
          user.IsActive = dataReader.GetBoolean(4);
          user.HackingAttempts = dataReader.GetInt32(5);
          

      return user;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [User] "

      
        + " Where "
        
          + " UserId = @UserId "
        
      ;
      public static void Delete(User user)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@UserId", user.UserId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [User] ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " UserId, "
      
        + " PersonId, "
      
        + " Login, "
      
        + " Password, "
      
        + " IsActive, "
      
        + " HackingAttempts "
      

      + " From [User] ";
      public static List<User> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
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
      
        protected int m_userId;
      
        protected int m_personId;
      
        protected String m_login;
      
        protected String m_password;
      
        protected bool m_isActive;
      
        protected int m_hackingAttempts;
      
      #endregion

      #region Constructors
      public User(
      int 
          userId
      )
      {
      
        m_userId = userId;
      
      }

      


        public User(
        int 
          userId,int 
          personId,String 
          login,String 
          password,bool 
          isActive,int 
          hackingAttempts
        )
        {
        
          m_userId = userId;
        
          m_personId = personId;
        
          m_login = login;
        
          m_password = password;
        
          m_isActive = isActive;
        
          m_hackingAttempts = hackingAttempts;
        
        }


      
      #endregion

      
        [XmlElement]
        public int UserId
        {
        get { return m_userId;}
        set { m_userId = value; }
        }
      
        [XmlElement]
        public int PersonId
        {
        get { return m_personId;}
        set { m_personId = value; }
        }
      
        [XmlElement]
        public String Login
        {
        get { return m_login;}
        set { m_login = value; }
        }
      
        [XmlElement]
        public String Password
        {
        get { return m_password;}
        set { m_password = value; }
        }
      
        [XmlElement]
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
        }
      
        [XmlElement]
        public int HackingAttempts
        {
        get { return m_hackingAttempts;}
        set { m_hackingAttempts = value; }
        }
      
      }
      #endregion
      }

    