package com.titus.catalog.controls
{
	import com.rubenswieringa.book.Book;
	import com.rubenswieringa.book.Page;
	import com.titus.catalog.view.pdf.BitmapPdfPage;
	
	import flash.display.DisplayObject;
	import flash.events.Event;
	
	import mx.collections.ArrayCollection;
	import mx.containers.Box;
	import mx.containers.VBox;
	import mx.core.ScrollPolicy;
	import mx.events.CollectionEvent;
	import com.titus.catalog.events.PageEvent;

	public class PdfBook extends VBox
	{
		
		[Event(name="complete", type="flash.events.Event")]

		[Event(name="pageNumberChanged", type="flash.events.PageEvent")]

		public var book:Book;
		
		public function PdfBook()
		{
			horizontalScrollPolicy = ScrollPolicy.OFF;
			verticalScrollPolicy = ScrollPolicy.OFF;
			Event.CHANGE
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
		
		private var leftPageLoaded:Boolean = false;
		private var rightPageLoaded:Boolean = false;
		
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
    		book.horizontalScrollPolicy = ScrollPolicy.OFF;
    		book.verticalScrollPolicy = ScrollPolicy.OFF;
    		book.liveBitmapping = true;
    			
			book.addEventListener("currentPageChanged", currentPageChangeHandler);
			
			for (var i:int = 0; i < 8; i++) 
			{
				var page:Page = new Page();
				page.width = this.width / 2;
				page.height = this.height;
				page.verticalScrollPolicy = ScrollPolicy.OFF;
				page.horizontalScrollPolicy = ScrollPolicy.OFF;
				page.setStyle("backgroundColor", 0xFFFFFF);
				page.alpha = 1;
				
				var container:Box = new Box();
				container.width = page.width;
				container.height = page.height;
				container.setStyle("backgroundColor", 0xFFFFFF);
				container.setStyle("backgroundAlpha", 1);
				
				page.addChild(container);

				book.addChild(page);
			}
			
			addChild(book);

			updatePaging();
		}

		public function updatePaging():void
		{
			trace("10 PdfBook:updatePaging");

			trace("11 PdfBook:updatePaging set Flipping to true");
			_isFlipping = true;
			
			refillPages();
			
			var currentPage:int = getCurrentPage();
			
			_oldCurrentPage = currentPage; 
			
			callLater(book.gotoPageWithoutFlip, [currentPage]);
			
			callLater(setFlipping, [false]);
		}
		
		public function refillPages():void 
		{
			var images:ArrayCollection = _pagesProvider.getPages(pageNumber);
			
			for (var i:int = 0; i < 8; i++) 
			{
				var bitmap:BitmapPdfPage = BitmapPdfPage(images[i]);
				bitmap.verticalScrollPolicy = ScrollPolicy.OFF;
				bitmap.horizontalScrollPolicy = ScrollPolicy.OFF;
				if (bitmap.isLoaded) {
					bitmap.scaleX = (this.unscaledWidth / 2) / bitmap.image.width;
					bitmap.scaleY = this.unscaledHeight / bitmap.image.height;
				}
				
				var page:Page = book.pages[i] as Page;
				
				var ch:Array = page.getChildren();
				for each (var o:Object in ch) 
				{
					if (o is Box) 
					{
						Box(o).removeAllChildren();
						Box(o).addChild(images[i] as DisplayObject);
					}
				}
			}
		}
		
		private function setFlipping(value:Boolean):void 
		{
			trace("10-1 PdfBook:setFlipping set Flipping to " + value.toString());
			_isFlipping = value;
			
			dispatchEvent(new PageEvent("pageNumberChanged", pageNumber));
		}

		private var _isFlipping:Boolean = false;
		
		private function currentPageChangeHandler(event:Event):void 
		{
			trace("1 PdfBook:currentPageChangeHandler");
			trace("2 PdfBook:currentPageChangeHandler - book.currentPage=" + book.currentPage);
			if (_isFlipping) {
				trace("3 PdfBook:currentPageChangeHandler - Flipping is true");
				
				// dispatchEvent(new PageEvent("pageNumberChanged", pageNumber));
				
				return;
			}
			
			trace("4 PdfBook:currentPageChangeHandler - _oldCurrentPage=" + _oldCurrentPage);
			trace("5 PdfBook:currentPageChangeHandler - book.currentPage=" + book.currentPage);
			var currentPageSaved:int = book.currentPage;
			if (_oldCurrentPage > book.currentPage) {
				_direction = PagesProvider.DIRECTION_BACKWARD;
				pageNumber = pageNumber - 2;
			} else if (_oldCurrentPage < book.currentPage) {
				_direction = PagesProvider.DIRECTION_FORWARD;
				pageNumber = pageNumber + 2;
			}

			callLater(switchFlipEnable);
			
			// dispatchEvent(new PageEvent("pageNumberChanged", pageNumber));
		}
		
		private function onPageBitmapCompleteHandler(event:PageLoadEvent):void 
		{
			event.bitmap.scaleX = ((this.width / 2) / this.scaleX ) / (event.bitmap.image.width);
			event.bitmap.scaleY = (this.height / this.scaleY) / (event.bitmap.image.height);
			
			switchFlipEnable();
		}
		
		private function switchFlipEnable():void 
		{
			var leftPage:Page;
			var rightPage:Page;
			
			if (book.currentPage < 0) 
			{
				leftPage = null;
				leftPageLoaded = true;
			}
			else 
			{
				leftPage = book.pages[book.currentPage] as Page;
				leftPageLoaded = getPageLoadedFlag(leftPage);
			} 
				
			if (book.currentPage > book.pages.length - 2) 
			{
				rightPage = null;
				rightPageLoaded = true;
			}
			else 
			{
				rightPage = book.pages[book.currentPage + 1] as Page;
				rightPageLoaded = getPageLoadedFlag(rightPage);
			} 
			
			if (leftPageLoaded && rightPageLoaded)
				this.book.flippingEnabled = true;
			else 
				this.book.flippingEnabled = false;
		}
		
		private function getPageLoadedFlag(page:Page):Boolean 
		{
			var ch:Array = page.getChildren();
			for each (var o:Object in ch) 
			{
				if (o is Box) 
				{
					var children:Array = o.getChildren();
					for each (var child:Object in children) 
					{
						if (child is BitmapPdfPage) 
						{
							return BitmapPdfPage(child).isLoaded;
						}
					}
				}
			}
			
			return true;
		}
		
		private function getCurrentPage():int {
			var result:int;
			if (pageNumber < 3) {
				result = pageNumber;
			} else if (pageNumber + 5 > dataProvider.length) {
				result = 8 - (dataProvider.length - pageNumber);
			} else {
				result = 3;
			}
			return result;
		}
		
		public function prevPage():void {
			if (_isFlipping)
				return;
			_isFlipping = true;
			this.book.flippingEnabled = true;
			this.book.prevPage();
			_isFlipping = false;
		}
		
		public function nextPage():void {
			if (_isFlipping)
				return;
			_isFlipping = true;
			this.book.flippingEnabled = true;
			this.book.nextPage();
			_isFlipping = false;
		}
		
	}
	
}
