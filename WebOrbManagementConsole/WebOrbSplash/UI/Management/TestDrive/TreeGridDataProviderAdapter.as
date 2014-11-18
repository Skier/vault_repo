package UI.Management.TestDrive
{
	import mx.collections.ArrayCollection;
	import mx.controls.treeClasses.ITreeDataDescriptor;
	import mx.collections.ICollectionView;

	[Bindable]
	public class TreeGridDataProviderAdapter extends ArrayCollection
	{
		public var m_dataDescriptor:ITreeDataDescriptor;
		
		private var m_dataProvider:Object;
		
		private var m_loadedBranches:ArrayCollection = new ArrayCollection();
		
		private var m_root:TreeGridNode;
		
		public function TreeGridDataProviderAdapter(dataProvider:Object, dataDescriptor:ITreeDataDescriptor)
		{
			m_dataDescriptor = dataDescriptor;
			m_dataProvider = dataProvider;
			m_root = new TreeGridNode("Root",null);
			
			if(dataProvider is Array)
				for each(var o:Object in dataProvider)	
					addItem(new TreeGridNode( o, m_root) );
			else
				addItem(new TreeGridNode( dataProvider, m_root ));
		}
		
		public function get Root():TreeGridNode
		{
			return m_root;
		}
		
		public function loadBranch(node:TreeGridNode):void
		{
			// loading childs
			if(node.Items.length == 0)
			{
				var items:ICollectionView = m_dataDescriptor.getChildren(node.Value,this);
							
				for each(var item:Object in items)
					new TreeGridNode( item , node);		
			}
			
			var startIndex:int = getItemIndex(node);
						
			for each(var childNode:TreeGridNode in node.Items)
			{
				++startIndex;
				
				if(!contains(childNode))
					addItemAt(childNode,startIndex);
			}
			
			if(!m_loadedBranches.contains(node))
				m_loadedBranches.addItem(node);
		}
		
		public function unloadBranch(node:TreeGridNode):void
		{
			for each(var item:TreeGridNode in node.Items)
			{
				removeItemAt(getItemIndex(item));
				
				if(isBranchLoaded(item))
					unloadBranch(item);
			}
			
			m_loadedBranches.removeItemAt(m_loadedBranches.getItemIndex(node));
		}
		
		public function isBranchLoaded(node:TreeGridNode):Boolean
		{
			return m_loadedBranches.contains(node);
		}
		
		public function deleteItem(node:TreeGridNode):void
		{
			if(contains(node))
			{
				if(isBranchLoaded(node))
					unloadBranch(node);
					
				removeItemAt(getItemIndex(node));
				
				node.Parent.removeChild(node);
			}
		}
		
		
	}
}