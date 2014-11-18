package com.llsvc.domain
{
	import com.llsvc.domain.vo.loginVO;

	[Bindable]
	public class Login extends loginVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public var person:Person;
		
		public function Login()
		{
			super();
		}
		
		public function updateFields(value:loginVO):void 
		{
			if (value == null)
				value = new loginVO(); 
			
			this.loginid = value.loginid;
			this.username = value.username;
			this.password = value.password;
			this.email = value.email;
			this.personid = value.personid;
		}
		
		public function toVO():loginVO 
		{
			var result:loginVO = new loginVO();
			
			result.loginid = this.loginid;
			result.username = this.username;
			result.password = this.password;
			result.email = this.email;
			result.personid = this.personid;
			
			return result;
		}
		
	}
}