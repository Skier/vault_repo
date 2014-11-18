package com.llsvc.domain
{
	import com.llsvc.domain.vo.invoiceitemVO;
	
	import mx.binding.utils.ChangeWatcher;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class InvoiceItem extends invoiceitemVO
	{
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public var amount:Number;
		public var adjustment:Number;
		
		public var invoice:Invoice;
		public var project:Project;
		public var expenceType:ExpenceType;
		
		public var attachments:ArrayCollection;
		
		public function InvoiceItem()
		{
			super();
			
			attachments = new ArrayCollection();
			
			ChangeWatcher.watch(this, "rate", propChangeHandler);
			ChangeWatcher.watch(this, "quantity", propChangeHandler);
			ChangeWatcher.watch(this, "total", propChangeHandler);
		}
		
		public function get projectName():String 
		{
			if (project != null) 
				return project.projectname;
			else 
				return "n/a";
		}
		
		public function get itemDateStr():String 
		{
			if (itemdate == null)
				return "n/a";
			
			var mm:String = (itemdate.month + 1).toString();
				mm = mm.length > 1 ? mm : "0" + mm;
			var dd:String = itemdate.date.toString();
				dd = dd.length > 1 ? dd : "0" + dd;
			var yyyy:String = itemdate.fullYear.toString();
			
			return (yyyy + "/" + mm + "/" + dd);
		}
		
		public function get clientName():String 
		{
			if (project != null && project.client != null) 
				return project.client.name;
			else 
				return "n/a";
		}
		
		public function get companyName():String 
		{
			if (project != null && project.client != null && project.client.company != null) 
				return project.client.company.name;
			else 
				return "n/a";
		}
		
		public function get itemTypeName():String 
		{
			if (expenceType != null) 
				return expenceType.itemname;
			else 
				return "n/a";
		}
		
		public function updateFields(item:invoiceitemVO):void 
		{
			if (item == null)
				item = new invoiceitemVO(); 
			
			this.invoiceitemid = item.invoiceitemid;
			this.invoiceid = item.invoiceid;
			this.projectid = item.projectid;
			this.expencetypeid = item.expencetypeid;
			this.itemdate = item.itemdate;
			this.rate = item.rate;
			this.quantity = item.quantity;
			this.total = item.total;
			this.status = item.status;
		}
		
		public function toVO():invoiceitemVO 
		{
			var result:invoiceitemVO = new invoiceitemVO();
			
			result.invoiceitemid = this.invoiceitemid;
			result.invoiceid = this.invoiceid;
			result.projectid = this.projectid;
			result.expencetypeid = this.expencetypeid;
			result.itemdate = this.itemdate;
			result.rate = this.rate;
			result.quantity = this.quantity;
			result.total = this.total;
			result.status = this.status;
			
			return result;
		}
		
		private function propChangeHandler(e:*):void 
		{
			amount = rate * quantity;
			adjustment = total - amount;
		}
		
	}
}