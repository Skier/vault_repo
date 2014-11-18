package com.titus.catalog.storage
{
	
	import com.titus.catalog.model.CatalogItem;
	import com.titus.catalog.model.CatalogItemPackage;
	import com.titus.catalog.model.search.SearchResultPackage;
	
	import mx.collections.ArrayCollection;
	
public interface IStorage
{
	
	function getCatalog():CatalogItem;
	function getCatalogItemPackage(catalogItem:CatalogItem):CatalogItemPackage;
	function getCatalogPages():ArrayCollection;
	function findPageByCode(code:String):int;
	function searchModel(modelName:String, exactMatch:Boolean, caseSensitive:Boolean):ArrayCollection;
	function search(searchString:String):SearchResultPackage;
	function addEventListener(type:String, listener:Function, useCapture:Boolean = false, priority:int = 0, useWeakReference:Boolean = false):void;
	
}

}
