
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


      public partial class UserPreference
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [UserPreference] ( " +
      
        " UserId, " +
      
        " DefaultSite, " +
      
        " NewTracts " +
      
      ") Values (" +
      
        " @UserId, " +
      
        " @DefaultSite, " +
      
        " @NewTracts " +
      
      ")";

      public static void Insert(UserPreference userPreference)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@UserId", userPreference.UserId);
      
        Database.PutParameter(dbCommand,"@DefaultSite", userPreference.DefaultSite);
      
        Database.PutParameter(dbCommand,"@NewTracts", userPreference.NewTracts);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
          decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
          userPreference.UserPereferenceId = (int)identValue;
        }        
      

      }
      }

      public static void Insert(List<UserPreference>  userPreferenceList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(UserPreference userPreference in  userPreferenceList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@UserId", userPreference.UserId);
      
        Database.PutParameter(dbCommand,"@DefaultSite", userPreference.DefaultSite);
      
        Database.PutParameter(dbCommand,"@NewTracts", userPreference.NewTracts);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@UserId",userPreference.UserId);
      
        Database.UpdateParameter(dbCommand,"@DefaultSite",userPreference.DefaultSite);
      
        Database.UpdateParameter(dbCommand,"@NewTracts",userPreference.NewTracts);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT @@IDENTITY AS [IDENTITY]", dbCommand.Connection, dbCommand.Transaction))
        {
        decimal identValue = (decimal)dbIdentityCommand.ExecuteScalar();
        userPreference.UserPereferenceId = (int)identValue;
        }
      

      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [UserPreference] Set "
      
        + " UserId = @UserId, "
      
        + " DefaultSite = @DefaultSite, "
      
        + " NewTracts = @NewTracts "
      
        + " Where "
        
          + " UserPereferenceId = @UserPereferenceId "
        
      ;

      public static void Update(UserPreference userPreference)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@UserPereferenceId", userPreference.UserPereferenceId);
      
        Database.PutParameter(dbCommand,"@UserId", userPreference.UserId);
      
        Database.PutParameter(dbCommand,"@DefaultSite", userPreference.DefaultSite);
      
        Database.PutParameter(dbCommand,"@NewTracts", userPreference.NewTracts);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " UserPereferenceId, "
      
        + " UserId, "
      
        + " DefaultSite, "
      
        + " NewTracts "
      

      + " From [UserPreference] "

      
        + " Where "
        
          + " UserPereferenceId = @UserPereferenceId "
        
      ;

      public static UserPreference FindByPrimaryKey(
      int userPereferenceId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@UserPereferenceId", userPereferenceId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("UserPreference not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(UserPreference userPreference)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@UserPereferenceId",userPreference.UserPereferenceId);
      

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
      String sql = "select 1 from UserPreference";

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

      public static UserPreference Load(IDataReader dataReader)
      {
      UserPreference userPreference = new UserPreference();

      userPreference.UserPereferenceId = dataReader.GetInt32(0);
          userPreference.UserId = dataReader.GetInt32(1);
          userPreference.DefaultSite = dataReader.GetString(2);
          userPreference.NewTracts = dataReader.GetInt32(3);
          

      return userPreference;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [UserPreference] "

      
        + " Where "
        
          + " UserPereferenceId = @UserPereferenceId "
        
      ;
      public static void Delete(UserPreference userPreference)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@UserPereferenceId", userPreference.UserPereferenceId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [UserPreference] ";

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

      
        + " UserPereferenceId, "
      
        + " UserId, "
      
        + " DefaultSite, "
      
        + " NewTracts "
      

      + " From [UserPreference] ";
      public static List<UserPreference> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
      List<UserPreference> rv = new List<UserPreference>();

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
      List<UserPreference> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<UserPreference> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserPreference));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(UserPreference item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<UserPreference>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserPreference));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<UserPreference> itemsList
      = new List<UserPreference>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is UserPreference)
      itemsList.Add(deserializedObject as UserPreference);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_userPereferenceId;
      
        protected int m_userId;
      
        protected String m_defaultSite;
      
        protected int m_newTracts;
      
      #endregion

      #region Constructors
      public UserPreference(
      int 
          userPereferenceId
      )
      {
      
        m_userPereferenceId = userPereferenceId;
      
      }

      


        public UserPreference(
        int 
          userPereferenceId,int 
          userId,String 
          defaultSite,int 
          newTracts
        )
        {
        
          m_userPereferenceId = userPereferenceId;
        
          m_userId = userId;
        
          m_defaultSite = defaultSite;
        
          m_newTracts = newTracts;
        
        }


      
      #endregion

      
        [XmlElement]
        public int UserPereferenceId
        {
        get { return m_userPereferenceId;}
        set { m_userPereferenceId = value; }
        }
      
        [XmlElement]
        public int UserId
        {
        get { return m_userId;}
        set { m_userId = value; }
        }
      
        [XmlElement]
        public String DefaultSite
        {
        get { return m_defaultSite;}
        set { m_defaultSite = value; }
        }
      
        [XmlElement]
        public int NewTracts
        {
        get { return m_newTracts;}
        set { m_newTracts = value; }
        }
      
      }
      #endregion
      }

    