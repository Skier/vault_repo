using System;

namespace AerSysCo.Entity {

public class Item : Traceable {
    public int itemId = 0;
    public string sku = "";
    public string description = "";
    public double width = 0.0;
    public double length = 0.0;
    public double height = 0.0;
    public double weight = 0.0;
    public int qtyIncrement = 1;
    public bool isActive = false;

}; 
};