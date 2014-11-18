package UI.landman
{
	
	import mx.collections.ListCollectionView;
	import mx.collections.ArrayCollection;
	import App.Entity.BillDataObject;
	
	public class LandmanHomeController
	{
		
        [Bindable]
        public var model: LandmanHomeModel = new LandmanHomeModel();
        
        [Bindable]
        public var mainApp: ExpenseController;
        private var view: LandmanHomeView;

        public function LandmanHomeController(view: LandmanHomeView, parent:ExpenseController): void {
            this.view = view;
            mainApp = parent;
        }
        
        public function open():void {
            view.msgPanel.init(mainApp.mainApp.Model.CurrentUser.UserId);
            
        	model.bills = new ArrayCollection(mainApp.model.landmanData.Bills);
        	
        	model.rejectedBills = new ListCollectionView(model.bills);
        	model.rejectedBills.filterFunction = filterRejectedBills;
        	model.rejectedBills.refresh();
        	
        	model.approvedBills = new ListCollectionView(model.bills);
        	model.approvedBills.filterFunction = filterApprovedBills;
        	model.approvedBills.refresh();
        	
        	model.newBills = new ListCollectionView(model.bills);
        	model.newBills.filterFunction = filterNewBills;
        	model.newBills.refresh();
        	
        	model.submittedBills = new ListCollectionView(model.bills);
        	model.submittedBills.filterFunction = filterSubmittedBills;
        	model.submittedBills.refresh();
        	
        	view.enabled = true;
        }
        
        private function filterRejectedBills(bill:BillDataObject):Boolean {
        	return BillDataObject.BILL_STATUS_REJECTED == bill.Status
       			|| BillDataObject.BILL_STATUS_CHANGED == bill.Status;
        }

        private function filterApprovedBills(bill:BillDataObject):Boolean {
        	return !filterRejectedBills(bill)
        		&& !filterNewBills(bill)
        		&& !filterSubmittedBills(bill)
        		&& (new Date()).time > Date.parse(bill.StartDate);
        }

        private function filterNewBills(bill:BillDataObject):Boolean {
        	return BillDataObject.BILL_STATUS_NEW == bill.Status
        		&& (new Date()).time > Date.parse(bill.StartDate);
        }

        private function filterSubmittedBills(bill:BillDataObject):Boolean {
        	return BillDataObject.BILL_STATUS_SUBMITTED == bill.Status
        		|| BillDataObject.BILL_STATUS_CORRECTED == bill.Status;
        }

	}
	
}
