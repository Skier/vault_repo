package com.titus.catalog.model.search
{
	import mx.collections.ArrayCollection;
	
[Bindable]
public class SearchResultSection
{
	public var name:String;
	public var pages:ArrayCollection;
	
	public function SearchResultSection()
	{
		pages = new ArrayCollection(); /* of SearchResultPage */
	}

}
}