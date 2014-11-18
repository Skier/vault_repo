
    using System;
    using System.Data;
    using System.Collections.Generic;
    using MobileTech.Data;
    using System.Xml;
    using System.Xml.Serialization;
    using System.Text;
  
      namespace MobileTech.Domain
      {


      public partial class RouteInventory
      {

      #region Store


      #region Insert

      private const String SqlInsert = "Insert Into RouteInventory ( " +
      
        " SessionId, " +
      
        " LocationId, " +
      
        " RouteNumber, " +
      
        " ItemNumber, " +
      
        " StorageTypeId, " +
      
        " InventoryPeriodId, " +
      
        " ClosedIndicator, " +
      
        " StartQty, " +
      
        " LoadQty, " +
      
        " LoadAdjustmentQty, " +
      
        " TransInQty, " +
      
        " TransOutQty, " +
      
        " ReturnQty, " +
      
        " SaleQty, " +
      
        " DmgStartQty, " +
      
        " DmgLoadQty, " +
      
        " DmgLoadAdjustmentQty, " +
      
        " DmgTransInQty, " +
      
        " DmgTransOutQty, " +
      
        " DmgReturnQty, " +
      
        " DmgSaleQty, " +
      
        " DmgUnloadQty, " +
      
        " RouteDmgQty, " +
      
        " UnloadQty, " +
      
        " EndQty, " +
      
        " DmgEndQty " +
      
      ") Values (" +
      
        " @SessionId, " +
      
        " @LocationId, " +
      
        " @RouteNumber, " +
      
        " @ItemNumber, " +
      
        " @StorageTypeId, " +
      
        " @InventoryPeriodId, " +
      
        " @ClosedIndicator, " +
      
        " @StartQty, " +
      
        " @LoadQty, " +
      
        " @LoadAdjustmentQty, " +
      
        " @TransInQty, " +
      
        " @TransOutQty, " +
      
        " @ReturnQty, " +
      
        " @SaleQty, " +
      
        " @DmgStartQty, " +
      
        " @DmgLoadQty, " +
      
        " @DmgLoadAdjustmentQty, " +
      
        " @DmgTransInQty, " +
      
        " @DmgTransOutQty, " +
      
        " @DmgReturnQty, " +
      
        " @DmgSaleQty, " +
      
        " @DmgUnloadQty, " +
      
        " @RouteDmgQty, " +
      
        " @UnloadQty, " +
      
        " @EndQty, " +
      
        " @DmgEndQty " +
      
      ")";

      public static void Insert(RouteInventory routeInventory)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", routeInventory.SessionId);
      
        Database.PutParameter(dbCommand,"@LocationId", routeInventory.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeInventory.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", routeInventory.ItemNumber);
      
        Database.PutParameter(dbCommand,"@StorageTypeId", routeInventory.StorageTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryPeriodId", routeInventory.InventoryPeriodId);
      
        Database.PutParameter(dbCommand,"@ClosedIndicator", routeInventory.ClosedIndicator);
      
        Database.PutParameter(dbCommand,"@StartQty", routeInventory.StartQty);
      
        Database.PutParameter(dbCommand,"@LoadQty", routeInventory.LoadQty);
      
        Database.PutParameter(dbCommand,"@LoadAdjustmentQty", routeInventory.LoadAdjustmentQty);
      
        Database.PutParameter(dbCommand,"@TransInQty", routeInventory.TransInQty);
      
        Database.PutParameter(dbCommand,"@TransOutQty", routeInventory.TransOutQty);
      
        Database.PutParameter(dbCommand,"@ReturnQty", routeInventory.ReturnQty);
      
        Database.PutParameter(dbCommand,"@SaleQty", routeInventory.SaleQty);
      
        Database.PutParameter(dbCommand,"@DmgStartQty", routeInventory.DmgStartQty);
      
        Database.PutParameter(dbCommand,"@DmgLoadQty", routeInventory.DmgLoadQty);
      
        Database.PutParameter(dbCommand,"@DmgLoadAdjustmentQty", routeInventory.DmgLoadAdjustmentQty);
      
        Database.PutParameter(dbCommand,"@DmgTransInQty", routeInventory.DmgTransInQty);
      
        Database.PutParameter(dbCommand,"@DmgTransOutQty", routeInventory.DmgTransOutQty);
      
        Database.PutParameter(dbCommand,"@DmgReturnQty", routeInventory.DmgReturnQty);
      
        Database.PutParameter(dbCommand,"@DmgSaleQty", routeInventory.DmgSaleQty);
      
        Database.PutParameter(dbCommand,"@DmgUnloadQty", routeInventory.DmgUnloadQty);
      
        Database.PutParameter(dbCommand,"@RouteDmgQty", routeInventory.RouteDmgQty);
      
        Database.PutParameter(dbCommand,"@UnloadQty", routeInventory.UnloadQty);
      
        Database.PutParameter(dbCommand,"@EndQty", routeInventory.EndQty);
      
        Database.PutParameter(dbCommand,"@DmgEndQty", routeInventory.DmgEndQty);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      public static void Insert(List<RouteInventory>  routeInventoryList)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlInsert))
      {
      bool parametersAdded = false;

      foreach(RouteInventory routeInventory in  routeInventoryList)
      {
      if(!parametersAdded)
      {
      
        Database.PutParameter(dbCommand,"@SessionId", routeInventory.SessionId);
      
        Database.PutParameter(dbCommand,"@LocationId", routeInventory.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeInventory.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", routeInventory.ItemNumber);
      
        Database.PutParameter(dbCommand,"@StorageTypeId", routeInventory.StorageTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryPeriodId", routeInventory.InventoryPeriodId);
      
        Database.PutParameter(dbCommand,"@ClosedIndicator", routeInventory.ClosedIndicator);
      
        Database.PutParameter(dbCommand,"@StartQty", routeInventory.StartQty);
      
        Database.PutParameter(dbCommand,"@LoadQty", routeInventory.LoadQty);
      
        Database.PutParameter(dbCommand,"@LoadAdjustmentQty", routeInventory.LoadAdjustmentQty);
      
        Database.PutParameter(dbCommand,"@TransInQty", routeInventory.TransInQty);
      
        Database.PutParameter(dbCommand,"@TransOutQty", routeInventory.TransOutQty);
      
        Database.PutParameter(dbCommand,"@ReturnQty", routeInventory.ReturnQty);
      
        Database.PutParameter(dbCommand,"@SaleQty", routeInventory.SaleQty);
      
        Database.PutParameter(dbCommand,"@DmgStartQty", routeInventory.DmgStartQty);
      
        Database.PutParameter(dbCommand,"@DmgLoadQty", routeInventory.DmgLoadQty);
      
        Database.PutParameter(dbCommand,"@DmgLoadAdjustmentQty", routeInventory.DmgLoadAdjustmentQty);
      
        Database.PutParameter(dbCommand,"@DmgTransInQty", routeInventory.DmgTransInQty);
      
        Database.PutParameter(dbCommand,"@DmgTransOutQty", routeInventory.DmgTransOutQty);
      
        Database.PutParameter(dbCommand,"@DmgReturnQty", routeInventory.DmgReturnQty);
      
        Database.PutParameter(dbCommand,"@DmgSaleQty", routeInventory.DmgSaleQty);
      
        Database.PutParameter(dbCommand,"@DmgUnloadQty", routeInventory.DmgUnloadQty);
      
        Database.PutParameter(dbCommand,"@RouteDmgQty", routeInventory.RouteDmgQty);
      
        Database.PutParameter(dbCommand,"@UnloadQty", routeInventory.UnloadQty);
      
        Database.PutParameter(dbCommand,"@EndQty", routeInventory.EndQty);
      
        Database.PutParameter(dbCommand,"@DmgEndQty", routeInventory.DmgEndQty);
      
      parametersAdded = true;
      }
      else
      {

      
        Database.UpdateParameter(dbCommand,"@SessionId",routeInventory.SessionId);
      
        Database.UpdateParameter(dbCommand,"@LocationId",routeInventory.LocationId);
      
        Database.UpdateParameter(dbCommand,"@RouteNumber",routeInventory.RouteNumber);
      
        Database.UpdateParameter(dbCommand,"@ItemNumber",routeInventory.ItemNumber);
      
        Database.UpdateParameter(dbCommand,"@StorageTypeId",routeInventory.StorageTypeId);
      
        Database.UpdateParameter(dbCommand,"@InventoryPeriodId",routeInventory.InventoryPeriodId);
      
        Database.UpdateParameter(dbCommand,"@ClosedIndicator",routeInventory.ClosedIndicator);
      
        Database.UpdateParameter(dbCommand,"@StartQty",routeInventory.StartQty);
      
        Database.UpdateParameter(dbCommand,"@LoadQty",routeInventory.LoadQty);
      
        Database.UpdateParameter(dbCommand,"@LoadAdjustmentQty",routeInventory.LoadAdjustmentQty);
      
        Database.UpdateParameter(dbCommand,"@TransInQty",routeInventory.TransInQty);
      
        Database.UpdateParameter(dbCommand,"@TransOutQty",routeInventory.TransOutQty);
      
        Database.UpdateParameter(dbCommand,"@ReturnQty",routeInventory.ReturnQty);
      
        Database.UpdateParameter(dbCommand,"@SaleQty",routeInventory.SaleQty);
      
        Database.UpdateParameter(dbCommand,"@DmgStartQty",routeInventory.DmgStartQty);
      
        Database.UpdateParameter(dbCommand,"@DmgLoadQty",routeInventory.DmgLoadQty);
      
        Database.UpdateParameter(dbCommand,"@DmgLoadAdjustmentQty",routeInventory.DmgLoadAdjustmentQty);
      
        Database.UpdateParameter(dbCommand,"@DmgTransInQty",routeInventory.DmgTransInQty);
      
        Database.UpdateParameter(dbCommand,"@DmgTransOutQty",routeInventory.DmgTransOutQty);
      
        Database.UpdateParameter(dbCommand,"@DmgReturnQty",routeInventory.DmgReturnQty);
      
        Database.UpdateParameter(dbCommand,"@DmgSaleQty",routeInventory.DmgSaleQty);
      
        Database.UpdateParameter(dbCommand,"@DmgUnloadQty",routeInventory.DmgUnloadQty);
      
        Database.UpdateParameter(dbCommand,"@RouteDmgQty",routeInventory.RouteDmgQty);
      
        Database.UpdateParameter(dbCommand,"@UnloadQty",routeInventory.UnloadQty);
      
        Database.UpdateParameter(dbCommand,"@EndQty",routeInventory.EndQty);
      
        Database.UpdateParameter(dbCommand,"@DmgEndQty",routeInventory.DmgEndQty);
      
      }

      dbCommand.ExecuteNonQuery();
      }
      }
      }

      #endregion

      #region Update


      private const String SqlUpdate = "Update RouteInventory Set "
      
        + " ClosedIndicator = @ClosedIndicator, "
      
        + " StartQty = @StartQty, "
      
        + " LoadQty = @LoadQty, "
      
        + " LoadAdjustmentQty = @LoadAdjustmentQty, "
      
        + " TransInQty = @TransInQty, "
      
        + " TransOutQty = @TransOutQty, "
      
        + " ReturnQty = @ReturnQty, "
      
        + " SaleQty = @SaleQty, "
      
        + " DmgStartQty = @DmgStartQty, "
      
        + " DmgLoadQty = @DmgLoadQty, "
      
        + " DmgLoadAdjustmentQty = @DmgLoadAdjustmentQty, "
      
        + " DmgTransInQty = @DmgTransInQty, "
      
        + " DmgTransOutQty = @DmgTransOutQty, "
      
        + " DmgReturnQty = @DmgReturnQty, "
      
        + " DmgSaleQty = @DmgSaleQty, "
      
        + " DmgUnloadQty = @DmgUnloadQty, "
      
        + " RouteDmgQty = @RouteDmgQty, "
      
        + " UnloadQty = @UnloadQty, "
      
        + " EndQty = @EndQty, "
      
        + " DmgEndQty = @DmgEndQty "
      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " ItemNumber = @ItemNumber and  "
        
          + " StorageTypeId = @StorageTypeId and  "
        
          + " InventoryPeriodId = @InventoryPeriodId "
        
      ;

      public static void Update(RouteInventory routeInventory)
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlUpdate))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", routeInventory.SessionId);
      
        Database.PutParameter(dbCommand,"@LocationId", routeInventory.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeInventory.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", routeInventory.ItemNumber);
      
        Database.PutParameter(dbCommand,"@StorageTypeId", routeInventory.StorageTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryPeriodId", routeInventory.InventoryPeriodId);
      
        Database.PutParameter(dbCommand,"@ClosedIndicator", routeInventory.ClosedIndicator);
      
        Database.PutParameter(dbCommand,"@StartQty", routeInventory.StartQty);
      
        Database.PutParameter(dbCommand,"@LoadQty", routeInventory.LoadQty);
      
        Database.PutParameter(dbCommand,"@LoadAdjustmentQty", routeInventory.LoadAdjustmentQty);
      
        Database.PutParameter(dbCommand,"@TransInQty", routeInventory.TransInQty);
      
        Database.PutParameter(dbCommand,"@TransOutQty", routeInventory.TransOutQty);
      
        Database.PutParameter(dbCommand,"@ReturnQty", routeInventory.ReturnQty);
      
        Database.PutParameter(dbCommand,"@SaleQty", routeInventory.SaleQty);
      
        Database.PutParameter(dbCommand,"@DmgStartQty", routeInventory.DmgStartQty);
      
        Database.PutParameter(dbCommand,"@DmgLoadQty", routeInventory.DmgLoadQty);
      
        Database.PutParameter(dbCommand,"@DmgLoadAdjustmentQty", routeInventory.DmgLoadAdjustmentQty);
      
        Database.PutParameter(dbCommand,"@DmgTransInQty", routeInventory.DmgTransInQty);
      
        Database.PutParameter(dbCommand,"@DmgTransOutQty", routeInventory.DmgTransOutQty);
      
        Database.PutParameter(dbCommand,"@DmgReturnQty", routeInventory.DmgReturnQty);
      
        Database.PutParameter(dbCommand,"@DmgSaleQty", routeInventory.DmgSaleQty);
      
        Database.PutParameter(dbCommand,"@DmgUnloadQty", routeInventory.DmgUnloadQty);
      
        Database.PutParameter(dbCommand,"@RouteDmgQty", routeInventory.RouteDmgQty);
      
        Database.PutParameter(dbCommand,"@UnloadQty", routeInventory.UnloadQty);
      
        Database.PutParameter(dbCommand,"@EndQty", routeInventory.EndQty);
      
        Database.PutParameter(dbCommand,"@DmgEndQty", routeInventory.DmgEndQty);
      

      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region FindByPrimaryKey

      private const String SqlSelectByPk = "Select "

      
        + " SessionId, "
      
        + " LocationId, "
      
        + " RouteNumber, "
      
        + " ItemNumber, "
      
        + " StorageTypeId, "
      
        + " InventoryPeriodId, "
      
        + " ClosedIndicator, "
      
        + " StartQty, "
      
        + " LoadQty, "
      
        + " LoadAdjustmentQty, "
      
        + " TransInQty, "
      
        + " TransOutQty, "
      
        + " ReturnQty, "
      
        + " SaleQty, "
      
        + " DmgStartQty, "
      
        + " DmgLoadQty, "
      
        + " DmgLoadAdjustmentQty, "
      
        + " DmgTransInQty, "
      
        + " DmgTransOutQty, "
      
        + " DmgReturnQty, "
      
        + " DmgSaleQty, "
      
        + " DmgUnloadQty, "
      
        + " RouteDmgQty, "
      
        + " UnloadQty, "
      
        + " EndQty, "
      
        + " DmgEndQty "
      

      + " From RouteInventory "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " ItemNumber = @ItemNumber and  "
        
          + " StorageTypeId = @StorageTypeId and  "
        
          + " InventoryPeriodId = @InventoryPeriodId "
        
      ;

      public static RouteInventory FindByPrimaryKey(
      long sessionId,int locationId,int routeNumber,String itemNumber,int storageTypeId,int inventoryPeriodId
      )
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId", sessionId);
      
        Database.PutParameter(dbCommand,"@LocationId", locationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", itemNumber);
      
        Database.PutParameter(dbCommand,"@StorageTypeId", storageTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryPeriodId", inventoryPeriodId);
      

      using(IDataReader dataReader = dbCommand.ExecuteReader())
      {
      if(dataReader.Read())
      return Load(dataReader);
      }
      }
      throw new DataNotFoundException("RouteInventory not found, search by primary key");

      }

      #endregion

      #region Exists

      public static bool Exists(RouteInventory routeInventory)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByPk))
      {
      
        Database.PutParameter(dbCommand,"@SessionId",routeInventory.SessionId);
      
        Database.PutParameter(dbCommand,"@LocationId",routeInventory.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber",routeInventory.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber",routeInventory.ItemNumber);
      
        Database.PutParameter(dbCommand,"@StorageTypeId",routeInventory.StorageTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryPeriodId",routeInventory.InventoryPeriodId);
      

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
      String sql = "select 1 from RouteInventory";

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

      public static RouteInventory Load(IDataReader dataReader)
      {
      RouteInventory routeInventory = new RouteInventory();

      routeInventory.SessionId = dataReader.GetInt64(0);
          routeInventory.LocationId = dataReader.GetInt32(1);
          routeInventory.RouteNumber = dataReader.GetInt32(2);
          routeInventory.ItemNumber = dataReader.GetString(3);
          routeInventory.StorageTypeId = dataReader.GetInt32(4);
          routeInventory.InventoryPeriodId = dataReader.GetInt32(5);
          routeInventory.ClosedIndicator = dataReader.GetBoolean(6);
          routeInventory.StartQty = dataReader.GetInt32(7);
          routeInventory.LoadQty = dataReader.GetInt32(8);
          routeInventory.LoadAdjustmentQty = dataReader.GetInt32(9);
          routeInventory.TransInQty = dataReader.GetInt32(10);
          routeInventory.TransOutQty = dataReader.GetInt32(11);
          routeInventory.ReturnQty = dataReader.GetInt32(12);
          routeInventory.SaleQty = dataReader.GetInt32(13);
          routeInventory.DmgStartQty = dataReader.GetInt32(14);
          routeInventory.DmgLoadQty = dataReader.GetInt32(15);
          routeInventory.DmgLoadAdjustmentQty = dataReader.GetInt32(16);
          routeInventory.DmgTransInQty = dataReader.GetInt32(17);
          routeInventory.DmgTransOutQty = dataReader.GetInt32(18);
          routeInventory.DmgReturnQty = dataReader.GetInt32(19);
          routeInventory.DmgSaleQty = dataReader.GetInt32(20);
          routeInventory.DmgUnloadQty = dataReader.GetInt32(21);
          routeInventory.RouteDmgQty = dataReader.GetInt32(22);
          routeInventory.UnloadQty = dataReader.GetInt32(23);
          routeInventory.EndQty = dataReader.GetInt32(24);
          routeInventory.DmgEndQty = dataReader.GetInt32(25);
          

      return routeInventory;
      }

      #endregion

      #region Delete
      private const String SqlDelete = "Delete From RouteInventory "

      
        + " Where "
        
          + " SessionId = @SessionId and  "
        
          + " LocationId = @LocationId and  "
        
          + " RouteNumber = @RouteNumber and  "
        
          + " ItemNumber = @ItemNumber and  "
        
          + " StorageTypeId = @StorageTypeId and  "
        
          + " InventoryPeriodId = @InventoryPeriodId "
        
      ;
      public static void Delete(RouteInventory routeInventory)
      {

      using(IDbCommand dbCommand = Database.PrepareCommand(SqlDelete))
      {

      
        Database.PutParameter(dbCommand,"@SessionId", routeInventory.SessionId);
      
        Database.PutParameter(dbCommand,"@LocationId", routeInventory.LocationId);
      
        Database.PutParameter(dbCommand,"@RouteNumber", routeInventory.RouteNumber);
      
        Database.PutParameter(dbCommand,"@ItemNumber", routeInventory.ItemNumber);
      
        Database.PutParameter(dbCommand,"@StorageTypeId", routeInventory.StorageTypeId);
      
        Database.PutParameter(dbCommand,"@InventoryPeriodId", routeInventory.InventoryPeriodId);
      


      dbCommand.ExecuteNonQuery();
      }
      }

      #endregion

      #region Clear

      private const String SqlDeleteAll = "Delete From RouteInventory ";

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
      
        + " LocationId, "
      
        + " RouteNumber, "
      
        + " ItemNumber, "
      
        + " StorageTypeId, "
      
        + " InventoryPeriodId, "
      
        + " ClosedIndicator, "
      
        + " StartQty, "
      
        + " LoadQty, "
      
        + " LoadAdjustmentQty, "
      
        + " TransInQty, "
      
        + " TransOutQty, "
      
        + " ReturnQty, "
      
        + " SaleQty, "
      
        + " DmgStartQty, "
      
        + " DmgLoadQty, "
      
        + " DmgLoadAdjustmentQty, "
      
        + " DmgTransInQty, "
      
        + " DmgTransOutQty, "
      
        + " DmgReturnQty, "
      
        + " DmgSaleQty, "
      
        + " DmgUnloadQty, "
      
        + " RouteDmgQty, "
      
        + " UnloadQty, "
      
        + " EndQty, "
      
        + " DmgEndQty "
      

      + " From RouteInventory ";
      public static List<RouteInventory> Find()
      {
      using(IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAll))
      {
        List<RouteInventory> rv = new List<RouteInventory>();

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
        List<RouteInventory> itemsList = Load(xmlFilePath);

      if(itemsList.Count != 0)
      Insert(itemsList);

      return itemsList.Count;
      }

      #endregion

      #region Export to file
      public static int Export(String xmlFilePath)
      {

        List<RouteInventory> itemsList = Find();

        if (itemsList.Count == 0)
        return 0;


        XmlWriter xmlWriter = new XmlTextWriter(xmlFilePath, Encoding.UTF8);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteInventory));

      xmlWriter.WriteStartDocument();
      xmlWriter.WriteStartElement("Root");
      
      foreach(RouteInventory item in itemsList)
      xmlSerializer.Serialize(xmlWriter, item);

      xmlWriter.WriteEndElement();
      xmlWriter.WriteEndDocument();
      
      xmlWriter.Flush();
      xmlWriter.Close();

      return itemsList.Count;


      }
      #endregion

      #region Load from file

      public static List<RouteInventory>
        Load(String xmlFilePath)
        {
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(RouteInventory));
        XmlDocument xmlDocument = new XmlDocument();

        xmlDocument.Load(xmlFilePath);

        List<RouteInventory> itemsList
      = new List<RouteInventory>();

      foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
      {
      Object deserializedObject = xmlSerializer.Deserialize(
      new XmlNodeReader(xmlNode));

      if (deserializedObject is RouteInventory)
        itemsList.Add(deserializedObject as RouteInventory);
      }

      return itemsList;
      }
      
      #endregion

      #endregion


      #region Biz
      

        #region Fields
        
          protected long m_sessionId;
        
          protected int m_locationId;
        
          protected int m_routeNumber;
        
          protected String m_itemNumber;
        
          protected int m_storageTypeId;
        
          protected int m_inventoryPeriodId;
        
          protected bool m_closedIndicator;
        
          protected int m_startQty;
        
          protected int m_loadQty;
        
          protected int m_loadAdjustmentQty;
        
          protected int m_transInQty;
        
          protected int m_transOutQty;
        
          protected int m_returnQty;
        
          protected int m_saleQty;
        
          protected int m_dmgStartQty;
        
          protected int m_dmgLoadQty;
        
          protected int m_dmgLoadAdjustmentQty;
        
          protected int m_dmgTransInQty;
        
          protected int m_dmgTransOutQty;
        
          protected int m_dmgReturnQty;
        
          protected int m_dmgSaleQty;
        
          protected int m_dmgUnloadQty;
        
          protected int m_routeDmgQty;
        
          protected int m_unloadQty;
        
          protected int m_endQty;
        
          protected int m_dmgEndQty;
        
        #endregion
        
        #region Constructors
        public RouteInventory(
        long 
          sessionId,int 
          locationId,int 
          routeNumber,String 
          itemNumber,int 
          storageTypeId,int 
          inventoryPeriodId
         )
        {
        
          m_sessionId = sessionId;
        
          m_locationId = locationId;
        
          m_routeNumber = routeNumber;
        
          m_itemNumber = itemNumber;
        
          m_storageTypeId = storageTypeId;
        
          m_inventoryPeriodId = inventoryPeriodId;
        
        }
        
        


        public RouteInventory(
        long 
          sessionId,int 
          locationId,int 
          routeNumber,String 
          itemNumber,int 
          storageTypeId,int 
          inventoryPeriodId,bool 
          closedIndicator,int 
          startQty,int 
          loadQty,int 
          loadAdjustmentQty,int 
          transInQty,int 
          transOutQty,int 
          returnQty,int 
          saleQty,int 
          dmgStartQty,int 
          dmgLoadQty,int 
          dmgLoadAdjustmentQty,int 
          dmgTransInQty,int 
          dmgTransOutQty,int 
          dmgReturnQty,int 
          dmgSaleQty,int 
          dmgUnloadQty,int 
          routeDmgQty,int 
          unloadQty,int 
          endQty,int 
          dmgEndQty
        )
        {
        
          m_sessionId = sessionId;
        
          m_locationId = locationId;
        
          m_routeNumber = routeNumber;
        
          m_itemNumber = itemNumber;
        
          m_storageTypeId = storageTypeId;
        
          m_inventoryPeriodId = inventoryPeriodId;
        
          m_closedIndicator = closedIndicator;
        
          m_startQty = startQty;
        
          m_loadQty = loadQty;
        
          m_loadAdjustmentQty = loadAdjustmentQty;
        
          m_transInQty = transInQty;
        
          m_transOutQty = transOutQty;
        
          m_returnQty = returnQty;
        
          m_saleQty = saleQty;
        
          m_dmgStartQty = dmgStartQty;
        
          m_dmgLoadQty = dmgLoadQty;
        
          m_dmgLoadAdjustmentQty = dmgLoadAdjustmentQty;
        
          m_dmgTransInQty = dmgTransInQty;
        
          m_dmgTransOutQty = dmgTransOutQty;
        
          m_dmgReturnQty = dmgReturnQty;
        
          m_dmgSaleQty = dmgSaleQty;
        
          m_dmgUnloadQty = dmgUnloadQty;
        
          m_routeDmgQty = routeDmgQty;
        
          m_unloadQty = unloadQty;
        
          m_endQty = endQty;
        
          m_dmgEndQty = dmgEndQty;
        
          }


        
      #endregion

      
        [XmlElement]
        public long SessionId
        {
          get { return m_sessionId;}
          set { m_sessionId = value; }
        }
      
        [XmlElement]
        public int LocationId
        {
          get { return m_locationId;}
          set { m_locationId = value; }
        }
      
        [XmlElement]
        public int RouteNumber
        {
          get { return m_routeNumber;}
          set { m_routeNumber = value; }
        }
      
        [XmlElement]
        public String ItemNumber
        {
          get { return m_itemNumber;}
          set { m_itemNumber = value; }
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
        public bool ClosedIndicator
        {
          get { return m_closedIndicator;}
          set { m_closedIndicator = value; }
        }
      
        [XmlElement]
        public int StartQty
        {
          get { return m_startQty;}
          set { m_startQty = value; }
        }
      
        [XmlElement]
        public int LoadQty
        {
          get { return m_loadQty;}
          set { m_loadQty = value; }
        }
      
        [XmlElement]
        public int LoadAdjustmentQty
        {
          get { return m_loadAdjustmentQty;}
          set { m_loadAdjustmentQty = value; }
        }
      
        [XmlElement]
        public int TransInQty
        {
          get { return m_transInQty;}
          set { m_transInQty = value; }
        }
      
        [XmlElement]
        public int TransOutQty
        {
          get { return m_transOutQty;}
          set { m_transOutQty = value; }
        }
      
        [XmlElement]
        public int ReturnQty
        {
          get { return m_returnQty;}
          set { m_returnQty = value; }
        }
      
        [XmlElement]
        public int SaleQty
        {
          get { return m_saleQty;}
          set { m_saleQty = value; }
        }
      
        [XmlElement]
        public int DmgStartQty
        {
          get { return m_dmgStartQty;}
          set { m_dmgStartQty = value; }
        }
      
        [XmlElement]
        public int DmgLoadQty
        {
          get { return m_dmgLoadQty;}
          set { m_dmgLoadQty = value; }
        }
      
        [XmlElement]
        public int DmgLoadAdjustmentQty
        {
          get { return m_dmgLoadAdjustmentQty;}
          set { m_dmgLoadAdjustmentQty = value; }
        }
      
        [XmlElement]
        public int DmgTransInQty
        {
          get { return m_dmgTransInQty;}
          set { m_dmgTransInQty = value; }
        }
      
        [XmlElement]
        public int DmgTransOutQty
        {
          get { return m_dmgTransOutQty;}
          set { m_dmgTransOutQty = value; }
        }
      
        [XmlElement]
        public int DmgReturnQty
        {
          get { return m_dmgReturnQty;}
          set { m_dmgReturnQty = value; }
        }
      
        [XmlElement]
        public int DmgSaleQty
        {
          get { return m_dmgSaleQty;}
          set { m_dmgSaleQty = value; }
        }
      
        [XmlElement]
        public int DmgUnloadQty
        {
          get { return m_dmgUnloadQty;}
          set { m_dmgUnloadQty = value; }
        }
      
        [XmlElement]
        public int RouteDmgQty
        {
          get { return m_routeDmgQty;}
          set { m_routeDmgQty = value; }
        }
      
        [XmlElement]
        public int UnloadQty
        {
          get { return m_unloadQty;}
          set { m_unloadQty = value; }
        }
      
        [XmlElement]
        public int EndQty
        {
          get { return m_endQty;}
          set { m_endQty = value; }
        }
      
        [XmlElement]
        public int DmgEndQty
        {
          get { return m_dmgEndQty;}
          set { m_dmgEndQty = value; }
        }
      
      }
      #endregion
      }

    