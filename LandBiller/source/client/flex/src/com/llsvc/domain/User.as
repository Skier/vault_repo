package com.llsvc.domain
{
	import com.llsvc.domain.vo.userVO;
	
	import mx.collections.ArrayCollection;

	[Bindable]
	public class User extends userVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public var login:Login;
		
		public var companies:ArrayCollection;
		public var clients:ArrayCollection;
		public var projects:ArrayCollection;
		
		public var invoices:ArrayCollection;
		public var notes:ArrayCollection;
		public var expenceTypes:ArrayCollection;
		
		public function User()
		{
			super();
			companies = new ArrayCollection();
			clients = new ArrayCollection();
			projects = new ArrayCollection();
			
			invoices = new ArrayCollection();
			notes = new ArrayCollection();
			expenceTypes = new ArrayCollection();
		}
		
		public function updateFields(value:userVO):void 
		{
			this.userid = value.userid;
			this.loginid = value.loginid;
			this.logourl = value.logourl;
		}
		
		public function toVO():userVO 
		{
			var result:userVO = new userVO();
			
			result.userid = this.userid;
			result.loginid = this.loginid;
			result.logourl = this.logourl;

			return result;
		}
		
	}
}