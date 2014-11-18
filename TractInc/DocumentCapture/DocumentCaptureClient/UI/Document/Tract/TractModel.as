package UI.Document.Tract
{
	import Domain.Tract;
	import Domain.Document;
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class TractModel
	{

		public var tract:Tract;
		
		public var exceptions:ArrayCollection = new ArrayCollection();
		
		public var parentCollection:ArrayCollection;
		
	}
}