
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


      public partial class Compaign : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Compaign ( " +
      
        " SearchEngineId, " +
      
        " CompanyId, " +
      
        " Name " +
      
      ") Values (" +
      
        " ?SearchEngineId, " +
      
        " ?CompanyId, " +
      
        " ?Name " +
      
      ")";

      public static void Insert(Compaign compaign, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?SearchEngineId", compaign.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?CompanyId", compaign.CompanyId);
      
        Database.PutParameter(dbCommand,"?Name", compaign.Name);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        compaign.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Compaign compaign)
      {
        Insert(compaign, null);
      }


      public static void Insert(List<Compaign>  compaignList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Compaign compaign in  compaignList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?SearchEngineId", compaign.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?CompanyId", compaign.CompanyId);
      
        Database.PutParameter(dbCommand,"?Name", compaign.Name);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?SearchEngineId",compaign.SearchEngineId);
      
        Database.UpdateParameter(dbCommand,"?CompanyId",compaign.CompanyId);
      
        Database.UpdateParameter(dbCommand,"?Name",compaign.Name);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        compaign.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Compaign>  compaignList)
      {
        Insert(compaignList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Compaign Set "
      
        + " SearchEngineId = ?SearchEngineId, "
      
        + " CompanyId = ?CompanyId, "
      
        + " Name = ?Name "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(Compaign compaign, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", compaign.Id);
      
        Database.PutParameter(dbCommand,"?SearchEngineId", compaign.SearchEngineId);
      
        Database.PutParameter(dbCommand,"?CompanyId", compaign.CompanyId);
      
        Database.PutParameter(dbCommand,"?Name", compaign.Name);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Compaign compaign)
      {
        Update(compaign, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " SearchEngineId, "
      
        + " CompanyId, "
      
        + " Name "
      

      + " From Compaign "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static Compaign FindByPrimaryKey(
      int id, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Compaign not found, search by primary key");

      }

      public static Compaign FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Compaign compaign, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",compaign.Id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Compaign compaign)
      {
      return Exists(compaign, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Compaign limit 1";

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

      public static Compaign Load(IDataReader dataReader, int offset)
      {
      Compaign compaign = new Compaign();

      compaign.Id = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            compaign.SearchEngineId = dataReader.GetInt32(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            compaign.CompanyId = dataReader.GetInt32(2 + offset);
          compaign.Name = dataReader.GetString(3 + offset);
          

      return compaign;
      }

      public static Compaign Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Compaign "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(Compaign compaign, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", compaign.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Compaign compaign)
      {
        Delete(compaign, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Compaign ";

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

      
        + " Id, "
      
        + " SearchEngineId, "
      
        + " CompanyId, "
      
        + " Name "
      

      + " From Compaign ";
      public static List<Compaign> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Compaign> rv = new List<Compaign>();

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

      public static List<Compaign> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Compaign> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Compaign obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return Id == obj.Id && SearchEngineId == obj.SearchEngineId && CompanyId == obj.CompanyId && Name == obj.Name;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Compaign> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Compaign));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Compaign item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Compaign>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Compaign));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Compaign> itemsList
      = new List<Compaign>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Compaign)
      itemsList.Add(deserializedObject as Compaign);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_id;
      
        protected int? m_searchEngineId;
      
        protected int? m_companyId;
      
        protected String m_name;
      
      #endregion

      #region Constructors
      public Compaign(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public Compaign(
        int 
          id,int? 
          searchEngineId,int? 
          companyId,String 
          name
        ) : this()
        {
        
          m_id = id;
        
          m_searchEngineId = searchEngineId;
        
          m_companyId = companyId;
        
          m_name = name;
        
        }


      
      #endregion

      
        [XmlElement]
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        [XmlElement]
        public int? SearchEngineId
        {
        get { return m_searchEngineId;}
        set { m_searchEngineId = value; }
        }
      
        [XmlElement]
        public int? CompanyId
        {
        get { return m_companyId;}
        set { m_companyId = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      

      public static int FieldsCount
      {
      get { return 4; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    