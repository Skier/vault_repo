package truetract.plotter.components
{
    import flash.display.DisplayObject;
    import flash.display.Graphics;
    import flash.display.Sprite;
    import flash.events.Event;
    import flash.events.MouseEvent;
    import flash.geom.Point;
    
    import mx.core.EventPriority;
    import mx.core.UIComponent;
    import mx.managers.CursorManager;

    [Event(name="rotationAreaMouseDown", type="mx.events.Event")]
    [Event(name="rotationAreaRollOut", type="mx.events.Event")]
    [Event(name="rotationAreaRollOver", type="mx.events.Event")]
            
    public class ObjectControlFrame extends UIComponent
    {
        private var rotationHitArea:UIComponent;
        
        override protected function createChildren():void {
            super.createChildren();
            
            if (!rotationHitArea) {
                rotationHitArea = new UIComponent();
                addChild(rotationHitArea);
                
                rotationHitArea.addEventListener(MouseEvent.MOUSE_DOWN, rotationHitArea_mouseDownHandler);
                rotationHitArea.addEventListener(MouseEvent.MOUSE_OVER, rotationHitArea_rollOverHandler);
                rotationHitArea.addEventListener(MouseEvent.MOUSE_OUT, rotationHitArea_rollOutHandler);
            }
        }
        
        override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void {
            super.updateDisplayList(unscaledWidth, unscaledHeight);
            
            if (parent) {
                this.setActualSize(parent.width, parent.height);
            }

            var controlPoint:Point = new Point(width + 10, 0);
                        
            graphics.clear();
            graphics.lineStyle(1, 0xbfd8e4);
            graphics.drawRect(0,0, width, height);
            graphics.moveTo(width, 0);
            graphics.lineTo(controlPoint.x - 4, controlPoint.y);
            
            graphics.lineStyle(6, 0x00CC00);
            
            graphics.drawCircle(controlPoint.x, controlPoint.y, 1);

            rotationHitArea.x = controlPoint.x - 8;
            rotationHitArea.y = controlPoint.y - 8;
            rotationHitArea.width = 12;
            rotationHitArea.height = 12;

            var g:Graphics = rotationHitArea.graphics;
            g.clear();
            g.beginFill(0, 0);
            g.drawRect(0, 0, rotationHitArea.width, rotationHitArea.height);
            g.endFill();
        }
        
        protected function rotationHitArea_mouseDownHandler(event:MouseEvent):void {
            event.stopImmediatePropagation();
            dispatchEvent(new Event("rotationAreaMouseDown"));
        }

        protected function rotationHitArea_rollOverHandler(event:MouseEvent):void {
            event.stopPropagation();
            event.preventDefault();
            event.stopImmediatePropagation();

            dispatchEvent(new Event("rotationAreaRollOver"));
        }

        protected function rotationHitArea_rollOutHandler(event:MouseEvent):void {
            event.stopImmediatePropagation();

            dispatchEvent(new Event("rotationAreaRollOut"));
        }
        
    }
}