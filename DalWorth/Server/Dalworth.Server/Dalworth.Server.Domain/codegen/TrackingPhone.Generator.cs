
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


      public partial class TrackingPhone : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into TrackingPhone ( " +
      
        " Number " +
      
      ") Values (" +
      
        " ?Number " +
      
      ")";

      public static void Insert(TrackingPhone trackingPhone, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?Number", trackingPhone.Number);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        trackingPhone.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(TrackingPhone trackingPhone)
      {
        Insert(trackingPhone, null);
      }


      public static void Insert(List<TrackingPhone>  trackingPhoneList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(TrackingPhone trackingPhone in  trackingPhoneList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?Number", trackingPhone.Number);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?Number",trackingPhone.Number);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        trackingPhone.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<TrackingPhone>  trackingPhoneList)
      {
        Insert(trackingPhoneList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update TrackingPhone Set "
      
        + " Number = ?Number "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(TrackingPhone trackingPhone, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", trackingPhone.ID);
      
        Database.PutParameter(dbCommand,"?Number", trackingPhone.Number);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(TrackingPhone trackingPhone)
      {
        Update(trackingPhone, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Number "
      

      + " From TrackingPhone "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static TrackingPhone FindByPrimaryKey(
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
      throw new DataNotFoundException("TrackingPhone not found, search by primary key");

      }

      public static TrackingPhone FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(TrackingPhone trackingPhone, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",trackingPhone.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(TrackingPhone trackingPhone)
      {
      return Exists(trackingPhone, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from TrackingPhone limit 1";

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

      public static TrackingPhone Load(IDataReader dataReader, int offset)
      {
      TrackingPhone trackingPhone = new TrackingPhone();

      trackingPhone.ID = dataReader.GetInt32(0 + offset);
          trackingPhone.Number = dataReader.GetString(1 + offset);
          

      return trackingPhone;
      }

      public static TrackingPhone Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From TrackingPhone "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(TrackingPhone trackingPhone, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", trackingPhone.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(TrackingPhone trackingPhone)
      {
        Delete(trackingPhone, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From TrackingPhone ";

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
      
        + " Number "
      

      + " From TrackingPhone ";
      public static List<TrackingPhone> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<TrackingPhone> rv = new List<TrackingPhone>();

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

      public static List<TrackingPhone> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<TrackingPhone> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (TrackingPhone obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Number == obj.Number;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<TrackingPhone> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TrackingPhone));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(TrackingPhone item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<TrackingPhone>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(TrackingPhone));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<TrackingPhone> itemsList
      = new List<TrackingPhone>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is TrackingPhone)
      itemsList.Add(deserializedObject as TrackingPhone);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_number;
      
      #endregion

      #region Constructors
      public TrackingPhone(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public TrackingPhone(
        int 
          iD,String 
          number
        ) : this()
        {
        
          m_iD = iD;
        
          m_number = number;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public String Number
        {
        get { return m_number;}
        set { m_number = value; }
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

    