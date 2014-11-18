using System;
using System.Data;
using MobileTech.Data;
using System.Collections.Generic;


namespace MobileTech.Domain
{
    public partial class RouteInventory
    {

        #region Constructors

        public RouteInventory()
        {

        }

        public RouteInventory(Item item)
        {
            Item = item;
        }

        #endregion

        #region Extra fields

        Item m_item;
        public Item Item
        {
            get 
            {
                if (m_item == null)
                {
                    m_item = Item.FindByPrimaryKey(m_locationId, 
                        m_routeNumber, 
                        m_itemNumber);
                }
                return m_item; 
            }
            set
            {
                m_item = value;

                if (m_item != null)
                {
                    m_locationId = value.LocationId;
                    m_routeNumber = value.RouteNumber;
                    m_itemNumber = value.ItemNumber;
                }
            }
        }

        public StorageTypeEnum StorageType
        {
            get
            {
                return (StorageTypeEnum)m_storageTypeId;
            }
            set
            {
                m_storageTypeId = (int)value;
            }
        }

        static int s_currentPeriodIndex = 0;

        public static int CurrentPeriodIndex
        {
            get
            {
                if (s_currentPeriodIndex == 0)
                {
                    s_currentPeriodIndex = GetCurrentPeriodIndex();

                    if (s_currentPeriodIndex == 0)
                        ++s_currentPeriodIndex;
                }

                return s_currentPeriodIndex;
            }
            set { s_currentPeriodIndex = value; }
        }


        public int TruckQty
        {
            get { return m_startQty + m_loadQty + m_loadAdjustmentQty - m_saleQty; }
        }

        public int TruckDmgQty
        {
            get { return m_dmgStartQty + m_dmgLoadQty + m_dmgLoadAdjustmentQty - m_dmgSaleQty; }
        }
        #endregion

        #region Finders

        #region FindBy
        /// <summary>
        /// Find object in current workspace, using 
        /// current route and active session
        /// </summary>
        /// <param name="item"></param>
        /// <param name="storageType"></param>
        /// <returns></returns>
        public static RouteInventory FindBy(Item item, StorageTypeEnum storageType)
        {
            return FindByPrimaryKey(
                Session.ActiveSession.SessionId,
                Route.Current.LocationId,
                Route.Current.RouteNumber,
                item.ItemNumber,
                (int)storageType,
                GetCurrentPeriodIndex());
        }

        #endregion

        #region GetCurrentPeriodIndex

        /// <summary>
        /// Find period index in current workspace, using 
        /// current route and active session
        /// </summary>
        /// <returns></returns>
        public static int GetCurrentPeriodIndex()
        {
            return GetPeriodIndex(Session.ActiveSession, Route.Current);
        }

        #endregion

        #region GetPeriodIndex
        const String SqlFindCurrentPeriodIndex = "Select Max(InventoryPeriodId) from RouteInventory Where SessionId = @SessionId and RouteNumber = @RouteNumber and LocationId = @LocationId and ClosedIndicator = 0";


        public static int GetPeriodIndex(Session session, Route route)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindCurrentPeriodIndex))
            {

                Database.PutParameter(dbCommand, "@RouteNumber", route.RouteNumber);
                Database.PutParameter(dbCommand, "@LocationId", route.LocationId);
                Database.PutParameter(dbCommand, "@SessionId", session.SessionId);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read() && !dataReader.IsDBNull(0))
#if WINCE
                        return (int)dataReader.GetInt64(0);
#else
                        return dataReader.GetInt32(0);
