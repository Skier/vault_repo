
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


      public partial class QbPaymentMethod : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbPaymentMethod ( " +
      
        " ListId, " +
      
        " Name, " +
      
        " IsActive, " +
      
        " TimeCreated, " +
      
        " TimeModified, " +
      
        " EditSequence " +
      
      ") Values (" +
      
        " ?ListId, " +
      
        " ?Name, " +
      
        " ?IsActive, " +
      
        " ?TimeCreated, " +
      
        " ?TimeModified, " +
      
        " ?EditSequence " +
      
      ")";

      public static void Insert(QbPaymentMethod qbPaymentMethod, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbPaymentMethod.ListId);
      
        Database.PutParameter(dbCommand,"?Name", qbPaymentMethod.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbPaymentMethod.IsActive);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbPaymentMethod.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbPaymentMethod.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbPaymentMethod.EditSequence);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbPaymentMethod qbPaymentMethod)
      {
        Insert(qbPaymentMethod, null);
      }


      public static void Insert(List<QbPaymentMethod>  qbPaymentMethodList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbPaymentMethod qbPaymentMethod in  qbPaymentMethodList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbPaymentMethod.ListId);
      
        Database.PutParameter(dbCommand,"?Name", qbPaymentMethod.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbPaymentMethod.IsActive);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbPaymentMethod.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbPaymentMethod.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbPaymentMethod.EditSequence);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ListId",qbPaymentMethod.ListId);
      
        Database.UpdateParameter(dbCommand,"?Name",qbPaymentMethod.Name);
      
        Database.UpdateParameter(dbCommand,"?IsActive",qbPaymentMethod.IsActive);
      
        Database.UpdateParameter(dbCommand,"?TimeCreated",qbPaymentMethod.TimeCreated);
      
        Database.UpdateParameter(dbCommand,"?TimeModified",qbPaymentMethod.TimeModified);
      
        Database.UpdateParameter(dbCommand,"?EditSequence",qbPaymentMethod.EditSequence);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbPaymentMethod>  qbPaymentMethodList)
      {
        Insert(qbPaymentMethodList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbPaymentMethod Set "
      
        + " Name = ?Name, "
      
        + " IsActive = ?IsActive, "
      
        + " TimeCreated = ?TimeCreated, "
      
        + " TimeModified = ?TimeModified, "
      
        + " EditSequence = ?EditSequence "
      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static void Update(QbPaymentMethod qbPaymentMethod, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbPaymentMethod.ListId);
      
        Database.PutParameter(dbCommand,"?Name", qbPaymentMethod.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbPaymentMethod.IsActive);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbPaymentMethod.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbPaymentMethod.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbPaymentMethod.EditSequence);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbPaymentMethod qbPaymentMethod)
      {
        Update(qbPaymentMethod, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ListId, "
      
        + " Name, "
      
        + " IsActive, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence "
      

      + " From QbPaymentMethod "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static QbPaymentMethod FindByPrimaryKey(
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
      throw new DataNotFoundException("QbPaymentMethod not found, search by primary key");

      }

      public static QbPaymentMethod FindByPrimaryKey(
      String listId
      )
      {
      return FindByPrimaryKey(
      listId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbPaymentMethod qbPaymentMethod, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId",qbPaymentMethod.ListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbPaymentMethod qbPaymentMethod)
      {
      return Exists(qbPaymentMethod, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbPaymentMethod limit 1";

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

      public static QbPaymentMethod Load(IDataReader dataReader, int offset)
      {
      QbPaymentMethod qbPaymentMethod = new QbPaymentMethod();

      qbPaymentMethod.ListId = dataReader.GetString(0 + offset);
          qbPaymentMethod.Name = dataReader.GetString(1 + offset);
          qbPaymentMethod.IsActive = dataReader.GetBoolean(2 + offset);
          qbPaymentMethod.TimeCreated = dataReader.GetDateTime(3 + offset);
          qbPaymentMethod.TimeModified = dataReader.GetDateTime(4 + offset);
          qbPaymentMethod.EditSequence = dataReader.GetString(5 + offset);
          

      return qbPaymentMethod;
      }

      public static QbPaymentMethod Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbPaymentMethod "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;
      public static void Delete(QbPaymentMethod qbPaymentMethod, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ListId", qbPaymentMethod.ListId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbPaymentMethod qbPaymentMethod)
      {
        Delete(qbPaymentMethod, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbPaymentMethod ";

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
      
        + " Name, "
      
        + " IsActive, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence "
      

      + " From QbPaymentMethod ";
      public static List<QbPaymentMethod> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbPaymentMethod> rv = new List<QbPaymentMethod>();

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

      public static List<QbPaymentMethod> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbPaymentMethod> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbPaymentMethod obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ListId == obj.ListId && Name == obj.Name && IsActive == obj.IsActive && TimeCreated == obj.TimeCreated && TimeModified == obj.TimeModified && EditSequence == obj.EditSequence;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbPaymentMethod> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbPaymentMethod));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbPaymentMethod item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbPaymentMethod>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbPaymentMethod));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbPaymentMethod> itemsList
      = new List<QbPaymentMethod>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbPaymentMethod)
      itemsList.Add(deserializedObject as QbPaymentMethod);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_listId;
      
        protected String m_name;
      
        protected bool m_isActive;
      
        protected DateTime m_timeCreated;
      
        protected DateTime m_timeModified;
      
        protected String m_editSequence;
      
      #endregion

      #region Constructors
      public QbPaymentMethod(
      String 
          listId
      ) : this()
      {
      
        m_listId = listId;
      
      }

      


        public QbPaymentMethod(
        String 
          listId,String 
          name,bool 
          isActive,DateTime 
          timeCreated,DateTime 
          timeModified,String 
          editSequence
        ) : this()
        {
        
          m_listId = listId;
        
          m_name = name;
        
          m_isActive = isActive;
        
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
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
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

    