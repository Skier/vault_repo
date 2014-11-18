package com.dalworth.leadCentral.leadSource
{
	import com.dalworth.leadCentral.domain.LeadSource;
	
	import mx.collections.ICollectionView;
	import mx.controls.treeClasses.ITreeDataDescriptor;

	public class LeadSourceTreeDataDescriptor implements ITreeDataDescriptor
	{
		public function LeadSourceTreeDataDescriptor()
		{
		}

		public function getChildren(node:Object, model:Object=null):ICollectionView
		{
			if (LeadSource(node).relatedLeadSources == null || LeadSource(node).relatedLeadSources.length == 0)
				return null;
			else 
				return LeadSource(node).relatedLeadSources;
		}
		
		public function hasChildren(node:Object, model:Object=null):Boolean
		{
			if (LeadSource(node).relatedLeadSources == null || LeadSource(node).relatedLeadSources.length == 0)
				return false;
			else 
				return true;
		}
		
		public function isBranch(node:Object, model:Object=null):Boolean
		{
			if (LeadSource(node).relatedLeadSources == null || LeadSource(node).relatedLeadSources.length == 0)
				return false;
			else 
				return true;
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