package com.llsvc.domain
{
	import mx.collections.ArrayCollection;

	public class TractSection extends ArrayCollection
	{
		public var key:String;
		public var breakdowns:ArrayCollection;
		
		public function TractSection(source:Array=null)
		{
			super(source);
			breakdowns = new ArrayCollection();
		}
		
		public function addBreakdown(lb:LeaseBreakdown):void 
		{
/*			
			for each (var actor:DocumentActor in lb.lease.document.givers) 
			{
				var b:LeaseBreakdown = new LeaseBreakdown();
				b.actor = actor;
				breakdowns.addItem(b);
			}
*/			
		}
		
	}
}