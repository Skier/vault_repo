package com.llsvc.client.lms.view.lease.report
{
	import com.llsvc.domain.Lease;
	
	import mx.collections.ArrayCollection;
	import mx.containers.VBox;
	import mx.events.CollectionEvent;
	import mx.events.CollectionEventKind;

	public class LeasesContainer extends VBox
	{
		public function LeasesContainer()
		{
			super();
		}
		[Bindable] public var leaseStatusWidth:int;
		
		[Bindable] public var leaseNoWidth:int;
		[Bindable] public var leaseLessorWidth:int;
		[Bindable] public var leaseLesseeWidth:int;
		[Bindable] public var recordingWidth:int;
		[Bindable] public var leaseDateWidth:int;
		[Bindable] public var leaseExpDateWidth:int;

		[Bindable] public var townshipRangeWidth:int;
		[Bindable] public var sectionWidth:int;
		[Bindable] public var interestWidth:int;
		[Bindable] public var grossAcresWidth:int;
		[Bindable] public var netAcresWidth:int;
		[Bindable] public var leaseInteresWidth:int;
		[Bindable] public var leaseBurdenWidth:int;
		[Bindable] public var nriWidth:int;
		[Bindable] public var cwiWidth:int;
		[Bindable] public var burdenWidth:int;
		[Bindable] public var cnriWidth:int;
		[Bindable] public var cNetAcresWidth:int;

		private var _leases:ArrayCollection;
		[Bindable]
		public function get leases():ArrayCollection { return _leases; }
		public function set leases(value:ArrayCollection):void 
		{
			_leases = value;
			_leases.addEventListener(CollectionEvent.COLLECTION_CHANGE, leasesChangeHandler);
			refreshLeases();
		}
		
		private function leasesChangeHandler(event:CollectionEvent):void 
		{
			var i:uint;
			
		    switch (event.kind) {
		        case CollectionEventKind.ADD:
		            for (i = 0; i < event.items.length; i++) {
		            	addLeaseBox(Lease(event.items[i]));
		            }
		            break;
		
		        case CollectionEventKind.REMOVE:
		            for (i = 0; i < event.items.length; i++) {
		                removeLeaseBox(Lease(event.items[i]));
		            }
		            break;
		
		        case CollectionEventKind.RESET:
		            refreshLeases();
		            break;
		    }
		}
		
		private function addLeaseBox(lease:Lease):void 
		{
			var leaseBox:LeaseRenderer = new LeaseRenderer();
			leaseBox.percentWidth = 100;
			leaseBox.lease = lease;
			
			leaseBox.leaseStatusWidth = leaseStatusWidth;
			
			leaseBox.leaseNoWidth = leaseNoWidth;
			leaseBox.leaseLessorWidth = leaseLessorWidth;
			leaseBox.leaseLesseeWidth = leaseLesseeWidth;
			leaseBox.recordingWidth = recordingWidth;
			leaseBox.leaseDateWidth = leaseDateWidth;
			leaseBox.leaseExpDateWidth = leaseExpDateWidth;
	
			leaseBox.townshipRangeWidth = townshipRangeWidth;
			leaseBox.sectionWidth = sectionWidth;
			leaseBox.interestWidth = interestWidth;
			leaseBox.grossAcresWidth = grossAcresWidth;
			leaseBox.netAcresWidth = netAcresWidth;
			leaseBox.leaseInterestWidth = leaseInteresWidth;
			leaseBox.leaseBurdenWidth = leaseBurdenWidth;
			leaseBox.nriWidth = nriWidth;
			leaseBox.cwiWidth = cwiWidth;
			leaseBox.burdenWidth = burdenWidth;
			leaseBox.cnriWidth = cnriWidth;
			leaseBox.cNetAcresWidth = cNetAcresWidth;
			
			addChild(leaseBox);
			validateDisplayList();
		}
		
		private function removeLeaseBox(lease:Lease):void 
		{
			var leaseBox:LeaseRenderer;
			for (var i:int = 0; i < getChildren().length; i++) 
			{
				leaseBox = this.getChildAt(i) as LeaseRenderer;
				if (leaseBox.lease == lease) 
				{
					removeChildAt(i);
					return;
				}
			}
		}
		
		private function refreshLeases():void 
		{
			removeAllChildren();
			
			for each (var lease:Lease in leases) 
			{
				addLeaseBox(lease);
			}
		}
	}
}