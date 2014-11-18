package UI.Management.CodeGen
{
	import mx.controls.treeClasses.ITreeDataDescriptor;
	import mx.utils.ArrayUtil;
	import mx.collections.ArrayCollection;
	import mx.collections.ICollectionView;

	public class CodeTreeDataDescriptor implements ITreeDataDescriptor
	{
		public function getData(node:Object, model:Object=null):Object
		{
			return node;
		}
		
		public function hasChildren(node:Object, model:Object=null):Boolean
		{
			return CodeItem(node).IsDirectory() && CodeDirectory(node).Items.length > 0;
		}
		
		public function addChildAt(parent:Object, newChild:Object, index:int, model:Object=null):Boolean
		{
			return false;
		}
		
		public function isBranch(node:Object, model:Object=null):Boolean
		{
			return CodeItem(node).IsDirectory();
		}
		
		public function removeChildAt(parent:Object, child:Object, index:int, model:Object=null):Boolean
		{
			return false;
		}
		
		public function getChildren(node:Object, model:Object=null):ICollectionView
		{
			return new ArrayCollection(CodeDirectory(node).Items);
		}
		
	}
}