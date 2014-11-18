package com.llsvc.client.lms
{
	import com.llsvc.domain.Project;
	import com.llsvc.domain.User;
	
	import mx.collections.ArrayCollection;
	
	public class LocalCash
	{
		private static var instance:LocalCash;
		
		public static function getInstance():LocalCash 
		{
			if (instance == null)
				instance = new LocalCash();
			
			return instance;
		}
		
		public function LocalCash()
		{
			if (instance != null)
				throw new Error("Singleton!");
		}
		
		private var user:User;
		public function storeCurrentUser(value:User):void 
		{
			this.user = value;
		}
		public function getCurrentUser():User 
		{
			return this.user;
		}
		
		private var stateList:ArrayCollection = new ArrayCollection();
		public function storeStates(value:ArrayCollection):void 
		{
			stateList.removeAll();
			for each (var obj:Object in value) 
				stateList.addItem(obj);
		}
		public function getStates():ArrayCollection 
		{
			return stateList;
		}

		private var projectList:ArrayCollection = new ArrayCollection();
		public function storeProjects(value:ArrayCollection):void 
		{
			projectList.removeAll();
			for each (var obj:Object in value) 
				projectList.addItem(obj);
		}
		public function getProjects():ArrayCollection 
		{
			return projectList;
		}
		
		public function getProjectById(id:int):Project 
		{
			for each (var p:Project in projectList) 
			{
				if (p.id == id)
					return p;
			}
			return null;
		}

	}
}