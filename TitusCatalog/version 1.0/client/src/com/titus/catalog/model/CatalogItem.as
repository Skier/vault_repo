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
	}
	public function get Name():String {
		return _Name;
	}
	
	public function debugName():void {
		name = /*"[" + _PageCode + PdfPage(DBStorage.getInstance().getCatalogPages().getItemAt(PageNumber)).number.toString() + "] " +*/ _Name;
	}
	
	public var Color1:String;
	
	public var Color2:String;
	
	public var Color3:String;
	
	public var Color4:String;
	
	public var PageNumber:int;
	
	private var _PageCode:String;
	public function set PageCode(value:String):void {
		_PageCode = value;
	}
	public function get PageCode():String {
		return _PageCode;
	}
	
    public var Description:String;

    public var Information:String;
    
    public var IsBranch:Boolean = true;
    
    public var IsModelItem:Boolean;

	public function set SubItems(value:Array):void {
		numChildren = 0;
		for each (var item:CatalogItem in value) {
			item.parentItem = this;
			children.addItem(item);
			numChildren += item.numChildren;
		}
	}
	
	public var name:String;
	public var children:ArrayCollection;
	public var parentItem:CatalogItem;
	
	public var numChildren:int;
	
	public function CatalogItem(parentItem:CatalogItem = null)
	{
		this.parentItem = parentItem;
		this.children = new ArrayCollection();
	}

}

}
