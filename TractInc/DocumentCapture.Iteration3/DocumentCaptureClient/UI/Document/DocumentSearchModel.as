package UI.Document
{
	import mx.collections.ArrayCollection;
	import Domain.Document;
	
	[Bindable]
	public class DocumentSearchModel
	{
		public var Count:int;
		public var CurrentDocument:Document;
		public var Items:ArrayCollection = new ArrayCollection();
	}
}