//*******************************************************************
//  <auto-generated>
//       FleXtense : www.flextense.net
//       Version:1.0 Beta Release
//
//
//       Changes to this file may cause incorrect behavior and will be lost if
//       the code is regenerated.
//  <auto-generated>
//*******************************************************************

 package AerSysCo.Server
{
  import mx.collections.ArrayCollection;
  import mx.collections.IList;
  import AerSysCo.Server.*;

  public class ShipmentShippingOptionsCollection
  {
    private var _list:ArrayCollection;
    public function ShipmentShippingOptionsCollection():void
    {
        this._list = new ArrayCollection();
    }
    public function get count():int
    {
        return this._list.length;
    }
    public function get bindableData():IList
    {
        return this._list.list;
    }
    public function get itemType():ShipmentShippingOptions
    {
       return new ShipmentShippingOptions();
    }
    public function addItem(item:ShipmentShippingOptions):void
    {
       this._list.addItem(item);
    }
    public function addItemAt(item:ShipmentShippingOptions,index:int):void
    {
       this._list.addItemAt(item,index);
    }
    public function contains(item:ShipmentShippingOptions):Boolean
    {
       return this._list.contains(item);
    }
    public function getItemAt(index:int, prefetch:int = 0):ShipmentShippingOptions
    {
       return ShipmentShippingOptions(this._list.getItemAt(index,prefetch));
    }
    public function setItemAt(item:ShipmentShippingOptions,index:int):ShipmentShippingOptions
    {
        return ShipmentShippingOptions(this._list.setItemAt(item,index));
    }
    public function getItemIndex(item:ShipmentShippingOptions):int
    {
        return this._list.getItemIndex(item);
    }
    public function removeItemAt(index:int):ShipmentShippingOptions
    {
       return ShipmentShippingOptions(this._list.removeItemAt(index));
    }
    public function removeAll():void
    {
        return this._list.removeAll();
    }
    public function toArray():Array
    {
      return this._list.toArray();
    }

  }

}
