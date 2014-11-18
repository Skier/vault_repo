package com.titus.catalog.view.pdf
{
	import flash.display.Bitmap;
	import flash.display.Loader;
	import flash.display.PixelSnapping;
	import flash.events.IOErrorEvent;
	import flash.events.ProgressEvent;
	import flash.net.URLRequest;
	import flash.system.LoaderContext;
	import flash.system.Security;
	
	import mx.containers.Canvas;
	import mx.controls.ProgressBar;
	import mx.core.UIComponent;
	
	[Event(name="complete", type="flash.events.Event")]

	public class BitmapPdfPage extends Canvas
	{
		private var progressBar:ProgressBar;
		
		public function BitmapPdfPage() 
		{
			super();
			
			initLoader();
		}
		
		private function initLoader():void {
			loader = new Loader();
            loader.contentLoaderInfo.addEventListener(Event.COMPLETE, completeHandler);
            loader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, ioErrorHandler);
            loader.contentLoaderInfo.addEventListener(ProgressEvent.PROGRESS, progressHandler);
		}

		private var loader:Loader;
		
		[Bindable]
		public var image:UIComponent;

		private var _imageUrl:String;
		[Bindable] 
		public function get imageUrl():String {	return _imageUrl; }
		public function set imageUrl(value:String):void 
		{
			_imageUrl = value;
			isLoading = false;
		}
		
		public var isLoading:Boolean = false;
		public var isLoaded:Boolean = false;
		
        public function load():void {
            var request:URLRequest = new URLRequest(imageUrl);
            
            try {
            	loader.load(request, new LoaderContext(true));
            } catch (err:Error) {
            }

    		if (_bitmap != null) {
    			_bitmap.bitmapData.dispose();
    			_bitmap = null;
    		}

            isLoaded = false;
            isLoading = true;

			createProgressBar();
        }
        
        public function unload():void 
        {
        	if (isLoaded) 
        	{
        		if (null != _bitmap) {
        			_bitmap.bitmapData.dispose();
        			_bitmap = null;
        		}

        		removeAllChildren();
        	}
        	else if (isLoading) 
        	{
        		removeProgressBar();
        		loader.close();
        	}
        	
        	isLoaded = false;
        	isLoading = false;	
        }
        
        private function createProgressBar():void {
        	if (null != progressBar) {
        		removeProgressBar();
        	}
        	
        	progressBar = new ProgressBar();
        	progressBar.width = 100;
        	progressBar.x = 110; // this.width / 2 - progressBar.width / 2;
        	progressBar.y = 200; // this.height / 2 - progressBar.height / 2;
        	
        	progressBar.labelPlacement = "bottom";
        	// progressBar.label = "Loading...";
        	progressBar.mode = "manual";
        	this.addChild(progressBar);
        	
        	if (null != loader.loaderInfo) {
        		progressBar.setProgress(loader.loaderInfo.bytesLoaded, loader.loaderInfo.bytesTotal);
        	}
        }
        
        private function removeProgressBar():void {
        	if (null != progressBar) {
        		this.removeChild(progressBar);
        		progressBar = null;
        	}
        }
        
        private var _bitmap:Bitmap;

        private function completeHandler(event:Event):void {
        	removeProgressBar();
        	isLoading = false;
        	isLoaded = true;

            var loader:Loader = Loader(event.target.loader);
            _bitmap = Bitmap(loader.content);
            _bitmap.smoothing = true;
            _bitmap.pixelSnapping = PixelSnapping.ALWAYS;

            image = new UIComponent();
            image.width = _bitmap.width;
            image.height = _bitmap.height;
            image.addChild(_bitmap);
            
            clipContent = true;
            
            addChildAt(image, 0);
            
            dispatchEvent(new Event(Event.COMPLETE)); 
        }
        
        private function progressHandler(event:ProgressEvent): void {
        	if (null != progressBar) {
        		progressBar.setProgress(event.bytesLoaded, event.bytesTotal);
        	}
        }
        
        private function ioErrorHandler(event:IOErrorEvent):void {
            trace("Unable to load image: " + imageUrl);
        }
        
	}
}