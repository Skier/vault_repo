package com.llsvc.domain
{
	import com.llsvc.domain.vo.clientVO;
	
	import mx.collections.ArrayCollection;

	[Bindable]
	public class Client extends clientVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public var company:Company;
		public var user:User;
		public var person:Person;
		public var projects:ArrayCollection;
		
		public function Client()
		{
			super();
			
			projects = new ArrayCollection();
		}
		
		public function get personStr():String 
		{
			if (person != null) 
			{
				var result:String;
				result = person.firstname;
				if (person.middlename != null && person.middlename.length > 0) 
				{
					result += " ";
					result += person.middlename;
				}
				result += " ";
				result += person.lastname;
				return result;
			} else 
			{
				return "n/a";
			}
		}
		
		public function get phoneStr():String 
		{
			if (person != null) 
			{
				var result:String;
				result = person.phone;
				if (person.phonealt != null && person.phonealt.length > 0) 
				{
					result += " ";
					result += person.phonealt;
				}
				return result;
			} else 
			{
				return "n/a";
			}
		}
		
		public function get addressStr():String 
		{
			if (person != null) 
			{
				return person.addressStr;
			} else 
			{
				return "n/a";
			}
		}
		
		public function updateFields(value:clientVO):void 
		{
			if (value == null)
				value = new clientVO(); 
			
			this.clientid = value.clientid;
			this.userid = value.userid;
			this.name = value.name;
			this.personid = value.personid;
			this.description = value.description;
			this.companyid = value.companyid;
		}
		
		public function toVO():clientVO 
		{
			var result:clientVO = new clientVO();
			
			result.clientid = this.clientid;
			result.userid = this.userid;
			result.name = this.name;
			result.personid = this.personid;
			result.description = this.description;
			result.companyid = this.companyid;
			
			return result;
		}
		
	}
}