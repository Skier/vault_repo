
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.QbItem")]
    public class QbItem
    {
        public function QbItem()
        {
        }
      
        private var listId:String;
        public function get ListId():String { return listId; }
        public function set ListId(value:String):void 
        {
            listId = value;
        }
      
        private var isActive:Boolean;
        public function get IsActive():Boolean { return isActive; }
        public function set IsActive(value:Boolean):void 
        {
            isActive = value;
        }
      
        private var assetAccountName:String;
        public function get AssetAccountName():String { return assetAccountName; }
        public function set AssetAccountName(value:String):void 
        {
            assetAccountName = value;
        }
      
        private var avgCost:Number;
        public function get AvgCost():Number { return avgCost; }
        public function set AvgCost(value:Number):void 
        {
            avgCost = value;
        }
      
        private var description:String;
        public function get Description():String { return description; }
        public function set Description(value:String):void 
        {
            description = value;
        }
      
        private var parentItemName:String;
        public function get ParentItemName():String { return parentItemName; }
        public function set ParentItemName(value:String):void 
        {
            parentItemName = value;
        }
      
        private var name:String;
        public function get Name():String { return name; }
        public function set Name(value:String):void 
        {
            name = value;
        }
      
        private var manPartNum:String;
        public function get ManPartNum():String { return manPartNum; }
        public function set ManPartNum(value:String):void 
        {
            manPartNum = value;
        }
      
        private var prefVendorName:String;
        public function get PrefVendorName():String { return prefVendorName; }
        public function set PrefVendorName(value:String):void 
        {
            prefVendorName = value;
        }
      
        private var purchaseCost:Number;
        public function get PurchaseCost():Number { return purchaseCost; }
        public function set PurchaseCost(value:Number):void 
        {
            purchaseCost = value;
        }
      
        private var purchaseDesc:String;
        public function get PurchaseDesc():String { return purchaseDesc; }
        public function set PurchaseDesc(value:String):void 
        {
            purchaseDesc = value;
        }
      
        private var qtyOnHand:Number;
        public function get QtyOnHand():Number { return qtyOnHand; }
        public function set QtyOnHand(value:Number):void 
        {
            qtyOnHand = value;
        }
      
        private var qtyOnHandSpecified:Boolean;
        public function get QtyOnHandSpecified():Boolean { return qtyOnHandSpecified; }
        public function set QtyOnHandSpecified(value:Boolean):void 
        {
            qtyOnHandSpecified = value;
        }
      
        private var qtyOnPurchaseOrder:Number;
        public function get QtyOnPurchaseOrder():Number { return qtyOnPurchaseOrder; }
        public function set QtyOnPurchaseOrder(value:Number):void 
        {
            qtyOnPurchaseOrder = value;
        }
      
        private var qtyOnPurchaseOrderSpecified:Boolean;
        public function get QtyOnPurchaseOrderSpecified():Boolean { return qtyOnPurchaseOrderSpecified; }
        public function set QtyOnPurchaseOrderSpecified(value:Boolean):void 
        {
            qtyOnPurchaseOrderSpecified = value;
        }
      
        private var qtyOnSales:Number;
        public function get QtyOnSales():Number { return qtyOnSales; }
        public function set QtyOnSales(value:Number):void 
        {
            qtyOnSales = value;
        }
      
        private var qtyOnSalesSpecified:Boolean;
        public function get QtyOnSalesSpecified():Boolean { return qtyOnSalesSpecified; }
        public function set QtyOnSalesSpecified(value:Boolean):void 
        {
            qtyOnSalesSpecified = value;
        }
      
        private var reorderPoint:Number;
        public function get ReorderPoint():Number { return reorderPoint; }
        public function set ReorderPoint(value:Number):void 
        {
            reorderPoint = value;
        }
      
        private var reorderPointSpecified:Boolean;
        public function get ReorderPointSpecified():Boolean { return reorderPointSpecified; }
        public function set ReorderPointSpecified(value:Boolean):void 
        {
            reorderPointSpecified = value;
        }
      
        private var taxable:Boolean;
        public function get Taxable():Boolean { return taxable; }
        public function set Taxable(value:Boolean):void 
        {
            taxable = value;
        }
      
        private var taxableSpecified:Boolean;
        public function get TaxableSpecified():Boolean { return taxableSpecified; }
        public function set TaxableSpecified(value:Boolean):void 
        {
            taxableSpecified = value;
        }
      
        private var type:String;
        public function get Type():String { return type; }
        public function set Type(value:String):void 
        {
            type = value;
        }
      
        private var typeSpecified:Boolean;
        public function get TypeSpecified():Boolean { return typeSpecified; }
        public function set TypeSpecified(value:Boolean):void 
        {
            typeSpecified = value;
        }
      
        private var unitPrice:Number;
        public function get UnitPrice():Number { return unitPrice; }
        public function set UnitPrice(value:Number):void 
        {
            unitPrice = value;
        }
      
        private var uOMAbbr:String;
        public function get UOMAbbr():String { return uOMAbbr; }
        public function set UOMAbbr(value:String):void 
        {
            uOMAbbr = value;
        }
      
        private var uOMName:String;
        public function get UOMName():String { return uOMName; }
        public function set UOMName(value:String):void 
        {
            uOMName = value;
        }
      

        public function clone():QbItem
        {
            var result:QbItem = new QbItem();
      
            result.ListId = this.ListId;
      
            result.IsActive = this.IsActive;
      
            result.AssetAccountName = this.AssetAccountName;
      
            result.AvgCost = this.AvgCost;
      
            result.Description = this.Description;
      
            result.ParentItemName = this.ParentItemName;
      
            result.Name = this.Name;
      
            result.ManPartNum = this.ManPartNum;
      
            result.PrefVendorName = this.PrefVendorName;
      
            result.PurchaseCost = this.PurchaseCost;
      
            result.PurchaseDesc = this.PurchaseDesc;
      
            result.QtyOnHand = this.QtyOnHand;
      
            result.QtyOnHandSpecified = this.QtyOnHandSpecified;
      
            result.QtyOnPurchaseOrder = this.QtyOnPurchaseOrder;
      
            result.QtyOnPurchaseOrderSpecified = this.QtyOnPurchaseOrderSpecified;
      
            result.QtyOnSales = this.QtyOnSales;
      
            result.QtyOnSalesSpecified = this.QtyOnSalesSpecified;
      
            result.ReorderPoint = this.ReorderPoint;
      
            result.ReorderPointSpecified = this.ReorderPointSpecified;
      
            result.Taxable = this.Taxable;
      
            result.TaxableSpecified = this.TaxableSpecified;
      
            result.Type = this.Type;
      
            result.TypeSpecified = this.TypeSpecified;
      
            result.UnitPrice = this.UnitPrice;
      
            result.UOMAbbr = this.UOMAbbr;
      
            result.UOMName = this.UOMName;
      
            return result;
        }

        public function updateFields(value:QbItem):void 
        {
            if (value == null)
                value = new QbItem();
      
            this.ListId = value.ListId;
      
            this.IsActive = value.IsActive;
      
            this.AssetAccountName = value.AssetAccountName;
      
            this.AvgCost = value.AvgCost;
      
            this.Description = value.Description;
      
            this.ParentItemName = value.ParentItemName;
      
            this.Name = value.Name;
      
            this.ManPartNum = value.ManPartNum;
      
            this.PrefVendorName = value.PrefVendorName;
      
            this.PurchaseCost = value.PurchaseCost;
      
            this.PurchaseDesc = value.PurchaseDesc;
      
            this.QtyOnHand = value.QtyOnHand;
      
            this.QtyOnHandSpecified = value.QtyOnHandSpecified;
      
            this.QtyOnPurchaseOrder = value.QtyOnPurchaseOrder;
      
            this.QtyOnPurchaseOrderSpecified = value.QtyOnPurchaseOrderSpecified;
      
            this.QtyOnSales = value.QtyOnSales;
      
            this.QtyOnSalesSpecified = value.QtyOnSalesSpecified;
      
            this.ReorderPoint = value.ReorderPoint;
      
            this.ReorderPointSpecified = value.ReorderPointSpecified;
      
            this.Taxable = value.Taxable;
      
            this.TaxableSpecified = value.TaxableSpecified;
      
            this.Type = value.Type;
      
            this.TypeSpecified = value.TypeSpecified;
      
            this.UnitPrice = value.UnitPrice;
      
            this.UOMAbbr = value.UOMAbbr;
      
            this.UOMName = value.UOMName;
      
        }
    }
}
      