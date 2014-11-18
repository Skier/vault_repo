
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


      public partial class Item : DomainObject
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into [Item] ( " +
      
        " ID, " +
      
        " ServerId, " +
      
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
      
        " TotalCost " +
      
      ") Values (" +
      
        " @ID, " +
      
        " @ServerId, " +
      
        " @ItemTypeId, " +
      
        " @SerialNumber, " +
      
        " @ItemShapeId, " +
      
        " @Width, " +
      
        " @Height, " +
      
        " @Diameter, " +
      
        " @IsProtectorApplied, " +
      
        " @IsPaddingApplied, " +
      
        " @IsMothRepelApplied, " +
      
        " @IsRapApplied, " +
      
        " @CleanCost, " +
      
        " @ProtectorCost, " +
      
        " @PaddingCost, " +
      
        " @MothRepelCost, " +
      
        " @RapCost, " +
      
        " @OtherCost, " +
      
        " @SubTotalCost, " +
      
        " @TaxCost, " +
      
        " @TotalCost " +
      
      ")";

      public static void Insert(Item item, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", item.ID);
      
        Database.PutParameter(dbCommand,"@ServerId", item.ServerId);
      
        Database.PutParameter(dbCommand,"@ItemTypeId", item.ItemTypeId);
      
        Database.PutParameter(dbCommand,"@SerialNumber", item.SerialNumber);
      
        Database.PutParameter(dbCommand,"@ItemShapeId", item.ItemShapeId);
      
        Database.PutParameter(dbCommand,"@Width", item.Width);
      
        Database.PutParameter(dbCommand,"@Height", item.Height);
      
        Database.PutParameter(dbCommand,"@Diameter", item.Diameter);
      
        Database.PutParameter(dbCommand,"@IsProtectorApplied", item.IsProtectorApplied);
      
        Database.PutParameter(dbCommand,"@IsPaddingApplied", item.IsPaddingApplied);
      
        Database.PutParameter(dbCommand,"@IsMothRepelApplied", item.IsMothRepelApplied);
      
        Database.PutParameter(dbCommand,"@IsRapApplied", item.IsRapApplied);
      
        Database.PutParameter(dbCommand,"@CleanCost", item.CleanCost);
      
        Database.PutParameter(dbCommand,"@ProtectorCost", item.ProtectorCost);
      
        Database.PutParameter(dbCommand,"@PaddingCost", item.PaddingCost);
      
        Database.PutParameter(dbCommand,"@MothRepelCost", item.MothRepelCost);
      
        Database.PutParameter(dbCommand,"@RapCost", item.RapCost);
      
        Database.PutParameter(dbCommand,"@OtherCost", item.OtherCost);
      
        Database.PutParameter(dbCommand,"@SubTotalCost", item.SubTotalCost);
      
        Database.PutParameter(dbCommand,"@TaxCost", item.TaxCost);
      
        Database.PutParameter(dbCommand,"@TotalCost", item.TotalCost);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(Item item)
      {
        Insert(item, null);
      }

      public static void Insert(List<Item>  itemList, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert, transaction))
      {
      bool parametersAdded = false;

      foreach(Item item in  itemList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@ID", item.ID);
      
        Database.PutParameter(dbCommand,"@ServerId", item.ServerId);
      
        Database.PutParameter(dbCommand,"@ItemTypeId", item.ItemTypeId);
      
        Database.PutParameter(dbCommand,"@SerialNumber", item.SerialNumber);
      
        Database.PutParameter(dbCommand,"@ItemShapeId", item.ItemShapeId);
      
        Database.PutParameter(dbCommand,"@Width", item.Width);
      
        Database.PutParameter(dbCommand,"@Height", item.Height);
      
        Database.PutParameter(dbCommand,"@Diameter", item.Diameter);
      
        Database.PutParameter(dbCommand,"@IsProtectorApplied", item.IsProtectorApplied);
      
        Database.PutParameter(dbCommand,"@IsPaddingApplied", item.IsPaddingApplied);
      
        Database.PutParameter(dbCommand,"@IsMothRepelApplied", item.IsMothRepelApplied);
      
        Database.PutParameter(dbCommand,"@IsRapApplied", item.IsRapApplied);
      
        Database.PutParameter(dbCommand,"@CleanCost", item.CleanCost);
      
        Database.PutParameter(dbCommand,"@ProtectorCost", item.ProtectorCost);
      
        Database.PutParameter(dbCommand,"@PaddingCost", item.PaddingCost);
      
        Database.PutParameter(dbCommand,"@MothRepelCost", item.MothRepelCost);
      
        Database.PutParameter(dbCommand,"@RapCost", item.RapCost);
      
        Database.PutParameter(dbCommand,"@OtherCost", item.OtherCost);
      
        Database.PutParameter(dbCommand,"@SubTotalCost", item.SubTotalCost);
      
        Database.PutParameter(dbCommand,"@TaxCost", item.TaxCost);
      
        Database.PutParameter(dbCommand,"@TotalCost", item.TotalCost);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@ID",item.ID);
      
        Database.UpdateParameter(dbCommand,"@ServerId",item.ServerId);
      
        Database.UpdateParameter(dbCommand,"@ItemTypeId",item.ItemTypeId);
      
        Database.UpdateParameter(dbCommand,"@SerialNumber",item.SerialNumber);
      
        Database.UpdateParameter(dbCommand,"@ItemShapeId",item.ItemShapeId);
      
        Database.UpdateParameter(dbCommand,"@Width",item.Width);
      
        Database.UpdateParameter(dbCommand,"@Height",item.Height);
      
        Database.UpdateParameter(dbCommand,"@Diameter",item.Diameter);
      
        Database.UpdateParameter(dbCommand,"@IsProtectorApplied",item.IsProtectorApplied);
      
        Database.UpdateParameter(dbCommand,"@IsPaddingApplied",item.IsPaddingApplied);
      
        Database.UpdateParameter(dbCommand,"@IsMothRepelApplied",item.IsMothRepelApplied);
      
        Database.UpdateParameter(dbCommand,"@IsRapApplied",item.IsRapApplied);
      
        Database.UpdateParameter(dbCommand,"@CleanCost",item.CleanCost);
      
        Database.UpdateParameter(dbCommand,"@ProtectorCost",item.ProtectorCost);
      
        Database.UpdateParameter(dbCommand,"@PaddingCost",item.PaddingCost);
      
        Database.UpdateParameter(dbCommand,"@MothRepelCost",item.MothRepelCost);
      
        Database.UpdateParameter(dbCommand,"@RapCost",item.RapCost);
      
        Database.UpdateParameter(dbCommand,"@OtherCost",item.OtherCost);
      
        Database.UpdateParameter(dbCommand,"@SubTotalCost",item.SubTotalCost);
      
        Database.UpdateParameter(dbCommand,"@TaxCost",item.TaxCost);
      
        Database.UpdateParameter(dbCommand,"@TotalCost",item.TotalCost);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      public static void Insert(List<Item>  itemList)
      {
      Insert(itemList, null);
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update [Item] Set "
      
        + " ServerId = @ServerId, "
      
        + " ItemTypeId = @ItemTypeId, "
      
        + " SerialNumber = @SerialNumber, "
      
        + " ItemShapeId = @ItemShapeId, "
      
        + " Width = @Width, "
      
        + " Height = @Height, "
      
        + " Diameter = @Diameter, "
      
        + " IsProtectorApplied = @IsProtectorApplied, "
      
        + " IsPaddingApplied = @IsPaddingApplied, "
      
        + " IsMothRepelApplied = @IsMothRepelApplied, "
      
        + " IsRapApplied = @IsRapApplied, "
      
        + " CleanCost = @CleanCost, "
      
        + " ProtectorCost = @ProtectorCost, "
      
        + " PaddingCost = @PaddingCost, "
      
        + " MothRepelCost = @MothRepelCost, "
      
        + " RapCost = @RapCost, "
      
        + " OtherCost = @OtherCost, "
      
        + " SubTotalCost = @SubTotalCost, "
      
        + " TaxCost = @TaxCost, "
      
        + " TotalCost = @TotalCost "
      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static void Update(Item item, IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID", item.ID);
      
        Database.PutParameter(dbCommand,"@ServerId", item.ServerId);
      
        Database.PutParameter(dbCommand,"@ItemTypeId", item.ItemTypeId);
      
        Database.PutParameter(dbCommand,"@SerialNumber", item.SerialNumber);
      
        Database.PutParameter(dbCommand,"@ItemShapeId", item.ItemShapeId);
      
        Database.PutParameter(dbCommand,"@Width", item.Width);
      
        Database.PutParameter(dbCommand,"@Height", item.Height);
      
        Database.PutParameter(dbCommand,"@Diameter", item.Diameter);
      
        Database.PutParameter(dbCommand,"@IsProtectorApplied", item.IsProtectorApplied);
      
        Database.PutParameter(dbCommand,"@IsPaddingApplied", item.IsPaddingApplied);
      
        Database.PutParameter(dbCommand,"@IsMothRepelApplied", item.IsMothRepelApplied);
      
        Database.PutParameter(dbCommand,"@IsRapApplied", item.IsRapApplied);
      
        Database.PutParameter(dbCommand,"@CleanCost", item.CleanCost);
      
        Database.PutParameter(dbCommand,"@ProtectorCost", item.ProtectorCost);
      
        Database.PutParameter(dbCommand,"@PaddingCost", item.PaddingCost);
      
        Database.PutParameter(dbCommand,"@MothRepelCost", item.MothRepelCost);
      
        Database.PutParameter(dbCommand,"@RapCost", item.RapCost);
      
        Database.PutParameter(dbCommand,"@OtherCost", item.OtherCost);
      
        Database.PutParameter(dbCommand,"@SubTotalCost", item.SubTotalCost);
      
        Database.PutParameter(dbCommand,"@TaxCost", item.TaxCost);
      
        Database.PutParameter(dbCommand,"@TotalCost", item.TotalCost);
      

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
      
        + " ServerId, "
      
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
      
        + " TotalCost "
      

      + " From [Item] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;

      public static Item FindByPrimaryKey(
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
      throw new DataNotFoundException("Item not found, search by primary key");

      }

      public static Item FindByPrimaryKey(
      int iD
      )
      {
      return FindByPrimaryKey(
      iD
      ,null);
      }

      #endregion

      #region Exists

      public static bool Exists(Item item, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk, transaction))
      {
      
        Database.PutParameter(dbCommand,"@ID",item.ID);
      

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

      public static bool IsContainsData(IDbTransaction transaction)
      {
      String sql = "select 1 from Item";

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

      public static Item Load(IDataReader dataReader)
      {
      Item item = new Item();

      item.ID = dataReader.GetInt32(0);
          
            if(!dataReader.IsDBNull(1))
            item.ServerId = dataReader.GetInt32(1);
          item.ItemTypeId = dataReader.GetInt32(2);
          
            if(!dataReader.IsDBNull(3))
            item.SerialNumber = dataReader.GetString(3);
          
            if(!dataReader.IsDBNull(4))
            item.ItemShapeId = dataReader.GetInt32(4);
          
            if(!dataReader.IsDBNull(5))
            item.Width = dataReader.GetDecimal(5);
          
            if(!dataReader.IsDBNull(6))
            item.Height = dataReader.GetDecimal(6);
          
            if(!dataReader.IsDBNull(7))
            item.Diameter = dataReader.GetDecimal(7);
          
            if(!dataReader.IsDBNull(8))
            item.IsProtectorApplied = dataReader.GetBoolean(8);
          
            if(!dataReader.IsDBNull(9))
            item.IsPaddingApplied = dataReader.GetBoolean(9);
          
            if(!dataReader.IsDBNull(10))
            item.IsMothRepelApplied = dataReader.GetBoolean(10);
          
            if(!dataReader.IsDBNull(11))
            item.IsRapApplied = dataReader.GetBoolean(11);
          
            if(!dataReader.IsDBNull(12))
            item.CleanCost = dataReader.GetDecimal(12);
          
            if(!dataReader.IsDBNull(13))
            item.ProtectorCost = dataReader.GetDecimal(13);
          
            if(!dataReader.IsDBNull(14))
            item.PaddingCost = dataReader.GetDecimal(14);
          
            if(!dataReader.IsDBNull(15))
            item.MothRepelCost = dataReader.GetDecimal(15);
          
            if(!dataReader.IsDBNull(16))
            item.RapCost = dataReader.GetDecimal(16);
          
            if(!dataReader.IsDBNull(17))
            item.OtherCost = dataReader.GetDecimal(17);
          
            if(!dataReader.IsDBNull(18))
            item.SubTotalCost = dataReader.GetDecimal(18);
          
            if(!dataReader.IsDBNull(19))
            item.TaxCost = dataReader.GetDecimal(19);
          
            if(!dataReader.IsDBNull(20))
            item.TotalCost = dataReader.GetDecimal(20);
          

      return item;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From [Item] "

      
        + " Where "
        
          + " ID = @ID "
        
      ;
      public static void Delete(Item item, IDbTransaction transaction)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete, transaction))
      {

      
        Database.PutParameter(dbCommand,"@ID", item.ID);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Delete(Item item)
      {
      Delete(item, null);
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From [Item] ";

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
      
        + " ServerId, "
      
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
      
        + " TotalCost "
      

      + " From [Item] ";
      public static List<Item> Find(IDbTransaction transaction)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll, transaction))
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
      
        protected int? m_serverId;
      
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
      
      #endregion

      #region Constructors
      public Item(
      int 
          iD
      )
      {
      
        m_iD = iD;
      
      }

      


        public Item(
        int 
          iD,int? 
          serverId,int 
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
          totalCost
        )
        {
        
          m_iD = iD;
        
          m_serverId = serverId;
        
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
        
        }


      
      #endregion

      
        [XmlElement]
        public int ID
        {
        get { return m_iD;}
        set { m_iD = value; }
        }
      
        [XmlElement]
        public int? ServerId
        {
        get { return m_serverId;}
        set { m_serverId = value; }
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
      
      }
      #endregion
      }

    