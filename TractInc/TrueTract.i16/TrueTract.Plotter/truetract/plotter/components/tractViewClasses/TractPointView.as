package truetract.plotter.components.tractViewClasses
{
	import truetract.plotter.utils.GeoPosition;
	
	import flash.geom.Rectangle;
	
	import mx.styles.CSSStyleDeclaration;
	import mx.styles.StyleManager;
	
	[Style(name="bgColor", type="Number", format="Color", inherit="no")]
	[Style(name="bgRollOverColor", type="Number", format="Color", inherit="no")]
	[Style(name="borderColor", type="Number", format="Color", inherit="no")]
	[Style(name="borderRollOverColor", type="Number", format="Color", inherit="no")]
	[Style(name="width", type="Nmber", format="Number", inherit="no")]
				
	public class TractPointView extends ControlPointView
	{

        private static var classConstructed:Boolean = classConstruct();
    
        private static function classConstruct():Boolean {
        	
            if (!StyleManager.getStyleDeclaration("TractPointView")) {
                var newStyleDeclaration:CSSStyleDeclaration = new CSSStyleDeclaration();
                newStyleDeclaration.setStyle("backgroundColor", 0xFFCC00);
                newStyleDeclaration.setStyle("borderColor", 0x666666);
                newStyleDeclaration.setStyle("backgroundRollOverColor", 0xFF0000);
                newStyleDeclaration.setStyle("borderRollOverColor", 0x666666);
                newStyleDeclaration.setStyle("width", 8);
                                
                StyleManager.setStyleDeclaration("TractPointView", newStyleDeclaration, true);
            }
            
            return true;
        }
		
		public function TractPointView(position:GeoPosition) {
		    super(position);
		}
		
        override public function invalidateProperties():void {
		    super.invalidateProperties();
		    
            invalidateDisplayList();
        }
        
		override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
	    {
            graphics.clear();
            
            pointWidth = getStyle("width");

            if (_phase == "rolledOver"){
    			graphics.beginFill(getStyle("backgroundRollOverColor"));
                graphics.lineStyle(0, getStyle("borderRollOverColor"));
	            pointWidth += 2;
            } else {
    			graphics.beginFill(getStyle("backgroundColor"));
                graphics.lineStyle(0, getStyle("borderColor"));
            }

          	graphics.drawRect((pointWidth / 2 * -1), (pointWidth / 2 * -1), pointWidth, pointWidth);
            graphics.endFill();
	    }

	}
}