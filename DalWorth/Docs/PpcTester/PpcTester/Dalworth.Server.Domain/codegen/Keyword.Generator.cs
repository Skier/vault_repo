
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


      public partial class Keyword : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Keyword ( " +
      
        " KeywordString " +
      
      ") Values (" +
      
        " ?KeywordString " +
      
      ")";

      public static void Insert(Keyword keyword, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?KeywordString", keyword.KeywordString);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        keyword.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Keyword keyword)
      {
        Insert(keyword, null);
      }


      public static void Insert(List<Keyword>  keywordList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Keyword keyword in  keywordList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?KeywordString", keyword.KeywordString);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?KeywordString",keyword.KeywordString);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        keyword.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Keyword>  keywordList)
      {
        Insert(keywordList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Keyword Set "
      
        + " KeywordString = ?KeywordString "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(Keyword keyword, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", keyword.Id);
      
        Database.PutParameter(dbCommand,"?KeywordString", keyword.KeywordString);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Keyword keyword)
      {
        Update(keyword, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " KeywordString "
      

      + " From Keyword "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static Keyword FindByPrimaryKey(
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
      throw new DataNotFoundException("Keyword not found, search by primary key");

      }

      public static Keyword FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Keyword keyword, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",keyword.Id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Keyword keyword)
      {
      return Exists(keyword, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Keyword limit 1";

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

      public static Keyword Load(IDataReader dataReader, int offset)
      {
      Keyword keyword = new Keyword();

      keyword.Id = dataReader.GetInt32(0 + offset);
          keyword.KeywordString = dataReader.GetString(1 + offset);
          

      return keyword;
      }

      public static Keyword Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Keyword "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(Keyword keyword, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", keyword.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Keyword keyword)
      {
        Delete(keyword, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Keyword ";

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
      
        + " KeywordString "
      

      + " From Keyword ";
      public static List<Keyword> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Keyword> rv = new List<Keyword>();

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

      public static List<Keyword> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Keyword> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Keyword obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return Id == obj.Id && KeywordString == obj.KeywordString;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Keyword> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Keyword));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Keyword item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Keyword>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Keyword));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Keyword> itemsList
      = new List<Keyword>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Keyword)
      itemsList.Add(deserializedObject as Keyword);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_id;
      
        protected String m_keywordString;
      
      #endregion

      #region Constructors
      public Keyword(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public Keyword(
        int 
          id,String 
          keywordString
        ) : this()
        {
        
          m_id = id;
        
          m_keywordString = keywordString;
        
        }


      
      #endregion

      
        [XmlElement]
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        [XmlElement]
        public String KeywordString
        {
        get { return m_keywordString;}
        set { m_keywordString = value; }
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

    