package UI.Management.TestDrive
{
	import mx.controls.treeClasses.ITreeDataDescriptor;
	import mx.utils.ObjectUtil;
	import mx.collections.ArrayCollection;
	import mx.collections.ICollectionView;
	import mx.utils.ObjectProxy;

	public class ObjectBrowserTreeDataDescriptor implements ITreeDataDescriptor
	{
		public function getData(node:Object, model:Object=null):Object
		{
			return node;
		}
		
		public function hasChildren(node:Object, model:Object=null):Boolean
		{
			return ObjectBrowser(node).Items.length > 0;
		}
		
		public function addChildAt(parent:Object, newChild:Object, index:int, model:Object=null):Boolean
		{
			return false;
		}
		
		public function isBranch(node:Object, model:Object=null):Boolean
		{
			return  ObjectBrowser(node).Items.length > 0;
		}
		
		public function removeChildAt(parent:Object, child:Object, index:int, model:Object=null):Boolean
		{
			return false;
		}
		
		public function getChildren(node:Object, model:Object=null):ICollectionView
		{
			return new ArrayCollection(ObjectBrowser(node).Items);
		}
		
	}
}