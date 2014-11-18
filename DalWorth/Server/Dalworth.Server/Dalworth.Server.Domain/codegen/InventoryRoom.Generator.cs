
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


      public partial class InventoryRoom : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into InventoryRoom ( " +
      
        " AreaId, " +
      
        " Name, " +
      
        " InventoryRoomStatusId " +
      
      ") Values (" +
      
        " ?AreaId, " +
      
        " ?Name, " +
      
        " ?InventoryRoomStatusId " +
      
      ")";

      public static void Insert(InventoryRoom inventoryRoom, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?AreaId", inventoryRoom.AreaId);
      
        Database.PutParameter(dbCommand,"?Name", inventoryRoom.Name);
      
        Database.PutParameter(dbCommand,"?InventoryRoomStatusId", inventoryRoom.InventoryRoomStatusId);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        inventoryRoom.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(InventoryRoom inventoryRoom)
      {
        Insert(inventoryRoom, null);
      }


      public static void Insert(List<InventoryRoom>  inventoryRoomList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(InventoryRoom inventoryRoom in  inventoryRoomList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?AreaId", inventoryRoom.AreaId);
      
        Database.PutParameter(dbCommand,"?Name", inventoryRoom.Name);
      
        Database.PutParameter(dbCommand,"?InventoryRoomStatusId", inventoryRoom.InventoryRoomStatusId);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?AreaId",inventoryRoom.AreaId);
      
        Database.UpdateParameter(dbCommand,"?Name",inventoryRoom.Name);
      
        Database.UpdateParameter(dbCommand,"?InventoryRoomStatusId",inventoryRoom.InventoryRoomStatusId);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        inventoryRoom.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<InventoryRoom>  inventoryRoomList)
      {
        Insert(inventoryRoomList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update InventoryRoom Set "
      
        + " AreaId = ?AreaId, "
      
        + " Name = ?Name, "
      
        + " InventoryRoomStatusId = ?InventoryRoomStatusId "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(InventoryRoom inventoryRoom, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", inventoryRoom.ID);
      
        Database.PutParameter(dbCommand,"?AreaId", inventoryRoom.AreaId);
      
        Database.PutParameter(dbCommand,"?Name", inventoryRoom.Name);
      
        Database.PutParameter(dbCommand,"?InventoryRoomStatusId", inventoryRoom.InventoryRoomStatusId);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(InventoryRoom inventoryRoom)
      {
        Update(inventoryRoom, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " AreaId, "
      
        + " Name, "
      
        + " InventoryRoomStatusId "
      

      + " From InventoryRoom "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static InventoryRoom FindByPrimaryKey(
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
      throw new DataNotFoundException("InventoryRoom not found, search by primary key");

      }

      public static InventoryRoom FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(InventoryRoom inventoryRoom, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",inventoryRoom.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(InventoryRoom inventoryRoom)
      {
      return Exists(inventoryRoom, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from InventoryRoom limit 1";

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

      public static InventoryRoom Load(IDataReader dataReader, int offset)
      {
      InventoryRoom inventoryRoom = new InventoryRoom();

      inventoryRoom.ID = dataReader.GetInt32(0 + offset);
          inventoryRoom.AreaId = dataReader.GetByte(1 + offset);
          inventoryRoom.Name = dataReader.GetString(2 + offset);
          inventoryRoom.InventoryRoomStatusId = dataReader.GetInt32(3 + offset);
          

      return inventoryRoom;
      }

      public static InventoryRoom Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From InventoryRoom "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(InventoryRoom inventoryRoom, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", inventoryRoom.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(InventoryRoom inventoryRoom)
      {
        Delete(inventoryRoom, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From InventoryRoom ";

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
      
        + " AreaId, "
      
        + " Name, "
      
        + " InventoryRoomStatusId "
      

      + " From InventoryRoom ";
      public static List<InventoryRoom> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<InventoryRoom> rv = new List<InventoryRoom>();

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

      public static List<InventoryRoom> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<InventoryRoom> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (InventoryRoom obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && AreaId == obj.AreaId && Name == obj.Name && InventoryRoomStatusId == obj.InventoryRoomStatusId;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<InventoryRoom> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryRoom));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(InventoryRoom item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<InventoryRoom>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryRoom));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<InventoryRoom> itemsList
      = new List<InventoryRoom>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is InventoryRoom)
      itemsList.Add(deserializedObject as InventoryRoom);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected byte m_areaId;
      
        protected String m_name;
      
        protected int m_inventoryRoomStatusId;
      
      #endregion

      #region Constructors
      public InventoryRoom(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public InventoryRoom(
        int 
          iD,byte 
          areaId,String 
          name,int 
          inventoryRoomStatusId
        ) : this()
        {
        
          m_iD = iD;
        
          m_areaId = areaId;
        
          m_name = name;
        
          m_inventoryRoomStatusId = inventoryRoomStatusId;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public byte AreaId
        {
        get { return m_areaId;}
        set { m_areaId = value; }
        }
      
        [XmlElement]
        public String Name
        {
        get { return m_name;}
        set { m_name = value; }
        }
      
        [XmlElement]
        public int InventoryRoomStatusId
        {
        get { return m_inventoryRoomStatusId;}
        set { m_inventoryRoomStatusId = value; }
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

    