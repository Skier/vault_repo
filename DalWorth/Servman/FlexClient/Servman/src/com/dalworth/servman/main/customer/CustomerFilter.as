package com.dalworth.servman.main.customer
{
	[Bindable]
    [RemoteClass(alias="Servman.Domain.CustomerFilter")]
	public class CustomerFilter
	{
		public var Name:String;
		public var PhoneNumber:String;
		
		public function CustomerFilter()
		{
		}

	}
}