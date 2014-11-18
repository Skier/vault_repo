package com.affilia.controls
{
	import mx.controls.Tree;
	import mx.events.ListEvent;
	
	public class TreeComboBoxRenderer extends Tree
	{
		[Bindable]
		public var outerDocument:TreeComboBox;
		
		public function TreeComboBoxRenderer()
		{
			super();
			this.addEventListener(ListEvent.CHANGE, onSelectionChanged);
			iconFunction = function(obj:*):Class {return null;}
			setStyle("borderColor", 0xCCCCCC);
			setStyle("borderStyle", "solid");
		}
	
		private function onSelectionChanged(event:ListEvent):void
		{
			outerDocument.selectTreeItem(event.currentTarget.selectedItem);
		}
	
		public function expandParents(node:Object):void
		{
			if (node && !isItemOpen(node))
			{
				expandItem(node, true);
	            expandParents(node.parent());
	        }		
		}
		
		public function selectNode(node:Object):void
		{
			selectedItem = node;
			var idx:int = getItemIndex(selectedItem);
			if (idx != -1)
		    	scrollToIndex(idx);
		}
	}
}