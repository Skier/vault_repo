package com.affilia.controls
{
	import mx.collections.ICollectionView;
	import mx.controls.ComboBox;
	import mx.controls.treeClasses.ITreeDataDescriptor;
	import mx.core.ClassFactory;
	import mx.events.DropdownEvent;
	
	public class TreeComboBox extends ComboBox
	{
		private var _ddFactory:ClassFactory;
		
		private function get ddFactory():ClassFactory
		{
			if (_ddFactory == null)
			{
				_ddFactory = new ClassFactory();
				_ddFactory.generator = TreeComboBoxRenderer;
				_ddFactory.properties = {
						width:this.width, 
						height:this.treeHeight,
						outerDocument:this
				};
			}
			return _ddFactory;		
		}	
		
		public function set treeDataDescriptor(value:ITreeDataDescriptor):void 
		{
			var tree:TreeComboBoxRenderer = ddFactory.generator as TreeComboBoxRenderer;
			tree.dataDescriptor = value;
		}
	
		private var _treeHeight:Number;
		
		public function get treeHeight():Number
		{
			return _treeHeight;
		}
		
		public function set treeHeight(value:Number):void
		{
			_treeHeight = value;
			ddFactory.properties["height"] = value;
		}
	
		private var _treeSelectedItem:Object;
		public function get treeSelectedItem():Object
		{
			return _treeSelectedItem;
		}
		public function set treeSelectedItem(value:Object):void 
		{
			_treeSelectedItem = value;
			
			if (_treeSelectedItem)
				text = _treeSelectedItem[labelField];
			else 
				text = "";
		}
		
		private function selectIndex():void 
		{
			if (!treeSelectedItem)
				selectedIndex = -1;
			else 
				selectTreeItem(treeSelectedItem);
		}
	
		public function TreeComboBox()
		{
			super();
			
			this.dropdownFactory = ddFactory;
			this.addEventListener(DropdownEvent.OPEN, onComboOpen);
		}
		
		private function onComboOpen(event:DropdownEvent):void
		{
			var tree:TreeComboBoxRenderer = dropdown as TreeComboBoxRenderer;
			for each (var item:Object in ICollectionView(dataProvider))
			{
				tree.expandChildrenOf(item, true);
				tree.invalidateList();
			}

			if (treeSelectedItem)
				tree.selectNode(treeSelectedItem);
/* 
			if (treeSelectedItem)
			{
				tree.expandParents(treeSelectedItem);
				tree.selectNode(treeSelectedItem);
			}
			else
			{
				tree.expandItem(dataProvider.getItemAt(0), true);
			}
 */
		}
		
		override protected function updateDisplayList(unscaledWidth:Number, 
		                                              unscaledHeight:Number):void 
		{ 
			super.updateDisplayList(unscaledWidth, unscaledHeight);   
	
			if(dropdown && treeSelectedItem && treeSelectedItem[labelField] != null)
	        	text = treeSelectedItem[labelField]; 
		} 
	
		public function selectTreeItem(item:Object):void
		{
			treeSelectedItem = item;
		}
	
	}
}