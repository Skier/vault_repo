package com.loveland.mapper.UI.events
{
import flash.events.Event;

public class ToolBarEvent extends Event
{
    public static const MAPPOINT_ACTIVATE:String = "mappointActivate";
    public static const POLYLINE_ACTIVATE:String = "polylineActivate";
    public static const POLYGON_ACTIVATE:String = "polygonActivate";
    public static const DEACTIVATE:String = "deActivate";

    public function ToolBarEvent(type:String, 
       bubbles:Boolean=true, cancelable:Boolean=false)
    {
        super(type);
    }
            
    override public function clone():Event
    {
        return new ToolBarEvent(type, bubbles, cancelable);
    }

}
}