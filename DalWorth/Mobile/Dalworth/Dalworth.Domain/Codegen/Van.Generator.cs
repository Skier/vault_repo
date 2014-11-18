
    using System;
    using System.Data;
    using System.Collections.Generic;
    using Dalworth.Data;
    using Dalworth.SDK;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace Dalworth.Domain
      {


      public partial class Van : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Van] ( " +
      
        " ID, " +
      
        " LicensePlateNumber, " +
      
        " EngineNumber, " +
      
        " BodyNumber, " +
      
        " Color, " +
      
        " OilChangeDue " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @LicensePlateNumber, " +
      
        " @EngineNumber, " +
      
        " @BodyNumber, " +
      
        " @Color, " +
      
        " @OilChangeDue " +
      
      ")";

      public static void Insert(Van van, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", van.ID);
      
        Database.PutParameter(dbCommand,"@LicensePlateNumber", van.LicensePlateNumber);
      
        Database.PutParameter(dbCommand,"@EngineNumber", van.EngineNumber);
      
        Database.PutParameter(dbCommand,"@BodyNumber", van.BodyNumber);
      
        Database.PutParameter(dbCommand,"@Color", van.Color);
      
        Database.PutParameter(dbCommand,"@OilChangeDue", van.OilChangeDue);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(Van van)
      {
        Insert(van, null);
      }

      public static void Insert(List<Van>  vanList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(Van van in  vanList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", van.ID);
      
        Database.PutParameter(dbCommand,"@LicensePlateNumber", van.LicensePlateNumber);
      
        Database.PutParameter(dbCommand,"@EngineNumber", van.EngineNumber);
      
        Database.PutParameter(dbCommand,"@BodyNumber", van.BodyNumber);
      
        Database.PutParameter(dbCommand,"@Color", van.Color);
      
        Database.PutParameter(dbCommand,"@OilChangeDue", van.OilChangeDue);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",van.ID);
      
        Database.UpdateParameter(dbCommand,"@LicensePlateNumber",van.LicensePlateNumber);
      
        Database.UpdateParameter(dbCommand,"@EngineNumber",van.EngineNumber);
      
        Database.UpdateParameter(dbCommand,"@BodyNumber",van.BodyNumber);
      
        Database.UpdateParameter(dbCommand,"@Color",van.Color);
      
        Database.UpdateParameter(dbCommand,"@OilChangeDue",van.OilChangeDue);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<Van>  vanList)
      {
      Insert(vanList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Van] Set "
      
        + " LicensePlateNumber = @LicensePlateNumber, "
      
        + " EngineNumber = @EngineNumber, "
      
        + " BodyNumber = @BodyNumber, "
      
        + " Color = @Color, "
      
        + " OilChangeDue = @OilChangeDue "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(Van van, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", van.ID);
      
        Database.PutParameter(dbCommand,"@LicensePlateNumber", van.LicensePlateNumber);
      
        Database.PutParameter(dbCommand,"@EngineNumber", van.EngineNumber);
      
        Database.PutParameter(dbCommand,"@BodyNumber", van.BodyNumber);
      
        Database.PutParameter(dbCommand,"@Color", van.Color);
      
        Database.PutParameter(dbCommand,"@OilChangeDue", van.OilChangeDue);
      

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
      
        + " LicensePlateNumber, "
      
        + " EngineNumber, "
      
        + " BodyNumber, "
      
        + " Color, "
      
        + " OilChangeDue "
      

      + " From [Van] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static Van FindByPrimaryKey(
      int iD, IDbTransaction transaction
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", iD);
      

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
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(Van van, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",van.ID);
      

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

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from Van";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql, transaction))
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

      public static Van Load(IDataReader dataReader)
      {
      Van van = new Van();

      van.ID = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            van.LicensePlateNumber = dataReader.GetString(1);
          
            if(!dataReader.IsDBNull(2))
            van.EngineNumber = dataReader.GetString(2);
          
            if(!dataReader.IsDBNull(3))
            van.BodyNumber = dataReader.GetString(3);
          
            if(!dataReader.IsDBNull(4))
            van.Color = dataReader.GetString(4);
          
            if(!dataReader.IsDBNull(5))
            van.OilChangeDue = dataReader.GetString(5);
          

      return van;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Van] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(Van van, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", van.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Van van)
      {
      Delete(van, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Van] ";

      public static void Clear(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll, transaction))
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
      
        + " LicensePlateNumber, "
      
        + " EngineNumber, "
      
        + " BodyNumber, "
      
        + " Color, "
      
        + " OilChangeDue "
      

      + " From [Van] ";
      public static List<Van> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
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
      
        protected String m_licensePlateNumber;
      
        protected String m_engineNumber;
      
        protected String m_bodyNumber;
      
        protected String m_color;
      
        protected String m_oilChangeDue;
      
      #endregion

      #region Constructors
      public Van(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public Van(
        int 
          iD,String 
          licensePlateNumber,String 
          engineNumber,String 
          bodyNumber,String 
          color,String 
          oilChangeDue
        )
        {
        
          m_iD = iD;
        
          m_licensePlateNumber = licensePlateNumber;
        
          m_engineNumber = engineNumber;
        
          m_bodyNumber = bodyNumber;
        
          m_color = color;
        
          m_oilChangeDue = oilChangeDue;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
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
      
      }
      #endregion
      }

    