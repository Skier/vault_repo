package com.titus.catalog.model
{
	import com.titus.catalog.storage.IStorage;
	
	import mx.collections.ArrayCollection;
	
	
public class PagesCache
{

	public var startingNumber:int = 0;
	
	private var _pages:ArrayCollection = new ArrayCollection();
	public function get pages():ArrayCollection {
		return _pages;
	}
	
	private var _storage:IStorage;
	
	public function PagesCache(storage:IStorage)
	{
		_storage = storage;
	}

}

}
