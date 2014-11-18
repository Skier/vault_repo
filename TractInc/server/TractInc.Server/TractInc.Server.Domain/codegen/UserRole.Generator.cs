
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


      public partial class UserRole
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [UserRole] ( " +
      
        " UserId, " +
      
        " RoleId " +
      
      ") Values (" +
      
        " @UserId, " +
      
        " @RoleId " +
      
      ")";

      public static void Insert(UserRole userRole)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@UserId", userRole.UserId);
      
        Database.PutParameter(dbCommand,"@RoleId", userRole.RoleId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          userRole.UserRoleId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<UserRole>  userRoleList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(UserRole userRole in  userRoleList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@UserId", userRole.UserId);
      
        Database.PutParameter(dbCommand,"@RoleId", userRole.RoleId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@UserId",userRole.UserId);
      
        Database.UpdateParameter(dbCommand,"@RoleId",userRole.RoleId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        userRole.UserRoleId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [UserRole] Set "
      
        + " UserId = @UserId, "
      
        + " RoleId = @RoleId "
      
        + " Where "
        
          + " UserRoleId = @UserRoleId "
        
      ;

      public static void Update(UserRole userRole)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@UserRoleId", userRole.UserRoleId);
      
        Database.PutParameter(dbCommand,"@UserId", userRole.UserId);
      
        Database.PutParameter(dbCommand,"@RoleId", userRole.RoleId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " UserRoleId, "
      
        + " UserId, "
      
        + " RoleId "
      

      + " From [UserRole] "

      
        + " Where "
        
          + " UserRoleId = @UserRoleId "
        
      ;

      public static UserRole FindByPrimaryKey(
      int userRoleId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@UserRoleId", userRoleId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("UserRole not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(UserRole userRole)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@UserRoleId",userRole.UserRoleId);
      

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
      String sql = "select 1 from UserRole";

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

      public static UserRole Load(IDataReader dataReader)
      {
      UserRole userRole = new UserRole();

      userRole.UserRoleId = dataReader.GetInt32(0);
          userRole.UserId = dataReader.GetInt32(1);
          userRole.RoleId = dataReader.GetInt32(2);
          

      return userRole;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [UserRole] "

      
        + " Where "
        
          + " UserRoleId = @UserRoleId "
        
      ;
      public static void Delete(UserRole userRole)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@UserRoleId", userRole.UserRoleId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [UserRole] ";

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

      
        + " UserRoleId, "
      
        + " UserId, "
      
        + " RoleId "
      

      + " From [UserRole] ";
      public static List<UserRole> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<UserRole> rv = new List<UserRole>();

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
      List<UserRole> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<UserRole> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserRole));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(UserRole item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<UserRole>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserRole));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<UserRole> itemsList
      = new List<UserRole>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is UserRole)
      itemsList.Add(deserializedObject as UserRole);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_userRoleId;
      
        protected int m_userId;
      
        protected int m_roleId;
      
      #endregion

      #region Constructors
      public UserRole(
      int 
          userRoleId
      )
      {
      
        m_userRoleId = userRoleId;
      
      }

      


        public UserRole(
        int 
          userRoleId,int 
          userId,int 
          roleId
        )
        {
        
          m_userRoleId = userRoleId;
        
          m_userId = userId;
        
          m_roleId = roleId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int UserRoleId
        {
        get { return m_userRoleId;}
        set { m_userRoleId = value; }
        }
      
        [XmlElement]
        public int UserId
        {
        get { return m_userId;}
        set { m_userId = value; }
        }
      
        [XmlElement]
        public int RoleId
        {
        get { return m_roleId;}
        set { m_roleId = value; }
        }
      
      }
      #endregion
      }

    