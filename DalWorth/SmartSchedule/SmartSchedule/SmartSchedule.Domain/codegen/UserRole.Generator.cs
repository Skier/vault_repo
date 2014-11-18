
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
      public partial class UserRole : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into UserRole ( " +
      
        " ID, " +
      
        " Role " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Role " +
      
      ")";

      public static void Insert(UserRole userRole, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", userRole.ID);
      
        Database.PutParameter(dbCommand,"?Role", userRole.Role);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(UserRole userRole)
      {
        Insert(userRole, null);
      }


      public static void Insert(List<UserRole>  userRoleList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(UserRole userRole in  userRoleList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", userRole.ID);
      
        Database.PutParameter(dbCommand,"?Role", userRole.Role);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",userRole.ID);
      
        Database.UpdateParameter(dbCommand,"?Role",userRole.Role);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<UserRole>  userRoleList)
      {
        Insert(userRoleList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update UserRole Set "
      
        + " Role = ?Role "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(UserRole userRole, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", userRole.ID);
      
        Database.PutParameter(dbCommand,"?Role", userRole.Role);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(UserRole userRole)
      {
        Update(userRole, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Role "
      

      + " From UserRole "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static UserRole FindByPrimaryKey(
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
      throw new DataNotFoundException("UserRole not found, search by primary key");

      }

      public static UserRole FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(UserRole userRole, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",userRole.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(UserRole userRole)
      {
      return Exists(userRole, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from UserRole limit 1";

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

      public static UserRole Load(IDataReader dataReader, int offset)
      {
      UserRole userRole = new UserRole();

      userRole.ID = dataReader.GetInt32(0 + offset);
          userRole.Role = dataReader.GetString(1 + offset);
          

      return userRole;
      }

      public static UserRole Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From UserRole "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(UserRole userRole, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", userRole.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(UserRole userRole)
      {
        Delete(userRole, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From UserRole ";

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
      
        + " Role "
      

      + " From UserRole ";
      public static List<UserRole> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
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

      public static List<UserRole> Find()
      {
      return Find(null);
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
      
        protected int m_iD;
      
        protected String m_role;
      
      #endregion

      #region Constructors
      public UserRole(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public UserRole(
        int 
          iD,String 
          role
        ) : this()
        {
        
          m_iD = iD;
        
          m_role = role;
        
        }


      
      #endregion

      
        [DataMember]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [DataMember]
        public String Role
        {
        get { return m_role;}
        set { m_role = value; }
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

    