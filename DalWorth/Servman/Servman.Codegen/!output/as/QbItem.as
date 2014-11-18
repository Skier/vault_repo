
package com.dalworth.servman.domain
{
    [Bindable]
    [RemoteClass(alias="Servman.Domain.QbItem")]
    public class QbItem
    {
        public function QbItem()
        {
        }
      
        private var _listId:String;
        public function get listId():String { return _listId; }
        public function set listId(value:String):void 
        {
            _listId = value;
        }
      
        private var _isActive:Boolean;
        public function get isActive():Boolean { return _isActive; }
        public function set isActive(value:Boolean):void 
        {
            _isActive = value;
        }
      
        private var _assetAccountName:String;
        public function get assetAccountName():String { return _assetAccountName; }
        public function set assetAccountName(value:String):void 
        {
            _assetAccountName = value;
        }
      
        private var _avgCost:Number;
        public function get avgCost():Number { return _avgCost; }
        public function set avgCost(value:Number):void 
        {
            _avgCost = value;
        }
      
        private var _description:String;
        public function get description():String { return _description; }
        public function set description(value:String):void 
        {
            _description = value;
        }
      
        private var _parentItemName:String;
        public function get parentItemName():String { return _parentItemName; }
        public function set parentItemName(value:String):void 
        {
            _parentItemName = value;
        }
      
        private var _name:String;
        public function get name():String { return _name; }
        public function set name(value:String):void 
        {
            _name = value;
        }
      
        private var _manPartNum:String;
        public function get manPartNum():String { return _manPartNum; }
        public function set manPartNum(value:String):void 
        {
            _manPartNum = value;
        }
      
        private var _prefVendorName:String;
        public function get prefVendorName():String { return _prefVendorName; }
        public function set prefVendorName(value:String):void 
        {
            _prefVendorName = value;
        }
      
        private var _purchaseCost:Number;
        public function get purchaseCost():Number { return _purchaseCost; }
        public function set purchaseCost(value:Number):void 
        {
            _purchaseCost = value;
        }
      
        private var _purchaseDesc:String;
        public function get purchaseDesc():String { return _purchaseDesc; }
        public function set purchaseDesc(value:String):void 
        {
            _purchaseDesc = value;
        }
      
        private var _qtyOnHand:Number;
        public function get qtyOnHand():Number { return _qtyOnHand; }
        public function set qtyOnHand(value:Number):void 
        {
            _qtyOnHand = value;
        }
      
        private var _qtyOnHandSpecified:Boolean;
        public function get qtyOnHandSpecified():Boolean { return _qtyOnHandSpecified; }
        public function set qtyOnHandSpecified(value:Boolean):void 
        {
            _qtyOnHandSpecified = value;
        }
      
        private var _qtyOnPurchaseOrder:Number;
        public function get qtyOnPurchaseOrder():Number { return _qtyOnPurchaseOrder; }
        public function set qtyOnPurchaseOrder(value:Number):void 
        {
            _qtyOnPurchaseOrder = value;
        }
      
        private var _qtyOnPurchaseOrderSpecified:Boolean;
        public function get qtyOnPurchaseOrderSpecified():Boolean { return _qtyOnPurchaseOrderSpecified; }
        public function set qtyOnPurchaseOrderSpecified(value:Boolean):void 
        {
            _qtyOnPurchaseOrderSpecified = value;
        }
      
        private var _qtyOnSales:Number;
        public function get qtyOnSales():Number { return _qtyOnSales; }
        public function set qtyOnSales(value:Number):void 
        {
            _qtyOnSales = value;
        }
      
        private var _qtyOnSalesSpecified:Boolean;
        public function get qtyOnSalesSpecified():Boolean { return _qtyOnSalesSpecified; }
        public function set qtyOnSalesSpecified(value:Boolean):void 
        {
            _qtyOnSalesSpecified = value;
        }
      
        private var _reorderPoint:Number;
        public function get reorderPoint():Number { return _reorderPoint; }
        public function set reorderPoint(value:Number):void 
        {
            _reorderPoint = value;
        }
      
        private var _reorderPointSpecified:Boolean;
        public function get reorderPointSpecified():Boolean { return _reorderPointSpecified; }
        public function set reorderPointSpecified(value:Boolean):void 
        {
            _reorderPointSpecified = value;
        }
      
        private var _taxable:Boolean;
        public function get taxable():Boolean { return _taxable; }
        public function set taxable(value:Boolean):void 
        {
            _taxable = value;
        }
      
        private var _taxableSpecified:Boolean;
        public function get taxableSpecified():Boolean { return _taxableSpecified; }
        public function set taxableSpecified(value:Boolean):void 
        {
            _taxableSpecified = value;
        }
      
        private var _type:String;
        public function get type():String { return _type; }
        public function set type(value:String):void 
        {
            _type = value;
        }
      
        private var _typeSpecified:Boolean;
        public function get typeSpecified():Boolean { return _typeSpecified; }
        public function set typeSpecified(value:Boolean):void 
        {
            _typeSpecified = value;
        }
      
        private var _unitPrice:Number;
        public function get unitPrice():Number { return _unitPrice; }
        public function set unitPrice(value:Number):void 
        {
            _unitPrice = value;
        }
      
        private var _uOMAbbr:String;
        public function get uOMAbbr():String { return _uOMAbbr; }
        public function set uOMAbbr(value:String):void 
        {
            _uOMAbbr = value;
        }
      
        private var _uOMName:String;
        public function get uOMName():String { return _uOMName; }
        public function set uOMName(value:String):void 
        {
            _uOMName = value;
        }
      
    }
}
      