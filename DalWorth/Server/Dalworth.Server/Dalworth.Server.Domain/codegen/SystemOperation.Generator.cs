
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


      public partial class SystemOperation : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into SystemOperation ( " +
      
        " Name, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?Name, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(SystemOperation systemOperation, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?Name", systemOperation.Name);
      
        Database.PutParameter(dbCommand,"?Description", systemOperation.Description);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        systemOperation.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(SystemOperation systemOperation)
      {
        Insert(systemOperation, null);
      }


      public static void Insert(List<SystemOperation>  systemOperationList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(SystemOperation systemOperation in  systemOperationList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?Name", systemOperation.Name);
      
        Database.PutParameter(dbCommand,"?Description", systemOperation.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?Name",systemOperation.Name);
      
        Database.UpdateParameter(dbCommand,"?Description",systemOperation.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        systemOperation.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<SystemOperation>  systemOperationList)
      {
        Insert(systemOperationList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update SystemOperation Set "
      
        + " Name = ?Name, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(SystemOperation systemOperation, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", systemOperation.ID);
      
        Database.PutParameter(dbCommand,"?Name", systemOperation.Name);
      
        Database.PutParameter(dbCommand,"?Description", systemOperation.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(SystemOperation systemOperation)
      {
        Update(systemOperation, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Name, "
      
        + " Description "
      

      + " From SystemOperation "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static SystemOperation FindByPrimaryKey(
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
      throw new DataNotFoundException("SystemOperation not found, search by primary key");

      }

      public static SystemOperation FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(SystemOperation systemOperation, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",systemOperation.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(SystemOperation systemOperation)
      {
      return Exists(systemOperation, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from SystemOperation limit 1";

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

      public static SystemOperation Load(IDataReader dataReader, int offset)
      {
      SystemOperation systemOperation = new SystemOperation();

      systemOperation.ID = dataReader.GetInt32(0 + offset);
          systemOperation.Name = dataReader.GetString(1 + offset);
          systemOperation.Description = dataReader.GetString(2 + offset);
          

      return systemOperation;
      }

      public static SystemOperation Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From SystemOperation "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(SystemOperation systemOperation, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", systemOperation.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(SystemOperation systemOperation)
      {
        Delete(systemOperation, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From SystemOperation ";

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
      

      + " From SystemOperation ";
      public static List<SystemOperation> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<SystemOperation> rv = new List<SystemOperation>();

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

      public static List<SystemOperation> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<SystemOperation> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (SystemOperation obj)
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

      List<SystemOperation> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SystemOperation));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(SystemOperation item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<SystemOperation>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(SystemOperation));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<SystemOperation> itemsList
      = new List<SystemOperation>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is SystemOperation)
      itemsList.Add(deserializedObject as SystemOperation);
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
      public SystemOperation(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public SystemOperation(
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

    