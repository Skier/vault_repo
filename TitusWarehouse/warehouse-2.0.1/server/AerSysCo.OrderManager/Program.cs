using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using AerSysCo.Entity;
using AerSysCo.Common;
using AerSysCo.Warehouse;
using AerSysCo.MacPac;
using AerSysCo.CustomerCenter;
using log4net;

namespace AerSysCo.OrderManager
{
class Program
{

static void Main(string[] args)
{
    log4net.Config.XmlConfigurator.Configure();
    Program Iam = new Program();
    try {
        List<Brand> brands = (List<Brand>)TransactionHelper.QueryTransaction(Iam, "GetBrands");

        if ( args.Length != 1 ) {
            ShowUsage(brands);
            return;
        }

        String brandCode = null;
        if ( "ALL".Equals(args[0].ToUpper()) ) {
            brandCode = "ALL";
        } else {
            if ( !CheckParam(args[0], brands) ) {
                Console.WriteLine("Unknown brand '"+args[0]+"'");
                ShowUsage(brands);
                return;
            }
            brandCode = args[0].ToUpper();
            Brand toDo = null;
            foreach(Brand b in brands) {
                if ( b.code.ToUpper().Equals(brandCode) ) {
                    toDo = b;
                    break;
                }
            }
            brands.Clear();
            brands.Add(toDo);
        }

        List<Order> orders = (List<Order>)TransactionHelper.QueryTransaction(Iam, "GetOrders");

        foreach( Order order in orders ) {
            try {
                TransactionHelper.QueryTransaction(Iam, "UpdateOrder", order);
            } catch (Exception ex ) {
                Logger.GetSysLogger().Error("Cannot update status for order '"+order.orderId+"' due to error "+ex.Message, ex);
            }
        }
        Logger.GetSysLogger().Info("Orders synchronization done");
    } catch (Exception ex) {
        Logger.GetSysLogger().Fatal("Orders Synchronization fail", ex);
    }
}

private Program() {
}

private List<Order> GetOrders(SqlTransaction tran) {
    OrderFilter filter = new OrderFilter();
    filter.statusId = Entity.OrderStatus.SUBMITTED;
    return OrderSvc.GetOrders(tran, filter);
}

private List<Brand> GetBrands(SqlTransaction tran) {
    return BrandSvc.GetAll(tran);
}

private void UpdateOrder(SqlTransaction tran, Order order) {
    MacPacOrder o = MacPacService.GetOrderStatus(order.customer.MACPACCustonerNumber, order.PONumber, order.dateCreated);
    switch ( o.status ) {
        case MacPac.OrderStatus.UNKNOWN :
             string msg = string.Format("MACPAC: An order for customer '{0}' with PO '{1}' do not found", 
                                        order.customer.MACPACCustonerNumber, order.PONumber);
             Logger.GetAppLogger().Info(msg);
             break;
        case MacPac.OrderStatus.PENDING :
             string msg1 
                 = string.Format("RENDING: OrderId {0}  MACPACOrderNum {1}", order.orderId, o.orderNumber);
             Logger.GetAppLogger().Info(msg1);
             break;
        case MacPac.OrderStatus.OPEN :
             string msg2 
                 = string.Format("OPEN: OrderId {0}  MACPACOrderNum {1}", order.orderId, o.orderNumber);

             CloseOrder(tran, order, o);
             msg2 = "INV UPDATED: " + msg2;
             Logger.GetAppLogger().Info(msg2); 
             break;
        case MacPac.OrderStatus.CLOSED :
             string msg3
                 = string.Format("CLOSED: OrderId {0}  MACPACOrderNum {1}", order.orderId, o.orderNumber);

             CloseOrder(tran, order, o);
             msg3 = "INV UPDATED: " + msg3;
             Logger.GetAppLogger().Info(msg3);
             break;
        default : 
             Logger.GetSysLogger().Fatal("Unknown macpac order status");
             break;
    }
}

private void CloseOrder(SqlTransaction tran, Order order, MacPacOrder mOrder) {
    OrderSvc.MakOrderClosed(tran, order.orderId, mOrder.orderNumber, mOrder.shippingDate, mOrder.releaseNumber, mOrder.trackingNumber);
    List<OrderDetail> details = OrderDatailSvc.GetByOrderId(tran, order.orderId);
    foreach(OrderDetail detail in details) {
        Inventory inv = InventorySvc.FindByItemWarehouse(tran, detail.itemId, order.warehouseId);
        inv.qty = inv.qty - detail.qty;
        InventorySvc.Update(tran, inv);
    }
} 

private static void ShowUsage(List<Brand> brands) {
    Console.Write("Use: ALL");
    foreach ( Brand brand in brands ) {
        Console.Write("|"+brand.code);
    }
    Console.WriteLine("");
}

private static bool CheckParam(String param, List<Brand> brands) {
    foreach (Brand brand in brands) {
        if ( param.ToUpper().Equals(brand.code.ToUpper()) ) {
            return true;
        }
    }
    return false;
}


}
}
