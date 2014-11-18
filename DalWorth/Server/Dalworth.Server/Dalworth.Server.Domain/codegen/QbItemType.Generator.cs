
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


      public partial class QbItemType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbItemType ( " +
      
        " ID, " +
      
        " Name " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Name " +
      
      ")";

      public static void Insert(QbItemType qbItemType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qbItemType.ID);
      
        Database.PutParameter(dbCommand,"?Name", qbItemType.Name);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbItemType qbItemType)
      {
        Insert(qbItemType, null);
      }


      public static void Insert(List<QbItemType>  qbItemTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbItemType qbItemType in  qbItemTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", qbItemType.ID);
      
        Database.PutParameter(dbCommand,"?Name", qbItemType.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",qbItemType.ID);
      
        Database.UpdateParameter(dbCommand,"?Name",qbItemType.Name);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbItemType>  qbItemTypeList)
      {
        Insert(qbItemTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbItemType Set "
      
        + " Name = ?Name "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(QbItemType qbItemType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qbItemType.ID);
      
        Database.PutParameter(dbCommand,"?Name", qbItemType.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbItemType qbItemType)
      {
        Update(qbItemType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Name "
      

      + " From QbItemType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static QbItemType FindByPrimaryKey(
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
      throw new DataNotFoundException("QbItemType not found, search by primary key");

      }

      public static QbItemType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbItemType qbItemType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",qbItemType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbItemType qbItemType)
      {
      return Exists(qbItemType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbItemType limit 1";

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

      public static QbItemType Load(IDataReader dataReader, int offset)
      {
      QbItemType qbItemType = new QbItemType();

      qbItemType.ID = dataReader.GetInt32(0 + offset);
          qbItemType.Name = dataReader.GetString(1 + offset);
          

      return qbItemType;
      }

      public static QbItemType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbItemType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(QbItemType qbItemType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", qbItemType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbItemType qbItemType)
      {
        Delete(qbItemType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbItemType ";

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
      

      + " From QbItemType ";
      public static List<QbItemType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbItemType> rv = new List<QbItemType>();

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

      public static List<QbItemType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbItemType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbItemType obj)
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

      List<QbItemType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbItemType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbItemType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbItemType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbItemType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbItemType> itemsList
      = new List<QbItemType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbItemType)
      itemsList.Add(deserializedObject as QbItemType);
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
      public QbItemType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public QbItemType(
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

    