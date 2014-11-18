
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


      public partial class Advertisement : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Advertisement ( " +
      
        " AdgroupId, " +
      
        " Headline, " +
      
        " DescriptionLine1, " +
      
        " DescriptionLine2, " +
      
        " DisplayURL " +
      
      ") Values (" +
      
        " ?AdgroupId, " +
      
        " ?Headline, " +
      
        " ?DescriptionLine1, " +
      
        " ?DescriptionLine2, " +
      
        " ?DisplayURL " +
      
      ")";

      public static void Insert(Advertisement advertisement, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?AdgroupId", advertisement.AdgroupId);
      
        Database.PutParameter(dbCommand,"?Headline", advertisement.Headline);
      
        Database.PutParameter(dbCommand,"?DescriptionLine1", advertisement.DescriptionLine1);
      
        Database.PutParameter(dbCommand,"?DescriptionLine2", advertisement.DescriptionLine2);
      
        Database.PutParameter(dbCommand,"?DisplayURL", advertisement.DisplayURL);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        advertisement.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Advertisement advertisement)
      {
        Insert(advertisement, null);
      }


      public static void Insert(List<Advertisement>  advertisementList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Advertisement advertisement in  advertisementList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?AdgroupId", advertisement.AdgroupId);
      
        Database.PutParameter(dbCommand,"?Headline", advertisement.Headline);
      
        Database.PutParameter(dbCommand,"?DescriptionLine1", advertisement.DescriptionLine1);
      
        Database.PutParameter(dbCommand,"?DescriptionLine2", advertisement.DescriptionLine2);
      
        Database.PutParameter(dbCommand,"?DisplayURL", advertisement.DisplayURL);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?AdgroupId",advertisement.AdgroupId);
      
        Database.UpdateParameter(dbCommand,"?Headline",advertisement.Headline);
      
        Database.UpdateParameter(dbCommand,"?DescriptionLine1",advertisement.DescriptionLine1);
      
        Database.UpdateParameter(dbCommand,"?DescriptionLine2",advertisement.DescriptionLine2);
      
        Database.UpdateParameter(dbCommand,"?DisplayURL",advertisement.DisplayURL);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        advertisement.Id = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Advertisement>  advertisementList)
      {
        Insert(advertisementList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Advertisement Set "
      
        + " AdgroupId = ?AdgroupId, "
      
        + " Headline = ?Headline, "
      
        + " DescriptionLine1 = ?DescriptionLine1, "
      
        + " DescriptionLine2 = ?DescriptionLine2, "
      
        + " DisplayURL = ?DisplayURL "
      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static void Update(Advertisement advertisement, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id", advertisement.Id);
      
        Database.PutParameter(dbCommand,"?AdgroupId", advertisement.AdgroupId);
      
        Database.PutParameter(dbCommand,"?Headline", advertisement.Headline);
      
        Database.PutParameter(dbCommand,"?DescriptionLine1", advertisement.DescriptionLine1);
      
        Database.PutParameter(dbCommand,"?DescriptionLine2", advertisement.DescriptionLine2);
      
        Database.PutParameter(dbCommand,"?DisplayURL", advertisement.DisplayURL);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Advertisement advertisement)
      {
        Update(advertisement, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " Id, "
      
        + " AdgroupId, "
      
        + " Headline, "
      
        + " DescriptionLine1, "
      
        + " DescriptionLine2, "
      
        + " DisplayURL "
      

      + " From Advertisement "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;

      public static Advertisement FindByPrimaryKey(
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
      throw new DataNotFoundException("Advertisement not found, search by primary key");

      }

      public static Advertisement FindByPrimaryKey(
      int id
      )
      {
      return FindByPrimaryKey(
      id, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Advertisement advertisement, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?Id",advertisement.Id);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Advertisement advertisement)
      {
      return Exists(advertisement, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Advertisement limit 1";

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

      public static Advertisement Load(IDataReader dataReader, int offset)
      {
      Advertisement advertisement = new Advertisement();

      advertisement.Id = dataReader.GetInt32(0 + offset);
          advertisement.AdgroupId = dataReader.GetInt32(1 + offset);
          advertisement.Headline = dataReader.GetString(2 + offset);
          advertisement.DescriptionLine1 = dataReader.GetString(3 + offset);
          advertisement.DescriptionLine2 = dataReader.GetString(4 + offset);
          advertisement.DisplayURL = dataReader.GetString(5 + offset);
          

      return advertisement;
      }

      public static Advertisement Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Advertisement "

      
        + " Where "
        
          + " Id = ?Id "
        
      ;
      public static void Delete(Advertisement advertisement, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?Id", advertisement.Id);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Advertisement advertisement)
      {
        Delete(advertisement, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Advertisement ";

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
      
        + " AdgroupId, "
      
        + " Headline, "
      
        + " DescriptionLine1, "
      
        + " DescriptionLine2, "
      
        + " DisplayURL "
      

      + " From Advertisement ";
      public static List<Advertisement> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Advertisement> rv = new List<Advertisement>();

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

      public static List<Advertisement> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Advertisement> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Advertisement obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return Id == obj.Id && AdgroupId == obj.AdgroupId && Headline == obj.Headline && DescriptionLine1 == obj.DescriptionLine1 && DescriptionLine2 == obj.DescriptionLine2 && DisplayURL == obj.DisplayURL;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Advertisement> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Advertisement));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Advertisement item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Advertisement>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Advertisement));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Advertisement> itemsList
      = new List<Advertisement>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Advertisement)
      itemsList.Add(deserializedObject as Advertisement);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_id;
      
        protected int m_adgroupId;
      
        protected String m_headline;
      
        protected String m_descriptionLine1;
      
        protected String m_descriptionLine2;
      
        protected String m_displayURL;
      
      #endregion

      #region Constructors
      public Advertisement(
      int 
          id
      ) : this()
      {
      
        m_id = id;
      
      }

      


        public Advertisement(
        int 
          id,int 
          adgroupId,String 
          headline,String 
          descriptionLine1,String 
          descriptionLine2,String 
          displayURL
        ) : this()
        {
        
          m_id = id;
        
          m_adgroupId = adgroupId;
        
          m_headline = headline;
        
          m_descriptionLine1 = descriptionLine1;
        
          m_descriptionLine2 = descriptionLine2;
        
          m_displayURL = displayURL;
        
        }


      
      #endregion

      
        [XmlElement]
        public int Id
        {
        get { return m_id;}
        set { m_id = value; }
        }
      
        [XmlElement]
        public int AdgroupId
        {
        get { return m_adgroupId;}
        set { m_adgroupId = value; }
        }
      
        [XmlElement]
        public String Headline
        {
        get { return m_headline;}
        set { m_headline = value; }
        }
      
        [XmlElement]
        public String DescriptionLine1
        {
        get { return m_descriptionLine1;}
        set { m_descriptionLine1 = value; }
        }
      
        [XmlElement]
        public String DescriptionLine2
        {
        get { return m_descriptionLine2;}
        set { m_descriptionLine2 = value; }
        }
      
        [XmlElement]
        public String DisplayURL
        {
        get { return m_displayURL;}
        set { m_displayURL = value; }
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

    