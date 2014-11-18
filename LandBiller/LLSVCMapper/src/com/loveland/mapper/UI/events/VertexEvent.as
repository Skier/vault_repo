package com.loveland.mapper.UI.events
{
import com.loveland.mapper.markers.VertexMarker;

import flash.events.Event;

public class VertexEvent extends Event
{
    public static const DELETE:String = "deleteVertex";
    public static const CHANGE:String = "changeVertex";

	public var vertex:VertexMarker;

    public function VertexEvent(type:String, vertex:VertexMarker, 
       bubbles:Boolean=true, cancelable:Boolean=false)
    {
        this.vertex = vertex;
        super(type);
    }
            
    override public function clone():Event
    {
        return new VertexEvent(type, vertex, bubbles, cancelable);
    }

}
}