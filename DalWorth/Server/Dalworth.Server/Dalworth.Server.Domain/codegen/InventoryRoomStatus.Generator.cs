
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


      public partial class InventoryRoomStatus : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into InventoryRoomStatus ( " +
      
        " ID, " +
      
        " Status, " +
      
        " Description " +
      
      ") Values (" +
      
        " ?ID, " +
      
        " ?Status, " +
      
        " ?Description " +
      
      ")";

      public static void Insert(InventoryRoomStatus inventoryRoomStatus, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", inventoryRoomStatus.ID);
      
        Database.PutParameter(dbCommand,"?Status", inventoryRoomStatus.Status);
      
        Database.PutParameter(dbCommand,"?Description", inventoryRoomStatus.Description);
      

      dbCommand.ExecuteNonQuery();

      

      }
      }

      public static void Insert(InventoryRoomStatus inventoryRoomStatus)
      {
        Insert(inventoryRoomStatus, null);
      }


      public static void Insert(List<InventoryRoomStatus>  inventoryRoomStatusList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(InventoryRoomStatus inventoryRoomStatus in  inventoryRoomStatusList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?ID", inventoryRoomStatus.ID);
      
        Database.PutParameter(dbCommand,"?Status", inventoryRoomStatus.Status);
      
        Database.PutParameter(dbCommand,"?Description", inventoryRoomStatus.Description);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?ID",inventoryRoomStatus.ID);
      
        Database.UpdateParameter(dbCommand,"?Status",inventoryRoomStatus.Status);
      
        Database.UpdateParameter(dbCommand,"?Description",inventoryRoomStatus.Description);
      
      }

      dbCommand.ExecuteNonQuery();

      

      }
      }
      }

      public static void Insert(List<InventoryRoomStatus>  inventoryRoomStatusList)
      {
        Insert(inventoryRoomStatusList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update InventoryRoomStatus Set "
      
        + " Status = ?Status, "
      
        + " Description = ?Description "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(InventoryRoomStatus inventoryRoomStatus, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", inventoryRoomStatus.ID);
      
        Database.PutParameter(dbCommand,"?Status", inventoryRoomStatus.Status);
      
        Database.PutParameter(dbCommand,"?Description", inventoryRoomStatus.Description);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(InventoryRoomStatus inventoryRoomStatus)
      {
        Update(inventoryRoomStatus, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " Status, "
      
        + " Description "
      

      + " From InventoryRoomStatus "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static InventoryRoomStatus FindByPrimaryKey(
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
      throw new DataNotFoundException("InventoryRoomStatus not found, search by primary key");

      }

      public static InventoryRoomStatus FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(InventoryRoomStatus inventoryRoomStatus, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",inventoryRoomStatus.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(InventoryRoomStatus inventoryRoomStatus)
      {
      return Exists(inventoryRoomStatus, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from InventoryRoomStatus limit 1";

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

      public static InventoryRoomStatus Load(IDataReader dataReader, int offset)
      {
      InventoryRoomStatus inventoryRoomStatus = new InventoryRoomStatus();

      inventoryRoomStatus.ID = dataReader.GetInt32(0 + offset);
          inventoryRoomStatus.Status = dataReader.GetString(1 + offset);
          
            if(!dataReader.IsDBNull(2 + offset))
            inventoryRoomStatus.Description = dataReader.GetString(2 + offset);
          

      return inventoryRoomStatus;
      }

      public static InventoryRoomStatus Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From InventoryRoomStatus "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(InventoryRoomStatus inventoryRoomStatus, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", inventoryRoomStatus.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(InventoryRoomStatus inventoryRoomStatus)
      {
        Delete(inventoryRoomStatus, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From InventoryRoomStatus ";

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
      
        + " Status, "
      
        + " Description "
      

      + " From InventoryRoomStatus ";
      public static List<InventoryRoomStatus> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<InventoryRoomStatus> rv = new List<InventoryRoomStatus>();

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

      public static List<InventoryRoomStatus> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<InventoryRoomStatus> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (InventoryRoomStatus obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && Status == obj.Status && Description == obj.Description;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<InventoryRoomStatus> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryRoomStatus));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(InventoryRoomStatus item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<InventoryRoomStatus>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryRoomStatus));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<InventoryRoomStatus> itemsList
      = new List<InventoryRoomStatus>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is InventoryRoomStatus)
      itemsList.Add(deserializedObject as InventoryRoomStatus);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected String m_status;
      
        protected String m_description;
      
      #endregion

      #region Constructors
      public InventoryRoomStatus(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public InventoryRoomStatus(
        int 
          iD,String 
          status,String 
          description
        ) : this()
        {
        
          m_iD = iD;
        
          m_status = status;
        
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
        public String Status
        {
        get { return m_status;}
        set { m_status = value; }
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

    