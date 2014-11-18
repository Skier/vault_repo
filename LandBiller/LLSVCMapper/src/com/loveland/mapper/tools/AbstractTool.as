package com.loveland.mapper.tools
{
    import com.afcomponents.umap.events.OverlayEvent;
    
    import flash.events.EventDispatcher;
    import flash.events.MouseEvent;
    
    public class AbstractTool extends EventDispatcher
    {

        public function AbstractTool() {
        }
        
        [Bindable] public var icon:Class;
        [Bindable] public var title:String = "";
        [Bindable] public var description:String = "";

        public function activate():void {
            throw new Error("This method must be overriden.");
        }
        
        public function deactivate():void {
            throw new Error("This method must be overriden.");
        }
        
        public function mouseDownHandler(event:OverlayEvent):void {
        }

        public function mouseMoveHandler(event:MouseEvent):void {
        }
        
        public function mouseUpHandler(event:OverlayEvent):void {
        }
        
    }
}    
