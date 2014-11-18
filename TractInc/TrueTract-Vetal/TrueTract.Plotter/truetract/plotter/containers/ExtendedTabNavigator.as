package truetract.plotter.containers
{
	import flash.display.DisplayObject;
	import flash.events.Event;
	
	import mx.containers.TabNavigator;
	import mx.controls.TabBar;
	import mx.core.Container;
	import mx.core.IFlexDisplayObject;
	import mx.core.mx_internal;
	import mx.events.DragEvent;
	import mx.events.FlexEvent;
	import mx.events.IndexChangedEvent;
	import mx.events.ItemClickEvent;
	import mx.managers.DragManager;
	
	import truetract.plotter.containers.extendedTabNavigatorClasses.*;

	use namespace mx_internal;

	[Event(name="CloseClick",type="mx.events.ItemClickEvent")]
	public class ExtendedTabNavigator extends TabNavigator
	{

		private var dropIndicatorClass:Class = ExtendedTabDropIndicator;
		private var tabDropIndicator:IFlexDisplayObject;

		public function ExtendedTabNavigator()
		{
			super();
			addEventListener("TAB_CLOSE_CLICK",handleTabCloseClick);
			addEventListener("TAB_DRAG_DROP",handleTabDragDrop);
			
			addEventListener(DragEvent.DRAG_ENTER,handleDragEnter);
			addEventListener(DragEvent.DRAG_DROP,handleTabDragDrop);
			addEventListener(DragEvent.DRAG_EXIT,handleDragExit);
			addEventListener(DragEvent.DRAG_OVER,handleDragOver);
			
		}
				
		/**
		 * make sure that the tabbar is our extended version of the adobe tabbar
		 **/
		override protected function createChildren():void{
			tabBar = new ExtendedTabBar();
			tabBar.name = "tabBar";
			tabBar.focusEnabled = false;
			tabBar.styleName = this;

			tabBar.setStyle("borderStyle", "none");
			tabBar.setStyle("paddingTop", 0);
			tabBar.setStyle("paddingBottom", 0);
						
			rawChildren.addChild(tabBar);
						
			//create the drop indicator
			tabDropIndicator = IFlexDisplayObject(new dropIndicatorClass());
			rawChildren.addChildAt(DisplayObject(tabDropIndicator),rawChildren.numChildren-1)
			tabDropIndicator.visible=false;
			
			super.createChildren();
		}
		
		
		/**
		 * accept drag and drop on the navigator as well,
		 * for when there are no tabs present
		 **/
		private function handleDragEnter(event:DragEvent):void{
			if (event.dragSource.hasFormat('tabButton')) {
            	var dropTarget:ExtendedTabNavigator=ExtendedTabNavigator(event.currentTarget);
               	DragManager.acceptDragDrop(dropTarget);
               	
               	//make sure it is topmost display object and show it
               	rawChildren.setChildIndex(DisplayObject(tabDropIndicator),rawChildren.numChildren-1)
				tabDropIndicator.visible=true;	                
				
           	}
		}
		
		/**
		 * draw indicator to show user where the tab 
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
		 * when dragged (even between navigators) if the format is correct
		 * move the tab to the new parent and location if the target is the 
		 * navigator then the new tab is inserted at location 0.
		 **/
		private function handleTabDragDrop(event:DragEvent):void{
	
			if(event.dragInitiator.parent.parent is ExtendedTabNavigator){		
				if (event.dragSource.hasFormat('tabButton')) {
					var oldIndex:int = event.dragInitiator.parent.getChildIndex(DisplayObject(event.dragInitiator));
    	        	var newIndex:int 
    	        	
    	        	if (event.target is ExtendedTab){
    	        		newIndex= event.target.parent.getChildIndex(event.target);
    	        	}
    	        	else{
    	        		newIndex=0;
    	        		tabDropIndicator.visible=false;
    	        	}
	            	
	            	var obj:DisplayObject = ExtendedTabNavigator(event.dragInitiator.parent.parent).removeChildAt(oldIndex);
					addChildAt(obj,newIndex);
	            	
					//validate the visible items for the tab bar.
					ExtendedTabBar(tabBar).validateVisibleTabs();
					
				}
            }
			
            
		}
		
		/**
		 * handle removal of container when close button is clicked
		 **/
	    private function handleTabCloseClick(event:ItemClickEvent):void{
			event.stopPropagation();
			event.preventDefault();
			
			//cleanup
			var obj:DisplayObject = removeChildAt(event.index);
			obj = null;
			
			//dispatch event so other actions can be taken
			var dispEvent:ItemClickEvent = new ItemClickEvent("CloseClick");
			dispEvent.index =event.index;
			dispEvent.item = event.item;
			dispEvent.label = event.label;
			dispEvent.relatedObject = event.relatedObject;
			dispatchEvent(dispEvent);
			
			
		}
		
	}
}