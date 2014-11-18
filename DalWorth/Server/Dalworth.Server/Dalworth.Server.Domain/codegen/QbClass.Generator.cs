
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


      public partial class QbClass : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbClass ( " +
      
        " ListId, " +
      
        " FullName, " +
      
        " TimeCreated, " +
      
        " TimeModified, " +
      
        " EditSequence, " +
      
        " Name, " +
      
        " IsActive, " +
      
        " ParentClassListID, " +
      
        " SubLevel " +
      
      ") Values (" +
      
        " ?ListId, " +
      
        " ?FullName, " +
      
        " ?TimeCreated, " +
      
        " ?TimeModified, " +
      
        " ?EditSequence, " +
      
        " ?Name, " +
      
        " ?IsActive, " +
      
        " ?ParentClassListID, " +
      
        " ?SubLevel " +
      
      ")";

      public static void Insert(QbClass qbClass, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbClass.ListId);
      
        Database.PutParameter(dbCommand,"?FullName", qbClass.FullName);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbClass.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbClass.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbClass.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbClass.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbClass.IsActive);
      
        Database.PutParameter(dbCommand,"?ParentClassListID", qbClass.ParentClassListID);
      
        Database.PutParameter(dbCommand,"?SubLevel", qbClass.SubLevel);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbClass qbClass)
      {
        Insert(qbClass, null);
      }


      public static void Insert(List<QbClass>  qbClassList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbClass qbClass in  qbClassList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbClass.ListId);
      
        Database.PutParameter(dbCommand,"?FullName", qbClass.FullName);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbClass.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbClass.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbClass.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbClass.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbClass.IsActive);
      
        Database.PutParameter(dbCommand,"?ParentClassListID", qbClass.ParentClassListID);
      
        Database.PutParameter(dbCommand,"?SubLevel", qbClass.SubLevel);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ListId",qbClass.ListId);
      
        Database.UpdateParameter(dbCommand,"?FullName",qbClass.FullName);
      
        Database.UpdateParameter(dbCommand,"?TimeCreated",qbClass.TimeCreated);
      
        Database.UpdateParameter(dbCommand,"?TimeModified",qbClass.TimeModified);
      
        Database.UpdateParameter(dbCommand,"?EditSequence",qbClass.EditSequence);
      
        Database.UpdateParameter(dbCommand,"?Name",qbClass.Name);
      
        Database.UpdateParameter(dbCommand,"?IsActive",qbClass.IsActive);
      
        Database.UpdateParameter(dbCommand,"?ParentClassListID",qbClass.ParentClassListID);
      
        Database.UpdateParameter(dbCommand,"?SubLevel",qbClass.SubLevel);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbClass>  qbClassList)
      {
        Insert(qbClassList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbClass Set "
      
        + " FullName = ?FullName, "
      
        + " TimeCreated = ?TimeCreated, "
      
        + " TimeModified = ?TimeModified, "
      
        + " EditSequence = ?EditSequence, "
      
        + " Name = ?Name, "
      
        + " IsActive = ?IsActive, "
      
        + " ParentClassListID = ?ParentClassListID, "
      
        + " SubLevel = ?SubLevel "
      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static void Update(QbClass qbClass, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbClass.ListId);
      
        Database.PutParameter(dbCommand,"?FullName", qbClass.FullName);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbClass.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbClass.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbClass.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbClass.Name);
      
        Database.PutParameter(dbCommand,"?IsActive", qbClass.IsActive);
      
        Database.PutParameter(dbCommand,"?ParentClassListID", qbClass.ParentClassListID);
      
        Database.PutParameter(dbCommand,"?SubLevel", qbClass.SubLevel);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbClass qbClass)
      {
        Update(qbClass, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ListId, "
      
        + " FullName, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence, "
      
        + " Name, "
      
        + " IsActive, "
      
        + " ParentClassListID, "
      
        + " SubLevel "
      

      + " From QbClass "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static QbClass FindByPrimaryKey(
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
      throw new DataNotFoundException("QbClass not found, search by primary key");

      }

      public static QbClass FindByPrimaryKey(
      String listId
      )
      {
      return FindByPrimaryKey(
      listId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbClass qbClass, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId",qbClass.ListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbClass qbClass)
      {
      return Exists(qbClass, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbClass limit 1";

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

      public static QbClass Load(IDataReader dataReader, int offset)
      {
      QbClass qbClass = new QbClass();

      qbClass.ListId = dataReader.GetString(0 + offset);
          qbClass.FullName = dataReader.GetString(1 + offset);
          qbClass.TimeCreated = dataReader.GetDateTime(2 + offset);
          qbClass.TimeModified = dataReader.GetDateTime(3 + offset);
          qbClass.EditSequence = dataReader.GetString(4 + offset);
          qbClass.Name = dataReader.GetString(5 + offset);
          qbClass.IsActive = dataReader.GetBoolean(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            qbClass.ParentClassListID = dataReader.GetString(7 + offset);
          qbClass.SubLevel = dataReader.GetInt32(8 + offset);
          

      return qbClass;
      }

      public static QbClass Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbClass "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;
      public static void Delete(QbClass qbClass, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ListId", qbClass.ListId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbClass qbClass)
      {
        Delete(qbClass, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbClass ";

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
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence, "
      
        + " Name, "
      
        + " IsActive, "
      
        + " ParentClassListID, "
      
        + " SubLevel "
      

      + " From QbClass ";
      public static List<QbClass> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbClass> rv = new List<QbClass>();

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

      public static List<QbClass> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbClass> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbClass obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ListId == obj.ListId && FullName == obj.FullName && TimeCreated == obj.TimeCreated && TimeModified == obj.TimeModified && EditSequence == obj.EditSequence && Name == obj.Name && IsActive == obj.IsActive && ParentClassListID == obj.ParentClassListID && SubLevel == obj.SubLevel;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbClass> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbClass));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbClass item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbClass>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbClass));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbClass> itemsList
      = new List<QbClass>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbClass)
      itemsList.Add(deserializedObject as QbClass);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_listId;
      
        protected String m_fullName;
      
        protected DateTime m_timeCreated;
      
        protected DateTime m_timeModified;
      
        protected String m_editSequence;
      
        protected String m_name;
      
        protected bool m_isActive;
      
        protected String m_parentClassListID;
      
        protected int m_subLevel;
      
      #endregion

      #region Constructors
      public QbClass(
      String 
          listId
      ) : this()
      {
      
        m_listId = listId;
      
      }

      


        public QbClass(
        String 
          listId,String 
          fullName,DateTime 
          timeCreated,DateTime 
          timeModified,String 
          editSequence,String 
          name,bool 
          isActive,String 
          parentClassListID,int 
          subLevel
        ) : this()
        {
        
          m_listId = listId;
        
          m_fullName = fullName;
        
          m_timeCreated = timeCreated;
        
          m_timeModified = timeModified;
        
          m_editSequence = editSequence;
        
          m_name = name;
        
          m_isActive = isActive;
        
          m_parentClassListID = parentClassListID;
        
          m_subLevel = subLevel;
        
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
        public String ParentClassListID
        {
        get { return m_parentClassListID;}
        set { m_parentClassListID = value; }
        }
      
        [XmlElement]
        public int SubLevel
        {
        get { return m_subLevel;}
        set { m_subLevel = value; }
        }
      

      public static int FieldsCount
      {
      get { return 9; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    