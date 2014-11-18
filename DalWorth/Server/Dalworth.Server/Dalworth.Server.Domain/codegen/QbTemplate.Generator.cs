
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


      public partial class QbTemplate : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbTemplate ( " +
      
        " ListId, " +
      
        " TimeCreated, " +
      
        " TimeModified, " +
      
        " EditSequence, " +
      
        " Name, " +
      
        " IsActive " +
      
      ") Values (" +
      
        " ?ListId, " +
      
        " ?TimeCreated, " +
      
        " ?TimeModified, " +
      
        " ?EditSequence, " +
      
        " ?Name, " +
      
        " ?IsActive " +
      
      ")";

      public static void Insert(QbTemplate qbTemplate, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbTemplate.ListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbTemplate.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbTemplate.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbTemplate.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbTemplate.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbTemplate.IsActive);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbTemplate qbTemplate)
      {
        Insert(qbTemplate, null);
      }


      public static void Insert(List<QbTemplate>  qbTemplateList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbTemplate qbTemplate in  qbTemplateList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbTemplate.ListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbTemplate.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbTemplate.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbTemplate.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbTemplate.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbTemplate.IsActive);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ListId",qbTemplate.ListId);
      
        Database.UpdateParameter(dbCommand,"?TimeCreated",qbTemplate.TimeCreated);
      
        Database.UpdateParameter(dbCommand,"?TimeModified",qbTemplate.TimeModified);
      
        Database.UpdateParameter(dbCommand,"?EditSequence",qbTemplate.EditSequence);
      
        Database.UpdateParameter(dbCommand,"?Name",qbTemplate.Name);
      
        Database.UpdateParameter(dbCommand,"?IsActive",qbTemplate.IsActive);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbTemplate>  qbTemplateList)
      {
        Insert(qbTemplateList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbTemplate Set "
      
        + " TimeCreated = ?TimeCreated, "
      
        + " TimeModified = ?TimeModified, "
      
        + " EditSequence = ?EditSequence, "
      
        + " Name = ?Name, "
      
        + " IsActive = ?IsActive "
      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static void Update(QbTemplate qbTemplate, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbTemplate.ListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbTemplate.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbTemplate.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbTemplate.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbTemplate.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbTemplate.IsActive);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbTemplate qbTemplate)
      {
        Update(qbTemplate, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ListId, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence, "
      
        + " Name, "
      
        + " IsActive "
      

      + " From QbTemplate "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static QbTemplate FindByPrimaryKey(
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
      throw new DataNotFoundException("QbTemplate not found, search by primary key");

      }

      public static QbTemplate FindByPrimaryKey(
      String listId
      )
      {
      return FindByPrimaryKey(
      listId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbTemplate qbTemplate, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId",qbTemplate.ListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbTemplate qbTemplate)
      {
      return Exists(qbTemplate, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbTemplate limit 1";

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

      public static QbTemplate Load(IDataReader dataReader, int offset)
      {
      QbTemplate qbTemplate = new QbTemplate();

      qbTemplate.ListId = dataReader.GetString(0 + offset);
          qbTemplate.TimeCreated = dataReader.GetDateTime(1 + offset);
          qbTemplate.TimeModified = dataReader.GetDateTime(2 + offset);
          qbTemplate.EditSequence = dataReader.GetString(3 + offset);
          qbTemplate.Name = dataReader.GetString(4 + offset);
          qbTemplate.IsActive = dataReader.GetBoolean(5 + offset);
          

      return qbTemplate;
      }

      public static QbTemplate Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbTemplate "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;
      public static void Delete(QbTemplate qbTemplate, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ListId", qbTemplate.ListId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbTemplate qbTemplate)
      {
        Delete(qbTemplate, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbTemplate ";

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
      
        + " IsActive "
      

      + " From QbTemplate ";
      public static List<QbTemplate> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbTemplate> rv = new List<QbTemplate>();

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

      public static List<QbTemplate> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbTemplate> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbTemplate obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ListId == obj.ListId && TimeCreated == obj.TimeCreated && TimeModified == obj.TimeModified && EditSequence == obj.EditSequence && Name == obj.Name && IsActive == obj.IsActive;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbTemplate> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbTemplate));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbTemplate item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbTemplate>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbTemplate));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbTemplate> itemsList
      = new List<QbTemplate>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbTemplate)
      itemsList.Add(deserializedObject as QbTemplate);
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
      
      #endregion

      #region Constructors
      public QbTemplate(
      String 
          listId
      ) : this()
      {
      
        m_listId = listId;
      
      }

      


        public QbTemplate(
        String 
          listId,DateTime 
          timeCreated,DateTime 
          timeModified,String 
          editSequence,String 
          name,bool 
          isActive
        ) : this()
        {
        
          m_listId = listId;
        
          m_timeCreated = timeCreated;
        
          m_timeModified = timeModified;
        
          m_editSequence = editSequence;
        
          m_name = name;
        
          m_isActive = isActive;
        
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

    