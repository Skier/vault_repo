package com.llsvc.domain
{
	import mx.collections.ArrayCollection;

	public class TractRange extends ArrayCollection
	{
		public var key:String;
		public var sections:ArrayCollection;

		public function TractRange(source:Array=null)
		{
			super(source);
			sections = new ArrayCollection();
		}
		
		public function getSection(key:String):TractSection 
		{
			for each (var s:TractSection in sections) 
			{
				if (s.key == key) 
				{
					return s;
				}
			}
			
			return null;
		}
		
		public function addTract(b:LeaseBreakdown):void 
		{
/*			
			var s:TractSection = getSection(b.section);
			if (s == null) 
			{
				s = new TractSection();
				s.key = b.section;
				sections.addItem(s);
			}
			
			s.addBreakdown(b);
*/			
		}
	}
}