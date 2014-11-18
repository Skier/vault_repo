
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


      public partial class Zip : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Zip ( " +
      
        " ZipCode, " +
      
        " AreaId, " +
      
        " City, " +
      
        " State " +
      
      ") Values (" +
      
        " ?ZipCode, " +
      
        " ?AreaId, " +
      
        " ?City, " +
      
        " ?State " +
      
      ")";

      public static void Insert(Zip zip, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ZipCode", zip.ZipCode);
      
        Database.PutParameter(dbCommand,"?AreaId", zip.AreaId);
      
        Database.PutParameter(dbCommand,"?City", zip.City);
      
        Database.PutParameter(dbCommand,"?State", zip.State);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(Zip zip)
      {
        Insert(zip, null);
      }


      public static void Insert(List<Zip>  zipList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Zip zip in  zipList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ZipCode", zip.ZipCode);
      
        Database.PutParameter(dbCommand,"?AreaId", zip.AreaId);
      
        Database.PutParameter(dbCommand,"?City", zip.City);
      
        Database.PutParameter(dbCommand,"?State", zip.State);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ZipCode",zip.ZipCode);
      
        Database.UpdateParameter(dbCommand,"?AreaId",zip.AreaId);
      
        Database.UpdateParameter(dbCommand,"?City",zip.City);
      
        Database.UpdateParameter(dbCommand,"?State",zip.State);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<Zip>  zipList)
      {
        Insert(zipList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Zip Set "
      
        + " AreaId = ?AreaId, "
      
        + " State = ?State "
      
        + " Where "
        
          + " ZipCode = ?ZipCode and  "
        
          + " City = ?City "
        
      ;

      public static void Update(Zip zip, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ZipCode", zip.ZipCode);
      
        Database.PutParameter(dbCommand,"?AreaId", zip.AreaId);
      
        Database.PutParameter(dbCommand,"?City", zip.City);
      
        Database.PutParameter(dbCommand,"?State", zip.State);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Zip zip)
      {
        Update(zip, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ZipCode, "
      
        + " AreaId, "
      
        + " City, "
      
        + " State "
      

      + " From Zip "

      
        + " Where "
        
          + " ZipCode = ?ZipCode and  "
        
          + " City = ?City "
        
      ;

      public static Zip FindByPrimaryKey(
      String zipCode,String city, IDbConnection connection
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ZipCode", zipCode);
      
        Database.PutParameter(dbCommand,"?City", city);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("Zip not found, search by primary key");

      }

      public static Zip FindByPrimaryKey(
      String zipCode,String city
      )
      {
      return FindByPrimaryKey(
      zipCode,city, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Zip zip, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ZipCode",zip.ZipCode);
      
        Database.PutParameter(dbCommand,"?City",zip.City);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Zip zip)
      {
      return Exists(zip, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Zip limit 1";

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

      public static Zip Load(IDataReader dataReader, int offset)
      {
      Zip zip = new Zip();

      zip.ZipCode = dataReader.GetString(0 + offset);
          zip.AreaId = dataReader.GetByte(1 + offset);
          zip.City = dataReader.GetString(2 + offset);
          zip.State = dataReader.GetString(3 + offset);
          

      return zip;
      }

      public static Zip Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Zip "

      
        + " Where "
        
          + " ZipCode = ?ZipCode and  "
        
          + " City = ?City "
        
      ;
      public static void Delete(Zip zip, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ZipCode", zip.ZipCode);
      
        Database.PutParameter(dbCommand,"?City", zip.City);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Zip zip)
      {
        Delete(zip, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Zip ";

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

      
        + " ZipCode, "
      
        + " AreaId, "
      
        + " City, "
      
        + " State "
      

      + " From Zip ";
      public static List<Zip> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Zip> rv = new List<Zip>();

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

      public static List<Zip> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Zip> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Zip obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ZipCode == obj.ZipCode && AreaId == obj.AreaId && City == obj.City && State == obj.State;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Zip> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Zip));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Zip item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Zip>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Zip));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Zip> itemsList
      = new List<Zip>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Zip)
      itemsList.Add(deserializedObject as Zip);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected String m_zipCode;
      
        protected byte m_areaId;
      
        protected String m_city;
      
        protected String m_state;
      
      #endregion

      #region Constructors
      public Zip(
      String 
          zipCode,String 
          city
      ) : this()
      {
      
        m_zipCode = zipCode;
      
        m_city = city;
      
      }

      


        public Zip(
        String 
          zipCode,byte 
          areaId,String 
          city,String 
          state
        ) : this()
        {
        
          m_zipCode = zipCode;
        
          m_areaId = areaId;
        
          m_city = city;
        
          m_state = state;
        
        }


      
      #endregion

      
        [XmlElement]
        public String ZipCode
        {
        get { return m_zipCode;}
        set { m_zipCode = value; }
        }
      
        [XmlElement]
        public byte AreaId
        {
        get { return m_areaId;}
        set { m_areaId = value; }
        }
      
        [XmlElement]
        public String City
        {
        get { return m_city;}
        set { m_city = value; }
        }
      
        [XmlElement]
        public String State
        {
        get { return m_state;}
        set { m_state = value; }
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

    