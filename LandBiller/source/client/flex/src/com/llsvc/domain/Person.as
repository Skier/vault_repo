package com.llsvc.domain
{
	import com.llsvc.domain.vo.personVO;
	
	import mx.binding.utils.ChangeWatcher;

	[Bindable]
	public class Person extends personVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public function Person()
		{
			super();
		}
		
		private var _address:Address;
		public function get address():Address { return _address; }
		public function set address(value:Address):void 
		{
			_address = value;
			if (value != null) 
			{
				updateAddressStr();
				ChangeWatcher.watch(_address, "address1", addressChangeHandler);
				ChangeWatcher.watch(_address, "address2", addressChangeHandler);
				ChangeWatcher.watch(_address, "city", addressChangeHandler);
				ChangeWatcher.watch(_address, "state", addressChangeHandler);
				ChangeWatcher.watch(_address, "zip", addressChangeHandler);
			}
		}
		
		private function addressChangeHandler(e:*):void 
		{
			updateAddressStr();
		}
		
		public var addressStr:String;
		private function updateAddressStr():void 
		{
			if (address != null) 
			{
				var result:String;
				result = address.address1;
				if (address.address2 != null && address.address2.length > 0) 
				{
					result += " ";
					result += address.address2;
				}
				result += " ";
				result += address.city;
				if (address.state != null) 
				{
					result += " ";
					result += address.state.stateabbr;
				}
				result += " ";
				result += address.zip;
				
				addressStr = result;
			} else 
			{
				addressStr = "";
			}
		}
		
		public function get fullName():String 
		{
			var result:String = "";

			if (firstname != null && firstname.length > 0)
				result += firstname;
			if (middlename != null && middlename.length > 0)
				result += (" " + middlename);
			if (lastname != null && lastname.length > 0)
				result += (" " + lastname);

			return result;
		}
		
		public function updateFields(value:personVO):void 
		{
			if (value == null)
				value = new personVO(); 
			
			this.personid = value.personid;
			this.firstname = value.firstname;
			this.middlename = value.middlename;
			this.lastname = value.lastname;
			this.phone = value.phone;
			this.phonealt = value.phonealt;
			this.addressid = value.addressid;
		}
		
		public function toVO():personVO
		{
			var result:personVO = new personVO();
			
			result.personid = this.personid;
			result.firstname = this.firstname;
			result.middlename = this.middlename;
			result.lastname = this.lastname;
			result.phone = this.phone;
			result.phonealt = this.phonealt;
			result.addressid = this.addressid;
			
			return result;
		}
		
	}
}