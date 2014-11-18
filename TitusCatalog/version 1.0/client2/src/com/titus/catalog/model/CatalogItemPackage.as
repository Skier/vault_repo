package com.titus.catalog.model
{
	import mx.collections.ArrayCollection;
	
[Bindable]
public class CatalogItemPackage
{
	
	public var catalogItem:CatalogItem;
	
	public var submittals:ArrayCollection;
	
	public var videos:ArrayCollection;
	
	public function CatalogItemPackage()
	{
		submittals = new ArrayCollection();
		videos = new ArrayCollection();
	}

}
}