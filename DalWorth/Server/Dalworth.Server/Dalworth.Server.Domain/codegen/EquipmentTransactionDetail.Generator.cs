
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


      public partial class EquipmentTransactionDetail : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into EquipmentTransactionDetail ( " +
      
        " EquipmentTransactionId, " +
      
        " EquipmentTypeId, " +
      
        " VanId, " +
      
        " AddressId, " +
      
        " Quantity, " +
      
        " QuantityChange " +
      
      ") Values (" +
      
        " ?EquipmentTransactionId, " +
      
        " ?EquipmentTypeId, " +
      
        " ?VanId, " +
      
        " ?AddressId, " +
      
        " ?Quantity, " +
      
        " ?QuantityChange " +
      
      ")";

      public static void Insert(EquipmentTransactionDetail equipmentTransactionDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?EquipmentTransactionId", equipmentTransactionDetail.EquipmentTransactionId);
      
        Database.PutParameter(dbCommand,"?EquipmentTypeId", equipmentTransactionDetail.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"?VanId", equipmentTransactionDetail.VanId);
      
        Database.PutParameter(dbCommand,"?AddressId", equipmentTransactionDetail.AddressId);
      
        Database.PutParameter(dbCommand,"?Quantity", equipmentTransactionDetail.Quantity);
      
        Database.PutParameter(dbCommand,"?QuantityChange", equipmentTransactionDetail.QuantityChange);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        equipmentTransactionDetail.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(EquipmentTransactionDetail equipmentTransactionDetail)
      {
        Insert(equipmentTransactionDetail, null);
      }


      public static void Insert(List<EquipmentTransactionDetail>  equipmentTransactionDetailList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(EquipmentTransactionDetail equipmentTransactionDetail in  equipmentTransactionDetailList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?EquipmentTransactionId", equipmentTransactionDetail.EquipmentTransactionId);
      
        Database.PutParameter(dbCommand,"?EquipmentTypeId", equipmentTransactionDetail.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"?VanId", equipmentTransactionDetail.VanId);
      
        Database.PutParameter(dbCommand,"?AddressId", equipmentTransactionDetail.AddressId);
      
        Database.PutParameter(dbCommand,"?Quantity", equipmentTransactionDetail.Quantity);
      
        Database.PutParameter(dbCommand,"?QuantityChange", equipmentTransactionDetail.QuantityChange);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?EquipmentTransactionId",equipmentTransactionDetail.EquipmentTransactionId);
      
        Database.UpdateParameter(dbCommand,"?EquipmentTypeId",equipmentTransactionDetail.EquipmentTypeId);
      
        Database.UpdateParameter(dbCommand,"?VanId",equipmentTransactionDetail.VanId);
      
        Database.UpdateParameter(dbCommand,"?AddressId",equipmentTransactionDetail.AddressId);
      
        Database.UpdateParameter(dbCommand,"?Quantity",equipmentTransactionDetail.Quantity);
      
        Database.UpdateParameter(dbCommand,"?QuantityChange",equipmentTransactionDetail.QuantityChange);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        equipmentTransactionDetail.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<EquipmentTransactionDetail>  equipmentTransactionDetailList)
      {
        Insert(equipmentTransactionDetailList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update EquipmentTransactionDetail Set "
      
        + " EquipmentTransactionId = ?EquipmentTransactionId, "
      
        + " EquipmentTypeId = ?EquipmentTypeId, "
      
        + " VanId = ?VanId, "
      
        + " AddressId = ?AddressId, "
      
        + " Quantity = ?Quantity, "
      
        + " QuantityChange = ?QuantityChange "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(EquipmentTransactionDetail equipmentTransactionDetail, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", equipmentTransactionDetail.ID);
      
        Database.PutParameter(dbCommand,"?EquipmentTransactionId", equipmentTransactionDetail.EquipmentTransactionId);
      
        Database.PutParameter(dbCommand,"?EquipmentTypeId", equipmentTransactionDetail.EquipmentTypeId);
      
        Database.PutParameter(dbCommand,"?VanId", equipmentTransactionDetail.VanId);
      
        Database.PutParameter(dbCommand,"?AddressId", equipmentTransactionDetail.AddressId);
      
        Database.PutParameter(dbCommand,"?Quantity", equipmentTransactionDetail.Quantity);
      
        Database.PutParameter(dbCommand,"?QuantityChange", equipmentTransactionDetail.QuantityChange);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(EquipmentTransactionDetail equipmentTransactionDetail)
      {
        Update(equipmentTransactionDetail, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " EquipmentTransactionId, "
      
        + " EquipmentTypeId, "
      
        + " VanId, "
      
        + " AddressId, "
      
        + " Quantity, "
      
        + " QuantityChange "
      

      + " From EquipmentTransactionDetail "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static EquipmentTransactionDetail FindByPrimaryKey(
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
      throw new DataNotFoundException("EquipmentTransactionDetail not found, search by primary key");

      }

      public static EquipmentTransactionDetail FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(EquipmentTransactionDetail equipmentTransactionDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",equipmentTransactionDetail.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(EquipmentTransactionDetail equipmentTransactionDetail)
      {
      return Exists(equipmentTransactionDetail, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from EquipmentTransactionDetail limit 1";

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

      public static EquipmentTransactionDetail Load(IDataReader dataReader, int offset)
      {
      EquipmentTransactionDetail equipmentTransactionDetail = new EquipmentTransactionDetail();

      equipmentTransactionDetail.ID = dataReader.GetInt32(0 + offset);
          equipmentTransactionDetail.EquipmentTransactionId = dataReader.GetInt32(1 + offset);
          equipmentTransactionDetail.EquipmentTypeId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            equipmentTransactionDetail.VanId = dataReader.GetInt32(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            equipmentTransactionDetail.AddressId = dataReader.GetInt32(4 + offset);
          equipmentTransactionDetail.Quantity = dataReader.GetInt32(5 + offset);
          equipmentTransactionDetail.QuantityChange = dataReader.GetInt32(6 + offset);
          

      return equipmentTransactionDetail;
      }

      public static EquipmentTransactionDetail Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From EquipmentTransactionDetail "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(EquipmentTransactionDetail equipmentTransactionDetail, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", equipmentTransactionDetail.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(EquipmentTransactionDetail equipmentTransactionDetail)
      {
        Delete(equipmentTransactionDetail, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From EquipmentTransactionDetail ";

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
      
        + " EquipmentTransactionId, "
      
        + " EquipmentTypeId, "
      
        + " VanId, "
      
        + " AddressId, "
      
        + " Quantity, "
      
        + " QuantityChange "
      

      + " From EquipmentTransactionDetail ";
      public static List<EquipmentTransactionDetail> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<EquipmentTransactionDetail> rv = new List<EquipmentTransactionDetail>();

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

      public static List<EquipmentTransactionDetail> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<EquipmentTransactionDetail> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

      List<EquipmentTransactionDetail> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(EquipmentTransactionDetail));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(EquipmentTransactionDetail item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<EquipmentTransactionDetail>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(EquipmentTransactionDetail));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<EquipmentTransactionDetail> itemsList
      = new List<EquipmentTransactionDetail>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is EquipmentTransactionDetail)
      itemsList.Add(deserializedObject as EquipmentTransactionDetail);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_equipmentTransactionId;
      
        protected int m_equipmentTypeId;
      
        protected int? m_vanId;
      
        protected int? m_addressId;
      
        protected int m_quantity;
      
        protected int m_quantityChange;
      
      #endregion

      #region Constructors
      public EquipmentTransactionDetail(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public EquipmentTransactionDetail(
        int 
          iD,int 
          equipmentTransactionId,int 
          equipmentTypeId,int? 
          vanId,int? 
          addressId,int 
          quantity,int 
          quantityChange
        ) : this()
        {
        
          m_iD = iD;
        
          m_equipmentTransactionId = equipmentTransactionId;
        
          m_equipmentTypeId = equipmentTypeId;
        
          m_vanId = vanId;
        
          m_addressId = addressId;
        
          m_quantity = quantity;
        
          m_quantityChange = quantityChange;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int EquipmentTransactionId
        {
        get { return m_equipmentTransactionId;}
        set { m_equipmentTransactionId = value; }
        }
      
        [XmlElement]
        public int EquipmentTypeId
        {
        get { return m_equipmentTypeId;}
        set { m_equipmentTypeId = value; }
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
        public int Quantity
        {
        get { return m_quantity;}
        set { m_quantity = value; }
        }
      
        [XmlElement]
        public int QuantityChange
        {
        get { return m_quantityChange;}
        set { m_quantityChange = value; }
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

    