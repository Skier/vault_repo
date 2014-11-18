package com.dalworth.leadCentral.setting.leadSource
{
	
	import Intuit.Sb.Cdm.vo.CustomerType;
	
	import mx.collections.ICollectionView;
	import mx.controls.treeClasses.ITreeDataDescriptor;

	public class CustomerTypeTreeDataDescriptor implements ITreeDataDescriptor
	{
		public function CustomerTypeTreeDataDescriptor()
		{
		}

		public function getChildren(node:Object, model:Object=null):ICollectionView
		{
			if (CustomerType(node).relatedTypes == null || CustomerType(node).relatedTypes.length == 0)
				return null;
			else 
				return CustomerType(node).relatedTypes;
		}
		
		public function hasChildren(node:Object, model:Object=null):Boolean
		{
			if (CustomerType(node).relatedTypes == null || CustomerType(node).relatedTypes.length == 0)
				return false;
			else 
				return true;
		}
		
		public function isBranch(node:Object, model:Object=null):Boolean
		{
			if (CustomerType(node).relatedTypes == null || CustomerType(node).relatedTypes.length == 0)
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