package com.titus.catalog.model.search
{
	import mx.collections.ArrayCollection;
	
[Bindable]
public class SearchResultPage
{
	public var page:int;
	public var modelItems:ArrayCollection;
	public var searchItems:ArrayCollection;
	
	public function SearchResultPage()
	{
		modelItems = new ArrayCollection(); /* of ModelItem */
		searchItems = new ArrayCollection(); /* of SearchItem */
	}
	
	public function get name():String 
	{
		return "Page: " + page.toString();
	}

}
}