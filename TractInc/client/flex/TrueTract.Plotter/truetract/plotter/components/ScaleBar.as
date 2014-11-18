package truetract.plotter.components
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

import truetract.plotter.utils.ScaleValue;

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
        nf.useThousandsSeparator = false;

        verticalScrollPolicy = "off";
        horizontalScrollPolicy = "off";
        
        minWidth = 50;
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
        }
    }

    override protected function updateDisplayList(w:Number, h:Number):void
    {
        super.updateDisplayList(w, h);

        if (!scaleValue) return;

        uomNameTextField.x = w / 2 - uomNameTextField.width / 2;

        var LABEL_INDENT:Number = 15; //TODO: should be calculated based on max labelWidth / 2

        var barSpriteMaxWidth:Number = w - (LABEL_INDENT * 2);
        var barSpriteMinWidth:Number = minWidth - (LABEL_INDENT * 2);

        var maxUomValue:Number = barSpriteMaxWidth / scaleValue.PointsInOneFeet / scaleValue.uom.RateToOneFeet;
        var minUomValue:Number = barSpriteMinWidth / scaleValue.PointsInOneFeet / scaleValue.uom.RateToOneFeet;
        
        var niceUomValue:Number = getRoundedDownValue(minUomValue, maxUomValue);

        var barWidth:Number = niceUomValue * scaleValue.uom.RateToOneFeet * scaleValue.PointsInOneFeet;

        barSprite.width = barWidth;

        barSprite.move((w / 2) - (barSprite.width / 2), barSprite.y);

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

        label.text = nf.format(labelValue);

        //Delete decimal zeros
        if (int(label.text) == Number(label.text))
            label.text = int(label.text).toString();

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
    
    private function getRoundedDownValue(fromValue:Number, toValue:Number):Number {
        var i:Number = 100000;

        var result:Number;

        while (i > 0)
        {
            if (toValue / i > 1) {
                result = int(toValue / i) * i;
                break;
            }

            i = i / 10;
        }

        if (result < fromValue) {
            result += getRoundedDownValue(fromValue - result, toValue - result);
        }

        return result;
    }
    
}
}