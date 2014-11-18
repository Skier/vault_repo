package UI.landman
{
	import flash.events.MouseEvent;
	import mx.events.CloseEvent;
	import mx.controls.Alert;
	import mx.collections.ArrayCollection;
	import mx.events.CollectionEvent;
	import common.TypesRegistry;
	import common.StatusesRegistry;
	import mx.collections.ListCollectionView;
	import mx.skins.halo.ActivatorSkin;
	import mx.events.DynamicEvent;
	import util.DateUtil;
	import mx.rpc.Responder;
	import mx.rpc.events.FaultEvent;
	import App.Entity.BillItemCompositionDataObject;
	import App.Entity.BillItemTypeDataObject;
	
	public class CompositionController
	{
		
        public var view:CompositionView;
        
        [Bindable]
        public var model:CompositionModel = new CompositionModel();
        
        [Bindable]
        public var diaryModel:DiaryModel = new DiaryModel();
        
        [Bindable]
        public var parentController:DiaryController;
		
        public function CompositionController(view: CompositionView, pc:DiaryController): void {
            this.view = view;
            this.parentController = pc;
        }
        
        public function open(diaryModel:DiaryModel):void {
        	this.diaryModel = diaryModel;
        	
        	model.types = TypesRegistry.instance.billItemTypes;
        	
	   		processCompositions();
        }
        
        private function processCompositions():void {
       		for each (var compositionRecord:BillItemCompositionDataObject in diaryModel.bill.Compositions) {
       			var composition:Composition = new Composition();
       			composition.compositeRecord = compositionRecord;
       			model.compositions.addItem(composition);
       		}
        }
        
        public function addCompositeItem(itemType:BillItemTypeDataObject, description:String, amount:Number):void {
        	var oldComposition:Composition = null;
        	if (null == model.composition) {
        		model.composition = new Composition();
	        	model.composition.compositeRecord = new BillItemCompositionDataObject();
	        	model.composition.itemsCount = 0;
	        	model.composition.compositeRecord.BillItems = new Array();
	        	model.composition.compositeRecord.BillId = parentController.Model.bill.BillId;
        	} else {
        		oldComposition = model.composition;
        	}
        	
        	model.composition.compositeRecord.BillItemTypeId = itemType.BillItemTypeId;
        	model.composition.compositeRecord.Description = description;
        	model.composition.amount = amount;
        	model.composition.isProceeded = false;
        	
        	if (null == oldComposition) {
        		completeCreation();
        	} else {
        		
        	}
        }
        
        private function completeCreation():void {
        	parentController.Model.compositions.addItem(model.composition);
        	parentController.view.dgCompositeItems.selectedIndex = parentController.Model.compositions.getItemIndex(model.composition);
        	parentController.buildCalendar();
        }
        
	}
	
}
