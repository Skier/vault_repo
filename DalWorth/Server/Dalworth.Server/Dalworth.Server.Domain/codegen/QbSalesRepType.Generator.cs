
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


      public partial class QbSalesRepType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into QbSalesRepType ( " +
      
        " ID, " +
      
        " Name " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Name " +
      
      ")";

      public static void Insert(QbSalesRepType qbSalesRepType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qbSalesRepType.ID);
      
        Database.PutParameter(dbCommand,"?Name", qbSalesRepType.Name);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(QbSalesRepType qbSalesRepType)
      {
        Insert(qbSalesRepType, null);
      }


      public static void Insert(List<QbSalesRepType>  qbSalesRepTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(QbSalesRepType qbSalesRepType in  qbSalesRepTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", qbSalesRepType.ID);
      
        Database.PutParameter(dbCommand,"?Name", qbSalesRepType.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",qbSalesRepType.ID);
      
        Database.UpdateParameter(dbCommand,"?Name",qbSalesRepType.Name);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<QbSalesRepType>  qbSalesRepTypeList)
      {
        Insert(qbSalesRepTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update QbSalesRepType Set "
      
        + " Name = ?Name "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(QbSalesRepType qbSalesRepType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", qbSalesRepType.ID);
      
        Database.PutParameter(dbCommand,"?Name", qbSalesRepType.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(QbSalesRepType qbSalesRepType)
      {
        Update(qbSalesRepType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Name "
      

      + " From QbSalesRepType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static QbSalesRepType FindByPrimaryKey(
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
      throw new DataNotFoundException("QbSalesRepType not found, search by primary key");

      }

      public static QbSalesRepType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(QbSalesRepType qbSalesRepType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",qbSalesRepType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(QbSalesRepType qbSalesRepType)
      {
      return Exists(qbSalesRepType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from QbSalesRepType limit 1";

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

      public static QbSalesRepType Load(IDataReader dataReader, int offset)
      {
      QbSalesRepType qbSalesRepType = new QbSalesRepType();

      qbSalesRepType.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            qbSalesRepType.Name = dataReader.GetString(1 + offset);
          

      return qbSalesRepType;
      }

      public static QbSalesRepType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From QbSalesRepType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(QbSalesRepType qbSalesRepType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", qbSalesRepType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(QbSalesRepType qbSalesRepType)
      {
        Delete(qbSalesRepType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From QbSalesRepType ";

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
      

      + " From QbSalesRepType ";
      public static List<QbSalesRepType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<QbSalesRepType> rv = new List<QbSalesRepType>();

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

      public static List<QbSalesRepType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<QbSalesRepType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (QbSalesRepType obj)
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

      List<QbSalesRepType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbSalesRepType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(QbSalesRepType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<QbSalesRepType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(QbSalesRepType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<QbSalesRepType> itemsList
      = new List<QbSalesRepType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is QbSalesRepType)
      itemsList.Add(deserializedObject as QbSalesRepType);
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
      public QbSalesRepType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public QbSalesRepType(
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

    