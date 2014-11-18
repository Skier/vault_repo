package com.dalworth.leadCentral
{
	[Bindable]
    [RemoteClass(alias="Dalworth.LeadCentral.Domain.CustomerFilter")]
	public class CustomerFilter
	{
		public var Name:String;
		public var PhoneNumber:String;
		
		public function CustomerFilter()
		{
		}

	}
}