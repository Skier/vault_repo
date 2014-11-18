
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


      public partial class QbCustomerType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbCustomerType ( " +
      
        " ListId, " +
      
        " TimeCreated, " +
      
        " TimeModified, " +
      
        " EditSequence, " +
      
        " Name, " +
      
        " FullName, " +
      
        " IsActive, " +
      
        " ParentRefListId, " +
      
        " SubLevel, " +
      
        " RequiredQbSalesRepListId " +
      
      ") Values (" +
      
        " ?ListId, " +
      
        " ?TimeCreated, " +
      
        " ?TimeModified, " +
      
        " ?EditSequence, " +
      
        " ?Name, " +
      
        " ?FullName, " +
      
        " ?IsActive, " +
      
        " ?ParentRefListId, " +
      
        " ?SubLevel, " +
      
        " ?RequiredQbSalesRepListId " +
      
      ")";

      public static void Insert(QbCustomerType qbCustomerType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbCustomerType.ListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbCustomerType.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbCustomerType.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbCustomerType.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbCustomerType.Name);
      
        Database.PutParameter(dbCommand,"?FullName", qbCustomerType.FullName);
      
        Database.PutParameter(dbCommand,"?IsActive", qbCustomerType.IsActive);
      
        Database.PutParameter(dbCommand,"?ParentRefListId", qbCustomerType.ParentRefListId);
      
        Database.PutParameter(dbCommand,"?SubLevel", qbCustomerType.SubLevel);
      
        Database.PutParameter(dbCommand,"?RequiredQbSalesRepListId", qbCustomerType.RequiredQbSalesRepListId);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbCustomerType qbCustomerType)
      {
        Insert(qbCustomerType, null);
      }


      public static void Insert(List<QbCustomerType>  qbCustomerTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbCustomerType qbCustomerType in  qbCustomerTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbCustomerType.ListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbCustomerType.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbCustomerType.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbCustomerType.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbCustomerType.Name);
      
        Database.PutParameter(dbCommand,"?FullName", qbCustomerType.FullName);
      
        Database.PutParameter(dbCommand,"?IsActive", qbCustomerType.IsActive);
      
        Database.PutParameter(dbCommand,"?ParentRefListId", qbCustomerType.ParentRefListId);
      
        Database.PutParameter(dbCommand,"?SubLevel", qbCustomerType.SubLevel);
      
        Database.PutParameter(dbCommand,"?RequiredQbSalesRepListId", qbCustomerType.RequiredQbSalesRepListId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ListId",qbCustomerType.ListId);
      
        Database.UpdateParameter(dbCommand,"?TimeCreated",qbCustomerType.TimeCreated);
      
        Database.UpdateParameter(dbCommand,"?TimeModified",qbCustomerType.TimeModified);
      
        Database.UpdateParameter(dbCommand,"?EditSequence",qbCustomerType.EditSequence);
      
        Database.UpdateParameter(dbCommand,"?Name",qbCustomerType.Name);
      
        Database.UpdateParameter(dbCommand,"?FullName",qbCustomerType.FullName);
      
        Database.UpdateParameter(dbCommand,"?IsActive",qbCustomerType.IsActive);
      
        Database.UpdateParameter(dbCommand,"?ParentRefListId",qbCustomerType.ParentRefListId);
      
        Database.UpdateParameter(dbCommand,"?SubLevel",qbCustomerType.SubLevel);
      
        Database.UpdateParameter(dbCommand,"?RequiredQbSalesRepListId",qbCustomerType.RequiredQbSalesRepListId);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbCustomerType>  qbCustomerTypeList)
      {
        Insert(qbCustomerTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbCustomerType Set "
      
        + " TimeCreated = ?TimeCreated, "
      
        + " TimeModified = ?TimeModified, "
      
        + " EditSequence = ?EditSequence, "
      
        + " Name = ?Name, "
      
        + " FullName = ?FullName, "
      
        + " IsActive = ?IsActive, "
      
        + " ParentRefListId = ?ParentRefListId, "
      
        + " SubLevel = ?SubLevel, "
      
        + " RequiredQbSalesRepListId = ?RequiredQbSalesRepListId "
      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static void Update(QbCustomerType qbCustomerType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId", qbCustomerType.ListId);
      
        Database.PutParameter(dbCommand,"?TimeCreated", qbCustomerType.TimeCreated);
      
        Database.PutParameter(dbCommand,"?TimeModified", qbCustomerType.TimeModified);
      
        Database.PutParameter(dbCommand,"?EditSequence", qbCustomerType.EditSequence);
      
        Database.PutParameter(dbCommand,"?Name", qbCustomerType.Name);
      
        Database.PutParameter(dbCommand,"?FullName", qbCustomerType.FullName);
      
        Database.PutParameter(dbCommand,"?IsActive", qbCustomerType.IsActive);
      
        Database.PutParameter(dbCommand,"?ParentRefListId", qbCustomerType.ParentRefListId);
      
        Database.PutParameter(dbCommand,"?SubLevel", qbCustomerType.SubLevel);
      
        Database.PutParameter(dbCommand,"?RequiredQbSalesRepListId", qbCustomerType.RequiredQbSalesRepListId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbCustomerType qbCustomerType)
      {
        Update(qbCustomerType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ListId, "
      
        + " TimeCreated, "
      
        + " TimeModified, "
      
        + " EditSequence, "
      
        + " Name, "
      
        + " FullName, "
      
        + " IsActive, "
      
        + " ParentRefListId, "
      
        + " SubLevel, "
      
        + " RequiredQbSalesRepListId "
      

      + " From QbCustomerType "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;

      public static QbCustomerType FindByPrimaryKey(
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
      throw new DataNotFoundException("QbCustomerType not found, search by primary key");

      }

      public static QbCustomerType FindByPrimaryKey(
      String listId
      )
      {
      return FindByPrimaryKey(
      listId, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbCustomerType qbCustomerType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ListId",qbCustomerType.ListId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbCustomerType qbCustomerType)
      {
      return Exists(qbCustomerType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbCustomerType limit 1";

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

      public static QbCustomerType Load(IDataReader dataReader, int offset)
      {
      QbCustomerType qbCustomerType = new QbCustomerType();

      qbCustomerType.ListId = dataReader.GetString(0 + offset);
          qbCustomerType.TimeCreated = dataReader.GetDateTime(1 + offset);
          qbCustomerType.TimeModified = dataReader.GetDateTime(2 + offset);
          qbCustomerType.EditSequence = dataReader.GetString(3 + offset);
          qbCustomerType.Name = dataReader.GetString(4 + offset);
          qbCustomerType.FullName = dataReader.GetString(5 + offset);
          qbCustomerType.IsActive = dataReader.GetBoolean(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            qbCustomerType.ParentRefListId = dataReader.GetString(7 + offset);
          qbCustomerType.SubLevel = dataReader.GetInt32(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            qbCustomerType.RequiredQbSalesRepListId = dataReader.GetString(9 + offset);
          

      return qbCustomerType;
      }

      public static QbCustomerType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbCustomerType "

      
        + " Where "
        
          + " ListId = ?ListId "
        
      ;
      public static void Delete(QbCustomerType qbCustomerType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ListId", qbCustomerType.ListId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbCustomerType qbCustomerType)
      {
        Delete(qbCustomerType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbCustomerType ";

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
      
        + " FullName, "
      
        + " IsActive, "
      
        + " ParentRefListId, "
      
        + " SubLevel, "
      
        + " RequiredQbSalesRepListId "
      

      + " From QbCustomerType ";
      public static List<QbCustomerType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbCustomerType> rv = new List<QbCustomerType>();

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

      public static List<QbCustomerType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbCustomerType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbCustomerType obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ListId == obj.ListId && TimeCreated == obj.TimeCreated && TimeModified == obj.TimeModified && EditSequence == obj.EditSequence && Name == obj.Name && FullName == obj.FullName && IsActive == obj.IsActive && ParentRefListId == obj.ParentRefListId && SubLevel == obj.SubLevel && RequiredQbSalesRepListId == obj.RequiredQbSalesRepListId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<QbCustomerType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbCustomerType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbCustomerType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbCustomerType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbCustomerType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbCustomerType> itemsList
      = new List<QbCustomerType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbCustomerType)
      itemsList.Add(deserializedObject as QbCustomerType);
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
      
        protected String m_fullName;
      
        protected bool m_isActive;
      
        protected String m_parentRefListId;
      
        protected int m_subLevel;
      
        protected String m_requiredQbSalesRepListId;
      
      #endregion

      #region Constructors
      public QbCustomerType(
      String 
          listId
      ) : this()
      {
      
        m_listId = listId;
      
      }

      


        public QbCustomerType(
        String 
          listId,DateTime 
          timeCreated,DateTime 
          timeModified,String 
          editSequence,String 
          name,String 
          fullName,bool 
          isActive,String 
          parentRefListId,int 
          subLevel,String 
          requiredQbSalesRepListId
        ) : this()
        {
        
          m_listId = listId;
        
          m_timeCreated = timeCreated;
        
          m_timeModified = timeModified;
        
          m_editSequence = editSequence;
        
          m_name = name;
        
          m_fullName = fullName;
        
          m_isActive = isActive;
        
          m_parentRefListId = parentRefListId;
        
          m_subLevel = subLevel;
        
          m_requiredQbSalesRepListId = requiredQbSalesRepListId;
        
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
        public String FullName
        {
        get { return m_fullName;}
        set { m_fullName = value; }
        }
      
        [XmlElement]
        public bool IsActive
        {
        get { return m_isActive;}
        set { m_isActive = value; }
        }
      
        [XmlElement]
        public String ParentRefListId
        {
        get { return m_parentRefListId;}
        set { m_parentRefListId = value; }
        }
      
        [XmlElement]
        public int SubLevel
        {
        get { return m_subLevel;}
        set { m_subLevel = value; }
        }
      
        [XmlElement]
        public String RequiredQbSalesRepListId
        {
        get { return m_requiredQbSalesRepListId;}
        set { m_requiredQbSalesRepListId = value; }
        }
      

      public static int FieldsCount
      {
      get { return 10; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    