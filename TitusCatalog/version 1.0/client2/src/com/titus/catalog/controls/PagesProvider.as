package com.titus.catalog.controls
{
	import com.titus.catalog.model.PdfPage;
	
	import mx.collections.ArrayCollection;
	
public class PagesProvider
{
	
/*	private static const PRIORITY_MATRIX_RIGHT:ArrayCollection = new ArrayCollection([5, 3, 3, 1, 1, 2, 2, 4]);
	private static const PRIORITY_MATRIX_LEFT:ArrayCollection = new ArrayCollection([4, 2, 2, 1, 1, 3, 3, 5]);*/
	
	private static const PRIORITY_MATRIX_RIGHT:ArrayCollection = new ArrayCollection([0, 0, 0, 1, 1, 0, 0, 0]);
	private static const PRIORITY_MATRIX_LEFT:ArrayCollection = new ArrayCollection([0, 0, 0, 1, 1, 0, 0, 0]);
	
	public static const DIRECTION_FORWARD:int = 0;
	public static const DIRECTION_BACKWARD:int = 1;
	
	private var _cache:PagesCache;
	public function get cache():PagesCache {
		return _cache;
	}
	
	private var pages:ArrayCollection;
	
	public function PagesProvider(pages:ArrayCollection)
	{
		_cache = new PagesCache();
		this.pages = pages;
	}
	
	public function getPages(currentPage:int, direction:int = DIRECTION_FORWARD):ArrayCollection {
		var currentPageFixed:int = (0 == currentPage % 2)? currentPage - 1: currentPage;
		
		var urls:ArrayCollection = new ArrayCollection();
		
		for (var index:int = -3; index < 5; index ++) {
			if ((currentPageFixed + index >= 0)
					&& (currentPageFixed + index < pages.length)) {
				urls.addItem(PdfPage(pages[currentPageFixed + index]).imageUrl);
			} else {
				urls.addItem(null);
			}
		}
		
		return cache.getPagesByPriorities(urls,
			(DIRECTION_FORWARD == direction)? PRIORITY_MATRIX_RIGHT: PRIORITY_MATRIX_LEFT);
	}

}

}
