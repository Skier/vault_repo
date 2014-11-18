package UI.Management.TestDrive
{
	import mx.controls.treeClasses.ITreeDataDescriptor;

	public class ArgsTreeDataDescriptor implements ITreeDataDescriptor
	{
		public function getData(node:Object, model:Object=null):Object
		{
			return node;
		}
		
		public function hasChildren(node:Object, model:Object=null):Boolean
		{
			return Service;
		}
		
		public function addChildAt(parent:Object, newChild:Object, index:int, model:Object=null):Boolean
		{
			return false;
		}
		
		public function isBranch(node:Object, model:Object=null):Boolean
		{
			return false;
		}
		
		public function removeChildAt(parent:Object, child:Object, index:int, model:Object=null):Boolean
		{
			return false;
		}
		
		public function getChildren(node:Object, model:Object=null):ICollectionView
		{
			return null;
		}
		
	}
}