package UI.crew
{
	import flash.net.FileReference;
	import mx.rpc.remoting.RemoteObject;
	import mx.rpc.events.FaultEvent;
	import mx.controls.Alert;
	import mx.managers.PopUpManager;
	import flash.events.MouseEvent;
	import flash.net.URLRequest;
	import flash.net.URLVariables;
	import mx.utils.UIDUtil;
	import mx.rpc.events.ResultEvent;
	import flash.events.Event;
	import flash.events.ProgressEvent;
	import flash.events.IOErrorEvent;
	import flash.net.navigateToURL;
	import UI.landman.Composition;
	import App.Service.CrewChiefService;
	import mx.rpc.Responder;
	import mx.collections.ArrayCollection;
	import App.Entity.BillItemAttachmentDataObject;
	import mx.events.DynamicEvent;
	import App.Entity.BillDataObject;
	import App.Entity.BillItemDataObject;
	import common.TypesRegistry;
	
	[Bindable]
	public class AttachmentsController
	{
		
		private var view:AttachmentsView;

		public var model:AttachmentsModel;
		
		private var attachments:ArrayCollection;
		
		public function AttachmentsController(view:AttachmentsView, bill:BillDataObject)
		{
			this.view = view;
			model = new AttachmentsModel(bill);
			
			CrewChiefService.getInstance().getBillAttachments(bill.BillId, new Responder(onLoaded, onFault));
		}
		
		public function onClickClose():void 
		{
			PopUpManager.removePopUp(view);
		}
		
		private function onLoaded(event:ResultEvent):void 
		{
			attachments = new ArrayCollection(event.result as Array);
			
			var compositionsHash:Array = new Array();
			
			for each (var attachmentInfo:BillItemAttachmentDataObject in attachments) {
				if (0 == attachmentInfo.BillItemInfo.BillItemCompositionId) {
					attachmentInfo.amount = attachmentInfo.BillItemInfo.BillRate * attachmentInfo.BillItemInfo.Qty;
					attachmentInfo.type = TypesRegistry.instance.getBillItemTypeById(attachmentInfo.BillItemInfo.BillItemTypeId).Name;
					attachmentInfo.date = attachmentInfo.BillItemInfo.BillingDate;
					model.itemAttachments.addItem(attachmentInfo);
				} else {
					if (null == compositionsHash[attachmentInfo.BillItemInfo.BillItemCompositionId]) {
						attachmentInfo.amount = attachmentInfo.CompositionInfo.Amount;
						attachmentInfo.type = TypesRegistry.instance.getBillItemTypeById(attachmentInfo.BillItemInfo.BillItemTypeId).Name;
						attachmentInfo.description = attachmentInfo.CompositionInfo.Description;
						model.compositeAttachments.addItem(attachmentInfo);
						compositionsHash[attachmentInfo.BillItemInfo.BillItemCompositionId] = attachmentInfo;
					}
				}
			}
			
			model.isLoaded = true;
		}
		
		private static function onFault(event:FaultEvent):void 
		{
			Alert.show(event.fault.message);
		}
			
	}
	
}
