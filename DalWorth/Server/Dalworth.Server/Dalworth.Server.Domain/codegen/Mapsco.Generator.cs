
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


      public partial class Mapsco : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Mapsco ( " +
      
        " IdOld, " +
      
        " IdProduct, " +
      
        " Map, " +
      
        " UpperLeftLatitude, " +
      
        " UpperLeftLongitude, " +
      
        " UpperRightLatitude, " +
      
        " UpperRightLongitude, " +
      
        " LowerLeftLatitude, " +
      
        " LowerLeftLongitude, " +
      
        " LowerRightLatitude, " +
      
        " LowerRightLongitude, " +
      
        " PrintingSc " +
      
      ") Values (" +
      
        " ?IdOld, " +
      
        " ?IdProduct, " +
      
        " ?Map, " +
      
        " ?UpperLeftLatitude, " +
      
        " ?UpperLeftLongitude, " +
      
        " ?UpperRightLatitude, " +
      
        " ?UpperRightLongitude, " +
      
        " ?LowerLeftLatitude, " +
      
        " ?LowerLeftLongitude, " +
      
        " ?LowerRightLatitude, " +
      
        " ?LowerRightLongitude, " +
      
        " ?PrintingSc " +
      
      ")";

      public static void Insert(Mapsco mapsco, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?IdOld", mapsco.IdOld);
      
        Database.PutParameter(dbCommand,"?IdProduct", mapsco.IdProduct);
      
        Database.PutParameter(dbCommand,"?Map", mapsco.Map);
      
        Database.PutParameter(dbCommand,"?UpperLeftLatitude", mapsco.UpperLeftLatitude);
      
        Database.PutParameter(dbCommand,"?UpperLeftLongitude", mapsco.UpperLeftLongitude);
      
        Database.PutParameter(dbCommand,"?UpperRightLatitude", mapsco.UpperRightLatitude);
      
        Database.PutParameter(dbCommand,"?UpperRightLongitude", mapsco.UpperRightLongitude);
      
        Database.PutParameter(dbCommand,"?LowerLeftLatitude", mapsco.LowerLeftLatitude);
      
        Database.PutParameter(dbCommand,"?LowerLeftLongitude", mapsco.LowerLeftLongitude);
      
        Database.PutParameter(dbCommand,"?LowerRightLatitude", mapsco.LowerRightLatitude);
      
        Database.PutParameter(dbCommand,"?LowerRightLongitude", mapsco.LowerRightLongitude);
      
        Database.PutParameter(dbCommand,"?PrintingSc", mapsco.PrintingSc);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        mapsco.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Mapsco mapsco)
      {
        Insert(mapsco, null);
      }


      public static void Insert(List<Mapsco>  mapscoList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Mapsco mapsco in  mapscoList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?IdOld", mapsco.IdOld);
      
        Database.PutParameter(dbCommand,"?IdProduct", mapsco.IdProduct);
      
        Database.PutParameter(dbCommand,"?Map", mapsco.Map);
      
        Database.PutParameter(dbCommand,"?UpperLeftLatitude", mapsco.UpperLeftLatitude);
      
        Database.PutParameter(dbCommand,"?UpperLeftLongitude", mapsco.UpperLeftLongitude);
      
        Database.PutParameter(dbCommand,"?UpperRightLatitude", mapsco.UpperRightLatitude);
      
        Database.PutParameter(dbCommand,"?UpperRightLongitude", mapsco.UpperRightLongitude);
      
        Database.PutParameter(dbCommand,"?LowerLeftLatitude", mapsco.LowerLeftLatitude);
      
        Database.PutParameter(dbCommand,"?LowerLeftLongitude", mapsco.LowerLeftLongitude);
      
        Database.PutParameter(dbCommand,"?LowerRightLatitude", mapsco.LowerRightLatitude);
      
        Database.PutParameter(dbCommand,"?LowerRightLongitude", mapsco.LowerRightLongitude);
      
        Database.PutParameter(dbCommand,"?PrintingSc", mapsco.PrintingSc);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?IdOld",mapsco.IdOld);
      
        Database.UpdateParameter(dbCommand,"?IdProduct",mapsco.IdProduct);
      
        Database.UpdateParameter(dbCommand,"?Map",mapsco.Map);
      
        Database.UpdateParameter(dbCommand,"?UpperLeftLatitude",mapsco.UpperLeftLatitude);
      
        Database.UpdateParameter(dbCommand,"?UpperLeftLongitude",mapsco.UpperLeftLongitude);
      
        Database.UpdateParameter(dbCommand,"?UpperRightLatitude",mapsco.UpperRightLatitude);
      
        Database.UpdateParameter(dbCommand,"?UpperRightLongitude",mapsco.UpperRightLongitude);
      
        Database.UpdateParameter(dbCommand,"?LowerLeftLatitude",mapsco.LowerLeftLatitude);
      
        Database.UpdateParameter(dbCommand,"?LowerLeftLongitude",mapsco.LowerLeftLongitude);
      
        Database.UpdateParameter(dbCommand,"?LowerRightLatitude",mapsco.LowerRightLatitude);
      
        Database.UpdateParameter(dbCommand,"?LowerRightLongitude",mapsco.LowerRightLongitude);
      
        Database.UpdateParameter(dbCommand,"?PrintingSc",mapsco.PrintingSc);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        mapsco.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Mapsco>  mapscoList)
      {
        Insert(mapscoList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Mapsco Set "
      
        + " IdOld = ?IdOld, "
      
        + " IdProduct = ?IdProduct, "
      
        + " Map = ?Map, "
      
        + " UpperLeftLatitude = ?UpperLeftLatitude, "
      
        + " UpperLeftLongitude = ?UpperLeftLongitude, "
      
        + " UpperRightLatitude = ?UpperRightLatitude, "
      
        + " UpperRightLongitude = ?UpperRightLongitude, "
      
        + " LowerLeftLatitude = ?LowerLeftLatitude, "
      
        + " LowerLeftLongitude = ?LowerLeftLongitude, "
      
        + " LowerRightLatitude = ?LowerRightLatitude, "
      
        + " LowerRightLongitude = ?LowerRightLongitude, "
      
        + " PrintingSc = ?PrintingSc "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Mapsco mapsco, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", mapsco.ID);
      
        Database.PutParameter(dbCommand,"?IdOld", mapsco.IdOld);
      
        Database.PutParameter(dbCommand,"?IdProduct", mapsco.IdProduct);
      
        Database.PutParameter(dbCommand,"?Map", mapsco.Map);
      
        Database.PutParameter(dbCommand,"?UpperLeftLatitude", mapsco.UpperLeftLatitude);
      
        Database.PutParameter(dbCommand,"?UpperLeftLongitude", mapsco.UpperLeftLongitude);
      
        Database.PutParameter(dbCommand,"?UpperRightLatitude", mapsco.UpperRightLatitude);
      
        Database.PutParameter(dbCommand,"?UpperRightLongitude", mapsco.UpperRightLongitude);
      
        Database.PutParameter(dbCommand,"?LowerLeftLatitude", mapsco.LowerLeftLatitude);
      
        Database.PutParameter(dbCommand,"?LowerLeftLongitude", mapsco.LowerLeftLongitude);
      
        Database.PutParameter(dbCommand,"?LowerRightLatitude", mapsco.LowerRightLatitude);
      
        Database.PutParameter(dbCommand,"?LowerRightLongitude", mapsco.LowerRightLongitude);
      
        Database.PutParameter(dbCommand,"?PrintingSc", mapsco.PrintingSc);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Mapsco mapsco)
      {
        Update(mapsco, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " IdOld, "
      
        + " IdProduct, "
      
        + " Map, "
      
        + " UpperLeftLatitude, "
      
        + " UpperLeftLongitude, "
      
        + " UpperRightLatitude, "
      
        + " UpperRightLongitude, "
      
        + " LowerLeftLatitude, "
      
        + " LowerLeftLongitude, "
      
        + " LowerRightLatitude, "
      
        + " LowerRightLongitude, "
      
        + " PrintingSc "
      

      + " From Mapsco "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Mapsco FindByPrimaryKey(
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
      throw new DataNotFoundException("Mapsco not found, search by primary key");

      }

      public static Mapsco FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Mapsco mapsco, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",mapsco.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Mapsco mapsco)
      {
      return Exists(mapsco, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Mapsco limit 1";

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

      public static Mapsco Load(IDataReader dataReader, int offset)
      {
      Mapsco mapsco = new Mapsco();

      mapsco.ID = dataReader.GetInt32(0 + offset);
          mapsco.IdOld = dataReader.GetFloat(1 + offset);
          mapsco.IdProduct = dataReader.GetFloat(2 + offset);
          mapsco.Map = dataReader.GetString(3 + offset);
          mapsco.UpperLeftLatitude = dataReader.GetFloat(4 + offset);
          mapsco.UpperLeftLongitude = dataReader.GetFloat(5 + offset);
          mapsco.UpperRightLatitude = dataReader.GetFloat(6 + offset);
          mapsco.UpperRightLongitude = dataReader.GetFloat(7 + offset);
          mapsco.LowerLeftLatitude = dataReader.GetFloat(8 + offset);
          mapsco.LowerLeftLongitude = dataReader.GetFloat(9 + offset);
          mapsco.LowerRightLatitude = dataReader.GetFloat(10 + offset);
          mapsco.LowerRightLongitude = dataReader.GetFloat(11 + offset);
          mapsco.PrintingSc = dataReader.GetFloat(12 + offset);
          

      return mapsco;
      }

      public static Mapsco Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Mapsco "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Mapsco mapsco, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", mapsco.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Mapsco mapsco)
      {
        Delete(mapsco, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Mapsco ";

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
      
        + " IdOld, "
      
        + " IdProduct, "
      
        + " Map, "
      
        + " UpperLeftLatitude, "
      
        + " UpperLeftLongitude, "
      
        + " UpperRightLatitude, "
      
        + " UpperRightLongitude, "
      
        + " LowerLeftLatitude, "
      
        + " LowerLeftLongitude, "
      
        + " LowerRightLatitude, "
      
        + " LowerRightLongitude, "
      
        + " PrintingSc "
      

      + " From Mapsco ";
      public static List<Mapsco> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Mapsco> rv = new List<Mapsco>();

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

      public static List<Mapsco> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Mapsco> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Mapsco obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && IdOld == obj.IdOld && IdProduct == obj.IdProduct && Map == obj.Map && UpperLeftLatitude == obj.UpperLeftLatitude && UpperLeftLongitude == obj.UpperLeftLongitude && UpperRightLatitude == obj.UpperRightLatitude && UpperRightLongitude == obj.UpperRightLongitude && LowerLeftLatitude == obj.LowerLeftLatitude && LowerLeftLongitude == obj.LowerLeftLongitude && LowerRightLatitude == obj.LowerRightLatitude && LowerRightLongitude == obj.LowerRightLongitude && PrintingSc == obj.PrintingSc;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Mapsco> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Mapsco));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Mapsco item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Mapsco>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Mapsco));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Mapsco> itemsList
      = new List<Mapsco>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Mapsco)
      itemsList.Add(deserializedObject as Mapsco);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected float m_idOld;
      
        protected float m_idProduct;
      
        protected String m_map;
      
        protected float m_upperLeftLatitude;
      
        protected float m_upperLeftLongitude;
      
        protected float m_upperRightLatitude;
      
        protected float m_upperRightLongitude;
      
        protected float m_lowerLeftLatitude;
      
        protected float m_lowerLeftLongitude;
      
        protected float m_lowerRightLatitude;
      
        protected float m_lowerRightLongitude;
      
        protected float m_printingSc;
      
      #endregion

      #region Constructors
      public Mapsco(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Mapsco(
        int 
          iD,float 
          idOld,float 
          idProduct,String 
          map,float 
          upperLeftLatitude,float 
          upperLeftLongitude,float 
          upperRightLatitude,float 
          upperRightLongitude,float 
          lowerLeftLatitude,float 
          lowerLeftLongitude,float 
          lowerRightLatitude,float 
          lowerRightLongitude,float 
          printingSc
        ) : this()
        {
        
          m_iD = iD;
        
          m_idOld = idOld;
        
          m_idProduct = idProduct;
        
          m_map = map;
        
          m_upperLeftLatitude = upperLeftLatitude;
        
          m_upperLeftLongitude = upperLeftLongitude;
        
          m_upperRightLatitude = upperRightLatitude;
        
          m_upperRightLongitude = upperRightLongitude;
        
          m_lowerLeftLatitude = lowerLeftLatitude;
        
          m_lowerLeftLongitude = lowerLeftLongitude;
        
          m_lowerRightLatitude = lowerRightLatitude;
        
          m_lowerRightLongitude = lowerRightLongitude;
        
          m_printingSc = printingSc;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public float IdOld
        {
        get { return m_idOld;}
        set { m_idOld = value; }
        }
      
        [XmlElement]
        public float IdProduct
        {
        get { return m_idProduct;}
        set { m_idProduct = value; }
        }
      
        [XmlElement]
        public String Map
        {
        get { return m_map;}
        set { m_map = value; }
        }
      
        [XmlElement]
        public float UpperLeftLatitude
        {
        get { return m_upperLeftLatitude;}
        set { m_upperLeftLatitude = value; }
        }
      
        [XmlElement]
        public float UpperLeftLongitude
        {
        get { return m_upperLeftLongitude;}
        set { m_upperLeftLongitude = value; }
        }
      
        [XmlElement]
        public float UpperRightLatitude
        {
        get { return m_upperRightLatitude;}
        set { m_upperRightLatitude = value; }
        }
      
        [XmlElement]
        public float UpperRightLongitude
        {
        get { return m_upperRightLongitude;}
        set { m_upperRightLongitude = value; }
        }
      
        [XmlElement]
        public float LowerLeftLatitude
        {
        get { return m_lowerLeftLatitude;}
        set { m_lowerLeftLatitude = value; }
        }
      
        [XmlElement]
        public float LowerLeftLongitude
        {
        get { return m_lowerLeftLongitude;}
        set { m_lowerLeftLongitude = value; }
        }
      
        [XmlElement]
        public float LowerRightLatitude
        {
        get { return m_lowerRightLatitude;}
        set { m_lowerRightLatitude = value; }
        }
      
        [XmlElement]
        public float LowerRightLongitude
        {
        get { return m_lowerRightLongitude;}
        set { m_lowerRightLongitude = value; }
        }
      
        [XmlElement]
        public float PrintingSc
        {
        get { return m_printingSc;}
        set { m_printingSc = value; }
        }
      

      public static int FieldsCount
      {
      get { return 13; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    