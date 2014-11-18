
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


      public partial class Item : DomainObject, ICloneable
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into Item ( " +
      
        " TaskId, " +
      
        " ItemTypeId, " +
      
        " SerialNumber, " +
      
        " ItemShapeId, " +
      
        " Width, " +
      
        " Height, " +
      
        " Diameter, " +
      
        " IsProtectorApplied, " +
      
        " IsPaddingApplied, " +
      
        " IsMothRepelApplied, " +
      
        " IsRapApplied, " +
      
        " CleanCost, " +
      
        " ProtectorCost, " +
      
        " PaddingCost, " +
      
        " MothRepelCost, " +
      
        " RapCost, " +
      
        " OtherCost, " +
      
        " SubTotalCost, " +
      
        " TaxCost, " +
      
        " TotalCost, " +
      
        " CleaningRate " +
      
      ") Values (" +
      
        " ?TaskId, " +
      
        " ?ItemTypeId, " +
      
        " ?SerialNumber, " +
      
        " ?ItemShapeId, " +
      
        " ?Width, " +
      
        " ?Height, " +
      
        " ?Diameter, " +
      
        " ?IsProtectorApplied, " +
      
        " ?IsPaddingApplied, " +
      
        " ?IsMothRepelApplied, " +
      
        " ?IsRapApplied, " +
      
        " ?CleanCost, " +
      
        " ?ProtectorCost, " +
      
        " ?PaddingCost, " +
      
        " ?MothRepelCost, " +
      
        " ?RapCost, " +
      
        " ?OtherCost, " +
      
        " ?SubTotalCost, " +
      
        " ?TaxCost, " +
      
        " ?TotalCost, " +
      
        " ?CleaningRate " +
      
      ")";

      public static void Insert(Item item, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      
        Database.PutParameter(dbCommand,"?TaskId", item.TaskId);
      
        Database.PutParameter(dbCommand,"?ItemTypeId", item.ItemTypeId);
      
        Database.PutParameter(dbCommand,"?SerialNumber", item.SerialNumber);
      
        Database.PutParameter(dbCommand,"?ItemShapeId", item.ItemShapeId);
      
        Database.PutParameter(dbCommand,"?Width", item.Width);
      
        Database.PutParameter(dbCommand,"?Height", item.Height);
      
        Database.PutParameter(dbCommand,"?Diameter", item.Diameter);
      
        Database.PutParameter(dbCommand,"?IsProtectorApplied", item.IsProtectorApplied);
      
        Database.PutParameter(dbCommand,"?IsPaddingApplied", item.IsPaddingApplied);
      
        Database.PutParameter(dbCommand,"?IsMothRepelApplied", item.IsMothRepelApplied);
      
        Database.PutParameter(dbCommand,"?IsRapApplied", item.IsRapApplied);
      
        Database.PutParameter(dbCommand,"?CleanCost", item.CleanCost);
      
        Database.PutParameter(dbCommand,"?ProtectorCost", item.ProtectorCost);
      
        Database.PutParameter(dbCommand,"?PaddingCost", item.PaddingCost);
      
        Database.PutParameter(dbCommand,"?MothRepelCost", item.MothRepelCost);
      
        Database.PutParameter(dbCommand,"?RapCost", item.RapCost);
      
        Database.PutParameter(dbCommand,"?OtherCost", item.OtherCost);
      
        Database.PutParameter(dbCommand,"?SubTotalCost", item.SubTotalCost);
      
        Database.PutParameter(dbCommand,"?TaxCost", item.TaxCost);
      
        Database.PutParameter(dbCommand,"?TotalCost", item.TotalCost);
      
        Database.PutParameter(dbCommand,"?CleaningRate", item.CleaningRate);
      

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        item.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }

      public static void Insert(Item item)
      {
        Insert(item, null);
      }


      public static void Insert(List<Item>  itemList, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, connection))
      {
      bool parametersAdded = false;

      foreach(Item item in  itemList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"?TaskId", item.TaskId);
      
        Database.PutParameter(dbCommand,"?ItemTypeId", item.ItemTypeId);
      
        Database.PutParameter(dbCommand,"?SerialNumber", item.SerialNumber);
      
        Database.PutParameter(dbCommand,"?ItemShapeId", item.ItemShapeId);
      
        Database.PutParameter(dbCommand,"?Width", item.Width);
      
        Database.PutParameter(dbCommand,"?Height", item.Height);
      
        Database.PutParameter(dbCommand,"?Diameter", item.Diameter);
      
        Database.PutParameter(dbCommand,"?IsProtectorApplied", item.IsProtectorApplied);
      
        Database.PutParameter(dbCommand,"?IsPaddingApplied", item.IsPaddingApplied);
      
        Database.PutParameter(dbCommand,"?IsMothRepelApplied", item.IsMothRepelApplied);
      
        Database.PutParameter(dbCommand,"?IsRapApplied", item.IsRapApplied);
      
        Database.PutParameter(dbCommand,"?CleanCost", item.CleanCost);
      
        Database.PutParameter(dbCommand,"?ProtectorCost", item.ProtectorCost);
      
        Database.PutParameter(dbCommand,"?PaddingCost", item.PaddingCost);
      
        Database.PutParameter(dbCommand,"?MothRepelCost", item.MothRepelCost);
      
        Database.PutParameter(dbCommand,"?RapCost", item.RapCost);
      
        Database.PutParameter(dbCommand,"?OtherCost", item.OtherCost);
      
        Database.PutParameter(dbCommand,"?SubTotalCost", item.SubTotalCost);
      
        Database.PutParameter(dbCommand,"?TaxCost", item.TaxCost);
      
        Database.PutParameter(dbCommand,"?TotalCost", item.TotalCost);
      
        Database.PutParameter(dbCommand,"?CleaningRate", item.CleaningRate);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"?TaskId",item.TaskId);
      
        Database.UpdateParameter(dbCommand,"?ItemTypeId",item.ItemTypeId);
      
        Database.UpdateParameter(dbCommand,"?SerialNumber",item.SerialNumber);
      
        Database.UpdateParameter(dbCommand,"?ItemShapeId",item.ItemShapeId);
      
        Database.UpdateParameter(dbCommand,"?Width",item.Width);
      
        Database.UpdateParameter(dbCommand,"?Height",item.Height);
      
        Database.UpdateParameter(dbCommand,"?Diameter",item.Diameter);
      
        Database.UpdateParameter(dbCommand,"?IsProtectorApplied",item.IsProtectorApplied);
      
        Database.UpdateParameter(dbCommand,"?IsPaddingApplied",item.IsPaddingApplied);
      
        Database.UpdateParameter(dbCommand,"?IsMothRepelApplied",item.IsMothRepelApplied);
      
        Database.UpdateParameter(dbCommand,"?IsRapApplied",item.IsRapApplied);
      
        Database.UpdateParameter(dbCommand,"?CleanCost",item.CleanCost);
      
        Database.UpdateParameter(dbCommand,"?ProtectorCost",item.ProtectorCost);
      
        Database.UpdateParameter(dbCommand,"?PaddingCost",item.PaddingCost);
      
        Database.UpdateParameter(dbCommand,"?MothRepelCost",item.MothRepelCost);
      
        Database.UpdateParameter(dbCommand,"?RapCost",item.RapCost);
      
        Database.UpdateParameter(dbCommand,"?OtherCost",item.OtherCost);
      
        Database.UpdateParameter(dbCommand,"?SubTotalCost",item.SubTotalCost);
      
        Database.UpdateParameter(dbCommand,"?TaxCost",item.TaxCost);
      
        Database.UpdateParameter(dbCommand,"?TotalCost",item.TotalCost);
      
        Database.UpdateParameter(dbCommand,"?CleaningRate",item.CleaningRate);
      
      }

      dbCommand.ExecuteNonQuery();

      
        using (IDbCommand dbIdentityCommand = Database.PrepareCommand("SELECT LAST_INSERT_ID()", dbCommand.Connection, dbCommand.Transaction))
        {
        item.ID = Convert.ToInt32(dbIdentityCommand.ExecuteScalar());
        }
      

      }
      }
      }

      public static void Insert(List<Item>  itemList)
      {
        Insert(itemList, null);
    }

    #endregion

    #region Update


    private const String SqlUpdate = "Update Item Set "
      
        + " TaskId = ?TaskId, "
      
        + " ItemTypeId = ?ItemTypeId, "
      
        + " SerialNumber = ?SerialNumber, "
      
        + " ItemShapeId = ?ItemShapeId, "
      
        + " Width = ?Width, "
      
        + " Height = ?Height, "
      
        + " Diameter = ?Diameter, "
      
        + " IsProtectorApplied = ?IsProtectorApplied, "
      
        + " IsPaddingApplied = ?IsPaddingApplied, "
      
        + " IsMothRepelApplied = ?IsMothRepelApplied, "
      
        + " IsRapApplied = ?IsRapApplied, "
      
        + " CleanCost = ?CleanCost, "
      
        + " ProtectorCost = ?ProtectorCost, "
      
        + " PaddingCost = ?PaddingCost, "
      
        + " MothRepelCost = ?MothRepelCost, "
      
        + " RapCost = ?RapCost, "
      
        + " OtherCost = ?OtherCost, "
      
        + " SubTotalCost = ?SubTotalCost, "
      
        + " TaxCost = ?TaxCost, "
      
        + " TotalCost = ?TotalCost, "
      
        + " CleaningRate = ?CleaningRate "
      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static void Update(Item item, IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID", item.ID);
      
        Database.PutParameter(dbCommand,"?TaskId", item.TaskId);
      
        Database.PutParameter(dbCommand,"?ItemTypeId", item.ItemTypeId);
      
        Database.PutParameter(dbCommand,"?SerialNumber", item.SerialNumber);
      
        Database.PutParameter(dbCommand,"?ItemShapeId", item.ItemShapeId);
      
        Database.PutParameter(dbCommand,"?Width", item.Width);
      
        Database.PutParameter(dbCommand,"?Height", item.Height);
      
        Database.PutParameter(dbCommand,"?Diameter", item.Diameter);
      
        Database.PutParameter(dbCommand,"?IsProtectorApplied", item.IsProtectorApplied);
      
        Database.PutParameter(dbCommand,"?IsPaddingApplied", item.IsPaddingApplied);
      
        Database.PutParameter(dbCommand,"?IsMothRepelApplied", item.IsMothRepelApplied);
      
        Database.PutParameter(dbCommand,"?IsRapApplied", item.IsRapApplied);
      
        Database.PutParameter(dbCommand,"?CleanCost", item.CleanCost);
      
        Database.PutParameter(dbCommand,"?ProtectorCost", item.ProtectorCost);
      
        Database.PutParameter(dbCommand,"?PaddingCost", item.PaddingCost);
      
        Database.PutParameter(dbCommand,"?MothRepelCost", item.MothRepelCost);
      
        Database.PutParameter(dbCommand,"?RapCost", item.RapCost);
      
        Database.PutParameter(dbCommand,"?OtherCost", item.OtherCost);
      
        Database.PutParameter(dbCommand,"?SubTotalCost", item.SubTotalCost);
      
        Database.PutParameter(dbCommand,"?TaxCost", item.TaxCost);
      
        Database.PutParameter(dbCommand,"?TotalCost", item.TotalCost);
      
        Database.PutParameter(dbCommand,"?CleaningRate", item.CleaningRate);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Update(Item item)
      {
        Update(item, null);
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " ID, "
      
        + " TaskId, "
      
        + " ItemTypeId, "
      
        + " SerialNumber, "
      
        + " ItemShapeId, "
      
        + " Width, "
      
        + " Height, "
      
        + " Diameter, "
      
        + " IsProtectorApplied, "
      
        + " IsPaddingApplied, "
      
        + " IsMothRepelApplied, "
      
        + " IsRapApplied, "
      
        + " CleanCost, "
      
        + " ProtectorCost, "
      
        + " PaddingCost, "
      
        + " MothRepelCost, "
      
        + " RapCost, "
      
        + " OtherCost, "
      
        + " SubTotalCost, "
      
        + " TaxCost, "
      
        + " TotalCost, "
      
        + " CleaningRate "
      

      + " From Item "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;

      public static Item FindByPrimaryKey(
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
      throw new DataNotFoundException("Item not found, search by primary key");

      }

      public static Item FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD, null
      );
      }


      #endregion

      #region Exists

      public static bool Exists(Item item, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, connection))
      {
      
        Database.PutParameter(dbCommand,"?ID",item.ID);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      public static bool Exists(Item item)
      {
      return Exists(item, null);
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData(IDbConnection connection)
      {
      String sql = "select * from Item limit 1";

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

      public static Item Load(IDataReader dataReader, int offset)
      {
      Item item = new Item();

      item.ID = dataReader.GetInt32(0 + offset);
          item.TaskId = dataReader.GetInt32(1 + offset);
          item.ItemTypeId = dataReader.GetInt32(2 + offset);
          
            if(!dataReader.IsDBNull(3 + offset))
            item.SerialNumber = dataReader.GetString(3 + offset);
          
            if(!dataReader.IsDBNull(4 + offset))
            item.ItemShapeId = dataReader.GetInt32(4 + offset);
          
            if(!dataReader.IsDBNull(5 + offset))
            item.Width = dataReader.GetDecimal(5 + offset);
          
            if(!dataReader.IsDBNull(6 + offset))
            item.Height = dataReader.GetDecimal(6 + offset);
          
            if(!dataReader.IsDBNull(7 + offset))
            item.Diameter = dataReader.GetDecimal(7 + offset);
          
            if(!dataReader.IsDBNull(8 + offset))
            item.IsProtectorApplied = dataReader.GetBoolean(8 + offset);
          
            if(!dataReader.IsDBNull(9 + offset))
            item.IsPaddingApplied = dataReader.GetBoolean(9 + offset);
          
            if(!dataReader.IsDBNull(10 + offset))
            item.IsMothRepelApplied = dataReader.GetBoolean(10 + offset);
          
            if(!dataReader.IsDBNull(11 + offset))
            item.IsRapApplied = dataReader.GetBoolean(11 + offset);
          
            if(!dataReader.IsDBNull(12 + offset))
            item.CleanCost = dataReader.GetDecimal(12 + offset);
          
            if(!dataReader.IsDBNull(13 + offset))
            item.ProtectorCost = dataReader.GetDecimal(13 + offset);
          
            if(!dataReader.IsDBNull(14 + offset))
            item.PaddingCost = dataReader.GetDecimal(14 + offset);
          
            if(!dataReader.IsDBNull(15 + offset))
            item.MothRepelCost = dataReader.GetDecimal(15 + offset);
          
            if(!dataReader.IsDBNull(16 + offset))
            item.RapCost = dataReader.GetDecimal(16 + offset);
          
            if(!dataReader.IsDBNull(17 + offset))
            item.OtherCost = dataReader.GetDecimal(17 + offset);
          
            if(!dataReader.IsDBNull(18 + offset))
            item.SubTotalCost = dataReader.GetDecimal(18 + offset);
          
            if(!dataReader.IsDBNull(19 + offset))
            item.TaxCost = dataReader.GetDecimal(19 + offset);
          
            if(!dataReader.IsDBNull(20 + offset))
            item.TotalCost = dataReader.GetDecimal(20 + offset);
          
            if(!dataReader.IsDBNull(21 + offset))
            item.CleaningRate = dataReader.GetDecimal(21 + offset);
          

      return item;
      }

      public static Item Load(IDataReader dataReader)
      {
      return Load(dataReader, 0);
      }


      #endregion

      #region Delete
      private const String SqlDelete = "Delete From Item "

      
        + " Where "
        
          + " ID = ?ID "
        
      ;
      public static void Delete(Item item, IDbConnection connection)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, connection))
      {

      
        Database.PutParameter(dbCommand,"?ID", item.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Item item)
      {
        Delete(item, null);
    }

    #endregion

    #region Clear

    private const String SqlDeleteAll = "Delete From Item ";

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
      
        + " TaskId, "
      
        + " ItemTypeId, "
      
        + " SerialNumber, "
      
        + " ItemShapeId, "
      
        + " Width, "
      
        + " Height, "
      
        + " Diameter, "
      
        + " IsProtectorApplied, "
      
        + " IsPaddingApplied, "
      
        + " IsMothRepelApplied, "
      
        + " IsRapApplied, "
      
        + " CleanCost, "
      
        + " ProtectorCost, "
      
        + " PaddingCost, "
      
        + " MothRepelCost, "
      
        + " RapCost, "
      
        + " OtherCost, "
      
        + " SubTotalCost, "
      
        + " TaxCost, "
      
        + " TotalCost, "
      
        + " CleaningRate "
      

      + " From Item ";
      public static List<Item> Find(IDbConnection connection)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, connection))
      {
      List<Item> rv = new List<Item>();

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

      public static List<Item> Find()
      {
      return Find(null);
      }


      #endregion

      #region Import from file

      public static int Import(String xmlFilePath)
      {
      List<Item> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region ValueEquals

      public bool ValueEquals (Item obj)
      {
      if (obj == null)
      return false;

      if (ReferenceEquals(this, obj))
      return true;

      return ID == obj.ID && TaskId == obj.TaskId && ItemTypeId == obj.ItemTypeId && SerialNumber == obj.SerialNumber && ItemShapeId == obj.ItemShapeId && Width == obj.Width && Height == obj.Height && Diameter == obj.Diameter && IsProtectorApplied == obj.IsProtectorApplied && IsPaddingApplied == obj.IsPaddingApplied && IsMothRepelApplied == obj.IsMothRepelApplied && IsRapApplied == obj.IsRapApplied && CleanCost == obj.CleanCost && ProtectorCost == obj.ProtectorCost && PaddingCost == obj.PaddingCost && MothRepelCost == obj.MothRepelCost && RapCost == obj.RapCost && OtherCost == obj.OtherCost && SubTotalCost == obj.SubTotalCost && TaxCost == obj.TaxCost && TotalCost == obj.TotalCost && CleaningRate == obj.CleaningRate;
      }

      #endregion

      #region Export to file
      
      public static int Export(String xmlFilePath)
      {

      List<Item> itemsList = Find();

      if (itemsList.Count == 0)
      return 0;


      XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Item));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");

      foreach(Item item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();

      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<Item>
      Load(String xmlFilePath)
      {
      XmlSerializer xmlSerializer = new XmlSerializer(typeof(Item));
      XmlDocument xmlDocument = new XmlDocument();

      xmlDocument.Load(xmlFilePath);

      List<Item> itemsList
      = new List<Item>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is Item)
      itemsList.Add(deserializedObject as Item);
      }

      return itemsList;
      }

      #endregion

      #endregion


      #region Biz
      

      #region Fields
      
        protected int m_iD;
      
        protected int m_taskId;
      
        protected int m_itemTypeId;
      
        protected String m_serialNumber;
      
        protected int? m_itemShapeId;
      
        protected decimal m_width;
      
        protected decimal m_height;
      
        protected decimal m_diameter;
      
        protected bool m_isProtectorApplied;
      
        protected bool m_isPaddingApplied;
      
        protected bool m_isMothRepelApplied;
      
        protected bool m_isRapApplied;
      
        protected decimal m_cleanCost;
      
        protected decimal m_protectorCost;
      
        protected decimal m_paddingCost;
      
        protected decimal m_mothRepelCost;
      
        protected decimal m_rapCost;
      
        protected decimal m_otherCost;
      
        protected decimal m_subTotalCost;
      
        protected decimal m_taxCost;
      
        protected decimal m_totalCost;
      
        protected decimal m_cleaningRate;
      
      #endregion

      #region Constructors
      public Item(
      int 
          iD
      ) : this()
      {
      
        m_iD = iD;
      
      }

      


        public Item(
        int 
          iD,int 
          taskId,int 
          itemTypeId,String 
          serialNumber,int? 
          itemShapeId,decimal 
          width,decimal 
          height,decimal 
          diameter,bool 
          isProtectorApplied,bool 
          isPaddingApplied,bool 
          isMothRepelApplied,bool 
          isRapApplied,decimal 
          cleanCost,decimal 
          protectorCost,decimal 
          paddingCost,decimal 
          mothRepelCost,decimal 
          rapCost,decimal 
          otherCost,decimal 
          subTotalCost,decimal 
          taxCost,decimal 
          totalCost,decimal 
          cleaningRate
        ) : this()
        {
        
          m_iD = iD;
        
          m_taskId = taskId;
        
          m_itemTypeId = itemTypeId;
        
          m_serialNumber = serialNumber;
        
          m_itemShapeId = itemShapeId;
        
          m_width = width;
        
          m_height = height;
        
          m_diameter = diameter;
        
          m_isProtectorApplied = isProtectorApplied;
        
          m_isPaddingApplied = isPaddingApplied;
        
          m_isMothRepelApplied = isMothRepelApplied;
        
          m_isRapApplied = isRapApplied;
        
          m_cleanCost = cleanCost;
        
          m_protectorCost = protectorCost;
        
          m_paddingCost = paddingCost;
        
          m_mothRepelCost = mothRepelCost;
        
          m_rapCost = rapCost;
        
          m_otherCost = otherCost;
        
          m_subTotalCost = subTotalCost;
        
          m_taxCost = taxCost;
        
          m_totalCost = totalCost;
        
          m_cleaningRate = cleaningRate;
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int TaskId
        {
        get { return m_taskId;}
        set { m_taskId = value; }
        }
      
        [XmlElement]
        public int ItemTypeId
        {
        get { return m_itemTypeId;}
        set { m_itemTypeId = value; }
        }
      
        [XmlElement]
        public String SerialNumber
        {
        get { return m_serialNumber;}
        set { m_serialNumber = value; }
        }
      
        [XmlElement]
        public int? ItemShapeId
        {
        get { return m_itemShapeId;}
        set { m_itemShapeId = value; }
        }
      
        [XmlElement]
        public decimal Width
        {
        get { return m_width;}
        set { m_width = value; }
        }
      
        [XmlElement]
        public decimal Height
        {
        get { return m_height;}
        set { m_height = value; }
        }
      
        [XmlElement]
        public decimal Diameter
        {
        get { return m_diameter;}
        set { m_diameter = value; }
        }
      
        [XmlElement]
        public bool IsProtectorApplied
        {
        get { return m_isProtectorApplied;}
        set { m_isProtectorApplied = value; }
        }
      
        [XmlElement]
        public bool IsPaddingApplied
        {
        get { return m_isPaddingApplied;}
        set { m_isPaddingApplied = value; }
        }
      
        [XmlElement]
        public bool IsMothRepelApplied
        {
        get { return m_isMothRepelApplied;}
        set { m_isMothRepelApplied = value; }
        }
      
        [XmlElement]
        public bool IsRapApplied
        {
        get { return m_isRapApplied;}
        set { m_isRapApplied = value; }
        }
      
        [XmlElement]
        public decimal CleanCost
        {
        get { return m_cleanCost;}
        set { m_cleanCost = value; }
        }
      
        [XmlElement]
        public decimal ProtectorCost
        {
        get { return m_protectorCost;}
        set { m_protectorCost = value; }
        }
      
        [XmlElement]
        public decimal PaddingCost
        {
        get { return m_paddingCost;}
        set { m_paddingCost = value; }
        }
      
        [XmlElement]
        public decimal MothRepelCost
        {
        get { return m_mothRepelCost;}
        set { m_mothRepelCost = value; }
        }
      
        [XmlElement]
        public decimal RapCost
        {
        get { return m_rapCost;}
        set { m_rapCost = value; }
        }
      
        [XmlElement]
        public decimal OtherCost
        {
        get { return m_otherCost;}
        set { m_otherCost = value; }
        }
      
        [XmlElement]
        public decimal SubTotalCost
        {
        get { return m_subTotalCost;}
        set { m_subTotalCost = value; }
        }
      
        [XmlElement]
        public decimal TaxCost
        {
        get { return m_taxCost;}
        set { m_taxCost = value; }
        }
      
        [XmlElement]
        public decimal TotalCost
        {
        get { return m_totalCost;}
        set { m_totalCost = value; }
        }
      
        [XmlElement]
        public decimal CleaningRate
        {
        get { return m_cleaningRate;}
        set { m_cleaningRate = value; }
        }
      

      public static int FieldsCount
      {
      get { return 22; }
      }


      public object Clone()
      {
      return MemberwiseClone();
      }

      }
      #endregion

      }

    