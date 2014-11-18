
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


      public partial class QbInvoiceSyncStatus : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbInvoiceSyncStatus ( " +
      
        " ID, " +
      
        " Name " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Name " +
      
      ")";

      public static void Insert(QbInvoiceSyncStatus qbInvoiceSyncStatus, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qbInvoiceSyncStatus.ID);
      
        Database.PutParameter(dbCommand,"?Name", qbInvoiceSyncStatus.Name);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbInvoiceSyncStatus qbInvoiceSyncStatus)
      {
        Insert(qbInvoiceSyncStatus, null);
      }


      public static void Insert(List<QbInvoiceSyncStatus>  qbInvoiceSyncStatusList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbInvoiceSyncStatus qbInvoiceSyncStatus in  qbInvoiceSyncStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", qbInvoiceSyncStatus.ID);
      
        Database.PutParameter(dbCommand,"?Name", qbInvoiceSyncStatus.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",qbInvoiceSyncStatus.ID);
      
        Database.UpdateParameter(dbCommand,"?Name",qbInvoiceSyncStatus.Name);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbInvoiceSyncStatus>  qbInvoiceSyncStatusList)
      {
        Insert(qbInvoiceSyncStatusList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbInvoiceSyncStatus Set "
      
        + " Name = ?Name "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(QbInvoiceSyncStatus qbInvoiceSyncStatus, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qbInvoiceSyncStatus.ID);
      
        Database.PutParameter(dbCommand,"?Name", qbInvoiceSyncStatus.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbInvoiceSyncStatus qbInvoiceSyncStatus)
      {
        Update(qbInvoiceSyncStatus, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Name "
      

      + " From QbInvoiceSyncStatus "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static QbInvoiceSyncStatus FindByPrimaryKey(
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
      throw new DataNotFoundException("QbInvoiceSyncStatus not found, search by primary key");

      }

      public static QbInvoiceSyncStatus FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbInvoiceSyncStatus qbInvoiceSyncStatus, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",qbInvoiceSyncStatus.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbInvoiceSyncStatus qbInvoiceSyncStatus)
      {
      return Exists(qbInvoiceSyncStatus, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbInvoiceSyncStatus limit 1";

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

      public static QbInvoiceSyncStatus Load(IDataReader dataReader, int offset)
      {
      QbInvoiceSyncStatus qbInvoiceSyncStatus = new QbInvoiceSyncStatus();

      qbInvoiceSyncStatus.ID = dataReader.GetInt32(0 + offset);
          qbInvoiceSyncStatus.Name = dataReader.GetString(1 + offset);
          

      return qbInvoiceSyncStatus;
      }

      public static QbInvoiceSyncStatus Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbInvoiceSyncStatus "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(QbInvoiceSyncStatus qbInvoiceSyncStatus, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", qbInvoiceSyncStatus.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbInvoiceSyncStatus qbInvoiceSyncStatus)
      {
        Delete(qbInvoiceSyncStatus, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbInvoiceSyncStatus ";

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
      
        + " Name "
      

      + " From QbInvoiceSyncStatus ";
      public static List<QbInvoiceSyncStatus> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbInvoiceSyncStatus> rv = new List<QbInvoiceSyncStatus>();

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

      public static List<QbInvoiceSyncStatus> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbInvoiceSyncStatus> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbInvoiceSyncStatus obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Name == obj.Name;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbInvoiceSyncStatus> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbInvoiceSyncStatus));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbInvoiceSyncStatus item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbInvoiceSyncStatus>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbInvoiceSyncStatus));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbInvoiceSyncStatus> itemsList
      = new List<QbInvoiceSyncStatus>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbInvoiceSyncStatus)
      itemsList.Add(deserializedObject as QbInvoiceSyncStatus);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_name;
      
      #endregion

      #region Constructors
      public QbInvoiceSyncStatus(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public QbInvoiceSyncStatus(
        int 
          iD,String 
          name
        ) : this()
        {
        
          m_iD = iD;
        
          m_name = name;
        
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

    