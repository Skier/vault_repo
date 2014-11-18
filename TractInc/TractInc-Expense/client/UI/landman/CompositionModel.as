package UI.landman
{
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class CompositionModel
	{
		
		public var startDate:Date;
		
		public var endDate:Date;
		
		public var compositions:ArrayCollection = new ArrayCollection();
	
		public var types:ArrayCollection;
		
		public var composition:Composition = null;
		
	}
	
}
