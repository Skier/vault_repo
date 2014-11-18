package com.llsvc.expense.view.project
{
	import com.llsvc.domain.Client;
	import com.llsvc.domain.Company;
	import com.llsvc.domain.User;
	
	import mx.collections.ICollectionView;
	import mx.controls.treeClasses.ITreeDataDescriptor;

	public class ProjectsDataTreeDescriptor implements ITreeDataDescriptor
	{
		public function ProjectsDataTreeDescriptor()
		{
		}

		public function getChildren(node:Object, model:Object=null):ICollectionView
		{
			if (node is User) 
			{
				return User(node).companies;
			} else if (node is Company) 
			{
				return Company(node).clients;
			} else if (node is Client) 
			{
				return Client(node).projects;
			}

			return null;
		}
		
		public function hasChildren(node:Object, model:Object=null):Boolean
		{
			if (node is User) 
			{
				return User(node).companies.length > 0;
			} else if (node is Company) 
			{
				return Company(node).clients.length > 0;
			} else if (node is Client) 
			{
				return Client(node).projects.length > 0;
			}

			return false;
		}
		
		public function isBranch(node:Object, model:Object=null):Boolean
		{
			if (node is User || node is Company || node is Client) 
			{
				return true;
			} else  
			{
				return false;
			}
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