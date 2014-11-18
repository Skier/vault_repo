
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


      public partial class BackgroundJobType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into BackgroundJobType ( " +
      
        " ID, " +
      
        " Type, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Type, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(BackgroundJobType backgroundJobType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", backgroundJobType.ID);
      
        Database.PutParameter(dbCommand,"?Type", backgroundJobType.Type);
      
        Database.PutParameter(dbCommand,"?Description", backgroundJobType.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(BackgroundJobType backgroundJobType)
      {
        Insert(backgroundJobType, null);
      }


      public static void Insert(List<BackgroundJobType>  backgroundJobTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(BackgroundJobType backgroundJobType in  backgroundJobTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", backgroundJobType.ID);
      
        Database.PutParameter(dbCommand,"?Type", backgroundJobType.Type);
      
        Database.PutParameter(dbCommand,"?Description", backgroundJobType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",backgroundJobType.ID);
      
        Database.UpdateParameter(dbCommand,"?Type",backgroundJobType.Type);
      
        Database.UpdateParameter(dbCommand,"?Description",backgroundJobType.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<BackgroundJobType>  backgroundJobTypeList)
      {
        Insert(backgroundJobTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update BackgroundJobType Set "
      
        + " Type = ?Type, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(BackgroundJobType backgroundJobType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", backgroundJobType.ID);
      
        Database.PutParameter(dbCommand,"?Type", backgroundJobType.Type);
      
        Database.PutParameter(dbCommand,"?Description", backgroundJobType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(BackgroundJobType backgroundJobType)
      {
        Update(backgroundJobType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Type, "
      
        + " Description "
      

      + " From BackgroundJobType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static BackgroundJobType FindByPrimaryKey(
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
      throw new DataNotFoundException("BackgroundJobType not found, search by primary key");

      }

      public static BackgroundJobType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(BackgroundJobType backgroundJobType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",backgroundJobType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(BackgroundJobType backgroundJobType)
      {
      return Exists(backgroundJobType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from BackgroundJobType limit 1";

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

      public static BackgroundJobType Load(IDataReader dataReader, int offset)
      {
      BackgroundJobType backgroundJobType = new BackgroundJobType();

      backgroundJobType.ID = dataReader.GetInt32(0 + offset);
          backgroundJobType.Type = dataReader.GetString(1 + offset);
          backgroundJobType.Description = dataReader.GetString(2 + offset);
          

      return backgroundJobType;
      }

      public static BackgroundJobType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From BackgroundJobType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(BackgroundJobType backgroundJobType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", backgroundJobType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(BackgroundJobType backgroundJobType)
      {
        Delete(backgroundJobType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From BackgroundJobType ";

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
      

      + " From BackgroundJobType ";
      public static List<BackgroundJobType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<BackgroundJobType> rv = new List<BackgroundJobType>();

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

      public static List<BackgroundJobType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<BackgroundJobType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (BackgroundJobType obj)
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

      List<BackgroundJobType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(BackgroundJobType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(BackgroundJobType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<BackgroundJobType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(BackgroundJobType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<BackgroundJobType> itemsList
      = new List<BackgroundJobType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is BackgroundJobType)
      itemsList.Add(deserializedObject as BackgroundJobType);
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
      public BackgroundJobType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public BackgroundJobType(
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

    