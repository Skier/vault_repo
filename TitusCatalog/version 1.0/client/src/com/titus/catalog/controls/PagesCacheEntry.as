package com.titus.catalog.controls
{
	
public class PagesCacheEntry
{
	
	private var _key:*;
	public function get key():* {
		_accessTime = PagesCache.getAccessCounter();
		return _key;
	}
	
	private var _accessTime:int;
	public function get accessTime():int {
		return _accessTime;
	}
	
	public function PagesCacheEntry()
	{
	}

}

}
