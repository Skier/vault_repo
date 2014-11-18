package com.titus.catalog.model.search
{
	import mx.collections.ArrayCollection;
	
[Bindable]
public class SearchResultPackage
{
	public var searchString:String;
	public var sections:ArrayCollection; /* of SearchResultSection */

	public function SearchResultPackage()
	{
		sections = new ArrayCollection();
	}

}
}