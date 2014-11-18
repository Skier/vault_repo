
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


      public partial class QbSalesTaxCode : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbSalesTaxCode ( " +
      
        " ListId, " +
      
        " TimeCreated, " +
      
        " TimeModified, " +
      
        " EditSequence, " +
      
        " Name, " +
      
        " IsActive, " +
      
        " IsTaxable, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ListId, " +
      
        " ?TimeCreated, " +
      
        " ?TimeModified, " +
      
        " ?EditSequence, " +
      
        " ?Name, " +
      
        " ?IsActive, " +
      
        " ?IsTaxable, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(QbSalesTaxCode qbSalesTaxCode, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbSalesTaxCode.ListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbSalesTaxCode.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbSalesTaxCode.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbSalesTaxCode.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbSalesTaxCode.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbSalesTaxCode.IsActive);
      
        Database.PutParameter(dbCommand,"?IsTaxable", qbSalesTaxCode.IsTaxable);
      
        Database.PutParameter(dbCommand,"?Description", qbSalesTaxCode.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbSalesTaxCode qbSalesTaxCode)
      {
        Insert(qbSalesTaxCode, null);
      }


      public static void Insert(List<QbSalesTaxCode>  qbSalesTaxCodeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbSalesTaxCode qbSalesTaxCode in  qbSalesTaxCodeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbSalesTaxCode.ListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbSalesTaxCode.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbSalesTaxCode.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbSalesTaxCode.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbSalesTaxCode.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbSalesTaxCode.IsActive);
      
        Database.PutParameter(dbCommand,"?IsTaxable", qbSalesTaxCode.IsTaxable);
      
        Database.PutParameter(dbCommand,"?Description", qbSalesTaxCode.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ListId",qbSalesTaxCode.ListId);
      
        Database.UpdateParameter(dbCommand,"?TimeCreated",qbSalesTaxCode.TimeCreated);
      
        Database.UpdateParameter(dbCommand,"?TimeModified",qbSalesTaxCode.TimeModified);
      
        Database.UpdateParameter(dbCommand,"?EditSequence",qbSalesTaxCode.EditSequence);
      
        Database.UpdateParameter(dbCommand,"?Name",qbSalesTaxCode.Name);
      
        Database.UpdateParameter(dbCommand,"?IsActive",qbSalesTaxCode.IsActive);
      
        Database.UpdateParameter(dbCommand,"?IsTaxable",qbSalesTaxCode.IsTaxable);
      
        Database.UpdateParameter(dbCommand,"?Description",qbSalesTaxCode.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbSalesTaxCode>  qbSalesTaxCodeList)
      {
        Insert(qbSalesTaxCodeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbSalesTaxCode Set "
      
        + " TimeCreated = ?TimeCreated, "
      
        + " TimeModified = ?TimeModified, "
      
        + " EditSequence = ?EditSequence, "
      
        + " Name = ?Name, "
      
        + " IsActive = ?IsActive, "
      
        + " IsTaxable = ?IsTaxable, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static void Update(QbSalesTaxCode qbSalesTaxCode, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbSalesTaxCode.ListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbSalesTaxCode.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbSalesTaxCode.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbSalesTaxCode.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbSalesTaxCode.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbSalesTaxCode.IsActive);
      
        Database.PutParameter(dbCommand,"?IsTaxable", qbSalesTaxCode.IsTaxable);
      
        Database.PutParameter(dbCommand,"?Description", qbSalesTaxCode.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbSalesTaxCode qbSalesTaxCode)
      {
        Update(qbSalesTaxCode, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ListId, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence, "
      
        + " Name, "
      
        + " IsActive, "
      
        + " IsTaxable, "
      
        + " Description "
      

      + " From QbSalesTaxCode "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static QbSalesTaxCode FindByPrimaryKey(
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
      throw new DataNotFoundException("QbSalesTaxCode not found, search by primary key");

      }

      public static QbSalesTaxCode FindByPrimaryKey(
      String listId
      )
      {
      return FindByPrimaryKey(
      listId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbSalesTaxCode qbSalesTaxCode, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId",qbSalesTaxCode.ListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbSalesTaxCode qbSalesTaxCode)
      {
      return Exists(qbSalesTaxCode, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbSalesTaxCode limit 1";

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

      public static QbSalesTaxCode Load(IDataReader dataReader, int offset)
      {
      QbSalesTaxCode qbSalesTaxCode = new QbSalesTaxCode();

      qbSalesTaxCode.ListId = dataReader.GetString(0 + offset);
          qbSalesTaxCode.TimeCreated = dataReader.GetDateTime(1 + offset);
          qbSalesTaxCode.TimeModified = dataReader.GetDateTime(2 + offset);
          qbSalesTaxCode.EditSequence = dataReader.GetString(3 + offset);
          qbSalesTaxCode.Name = dataReader.GetString(4 + offset);
          qbSalesTaxCode.IsActive = dataReader.GetBoolean(5 + offset);
          qbSalesTaxCode.IsTaxable = dataReader.GetBoolean(6 + offset);
          qbSalesTaxCode.Description = dataReader.GetString(7 + offset);
          

      return qbSalesTaxCode;
      }

      public static QbSalesTaxCode Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbSalesTaxCode "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;
      public static void Delete(QbSalesTaxCode qbSalesTaxCode, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ListId", qbSalesTaxCode.ListId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbSalesTaxCode qbSalesTaxCode)
      {
        Delete(qbSalesTaxCode, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbSalesTaxCode ";

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
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence, "
      
        + " Name, "
      
        + " IsActive, "
      
        + " IsTaxable, "
      
        + " Description "
      

      + " From QbSalesTaxCode ";
      public static List<QbSalesTaxCode> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbSalesTaxCode> rv = new List<QbSalesTaxCode>();

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

      public static List<QbSalesTaxCode> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbSalesTaxCode> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbSalesTaxCode obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ListId == obj.ListId && TimeCreated == obj.TimeCreated && TimeModified == obj.TimeModified && EditSequence == obj.EditSequence && Name == obj.Name && IsActive == obj.IsActive && IsTaxable == obj.IsTaxable && Description == obj.Description;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbSalesTaxCode> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbSalesTaxCode));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbSalesTaxCode item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbSalesTaxCode>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbSalesTaxCode));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbSalesTaxCode> itemsList
      = new List<QbSalesTaxCode>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbSalesTaxCode)
      itemsList.Add(deserializedObject as QbSalesTaxCode);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_listId;
      
        protected DateTime m_timeCreated;
      
        protected DateTime m_timeModified;
      
        protected String m_editSequence;
      
        protected String m_name;
      
        protected bool m_isActive;
      
        protected bool m_isTaxable;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public QbSalesTaxCode(
      String 
          listId
      ) : this()
      {
      
        m_listId = listId;
      
      }

      


        public QbSalesTaxCode(
        String 
          listId,DateTime 
          timeCreated,DateTime 
          timeModified,String 
          editSequence,String 
          name,bool 
          isActive,bool 
          isTaxable,String 
          description
        ) : this()
        {
        
          m_listId = listId;
        
          m_timeCreated = timeCreated;
        
          m_timeModified = timeModified;
        
          m_editSequence = editSequence;
        
          m_name = name;
        
          m_isActive = isActive;
        
          m_isTaxable = isTaxable;
        
          m_description = description;
        
        }


      
      #endregion

      
        [XmlElement]
        public String ListId
        {
        get { return m_listId;}
        set { m_listId = value; }
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
        public bool IsTaxable
        {
        get { return m_isTaxable;}
        set { m_isTaxable = value; }
        }
      
        [XmlElement]
        public String Description
        {
        get { return m_description;}
        set { m_description = value; }
        }
      

      public static int FieldsCount
      {
      get { return 8; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    