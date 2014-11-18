package UI.landman
{
	import weborb.data.DynamicLoadEvent;
	import App.Domain.Asset;
	import mx.collections.ListCollectionView;
	import App.Domain.Bill;
	import App.Domain.BillStatus;
	import weborb.data.ActiveCollection;
	
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
        
        public function open(bills:ActiveCollection):void {
            view.msgPanel.init(mainApp.mainApp.Model.CurrentUser);
            
        	model.bills = bills;
        	
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
        
        private function filterRejectedBills(bill:Bill):Boolean {
        	return BillStatus.BILL_STATUS_REJECTED == bill.Status
       			|| BillStatus.BILL_STATUS_CHANGED == bill.Status;
        }

        private function filterApprovedBills(bill:Bill):Boolean {
        	return !filterRejectedBills(bill)
        		&& !filterNewBills(bill)
        		&& !filterSubmittedBills(bill)
        		&& (new Date()).time > Date.parse(bill.StartDate);
        }

        private function filterNewBills(bill:Bill):Boolean {
        	return BillStatus.BILL_STATUS_NEW == bill.Status
        		&& (new Date()).time > Date.parse(bill.StartDate);
        }

        private function filterSubmittedBills(bill:Bill):Boolean {
        	return BillStatus.BILL_STATUS_SUBMITTED == bill.Status
        		|| BillStatus.BILL_STATUS_CORRECTED == bill.Status;
        }

	}
	
}
