
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


      public partial class SecurityRole : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into SecurityRole ( " +
      
        " Name, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?Name, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(SecurityRole securityRole, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?Name", securityRole.Name);
      
        Database.PutParameter(dbCommand,"?Description", securityRole.Description);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        securityRole.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(SecurityRole securityRole)
      {
        Insert(securityRole, null);
      }


      public static void Insert(List<SecurityRole>  securityRoleList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(SecurityRole securityRole in  securityRoleList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?Name", securityRole.Name);
      
        Database.PutParameter(dbCommand,"?Description", securityRole.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?Name",securityRole.Name);
      
        Database.UpdateParameter(dbCommand,"?Description",securityRole.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        securityRole.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<SecurityRole>  securityRoleList)
      {
        Insert(securityRoleList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update SecurityRole Set "
      
        + " Name = ?Name, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(SecurityRole securityRole, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", securityRole.ID);
      
        Database.PutParameter(dbCommand,"?Name", securityRole.Name);
      
        Database.PutParameter(dbCommand,"?Description", securityRole.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(SecurityRole securityRole)
      {
        Update(securityRole, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Name, "
      
        + " Description "
      

      + " From SecurityRole "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static SecurityRole FindByPrimaryKey(
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
      throw new DataNotFoundException("SecurityRole not found, search by primary key");

      }

      public static SecurityRole FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(SecurityRole securityRole, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",securityRole.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(SecurityRole securityRole)
      {
      return Exists(securityRole, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from SecurityRole limit 1";

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

      public static SecurityRole Load(IDataReader dataReader, int offset)
      {
      SecurityRole securityRole = new SecurityRole();

      securityRole.ID = dataReader.GetInt32(0 + offset);
          securityRole.Name = dataReader.GetString(1 + offset);
          securityRole.Description = dataReader.GetString(2 + offset);
          

      return securityRole;
      }

      public static SecurityRole Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From SecurityRole "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(SecurityRole securityRole, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", securityRole.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(SecurityRole securityRole)
      {
        Delete(securityRole, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From SecurityRole ";

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
      

      + " From SecurityRole ";
      public static List<SecurityRole> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<SecurityRole> rv = new List<SecurityRole>();

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

      public static List<SecurityRole> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<SecurityRole> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (SecurityRole obj)
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

      List<SecurityRole> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SecurityRole));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(SecurityRole item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<SecurityRole>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SecurityRole));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<SecurityRole> itemsList
      = new List<SecurityRole>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is SecurityRole)
      itemsList.Add(deserializedObject as SecurityRole);
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
      public SecurityRole(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public SecurityRole(
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

    