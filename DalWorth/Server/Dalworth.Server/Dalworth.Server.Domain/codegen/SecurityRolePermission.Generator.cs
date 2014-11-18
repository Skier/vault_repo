
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


      public partial class SecurityRolePermission : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into SecurityRolePermission ( " +
      
        " SecurityRoleId, " +
      
        " SecurityPermissionId " +
      
      ") Values (" +
      
        " ?SecurityRoleId, " +
      
        " ?SecurityPermissionId " +
      
      ")";

      public static void Insert(SecurityRolePermission securityRolePermission, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?SecurityRoleId", securityRolePermission.SecurityRoleId);
      
        Database.PutParameter(dbCommand,"?SecurityPermissionId", securityRolePermission.SecurityPermissionId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(SecurityRolePermission securityRolePermission)
      {
        Insert(securityRolePermission, null);
      }


      public static void Insert(List<SecurityRolePermission>  securityRolePermissionList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(SecurityRolePermission securityRolePermission in  securityRolePermissionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?SecurityRoleId", securityRolePermission.SecurityRoleId);
      
        Database.PutParameter(dbCommand,"?SecurityPermissionId", securityRolePermission.SecurityPermissionId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?SecurityRoleId",securityRolePermission.SecurityRoleId);
      
        Database.UpdateParameter(dbCommand,"?SecurityPermissionId",securityRolePermission.SecurityPermissionId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<SecurityRolePermission>  securityRolePermissionList)
      {
        Insert(securityRolePermissionList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update SecurityRolePermission Set "
      
        + " Where "
        
          + " SecurityRoleId = ?SecurityRoleId and  "
        
          + " SecurityPermissionId = ?SecurityPermissionId "
        
      ;

      public static void Update(SecurityRolePermission securityRolePermission, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?SecurityRoleId", securityRolePermission.SecurityRoleId);
      
        Database.PutParameter(dbCommand,"?SecurityPermissionId", securityRolePermission.SecurityPermissionId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(SecurityRolePermission securityRolePermission)
      {
        Update(securityRolePermission, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " SecurityRoleId, "
      
        + " SecurityPermissionId "
      

      + " From SecurityRolePermission "

      
        + " Where "
        
          + " SecurityRoleId = ?SecurityRoleId and  "
        
          + " SecurityPermissionId = ?SecurityPermissionId "
        
      ;

      public static SecurityRolePermission FindByPrimaryKey(
      int securityRoleId,int securityPermissionId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?SecurityRoleId", securityRoleId);
      
        Database.PutParameter(dbCommand,"?SecurityPermissionId", securityPermissionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("SecurityRolePermission not found, search by primary key");

      }

      public static SecurityRolePermission FindByPrimaryKey(
      int securityRoleId,int securityPermissionId
      )
      {
      return FindByPrimaryKey(
      securityRoleId,securityPermissionId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(SecurityRolePermission securityRolePermission, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?SecurityRoleId",securityRolePermission.SecurityRoleId);
      
        Database.PutParameter(dbCommand,"?SecurityPermissionId",securityRolePermission.SecurityPermissionId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(SecurityRolePermission securityRolePermission)
      {
      return Exists(securityRolePermission, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from SecurityRolePermission limit 1";

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

      public static SecurityRolePermission Load(IDataReader dataReader, int offset)
      {
      SecurityRolePermission securityRolePermission = new SecurityRolePermission();

      securityRolePermission.SecurityRoleId = dataReader.GetInt32(0 + offset);
          securityRolePermission.SecurityPermissionId = dataReader.GetInt32(1 + offset);
          

      return securityRolePermission;
      }

      public static SecurityRolePermission Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From SecurityRolePermission "

      
        + " Where "
        
          + " SecurityRoleId = ?SecurityRoleId and  "
        
          + " SecurityPermissionId = ?SecurityPermissionId "
        
      ;
      public static void Delete(SecurityRolePermission securityRolePermission, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?SecurityRoleId", securityRolePermission.SecurityRoleId);
      
        Database.PutParameter(dbCommand,"?SecurityPermissionId", securityRolePermission.SecurityPermissionId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(SecurityRolePermission securityRolePermission)
      {
        Delete(securityRolePermission, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From SecurityRolePermission ";

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

      
        + " SecurityRoleId, "
      
        + " SecurityPermissionId "
      

      + " From SecurityRolePermission ";
      public static List<SecurityRolePermission> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<SecurityRolePermission> rv = new List<SecurityRolePermission>();

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

      public static List<SecurityRolePermission> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<SecurityRolePermission> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (SecurityRolePermission obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return SecurityRoleId == obj.SecurityRoleId && SecurityPermissionId == obj.SecurityPermissionId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<SecurityRolePermission> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SecurityRolePermission));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(SecurityRolePermission item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<SecurityRolePermission>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SecurityRolePermission));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<SecurityRolePermission> itemsList
      = new List<SecurityRolePermission>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is SecurityRolePermission)
      itemsList.Add(deserializedObject as SecurityRolePermission);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_securityRoleId;
      
        protected int m_securityPermissionId;
      
      #endregion

      #region Constructors
      public SecurityRolePermission(
      int 
          securityRoleId,int 
          securityPermissionId
      ) : this()
      {
      
        m_securityRoleId = securityRoleId;
      
        m_securityPermissionId = securityPermissionId;
      
      }

      
      #endregion

      
        [XmlElement]
        public int SecurityRoleId
        {
        get { return m_securityRoleId;}
        set { m_securityRoleId = value; }
        }
      
        [XmlElement]
        public int SecurityPermissionId
        {
        get { return m_securityPermissionId;}
        set { m_securityPermissionId = value; }
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

    