
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


      public partial class QbAccount : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbAccount ( " +
      
        " ListId, " +
      
        " FullName, " +
      
        " AccountType, " +
      
        " TimeCreated, " +
      
        " TimeModified, " +
      
        " EditSequence " +
      
      ") Values (" +
      
        " ?ListId, " +
      
        " ?FullName, " +
      
        " ?AccountType, " +
      
        " ?TimeCreated, " +
      
        " ?TimeModified, " +
      
        " ?EditSequence " +
      
      ")";

      public static void Insert(QbAccount qbAccount, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbAccount.ListId);
      
        Database.PutParameter(dbCommand,"?FullName", qbAccount.FullName);
      
        Database.PutParameter(dbCommand,"?AccountType", qbAccount.AccountType);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbAccount.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbAccount.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbAccount.EditSequence);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbAccount qbAccount)
      {
        Insert(qbAccount, null);
      }


      public static void Insert(List<QbAccount>  qbAccountList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbAccount qbAccount in  qbAccountList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbAccount.ListId);
      
        Database.PutParameter(dbCommand,"?FullName", qbAccount.FullName);
      
        Database.PutParameter(dbCommand,"?AccountType", qbAccount.AccountType);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbAccount.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbAccount.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbAccount.EditSequence);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ListId",qbAccount.ListId);
      
        Database.UpdateParameter(dbCommand,"?FullName",qbAccount.FullName);
      
        Database.UpdateParameter(dbCommand,"?AccountType",qbAccount.AccountType);
      
        Database.UpdateParameter(dbCommand,"?TimeCreated",qbAccount.TimeCreated);
      
        Database.UpdateParameter(dbCommand,"?TimeModified",qbAccount.TimeModified);
      
        Database.UpdateParameter(dbCommand,"?EditSequence",qbAccount.EditSequence);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbAccount>  qbAccountList)
      {
        Insert(qbAccountList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbAccount Set "
      
        + " FullName = ?FullName, "
      
        + " AccountType = ?AccountType, "
      
        + " TimeCreated = ?TimeCreated, "
      
        + " TimeModified = ?TimeModified, "
      
        + " EditSequence = ?EditSequence "
      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static void Update(QbAccount qbAccount, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbAccount.ListId);
      
        Database.PutParameter(dbCommand,"?FullName", qbAccount.FullName);
      
        Database.PutParameter(dbCommand,"?AccountType", qbAccount.AccountType);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbAccount.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbAccount.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbAccount.EditSequence);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbAccount qbAccount)
      {
        Update(qbAccount, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ListId, "
      
        + " FullName, "
      
        + " AccountType, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence "
      

      + " From QbAccount "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static QbAccount FindByPrimaryKey(
      String listId, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", listId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("QbAccount not found, search by primary key");

      }

      public static QbAccount FindByPrimaryKey(
      String listId
      )
      {
      return FindByPrimaryKey(
      listId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbAccount qbAccount, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId",qbAccount.ListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbAccount qbAccount)
      {
      return Exists(qbAccount, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbAccount limit 1";

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

      public static QbAccount Load(IDataReader dataReader, int offset)
      {
      QbAccount qbAccount = new QbAccount();

      qbAccount.ListId = dataReader.GetString(0 + offset);
          qbAccount.FullName = dataReader.GetString(1 + offset);
          qbAccount.AccountType = dataReader.GetString(2 + offset);
          qbAccount.TimeCreated = dataReader.GetDateTime(3 + offset);
          qbAccount.TimeModified = dataReader.GetDateTime(4 + offset);
          qbAccount.EditSequence = dataReader.GetString(5 + offset);
          

      return qbAccount;
      }

      public static QbAccount Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbAccount "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;
      public static void Delete(QbAccount qbAccount, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ListId", qbAccount.ListId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbAccount qbAccount)
      {
        Delete(qbAccount, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbAccount ";

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

      
        + " ListId, "
      
        + " FullName, "
      
        + " AccountType, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence "
      

      + " From QbAccount ";
      public static List<QbAccount> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbAccount> rv = new List<QbAccount>();

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

      public static List<QbAccount> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbAccount> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbAccount obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ListId == obj.ListId && FullName == obj.FullName && AccountType == obj.AccountType && TimeCreated == obj.TimeCreated && TimeModified == obj.TimeModified && EditSequence == obj.EditSequence;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbAccount> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbAccount));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbAccount item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbAccount>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbAccount));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbAccount> itemsList
      = new List<QbAccount>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbAccount)
      itemsList.Add(deserializedObject as QbAccount);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_listId;
      
        protected String m_fullName;
      
        protected String m_accountType;
      
        protected DateTime m_timeCreated;
      
        protected DateTime m_timeModified;
      
        protected String m_editSequence;
      
      #endregion

      #region Constructors
      public QbAccount(
      String 
          listId
      ) : this()
      {
      
        m_listId = listId;
      
      }

      


        public QbAccount(
        String 
          listId,String 
          fullName,String 
          accountType,DateTime 
          timeCreated,DateTime 
          timeModified,String 
          editSequence
        ) : this()
        {
        
          m_listId = listId;
        
          m_fullName = fullName;
        
          m_accountType = accountType;
        
          m_timeCreated = timeCreated;
        
          m_timeModified = timeModified;
        
          m_editSequence = editSequence;
        
        }


      
      #endregion

      
        [XmlElement]
        public String ListId
        {
        get { return m_listId;}
        set { m_listId = value; }
        }
      
        [XmlElement]
        public String FullName
        {
        get { return m_fullName;}
        set { m_fullName = value; }
        }
      
        [XmlElement]
        public String AccountType
        {
        get { return m_accountType;}
        set { m_accountType = value; }
        }
      
        [XmlElement]
        public DateTime TimeCreated
        {
        get { return m_timeCreated;}
        set { m_timeCreated = value; }
        }
      
        [XmlElement]
        public DateTime TimeModified
        {
        get { return m_timeModified;}
        set { m_timeModified = value; }
        }
      
        [XmlElement]
        public String EditSequence
        {
        get { return m_editSequence;}
        set { m_editSequence = value; }
        }
      

      public static int FieldsCount
      {
      get { return 6; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    