package com.titus.catalog.controller
{
	import com.titus.catalog.model.search.SearchResultPage;
	import com.titus.catalog.model.search.SearchResultSection;
	
	import mx.collections.ICollectionView;
	import mx.controls.treeClasses.ITreeDataDescriptor;

	public class SearchTreeDataDescriptor implements ITreeDataDescriptor
	{
		public function SearchTreeDataDescriptor()
		{
		}

		public function getChildren(node:Object, model:Object=null):ICollectionView
		{
			if (node is SearchResultSection) {
				return (SearchResultSection(node).pages);
			} else if (node is SearchResultPage) {
				return (SearchResultPage(node).modelItems);
			} else {
				return null;
			} 
		}
		
		public function hasChildren(node:Object, model:Object=null):Boolean
		{
			if (node is SearchResultSection && SearchResultSection(node).pages != null) {
				return (SearchResultSection(node).pages.length > 0);
			} else if (node is SearchResultPage && SearchResultPage(node).modelItems != null) {
				return (SearchResultPage(node).modelItems.length > 0);
			} else {
				return false;
			} 
		}
		
		public function isBranch(node:Object, model:Object=null):Boolean
		{
			return (node is SearchResultSection || node is SearchResultPage);
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