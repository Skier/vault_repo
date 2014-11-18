
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


      public partial class Equipment : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Equipment ( " +
      
        " EquipmentTypeId, " +
      
        " EquipmentStatusId, " +
      
        " InventoryRoomId, " +
      
        " VanId, " +
      
        " AddressId, " +
      
        " SerialNumber " +
      
      ") Values (" +
      
        " ?EquipmentTypeId, " +
      
        " ?EquipmentStatusId, " +
      
        " ?InventoryRoomId, " +
      
        " ?VanId, " +
      
        " ?AddressId, " +
      
        " ?SerialNumber " +
      
      ")";

      public static void Insert(Equipment equipment, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?EquipmentTypeId", equipment.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"?EquipmentStatusId", equipment.EquipmentStatusId);
      
        Database.PutParameter(dbCommand,"?InventoryRoomId", equipment.InventoryRoomId);
      
        Database.PutParameter(dbCommand,"?VanId", equipment.VanId);
      
        Database.PutParameter(dbCommand,"?AddressId", equipment.AddressId);
      
        Database.PutParameter(dbCommand,"?SerialNumber", equipment.SerialNumber);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        equipment.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Equipment equipment)
      {
        Insert(equipment, null);
      }


      public static void Insert(List<Equipment>  equipmentList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Equipment equipment in  equipmentList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?EquipmentTypeId", equipment.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"?EquipmentStatusId", equipment.EquipmentStatusId);
      
        Database.PutParameter(dbCommand,"?InventoryRoomId", equipment.InventoryRoomId);
      
        Database.PutParameter(dbCommand,"?VanId", equipment.VanId);
      
        Database.PutParameter(dbCommand,"?AddressId", equipment.AddressId);
      
        Database.PutParameter(dbCommand,"?SerialNumber", equipment.SerialNumber);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?EquipmentTypeId",equipment.EquipmentTypeId);
      
        Database.UpdateParameter(dbCommand,"?EquipmentStatusId",equipment.EquipmentStatusId);
      
        Database.UpdateParameter(dbCommand,"?InventoryRoomId",equipment.InventoryRoomId);
      
        Database.UpdateParameter(dbCommand,"?VanId",equipment.VanId);
      
        Database.UpdateParameter(dbCommand,"?AddressId",equipment.AddressId);
      
        Database.UpdateParameter(dbCommand,"?SerialNumber",equipment.SerialNumber);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        equipment.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Equipment>  equipmentList)
      {
        Insert(equipmentList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Equipment Set "
      
        + " EquipmentTypeId = ?EquipmentTypeId, "
      
        + " EquipmentStatusId = ?EquipmentStatusId, "
      
        + " InventoryRoomId = ?InventoryRoomId, "
      
        + " VanId = ?VanId, "
      
        + " AddressId = ?AddressId, "
      
        + " SerialNumber = ?SerialNumber "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Equipment equipment, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", equipment.ID);
      
        Database.PutParameter(dbCommand,"?EquipmentTypeId", equipment.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"?EquipmentStatusId", equipment.EquipmentStatusId);
      
        Database.PutParameter(dbCommand,"?InventoryRoomId", equipment.InventoryRoomId);
      
        Database.PutParameter(dbCommand,"?VanId", equipment.VanId);
      
        Database.PutParameter(dbCommand,"?AddressId", equipment.AddressId);
      
        Database.PutParameter(dbCommand,"?SerialNumber", equipment.SerialNumber);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Equipment equipment)
      {
        Update(equipment, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " EquipmentTypeId, "
      
        + " EquipmentStatusId, "
      
        + " InventoryRoomId, "
      
        + " VanId, "
      
        + " AddressId, "
      
        + " SerialNumber "
      

      + " From Equipment "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Equipment FindByPrimaryKey(
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
      throw new DataNotFoundException("Equipment not found, search by primary key");

      }

      public static Equipment FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Equipment equipment, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",equipment.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Equipment equipment)
      {
      return Exists(equipment, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Equipment limit 1";

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

      public static Equipment Load(IDataReader dataReader, int offset)
      {
      Equipment equipment = new Equipment();

      equipment.ID = dataReader.GetInt32(0 + offset);
          equipment.EquipmentTypeId = dataReader.GetInt32(1 + offset);
          equipment.EquipmentStatusId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            equipment.InventoryRoomId = dataReader.GetInt32(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            equipment.VanId = dataReader.GetInt32(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            equipment.AddressId = dataReader.GetInt32(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            equipment.SerialNumber = dataReader.GetString(6 + offset);
          

      return equipment;
      }

      public static Equipment Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Equipment "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Equipment equipment, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", equipment.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Equipment equipment)
      {
        Delete(equipment, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Equipment ";

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
      
        + " EquipmentTypeId, "
      
        + " EquipmentStatusId, "
      
        + " InventoryRoomId, "
      
        + " VanId, "
      
        + " AddressId, "
      
        + " SerialNumber "
      

      + " From Equipment ";
      public static List<Equipment> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Equipment> rv = new List<Equipment>();

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

      public static List<Equipment> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Equipment> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Equipment obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && EquipmentTypeId == obj.EquipmentTypeId && EquipmentStatusId == obj.EquipmentStatusId && InventoryRoomId == obj.InventoryRoomId && VanId == obj.VanId && AddressId == obj.AddressId && SerialNumber == obj.SerialNumber;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Equipment> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Equipment));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Equipment item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Equipment>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Equipment));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Equipment> itemsList
      = new List<Equipment>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Equipment)
      itemsList.Add(deserializedObject as Equipment);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_equipmentTypeId;
      
        protected int m_equipmentStatusId;
      
        protected int? m_inventoryRoomId;
      
        protected int? m_vanId;
      
        protected int? m_addressId;
      
        protected String m_serialNumber;
      
      #endregion

      #region Constructors
      public Equipment(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Equipment(
        int 
          iD,int 
          equipmentTypeId,int 
          equipmentStatusId,int? 
          inventoryRoomId,int? 
          vanId,int? 
          addressId,String 
          serialNumber
        ) : this()
        {
        
          m_iD = iD;
        
          m_equipmentTypeId = equipmentTypeId;
        
          m_equipmentStatusId = equipmentStatusId;
        
          m_inventoryRoomId = inventoryRoomId;
        
          m_vanId = vanId;
        
          m_addressId = addressId;
        
          m_serialNumber = serialNumber;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int EquipmentTypeId
        {
        get { return m_equipmentTypeId;}
        set { m_equipmentTypeId = value; }
        }
      
        [XmlElement]
        public int EquipmentStatusId
        {
        get { return m_equipmentStatusId;}
        set { m_equipmentStatusId = value; }
        }
      
        [XmlElement]
        public int? InventoryRoomId
        {
        get { return m_inventoryRoomId;}
        set { m_inventoryRoomId = value; }
        }
      
        [XmlElement]
        public int? VanId
        {
        get { return m_vanId;}
        set { m_vanId = value; }
        }
      
        [XmlElement]
        public int? AddressId
        {
        get { return m_addressId;}
        set { m_addressId = value; }
        }
      
        [XmlElement]
        public String SerialNumber
        {
        get { return m_serialNumber;}
        set { m_serialNumber = value; }
        }
      

      public static int FieldsCount
      {
      get { return 7; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    