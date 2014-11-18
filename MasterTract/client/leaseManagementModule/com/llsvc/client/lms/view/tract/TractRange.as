package com.llsvc.client.lms.view.tract
{
	import com.llsvc.domain.Lease;
	import com.llsvc.domain.LeaseTract;
	
	import mx.collections.ArrayCollection;

	[Bindable]
	public class TractRange
	{
		public var township:String = "";
		public var range:String = "";
		
		public var lease:Lease;
		public var sections:ArrayCollection;

		public var needNewSection:Boolean = false;

		public function TractRange(lease:Lease)
		{
			this.lease = lease;
			sections = new ArrayCollection();
		}
		
		public function get key():String 
		{
	        return "TWN " + township + " RNG " + range;
		}
		
		public function getSection(lt:LeaseTract):TractSection 
		{

			for each (var s:TractSection in sections) 
			{
				if (s.section == lt.section && s.tract == lt.tract) 
				{
					return s;
				}
			}
			return null;
		}
		
		public function addTract(lt:LeaseTract):void 
		{
			//var s:TractSection = getSection(lt);
			//if (s == null) 
			//{
				var s:TractSection = new TractSection(this);
				s.leaseTract = lt;
				s.section = lt.section;
				s.tract = lt.tract;
				s.grossAC = lt.grossAcres;
				s.netAC = lt.netAcres;
				
				sections.addItem(s);
			//}

			s.checkOutTracts();
			s.checkoutBreakdowns();
		}
	}
}