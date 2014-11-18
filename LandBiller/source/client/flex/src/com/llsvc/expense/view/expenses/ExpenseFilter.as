package com.llsvc.expense.view.expenses
{
	import com.llsvc.domain.Client;
	import com.llsvc.domain.Company;
	import com.llsvc.domain.InvoiceItem;
	import com.llsvc.domain.Project;
	
	[Bindable]
	public class ExpenseFilter
	{
		public var company:Company;
		public var client:Client;
		public var project:Project;
		public var date:Date;
		
		public function ExpenseFilter()
		{
		}
		
		public function allow(item:InvoiceItem):Boolean 
		{
			var result:Boolean = true;
			
			if (company != null && item.project.client.company.companyid != company.companyid)
			{
				result = false;
			} else if (client != null && item.project.client.clientid != client.clientid) 
			{
				result = false;
			} else if (project != null && item.project.projectid != project.projectid) 
			{
				result = false;
			} else if (date != null &&
				(item.itemdate.fullYear != date.fullYear
				|| item.itemdate.month != date.month
				|| item.itemdate.date != date.date)) 
			{
				result = false;
			}
			
			return result;
		}

	}
}