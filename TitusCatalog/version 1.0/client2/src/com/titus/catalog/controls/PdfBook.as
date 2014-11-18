package com.titus.catalog.controls
{
	import com.rubenswieringa.book.Book;
	import com.rubenswieringa.book.Page;
	import com.titus.catalog.view.pdf.CatalogPanel;
	
	import flash.events.Event;
	
	import mx.collections.ArrayCollection;
	import mx.containers.VBox;
	import mx.events.CollectionEvent;

	public class PdfBook extends VBox
	{
		
		private var book:Book;
		
		public function PdfBook()
		{
		}
		
		[Bindable]
		public function get flippingEnabled():Boolean {
			return this.book.flippingEnabled;
		}
		public function set flippingEnabled(value:Boolean):void {
			this.book.flippingEnabled = value;
		}
		
		private var _dataProvider:ArrayCollection;
		
		private var _pagesProvider:PagesProvider;
		
		private var _pageNumber:int = -1;
		
		private var _oldCurrentPage:int = 0;
		
		private var _direction:int = PagesProvider.DIRECTION_FORWARD;
		
		[Bindable]
		public function get pageNumber():int {
			return _pageNumber;
		}
		public function set pageNumber(value:int):void {
			_pageNumber = (0 == value % 2)? value - 1: value;
			
			updatePaging();
		}
		
		[Bindable]
		public function get dataProvider():ArrayCollection { return _dataProvider; }
		public function set dataProvider(value:ArrayCollection):void 
		{
			_dataProvider = value;
			_dataProvider.addEventListener(CollectionEvent.COLLECTION_CHANGE, dataProviderChangeHandler);
			
			initBook();
		}
		
		private function dataProviderChangeHandler(event:CollectionEvent):void 
		{
			initBook();
		}
		
		private function initBook():void
		{
			_pagesProvider = new PagesProvider(_dataProvider);
			_pagesProvider.cache.addEventListener(Event.COMPLETE, onPageBitmapCompleteHandler);
			
			updatePaging();
		}
		
		private function updatePaging():void
		{
			_isFlipping = true;
			
			removeAllChildren();
						
			book = new Book();
			
			book.width = this.width;
   			book.height = this.height;
    			
    		book.autoFlipDuration = 600;
    		book.easing = 0.7;
    		book.regionSize = 150;
    		book.sideFlip = true;
    		book.hardCover = false;
    		book.hover = true;
    		book.snap = false;
    		book.flipOnClick = true;
    		book.creationPolicy = "auto";
    			
			book.addEventListener("currentPageChanged", currentPageChangeHandler);
			
			var images:ArrayCollection = _pagesProvider.getPages(pageNumber);
			
			for (var i:int = 0; i < 8; i++) 
			{
				if (images.length <= i) {
					break;
				}
				
				if (null == images[i]) {
					continue;
				}
				
				var page:Page = new Page();
				page.setStyle("backgroundAlpha", 1);
				page.setStyle("backgroundColor", 0xFFFFFF);
				
				page.addChild(images[i]);
				
				book.addChild(page);
			}
			
			var currentPage:int = getCurrentPage();
			
			_oldCurrentPage = currentPage; 
			book.openAt = currentPage;
			
			addChild(book);
			
			_isFlipping = false;
		}
		
		private var _isFlipping:Boolean = false;
		
		private function currentPageChangeHandler(event:Event):void 
		{
			if (_isFlipping) {
				return;
			}
			
			var currentPageSaved:int = book.currentPage;
			if (_oldCurrentPage > book.currentPage) {
				_direction = PagesProvider.DIRECTION_BACKWARD;
				pageNumber -= 2;
			} else if (_oldCurrentPage < book.currentPage) {
				_direction = PagesProvider.DIRECTION_FORWARD;
				pageNumber += 2;
			}
			
			// _oldCurrentPage = currentPageSaved;
		}
		
		private function onPageBitmapCompleteHandler(event:PageLoadEvent):void 
		{
			event.bitmap.image.scaleX = (this.width / 2) / event.bitmap.image.width;
			event.bitmap.image.scaleY = this.height / event.bitmap.image.height;
		}
		
		private function getCurrentPage():int {
			var result:int;
			if (pageNumber < 3) {
				result = pageNumber;
			} else {
				result = 3;
			}
			return result;
		}
		
		public function prevPage():void {
			book.prevPage();
		}
		
		public function nextPage():void {
			book.nextPage();
		}
		
	}
	
}
