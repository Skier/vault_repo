package com.titus.catalog.storage
{
	
	import com.titus.catalog.model.CatalogItem;
	import com.titus.catalog.model.CatalogItemPackage;
	import com.titus.catalog.model.CatalogLocation;
	import com.titus.catalog.model.Submittal;
	import com.titus.catalog.model.search.SearchResultPackage;
	import com.titus.catalog.view.pdf.BitmapPdfPage;
	
	import mx.collections.ArrayCollection;
	
public interface IStorage
{
	
//	function getCart():ArrayCollection;
//	function addToCart(item:Submittal):void;
//	function removeFromCart(item:Submittal):void;
//	function isInCart(item:Submittal):Boolean;
//	function clearCart():void;
//	function prepareCartPackage():void;
	
	function getStorageURL():String;
	
	function getCatalog():CatalogItem;
	function getCatalogItemPackage(catalogItem:CatalogItem):CatalogItemPackage;
	function getCatalogPages():ArrayCollection;
	function getPage(key:String):BitmapPdfPage;
	
	function findPageByCode(code:String):CatalogLocation;
	function findCodeByPage(page:int):String;
	
	function searchModel(modelName:String, exactMatch:Boolean, caseSensitive:Boolean):ArrayCollection;
	function search(searchString:String):SearchResultPackage;
	
	function addEventListener(type:String, listener:Function, useCapture:Boolean = false, priority:int = 0, useWeakReference:Boolean = false):void;
	
}

}
