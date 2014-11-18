package UI.landman
{
	
    import mx.collections.ArrayCollection;
    import mx.events.PropertyChangeEvent;
    import mx.binding.utils.ChangeWatcher;
    import App.Domain.BillItemComposition;
    import App.Domain.BillItem;
    import App.Domain.BillItemStatus;
    import util.DateUtil;
    import common.StatusesRegistry;
    import App.Entity.BillItemDataObject;
    import App.Entity.BillItemCompositionDataObject;
    
    [Bindable]
	public class Composition
	{
		
		public var compositeRecord:BillItemCompositionDataObject;
		
    	public var days:ArrayCollection = new ArrayCollection();
    	
    	public var temporaryItems:ArrayCollection = new ArrayCollection();
    	
    	public var itemsCount:int = 0;
    	
    	private var _isProceeded:Boolean = true;
    	public function get isProceeded():Boolean {
    		return _isProceeded;
    	}
    	public function set isProceeded(value:Boolean):void {
    		_isProceeded = value;
    	}
    	
    	public var isLoading:Boolean = true;
    	
    	public var isActive:Boolean = false;
    	
    	public var isChanged:Boolean = false;
    	
    	private var _isEnabled:Boolean = false;
    	public function get isEnabled():Boolean {
    		return _isEnabled;
    	}
    	public function set isEnabled(value:Boolean):void {
    		_isEnabled = value;
    	}
    	
    	private var _isRejected:Boolean = false;
    	public function get isRejected():Boolean {
    		return _isRejected;
    	}
    	public function set isRejected(value:Boolean):void {
    		if (value) { isChanged = true; }
    		_isRejected = value;
    	}
    	
    	private var _isEditable:Boolean = true;
    	public function get isEditable():Boolean {
    		return _isEditable;
    	}
    	public function set isEditable(value:Boolean):void {
    		_isEditable = value;
    	}
    	
    	public function get amount():Number { return compositeRecord.Amount; }
    	public function set amount(value:Number):void 
    	{
    		compositeRecord.Amount = value;
    		
    		updateAmounts();
    	}
    	
    	public function addItem(item:DailyEventGroup):void 
    	{
    		if (!isEnabled) {
	    		var hasEnabled:Boolean = false;
				for each (var billItem:BillItemDataObject in compositeRecord.BillItems) {
					if (BillItemDataObject.BILL_ITEM_STATUS_REJECTED == billItem.Status) {
						isRejected = true;
					}
					
    				if (billItem.isBillItemEditableOld()) {
    					hasEnabled = true;
    					break;
    				}
				}
				
				if (hasEnabled || (0 == compositeRecord.BillItems.length)) {
					isEnabled = true;
				}
    		}
    		
    		item.composition = this;
    		days.addItem(item);
    		updateAmounts();
    		ChangeWatcher.watch(item, ["selected"], onItemSelectionChange);
    	}
    	
    	private function onItemSelectionChange(evt:PropertyChangeEvent):void 
    	{
    		if (!isLoading && isActive) {
    			if (isProceeded) {
    				isChanged = true;
    				isProceeded = false;
    			}
    		}
			updateAmounts();        		
    	}
    	
		private function updateAmounts():void 
    	{
    		if (!isActive) {
    			return;
    		}
    		
			var countSelected:int = 0;
       		
       		var day:DailyEventGroup;
       		
       		for each (day in days) {
    			if (day.selected) {
       				countSelected++;
       			}
       		}
       		
			var count:int = 0;
			var sum:Number = 0;
			
       		for each (day in days) {
       			if (day.selected) {

       				count++;

       				if (count == countSelected) {
    					day.compositeAmount = amount - sum;
       				} else {
	       				day.compositeAmount = amount / countSelected;
	       				sum += day.compositeAmount;
       				}

    			} else {
    				day.compositeAmount = 0;
    			}
    		}
    		
    		itemsCount = count;
    		amountString = '';
    	}
    	
        public function storeTemporaryItems():void {
        	isLoading = true;
        	
    		temporaryItems = new ArrayCollection();
   			for each (var day:DailyEventGroup in days) {
   				for each (var project:DailyEvent in day.events) {
   					if (project.selected) {
						temporaryItems.addItem(project);
   					}
					
    				project.selected = false;
   				}

				day.composition = null;

   				day.isCompositionEditable = false;
    		}
    		isActive = false;
    		isLoading = false;
        }
        
        public function restoreTemporaryItems():void {
        	isLoading = true;
        	var day:DailyEventGroup;
        	
        	var project:DailyEvent;
        	if (isProceeded) {
        		for each (day in days) {
        			day.composition = this;
        			
   					for each (project in day.events) {
   						for each (var item:BillItemDataObject in project.billItems) {
   							for each (var newItem:BillItemDataObject in compositeRecord.BillItems) {
   								if (newItem.BillItemId == item.BillItemId) {
   									project.selected = true;
   								}
   							}
   						}
   						project.isCompositionEditable = isEditable;
   					}
   					
        			day.isCompositionEditable = isEditable;
        		}
        	} else {
				for each (project in temporaryItems) {
    				project.selected = true;
       			}
       			
        		for each (day in days) {
        			day.composition = this;
        			
   					for each (project in day.events) {
   						project.isCompositionEditable = isEditable;
   					}
   					
        			day.isCompositionEditable = isEditable;
        		}
       		}
       		
        	isActive = true;
       		updateAmounts();
    		isLoading = false;
        }
        
        public function get amountString():String {
        	return (Math.round(compositeRecord.Amount * 100) / 100).toFixed(2);
        }
        
        public function set amountString(value:String):void {
        }
        
	}
	
}
