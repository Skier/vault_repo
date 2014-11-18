
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
      public partial class UserActionType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into UserActionType ( " +
      
        " ID, " +
      
        " ActionType " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?ActionType " +
      
      ")";

      public static void Insert(UserActionType userActionType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", userActionType.ID);
      
        Database.PutParameter(dbCommand,"?ActionType", userActionType.ActionType);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(UserActionType userActionType)
      {
        Insert(userActionType, null);
      }


      public static void Insert(List<UserActionType>  userActionTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(UserActionType userActionType in  userActionTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", userActionType.ID);
      
        Database.PutParameter(dbCommand,"?ActionType", userActionType.ActionType);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",userActionType.ID);
      
        Database.UpdateParameter(dbCommand,"?ActionType",userActionType.ActionType);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<UserActionType>  userActionTypeList)
      {
        Insert(userActionTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update UserActionType Set "
      
        + " ActionType = ?ActionType "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(UserActionType userActionType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", userActionType.ID);
      
        Database.PutParameter(dbCommand,"?ActionType", userActionType.ActionType);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(UserActionType userActionType)
      {
        Update(userActionType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ActionType "
      

      + " From UserActionType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static UserActionType FindByPrimaryKey(
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
      throw new DataNotFoundException("UserActionType not found, search by primary key");

      }

      public static UserActionType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(UserActionType userActionType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",userActionType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(UserActionType userActionType)
      {
      return Exists(userActionType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from UserActionType limit 1";

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

      public static UserActionType Load(IDataReader dataReader, int offset)
      {
      UserActionType userActionType = new UserActionType();

      userActionType.ID = dataReader.GetInt32(0 + offset);
          userActionType.ActionType = dataReader.GetString(1 + offset);
          

      return userActionType;
      }

      public static UserActionType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From UserActionType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(UserActionType userActionType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", userActionType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(UserActionType userActionType)
      {
        Delete(userActionType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From UserActionType ";

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
      
        + " ActionType "
      

      + " From UserActionType ";
      public static List<UserActionType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<UserActionType> rv = new List<UserActionType>();

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

      public static List<UserActionType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<UserActionType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<UserActionType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserActionType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(UserActionType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<UserActionType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserActionType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<UserActionType> itemsList
      = new List<UserActionType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is UserActionType)
      itemsList.Add(deserializedObject as UserActionType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_actionType;
      
      #endregion

      #region Constructors
      public UserActionType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public UserActionType(
        int 
          iD,String 
          actionType
        ) : this()
        {
        
          m_iD = iD;
        
          m_actionType = actionType;
        
        }


      
      #endregion

      
        [DataMember]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [DataMember]
        public String ActionType
        {
        get { return m_actionType;}
        set { m_actionType = value; }
        }
      

      public static int FieldsCount
      {
      get { return 2; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    