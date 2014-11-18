package UI.landman
{
	
    import mx.collections.ArrayCollection;
    import common.StatusesRegistry;
    import mx.rpc.Responder;
    import mx.rpc.events.FaultEvent;
    import mx.controls.Alert;
    import mx.managers.PopUpManager;
    import App.Entity.BillItemDataObject;

    [Bindable]
    public class AttachmentController
    {
        
        public var view: AttachmentView;
        public var Model: AttachmentModel = new AttachmentModel();
        public var mainApp: SubmitController;
        public var mainModel: SubmitModel;
        
        public function AttachmentController(view:AttachmentView, parent:SubmitController): void {
            this.view = view;
            mainApp = parent;
            mainModel = mainApp.Model;
        }
        
        public function init(items:Array, compositions:Array):void {
   			Model.compositeItems = new ArrayCollection(compositions);
        	Model.items = new ArrayCollection(items);
        	Model.items.filterFunction = itemsFilter;
        	Model.items.refresh();
        }
        
        private function itemsFilter(item:Object):Boolean {
        	return (null == BillItemDataObject(item).AttachmentInfo);
        }
        
        public function onCancelClick():void {
        	view.onClose();
        }

        public function onSubmitClick():void {
        	Model.items.refresh();
        	for each (var item:BillItemDataObject in Model.items) {
        		item.fromTempFields();
        		
        		if (null == item.AttachmentInfo) {
        			return;
        		}
        	}
        	
        	view.responder();
        	view.onClose();
        }
        
    }

}
