package com.llsvc.client.lms.view.tract
{
	import com.llsvc.domain.DocumentActor;
	import com.llsvc.domain.LeaseBreakdown;
	import com.llsvc.domain.LeaseTract;
	import com.llsvc.domain.LeaseTractQQ;
	import com.llsvc.domain.Tract;
	
	import mx.binding.utils.ChangeWatcher;
	import mx.collections.ArrayCollection;

	[Bindable]
	public class TractSection
	{
		public var section:String = "";
		public var tract:String = "";
		
		public var range:TractRange;
		
		public var grossAC:Number = 0;
		public var netAC:Number = 0;
		public var totalInterest:Number = 0;
		
		public var leaseTract:LeaseTract;
		public var breakdowns:ArrayCollection;
		
		public var geoTracts:ArrayCollection;
		
		public var useBreakdowns:Boolean = true;
		public var expanded:Boolean;
		
		public function get key():String 
		{
	        return "Section " + section + ": " + tract;
		}
		
		public function TractSection(r:TractRange)
		{
			leaseTract = new LeaseTract();
			leaseTract.lease = r.lease;
			
			checkoutBreakdowns();
			
			geoTracts = new ArrayCollection();
		}
		
		public function checkoutBreakdowns():void 
		{
			breakdowns = new ArrayCollection();
			for each (var actor:DocumentActor in leaseTract.lease.document.givers) 
			{
				var b:LeaseBreakdown = new LeaseBreakdown();
				b.actor = actor;
				b.tract = leaseTract;
				b.fromDepth = "SURFACE";
				b.toDepth = "ALL";
				b.product = "ALL";
				b.formation = "ALL";
				
				ChangeWatcher.watch(b, "interest", interesChanged);
				breakdowns.addItem(b);
			}
			
			if (leaseTract.breakdown.length == 0)
				useBreakdowns = false; 

			for each (var lb:LeaseBreakdown in leaseTract.breakdown) 
			{
				setBreakdown(lb);
			}
		}
		
		private function setBreakdown(lb:LeaseBreakdown):void 
		{
			for each (var b:LeaseBreakdown in breakdowns) 
			{
				if (lb.actor.name == b.actor.name) 
				{
					b.actor = lb.actor;
					b.interest = lb.interest;
					b.product = lb.product;
					b.fromDepth = lb.fromDepth;
					b.toDepth = lb.toDepth;
					b.formation = lb.formation;
				}
			}
		}
		
		public function commitBreakdowns():void 
		{
			leaseTract.breakdown.removeAll();
			if (useBreakdowns) 
			{
				for each (var b:LeaseBreakdown in breakdowns) 
				{
					if (b.interest > 0) 
					{
						b.tract = leaseTract;
						leaseTract.breakdown.addItem(b);
					}
				}
			}
		} 
		
		public function checkOutTracts():void 
		{
			geoTracts.removeAll();
			for each (var tractQQ:LeaseTractQQ in leaseTract.qqs) 
			{
				geoTracts.addItem(tractQQ.tract);
			}
		}
		
		public function checkInTracts():void 
		{
			leaseTract.qqs.removeAll();
			for each (var tract:Tract in geoTracts) 
			{
				var qq:LeaseTractQQ = new LeaseTractQQ();
				qq.leaseTract = leaseTract;
				qq.tract = tract;
				leaseTract.qqs.addItem(qq);
			}
		} 
		
		private function interesChanged(e:*):void 
		{
			totalInterest = 0;
			
			for each (var b:LeaseBreakdown in breakdowns) 
			{
				totalInterest += (b.interest * 100);
			}
		}
		
	}
}
