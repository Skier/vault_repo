package truetract.plotter
{
    import flash.display.BitmapData;
    import flash.display.DisplayObject;
    import flash.display.Shape;
    import flash.events.Event;
    import flash.events.KeyboardEvent;
    import flash.events.MouseEvent;
    import flash.external.ExternalInterface;
    import flash.geom.Matrix;
    import flash.geom.Point;
    import flash.geom.Rectangle;
    import flash.utils.ByteArray;
    import flash.utils.getDefinitionByName;
    
    import mx.collections.ArrayCollection;
    import mx.collections.ItemResponder;
    import mx.containers.VBox;
    import mx.containers.ViewStack;
    import mx.controls.Alert;
    import mx.controls.Button;
    import mx.controls.ComboBox;
    import mx.controls.DataGrid;
    import mx.controls.List;
    import mx.controls.TextInput;
    import mx.controls.ToggleButtonBar;
    import mx.controls.dataGridClasses.DataGridColumn;
    import mx.core.Application;
    import mx.core.ClassFactory;
    import mx.core.Container;
    import mx.core.UIComponent;
    import mx.effects.Move;
    import mx.effects.Parallel;
    import mx.effects.Resize;
    import mx.events.CloseEvent;
    import mx.events.DataGridEvent;
    import mx.events.FlexEvent;
    import mx.events.ListEvent;
    import mx.events.ValidationResultEvent;
    import mx.managers.CursorManager;
    import mx.managers.FocusManager;
    import mx.managers.PopUpManager;
    import mx.rpc.Responder;
    import mx.validators.NumberValidator;
    
    import truetract.plotter.components.ExtendedDataGrid;
    import truetract.plotter.components.ScaleBar;
    import truetract.plotter.components.TractPrintView;
    import truetract.plotter.components.TractView;
    import truetract.plotter.components.tractViewClasses.*;
    import truetract.plotter.components.tractViewClasses.Events.*;
    import truetract.plotter.components.tractViewClasses.call.CallView;
    import truetract.plotter.components.tractViewClasses.call.factories.CallViewFactory;
    import truetract.plotter.components.tractViewClasses.call.propertyViews.*;
    import truetract.plotter.components.tractViewClasses.events.TractCallEvent;
    import truetract.plotter.components.tractViewClasses.events.TractPointEvent;
    import truetract.plotter.containers.ExtendedTitleWindow;
    import truetract.plotter.containers.GeoCanvas;
    import truetract.plotter.containers.dockerClasses.DockableToolBar;
    import truetract.plotter.domain.*;
    import truetract.plotter.domain.callparams.*;
    import truetract.plotter.domain.dictionary.DictionaryRegistry;
    import truetract.plotter.tools.*;
    import truetract.plotter.utils.*;
    import truetract.plotter.validators.GeoBearingValidator;
    import truetract.plotter.validators.ValidatorsGroup;
    
    public class Plotter_code extends VBox
    {

        [Bindable] public var tractView:TractView;
        [Bindable] public var drawingSurface:GeoCanvas;
        [Bindable] public var tractCallsToolbar:ExtendedTitleWindow;
        [Bindable] public var traverseInputToolbar:ExtendedTitleWindow;
        [Bindable] public var tractCallsList:ExtendedDataGrid;
        [Bindable] public var bearingTxt:TextInput;
        [Bindable] public var distanceTxt:TextInput;
        [Bindable] public var viewStack:ViewStack;
        [Bindable] public var tractPrintView:TractPrintView;
        [Bindable] public var tractTool:TractTool;
        [Bindable] public var uomList:ComboBox;
        [Bindable] public var traverseInputVG:ValidatorsGroup;
        [Bindable] public var bearingValidator:GeoBearingValidator;
        [Bindable] public var callsTlbAdjust:Parallel;
        [Bindable] public var callsTlbMove:Move;
        [Bindable] public var callsTlbResize:Resize;
        [Bindable] public var horisontalToolBar:Container;
        [Bindable] public var verticalToolBar:Container;
        
        [Bindable] public var Model:Tract;
        [Bindable] public var currentUser:User;

		[Bindable] public var defaultUOM:UnitOfMeasure = ScaleValue.Default.uom;
    
        [Bindable] public var showTraverseInputPanel:Boolean = true;
        [Bindable] public var showTractCallsPanel:Boolean = false;

		private var selectedCall:CallView;
		private var selectedText:TractTextObjectView;
        private var editMode:Boolean;

        private var _activeTool:AbstractTool;
        [Bindable] public function get activeTool():AbstractTool { return _activeTool; }
        public function set activeTool(tool:AbstractTool):void 
        {
            try 
            {
                if (activeTool) 
                {
                    activeTool.Deactivate();
                }

                _activeTool = tool;
                activeTool.Activate();             
            }
            catch (e:Error)
            {
                Alert.show( e.message );
            }
        }

		public function initTract(tract:Tract):void 
		{
		    clean();

            Model = tract;

            tractView.move(drawingSurface.StartPoint.x, drawingSurface.StartPoint.y);

            setEditMode(true);

            activeTool = tractTool;
            adjustTractCallListWidth();

            Application.application.callLater(ZoomAll);
		}

        public function clean():void 
        {
	        tractView.tract = Model = null;
            tractView.validateNow();

	        drawingSurface.ResetStartPointPosition();
            drawingSurface.Scale = ScaleValue.Default.clone();

            viewStack.selectedIndex = 0;
        }

        public function AddCall( call:TractCall ):void 
        {

            tractView.AddCall(call);

            fitToScreen();
            adjustTractCallListWidth();            
            
            if (Model.Calls.length == 1 && !showTractCallsPanel) {
                showTractCallsPanel = true;
            }

			bearingTxt.setFocus();
			adjustTractCallListWidth();
        }

        public function AddCallAt(call:TractCall, index:int):void 
        {
            try 
            {
                tractView.AddCallAt(call, index);
            } 
            catch (e:Error) 
            {
                Alert.show(e.message);
            }

            fitToScreen();
            adjustTractCallListWidth()
        }

        public function UpdateCall(call:TractCall):void 
        {
            
            var propView:CallPropertiesView = 
                CallViewFactory.Instance().GetCallPropertiesView(call);
            
            PopUpManager.addPopUp(propView, this, true);
            PopUpManager.centerPopUp(propView);
                        
            propView.Responder = new ItemResponder(
                function (call:TractCall, token:Object = null):void 
                {
                    call.CreatedByMouse = 0;
                    tractView.UpdateCall(call);
                    
                    fitToScreen();
                    
                }, null);

            adjustTractCallListWidth();
        }

		public function InsertCall():void 
		{
		    var selectedCall:TractCall = TractCall(tractCallsList.selectedItem);
            var tangentInBearing:GeoBearing = new GeoBearing();
            
            if (selectedCall.CallOrder > 0)
            {
                var previousCall:TractCall = Model.GetCallByOrder(selectedCall.CallOrder - 1);
                var previousCallView:CallView = tractView.GetCallView(previousCall);
                
                tangentInBearing = previousCallView.Shape.bearingOut;
            }
            
            var dialog:CallTypeSelector =  CallTypeSelector.Open(this, true);
            dialog.TangentInBearing = tangentInBearing;
            dialog.Responder = new ItemResponder(
    		    function (call:TractCall, token:Object = null):void {
    		        AddCallAt(call, selectedCall.CallOrder);
    		    }, 
    		    null);
    		    
        	adjustTractCallListWidth();
		}

        public function DeleteCall(call:TractCall):void 
        {

            try 
            {
                tractView.DeleteCall(call);
                fitToScreen();

                if (call == selectedCall.Model) 
                {
                    selectedCall = null;
                }
                
            } 
            catch (e:Error) 
            { 
                Alert.show(e.message) 
            }

            adjustTractCallListWidth();
        }

        public function DeleteTextObject(text:TractTextObject):void 
        {
        }

        public function DeleteSelectedCall():void 
        {
            if (selectedCall == null) { return }
            
            Alert.show("Are you sure you want to delete selected Call ?", "Confirm Deleting", 
                Alert.YES | Alert.NO, null, 
                
                function(event:CloseEvent):void 
                {
                    if (event.detail == Alert.YES) 
                    {
                        DeleteCall(selectedCall.Model);
                    }
                },
                
                null, Alert.NO);
        }

        public function DeleteSelectedText():void 
        {
            if (selectedText == null) { return }
            
            Alert.show("Are you sure you want to delete selected Text ?", "Confirm Deleting", 
                Alert.YES | Alert.NO, null, 
                
                function(event:CloseEvent):void 
                {
                    if (event.detail == Alert.YES) {
                        DeleteTextObject(selectedText.Model);
                    }
                }, 
                null, Alert.NO);
        }

        public function Print():void 
        {
            tractPrintView.tract = null;
            tractPrintView.tract = Model; //FIXME: strange behaviour

            viewStack.selectedIndex = 1;
        }

        public function ZoomAll():void 
        {
            var tractBounds:Rectangle = tractView.getComponentBounds();

            var surfaceWidth:Number = drawingSurface.width - 20;
            var surfaceHeight:Number = drawingSurface.height - 20;

            if (tractBounds.width > 8 || tractBounds.height > 8) 
            {
                drawingSurface.Scale = tractView.calcaluateRequiredScaling(
                    surfaceWidth, surfaceHeight, drawingSurface.Scale.uom);
/*                 var tractWidthFeets:Number = tractBounds.width / drawingSurface.Scale.PointsInOneFeet;
                var tractHeightFeets:Number = tractBounds.height / drawingSurface.Scale.PointsInOneFeet;

                var pointInOneFeetWidthRequired:Number = surfaceWidth / tractWidthFeets;
                var pointInOneFeetHeightRequired:Number = surfaceHeight / tractHeightFeets;

                drawingSurface.Scale.PointsInOneFeet = Math.min(pointInOneFeetWidthRequired, pointInOneFeetHeightRequired);
                drawingSurface.Scale = drawingSurface.Scale;
 */            }
            
            Application.application.callLater(centreTract);
        }
        
        public function Zoom(scaleDelta:Number, zoomPoint:Point):void 
        {
            var zoomPositionBefore:GeoPosition = drawingSurface.GetGeoPosition(zoomPoint);
            var scale:ScaleValue = drawingSurface.Scale.clone();
            scale.PointsInOneFeet += scaleDelta;
            
            drawingSurface.Scale = scale;

            var zoomPositionAfter:GeoPosition = drawingSurface.GetGeoPosition(zoomPoint);
        
            var deltaX:Number = (zoomPositionAfter.Easting - zoomPositionBefore.Easting) 
                * drawingSurface.Scale.PointsInOneFeet;
            var deltaY:Number = - (zoomPositionAfter.Northing - zoomPositionBefore.Northing) 
                * drawingSurface.Scale.PointsInOneFeet;

            drawingSurface.scroll(deltaX, deltaY);
        }
        
        public function ZoomIn():void 
        {
            var scaleDelta:Number = 0.3 * drawingSurface.Scale.PointsInOneFeet;
            var zoomPoint:Point = new Point(drawingSurface.width / 2, drawingSurface.height / 2);
            
            Zoom(scaleDelta, zoomPoint);
        }

        public function ZoomOut():void 
        {
            var scaleDelta:Number = - 0.3 * drawingSurface.Scale.PointsInOneFeet;
            var zoomPoint:Point = new Point(drawingSurface.width / 2, drawingSurface.height / 2);
            
            Zoom(scaleDelta, zoomPoint);
        }

        public function addToolbarButton(button:Button, position:Number, horisontal:Boolean = true):void
        {
            if (!button) return;
            if (button.parent)
                button.parent.removeChild(button);

            button.styleName="toolButton";
            
            var container:Container = horisontal ? horisontalToolBar : verticalToolBar;
            
            position = Math.min(position, container.numChildren);
            container.addChildAt(button, position);
        }

        public function openLineSelector():void
        {
            var popup:LinePropertiesView = LinePropertiesView(
                PopUpManager.createPopUp(this, LinePropertiesView, true));
            PopUpManager.centerPopUp(popup);

            popup.TangentInBearing = tractView.GetLastBearing();
            popup.Responder = new ItemResponder(
    		    function (call:TractCall, token:Object = null):void 
    		    {
    		       AddCall(call);
    		    }, 
    		    null);
        }

        public function openCurveSelector():void
        {
            var dialog:CallTypeSelector =  CallTypeSelector.Open(this, true);

            dialog.TangentInBearing = tractView.GetLastBearing();
            dialog.lineType.visible = dialog.lineType.includeInLayout = false;
            dialog.curveTypeList.setFocus();
            dialog.Responder = new ItemResponder(
            
    		    function (call:TractCall, token:Object = null):void 
    		    {
    		       AddCall(call);
    		    }, 
    		    null);
        }
        
        private function highlightCallView(callView:CallView):void 
        {
		    if (selectedCall) 
		    {
		        selectedCall.Highlighted = false;
		    }

		    callView.Highlighted = true;
		    
		    selectedCall = callView;
        }

        private function setEditMode(mode:Boolean):void 
        {
            this.editMode = mode;
            
            tractView.enabled = mode;
        }

        private function fitToScreen():void 
        {
            var tractBounds:Rectangle = tractView.getComponentBounds();

            var surfaceWidth:Number = drawingSurface.width - 20;
            var surfaceHeight:Number = drawingSurface.height - 20;

            var widthRate:Number = tractBounds.width / surfaceWidth;
            var hightRate:Number = tractBounds.height / surfaceHeight;

            if ( Math.max(widthRate, hightRate) > 1 ) 
            {
                var scale:ScaleValue = drawingSurface.Scale;
                scale.uomValue *= Math.max(widthRate, hightRate);
                tractView.surfaceScale = drawingSurface.Scale = scale;
            }

            centreTract();
        }

        private function centreTract():void 
        {
            var tractBounds:Rectangle = tractView.getComponentBounds();

            var tractMiddlePoint:Point = new Point (
                tractView.x + tractBounds.x + tractBounds.width / 2,
                tractView.y + tractBounds.y + tractBounds.height / 2
            );

            var screenMiddlePoint:Point = new Point(
                (drawingSurface.width / drawingSurface.scaleX) / 2, 
                (drawingSurface.height / drawingSurface.scaleY) / 2
            );
            
            var deltaX:Number = screenMiddlePoint.x - tractMiddlePoint.x;
            var deltaY:Number = screenMiddlePoint.y - tractMiddlePoint.y;

            // apply the delta to all components                
            drawingSurface.scroll(deltaX, deltaY);

            if (tractView.tempLine)
            {
                var p:Point = new Point(drawingSurface.mouseX, drawingSurface.mouseY);
                var line:GeoLine = GeoLine.createByEndPosition(
                    tractView.tempLine.Shape.startPosition, drawingSurface.GetGeoPosition(p));
                tractView.tempLine.Shape = line;
            }
        }

        private function cleanInputFields():void 
        {
            bearingTxt.text = "";
            distanceTxt.text = "";
            uomList.selectedItem = defaultUOM;
        }
        
        private function isTractFieldValid():Boolean 
        {
            return traverseInputVG.validate(true);
        }

        private function createLineCall(bearing:GeoBearing, distance:Number, uom:UnitOfMeasure):TractCall 
        {
            var call:TractCall = new TractCall();

            call.Params.addItem(new BearingParam(bearing));
            call.Params.addItem(new DistanceParam(distance, uom));
		    call.CallType = TractCall.CALL_TYPE_LINE;

		    return call;
        }

        private function adjustTractCallListWidth():void 
        {
            var optimalGridWidth:Number = 0;

            var maxRefNameWidth:Number = 
                tractCallsList.getOptimalColumnWidth(tractCallsList.columns[1]);

            optimalGridWidth = 
                tractCallsList.columns[0].width + 
                tractCallsList.columns[2].width + 
                maxRefNameWidth;

            if (tractCallsList.getVerticalScrollBar())
                optimalGridWidth += tractCallsList.getVerticalScrollBar().width;

//            optimalGridWidth += 10; //TODO: something is missing. Need to figure out what is it.
            var padding:Number = tractCallsToolbar.width - tractCallsList.width;

            resizeTractCallsPanel(padding + optimalGridWidth);
        }
        
        private function resizeTractCallsPanel(newWidth:Number):void {
            var oldWidth:Number = tractCallsToolbar.width;

            callsTlbResize.widthTo = newWidth;
            callsTlbMove.xTo = tractCallsToolbar.x;

            if (tractCallsToolbar.x + newWidth > Application.application.width)
            {
                callsTlbMove.xTo = Application.application.width - newWidth - 5;
            }

            callsTlbAdjust.play();
        }

// ---- UI COMPONENTS EVENT HANDLERS -----

		protected function drawingSurface_mouseWheelHandler(event:MouseEvent):void 
		{
            var scaleDelta:Number = 0.3 * drawingSurface.Scale.PointsInOneFeet; //30 % from current scaling
            scaleDelta *= event.delta / Math.abs(event.delta); //zoomIn or zoomOut ?

            var drawingSurfaceMousePoint:Point = new Point (
                drawingSurface.mouseX, drawingSurface.mouseY);

            Zoom(scaleDelta, drawingSurfaceMousePoint);
		}

		protected function drawingSurface_mouseMoveHandler(event:MouseEvent):void 
		{
		    if (activeTool)
                activeTool.onPlotterMouseMove(event);
		}

		protected function drawingSurface_mouseDownHandler(event:MouseEvent):void 
		{
		    if (activeTool)
                activeTool.onPlotterMouseDown(event);
		}
		
		protected function drawingSurface_mouseUpHandler(event:MouseEvent):void 
		{
		    if (activeTool)
		        activeTool.onPlotterMouseUp(event);
		}

        protected function drawingSurface_keyUpHandler(event:KeyboardEvent):void 
        {
            if (event.keyCode != 46) { return; } // handle only "Delete" button

            if (selectedCall) 
            {
                DeleteSelectedCall();
            } 
            else if (selectedText) 
            {
                DeleteSelectedText();
            }
        }

        protected function tractCall_clickHandler(event:TractCallEvent):void 
        {
            if (!editMode) return;
                
		    highlightCallView(event.callView);
		    
		    var index:Number = Model.Calls.getItemIndex(event.callView.Model);
		    tractCallsList.selectedIndex = index;
		    tractCallsList.scrollToIndex(index);
		    drawingSurface.setFocus();
        }

        protected function tractCall_doubleClickHandler(event:TractCallEvent):void 
        {
            if (!editMode) return;
            
            UpdateCall(event.callView.Model);
        }
		
        protected function tractPoint_mouseDownHandler(event:TractPointEvent):void 
        {
            switch (event.pointType) 
            {
                case TractPointEvent.TRACT_START_POINT :

                    //do nothing if Tract is closed
                    if (Model.IsClosed) return;
                    
                    //add TempLine to Tract if it doesn't have any calls
                    if (editMode && Model.Calls.length == 0)
                    {
                        tractView.createTempLine();
                        setEditMode(false);
                        return;
                    }

                    //close Tract
                    if (tractView.tempLine) 
                    {
                        if (Model.Calls.length < 2) 
                        {
                            Alert.show("Unable to close Tract. Not enough children count.");
                            return;
                        }
                        
                        tractView.CloseTract(true);
                        
                        setEditMode(true);
                    }
                    
                    break;
                    
                case TractPointEvent.TRACT_END_POINT:
                    if (editMode)
                    {
                        //attach mouse to Tract drawing
                        tractView.createTempLine();
                        setEditMode(false);
                    } 
                    else 
                    {
                        //release mouse from Tract drawing
                        tractView.removeTempLine();
                        setEditMode(true);
                    }
                    break;
                    
                case TractPointEvent.TRACT_CONTROL_POINT:
                    break;
            }
        }

		protected function tractCallList_changeHandler(event:ListEvent):void 
		{
		    var call:TractCall = TractCall(tractCallsList.selectedItem);
		    highlightCallView( tractView.GetCallView(call) );
		}

        protected function tractCallList_doubleClickHandler(event:ListEvent):void 
        {
		    var call:TractCall = TractCall(tractCallsList.selectedItem);

            UpdateCall(call);
        }

        protected function bearingTxt_changeHandler(event:Event):void 
        {
        	var input:TextInput = event.currentTarget as TextInput;
        	
            if (input.text.length < 2) return;
            
			input.text = input.text.replace("\u00b0", "*");
			input.text = input.text.replace("'", "*");
			input.text = input.text.replace("\"", "*");

			var gradFound:Boolean = false;
			var minFound:Boolean = false;
			var secFound:Boolean = false;

			var result:String = "";

			for ( var i:int=0; i < input.text.length; i++ ) {
				if (input.text.charAt(i) == "*" && !gradFound) {
					result += "\u00b0";
					gradFound = true;
				} else if (input.text.charAt(i) == "*" && !minFound) {
					result += "'";
					minFound = true;
				} else if (input.text.charAt(i) == "*" && !secFound) {
					result += "\"";
					secFound = true;
				} else {
					result += input.text.charAt(i);
				}
			}
			
			input.text = result;
        	
            if (bearingValidator.validate().type == ValidationResultEvent.VALID)
            {
                distanceTxt.setFocus();
            }
        }
        
        protected function bearingTxt_enterHandler(event:FlexEvent):void 
        {
            if (bearingTxt.text.length == 1) 
            {
                
                var s:String = bearingTxt.text.toUpperCase();
                
                if (s == 'C' || s == '/') 
                {
                    openCurveSelector();
                    return;
                }
            }

            if (!isTractFieldValid()) return;

            var lineBearing:GeoBearing = GeoBearing.Parse(bearingTxt.text);
            var lineDistance:Number = Number(distanceTxt.text);
            var lineUom:UnitOfMeasure = UnitOfMeasure(uomList.selectedItem);

            AddCall(createLineCall(lineBearing, lineDistance, lineUom));
        }

        protected function uomList_keyDownHandler(event:KeyboardEvent):void 
        {
            if (event.keyCode != 13) return;
            
            if (!isTractFieldValid()) return;
            
            var call:TractCall = createLineCall(GeoBearing.Parse(bearingTxt.text), 
                Number(distanceTxt.text), UnitOfMeasure (uomList.selectedItem));

            AddCall(call);

            cleanInputFields();
        }

        protected function distanceTxt_keyDownHandler(event:KeyboardEvent):void 
        {
            if (event.keyCode != 13 && event.keyCode != 39) {
                return;
            }
                     
            if (!isTractFieldValid()){
                return;
            }
                
            if (event.keyCode == 13) 
            {
                
                var call:TractCall = createLineCall(GeoBearing.Parse(bearingTxt.text), 
                    Number(distanceTxt.text), UnitOfMeasure (uomList.selectedItem));
                
                AddCall(call);

                cleanInputFields();
            } 
            else 
            {
                focusManager.setFocus(uomList);
            }
        }

    }
}