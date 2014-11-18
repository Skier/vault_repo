using System;
using System.Collections.Generic;

namespace AerSysCo.Entity
{

public class CatalogItem
{
    public ModelItem modelItem = null;
    public Item item = null;
    public List<Inventory> inventories = new List<Inventory>();
    public string modelName;
    public int customerId;
}

}
