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

  public class ShoppingCartDetailCollection
  {
    private var _list:ArrayCollection;
    public function ShoppingCartDetailCollection():void
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
    public function get itemType():ShoppingCartDetail
    {
       return new ShoppingCartDetail();
    }
    public function addItem(item:ShoppingCartDetail):void
    {
       this._list.addItem(item);
    }
    public function addItemAt(item:ShoppingCartDetail,index:int):void
    {
       this._list.addItemAt(item,index);
    }
    public function contains(item:ShoppingCartDetail):Boolean
    {
       return this._list.contains(item);
    }
    public function getItemAt(index:int, prefetch:int = 0):ShoppingCartDetail
    {
       return ShoppingCartDetail(this._list.getItemAt(index,prefetch));
    }
    public function setItemAt(item:ShoppingCartDetail,index:int):ShoppingCartDetail
    {
        return ShoppingCartDetail(this._list.setItemAt(item,index));
    }
    public function getItemIndex(item:ShoppingCartDetail):int
    {
        return this._list.getItemIndex(item);
    }
    public function removeItemAt(index:int):ShoppingCartDetail
    {
       return ShoppingCartDetail(this._list.removeItemAt(index));
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