#endif
                }
            }

            return 0;
        }

        #endregion

        #region Fill

        private static void Fill(RouteInventory routeInventory, IDataReader dataReader, int startFieldIndex)
        {
            routeInventory.ClosedIndicator = dataReader.GetBoolean(startFieldIndex++);
            routeInventory.StartQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.LoadQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.LoadAdjustmentQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.TransInQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.TransOutQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.ReturnQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.SaleQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.DmgStartQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.DmgLoadQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.DmgLoadAdjustmentQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.DmgTransInQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.DmgTransOutQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.DmgReturnQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.DmgSaleQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.DmgUnloadQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.RouteDmgQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.UnloadQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.EndQty = dataReader.GetInt32(startFieldIndex++);
            routeInventory.DmgEndQty = dataReader.GetInt32(startFieldIndex++);
        }

        #endregion

        #region FindByRoute

        /// <summary>
        /// Find route inventory items by current period index, route and session
        /// </summary>
        /// <param name="storageType"></param>
        /// <returns></returns>
        public static List<RouteInventory> FindBy(StorageTypeEnum storageType, ItemTypeEnum itemType)
        {
            return FindBy(
                Route.Current,
                Session.ActiveSession,
                storageType,
                itemType,
                CurrentPeriodIndex);
        }

        const String SqlFindByRoute = "Select Item.ItemNumber, ItemCategoryId, ItemTypeId, Name, Description, StorageTypeId,InventoryPeriodId,ClosedIndicator,StartQty,LoadQty,LoadAdjustmentQty,TransInQty,TransOutQty,ReturnQty,SaleQty,DmgStartQty,DmgLoadQty,DmgLoadAdjustmentQty,DmgTransInQty,DmgTransOutQty,DmgReturnQty,DmgSaleQty,DmgUnloadQty,RouteDmgQty,UnloadQty,EndQty,DmgEndQty  " +
            " From Item Inner Join RouteInventory on RouteInventory.ItemNumber = Item.ItemNumber " +
            " and Item.RouteNumber = RouteInventory.RouteNumber " +
            " and Item.LocationId = RouteInventory.LocationId " +
            " and RouteInventory.SessionId = @SessionId and ClosedIndicator = 0" +
            " where Item.RouteNumber = @RouteNumber" +
            " and Item.LocationId = @LocationId " +
            " and InventoryPeriodId = @InventoryPeriodId " +
            " and RouteInventory.StorageTypeId = @StorageTypeId and ItemTypeId = @ItemTypeId";

        public static List<RouteInventory> FindBy(Route route, 
            Session session, 
            StorageTypeEnum storageType, 
            ItemTypeEnum itemType,
            int periodIndex)
        {

            List<RouteInventory> rv = new List<RouteInventory>();

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByRoute))
            {

                Database.PutParameter(dbCommand, "@LocationId", route.LocationId);
                Database.PutParameter(dbCommand, "@RouteNumber", route.RouteNumber);
                Database.PutParameter(dbCommand, "@SessionId", session.SessionId);
                Database.PutParameter(dbCommand, "@InventoryPeriodId", periodIndex);
                Database.PutParameter(dbCommand, "@StorageTypeId", (int)storageType);
                Database.PutParameter(dbCommand, "@ItemTypeId", (int)itemType);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Item product = new Item(
                            route.LocationId,
                            route.RouteNumber,
                            dataReader.GetString(0),
                            dataReader.GetInt32(1),
                            dataReader.GetInt32(2),
                            dataReader.GetString(3),
                            dataReader.GetString(4),
                            0, 0
                            );

                        RouteInventory routeInventory = new RouteInventory(product);

                        routeInventory.SessionId = session.SessionId;
                        routeInventory.StorageTypeId = (int)storageType;
                        routeInventory.InventoryPeriodId = dataReader.IsDBNull(6) ? 0 : dataReader.GetInt32(6);


                        if (routeInventory.InventoryPeriodId != 0)
                        {
                            Fill(routeInventory, dataReader, 7);
                        }

                        rv.Add(routeInventory);
                    }
                }
            }

            return rv;
        }

        #endregion

        #region FindByPrimaryKey

        public static RouteInventory FindByPrimaryKey(RouteInventory routeInventory)
        {
            return FindByPrimaryKey(
                routeInventory.SessionId, routeInventory.LocationId, routeInventory.RouteNumber,
                routeInventory.ItemNumber, routeInventory.StorageTypeId, routeInventory.InventoryPeriodId);
        }

        #endregion

        #endregion

        #region Service

        #region Prepare

        public static RouteInventory Prepare(StorageTypeEnum storageType)
        {
            RouteInventory routeInventory = new RouteInventory();

            routeInventory.InventoryPeriodId = CurrentPeriodIndex;
            routeInventory.SessionId = Session.ActiveSession.SessionId;
            routeInventory.RouteNumber = Route.Current.RouteNumber;
            routeInventory.LocationId = Route.Current.LocationId;
            routeInventory.StorageType = storageType;

            return routeInventory;
        }

        #endregion

        #region Prepare

        public static RouteInventory Prepare(long sessionId, int locationId, int routeNumber, StorageTypeEnum storage)
        {
            RouteInventory routeInventory = new RouteInventory();

            routeInventory.InventoryPeriodId = CurrentPeriodIndex;

            routeInventory.SessionId = sessionId;
            routeInventory.LocationId = locationId;
            routeInventory.RouteNumber = routeNumber;
            routeInventory.StorageType = storage;

            if (routeInventory.InventoryPeriodId == 0)
                routeInventory.InventoryPeriodId = 1;

            return routeInventory;

        }

        #endregion

        #region ModifySale

        public void ModifySale(int quantity)
        {
            ModifySale(this, quantity);
        }

        #endregion

        #region AssignInventoryPeriodIndex

        public void AssignInventoryPeriodIndex()
        {
            m_inventoryPeriodId = CurrentPeriodIndex;
        }

        #endregion

        #region UpdateOrCreate

        public static void UpdateOrCreate(RouteInventory routeInventory)
        {

            if (RouteInventory.Exists(routeInventory))
            {
                RouteInventory.Update(routeInventory);
            }
            else
                RouteInventory.Insert(routeInventory);

        }

        #endregion

        #region ModifySale

        public static void ModifySale(RouteInventory routeInventory, int quantity)
        {


            RouteInventory routeInventoryModify = null;

            try
            {
                routeInventoryModify = RouteInventory.FindByPrimaryKey(routeInventory);

                routeInventoryModify.SaleQty += quantity;

                RouteInventory.Update(routeInventoryModify);


            }
            catch (DataNotFoundException)
            {
                routeInventory.SaleQty = quantity;

                Insert(routeInventory);
            }

        }

        #endregion

        #region UpdateLoad

        private const String SqlUpdateLoad = "Update RouteInventory Set LoadQty = @LoadQty Where  SessionId = @SessionId and  LocationId = @LocationId and  RouteNumber = @RouteNumber and  ItemNumber = @ItemNumber and  StorageTypeId = @StorageTypeId and  InventoryPeriodId = @InventoryPeriodId";
        public static void UpdateLoad(RouteInventory routeInventory, int quantity)
        {
            if (Exists(routeInventory))
            {
                using (IDbCommand dbCommand = Database.PrepareCommand(SqlUpdateLoad))
                {

                    Database.PutParameter(dbCommand, "@LoadQty", quantity);

                    Database.PutParameter(dbCommand, "@SessionId", routeInventory.SessionId);

                    Database.PutParameter(dbCommand, "@LocationId", routeInventory.LocationId);

                    Database.PutParameter(dbCommand, "@RouteNumber", routeInventory.RouteNumber);

                    Database.PutParameter(dbCommand, "@ItemNumber", routeInventory.ItemNumber);

                    Database.PutParameter(dbCommand, "@StorageTypeId", routeInventory.StorageTypeId);

                    Database.PutParameter(dbCommand, "@InventoryPeriodId", routeInventory.InventoryPeriodId);

                    dbCommand.ExecuteNonQuery();
                }

            }
            else
            {
                RouteInventory routeInventoryModify = Prepare(routeInventory.SessionId,
                    routeInventory.LocationId,
                    routeInventory.RouteNumber,
                    routeInventory.StorageType);

                routeInventoryModify.Item = routeInventory.Item;
                routeInventoryModify.LoadQty = quantity;

                Insert(routeInventoryModify);
            }
        }

        #endregion

        #region UpdateLoadAdjustment

        #region Single

        private const String SqlUpdateLoadAdjustment = "Update RouteInventory Set LoadAdjustmentQty = @LoadAdjustmentQty Where  SessionId = @SessionId and  LocationId = @LocationId and  RouteNumber = @RouteNumber and  ItemNumber = @ItemNumber and  StorageTypeId = @StorageTypeId and  InventoryPeriodId = @InventoryPeriodId";
        public static void UpdateLoadAdjustment(RouteInventory routeInventory, int quantity)
        {
            int affected = 0;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlUpdateLoadAdjustment))
            {

                Database.PutParameter(dbCommand, "@LoadAdjustmentQty", quantity);

                Database.PutParameter(dbCommand, "@SessionId", routeInventory.SessionId);

                Database.PutParameter(dbCommand, "@LocationId", routeInventory.LocationId);

                Database.PutParameter(dbCommand, "@RouteNumber", routeInventory.RouteNumber);

                Database.PutParameter(dbCommand, "@ItemNumber", routeInventory.ItemNumber);

                Database.PutParameter(dbCommand, "@StorageTypeId", routeInventory.StorageTypeId);

                Database.PutParameter(dbCommand, "@InventoryPeriodId", routeInventory.InventoryPeriodId);

                affected = dbCommand.ExecuteNonQuery();
            }


            if (affected != 1)
            {
                RouteInventory routeInventoryModify = Prepare(routeInventory.SessionId,
                    routeInventory.LocationId,
                    routeInventory.RouteNumber,
                    routeInventory.StorageType);

                routeInventoryModify.Item = routeInventory.Item;
                routeInventoryModify.LoadAdjustmentQty = quantity;

                Insert(routeInventoryModify);
            }
        }

        #endregion

        #region Multiple

        public static void UpdateLoadAdjustment(List<RouteInventory> routeInventoryList)
        {
            int affected = 0;
            bool argsAdded = false;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlUpdateLoadAdjustment))
            {

                foreach (RouteInventory routeInventory in routeInventoryList)
                {
                    affected = 0;

                    if (!argsAdded)
                    {

                        Database.PutParameter(dbCommand, "@SessionId", routeInventory.SessionId);

                        Database.PutParameter(dbCommand, "@LocationId", routeInventory.LocationId);

                        Database.PutParameter(dbCommand, "@RouteNumber", routeInventory.RouteNumber);

                        Database.PutParameter(dbCommand, "@StorageTypeId", routeInventory.StorageTypeId);

                        Database.PutParameter(dbCommand, "@InventoryPeriodId", routeInventory.InventoryPeriodId);

                        Database.PutParameter(dbCommand, "@LoadAdjustmentQty", routeInventory.LoadAdjustmentQty);

                        Database.PutParameter(dbCommand, "@ItemNumber", routeInventory.ItemNumber);


                        argsAdded = true;
                    }
                    else
                    {
                        ((IDataParameter)dbCommand.Parameters["@LoadAdjustmentQty"]).Value = routeInventory.LoadAdjustmentQty;
                        ((IDataParameter)dbCommand.Parameters["@ItemNumber"]).Value = routeInventory.ItemNumber;
                    }


                    affected = dbCommand.ExecuteNonQuery();

                    if (affected != 1)
                    {
                        RouteInventory routeInventoryModify = Prepare(routeInventory.SessionId,
                            routeInventory.LocationId,
                            routeInventory.RouteNumber,
                            routeInventory.StorageType);

                        routeInventoryModify.Item = routeInventory.Item;
                        routeInventoryModify.LoadAdjustmentQty = routeInventory.LoadAdjustmentQty;

                        Insert(routeInventoryModify);
                    }

                }
            }



        }

        #endregion

        #endregion

        #region UpdateDmgLoadAdjustment

        #region Single

        private const String SqlUpdateDmgLoadAdjustment = "Update RouteInventory Set DmgLoadAdjustmentQty = @DmgLoadAdjustmentQty Where  SessionId = @SessionId and  LocationId = @LocationId and  RouteNumber = @RouteNumber and  ItemNumber = @ItemNumber and  StorageTypeId = @StorageTypeId and  InventoryPeriodId = @InventoryPeriodId";
        public static void UpdateDmgLoadAdjustment(RouteInventory routeInventory, int quantity)
        {
            int affected = 0;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlUpdateDmgLoadAdjustment))
            {

                Database.PutParameter(dbCommand, "@DmgLoadAdjustmentQty", quantity);

                Database.PutParameter(dbCommand, "@SessionId", routeInventory.SessionId);

                Database.PutParameter(dbCommand, "@LocationId", routeInventory.LocationId);

                Database.PutParameter(dbCommand, "@RouteNumber", routeInventory.RouteNumber);

                Database.PutParameter(dbCommand, "@ItemNumber", routeInventory.ItemNumber);

                Database.PutParameter(dbCommand, "@StorageTypeId", routeInventory.StorageTypeId);

                Database.PutParameter(dbCommand, "@InventoryPeriodId", routeInventory.InventoryPeriodId);

                affected = dbCommand.ExecuteNonQuery();
            }


            if (affected != 1)
            {
                RouteInventory routeInventoryModify = Prepare(routeInventory.SessionId,
                    routeInventory.LocationId,
                    routeInventory.RouteNumber,
                    routeInventory.StorageType);

                routeInventoryModify.Item = routeInventory.Item;
                routeInventoryModify.DmgLoadAdjustmentQty = quantity;

                Insert(routeInventoryModify);
            }
        }

        #endregion

        #region Multiple

        public static void UpdateDmgLoadAdjustment(List<RouteInventory> routeInventoryList)
        {
            int affected = 0;
            bool argsAdded = false;

            using (IDbCommand dbCommand = Database.PrepareCommand(SqlUpdateDmgLoadAdjustment))
            {

                foreach (RouteInventory routeInventory in routeInventoryList)
                {
                    affected = 0;

                    if (!argsAdded)
                    {

                        Database.PutParameter(dbCommand, "@SessionId", routeInventory.SessionId);

                        Database.PutParameter(dbCommand, "@LocationId", routeInventory.LocationId);

                        Database.PutParameter(dbCommand, "@RouteNumber", routeInventory.RouteNumber);

                        Database.PutParameter(dbCommand, "@StorageTypeId", routeInventory.StorageTypeId);

                        Database.PutParameter(dbCommand, "@InventoryPeriodId", routeInventory.InventoryPeriodId);

                        Database.PutParameter(dbCommand, "@DmgLoadAdjustmentQty", routeInventory.DmgLoadAdjustmentQty);

                        Database.PutParameter(dbCommand, "@ItemNumber", routeInventory.ItemNumber);


                        argsAdded = true;
                    }
                    else
                    {
                        ((IDataParameter)dbCommand.Parameters["@DmgLoadAdjustmentQty"]).Value = routeInventory.DmgLoadAdjustmentQty;
                        ((IDataParameter)dbCommand.Parameters["@ItemNumber"]).Value = routeInventory.ItemNumber;
                    }


                    affected = dbCommand.ExecuteNonQuery();

                    if (affected != 1)
                    {
                        RouteInventory routeInventoryModify = Prepare(routeInventory.SessionId,
                            routeInventory.LocationId,
                            routeInventory.RouteNumber,
                            routeInventory.StorageType);

                        routeInventoryModify.Item = routeInventory.Item;
                        routeInventoryModify.DmgLoadAdjustmentQty = routeInventory.DmgLoadAdjustmentQty;

                        Insert(routeInventoryModify);
                    }

                }
            }



        }

        #endregion

        #endregion

        #region ChangePeriodIndex

        const String SqlSelectAllCurrent = "Select ItemNumber, StorageTypeId, EndQty "+
            "from RouteInventory Where "+
            "SessionId = @SessionId " +
            "and LocationId = @LocationId " +
            "and RouteNumber = @RouteNumber " +
            "and InventoryPeriodId = @InventoryPeriodId";

        const String SqlClosePeriod = "Update RouteInventory Set ClosedIndicator = 1 " +
            "Where " +
            "SessionId = @SessionId " +
            "and LocationId = @LocationId " +
            "and RouteNumber = @RouteNumber " +
            "and InventoryPeriodId = @InventoryPeriodId";

        [TransactionRequired]
        public static void CreateNextPeriod()
        {
            int currentPeriodIndex = GetCurrentPeriodIndex();

            Route route = Route.Current;
            Session session = Session.ActiveSession;
            List<RouteInventory> routeInventoryList = new List<RouteInventory>();


            using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectAllCurrent))
            {

                Database.PutParameter(dbCommand, "@SessionId", session.SessionId);
                Database.PutParameter(dbCommand, "@LocationId", route.LocationId);
                Database.PutParameter(dbCommand, "@RouteNumber", route.RouteNumber);
                Database.PutParameter(dbCommand, "@InventoryPeriodId", currentPeriodIndex);


                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        routeInventoryList.Add(
                            new RouteInventory(session.SessionId,
                            route.LocationId,
                            route.RouteNumber,
                            dataReader.GetString(0),
                            dataReader.GetInt32(1),
                            currentPeriodIndex + 1,
                            false,
                            dataReader.GetInt32(2),
                            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
                    }
                }
            }


            using (IDbCommand dbCommand = Database.PrepareCommand(SqlClosePeriod))
            {
                Database.PutParameter(dbCommand, "@SessionId", session.SessionId);
                Database.PutParameter(dbCommand, "@LocationId", route.LocationId);
                Database.PutParameter(dbCommand, "@RouteNumber", route.RouteNumber);
                Database.PutParameter(dbCommand, "@InventoryPeriodId", currentPeriodIndex);

                dbCommand.ExecuteNonQuery();
            }


            Insert(routeInventoryList);

            ++s_currentPeriodIndex;
        }

        #endregion

        public static void Reset()
        {
            s_currentPeriodIndex = 0;
        }
        #endregion
    }
}
