
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


      public partial class ConnectionKey : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ConnectionKey ( " +
      
        " ConnectionKeyValue, " +
      
        " IsActive " +
      
      ") Values (" +
      
        " ?ConnectionKeyValue, " +
      
        " ?IsActive " +
      
      ")";

      public static void Insert(ConnectionKey connectionKey, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ConnectionKeyValue", connectionKey.ConnectionKeyValue);
      
        Database.PutParameter(dbCommand,"?IsActive", connectionKey.IsActive);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ConnectionKey connectionKey)
      {
        Insert(connectionKey, null);
      }


      public static void Insert(List<ConnectionKey>  connectionKeyList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ConnectionKey connectionKey in  connectionKeyList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ConnectionKeyValue", connectionKey.ConnectionKeyValue);
      
        Database.PutParameter(dbCommand,"?IsActive", connectionKey.IsActive);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ConnectionKeyValue",connectionKey.ConnectionKeyValue);
      
        Database.UpdateParameter(dbCommand,"?IsActive",connectionKey.IsActive);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ConnectionKey>  connectionKeyList)
      {
        Insert(connectionKeyList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ConnectionKey Set "
      
        + " IsActive = ?IsActive "
      
        + " Where "
        
          + " ConnectionKeyValue = ?ConnectionKeyValue "
        
      ;

      public static void Update(ConnectionKey connectionKey, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ConnectionKeyValue", connectionKey.ConnectionKeyValue);
      
        Database.PutParameter(dbCommand,"?IsActive", connectionKey.IsActive);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ConnectionKey connectionKey)
      {
        Update(connectionKey, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ConnectionKeyValue, "
      
        + " IsActive "
      

      + " From ConnectionKey "

      
        + " Where "
        
          + " ConnectionKeyValue = ?ConnectionKeyValue "
        
      ;

      public static ConnectionKey FindByPrimaryKey(
      String connectionKeyValue, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ConnectionKeyValue", connectionKeyValue);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("ConnectionKey not found, search by primary key");

      }

      public static ConnectionKey FindByPrimaryKey(
      String connectionKeyValue
      )
      {
      return FindByPrimaryKey(
      connectionKeyValue, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ConnectionKey connectionKey, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ConnectionKeyValue",connectionKey.ConnectionKeyValue);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ConnectionKey connectionKey)
      {
      return Exists(connectionKey, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ConnectionKey limit 1";

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

      public static ConnectionKey Load(IDataReader dataReader, int offset)
      {
      ConnectionKey connectionKey = new ConnectionKey();

      connectionKey.ConnectionKeyValue = dataReader.GetString(0 + offset);
          connectionKey.IsActive = dataReader.GetBoolean(1 + offset);
          

      return connectionKey;
      }

      public static ConnectionKey Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ConnectionKey "

      
        + " Where "
        
          + " ConnectionKeyValue = ?ConnectionKeyValue "
        
      ;
      public static void Delete(ConnectionKey connectionKey, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ConnectionKeyValue", connectionKey.ConnectionKeyValue);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ConnectionKey connectionKey)
      {
        Delete(connectionKey, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ConnectionKey ";

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

      
        + " ConnectionKeyValue, "
      
        + " IsActive "
      

      + " From ConnectionKey ";
      public static List<ConnectionKey> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ConnectionKey> rv = new List<ConnectionKey>();

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

      public static List<ConnectionKey> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ConnectionKey> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ConnectionKey obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ConnectionKeyValue == obj.ConnectionKeyValue && IsActive == obj.IsActive;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ConnectionKey> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ConnectionKey));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ConnectionKey item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ConnectionKey>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ConnectionKey));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ConnectionKey> itemsList
      = new List<ConnectionKey>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ConnectionKey)
      itemsList.Add(deserializedObject as ConnectionKey);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_connectionKeyValue;
      
        protected bool m_isActive;
      
      #endregion

      #region Constructors
      public ConnectionKey(
      String 
          connectionKeyValue
      ) : this()
      {
      
        m_connectionKeyValue = connectionKeyValue;
      
      }

      


        public ConnectionKey(
        String 
          connectionKeyValue,bool 
          isActive
        ) : this()
        {
        
          m_connectionKeyValue = connectionKeyValue;
        
          m_isActive = isActive;
        
        }


      
      #endregion

      
        [XmlElement]
        public String ConnectionKeyValue
        {
        get { return m_connectionKeyValue;}
        set { m_connectionKeyValue = value; }
        }
      
        [XmlElement]
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
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

    