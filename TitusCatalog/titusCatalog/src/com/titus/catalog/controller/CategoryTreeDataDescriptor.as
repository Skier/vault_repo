package com.titus.catalog.controller
{
	import com.titus.catalog.model.CatalogItem;
	
	import mx.collections.ICollectionView;
	import mx.controls.treeClasses.ITreeDataDescriptor;

	public class CategoryTreeDataDescriptor implements ITreeDataDescriptor
	{
		public function CategoryTreeDataDescriptor()
		{
		}

		public function getChildren(node:Object, model:Object=null):ICollectionView
		{
			return CatalogItem(node).children;
		}
		
		public function hasChildren(node:Object, model:Object=null):Boolean
		{
			if (CatalogItem(node).children != null) {
				return CatalogItem(node).children.length > 0;
			} else {
				return false;
			} 
		}
		
		public function isBranch(node:Object, model:Object=null):Boolean
		{
			return (node as CatalogItem).IsBranch;
		}
		
		public function getData(node:Object, model:Object=null):Object
		{
			return node;
		}
		
		public function addChildAt(parent:Object, newChild:Object, index:int, model:Object=null):Boolean
		{
			return false;
		}
		
		public function removeChildAt(parent:Object, child:Object, index:int, model:Object=null):Boolean
		{
			return false;
		}
		
	}
}