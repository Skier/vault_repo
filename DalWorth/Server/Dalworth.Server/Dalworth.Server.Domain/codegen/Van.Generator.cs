
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


      public partial class Van : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Van ( " +
      
        " ServmanTruckId, " +
      
        " ServmanTruckNum, " +
      
        " AreaId, " +
      
        " LicensePlateNumber, " +
      
        " EngineNumber, " +
      
        " BodyNumber, " +
      
        " Color, " +
      
        " OilChangeDue, " +
      
        " PagerNumber " +
      
      ") Values (" +
      
        " ?ServmanTruckId, " +
      
        " ?ServmanTruckNum, " +
      
        " ?AreaId, " +
      
        " ?LicensePlateNumber, " +
      
        " ?EngineNumber, " +
      
        " ?BodyNumber, " +
      
        " ?Color, " +
      
        " ?OilChangeDue, " +
      
        " ?PagerNumber " +
      
      ")";

      public static void Insert(Van van, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ServmanTruckId", van.ServmanTruckId);
      
        Database.PutParameter(dbCommand,"?ServmanTruckNum", van.ServmanTruckNum);
      
        Database.PutParameter(dbCommand,"?AreaId", van.AreaId);
      
        Database.PutParameter(dbCommand,"?LicensePlateNumber", van.LicensePlateNumber);
      
        Database.PutParameter(dbCommand,"?EngineNumber", van.EngineNumber);
      
        Database.PutParameter(dbCommand,"?BodyNumber", van.BodyNumber);
      
        Database.PutParameter(dbCommand,"?Color", van.Color);
      
        Database.PutParameter(dbCommand,"?OilChangeDue", van.OilChangeDue);
      
        Database.PutParameter(dbCommand,"?PagerNumber", van.PagerNumber);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        van.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Van van)
      {
        Insert(van, null);
      }


      public static void Insert(List<Van>  vanList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Van van in  vanList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ServmanTruckId", van.ServmanTruckId);
      
        Database.PutParameter(dbCommand,"?ServmanTruckNum", van.ServmanTruckNum);
      
        Database.PutParameter(dbCommand,"?AreaId", van.AreaId);
      
        Database.PutParameter(dbCommand,"?LicensePlateNumber", van.LicensePlateNumber);
      
        Database.PutParameter(dbCommand,"?EngineNumber", van.EngineNumber);
      
        Database.PutParameter(dbCommand,"?BodyNumber", van.BodyNumber);
      
        Database.PutParameter(dbCommand,"?Color", van.Color);
      
        Database.PutParameter(dbCommand,"?OilChangeDue", van.OilChangeDue);
      
        Database.PutParameter(dbCommand,"?PagerNumber", van.PagerNumber);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ServmanTruckId",van.ServmanTruckId);
      
        Database.UpdateParameter(dbCommand,"?ServmanTruckNum",van.ServmanTruckNum);
      
        Database.UpdateParameter(dbCommand,"?AreaId",van.AreaId);
      
        Database.UpdateParameter(dbCommand,"?LicensePlateNumber",van.LicensePlateNumber);
      
        Database.UpdateParameter(dbCommand,"?EngineNumber",van.EngineNumber);
      
        Database.UpdateParameter(dbCommand,"?BodyNumber",van.BodyNumber);
      
        Database.UpdateParameter(dbCommand,"?Color",van.Color);
      
        Database.UpdateParameter(dbCommand,"?OilChangeDue",van.OilChangeDue);
      
        Database.UpdateParameter(dbCommand,"?PagerNumber",van.PagerNumber);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        van.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Van>  vanList)
      {
        Insert(vanList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Van Set "
      
        + " ServmanTruckId = ?ServmanTruckId, "
      
        + " ServmanTruckNum = ?ServmanTruckNum, "
      
        + " AreaId = ?AreaId, "
      
        + " LicensePlateNumber = ?LicensePlateNumber, "
      
        + " EngineNumber = ?EngineNumber, "
      
        + " BodyNumber = ?BodyNumber, "
      
        + " Color = ?Color, "
      
        + " OilChangeDue = ?OilChangeDue, "
      
        + " PagerNumber = ?PagerNumber "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Van van, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", van.ID);
      
        Database.PutParameter(dbCommand,"?ServmanTruckId", van.ServmanTruckId);
      
        Database.PutParameter(dbCommand,"?ServmanTruckNum", van.ServmanTruckNum);
      
        Database.PutParameter(dbCommand,"?AreaId", van.AreaId);
      
        Database.PutParameter(dbCommand,"?LicensePlateNumber", van.LicensePlateNumber);
      
        Database.PutParameter(dbCommand,"?EngineNumber", van.EngineNumber);
      
        Database.PutParameter(dbCommand,"?BodyNumber", van.BodyNumber);
      
        Database.PutParameter(dbCommand,"?Color", van.Color);
      
        Database.PutParameter(dbCommand,"?OilChangeDue", van.OilChangeDue);
      
        Database.PutParameter(dbCommand,"?PagerNumber", van.PagerNumber);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Van van)
      {
        Update(van, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " ServmanTruckId, "
      
        + " ServmanTruckNum, "
      
        + " AreaId, "
      
        + " LicensePlateNumber, "
      
        + " EngineNumber, "
      
        + " BodyNumber, "
      
        + " Color, "
      
        + " OilChangeDue, "
      
        + " PagerNumber "
      

      + " From Van "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Van FindByPrimaryKey(
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
      throw new DataNotFoundException("Van not found, search by primary key");

      }

      public static Van FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Van van, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",van.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Van van)
      {
      return Exists(van, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Van limit 1";

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

      public static Van Load(IDataReader dataReader, int offset)
      {
      Van van = new Van();

      van.ID = dataReader.GetInt32(0 + offset);
          
            if(!dataReader.IsDBNull(1 + offset))
            van.ServmanTruckId = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            van.ServmanTruckNum = dataReader.GetString(2 + offset);
          van.AreaId = dataReader.GetByte(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            van.LicensePlateNumber = dataReader.GetString(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            van.EngineNumber = dataReader.GetString(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            van.BodyNumber = dataReader.GetString(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            van.Color = dataReader.GetString(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            van.OilChangeDue = dataReader.GetString(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            van.PagerNumber = dataReader.GetString(9 + offset);
          

      return van;
      }

      public static Van Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Van "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Van van, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", van.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Van van)
      {
        Delete(van, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Van ";

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
      
        + " ServmanTruckId, "
      
        + " ServmanTruckNum, "
      
        + " AreaId, "
      
        + " LicensePlateNumber, "
      
        + " EngineNumber, "
      
        + " BodyNumber, "
      
        + " Color, "
      
        + " OilChangeDue, "
      
        + " PagerNumber "
      

      + " From Van ";
      public static List<Van> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Van> rv = new List<Van>();

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

      public static List<Van> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Van> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Van obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && ServmanTruckId == obj.ServmanTruckId && ServmanTruckNum == obj.ServmanTruckNum && AreaId == obj.AreaId && LicensePlateNumber == obj.LicensePlateNumber && EngineNumber == obj.EngineNumber && BodyNumber == obj.BodyNumber && Color == obj.Color && OilChangeDue == obj.OilChangeDue && PagerNumber == obj.PagerNumber;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Van> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Van));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Van item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Van>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Van));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Van> itemsList
      = new List<Van>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Van)
      itemsList.Add(deserializedObject as Van);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_servmanTruckId;
      
        protected String m_servmanTruckNum;
      
        protected byte m_areaId;
      
        protected String m_licensePlateNumber;
      
        protected String m_engineNumber;
      
        protected String m_bodyNumber;
      
        protected String m_color;
      
        protected String m_oilChangeDue;
      
        protected String m_pagerNumber;
      
      #endregion

      #region Constructors
      public Van(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Van(
        int 
          iD,String 
          servmanTruckId,String 
          servmanTruckNum,byte 
          areaId,String 
          licensePlateNumber,String 
          engineNumber,String 
          bodyNumber,String 
          color,String 
          oilChangeDue,String 
          pagerNumber
        ) : this()
        {
        
          m_iD = iD;
        
          m_servmanTruckId = servmanTruckId;
        
          m_servmanTruckNum = servmanTruckNum;
        
          m_areaId = areaId;
        
          m_licensePlateNumber = licensePlateNumber;
        
          m_engineNumber = engineNumber;
        
          m_bodyNumber = bodyNumber;
        
          m_color = color;
        
          m_oilChangeDue = oilChangeDue;
        
          m_pagerNumber = pagerNumber;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String ServmanTruckId
        {
        get { return m_servmanTruckId;}
        set { m_servmanTruckId = value; }
        }
      
        [XmlElement]
        public String ServmanTruckNum
        {
        get { return m_servmanTruckNum;}
        set { m_servmanTruckNum = value; }
        }
      
        [XmlElement]
        public byte AreaId
        {
        get { return m_areaId;}
        set { m_areaId = value; }
        }
      
        [XmlElement]
        public String LicensePlateNumber
        {
        get { return m_licensePlateNumber;}
        set { m_licensePlateNumber = value; }
        }
      
        [XmlElement]
        public String EngineNumber
        {
        get { return m_engineNumber;}
        set { m_engineNumber = value; }
        }
      
        [XmlElement]
        public String BodyNumber
        {
        get { return m_bodyNumber;}
        set { m_bodyNumber = value; }
        }
      
        [XmlElement]
        public String Color
        {
        get { return m_color;}
        set { m_color = value; }
        }
      
        [XmlElement]
        public String OilChangeDue
        {
        get { return m_oilChangeDue;}
        set { m_oilChangeDue = value; }
        }
      
        [XmlElement]
        public String PagerNumber
        {
        get { return m_pagerNumber;}
        set { m_pagerNumber = value; }
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

    