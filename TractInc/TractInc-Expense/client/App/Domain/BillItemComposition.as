package App.Domain
{     	
    import App.Domain.Codegen.*;
    import weborb.data.DynamicLoadEvent;
    import weborb.data.ActiveCollection;
    
    [Bindable]
    [RemoteClass(alias="TractInc.Expense.Domain.BillItemComposition")]
    public dynamic class BillItemComposition extends _BillItemComposition
    {
    	
    	public function removeAttachments():void {
    		if (this.RelatedBillItem.IsLoaded) {
    			processBillItems();
    		} else {
    			this.RelatedBillItem.addEventListener("loaded", onBillItemsLoaded);
    		}
    	}
    	
    	private function onBillItemsLoaded(evt:DynamicLoadEvent):void {
    		this.RelatedBillItem.removeEventListener("loaded", onBillItemsLoaded);
    		processBillItems();
    	}
    	
    	private function processBillItems():void {
    		for each (var item:BillItem in this.RelatedBillItem) {
    			if (item.RelatedBillItemAttachment.IsLoaded) {
    				processRemoveAttachments(item.RelatedBillItemAttachment);
    			} else {
    				item.RelatedBillItemAttachment.addEventListener("loaded", onBillItemAttachmentsLoaded);
    			}
    		}
    	}
    	
    	private function onBillItemAttachmentsLoaded(evt:DynamicLoadEvent):void {
    		var attachments:ActiveCollection = ActiveCollection(evt.data);
    		attachments.removeEventListener("loaded", onBillItemAttachmentsLoaded);
    		processRemoveAttachments(attachments);
    	}
    	
    	private function processRemoveAttachments(attachments:ActiveCollection):void {
    		for each (var attachment:BillItemAttachment in attachments) {
    			attachment.IsDeleted = true;
    			attachments.removeItemAt(attachments.getItemIndex(attachment));
    		}
    	}
    	
    }

}
