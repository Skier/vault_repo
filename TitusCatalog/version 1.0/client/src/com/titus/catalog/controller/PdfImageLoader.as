package com.titus.catalog.controller
{
	import com.titus.catalog.model.PdfImageLoadProcess;
	
	import flash.events.Event;
	import flash.events.IOErrorEvent;
	
	import mx.collections.ArrayCollection;
	
	public class PdfImageLoader
	{
        private static var instance:PdfImageLoader;
        
        public static function getInstance():PdfImageLoader 
        {
            if (instance == null)
                instance = new PdfImageLoader();
            
            return instance;
        }
        
        public function PdfImageLoader()
        {
            if (instance != null)
                throw new Error("Singleton!");
        }
        
        private var loadingProcesses:ArrayCollection = new ArrayCollection();
        
        public function load(url:String, priority:int):PdfImageLoadProcess 
        {
        	var loader:PdfImageLoadProcess = new PdfImageLoadProcess();
				loader.url = url;        	
        		loader.priority = priority;
	            loader.contentLoaderInfo.addEventListener(Event.COMPLETE, completeHandler);
	            loader.contentLoaderInfo.addEventListener(IOErrorEvent.IO_ERROR, ioErrorHandler);

        	loadingProcesses.addItem(loader);
        	
        	refreshProcesses();
        	
        	return loader;
        }
        
        private function refreshProcesses():void 
        {
        	var foundZeroProcesses:Boolean = false;
        	var foundLoadingProcesses:Boolean = false;
        	
        	for each (var loader:PdfImageLoadProcess in loadingProcesses) 
        	{
        		if (loader.priority == 0){
        			foundZeroProcesses = true;

        			if (!loader.isLoading) {
	        			loader.loadImage();
        			} else {
        				foundLoadingProcesses = true;
        			}
        		}
        	}
        	
        	if (!foundZeroProcesses && loadingProcesses.length > 0 && !foundLoadingProcesses) 
        	{
        		reducePriority();
        		refreshProcesses();
        	}
        }
        
        private function reducePriority():void 
        {
        	for each (var loader:PdfImageLoadProcess in loadingProcesses) 
        	{
        		if (loader.priority > 0) {
        			loader.priority--;
        		}
        	}
        }
        
        private function completeHandler(event:Event):void 
        {
        	var loader:PdfImageLoadProcess = PdfImageLoadProcess(event.target.loader);
        	var idx:int = loadingProcesses.getItemIndex(loader);
        	if (idx > -1)
        		loadingProcesses.removeItemAt(idx);
        	
        	refreshProcesses();
        }
        
        private function ioErrorHandler(event:IOErrorEvent):void 
        {
        	var loader:PdfImageLoadProcess = PdfImageLoadProcess(event.target.loader);
        	var idx:int = loadingProcesses.getItemIndex(loader);
        	if (idx > -1)
        		loadingProcesses.removeItemAt(idx);
        	
        	refreshProcesses();
        }

	}
}