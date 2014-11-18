package com.llsvc.expense.view.invoice
{
	import com.llsvc.domain.Client;
	import com.llsvc.domain.Company;
	import com.llsvc.domain.Invoice;
	
	[Bindable]
	public class InvoiceFilter
	{
		public var company:Company;
		public var client:Client;
		public var fromDate:Date;
		public var toDate:Date;
		public var status:String;
		
		public function InvoiceFilter()
		{
		}
		
		public function allow(item:Invoice):Boolean 
		{
			if (item == null)
				return false;
			
			var result:Boolean = true;
			
			if (company != null && company.companyid != item.client.company.companyid)
				result = false
			else if (client != null && client.clientid != item.client.clientid)
				result = false
			else if (fromDate != null && fromDate > item.invoicedate)
				result = false
			else if (toDate != null && toDate < item.invoicedate)
				result = false
			else if (status != null && status != item.status)
				result = false;
			
			return result;
		}

	}
}