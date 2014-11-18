using System;
using System.Collections.Generic;
using System.Text;

namespace AerSysCo.Entity
{
public class OrderDetail
{
    public int orderDetailId;
    public int orderId;
    public int itemId;
    public int qty;
    public decimal price;
    public string sku;
    public double multiplier;
    public decimal cost;
    public int lineNumber;
    public int shopingCartDetailId;
    public Item item = null;
    public ShoppingCartDetail shoppingCartDetail = null;
}
}
