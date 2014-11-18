package com.titus.catalog.controls
{
	import com.rubenswieringa.book.Book;
	import com.rubenswieringa.book.Page;
	import com.titus.catalog.model.PdfPage;
	import com.titus.catalog.view.pdf.BitmapPdfPage;
	
	import flash.events.Event;
	
	import mx.collections.ArrayCollection;
	import mx.containers.Canvas;

	public class BookWrapper extends Canvas
	{
		[Bindable] public var book:Book
		public var dataProvider:ArrayCollection = new ArrayCollection();
		
		private var _pageNumber:int = -1;
		[Bindable]
		public function get pageNumber():int { return _pageNumber; }
		public function set pageNumber(value:int):void 
		{
			_pageNumber = value;
//			reloadBook();
		}  
		
		private var oldCurrentPage:int = 0;
		
		public function BookWrapper()
		{
			super();
			
			callLater(initBook);
		}
		
		private function initBook():void 
		{
			book = new Book();
			book.width = 640;
			book.height = 416;
			
			book.autoFlipDuration = 600;
			book.easing = 0.7;
			book.regionSize = 150;
			book.sideFlip = true;
			book.hardCover = false;
			book.hover = true;
			book.snap = false;
			book.flipOnClick = true;

			for (var i:int = 0; i < 8; i++) 
			{
				var page:Page = new Page();
					page.setStyle("backgroundAlpha", 1);
					page.width = book.width / 2;
					page.height = book.height;

				var bitmap:BitmapPdfPage = new BitmapPdfPage();
					bitmap.imageUrl = PdfPage(dataProvider.getItemAt(i)).imageUrl;
					bitmap.addEventListener(Event.COMPLETE, onPageBitmapCompleteHandler);
//					bitmap.load();
					
				page.addChild(bitmap);
				book.addChild(page);
			}
			
			this.addChild(book);

			book.addEventListener("currentPageChanged", currentPageChangeHandler);
			
			oldCurrentPage = book.currentPage;
			
			loadImages();
		}
		
		private function loadImages():void 
		{
			var bitmap3:BitmapPdfPage = BitmapPdfPage(Page(book.pages[3]).getChildAt(0));
				bitmap3.load(0);
			var bitmap4:BitmapPdfPage = BitmapPdfPage(Page(book.pages[4]).getChildAt(0));
				bitmap4.load(0);
			var bitmap5:BitmapPdfPage = BitmapPdfPage(Page(book.pages[5]).getChildAt(0));
				bitmap5.load(1);
			var bitmap6:BitmapPdfPage = BitmapPdfPage(Page(book.pages[6]).getChildAt(0));
				bitmap6.load(1);
			var bitmap1:BitmapPdfPage = BitmapPdfPage(Page(book.pages[1]).getChildAt(0));
				bitmap1.load(2);
			var bitmap2:BitmapPdfPage = BitmapPdfPage(Page(book.pages[2]).getChildAt(0));
				bitmap2.load(2);
			var bitmap7:BitmapPdfPage = BitmapPdfPage(Page(book.pages[7]).getChildAt(0));
				bitmap7.load(3);
			var bitmap0:BitmapPdfPage = BitmapPdfPage(Page(book.pages[0]).getChildAt(0));
				bitmap0.load(3);
		}
		
		private function reloadBook():void 
		{
			if ( pageNumber < 3 || pageNumber > (dataProvider.length - 4) ) 
			{
				oldCurrentPage = book.currentPage;
				return;
			}
			
			for (var i:int = 0; i < 8; i++) 
			{
				var bitmap:BitmapPdfPage = Page(book.pages[i]).getChildAt(0) as BitmapPdfPage;
					bitmap.imageUrl = PdfPage(dataProvider.getItemAt(pageNumber - 3 + i)).imageUrl;
//					bitmap.load();
			}
			
			oldCurrentPage = 3;
			book.gotoPageWithoutFlip(3);
			
			loadImages();
		}
		
		private function currentPageChangeHandler(event:Event):void 
		{
			if (oldCurrentPage == book.currentPage)
				return;
				
			if (oldCurrentPage < book.currentPage) {
				pageNumber += 2;
			} else if (oldCurrentPage > book.currentPage) {
				pageNumber -= 2;
			}
			
			reloadBook();
		}
		
		private function onPageBitmapCompleteHandler(event:Event):void 
		{
			BitmapPdfPage(event.target).image.scaleX = (book.width / 2) / BitmapPdfPage(event.target).image.width;
			BitmapPdfPage(event.target).image.scaleY = book.height / BitmapPdfPage(event.target).image.height;
		}
			
	}
}