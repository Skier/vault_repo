package com.ebs.eroof.model.company
{
	import com.ebs.eroof.model.wrapper.Client;
	import com.ebs.eroof.model.wrapper.Company;
	import com.ebs.eroof.model.wrapper.Facility;
	import com.ebs.eroof.model.wrapper.Segment;
	
	import mx.collections.ICollectionView;
	import mx.controls.treeClasses.ITreeDataDescriptor;

	public class CompanyTreeDataDescriptor implements ITreeDataDescriptor
	{
		public function CompanyTreeDataDescriptor()
		{
		}

		public function getChildren(node:Object, model:Object=null):ICollectionView
		{
			if (node is Company)
				return Company(node).segmentCollection;
			else if (node is Segment)
				return Segment(node).clientCollection;
			else if (node is Client)
				return Client(node).facilityCollection;
			else if (node is Facility)
				return Facility(node).sectionsCollection;
			else 
				return null;
		}
		
		public function hasChildren(node:Object, model:Object=null):Boolean
		{
			if (node is Company)
				return Company(node).segmentCollection.length > 0;
			else if (node is Segment)
				return Segment(node).clientCollection.length > 0;
			else if (node is Client)
				return Client(node).facilityCollection.length > 0;
			else if (node is Facility)
				return Facility(node).sectionsCollection.length > 0;
			else 
				return false;
		}
		
		public function isBranch(node:Object, model:Object=null):Boolean
		{
			if (node is Company)
				return true;
			else if (node is Segment)
				return true;
			else if (node is Client)
				return true;
			else if (node is Facility)
				return true;
			else 
				return false;
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