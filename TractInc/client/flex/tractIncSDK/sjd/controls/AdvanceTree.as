package sjd.controls
{
	import mx.controls.Tree;
	import mx.events.DragEvent;
	import mx.managers.DragManager;
	import mx.core.Application;
	import flash.events.MouseEvent;
	import mx.core.EdgeMetrics;
	import mx.core.mx_internal;
	import mx.collections.IViewCursor;
	import mx.collections.ICollectionView;
	import flash.geom.Point;
	import mx.controls.listClasses.IListItemRenderer;
	import mx.collections.XMLListCollection;
	import mx.skins.halo.ListDropIndicator;
	import mx.core.IFlexDisplayObject;
	import flash.display.DisplayObject;
	import mx.events.ListEvent;
	import mx.utils.UIDUtil;
	import mx.core.DragSource;
	import sjd.utils.XMLUtils;
	import flash.events.Event;
	import mx.core.ClassFactory;
	import sjd.controls.treeClasses.AdvanceTreeItemRenderer;
	import mx.controls.treeClasses.TreeItemRenderer;
	import mx.collections.CursorBookmark;
	import mx.controls.treeClasses.HierarchicalViewCursor;
	import flash.utils.setTimeout;
	import mx.events.FlexEvent;
	import mx.events.TreeEvent;
	import mx.core.ScrollPolicy;
	import mx.events.ScrollEvent;
	import mx.events.ScrollEventDirection;

	import sjd.core.sjd_internal;
	
	use namespace mx_internal;
	
	use namespace sjd_internal;

	/**
	 * @class AdvanceTree
	 * @brief A Tree with new drag/drop way and horizontal scroll, fix bug that scroll up/down when open the branch will cause crash.
	 *		  Currently only support XML as DataProvider, if Array, use XMLUtils to convert it to an XML.
	 * @author Jove
	 * @version 1.2
	 */
	public class AdvanceTree extends Tree{
		
		public var allowOutSideDrag:Boolean = false;
		
		
		private var _allowChangeHorizontalScroll:Boolean = true;
		public function set allowChangeHorizontalScroll(value:Boolean):void{
			_allowChangeHorizontalScroll = value;
			if(horizontalScrollPolicy == ScrollPolicy.OFF){
				horizontalScrollPolicy = ScrollPolicy.AUTO;
			}
		}
		public function get allowChangeHorizontalScroll():Boolean{
			return _allowChangeHorizontalScroll;
		}
		
		
		public function AdvanceTree(){
			super();
			itemRenderer = new ClassFactory(AdvanceTreeItemRenderer);
			this.addEventListener(FlexEvent.CREATION_COMPLETE, init);
		}
	    
	    public function init(event:Event):void{
	    	
	    	this.addEventListener(TreeEvent.ITEM_OPENING, setIsExpanding);
	    	
	    	this.addEventListener(TreeEvent.ITEM_OPEN, calculateMaxHorizontalScrollHandle);
	    	this.addEventListener(TreeEvent.ITEM_CLOSE, calculateMaxHorizontalScrollHandle);
	    	
	    	this.addEventListener(ScrollEvent.SCROLL, calculateMaxHorizontalScrollHandle);
	    	
	    	this.addEventListener(TreeEvent.ITEM_OPEN, setIsExpanding);
	    	this.addEventListener(TreeEvent.ITEM_CLOSE, setIsExpanding);
	    }
	    
		
//////////////////////////////////////////////////////////////////////////////////////
//Handle the drag/drop envents: Rewrite the showDropFeedback function to calculate the position of drag indicator(the black line).
//Currently we only support the XML as tree dataProvider, if the data is array, please use the sjd.utils.XMLUtils to convert it to an XML.
//If allowOutSideDrag is True, the drag source can be a List or DataGrid, which dragSource.hasFormat("items").
//And if the "items" is Object, XMLUtils will be used to convert it to XML to add in to the tree.
//////////////////////////////////////////////////////////////////////////////////////	
		
		protected var lastBranchNode:XML = null;
		
		override public function showDropFeedback(event:DragEvent):void{
			
			if(_dropData2){
	    		event.localY = event.localY + _dropData2.rowHeight;
	    	}
	    	
	    	//super.showDropFeedback(event);
	    	
			if (!dropIndicator){
	            var dropIndicatorClass:Class = getStyle("dropIndicatorSkin");
	            if (!dropIndicatorClass)
	                dropIndicatorClass = ListDropIndicator;
	            dropIndicator = IFlexDisplayObject(new dropIndicatorClass());
	
	            var vm:EdgeMetrics = viewMetrics;
	
	            drawFocus(true);
	
	            dropIndicator.x = 2;
	            dropIndicator.setActualSize(listContent.width - 4, 4);
	            dropIndicator.visible = true;
	            listContent.addChild(DisplayObject(dropIndicator));
	
	            if (collection){
	                dragScroll();
	            }
	        }
	
	        var rowNum:Number = calculateDropIndex(event);
	        if (rowNum >= lockedRowCount)
	         	rowNum -= verticalScrollPosition;
	
			var rc:Number = listItems.length;
	        if (rowNum >= rc)
	            rowNum = rc - 1;
	        
	        if (rowNum < 0)
	        	rowNum = 0;
	
	        dropIndicator.y = calculateDropIndicatorY(rc, rowNum);
	        
	         // Adjust for indent
	        var vm2:EdgeMetrics = viewMetrics;
	        var offset:int = 0;
	       
			updateDropData(event);
	       
			var indent:int = 0;
			var depth:int;
			
			if (_dropData2.parent)
			{
				depth = getParentDepth(_dropData2.parent);
				indent = (depth + 1) * getStyle("indentation");
			}
			else 
			{
				indent = getStyle("indentation");
			}
	        if (indent < 0)
	            indent = 0;
	        //position drop indicator
	        //dropIndicator.width = listContent.width - indent;
	        dropIndicator.width = listContent.width - indent + this.horizontalScrollPosition //+ this.maxHorizontalScrollPosition ;
	        //dropIndicator.x = indent + vm2.left + 2;
	        dropIndicator.x = indent + vm2.left + 2 - this.horizontalScrollPosition;
			
			/*if (_dropData2.emptyFolder)
			{
				dropIndicator.y += _dropData2.rowHeight / 2;
			}*/
			
			
			event.localY -= _dropData2.rowHeight / 2;
			var cIndex:Number = super.calculateDropIndex(event) - 1
			cIndex -= this.verticalScrollPosition;
			var cNode:XML = getOpenItemByIndex(cIndex) as XML
			
			
			var uid:String = null;
			if(lastBranchNode){
				uid = UIDUtil.getUID(lastBranchNode);
				drawItem(visibleData[uid], isItemSelected(lastBranchNode), false, uid == caretUID);
			}
				
			var ds:DragSource = event.dragSource;
			
			if(cNode && dataDescriptor.isBranch(cNode) &&
				( (yOff >= 0 && yOff < _dropData2.rowHeight / 4 * 1) || ( yOff > _dropData2.rowHeight / 4 * 3 && yOff <= _dropData2.rowHeight / 4 * 4) )
				){
				dropIndicator.visible = false;
				
				//HightLight
				uid = UIDUtil.getUID(cNode)
				drawItem(visibleData[uid], isItemSelected(cNode), true, uid == caretUID);
								
				lastBranchNode = cNode;
				
 			}else{
 				
 				dropIndicator.visible = true;
 				
 			}
		}
		
		
		
	    override public function calculateDropIndex(event:DragEvent = null):int{
			if (event){
				if(_dropData2){
					event.localY = event.localY + _dropData2.rowHeight / 2;
					
				}else{
					event.localY = event.localY + this.rowHeight / 2;
				}
			}
			
			var result:Number = super.calculateDropIndex(event) - 1;
			
	        return result;
	    }
	    
	    
	    
	    override protected function dragEnterHandler(event:DragEvent):void{
	    	
	        if (event.isDefaultPrevented())
	            return;
			//Support drag from owner, or dragSource.hasFormat("items") 
	        if ((event.dragSource.hasFormat("treeItems") && event.dragInitiator == this) || (allowOutSideDrag && event.dragSource.hasFormat("items")))
	        {
	        	allowDragSelection = false;
	        	
	            DragManager.acceptDragDrop(this);
	            DragManager.showFeedback(event.ctrlKey ?
	                                     DragManager.COPY :
	                                     DragManager.MOVE);
	            showDropFeedback(event);
	            return;
	        }
	        hideDropFeedback(event);
	        DragManager.showFeedback(DragManager.NONE);
	    }
		
		override protected function dragOverHandler(event:DragEvent):void{
			
							
			if (event.isDefaultPrevented())
				return;
	
			if ((event.dragSource.hasFormat("treeItems") && event.dragInitiator == this) || (allowOutSideDrag && event.dragSource.hasFormat("items")))
			{
				DragManager.showFeedback(event.ctrlKey ? DragManager.COPY : DragManager.MOVE);
				showDropFeedback(event);
				return;
			}
			hideDropFeedback(event);
			DragManager.showFeedback(DragManager.NONE);
			
		}
		
		override protected function dragDropHandler(event:DragEvent):void{
			if (event.isDefaultPrevented())
			return;
			//Drop on the Branch
			if(dropIndicator && !dropIndicator.visible){
				//Branch
				hideDropFeedback(event);
				dragDropHandlerBranch(event);
				//return;
			}else{
				hideDropFeedback(event);
				dragDropHandlerLeaf(event);
			}
			
			callLater(calculateMaxHorizontalScrollHandle);
		}
		
		
		protected function dragDropHandlerBranch(event:DragEvent):void{
			if(event.dragSource.hasFormat("treeItems") && lastBranchNode){
				
				if ((event.action == DragManager.MOVE || event.action == DragManager.COPY) && dragMoveEnabled){
					
					var items:Array = event.dragSource.dataForFormat("treeItems") as Array;
					
					if (event.dragInitiator == this){
						var index:int;
			        	var parent:*;
			        	var parentItem:*;
			        	var position:Number = 0;
			        	//get ancestors of the drop target item
			        	
			        	var newItem:XML;
			        	
			        	if(event.action == DragManager.MOVE){
			        		var dropParentStack:Array = getParentStack(_dropData2.parent);
			        		dropParentStack.unshift(_dropData2.parent); //optimize stack method
			        		for (var i:int = 0; i < items.length; i++){
			        			if(items[i] == lastBranchNode){
			        				return;
			        			}
								parent = getParentItem(items[i]);
				            	index = getChildIndexInParent(parent, items[i]);
								//check ancestors of the dropTarget if the item matches, we're invalid
								for each (parentItem in dropParentStack){ 
				            		//we dont want to drop into one of our own sets of children
				            		if (items[i] == parentItem){
				            			return;
				            		}
				              	}
				              					              	
								removeChildItem(parent, items[i], index);
								
								if(lastBranchNode.children()){
			        				position = lastBranchNode.children().length();
			        			}
			        			
			        			addChildItem(lastBranchNode, (items[i] as XML).copy(), position);
			        		}
			        	}else if (event.action == DragManager.COPY){
			        		for (var j:int = 0; j < items.length; j++){
			        			
			        			if(lastBranchNode.children()){
			        				position = lastBranchNode.children().length();
			        			}
			        		
			        			addChildItem(lastBranchNode, (items[i] as XML).copy(), position);
			        		}
			        	}
					}
				}
			}else if(allowOutSideDrag && event.dragSource.hasFormat("items")){
				dragDropHandlerOutsideBranch(event);
			}
			
			callLater(invalidateDisplayList);
		}
		
		
		
		protected function dragDropHandlerLeaf(event:DragEvent):void{
	
			if (event.dragSource.hasFormat("treeItems")){
				//we only support MOVE by default
				if ((event.action == DragManager.MOVE || event.action == DragManager.COPY) && dragMoveEnabled){
					var items:Array = event.dragSource.dataForFormat("treeItems") as Array;
					//Are we dropping on ourselves?
		        	if (event.dragInitiator == this){
		        		//updateDropData(event);
						// If we're dropping onto ourselves or a child of a descendant then dont actually drop
						var dropIndex:* = super.calculateDropIndex(event);
					
						// If we did start this drag op then we need to remove first
			        	var index:int;
			        	var parent:*;
			        	var parentItem:*;
			        	if(event.action == DragManager.MOVE){
				        	//get ancestors of the drop target item
				        	var dropParentStack:Array = getParentStack(_dropData2.parent);
				        	dropParentStack.unshift(_dropData2.parent); //optimize stack method
			        	}
			            for (var i:int = 0; i < items.length; i++){ 
			            	parent = getParentItem(items[i]);
			            	index = getChildIndexInParent(parent, items[i]);
							//check ancestors of the dropTarget if the item matches, we're invalid
							for each (parentItem in dropParentStack){ 
			            		//we dont want to drop into one of our own sets of children
			            		if (items[i] == parentItem)
			            			return;
			              	}
			            	//we remove before we add due to the behavior 
			            	//of structures with parent pointers like e4x

							if(event.action == DragManager.MOVE){
								removeChildItem(parent, items[i], index);
						    }

	 			             //is the removed item before the drop location?
	 		                if (parent == _dropData2.parent && index < _dropData2.index){
	  		                	if(event.action == DragManager.MOVE){
	  		                		addChildItem(_dropData2.parent, (items[i] as XML).copy(), _dropData2.index - 1);
	  		                	}else{
	  		                		addChildItem(_dropData2.parent, (items[i] as XML).copy(), _dropData2.index);
	  		                	}
	  		                }else{
	   			            	if(event.action == DragManager.MOVE){
	   			            		addChildItem(_dropData2.parent, (items[i] as XML).copy(), _dropData2.index);
	   			            	}else{
	   			            		addChildItem(_dropData2.parent, (items[i] as XML).copy(), _dropData2.index);
	   			            	}
	   			            }
				        }
					}
				}
			}else if(allowOutSideDrag && event.dragSource.hasFormat("items")){
				dragDropHandlerOutsideLeaf(event);
			}
		}
		
		protected function dragDropHandlerOutsideBranch(event:DragEvent):void{
			if ((event.action == DragManager.MOVE || event.action == DragManager.COPY) && dragMoveEnabled){
				var items:Array = event.dragSource.dataForFormat("items") as Array;
				var position:Number = 0;
				items.reverse();
				for (var i:int = 0; i < items.length; i++){
					var itemXML:XML = null;
					if(items[i] is XML){
						itemXML = (items[i] as XML).copy();
					}else{
						itemXML = XMLUtils.objectToXML(items[i]);
					}
					if(lastBranchNode.children()){
						position = lastBranchNode.children().length();
					}
				 	if(event.action == DragManager.MOVE){
   	            		addChildItem(lastBranchNode, itemXML, position);
   	            	}else{
   	            		addChildItem(lastBranchNode, itemXML, position);
   	            	}
				}
			}
		}
		
		protected function dragDropHandlerOutsideLeaf(event:DragEvent):void{
			if ((event.action == DragManager.MOVE || event.action == DragManager.COPY) && dragMoveEnabled){
				var items:Array = event.dragSource.dataForFormat("items") as Array;
				//items.reverse();
				for (var i:int = 0; i < items.length; i++){
					var itemXML:XML = null;
					if(items[i] is XML){
						itemXML = (items[i] as XML).copy();
					}else{
						itemXML = XMLUtils.objectToXML(items[i]);
					}
				 	if(event.action == DragManager.MOVE){
   	            		addChildItem(_dropData2.parent, itemXML, _dropData2.index);
   	            	}else{
   	            		addChildItem(_dropData2.parent, itemXML, _dropData2.index);
   	            	}
				}
			}
		}
		
		
		
		
//////////////////////////////////////////////////////////////////////////////////////
//Rewrite the super's function: get the node info when drag over
//
//////////////////////////////////////////////////////////////////////////////////////		
		
	    
	    sjd_internal var _dropData2:Object;
	    
	    private var yOff:Number = 0;
	    
	    private var oldRowNum:Number = 0;
	    
	    //public var oldPointY:Number = 0;
	    
	    private function updateDropData(event:DragEvent):void
	    {
	    	
			var rowCount:int = rowInfo.length;
			var rowNum:int = 0;
			var yy:int = rowInfo[rowNum].height;
			
//////////////////////////////////////////////////////////////////////////////////////////
//			var pt:Point = globalToLocal(new Point(event.stageX, event.stageY));
			
			var pt:Point = null;

yy += rowInfo[rowNum].height / 2;
if(_dropData2){
	pt = globalToLocal(new Point(event.stageX, event.stageY + _dropData2.rowHeight / 2));
	
}else{
	pt = globalToLocal(new Point(event.stageX, event.stageY + this.rowHeight / 2));
}
			
//////////////////////////////////////////////////////////////////////////////////////////			
			//while (rowInfo[rowNum] && pt.y > yy)
			while (rowInfo[rowNum] && pt.y > yy && rowNum <= getOpenItemLength())
	   		{
	 			if (rowNum != rowInfo.length-1){
	 				rowNum++;
	 			}
	 			yy += rowInfo[rowNum].height;
	   		}
	   		//up half or bottom half
			var yOffset:Number = pt.y - rowInfo[rowNum].y - this.rowHeight / 2;
			yOff = yOffset;
			
			//var yOffset:Number = pt.y - rowInfo[rowNum].y
			var rowHeight:Number = rowInfo[rowNum].height;
			rowNum += verticalScrollPosition;
	
	        var parent:Object;
	        var index:int;
			var emptyFolder:Boolean = false;
			var numItems:int = collection ? collection.length : 0;

//the width of indicator line deponds on parent
rowNum--;
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
//if no change, return, optimize the drag effect/speed
if(oldRowNum == rowNum && yOffset > rowHeight * .5){
	return;
}else{
	oldRowNum = rowNum;
}
///////////////////////////////////////////////////////////////////////////////////////////////////////////////
			try
			{
		        var topItem:Object = (rowNum > 0) ? listItems[rowNum - _verticalScrollPosition - 1][0].data : null;
		        var bottomItem:Object = (rowNum < numItems) ? listItems[rowNum - _verticalScrollPosition][0].data  : null;
			}
			catch (e:Error)
			{
				//swallow since this should be recoverable
			}

	        var topParent:Object = collection ? getParentItem(topItem) : null;
	        var bottomParent:Object = collection ? getParentItem(bottomItem) : null;


	        // check their relationship
			/*if (false && yOffset > rowHeight * .5 && 
				isItemOpen(bottomItem) &&
	            dataDescriptor.isBranch(bottomItem, iterator.view) &&
				!dataDescriptor.hasChildren(bottomItem, iterator.view))*/
			if (false && yOffset > rowHeight * .5 && dataDescriptor.isBranch(bottomItem, iterator.view))
			{
				// we'll get here if we're dropping into an empty folder.
				// we have to be in the lower 50% of the row, otherwise
				// we're "between" rows.
				parent = bottomItem;
				//parent = null;
				index = 0;
				emptyFolder = true;
			}
	        else if (!topItem && !rowNum == rowCount)
	        {
	            parent = collection ? getParentItem(bottomItem) : null;
	            index =  bottomItem ? getChildIndexInParent(parent, bottomItem) : 0;
				rowNum = 0;
	        }
	        else if (bottomItem && bottomParent == topItem)
	        {
	            // we're dropping in the first item of a folder, that's an easy one
	            parent = topItem;
	            //parent = null;
	            index = 0;
	        }
//////////////////////// in the same nodes	        
			else if (topItem && bottomItem && topParent == bottomParent)
	        {
				parent = collection ? getParentItem(topItem) : null;
	            index = iterator ? getChildIndexInParent(parent, bottomItem) : 0;
	        }
	        else
	        {
	            //we're dropping at the end of a folder.  Pay attention to the position.
	            if (topItem && (yOffset < (rowHeight * .5)))
	            {
	                // ok, we're on the top half of the bottomItem.
	                parent = topParent;
					index = getChildIndexInParent(parent, topItem) + 1; // insert after
	            }
	            else if (!bottomItem)
	            {
	                parent = null;
	                index = collection ? collection.length: 0;
	            }
	            else
	            {
	                parent = bottomParent;
	                index = getChildIndexInParent(parent, bottomItem);
	            }
	        }
			_dropData2 = null;
	        _dropData2 = { parent: parent, index: index, localX: event.localX, localY: event.localY, 
							emptyFolder: emptyFolder, rowHeight: rowHeight, rowIndex: rowNum, yOffSet: yOffset};
			
	    }
	    
	    
	   
	    
	    
	    
	    
		
		
		
//////////////////////////////////////////////////////////////////////////////////////
//Process expand events: calculate the horizontal position
//
//////////////////////////////////////////////////////////////////////////////////////		
	
		
		override public function expandItem(item:Object, open:Boolean,
                              animate:Boolean = false,
                              dispatchEvent:Boolean = false,    
                              cause:Event = null):void{
       		
       		toMaxHorizontalPosition = 0;
       		
       		//effect > 20 row: now animate effect, Tree line:1603
       		try{
       			super.expandItem(item, open, animate, dispatchEvent, cause);
       		}catch (e:Error){
       			
       		}
       		
		}
   
		private var toMaxHorizontalPosition:Number = 0;
		//let the tree item renderers call back to set the width, keep the max position
		sjd_internal function setMaxWidth(value:Number):void{
			if(_allowChangeHorizontalScroll && value > this.width){
				
				if(value - this.width > toMaxHorizontalPosition){
					toMaxHorizontalPosition = value - this.width;
				}
			}
		}
		
		private function calculateMaxHorizontalScrollHandle(e:Event = null):void{
			if(_allowChangeHorizontalScroll){
				if(e is ScrollEvent){
					if((e as ScrollEvent).direction == ScrollEventDirection.HORIZONTAL){
						return;
					}
				}
				callLater(calculateMaxHorizontalScroll);
			}
		}
		
		private function calculateMaxHorizontalScroll():void{
			
			invalidateDisplayList();
			
			toMaxHorizontalPosition = 0;
			//just show current position's visible list
			var openList:Object = this.visibleData;
			for each(var obj:AdvanceTreeItemRenderer in openList){
				obj.calculateMaxPosition();
			}
			
			if(this.verticalScrollBar && this.verticalScrollBar.visible){
				this.maxHorizontalScrollPosition = toMaxHorizontalPosition + this.verticalScrollBar.width - getIndent();
			}else{
				this.maxHorizontalScrollPosition = toMaxHorizontalPosition - getIndent();
			}
			
			
			toMaxHorizontalPosition = 0;
			
			this.invalidateDisplayList();
		}

		
	    
//////////////////////////////////////////////////////////////////////////////////////
//Process mouse events: double click and mouse wheel
//
//////////////////////////////////////////////////////////////////////////////////////	
	
		          
		private var isExpanding:Boolean = false;
		private function setIsExpanding(e:TreeEvent):void{
			isExpanding = (e.type == TreeEvent.ITEM_OPENING);
		}
		                   
		override protected function mouseWheelHandler(event:MouseEvent):void{
			//prevent scroll when open/close the branch node
			if(!isExpanding){
		        super.mouseWheelHandler(event);
		  	}else{
		  		event.stopImmediatePropagation();
		  	}
	    }
	
		override protected function mouseDoubleClickHandler(event:MouseEvent):void{
			super.mouseDoubleClickHandler(event);
			expandItem(this.selectedItem, !this.isItemOpen(this.selectedItem), true, true);
	    }
	    
	    
	
//////////////////////////////////////////////////////////////////////////////////////
//Utilities: Calculate the node position
//
//////////////////////////////////////////////////////////////////////////////////////		
	
		
		override mx_internal function getItemDepth(item:Object, offset:int):int{
		//first test for a match (most cases)
			if (!collection)
				return 0;
			
			if (!iterator)
				iterator = collection.createCursor();
			
			if (iterator.current == item)
				return getCurrentCursorDepth();
			
			//otherwise seek to offset and get the depth
			var bookmark:CursorBookmark = iterator.bookmark;
			iterator.seek(bookmark, offset);
			var depth:int = getCurrentCursorDepth();
			//put the cursor back
			iterator.seek(bookmark, 0);
			return depth;
		}
	    
	    protected function getCurrentCursorDepth():int{
	        return HierarchicalViewCursor(iterator).currentDepth;
	    }
	    
	    protected function getParentDepth(item:XML):Number{
	    	var i:Number = 0;
	    	if(item){
		    	while(item.parent() != null){
		    		item = item.parent();
		    		i++;
		    	}
	    	}
	    	return i;
	    }

		protected function getOpenItemByIndex(index:Number):Object{
			var openList:Object = this.visibleData;
			var target:Object = null;
			for each(var obj:TreeItemRenderer in openList){
				if(obj.listData.rowIndex == index){
					target = obj.data;
					break;
				}
			}
			return target;
		}
		
		protected function getOpenItemLength():Number{
			var openList:Object = this.visibleData;
			var count:Number = 0;
			for each(var obj:TreeItemRenderer in openList){
				count++;
			}
			return count;
		}
		
		protected function getIndent():Number{
	        var depth:Number = 0;
	        for (var p:String in openItems){
	            // add one since its children are actually indented
	            depth = Math.max(getItemDepth(openItems[p], 0), depth);
	        }
	        return depth * getStyle("indentation");
	    }
	    
	    protected function getItemIndex(item:Object):int{
	        var cursor:IViewCursor = collection.createCursor();
	        var i:int = 0;
	        do
	        {
	            if (cursor.current === item)
	                break;
	            i++;
	        }
	        while (cursor.moveNext());
	        return i;
	    }
	    
	    protected function getChildren(item:Object, view:Object):ICollectionView{
	    	//get the collection of children
	    	var children:ICollectionView = _dataDescriptor.getChildren(item, view);
			return children;
	    }
	    
	    protected function getParentStack(item:Object):Array
	    {
	    	var stack:Array = [];
	    	if (item == null)
	    		return stack;
	  		
	  		var parent:* = getParentItem(item);
	  		while (parent)
	  		{
	  			stack.push(parent);
	  			parent = getParentItem(parent);
	  		}
	  		return stack;	  	
	    }
	    
	    protected function getChildIndexInParent(parent:Object, child:Object):int
		{
			var index:int = 0;
			if (!parent)
			{
				var cursor:IViewCursor = ICollectionView(iterator.view).createCursor();
				while (!cursor.afterLast)
				{
					if (child === cursor.current)
						break;
					index++;
					cursor.moveNext();
				}
			}
			else
			{
				if (parent != null && 
					dataDescriptor.isBranch(parent, iterator.view) &&
					dataDescriptor.hasChildren(parent, iterator.view))
				{
					var children:ICollectionView = getChildren(parent, iterator.view);
					if (children.contains(child))
					{
						for (; index < children.length; index++)
						{
							if (child === children[index])
								break;
						}
					}
					else 
					{
						//throw new Error("Parent item does not contain specified child: " + itemToUID(child));
					}
				}
			}
			return index;
		}
		
		/*
		override protected function adjustVerticalScrollPositionDownward(rowCount:int):Boolean
    {
    	return false;
    }*/
	
	}
	
	
}