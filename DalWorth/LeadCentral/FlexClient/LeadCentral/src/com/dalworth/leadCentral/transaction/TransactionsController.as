package com.dalworth.leadCentral.transaction
{
	import com.dalworth.leadCentral.service.BaseService;
	import com.dalworth.leadCentral.service.BillingService;
	
	import mx.controls.Alert;
	import mx.core.UIComponent;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import mx.rpc.events.ResultEvent;
	
	public class TransactionsController
	{
		private var view:UIComponent;
		private var model:TransactionsModel;
		
		public function TransactionsController(view:UIComponent)
		{
			this.model = TransactionsModel.getInstance();
			this.view = view;
		}
/* 		
		public function refreshTransactions():void 
		{
			BillingService.getInstance().getAllTransactions( 
				new Responder(
					function(event:ResultEvent):void 
					{
						var result:Array = event.result as Array;
						model.transactions.source = result;
						model.transactionsCount = result.length;
						refreshPaging();
					},
					function(event:FaultEvent):void 
					{
					}))
		}
 */		
		public function refreshTransactions():void 
		{
			BillingService.getInstance().getTransactionsCount( 
				new Responder(
					function(event:ResultEvent):void 
					{
						model.transactionsCount = event.result as int;
						refreshPaging();
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
		}
		
		public function refreshCurrentBalance():void 
		{
			BaseService.getInstance().getCurrentBalance(
				new mx.rpc.Responder(
					function (event:ResultEvent):void 
					{
						CurrentBalanceView.balance = event.result as Number;
					}, 
					function (event:FaultEvent):void 
					{
					}));
		}
		
        public function goFirst():void 
        {
        	model.currentPage = 1;
        	loadCurrentPage();
        }
        
        public function goPrev():void 
        {
        	if (model.currentPage > 1)
        	{
        		model.currentPage--;
        		loadCurrentPage();
        	}
        }
        
        public function goNext():void 
        {
        	if (model.currentPage < model.pages.length)
        	{
        		model.currentPage++;
        		loadCurrentPage();
        	}
        }
        
        public function goLast():void 
        {
        	model.currentPage = model.pages.length;
        	loadCurrentPage();
        }
        
        public function setPageSize(pgSize:int):void 
        {
        	model.pageSize = pgSize;
        	refreshPaging();
        }
        
        public function setCurrentPage(pgNum:int):void 
        {
        	model.currentPage = pgNum;
        	refreshPaging();
        }

        private function refreshPaging():void 
        {
            var pagesCount:int = Math.ceil(model.transactionsCount / model.pageSize);

            var pages:Array = new Array();
            for (var i:int = 0; i < pagesCount; i++) 
            {
                var name:String = int(i + 1).toString();
                pages.push({label:name, data:i});
            }
            
            model.pages.source = pages;
            
            var pg:int = model.currentPage;

            if (model.currentPage > pagesCount)
            {
                model.currentPage = pagesCount;
            } else if (model.currentPage < 1)
            {
                model.currentPage = 1;
            } else 
            {
            	model.currentPage = 0;
            	model.currentPage = pg;
            } 
            
            loadCurrentPage();
        }
        
        private function loadCurrentPage():void 
        {
        	var offset:int = (model.currentPage - 1) * model.pageSize;
        	var limit:int = model.pageSize;
        	
			BillingService.getInstance().getTransactions(offset, limit,
				new Responder(
					function(event:ResultEvent):void 
					{
						model.transactions.source = event.result as Array;
					},
					function(event:FaultEvent):void 
					{
						Alert.show(event.fault.message);
					}))
        }
        
	}
}