package com.llsvc.admin
{
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class AdminModel
	{
		public var clients:ArrayCollection;
		public var users:ArrayCollection;
		public var freeUsers:ArrayCollection;
		
		public function AdminModel()
		{
			clients = new ArrayCollection();
			users = new ArrayCollection();
			freeUsers = new ArrayCollection();
		}

	}
}