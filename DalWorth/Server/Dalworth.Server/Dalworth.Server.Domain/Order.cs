using System;
using System.Collections.Generic;
using System.Data;
using Dalworth.Server.Data;
using Dalworth.Server.Domain.ServmanSync;
using Dalworth.Server.SDK;
using Dalworth.Server.Servman.Domain;

namespace Dalworth.Server.Domain
{
    public class CustomerNotReadyException : Exception
    {

    }


    public partial class Order
    {
        public Order(){ }

        #region ServmanCustomerId

        private string m_servmanCustomerId;
        public string ServmanCustomerId
        {
            get { return m_servmanCustomerId; }
            set { m_servmanCustomerId = value; }
        }

        #endregion

        #region FindByScheduleDate 

    private const string SqlSelectByScheduleDate = SqlSelectAll + " where scheduledate > ?ScheduleDate";
    private static List<Order> FindByScheduleDate(DateTime scheduleDate, IDbConnection connection= null )
    {
        using (IDbCommand dbCommand = Database.PrepareCommand(SqlSelectByScheduleDate, connection))
        {
            List<Order> rv = new List<Order>();

            Database.PutParameter(dbCommand, "?ScheduleDate", scheduleDate);
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

        #region UpdateOrders 

        public static void UpdateCurrentOrders(DateTime fromScheduleDate)
        {
            Host.Trace("Sync", "UpdateCurrentOrders Started");

            var ordersInDb = FindByScheduleDate(fromScheduleDate);
            var ordersInServman = FindServmanOrders(fromScheduleDate);

            int i = 0;
            foreach (var orderInDb in ordersInDb)
            {
                i++;
                Host.Trace("Sync", "Updating order " + orderInDb.TicketNumber + " " + i + "/" + ordersInDb.Count + "--------", 
                    HostTraceLevelEnum.Debug);
                var orderInServman = ordersInServman.Find(order => order.TicketNumber == orderInDb.TicketNumber);
                if (orderInServman == null)
                    continue;

                ordersInServman.Remove(orderInServman);
                orderInServman.CustomerId = orderInDb.CustomerId;
                orderInServman.OrderSourceId = orderInDb.OrderSourceId;

                try
                {
                    Update(orderInServman);
                }
                catch (Exception ex)
                {
                    Host.Trace("Sync", "Failed to update Order " + orderInServman.TicketNumber + " " + ex);
                    Host.SendErrorEmail("Failed to update Order " + orderInServman.TicketNumber + " " + ex);
                }
            }

            var missingOrders = string.Empty;

            foreach (var orderInServman in ordersInServman)
            {
                if (DateTime.Now.Subtract(orderInServman.ScheduleDate).Minutes > 10 && !orderInServman.TicketNumber.StartsWith("9"))
                {
                    missingOrders += orderInServman.TicketNumber + ",";
                }
            }

            if (missingOrders != string.Empty)
            {
                Host.Trace("Sync", "Missing orders " + missingOrders);
                Host.SendErrorEmail("Missing Orders " + missingOrders);
            }

            Host.Trace("Sync", "UpdateCurrentOrders Completed");
        }

    #endregion

        #region InsertUpdateOrder

    public static void InsertUpdateOrder(string ticketNumber)
        {
            Order order = null;
            try
            {                
                order = FindByPrimaryKey(ticketNumber);
            }
            catch (DataNotFoundException){}

            Order servmanOrder = null;
            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection(ConnectionKeyEnum.Servman))
            {
                connection.Open();
                servmanOrder = FindServmanOrder(ticketNumber, connection);
            }
             
            if (servmanOrder == null)
            {
                Host.Trace("Digium", string.Format("Servman order import Ticket {0} not found in h_order", ticketNumber), HostTraceLevelEnum.Debug);
                throw new DataNotFoundException("Servman ticket is not found.  Ticket Number" + ticketNumber);
            }
                
            servmanOrder.TicketNumber = servmanOrder.TicketNumber.Trim();

            if (order != null)
            {
                servmanOrder.OrderSourceId = order.OrderSourceId;
                servmanOrder.CustomerId = order.CustomerId;                
                Update(servmanOrder);
                return;
            }
            
            try
            {
                servmanOrder.CustomerId = Customer.FindBy(servmanOrder.ServmanCustomerId).ID;
            }
            catch (DataNotFoundException)
            {
                custmast custmast;
                using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection(ConnectionKeyEnum.Servman))
                {
                    connection.Open();
                    custmast = custmast.FindByPrimaryKey(servmanOrder.ServmanCustomerId, connection);
                }

                if (custmast == null)
                    throw new DataNotFoundException("Customer is missing, customer id: " + servmanOrder.ServmanCustomerId);

                Customer customer = ImportModel.ImportCustomer(custmast);
                servmanOrder.CustomerId = customer.ID;                        
            }

            Insert(servmanOrder);
        }

