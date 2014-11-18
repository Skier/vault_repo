package truetract.plotter.components.tractViewClasses.call
{
    
    import flash.display.Sprite;
    import flash.events.Event;
    import flash.events.MouseEvent;
    
    import mx.core.Application;
    import mx.core.UIComponent;
    import mx.core.UITextField;
    import mx.states.SetStyle;
    import mx.styles.CSSStyleDeclaration;
    import mx.styles.StyleManager;
    
    import truetract.domain.TractCall;
    import truetract.plotter.components.TractView;
    import truetract.plotter.components.tractViewClasses.ControlPointView;
    import truetract.utils.*;

    public class CallView extends UIComponent
    {
        public static const CALL_SHAPE_CLICK_EVENT:String = "call_shape_click";
        
        public static const CALL_SHAPE_DOUBLE_CLICK_EVENT:String = "call_shape_double_click";

        private var annotationStyleNameProp:String = "annotationStyleName";

        public var StartPoint:ControlPointView;

        public var EndPoint:ControlPointView;
        
        protected var callModel:TractCall;
        
		protected var rollPhase:String = RollPhase.OUT;

        protected var highlighted:Boolean;
        
        protected var shape:IGeoShape;

        protected var drawingShape:Sprite;

        protected var upperLabel:UITextField;

        protected var lowerLabel:UITextField;
        
        public var labelIndent:Number = 5;
        
        private static var _classConstructed:Boolean = classConstruct();

        private static function classConstruct():Boolean 
        {
            if (!StyleManager.getStyleDeclaration("CallView")) 
            {
                var newStyleDeclaration:CSSStyleDeclaration = new CSSStyleDeclaration();
                
                newStyleDeclaration.setStyle("lineColor", 0x666666);
                newStyleDeclaration.setStyle("lineRollOverColor", 0xFF0000);

                StyleManager.setStyleDeclaration("CallView", newStyleDeclaration, true);
            }
            
            return true;
        }

        public function CallView(shape:IGeoShape) 
        {
            this.shape = shape;
        }

        public function get Model():TractCall
        {
            return callModel;
        }

        public function set Model(value:TractCall):void 
        {
            callModel = value;
            toolTip = value.Params.GetDisplayString();
        }

        public function get Shape():IGeoShape 
        {
            return shape;
        }
        
        public function set Shape(value:IGeoShape):void 
        {
            shape = value;
            invalidateDisplayList();
        }
        
        public function set Highlighted(value:Boolean):void 
        {
            highlighted = value;
            rollPhase = value ?  RollPhase.OVER : RollPhase.OUT;

            invalidateDisplayList();
        }
        
        public function get Highlighted():Boolean 
        {
            return highlighted;
        }
        
        public function get RelatedTractView():TractView 
        {
            return TractView(parent);
        }
        
        private var _showAnnotations:Boolean = true;
        public function get showAnnotations():Boolean {
            return _showAnnotations;
        }

        public function set showAnnotations(value:Boolean):void {
            _showAnnotations = value;
            
            if (upperLabel) {
                upperLabel.visible = _showAnnotations;
            }
            
            if (lowerLabel) {
                lowerLabel.visible = _showAnnotations;
            }
        }

        public function getLabelMaxIndent():Number 
        {       
            return labelIndent + Math.max( 
                lowerLabel ? lowerLabel.textHeight : 0, 
                upperLabel ? upperLabel.textHeight : 0);
        }
        
        override protected function createChildren():void
        {
            super.createChildren();

            if (!upperLabel) {
    		    upperLabel = createCallAnnotationField();
    		    this.addChild(upperLabel);
            }
            
            if (!lowerLabel) {
    		    lowerLabel = createCallAnnotationField();
    		    this.addChild(lowerLabel);
            }
            
            if (!drawingShape) {
                drawingShape = new Sprite();
                drawingShape.addEventListener(MouseEvent.ROLL_OUT, rollOutHandler);
			    drawingShape.addEventListener(MouseEvent.ROLL_OVER, rollOverHandler);
			    drawingShape.addEventListener(MouseEvent.CLICK, clickHandler);
			    drawingShape.addEventListener(MouseEvent.DOUBLE_CLICK, doubleClickHandler);
			    this.addChild(drawingShape);
            }
        }
    
        override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void
        {
            super.updateDisplayList(unscaledWidth, unscaledHeight);
            
            if (RelatedTractView)
                showAnnotations = RelatedTractView.showCallAnnotations;
        }

        //for testing
        private function drawBoundRectangle():void {
            drawingShape.graphics.lineStyle(0, 0x00FF00);
            
            var rec:BoundRectangle = shape.getBounds();
            
            drawingShape.graphics.drawRect(
                rec.minX - shape.startPosition.Easting,
                shape.startPosition.Northing - rec.maxY, 
                rec.maxX - rec.minX,
                rec.maxY - rec.minY);
        }
    
        private function createCallAnnotationField():UITextField
        {
            var tf:UITextField = new UITextField();

	        var annotationStyleName:String = getStyle(annotationStyleNameProp);

		    tf.mouseEnabled = false;
            tf.visible = showAnnotations;
            tf.embedFonts = true;

	        if (annotationStyleName) {
	            tf.styleName = annotationStyleName;
	        }

            return tf;
        }
        
		protected function rollOverHandler(event:MouseEvent):void 
		{
		    if (!enabled || highlighted) return;
		    
			rollPhase = RollPhase.OVER;
			updateDisplayList(this.unscaledWidth, this.unscaledHeight);
		}

		protected function rollOutHandler(event:MouseEvent):void 
		{
                        
		    if (!enabled || highlighted) return;
		    
			rollPhase = RollPhase.OUT;			
			updateDisplayList(this.unscaledWidth, this.unscaledHeight);
		}

		protected function clickHandler(event:MouseEvent):void 
		{
		    dispatchEvent(new MouseEvent(CALL_SHAPE_CLICK_EVENT));
		}

		protected function doubleClickHandler(event:MouseEvent):void 
		{
		    dispatchEvent(new MouseEvent(CALL_SHAPE_DOUBLE_CLICK_EVENT));
		}

    }
}

class RollPhase {
    public static var OVER:String = "over";
    public static var OUT:String = "out";
}