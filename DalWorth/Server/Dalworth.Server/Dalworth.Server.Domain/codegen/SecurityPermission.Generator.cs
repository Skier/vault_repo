
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


      public partial class SecurityPermission : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into SecurityPermission ( " +
      
        " Name, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?Name, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(SecurityPermission securityPermission, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?Name", securityPermission.Name);
      
        Database.PutParameter(dbCommand,"?Description", securityPermission.Description);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        securityPermission.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(SecurityPermission securityPermission)
      {
        Insert(securityPermission, null);
      }


      public static void Insert(List<SecurityPermission>  securityPermissionList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(SecurityPermission securityPermission in  securityPermissionList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?Name", securityPermission.Name);
      
        Database.PutParameter(dbCommand,"?Description", securityPermission.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?Name",securityPermission.Name);
      
        Database.UpdateParameter(dbCommand,"?Description",securityPermission.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        securityPermission.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<SecurityPermission>  securityPermissionList)
      {
        Insert(securityPermissionList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update SecurityPermission Set "
      
        + " Name = ?Name, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(SecurityPermission securityPermission, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", securityPermission.ID);
      
        Database.PutParameter(dbCommand,"?Name", securityPermission.Name);
      
        Database.PutParameter(dbCommand,"?Description", securityPermission.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(SecurityPermission securityPermission)
      {
        Update(securityPermission, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Name, "
      
        + " Description "
      

      + " From SecurityPermission "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static SecurityPermission FindByPrimaryKey(
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
      throw new DataNotFoundException("SecurityPermission not found, search by primary key");

      }

      public static SecurityPermission FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(SecurityPermission securityPermission, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",securityPermission.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(SecurityPermission securityPermission)
      {
      return Exists(securityPermission, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from SecurityPermission limit 1";

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

      public static SecurityPermission Load(IDataReader dataReader, int offset)
      {
      SecurityPermission securityPermission = new SecurityPermission();

      securityPermission.ID = dataReader.GetInt32(0 + offset);
          securityPermission.Name = dataReader.GetString(1 + offset);
          securityPermission.Description = dataReader.GetString(2 + offset);
          

      return securityPermission;
      }

      public static SecurityPermission Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From SecurityPermission "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(SecurityPermission securityPermission, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", securityPermission.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(SecurityPermission securityPermission)
      {
        Delete(securityPermission, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From SecurityPermission ";

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
      

      + " From SecurityPermission ";
      public static List<SecurityPermission> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<SecurityPermission> rv = new List<SecurityPermission>();

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

      public static List<SecurityPermission> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<SecurityPermission> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (SecurityPermission obj)
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

      List<SecurityPermission> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SecurityPermission));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(SecurityPermission item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<SecurityPermission>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SecurityPermission));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<SecurityPermission> itemsList
      = new List<SecurityPermission>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is SecurityPermission)
      itemsList.Add(deserializedObject as SecurityPermission);
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
      public SecurityPermission(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public SecurityPermission(
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

    