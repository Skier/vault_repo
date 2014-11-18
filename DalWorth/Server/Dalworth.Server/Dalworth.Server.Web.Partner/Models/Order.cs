using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Dalworth.Server.Data;
using Dalworth.Server.Domain;

namespace Dalworth.Server.Web.Partner.Models
{
    public class Order
    {
        public Order()
        {
            OrderCalls = new List<CallLogItem>();
        }

        public Order(List<CallLogItem> orderCalls, string ticketNumber)
        {
            OrderCalls = orderCalls;

            using (IDbConnection connection = Connection.Instance.GetTemporaryDbConnection())
            {
                connection.Open();

                Domain.Order order;
                Customer customer;
                if (OrderCalls.Count > 0)
                {                        
                    order = Domain.Order.FindByPrimaryKey(OrderCalls[0].TicketNumber, connection);
                    customer = Customer.FindByPrimaryKey(order.CustomerId, connection);
                    if (order.OrderSourceId.HasValue)
                    {
                        OrderSource source = Domain.OrderSource.FindByPrimaryKey(
                            order.OrderSourceId.Value, connection);
                        OrderSource = source.Name;
                    }
                }
                else
                {
                    CallLogItem item = new CallLogItem();
                    order = Domain.Order.FindByPrimaryKey(ticketNumber, connection);
                    customer = Customer.FindByPrimaryKey(order.CustomerId, connection);

                    item.TicketNumber = ticketNumber;
                    item.CustomerName = customer.DisplayName;
                    item.ClosedAmount = order.Amount;
                    item.CompType = order.CompletionType;
                    item.TicketStatus = order.TransactionStatus;

                    OrderSource source = null;
                    if (order.AdvertisingSourceId.HasValue)
                        source = Domain.OrderSource.FindByAdSource(order.AdvertisingSourceId.Value, connection);
                    else if (order.OrderSourceId.HasValue)
                        source = Domain.OrderSource.FindByPrimaryKey(order.OrderSourceId.Value, connection);
                    if (source != null)
                        OrderSource = source.Name;

                    OrderCalls.Add(item);
                }

                HomePhone = customer.Phone1Formatted;
                BusinessPhone = customer.Phone2Formatted;
            }
        }

        public string TicketNumber
        {
            get
            {
                if (OrderCalls.Count > 0)
                    return OrderCalls[0].TicketNumber;
                return string.Empty;
            }
        }

        public string ClosedAmountText
        {
            get
            {
                if (OrderCalls.Count > 0 && OrderCalls[0].ClosedAmountText != string.Empty)
                    return OrderCalls[0].ClosedAmountText;
                return 0.ToString("C");
            }
        }

        public string TicketStatus
        {
            get
            {
                if (OrderCalls.Count > 0)
                    return OrderCalls[0].TicketStatusText;
                return string.Empty;
            }
        }

        public string CustomerName
        {
            get
            {
                if (OrderCalls.Count > 0)
                    return OrderCalls[0].CustomerName;
                return string.Empty;
            }            
        }

        public string HomePhone { get; set; }
        public string BusinessPhone { get; set; }

        public string OrderSource { get; set; }
        public List<CallLogItem> OrderCalls { get; set; }
        public string BackLinkQueryString { get; set; }
    }
}