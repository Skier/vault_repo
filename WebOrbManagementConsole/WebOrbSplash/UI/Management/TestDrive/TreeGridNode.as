package UI.Management.TestDrive
{
	import mx.collections.ArrayCollection;
	
	public class TreeGridNode
	{
		public var Parent:TreeGridNode;
		
		public var Items:ArrayCollection = new ArrayCollection();
		
		public var Value:Object;
		
		public var Data:Object;
		
		public var IsNew:Boolean;

		public function TreeGridNode(value:Object, parent:TreeGridNode)
		{
			Value = value;
			Parent = parent;
			
			if(Parent != null)
				Parent.Items.addItem(this);	
		}
		
		public function appendChild(node:TreeGridNode):void
		{
			if(!Items.contains(node))
				Items.addItem(node);
		}
		
		public function removeChild(node:TreeGridNode):void
		{
			if(Items.contains(node))
				Items.removeItemAt(Items.getItemIndex(node));
		}
		public function getDepth():int
		{
			var depth:int = 0;
			var parent:TreeGridNode = Parent;
			
			while(parent != null)
			{
				++depth;
				parent = parent.Parent;
			}
			
			return depth;
		}
	}
}