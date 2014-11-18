package com.titus.catalog.model
{
	import mx.collections.ArrayCollection;
	

[Bindable]
[RemoteClass(alias="Titus.ECatalog.Entity.CatalogItem")]
public class CatalogItem
{

	public var Id:int;
	
	public var Sort:int;
	
	private var _Name:String;
	public function set Name(value:String):void {
		_Name = value;
		name = _Name + " " + _PageCode;
	}
	
	public var Color1:String;
	
	public var Color2:String;
	
	public var Color3:String;
	
	public var Color4:String;
	
	public var PageNumber:int;
	
	private var _PageCode:String;
	public function set PageCode(value:String):void {
		_PageCode = value;
		name = _Name + " " + _PageCode;
	}
	public function get PageCode():String {
		return _PageCode;
	}
	
    public var Description:String;

    public var Information:String;
    
    public var IsBranch:Boolean = true;

	public function set SubItems(value:Array):void {
		numChildren = 0;
		for each (var item:CatalogItem in value) {
			children.addItem(item);
			numChildren += item.numChildren;
		}
	}
	
	public var name:String;
	public var children:ArrayCollection;
	public var parentItem:CatalogItem;
	
	public var numChildren:int;
	
	public function CatalogItem()
	{
		children = new ArrayCollection();
	}

}

}