        #endregion

        #region FindServmanOrder

        private const String SqlFindServmanOrder =
            @" SELECT ho.ticket_num, ho.cust_id, ho.Date, ho.serv_type, ho.tran_type, 
                 ho.tran_stat, ho.comp_type, ho.amount, 
                cc.trans_num, cc.d_complete, cc.ad_source, 
                hduct.trans_num hduct_trans_num, hduct.d_complete hduct_d_complete, hduct.ad_source hduct_ad_source, 
                hcomm_cc.trans_num hcomm_cc_trans_num, hcomm_cc.d_complete hcomm_cc_d_complete, hcomm_cc.ad_source hcomm_cc_ad_source 
                From [H_ORDER] ho 
                left join [hresi_cc] cc on ho.ticket_num = cc.ticket_num 
                left join [hduct] on ho.ticket_num = hduct.ticket_num 
                left join [hcomm_cc] on ho.ticket_num = hcomm_cc.ticket_num ";

        private const string SqlFindServmanOrderByTicketNumber = SqlFindServmanOrder + "  where ho.ticket_num = ?";
               
        private static Order FindServmanOrder(string ticketNumber, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindServmanOrderByTicketNumber, connection))
            {
                Database.PutParameter(dbCommand, "@ticket_num", ticketNumber);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return LoadServmanOrder(dataReader);                        
                }
            }

            return null;
        }
        
        #endregion

        #region FindServmanOrders

        private static List<Order> FindServmanOrders(DateTime fromScheduleDate)
        {
            var sql = SqlFindServmanOrder + " where  ho.Date > {^" + fromScheduleDate.ToString("yyyy-MM-dd") + "} and ho.serv_type in (1,2,5)";
            var result = new List<Order>();

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection(ConnectionKeyEnum.Servman))
            {
                connection.Open();
                using (IDbCommand dbCommand = Database.PrepareCommand(sql, connection))
                {
                    using (IDataReader dataReader = dbCommand.ExecuteReader())
                    {
                        while(dataReader.Read())
                            result.Add(LoadServmanOrder(dataReader));
                    }
                }
            }
           
            return result;
        }

        #endregion

        #region FindLinkedTicket

        public static string FindLinkedTicket(string ticketNumber, IDbConnection connection)
        {
            Order order;

            try
            {
                order = FindByPrimaryKey(ticketNumber, connection);
            }
            catch (DataNotFoundException)
            {
                return string.Empty;
            }

            if (!string.IsNullOrEmpty(order.TransNum))
                return order.TransNum;

            Order transOrder = FindByTransNum(ticketNumber, connection);
            if (transOrder != null)
                return transOrder.TicketNumber;            

            return string.Empty;
        }

        #endregion

        #region FindByTransNum

        private const String SqlFindByTransNum =
            @"SELECT * FROM `order`
                where TransNum = ?TransNum";

        private static Order FindByTransNum(string transNum, IDbConnection connection)
        {
            using (IDbCommand dbCommand = Database.PrepareCommand(SqlFindByTransNum, connection))
            {
                Database.PutParameter(dbCommand, "?TransNum", transNum);

                using (IDataReader dataReader = dbCommand.ExecuteReader())
                {
                    if (dataReader.Read())
                        return Load(dataReader);
                }
            }

            return null;
        }

        #endregion

        #region LoadServmanOrder

        private static Order LoadServmanOrder(IDataReader dataReader)
        {
            var ticketNumber = dataReader.GetString(0);
            var servmanCustomerId = dataReader.GetString(1);
            var scheduleDate = dataReader.GetDateTime(2);
            var serviceType = (int) dataReader.GetDecimal(3);
            var transactionType = (int) dataReader.GetDecimal(4);
            var transactionStatus = (int) dataReader.GetDecimal(5);
            var completionType = (int) dataReader.GetDecimal(6);
            var amount = dataReader.GetDecimal(7);

            var transactionNumber = string.Empty;
            DateTime? datecomplete = null;
            var adSource = string.Empty;
           
            switch (serviceType)
            {
                case 1: // carpet cleaning
                    transactionNumber = dataReader.IsDBNull(8) ? string.Empty : dataReader.GetString(8);
                    datecomplete = dataReader.IsDBNull(9) ? (DateTime?) null : dataReader.GetDateTime(9);
                    adSource = dataReader.IsDBNull(10) ? string.Empty : dataReader.GetString(10);
                    break;
                case 2: // duct cleaning
                    transactionNumber = dataReader.IsDBNull(11) ? string.Empty : dataReader.GetString(11);
                    datecomplete = dataReader.IsDBNull(12) ? (DateTime?) null : dataReader.GetDateTime(12);
                    adSource = dataReader.IsDBNull(13) ? string.Empty : dataReader.GetString(13);
                    break;
                case 5: // commercial
                    transactionNumber = dataReader.IsDBNull(14) ? string.Empty : dataReader.GetString(14);
                    datecomplete = dataReader.IsDBNull(15) ? (DateTime?) null : dataReader.GetDateTime(15);
                    adSource = dataReader.IsDBNull(16) ? string.Empty : dataReader.GetString(16);
                    break;
            }
            transactionNumber = transactionNumber.Trim();
            adSource = adSource.Trim();

            int? adSourceId = null;

            if (!string.IsNullOrEmpty(adSource))
                    adSourceId = int.Parse(adSource);

            var order = new Order(ticketNumber: ticketNumber, customerId: 0, orderSourceId: null,
                                  advertisingSourceId: adSourceId, scheduleDate: scheduleDate,
                                  transNum: transactionNumber, serviceType: serviceType,
                                  transactionType: transactionType, transactionStatus: transactionStatus,
                                  completionType: completionType, dateCompleted: datecomplete, amount: amount);

            order.ServmanCustomerId = servmanCustomerId;
            
            if (order.DateCompleted.HasValue && order.DateCompleted.Value == Utils.SERVMAN_NULL_DATE)
                order.DateCompleted = null;
            return order;
        }

        #endregion

        #region SetNewOrdersSources

        public static void SetNewOrdersSources()
        {
            string lastTransactionId = Transaction.FindLastAssignedOrderSourceToOrder();            
            
            List<Transaction> transactionsToProcess = Transaction.FindLatestTransactions(
                lastTransactionId, DateTime.Now.AddMinutes(-Configuration.DigiumTransactionImportDelayMin));

            foreach (Transaction transaction in transactionsToProcess)
            {
                if (Transaction.IsFirstTransactionForTicket(transaction))
                {
                    Order order = Order.FindByPrimaryKey(transaction.TicketNumber);
                    DigiumLogItem call = DigiumLogItem.FindByPrimaryKey(transaction.DigiumLogItemId.Value);
                    order.OrderSourceId = call.CallSourceId;
                    Order.Update(order);
                }
            }
        }

        #endregion
    }
}
      
