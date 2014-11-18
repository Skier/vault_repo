
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


      public partial class ConstructionDamageType : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into ConstructionDamageType ( " +
      
        " ID, " +
      
        " DamageType, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?DamageType, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(ConstructionDamageType constructionDamageType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", constructionDamageType.ID);
      
        Database.PutParameter(dbCommand,"?DamageType", constructionDamageType.DamageType);
      
        Database.PutParameter(dbCommand,"?Description", constructionDamageType.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(ConstructionDamageType constructionDamageType)
      {
        Insert(constructionDamageType, null);
      }


      public static void Insert(List<ConstructionDamageType>  constructionDamageTypeList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(ConstructionDamageType constructionDamageType in  constructionDamageTypeList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", constructionDamageType.ID);
      
        Database.PutParameter(dbCommand,"?DamageType", constructionDamageType.DamageType);
      
        Database.PutParameter(dbCommand,"?Description", constructionDamageType.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",constructionDamageType.ID);
      
        Database.UpdateParameter(dbCommand,"?DamageType",constructionDamageType.DamageType);
      
        Database.UpdateParameter(dbCommand,"?Description",constructionDamageType.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<ConstructionDamageType>  constructionDamageTypeList)
      {
        Insert(constructionDamageTypeList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update ConstructionDamageType Set "
      
        + " DamageType = ?DamageType, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(ConstructionDamageType constructionDamageType, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", constructionDamageType.ID);
      
        Database.PutParameter(dbCommand,"?DamageType", constructionDamageType.DamageType);
      
        Database.PutParameter(dbCommand,"?Description", constructionDamageType.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(ConstructionDamageType constructionDamageType)
      {
        Update(constructionDamageType, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " DamageType, "
      
        + " Description "
      

      + " From ConstructionDamageType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static ConstructionDamageType FindByPrimaryKey(
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
      throw new DataNotFoundException("ConstructionDamageType not found, search by primary key");

      }

      public static ConstructionDamageType FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(ConstructionDamageType constructionDamageType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",constructionDamageType.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(ConstructionDamageType constructionDamageType)
      {
      return Exists(constructionDamageType, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from ConstructionDamageType limit 1";

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

      public static ConstructionDamageType Load(IDataReader dataReader, int offset)
      {
      ConstructionDamageType constructionDamageType = new ConstructionDamageType();

      constructionDamageType.ID = dataReader.GetInt32(0 + offset);
          constructionDamageType.DamageType = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            constructionDamageType.Description = dataReader.GetString(2 + offset);
          

      return constructionDamageType;
      }

      public static ConstructionDamageType Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From ConstructionDamageType "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(ConstructionDamageType constructionDamageType, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", constructionDamageType.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(ConstructionDamageType constructionDamageType)
      {
        Delete(constructionDamageType, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From ConstructionDamageType ";

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
      
        + " DamageType, "
      
        + " Description "
      

      + " From ConstructionDamageType ";
      public static List<ConstructionDamageType> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<ConstructionDamageType> rv = new List<ConstructionDamageType>();

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

      public static List<ConstructionDamageType> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<ConstructionDamageType> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (ConstructionDamageType obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && DamageType == obj.DamageType && Description == obj.Description;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<ConstructionDamageType> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ConstructionDamageType));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(ConstructionDamageType item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<ConstructionDamageType>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(ConstructionDamageType));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<ConstructionDamageType> itemsList
      = new List<ConstructionDamageType>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is ConstructionDamageType)
      itemsList.Add(deserializedObject as ConstructionDamageType);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_damageType;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public ConstructionDamageType(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public ConstructionDamageType(
        int 
          iD,String 
          damageType,String 
          description
        ) : this()
        {
        
          m_iD = iD;
        
          m_damageType = damageType;
        
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
        public String DamageType
        {
        get { return m_damageType;}
        set { m_damageType = value; }
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

    