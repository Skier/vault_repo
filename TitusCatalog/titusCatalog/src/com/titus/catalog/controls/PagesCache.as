package com.titus.catalog.controls
{
    import com.titus.catalog.storage.DBStorage;
    import com.titus.catalog.view.pdf.BitmapPdfPage;
    
    import flash.events.Event;
    import flash.events.EventDispatcher;
    
    import mx.collections.ArrayCollection;
    

public class PagesCache
    extends EventDispatcher
{
    
    [Event(name="complete", type="com.titus.catalog.controls.PageLoadEvent")]
    
    private static var _accessCounter:int = 0;
    public static function getAccessCounter():int {
        return _accessCounter++;
    }
    
    private const CACHE_SIZE:int = 6;
    
    private var _cacheSize:int;
    public function get cacheSize():int {
        return _cacheSize;
    }

    private var _hash:Array;
    private var loadedPages:ArrayCollection;
    
    public function PagesCache(cacheSize:int = CACHE_SIZE)
    {
        this._cacheSize = cacheSize;
        
        _hash = new Array();
        loadedPages = new ArrayCollection();
    }

    public function getPage(key:*, loadImmediately:Boolean = true):BitmapPdfPage {
        var bitmap:BitmapPdfPage;

        if (key == null) {
            return null;
        } else {
            bitmap = getPageByUrl(key);

            if (null == bitmap) {
                bitmap = DBStorage.getInstance().getPage(key);

                if (loadImmediately) {
                    bitmap.load();
                }
                
                bitmap.addEventListener(Event.COMPLETE, onLoadCompleteHandler);
                
                cleanUp();
            }
        }

        return bitmap;
    }
    
    private function cleanUp():void 
    {
        if (loadedPages
        		&& (loadedPages.length > CACHE_SIZE)) 
        {
        	var bitmap:BitmapPdfPage = BitmapPdfPage(loadedPages.getItemAt(0));
       		bitmap.unload();
            loadedPages.removeItemAt(0);
            cleanUp();
        }
    }

    private function getPageByUrl(url:String):BitmapPdfPage 
    {
        for each (var page:BitmapPdfPage in loadedPages) 
        {
            if (page.imageUrl == url)
                return page;
        }

        return null;
    }
    
    public function getPages(keys:ArrayCollection):ArrayCollection { 
        var result:ArrayCollection = new ArrayCollection();
        for each (var key:* in keys) {
            result.addItem(getPage(key));
        }
        return result;
    }
    
    public function getPagesByPriorities(keys:ArrayCollection, priorities:ArrayCollection):ArrayCollection {
        if (keys.length != priorities.length) {
            throw new Error("Number of keys must be equal to number of priority values.");
        }
        
        var result:ArrayCollection = new ArrayCollection();
        var i:int;
        for (i = 0; i < keys.length; i ++) {
            result.addItem(null);
        }
        
        var itemsHash:Array = new Array();
        for (i = 0; i < priorities.length; i ++) {
            var items:ArrayCollection = itemsHash[priorities[i]];
            if (null == items) {
                items = new ArrayCollection();
                itemsHash[priorities[i]] = items;
            }
            
            var page:BitmapPdfPage = getPage(keys[i], false);
            
            result.setItemAt(page, i);
            
            if (null != page) {
                items.addItem(page);
            }
        }
        
        var queueItems:ArrayCollection = new ArrayCollection();
        var alreadyProcessed:Array = new Array();
        for (i = 0; i < priorities.length; i++) {
            if (null == alreadyProcessed[priorities[i]]) {
                var item:PagesQueueItem = new PagesQueueItem();
                item.priority = priorities[i];
                item.pages = ArrayCollection(itemsHash[priorities[i]]);
                queueItems.addItem(item);
                alreadyProcessed[priorities[i]] = item;
            }
        }
        
        var queue:PagesQueue = new PagesQueue(queueItems);
        
        return result;
    }
    
    private function onLoadCompleteHandler(evt:Event):void {
        var bitmap:BitmapPdfPage = BitmapPdfPage(evt.currentTarget);
        
        loadedPages.addItem(bitmap);
                
        dispatchEvent(new PageLoadEvent(Event.COMPLETE, bitmap));
    }
    
}

}