using System;
using System.Collections.Generic;
using MobileTech.Data;
using System.Data;

namespace MobileTech.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public partial class CustomerTransactionDetail
    {

        #region SqlStatements
        //static String SqlFindByTransactionWithProducts = "Select " +
        //    /* 0 */	" CustomerTransactionDetail.SessionId," +
        //    /* 1 */	" CustomerTransactionDetail.BusinessTransactionId, " +
        //    /* 2 */	" DateCreated, " +
        //    /* 3,4,5 */	"Item.LocationId, Item.RouteNumber, Item.ItemNumber,  " +
        //    /* 6 */	" CustomerTransactionDetail.Quantity," +
        //    /* 7,8,9,10 */	" Name,Description,ItemCategoryId,ItemTypeId," +
        //    /* 11*/ " RouteInventory.StartQty + RouteInventory.LoadQty  + RouteInventory.LoadAdjustmentQty - RouteInventory.SaleQty " +
        //        " From Item" +
        //        " Left Outer Join RouteInventory On Item.ItemNumber = RouteInventory.ItemNumber " +
        //        " and Item.LocationId = RouteInventory.LocationId " +
        //        " and Item.RouteNumber = RouteInventory.RouteNumber " +
        //        " and RouteInventory.SessionId = @SessionId2" +
        //        " Inner Join CustomerTransactionDetail On Item.ItemNumber = CustomerTransactionDetail.ItemNumber" +
        //        " and CustomerTransactionDetail.BusinessTransactionId = @BusinessTransactionId " +
        //        " and CustomerTransactionDetail.SessionId = @SessionId";


        static String SqlClearByTransaction = "Delete From CustomerTransactionDetail Where BusinessTransactionId = @BusinessTransactionId and SessionId = @SessionId";



        #endregion

        #region Constructors

        public CustomerTransactionDetail()
        {

        }

        public CustomerTransactionDetail(CustomerTransaction customerTransaction, Item item)
        {
            Item = item;
            CustomerTransaction = customerTransaction;
            m_dateCreated = DateTime.Now;
        }

        public CustomerTransactionDetail(Item item)
        {
            Item = item;
            m_dateCreated = DateTime.Now;
        }

        #endregion

        #region Extra fields

        CustomerTransaction m_customerTransaction;

        public CustomerTransaction CustomerTransaction
        {
            get { return m_customerTransaction; }
            set 
            { 
                m_customerTransaction = value;

                if (value != null)
                {
                    m_businessTransactionId = value.BusinessTransactionId;
                    m_sessionId = value.SessionId;
                }
            }
        }

        Item m_item;
        public Item Item
        {
            get { return m_item; }
            set 
            {
                m_item = value;

                if (value != null)
                {
                    m_itemNumber = value.ItemNumber;
                    m_routeNumber = value.RouteNumber;
                    m_locationId = value.LocationId;
                }
            }
        }
        int m_inventoryQuantity;
        public int InventoryQuantity
        {
            get { return m_inventoryQuantity; }
            set { m_inventoryQuantity = value; }
        }

        #endregion

        #region Finders

        #region Find by transaction

        static String SqlFindByTransaction = "Select " +
            /* 0 */	" CustomerTransactionDetail.SessionId," +
            /* 1 */	" CustomerTransactionDetail.BusinessTransactionId, " +
            /* 2 */	" DateCreated, " +
            /* 3,4,5 */	"Item.LocationId, Item.RouteNumber, Item.ItemNumber,  " +
            /* 6 */	" CustomerTransactionDetail.Quantity," +
            /* 7,8,9,10 */	" Name,Description,ItemCategoryId,ItemTypeId," +
            /* 11*/ " RouteInventory.StartQty + RouteInventory.LoadQty  + RouteInventory.LoadAdjustmentQty - RouteInventory.SaleQty" +
            " From CustomerTransactionDetail" +
            " Inner Join Item On Item.ItemNumber = CustomerTransactionDetail.ItemNumber" +
            " Left Outer Join RouteInventory On Item.ItemNumber = RouteInventory.ItemNumber " +
            " and Item.LocationId = RouteInventory.LocationId " +
            " and Item.RouteNumber = RouteInventory.RouteNumber " +
            " and RouteInventory.SessionId = @SessionId2 and InventoryPeriodId = @InventoryPeriodId" +
            " Where BusinessTransactionId = @BusinessTransactionId and CustomerTransactionDetail.SessionId = @SessionId ";



        public static List<CustomerTransactionDetail> FindBy(CustomerTransaction transaction)
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTransaction);

            Database.PutParameter(dbCommand, "@BusinessTransactionId", transaction.BusinessTransactionId);
            Database.PutParameter(dbCommand, "@SessionId", transaction.SessionId);
            Database.PutParameter(dbCommand, "@SessionId2", transaction.SessionId);
            Database.PutParameter(dbCommand, "@InventoryPeriodId", RouteInventory.CurrentPeriodIndex);

            List<CustomerTransactionDetail> rv = new List<CustomerTransactionDetail>();

            using (IDataReader dataReader = dbCommand.ExecuteReader())
            {
                while (dataReader.Read())
                {
                    Item product = new Item(
                        dataReader.GetInt32(3),
                        dataReader.GetInt32(4),
                        dataReader.GetString(5),
                        dataReader.GetInt32(9),
                        dataReader.GetInt32(10),
                        dataReader.GetString(7),
                        dataReader.GetString(8),
                        0, 0);

                    CustomerTransactionDetail customerTransactionDetail = new CustomerTransactionDetail(transaction,product);

                    if (dataReader.IsDBNull(0))
                    {
                        customerTransactionDetail.DateCreated = DateTime.Now;
                    }
                    else
                    {
                        customerTransactionDetail.DateCreated = dataReader.GetDateTime(2);
                        customerTransactionDetail.Quantity = dataReader.GetInt32(6);
                    }

                    customerTransactionDetail.InventoryQuantity = dataReader.IsDBNull(11) ? 0 : dataReader.GetInt32(11);


                    rv.Add(customerTransactionDetail);

                }
            }

            return rv;
        }

        #endregion

        public static int? FindAll(DataTable dtCustomerDetailTransaction)
        {
            IDbCommand command = Database.PrepareCommand(SqlSelectAll);

            IDataReader dataReader = command.ExecuteReader(CommandBehavior.SingleRow);
            dtCustomerDetailTransaction.Load(dataReader);
            return dtCustomerDetailTransaction.Rows.Count;
        }

        #endregion

        #region Clear

        public static int Clear(CustomerTransaction transaction)
        {
            IDbCommand dbCommand = Database.PrepareCommand(SqlClearByTransaction);

            Database.PutParameter(dbCommand, "@SessionId", transaction.SessionId);
            Database.PutParameter(dbCommand, "@BusinessTransactionId", transaction.BusinessTransactionId);

            return dbCommand.ExecuteNonQuery();
        }

        #endregion

    }
}
