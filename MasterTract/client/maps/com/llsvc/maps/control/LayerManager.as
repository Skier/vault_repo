package com.llsvc.maps.control
{
	import com.llsvc.maps.events.LayerEvent;
	import com.llsvc.maps.layers.BaseLayer;
	import com.llsvc.maps.layers.DynLayer;
	
	import flash.display.DisplayObject;
	
	import mx.collections.ArrayCollection;
	import mx.containers.Canvas;
	import mx.core.Application;
	
	public class LayerManager
	{
        private static var _container:Canvas;
        
        [Bindable]
        public static var allLayers:ArrayCollection = new ArrayCollection();
        
        [Bindable]
        public static var selectedLayers:ArrayCollection = new ArrayCollection();

        public static function setContainer(container:Canvas):void
        {
            _container = container;
        }
        
        public static function addLayer(layer:BaseLayer):void 
        {
			if (layer.isSelected) 
				selectLayer(layer);
        	
        	allLayers.addItem(layer);
        }
        
        public static function selectLayer(layer:BaseLayer):void 
        {
        	layer.addEventListener(LayerEvent.MOVE_LAYER_UP, moveLayerUpHandler);
        	layer.addEventListener(LayerEvent.MOVE_LAYER_DOWN, moveLayerDownHandler);
        	layer.addEventListener(LayerEvent.MOVE_LAYER_TOP, moveLayerTopHandler);
        	layer.addEventListener(LayerEvent.MOVE_LAYER_BOTTOM, moveLayerBottomHandler);

        	layer.isSelected = true;

        	_container.addChild(layer);
        	selectedLayers.addItemAt(layer, 0);
        	
  			DisplayObject(Application.application).dispatchEvent(new Event("layers_changed"));
        }
        
        public static function deselectLayer(layer:BaseLayer):void 
        {
        	layer.removeEventListener(LayerEvent.MOVE_LAYER_UP, moveLayerUpHandler);
        	layer.removeEventListener(LayerEvent.MOVE_LAYER_DOWN, moveLayerDownHandler);
        	layer.removeEventListener(LayerEvent.MOVE_LAYER_TOP, moveLayerTopHandler);
        	layer.removeEventListener(LayerEvent.MOVE_LAYER_BOTTOM, moveLayerBottomHandler);

        	layer.isSelected = false;

        	if (_container.getChildIndex(layer) > -1)
        		_container.removeChild(layer);

			var index:int = selectedLayers.getItemIndex(layer);
			if (index > -1) {
	        	selectedLayers.removeItemAt(index);
			}

  			DisplayObject(Application.application).dispatchEvent(new Event("layers_changed"));
        }
        
        public static function removeLayer(layer:BaseLayer):void 
        {
        	if ( layer.isSelected ) {
        		deselectLayer(layer);
        	}
        	
			var index:int = allLayers.getItemIndex(layer);
			if (index > -1) {
	        	allLayers.removeItemAt(index);
			}
        }
        
        public static function removeDynLayers():void {
        	for each(var layer:BaseLayer in allLayers) {
        		if ( layer is DynLayer ) {
        			removeLayer(layer);
        		}
        	}	
        }
        
        public static function moveLayerUp(layer:BaseLayer):void 
        {
        	var index:int;
        	index = _container.getChildIndex(layer);
        	if (index < (_container.getChildren().length - 1) && index > -1) 
        	{
	        	if (_container.getChildIndex(layer) > -1)
    	    		_container.removeChild(layer);
        		_container.addChildAt(layer, (index + 1));
        	}
        	
        	index = selectedLayers.getItemIndex(layer);
        	if (index > 0 ) 
        	{
	        	selectedLayers.removeItemAt(index);
	        	selectedLayers.addItemAt(layer, (index - 1));
        	}
        }
        
        public static function moveLayerDown(layer:BaseLayer):void 
        {
        	var index:int;
        	index = _container.getChildIndex(layer);
        	if (index > 0) 
        	{
	        	if (_container.getChildIndex(layer) > -1)
    	    		_container.removeChild(layer);
        		_container.addChildAt(layer, (index - 1));
        	}
        	
        	index = selectedLayers.getItemIndex(layer);
        	if (index < (selectedLayers.length - 1)) 
        	{
	        	selectedLayers.removeItemAt(index);
	        	selectedLayers.addItemAt(layer, (index + 1));
        	}
        }
        
        public static function moveLayerTop(layer:BaseLayer):void 
        {
        	var index:int
        	index = _container.getChildIndex(layer);
        	if (index > -1) 
        	{
	        	if (_container.getChildIndex(layer) > -1)
    	    		_container.removeChild(layer);
        		_container.addChildAt(layer, _container.getChildren().length);
        	}
        	
        	index = selectedLayers.getItemIndex(layer);
        	selectedLayers.removeItemAt(index);
        	selectedLayers.addItemAt(layer, 0);
        }
        
        public static function moveLayerBottom(layer:BaseLayer):void 
        {
        	var index:int;
        	index = _container.getChildIndex(layer);
        	if (index > -1) 
        	{
	        	if (_container.getChildIndex(layer) > -1)
    	    		_container.removeChild(layer);
        		_container.addChildAt(layer, 0);
        	}
        	
        	index = selectedLayers.getItemIndex(layer);
        	selectedLayers.removeItemAt(index);
        	selectedLayers.addItemAt(layer, selectedLayers.length);
        }
        
        public static function moveLayerUpHandler(event:LayerEvent):void 
        {
        	var layer:BaseLayer = event.layer as BaseLayer;
        	moveLayerUp(layer);
        }
    
        public static function moveLayerDownHandler(event:LayerEvent):void 
        {
        	var layer:BaseLayer = event.layer as BaseLayer;
        	moveLayerDown(layer);
        }
    
        public static function moveLayerTopHandler(event:LayerEvent):void 
        {
        	var layer:BaseLayer = event.layer as BaseLayer;
        	moveLayerTop(layer);
        }
    
        public static function moveLayerBottomHandler(event:LayerEvent):void 
        {
        	var layer:BaseLayer = event.layer as BaseLayer;
        	moveLayerBottom(layer);
        }

	}
}