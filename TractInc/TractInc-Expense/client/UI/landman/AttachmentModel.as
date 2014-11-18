package UI.landman
{
	
	import mx.collections.ArrayCollection;
	
	[Bindable]
	public class AttachmentModel
	{
		
		public var items:ArrayCollection;
		
		public var compositeItems:ArrayCollection = new ArrayCollection();
		
		public var assignmentsByIdHash:Array;
		
	}
	
}
