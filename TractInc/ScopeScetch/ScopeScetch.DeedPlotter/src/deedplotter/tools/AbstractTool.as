package src.deedplotter.tools
{
    import flash.events.MouseEvent;
    
    import src.deedplotter.DeedPlotter;
    import src.deedplotter.containers.GeoCanvas;
    
    public class AbstractTool
    {

        public function AbstractTool() {
        }
        
        [Bindable] public var Icon:Class;
        [Bindable] public var Title:String = "";
        [Bindable] public var Description:String = "";

        public var plotter:DeedPlotter;
        
        public function Activate():void {
            throw new Error("This method must be overriden.");
        }
        
        public function Deactivate():void {
            throw new Error("This method must be overriden.");
        }
        
        public function onPlotterMouseDown(event:MouseEvent):void {
        }

        public function onPlotterMouseMove(event:MouseEvent):void {
        }
        
        public function onPlotterMouseUp(event:MouseEvent):void {
        }
        
        protected function isMouseOverCanvas():Boolean 
        {    
            var canvas:GeoCanvas = plotter.drawingSurface;

            return canvas.mouseX > 0 && canvas.mouseX < canvas.width && 
                   canvas.mouseY > 0 && canvas.mouseY < canvas.height;
        }
        
    }
}    
