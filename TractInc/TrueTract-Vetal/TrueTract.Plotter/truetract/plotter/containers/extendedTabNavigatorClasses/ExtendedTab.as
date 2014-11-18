package truetract.plotter.containers.extendedTabNavigatorClasses
{

	import flash.events.Event;
	import mx.controls.Button; 
	import flash.display.InteractiveObject;
	import flash.events.MouseEvent;
	import mx.events.ItemClickEvent;
	
	//the adobe tab component (marked as private but this seems to work for extending)
	import mx.controls.tabBarClasses.*;
	
	
	import mx.core.mx_internal;
	
	import mx.events.DragEvent;
	import mx.managers.DragManager;
	import mx.core.DragSource;
	import flash.display.DisplayObject;
	import mx.events.IndexChangedEvent;
	
	import mx.core.IFlexDisplayObject;
	
	
	use namespace mx_internal;
	
	[Style(name="showCloseButton", type="Boolean")]
	public class ExtendedTab extends Tab
	{
		
		private var closeButton:Button;
		private var dropIndicatorClass:Class = ExtendedTabDropIndicator;
		private var tabDropIndicator:IFlexDisplayObject;
		
		public function ExtendedTab()
		{
			
			super();
			
			mouseChildren = true;
			
			//use if text align is right
			//setStyle("paddingRight",15);
			setStyle("textAlign","left");
			setStyle("labelPlacement","right");
			
			addEventListener("creationComplete", handleCreationComplete);
			addEventListener(MouseEvent.MOUSE_MOVE,handleMouseMove);
			
			addEventListener(DragEvent.DRAG_ENTER,handleDragEnter);
			addEventListener(DragEvent.DRAG_DROP,handleDragDrop);
			addEventListener(DragEvent.DRAG_EXIT,handleDragExit);
			addEventListener(DragEvent.DRAG_OVER,handleDragOver);
		}

		//add close button and drop indication 
		override protected function createChildren():void{
			
			super.createChildren();
			
			if (!closeButton) {
				closeButton = new Button();
				closeButton.width = 11;
				closeButton.height = 11;
				closeButton.name="CloseButton"
				closeButton.styleName="CloseButton";
				closeButton.mouseEnabled =true;

				addChild(closeButton);
				closeButton.move(width-16,textField.y+4);
			
				//the tab gets focus on mouse down so the only event 
				//we can use is mouse down on the button for close
				closeButton.addEventListener(MouseEvent.MOUSE_DOWN,handleMouseDown);
			}
			
			//create the drop indicator
			tabDropIndicator = IFlexDisplayObject(new dropIndicatorClass());
			addChildAt(DisplayObject(tabDropIndicator),numChildren-1)
			tabDropIndicator.visible=false;
		}
		
		override protected function updateDisplayList(w:Number, h:Number):void
		{
		    super.updateDisplayList(w, h);
		    
            var showCloseButton:Boolean = this.getStyle("showCloseButton") == true;
            
            closeButton.visible = showCloseButton;
		}

		/**
		 * if the drag format is correct in form the user that
		 * the location is acceptable for the drop
		 **/	
		private function handleDragEnter(event:DragEvent):void{
	        if (event.dragSource.hasFormat('tabButton')) {
	            var dropTarget:ExtendedTab=ExtendedTab(event.currentTarget);
                DragManager.acceptDragDrop(dropTarget);
                
                //make sure it is topmost display object and show it
               	setChildIndex(DisplayObject(tabDropIndicator),numChildren-1)
				tabDropIndicator.visible=true;	  
            }
		}
		
		/**
		 * basically draw an indicator to show user where the tab 
		 * will be inserted
		 **/
		private function handleDragOver(event:DragEvent):void{
			if (event.dragSource.hasFormat('tabButton')) {
                
 			}
		}
		
		/**
		 * clean up the drag drop indicator
		 **/
		private function handleDragExit(event:DragEvent):void{
			//hide it
			tabDropIndicator.visible=false;
		}
		
		/**
		 * begin the drag operation
		 **/
		private function handleMouseMove(event:MouseEvent):void{
			if (event.buttonDown){
				startTabDrag(event,"tabButton");
			}	
		}
		
		/**
		 * dispatch the drop event to the parent tab navigator
		 **/
		private function handleDragDrop(event:DragEvent):void {
            
            var DragDropEvent:DragEvent = new DragEvent("TAB_DRAG_DROP",true
            ,true,event.dragInitiator,event.dragSource,event.action,event.ctrlKey
            ,event.altKey,event.shiftKey);
            
            dispatchEvent(DragDropEvent);
                        
            //hide it
			tabDropIndicator.visible=false;
			
        }
		
		/**
		 * set up a proxy and begin the drag operation
		 * */
		private function startTabDrag(event:MouseEvent, format:String):void {
            var dragInitiator:Button=Button(event.currentTarget);
            
            var ds:DragSource = new DragSource();
            ds.addData(this,format);

            var dragProxy:ExtendedTab = new ExtendedTab();
            dragProxy.label=label;
            dragProxy.setStyle("icon",getStyle("icon"));
            dragProxy.width = width;
            dragProxy.height = height;
                        
            DragManager.doDrag(dragInitiator, ds, event, dragProxy);
        }
		
		
		
		/**
		 * handle the passing of an event to the parent
		 * Note: We have to unfortunately close on mouse down
		 * as I’ve yet to figure out how to stop the tab selecting
		 * and causing index problems
		 **/
		private function handleMouseDown(event:MouseEvent):void{

			event.stopPropagation();
			event.preventDefault();
			
			if (event.target is Button && event.target.name=="CloseButton"){
				var closeEvent:ItemClickEvent = new ItemClickEvent("TAB_CLOSE_CLICK", true, true);
				closeEvent.index = parent.getChildIndex(this)
				
				closeButton.removeEventListener(MouseEvent.MOUSE_DOWN,handleMouseDown);
			
				removeEventListener("creationComplete", handleCreationComplete);
			
				dispatchEvent(closeEvent);
				
			}
			
		}
				
		/**
		 * adjust position of button when creation is complete
		 **/
		private function handleCreationComplete(event:Event):void{
			if (this.getStyle("showCloseButton")==true){
				closeButton.move(width-16, height / 2 - closeButton.height / 2);
			}
		}
	
		/**
		 * swap the button to the top of the z/depth order
		 **/
		override mx_internal function layoutContents(unscaledWidth:Number,unscaledHeight:Number, offset:Boolean):void{
	    	super.layoutContents(unscaledWidth, unscaledHeight, offset);
    		
    		if (this.getStyle("showCloseButton")==true){
	    		setChildIndex(getChildByName("CloseButton") , numChildren-1);
    			closeButton.move(width-16, height / 2 - closeButton.height / 2);
    		} 

    		//make sure we set this otherwise the tabs are resized and 
    		//squished together using measured width to get a proper 
    		//tab size
    		explicitWidth = measuredWidth;
	    }
	}
}