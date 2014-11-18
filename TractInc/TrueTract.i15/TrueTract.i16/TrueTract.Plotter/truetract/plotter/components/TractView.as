package truetract.plotter.components
{

import flash.display.DisplayObject;
import flash.display.Shape;
import flash.events.Event;
import flash.events.MouseEvent;
import flash.events.TimerEvent;
import flash.geom.Point;
import flash.geom.Rectangle;
import flash.utils.Timer;

import mx.collections.ArrayCollection;
import mx.collections.Sort;
import mx.collections.SortField;
import mx.controls.Text;
import mx.core.Application;
import mx.core.UIComponent;
import mx.core.UITextField;
import mx.events.FlexEvent;
import mx.events.ListEvent;
import mx.formatters.NumberBaseRoundType;
import mx.formatters.NumberFormatter;
import mx.managers.CursorManager;

import truetract.plotter.domain.*;
import truetract.plotter.components.tractViewClasses.*;
import truetract.plotter.components.tractViewClasses.call.*;
import truetract.plotter.components.tractViewClasses.call.factories.CallViewFactory;
import truetract.plotter.components.tractViewClasses.events.*;
import truetract.plotter.domain.callparams.*;
import truetract.plotter.utils.*;

[Event(name="callClick", type="truetract.plotter.components.tractViewClasses.events.TractCallEvent")]
[Event(name="callDoubleClick", type="truetract.plotter.components.tractViewClasses.events.TractCallEvent")]
[Event(name="pointMouseDown", type="truetract.plotter.components.tractViewClasses.events.TractPointEvent")]
[Event(name="dataChange", type="mx.events.FlexEvent")]

public class TractView extends UIComponent
{

    //--------------------------------------------------------------------------
    //
    //  Constructor
    //
    //--------------------------------------------------------------------------
	public function TractView() 
	{
        nf = new NumberFormatter();
        nf.useThousandsSeparator = true;
        nf.rounding = NumberBaseRoundType.NEAREST;
        nf.precision = 2;
	}

    //--------------------------------------------------------------------------
    //
    //  Variables
    //
    //--------------------------------------------------------------------------

	public var textViewList:ArrayCollection = new ArrayCollection();
	public var callViewList:ArrayCollection = new ArrayCollection();

    private var areaStyleNameProp:String = "areaStyleName";
    private var callStyleNameProp:String = "callStyleName";
    private var startPointStyleNameProp:String = "startPointStyleName";
    private var endPointStyleNameProp:String = "endPointStyleName";
    private var controlPointStyleNameProp:String = "controlPointStyleName";
    private var textObjectStyleNameProp:String = "textObjectStyleName";

	private var startPoint:TractPointView;
	private var tractAreaTextField:UITextField;
	private var tractBoundShape:UIComponent;

    private var nf:NumberFormatter;
    
    //--------------------------------------------------------------------------
    //
    //  Properties
    //
    //--------------------------------------------------------------------------

	private var _tract:Tract;
    private var tractChanged:Boolean = false;

    [Bindable("dataChange")] 
    public function get tract():Tract { return _tract; }
    public function set tract(value:Tract):void 
    {
        _tract = value;
        tractChanged = true;

        commitProperties();
        dispatchEvent(new FlexEvent(FlexEvent.DATA_CHANGE));
    }

    private var _surfaceScale:ScaleValue = ScaleValue.Default;
    public function get surfaceScale():ScaleValue { return _surfaceScale; }
    public function set surfaceScale(value:ScaleValue):void 
    {
        _surfaceScale = value;

        if (tract)
        {
            rearrangeCallView(false);
            refreshTextObjectViews();
	        refreshTractAreaPosition();
        }

        invalidateDisplayList();
    }
	
	private var tractAreaVisibilityChanged:Boolean = true;
	private var _showArea:Boolean = true;
    public function get showArea():Boolean { return _showArea }
    public function set showArea(value:Boolean):void 
    {
        _showArea = value;
        tractAreaVisibilityChanged = true;
        
        invalidateDisplayList();
    }
	
	private var _showCallAnnotations:Boolean = true;
	private var callAnnotationsVisibilityChanged:Boolean;
	public function get showCallAnnotations():Boolean { return _showCallAnnotations }
	public function set showCallAnnotations(value:Boolean):void
	{
	    callAnnotationsVisibilityChanged = true;
	    _showCallAnnotations = value;
	    
	    invalidateDisplayList();
	}

	private var _showTractPoints:Boolean = true;
	private var tractPointsVisibilityChanged:Boolean;
	public function get showTractPoints():Boolean { return _showTractPoints }
	public function set showTractPoints(value:Boolean):void 
	{
	    _showTractPoints = value;
	    tractPointsVisibilityChanged = true;
	    
	    invalidateDisplayList();
	}
	
	override public function set enabled(value:Boolean):void 
	{
	    super.enabled = value;
	    
	    for each (var callView:CallView in callViewList)
	    {
	        callView.enabled = value;
	    }
	}

	private var _tempLine:CallView;
    public function get tempLine():CallView 
    {
        return _tempLine;
    }

    //--------------------------------------------------------------------------
    //
    //  Overridden methods: UIComponent
    //
    //--------------------------------------------------------------------------

	override public function styleChanged(styleProp:String):void 
	{
	    var allStyles:Boolean = styleProp == null || styleProp == "styleName";
	    
	    super.styleChanged(styleProp);
	
	    if (allStyles || styleProp == areaStyleNameProp) 
	    {
	        if (tractAreaTextField) 
	        {
	            var areaStyleName:String = getStyle(areaStyleNameProp);
	            if (areaStyleName) 
	            {
	                tractAreaTextField.styleName = areaStyleName;
	            }
	        }
	    }
	}

	override protected function createChildren():void 
	{
	    super.createChildren();

	    if (!tractAreaTextField) 
	    {
	        var areaStyleName:String = getStyle(areaStyleNameProp);

		    tractAreaTextField = new UITextField();
		    tractAreaTextField.width = 0
		    tractAreaTextField.height = 0;
		    tractAreaTextField.mouseEnabled = false;
		    tractAreaTextField.text = "";
            tractAreaTextField.visible = false;
            tractAreaTextField.ignorePadding = true;

	        if (areaStyleName) 
	        {
                tractAreaTextField.styleName = areaStyleName;
	        }

		    this.addChild(tractAreaTextField);
	    }

	    if (!tractBoundShape) 
	    {
	        tractBoundShape = new UIComponent();
	        parent.addChild(tractBoundShape); // TODO: bad manners - should be refactored
	    }
	}
	
    override protected function commitProperties():void
    {
        if (tractChanged) 
        {
            tractChanged = false;

            removeAll();

            if (tract) 
            {

                //starting longitude, latitude is not supported yet
    			//startPoint = new TractPointView(new GeoPosition(tract.Easting, tract.Northing));

    			startPoint = new TractPointView(new GeoPosition(0, 0));
    			startPoint.addEventListener(MouseEvent.MOUSE_DOWN, startPoint_mouseDownHandler);
    			startPoint.visible = showTractPoints;
    			
    			var startPointStyleName:String = getStyle(startPointStyleNameProp);
    			if (startPointStyleName) {
    			    startPoint.styleName = startPointStyleName;
    			}
    			
    			this.addChild(startPoint);
    			
                var endPosition:GeoPosition = startPoint.startPosition;
                
                var cp:ControlPointView = startPoint;
                
                sortCallsByOrder();
                
                for each (var call:TractCall in tract.CallsList) 
                {
        		    var callView:CallView = CallViewFactory.Instance().GetCallView(call);

        		    callViewList.addItem(callView);

        		    callView.StartPoint = cp;
        		    callView.Shape.startPosition = endPosition;
        		    callView.move(localX(endPosition.Easting), localY(endPosition.Northing));
        		    callView.addEventListener(CallView.CALL_SHAPE_CLICK_EVENT, call_clickHandler);
        		    callView.addEventListener(CallView.CALL_SHAPE_DOUBLE_CLICK_EVENT, call_doubleClickHandler);
        		    callView.doubleClickEnabled = true;

                    var callStyleName:String = getStyle(callStyleNameProp);

                    if (callStyleName) {
                        callView.styleName = callStyleName;
                    }

        		    endPosition = callView.Shape.endPosition;

        		    callView.EndPoint = cp = createControlPoint(endPosition);

        		    this.addChild(callView);
                }

                moveControlPointsOnTop();
    
                if (tract.CallsList.length > 0) 
                {
                    removeChild(cp);
                    
                    tract.IsClosed = endPosition.Equals(startPoint.startPosition);
                    
                    if (tract.IsClosed) 
                    {
                        callViewList[callViewList.length - 1].EndPoint = startPoint;
                        refreshTractArea();
                    } 
                    else 
                    {
                        var endPoint:ControlPointView = createControlPoint(endPosition, true);
                        callViewList[callViewList.length - 1].EndPoint = endPoint;
                        
                        this.setChildIndex(endPoint, numChildren - 1);
                        tractAreaTextField.visible = false;
                    }
                }

                for each (var textObject:TractTextObject in tract.TextObjects) 
                {
                    createTextView(textObject);
                }
            }
		}

        super.commitProperties();
    }

	override protected function updateDisplayList(unscaledWidth:Number, unscaledHeight:Number):void 
	{
	    super.updateDisplayList(unscaledWidth, unscaledHeight);

        if (tractPointsVisibilityChanged) 
        {
            tractPointsVisibilityChanged = false;

            for each (var callView:CallView in callViewList) {
                callView.StartPoint.visible = _showTractPoints;
                callView.EndPoint.visible = _showTractPoints;
            }
        }

        if (callAnnotationsVisibilityChanged) 
        {
            callAnnotationsVisibilityChanged = false;
            
            for each (callView in callViewList) 
            {
                callView.invalidateDisplayList();
            }
        }
        
	    if (tractAreaVisibilityChanged && tractAreaTextField != null) {
	        tractAreaVisibilityChanged = false;

	        tractAreaTextField.visible = tract && tract.IsClosed && showArea;
	    }

	    if (tractBoundShape)
	    {
		    var bounds:Rectangle = getComponentBounds();

            tractBoundShape.move(this.x + bounds.x, this.y + bounds.y);
            tractBoundShape.width = bounds.width;
            tractBoundShape.height = bounds.height;
            
/*             tractBoundShape.graphics.clear();
            tractBoundShape.graphics.lineStyle(2, 0xFF00FF);
            tractBoundShape.graphics.drawRect(0, 0, tractBoundShape.width, tractBoundShape.height);
 */        }
	}

    //--------------------------------------------------------------------------
    //
    //  Methods
    //
    //--------------------------------------------------------------------------
    /**
    * Returns rectangle that defined component size. Doesn't includes temporary line.
    * It is the replacement of method DisplayObject.getBounds(), which return component size 
    * since last invalidate cycle.
    * 
    * Возвращает квадрат, который определяет размер компонента.
    * Не включает временную линию (tempLine).
    * Является заменой метода DisplayObject.getBounds(), поскольку последний возвращает размеры
    * с момента последней инвалидации объекта.
    **/
    public function getComponentBounds():Rectangle 
    {
        if (!tract) {
            return new Rectangle();
        }

        var componentBounds:BoundRectangle = getCallsBounds();

        var extraSize:Number = 0;

        if (showCallAnnotations) {
            extraSize = Math.max(extraSize, getCallAnnotationIndent());
        }

        if (showTractPoints) {
            extraSize = Math.max(extraSize, 4); //TODO: Hmm...
        }

        componentBounds.minX -= extraSize;
        componentBounds.minY -= extraSize;
        componentBounds.maxX += extraSize;
        componentBounds.maxY += extraSize;

        //add text objects bounds
        for each (var textView:TractTextObjectView in textViewList) 
        {
            var textViewBounds:Rectangle = textView.getBounds(this);

            componentBounds.addPoint(textViewBounds.topLeft);
            componentBounds.addPoint(textViewBounds.bottomRight);
        }

        return componentBounds.toRectangle();
    }

    /**
    * Calculates a scale, needed to fit the tract into the given sizes
    * 
    * Рассчитывает масштаб, необходимый для того чтобы вписать тракт в заданные размеры
    **/
    public function calcaluateRequiredScaling(reqWidth:Number, reqHeight:Number, uom:UnitOfMeasure):ScaleValue 
    {
        if (callViewList.length == 0 && textViewList.length == 0) {
            return surfaceScale;
        }

        var geoObjectBounds:BoundRectangle = getGeoObjectsBounds();
        
        var tractBounds:Rectangle = getComponentBounds();
        
        var availableCallWidth:Number = reqWidth - (tractBounds.width - geoObjectBounds.width);
        var availableCallHeight:Number = reqHeight - (tractBounds.height - geoObjectBounds.height);

        var tractWidthFeets:Number = geoObjectBounds.width / surfaceScale.PointsInOneFeet;
        var tractHeightFeets:Number = geoObjectBounds.height / surfaceScale.PointsInOneFeet;

        var pointInOneFeetWidthRequired:Number = availableCallWidth / tractWidthFeets;
        var pointInOneFeetHeightRequired:Number = availableCallHeight / tractHeightFeets;

        var scale:ScaleValue = new ScaleValue();
        scale.uom = uom;
        scale.PointsInOneFeet = Math.min(pointInOneFeetWidthRequired, pointInOneFeetHeightRequired);

        return scale;
    }

    /**
    * 
    * 
    * Возвращает локальные границы объектов, размеры и размещение которых привязаны к геодезическим 
    * координатам. Не включает размеры и отступы callAnnotatios и размеры котнтрольных точек тракта.
    * 
    **/
    
    public function getGeoObjectsBounds():BoundRectangle {
        var points:Array = [];
        
        for each (var callView:CallView in callViewList) 
        {
            points.push(new Point(callView.StartPoint.x, callView.StartPoint.y));
            points.push(new Point(callView.EndPoint.x, callView.EndPoint.y));
            
            if (callView.Shape is GeoCurve) {
                var curve:GeoCurve = GeoCurve(callView.Shape);
                var rectangle:BoundRectangle = curve.getBounds();
                
                points.push(new Point(localX(rectangle.minX), localY(rectangle.maxY)));
                points.push(new Point(localX(rectangle.maxX), localY(rectangle.minY)));
            }
        }
        
        for each (var tf:TractTextObjectView in textViewList) {
            points.push(new Point(tf.x, tf.y));
        }
        
        return BoundRectangle.createByPoints(points);
    }
    
    private function getCallAnnotationIndent():Number {
        var result:Number = 0;

        for each (var callView:CallView in callViewList) {
            result = Math.max(result, callView.getLabelMaxIndent());
        }

        return result;
    }

    public function getCallsBounds():BoundRectangle {
        var tractPoints:Array = [];
        
        for each (var callView:CallView in callViewList) 
        {
            tractPoints.push(new Point(callView.StartPoint.x, callView.StartPoint.y));
            tractPoints.push(new Point(callView.EndPoint.x, callView.EndPoint.y));
            
            if (callView.Shape is GeoCurve) 
            {
                var curve:GeoCurve = GeoCurve(callView.Shape);
                var rectangle:BoundRectangle = curve.getBounds();
                
                tractPoints.push(new Point(localX(rectangle.minX), localY(rectangle.maxY)));
                tractPoints.push(new Point(localX(rectangle.maxX), localY(rectangle.minY)));
            }
        }

        return BoundRectangle.createByPoints(tractPoints);
    }

    public function createTempLine():void 
    {
        removeTempLine();

        var tractEndPoint:ControlPointView = getTractEndPoint();
        var line:GeoLine = GeoLine.createByEndPosition(tractEndPoint.startPosition, tractEndPoint.startPosition);

        _tempLine = new CallLineView(line);
        _tempLine.StartPoint = tractEndPoint;
        _tempLine.Model = new TractCall();
        _tempLine.Model.CallOrder = tract.CallsList.length;
        _tempLine.Model.CallType = TractCall.CALL_TYPE_LINE;
        _tempLine.move(tractEndPoint.x, tractEndPoint.y);

        var callStyleName:String = getStyle(callStyleNameProp);

        if (callStyleName) {
            _tempLine.styleName = callStyleName;
        }

        addChild(_tempLine);

        setChildIndex(tractEndPoint, numChildren - 1);
        setChildIndex(startPoint, numChildren - 1);
    }

    public function removeTempLine():void 
    {
        if (tempLine) {
            removeChild(tempLine);
            _tempLine = null;
        }
    }
    
	public function AddCall(call:TractCall):CallView 
	{

        if (tract.IsClosed) 
        {
            throw new Error("Unable to add call. Tract is closed");
        }
        
	    call.CallOrder = tract.CallsList.length;
        call.TractId = tract.TractId;
        
        var lastCallView:CallView = GetLastCallView();
	    if (lastCallView)
	    {
	        this.removeChild(lastCallView.EndPoint);
	        lastCallView.EndPoint = createControlPoint(lastCallView.Shape.endPosition, false);
	    }

        var tractEndPoint:ControlPointView = getTractEndPoint();
        
        var callView:CallView = CallViewFactory.Instance().GetCallView(call);
        var callStyleName:String = getStyle(callStyleNameProp);

        if (callStyleName) {
            callView.styleName = callStyleName;
        }
        
	    addChild(callView);
	    callViewList.addItem(callView);

	    callView.Shape.startPosition = tractEndPoint.startPosition;
	    callView.move(tractEndPoint.x, tractEndPoint.y);
  	    callView.addEventListener(CallView.CALL_SHAPE_CLICK_EVENT, call_clickHandler);
  	    callView.addEventListener(CallView.CALL_SHAPE_DOUBLE_CLICK_EVENT, call_doubleClickHandler);
  	    callView.doubleClickEnabled = true;
	    callView.StartPoint = tractEndPoint;
	    callView.EndPoint = createControlPoint(callView.Shape.endPosition, true);

        tract.CallsList.addItem(call);

        if (tempLine)
        {
            tempLine.Shape.startPosition = callView.Shape.endPosition;
            tempLine.move(callView.EndPoint.x, callView.EndPoint.y);
            tempLine.StartPoint = callView.EndPoint;
            tempLine.Model.CallOrder = tract.CallsList.length;
        }
        
        moveControlPointsOnTop();

	    return callView;
	}

	public function AddCallAt(call:TractCall, index:int):void 
	{
	    if (index > tract.CallsList.length - 1)
	    {
	        throw new Error("Argument overflow exception");
	    }
	    
	    if (tract.IsClosed) UnCloseTract();

        var nextCall:TractCall = tract.GetCallByOrder(index);
        var nextCallView:CallView = GetCallView(nextCall);

	    var callView:CallView = CallViewFactory.Instance().GetCallView(call);
        var callStyleName:String = getStyle(callStyleNameProp);

        if (callStyleName) {
            callView.styleName = callStyleName;
        }
	    
	    addChild(callView);
        callViewList.addItem(callView);
        
	    call.CallOrder = index - 0.5;
	    call.TractId = tract.TractId;
        tract.CallsList.addItem(call);
        
  	    callView.addEventListener(CallView.CALL_SHAPE_CLICK_EVENT, call_clickHandler);
  	    callView.addEventListener(CallView.CALL_SHAPE_DOUBLE_CLICK_EVENT, call_doubleClickHandler);
  	    callView.doubleClickEnabled = true;
        callView.StartPoint = nextCallView.StartPoint;
        nextCallView.StartPoint = callView.EndPoint = createControlPoint(callView.StartPoint.startPosition);

        rearrangeCallView();
	}

    public function AddTextObject(textObject:TractTextObject):TractTextObjectView 
    {
        textObject.TractId = tract.TractId;
        tract.TextObjects.addItem(textObject);
        
        return createTextView(textObject);
    }

	public function DeleteCall(call:TractCall):void 
	{
	    var callView:CallView = GetCallView(call);

        if (tract.IsClosed)
        {
            UnCloseTract();
        }

	    callViewList.removeItemAt(callViewList.getItemIndex(callView));
	    removeChild(callView);
	    removeChild(callView.EndPoint);

	    if (tract.CallsList.length > 1) 
	    {
	        if (call.CallOrder == tract.CallsList.length - 1) 
	        {
	            //if call is the last call in Tract, we should add Tract EndPoint to the last call    
	            var previousCall:TractCall = tract.GetCallByOrder(call.CallOrder - 1);
	            var previousCallView:CallView = GetCallView(previousCall);
	            removeChild(previousCallView.EndPoint);
	            //TODO: refactore TractPoints to be able just change the point type in this place
	            previousCallView.EndPoint = createControlPoint(previousCallView.EndPoint.startPosition, true);
	        } 
	        else 
	        {
	            //else we should reset StartPoint for the next call after deleted
    		    var nextCall:TractCall = tract.GetCallByOrder(call.CallOrder + 1);
    		    var nextCallView:CallView = GetCallView(nextCall);
    		    nextCallView.StartPoint = callView.StartPoint;
		    }
	    }
	    
	    tract.RemoveCall(call);
	    
        rearrangeCallView();
	}
	
    public function DeleteTextObject(textObject:TractTextObject):void 
    {
        var textView:TractTextObjectView = GetTextObjectView(textObject);
        if (textView) 
        {
            textViewList.removeItemAt(textViewList.getItemIndex(textView));
            removeChild(textView);
        }
        
        tract.RemoveTextObject(textObject);
    }
	
	public function UpdateCall(call:TractCall):void 
	{
	    var callView:CallView = GetCallView(call);

        var bearingOutBeforeUpdate:GeoBearing = callView.Shape.bearingOut;
        
        callView.Shape = CallViewFactory.Instance().GetCallGeoShape(call);
        
        var bearingDelta:Number = callView.Shape.bearingOut.Azimuth - bearingOutBeforeUpdate.Azimuth;
        
        //TODO: aply tangentBearingIn for each call after this

        callView.invalidateDisplayList();
        
        rearrangeCallView();
	}
	
	//Close Tract. Add line if neccesary between EndPoint and StartPoint. Set EndPoint to reference on StartPoint
	public function CloseTract(needTractAreaRefreshing:Boolean):void 
	{
        var tractEndPoint:ControlPointView = getTractEndPoint();

        if (!tractEndPoint.startPosition.Equals(startPoint.startPosition)) 
        {
            var line:GeoLine = GeoLine.createByEndPosition(tractEndPoint.startPosition, startPoint.startPosition);

            var callParams:ParamCollection = new ParamCollection();
            callParams.addItem(new BearingParam(line.bearing));
            callParams.addItem(new DistanceParam(line.distance, UOMUtil.Instance().DefaultUOM));
		    
		    var call:TractCall = new TractCall();
		    call.TractId = tract.TractId;
		    call.CallDBValue = callParams.GetDBString();
            call.CallType = TractCall.CALL_TYPE_LINE;
            call.CreatedByMouse = 1;
            
		    AddCall(call);
        }
        
        tract.IsClosed = true;

        var lastCallView:CallView = GetLastCallView();
        removeChild(lastCallView.EndPoint);
        lastCallView.EndPoint = startPoint;

        removeTempLine();
        moveControlPointsOnTop();
        
        if (needTractAreaRefreshing) {
            refreshTractArea();
        }
        
        refreshTractAreaPosition();
	}
	
	public function UnCloseTract():void 
	{
	    var lastCallView:CallView = GetLastCallView();
	    
	    lastCallView.EndPoint = createControlPoint(lastCallView.Shape.endPosition, true);
	    
	    tract.IsClosed = false;
	    refreshTractAreaPosition();
	}

	public function SetTractEditable(editable:Boolean):void 
	{
	    for each (var callView:CallView in callViewList)
	    {
	        callView.enabled = editable;
	    }
	}
	
    public function GetCallView(call:TractCall):CallView 
    {
        for each (var callView:CallView in callViewList)
        {
            if (callView.Model == call) return callView;
        }
        
        return null;
    }
    
    public function GetTextObjectView(textObject:TractTextObject):TractTextObjectView 
    {
        for each (var textView:TractTextObjectView in textViewList)
        {
            if (textView.Model == textObject) return textView;
        }
        
        return null;
    }
    
    public function GetLastCallView():CallView 
    {
        var result:CallView = null;
        
        var lastCall:TractCall = tract.GetCallByOrder(tract.CallsList.length - 1);
        if (lastCall) 
        {
            result = GetCallView(lastCall);
        }
        
        return result;
    }
    
	public function GetLastBearing():GeoBearing 
	{
        var lastCallView:CallView = GetLastCallView();

        if (lastCallView)
	        return lastCallView.Shape.bearingOut;
	    else
	        return new GeoBearing();
	}
	
	public function localX(easting:Number):Number 
	{
	    return (easting - startPoint.startPosition.Easting) * _surfaceScale.PointsInOneFeet;
	}

	public function localY(northing:Number):Number 
	{
	    return (- northing + startPoint.startPosition.Northing) * _surfaceScale.PointsInOneFeet;
	}
	
    public function localPoint(geoPos:GeoPosition):Point 
    {
        return new Point(localX(geoPos.Easting), localY(geoPos.Northing));
    }

	private function rearrangeCallView(needTractAreaRefreshing:Boolean=true):void 
	{
	    if (!tract) 
	        return;
	    
	    sortCallsByOrder();
	    
	    if (tract.IsClosed) UnCloseTract();
	    
	    var callStartPoint:ControlPointView = startPoint;
	    
        for (var i:int = 0; i < tract.CallsList.length; i++) 
        {
            var call:TractCall = tract.CallsList[i];
            call.CallOrder = i;

            var callView:CallView = GetCallView(call);
            
            if (callView.StartPoint != callStartPoint){ throw new Error("Internall error") }
            
            callView.Shape.startPosition = callStartPoint.startPosition;
            callView.move(localX(callStartPoint.startPosition.Easting), 
                localY(callStartPoint.startPosition.Northing));
            callView.EndPoint.startPosition = callView.Shape.endPosition;
            callView.EndPoint.move(localX(callView.Shape.endPosition.Easting), 
                localY(callView.Shape.endPosition.Northing));
            callView.invalidateDisplayList();
            callStartPoint = callView.EndPoint;
        }

        if (tempLine)
        {
            tempLine.StartPoint = callStartPoint;
            tempLine.Shape.startPosition = tempLine.StartPoint.startPosition;
            tempLine.move(tempLine.StartPoint.x, tempLine.StartPoint.y);
            tempLine.Model.CallOrder = tract.CallsList.length;
        }

        if (tract.CallsList.length > 0 && callStartPoint.startPosition.Equals(startPoint.startPosition)) 
        {
            CloseTract(needTractAreaRefreshing);
        }
        
        moveControlPointsOnTop();
	}

    private function createTextView(textObject:TractTextObject):TractTextObjectView 
    {
        var textView:TractTextObjectView = new TractTextObjectView();
        textView.Model = textObject;

        addChild(textView);

        textView.x = localX(textObject.Position.Easting);
        textView.y = localY(textObject.Position.Northing);
        
        textViewList.addItem(textView);

        textView.rotation = textObject.Rotation;
        
        return textView;
    }

    private function createControlPoint(position:GeoPosition, isEndPoint:Boolean = false):ControlPointView 
    {
        var result:ControlPointView;
        var controlPointStyleName:String;

        if (isEndPoint)
        {
            result = new TractPointView(position);
            result.addEventListener(MouseEvent.MOUSE_DOWN, endPoint_mouseDownHandler);

            controlPointStyleName = getStyle(endPointStyleNameProp);
        } 
        else 
        {
            result = new ControlPointView(position);
            result.addEventListener(MouseEvent.MOUSE_DOWN, controlPoint_mouseDownHandler);
            
            controlPointStyleName = getStyle(controlPointStyleNameProp);
        }

        if (controlPointStyleName) 
        {
            result.styleName = controlPointStyleName;
        }

        result.startPosition = position;
        result.move(localX(position.Easting), localY(position.Northing));
        
        result.visible = _showTractPoints;

        this.addChild(result);
        
        return result;
    }
	
	private function moveControlPointsOnTop():void 
	{
	    this.setChildIndex(startPoint, numChildren - 1);
	    
	    for each (var callView:CallView in callViewList)
	    {
	        this.setChildIndex(callView.EndPoint, numChildren - 1);    
	    }
	}
	
	/**
	 * 
	 * Выполняет позиционирование, изменение размеров, управление видимостью текстового поля в 
	 * котором находится площадь тракта (tractAreaTextField)
	 * 
	 **/
    private function refreshTractAreaPosition():void 
    {
        var tractPoints:Array = getTractPoints();
        
        var callBounds:BoundRectangle = getCallsBounds();

        if (showCallAnnotations) {
            var callAnnotationIndent:Number = getCallAnnotationIndent();

            callBounds.minX += callAnnotationIndent;
            callBounds.minY += callAnnotationIndent;
            callBounds.maxX -= callAnnotationIndent;
            callBounds.maxY -= callAnnotationIndent;
        }

        var geoTractCentroid:GeoPosition = GeoPosition.CreateFromPoint(
            PolygonUtil.centerOfMass(tractPoints) );

        var tractCentroid:Point = localPoint(geoTractCentroid);

        tractAreaTextField.width = tractAreaTextField.getExplicitOrMeasuredWidth();
        tractAreaTextField.height = tractAreaTextField.getExplicitOrMeasuredHeight();

        tractAreaTextField.x = tractCentroid.x - (tractAreaTextField.width / 2);
        tractAreaTextField.y = tractCentroid.y - (tractAreaTextField.height / 2);

        if (showArea) 
        {
            tractAreaTextField.visible = tract.IsClosed &&
                (tractAreaTextField.textWidth <= callBounds.width) &&
                (tractAreaTextField.textHeight <= callBounds.height);
        }
    }

	/**
	 * Calculates Tract Area and shows it in the tractAreaTextField
	 * Рассчитывает площадь тракта и показывает его в tractAreaTextField
	 **/
    private function refreshTractArea():void
    {
        CursorManager.setBusyCursor();

        var tractPoints:Array = getTractPoints();
        var tractArea:Number = Math.abs(PolygonUtil.area(tractPoints));
        var tractAreaAkres:Number = tractArea / 43560;

        tractAreaTextField.text = nf.format(tractAreaAkres.toString()) + " Acres\n";
        tractAreaTextField.appendText(nf.format(tractArea) + " Sq. feet");

        CursorManager.removeBusyCursor();
    }
    
    /**
    * 
    * Возвращает массив геодезических координат контрольных точек тракта в порядке его обхода
    * Учитываются точки из которых состоят кривые тракта.
    * Если Тракт закрытый, последний элемент массива будет равным первому
    **/
    private function getTractPoints():Array 
    {
        var result:Array = []

        if (tract) 
        {
            result.push(startPoint.startPosition.toPoint());

            sortCallsByOrder();

            for each (var call:TractCall in tract.CallsList)
            {
                var callView:CallView = GetCallView(call);
                
                if (callView.Shape is GeoCurve) 
                {
                    var curve:GeoCurve = GeoCurve(callView.Shape);
                    var curvePoints:Array = curve.getPoints();
                    
                    result.pop(); //remove previous point - curvePoints[0] has it
                    ArrayUtil.addRange(result, curvePoints);
                } 
                else 
                {
                    result.push( callView.Shape.endPosition.toPoint() );
                }
            }
        }

        return result;
    }
    
    private function refreshTextObjectViews():void 
    {
        for each (var textView:TractTextObjectView in textViewList) 
        {
            textView.x = localX(textView.Model.Easting);
            textView.y = localY(textView.Model.Northing);
        }
    }
    
    private function sortCallsByOrder():void 
    {
        tract.CallsList.sort = new Sort();
        tract.CallsList.sort.fields = [ new SortField("CallOrder", false, false, true) ];
        tract.CallsList.refresh();
    }
    
    private function getTractEndPoint():ControlPointView 
    {
        var lastCallView:CallView = GetLastCallView();
        
        if (lastCallView)
            return lastCallView.EndPoint;
        else
            return startPoint;
    }
    
    private function removeAll():void
    {
        for each (var callView:CallView in callViewList)
        {
            removeChild(callView);
            removeChild(callView.EndPoint);
        }
        
        callViewList.removeAll();

        for each (var textView:TractTextObjectView in textViewList)
        {
            removeChild(textView);
        }
        
        textViewList.removeAll();

        if (startPoint && startPoint.parent) 
        {
            removeChild(startPoint);
        }

        startPoint = null;
        
        removeTempLine();

        tractAreaTextField.text = "";
        
        invalidateDisplayList();
    }

	private function call_clickHandler(event:MouseEvent):void 
	{
	    event.preventDefault();
	    event.stopPropagation();

	    var callEvent:TractCallEvent = new TractCallEvent(TractCallEvent.CALL_CLICK, CallView(event.currentTarget));
	    
	    dispatchEvent(callEvent);
	}

	private function call_doubleClickHandler(event:MouseEvent):void 
	{
	    var callEvent:TractCallEvent = new TractCallEvent(
	        TractCallEvent.CALL_DOUBLE_CLICK, CallView(event.target));
	    
	    dispatchEvent(callEvent);
	}

	private function controlPoint_mouseDownHandler(event:MouseEvent):void 
	{
	    event.preventDefault();
	    event.stopPropagation();
	    
	    var callEvent:TractPointEvent = new TractPointEvent(TractPointEvent.POINT_MOUSE_DOWN, 
	        TractPointEvent.TRACT_CONTROL_POINT, ControlPointView(event.target));
	    
	    dispatchEvent(callEvent);
	}

	private function endPoint_mouseDownHandler(event:MouseEvent):void 
	{
	    event.preventDefault();
	    event.stopPropagation();
	    
	    var callEvent:TractPointEvent = new TractPointEvent(TractPointEvent.POINT_MOUSE_DOWN, 
	        TractPointEvent.TRACT_END_POINT, ControlPointView(event.target));
	    
	    dispatchEvent(callEvent);
	}

	private function startPoint_mouseDownHandler(event:MouseEvent):void 
	{
	    event.preventDefault();
	    event.stopPropagation();
	    
	    var callEvent:TractPointEvent = new TractPointEvent(TractPointEvent.POINT_MOUSE_DOWN, 
	        TractPointEvent.TRACT_START_POINT, ControlPointView(event.target), true, true);

	    dispatchEvent(callEvent);
	}

}
}