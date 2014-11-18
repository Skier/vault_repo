package com.llsvc.maps.events
{
import flash.events.Event;
	
public class LayerEvent extends Event
{
    public var layer:Object;

    public static const ADD_LAYER:String = "addLayer";
    public static const REMOVE_LAYER:String = "removeLayer";
    public static const MOVE_LAYER_UP:String = "moveLayerUp";
    public static const MOVE_LAYER_DOWN:String = "moveLayerDown";
    public static const MOVE_LAYER_TOP:String = "moveLayerTop";
    public static const MOVE_LAYER_BOTTOM:String = "moveLayerBottom";
    
    public function LayerEvent(type:String, layer:Object,
            bubbles:Boolean=false, cancelable:Boolean=false)
    {
        this.layer = layer;
        super(type, bubbles, cancelable);
    }
 
    override public function clone():Event 
    {
        return new LayerEvent(type, layer, bubbles, cancelable);
    }

}
}