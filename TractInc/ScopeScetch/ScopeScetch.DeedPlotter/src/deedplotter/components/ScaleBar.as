package src.deedplotter.components
{
import flash.display.CapsStyle;
import flash.display.Graphics;
import flash.display.JointStyle;
import flash.display.Sprite;
import flash.system.Capabilities;

import mx.containers.VBox;
import mx.core.UIComponent;
import mx.core.UITextField;
import mx.formatters.NumberFormatter;
import mx.graphics.Stroke;

import src.deedplotter.utils.ScaleValue;
import src.deedplotter.utils.UOMUtil;
import src.deedplotter.utils.UnitOfMeasure;

public class ScaleBar extends VBox
{

    //--------------------------------------------------------------------------
    //
    //  Constructor
    //
    //--------------------------------------------------------------------------
    
    public function ScaleBar()
    {
        nf.precision = 2;
        nf.rounding = "nearest";
        
        verticalScrollPolicy = "off";
        horizontalScrollPolicy = "off";
    }

    //--------------------------------------------------------------------------
    //
    //  Variables
    //
    //--------------------------------------------------------------------------

    /**
    * Number Formatter that formats divisions values
    */
    private var nf:NumberFormatter = new NumberFormatter();

    /**
    * The TextField for Unit Of Measure Name
    */    
    private var uomNameTextField:UITextField;
    
    /**
    * The UIComponent where Scale Bar should be drawed
    */    
    private var barSprite:UIComponent;
    
    /**
    * The UIComponent that contains divisions labels
    */    
    private var labelObjects:UIComponent;

    /**
    * 
    */
    private var labelsCountChanged:Boolean = true;

    //--------------------------------------------------------------------------
    //
    //  Properties
    //
    //--------------------------------------------------------------------------

    /**
    * Property barHeight. TODO:Should be refactored to style.
    */    
    private var _barHeight:Number = 5;
    private var barHeightChanged:Boolean = true;
    public function get barHeight():Number { return _barHeight; }
    public function set barHeight(value:Number):void
    {
        _barHeight = value;
        barHeightChanged = true;

        invalidateProperties();
        invalidateSize();
        invalidateDisplayList();
    }

    /**
    * Property divisionsCount.
    */    
    private var _divisionsCount:int = 4;
    public function get divisionsCount():int { return _divisionsCount; }
    public function set divisionsCount(value:int):void 
    {
        var numDivisions:int = value < 1 ? 1 : value;

        if (numDivisions != _divisionsCount)
        {
            _divisionsCount =  numDivisions;
            labelsCountChanged = true;

            invalidateProperties();
            invalidateSize();
            invalidateDisplayList();
        }
    }

    /**
    * Property subDivisionsCount.
    */    
    private var _subDivisionsCount:int = 4;
    public function get subDivisionsCount():int { return _subDivisionsCount; }
    public function set subDivisionsCount(value:int):void 
    {
        if (value != _subDivisionsCount)
        {
            _subDivisionsCount =  value;
            labelsCountChanged = true;

            invalidateProperties();
            invalidateSize();
            invalidateDisplayList();
        }
    }

    /**
    * Property scaleValue.
    */
    private var _scaleValue:ScaleValue;
    private var scaleValueChanged:Boolean = false;
    public function get scaleValue():ScaleValue { return _scaleValue; }
    public function set scaleValue(value:ScaleValue):void
    {
        _scaleValue = value;

        scaleValueChanged = true;

        invalidateProperties();
        invalidateSize();
        invalidateDisplayList();
    }

    //--------------------------------------------------------------------------
    //
    //  Overridden methods
    //
    //--------------------------------------------------------------------------
    
    override protected function createChildren():void
    {
        super.createChildren();
        
        if(!uomNameTextField)
        {
            uomNameTextField = new UITextField();
            uomNameTextField.setStyle("textAlign", "center");

            addChild(uomNameTextField);
        }
        
        if (!barSprite)
        {
            barSprite = new UIComponent();

            addChild(barSprite);
        }
        
        if (!labelObjects)
        {
            labelObjects = new UIComponent();
            labelObjects.height = 20;

            addChild(labelObjects);
        }
    }

    override protected function commitProperties():void
    {
        super.commitProperties();
        
        if (barHeightChanged)
        {
            barSprite.height = barHeight;
        }

        if (labelsCountChanged)
        {
            labelsCountChanged = false;

            createLabels();
        }

        if (scaleValueChanged)
        {
            scaleValueChanged = false;
            
            uomNameTextField.text = scaleValue.uom.Name;
            uomNameTextField.width = uomNameTextField.getExplicitOrMeasuredWidth();
            uomNameTextField.height = uomNameTextField.getExplicitOrMeasuredHeight();

/*             var maxWidthInFeet:Number = maxWidth / scaleValue.PointsInOneFeet;
            var maxWidthInUom:Number = maxWidthInFeet / scaleValue.uom.RateToOneFeet;
            
            var optimalUomValue:Number = Math.round(maxWidthInUom);
            var optimalFeetValue:Number = optimalUomValue * scaleValue.uom.RateToOneFeet;
            var optimalWidth:Number = optimalFeetValue * scaleValue.PointsInOneFeet;

            if (optimalWidth < minWidth || optimalWidth > maxWidth)
            {
                width = maxWidth;
            }                
                else
            {
                width = optimalWidth;
                scaleValue.uomValue = optimalUomValue;
            }
            
            trace("uomValue: " + scaleValue.uomValue + ", optimal UOM value: " + optimalUomValue + ", optimal width: " +  optimalWidth );
 */
 
 
 
/*             var minRoundedValue:Number = 
                Math.round((value - (divisionsCount / 2)) / divisionsCount) * divisionsCount;
    
            var maxRoundedValue:Number =
                Math.round((value + (divisionsCount / 2)) / divisionsCount) * divisionsCount;
    
            if (Math.abs(value - minRoundedValue) < Math.abs(value - maxRoundedValue))
                value = minRoundedValue;
            else
                value = maxRoundedValue;
    
            if (scaleValue)
            {
                var supposedWidth:Number = 
                    Capabilities.screenDPI * scaleValue.FeetsInOneInch * value * uom.RateToOneFeet;
    
                if (supposedWidth < minWidth || supposedWidth > maxWidth)
                {
                    throw new Error("the maxFeetValue is too small or too big");
                }
            }
    
            _maxUomValue = value;
 */            
        }
    }

    override protected function updateDisplayList(w:Number, h:Number):void
    {
        super.updateDisplayList(w, h);

        if (!scaleValue) return;

        uomNameTextField.x = w / 2 - uomNameTextField.width / 2;

        var numLabels:Number = labelObjects.numChildren;
        var leftMargin:Number = 0;
        var rightMargin:Number = 0;

        if (numLabels > 0)
            leftMargin = UITextField(labelObjects.getChildAt(0)).getExplicitOrMeasuredWidth() / 2;

        if (numLabels > 1)
            rightMargin = UITextField(labelObjects.getChildAt(numLabels - 1)).getExplicitOrMeasuredWidth() / 2;

        var barMargin:Number = Math.max(leftMargin, rightMargin);

        barSprite.width = w - (barMargin * 2);
        barSprite.move(barMargin, barSprite.y);

        drawBar();

        layoutLabels();
    }

    //--------------------------------------------------------------------------
    //
    //  Methods
    //
    //--------------------------------------------------------------------------

    /**
     * @private
     * Does division labels layout
     */
    private function layoutLabels():void
    {
        var numLabels:Number = labelObjects ? labelObjects.numChildren : 0;
        
        if (numLabels)
        {
            var left:Number = barSprite.x;

            var label:UITextField = UITextField(labelObjects.getChildAt(0));
            setLabelProperties(label, left);

            labelObjects.setActualSize(width, label.getExplicitOrMeasuredHeight());

            var labelIterator:int = 1; //one label (for zero) we already processed
            var thumbXPosition:Number;

            //We support only one subDivision label and only if subDivisionsCount is event number
            if (subDivisionsCount > 1 && (subDivisionsCount % 2) == 0)
            {
                label = UITextField(labelObjects.getChildAt(labelIterator));
                label.setActualSize(label.getExplicitOrMeasuredWidth(), label.getExplicitOrMeasuredHeight());
                
                thumbXPosition = left + ((barSprite.width / divisionsCount) / 2);
                setLabelProperties(label, thumbXPosition);
                
                labelIterator++;
            }

            for (var i:int = 1; i <= divisionsCount; i++)
            {
                label = UITextField(labelObjects.getChildAt(labelIterator));
                thumbXPosition = left + ((barSprite.width / divisionsCount) * i);

                setLabelProperties(label, thumbXPosition);

                labelIterator++;
            }
            
        }
    }

    private function setLabelProperties(label:UITextField, xPos:Number):void
    {
        var left:Number = barSprite.x;
        var labelValue:Number = ((xPos - left) / scaleValue.PointsInOneFeet) / scaleValue.uom.RateToOneFeet;

        if (labelValue == Math.round(labelValue))
            label.text = Math.round(labelValue).toString();
        else
            label.text = nf.format(labelValue);

        //Add leading zero. e.g: convert ".57" string to "0.57"
        if (labelValue < 1 && labelValue > 0)
            label.text = '0' + label.text;

        label.width = label.getExplicitOrMeasuredWidth();
        label.x = xPos - label.width / 2;
    }

    /**
     * @private
     * Creates divisions labels
     */
    private function createLabels():void
    {
        for (var i:int = 0; i < labelObjects.numChildren; i++)
        {
            labelObjects.removeChildAt(i);
        }

        var labelsCount:Number = divisionsCount + 1; //one label for zero

        if (subDivisionsCount > 1 && (subDivisionsCount % 2) == 0)
        {
            labelsCount++;
        }

        for (i = 0; i < labelsCount; i++)
        {
            var labelTextField:UITextField = new UITextField();

            labelObjects.addChild(labelTextField);
        }
    }

    /**
     * @private
     * Draws Scale Bar
     */    
    private function drawBar():void 
    {
        var i:int;
        
        var divisionHeight:Number = barSprite.height - 2; //2 pixels reserved for border
        
        var whiteLineStyle:Stroke = 
            new Stroke(0xFFFFFF, divisionHeight, 1, true, "normal", CapsStyle.NONE);

        var blackLineStyle:Stroke = 
            new Stroke(0, divisionHeight, 1, true, "normal", CapsStyle.NONE);

        var divisionWidth:Number = (barSprite.width - 2) / divisionsCount;
        var subDivisionWidth:Number = subDivisionsCount < 2 ? 0 : divisionWidth / subDivisionsCount;

        var lineYPosition:Number = Math.round((barHeight - 2) / 2);

        var drawBlackLine:Boolean = true;

        var g:Graphics = barSprite.graphics;
        g.clear();

        //draw bar background
        g.beginFill(0);
        g.drawRect(0, 0, barSprite.width, barSprite.height);
        g.endFill();

        g.moveTo(3, lineYPosition);

        if (subDivisionsCount > 1)
        {
            //draw subDivisions
            for (i = 1; i <= subDivisionsCount; i++)
            {
                if (drawBlackLine)
                    blackLineStyle.apply(g);
                else
                    whiteLineStyle.apply(g);
                
                g.lineTo(subDivisionWidth * i, lineYPosition);
                g.moveTo((subDivisionWidth * i) + 1, lineYPosition);
    
                drawBlackLine = !drawBlackLine;
            }

        }

        i = subDivisionsCount > 1 ? 2 : 1;
        
        //draw divisions
        for (i; i <= divisionsCount; i++)
        {
             if (drawBlackLine)
                blackLineStyle.apply(g);
            else
                whiteLineStyle.apply(g);
                
            var endXPosition:Number = divisionWidth * i;

            g.lineTo(endXPosition, lineYPosition);
            g.moveTo(endXPosition + 1, lineYPosition);
            
            drawBlackLine = !drawBlackLine;
        }
    }
    
}
}