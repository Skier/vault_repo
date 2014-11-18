package com.titus.catalog.view.pdf
{
	import flash.display.Bitmap;
	import flash.display.Loader;
	import flash.display.PixelSnapping;
	import flash.events.HTTPStatusEvent;
	import flash.events.IOErrorEvent;
	import flash.events.ProgressEvent;
	import flash.net.URLRequest;
	import flash.system.LoaderContext;
	
	import mx.containers.Canvas;
	import mx.controls.ProgressBar;
	import mx.controls.Text;
	import mx.core.UIComponent;
	
	[Event(name="complete", type="flash.events.Event")]

	public class BitmapPdfPage extends Canvas
	{
		private var progressBar:ProgressBar;
		private var errorLabel:Text;
		
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
		
        public function load():void 
        {
            var request:URLRequest = new URLRequest(imageUrl);
            
            initLoader();
            
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

        	hideErrorMessage();
			createProgressBar();
        }
        
        public function unload():void 
        {
        	if (isLoaded) 
        	{
        		if (null != _bitmap) {
        			_bitmap.bitmapData.dispose();
        			//loader.unload();
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
        	
        	this.scaleX = 1;
        	this.scaleY = 1;

        	hideErrorMessage();
        }
        
        private function createProgressBar():void {
        	if (null != progressBar) {
        		removeProgressBar();
        	}
        	
        	progressBar = new ProgressBar();
        	progressBar.width = 100;
        	progressBar.labelPlacement = "bottom";
        	progressBar.mode = "manual";
        	addChild(progressBar);
        	
        	callLater(adjustProgressBar, []);
        	
        	if (null != loader.loaderInfo) {
        		progressBar.setProgress(loader.loaderInfo.bytesLoaded, loader.loaderInfo.bytesTotal);
        	}
        }
        
        private function adjustProgressBar():void {
			progressBar.x = this.width / 2 - progressBar.width / 2;
			progressBar.y = this.height / 2 - progressBar.height / 2;
        }
        
        private function removeProgressBar():void {
        	if ((null != progressBar)
        			&& this.contains(progressBar)) {
        		this.removeChild(progressBar);
        		progressBar = null;
        	}
        }
        
        private function addErrorLabel():void 
        {
        	if (null != errorLabel) {
        		removeErrorLabel();
        	}
        	
        	errorLabel = new Text();
        	errorLabel.percentWidth = 100;
        	errorLabel.setStyle("color", 0xff0000);
        	
        	addChild(errorLabel);
        }
        
        private function removeErrorLabel():void 
        {
        	if ((errorLabel != null)
        			&& (0 <= this.getChildIndex(errorLabel))) {
        		this.removeChild(errorLabel);
        		errorLabel = null;
        	}
        }
        
        private function showErrorMessage(message:String):void 
        {
        	if (errorLabel == null) 
        		addErrorLabel();
        	
        	errorLabel.text = message;
        	errorLabel.visible = true;
        }
        
        private function hideErrorMessage():void 
        {
        	if (errorLabel == null)
        		return;
        	
        	errorLabel.text = "";
        	errorLabel.visible = false;
        } 
        
        private var _bitmap:Bitmap;

        private function completeHandler(event:Event):void {
        	removeAllChildren();
        	
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
            
            addChild(image);
            
            dispatchEvent(new Event(Event.COMPLETE)); 
        }
        
        private function progressHandler(event:ProgressEvent): void 
        {
        	if (null != progressBar) {
        		progressBar.setProgress(event.bytesLoaded, event.bytesTotal);
        	}
        }
        
        private function ioErrorHandler(event:IOErrorEvent):void 
        {
        	isLoaded = false;
        	isLoading = false;	

        	removeProgressBar();
        	showErrorMessage("Unable to load image: " + imageUrl + "\n" + event.text);
        }
        
        private function httpStatusHandler(event:HTTPStatusEvent):void 
        {
        	isLoaded = false;
        	isLoading = false;	

        	removeProgressBar();
        	showErrorMessage("Unable to load image: " + imageUrl + "\n" + event.type);
        }
        
	}
}