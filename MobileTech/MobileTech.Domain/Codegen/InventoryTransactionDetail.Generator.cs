
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class InventoryTransactionDetail
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into InventoryTransactionDetail ( " +
      
        " SessionId, " +
      
        " BusinessTransactionId, " +
      
        " ItemNumber, " +
      
        " RouteNumber, " +
      
        " LocationId, " +
      
        " StorageTypeId, " +
      
        " InventoryPeriodId, " +
      
        " InventoryTransactionDetailTypeId, " +
      
        " InventoryTransactionTypeId, " +
      
        " DateCreated, " +
      
        " Quantity " +
      
      ") Values (" +
      
        " @SessionId, " +
      
        " @BusinessTransactionId, " +
      
        " @ItemNumber, " +
      
        " @RouteNumber, " +
      
        " @LocationId, " +
      
        " @StorageTypeId, " +
      
        " @InventoryPeriodId, " +
      
        " @InventoryTransactionDetailTypeId, " +
      
        " @InventoryTransactionTypeId, " +
      
        " @DateCreated, " +
      
        " @Quantity " +
      
      ")";

      public static void Insert(InventoryTransactionDetail inventoryTransactionDetail)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", inventoryTransactionDetail.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", inventoryTransactionDetail.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemNumber", inventoryTransactionDetail.ItemNumber);
      
        Database.PutParameter(dbCommand,"@RouteNumber", inventoryTransactionDetail.RouteNumber);
      
        Database.PutParameter(dbCommand,"@LocationId", inventoryTransactionDetail.LocationId);
      
        Database.PutParameter(dbCommand,"@StorageTypeId", inventoryTransactionDetail.StorageTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryPeriodId", inventoryTransactionDetail.InventoryPeriodId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetail.InventoryTransactionDetailTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransactionDetail.InventoryTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@DateCreated", inventoryTransactionDetail.DateCreated);
      
        Database.PutParameter(dbCommand,"@Quantity", inventoryTransactionDetail.Quantity);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<InventoryTransactionDetail>  inventoryTransactionDetailList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(InventoryTransactionDetail inventoryTransactionDetail in  inventoryTransactionDetailList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@SessionId", inventoryTransactionDetail.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", inventoryTransactionDetail.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemNumber", inventoryTransactionDetail.ItemNumber);
      
        Database.PutParameter(dbCommand,"@RouteNumber", inventoryTransactionDetail.RouteNumber);
      
        Database.PutParameter(dbCommand,"@LocationId", inventoryTransactionDetail.LocationId);
      
        Database.PutParameter(dbCommand,"@StorageTypeId", inventoryTransactionDetail.StorageTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryPeriodId", inventoryTransactionDetail.InventoryPeriodId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetail.InventoryTransactionDetailTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransactionDetail.InventoryTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@DateCreated", inventoryTransactionDetail.DateCreated);
      
        Database.PutParameter(dbCommand,"@Quantity", inventoryTransactionDetail.Quantity);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@SessionId",inventoryTransactionDetail.SessionId);
      
        Database.UpdateParameter(dbCommand,"@BusinessTransactionId",inventoryTransactionDetail.BusinessTransactionId);
      
        Database.UpdateParameter(dbCommand,"@ItemNumber",inventoryTransactionDetail.ItemNumber);
      
        Database.UpdateParameter(dbCommand,"@RouteNumber",inventoryTransactionDetail.RouteNumber);
      
        Database.UpdateParameter(dbCommand,"@LocationId",inventoryTransactionDetail.LocationId);
      
        Database.UpdateParameter(dbCommand,"@StorageTypeId",inventoryTransactionDetail.StorageTypeId);
      
        Database.UpdateParameter(dbCommand,"@InventoryPeriodId",inventoryTransactionDetail.InventoryPeriodId);
      
        Database.UpdateParameter(dbCommand,"@InventoryTransactionDetailTypeId",inventoryTransactionDetail.InventoryTransactionDetailTypeId);
      
        Database.UpdateParameter(dbCommand,"@InventoryTransactionTypeId",inventoryTransactionDetail.InventoryTransactionTypeId);
      
        Database.UpdateParameter(dbCommand,"@DateCreated",inventoryTransactionDetail.DateCreated);
      
        Database.UpdateParameter(dbCommand,"@Quantity",inventoryTransactionDetail.Quantity);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update InventoryTransactionDetail Set "
      
        + " InventoryTransactionTypeId = @InventoryTransactionTypeId, "
      
        + " DateCreated = @DateCreated, "
      
        + " Quantity = @Quantity "
      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId and  "
        
          + " ItemNumber = @ItemNumber and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " LocationId = @LocationId and  "
        
          + " StorageTypeId = @StorageTypeId and  "
        
          + " InventoryPeriodId = @InventoryPeriodId and  "
        
          + " InventoryTransactionDetailTypeId = @InventoryTransactionDetailTypeId "
        
      ;

      public static void Update(InventoryTransactionDetail inventoryTransactionDetail)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", inventoryTransactionDetail.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", inventoryTransactionDetail.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemNumber", inventoryTransactionDetail.ItemNumber);
      
        Database.PutParameter(dbCommand,"@RouteNumber", inventoryTransactionDetail.RouteNumber);
      
        Database.PutParameter(dbCommand,"@LocationId", inventoryTransactionDetail.LocationId);
      
        Database.PutParameter(dbCommand,"@StorageTypeId", inventoryTransactionDetail.StorageTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryPeriodId", inventoryTransactionDetail.InventoryPeriodId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetail.InventoryTransactionDetailTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionTypeId", inventoryTransactionDetail.InventoryTransactionTypeId);
      
        Database.PutParameter(dbCommand,"@DateCreated", inventoryTransactionDetail.DateCreated);
      
        Database.PutParameter(dbCommand,"@Quantity", inventoryTransactionDetail.Quantity);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " SessionId, "
      
        + " BusinessTransactionId, "
      
        + " ItemNumber, "
      
        + " RouteNumber, "
      
        + " LocationId, "
      
        + " StorageTypeId, "
      
        + " InventoryPeriodId, "
      
        + " InventoryTransactionDetailTypeId, "
      
        + " InventoryTransactionTypeId, "
      
        + " DateCreated, "
      
        + " Quantity "
      

      + " From InventoryTransactionDetail "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId and  "
        
          + " ItemNumber = @ItemNumber and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " LocationId = @LocationId and  "
        
          + " StorageTypeId = @StorageTypeId and  "
        
          + " InventoryPeriodId = @InventoryPeriodId and  "
        
          + " InventoryTransactionDetailTypeId = @InventoryTransactionDetailTypeId "
        
      ;

      public static InventoryTransactionDetail FindByPrimaryKey(
      long sessionId,int businessTransactionId,String itemNumber,int routeNumber,int locationId,int storageTypeId,int inventoryPeriodId,int inventoryTransactionDetailTypeId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", sessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", businessTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemNumber", itemNumber);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeNumber);
      
        Database.PutParameter(dbCommand,"@LocationId", locationId);
      
        Database.PutParameter(dbCommand,"@StorageTypeId", storageTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryPeriodId", inventoryPeriodId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetailTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("InventoryTransactionDetail not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(InventoryTransactionDetail inventoryTransactionDetail)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId",inventoryTransactionDetail.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId",inventoryTransactionDetail.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemNumber",inventoryTransactionDetail.ItemNumber);
      
        Database.PutParameter(dbCommand,"@RouteNumber",inventoryTransactionDetail.RouteNumber);
      
        Database.PutParameter(dbCommand,"@LocationId",inventoryTransactionDetail.LocationId);
      
        Database.PutParameter(dbCommand,"@StorageTypeId",inventoryTransactionDetail.StorageTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryPeriodId",inventoryTransactionDetail.InventoryPeriodId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId",inventoryTransactionDetail.InventoryTransactionDetailTypeId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      return dataReader.Read();
      }
      }
      }

      #endregion

      #region IsContainsData

      public static bool IsContainsData()
      {
      String sql = "select 1 from InventoryTransactionDetail";

      using(IDbCommand dbCommand = Database.PrepareCommand(sql))
      {
      using (IDataReader reader = dbCommand.ExecuteReader(CommandBehavior.SingleRow))
      {
      return reader.Read();
      }
      }
      }

      #endregion

      #region Load

      public static InventoryTransactionDetail Load(IDataReader dataReader)
      {
      InventoryTransactionDetail inventoryTransactionDetail = new InventoryTransactionDetail();

      inventoryTransactionDetail.SessionId = dataReader.GetInt64(0);
          inventoryTransactionDetail.BusinessTransactionId = dataReader.GetInt32(1);
          inventoryTransactionDetail.ItemNumber = dataReader.GetString(2);
          inventoryTransactionDetail.RouteNumber = dataReader.GetInt32(3);
          inventoryTransactionDetail.LocationId = dataReader.GetInt32(4);
          inventoryTransactionDetail.StorageTypeId = dataReader.GetInt32(5);
          inventoryTransactionDetail.InventoryPeriodId = dataReader.GetInt32(6);
          inventoryTransactionDetail.InventoryTransactionDetailTypeId = dataReader.GetInt16(7);
          
            if(!dataReader.IsDBNull(8))
            inventoryTransactionDetail.InventoryTransactionTypeId = dataReader.GetInt16(8);
          inventoryTransactionDetail.DateCreated = dataReader.GetDateTime(9);
          inventoryTransactionDetail.Quantity = dataReader.GetInt32(10);
          

      return inventoryTransactionDetail;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From InventoryTransactionDetail "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " BusinessTransactionId = @BusinessTransactionId and  "
        
          + " ItemNumber = @ItemNumber and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " LocationId = @LocationId and  "
        
          + " StorageTypeId = @StorageTypeId and  "
        
          + " InventoryPeriodId = @InventoryPeriodId and  "
        
          + " InventoryTransactionDetailTypeId = @InventoryTransactionDetailTypeId "
        
      ;
      public static void Delete(InventoryTransactionDetail inventoryTransactionDetail)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@SessionId", inventoryTransactionDetail.SessionId);
      
        Database.PutParameter(dbCommand,"@BusinessTransactionId", inventoryTransactionDetail.BusinessTransactionId);
      
        Database.PutParameter(dbCommand,"@ItemNumber", inventoryTransactionDetail.ItemNumber);
      
        Database.PutParameter(dbCommand,"@RouteNumber", inventoryTransactionDetail.RouteNumber);
      
        Database.PutParameter(dbCommand,"@LocationId", inventoryTransactionDetail.LocationId);
      
        Database.PutParameter(dbCommand,"@StorageTypeId", inventoryTransactionDetail.StorageTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryPeriodId", inventoryTransactionDetail.InventoryPeriodId);
      
        Database.PutParameter(dbCommand,"@InventoryTransactionDetailTypeId", inventoryTransactionDetail.InventoryTransactionDetailTypeId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From InventoryTransactionDetail ";

      public static void Clear()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDeleteAll))
      {
      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Find
      private const String SqlSelectAll = "Select "

      
        + " SessionId, "
      
        + " BusinessTransactionId, "
      
        + " ItemNumber, "
      
        + " RouteNumber, "
      
        + " LocationId, "
      
        + " StorageTypeId, "
      
        + " InventoryPeriodId, "
      
        + " InventoryTransactionDetailTypeId, "
      
        + " InventoryTransactionTypeId, "
      
        + " DateCreated, "
      
        + " Quantity "
      

      + " From InventoryTransactionDetail ";
      public static List<InventoryTransactionDetail> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<InventoryTransactionDetail> rv = new List<InventoryTransactionDetail>();

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
      #endregion

      #region Import from file
      
      public static int Import(String xmlFilePath)
      {
        List<InventoryTransactionDetail> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<InventoryTransactionDetail> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryTransactionDetail));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(InventoryTransactionDetail item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<InventoryTransactionDetail>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(InventoryTransactionDetail));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<InventoryTransactionDetail> itemsList
      = new List<InventoryTransactionDetail>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is InventoryTransactionDetail)
        itemsList.Add(deserializedObject as InventoryTransactionDetail);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected long m_sessionId;
        
          protected int m_businessTransactionId;
        
          protected String m_itemNumber;
        
          protected int m_routeNumber;
        
          protected int m_locationId;
        
          protected int m_storageTypeId;
        
          protected int m_inventoryPeriodId;
        
          protected int m_inventoryTransactionDetailTypeId;
        
          protected int m_inventoryTransactionTypeId;
        
          protected DateTime m_dateCreated;
        
          protected int m_quantity;
        
        #endregion
        
        #region Constructors
        public InventoryTransactionDetail(
        long 
          sessionId,int 
          businessTransactionId,String 
          itemNumber,int 
          routeNumber,int 
          locationId,int 
          storageTypeId,int 
          inventoryPeriodId,int 
          inventoryTransactionDetailTypeId
         )
        {
        
          m_sessionId = sessionId;
        
          m_businessTransactionId = businessTransactionId;
        
          m_itemNumber = itemNumber;
        
          m_routeNumber = routeNumber;
        
          m_locationId = locationId;
        
          m_storageTypeId = storageTypeId;
        
          m_inventoryPeriodId = inventoryPeriodId;
        
          m_inventoryTransactionDetailTypeId = inventoryTransactionDetailTypeId;
        
        }
        
        


        public InventoryTransactionDetail(
        long 
          sessionId,int 
          businessTransactionId,String 
          itemNumber,int 
          routeNumber,int 
          locationId,int 
          storageTypeId,int 
          inventoryPeriodId,int 
          inventoryTransactionDetailTypeId,int 
          inventoryTransactionTypeId,DateTime 
          dateCreated,int 
          quantity
        )
        {
        
          m_sessionId = sessionId;
        
          m_businessTransactionId = businessTransactionId;
        
          m_itemNumber = itemNumber;
        
          m_routeNumber = routeNumber;
        
          m_locationId = locationId;
        
          m_storageTypeId = storageTypeId;
        
          m_inventoryPeriodId = inventoryPeriodId;
        
          m_inventoryTransactionDetailTypeId = inventoryTransactionDetailTypeId;
        
          m_inventoryTransactionTypeId = inventoryTransactionTypeId;
        
          m_dateCreated = dateCreated;
        
          m_quantity = quantity;
        
          }


        
      #endregion

      
        [XmlElement]
        public long SessionId
        {
          get { return m_sessionId;}
          set { m_sessionId = value; }
        }
      
        [XmlElement]
        public int BusinessTransactionId
        {
          get { return m_businessTransactionId;}
          set { m_businessTransactionId = value; }
        }
      
        [XmlElement]
        public String ItemNumber
        {
          get { return m_itemNumber;}
          set { m_itemNumber = value; }
        }
      
        [XmlElement]
        public int RouteNumber
        {
          get { return m_routeNumber;}
          set { m_routeNumber = value; }
        }
      
        [XmlElement]
        public int LocationId
        {
          get { return m_locationId;}
          set { m_locationId = value; }
        }
      
        [XmlElement]
        public int StorageTypeId
        {
          get { return m_storageTypeId;}
          set { m_storageTypeId = value; }
        }
      
        [XmlElement]
        public int InventoryPeriodId
        {
          get { return m_inventoryPeriodId;}
          set { m_inventoryPeriodId = value; }
        }
      
        [XmlElement]
        public int InventoryTransactionDetailTypeId
        {
          get { return m_inventoryTransactionDetailTypeId;}
          set { m_inventoryTransactionDetailTypeId = value; }
        }
      
        [XmlElement]
        public int InventoryTransactionTypeId
        {
          get { return m_inventoryTransactionTypeId;}
          set { m_inventoryTransactionTypeId = value; }
        }
      
        [XmlElement]
        public DateTime DateCreated
        {
          get { return m_dateCreated;}
          set { m_dateCreated = value; }
        }
      
        [XmlElement]
        public int Quantity
        {
          get { return m_quantity;}
          set { m_quantity = value; }
        }
      
      }
      #endregion
      }

    