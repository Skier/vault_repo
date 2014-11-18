package {
    import flash.display.Bitmap;
    import flash.display.Loader;
    import flash.events.*;
    import flash.net.URLRequest;
    
    import mx.core.UIComponent;

    public class BitmapExample extends UIComponent {
        private var url:String = "assets/titus/__00001";

        public function BitmapExample() {
            configureAssets();
        }

        private function configureAssets():void {
            var loader:Loader = new Loader();
            loader.contentLoaderInfo.addEventListener(Event.COMPLETE, completeHandler);
            loader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, ioErrorHandler);

            var request:URLRequest = new URLRequest(url);
            loader.load(request);
        }

        private function completeHandler(event:Event):void {
            var loader:Loader = Loader(event.target.loader);
            var image:Bitmap = Bitmap(loader.content);
            image.smoothing = true;
            width = image.bitmapData.width;
            height = image.bitmapData.height;
            addChild(image);
            
            dispatchEvent(new Event(Event.COMPLETE)); 
        }
        
        private function ioErrorHandler(event:IOErrorEvent):void {
            trace("Unable to load image: " + url);
        }
    }
}