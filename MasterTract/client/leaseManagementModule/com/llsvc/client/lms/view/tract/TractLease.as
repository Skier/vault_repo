package com.llsvc.client.lms.view.tract
{
	import com.llsvc.domain.Lease;
	import com.llsvc.domain.LeaseTract;
	
	import mx.collections.ArrayCollection;
	import mx.events.CollectionEvent;
	
	[Bindable]
	public class TractLease 
	{
    	public var ranges:ArrayCollection;
    
		public function TractLease()
		{
	    	this.ranges = new ArrayCollection();
		}

    	private var _lease:Lease;
    	public function get lease():Lease { return _lease }
    	public function set lease(value:Lease):void 
    	{
    		_lease = value;
    		
    		if (_lease) {
    			_lease.tracts.addEventListener("tractsReloaded", tractsChangeHandler);
    			_lease.document.givers.addEventListener(CollectionEvent.COLLECTION_CHANGE, giversChanged);
    		}
    	}
    
    	private function tractsChangeHandler(e:*):void 
    	{
    		organizeTracts();
    	}
    
    	private function giversChanged(e:*):void 
    	{
    		for each (var r:TractRange in ranges) 
    		{
    			for each (var s:TractSection in r.sections) 
    			{
    				s.commitBreakdowns();
    				s.checkoutBreakdowns();
    			}
    		} 
    	} 
    
	    private function organizeTracts():void 
	    {
	    	ranges.removeAll();
	    	for each (var lt:LeaseTract in lease.tracts) 
	    	{
	    		lt.lease = this.lease;
	    		
	    		var range:TractRange = getRange(lt);
	    		if (range == null) 
	    		{
	    			range = new TractRange(lease);
	    			range.township = lt.township;
	    			range.range = lt.range;
	    			ranges.addItem(range);
	    		}
	    		
	    		range.addTract(lt);
	    	}
	    }
	    
	    private function getRange(lt:LeaseTract):TractRange 
	    {
	    	for each (var r:TractRange in ranges) 
	    	{
	    		if (r.township == lt.township && r.range == lt.range) 
	    		{
	    			return r;
	    		}
	    	}
	    	return null;
	    }
	}
}