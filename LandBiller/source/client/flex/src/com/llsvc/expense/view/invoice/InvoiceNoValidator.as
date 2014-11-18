package com.llsvc.expense.view.invoice
{
	import com.llsvc.domain.Invoice;
	
	import mx.collections.ArrayCollection;
	import mx.validators.ValidationResult;
	import mx.validators.Validator;

	public class InvoiceNoValidator extends Validator
	{
		public var invoices:ArrayCollection;
		public var currInvoice:Invoice;
		
		public function InvoiceNoValidator()
		{
			super();
		}
		
		override protected function doValidation(value:Object):Array 
		{
			var results:Array = super.doValidation(value);
			
			if (!isInvoiceNoUnique(value as String)) 
			{
				results.push(new ValidationResult(true, "", "notUnique", "Invoice Number is not Unique"));
			}
			
			return results;
		}
		
		private function isInvoiceNoUnique(invoiceNo:String):Boolean 
		{
			if (invoices != null && invoices.length > 0 && currInvoice != null) 
			{
				for each (var invoice:Invoice in invoices) 
				{
					if (invoice.invoiceno.toUpperCase() == invoiceNo.toUpperCase() && invoice.invoiceid != currInvoice.invoiceid) 
						return false;
				}
			}
			
			return true;
		}
		
	}
}