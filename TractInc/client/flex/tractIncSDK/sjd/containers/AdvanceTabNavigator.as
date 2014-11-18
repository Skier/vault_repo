package sjd.containers{
	
	import flash.events.MouseEvent;
	import flash.display.DisplayObject;
	
	import mx.core.DragSource;
	import mx.core.IUIComponent;
	import mx.containers.TabNavigator;
	import mx.controls.tabBarClasses.Tab;
	import mx.controls.Button;
	import mx.controls.TabBar;
	import mx.events.FlexEvent;
	import mx.events.DragEvent;
	import mx.managers.DragManager;
	import sjd.utils.Constant;
	import mx.core.Application;

	/**
	 * @class AdvanceTabNavigator
	 * @brief A New TabNavigator with Drag/Drop Enabled and Close Button, fix the bug with HistoryManager
	 * @author Jove
	 * @version 1.2
	 */
  	public class AdvanceTabNavigator extends TabNavigator	{
		
		protected var tabCloseButton:Button = new Button();
		protected var currentTab:Tab;
		
		private var _tabDragEnable:Boolean = false;
		private var _tabCloseEnable:Boolean = false;
		
		public function  set tabDragEnable(enable:Boolean):void{
			this._tabDragEnable = enable;
			this.callLater(enableTabDrag, [enable]);
		}
		
		public function  get tabDragEnable():Boolean{
			 return this._tabDragEnable;
		}
		
		public function  set tabCloseEnable(enable:Boolean):void{
			this._tabCloseEnable = enable;
			this.callLater(enableButton, [enable]);
		}
		
		public function  get tabCloseEnable():Boolean{
			 return this._tabCloseEnable;
		}
		
		public function AdvanceTabNavigator(){
			super();
			this.addEventListener(FlexEvent.CREATION_COMPLETE, buildButton);
		}
		
		protected function buildButton(enable:Boolean):void{
			tabCloseButton.width=10;
			tabCloseButton.height=10;
			tabCloseButton.toolTip = "close";
			tabCloseButton.focusEnabled=false;
			tabCloseButton.setStyle("upSkin", Constant.WINDOW_CLOSE_BUTTON_1);
			tabCloseButton.setStyle("overSkin", Constant.WINDOW_CLOSE_BUTTON_2);
			tabCloseButton.setStyle("downSkin", Constant.WINDOW_CLOSE_BUTTON_2);
			tabCloseButton.visible = false;
			//Creating a hand cursor over a component
			tabCloseButton.buttonMode = true;
			tabCloseButton.useHandCursor = true;
			tabCloseButton.mouseChildren = false;
			
			rawChildren.addChild(tabCloseButton);
		}
		
		protected function enableButton(enable:Boolean):void{
			if(tabCloseButton != null){
				if(enable){
					this.addEventListener(MouseEvent.MOUSE_OVER, addCloseButton);
					tabCloseButton.addEventListener(MouseEvent.CLICK, tabCloseButton_clickHandler);
				}else{
					this.removeEventListener(MouseEvent.MOUSE_OVER, addCloseButton);
					tabCloseButton.removeEventListener(MouseEvent.CLICK, tabCloseButton_clickHandler);
				}
			}
		}
		
		protected function enableTabDrag(enable:Boolean):void{
			if(tabBar != null){
				if(enable){
					this.addEventListener(MouseEvent.MOUSE_MOVE, moveTab);
					tabBar.addEventListener(DragEvent.DRAG_ENTER, doDragEnter);
					tabBar.addEventListener(DragEvent.DRAG_DROP, doDragDrop);
					tabBar.addEventListener(DragEvent.DRAG_OVER, doDragOver);
					tabBar.addEventListener(DragEvent.DRAG_EXIT, doDragExit);
				}else{
					this.removeEventListener(MouseEvent.MOUSE_MOVE, moveTab);
					tabBar.removeEventListener(DragEvent.DRAG_ENTER, doDragEnter);
					tabBar.removeEventListener(DragEvent.DRAG_DROP, doDragDrop);
					tabBar.removeEventListener(DragEvent.DRAG_OVER, doDragOver);
					tabBar.removeEventListener(DragEvent.DRAG_EXIT, doDragExit);
				}
			}
		}
		
		protected function addCloseButton(event:MouseEvent):void{
			if(event.target != currentTab && event.target != tabCloseButton ){
				tabCloseButton.visible = false;
			}
			
			if(event.target is Tab && !DragManager.isDragging){
				// From: m.maher 12/19/2006
                // we really need to see if this tab is a direct
                // disendent of this tabnavigator so that tabnavigators inside of
                // this object don't display the close behaiour
				for each (var childTab:Object in this.tabBar.getChildren()) {
					if(childTab == event.target) {
						currentTab= Tab(event.target);
						tabCloseButton.x = currentTab.x + currentTab.width - 14;
						tabCloseButton.y = currentTab.y + ( currentTab.height - 10 ) / 2;
						tabCloseButton.visible = true;
						break;                                          
					}
				}
				//currentTab= Tab(event.target);
				//tabCloseButton.x = currentTab.x + currentTab.width - 14;
				//tabCloseButton.y = currentTab.y + ( currentTab.height - 10 ) / 2;
				//tabCloseButton.visible = true;
			}
		}		
		
		protected function tabCloseButton_clickHandler(event:MouseEvent):void{
			removeChildAt(tabBar.getChildIndex(currentTab));
			tabCloseButton.visible = false;
 		}
 		
 		protected function moveTab(event:MouseEvent):void{
			if(event.target is Tab){
				var dragInitiator:Tab=Tab(event.target);
				var ds:DragSource = new DragSource();
				var tabProxy:Tab = new Tab();
				tabProxy.label = dragInitiator.label;
				DragManager.doDrag(dragInitiator, ds, event, tabProxy);
			}
 		}
 		
 		protected function doDragEnter(event:DragEvent):void {
        	if(tabBar.getChildIndex(DisplayObject(event.dragInitiator)) >= 0){
	        	var dropTarget:IUIComponent=IUIComponent(event.currentTarget);
	        	if(dropTarget == this.tabBar){
		        	DragManager.acceptDragDrop(dropTarget);
	        	}
        	}
        }
        
        protected function doDragOver(event:DragEvent):void {
        	if(tabBar.getChildIndex(DisplayObject(event.dragInitiator)) >= 0){
        		tabCloseButton.visible = false;
	        	var dragX:Number = tabBar.mouseX;
	        	var targetTabIndex:Number = -1;
	        	for(var i:Number = 0; i < tabBar.getChildren().length; i++){
	        		if(dragX < tabBar.getChildAt(i).x){
	        			targetTabIndex = i;
	        			tabBar.graphics.clear();
						tabBar.graphics.beginFill(0x000000);
						tabBar.graphics.lineStyle(0, 0x000000);
						tabBar.graphics.drawRect(
											tabBar.getChildAt(targetTabIndex).x - 2,
											2,
											1,
											tabBar.getChildAt(targetTabIndex).height - 3);
						tabBar.graphics.endFill();
	        			break;
	        		}
	        	}
	        	if(targetTabIndex < 0
        			&& dragX > tabBar.getChildAt(tabBar.getChildren().length - 1).x
        			&& dragX < tabBar.getChildAt(tabBar.getChildren().length - 1).x + tabBar.getChildAt(tabBar.getChildren().length - 1).width){
	        		targetTabIndex = tabBar.getChildren().length;
	        		tabBar.graphics.clear();
					tabBar.graphics.beginFill(0x000000);
					tabBar.graphics.lineStyle(0, 0x000000);
					tabBar.graphics.drawRect(
										tabBar.getChildAt(targetTabIndex - 1).x + tabBar.getChildAt(targetTabIndex - 1).width - 3,
										2,
										1,
										tabBar.getChildAt(targetTabIndex - 1).height - 3);
					tabBar.graphics.endFill();
	        	}
	        	if(targetTabIndex >= 0){
	        		event.dragSource.addData(targetTabIndex, Constant.TARGET_TAB_INDEX);
	        	}
        	}
        }
        
        protected function doDragDrop(event:DragEvent):void {
			if(tabBar.getChildIndex(DisplayObject(event.dragInitiator)) >= 0 && event.dragSource.hasFormat(Constant.TARGET_TAB_INDEX)){
	        	tabBar.graphics.clear();
				var sourceTabIndex:Number = tabBar.getChildIndex(DisplayObject(event.dragInitiator));
				var targetTabIndex:Number = Number(event.dragSource.dataForFormat(Constant.TARGET_TAB_INDEX));
				
				if(sourceTabIndex < targetTabIndex || targetTabIndex == tabBar.getChildren().length){
					targetTabIndex--;
				}
				if(sourceTabIndex == targetTabIndex){
					return;
				}
				
				var sourceTab:DisplayObject = this.getChildAt(sourceTabIndex);
				this.removeChildAt(sourceTabIndex);
				this.addChildAt(sourceTab, targetTabIndex);
				this.selectedIndex = targetTabIndex;
				
			}
        }
        
        protected function doDragExit(event:DragEvent):void {
        	if(tabBar.getChildIndex(DisplayObject(event.dragInitiator)) >= 0){
				tabBar.graphics.clear();
        	}
		}
		
	
	
		override public function loadState(state:Object):void{
			var newIndex:int = state ? int(state.selectedIndex) : 0;
			if(newIndex < this.numChildren && this.getChildAt(newIndex)){
				super.loadState(state);
			}
		}
	}
}