
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


      public partial class MonitoringReadingType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into MonitoringReadingType ( " +
      
        " ID, " +
      
        " Type, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Type, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(MonitoringReadingType monitoringReadingType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", monitoringReadingType.ID);
      
        Database.PutParameter(dbCommand,"?Type", monitoringReadingType.Type);
      
        Database.PutParameter(dbCommand,"?Description", monitoringReadingType.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(MonitoringReadingType monitoringReadingType)
      {
        Insert(monitoringReadingType, null);
      }


      public static void Insert(List<MonitoringReadingType>  monitoringReadingTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(MonitoringReadingType monitoringReadingType in  monitoringReadingTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", monitoringReadingType.ID);
      
        Database.PutParameter(dbCommand,"?Type", monitoringReadingType.Type);
      
        Database.PutParameter(dbCommand,"?Description", monitoringReadingType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",monitoringReadingType.ID);
      
        Database.UpdateParameter(dbCommand,"?Type",monitoringReadingType.Type);
      
        Database.UpdateParameter(dbCommand,"?Description",monitoringReadingType.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<MonitoringReadingType>  monitoringReadingTypeList)
      {
        Insert(monitoringReadingTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update MonitoringReadingType Set "
      
        + " Type = ?Type, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(MonitoringReadingType monitoringReadingType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", monitoringReadingType.ID);
      
        Database.PutParameter(dbCommand,"?Type", monitoringReadingType.Type);
      
        Database.PutParameter(dbCommand,"?Description", monitoringReadingType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(MonitoringReadingType monitoringReadingType)
      {
        Update(monitoringReadingType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Type, "
      
        + " Description "
      

      + " From MonitoringReadingType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static MonitoringReadingType FindByPrimaryKey(
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
      throw new DataNotFoundException("MonitoringReadingType not found, search by primary key");

      }

      public static MonitoringReadingType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(MonitoringReadingType monitoringReadingType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",monitoringReadingType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(MonitoringReadingType monitoringReadingType)
      {
      return Exists(monitoringReadingType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from MonitoringReadingType limit 1";

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

      public static MonitoringReadingType Load(IDataReader dataReader, int offset)
      {
      MonitoringReadingType monitoringReadingType = new MonitoringReadingType();

      monitoringReadingType.ID = dataReader.GetInt32(0 + offset);
          monitoringReadingType.Type = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            monitoringReadingType.Description = dataReader.GetString(2 + offset);
          

      return monitoringReadingType;
      }

      public static MonitoringReadingType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From MonitoringReadingType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(MonitoringReadingType monitoringReadingType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", monitoringReadingType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(MonitoringReadingType monitoringReadingType)
      {
        Delete(monitoringReadingType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From MonitoringReadingType ";

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
      
        + " Type, "
      
        + " Description "
      

      + " From MonitoringReadingType ";
      public static List<MonitoringReadingType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<MonitoringReadingType> rv = new List<MonitoringReadingType>();

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

      public static List<MonitoringReadingType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<MonitoringReadingType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (MonitoringReadingType obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Type == obj.Type && Description == obj.Description;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<MonitoringReadingType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(MonitoringReadingType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(MonitoringReadingType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<MonitoringReadingType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(MonitoringReadingType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<MonitoringReadingType> itemsList
      = new List<MonitoringReadingType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is MonitoringReadingType)
      itemsList.Add(deserializedObject as MonitoringReadingType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_type;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public MonitoringReadingType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public MonitoringReadingType(
        int 
          iD,String 
          type,String 
          description
        ) : this()
        {
        
          m_iD = iD;
        
          m_type = type;
        
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
        public String Type
        {
        get { return m_type;}
        set { m_type = value; }
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

    