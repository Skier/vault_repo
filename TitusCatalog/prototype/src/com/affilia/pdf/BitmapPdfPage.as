package com.affilia.pdf
{
	import flash.display.Bitmap;
	import flash.display.Loader;
	import flash.display.PixelSnapping;
	import flash.events.IOErrorEvent;
	import flash.net.URLRequest;
	
	import mx.containers.Canvas;
	import mx.core.UIComponent;
	
	[Event(name="complete", type="flash.events.Event")]

	public class BitmapPdfPage extends Canvas
	{
		public function BitmapPdfPage() 
		{
			super();
			
			loader = new Loader();
            loader.contentLoaderInfo.addEventListener(Event.COMPLETE, completeHandler);
            loader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, ioErrorHandler);
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
			
			reload();
		}
		
        private function reload():void {
            var request:URLRequest = new URLRequest(imageUrl);
            loader.load(request);
        }

        private function completeHandler(event:Event):void {
            var loader:Loader = Loader(event.target.loader);
            var bitmap:Bitmap = Bitmap(loader.content);
            bitmap.smoothing = true;
            bitmap.pixelSnapping = PixelSnapping.ALWAYS;
            image = new UIComponent();
            image.width = bitmap.width;
            image.height = bitmap.height;
            image.addChild(bitmap);
            
            clipContent = true;
            addChildAt(image, 0); 
            
            dispatchEvent(new Event(Event.COMPLETE)); 
        }
        
        private function ioErrorHandler(event:IOErrorEvent):void {
            trace("Unable to load image: " + imageUrl);
        }
	}
}