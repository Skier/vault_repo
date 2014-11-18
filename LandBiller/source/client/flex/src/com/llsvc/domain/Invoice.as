package com.llsvc.domain
{
	import com.llsvc.domain.vo.invoiceVO;
	
	import mx.binding.utils.ChangeWatcher;
	import mx.collections.ArrayCollection;
	import mx.events.CollectionEvent;
	import mx.events.CollectionEventKind;

	[Bindable]
	public class Invoice extends invoiceVO
	{
		public static const STATUS_NEW:String = "NEW";
		public static const STATUS_SUBMITTED:String = "SUBMITTED";
		public static const STATUS_REJECTED:String = "REJECTED";
		public static const STATUS_PAID:String = "PAID";
		
		public var isLoading:Boolean;
		public var isLoaded:Boolean;
		public var isDirty:Boolean;
		
		public var user:User;
		public var expenceItems:ArrayCollection;
		public var notes:ArrayCollection;
		
		public function Invoice()
		{
			super();
			
			expenceItems = new ArrayCollection();
			notes = new ArrayCollection();
			
			expenceItems.addEventListener(CollectionEvent.COLLECTION_CHANGE, itemsChangeHandler);
		}
		
		public static function getStates():ArrayCollection 
		{
			var result:ArrayCollection = new ArrayCollection();
			
			result.addItem({data:STATUS_NEW});
			result.addItem({data:STATUS_SUBMITTED});
			result.addItem({data:STATUS_REJECTED});
			result.addItem({data:STATUS_PAID});

			return result;
		}
		
		private var _client:Client;
		public function get client():Client { return _client; }
		public function set client(value:Client):void 
		{
			_client = value;
			if (_client != null) 
			{
				updateClientStr();
				ChangeWatcher.watch(_client, "name", clientNameChangeHandler);
			}
		}
		
		private function clientNameChangeHandler(e:*):void 
		{
			updateClientStr();
		}

		public var clientStr:String; 

		public function updateClientStr():void 
		{
			if (client != null) 
			{
				clientStr = client.name;
			} else 
			{
				clientStr = "n/a";
			}
		}
		
		public function get dateStr():String 
		{
			var mm:String = (invoicedate.month + 1).toString();
				mm = mm.length > 1 ? mm : "0" + mm;
			var dd:String = invoicedate.date.toString();
				dd = dd.length > 1 ? dd : "0" + dd;
			var yyyy:String = invoicedate.fullYear.toString();
			
			return (yyyy + "/" + mm + "/" + dd);
		}
		
		public function get dateFromStr():String 
		{
			var mm:String = (startdate.month + 1).toString();
				mm = mm.length > 1 ? mm : "0" + mm;
			var dd:String = startdate.date.toString();
				dd = dd.length > 1 ? dd : "0" + dd;
			var yyyy:String = startdate.fullYear.toString();
			
			return (yyyy + "/" + mm + "/" + dd);
		}
		
		public function get dateToStr():String 
		{
			var mm:String = (enddate.month + 1).toString();
				mm = mm.length > 1 ? mm : "0" + mm;
			var dd:String = enddate.date.toString();
				dd = dd.length > 1 ? dd : "0" + dd;
			var yyyy:String = enddate.fullYear.toString();
			
			return (yyyy + "/" + mm + "/" + dd);
		}
		
		public function updateFields(value:invoiceVO):void 
		{
			if (value == null)
				value = new invoiceVO(); 
			
			this.invoiceid = value.invoiceid;
			this.clientid = value.clientid;
			this.userid = value.userid;
			this.invoicedate = value.invoicedate;
			this.startdate = value.startdate;
			this.enddate = value.enddate;
			this.amount = value.amount;
			this.adjustment = value.adjustment;
			this.total = value.total;
			this.status = value.status;
			this.invoiceno = value.invoiceno;
		}
		
		public function toVO():invoiceVO 
		{
			var result:invoiceVO = new invoiceVO();
			
			result.invoiceid = this.invoiceid;
			result.clientid = this.clientid;
			result.userid = this.userid;
			result.invoicedate = this.invoicedate;
			result.startdate = this.startdate;
			result.enddate = this.enddate;
			result.amount = this.amount;
			result.adjustment = this.adjustment;
			result.total = this.total;
			result.status = this.status;
			result.invoiceno = this.invoiceno;
			
			return result;
		}
		
		private function recalculateTotals():void 
		{
			var a:Number = 0;
			var t:Number = 0;
			
			for each (var item:InvoiceItem in expenceItems) 
			{
				a += item.amount;
				t += item.total;
			}
			
			amount = a;
			total = t;
			adjustment = total - amount;
		}
		
		private function itemsChangeHandler(event:CollectionEvent):void 
		{
			if (event.kind == CollectionEventKind.ADD) 
			{
				for each (var item:InvoiceItem in event.items) 
				{
					ChangeWatcher.watch(item, "amount", invoiceItemChangeHandler);
					ChangeWatcher.watch(item, "adjustment", invoiceItemChangeHandler);
					ChangeWatcher.watch(item, "total", invoiceItemChangeHandler);
				}
			}
			
			recalculateTotals();
		}
		
		private function invoiceItemChangeHandler(e:*):void 
		{
			recalculateTotals();
		} 
		
	}
}