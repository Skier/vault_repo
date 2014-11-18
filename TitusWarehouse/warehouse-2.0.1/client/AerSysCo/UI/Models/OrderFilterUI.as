package AerSysCo.UI.Models
{
	import AerSysCo.Server.OrderFilter;
	
	[Bindable]
	public class OrderFilterUI
	{
		public var customerId:int;
        public var statusId:int;
        public var poNumber:String;
        public var confirmNumber:String;
        public var quantity:int;
        public var fromDate:Date;
        public var toDate:Date;
        
        public function populateFromOrderFilter(value:OrderFilter):void 
        {
        	this.customerId = value.customerId;
        	this.statusId = value.statusId;
        	this.poNumber = value.poNumber;
        	this.confirmNumber = value.confirmNumber;
        	this.quantity = value.quantity;
        	this.fromDate = value.fromDate;
        	this.toDate = value.toDate;
        }
        
        public function toOrderFilter():OrderFilter 
        {
        	var result:OrderFilter = new OrderFilter();
        	
        	result.customerId = this.customerId;
        	result.statusId = this.statusId;
        	result.quantity = this.quantity;
        	result.poNumber = this.poNumber;
        	result.confirmNumber = this.confirmNumber;

        	if (this.fromDate) 
	        	result.fromDate = this.fromDate;
        	else 
        		result.fromDate = getMinDate();

        	if (this.toDate) 
	        	result.toDate = this.toDate;
        	else 
        		result.toDate = getMaxDate();

        	return result;
        }
        
        private function getMinDate():Date 
        {
        	var result:Date = new Date(1900, 0);
        	return result;
        }

        private function getMaxDate():Date 
        {
        	var result:Date = new Date(2999, 0);
        	return result;
        }
	}
}